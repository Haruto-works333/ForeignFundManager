Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmExchangeRateList

        Private ReadOnly _rateService As New ExchangeRateService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmExchangeRateList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' 通貨コンボ設定
                Dim currencies = _rateService.GetCurrencies()
                cmbCurrency.Items.Clear()
                cmbCurrency.Items.Add("")
                For Each c In currencies
                    cmbCurrency.Items.Add(c.CodeValue)
                Next
                cmbCurrency.SelectedIndex = 0

                ' 日付範囲の初期値：当月1日〜本日
                dtpFrom.Value = New Date(DateTime.Today.Year, DateTime.Today.Month, 1)
                dtpTo.Value = DateTime.Today

                ' DataGridView設定
                dgvRates.AutoGenerateColumns = False

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "為替レート一覧の初期化中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 検索ボタンクリック
        ''' </summary>
        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                Dim currencyCode = If(cmbCurrency.SelectedItem IsNot Nothing, cmbCurrency.SelectedItem.ToString(), "")
                Dim rates = _rateService.Search(currencyCode, dtpFrom.Value.Date, dtpTo.Value.Date)

                dgvRates.Rows.Clear()
                For Each fxRate In rates
                    dgvRates.Rows.Add(
                        fxRate.CurrencyCode,
                        fxRate.RateDate.ToString("yyyy/MM/dd"),
                        fxRate.Ttm.ToString("F6"),
                        fxRate.Tts.ToString("F6"),
                        fxRate.Ttb.ToString("F6")
                    )
                Next

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "為替レート検索中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 新規登録ボタンクリック
        ''' </summary>
        Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            Try
                Dim frm As New FrmExchangeRateEdit()
                If frm.ShowDialog(Me) = DialogResult.OK Then
                    ' 登録後に再検索
                    btnSearch.PerformClick()
                End If
            Catch ex As Exception
                _logger.Fatal(ex, "為替レート登録画面の起動中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class

End Namespace
