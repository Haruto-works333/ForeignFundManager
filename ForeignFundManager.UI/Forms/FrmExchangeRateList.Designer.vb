Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmExchangeRateList
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
            Me.lblFrom = New System.Windows.Forms.Label()
            Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
            Me.lblTo = New System.Windows.Forms.Label()
            Me.dtpTo = New System.Windows.Forms.DateTimePicker()
            Me.btnSearch = New System.Windows.Forms.Button()
            Me.btnNew = New System.Windows.Forms.Button()
            Me.dgvRates = New System.Windows.Forms.DataGridView()
            Me.colCurrency = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colRateDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTtm = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTts = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTtb = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.dgvRates, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblCurrency
            '
            Me.lblCurrency.AutoSize = True
            Me.lblCurrency.Location = New System.Drawing.Point(12, 18)
            Me.lblCurrency.Name = "lblCurrency"
            Me.lblCurrency.Size = New System.Drawing.Size(29, 12)
            Me.lblCurrency.TabIndex = 0
            Me.lblCurrency.Text = "通貨"
            '
            'cmbCurrency
            '
            Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCurrency.Location = New System.Drawing.Point(47, 15)
            Me.cmbCurrency.Name = "cmbCurrency"
            Me.cmbCurrency.Size = New System.Drawing.Size(80, 20)
            Me.cmbCurrency.TabIndex = 1
            '
            'lblFrom
            '
            Me.lblFrom.AutoSize = True
            Me.lblFrom.Location = New System.Drawing.Point(143, 18)
            Me.lblFrom.Name = "lblFrom"
            Me.lblFrom.Size = New System.Drawing.Size(29, 12)
            Me.lblFrom.TabIndex = 2
            Me.lblFrom.Text = "From"
            '
            'dtpFrom
            '
            Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpFrom.Location = New System.Drawing.Point(178, 15)
            Me.dtpFrom.Name = "dtpFrom"
            Me.dtpFrom.Size = New System.Drawing.Size(120, 19)
            Me.dtpFrom.TabIndex = 3
            '
            'lblTo
            '
            Me.lblTo.AutoSize = True
            Me.lblTo.Location = New System.Drawing.Point(310, 18)
            Me.lblTo.Name = "lblTo"
            Me.lblTo.Size = New System.Drawing.Size(17, 12)
            Me.lblTo.TabIndex = 4
            Me.lblTo.Text = "To"
            '
            'dtpTo
            '
            Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpTo.Location = New System.Drawing.Point(333, 15)
            Me.dtpTo.Name = "dtpTo"
            Me.dtpTo.Size = New System.Drawing.Size(120, 19)
            Me.dtpTo.TabIndex = 5
            '
            'btnSearch
            '
            Me.btnSearch.Location = New System.Drawing.Point(470, 13)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(75, 23)
            Me.btnSearch.TabIndex = 6
            Me.btnSearch.Text = "検索"
            Me.btnSearch.UseVisualStyleBackColor = True
            '
            'btnNew
            '
            Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnNew.Location = New System.Drawing.Point(697, 13)
            Me.btnNew.Name = "btnNew"
            Me.btnNew.Size = New System.Drawing.Size(75, 23)
            Me.btnNew.TabIndex = 7
            Me.btnNew.Text = "新規登録"
            Me.btnNew.UseVisualStyleBackColor = True
            '
            'dgvRates
            '
            Me.dgvRates.AllowUserToAddRows = False
            Me.dgvRates.AllowUserToDeleteRows = False
            Me.dgvRates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvRates.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCurrency, Me.colRateDate, Me.colTtm, Me.colTts, Me.colTtb})
            Me.dgvRates.Location = New System.Drawing.Point(12, 45)
            Me.dgvRates.Name = "dgvRates"
            Me.dgvRates.ReadOnly = True
            Me.dgvRates.RowHeadersVisible = False
            Me.dgvRates.RowTemplate.Height = 21
            Me.dgvRates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvRates.Size = New System.Drawing.Size(760, 410)
            Me.dgvRates.TabIndex = 8
            '
            'colCurrency
            '
            Me.colCurrency.HeaderText = "通貨"
            Me.colCurrency.Name = "colCurrency"
            Me.colCurrency.ReadOnly = True
            Me.colCurrency.Width = 80
            '
            'colRateDate
            '
            Me.colRateDate.HeaderText = "基準日"
            Me.colRateDate.Name = "colRateDate"
            Me.colRateDate.ReadOnly = True
            Me.colRateDate.Width = 120
            '
            'colTtm
            '
            Me.colTtm.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
            Me.colTtm.HeaderText = "TTM"
            Me.colTtm.Name = "colTtm"
            Me.colTtm.ReadOnly = True
            Me.colTtm.Width = 130
            '
            'colTts
            '
            Me.colTts.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
            Me.colTts.HeaderText = "TTS"
            Me.colTts.Name = "colTts"
            Me.colTts.ReadOnly = True
            Me.colTts.Width = 130
            '
            'colTtb
            '
            Me.colTtb.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
            Me.colTtb.HeaderText = "TTB"
            Me.colTtb.Name = "colTtb"
            Me.colTtb.ReadOnly = True
            Me.colTtb.Width = 130
            '
            'FrmExchangeRateList
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(784, 461)
            Me.Controls.Add(Me.dgvRates)
            Me.Controls.Add(Me.btnNew)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.dtpTo)
            Me.Controls.Add(Me.lblTo)
            Me.Controls.Add(Me.dtpFrom)
            Me.Controls.Add(Me.lblFrom)
            Me.Controls.Add(Me.cmbCurrency)
            Me.Controls.Add(Me.lblCurrency)
            Me.Name = "FrmExchangeRateList"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "為替レート一覧"
            CType(Me.dgvRates, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblCurrency As System.Windows.Forms.Label
        Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
        Friend WithEvents lblFrom As System.Windows.Forms.Label
        Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblTo As System.Windows.Forms.Label
        Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Friend WithEvents btnNew As System.Windows.Forms.Button
        Friend WithEvents dgvRates As System.Windows.Forms.DataGridView
        Friend WithEvents colCurrency As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colRateDate As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTtm As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTts As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTtb As System.Windows.Forms.DataGridViewTextBoxColumn

    End Class
End Namespace
