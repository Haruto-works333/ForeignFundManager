Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Utils
Imports ForeignFundManager.Core.Validators
Imports ForeignFundManager.Data.Infrastructure
Imports ForeignFundManager.Data.Repositories
Imports NLog

Namespace Services
    Public Class FundService

        Private ReadOnly _fundRepo As New FundRepository()
        Private ReadOnly _currencyRepo As New CurrencyRepository()
        Private ReadOnly _countryRepo As New CountryRepository()
        Private ReadOnly _codeRepo As New CodeRepository()
        Private ReadOnly _auditLogRepo As New AuditLogRepository()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function Search(isin As String, fundName As String,
                               currencyCode As String, countryCode As String,
                               activeOnly As Boolean) As List(Of Fund)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _fundRepo.Search(isin, fundName, currencyCode, countryCode, activeOnly, conn)
            End Using
        End Function

        Public Function GetByIsin(isin As String) As Fund
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _fundRepo.GetByIsin(isin, conn)
            End Using
        End Function

        Public Sub Save(fund As Fund, isNew As Boolean)
            If Not IsinValidator.Validate(fund.Isin) Then
                Throw New BusinessException(MessageConstants.MsgIsinInvalidCheckDigit, "ISIN_INVALID", "Isin")
            End If

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()

                If isNew AndAlso _fundRepo.Exists(fund.Isin, conn) Then
                    Throw New BusinessException(String.Format(MessageConstants.MsgIsinDuplicate, fund.Isin), "ISIN_DUPLICATE", "Isin")
                End If
                If Not _currencyRepo.Exists(fund.CurrencyCode, conn) Then
                    Throw New BusinessException(MessageConstants.MsgCurrencyNotFound, "CURRENCY_NOT_FOUND", "CurrencyCode")
                End If
                If Not _countryRepo.Exists(fund.DomicileCountryCode, conn) Then
                    Throw New BusinessException(MessageConstants.MsgCountryNotFound, "COUNTRY_NOT_FOUND", "DomicileCountryCode")
                End If
                If fund.InceptionDate > Date.Today Then
                    Throw New BusinessException(MessageConstants.MsgFutureDateNotAllowed, "FUTURE_DATE", "InceptionDate")
                End If

                Dim now = DateTime.Now
                Dim userName = AppConfig.UserName
                fund.UpdatedAt = now
                fund.UpdatedBy = userName

                Using trans = conn.BeginTransaction()
                    Try
                        If isNew Then
                            fund.CreatedAt = now
                            fund.CreatedBy = userName
                            If String.IsNullOrEmpty(fund.Status) Then fund.Status = AppConstants.FundStatusActive
                            _fundRepo.Insert(fund, conn, trans)

                            Dim auditLogs As New List(Of AuditLog)
                            auditLogs.Add(CreateAuditLog(fund.Isin, AppConstants.OperationInsert, Nothing, Nothing, Nothing, userName, now))
                            _auditLogRepo.Insert(auditLogs, conn, trans)
                        Else
                            Dim oldFund = _fundRepo.GetByIsin(fund.Isin, conn, trans)
                            _fundRepo.Update(fund, conn, trans)

                            Dim auditLogs = DetectChanges(oldFund, fund, userName, now)
                            If auditLogs.Count > 0 Then
                                _auditLogRepo.Insert(auditLogs, conn, trans)
                            End If
                        End If
                        trans.Commit()
                        _logger.Info($"Fund saved: {fund.Isin} (isNew={isNew})")
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Sub Delete(isin As String)
            Dim userName = AppConfig.UserName
            Dim now = DateTime.Now

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        _fundRepo.SoftDelete(isin, userName, conn, trans)

                        Dim auditLogs As New List(Of AuditLog)
                        auditLogs.Add(CreateAuditLog(isin, AppConstants.OperationDelete, "STATUS", AppConstants.FundStatusActive, AppConstants.FundStatusDeleted, userName, now))
                        _auditLogRepo.Insert(auditLogs, conn, trans)

                        trans.Commit()
                        _logger.Info($"Fund deleted: {isin}")
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Function GetChangeHistory(isin As String) As List(Of AuditLog)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _auditLogRepo.GetByTableAndKey("M_FUND", isin, conn)
            End Using
        End Function

        Public Sub ExportCsv(filePath As String, funds As List(Of Fund))
            Dim rows As New List(Of String())
            For Each f In funds
                rows.Add(New String() {
                    f.Isin, f.FundNameEn, f.FundNameJp, f.CurrencyCode,
                    f.DomicileCountryCode, f.InceptionDate.ToString("yyyy-MM-dd"),
                    f.SettlementFrequency, f.RoundingType, f.Status,
                    If(f.Remarks, "")
                })
            Next
            CsvHelper.WriteCsv(filePath, AppConstants.FundExportHeaders, rows)
            _logger.Info($"Fund CSV exported: {filePath} ({funds.Count} records)")
        End Sub

        Public Function GetActiveFunds() As List(Of Fund)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _fundRepo.GetActiveFunds(conn)
            End Using
        End Function

        Public Function GetCurrencies() As List(Of CodeMaster)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _currencyRepo.GetAll(conn)
            End Using
        End Function

        Public Function GetCountries() As List(Of CodeMaster)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _countryRepo.GetAll(conn)
            End Using
        End Function

        Public Function GetCodeList(codeType As String) As List(Of CodeMaster)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _codeRepo.GetByType(codeType, conn)
            End Using
        End Function

        Private Function DetectChanges(oldFund As Fund, newFund As Fund, userName As String, now As DateTime) As List(Of AuditLog)
            Dim logs As New List(Of AuditLog)
            Dim opId = Guid.NewGuid().ToString()

            CheckField(logs, opId, newFund.Isin, "FUND_NAME_EN", oldFund.FundNameEn, newFund.FundNameEn, userName, now)
            CheckField(logs, opId, newFund.Isin, "FUND_NAME_JP", oldFund.FundNameJp, newFund.FundNameJp, userName, now)
            CheckField(logs, opId, newFund.Isin, "CURRENCY_CODE", oldFund.CurrencyCode, newFund.CurrencyCode, userName, now)
            CheckField(logs, opId, newFund.Isin, "DOMICILE_COUNTRY_CODE", oldFund.DomicileCountryCode, newFund.DomicileCountryCode, userName, now)
            CheckField(logs, opId, newFund.Isin, "INCEPTION_DATE", oldFund.InceptionDate.ToString("yyyy-MM-dd"), newFund.InceptionDate.ToString("yyyy-MM-dd"), userName, now)
            CheckField(logs, opId, newFund.Isin, "SETTLEMENT_FREQUENCY", oldFund.SettlementFrequency, newFund.SettlementFrequency, userName, now)
            CheckField(logs, opId, newFund.Isin, "ROUNDING_TYPE", oldFund.RoundingType, newFund.RoundingType, userName, now)
            CheckField(logs, opId, newFund.Isin, "STATUS", oldFund.Status, newFund.Status, userName, now)
            CheckField(logs, opId, newFund.Isin, "REMARKS", If(oldFund.Remarks, ""), If(newFund.Remarks, ""), userName, now)

            Return logs
        End Function

        Private Sub CheckField(logs As List(Of AuditLog), opId As String, isin As String,
                                columnName As String, oldVal As String, newVal As String,
                                userName As String, now As DateTime)
            If oldVal <> newVal Then
                logs.Add(CreateAuditLog(isin, AppConstants.OperationUpdate, columnName, oldVal, newVal, userName, now, opId))
            End If
        End Sub

        Private Function CreateAuditLog(isin As String, opType As String,
                                         columnName As String, oldVal As String, newVal As String,
                                         userName As String, now As DateTime,
                                         Optional opId As String = Nothing) As AuditLog
            Dim a As New AuditLog()
            a.OperationId = If(opId, Guid.NewGuid().ToString())
            a.TableName = "M_FUND"
            a.RecordKey = isin
            a.OperationType = opType
            a.ColumnName = columnName
            a.OldValue = oldVal
            a.NewValue = newVal
            a.OperatedAt = now
            a.OperatedBy = userName
            Return a
        End Function

    End Class
End Namespace
