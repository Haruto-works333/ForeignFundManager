Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Utils
Imports ForeignFundManager.Data.Infrastructure
Imports ForeignFundManager.Data.Repositories
Imports NLog

Namespace Services
    Public Class NavCalculationService

        Private ReadOnly _navRepo As New NavHistoryRepository()
        Private ReadOnly _rateRepo As New ExchangeRateRepository()
        Private ReadOnly _calRepo As New CalendarRepository()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function Execute(targetDate As Date) As BatchResult
            _logger.Info($"NAV calculation started: targetDate={targetDate:yyyy-MM-dd}")
            Dim sw = Stopwatch.StartNew()
            Dim result As New BatchResult()

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()

                Dim targets = _navRepo.GetUnconverted(targetDate, conn)
                result.TotalCount = targets.Count

                If targets.Count = 0 Then
                    _logger.Info("No records to process")
                    sw.Stop()
                    result.Elapsed = sw.Elapsed
                    Return result
                End If

                For Each target In targets
                    Dim detail As New BatchResultDetail()
                    detail.Isin = target.Isin
                    detail.CurrencyCode = target.CurrencyCode
                    detail.NavPerUnit = target.NavPerUnit

                    Try
                        Dim rateResult = GetTtmWithFallback(target.CurrencyCode, targetDate, conn)
                        If rateResult Is Nothing Then
                            detail.Status = "SKIP"
                            detail.Note = String.Format(MessageConstants.MsgBatchRateNotFound, target.Isin, target.CurrencyCode, targetDate.ToString("yyyy-MM-dd"))
                            result.SkippedCount += 1
                            _logger.Error(detail.Note)
                        Else
                            Dim ttm = rateResult.ExchangeRate.Ttm
                            Dim navJpy = RoundingHelper.ApplyRounding(target.NavPerUnit * ttm, target.RoundingType)

                            Using trans = conn.BeginTransaction()
                                Try
                                    _navRepo.UpdateNavJpy(target.NavHistoryId, navJpy, ttm, DateTime.Now,
                                                          AppConfig.UserName, conn, trans)
                                    trans.Commit()

                                    detail.AppliedRate = ttm
                                    detail.NavJpy = navJpy
                                    detail.Status = "OK"
                                    If rateResult.IsSubstitute Then
                                        detail.Note = String.Format(MessageConstants.MsgBatchPrevRateUsed, rateResult.ActualDate.ToString("yyyy-MM-dd"))
                                    End If
                                    result.SuccessCount += 1
                                    _logger.Info($"NAV calculated: {target.Isin}, NAV={target.NavPerUnit}, TTM={ttm}, JPY={navJpy}")
                                Catch ex As Exception
                                    trans.Rollback()
                                    detail.Status = "ERROR"
                                    detail.Note = ex.Message
                                    result.ErrorCount += 1
                                    _logger.Error(ex, $"NAV calculation failed: {target.Isin}")
                                End Try
                            End Using
                        End If
                    Catch ex As Exception
                        detail.Status = "ERROR"
                        detail.Note = ex.Message
                        result.ErrorCount += 1
                        _logger.Error(ex, $"NAV calculation failed: {target.Isin}")
                    End Try

                    result.Details.Add(detail)
                Next
            End Using

            sw.Stop()
            result.Elapsed = sw.Elapsed
            _logger.Info($"NAV calculation completed: total={result.TotalCount}, success={result.SuccessCount}, skipped={result.SkippedCount}, error={result.ErrorCount}, elapsed={result.Elapsed}")
            Return result
        End Function

        Public Function GetTtmWithFallback(currencyCode As String, targetDate As Date,
                                            conn As SqlConnection) As RateLookupResult
            Dim fxRate = _rateRepo.GetRate(currencyCode, targetDate, conn)
            If fxRate IsNot Nothing Then
                Dim r As New RateLookupResult()
                r.ExchangeRate = fxRate
                r.ActualDate = targetDate
                r.IsSubstitute = False
                Return r
            End If

            Dim lookbackCount = 0
            Dim currentDate = targetDate

            While lookbackCount < AppConfig.MaxRateLookbackDays
                currentDate = currentDate.AddDays(-1)

                If currentDate.DayOfWeek = DayOfWeek.Saturday OrElse currentDate.DayOfWeek = DayOfWeek.Sunday Then
                    Continue While
                End If

                If _calRepo.IsHoliday(AppConstants.RegionJapan, currentDate, conn) Then
                    Continue While
                End If

                lookbackCount += 1
                fxRate = _rateRepo.GetRate(currencyCode, currentDate, conn)
                If fxRate IsNot Nothing Then
                    _logger.Warn($"Previous business day rate used: {currencyCode} {currentDate:yyyy-MM-dd} (original: {targetDate:yyyy-MM-dd})")
                    Dim r As New RateLookupResult()
                    r.ExchangeRate = fxRate
                    r.ActualDate = currentDate
                    r.IsSubstitute = True
                    Return r
                End If
            End While

            Return Nothing
        End Function

    End Class
End Namespace
