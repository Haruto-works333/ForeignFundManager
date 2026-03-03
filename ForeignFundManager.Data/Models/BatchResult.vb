Namespace Models
    Public Class BatchResult
        Public Property TotalCount As Integer
        Public Property SuccessCount As Integer
        Public Property SkippedCount As Integer
        Public Property ErrorCount As Integer
        Public Property Elapsed As TimeSpan
        Public Property Details As New List(Of BatchResultDetail)
    End Class
End Namespace
