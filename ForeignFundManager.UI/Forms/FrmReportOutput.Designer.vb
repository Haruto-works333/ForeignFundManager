Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmReportOutput
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
            Me.lblYear = New System.Windows.Forms.Label()
            Me.nudYear = New System.Windows.Forms.NumericUpDown()
            Me.lblMonth = New System.Windows.Forms.Label()
            Me.cmbMonth = New System.Windows.Forms.ComboBox()
            Me.lblFunds = New System.Windows.Forms.Label()
            Me.clbFunds = New System.Windows.Forms.CheckedListBox()
            Me.lblOutputFolder = New System.Windows.Forms.Label()
            Me.txtOutputFolder = New System.Windows.Forms.TextBox()
            Me.btnBrowseFolder = New System.Windows.Forms.Button()
            Me.btnExecute = New System.Windows.Forms.Button()
            CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblYear
            '
            Me.lblYear.AutoSize = True
            Me.lblYear.Location = New System.Drawing.Point(12, 18)
            Me.lblYear.Name = "lblYear"
            Me.lblYear.Size = New System.Drawing.Size(17, 12)
            Me.lblYear.TabIndex = 0
            Me.lblYear.Text = "年"
            '
            'nudYear
            '
            Me.nudYear.Location = New System.Drawing.Point(35, 15)
            Me.nudYear.Maximum = New Decimal(New Integer() {2099, 0, 0, 0})
            Me.nudYear.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
            Me.nudYear.Name = "nudYear"
            Me.nudYear.Size = New System.Drawing.Size(70, 19)
            Me.nudYear.TabIndex = 1
            Me.nudYear.Value = New Decimal(New Integer() {2026, 0, 0, 0})
            '
            'lblMonth
            '
            Me.lblMonth.AutoSize = True
            Me.lblMonth.Location = New System.Drawing.Point(120, 18)
            Me.lblMonth.Name = "lblMonth"
            Me.lblMonth.Size = New System.Drawing.Size(17, 12)
            Me.lblMonth.TabIndex = 2
            Me.lblMonth.Text = "月"
            '
            'cmbMonth
            '
            Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbMonth.Location = New System.Drawing.Point(143, 15)
            Me.cmbMonth.Name = "cmbMonth"
            Me.cmbMonth.Size = New System.Drawing.Size(50, 20)
            Me.cmbMonth.TabIndex = 3
            '
            'lblFunds
            '
            Me.lblFunds.AutoSize = True
            Me.lblFunds.Location = New System.Drawing.Point(12, 50)
            Me.lblFunds.Name = "lblFunds"
            Me.lblFunds.Size = New System.Drawing.Size(65, 12)
            Me.lblFunds.TabIndex = 4
            Me.lblFunds.Text = "対象ファンド"
            '
            'clbFunds
            '
            Me.clbFunds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.clbFunds.CheckOnClick = True
            Me.clbFunds.FormattingEnabled = True
            Me.clbFunds.Location = New System.Drawing.Point(12, 68)
            Me.clbFunds.Name = "clbFunds"
            Me.clbFunds.Size = New System.Drawing.Size(560, 284)
            Me.clbFunds.TabIndex = 5
            '
            'lblOutputFolder
            '
            Me.lblOutputFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblOutputFolder.AutoSize = True
            Me.lblOutputFolder.Location = New System.Drawing.Point(12, 370)
            Me.lblOutputFolder.Name = "lblOutputFolder"
            Me.lblOutputFolder.Size = New System.Drawing.Size(77, 12)
            Me.lblOutputFolder.TabIndex = 6
            Me.lblOutputFolder.Text = "出力先フォルダ"
            '
            'txtOutputFolder
            '
            Me.txtOutputFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtOutputFolder.Location = New System.Drawing.Point(95, 367)
            Me.txtOutputFolder.Name = "txtOutputFolder"
            Me.txtOutputFolder.ReadOnly = True
            Me.txtOutputFolder.Size = New System.Drawing.Size(390, 19)
            Me.txtOutputFolder.TabIndex = 7
            '
            'btnBrowseFolder
            '
            Me.btnBrowseFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBrowseFolder.Location = New System.Drawing.Point(497, 365)
            Me.btnBrowseFolder.Name = "btnBrowseFolder"
            Me.btnBrowseFolder.Size = New System.Drawing.Size(75, 23)
            Me.btnBrowseFolder.TabIndex = 8
            Me.btnBrowseFolder.Text = "参照..."
            Me.btnBrowseFolder.UseVisualStyleBackColor = True
            '
            'btnExecute
            '
            Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnExecute.Location = New System.Drawing.Point(230, 410)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(120, 35)
            Me.btnExecute.TabIndex = 9
            Me.btnExecute.Text = "出力"
            Me.btnExecute.UseVisualStyleBackColor = True
            '
            'FrmReportOutput
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(584, 461)
            Me.Controls.Add(Me.btnExecute)
            Me.Controls.Add(Me.btnBrowseFolder)
            Me.Controls.Add(Me.txtOutputFolder)
            Me.Controls.Add(Me.lblOutputFolder)
            Me.Controls.Add(Me.clbFunds)
            Me.Controls.Add(Me.lblFunds)
            Me.Controls.Add(Me.cmbMonth)
            Me.Controls.Add(Me.lblMonth)
            Me.Controls.Add(Me.nudYear)
            Me.Controls.Add(Me.lblYear)
            Me.Name = "FrmReportOutput"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "月次レポート出力"
            CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblYear As System.Windows.Forms.Label
        Friend WithEvents nudYear As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblMonth As System.Windows.Forms.Label
        Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
        Friend WithEvents lblFunds As System.Windows.Forms.Label
        Friend WithEvents clbFunds As System.Windows.Forms.CheckedListBox
        Friend WithEvents lblOutputFolder As System.Windows.Forms.Label
        Friend WithEvents txtOutputFolder As System.Windows.Forms.TextBox
        Friend WithEvents btnBrowseFolder As System.Windows.Forms.Button
        Friend WithEvents btnExecute As System.Windows.Forms.Button

    End Class
End Namespace
