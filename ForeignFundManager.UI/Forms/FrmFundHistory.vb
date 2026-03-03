Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmFundHistory

        Private ReadOnly _fundService As New FundService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' 表示対象のISINコード
        ''' </summary>
        Public Property Isin As String

        ''' <summary>
        ''' フォームロード時に変更履歴を取得して表示する
        ''' </summary>
        Private Sub FrmFundHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                Me.Text = $"変更履歴 - {Isin}"
                LoadHistory()
            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "変更履歴の読み込み中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 変更履歴を取得してDataGridViewに表示する
        ''' </summary>
        Private Sub LoadHistory()
            Dim logs = _fundService.GetChangeHistory(Isin)

            dgvHistory.AutoGenerateColumns = False
            dgvHistory.DataSource = Nothing
            dgvHistory.Rows.Clear()

            For Each log In logs
                dgvHistory.Rows.Add(
                    log.OperatedAt.ToString("yyyy/MM/dd HH:mm:ss"),
                    log.OperationType,
                    log.ColumnName,
                    log.OldValue,
                    log.NewValue,
                    log.OperatedBy
                )
            Next
        End Sub

    End Class

End Namespace
