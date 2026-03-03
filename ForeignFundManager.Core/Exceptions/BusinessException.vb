Namespace Exceptions
    Public Class BusinessException
        Inherits Exception

        Public Property ErrorCode As String
        Public Property FieldName As String

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(message As String, errorCode As String)
            MyBase.New(message)
            Me.ErrorCode = errorCode
        End Sub

        Public Sub New(message As String, errorCode As String, fieldName As String)
            MyBase.New(message)
            Me.ErrorCode = errorCode
            Me.FieldName = fieldName
        End Sub
    End Class
End Namespace
