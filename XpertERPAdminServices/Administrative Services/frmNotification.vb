Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Public Class frmNotification
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isNewEntry As Boolean = True
#End Region
    Private Sub frmNotification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage1

        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)
        'coll.Add("Document_No", "varchar(30) NOT NULL Primary key")
        'coll.Add("Document_Date", "DateTime not Null")
        'coll.Add("Subject", "nvarchar(100)")
        'coll.Add("Description", "nvarchar(MAX)")
        'coll.Add("Attachment_Count", "Int")
        'coll.Add("Start_Date", "Date not NULL")
        'coll.Add("End_Date", "Date null")
        'coll.Add("Status", "integer null")
        'coll.Add("Created_By", "VARCHAR(12) not NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        'coll.Add("Created_Date", "DateTime not NULL")
        'coll.Add("Modify_By", "VARCHAR(12) not NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        'coll.Add("Modify_Date", "DateTime not NULL")
        'coll.Add("Post_By", "VARCHAR(12) NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        'coll.Add("Post_Date", "DateTime NULL")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_NOTIFICATIONS", coll, "", False, False, "", "Document_No", "Document_Date")

        'coll = New Dictionary(Of String, String)
        'coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        'coll.Add("SNO", "integer NUll")
        'coll.Add("Document_No", "VARCHAR(30)  NULL REFERENCES TSPL_NOTIFICATIONS(Document_No) ")
        'coll.Add("Login_Type", "varchar(12) null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_NOTIFICATIONS_USER_TYPE", coll, "", False, True, "TSPL_Notifications", "Document_No", "")
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Value = clsCommon.GETSERVERDATE()
        UcAttachment1.Form_ID = MyBase.Form_ID
        Addnew()
    End Sub

    Function AllowToSave() As Boolean
        UcAttachment1.AllowToSave()
        Return True
    End Function
    Public Sub Addnew()
        UcAttachment1.BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Checked = False
        txtEndDate.Value = txtDate.Value
        txtCode.Focus()
        txtUserType.arrDispalyMember = Nothing
        txtUserType.arrValueMember = Nothing
        txtSubject.Text = ""
        txtDescription.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = True
        UcAttachment1.BlankAllControls()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Addnew()
            txtCode.MyReadOnly = True
            Dim obj As New ClsNotification()
            obj = ClsNotification.GetData(strCode, NavType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDate.Value = obj.Document_Date
                txtStartDate.Value = obj.Start_Date
                'If clsCommon.myLen(obj.End_Date) > 0 Then
                '    txtEndDate.Value = obj.End_Date
                'Else
                '    txtEndDate.Value = Nothing
                'End If
                If obj.End_Date.HasValue Then
                    txtEndDate.Value = obj.End_Date
                    txtEndDate.Checked = True
                End If
                txtSubject.Text = obj.Subject
                txtDescription.Text = obj.Description
                Dim arrUserType As New ArrayList

                For i As Integer = 0 To obj.Arr.Count - 1
                    arrUserType.Add(obj.Arr(i).Login_Type)
                Next
                txtUserType.arrValueMember = arrUserType
                If clsCommon.myCdbl(ERPTransactionStatus.Approved) = clsCommon.myCdbl(obj.Status) Then
                    UsLock1.Status = obj.Status
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                ElseIf ERPTransactionStatus.Pending = obj.Status Then
                    UsLock1.Status = obj.Status
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
                UcAttachment1.LoadData(obj.Code)
            Else
                Addnew()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_NOTIFICATIONS where Document_No='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Document_No as Code,Document_Date as Date,Start_Date As 'Start Date',End_Date As 'End Date',Subject,Status from TSPL_NOTIFICATIONS"
            txtCode.Value = clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "TSPL_NOTIFICATIONS.Document_No ", isButtonClicked, "")
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_NOTIFICATIONS where Document_No='" + txtCode.Value + "'"

            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtEndDate_CheckedChanged(sender As Object, e As EventArgs) Handles txtEndDate.CheckedChanged
        If txtEndDate.Checked Then
            txtEndDate.Value = clsCommon.GETSERVERDATE()
        Else
            txtEndDate.Value = Nothing
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New ClsNotification()
                obj.Code = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.Start_Date = txtStartDate.Value
                If txtEndDate.Checked Then
                    obj.End_Date = txtEndDate.Value
                End If
                obj.Subject = txtSubject.Text
                obj.Description = txtDescription.Text
                Dim arrUserType As New List(Of String)
                If txtUserType.arrValueMember IsNot Nothing Then
                    For i As Integer = 0 To txtUserType.arrValueMember.Count - 1
                        arrUserType.Add(txtUserType.arrValueMember(i))
                    Next
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select atleast one User type")
                    Exit Sub
                End If

                obj.Arr = New List(Of clsNotificationDetails)
                For i As Integer = 0 To arrUserType.count - 1
                    Dim objtr As New clsNotificationDetails
                    objtr.Login_Type = arrUserType(i)
                    obj.Arr.Add(objtr)
                Next
                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Code)
                    Dim AttachmentCount As Integer = clsDBFuncationality.getSingleValue("SELECT COUNT(1) FROM TSPL_ATTACHMENTS WHERE TransactionId='" & obj.Code & "'")
                    Dim sql As String = "UPDATE TSPL_NOTIFICATIONS SET Attachment_Count = '" & AttachmentCount & "' where Document_No = '" & obj.Code & "'"
                    clsDBFuncationality.ExecuteNonQuery(sql)
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            PostData(txtCode.Value)
        Else
        End If
    End Sub

    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                ClsNotification.PostData(clsCommon.myCstr(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (ClsNotification.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtUserType__My_Click(sender As Object, e As EventArgs) Handles txtUserType._My_Click
        Dim qry As String = "select  Distinct(Login_Type) AS UserType,User_Code as Code,User_Name as Name from TSPL_USER_MASTER where Login_Type is not null"
        txtUserType.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "UserType", "UserType", txtUserType.arrValueMember, txtUserType.arrDispalyMember)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            Dim str As String = ""
            str = " SELECT TSPL_NOTIFICATIONS.Document_No AS Code,CONVERT(DATE, Document_Date, 103) AS Date,Subject,Description,Start_Date AS [Start Date],End_Date AS [End Date],MAX(CASE WHEN SNO = 1 THEN Login_Type ELSE NULL END) AS Login_Type1,MAX(CASE WHEN SNO = 2 THEN Login_Type ELSE NULL END) AS Login_Type2,
                    MAX(CASE WHEN SNO = 3 THEN Login_Type ELSE NULL END) AS Login_Type3,MAX(CASE WHEN SNO = 4 THEN Login_Type ELSE NULL END) AS Login_Type4,MAX(CASE WHEN SNO = 5 THEN Login_Type ELSE NULL END) AS Login_Type5 FROM TSPL_NOTIFICATIONS LEFT JOIN TSPL_NOTIFICATIONS_USER_TYPE ON TSPL_NOTIFICATIONS_USER_TYPE.Document_No = TSPL_NOTIFICATIONS.Document_No  "
            Dim whrCls As String = " GROUP BY TSPL_NOTIFICATIONS.Document_No,Document_Date,Subject,Description,Start_Date,End_Date"
            'ListImpExpColumnsMandatory = New List(Of String)({"Route Code", "Distributor Code"})
            transportSql.ExporttoExcel(str, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, "Code", "Date", "Subject", "Description", "Start Date", "End Date", "Login_Type1", "Login_Type2", "Login_Type3", "Login_Type4", "Login_Type5") Then
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv)
    End Sub
End Class