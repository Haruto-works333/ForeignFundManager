Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmExchangeRateEdit

        Private ReadOnly _rateService As New ExchangeRateService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmExchangeRateEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' 通貨コンボ設定
                Dim currencies = _rateService.GetCurrencies()
                cmbCurrency.Items.Clear()
                For Each c In currencies
                    cmbCurrency.Items.Add(c.CodeValue)
                Next
                If cmbCurrency.Items.Count > 0 Then cmbCurrency.SelectedIndex = 0

                ' 基準日を本日に設定
                dtpRateDate.Value = DateTime.Today

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "為替レート登録画面の初期化中にエラーが発生しました。")
                MessageBox.Show(MessageConstants.MsgSystemError,
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 保存ボタンクリック
        ''' </summary>
        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Try
                ' 入力チェック
                If cmbCurrency.SelectedItem Is Nothing Then
                    MessageBox.Show("通貨を選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbCurrency.Focus()
                    Return
                End If

                Dim ttm As Decimal
                If Not Decimal.TryParse(txtTtm.Text, ttm) Then
                    MessageBox.Show("TTMを正しい数値で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtTtm.Focus()
                    Return
                End If

                Dim tts As Decimal
                If Not Decimal.TryParse(txtTts.Text, tts) Then
                    MessageBox.Show("TTSを正しい数値で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtTts.Focus()
                    Return
                End If

                Dim ttb As Decimal
                If Not Decimal.TryParse(txtTtb.Text, ttb) Then
                    MessageBox.Show("TTBを正しい数値で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtTtb.Focus()
                    Return
                End If

                ' 確認ダイアログ
                If MessageBox.Show(MessageConstants.MsgConfirmSave, "確認",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If

                ' モデル構築
                Dim fxRate As New ExchangeRate()
                fxRate.CurrencyCode = cmbCurrency.SelectedItem.ToString()
                fxRate.RateDate = dtpRateDate.Value.Date
                fxRate.Ttm = ttm
                fxRate.Tts = tts
                fxRate.Ttb = ttb

                ' 保存実行
                _rateService.Save(fxRate)

                MessageBox.Show(MessageConstants.MsgSaveSuccess, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "為替レートの保存中にエラーが発生しました。")
                MessageBox.Show(MessageConstants.MsgSystemError,
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class

End Namespace
