Imports System.Data.SqlClient
Imports common
Public Class frmNotification
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isNewEntry As Boolean = True
    Private WithEvents RadDropDownList1 As New RadDropDownList()

#End Region
    Private Sub frmNotification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage1
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Value = txtStartDate.Value
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.RunServiceForUploadFolder = True
        Addnew()
        SetMaxLength()
        'PopulateComboBox()
    End Sub
    'Private Sub PopulateComboBox()
    '    cmbType.Items.Clear()
    '    cmbType.Items.Add("Saras Sale")
    '    cmbType.Items.Add("Saras Pro")

    '    cmbType.SelectedIndex = 0 ' Default selection for m²
    'End Sub

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
        txtStartDate.Value = txtDate.Value
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
                'If obj.Type = 0 Then
                '    cmbType.Text = "Saras Sale"
                'ElseIf obj.Type = 1 Then
                '    cmbType.Text = "Saras Pro"
                'End If
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
        'If txtCode.MyReadOnly OrElse isButtonClicked Then
        Dim whrClas As String = ""
        Dim qry As String = "select Document_No as Code,Document_Date as Date,Start_Date As 'Start Date',End_Date As 'End Date',Subject,Status,Type from TSPL_NOTIFICATIONS"
        LoadData(clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "TSPL_NOTIFICATIONS.Document_No  ", isButtonClicked), NavigatorType.Current)
        ' txtCode.Value = clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "TSPL_NOTIFICATIONS.Document_No ", isButtonClicked, "")
        'LoadData(txtCode.Value, NavigatorType.Current)
        ' End If
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

                Dim typeName As String=Nothing
                Dim arrUserType As New List(Of String)
                If txtUserType.arrValueMember IsNot Nothing Then
                    For i As Integer = 0 To txtUserType.arrValueMember.Count - 1
                        arrUserType.Add(txtUserType.arrValueMember(i))
                        If clsCommon.myLen(typeName) > 0 Then
                            typeName += "," + clsCommon.myCstr(txtUserType.arrValueMember(i))
                        Else
                            typeName = clsCommon.myCstr(txtUserType.arrValueMember(i))
                        End If
                    Next
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select atleast one User type", Me.Text)
                    Exit Sub
                End If

                obj.Arr = New List(Of clsNotificationDetails)
                    For i As Integer = 0 To arrUserType.Count - 1
                    Dim objtr As New clsNotificationDetails

                    If arrUserType(i).Contains("Admin") Then
                        objtr.Login_Type = "A"
                    ElseIf arrUserType(i).Contains("BMC Transporter") Then
                        objtr.Login_Type = "B"
                    ElseIf arrUserType(i).Contains("MCC") Then
                        objtr.Login_Type = "M"
                    ElseIf arrUserType(i).Contains("Milk Producer") Then
                        objtr.Login_Type = "F"
                    ElseIf arrUserType(i).Contains("RP") Then
                        objtr.Login_Type = "R"
                    ElseIf arrUserType(i).Contains("VSP") Then
                        objtr.Login_Type = "V"
                    ElseIf arrUserType(i).Contains("Zone") Then
                        objtr.Login_Type = "Z"
                    ElseIf arrUserType(i).Contains("SuperUser") Then
                        objtr.Login_Type = "SuperUser"
                    ElseIf arrUserType(i).Contains("CNF") Then
                        objtr.Login_Type = "CNF"
                    End If
                    obj.Arr.Add(objtr)
                Next
                    Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        ClsNotification.SaveData(obj, isNewEntry, tran)
                        UcAttachment1.SaveData(obj.Code, False, tran)
                        Dim AttachmentCount As Integer = clsDBFuncationality.getSingleValue("SELECT COUNT(1) FROM TSPL_ATTACHMENTS WHERE TransactionId='" & obj.Code & "'", tran)
                        Dim sql As String = "UPDATE TSPL_NOTIFICATIONS SET Attachment_Count = '" & AttachmentCount & "' where Document_No = '" & obj.Code & "'"
                        clsDBFuncationality.ExecuteNonQuery(sql, tran)
                        tran.Commit()
                    Catch ex As Exception
                        tran.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
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
                    UcAttachment1.funDelete(txtCode.Value)
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
        Try
            'Dim qry As String = "SELECT DISTINCT Login_Type AS [Type] FROM TSPL_USER_MASTER WHERE (len(isnull(Login_Type,''))>0)"
            Dim qry As String = "SELECT DISTINCT 
COALESCE(
        CASE WHEN Login_type = 'SuperUser' THEN 'SuperUser' END,
        CASE WHEN Login_type = 'CNF' THEN 'CNF' END)
   AS [Code],
      COALESCE(CASE WHEN Login_type = 'SuperUser' THEN 'SuperUser' END,
        CASE WHEN Login_type = 'CNF' THEN 'CNF' END) AS [Name],
    'SARAS ORDER' AS Application
FROM 
    TSPL_USER_MASTER 
WHERE 
    LEN(ISNULL(Login_Type,'')) > 0

UNION ALL

SELECT DISTINCT
    COALESCE(
        CASE WHEN User_APP_Type = 'A' THEN 'A' END,
        CASE WHEN User_APP_Type = 'B' THEN 'B' END,
        CASE WHEN User_APP_Type = 'M' THEN 'M' END,
        CASE WHEN User_APP_Type = 'F' THEN 'F' END,
        CASE WHEN User_APP_Type = 'R' THEN 'R' END,
        CASE WHEN User_APP_Type = 'V' THEN 'V' END,
        CASE WHEN User_APP_Type = 'Z' THEN 'V' END
    ) AS [Code],
    COALESCE(
        CASE WHEN User_APP_Type = 'A' THEN 'Admin' END,
        CASE WHEN User_APP_Type = 'B' THEN 'BMC Transporter' END,
        CASE WHEN User_APP_Type = 'M' THEN 'MCC' END,
        CASE WHEN User_APP_Type = 'F' THEN 'Milk Producer' END,
        CASE WHEN User_APP_Type = 'R' THEN 'RP' END,
        CASE WHEN User_APP_Type = 'V' THEN 'VSP' END,
        CASE WHEN User_APP_Type = 'Z' THEN 'Zone' END
    ) AS [Name],
    'SARAS PRO' AS Application
FROM 
    TSPL_USER_MASTER 
WHERE 
    LEN(ISNULL(User_APP_Type,'')) > 0


"
            txtUserType.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeM", qry, "Name", "", txtUserType.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        'Dim qry As String = " SELECT DISTINCT Login_Type AS [User Type] FROM TSPL_USER_MASTER WHERE Login_Type IS NOT NULL AND Login_Type !=''"
        'txtUserType.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "User Type", "User Type", txtUserType.arrValueMember, txtUserType.arrDispalyMember)
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
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv)
    End Sub

    Private Sub SetMaxLength()
        txtCode.MyMaxLength = 30
        txtSubject.MaxLength = 200
        txtDescription.MaxLength = 5000
        'txtDescription.MaxLength = Integer.MaxValue
    End Sub


End Class