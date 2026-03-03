Imports System.Configuration
Imports System.Data.SqlClient

Namespace Infrastructure
    Public Class DbConnectionFactory

        Private Shared ReadOnly ConnectionStringName As String = "ForeignFundManager"
        Private Shared ReadOnly CommandTimeoutSeconds As Integer = 60

        Public Shared Function CreateConnection() As SqlConnection
            Dim connStr = ConfigurationManager.ConnectionStrings(ConnectionStringName).ConnectionString
            Return New SqlConnection(connStr)
        End Function

        Public Shared Function CreateCommand(sql As String, conn As SqlConnection,
                                              Optional trans As SqlTransaction = Nothing) As SqlCommand
            Dim cmd As New SqlCommand(sql, conn)
            cmd.CommandTimeout = CommandTimeoutSeconds
            If trans IsNot Nothing Then
                cmd.Transaction = trans
            End If
            Return cmd
        End Function

    End Class
End Namespace
