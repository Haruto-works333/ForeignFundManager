Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms
    Public Class FrmFundEdit

        Private ReadOnly _fundService As New FundService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>新規登録モードかどうかを示す。</summary>
        Public Property IsNew As Boolean

        ''' <summary>編集対象のISINコード。</summary>
        Public Property Isin As String

        ''' <summary>
        ''' 画面ロード時の処理。コンボボックスの初期化とデータ読み込み。
        ''' </summary>
        Private Sub FrmFundEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                InitializeComboBoxes()

                If IsNew Then
                    Me.Text = "ファンド新規登録"
                    txtIsin.ReadOnly = False
                    txtIsin.BackColor = SystemColors.Window
                Else
                    Me.Text = "ファンド編集"
                    txtIsin.ReadOnly = True
                    txtIsin.BackColor = SystemColors.Control
                    LoadFundData()
                End If

            Catch bex As BusinessException
                MessageBox.Show(bex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "FrmFundEdit_Load")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End Try
        End Sub

        ''' <summary>
        ''' 通貨・籍国・決算頻度・端数処理のコンボボックスにマスタデータをバインドする。
        ''' </summary>
        Private Sub InitializeComboBoxes()
            ' 通貨コンボボックス
            Dim currencies = _fundService.GetCurrencies()
            cmbCurrency.DataSource = currencies
            cmbCurrency.DisplayMember = "CodeNameJp"
            cmbCurrency.ValueMember = "CodeValue"

            ' 籍国コンボボックス
            Dim countries = _fundService.GetCountries()
            cmbCountry.DataSource = countries
            cmbCountry.DisplayMember = "CodeNameJp"
            cmbCountry.ValueMember = "CodeValue"

            ' 決算頻度コンボボックス
            Dim settlementFreqs = _fundService.GetCodeList(AppConstants.CodeTypeSettlement)
            cmbSettlementFrequency.DataSource = settlementFreqs
            cmbSettlementFrequency.DisplayMember = "CodeNameJp"
            cmbSettlementFrequency.ValueMember = "CodeValue"

            ' 端数処理コンボボックス
            Dim roundingTypes = _fundService.GetCodeList(AppConstants.CodeTypeRounding)
            cmbRoundingType.DataSource = roundingTypes
            cmbRoundingType.DisplayMember = "CodeNameJp"
            cmbRoundingType.ValueMember = "CodeValue"
        End Sub

        ''' <summary>
        ''' 編集対象のファンドデータを読み込み、各コントロールにセットする。
        ''' </summary>
        Private Sub LoadFundData()
            Dim fund = _fundService.GetByIsin(Isin)
            If fund Is Nothing Then
                Throw New BusinessException($"ISIN: {Isin} のファンドが見つかりません。", "FUND_NOT_FOUND", "Isin")
            End If

            txtIsin.Text = fund.Isin
            txtFundNameEn.Text = fund.FundNameEn
            txtFundNameJp.Text = fund.FundNameJp
            cmbCurrency.SelectedValue = fund.CurrencyCode
            cmbCountry.SelectedValue = fund.DomicileCountryCode
            dtpInceptionDate.Value = fund.InceptionDate
            cmbSettlementFrequency.SelectedValue = fund.SettlementFrequency
            cmbRoundingType.SelectedValue = fund.RoundingType
            txtRemarks.Text = If(fund.Remarks, "")
        End Sub

        ''' <summary>
        ''' 保存ボタンクリック時の処理。バリデーション後にFundServiceを呼び出す。
        ''' </summary>
        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Try
                ' UI側の必須チェック
                If Not ValidateInput() Then Return

                ' 確認ダイアログ
                Dim result = MessageBox.Show(
                    MessageConstants.MsgConfirmSave,
                    "確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)

                If result <> DialogResult.Yes Then Return

                ' Fundオブジェクトの構築
                Dim fund As New Fund()
                fund.Isin = txtIsin.Text.Trim()
                fund.FundNameEn = txtFundNameEn.Text.Trim()
                fund.FundNameJp = txtFundNameJp.Text.Trim()
                fund.CurrencyCode = cmbCurrency.SelectedValue.ToString()
                fund.DomicileCountryCode = cmbCountry.SelectedValue.ToString()
                fund.InceptionDate = dtpInceptionDate.Value.Date
                fund.SettlementFrequency = cmbSettlementFrequency.SelectedValue.ToString()
                fund.RoundingType = cmbRoundingType.SelectedValue.ToString()
                fund.Remarks = txtRemarks.Text.Trim()

                ' 編集時はステータスを維持（既存値をロードして引き継ぐ）
                If Not IsNew Then
                    Dim existingFund = _fundService.GetByIsin(Isin)
                    If existingFund IsNot Nothing Then
                        fund.Status = existingFund.Status
                    End If
                End If

                ' サービス呼び出し
                _fundService.Save(fund, IsNew)

                MessageBox.Show(MessageConstants.MsgSaveSuccess, "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()

            Catch bex As BusinessException
                MessageBox.Show(bex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                FocusOnErrorField(bex.FieldName)
            Catch ex As Exception
                _logger.Fatal(ex, "btnSave_Click")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' UI側の必須入力チェックを行う。
        ''' </summary>
        Private Function ValidateInput() As Boolean
            Dim isValid = True

            ' 背景色リセット
            ResetFieldBackColors()

            ' ISIN
            If String.IsNullOrWhiteSpace(txtIsin.Text) Then
                HighlightField(txtIsin)
                isValid = False
            End If

            ' ファンド名称（英語）
            If String.IsNullOrWhiteSpace(txtFundNameEn.Text) Then
                HighlightField(txtFundNameEn)
                isValid = False
            End If

            ' ファンド名称（日本語）
            If String.IsNullOrWhiteSpace(txtFundNameJp.Text) Then
                HighlightField(txtFundNameJp)
                isValid = False
            End If

            ' 通貨
            If cmbCurrency.SelectedIndex < 0 Then
                HighlightField(cmbCurrency)
                isValid = False
            End If

            ' 籍国
            If cmbCountry.SelectedIndex < 0 Then
                HighlightField(cmbCountry)
                isValid = False
            End If

            ' 決算頻度
            If cmbSettlementFrequency.SelectedIndex < 0 Then
                HighlightField(cmbSettlementFrequency)
                isValid = False
            End If

            ' 端数処理
            If cmbRoundingType.SelectedIndex < 0 Then
                HighlightField(cmbRoundingType)
                isValid = False
            End If

            If Not isValid Then
                MessageBox.Show("必須項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            Return isValid
        End Function

        ''' <summary>
        ''' 必須未入力項目の背景色を薄黄色にする。
        ''' </summary>
        Private Sub HighlightField(ctrl As Control)
            ctrl.BackColor = Color.FromArgb(255, 255, 200)
        End Sub

        ''' <summary>
        ''' 全フィールドの背景色をリセットする。
        ''' </summary>
        Private Sub ResetFieldBackColors()
            txtIsin.BackColor = If(IsNew, SystemColors.Window, SystemColors.Control)
            txtFundNameEn.BackColor = SystemColors.Window
            txtFundNameJp.BackColor = SystemColors.Window
            cmbCurrency.BackColor = SystemColors.Window
            cmbCountry.BackColor = SystemColors.Window
            cmbSettlementFrequency.BackColor = SystemColors.Window
            cmbRoundingType.BackColor = SystemColors.Window
        End Sub

        ''' <summary>
        ''' BusinessExceptionのFieldNameに対応するコントロールにフォーカスをセットする。
        ''' </summary>
        Private Sub FocusOnErrorField(fieldName As String)
            If String.IsNullOrEmpty(fieldName) Then Return

            Select Case fieldName
                Case "Isin"
                    HighlightField(txtIsin)
                    txtIsin.Focus()
                Case "FundNameEn"
                    HighlightField(txtFundNameEn)
                    txtFundNameEn.Focus()
                Case "FundNameJp"
                    HighlightField(txtFundNameJp)
                    txtFundNameJp.Focus()
                Case "CurrencyCode"
                    HighlightField(cmbCurrency)
                    cmbCurrency.Focus()
                Case "DomicileCountryCode", "CountryCode"
                    HighlightField(cmbCountry)
                    cmbCountry.Focus()
                Case "InceptionDate"
                    dtpInceptionDate.Focus()
                Case "SettlementFrequency"
                    HighlightField(cmbSettlementFrequency)
                    cmbSettlementFrequency.Focus()
                Case "RoundingType"
                    HighlightField(cmbRoundingType)
                    cmbRoundingType.Focus()
            End Select
        End Sub

        ''' <summary>
        ''' キャンセルボタンクリック時の処理。変更確認ダイアログを表示し画面を閉じる。
        ''' </summary>
        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Dim result = MessageBox.Show(
                MessageConstants.MsgConfirmDiscard,
                "確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Me.Close()
            End If
        End Sub

    End Class
End Namespace
