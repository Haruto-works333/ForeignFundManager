Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class ImportLogRepository

        Public Function Insert(log As ImportLog, conn As SqlConnection,
                                Optional trans As SqlTransaction = Nothing) As Integer
            Dim sql = "INSERT INTO T_IMPORT_LOG (IMPORT_TYPE, FILE_NAME, IMPORT_STARTED_AT, " &
                      "IMPORT_FINISHED_AT, TOTAL_COUNT, SUCCESS_COUNT, ERROR_COUNT, WARNING_COUNT, " &
                      "IMPORT_STATUS, CREATED_AT, CREATED_BY, UPDATED_AT, UPDATED_BY) " &
                      "VALUES (@type, @file, @started, @finished, @total, @success, @error, @warning, " &
                      "@status, @createdAt, @createdBy, @updatedAt, @updatedBy); " &
                      "SELECT SCOPE_IDENTITY()"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@type", log.ImportType)
                cmd.Parameters.AddWithValue("@file", log.FileName)
                cmd.Parameters.AddWithValue("@started", log.ImportStartedAt)
                cmd.Parameters.AddWithValue("@finished", If(log.ImportFinishedAt.HasValue, CObj(log.ImportFinishedAt.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@total", log.TotalCount)
                cmd.Parameters.AddWithValue("@success", log.SuccessCount)
                cmd.Parameters.AddWithValue("@error", log.ErrorCount)
                cmd.Parameters.AddWithValue("@warning", log.WarningCount)
                cmd.Parameters.AddWithValue("@status", log.ImportStatus)
                cmd.Parameters.AddWithValue("@createdAt", log.CreatedAt)
                cmd.Parameters.AddWithValue("@createdBy", log.CreatedBy)
                cmd.Parameters.AddWithValue("@updatedAt", log.UpdatedAt)
                cmd.Parameters.AddWithValue("@updatedBy", log.UpdatedBy)
                Return CInt(cmd.ExecuteScalar())
            End Using
        End Function

        Public Function UpdateResult(importLogId As Integer, importFinishedAt As DateTime,
                                      totalCount As Integer, successCount As Integer,
                                      errorCount As Integer, warningCount As Integer,
                                      importStatus As String,
                                      conn As SqlConnection,
                                      Optional trans As SqlTransaction = Nothing) As Integer
            Dim sql = "UPDATE T_IMPORT_LOG SET IMPORT_FINISHED_AT = @finished, " &
                      "TOTAL_COUNT = @total, SUCCESS_COUNT = @success, " &
                      "ERROR_COUNT = @error, WARNING_COUNT = @warning, " &
                      "IMPORT_STATUS = @status, UPDATED_AT = @updatedAt " &
                      "WHERE IMPORT_LOG_ID = @id"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@finished", importFinishedAt)
                cmd.Parameters.AddWithValue("@total", totalCount)
                cmd.Parameters.AddWithValue("@success", successCount)
                cmd.Parameters.AddWithValue("@error", errorCount)
                cmd.Parameters.AddWithValue("@warning", warningCount)
                cmd.Parameters.AddWithValue("@status", importStatus)
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now)
                cmd.Parameters.AddWithValue("@id", importLogId)
                Return cmd.ExecuteNonQuery()
            End Using
        End Function

    End Class
End Namespace
