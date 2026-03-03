Imports System.Text.RegularExpressions

Namespace Validators
    Public Class IsinValidator

        Public Shared Function Validate(isin As String) As Boolean
            If String.IsNullOrEmpty(isin) OrElse isin.Length <> 12 Then
                Return False
            End If

            If Not Regex.IsMatch(isin, "^[A-Z]{2}[A-Z0-9]{9}[0-9]$") Then
                Return False
            End If

            Dim digits As New Text.StringBuilder()
            For i = 0 To 10
                Dim c = isin(i)
                If Char.IsLetter(c) Then
                    Dim val = Asc(c) - Asc("A"c) + 10
                    digits.Append(val.ToString())
                Else
                    digits.Append(c)
                End If
            Next

            Dim digitStr = digits.ToString()
            Dim sum = 0
            Dim isDouble = True

            For i = digitStr.Length - 1 To 0 Step -1
                Dim d = CInt(Char.GetNumericValue(digitStr(i)))
                If isDouble Then
                    d *= 2
                    If d > 9 Then d -= 9
                End If
                sum += d
                isDouble = Not isDouble
            Next

            Dim checkDigit = (10 - (sum Mod 10)) Mod 10
            Return checkDigit = CInt(Char.GetNumericValue(isin(11)))
        End Function

    End Class
End Namespace
