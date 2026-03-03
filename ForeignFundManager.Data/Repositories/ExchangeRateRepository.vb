Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class ExchangeRateRepository

        Public Function Search(currencyCode As String, dateFrom As Date, dateTo As Date,
                                conn As SqlConnection) As List(Of ExchangeRate)
            Dim sql = "SELECT * FROM M_EXCHANGE_RATE " &
                      "WHERE (@ccy = '' OR CURRENCY_CODE = @ccy) " &
                      "AND RATE_DATE >= @from AND RATE_DATE <= @to " &
                      "ORDER BY CURRENCY_CODE, RATE_DATE DESC"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ccy", If(currencyCode, ""))
                cmd.Parameters.AddWithValue("@from", dateFrom)
                cmd.Parameters.AddWithValue("@to", dateTo)
                Dim list As New List(Of ExchangeRate)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapRate(reader))
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function GetRate(currencyCode As String, rateDate As Date,
                                 conn As SqlConnection) As ExchangeRate
            Dim sql = "SELECT * FROM M_EXCHANGE_RATE WHERE CURRENCY_CODE = @ccy AND RATE_DATE = @date"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ccy", currencyCode)
                cmd.Parameters.AddWithValue("@date", rateDate)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return MapRate(reader)
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetMonthlyRates(currencyCode As String, year As Integer, month As Integer,
                                         conn As SqlConnection) As List(Of ExchangeRate)
            Dim monthStart = New Date(year, month, 1)
            Dim monthEnd = monthStart.AddMonths(1).AddDays(-1)
            Dim sql = "SELECT * FROM M_EXCHANGE_RATE " &
                      "WHERE CURRENCY_CODE = @ccy AND RATE_DATE >= @start AND RATE_DATE <= @end " &
                      "ORDER BY RATE_DATE"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ccy", currencyCode)
                cmd.Parameters.AddWithValue("@start", monthStart)
                cmd.Parameters.AddWithValue("@end", monthEnd)
                Dim list As New List(Of ExchangeRate)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapRate(reader))
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function Exists(currencyCode As String, rateDate As Date,
                                conn As SqlConnection) As Boolean
            Dim sql = "SELECT COUNT(*) FROM M_EXCHANGE_RATE WHERE CURRENCY_CODE = @ccy AND RATE_DATE = @date"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ccy", currencyCode)
                cmd.Parameters.AddWithValue("@date", rateDate)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Public Function Insert(rate As ExchangeRate, conn As SqlConnection,
                                trans As SqlTransaction) As Integer
            Dim sql = "INSERT INTO M_EXCHANGE_RATE (CURRENCY_CODE, RATE_DATE, TTM, TTS, TTB, " &
                      "CREATED_AT, CREATED_BY, UPDATED_AT, UPDATED_BY) " &
                      "VALUES (@ccy, @date, @ttm, @tts, @ttb, @createdAt, @createdBy, @updatedAt, @updatedBy)"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@ccy", rate.CurrencyCode)
                cmd.Parameters.AddWithValue("@date", rate.RateDate)
                cmd.Parameters.AddWithValue("@ttm", rate.Ttm)
                cmd.Parameters.AddWithValue("@tts", rate.Tts)
                cmd.Parameters.AddWithValue("@ttb", rate.Ttb)
                cmd.Parameters.AddWithValue("@createdAt", rate.CreatedAt)
                cmd.Parameters.AddWithValue("@createdBy", rate.CreatedBy)
                cmd.Parameters.AddWithValue("@updatedAt", rate.UpdatedAt)
                cmd.Parameters.AddWithValue("@updatedBy", rate.UpdatedBy)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Private Function MapRate(reader As SqlDataReader) As ExchangeRate
            Dim r As New ExchangeRate()
            r.CurrencyCode = reader("CURRENCY_CODE").ToString().Trim()
            r.RateDate = CDate(reader("RATE_DATE"))
            r.Ttm = CDec(reader("TTM"))
            r.Tts = CDec(reader("TTS"))
            r.Ttb = CDec(reader("TTB"))
            r.CreatedAt = CDate(reader("CREATED_AT"))
            r.CreatedBy = reader("CREATED_BY").ToString()
            r.UpdatedAt = CDate(reader("UPDATED_AT"))
            r.UpdatedBy = reader("UPDATED_BY").ToString()
            Return r
        End Function

    End Class
End Namespace
