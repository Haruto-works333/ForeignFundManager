Imports ForeignFundManager.Core.Utils

Namespace Forms

    Public Class FrmMain

        Private ReadOnly _logger As NLog.Logger = NLog.LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' ステータスバーにユーザー名と現在日付を表示
                lblStatusUser.Text = "ユーザー: " & AppConfig.UserName
                lblStatusDate.Text = DateTime.Today.ToString("yyyy/MM/dd")
            Catch ex As Exception
                _logger.Fatal(ex, "メイン画面の初期化中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' MDI子フォームを開く共通メソッド。
        ''' 既に同じ型のフォームが開いている場合はアクティブ化し、重複起動を防止する。
        ''' </summary>
        ''' <typeparam name="T">開くフォームの型</typeparam>
        Private Sub ShowMdiChildForm(Of T As {Form, New})()
            Try
                ' 既に開いているか確認
                For Each childForm As Form In Me.MdiChildren
                    If TypeOf childForm Is T Then
                        childForm.Activate()
                        If childForm.WindowState = FormWindowState.Minimized Then
                            childForm.WindowState = FormWindowState.Normal
                        End If
                        Return
                    End If
                Next

                ' 新規にフォームを生成して表示
                Dim frm As New T()
                frm.MdiParent = Me
                frm.Show()

            Catch ex As Exception
                _logger.Fatal(ex, "画面の起動中にエラーが発生しました。画面型: {0}", GetType(T).Name)
                MessageBox.Show("画面の起動中にエラーが発生しました。" & Environment.NewLine & ex.Message,
                                "エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            End Try
        End Sub

#Region "メニューイベントハンドラ"

        ''' <summary>
        ''' マスタ管理 > ファンドマスタ
        ''' </summary>
        Private Sub mnuMasterFund_Click(sender As Object, e As EventArgs) Handles mnuMasterFund.Click
            ShowMdiChildForm(Of FrmFundList)()
        End Sub

        ''' <summary>
        ''' マスタ管理 > 為替レート
        ''' </summary>
        Private Sub mnuMasterExchangeRate_Click(sender As Object, e As EventArgs) Handles mnuMasterExchangeRate.Click
            ShowMdiChildForm(Of FrmExchangeRateList)()
        End Sub

        ''' <summary>
        ''' マスタ管理 > 営業日カレンダー
        ''' </summary>
        Private Sub mnuMasterCalendar_Click(sender As Object, e As EventArgs) Handles mnuMasterCalendar.Click
            ShowMdiChildForm(Of FrmCalendar)()
        End Sub

        ''' <summary>
        ''' データ管理 > データ取込
        ''' </summary>
        Private Sub mnuDataImport_Click(sender As Object, e As EventArgs) Handles mnuDataImport.Click
            ShowMdiChildForm(Of FrmDataImport)()
        End Sub

        ''' <summary>
        ''' データ管理 > バッチ実行
        ''' </summary>
        Private Sub mnuDataBatch_Click(sender As Object, e As EventArgs) Handles mnuDataBatch.Click
            ShowMdiChildForm(Of FrmBatchExecution)()
        End Sub

        ''' <summary>
        ''' 帳票 > 月次レポート出力
        ''' </summary>
        Private Sub mnuReportMonthly_Click(sender As Object, e As EventArgs) Handles mnuReportMonthly.Click
            ShowMdiChildForm(Of FrmReportOutput)()
        End Sub

#End Region

    End Class

End Namespace
