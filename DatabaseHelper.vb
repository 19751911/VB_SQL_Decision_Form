Imports System.Data
Imports System.Data.SqlClient

Public Class DatabaseHelper
    ' سلسلة الاتصال - عدّل اسم السيرفر وقاعدة البيانات حسب بيئتك
    Private Const ConnectionString As String = "Server=YOUR_SERVER_NAME;Database=DecisionsDB;Integrated Security=true;"
    
    ' فتح الاتصال
    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(ConnectionString)
    End Function
    
    ' الحصول على جميع الأسماء المميزة للقائمة المنسدلة
    Public Function GetDistinctNames() As DataTable
        Try
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_GetDistinctNames", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    
                    Dim adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    
                    Return dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في جلب الأسماء: " & ex.Message, MsgBoxStyle.Critical)
            Return New DataTable()
        End Try
    End Function
    
    ' البحث عن السجلات باسم معين
    Public Function GetRecordsByName(beneficiaryName As String) As DataTable
        Try
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_GetRecordsByName", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@BeneficiaryName", beneficiaryName)
                    
                    Dim adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    
                    Return dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في جلب السجلات: " & ex.Message, MsgBoxStyle.Critical)
            Return New DataTable()
        End Try
    End Function
    
    ' البحث بالاسم في TextBox
    Public Function SearchByName(searchText As String) As DataTable
        Try
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_SearchByName", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@BeneficiaryName", searchText)
                    
                    Dim adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    
                    Return dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في البحث: " & ex.Message, MsgBoxStyle.Critical)
            Return New DataTable()
        End Try
    End Function
    
    ' إدراج سجل جديد
    Public Function InsertDecision(decisionNumber As String, decisionDate As Date, year As Integer,
                                   status As String, chapter As String, description As String,
                                   beneficiaryName As String, details As String, amount As Decimal,
                                   notes As String) As Boolean
        Try
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_InsertDecision", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    
                    cmd.Parameters.AddWithValue("@DecisionNumber", decisionNumber)
                    cmd.Parameters.AddWithValue("@DecisionDate", decisionDate)
                    cmd.Parameters.AddWithValue("@Year", year)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@Chapter", chapter)
                    cmd.Parameters.AddWithValue("@Description", description)
                    cmd.Parameters.AddWithValue("@BeneficiaryName", beneficiaryName)
                    cmd.Parameters.AddWithValue("@Details", details)
                    cmd.Parameters.AddWithValue("@Amount", amount)
                    cmd.Parameters.AddWithValue("@Notes", notes)
                    
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    
                    MsgBox("تم إدراج السجل بنجاح!", MsgBoxStyle.Information)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في الإدراج: " & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function
    
    ' تحديث سجل
    Public Function UpdateDecision(decisionID As Integer, decisionNumber As String, decisionDate As Date,
                                   year As Integer, status As String, chapter As String,
                                   description As String, beneficiaryName As String, details As String,
                                   amount As Decimal, notes As String) As Boolean
        Try
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_UpdateDecision", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    
                    cmd.Parameters.AddWithValue("@DecisionID", decisionID)
                    cmd.Parameters.AddWithValue("@DecisionNumber", decisionNumber)
                    cmd.Parameters.AddWithValue("@DecisionDate", decisionDate)
                    cmd.Parameters.AddWithValue("@Year", year)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@Chapter", chapter)
                    cmd.Parameters.AddWithValue("@Description", description)
                    cmd.Parameters.AddWithValue("@BeneficiaryName", beneficiaryName)
                    cmd.Parameters.AddWithValue("@Details", details)
                    cmd.Parameters.AddWithValue("@Amount", amount)
                    cmd.Parameters.AddWithValue("@Notes", notes)
                    
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    
                    MsgBox("تم تحديث السجل بنجاح!", MsgBoxStyle.Information)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في التحديث: " & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function
    
    ' حذف سجل
    Public Function DeleteDecision(decisionID As Integer) As Boolean
        Try
            Dim result As DialogResult = MsgBox("هل أنت متأكد من حذف هذا السجل؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
            
            If result = DialogResult.No Then
                Return False
            End If
            
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_DeleteDecision", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@DecisionID", decisionID)
                    
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    
                    MsgBox("تم حذف السجل بنجاح!", MsgBoxStyle.Information)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في الحذف: " & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function
    
    ' الحصول على جميع السجلات
    Public Function GetAllRecords() As DataTable
        Try
            Using connection As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("SELECT * FROM Decisions ORDER BY DecisionDate DESC", connection)
                    Dim adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    
                    Return dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("خطأ في جلب البيانات: " & ex.Message, MsgBoxStyle.Critical)
            Return New DataTable()
        End Try
    End Function
End Class
