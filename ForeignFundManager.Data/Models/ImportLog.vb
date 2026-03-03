Namespace Models
    Public Class ImportLog
        Public Property ImportLogId As Integer
        Public Property ImportType As String
        Public Property FileName As String
        Public Property ImportStartedAt As DateTime
        Public Property ImportFinishedAt As DateTime?
        Public Property TotalCount As Integer
        Public Property SuccessCount As Integer
        Public Property ErrorCount As Integer
        Public Property WarningCount As Integer
        Public Property ImportStatus As String
        Public Property CreatedAt As DateTime
        Public Property CreatedBy As String
        Public Property UpdatedAt As DateTime
        Public Property UpdatedBy As String
    End Class
End Namespace
