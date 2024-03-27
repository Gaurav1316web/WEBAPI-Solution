Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

'created by --> Vipin
'createddate --> 25/05/2011
'modifiedby --> Vipin
'Modified date -->03/06/2011
'Tables Used --> TSPL_User_Group_Master
Public Class FrmUserGroupMaster
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim str As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003130
        'MyBase.SetUserMgmt(clsUserMgtCode.userGroupMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            menuImport.Enabled = True
            menuExport.Enabled = True
        Else
            menuImport.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmUserGroupMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()

        End If
    End Sub
    'Main Form Load
    Private Sub FrmUserGroupMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fndgroup.txtValue, txtname)
        'fndgroup.ConnectionString = connectSql.SqlCon()
        'fndgroup.Query = " select group_Code As [Group Code],group_desc  as [Group Name] from TSPL_User_Group_Master "
        'fndgroup.ValueToSelect = "Group Code"
        'fndgroup.Caption = "User Group Master"
        'fndgroup.ValueToSelect1 = "Group Name"
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        ToolTipnew.SetToolTip(btnnew, "New")
        '  fndgroup.txtValue.MaxLength = 12
        'AddHandler fndgroup.ValueChanged, AddressOf text_changed
        'AddHandler fndgroup.txtValue.KeyPress, AddressOf key_press
        btndelete.Enabled = False
        btnsave.Enabled = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'it will fill controls in screen according to finder's value 
    Sub text_changed()
        Try
            Dim str As String = "select group_code,group_desc  from TSPL_User_Group_Master where group_code = '" + fndgroup.Value + "'"
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable(str)
            Dim strvalue As String = ""
            Dim strname As String = ""
            For Each row As DataRow In dr.Rows
                strvalue = row(0).ToString()
                strname = row(1).ToString()
            Next
            If (strvalue <> "") Then
                txtname.Text = strname
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            Else
                txtname.Text = ""
                btnsave.Text = "Save"
                btnsave.Enabled = True
                btndelete.Enabled = False
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndgroup.txtValue.CharacterCasing = CharacterCasing.Upper
        'If (e.KeyChar = Chr(39)) Then
        '    e.Handled = True
        'End If
    End Sub
    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_User_Group_Master where Group_Code='" & fndgroup.Value & "'")
                If ChkNewEntry = 0 Then
                    fndgroup.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.UserGroupMaster, "", "")
                    If clsCommon.myLen(fndgroup.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_UserGroupMaster_insert", New SqlParameter("@groupcode", fndgroup.Value.ToString()), New SqlParameter("@groupname", txtname.Text.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))

            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Funtion for updation  of data
    Public Sub funupdate()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_UserGroupMaster_update", New SqlParameter("@groupcode", fndgroup.Value.ToString()), New SqlParameter("@groupname", txtname.Text.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            connectSql.RunSp("sp_UserGroupMaster_delete", New SqlParameter("@groupcode", fndgroup.Value))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        fndgroup.MyReadOnly = False
        fndgroup.Value = ""
        txtname.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    'Validation on save button click and calling funinsert,funupdate
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndgroup.Value = "" Then
                myMessages.blankValue(Me, "User Group Code", Me.Text)
                fndgroup.Focus()
            ElseIf txtname.Text = "" Then
                myMessages.blankValue(Me, "User Group Name", Me.Text)
                txtname.Focus()
            Else

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.userGroupMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Delete funtion call on delete button
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndgroup.Value = "" Then
            myMessages.blankValue(Me, "Group Code", Me.Text)
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub
    'closing of current window form
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    'For Export functionality 
    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        Dim str As String
        str = "select group_code As [Group Code],group_desc as [Group Name] from tspl_User_Group_Master"

        ListImpExpColumnsMandatory = New List(Of String)({"Group Code", "Group Name"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Group Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    'For Import functionality 
    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Group Code", "Group Name") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strcode As String = grow.Cells(0).Value.ToString()
                    If strcode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Check The lenght of Group Code", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim strdes As String = grow.Cells(1).Value.ToString()
                    If strdes.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Check the lenght of Group Name", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strcode) Then
                        common.clsCommon.MyMessageBoxShow(Me, " Group Code can not be blank", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strdes) Then
                        common.clsCommon.MyMessageBoxShow(Me, " Group Name can not be blank", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim sql1 As String = "select count(*) from tspl_User_Group_Master where group_code='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "sp_UserGroupMaster_insert", New SqlParameter("@groupcode", strcode), New SqlParameter("@groupname", strdes), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@comp_code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_UserGroupMaster_update", New SqlParameter("@groupcode", strcode), New SqlParameter("@groupname", strdes), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@comp_code", companyCode))
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    'For closing current screen by menu strip Close
    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "USERGRP-MAST"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub fndgroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndgroup._MYValidating
        Dim qry As String

        If isButtonClicked Then
            'qry = " select group_Code As [Group Code],group_desc  as [Group Name] from TSPL_User_Group_Master "
            'fndgroup.Value = clsCommon.ShowSelectForm("fmGroup", qry, "Group Code", "", fndgroup.Value, "", isButtonClicked)
            fndgroup.Value = clsUserGroupMaster.getFinder("", fndgroup.Value, isButtonClicked)
            txtname.Text = clsDBFuncationality.getSingleValue("select group_desc from TSPL_User_Group_Master where Group_desc = '" + fndgroup.Value + "'")

            text_changed()
            ' fndUser_NameLeave()
        ElseIf fndgroup.MyReadOnly OrElse fndgroup.Value IsNot Nothing Then
            qry = "Select * from TSPL_User_Group_Master where group_Code ='" + fndgroup.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                txtname.Text = ""
                btnsave.Text = "Save"
            Else
                text_changed()
                fndgroup.MyReadOnly = True
            End If

        End If

    End Sub

    Private Sub fndgroup__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndgroup._MYNavigator
        Dim qst As String = "select group_Code As [Group Code],group_desc  as [Group Name] from TSPL_User_Group_Master  where  2=2"
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and group_Code in (select min(group_Code) from TSPL_User_Group_Master where group_Code>'" + fndgroup.Value + "' ) "
            Case NavigatorType.First
                qst += "and group_Code in (select MIN(group_Code) from TSPL_User_Group_Master )"
            Case NavigatorType.Last
                qst += "and group_Code in (select Max(group_Code) from TSPL_User_Group_Master  )"
            Case NavigatorType.Previous
                qst += "and group_Code in (select max(group_Code) from TSPL_User_Group_Master where group_Code<'" + fndgroup.Value + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndgroup.Value = clsCommon.myCstr(dt.Rows(0)("Group Code"))
            txtname.Text = clsCommon.myCstr(dt.Rows(0)("Group Name"))
        End If
        text_changed()
        'fndUser_NameLeave()
    End Sub
End Class
