Imports System.Drawing
Imports System.IO
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure
Imports ForeignFundManager.Data.Repositories
Imports NLog
Imports OfficeOpenXml
Imports OfficeOpenXml.Drawing.Chart
Imports OfficeOpenXml.Style

Namespace Services
    Public Class ReportService

        Private ReadOnly _fundRepo As New FundRepository()
        Private ReadOnly _countryRepo As New CountryRepository()
        Private ReadOnly _navRepo As New NavHistoryRepository()
        Private ReadOnly _rateRepo As New ExchangeRateRepository()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Private ReadOnly HeaderColor As Color = ColorTranslator.FromHtml("#4472C4")
        Private ReadOnly AltRowColor As Color = ColorTranslator.FromHtml("#D9E2F3")
        Private ReadOnly LabelBgColor As Color = ColorTranslator.FromHtml("#F2F2F2")

        Public Sub GenerateMonthlyReport(year As Integer, month As Integer,
                                          isinList As List(Of String), outputFolder As String)
            _logger.Info($"Report generation started: {year}/{month:D2}, funds={isinList.Count}")

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()

                Using pkg As New ExcelPackage()
                    For Each isin In isinList
                        Dim fund = _fundRepo.GetByIsin(isin, conn)
                        If fund Is Nothing Then Continue For

                        Dim sheetName = If(fund.FundNameJp.Length > 20, fund.FundNameJp.Substring(0, 20), fund.FundNameJp)
                        Dim ws = pkg.Workbook.Worksheets.Add(sheetName)

                        ws.PrinterSettings.PaperSize = ePaperSize.A4
                        ws.PrinterSettings.Orientation = eOrientation.Landscape
                        ws.PrinterSettings.TopMargin = 0.59D
                        ws.PrinterSettings.BottomMargin = 0.59D
                        ws.PrinterSettings.LeftMargin = 0.59D
                        ws.PrinterSettings.RightMargin = 0.59D
                        ws.DefaultRowHeight = 15
                        ws.Cells.Style.Font.Name = "游ゴシック"
                        ws.Cells.Style.Font.Size = 10

                        Dim row = 1
                        row = BuildFundInfoSection(ws, fund, year, month, conn, row)
                        row += 1

                        Dim navList = _navRepo.GetMonthly(isin, year, month, conn)
                        row = BuildNavChartSection(ws, navList, year, month, row)
                        row += 1

                        Dim monthEnd = New Date(year, month, DateTime.DaysInMonth(year, month))
                        Dim monthEndNavs = _navRepo.GetMonthEndNavs(isin, monthEnd, conn)
                        row = BuildNetAssetsChartSection(ws, monthEndNavs, row)
                        row += 1

                        row = BuildReturnTableSection(ws, isin, year, month, conn, row)
                        row += 1

                        row = BuildExchangeRateSection(ws, fund.CurrencyCode, year, month, conn, row)
                    Next

                    Dim fileName As String
                    If isinList.Count = 1 Then
                        fileName = $"MonthlyReport_{isinList(0)}_{year}{month:D2}.xlsx"
                    Else
                        fileName = $"MonthlyReport_{year}{month:D2}.xlsx"
                    End If

                    Dim filePath = Path.Combine(outputFolder, fileName)
                    Dim fi As New FileInfo(filePath)
                    pkg.SaveAs(fi)
                    _logger.Info($"Report saved: {filePath}")
                End Using
            End Using
        End Sub

        Private Function BuildFundInfoSection(ws As ExcelWorksheet, fund As Fund,
                                               year As Integer, month As Integer,
                                               conn As SqlConnection, startRow As Integer) As Integer
            Dim row = startRow
            ws.Cells(row, 1).Value = $"月次運用レポート {year}年{month}月"
            ws.Cells(row, 1).Style.Font.Size = 14
            ws.Cells(row, 1).Style.Font.Bold = True
            row += 2

            Dim countryName = _countryRepo.GetNameJp(fund.DomicileCountryCode, conn)

            Dim labels = {"ファンド名（英語）", "ファンド名（日本語）", "ISIN", "通貨", "籍国", "設定日", "決算頻度"}
            Dim values = {fund.FundNameEn, fund.FundNameJp, fund.Isin, fund.CurrencyCode,
                          countryName, fund.InceptionDate.ToString("yyyy/MM/dd"), fund.SettlementFrequency}

            For i = 0 To labels.Length - 1
                ws.Cells(row, 1).Value = labels(i)
                ws.Cells(row, 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(row, 1).Style.Fill.BackgroundColor.SetColor(LabelBgColor)
                ws.Cells(row, 1).Style.Font.Bold = True
                ws.Cells(row, 2).Value = values(i)
                ws.Column(1).Width = 22
                ws.Column(2).Width = 40
                row += 1
            Next

            Return row
        End Function

        Private Function BuildNavChartSection(ws As ExcelWorksheet, navList As List(Of NavHistory),
                                               year As Integer, month As Integer,
                                               startRow As Integer) As Integer
            Dim row = startRow
            ws.Cells(row, 1).Value = "NAV推移"
            ws.Cells(row, 1).Style.Font.Size = 12
            ws.Cells(row, 1).Style.Font.Bold = True
            row += 1

            Dim headerRow = row
            ws.Cells(row, 1).Value = "日付"
            ws.Cells(row, 2).Value = "外貨建てNAV"
            ws.Cells(row, 3).Value = "円建てNAV"
            StyleHeaderRow(ws, row, 1, 3)
            row += 1

            Dim dataStartRow = row
            For Each nav In navList
                ws.Cells(row, 1).Value = nav.NavDate
                ws.Cells(row, 1).Style.Numberformat.Format = "yyyy/MM/dd"
                ws.Cells(row, 2).Value = nav.NavPerUnit
                ws.Cells(row, 2).Style.Numberformat.Format = "#,##0.000000"
                ws.Cells(row, 3).Value = If(nav.NavJpy.HasValue, CDec(nav.NavJpy.Value), 0D)
                ws.Cells(row, 3).Style.Numberformat.Format = "#,##0"
                If (row - dataStartRow) Mod 2 = 1 Then
                    ApplyAltRowStyle(ws, row, 1, 3)
                End If
                row += 1
            Next
            Dim dataEndRow = row - 1

            If navList.Count > 0 Then
                Dim chart = ws.Drawings.AddChart("NavChart", eChartType.Line)
                chart.SetPosition(headerRow - 1, 0, 4, 0)
                chart.SetSize(500, 300)
                chart.Title.Text = $"NAV推移 {year}年{month}月"

                Dim series1 = chart.Series.Add(ws.Cells(dataStartRow, 2, dataEndRow, 2),
                                                ws.Cells(dataStartRow, 1, dataEndRow, 1))
                series1.Header = "外貨建てNAV"

                Dim series2 = chart.Series.Add(ws.Cells(dataStartRow, 3, dataEndRow, 3),
                                                ws.Cells(dataStartRow, 1, dataEndRow, 1))
                series2.Header = "円建てNAV"
            End If

            Return row
        End Function

        Private Function BuildNetAssetsChartSection(ws As ExcelWorksheet,
                                                     monthEndNavs As List(Of NavHistory),
                                                     startRow As Integer) As Integer
            Dim row = startRow
            ws.Cells(row, 1).Value = "純資産総額推移（月末）"
            ws.Cells(row, 1).Style.Font.Size = 12
            ws.Cells(row, 1).Style.Font.Bold = True
            row += 1

            Dim headerRow = row
            ws.Cells(row, 1).Value = "年月"
            ws.Cells(row, 2).Value = "純資産総額"
            StyleHeaderRow(ws, row, 1, 2)
            row += 1

            Dim dataStartRow = row
            For Each nav In monthEndNavs
                ws.Cells(row, 1).Value = nav.NavDate.ToString("yyyy/MM")
                ws.Cells(row, 2).Value = nav.TotalNetAssets
                ws.Cells(row, 2).Style.Numberformat.Format = "#,##0.00"
                If (row - dataStartRow) Mod 2 = 1 Then
                    ApplyAltRowStyle(ws, row, 1, 2)
                End If
                row += 1
            Next
            Dim dataEndRow = row - 1

            If monthEndNavs.Count > 0 Then
                Dim chart = ws.Drawings.AddChart("NetAssetsChart", eChartType.ColumnClustered)
                chart.SetPosition(headerRow - 1, 0, 3, 0)
                chart.SetSize(500, 300)
                chart.Title.Text = "純資産総額推移（直近12ヶ月）"

                Dim series1 = chart.Series.Add(ws.Cells(dataStartRow, 2, dataEndRow, 2),
                                                ws.Cells(dataStartRow, 1, dataEndRow, 1))
                series1.Header = "純資産総額"
            End If

            Return row
        End Function

        Private Function BuildReturnTableSection(ws As ExcelWorksheet, isin As String,
                                                  year As Integer, month As Integer,
                                                  conn As SqlConnection,
                                                  startRow As Integer) As Integer
            Dim row = startRow
            ws.Cells(row, 1).Value = "リターン"
            ws.Cells(row, 1).Style.Font.Size = 12
            ws.Cells(row, 1).Style.Font.Bold = True
            row += 1

            ws.Cells(row, 1).Value = ""
            ws.Cells(row, 2).Value = "外貨建て"
            ws.Cells(row, 3).Value = "円建て"
            StyleHeaderRow(ws, row, 1, 3)
            row += 1

            Dim monthEnd = New Date(year, month, DateTime.DaysInMonth(year, month))
            Dim prevMonthEnd = New Date(year, month, 1).AddDays(-1)
            Dim prevYearEnd = New Date(year - 1, 12, 31)

            Dim currentMonthNavs = _navRepo.GetMonthly(isin, year, month, conn)
            Dim currentNav As NavHistory = Nothing
            If currentMonthNavs.Count > 0 Then currentNav = currentMonthNavs.Last()

            Dim prevMonthNavs = _navRepo.GetMonthly(isin, prevMonthEnd.Year, prevMonthEnd.Month, conn)
            Dim prevNav As NavHistory = Nothing
            If prevMonthNavs.Count > 0 Then prevNav = prevMonthNavs.Last()

            Dim prevYearNavs = _navRepo.GetMonthly(isin, prevYearEnd.Year, prevYearEnd.Month, conn)
            Dim prevYearNav As NavHistory = Nothing
            If prevYearNavs.Count > 0 Then prevYearNav = prevYearNavs.Last()

            Dim firstNav = _navRepo.GetFirstNav(isin, conn)

            Dim labels = {"月間リターン", "年初来リターン", "設定来リターン"}
            Dim foreignReturns = {
                CalculateReturn(currentNav?.NavPerUnit, prevNav?.NavPerUnit),
                CalculateReturn(currentNav?.NavPerUnit, prevYearNav?.NavPerUnit),
                CalculateReturn(currentNav?.NavPerUnit, firstNav?.NavPerUnit)
            }
            Dim jpyReturns = {
                CalculateReturn(currentNav?.NavJpy, prevNav?.NavJpy),
                CalculateReturn(currentNav?.NavJpy, prevYearNav?.NavJpy),
                CalculateReturn(currentNav?.NavJpy, firstNav?.NavJpy)
            }

            For i = 0 To 2
                ws.Cells(row, 1).Value = labels(i)
                ws.Cells(row, 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(row, 1).Style.Fill.BackgroundColor.SetColor(LabelBgColor)
                FormatReturnCell(ws, row, 2, foreignReturns(i))
                FormatReturnCell(ws, row, 3, jpyReturns(i))
                row += 1
            Next

            Return row
        End Function

        Private Function BuildExchangeRateSection(ws As ExcelWorksheet, currencyCode As String,
                                                   year As Integer, month As Integer,
                                                   conn As SqlConnection,
                                                   startRow As Integer) As Integer
            Dim row = startRow
            ws.Cells(row, 1).Value = $"為替レート（{currencyCode}/JPY）"
            ws.Cells(row, 1).Style.Font.Size = 12
            ws.Cells(row, 1).Style.Font.Bold = True
            row += 1

            ws.Cells(row, 1).Value = "日付"
            ws.Cells(row, 2).Value = "TTM"
            ws.Cells(row, 3).Value = "TTS"
            ws.Cells(row, 4).Value = "TTB"
            StyleHeaderRow(ws, row, 1, 4)
            row += 1

            Dim rates = _rateRepo.GetMonthlyRates(currencyCode, year, month, conn)
            Dim dataStartRow = row
            For Each fxRate In rates
                ws.Cells(row, 1).Value = fxRate.RateDate
                ws.Cells(row, 1).Style.Numberformat.Format = "yyyy/MM/dd"
                ws.Cells(row, 2).Value = fxRate.Ttm
                ws.Cells(row, 2).Style.Numberformat.Format = "#,##0.000000"
                ws.Cells(row, 3).Value = fxRate.Tts
                ws.Cells(row, 3).Style.Numberformat.Format = "#,##0.000000"
                ws.Cells(row, 4).Value = fxRate.Ttb
                ws.Cells(row, 4).Style.Numberformat.Format = "#,##0.000000"
                If (row - dataStartRow) Mod 2 = 1 Then
                    ApplyAltRowStyle(ws, row, 1, 4)
                End If
                row += 1
            Next

            Return row
        End Function

        Private Function CalculateReturn(currentNav As Decimal?, baseNav As Decimal?) As Decimal?
            If Not currentNav.HasValue OrElse Not baseNav.HasValue OrElse baseNav.Value = 0 Then
                Return Nothing
            End If
            Return (currentNav.Value - baseNav.Value) / baseNav.Value * 100
        End Function

        Private Sub FormatReturnCell(ws As ExcelWorksheet, row As Integer, col As Integer, value As Decimal?)
            If value.HasValue Then
                ws.Cells(row, col).Value = value.Value / 100
                ws.Cells(row, col).Style.Numberformat.Format = "+0.00%;-0.00%"
                If value.Value >= 0 Then
                    ws.Cells(row, col).Style.Font.Color.SetColor(Color.Blue)
                Else
                    ws.Cells(row, col).Style.Font.Color.SetColor(Color.Red)
                End If
            Else
                ws.Cells(row, col).Value = "N/A"
            End If
        End Sub

        Private Sub StyleHeaderRow(ws As ExcelWorksheet, row As Integer, fromCol As Integer, toCol As Integer)
            For c = fromCol To toCol
                ws.Cells(row, c).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(row, c).Style.Fill.BackgroundColor.SetColor(HeaderColor)
                ws.Cells(row, c).Style.Font.Color.SetColor(Color.White)
                ws.Cells(row, c).Style.Font.Bold = True
            Next
        End Sub

        Private Sub ApplyAltRowStyle(ws As ExcelWorksheet, row As Integer, fromCol As Integer, toCol As Integer)
            For c = fromCol To toCol
                ws.Cells(row, c).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(row, c).Style.Fill.BackgroundColor.SetColor(AltRowColor)
            Next
        End Sub

    End Class
End Namespace
