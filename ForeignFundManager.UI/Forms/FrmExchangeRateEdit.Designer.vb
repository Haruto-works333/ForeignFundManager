Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmExchangeRateEdit
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
            Me.lblCurrency = New System.Windows.Forms.Label()
            Me.cmbCurrency = New System.Windows.Forms.ComboBox()
            Me.lblRateDate = New System.Windows.Forms.Label()
            Me.dtpRateDate = New System.Windows.Forms.DateTimePicker()
            Me.lblTtm = New System.Windows.Forms.Label()
            Me.txtTtm = New System.Windows.Forms.TextBox()
            Me.lblTts = New System.Windows.Forms.Label()
            Me.txtTts = New System.Windows.Forms.TextBox()
            Me.lblTtb = New System.Windows.Forms.Label()
            Me.txtTtb = New System.Windows.Forms.TextBox()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'lblCurrency
            '
            Me.lblCurrency.AutoSize = True
            Me.lblCurrency.Location = New System.Drawing.Point(30, 30)
            Me.lblCurrency.Name = "lblCurrency"
            Me.lblCurrency.Size = New System.Drawing.Size(29, 12)
            Me.lblCurrency.TabIndex = 0
            Me.lblCurrency.Text = "通貨"
            '
            'cmbCurrency
            '
            Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCurrency.Location = New System.Drawing.Point(130, 27)
            Me.cmbCurrency.Name = "cmbCurrency"
            Me.cmbCurrency.Size = New System.Drawing.Size(120, 20)
            Me.cmbCurrency.TabIndex = 1
            '
            'lblRateDate
            '
            Me.lblRateDate.AutoSize = True
            Me.lblRateDate.Location = New System.Drawing.Point(30, 70)
            Me.lblRateDate.Name = "lblRateDate"
            Me.lblRateDate.Size = New System.Drawing.Size(41, 12)
            Me.lblRateDate.TabIndex = 2
            Me.lblRateDate.Text = "基準日"
            '
            'dtpRateDate
            '
            Me.dtpRateDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpRateDate.Location = New System.Drawing.Point(130, 67)
            Me.dtpRateDate.Name = "dtpRateDate"
            Me.dtpRateDate.Size = New System.Drawing.Size(150, 19)
            Me.dtpRateDate.TabIndex = 3
            '
            'lblTtm
            '
            Me.lblTtm.AutoSize = True
            Me.lblTtm.Location = New System.Drawing.Point(30, 110)
            Me.lblTtm.Name = "lblTtm"
            Me.lblTtm.Size = New System.Drawing.Size(29, 12)
            Me.lblTtm.TabIndex = 4
            Me.lblTtm.Text = "TTM"
            '
            'txtTtm
            '
            Me.txtTtm.Location = New System.Drawing.Point(130, 107)
            Me.txtTtm.Name = "txtTtm"
            Me.txtTtm.Size = New System.Drawing.Size(150, 19)
            Me.txtTtm.TabIndex = 5
            Me.txtTtm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'lblTts
            '
            Me.lblTts.AutoSize = True
            Me.lblTts.Location = New System.Drawing.Point(30, 150)
            Me.lblTts.Name = "lblTts"
            Me.lblTts.Size = New System.Drawing.Size(26, 12)
            Me.lblTts.TabIndex = 6
            Me.lblTts.Text = "TTS"
            '
            'txtTts
            '
            Me.txtTts.Location = New System.Drawing.Point(130, 147)
            Me.txtTts.Name = "txtTts"
            Me.txtTts.Size = New System.Drawing.Size(150, 19)
            Me.txtTts.TabIndex = 7
            Me.txtTts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'lblTtb
            '
            Me.lblTtb.AutoSize = True
            Me.lblTtb.Location = New System.Drawing.Point(30, 190)
            Me.lblTtb.Name = "lblTtb"
            Me.lblTtb.Size = New System.Drawing.Size(26, 12)
            Me.lblTtb.TabIndex = 8
            Me.lblTtb.Text = "TTB"
            '
            'txtTtb
            '
            Me.txtTtb.Location = New System.Drawing.Point(130, 187)
            Me.txtTtb.Name = "txtTtb"
            Me.txtTtb.Size = New System.Drawing.Size(150, 19)
            Me.txtTtb.TabIndex = 9
            Me.txtTtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'btnSave
            '
            Me.btnSave.Location = New System.Drawing.Point(175, 250)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(100, 30)
            Me.btnSave.TabIndex = 10
            Me.btnSave.Text = "保存"
            Me.btnSave.UseVisualStyleBackColor = True
            '
            'FrmExchangeRateEdit
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(434, 311)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.txtTtb)
            Me.Controls.Add(Me.lblTtb)
            Me.Controls.Add(Me.txtTts)
            Me.Controls.Add(Me.lblTts)
            Me.Controls.Add(Me.txtTtm)
            Me.Controls.Add(Me.lblTtm)
            Me.Controls.Add(Me.dtpRateDate)
            Me.Controls.Add(Me.lblRateDate)
            Me.Controls.Add(Me.cmbCurrency)
            Me.Controls.Add(Me.lblCurrency)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmExchangeRateEdit"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "為替レート登録"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblCurrency As System.Windows.Forms.Label
        Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
        Friend WithEvents lblRateDate As System.Windows.Forms.Label
        Friend WithEvents dtpRateDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblTtm As System.Windows.Forms.Label
        Friend WithEvents txtTtm As System.Windows.Forms.TextBox
        Friend WithEvents lblTts As System.Windows.Forms.Label
        Friend WithEvents txtTts As System.Windows.Forms.TextBox
        Friend WithEvents lblTtb As System.Windows.Forms.Label
        Friend WithEvents txtTtb As System.Windows.Forms.TextBox
        Friend WithEvents btnSave As System.Windows.Forms.Button

    End Class
End Namespace
