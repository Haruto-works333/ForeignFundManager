Imports System.Configuration

Namespace Utils
    Public Class AppConfig

        Public Shared ReadOnly Property ConnectionString As String
            Get
                Return ConfigurationManager.ConnectionStrings("ForeignFundManager").ConnectionString
            End Get
        End Property

        Public Shared ReadOnly Property UserName As String
            Get
                Return GetSetting("UserName", "admin")
            End Get
        End Property

        Public Shared ReadOnly Property NavChangeThreshold As Decimal
            Get
                Dim val As Decimal
                If Decimal.TryParse(GetSetting("NavChangeThreshold", "10"), val) Then
                    Return val
                End If
                Return 10D
            End Get
        End Property

        Public Shared ReadOnly Property MaxRateLookbackDays As Integer
            Get
                Dim val As Integer
                If Integer.TryParse(GetSetting("MaxRateLookbackDays", "5"), val) Then
                    Return val
                End If
                Return 5
            End Get
        End Property

        Public Shared ReadOnly Property MaxImportFileSize As Long
            Get
                Dim val As Long
                If Long.TryParse(GetSetting("MaxImportFileSize", "10485760"), val) Then
                    Return val
                End If
                Return 10485760L
            End Get
        End Property

        Private Shared Function GetSetting(key As String, defaultValue As String) As String
            Dim val = ConfigurationManager.AppSettings(key)
            Return If(String.IsNullOrEmpty(val), defaultValue, val)
        End Function

    End Class
End Namespace
