Imports System.Text.RegularExpressions

Namespace Validators
    Public Class CalendarValidator

        Public Function ValidateRow(rowNumber As Integer, columns As String()) As List(Of String)
            Dim errors As New List(Of String)

            If columns.Length < 4 Then
                errors.Add($"行{rowNumber}: カラム数が不足しています（期待:4, 実際:{columns.Length}）")
                Return errors
            End If

            Dim regionCode = columns(0).Trim()
            Dim dateStr = columns(1).Trim()
            Dim holidayFlag = columns(2).Trim()
            Dim holidayName = columns(3).Trim()

            If Not Regex.IsMatch(regionCode, "^[A-Z]{2}$") Then
                errors.Add($"行{rowNumber}: 地域コードは英大文字2桁で入力してください")
            End If

            Dim calDate As Date
            If Not Date.TryParse(dateStr, calDate) Then
                errors.Add($"行{rowNumber}: 日付が不正です")
            End If

            If holidayFlag <> "0" AndAlso holidayFlag <> "1" Then
                errors.Add($"行{rowNumber}: 休日フラグは0または1で入力してください")
            End If

            If holidayFlag = "1" AndAlso String.IsNullOrEmpty(holidayName) Then
                errors.Add($"行{rowNumber}: 休日の場合、祝日名は必須です")
            End If

            Return errors
        End Function

    End Class
End Namespace
