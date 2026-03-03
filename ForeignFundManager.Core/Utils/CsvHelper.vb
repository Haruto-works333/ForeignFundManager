Imports System.IO
Imports System.Text

Namespace Utils
    Public Class CsvHelper

        Public Shared Function ReadCsv(filePath As String) As List(Of String())
            Dim rows As New List(Of String())
            Using reader As New StreamReader(filePath, Encoding.UTF8, True)
                While Not reader.EndOfStream
                    Dim line = reader.ReadLine()
                    If String.IsNullOrWhiteSpace(line) Then Continue While
                    rows.Add(ParseCsvLine(line))
                End While
            End Using
            Return rows
        End Function

        Public Shared Sub WriteCsv(filePath As String, headers As String(),
                                    rows As List(Of String()))
            Using writer As New StreamWriter(filePath, False, New UTF8Encoding(True))
                writer.WriteLine(String.Join(",", headers.Select(Function(h) EscapeCsvField(h))))
                For Each row In rows
                    writer.WriteLine(String.Join(",", row.Select(Function(f) EscapeCsvField(f))))
                Next
            End Using
        End Sub

        Public Shared Function ValidateFile(filePath As String) As String
            If Not File.Exists(filePath) Then
                Return Constants.MessageConstants.MsgFileNotFound
            End If

            If Not Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase) Then
                Return Constants.MessageConstants.MsgFileExtensionInvalid
            End If

            Dim fileInfo As New FileInfo(filePath)
            If fileInfo.Length > AppConfig.MaxImportFileSize Then
                Return Constants.MessageConstants.MsgFileTooLarge
            End If

            Return ""
        End Function

        Private Shared Function ParseCsvLine(line As String) As String()
            Dim fields As New List(Of String)
            Dim current As New StringBuilder()
            Dim inQuotes = False
            Dim i = 0

            While i < line.Length
                Dim c = line(i)
                If inQuotes Then
                    If c = """"c Then
                        If i + 1 < line.Length AndAlso line(i + 1) = """"c Then
                            current.Append(""""c)
                            i += 1
                        Else
                            inQuotes = False
                        End If
                    Else
                        current.Append(c)
                    End If
                Else
                    If c = """"c Then
                        inQuotes = True
                    ElseIf c = ","c Then
                        fields.Add(current.ToString())
                        current.Clear()
                    Else
                        current.Append(c)
                    End If
                End If
                i += 1
            End While
            fields.Add(current.ToString())
            Return fields.ToArray()
        End Function

        Private Shared Function EscapeCsvField(field As String) As String
            If field Is Nothing Then Return ""
            If field.Contains(",") OrElse field.Contains("""") OrElse field.Contains(vbCr) OrElse field.Contains(vbLf) Then
                Return """" & field.Replace("""", """""") & """"
            End If
            Return field
        End Function

    End Class
End Namespace
