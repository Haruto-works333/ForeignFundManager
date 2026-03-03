Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmBatchExecution

        Private ReadOnly _navCalcService As New NavCalculationService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmBatchExecution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' 対象日付を本日に設定
                dtpTargetDate.Value = DateTime.Today

                ' DataGridView設定
                dgvResults.AutoGenerateColumns = False

                ' サマリラベル初期化
                lblTotal.Text = "合計: -"
                lblSuccess.Text = "成功: -"
                lblSkip.Text = "スキップ: -"
                lblError.Text = "エラー: -"
                lblElapsed.Text = "経過時間: -"

            Catch ex As Exception
                _logger.Fatal(ex, "バッチ実行画面の初期化中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 実行ボタンクリック：NAV計算バッチを実行する
        ''' </summary>
        Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
            Try
                Dim targetDate = dtpTargetDate.Value.Date

                ' 確認ダイアログ
                Dim confirmMsg = String.Format(MessageConstants.MsgConfirmBatchExecute, targetDate.ToString("yyyy/MM/dd"))
                If MessageBox.Show(confirmMsg, "確認",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If

                ' カーソル変更
                Me.Cursor = Cursors.WaitCursor
                btnExecute.Enabled = False

                ' バッチ実行
                Dim result = _navCalcService.Execute(targetDate)

                ' サマリ表示
                lblTotal.Text = $"合計: {result.TotalCount}件"
                lblSuccess.Text = $"成功: {result.SuccessCount}件"
                lblSkip.Text = $"スキップ: {result.SkippedCount}件"
                lblError.Text = $"エラー: {result.ErrorCount}件"
                lblElapsed.Text = $"経過時間: {result.Elapsed.TotalSeconds:F1}秒"

                ' 明細表示
                dgvResults.Rows.Clear()
                For Each detail In result.Details
                    dgvResults.Rows.Add(
                        detail.Isin,
                        detail.CurrencyCode,
                        detail.NavPerUnit.ToString("F6"),
                        If(detail.AppliedRate.HasValue, detail.AppliedRate.Value.ToString("F6"), ""),
                        If(detail.NavJpy.HasValue, detail.NavJpy.Value.ToString("F0"), ""),
                        detail.Status,
                        detail.Note
                    )
                Next

                If result.TotalCount = 0 Then
                    MessageBox.Show(MessageConstants.MsgBatchNoRecords, "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"基準価額計算が完了しました。" & Environment.NewLine &
                                   $"成功: {result.SuccessCount}件, スキップ: {result.SkippedCount}件, エラー: {result.ErrorCount}件",
                                   "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "バッチ実行中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Cursor = Cursors.Default
                btnExecute.Enabled = True
            End Try
        End Sub

    End Class

End Namespace
