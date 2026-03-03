Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmReportOutput

        Private ReadOnly _fundService As New FundService()
        Private ReadOnly _reportService As New ReportService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmReportOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' 年の初期値
                nudYear.Minimum = 2000
                nudYear.Maximum = 2099
                nudYear.Value = DateTime.Today.Year

                ' 月コンボ設定
                cmbMonth.Items.Clear()
                For i = 1 To 12
                    cmbMonth.Items.Add(i.ToString())
                Next
                cmbMonth.SelectedIndex = DateTime.Today.Month - 1

                ' ファンド一覧を取得してCheckedListBoxに設定
                Dim activeFunds = _fundService.GetActiveFunds()
                clbFunds.Items.Clear()
                For Each f In activeFunds
                    clbFunds.Items.Add($"{f.Isin} - {f.FundNameJp}")
                Next

                ' 出力先フォルダの初期値
                txtOutputFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "レポート出力画面の初期化中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' フォルダ参照ボタンクリック
        ''' </summary>
        Private Sub btnBrowseFolder_Click(sender As Object, e As EventArgs) Handles btnBrowseFolder.Click
            Try
                Using fbd As New FolderBrowserDialog()
                    fbd.Description = "出力先フォルダの選択"
                    fbd.SelectedPath = txtOutputFolder.Text
                    If fbd.ShowDialog() = DialogResult.OK Then
                        txtOutputFolder.Text = fbd.SelectedPath
                    End If
                End Using
            Catch ex As Exception
                _logger.Fatal(ex, "フォルダ選択中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 出力ボタンクリック：月次レポートを生成する
        ''' </summary>
        Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
            Try
                ' ファンド選択チェック
                If clbFunds.CheckedItems.Count = 0 Then
                    MessageBox.Show(MessageConstants.MsgReportNoFundSelected, "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' 出力フォルダチェック
                If String.IsNullOrWhiteSpace(txtOutputFolder.Text) Then
                    MessageBox.Show("出力先フォルダを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If Not System.IO.Directory.Exists(txtOutputFolder.Text) Then
                    MessageBox.Show("指定された出力先フォルダが存在しません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' 選択されたファンドのISINリストを取得
                Dim isinList As New List(Of String)
                For Each item In clbFunds.CheckedItems
                    Dim displayText = item.ToString()
                    ' "LU1234567890 - ファンド名" 形式からISINを取得
                    Dim isin = displayText.Split(New String() {" - "}, StringSplitOptions.None)(0).Trim()
                    isinList.Add(isin)
                Next

                Dim year = CInt(nudYear.Value)
                Dim month = CInt(cmbMonth.SelectedItem.ToString())

                ' カーソル変更
                Me.Cursor = Cursors.WaitCursor
                btnExecute.Enabled = False

                ' レポート生成実行
                _reportService.GenerateMonthlyReport(year, month, isinList, txtOutputFolder.Text)

                MessageBox.Show(String.Format(MessageConstants.MsgReportOutputSuccess, isinList.Count),
                                "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "レポート出力中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Cursor = Cursors.Default
                btnExecute.Enabled = True
            End Try
        End Sub

    End Class

End Namespace
