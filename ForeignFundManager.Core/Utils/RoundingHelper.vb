Namespace Utils
    Public Class RoundingHelper

        Public Shared Function ApplyRounding(value As Decimal, roundingType As String) As Decimal
            Select Case roundingType
                Case "1"
                    Return Math.Floor(value)
                Case "2"
                    Return Math.Round(value, 0, MidpointRounding.AwayFromZero)
                Case "3"
                    Return Math.Ceiling(value)
                Case Else
                    Return Math.Round(value, 0, MidpointRounding.AwayFromZero)
            End Select
        End Function

    End Class
End Namespace
