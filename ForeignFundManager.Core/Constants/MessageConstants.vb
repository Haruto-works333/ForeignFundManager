Namespace Constants
    Public Class MessageConstants
        Public Const MsgConfirmSave As String = "保存してよろしいですか？"
        Public Const MsgConfirmDelete As String = "ISIN: {0} を削除してよろしいですか？"
        Public Const MsgSaveSuccess As String = "保存が完了しました。"
        Public Const MsgDeleteSuccess As String = "削除が完了しました。"
        Public Const MsgConfirmDiscard As String = "変更内容を破棄しますか？"
        Public Const MsgSystemError As String = "システムエラーが発生しました。管理者に連絡してください。"
        Public Const MsgNoSelection As String = "対象を選択してください。"

        Public Const MsgIsinDuplicate As String = "ISIN: {0} は既に登録されています。"
        Public Const MsgIsinInvalidCheckDigit As String = "ISINのチェックディジットが不正です。"
        Public Const MsgCurrencyNotFound As String = "指定された通貨コードが存在しません。"
        Public Const MsgCountryNotFound As String = "指定された籍国コードが存在しません。"
        Public Const MsgFutureDateNotAllowed As String = "未来日は指定できません。"

        Public Const MsgRateOrderInvalid As String = "TTB ≤ TTM ≤ TTS の関係を満たしてください。"
        Public Const MsgRateDuplicate As String = "同一通貨・同一日の為替レートが既に登録されています。"

        Public Const MsgFileNotFound As String = "ファイルが見つかりません。"
        Public Const MsgFileExtensionInvalid As String = "CSVファイル（.csv）を選択してください。"
        Public Const MsgFileTooLarge As String = "ファイルサイズが上限（10MB）を超えています。"
        Public Const MsgHeaderMismatch As String = "ヘッダー行が定義と一致しません。"
        Public Const MsgNoDataRows As String = "データ行がありません。"

        Public Const MsgConfirmBatchExecute As String = "対象日付 {0} の基準価額計算を実行してよろしいですか？"
        Public Const MsgBatchNoRecords As String = "処理対象のレコードがありません。"
        Public Const MsgBatchRateNotFound As String = "為替レートが取得できません。ISIN={0}, 通貨={1}, 日付={2}"
        Public Const MsgBatchPrevRateUsed As String = "直前営業日レートを使用。日付={0}"

        Public Const MsgReportNoFundSelected As String = "対象ファンドを1件以上選択してください。"
        Public Const MsgReportOutputSuccess As String = "{0}件のレポートを出力しました。"
    End Class
End Namespace
