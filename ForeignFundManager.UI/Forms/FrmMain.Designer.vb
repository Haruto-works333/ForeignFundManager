Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmMain
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
            Me.mnuMaster = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuMasterFund = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuMasterExchangeRate = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuMasterCalendar = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuData = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuDataImport = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuDataBatch = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuReport = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuReportMonthly = New System.Windows.Forms.ToolStripMenuItem()
            Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
            Me.lblStatusUser = New System.Windows.Forms.ToolStripStatusLabel()
            Me.lblStatusSpring = New System.Windows.Forms.ToolStripStatusLabel()
            Me.lblStatusDate = New System.Windows.Forms.ToolStripStatusLabel()
            Me.MenuStrip1.SuspendLayout()
            Me.StatusStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'MenuStrip1
            '
            Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMaster, Me.mnuData, Me.mnuReport})
            Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
            Me.MenuStrip1.MdiWindowListItem = Nothing
            Me.MenuStrip1.Name = "MenuStrip1"
            Me.MenuStrip1.Size = New System.Drawing.Size(1008, 24)
            Me.MenuStrip1.TabIndex = 0
            Me.MenuStrip1.Text = "MenuStrip1"
            '
            'mnuMaster
            '
            Me.mnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMasterFund, Me.mnuMasterExchangeRate, Me.mnuMasterCalendar})
            Me.mnuMaster.Name = "mnuMaster"
            Me.mnuMaster.Size = New System.Drawing.Size(88, 20)
            Me.mnuMaster.Text = "マスタ管理(&M)"
            '
            'mnuMasterFund
            '
            Me.mnuMasterFund.Name = "mnuMasterFund"
            Me.mnuMasterFund.Size = New System.Drawing.Size(200, 22)
            Me.mnuMasterFund.Text = "ファンドマスタ(&F)"
            '
            'mnuMasterExchangeRate
            '
            Me.mnuMasterExchangeRate.Name = "mnuMasterExchangeRate"
            Me.mnuMasterExchangeRate.Size = New System.Drawing.Size(200, 22)
            Me.mnuMasterExchangeRate.Text = "為替レート(&E)"
            '
            'mnuMasterCalendar
            '
            Me.mnuMasterCalendar.Name = "mnuMasterCalendar"
            Me.mnuMasterCalendar.Size = New System.Drawing.Size(200, 22)
            Me.mnuMasterCalendar.Text = "営業日カレンダー(&C)"
            '
            'mnuData
            '
            Me.mnuData.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDataImport, Me.mnuDataBatch})
            Me.mnuData.Name = "mnuData"
            Me.mnuData.Size = New System.Drawing.Size(88, 20)
            Me.mnuData.Text = "データ管理(&D)"
            '
            'mnuDataImport
            '
            Me.mnuDataImport.Name = "mnuDataImport"
            Me.mnuDataImport.Size = New System.Drawing.Size(180, 22)
            Me.mnuDataImport.Text = "データ取込(&I)"
            '
            'mnuDataBatch
            '
            Me.mnuDataBatch.Name = "mnuDataBatch"
            Me.mnuDataBatch.Size = New System.Drawing.Size(180, 22)
            Me.mnuDataBatch.Text = "バッチ実行(&B)"
            '
            'mnuReport
            '
            Me.mnuReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportMonthly})
            Me.mnuReport.Name = "mnuReport"
            Me.mnuReport.Size = New System.Drawing.Size(62, 20)
            Me.mnuReport.Text = "帳票(&R)"
            '
            'mnuReportMonthly
            '
            Me.mnuReportMonthly.Name = "mnuReportMonthly"
            Me.mnuReportMonthly.Size = New System.Drawing.Size(200, 22)
            Me.mnuReportMonthly.Text = "月次レポート出力(&M)"
            '
            'StatusStrip1
            '
            Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusUser, Me.lblStatusSpring, Me.lblStatusDate})
            Me.StatusStrip1.Location = New System.Drawing.Point(0, 707)
            Me.StatusStrip1.Name = "StatusStrip1"
            Me.StatusStrip1.Size = New System.Drawing.Size(1008, 22)
            Me.StatusStrip1.TabIndex = 1
            Me.StatusStrip1.Text = "StatusStrip1"
            '
            'lblStatusUser
            '
            Me.lblStatusUser.Name = "lblStatusUser"
            Me.lblStatusUser.Size = New System.Drawing.Size(65, 17)
            Me.lblStatusUser.Text = "ユーザー:"
            '
            'lblStatusSpring
            '
            Me.lblStatusSpring.Name = "lblStatusSpring"
            Me.lblStatusSpring.Size = New System.Drawing.Size(862, 17)
            Me.lblStatusSpring.Spring = True
            Me.lblStatusSpring.Text = ""
            '
            'lblStatusDate
            '
            Me.lblStatusDate.Name = "lblStatusDate"
            Me.lblStatusDate.Size = New System.Drawing.Size(65, 17)
            Me.lblStatusDate.Text = ""
            Me.lblStatusDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FrmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1008, 729)
            Me.Controls.Add(Me.StatusStrip1)
            Me.Controls.Add(Me.MenuStrip1)
            Me.IsMdiContainer = True
            Me.MainMenuStrip = Me.MenuStrip1
            Me.Name = "FrmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "外国籍投資信託管理システム"
            Me.MenuStrip1.ResumeLayout(False)
            Me.MenuStrip1.PerformLayout()
            Me.StatusStrip1.ResumeLayout(False)
            Me.StatusStrip1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
        Friend WithEvents mnuMaster As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuMasterFund As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuMasterExchangeRate As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuMasterCalendar As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuData As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuDataImport As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuDataBatch As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuReport As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuReportMonthly As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
        Friend WithEvents lblStatusUser As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents lblStatusSpring As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents lblStatusDate As System.Windows.Forms.ToolStripStatusLabel

    End Class

End Namespace
