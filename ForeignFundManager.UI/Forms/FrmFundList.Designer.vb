Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmFundList
        Inherits System.Windows.Forms.Form

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

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerNonUserCode()>
        Private Sub InitializeComponent()
            Me.grpSearch = New System.Windows.Forms.GroupBox()
            Me.lblIsin = New System.Windows.Forms.Label()
            Me.txtIsin = New System.Windows.Forms.TextBox()
            Me.lblFundName = New System.Windows.Forms.Label()
            Me.txtFundName = New System.Windows.Forms.TextBox()
            Me.lblCurrency = New System.Windows.Forms.Label()
            Me.cmbCurrency = New System.Windows.Forms.ComboBox()
            Me.lblCountry = New System.Windows.Forms.Label()
            Me.cmbCountry = New System.Windows.Forms.ComboBox()
            Me.chkActiveOnly = New System.Windows.Forms.CheckBox()
            Me.btnSearch = New System.Windows.Forms.Button()
            Me.btnClear = New System.Windows.Forms.Button()
            Me.pnlButtons = New System.Windows.Forms.Panel()
            Me.btnNew = New System.Windows.Forms.Button()
            Me.btnDelete = New System.Windows.Forms.Button()
            Me.btnHistory = New System.Windows.Forms.Button()
            Me.btnExportCsv = New System.Windows.Forms.Button()
            Me.dgvFunds = New System.Windows.Forms.DataGridView()
            Me.lblResultCount = New System.Windows.Forms.Label()
            Me.grpSearch.SuspendLayout()
            Me.pnlButtons.SuspendLayout()
            CType(Me.dgvFunds, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            ' grpSearch
            '
            Me.grpSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpSearch.Controls.Add(Me.btnClear)
            Me.grpSearch.Controls.Add(Me.btnSearch)
            Me.grpSearch.Controls.Add(Me.chkActiveOnly)
            Me.grpSearch.Controls.Add(Me.cmbCountry)
            Me.grpSearch.Controls.Add(Me.lblCountry)
            Me.grpSearch.Controls.Add(Me.cmbCurrency)
            Me.grpSearch.Controls.Add(Me.lblCurrency)
            Me.grpSearch.Controls.Add(Me.txtFundName)
            Me.grpSearch.Controls.Add(Me.lblFundName)
            Me.grpSearch.Controls.Add(Me.txtIsin)
            Me.grpSearch.Controls.Add(Me.lblIsin)
            Me.grpSearch.Location = New System.Drawing.Point(12, 12)
            Me.grpSearch.Name = "grpSearch"
            Me.grpSearch.Size = New System.Drawing.Size(860, 120)
            Me.grpSearch.TabIndex = 0
            Me.grpSearch.TabStop = False
            Me.grpSearch.Text = "検索条件"
            '
            ' lblIsin
            '
            Me.lblIsin.AutoSize = True
            Me.lblIsin.Location = New System.Drawing.Point(15, 28)
            Me.lblIsin.Name = "lblIsin"
            Me.lblIsin.Size = New System.Drawing.Size(31, 12)
            Me.lblIsin.TabIndex = 0
            Me.lblIsin.Text = "ISIN"
            '
            ' txtIsin
            '
            Me.txtIsin.Location = New System.Drawing.Point(100, 25)
            Me.txtIsin.MaxLength = 12
            Me.txtIsin.Name = "txtIsin"
            Me.txtIsin.Size = New System.Drawing.Size(150, 19)
            Me.txtIsin.TabIndex = 1
            '
            ' lblFundName
            '
            Me.lblFundName.AutoSize = True
            Me.lblFundName.Location = New System.Drawing.Point(15, 58)
            Me.lblFundName.Name = "lblFundName"
            Me.lblFundName.Size = New System.Drawing.Size(57, 12)
            Me.lblFundName.TabIndex = 2
            Me.lblFundName.Text = "ファンド名"
            '
            ' txtFundName
            '
            Me.txtFundName.Location = New System.Drawing.Point(100, 55)
            Me.txtFundName.MaxLength = 200
            Me.txtFundName.Name = "txtFundName"
            Me.txtFundName.Size = New System.Drawing.Size(250, 19)
            Me.txtFundName.TabIndex = 3
            '
            ' lblCurrency
            '
            Me.lblCurrency.AutoSize = True
            Me.lblCurrency.Location = New System.Drawing.Point(420, 28)
            Me.lblCurrency.Name = "lblCurrency"
            Me.lblCurrency.Size = New System.Drawing.Size(29, 12)
            Me.lblCurrency.TabIndex = 4
            Me.lblCurrency.Text = "通貨"
            '
            ' cmbCurrency
            '
            Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCurrency.FormattingEnabled = True
            Me.cmbCurrency.Location = New System.Drawing.Point(470, 25)
            Me.cmbCurrency.Name = "cmbCurrency"
            Me.cmbCurrency.Size = New System.Drawing.Size(160, 20)
            Me.cmbCurrency.TabIndex = 5
            '
            ' lblCountry
            '
            Me.lblCountry.AutoSize = True
            Me.lblCountry.Location = New System.Drawing.Point(420, 58)
            Me.lblCountry.Name = "lblCountry"
            Me.lblCountry.Size = New System.Drawing.Size(29, 12)
            Me.lblCountry.TabIndex = 6
            Me.lblCountry.Text = "籍国"
            '
            ' cmbCountry
            '
            Me.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCountry.FormattingEnabled = True
            Me.cmbCountry.Location = New System.Drawing.Point(470, 55)
            Me.cmbCountry.Name = "cmbCountry"
            Me.cmbCountry.Size = New System.Drawing.Size(160, 20)
            Me.cmbCountry.TabIndex = 7
            '
            ' chkActiveOnly
            '
            Me.chkActiveOnly.AutoSize = True
            Me.chkActiveOnly.Checked = True
            Me.chkActiveOnly.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkActiveOnly.Location = New System.Drawing.Point(100, 88)
            Me.chkActiveOnly.Name = "chkActiveOnly"
            Me.chkActiveOnly.Size = New System.Drawing.Size(72, 16)
            Me.chkActiveOnly.TabIndex = 8
            Me.chkActiveOnly.Text = "有効のみ"
            Me.chkActiveOnly.UseVisualStyleBackColor = True
            '
            ' btnSearch
            '
            Me.btnSearch.Location = New System.Drawing.Point(670, 83)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(85, 28)
            Me.btnSearch.TabIndex = 9
            Me.btnSearch.Text = "検索"
            Me.btnSearch.UseVisualStyleBackColor = True
            '
            ' btnClear
            '
            Me.btnClear.Location = New System.Drawing.Point(762, 83)
            Me.btnClear.Name = "btnClear"
            Me.btnClear.Size = New System.Drawing.Size(85, 28)
            Me.btnClear.TabIndex = 10
            Me.btnClear.Text = "クリア"
            Me.btnClear.UseVisualStyleBackColor = True
            '
            ' pnlButtons
            '
            Me.pnlButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.pnlButtons.Controls.Add(Me.btnNew)
            Me.pnlButtons.Controls.Add(Me.btnDelete)
            Me.pnlButtons.Controls.Add(Me.btnHistory)
            Me.pnlButtons.Controls.Add(Me.btnExportCsv)
            Me.pnlButtons.Location = New System.Drawing.Point(12, 138)
            Me.pnlButtons.Name = "pnlButtons"
            Me.pnlButtons.Size = New System.Drawing.Size(860, 35)
            Me.pnlButtons.TabIndex = 1
            '
            ' btnNew
            '
            Me.btnNew.Location = New System.Drawing.Point(0, 3)
            Me.btnNew.Name = "btnNew"
            Me.btnNew.Size = New System.Drawing.Size(85, 28)
            Me.btnNew.TabIndex = 0
            Me.btnNew.Text = "新規登録"
            Me.btnNew.UseVisualStyleBackColor = True
            '
            ' btnDelete
            '
            Me.btnDelete.Location = New System.Drawing.Point(92, 3)
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.Size = New System.Drawing.Size(85, 28)
            Me.btnDelete.TabIndex = 1
            Me.btnDelete.Text = "削除"
            Me.btnDelete.UseVisualStyleBackColor = True
            '
            ' btnHistory
            '
            Me.btnHistory.Location = New System.Drawing.Point(184, 3)
            Me.btnHistory.Name = "btnHistory"
            Me.btnHistory.Size = New System.Drawing.Size(85, 28)
            Me.btnHistory.TabIndex = 2
            Me.btnHistory.Text = "変更履歴"
            Me.btnHistory.UseVisualStyleBackColor = True
            '
            ' btnExportCsv
            '
            Me.btnExportCsv.Location = New System.Drawing.Point(276, 3)
            Me.btnExportCsv.Name = "btnExportCsv"
            Me.btnExportCsv.Size = New System.Drawing.Size(100, 28)
            Me.btnExportCsv.TabIndex = 3
            Me.btnExportCsv.Text = "CSV出力"
            Me.btnExportCsv.UseVisualStyleBackColor = True
            '
            ' dgvFunds
            '
            Me.dgvFunds.AllowUserToAddRows = False
            Me.dgvFunds.AllowUserToDeleteRows = False
            Me.dgvFunds.AllowUserToResizeRows = False
            Me.dgvFunds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvFunds.BackgroundColor = System.Drawing.SystemColors.Window
            Me.dgvFunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvFunds.Location = New System.Drawing.Point(12, 179)
            Me.dgvFunds.MultiSelect = False
            Me.dgvFunds.Name = "dgvFunds"
            Me.dgvFunds.ReadOnly = True
            Me.dgvFunds.RowHeadersVisible = False
            Me.dgvFunds.RowTemplate.Height = 21
            Me.dgvFunds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvFunds.Size = New System.Drawing.Size(860, 350)
            Me.dgvFunds.TabIndex = 2
            '
            ' lblResultCount
            '
            Me.lblResultCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblResultCount.AutoSize = True
            Me.lblResultCount.Location = New System.Drawing.Point(12, 538)
            Me.lblResultCount.Name = "lblResultCount"
            Me.lblResultCount.Size = New System.Drawing.Size(0, 12)
            Me.lblResultCount.TabIndex = 3
            '
            ' FrmFundList
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(884, 561)
            Me.Controls.Add(Me.lblResultCount)
            Me.Controls.Add(Me.dgvFunds)
            Me.Controls.Add(Me.pnlButtons)
            Me.Controls.Add(Me.grpSearch)
            Me.Name = "FrmFundList"
            Me.Text = "ファンド一覧"
            Me.grpSearch.ResumeLayout(False)
            Me.grpSearch.PerformLayout()
            Me.pnlButtons.ResumeLayout(False)
            CType(Me.dgvFunds, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

        Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
        Friend WithEvents lblIsin As System.Windows.Forms.Label
        Friend WithEvents txtIsin As System.Windows.Forms.TextBox
        Friend WithEvents lblFundName As System.Windows.Forms.Label
        Friend WithEvents txtFundName As System.Windows.Forms.TextBox
        Friend WithEvents lblCurrency As System.Windows.Forms.Label
        Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
        Friend WithEvents lblCountry As System.Windows.Forms.Label
        Friend WithEvents cmbCountry As System.Windows.Forms.ComboBox
        Friend WithEvents chkActiveOnly As System.Windows.Forms.CheckBox
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Friend WithEvents btnClear As System.Windows.Forms.Button
        Friend WithEvents pnlButtons As System.Windows.Forms.Panel
        Friend WithEvents btnNew As System.Windows.Forms.Button
        Friend WithEvents btnDelete As System.Windows.Forms.Button
        Friend WithEvents btnHistory As System.Windows.Forms.Button
        Friend WithEvents btnExportCsv As System.Windows.Forms.Button
        Friend WithEvents dgvFunds As System.Windows.Forms.DataGridView
        Friend WithEvents lblResultCount As System.Windows.Forms.Label

    End Class
End Namespace
