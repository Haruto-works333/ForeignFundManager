Namespace Models
    Public Class AuditLog
        Public Property AuditLogId As Integer
        Public Property OperationId As String
        Public Property TableName As String
        Public Property RecordKey As String
        Public Property OperationType As String
        Public Property ColumnName As String
        Public Property OldValue As String
        Public Property NewValue As String
        Public Property OperatedAt As DateTime
        Public Property OperatedBy As String
    End Class
End Namespace
