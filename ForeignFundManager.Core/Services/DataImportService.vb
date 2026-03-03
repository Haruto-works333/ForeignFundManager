Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Utils
Imports ForeignFundManager.Core.Validators
Imports ForeignFundManager.Data.Infrastructure
Imports ForeignFundManager.Data.Repositories
Imports NLog

Namespace Services
    Public Class DataImportService

        Private ReadOnly _fundRepo As New FundRepository()
        Private ReadOnly _currencyRepo As New CurrencyRepository()
        Private ReadOnly _rateRepo As New ExchangeRateRepository()
        Private ReadOnly _calRepo As New CalendarRepository()
        Private ReadOnly _navRepo As New NavHistoryRepository()
        Private ReadOnly _importLogRepo As New ImportLogRepository()
        Private ReadOnly _importDetailRepo As New ImportLogDetailRepository()
        Private ReadOnly _navValidator As New NavDataValidator()
        Private ReadOnly _rateValidator As New ExchangeRateValidator()
        Private ReadOnly _calValidator As New CalendarValidator()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function Import(importType As String, filePath As String) As ImportLog
            _logger.Info($"Import started: type={importType}, file={filePath}")

            Dim fileError = CsvHelper.ValidateFile(filePath)
            If Not String.IsNullOrEmpty(fileError) Then
                Throw New BusinessException(fileError, "FILE_VALIDATION")
            End If

            Dim rows = CsvHelper.ReadCsv(filePath)
            If rows.Count < 2 Then
                Throw New BusinessException(MessageConstants.MsgNoDataRows, "NO_DATA")
            End If

            Dim expectedHeaders As String()
            Select Case importType
                Case AppConstants.ImportTypeNav : expectedHeaders = AppConstants.NavCsvHeaders
                Case AppConstants.ImportTypeRate : expectedHeaders = AppConstants.RateCsvHeaders
                Case AppConstants.ImportTypeCalendar : expectedHeaders = AppConstants.CalendarCsvHeaders
                Case Else
                    Throw New BusinessException($"不明な取込種別: {importType}", "UNKNOWN_TYPE")
            End Select

            Dim headerRow = rows(0)
            If headerRow.Length <> expectedHeaders.Length OrElse
               Not headerRow.Zip(expectedHeaders, Function(a, b) a.Trim().Equals(b, StringComparison.OrdinalIgnoreCase)).All(Function(x) x) Then
                Throw New BusinessException(MessageConstants.MsgHeaderMismatch, "HEADER_MISMATCH")
            End If

            Dim dataRows = rows.Skip(1).ToList()

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()

                Dim now = DateTime.Now
                Dim userName = AppConfig.UserName
                Dim importLog As New ImportLog()
                importLog.ImportType = importType
                importLog.FileName = System.IO.Path.GetFileName(filePath)
                importLog.ImportStartedAt = now
                importLog.ImportStatus = AppConstants.ImportStatusRunning
                importLog.CreatedAt = now
                importLog.CreatedBy = userName
                importLog.UpdatedAt = now
                importLog.UpdatedBy = userName

                Dim importLogId = _importLogRepo.Insert(importLog, conn)
                importLog.ImportLogId = importLogId

                Try
                    Select Case importType
                        Case AppConstants.ImportTypeNav
                            ImportNavData(dataRows, importLogId, conn, userName)
                        Case AppConstants.ImportTypeRate
                            ImportExchangeRate(dataRows, importLogId, conn, userName)
                        Case AppConstants.ImportTypeCalendar
                            ImportCalendar(dataRows, importLogId, conn, userName)
                    End Select

                    _importLogRepo.UpdateResult(importLogId, DateTime.Now, dataRows.Count, dataRows.Count, 0, 0,
                                                AppConstants.ImportStatusSuccess, conn)
                    importLog.ImportStatus = AppConstants.ImportStatusSuccess
                    importLog.TotalCount = dataRows.Count
                    importLog.SuccessCount = dataRows.Count
                    _logger.Info($"Import completed: {importType}, {dataRows.Count} records")

                Catch ex As BusinessException
                    _importLogRepo.UpdateResult(importLogId, DateTime.Now, dataRows.Count, 0, dataRows.Count, 0,
                                                AppConstants.ImportStatusError, conn)
                    importLog.ImportStatus = AppConstants.ImportStatusError
                    importLog.TotalCount = dataRows.Count
                    importLog.ErrorCount = dataRows.Count
                    _logger.Error($"Import failed: {importType} - {ex.Message}")
                    Throw
                End Try

                Return importLog
            End Using
        End Function

        Private Sub ImportNavData(dataRows As List(Of String()), importLogId As Integer,
                                   conn As SqlConnection, userName As String)
            Dim allErrors As New List(Of ImportLogDetail)
            Dim warnings As New List(Of ImportLogDetail)
            Dim now = DateTime.Now

            For i = 0 To dataRows.Count - 1
                Dim rowNum = i + 2
                Dim formatErrors = _navValidator.ValidateRow(rowNum, dataRows(i))
                For Each errMsg In formatErrors
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", errMsg, String.Join(",", dataRows(i)), userName, now))
                Next
            Next

            If allErrors.Count > 0 Then
                _importDetailRepo.BulkInsert(allErrors, conn)
                Throw New BusinessException($"形式チェックエラー: {allErrors.Count}件", "FORMAT_ERROR")
            End If

            For i = 0 To dataRows.Count - 1
                Dim rowNum = i + 2
                Dim cols = dataRows(i)
                Dim isin = cols(0).Trim()
                Dim navDateStr = cols(2).Trim()
                Dim currency = cols(3).Trim()
                Dim navDate = Date.Parse(navDateStr)

                If Not _currencyRepo.Exists(currency, conn) Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: 通貨コード {currency} がマスタに存在しません", String.Join(",", cols), userName, now))
                End If

                Dim fund = _fundRepo.GetByIsin(isin, conn)
                If fund Is Nothing OrElse fund.Status <> AppConstants.FundStatusActive Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: ISIN {isin} が有効なファンドとして登録されていません", String.Join(",", cols), userName, now))
                ElseIf fund.CurrencyCode <> currency Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: 通貨コード {currency} がファンドの通貨 {fund.CurrencyCode} と一致しません", String.Join(",", cols), userName, now))
                End If

                If _navRepo.Exists(isin, navDate, conn) Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: ISIN {isin} / 基準日 {navDateStr} のNAVは既に登録されています", String.Join(",", cols), userName, now))
                End If

                Dim navPerUnit = Decimal.Parse(cols(4).Trim())
                Dim latestNav = _navRepo.GetLatestNav(isin, navDate, conn)
                If latestNav IsNot Nothing AndAlso latestNav.NavPerUnit <> 0 Then
                    Dim changeRate = Math.Abs((navPerUnit - latestNav.NavPerUnit) / latestNav.NavPerUnit * 100)
                    If changeRate > AppConfig.NavChangeThreshold Then
                        warnings.Add(CreateDetail(importLogId, rowNum, "WARNING",
                            $"行{rowNum}: NAV変動率が閾値超過（{changeRate:F2}%）", String.Join(",", cols), userName, now))
                    End If
                End If
            Next

            If allErrors.Count > 0 Then
                _importDetailRepo.BulkInsert(allErrors, conn)
                Throw New BusinessException($"業務チェックエラー: {allErrors.Count}件", "BUSINESS_ERROR")
            End If

            If warnings.Count > 0 Then
                _importDetailRepo.BulkInsert(warnings, conn)
            End If

            Dim navList As New List(Of NavHistory)
            For Each cols In dataRows
                Dim nav As New NavHistory()
                nav.Isin = cols(0).Trim()
                nav.NavDate = Date.Parse(cols(2).Trim())
                nav.CurrencyCode = cols(3).Trim()
                nav.NavPerUnit = Decimal.Parse(cols(4).Trim())
                nav.TotalNetAssets = Decimal.Parse(cols(5).Trim())
                nav.SharesOutstanding = Decimal.Parse(cols(6).Trim())
                nav.ImportLogId = importLogId
                nav.CreatedAt = now
                nav.CreatedBy = userName
                nav.UpdatedAt = now
                nav.UpdatedBy = userName
                navList.Add(nav)
            Next

            Using trans = conn.BeginTransaction()
                Try
                    _navRepo.BulkInsert(navList, conn, trans)
                    trans.Commit()
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Private Sub ImportExchangeRate(dataRows As List(Of String()), importLogId As Integer,
                                        conn As SqlConnection, userName As String)
            Dim allErrors As New List(Of ImportLogDetail)
            Dim now = DateTime.Now

            For i = 0 To dataRows.Count - 1
                Dim rowNum = i + 2
                Dim formatErrors = _rateValidator.ValidateRow(rowNum, dataRows(i))
                For Each errMsg In formatErrors
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", errMsg, String.Join(",", dataRows(i)), userName, now))
                Next
            Next

            If allErrors.Count > 0 Then
                _importDetailRepo.BulkInsert(allErrors, conn)
                Throw New BusinessException($"形式チェックエラー: {allErrors.Count}件", "FORMAT_ERROR")
            End If

            For i = 0 To dataRows.Count - 1
                Dim rowNum = i + 2
                Dim cols = dataRows(i)
                Dim currencyCode = cols(0).Trim()
                Dim rateDate = Date.Parse(cols(1).Trim())

                If Not _currencyRepo.Exists(currencyCode, conn) Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: 通貨コード {currencyCode} がマスタに存在しません", String.Join(",", cols), userName, now))
                End If
                If _rateRepo.Exists(currencyCode, rateDate, conn) Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: {currencyCode} / {rateDate:yyyy-MM-dd} の為替レートは既に登録されています", String.Join(",", cols), userName, now))
                End If
            Next

            If allErrors.Count > 0 Then
                _importDetailRepo.BulkInsert(allErrors, conn)
                Throw New BusinessException($"業務チェックエラー: {allErrors.Count}件", "BUSINESS_ERROR")
            End If

            Dim rateList As New List(Of ExchangeRate)
            For Each cols In dataRows
                Dim fxRate As New ExchangeRate()
                fxRate.CurrencyCode = cols(0).Trim()
                fxRate.RateDate = Date.Parse(cols(1).Trim())
                fxRate.Ttm = Decimal.Parse(cols(2).Trim())
                fxRate.Tts = Decimal.Parse(cols(3).Trim())
                fxRate.Ttb = Decimal.Parse(cols(4).Trim())
                fxRate.CreatedAt = now
                fxRate.CreatedBy = userName
                fxRate.UpdatedAt = now
                fxRate.UpdatedBy = userName
                rateList.Add(fxRate)
            Next

            Using trans = conn.BeginTransaction()
                Try
                    For Each fxRate In rateList
                        _rateRepo.Insert(fxRate, conn, trans)
                    Next
                    trans.Commit()
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Private Sub ImportCalendar(dataRows As List(Of String()), importLogId As Integer,
                                    conn As SqlConnection, userName As String)
            Dim allErrors As New List(Of ImportLogDetail)
            Dim now = DateTime.Now

            For i = 0 To dataRows.Count - 1
                Dim rowNum = i + 2
                Dim formatErrors = _calValidator.ValidateRow(rowNum, dataRows(i))
                For Each errMsg In formatErrors
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", errMsg, String.Join(",", dataRows(i)), userName, now))
                Next
            Next

            If allErrors.Count > 0 Then
                _importDetailRepo.BulkInsert(allErrors, conn)
                Throw New BusinessException($"形式チェックエラー: {allErrors.Count}件", "FORMAT_ERROR")
            End If

            For i = 0 To dataRows.Count - 1
                Dim rowNum = i + 2
                Dim cols = dataRows(i)
                Dim regionCode = cols(0).Trim()
                Dim calDate = Date.Parse(cols(1).Trim())

                If _calRepo.Exists(regionCode, calDate, conn) Then
                    allErrors.Add(CreateDetail(importLogId, rowNum, "ERROR", $"行{rowNum}: {regionCode} / {calDate:yyyy-MM-dd} のカレンダーは既に登録されています", String.Join(",", cols), userName, now))
                End If
            Next

            If allErrors.Count > 0 Then
                _importDetailRepo.BulkInsert(allErrors, conn)
                Throw New BusinessException($"業務チェックエラー: {allErrors.Count}件", "BUSINESS_ERROR")
            End If

            Dim calList As New List(Of BusinessCalendar)
            For Each cols In dataRows
                Dim cal As New BusinessCalendar()
                cal.RegionCode = cols(0).Trim()
                cal.CalendarDate = Date.Parse(cols(1).Trim())
                cal.IsHoliday = (cols(2).Trim() = "1")
                cal.HolidayName = If(String.IsNullOrEmpty(cols(3).Trim()), Nothing, cols(3).Trim())
                cal.CreatedAt = now
                cal.CreatedBy = userName
                cal.UpdatedAt = now
                cal.UpdatedBy = userName
                calList.Add(cal)
            Next

            Using trans = conn.BeginTransaction()
                Try
                    For Each cal In calList
                        _calRepo.Insert(cal, conn, trans)
                    Next
                    trans.Commit()
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Private Function CreateDetail(logId As Integer, rowNum As Integer, resultType As String,
                                       message As String, rowData As String,
                                       userName As String, now As DateTime) As ImportLogDetail
            Dim d As New ImportLogDetail()
            d.ImportLogId = logId
            d.RowNumber = rowNum
            d.ResultType = resultType
            d.Message = message
            d.RowData = rowData
            d.CreatedAt = now
            d.CreatedBy = userName
            d.UpdatedAt = now
            d.UpdatedBy = userName
            Return d
        End Function

    End Class
End Namespace
