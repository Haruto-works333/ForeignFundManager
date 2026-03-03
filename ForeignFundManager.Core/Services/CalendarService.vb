Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Utils
Imports ForeignFundManager.Data.Infrastructure
Imports ForeignFundManager.Data.Repositories
Imports NLog

Namespace Services
    Public Class CalendarService

        Private ReadOnly _calRepo As New CalendarRepository()
        Private ReadOnly _codeRepo As New CodeRepository()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function GetHolidays(regionCode As String, year As Integer, month As Integer) As List(Of BusinessCalendar)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _calRepo.GetHolidays(regionCode, year, month, conn)
            End Using
        End Function

        Public Sub AddHoliday(cal As BusinessCalendar)
            Dim now = DateTime.Now
            cal.IsHoliday = True
            cal.CreatedAt = now
            cal.CreatedBy = AppConfig.UserName
            cal.UpdatedAt = now
            cal.UpdatedBy = AppConfig.UserName

            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                If _calRepo.Exists(cal.RegionCode, cal.CalendarDate, conn) Then
                    Throw New BusinessException("指定された地域・日付の祝日は既に登録されています。", "CAL_DUPLICATE")
                End If
                Using trans = conn.BeginTransaction()
                    Try
                        _calRepo.Insert(cal, conn, trans)
                        trans.Commit()
                        _logger.Info($"Holiday added: {cal.RegionCode} {cal.CalendarDate:yyyy-MM-dd} {cal.HolidayName}")
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Sub RemoveHoliday(regionCode As String, calendarDate As Date)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        _calRepo.Delete(regionCode, calendarDate, conn, trans)
                        trans.Commit()
                        _logger.Info($"Holiday removed: {regionCode} {calendarDate:yyyy-MM-dd}")
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Function GetRegionCodes() As List(Of CodeMaster)
            Using conn = DbConnectionFactory.CreateConnection()
                conn.Open()
                Return _codeRepo.GetByType(AppConstants.CodeTypeRegion, conn)
            End Using
        End Function

    End Class
End Namespace
