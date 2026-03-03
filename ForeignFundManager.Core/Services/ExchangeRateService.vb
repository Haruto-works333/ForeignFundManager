Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Utils
Imports ForeignFundManager.Data.Infrastructure
Imports ForeignFundManager.Data.Repositories
Imports NLog

Namespace Services
    Public Class ExchangeRateService

        Private ReadOnly _rateRepo As New ExchangeRateRepository()
        Private ReadOnly _currencyRepo As New CurrencyRepository()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function Search(currencyCode As String, dateFrom As Date, dateTo As Date) As List(Of ExchangeRate)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _rateRepo.Search(currencyCode, dateFrom, dateTo, conn)
            End Using
        End Function

        Public Sub Save(fxRate As ExchangeRate)
            If fxRate.Ttb > fxRate.Ttm OrElse fxRate.Ttm > fxRate.Tts Then
                Throw New BusinessException(MessageConstants.MsgRateOrderInvalid, "RATE_ORDER", "Ttm")
            End If

            If fxRate.RateDate > Date.Today Then
                Throw New BusinessException(MessageConstants.MsgFutureDateNotAllowed, "FUTURE_DATE", "RateDate")
            End If

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()

                If _rateRepo.Exists(fxRate.CurrencyCode, fxRate.RateDate, conn) Then
                    Throw New BusinessException(MessageConstants.MsgRateDuplicate, "RATE_DUPLICATE", "RateDate")
                End If

                If Not _currencyRepo.Exists(fxRate.CurrencyCode, conn) Then
                    Throw New BusinessException(MessageConstants.MsgCurrencyNotFound, "CURRENCY_NOT_FOUND", "CurrencyCode")
                End If

                Dim now = DateTime.Now
                fxRate.CreatedAt = now
                fxRate.CreatedBy = AppConfig.UserName
                fxRate.UpdatedAt = now
                fxRate.UpdatedBy = AppConfig.UserName

                Using trans = conn.BeginTransaction()
                    Try
                        _rateRepo.Insert(fxRate, conn, trans)
                        trans.Commit()
                        _logger.Info($"Exchange rate saved: {fxRate.CurrencyCode} {fxRate.RateDate:yyyy-MM-dd}")
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Function GetCurrencies() As List(Of CodeMaster)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _currencyRepo.GetAll(conn)
            End Using
        End Function

    End Class
End Namespace
