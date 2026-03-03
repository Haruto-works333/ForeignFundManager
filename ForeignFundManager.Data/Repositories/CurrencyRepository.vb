Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class CurrencyRepository

        Public Function GetAll(conn As SqlConnection) As List(Of CodeMaster)
            Dim sql = "SELECT CURRENCY_CODE, CURRENCY_NAME_JP FROM M_CURRENCY ORDER BY CURRENCY_CODE"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                Dim list As New List(Of CodeMaster)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim cm As New CodeMaster()
                        cm.CodeValue = reader("CURRENCY_CODE").ToString().Trim()
                        cm.CodeNameJp = reader("CURRENCY_NAME_JP").ToString()
                        list.Add(cm)
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function Exists(currencyCode As String, conn As SqlConnection) As Boolean
            Dim sql = "SELECT COUNT(*) FROM M_CURRENCY WHERE CURRENCY_CODE = @code"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@code", currencyCode)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

    End Class
End Namespace
