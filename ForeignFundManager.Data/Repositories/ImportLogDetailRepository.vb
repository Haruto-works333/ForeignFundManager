Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class ImportLogDetailRepository

        Public Function BulkInsert(details As List(Of ImportLogDetail),
                                    conn As SqlConnection,
                                    Optional trans As SqlTransaction = Nothing) As Integer
            Dim count = 0
            Dim sql = "INSERT INTO T_IMPORT_LOG_DETAIL (IMPORT_LOG_ID, ROW_NUMBER, RESULT_TYPE, " &
                      "MESSAGE, ROW_DATA, CREATED_AT, CREATED_BY, UPDATED_AT, UPDATED_BY) " &
                      "VALUES (@logId, @row, @type, @msg, @data, @createdAt, @createdBy, @updatedAt, @updatedBy)"
            For Each d In details
                Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                    cmd.Parameters.AddWithValue("@logId", d.ImportLogId)
                    cmd.Parameters.AddWithValue("@row", d.RowNumber)
                    cmd.Parameters.AddWithValue("@type", d.ResultType)
                    cmd.Parameters.AddWithValue("@msg", d.Message)
                    cmd.Parameters.AddWithValue("@data", If(d.RowData, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@createdAt", d.CreatedAt)
                    cmd.Parameters.AddWithValue("@createdBy", d.CreatedBy)
                    cmd.Parameters.AddWithValue("@updatedAt", d.UpdatedAt)
                    cmd.Parameters.AddWithValue("@updatedBy", d.UpdatedBy)
                    count += cmd.ExecuteNonQuery()
                End Using
            Next
            Return count
        End Function

        Public Function GetByLogId(importLogId As Integer,
                                    conn As SqlConnection) As List(Of ImportLogDetail)
            Dim sql = "SELECT * FROM T_IMPORT_LOG_DETAIL WHERE IMPORT_LOG_ID = @logId ORDER BY ROW_NUMBER"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@logId", importLogId)
                Dim list As New List(Of ImportLogDetail)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim d As New ImportLogDetail()
                        d.ImportLogDetailId = CInt(reader("IMPORT_LOG_DETAIL_ID"))
                        d.ImportLogId = CInt(reader("IMPORT_LOG_ID"))
                        d.RowNumber = CInt(reader("ROW_NUMBER"))
                        d.ResultType = reader("RESULT_TYPE").ToString()
                        d.Message = reader("MESSAGE").ToString()
                        d.RowData = If(IsDBNull(reader("ROW_DATA")), Nothing, reader("ROW_DATA").ToString())
                        d.CreatedAt = CDate(reader("CREATED_AT"))
                        d.CreatedBy = reader("CREATED_BY").ToString()
                        d.UpdatedAt = CDate(reader("UPDATED_AT"))
                        d.UpdatedBy = reader("UPDATED_BY").ToString()
                        list.Add(d)
                    End While
                End Using
                Return list
            End Using
        End Function

    End Class
End Namespace
