Imports System.Data.SqlClient
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Data.Infrastructure

Namespace Repositories
    Public Class CodeRepository

        Public Function GetByType(codeType As String, conn As SqlConnection) As List(Of CodeMaster)
            Dim sql = "SELECT * FROM M_CODE WHERE CODE_TYPE = @type AND IS_ACTIVE = 1 ORDER BY SORT_ORDER"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@type", codeType)
                Dim list As New List(Of CodeMaster)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapCode(reader))
                    End While
                End Using
                Return list
            End Using
        End Function

        Public Function GetCodeName(codeType As String, codeValue As String,
                                     conn As SqlConnection) As String
            Dim sql = "SELECT CODE_NAME_JP FROM M_CODE WHERE CODE_TYPE = @type AND CODE_VALUE = @value"
            Using cmd = DbConnectionFactory.CreateCommand(sql, conn)
                cmd.Parameters.AddWithValue("@type", codeType)
                cmd.Parameters.AddWithValue("@value", codeValue)
                Dim result = cmd.ExecuteScalar()
                Return If(result IsNot Nothing, result.ToString(), "")
            End Using
        End Function

        Private Function MapCode(reader As SqlDataReader) As CodeMaster
            Dim c As New CodeMaster()
            c.CodeType = reader("CODE_TYPE").ToString()
            c.CodeValue = reader("CODE_VALUE").ToString()
            c.CodeNameJp = reader("CODE_NAME_JP").ToString()
            c.SortOrder = CInt(reader("SORT_ORDER"))
            c.IsActive = CBool(reader("IS_ACTIVE"))
            c.CreatedAt = CDate(reader("CREATED_AT"))
            c.CreatedBy = reader("CREATED_BY").ToString()
            c.UpdatedAt = CDate(reader("UPDATED_AT"))
            c.UpdatedBy = reader("UPDATED_BY").ToString()
            Return c
        End Function

    End Class
End Namespace
