Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class CountryRepository

        Public Function GetAll(conn As SqlConnection) As List(Of CodeMaster)
            Dim sql = "SELECT COUNTRY_CODE, COUNTRY_NAME_JP FROM M_COUNTRY ORDER BY COUNTRY_CODE"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                Dim list As New List(Of CodeMaster)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim cm As New CodeMaster()
                        cm.CodeValue = reader("COUNTRY_CODE").ToString().Trim()
                        cm.CodeNameJp = reader("COUNTRY_NAME_JP").ToString()
                        list.Add(cm)
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function Exists(countryCode As String, conn As SqlConnection) As Boolean
            Dim sql = "SELECT COUNT(*) FROM M_COUNTRY WHERE COUNTRY_CODE = @code"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@code", countryCode)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Public Function GetNameJp(countryCode As String, conn As SqlConnection) As String
            Dim sql = "SELECT COUNTRY_NAME_JP FROM M_COUNTRY WHERE COUNTRY_CODE = @code"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@code", countryCode)
                Dim result = cmd.ExecuteScalar()
                Return If(result IsNot Nothing, result.ToString(), "")
            End Using
        End Function

    End Class
End Namespace
