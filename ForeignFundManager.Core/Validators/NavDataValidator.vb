Imports System.Text.RegularExpressions

Namespace Validators
    Public Class NavDataValidator

        Public Function ValidateRow(rowNumber As Integer, columns As String()) As List(Of String)
            Dim errors As New List(Of String)

            If columns.Length < 7 Then
                errors.Add($"行{rowNumber}: カラム数が不足しています（期待:7, 実際:{columns.Length}）")
                Return errors
            End If

            Dim isin = columns(0).Trim()
            Dim fundName = columns(1).Trim()
            Dim navDateStr = columns(2).Trim()
            Dim currency = columns(3).Trim()
            Dim navPerUnitStr = columns(4).Trim()
            Dim totalNetAssetsStr = columns(5).Trim()
            Dim sharesOutstandingStr = columns(6).Trim()

            If String.IsNullOrEmpty(isin) OrElse isin.Length <> 12 Then
                errors.Add($"行{rowNumber}: ISINは12桁で入力してください")
            ElseIf Not IsinValidator.Validate(isin) Then
                errors.Add($"行{rowNumber}: ISINのチェックディジットが不正です")
            End If

            If String.IsNullOrEmpty(fundName) Then
                errors.Add($"行{rowNumber}: ファンド名は必須です")
            End If

            Dim navDate As Date
            If Not Date.TryParse(navDateStr, navDate) Then
                errors.Add($"行{rowNumber}: 基準日が不正な日付です")
            ElseIf navDate > Date.Today Then
                errors.Add($"行{rowNumber}: 基準日に未来日は指定できません")
            End If

            If Not Regex.IsMatch(currency, "^[A-Z]{3}$") Then
                errors.Add($"行{rowNumber}: 通貨コードは英大文字3桁で入力してください")
            End If

            Dim navPerUnit As Decimal
            If Not Decimal.TryParse(navPerUnitStr, navPerUnit) Then
                errors.Add($"行{rowNumber}: 基準価額が不正な数値です")
            ElseIf navPerUnit <= 0 Then
                errors.Add($"行{rowNumber}: 基準価額は正の数値を入力してください")
            ElseIf navPerUnitStr.Contains(".") AndAlso navPerUnitStr.Split("."c)(1).Length > 6 Then
                errors.Add($"行{rowNumber}: 基準価額の小数部は6桁以内で入力してください")
            End If

            Dim tna As Decimal
            If Not Decimal.TryParse(totalNetAssetsStr, tna) Then
                errors.Add($"行{rowNumber}: 純資産総額が不正な数値です")
            ElseIf tna <= 0 Then
                errors.Add($"行{rowNumber}: 純資産総額は正の数値を入力してください")
            End If

            Dim shares As Decimal
            If Not Decimal.TryParse(sharesOutstandingStr, shares) Then
                errors.Add($"行{rowNumber}: 発行済口数が不正な数値です")
            ElseIf shares <= 0 Then
                errors.Add($"行{rowNumber}: 発行済口数は正の数値を入力してください")
            End If

            Return errors
        End Function

    End Class
End Namespace
