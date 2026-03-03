Namespace Models
    Public Class NavHistory
        Public Property NavHistoryId As Integer
        Public Property Isin As String
        Public Property NavDate As Date
        Public Property CurrencyCode As String
        Public Property NavPerUnit As Decimal
        Public Property NavJpy As Decimal?
        Public Property TotalNetAssets As Decimal
        Public Property SharesOutstanding As Decimal
        Public Property AppliedRate As Decimal?
        Public Property CalculatedAt As DateTime?
        Public Property ImportLogId As Integer
        Public Property CreatedAt As DateTime
        Public Property CreatedBy As String
        Public Property UpdatedAt As DateTime
        Public Property UpdatedBy As String
    End Class
End Namespace
