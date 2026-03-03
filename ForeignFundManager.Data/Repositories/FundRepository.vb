Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class FundRepository

        Public Function Search(isin As String, fundName As String,
                               currencyCode As String, countryCode As String,
                               activeOnly As Boolean,
                               conn As SqlConnection,
                               Optional trans As SqlTransaction = Nothing) As List(Of Fund)
            Dim sql = "SELECT * FROM M_FUND " &
                      "WHERE (@isin = '' OR ISIN LIKE @isin + '%') " &
                      "AND (@name = '' OR FUND_NAME_EN LIKE '%' + @name + '%' OR FUND_NAME_JP LIKE '%' + @name + '%') " &
                      "AND (@ccy = '' OR CURRENCY_CODE = @ccy) " &
                      "AND (@ctry = '' OR DOMICILE_COUNTRY_CODE = @ctry) " &
                      "AND (@activeOnly = 0 OR STATUS = '1') " &
                      "ORDER BY ISIN"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@isin", If(isin, ""))
                cmd.Parameters.AddWithValue("@name", If(fundName, ""))
                cmd.Parameters.AddWithValue("@ccy", If(currencyCode, ""))
                cmd.Parameters.AddWithValue("@ctry", If(countryCode, ""))
                cmd.Parameters.AddWithValue("@activeOnly", If(activeOnly, 1, 0))
                Dim list As New List(Of Fund)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapFund(reader))
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function GetByIsin(isin As String, conn As SqlConnection,
                                   Optional trans As SqlTransaction = Nothing) As Fund
            Dim sql = "SELECT * FROM M_FUND WHERE ISIN = @isin"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@isin", isin)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return MapFund(reader)
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Exists(isin As String, conn As SqlConnection,
                                Optional trans As SqlTransaction = Nothing) As Boolean
            Dim sql = "SELECT COUNT(*) FROM M_FUND WHERE ISIN = @isin"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@isin", isin)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Public Function Insert(fund As Fund, conn As SqlConnection,
                                trans As SqlTransaction) As Integer
            Dim sql = "INSERT INTO M_FUND (ISIN, FUND_NAME_EN, FUND_NAME_JP, CURRENCY_CODE, " &
                      "DOMICILE_COUNTRY_CODE, INCEPTION_DATE, SETTLEMENT_FREQUENCY, ROUNDING_TYPE, " &
                      "STATUS, REMARKS, CREATED_AT, CREATED_BY, UPDATED_AT, UPDATED_BY) " &
                      "VALUES (@isin, @nameEn, @nameJp, @ccy, @ctry, @inception, @settlement, " &
                      "@rounding, @status, @remarks, @createdAt, @createdBy, @updatedAt, @updatedBy)"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                AddFundParameters(cmd, fund)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Public Function Update(fund As Fund, conn As SqlConnection,
                                trans As SqlTransaction) As Integer
            Dim sql = "UPDATE M_FUND SET FUND_NAME_EN = @nameEn, FUND_NAME_JP = @nameJp, " &
                      "CURRENCY_CODE = @ccy, DOMICILE_COUNTRY_CODE = @ctry, " &
                      "INCEPTION_DATE = @inception, SETTLEMENT_FREQUENCY = @settlement, " &
                      "ROUNDING_TYPE = @rounding, STATUS = @status, REMARKS = @remarks, " &
                      "UPDATED_AT = @updatedAt, UPDATED_BY = @updatedBy " &
                      "WHERE ISIN = @isin"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                AddFundParameters(cmd, fund)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Public Function SoftDelete(isin As String, updatedBy As String,
                                    conn As SqlConnection,
                                    trans As SqlTransaction) As Integer
            Dim sql = "UPDATE M_FUND SET STATUS = '9', UPDATED_AT = @now, UPDATED_BY = @user WHERE ISIN = @isin"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@isin", isin)
                cmd.Parameters.AddWithValue("@now", DateTime.Now)
                cmd.Parameters.AddWithValue("@user", updatedBy)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Public Function GetActiveFunds(conn As SqlConnection) As List(Of Fund)
            Dim sql = "SELECT * FROM M_FUND WHERE STATUS = '1' ORDER BY ISIN"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                Dim list As New List(Of Fund)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapFund(reader))
                    End While
                End Using
                Return list
            End Using
        End Function

        Private Sub AddFundParameters(cmd As SqlCommand, fund As Fund)
            cmd.Parameters.AddWithValue("@isin", fund.Isin)
            cmd.Parameters.AddWithValue("@nameEn", fund.FundNameEn)
            cmd.Parameters.AddWithValue("@nameJp", fund.FundNameJp)
            cmd.Parameters.AddWithValue("@ccy", fund.CurrencyCode)
            cmd.Parameters.AddWithValue("@ctry", fund.DomicileCountryCode)
            cmd.Parameters.AddWithValue("@inception", fund.InceptionDate)
            cmd.Parameters.AddWithValue("@settlement", fund.SettlementFrequency)
            cmd.Parameters.AddWithValue("@rounding", fund.RoundingType)
            cmd.Parameters.AddWithValue("@status", fund.Status)
            cmd.Parameters.AddWithValue("@remarks", If(fund.Remarks, CObj(DBNull.Value)))
            cmd.Parameters.AddWithValue("@createdAt", fund.CreatedAt)
            cmd.Parameters.AddWithValue("@createdBy", fund.CreatedBy)
            cmd.Parameters.AddWithValue("@updatedAt", fund.UpdatedAt)
            cmd.Parameters.AddWithValue("@updatedBy", fund.UpdatedBy)
        End Sub

        Private Function MapFund(reader As SqlDataReader) As Fund
            Dim f As New Fund()
            f.Isin = reader("ISIN").ToString().Trim()
            f.FundNameEn = reader("FUND_NAME_EN").ToString()
            f.FundNameJp = reader("FUND_NAME_JP").ToString()
            f.CurrencyCode = reader("CURRENCY_CODE").ToString().Trim()
            f.DomicileCountryCode = reader("DOMICILE_COUNTRY_CODE").ToString().Trim()
            f.InceptionDate = CDate(reader("INCEPTION_DATE"))
            f.SettlementFrequency = reader("SETTLEMENT_FREQUENCY").ToString()
            f.RoundingType = reader("ROUNDING_TYPE").ToString().Trim()
            f.Status = reader("STATUS").ToString().Trim()
            f.Remarks = If(IsDBNull(reader("REMARKS")), Nothing, reader("REMARKS").ToString())
            f.CreatedAt = CDate(reader("CREATED_AT"))
            f.CreatedBy = reader("CREATED_BY").ToString()
            f.UpdatedAt = CDate(reader("UPDATED_AT"))
            f.UpdatedBy = reader("UPDATED_BY").ToString()
            Return f
        End Function

    End Class
End Namespace
