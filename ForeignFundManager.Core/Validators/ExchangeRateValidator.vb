Imports System.Text.RegularExpressions

Namespace Validators
    Public Class ExchangeRateValidator

        Public Function ValidateRow(rowNumber As Integer, columns As String()) As List(Of String)
            Dim errors As New List(Of String)

            If columns.Length < 5 Then
                errors.Add($"行{rowNumber}: カラム数が不足しています（期待:5, 実際:{columns.Length}）")
                Return errors
            End If

            Dim currencyCode = columns(0).Trim()
            Dim rateDateStr = columns(1).Trim()
            Dim ttmStr = columns(2).Trim()
            Dim ttsStr = columns(3).Trim()
            Dim ttbStr = columns(4).Trim()

            If Not Regex.IsMatch(currencyCode, "^[A-Z]{3}$") Then
                errors.Add($"行{rowNumber}: 通貨コードは英大文字3桁で入力してください")
            End If

            Dim rateDate As Date
            If Not Date.TryParse(rateDateStr, rateDate) Then
                errors.Add($"行{rowNumber}: 基準日が不正な日付です")
            ElseIf rateDate > Date.Today Then
                errors.Add($"行{rowNumber}: 基準日に未来日は指定できません")
            End If

            Dim ttm As Decimal
            Dim tts As Decimal
            Dim ttb As Decimal
            Dim hasRateError = False

            If Not Decimal.TryParse(ttmStr, ttm) OrElse ttm <= 0 Then
                errors.Add($"行{rowNumber}: TTMが不正な数値です")
                hasRateError = True
            End If
            If Not Decimal.TryParse(ttsStr, tts) OrElse tts <= 0 Then
                errors.Add($"行{rowNumber}: TTSが不正な数値です")
                hasRateError = True
            End If
            If Not Decimal.TryParse(ttbStr, ttb) OrElse ttb <= 0 Then
                errors.Add($"行{rowNumber}: TTBが不正な数値です")
                hasRateError = True
            End If

            If Not hasRateError Then
                If ttb > ttm OrElse ttm > tts Then
                    errors.Add($"行{rowNumber}: TTB ≤ TTM ≤ TTS の関係を満たしてください")
                End If
            End If

            Return errors
        End Function

    End Class
End Namespace
