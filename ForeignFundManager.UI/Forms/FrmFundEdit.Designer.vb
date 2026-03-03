Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmFundEdit
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
            Me.lblIsin = New System.Windows.Forms.Label()
            Me.txtIsin = New System.Windows.Forms.TextBox()
            Me.lblFundNameEn = New System.Windows.Forms.Label()
            Me.txtFundNameEn = New System.Windows.Forms.TextBox()
            Me.lblFundNameJp = New System.Windows.Forms.Label()
            Me.txtFundNameJp = New System.Windows.Forms.TextBox()
            Me.lblCurrency = New System.Windows.Forms.Label()
            Me.cmbCurrency = New System.Windows.Forms.ComboBox()
            Me.lblCountry = New System.Windows.Forms.Label()
            Me.cmbCountry = New System.Windows.Forms.ComboBox()
            Me.lblInceptionDate = New System.Windows.Forms.Label()
            Me.dtpInceptionDate = New System.Windows.Forms.DateTimePicker()
            Me.lblSettlementFrequency = New System.Windows.Forms.Label()
            Me.cmbSettlementFrequency = New System.Windows.Forms.ComboBox()
            Me.lblRoundingType = New System.Windows.Forms.Label()
            Me.cmbRoundingType = New System.Windows.Forms.ComboBox()
            Me.lblRemarks = New System.Windows.Forms.Label()
            Me.txtRemarks = New System.Windows.Forms.TextBox()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.lblRequired = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            ' lblIsin
            '
            Me.lblIsin.AutoSize = True
            Me.lblIsin.Location = New System.Drawing.Point(30, 25)
            Me.lblIsin.Name = "lblIsin"
            Me.lblIsin.Size = New System.Drawing.Size(43, 12)
            Me.lblIsin.TabIndex = 0
            Me.lblIsin.Text = "ISIN *"
            '
            ' txtIsin
            '
            Me.txtIsin.Location = New System.Drawing.Point(170, 22)
            Me.txtIsin.MaxLength = 12
            Me.txtIsin.Name = "txtIsin"
            Me.txtIsin.Size = New System.Drawing.Size(150, 19)
            Me.txtIsin.TabIndex = 1
            '
            ' lblFundNameEn
            '
            Me.lblFundNameEn.AutoSize = True
            Me.lblFundNameEn.Location = New System.Drawing.Point(30, 60)
            Me.lblFundNameEn.Name = "lblFundNameEn"
            Me.lblFundNameEn.Size = New System.Drawing.Size(119, 12)
            Me.lblFundNameEn.TabIndex = 2
            Me.lblFundNameEn.Text = "ファンド名称（英語） *"
            '
            ' txtFundNameEn
            '
            Me.txtFundNameEn.Location = New System.Drawing.Point(170, 57)
            Me.txtFundNameEn.MaxLength = 200
            Me.txtFundNameEn.Name = "txtFundNameEn"
            Me.txtFundNameEn.Size = New System.Drawing.Size(380, 19)
            Me.txtFundNameEn.TabIndex = 3
            '
            ' lblFundNameJp
            '
            Me.lblFundNameJp.AutoSize = True
            Me.lblFundNameJp.Location = New System.Drawing.Point(30, 95)
            Me.lblFundNameJp.Name = "lblFundNameJp"
            Me.lblFundNameJp.Size = New System.Drawing.Size(125, 12)
            Me.lblFundNameJp.TabIndex = 4
            Me.lblFundNameJp.Text = "ファンド名称（日本語） *"
            '
            ' txtFundNameJp
            '
            Me.txtFundNameJp.Location = New System.Drawing.Point(170, 92)
            Me.txtFundNameJp.MaxLength = 200
            Me.txtFundNameJp.Name = "txtFundNameJp"
            Me.txtFundNameJp.Size = New System.Drawing.Size(380, 19)
            Me.txtFundNameJp.TabIndex = 5
            '
            ' lblCurrency
            '
            Me.lblCurrency.AutoSize = True
            Me.lblCurrency.Location = New System.Drawing.Point(30, 130)
            Me.lblCurrency.Name = "lblCurrency"
            Me.lblCurrency.Size = New System.Drawing.Size(71, 12)
            Me.lblCurrency.TabIndex = 6
            Me.lblCurrency.Text = "通貨コード *"
            '
            ' cmbCurrency
            '
            Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCurrency.FormattingEnabled = True
            Me.cmbCurrency.Location = New System.Drawing.Point(170, 127)
            Me.cmbCurrency.Name = "cmbCurrency"
            Me.cmbCurrency.Size = New System.Drawing.Size(220, 20)
            Me.cmbCurrency.TabIndex = 7
            '
            ' lblCountry
            '
            Me.lblCountry.AutoSize = True
            Me.lblCountry.Location = New System.Drawing.Point(30, 165)
            Me.lblCountry.Name = "lblCountry"
            Me.lblCountry.Size = New System.Drawing.Size(41, 12)
            Me.lblCountry.TabIndex = 8
            Me.lblCountry.Text = "籍国 *"
            '
            ' cmbCountry
            '
            Me.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCountry.FormattingEnabled = True
            Me.cmbCountry.Location = New System.Drawing.Point(170, 162)
            Me.cmbCountry.Name = "cmbCountry"
            Me.cmbCountry.Size = New System.Drawing.Size(220, 20)
            Me.cmbCountry.TabIndex = 9
            '
            ' lblInceptionDate
            '
            Me.lblInceptionDate.AutoSize = True
            Me.lblInceptionDate.Location = New System.Drawing.Point(30, 200)
            Me.lblInceptionDate.Name = "lblInceptionDate"
            Me.lblInceptionDate.Size = New System.Drawing.Size(47, 12)
            Me.lblInceptionDate.TabIndex = 10
            Me.lblInceptionDate.Text = "設定日 *"
            '
            ' dtpInceptionDate
            '
            Me.dtpInceptionDate.CustomFormat = "yyyy/MM/dd"
            Me.dtpInceptionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dtpInceptionDate.Location = New System.Drawing.Point(170, 197)
            Me.dtpInceptionDate.Name = "dtpInceptionDate"
            Me.dtpInceptionDate.Size = New System.Drawing.Size(150, 19)
            Me.dtpInceptionDate.TabIndex = 11
            '
            ' lblSettlementFrequency
            '
            Me.lblSettlementFrequency.AutoSize = True
            Me.lblSettlementFrequency.Location = New System.Drawing.Point(30, 235)
            Me.lblSettlementFrequency.Name = "lblSettlementFrequency"
            Me.lblSettlementFrequency.Size = New System.Drawing.Size(59, 12)
            Me.lblSettlementFrequency.TabIndex = 12
            Me.lblSettlementFrequency.Text = "決算頻度 *"
            '
            ' cmbSettlementFrequency
            '
            Me.cmbSettlementFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSettlementFrequency.FormattingEnabled = True
            Me.cmbSettlementFrequency.Location = New System.Drawing.Point(170, 232)
            Me.cmbSettlementFrequency.Name = "cmbSettlementFrequency"
            Me.cmbSettlementFrequency.Size = New System.Drawing.Size(220, 20)
            Me.cmbSettlementFrequency.TabIndex = 13
            '
            ' lblRoundingType
            '
            Me.lblRoundingType.AutoSize = True
            Me.lblRoundingType.Location = New System.Drawing.Point(30, 270)
            Me.lblRoundingType.Name = "lblRoundingType"
            Me.lblRoundingType.Size = New System.Drawing.Size(59, 12)
            Me.lblRoundingType.TabIndex = 14
            Me.lblRoundingType.Text = "端数処理 *"
            '
            ' cmbRoundingType
            '
            Me.cmbRoundingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbRoundingType.FormattingEnabled = True
            Me.cmbRoundingType.Location = New System.Drawing.Point(170, 267)
            Me.cmbRoundingType.Name = "cmbRoundingType"
            Me.cmbRoundingType.Size = New System.Drawing.Size(220, 20)
            Me.cmbRoundingType.TabIndex = 15
            '
            ' lblRemarks
            '
            Me.lblRemarks.AutoSize = True
            Me.lblRemarks.Location = New System.Drawing.Point(30, 305)
            Me.lblRemarks.Name = "lblRemarks"
            Me.lblRemarks.Size = New System.Drawing.Size(29, 12)
            Me.lblRemarks.TabIndex = 16
            Me.lblRemarks.Text = "備考"
            '
            ' txtRemarks
            '
            Me.txtRemarks.Location = New System.Drawing.Point(170, 302)
            Me.txtRemarks.MaxLength = 500
            Me.txtRemarks.Multiline = True
            Me.txtRemarks.Name = "txtRemarks"
            Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtRemarks.Size = New System.Drawing.Size(380, 80)
            Me.txtRemarks.TabIndex = 17
            '
            ' btnSave
            '
            Me.btnSave.Location = New System.Drawing.Point(350, 400)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(95, 30)
            Me.btnSave.TabIndex = 18
            Me.btnSave.Text = "保存"
            Me.btnSave.UseVisualStyleBackColor = True
            '
            ' btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(455, 400)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(95, 30)
            Me.btnCancel.TabIndex = 19
            Me.btnCancel.Text = "キャンセル"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            ' lblRequired
            '
            Me.lblRequired.AutoSize = True
            Me.lblRequired.ForeColor = System.Drawing.Color.Gray
            Me.lblRequired.Location = New System.Drawing.Point(30, 445)
            Me.lblRequired.Name = "lblRequired"
            Me.lblRequired.Size = New System.Drawing.Size(95, 12)
            Me.lblRequired.TabIndex = 20
            Me.lblRequired.Text = "* は必須入力です"
            '
            ' FrmFundEdit
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(584, 511)
            Me.Controls.Add(Me.lblRequired)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.txtRemarks)
            Me.Controls.Add(Me.lblRemarks)
            Me.Controls.Add(Me.cmbRoundingType)
            Me.Controls.Add(Me.lblRoundingType)
            Me.Controls.Add(Me.cmbSettlementFrequency)
            Me.Controls.Add(Me.lblSettlementFrequency)
            Me.Controls.Add(Me.dtpInceptionDate)
            Me.Controls.Add(Me.lblInceptionDate)
            Me.Controls.Add(Me.cmbCountry)
            Me.Controls.Add(Me.lblCountry)
            Me.Controls.Add(Me.cmbCurrency)
            Me.Controls.Add(Me.lblCurrency)
            Me.Controls.Add(Me.txtFundNameJp)
            Me.Controls.Add(Me.lblFundNameJp)
            Me.Controls.Add(Me.txtFundNameEn)
            Me.Controls.Add(Me.lblFundNameEn)
            Me.Controls.Add(Me.txtIsin)
            Me.Controls.Add(Me.lblIsin)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "FrmFundEdit"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "ファンド登録・編集"
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

        Friend WithEvents lblIsin As System.Windows.Forms.Label
        Friend WithEvents txtIsin As System.Windows.Forms.TextBox
        Friend WithEvents lblFundNameEn As System.Windows.Forms.Label
        Friend WithEvents txtFundNameEn As System.Windows.Forms.TextBox
        Friend WithEvents lblFundNameJp As System.Windows.Forms.Label
        Friend WithEvents txtFundNameJp As System.Windows.Forms.TextBox
        Friend WithEvents lblCurrency As System.Windows.Forms.Label
        Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
        Friend WithEvents lblCountry As System.Windows.Forms.Label
        Friend WithEvents cmbCountry As System.Windows.Forms.ComboBox
        Friend WithEvents lblInceptionDate As System.Windows.Forms.Label
        Friend WithEvents dtpInceptionDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblSettlementFrequency As System.Windows.Forms.Label
        Friend WithEvents cmbSettlementFrequency As System.Windows.Forms.ComboBox
        Friend WithEvents lblRoundingType As System.Windows.Forms.Label
        Friend WithEvents cmbRoundingType As System.Windows.Forms.ComboBox
        Friend WithEvents lblRemarks As System.Windows.Forms.Label
        Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents lblRequired As System.Windows.Forms.Label

    End Class
End Namespace
