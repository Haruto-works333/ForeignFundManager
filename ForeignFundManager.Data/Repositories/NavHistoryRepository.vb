Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class NavHistoryRepository

        Public Function GetUnconverted(targetDate As Date,
                                        conn As SqlConnection) As List(Of NavCalculationTarget)
            Dim sql = "SELECT nh.NAV_HISTORY_ID, nh.ISIN, nh.NAV_DATE, nh.CURRENCY_CODE, " &
                      "nh.NAV_PER_UNIT, nh.TOTAL_NET_ASSETS, nh.SHARES_OUTSTANDING, " &
                      "nh.IMPORT_LOG_ID, f.ROUNDING_TYPE, f.FUND_NAME_EN " &
                      "FROM T_NAV_HISTORY nh " &
                      "INNER JOIN M_FUND f ON nh.ISIN = f.ISIN " &
                      "WHERE nh.NAV_DATE = @date AND nh.NAV_JPY IS NULL"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@date", targetDate)
                Dim list As New List(Of NavCalculationTarget)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim t As New NavCalculationTarget()
                        t.NavHistoryId = CInt(reader("NAV_HISTORY_ID"))
                        t.Isin = reader("ISIN").ToString().Trim()
                        t.NavDate = CDate(reader("NAV_DATE"))
                        t.CurrencyCode = reader("CURRENCY_CODE").ToString().Trim()
                        t.NavPerUnit = CDec(reader("NAV_PER_UNIT"))
                        t.TotalNetAssets = CDec(reader("TOTAL_NET_ASSETS"))
                        t.SharesOutstanding = CDec(reader("SHARES_OUTSTANDING"))
                        t.ImportLogId = CInt(reader("IMPORT_LOG_ID"))
                        t.RoundingType = reader("ROUNDING_TYPE").ToString().Trim()
                        t.FundNameEn = reader("FUND_NAME_EN").ToString()
                        list.Add(t)
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function UpdateNavJpy(navHistoryId As Integer, navJpy As Decimal,
                                      appliedRate As Decimal, calculatedAt As DateTime,
                                      updatedBy As String,
                                      conn As SqlConnection,
                                      trans As SqlTransaction) As Integer
            Dim sql = "UPDATE T_NAV_HISTORY SET NAV_JPY = @jpy, APPLIED_RATE = @rate, " &
                      "CALCULATED_AT = @calcAt, UPDATED_AT = @updatedAt, UPDATED_BY = @user " &
                      "WHERE NAV_HISTORY_ID = @id"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@jpy", navJpy)
                cmd.Parameters.AddWithValue("@rate", appliedRate)
                cmd.Parameters.AddWithValue("@calcAt", calculatedAt)
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now)
                cmd.Parameters.AddWithValue("@user", updatedBy)
                cmd.Parameters.AddWithValue("@id", navHistoryId)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Public Function GetMonthly(isin As String, year As Integer, month As Integer,
                                    conn As SqlConnection) As List(Of NavHistory)
            Dim monthStart = New Date(year, month, 1)
            Dim monthEnd = monthStart.AddMonths(1).AddDays(-1)
            Dim sql = "SELECT * FROM T_NAV_HISTORY " &
                      "WHERE ISIN = @isin AND NAV_DATE >= @start AND NAV_DATE <= @end " &
                      "AND NAV_JPY IS NOT NULL " &
                      "ORDER BY NAV_DATE"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@isin", isin)
                cmd.Parameters.AddWithValue("@start", monthStart)
                cmd.Parameters.AddWithValue("@end", monthEnd)
                Return ReadNavList(cmd)
            End Using
        End Function

        Public Function GetMonthEndNavs(isin As String, monthEnd As Date,
                                         conn As SqlConnection) As List(Of NavHistory)
            Dim startDate = monthEnd.AddMonths(-11)
            startDate = New Date(startDate.Year, startDate.Month, 1)
            Dim sql = "SELECT nh.* FROM T_NAV_HISTORY nh " &
                      "INNER JOIN (" &
                      "  SELECT YEAR(NAV_DATE) AS Y, MONTH(NAV_DATE) AS M, MAX(NAV_DATE) AS MaxDate " &
                      "  FROM T_NAV_HISTORY " &
                      "  WHERE ISIN = @isin AND NAV_DATE >= @start AND NAV_DATE <= @end " &
                      "    AND NAV_JPY IS NOT NULL " &
                      "  GROUP BY YEAR(NAV_DATE), MONTH(NAV_DATE)" &
                      ") sub ON nh.NAV_DATE = sub.MaxDate " &
                      "WHERE nh.ISIN = @isin " &
                      "ORDER BY nh.NAV_DATE"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@isin", isin)
                cmd.Parameters.AddWithValue("@start", startDate)
                cmd.Parameters.AddWithValue("@end", monthEnd)
                Return ReadNavList(cmd)
            End Using
        End Function

        Public Function GetLatestNav(isin As String, beforeDate As Date,
                                      conn As SqlConnection) As NavHistory
            Dim sql = "SELECT TOP 1 * FROM T_NAV_HISTORY " &
                      "WHERE ISIN = @isin AND NAV_DATE < @date " &
                      "ORDER BY NAV_DATE DESC"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@isin", isin)
                cmd.Parameters.AddWithValue("@date", beforeDate)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return MapNav(reader)
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetFirstNav(isin As String, conn As SqlConnection) As NavHistory
            Dim sql = "SELECT TOP 1 * FROM T_NAV_HISTORY WHERE ISIN = @isin ORDER BY NAV_DATE ASC"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@isin", isin)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return MapNav(reader)
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Exists(isin As String, navDate As Date,
                                conn As SqlConnection) As Boolean
            Dim sql = "SELECT COUNT(*) FROM T_NAV_HISTORY WHERE ISIN = @isin AND NAV_DATE = @date"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@isin", isin)
                cmd.Parameters.AddWithValue("@date", navDate)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Public Function BulkInsert(navList As List(Of NavHistory),
                                    conn As SqlConnection,
                                    trans As SqlTransaction) As Integer
            Dim count = 0
            Dim sql = "INSERT INTO T_NAV_HISTORY (ISIN, NAV_DATE, CURRENCY_CODE, NAV_PER_UNIT, " &
                      "TOTAL_NET_ASSETS, SHARES_OUTSTANDING, IMPORT_LOG_ID, " &
                      "CREATED_AT, CREATED_BY, UPDATED_AT, UPDATED_BY) " &
                      "VALUES (@isin, @date, @ccy, @nav, @tna, @shares, @logId, " &
                      "@createdAt, @createdBy, @updatedAt, @updatedBy)"
            For Each nav In navList
                Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                    cmd.Parameters.AddWithValue("@isin", nav.Isin)
                    cmd.Parameters.AddWithValue("@date", nav.NavDate)
                    cmd.Parameters.AddWithValue("@ccy", nav.CurrencyCode)
                    cmd.Parameters.AddWithValue("@nav", nav.NavPerUnit)
                    cmd.Parameters.AddWithValue("@tna", nav.TotalNetAssets)
                    cmd.Parameters.AddWithValue("@shares", nav.SharesOutstanding)
                    cmd.Parameters.AddWithValue("@logId", nav.ImportLogId)
                    cmd.Parameters.AddWithValue("@createdAt", nav.CreatedAt)
                    cmd.Parameters.AddWithValue("@createdBy", nav.CreatedBy)
                    cmd.Parameters.AddWithValue("@updatedAt", nav.UpdatedAt)
                    cmd.Parameters.AddWithValue("@updatedBy", nav.UpdatedBy)
                    count += cmd.ExecuteNonQuery()
                End Using
            Next
            Return count
        End Function

        Private Function ReadNavList(cmd As SqlCommand) As List(Of NavHistory)
            Dim list As New List(Of NavHistory)
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(MapNav(reader))
                End While
            End Using
            Return list
        End Function

        Private Function MapNav(reader As SqlDataReader) As NavHistory
            Dim n As New NavHistory()
            n.NavHistoryId = CInt(reader("NAV_HISTORY_ID"))
            n.Isin = reader("ISIN").ToString().Trim()
            n.NavDate = CDate(reader("NAV_DATE"))
            n.CurrencyCode = reader("CURRENCY_CODE").ToString().Trim()
            n.NavPerUnit = CDec(reader("NAV_PER_UNIT"))
            n.NavJpy = If(IsDBNull(reader("NAV_JPY")), CType(Nothing, Decimal?), CDec(reader("NAV_JPY")))
            n.TotalNetAssets = CDec(reader("TOTAL_NET_ASSETS"))
            n.SharesOutstanding = CDec(reader("SHARES_OUTSTANDING"))
            n.AppliedRate = If(IsDBNull(reader("APPLIED_RATE")), CType(Nothing, Decimal?), CDec(reader("APPLIED_RATE")))
            n.CalculatedAt = If(IsDBNull(reader("CALCULATED_AT")), CType(Nothing, DateTime?), CDate(reader("CALCULATED_AT")))
            n.ImportLogId = CInt(reader("IMPORT_LOG_ID"))
            n.CreatedAt = CDate(reader("CREATED_AT"))
            n.CreatedBy = reader("CREATED_BY").ToString()
            n.UpdatedAt = CDate(reader("UPDATED_AT"))
            n.UpdatedBy = reader("UPDATED_BY").ToString()
            Return n
        End Function

    End Class
End Namespace
