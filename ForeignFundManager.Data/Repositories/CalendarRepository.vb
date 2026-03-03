Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class CalendarRepository

        Public Function GetHolidays(regionCode As String, year As Integer, month As Integer,
                                     conn As SqlConnection) As List(Of BusinessCalendar)
            Dim monthStart = New Date(year, month, 1)
            Dim monthEnd = monthStart.AddMonths(1).AddDays(-1)
            Dim sql = "SELECT * FROM M_CALENDAR " &
                      "WHERE REGION_CODE = @region " &
                      "AND CALENDAR_DATE >= @start AND CALENDAR_DATE <= @end " &
                      "AND IS_HOLIDAY = 1 " &
                      "ORDER BY CALENDAR_DATE"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@region", regionCode)
                cmd.Parameters.AddWithValue("@start", monthStart)
                cmd.Parameters.AddWithValue("@end", monthEnd)
                Dim list As New List(Of BusinessCalendar)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapCalendar(reader))
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function IsHoliday(regionCode As String, calendarDate As Date,
                                   conn As SqlConnection) As Boolean
            Dim sql = "SELECT COUNT(*) FROM M_CALENDAR " &
                      "WHERE REGION_CODE = @region AND CALENDAR_DATE = @date AND IS_HOLIDAY = 1"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@region", regionCode)
                cmd.Parameters.AddWithValue("@date", calendarDate)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Public Function Insert(cal As BusinessCalendar, conn As SqlConnection,
                                trans As SqlTransaction) As Integer
            Dim sql = "INSERT INTO M_CALENDAR (REGION_CODE, CALENDAR_DATE, IS_HOLIDAY, HOLIDAY_NAME, " &
                      "CREATED_AT, CREATED_BY, UPDATED_AT, UPDATED_BY) " &
                      "VALUES (@region, @date, @isHoliday, @name, @createdAt, @createdBy, @updatedAt, @updatedBy)"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@region", cal.RegionCode)
                cmd.Parameters.AddWithValue("@date", cal.CalendarDate)
                cmd.Parameters.AddWithValue("@isHoliday", cal.IsHoliday)
                cmd.Parameters.AddWithValue("@name", If(cal.HolidayName, CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@createdAt", cal.CreatedAt)
                cmd.Parameters.AddWithValue("@createdBy", cal.CreatedBy)
                cmd.Parameters.AddWithValue("@updatedAt", cal.UpdatedAt)
                cmd.Parameters.AddWithValue("@updatedBy", cal.UpdatedBy)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Public Function Delete(regionCode As String, calendarDate As Date,
                                conn As SqlConnection, trans As SqlTransaction) As Integer
            Dim sql = "DELETE FROM M_CALENDAR WHERE REGION_CODE = @region AND CALENDAR_DATE = @date"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@region", regionCode)
                cmd.Parameters.AddWithValue("@date", calendarDate)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

        Public Function Exists(regionCode As String, calendarDate As Date,
                                conn As SqlConnection) As Boolean
            Dim sql = "SELECT COUNT(*) FROM M_CALENDAR WHERE REGION_CODE = @region AND CALENDAR_DATE = @date"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@region", regionCode)
                cmd.Parameters.AddWithValue("@date", calendarDate)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Private Function MapCalendar(reader As SqlDataReader) As BusinessCalendar
            Dim c As New BusinessCalendar()
            c.RegionCode = reader("REGION_CODE").ToString().Trim()
            c.CalendarDate = CDate(reader("CALENDAR_DATE"))
            c.IsHoliday = CBool(reader("IS_HOLIDAY"))
            c.HolidayName = If(IsDBNull(reader("HOLIDAY_NAME")), Nothing, reader("HOLIDAY_NAME").ToString())
            c.CreatedAt = CDate(reader("CREATED_AT"))
            c.CreatedBy = reader("CREATED_BY").ToString()
            c.UpdatedAt = CDate(reader("UPDATED_AT"))
            c.UpdatedBy = reader("UPDATED_BY").ToString()
            Return c
        End Function

    End Class
End Namespace
