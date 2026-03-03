Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmFundHistory
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
            Me.dgvHistory = New System.Windows.Forms.DataGridView()
            Me.colOperatedAt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colOperationType = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colOldValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colNewValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colOperatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'dgvHistory
            '
            Me.dgvHistory.AllowUserToAddRows = False
            Me.dgvHistory.AllowUserToDeleteRows = False
            Me.dgvHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colOperatedAt, Me.colOperationType, Me.colColumnName, Me.colOldValue, Me.colNewValue, Me.colOperatedBy})
            Me.dgvHistory.Location = New System.Drawing.Point(12, 12)
            Me.dgvHistory.Name = "dgvHistory"
            Me.dgvHistory.ReadOnly = True
            Me.dgvHistory.RowHeadersVisible = False
            Me.dgvHistory.RowTemplate.Height = 21
            Me.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvHistory.Size = New System.Drawing.Size(660, 440)
            Me.dgvHistory.TabIndex = 0
            '
            'colOperatedAt
            '
            Me.colOperatedAt.HeaderText = "操作日時"
            Me.colOperatedAt.Name = "colOperatedAt"
            Me.colOperatedAt.ReadOnly = True
            Me.colOperatedAt.Width = 130
            '
            'colOperationType
            '
            Me.colOperationType.HeaderText = "操作種別"
            Me.colOperationType.Name = "colOperationType"
            Me.colOperationType.ReadOnly = True
            Me.colOperationType.Width = 80
            '
            'colColumnName
            '
            Me.colColumnName.HeaderText = "カラム名"
            Me.colColumnName.Name = "colColumnName"
            Me.colColumnName.ReadOnly = True
            Me.colColumnName.Width = 120
            '
            'colOldValue
            '
            Me.colOldValue.HeaderText = "変更前"
            Me.colOldValue.Name = "colOldValue"
            Me.colOldValue.ReadOnly = True
            Me.colOldValue.Width = 120
            '
            'colNewValue
            '
            Me.colNewValue.HeaderText = "変更後"
            Me.colNewValue.Name = "colNewValue"
            Me.colNewValue.ReadOnly = True
            Me.colNewValue.Width = 120
            '
            'colOperatedBy
            '
            Me.colOperatedBy.HeaderText = "操作者"
            Me.colOperatedBy.Name = "colOperatedBy"
            Me.colOperatedBy.ReadOnly = True
            Me.colOperatedBy.Width = 80
            '
            'FrmFundHistory
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(684, 461)
            Me.Controls.Add(Me.dgvHistory)
            Me.Name = "FrmFundHistory"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "変更履歴"
            CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents dgvHistory As System.Windows.Forms.DataGridView
        Friend WithEvents colOperatedAt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colOperationType As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colColumnName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colOldValue As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colNewValue As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colOperatedBy As System.Windows.Forms.DataGridViewTextBoxColumn

    End Class
End Namespace
