Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmDataImport

        Private ReadOnly _importService As New DataImportService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmDataImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' 取込種別コンボ設定
                cmbImportType.Items.Clear()
                cmbImportType.Items.Add(AppConstants.ImportTypeNav)
                cmbImportType.Items.Add(AppConstants.ImportTypeRate)
                cmbImportType.Items.Add(AppConstants.ImportTypeCalendar)
                cmbImportType.SelectedIndex = 0

                ' DataGridView設定
                dgvResults.AutoGenerateColumns = False

                ' 結果ラベル初期化
                lblResult.Text = ""

            Catch ex As Exception
                _logger.Fatal(ex, "データ取込画面の初期化中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 参照ボタンクリック：ファイル選択ダイアログを表示する
        ''' </summary>
        Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
            Try
                Using ofd As New OpenFileDialog()
                    ofd.Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"
                    ofd.FilterIndex = 1
                    ofd.Title = "取込ファイルの選択"
                    If ofd.ShowDialog() = DialogResult.OK Then
                        txtFilePath.Text = ofd.FileName
                    End If
                End Using
            Catch ex As Exception
                _logger.Fatal(ex, "ファイル選択中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 実行ボタンクリック：データ取込を実行する
        ''' </summary>
        Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
            Try
                ' 入力チェック
                If cmbImportType.SelectedItem Is Nothing Then
                    MessageBox.Show("取込種別を選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If String.IsNullOrWhiteSpace(txtFilePath.Text) Then
                    MessageBox.Show("ファイルを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' 確認ダイアログ
                Dim importType = cmbImportType.SelectedItem.ToString()
                If MessageBox.Show($"取込種別: {importType}" & Environment.NewLine &
                                   $"ファイル: {txtFilePath.Text}" & Environment.NewLine &
                                   "データ取込を実行してよろしいですか？", "確認",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If

                ' カーソル変更
                Me.Cursor = Cursors.WaitCursor
                btnExecute.Enabled = False

                ' 取込実行
                Dim result = _importService.Import(importType, txtFilePath.Text)

                ' 結果表示
                lblResult.Text = $"ステータス: {result.ImportStatus}  " &
                                 $"合計: {result.TotalCount}件  " &
                                 $"成功: {result.SuccessCount}件  " &
                                 $"エラー: {result.ErrorCount}件"

                dgvResults.Rows.Clear()
                dgvResults.Rows.Add("取込種別", importType)
                dgvResults.Rows.Add("ファイル名", result.FileName)
                dgvResults.Rows.Add("ステータス", result.ImportStatus)
                dgvResults.Rows.Add("合計件数", result.TotalCount.ToString())
                dgvResults.Rows.Add("成功件数", result.SuccessCount.ToString())
                dgvResults.Rows.Add("エラー件数", result.ErrorCount.ToString())

                If result.ImportStatus = AppConstants.ImportStatusSuccess Then
                    MessageBox.Show("データ取込が完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As BusinessException
                lblResult.Text = $"エラー: {ex.Message}"
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "データ取込中にエラーが発生しました。")
                lblResult.Text = "システムエラーが発生しました。"
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Cursor = Cursors.Default
                btnExecute.Enabled = True
            End Try
        End Sub

    End Class

End Namespace
