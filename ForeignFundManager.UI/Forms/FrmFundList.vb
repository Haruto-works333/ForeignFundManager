Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms
    Public Class FrmFundList

        Private ReadOnly _fundService As New FundService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private _fundList As List(Of Fund)

        ''' <summary>
        ''' 画面ロード時の処理。コンボボックスの初期化と検索実行。
        ''' </summary>
        Private Sub FrmFundList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                InitializeComboBoxes()
                InitializeDataGridView()
                ExecuteSearch()
            Catch ex As Exception
                _logger.Fatal(ex, "FrmFundList_Load")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 通貨・籍国コンボボックスにマスタデータをバインドする。
        ''' </summary>
        Private Sub InitializeComboBoxes()
            ' 通貨コンボボックス
            Dim currencies = _fundService.GetCurrencies()
            Dim allCurrency As New CodeMaster()
            allCurrency.CodeValue = ""
            allCurrency.CodeNameJp = "全て"
            currencies.Insert(0, allCurrency)
            cmbCurrency.DataSource = currencies
            cmbCurrency.DisplayMember = "CodeNameJp"
            cmbCurrency.ValueMember = "CodeValue"

            ' 籍国コンボボックス
            Dim countries = _fundService.GetCountries()
            Dim allCountry As New CodeMaster()
            allCountry.CodeValue = ""
            allCountry.CodeNameJp = "全て"
            countries.Insert(0, allCountry)
            cmbCountry.DataSource = countries
            cmbCountry.DisplayMember = "CodeNameJp"
            cmbCountry.ValueMember = "CodeValue"
        End Sub

        ''' <summary>
        ''' DataGridViewの列定義と共通設定を行う。
        ''' </summary>
        Private Sub InitializeDataGridView()
            dgvFunds.AutoGenerateColumns = False
            dgvFunds.Columns.Clear()

            Dim colIsin As New DataGridViewTextBoxColumn()
            colIsin.Name = "colIsin"
            colIsin.HeaderText = "ISIN"
            colIsin.DataPropertyName = "Isin"
            colIsin.Width = 120
            dgvFunds.Columns.Add(colIsin)

            Dim colNameEn As New DataGridViewTextBoxColumn()
            colNameEn.Name = "colFundNameEn"
            colNameEn.HeaderText = "ファンド名称（英語）"
            colNameEn.DataPropertyName = "FundNameEn"
            colNameEn.Width = 200
            dgvFunds.Columns.Add(colNameEn)

            Dim colNameJp As New DataGridViewTextBoxColumn()
            colNameJp.Name = "colFundNameJp"
            colNameJp.HeaderText = "ファンド名称（日本語）"
            colNameJp.DataPropertyName = "FundNameJp"
            colNameJp.Width = 200
            dgvFunds.Columns.Add(colNameJp)

            Dim colCurrency As New DataGridViewTextBoxColumn()
            colCurrency.Name = "colCurrency"
            colCurrency.HeaderText = "通貨"
            colCurrency.DataPropertyName = "CurrencyCode"
            colCurrency.Width = 60
            dgvFunds.Columns.Add(colCurrency)

            Dim colCountry As New DataGridViewTextBoxColumn()
            colCountry.Name = "colCountry"
            colCountry.HeaderText = "籍国"
            colCountry.DataPropertyName = "DomicileCountryCode"
            colCountry.Width = 60
            dgvFunds.Columns.Add(colCountry)

            Dim colStatus As New DataGridViewTextBoxColumn()
            colStatus.Name = "colStatus"
            colStatus.HeaderText = "状態"
            colStatus.DataPropertyName = "Status"
            colStatus.Width = 80
            dgvFunds.Columns.Add(colStatus)

            ' 交互行の背景色
            dgvFunds.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
        End Sub

        ''' <summary>
        ''' 検索ボタンクリック時の処理。
        ''' </summary>
        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                ExecuteSearch()
            Catch bex As BusinessException
                MessageBox.Show(bex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "btnSearch_Click")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 検索を実行し、結果をDataGridViewにバインドする。
        ''' </summary>
        Private Sub ExecuteSearch()
            Dim isin = txtIsin.Text.Trim()
            Dim fundName = txtFundName.Text.Trim()
            Dim currencyCode = If(cmbCurrency.SelectedValue IsNot Nothing, cmbCurrency.SelectedValue.ToString(), "")
            Dim countryCode = If(cmbCountry.SelectedValue IsNot Nothing, cmbCountry.SelectedValue.ToString(), "")
            Dim isActiveOnly = chkActiveOnly.Checked

            _fundList = _fundService.Search(isin, fundName, currencyCode, countryCode, isActiveOnly)

            dgvFunds.DataSource = Nothing
            dgvFunds.DataSource = _fundList

            ' ステータス列の表示変換
            FormatStatusColumn()

            lblResultCount.Text = $"検索結果: {_fundList.Count} 件"
        End Sub

        ''' <summary>
        ''' ステータスのコード値を名称に変換表示する。
        ''' </summary>
        Private Sub FormatStatusColumn()
            For Each row As DataGridViewRow In dgvFunds.Rows
                If row.Cells("colStatus").Value IsNot Nothing Then
                    Dim statusValue = row.Cells("colStatus").Value.ToString()
                    Select Case statusValue
                        Case AppConstants.FundStatusActive
                            row.Cells("colStatus").Value = "有効"
                        Case AppConstants.FundStatusDeleted
                            row.Cells("colStatus").Value = "削除済"
                        Case Else
                            row.Cells("colStatus").Value = statusValue
                    End Select
                End If
            Next
        End Sub

        ''' <summary>
        ''' クリアボタンクリック時の処理。検索条件を初期値に戻す。
        ''' </summary>
        Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
            txtIsin.Text = ""
            txtFundName.Text = ""
            cmbCurrency.SelectedIndex = 0
            cmbCountry.SelectedIndex = 0
            chkActiveOnly.Checked = True
            dgvFunds.DataSource = Nothing
            _fundList = Nothing
            lblResultCount.Text = ""
        End Sub

        ''' <summary>
        ''' 新規登録ボタンクリック時の処理。FrmFundEditを新規モードで開く。
        ''' </summary>
        Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            Try
                Dim frm As New FrmFundEdit()
                frm.IsNew = True
                frm.MdiParent = Me.MdiParent
                AddHandler frm.FormClosed, AddressOf FundEditForm_Closed
                frm.Show()
            Catch ex As Exception
                _logger.Fatal(ex, "btnNew_Click")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' DataGridView行ダブルクリック時の処理。FrmFundEditを編集モードで開く。
        ''' </summary>
        Private Sub dgvFunds_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFunds.CellDoubleClick
            If e.RowIndex < 0 Then Return

            Try
                Dim selectedIsin = dgvFunds.Rows(e.RowIndex).Cells("colIsin").Value.ToString()

                Dim frm As New FrmFundEdit()
                frm.IsNew = False
                frm.Isin = selectedIsin
                frm.MdiParent = Me.MdiParent
                AddHandler frm.FormClosed, AddressOf FundEditForm_Closed
                frm.Show()
            Catch ex As Exception
                _logger.Fatal(ex, "dgvFunds_CellDoubleClick")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' FrmFundEdit画面が閉じられた時に一覧を再検索する。
        ''' </summary>
        Private Sub FundEditForm_Closed(sender As Object, e As FormClosedEventArgs)
            Try
                ExecuteSearch()
            Catch ex As Exception
                _logger.Fatal(ex, "FundEditForm_Closed")
            End Try
        End Sub

        ''' <summary>
        ''' 削除ボタンクリック時の処理。選択行のファンドを論理削除する。
        ''' </summary>
        Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
            Try
                If dgvFunds.CurrentRow Is Nothing Then
                    MessageBox.Show(MessageConstants.MsgNoSelection, "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim selectedIsin = dgvFunds.CurrentRow.Cells("colIsin").Value.ToString()

                Dim result = MessageBox.Show(
                    String.Format(MessageConstants.MsgConfirmDelete, selectedIsin),
                    "確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    _fundService.Delete(selectedIsin)
                    MessageBox.Show(MessageConstants.MsgDeleteSuccess, "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ExecuteSearch()
                End If

            Catch bex As BusinessException
                MessageBox.Show(bex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "btnDelete_Click")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 変更履歴ボタンクリック時の処理。FrmFundHistoryを開く。
        ''' </summary>
        Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
            Try
                If dgvFunds.CurrentRow Is Nothing Then
                    MessageBox.Show(MessageConstants.MsgNoSelection, "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim selectedIsin = dgvFunds.CurrentRow.Cells("colIsin").Value.ToString()

                Dim frm As New FrmFundHistory()
                frm.Isin = selectedIsin
                frm.MdiParent = Me.MdiParent
                frm.Show()

            Catch ex As Exception
                _logger.Fatal(ex, "btnHistory_Click")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' CSV出力ボタンクリック時の処理。SaveFileDialogを表示しCSVファイルに出力する。
        ''' </summary>
        Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
            Try
                If _fundList Is Nothing OrElse _fundList.Count = 0 Then
                    MessageBox.Show("出力対象のデータがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Using sfd As New SaveFileDialog()
                    sfd.Filter = "CSVファイル (*.csv)|*.csv"
                    sfd.DefaultExt = "csv"
                    sfd.FileName = $"FundList_{Date.Today:yyyyMMdd}.csv"

                    If sfd.ShowDialog() = DialogResult.OK Then
                        _fundService.ExportCsv(sfd.FileName, _fundList)
                        MessageBox.Show($"CSVファイルを出力しました。{Environment.NewLine}{sfd.FileName}",
                                       "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using

            Catch bex As BusinessException
                MessageBox.Show(bex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "btnExportCsv_Click")
                MessageBox.Show(MessageConstants.MsgSystemError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' DataGridViewに行番号を描画する。
        ''' </summary>
        Private Sub dgvFunds_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvFunds.RowPostPaint
            Using b As New SolidBrush(dgvFunds.RowHeadersDefaultCellStyle.ForeColor)
                e.Graphics.DrawString(
                    (e.RowIndex + 1).ToString(),
                    dgvFunds.DefaultCellStyle.Font,
                    b,
                    e.RowBounds.Location.X + 4,
                    e.RowBounds.Location.Y + 4)
            End Using
        End Sub

    End Class
End Namespace
