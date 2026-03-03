Imports ForeignFundManager.Core.Exceptions
Imports ForeignFundManager.Data.Models
Imports ForeignFundManager.Core.Services
Imports NLog

Namespace Forms

    Public Class FrmCalendar

        Private ReadOnly _calService As New CalendarService()
        Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' フォームロード時の初期化処理
        ''' </summary>
        Private Sub FrmCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ' 地域コンボ設定
                Dim regions = _calService.GetRegionCodes()
                cmbRegion.Items.Clear()
                For Each r In regions
                    cmbRegion.Items.Add(r.CodeValue)
                Next
                If cmbRegion.Items.Count > 0 Then cmbRegion.SelectedIndex = 0

                ' 年の初期値
                nudYear.Minimum = 2000
                nudYear.Maximum = 2099
                nudYear.Value = DateTime.Today.Year

                ' 月コンボ設定
                cmbMonth.Items.Clear()
                For i = 1 To 12
                    cmbMonth.Items.Add(i.ToString())
                Next
                cmbMonth.SelectedIndex = DateTime.Today.Month - 1

                ' DataGridView設定
                dgvHolidays.AutoGenerateColumns = False

                ' 新規追加日付の初期値
                dtpNewDate.Value = DateTime.Today

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "営業日カレンダー画面の初期化中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 表示ボタンクリック：祝日一覧を取得して表示する
        ''' </summary>
        Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
            Try
                If cmbRegion.SelectedItem Is Nothing Then Return

                Dim regionCode = cmbRegion.SelectedItem.ToString()
                Dim year = CInt(nudYear.Value)
                Dim month = CInt(cmbMonth.SelectedItem.ToString())

                Dim holidays = _calService.GetHolidays(regionCode, year, month)

                dgvHolidays.Rows.Clear()
                For Each h In holidays
                    dgvHolidays.Rows.Add(
                        h.CalendarDate.ToString("yyyy/MM/dd"),
                        h.HolidayName
                    )
                Next

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "祝日一覧の取得中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 追加ボタンクリック：祝日を追加する
        ''' </summary>
        Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
            Try
                If cmbRegion.SelectedItem Is Nothing Then
                    MessageBox.Show("地域を選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If String.IsNullOrWhiteSpace(txtHolidayName.Text) Then
                    MessageBox.Show("祝日名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtHolidayName.Focus()
                    Return
                End If

                Dim cal As New BusinessCalendar()
                cal.RegionCode = cmbRegion.SelectedItem.ToString()
                cal.CalendarDate = dtpNewDate.Value.Date
                cal.HolidayName = txtHolidayName.Text.Trim()

                _calService.AddHoliday(cal)

                MessageBox.Show("祝日を追加しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' 追加後に再表示
                txtHolidayName.Text = ""
                btnDisplay.PerformClick()

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "祝日の追加中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 削除ボタンクリック：選択行の祝日を削除する
        ''' </summary>
        Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
            Try
                If dgvHolidays.SelectedRows.Count = 0 Then
                    MessageBox.Show("削除する祝日を選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If MessageBox.Show("選択した祝日を削除してよろしいですか？", "確認",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If

                Dim regionCode = cmbRegion.SelectedItem.ToString()
                Dim dateStr = dgvHolidays.SelectedRows(0).Cells(0).Value.ToString()
                Dim calendarDate = Date.Parse(dateStr)

                _calService.RemoveHoliday(regionCode, calendarDate)

                MessageBox.Show("祝日を削除しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' 削除後に再表示
                btnDisplay.PerformClick()

            Catch ex As BusinessException
                MessageBox.Show(ex.Message, "業務エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                _logger.Fatal(ex, "祝日の削除中にエラーが発生しました。")
                MessageBox.Show("システムエラーが発生しました。管理者に連絡してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class

End Namespace
