Namespace Constants
    Public Class AppConstants
        Public Const FundStatusActive As String = "1"
        Public Const FundStatusDeleted As String = "9"

        Public Const CodeTypeRounding As String = "ROUNDING_TYPE"
        Public Const CodeTypeSettlement As String = "SETTLEMENT_FREQ"
        Public Const CodeTypeFundStatus As String = "FUND_STATUS"
        Public Const CodeTypeRegion As String = "REGION_CODE"
        Public Const CodeTypeRateType As String = "RATE_TYPE"
        Public Const CodeTypeImportType As String = "IMPORT_TYPE"

        Public Const ImportTypeNav As String = "NAV"
        Public Const ImportTypeRate As String = "RATE"
        Public Const ImportTypeCalendar As String = "CAL"

        Public Const ImportStatusRunning As String = "RUNNING"
        Public Const ImportStatusSuccess As String = "SUCCESS"
        Public Const ImportStatusError As String = "ERROR"

        Public Const OperationInsert As String = "INSERT"
        Public Const OperationUpdate As String = "UPDATE"
        Public Const OperationDelete As String = "DELETE"

        Public Shared ReadOnly NavCsvHeaders As String() = {"ISIN", "FundName", "NAVDate", "Currency", "NAVPerUnit", "TotalNetAssets", "SharesOutstanding"}
        Public Shared ReadOnly RateCsvHeaders As String() = {"CurrencyCode", "RateDate", "TTM", "TTS", "TTB"}
        Public Shared ReadOnly CalendarCsvHeaders As String() = {"RegionCode", "Date", "HolidayFlag", "HolidayName"}
        Public Shared ReadOnly FundExportHeaders As String() = {"ISIN", "FundNameEN", "FundNameJP", "CurrencyCode", "DomicileCountryCode", "InceptionDate", "SettlementFrequency", "RoundingType", "Status", "Remarks"}

        Public Const RegionJapan As String = "JP"

        Public Const ExitCodeSuccess As Integer = 0
        Public Const ExitCodeWarning As Integer = 1
        Public Const ExitCodeFatal As Integer = 9
    End Class
End Namespace
