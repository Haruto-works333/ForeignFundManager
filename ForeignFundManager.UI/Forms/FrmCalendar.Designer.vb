Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCalendar
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
            Me.lblRegion = New System.Windows.Forms.Label()
            Me.cmbRegion = New System.Windows.Forms.ComboBox()
            Me.lblYear = New System.Windows.Forms.Label()
            Me.nudYear = New System.Windows.Forms.NumericUpDown()
            Me.lblMonth = New System.Windows.Forms.Label()
            Me.cmbMonth = New System.Windows.Forms.ComboBox()
            Me.btnDisplay = New System.Windows.Forms.Button()
            Me.dgvHolidays = New System.Windows.Forms.DataGridView()
            Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colHolidayName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.btnRemove = New System.Windows.Forms.Button()
            Me.pnlAdd = New System.Windows.Forms.Panel()
            Me.lblNewDate = New System.Windows.Forms.Label()
            Me.dtpNewDate = New System.Windows.Forms.DateTimePicker()
            Me.lblHolidayName = New System.Windows.Forms.Label()
            Me.txtHolidayName = New System.Windows.Forms.TextBox()
            Me.btnAdd = New System.Windows.Forms.Button()
            CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dgvHolidays, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlAdd.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblRegion
            '
            Me.lblRegion.AutoSize = True
            Me.lblRegion.Location = New System.Drawing.Point(12, 18)
            Me.lblRegion.Name = "lblRegion"
            Me.lblRegion.Size = New System.Drawing.Size(29, 12)
            Me.lblRegion.TabIndex = 0
            Me.lblRegion.Text = "地域"
            '
            'cmbRegion
            '
            Me.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbRegion.Location = New System.Drawing.Point(47, 15)
            Me.cmbRegion.Name = "cmbRegion"
            Me.cmbRegion.Size = New System.Drawing.Size(80, 20)
            Me.cmbRegion.TabIndex = 1
            '
            'lblYear
            '
            Me.lblYear.AutoSize = True
            Me.lblYear.Location = New System.Drawing.Point(143, 18)
            Me.lblYear.Name = "lblYear"
            Me.lblYear.Size = New System.Drawing.Size(17, 12)
            Me.lblYear.TabIndex = 2
            Me.lblYear.Text = "年"
            '
            'nudYear
            '
            Me.nudYear.Location = New System.Drawing.Point(166, 15)
            Me.nudYear.Maximum = New Decimal(New Integer() {2099, 0, 0, 0})
            Me.nudYear.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
            Me.nudYear.Name = "nudYear"
            Me.nudYear.Size = New System.Drawing.Size(70, 19)
            Me.nudYear.TabIndex = 3
            Me.nudYear.Value = New Decimal(New Integer() {2026, 0, 0, 0})
            '
            'lblMonth
            '
            Me.lblMonth.AutoSize = True
            Me.lblMonth.Location = New System.Drawing.Point(250, 18)
            Me.lblMonth.Name = "lblMonth"
            Me.lblMonth.Size = New System.Drawing.Size(17, 12)
            Me.lblMonth.TabIndex = 4
            Me.lblMonth.Text = "月"
            '
            'cmbMonth
            '
            Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbMonth.Location = New System.Drawing.Point(273, 15)
            Me.cmbMonth.Name = "cmbMonth"
            Me.cmbMonth.Size = New System.Drawing.Size(50, 20)
            Me.cmbMonth.TabIndex = 5
            '
            'btnDisplay
            '
            Me.btnDisplay.Location = New System.Drawing.Point(340, 13)
            Me.btnDisplay.Name = "btnDisplay"
            Me.btnDisplay.Size = New System.Drawing.Size(75, 23)
            Me.btnDisplay.TabIndex = 6
            Me.btnDisplay.Text = "表示"
            Me.btnDisplay.UseVisualStyleBackColor = True
            '
            'dgvHolidays
            '
            Me.dgvHolidays.AllowUserToAddRows = False
            Me.dgvHolidays.AllowUserToDeleteRows = False
            Me.dgvHolidays.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvHolidays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvHolidays.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colHolidayName})
            Me.dgvHolidays.Location = New System.Drawing.Point(12, 45)
            Me.dgvHolidays.Name = "dgvHolidays"
            Me.dgvHolidays.ReadOnly = True
            Me.dgvHolidays.RowHeadersVisible = False
            Me.dgvHolidays.RowTemplate.Height = 21
            Me.dgvHolidays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvHolidays.Size = New System.Drawing.Size(660, 300)
            Me.dgvHolidays.TabIndex = 7
            '
            'colDate
            '
            Me.colDate.HeaderText = "日付"
            Me.colDate.Name = "colDate"
            Me.colDate.ReadOnly = True
            Me.colDate.Width = 120
            '
            'colHolidayName
            '
            Me.colHolidayName.HeaderText = "祝日名"
            Me.colHolidayName.Name = "colHolidayName"
            Me.colHolidayName.ReadOnly = True
            Me.colHolidayName.Width = 300
            '
            'btnRemove
            '
            Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRemove.Location = New System.Drawing.Point(597, 351)
            Me.btnRemove.Name = "btnRemove"
            Me.btnRemove.Size = New System.Drawing.Size(75, 23)
            Me.btnRemove.TabIndex = 8
            Me.btnRemove.Text = "削除"
            Me.btnRemove.UseVisualStyleBackColor = True
            '
            'pnlAdd
            '
            Me.pnlAdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlAdd.Controls.Add(Me.lblNewDate)
            Me.pnlAdd.Controls.Add(Me.dtpNewDate)
            Me.pnlAdd.Controls.Add(Me.lblHolidayName)
            Me.pnlAdd.Controls.Add(Me.txtHolidayName)
            Me.pnlAdd.Controls.Add(Me.btnAdd)
            Me.pnlAdd.Location = New System.Drawing.Point(12, 385)
            Me.pnlAdd.Name = "pnlAdd"
            Me.pnlAdd.Size = New System.Drawing.Size(660, 65)
            Me.pnlAdd.TabIndex = 9
            '
            'lblNewDate
            '
            Me.lblNewDate.AutoSize = True
            Me.lblNewDate.Location = New System.Drawing.Point(10, 22)
            Me.lblNewDate.Name = "lblNewDate"
            Me.lblNewDate.Size = New System.Drawing.Size(29, 12)
            Me.lblNewDate.TabIndex = 0
            Me.lblNewDate.Text = "日付"
            '
            'dtpNewDate
            '
            Me.dtpNewDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpNewDate.Location = New System.Drawing.Point(45, 19)
            Me.dtpNewDate.Name = "dtpNewDate"
            Me.dtpNewDate.Size = New System.Drawing.Size(120, 19)
            Me.dtpNewDate.TabIndex = 1
            '
            'lblHolidayName
            '
            Me.lblHolidayName.AutoSize = True
            Me.lblHolidayName.Location = New System.Drawing.Point(180, 22)
            Me.lblHolidayName.Name = "lblHolidayName"
            Me.lblHolidayName.Size = New System.Drawing.Size(41, 12)
            Me.lblHolidayName.TabIndex = 2
            Me.lblHolidayName.Text = "祝日名"
            '
            'txtHolidayName
            '
            Me.txtHolidayName.Location = New System.Drawing.Point(227, 19)
            Me.txtHolidayName.Name = "txtHolidayName"
            Me.txtHolidayName.Size = New System.Drawing.Size(300, 19)
            Me.txtHolidayName.TabIndex = 3
            '
            'btnAdd
            '
            Me.btnAdd.Location = New System.Drawing.Point(545, 17)
            Me.btnAdd.Name = "btnAdd"
            Me.btnAdd.Size = New System.Drawing.Size(75, 23)
            Me.btnAdd.TabIndex = 4
            Me.btnAdd.Text = "追加"
            Me.btnAdd.UseVisualStyleBackColor = True
            '
            'FrmCalendar
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(684, 461)
            Me.Controls.Add(Me.pnlAdd)
            Me.Controls.Add(Me.btnRemove)
            Me.Controls.Add(Me.dgvHolidays)
            Me.Controls.Add(Me.btnDisplay)
            Me.Controls.Add(Me.cmbMonth)
            Me.Controls.Add(Me.lblMonth)
            Me.Controls.Add(Me.nudYear)
            Me.Controls.Add(Me.lblYear)
            Me.Controls.Add(Me.cmbRegion)
            Me.Controls.Add(Me.lblRegion)
            Me.Name = "FrmCalendar"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "営業日カレンダー"
            CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dgvHolidays, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlAdd.ResumeLayout(False)
            Me.pnlAdd.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblRegion As System.Windows.Forms.Label
        Friend WithEvents cmbRegion As System.Windows.Forms.ComboBox
        Friend WithEvents lblYear As System.Windows.Forms.Label
        Friend WithEvents nudYear As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblMonth As System.Windows.Forms.Label
        Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
        Friend WithEvents btnDisplay As System.Windows.Forms.Button
        Friend WithEvents dgvHolidays As System.Windows.Forms.DataGridView
        Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colHolidayName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents btnRemove As System.Windows.Forms.Button
        Friend WithEvents pnlAdd As System.Windows.Forms.Panel
        Friend WithEvents lblNewDate As System.Windows.Forms.Label
        Friend WithEvents dtpNewDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblHolidayName As System.Windows.Forms.Label
        Friend WithEvents txtHolidayName As System.Windows.Forms.TextBox
        Friend WithEvents btnAdd As System.Windows.Forms.Button

    End Class
End Namespace
