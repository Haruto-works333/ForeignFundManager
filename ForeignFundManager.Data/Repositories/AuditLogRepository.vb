Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class AuditLogRepository

        Public Function Insert(logs As List(Of AuditLog),
                                conn As SqlConnection,
                                trans As SqlTransaction) As Integer
            Dim count = 0
            Dim sql = "INSERT INTO T_AUDIT_LOG (OPERATION_ID, TABLE_NAME, RECORD_KEY, " &
                      "OPERATION_TYPE, COLUMN_NAME, OLD_VALUE, NEW_VALUE, OPERATED_AT, OPERATED_BY) " &
                      "VALUES (@opId, @table, @key, @opType, @col, @oldVal, @newVal, @opAt, @opBy)"
            For Each log In logs
                Using cmd = DbConnectionFactory.CreateCommand(sql, conn, trans)
                    cmd.Parameters.AddWithValue("@opId", log.OperationId)
                    cmd.Parameters.AddWithValue("@table", log.TableName)
                    cmd.Parameters.AddWithValue("@key", log.RecordKey)
                    cmd.Parameters.AddWithValue("@opType", log.OperationType)
                    cmd.Parameters.AddWithValue("@col", If(log.ColumnName, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@oldVal", If(log.OldValue, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@newVal", If(log.NewValue, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@opAt", log.OperatedAt)
                    cmd.Parameters.AddWithValue("@opBy", log.OperatedBy)
                    count += cmd.ExecuteNonQuery()
                End Using
            Next
            Return count
        End Function

        Public Function GetByTableAndKey(tableName As String, recordKey As String,
                                          conn As SqlConnection) As List(Of AuditLog)
            Dim sql = "SELECT * FROM T_AUDIT_LOG " &
                      "WHERE TABLE_NAME = @table AND RECORD_KEY = @key " &
                      "ORDER BY OPERATED_AT DESC"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@table", tableName)
                cmd.Parameters.AddWithValue("@key", recordKey)
                Dim list As New List(Of AuditLog)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim a As New AuditLog()
                        a.AuditLogId = CInt(reader("AUDIT_LOG_ID"))
                        a.OperationId = reader("OPERATION_ID").ToString()
                        a.TableName = reader("TABLE_NAME").ToString()
                        a.RecordKey = reader("RECORD_KEY").ToString()
                        a.OperationType = reader("OPERATION_TYPE").ToString()
                        a.ColumnName = If(IsDBNull(reader("COLUMN_NAME")), Nothing, reader("COLUMN_NAME").ToString())
                        a.OldValue = If(IsDBNull(reader("OLD_VALUE")), Nothing, reader("OLD_VALUE").ToString())
                        a.NewValue = If(IsDBNull(reader("NEW_VALUE")), Nothing, reader("NEW_VALUE").ToString())
                        a.OperatedAt = CDate(reader("OPERATED_AT"))
                        a.OperatedBy = reader("OPERATED_BY").ToString()
                        list.Add(a)
                    End While
                End Using
                Return list
            End Using
        End Function

    End Class
End Namespace
