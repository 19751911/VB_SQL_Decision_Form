<System.Diagnostics.DebuggerNonUserCode()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        
        ' Panel للبحث
        Dim panelSearch As New Panel()
        panelSearch.Dock = DockStyle.Top
        panelSearch.Height = 50
        panelSearch.BackColor = Color.LightBlue
        
        Dim labelSearch As New Label()
        labelSearch.Text = "بحث باسم المستفيد:"
        labelSearch.AutoSize = True
        labelSearch.Location = New Point(500, 15)
        
        Me.TextBoxSearch = New TextBox()
        Me.TextBoxSearch.Location = New Point(120, 12)
        Me.TextBoxSearch.Size = New Size(370, 25)
        
        Me.ButtonSearch = New Button()
        Me.ButtonSearch.Text = "بحث"
        Me.ButtonSearch.Location = New Point(40, 12)
        Me.ButtonSearch.Size = New Size(75, 25)
        
        Me.ButtonClearSearch = New Button()
        Me.ButtonClearSearch.Text = "مسح البحث"
        Me.ButtonClearSearch.Location = New Point(900, 12)
        Me.ButtonClearSearch.Size = New Size(75, 25)
        
        panelSearch.Controls.Add(labelSearch)
        panelSearch.Controls.Add(Me.TextBoxSearch)
        panelSearch.Controls.Add(Me.ButtonSearch)
        panelSearch.Controls.Add(Me.ButtonClearSearch)
        
        ' Panel للقائمة والبيانات
        Dim panelMain As New Panel()
        panelMain.Dock = DockStyle.Fill
        
        ' ListBox للأسماء
        Dim labelNames As New Label()
        labelNames.Text = "الأسماء المسجلة:"
        labelNames.Location = New Point(950, 60)
        labelNames.AutoSize = True
        
        Me.ListBoxNames = New ListBox()
        Me.ListBoxNames.Location = New Point(850, 85)
        Me.ListBoxNames.Size = New Size(150, 400)
        
        ' DataGridView
        Me.DataGridViewRecords = New DataGridView()
        Me.DataGridViewRecords.Location = New Point(10, 60)
        Me.DataGridViewRecords.Size = New Size(835, 400)
        Me.DataGridViewRecords.AllowUserToAddRows = False
        
        ' Label عدد السجلات
        Me.LabelRecordCount = New Label()
        Me.LabelRecordCount.Location = New Point(10, 465)
        Me.LabelRecordCount.AutoSize = True
        Me.LabelRecordCount.Text = "عدد السجلات: 0"
        
        ' Panel النموذج
        Dim panelForm As New Panel()
        panelForm.Dock = DockStyle.Bottom
        panelForm.Height = 200
        panelForm.BackColor = Color.WhiteSmoke
        
        ' إنشاء التحكمات
        Dim controls As New List(Of Tuple(Of String, TextBox))()
        
        ' رقم القرار
        Dim label1 As New Label()
        label1.Text = "رقم القرار:"
        label1.Location = New Point(480, 10)
        Me.TextBoxDecisionNumber = New TextBox()
        Me.TextBoxDecisionNumber.Location = New Point(360, 10)
        Me.TextBoxDecisionNumber.Size = New Size(110, 20)
        panelForm.Controls.Add(label1)
        panelForm.Controls.Add(Me.TextBoxDecisionNumber)
        
        ' التاريخ
        Dim label2 As New Label()
        label2.Text = "التاريخ:"
        label2.Location = New Point(730, 10)
        Me.DateTimePickerDecisionDate = New DateTimePicker()
        Me.DateTimePickerDecisionDate.Location = New Point(640, 10)
        Me.DateTimePickerDecisionDate.Size = New Size(80, 20)
        panelForm.Controls.Add(label2)
        panelForm.Controls.Add(Me.DateTimePickerDecisionDate)
        
        ' السنة
        Dim label3 As New Label()
        label3.Text = "السنة:"
        label3.Location = New Point(1040, 10)
        Me.TextBoxYear = New TextBox()
        Me.TextBoxYear.Location = New Point(920, 10)
        Me.TextBoxYear.Size = New Size(110, 20)
        panelForm.Controls.Add(label3)
        panelForm.Controls.Add(Me.TextBoxYear)
        
        ' الحالة
        Dim label4 As New Label()
        label4.Text = "الحالة:"
        label4.Location = New Point(480, 40)
        Me.TextBoxStatus = New TextBox()
        Me.TextBoxStatus.Location = New Point(360, 40)
        Me.TextBoxStatus.Size = New Size(110, 20)
        panelForm.Controls.Add(label4)
        panelForm.Controls.Add(Me.TextBoxStatus)
        
        ' الباب
        Dim label5 As New Label()
        label5.Text = "الباب:"
        label5.Location = New Point(730, 40)
        Me.TextBoxChapter = New TextBox()
        Me.TextBoxChapter.Location = New Point(640, 40)
        Me.TextBoxChapter.Size = New Size(80, 20)
        panelForm.Controls.Add(label5)
        panelForm.Controls.Add(Me.TextBoxChapter)
        
        ' الوصف
        Dim label6 As New Label()
        label6.Text = "الوصف:"
        label6.Location = New Point(1040, 40)
        Me.TextBoxDescription = New TextBox()
        Me.TextBoxDescription.Location = New Point(920, 40)
        Me.TextBoxDescription.Size = New Size(70, 20)
        panelForm.Controls.Add(label6)
        panelForm.Controls.Add(Me.TextBoxDescription)
        
        ' اسم المستفيد
        Dim label7 As New Label()
        label7.Text = "اسم المستفيد:"
        label7.Location = New Point(480, 70)
        Me.TextBoxBeneficiaryName = New TextBox()
        Me.TextBoxBeneficiaryName.Location = New Point(360, 70)
        Me.TextBoxBeneficiaryName.Size = New Size(110, 20)
        panelForm.Controls.Add(label7)
        panelForm.Controls.Add(Me.TextBoxBeneficiaryName)
        
        ' التفاصيل
        Dim label8 As New Label()
        label8.Text = "التفاصيل:"
        label8.Location = New Point(730, 70)
        Me.TextBoxDetails = New TextBox()
        Me.TextBoxDetails.Location = New Point(640, 70)
        Me.TextBoxDetails.Size = New Size(80, 20)
        panelForm.Controls.Add(label8)
        panelForm.Controls.Add(Me.TextBoxDetails)
        
        ' المبلغ
        Dim label9 As New Label()
        label9.Text = "المبلغ:"
        label9.Location = New Point(1040, 70)
        Me.TextBoxAmount = New TextBox()
        Me.TextBoxAmount.Location = New Point(920, 70)
        Me.TextBoxAmount.Size = New Size(70, 20)
        panelForm.Controls.Add(label9)
        panelForm.Controls.Add(Me.TextBoxAmount)
        
        ' الملاحظات
        Dim label10 As New Label()
        label10.Text = "الملاحظات:"
        label10.Location = New Point(480, 100)
        Me.TextBoxNotes = New TextBox()
        Me.TextBoxNotes.Location = New Point(360, 100)
        Me.TextBoxNotes.Size = New Size(110, 20)
        panelForm.Controls.Add(label10)
        panelForm.Controls.Add(Me.TextBoxNotes)
        
        ' الأزرار
        Me.ButtonAdd = New Button()
        Me.ButtonAdd.Text = "إضافة"
        Me.ButtonAdd.Location = New Point(10, 140)
        Me.ButtonAdd.Size = New Size(75, 25)
        panelForm.Controls.Add(Me.ButtonAdd)
        
        Me.ButtonUpdate = New Button()
        Me.ButtonUpdate.Text = "تحديث"
        Me.ButtonUpdate.Location = New Point(90, 140)
        Me.ButtonUpdate.Size = New Size(75, 25)
        panelForm.Controls.Add(Me.ButtonUpdate)
        
        Me.ButtonDelete = New Button()
        Me.ButtonDelete.Text = "حذف"
        Me.ButtonDelete.Location = New Point(170, 140)
        Me.ButtonDelete.Size = New Size(75, 25)
        panelForm.Controls.Add(Me.ButtonDelete)
        
        Me.ButtonClear = New Button()
        Me.ButtonClear.Text = "مسح"
        Me.ButtonClear.Location = New Point(250, 140)
        Me.ButtonClear.Size = New Size(75, 25)
        panelForm.Controls.Add(Me.ButtonClear)
        
        ' إضافة العناصر إلى الفورم
        Me.Controls.Add(panelMain)
        Me.Controls.Add(panelSearch)
        
        panelMain.Controls.Add(Me.ListBoxNames)
        panelMain.Controls.Add(labelNames)
        panelMain.Controls.Add(Me.DataGridViewRecords)
        panelMain.Controls.Add(Me.LabelRecordCount)
        panelMain.Controls.Add(panelForm)
        
        ' إعدادات الفورم
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1010, 670)
        Me.Name = "MainForm"
        Me.Text = "نظام إدارة القرارات"
        Me.RightToLeft = RightToLeft.Yes
        
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents TextBoxSearch As TextBox
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents ButtonClearSearch As Button
    Friend WithEvents ListBoxNames As ListBox
    Friend WithEvents DataGridViewRecords As DataGridView
    Friend WithEvents LabelRecordCount As Label
    
    Friend WithEvents TextBoxDecisionNumber As TextBox
    Friend WithEvents DateTimePickerDecisionDate As DateTimePicker
    Friend WithEvents TextBoxYear As TextBox
    Friend WithEvents TextBoxStatus As TextBox
    Friend WithEvents TextBoxChapter As TextBox
    Friend WithEvents TextBoxDescription As TextBox
    Friend WithEvents TextBoxBeneficiaryName As TextBox
    Friend WithEvents TextBoxDetails As TextBox
    Friend WithEvents TextBoxAmount As TextBox
    Friend WithEvents TextBoxNotes As TextBox
    
    Friend WithEvents ButtonAdd As Button
    Friend WithEvents ButtonUpdate As Button
    Friend WithEvents ButtonDelete As Button
    Friend WithEvents ButtonClear As Button
End Class
