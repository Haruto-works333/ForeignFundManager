Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmDataImport
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
            Me.lblImportType = New System.Windows.Forms.Label()
            Me.cmbImportType = New System.Windows.Forms.ComboBox()
            Me.lblFilePath = New System.Windows.Forms.Label()
            Me.txtFilePath = New System.Windows.Forms.TextBox()
            Me.btnBrowse = New System.Windows.Forms.Button()
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.lblResult = New System.Windows.Forms.Label()
            Me.dgvResults = New System.Windows.Forms.DataGridView()
            Me.colItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblImportType
            '
            Me.lblImportType.AutoSize = True
            Me.lblImportType.Location = New System.Drawing.Point(12, 18)
            Me.lblImportType.Name = "lblImportType"
            Me.lblImportType.Size = New System.Drawing.Size(53, 12)
            Me.lblImportType.TabIndex = 0
            Me.lblImportType.Text = "取込種別"
            '
            'cmbImportType
            '
            Me.cmbImportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbImportType.Location = New System.Drawing.Point(80, 15)
            Me.cmbImportType.Name = "cmbImportType"
            Me.cmbImportType.Size = New System.Drawing.Size(120, 20)
            Me.cmbImportType.TabIndex = 1
            '
            'lblFilePath
            '
            Me.lblFilePath.AutoSize = True
            Me.lblFilePath.Location = New System.Drawing.Point(12, 52)
            Me.lblFilePath.Name = "lblFilePath"
            Me.lblFilePath.Size = New System.Drawing.Size(42, 12)
            Me.lblFilePath.TabIndex = 2
            Me.lblFilePath.Text = "ファイル"
            '
            'txtFilePath
            '
            Me.txtFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtFilePath.Location = New System.Drawing.Point(80, 49)
            Me.txtFilePath.Name = "txtFilePath"
            Me.txtFilePath.ReadOnly = True
            Me.txtFilePath.Size = New System.Drawing.Size(500, 19)
            Me.txtFilePath.TabIndex = 3
            '
            'btnBrowse
            '
            Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBrowse.Location = New System.Drawing.Point(586, 47)
            Me.btnBrowse.Name = "btnBrowse"
            Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
            Me.btnBrowse.TabIndex = 4
            Me.btnBrowse.Text = "参照..."
            Me.btnBrowse.UseVisualStyleBackColor = True
            '
            'btnExecute
            '
            Me.btnExecute.Location = New System.Drawing.Point(80, 85)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(100, 30)
            Me.btnExecute.TabIndex = 5
            Me.btnExecute.Text = "実行"
            Me.btnExecute.UseVisualStyleBackColor = True
            '
            'lblResult
            '
            Me.lblResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblResult.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblResult.Location = New System.Drawing.Point(12, 128)
            Me.lblResult.Name = "lblResult"
            Me.lblResult.Size = New System.Drawing.Size(660, 20)
            Me.lblResult.TabIndex = 6
            Me.lblResult.Text = ""
            '
            'dgvResults
            '
            Me.dgvResults.AllowUserToAddRows = False
            Me.dgvResults.AllowUserToDeleteRows = False
            Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colItem, Me.colValue})
            Me.dgvResults.Location = New System.Drawing.Point(12, 155)
            Me.dgvResults.Name = "dgvResults"
            Me.dgvResults.ReadOnly = True
            Me.dgvResults.RowHeadersVisible = False
            Me.dgvResults.RowTemplate.Height = 21
            Me.dgvResults.Size = New System.Drawing.Size(660, 300)
            Me.dgvResults.TabIndex = 7
            '
            'colItem
            '
            Me.colItem.HeaderText = "項目"
            Me.colItem.Name = "colItem"
            Me.colItem.ReadOnly = True
            Me.colItem.Width = 150
            '
            'colValue
            '
            Me.colValue.HeaderText = "値"
            Me.colValue.Name = "colValue"
            Me.colValue.ReadOnly = True
            Me.colValue.Width = 400
            '
            'FrmDataImport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(684, 461)
            Me.Controls.Add(Me.dgvResults)
            Me.Controls.Add(Me.lblResult)
            Me.Controls.Add(Me.btnExecute)
            Me.Controls.Add(Me.btnBrowse)
            Me.Controls.Add(Me.txtFilePath)
            Me.Controls.Add(Me.lblFilePath)
            Me.Controls.Add(Me.cmbImportType)
            Me.Controls.Add(Me.lblImportType)
            Me.Name = "FrmDataImport"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "データ取込"
            CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblImportType As System.Windows.Forms.Label
        Friend WithEvents cmbImportType As System.Windows.Forms.ComboBox
        Friend WithEvents lblFilePath As System.Windows.Forms.Label
        Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
        Friend WithEvents btnBrowse As System.Windows.Forms.Button
        Friend WithEvents btnExecute As System.Windows.Forms.Button
        Friend WithEvents lblResult As System.Windows.Forms.Label
        Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
        Friend WithEvents colItem As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colValue As System.Windows.Forms.DataGridViewTextBoxColumn

    End Class
End Namespace
