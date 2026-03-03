Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmBatchExecution
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
            Me.lblTargetDate = New System.Windows.Forms.Label()
            Me.dtpTargetDate = New System.Windows.Forms.DateTimePicker()
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.lblTotal = New System.Windows.Forms.Label()
            Me.lblSuccess = New System.Windows.Forms.Label()
            Me.lblSkip = New System.Windows.Forms.Label()
            Me.lblError = New System.Windows.Forms.Label()
            Me.lblElapsed = New System.Windows.Forms.Label()
            Me.dgvResults = New System.Windows.Forms.DataGridView()
            Me.colIsin = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCurrency = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colNavPerUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAppliedRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colNavJpy = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colNote = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblTargetDate
            '
            Me.lblTargetDate.AutoSize = True
            Me.lblTargetDate.Location = New System.Drawing.Point(12, 18)
            Me.lblTargetDate.Name = "lblTargetDate"
            Me.lblTargetDate.Size = New System.Drawing.Size(53, 12)
            Me.lblTargetDate.TabIndex = 0
            Me.lblTargetDate.Text = "対象日付"
            '
            'dtpTargetDate
            '
            Me.dtpTargetDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpTargetDate.Location = New System.Drawing.Point(80, 15)
            Me.dtpTargetDate.Name = "dtpTargetDate"
            Me.dtpTargetDate.Size = New System.Drawing.Size(150, 19)
            Me.dtpTargetDate.TabIndex = 1
            '
            'btnExecute
            '
            Me.btnExecute.Location = New System.Drawing.Point(250, 12)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(100, 25)
            Me.btnExecute.TabIndex = 2
            Me.btnExecute.Text = "実行"
            Me.btnExecute.UseVisualStyleBackColor = True
            '
            'lblTotal
            '
            Me.lblTotal.AutoSize = True
            Me.lblTotal.Location = New System.Drawing.Point(12, 50)
            Me.lblTotal.Name = "lblTotal"
            Me.lblTotal.Size = New System.Drawing.Size(40, 12)
            Me.lblTotal.TabIndex = 3
            Me.lblTotal.Text = "合計: -"
            '
            'lblSuccess
            '
            Me.lblSuccess.AutoSize = True
            Me.lblSuccess.Location = New System.Drawing.Point(130, 50)
            Me.lblSuccess.Name = "lblSuccess"
            Me.lblSuccess.Size = New System.Drawing.Size(40, 12)
            Me.lblSuccess.TabIndex = 4
            Me.lblSuccess.Text = "成功: -"
            '
            'lblSkip
            '
            Me.lblSkip.AutoSize = True
            Me.lblSkip.Location = New System.Drawing.Point(250, 50)
            Me.lblSkip.Name = "lblSkip"
            Me.lblSkip.Size = New System.Drawing.Size(54, 12)
            Me.lblSkip.TabIndex = 5
            Me.lblSkip.Text = "スキップ: -"
            '
            'lblError
            '
            Me.lblError.AutoSize = True
            Me.lblError.Location = New System.Drawing.Point(380, 50)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(42, 12)
            Me.lblError.TabIndex = 6
            Me.lblError.Text = "エラー: -"
            '
            'lblElapsed
            '
            Me.lblElapsed.AutoSize = True
            Me.lblElapsed.Location = New System.Drawing.Point(510, 50)
            Me.lblElapsed.Name = "lblElapsed"
            Me.lblElapsed.Size = New System.Drawing.Size(66, 12)
            Me.lblElapsed.TabIndex = 7
            Me.lblElapsed.Text = "経過時間: -"
            '
            'dgvResults
            '
            Me.dgvResults.AllowUserToAddRows = False
            Me.dgvResults.AllowUserToDeleteRows = False
            Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIsin, Me.colCurrency, Me.colNavPerUnit, Me.colAppliedRate, Me.colNavJpy, Me.colStatus, Me.colNote})
            Me.dgvResults.Location = New System.Drawing.Point(12, 75)
            Me.dgvResults.Name = "dgvResults"
            Me.dgvResults.ReadOnly = True
            Me.dgvResults.RowHeadersVisible = False
            Me.dgvResults.RowTemplate.Height = 21
            Me.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvResults.Size = New System.Drawing.Size(760, 480)
            Me.dgvResults.TabIndex = 8
            '
            'colIsin
            '
            Me.colIsin.HeaderText = "ISIN"
            Me.colIsin.Name = "colIsin"
            Me.colIsin.ReadOnly = True
            Me.colIsin.Width = 120
            '
            'colCurrency
            '
            Me.colCurrency.HeaderText = "通貨"
            Me.colCurrency.Name = "colCurrency"
            Me.colCurrency.ReadOnly = True
            Me.colCurrency.Width = 60
            '
            'colNavPerUnit
            '
            Me.colNavPerUnit.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
            Me.colNavPerUnit.HeaderText = "外貨NAV"
            Me.colNavPerUnit.Name = "colNavPerUnit"
            Me.colNavPerUnit.ReadOnly = True
            Me.colNavPerUnit.Width = 100
            '
            'colAppliedRate
            '
            Me.colAppliedRate.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
            Me.colAppliedRate.HeaderText = "適用レート"
            Me.colAppliedRate.Name = "colAppliedRate"
            Me.colAppliedRate.ReadOnly = True
            Me.colAppliedRate.Width = 100
            '
            'colNavJpy
            '
            Me.colNavJpy.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
            Me.colNavJpy.HeaderText = "円建てNAV"
            Me.colNavJpy.Name = "colNavJpy"
            Me.colNavJpy.ReadOnly = True
            Me.colNavJpy.Width = 100
            '
            'colStatus
            '
            Me.colStatus.HeaderText = "状態"
            Me.colStatus.Name = "colStatus"
            Me.colStatus.ReadOnly = True
            Me.colStatus.Width = 60
            '
            'colNote
            '
            Me.colNote.HeaderText = "備考"
            Me.colNote.Name = "colNote"
            Me.colNote.ReadOnly = True
            Me.colNote.Width = 200
            '
            'FrmBatchExecution
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(784, 561)
            Me.Controls.Add(Me.dgvResults)
            Me.Controls.Add(Me.lblElapsed)
            Me.Controls.Add(Me.lblError)
            Me.Controls.Add(Me.lblSkip)
            Me.Controls.Add(Me.lblSuccess)
            Me.Controls.Add(Me.lblTotal)
            Me.Controls.Add(Me.btnExecute)
            Me.Controls.Add(Me.dtpTargetDate)
            Me.Controls.Add(Me.lblTargetDate)
            Me.Name = "FrmBatchExecution"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "基準価額計算バッチ実行"
            CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblTargetDate As System.Windows.Forms.Label
        Friend WithEvents dtpTargetDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnExecute As System.Windows.Forms.Button
        Friend WithEvents lblTotal As System.Windows.Forms.Label
        Friend WithEvents lblSuccess As System.Windows.Forms.Label
        Friend WithEvents lblSkip As System.Windows.Forms.Label
        Friend WithEvents lblError As System.Windows.Forms.Label
        Friend WithEvents lblElapsed As System.Windows.Forms.Label
        Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
        Friend WithEvents colIsin As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCurrency As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colNavPerUnit As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAppliedRate As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colNavJpy As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colNote As System.Windows.Forms.DataGridViewTextBoxColumn

    End Class
End Namespace
