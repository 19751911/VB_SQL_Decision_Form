Imports System.Data

Public Class MainForm
    Private dbHelper As New DatabaseHelper()
    Private selectedDecisionID As Integer = 0
    
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' تحميل الأسماء في ListBox
        LoadNamesInListBox()
        
        ' تحميل جميع السجلات
        LoadAllRecords()
        
        ' إعداد الفورم
        ConfigureForm()
    End Sub
    
    ' إعداد الفورم
    Private Sub ConfigureForm()
        Me.Text = "نظام إدارة القرارات"
        Me.RightToLeft = RightToLeft.Yes
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Size = New Size(1200, 700)
    End Sub
    
    ' تحميل الأسماء في ListBox
    Private Sub LoadNamesInListBox()
        Try
            Dim dt As DataTable = dbHelper.GetDistinctNames()
            ListBoxNames.Items.Clear()
            
            For Each row As DataRow In dt.Rows
                ListBoxNames.Items.Add(row("BeneficiaryName").ToString())
            Next
        Catch ex As Exception
            MsgBox("خطأ في تحميل الأسماء: " & ex.Message)
        End Try
    End Sub
    
    ' عند اختيار اسم من ListBox - جلب جميع سجلات الاسم
    Private Sub ListBoxNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxNames.SelectedIndexChanged
        If ListBoxNames.SelectedIndex >= 0 Then
            Dim selectedName As String = ListBoxNames.SelectedItem.ToString()
            LoadRecordsByName(selectedName)
        End If
    End Sub
    
    ' تحميل سجلات اسم معين
    Private Sub LoadRecordsByName(beneficiaryName As String)
        Try
            Dim dt As DataTable = dbHelper.GetRecordsByName(beneficiaryName)
            DataGridViewRecords.DataSource = dt
            FormatDataGridView()
            LabelRecordCount.Text = "عدد السجلات: " & dt.Rows.Count.ToString()
        Catch ex As Exception
            MsgBox("خطأ في جلب السجلات: " & ex.Message)
        End Try
    End Sub
    
    ' تحميل جميع السجلات
    Private Sub LoadAllRecords()
        Try
            Dim dt As DataTable = dbHelper.GetAllRecords()
            DataGridViewRecords.DataSource = dt
            FormatDataGridView()
            LabelRecordCount.Text = "إجمالي السجلات: " & dt.Rows.Count.ToString()
        Catch ex As Exception
            MsgBox("خطأ في جلب السجلات: " & ex.Message)
        End Try
    End Sub
    
    ' تنسيق DataGridView
    Private Sub FormatDataGridView()
        With DataGridViewRecords
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            .BackgroundColor = Color.White
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        End With
    End Sub
    
    ' البحث عند كتابة النص في TextBox
    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        If TextBoxSearch.Text.Length > 0 Then
            Dim dt As DataTable = dbHelper.SearchByName(TextBoxSearch.Text)
            DataGridViewRecords.DataSource = dt
            FormatDataGridView()
            LabelRecordCount.Text = "نتائج البحث: " & dt.Rows.Count.ToString()
        Else
            LoadAllRecords()
        End If
    End Sub
    
    ' زر البحث
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        If TextBoxSearch.Text.Length > 0 Then
            Dim dt As DataTable = dbHelper.SearchByName(TextBoxSearch.Text)
            DataGridViewRecords.DataSource = dt
            FormatDataGridView()
            LabelRecordCount.Text = "نتائج البحث: " & dt.Rows.Count.ToString()
        Else
            MsgBox("الرجاء إدخال اسم للبحث", MsgBoxStyle.Information)
        End If
    End Sub
    
    ' زر مسح البحث
    Private Sub ButtonClearSearch_Click(sender As Object, e As EventArgs) Handles ButtonClearSearch.Click
        TextBoxSearch.Clear()
        LoadAllRecords()
        ListBoxNames.ClearSelected()
    End Sub
    
    ' عند اختيار سجل من DataGridView
    Private Sub DataGridViewRecords_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewRecords.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewRecords.Rows(e.RowIndex)
            selectedDecisionID = CInt(row.Cells("DecisionID").Value)
            
            ' ملء التحكمات بالبيانات
            FillFormWithData(row)
        End If
    End Sub
    
    ' ملء الفورم بالبيانات
    Private Sub FillFormWithData(row As DataGridViewRow)
        TextBoxDecisionNumber.Text = row.Cells("DecisionNumber").Value.ToString()
        DateTimePickerDecisionDate.Value = CDate(row.Cells("DecisionDate").Value)
        TextBoxYear.Text = row.Cells("Year").Value.ToString()
        TextBoxStatus.Text = row.Cells("Status").Value.ToString()
        TextBoxChapter.Text = row.Cells("Chapter").Value.ToString()
        TextBoxDescription.Text = row.Cells("Description").Value.ToString()
        TextBoxBeneficiaryName.Text = row.Cells("BeneficiaryName").Value.ToString()
        TextBoxDetails.Text = row.Cells("Details").Value.ToString()
        TextBoxAmount.Text = row.Cells("Amount").Value.ToString()
        TextBoxNotes.Text = row.Cells("Notes").Value.ToString()
    End Sub
    
    ' زر إضافة سجل جديد
    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        If ValidateForm() Then
            Dim success As Boolean = dbHelper.InsertDecision(
                TextBoxDecisionNumber.Text,
                DateTimePickerDecisionDate.Value,
                CInt(TextBoxYear.Text),
                TextBoxStatus.Text,
                TextBoxChapter.Text,
                TextBoxDescription.Text,
                TextBoxBeneficiaryName.Text,
                TextBoxDetails.Text,
                CDec(TextBoxAmount.Text),
                TextBoxNotes.Text
            )
            
            If success Then
                ClearForm()
                LoadAllRecords()
                LoadNamesInListBox()
            End If
        End If
    End Sub
    
    ' زر تحديث السجل
    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        If selectedDecisionID = 0 Then
            MsgBox("الرجاء اختيار سجل من الجدول أولاً", MsgBoxStyle.Information)
            Return
        End If
        
        If ValidateForm() Then
            Dim success As Boolean = dbHelper.UpdateDecision(
                selectedDecisionID,
                TextBoxDecisionNumber.Text,
                DateTimePickerDecisionDate.Value,
                CInt(TextBoxYear.Text),
                TextBoxStatus.Text,
                TextBoxChapter.Text,
                TextBoxDescription.Text,
                TextBoxBeneficiaryName.Text,
                TextBoxDetails.Text,
                CDec(TextBoxAmount.Text),
                TextBoxNotes.Text
            )
            
            If success Then
                ClearForm()
                LoadAllRecords()
                LoadNamesInListBox()
                selectedDecisionID = 0
            End If
        End If
    End Sub
    
    ' زر حذف السجل
    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If selectedDecisionID = 0 Then
            MsgBox("الرجاء اختيار سجل من الجدول أولاً", MsgBoxStyle.Information)
            Return
        End If
        
        Dim success As Boolean = dbHelper.DeleteDecision(selectedDecisionID)
        
        If success Then
            ClearForm()
            LoadAllRecords()
            LoadNamesInListBox()
            selectedDecisionID = 0
        End If
    End Sub
    
    ' زر مسح النموذج
    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        ClearForm()
        selectedDecisionID = 0
    End Sub
    
    ' تنظيف النموذج
    Private Sub ClearForm()
        TextBoxDecisionNumber.Clear()
        DateTimePickerDecisionDate.Value = Today
        TextBoxYear.Clear()
        TextBoxStatus.Clear()
        TextBoxChapter.Clear()
        TextBoxDescription.Clear()
        TextBoxBeneficiaryName.Clear()
        TextBoxDetails.Clear()
        TextBoxAmount.Clear()
        TextBoxNotes.Clear()
    End Sub
    
    ' التحقق من صحة البيانات
    Private Function ValidateForm() As Boolean
        If String.IsNullOrWhiteSpace(TextBoxDecisionNumber.Text) Then
            MsgBox("الرجاء إدخال رقم القرار", MsgBoxStyle.Warning)
            Return False
        End If
        
        If String.IsNullOrWhiteSpace(TextBoxYear.Text) Then
            MsgBox("الرجاء إدخال السنة", MsgBoxStyle.Warning)
            Return False
        End If
        
        If Not Integer.TryParse(TextBoxYear.Text, Nothing) Then
            MsgBox("السنة يجب أن تكون رقماً", MsgBoxStyle.Warning)
            Return False
        End If
        
        If String.IsNullOrWhiteSpace(TextBoxBeneficiaryName.Text) Then
            MsgBox("الرجاء إدخال اسم المستفيد", MsgBoxStyle.Warning)
            Return False
        End If
        
        If String.IsNullOrWhiteSpace(TextBoxAmount.Text) Then
            MsgBox("الرجاء إدخال المبلغ", MsgBoxStyle.Warning)
            Return False
        End If
        
        If Not Decimal.TryParse(TextBoxAmount.Text, Nothing) Then
            MsgBox("المبلغ يجب أن يكون رقماً", MsgBoxStyle.Warning)
            Return False
        End If
        
        Return True
    End Function
End Class
