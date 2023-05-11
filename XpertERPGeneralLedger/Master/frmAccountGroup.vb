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

Public Class frmAccountGroup
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub frmAccountGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            Deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.accountGroup)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            accgrpim.Enabled = True
            accgpex.Enabled = True
        Else
            accgrpim.Enabled = False
            accgpex.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmAccountGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fndaccgp.txtValue, txtdes)
        'fndaccgp.txtValue.MaxLength = 12
        ToolTipaccgp.SetToolTip(btnnew, "New")
        'AddHandler fndaccgp.ValueChanged, AddressOf text_changed
        'AddHandler fndaccgp.txtValue.KeyPress, AddressOf key_press
        btnsave.Enabled = True
        btndelete.Enabled = False
        'fndaccgp.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndaccgp.ConnectionString = connectSql.SqlCon()
        'fndaccgp.Query = "select account_group_code as [Account Groups Code],account_group_desc as [Description] from tspl_account_groups"
        'fndaccgp.ValueToSelect = "Account Groups Code"
        'fndaccgp.Caption = "Account Groups"
        'fndaccgp.ValueToSelect1 = "Description"
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        SetLenght()
    End Sub

    Public Sub SetLenght()
        fndaccgp.MyMaxLength = 12
        txtdes.MaxLength = 50

    End Sub

    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData()
    End Sub

    Sub LoadData()
        Try
            Dim str As String = "select account_group_code,account_group_desc,ISNULL(Account_Main_Group_Code,'') As Account_Main_Group_Code  from TSPL_Account_Groups where account_group_code = '" + fndaccgp.Value + "'"
            Dim dt As DataTable
            Dim strvalue As String
            '' added by abhishek as on 12/10/2012
            dt = clsDBFuncationality.GetDataTable(str)
            If dt.Rows.Count = 0 Then
                strvalue = ""
            Else
                strvalue = dt.Rows(0).Item("account_group_code")
            End If
            If (strvalue <> "") Then
                txtdes.Text = fndaccgp.Tag
                Me.txtdes.Text = dt.Rows(0).Item("account_group_desc").ToString
                'If clsCommon.myLen(dt.Rows(0).Item("group_type")) > 0 Then
                '    Me.ddlaccounttype.Text = dt.Rows(0).Item("group_type")
                'Else
                '    Me.ddlaccounttype.Text = ""
                '    Me.ddlaccounttype.SelectedIndex = -1

                'End If
                '' Anubhooti 29-Sep-2014
                txtAccMainGrp.Value = clsCommon.myCstr(dt.Rows(0).Item("Account_Main_Group_Code"))
                If clsCommon.myLen(txtAccMainGrp.Value) > 0 Then
                    lblAccMain.Text = clsDBFuncationality.getSingleValue("select isnull(Account_Main_Group_Desc,'') As Account_Main_Group_Desc from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code='" + clsCommon.myCstr(txtAccMainGrp.Value) + "'")
                Else
                    lblAccMain.Text = ""
                End If
                ''
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnsave.Enabled = True
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            Else
                txtdes.Text = ""
                btnsave.Text = "Save"
                btndelete.Enabled = False
                btnsave.Enabled = True
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fndaccgp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndaccgp.ConnectionString = connectSql.SqlCon()
        'fndaccgp.Query = "select account_group_code as [Account Groups Code],account_group_desc as [Description] from tspl_account_groups"
        'fndaccgp.ValueToSelect = "Account Groups Code"
        'fndaccgp.Caption = "Account Groups"
        'fndaccgp.ValueToSelect1 = "Description"
    End Sub

    Public Sub funinsert()
        Try
            Dim AccMainGrp As String =""
            Dim strcode As String = fndaccgp.Value.ToString()
            If Not IsNumeric(strcode) Then
                Throw New Exception("Please Enter Numeric Value in Account Group Code")
            End If
            Dim currentdate As Date = Date.Today
            
            connectSql.RunSp("sp_accountgroups_insert", New SqlParameter("@accgpcode", fndaccgp.Value.ToString()), New SqlParameter("@des", txtdes.Text.ToString()), New SqlParameter("@createby", userCode), New SqlParameter("@createdate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@companycode", companyCode), New SqlParameter("@PrntOrdrno", fndaccgp.Value.ToString()), New SqlParameter("@GROUP_TYPE", ""))
            '' Anubhooti 29-Sep-2014 BM00000003709
            If clsCommon.myLen(txtAccMainGrp.Value) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_ACCOUNT_GROUPS SET Account_Main_Group_Code='" & clsCommon.myCstr(txtAccMainGrp.Value) & "' WHERE Account_Group_Code='" & clsCommon.myCstr(fndaccgp.Value) & "'")
            End If
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

    Public Sub funupdate()
        Try
            Dim currentdate As Date = Date.Today

            connectSql.RunSp("sp_accountGroups_update", New SqlParameter("@accgpcode", fndaccgp.Value.ToString()), New SqlParameter("@des", txtdes.Text.ToString()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@companycode", companyCode), New SqlParameter("@PrntOrdrNo", fndaccgp.Value.ToString()), New SqlParameter("@GROUP_TYPE", ""))

            If clsCommon.myLen(txtAccMainGrp.Value) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_ACCOUNT_GROUPS SET Account_Main_Group_Code='" & clsCommon.myCstr(txtAccMainGrp.Value) & "' WHERE Account_Group_Code='" & clsCommon.myCstr(fndaccgp.Value) & "'")
            End If
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub fundelete()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_AccountGroups_delete", New SqlParameter("@accgpcode", fndaccgp.Value()))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub funreset()
        fndaccgp.Value = ""
        txtdes.Text = ""
        'ddlaccounttype.SelectedIndex = -1
        txtAccMainGrp.Value = ""
        lblAccMain.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndaccgp.MyReadOnly = False
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            Dim AccMainGrpType As String = ""
            If clsCommon.myLen(txtAccMainGrp.Value) > 0 Then
                Dim qry As String = "select count(*) As Row from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code='" + clsCommon.myCstr(txtAccMainGrp.Value) + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check <= 0 Then
                    clsCommon.MyMessageBoxShow("'" & clsCommon.myCstr(txtAccMainGrp.Value) & "' code does not exists.First create account main group entry")
                    txtAccMainGrp.Focus()
                    Exit Sub
                End If
            End If
            If fndaccgp.Value = "" Then
                myMessages.blankValue("Account Group")
                fndaccgp.Focus()
            ElseIf txtdes.Text = "" Then
                myMessages.blankValue("Description")
                txtdes.Focus()
            ElseIf clsCommon.myLen(txtAccMainGrp.Value) <= 0 Then
                myMessages.blankValue("Account main group")
                txtAccMainGrp.Focus()
            Else
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.accountGroup, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                clsGLAccount.GetLinkAccountWithGroup(2, fndaccgp.Value, txtAccMainGrp.Value, Nothing)
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

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Deletedata()
    End Sub

    Sub Deletedata()
        If fndaccgp.Value = "" Then
            myMessages.blankValue("Account Group Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub accgpex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles accgpex.Click
        Dim str As String
        str = "select account_group_code As [Account Group Code],account_group_desc as [Description] ,ISNULL(Account_Main_Group_Code,'') AS [Account Main Group Code] from tspl_Account_Groups"
        ListImpExpColumnsMandatory = New List(Of String)({"Account Group Code", "Description", "Account Main Group Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Account Group Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub accgrpim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles accgrpim.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Account Group Code", "Description", "Account Main Group Code") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin() '' added by abhishek as on 12/10/2012
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("Check the length of Group Code")
                    End If

                    Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If clsCommon.myLen(strdes) > 100 Then
                        Throw New Exception("Check the length of Description ")
                    End If

                    If String.IsNullOrEmpty(strcode) Then
                        Throw New Exception("Account Group Code can't be blank")
                    End If

                    If String.IsNullOrEmpty(strdes) Then
                        Throw New Exception(" Description can't be blank")
                    End If

                     
                    '' Anubhooti 29-Sep-2014 
                    Dim strAccMainGrp As String = clsCommon.myCstr(grow.Cells("Account Main Group Code").Value)
                    If strAccMainGrp.Length <= 0 Or (String.IsNullOrEmpty(strAccMainGrp)) Then
                        Throw New Exception("Account main group code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf Not IsNumeric(strAccMainGrp) Then
                        Throw New Exception("Please enter numeric value in account main group code at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strAccMainGrp) > 0 Then
                        Dim qry As String = "select count(*) As Row from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code='" + strAccMainGrp + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("'" & clsCommon.myCstr(strAccMainGrp) & "' code does not exists at line no. " + clsCommon.myCstr(linno) + ".First create account main group entry")
                        End If
                    End If
                    clsGLAccount.GetLinkAccountWithGroup(2, strcode, strAccMainGrp, trans)
                    ''
                    Dim sql1 As String = "select count(*) from tspl_Account_Groups where account_group_code='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_AccountGroups_insert", New SqlParameter("@accgpcode", strcode), New SqlParameter("@des", strdes), New SqlParameter("@createby", userCode), New SqlParameter("@createdate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@companycode", companyCode), New SqlParameter("@PrntOrdrno", strcode), New SqlParameter("@Group_Type", ""))
                        '' Anubhooti 29-Sep-2014 BM00000003709
                        If clsCommon.myLen(strAccMainGrp) > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_ACCOUNT_GROUPS SET Account_Main_Group_Code='" & clsCommon.myCstr(strAccMainGrp) & "' WHERE Account_Group_Code='" & clsCommon.myCstr(strcode) & "'", trans)
                        End If
                    Else
                        connectSql.RunSpTransaction(trans, "sp_AccountGroups_update", New SqlParameter("@accgpcode", strcode), New SqlParameter("@des", strdes), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@companycode", companyCode), New SqlParameter("@PrntOrdrno", strcode), New SqlParameter("@Group_Type", ""))
                        '' Anubhooti 29-Sep-2014 BM00000003709
                        If clsCommon.myLen(strAccMainGrp) > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_ACCOUNT_GROUPS SET Account_Main_Group_Code='" & clsCommon.myCstr(strAccMainGrp) & "' WHERE Account_Group_Code='" & clsCommon.myCstr(strcode) & "'", trans)
                        End If
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub accgpclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles accgpclose.Click
        Me.Close()
    End Sub

    Private Sub btnChangeOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeOrder.Click
        Try
            Dim frm As New FrmChangePrntOrdr_ACGroup()
            frm.ShowDialog()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndaccgp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccgp._MYValidating
        Dim str As String = "select count(*) from tspl_account_groups   where  account_group_code ='" + fndaccgp.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndaccgp.MyReadOnly = False
        Else
            fndaccgp.MyReadOnly = True
        End If
        If fndaccgp.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select account_group_code as [AccountGroupsCode],account_group_desc as [Description],GROUP_TYPE AS Type from tspl_account_groups"
            'fndaccgp.Value = clsCommon.ShowSelectForm("ACCT_grp", qry, "AccountGroupsCode", "", fndaccgp.Value, "", isButtonClicked)
            fndaccgp.Value = clsAccountGroups.getFinder("", fndaccgp.Value, isButtonClicked)
            'txtdes.Text = clsDBFuncationality.getSingleValue("select account_group_desc  from tspl_account_groups where  account_group_code='" + fndaccgp.Value + "'")
            LoadData()
            txtdes.Text = clsDBFuncationality.getSingleValue("select account_group_desc  from tspl_account_groups where  account_group_code='" + fndaccgp.Value + "'")
            'key()

        End If
    End Sub

    Private Sub fndaccgp__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccgp._MYNavigator
        Dim qst As String = "select account_group_code as [Account Groups Code],account_group_desc as [Description],GROUP_TYPE AS Type,ISNULL(Account_Main_Group_Code,'') As [Account Main Group Code] from tspl_account_groups where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and tspl_account_groups .account_group_code in ('" + fndaccgp.Value + "')"
            Case NavigatorType.Next
                qst += " and tspl_account_groups .account_group_code in (select min(account_group_code ) from tspl_account_groups where account_group_code  >'" + fndaccgp.Value + "')"
            Case NavigatorType.First
                qst += " and tspl_account_groups .account_group_code in (select MIN(account_group_code ) from tspl_account_groups)"

            Case NavigatorType.Last
                qst += " and tspl_account_groups .account_group_code in (select Max(account_group_code ) from tspl_account_groups)"
            Case NavigatorType.Previous
                qst += " and tspl_account_groups .account_group_code in (select Max(account_group_code ) from tspl_account_groups where account_group_code  <'" + fndaccgp.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndaccgp.Value = clsCommon.myCstr(dt.Rows(0)("Account Groups Code"))
            txtdes.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()

        'txtdes.Text = clsDBFuncationality.getSingleValue("select account_group_desc  from tspl_account_groups where  account_group_code='" + fndaccgp.Value + "'")
        'key()
    End Sub

    Private Sub txtAccMainGrp__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAccMainGrp._MYValidating
        Dim Qry As String = "select  Account_Main_Group_Code As [Code], Account_Main_Group_Desc As [Account Main Group Desc],Group_Type AS [Group Type] from TSPL_ACCOUNT_MAIN_GROUPS "
        txtAccMainGrp.Value = clsCommon.ShowSelectForm("AccMainInGrp", Qry, "Code", "", txtAccMainGrp.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtAccMainGrp.Value) > 0 Then
            lblAccMain.Text = clsDBFuncationality.getSingleValue("Select ISNULL(Account_Main_Group_Desc,'') As Account_Main_Group_Desc from TSPL_ACCOUNT_MAIN_GROUPS Where Account_Main_Group_Code='" + clsCommon.myCstr(txtAccMainGrp.Value) + "' ")
        Else
            lblAccMain.Text = ""
        End If
    End Sub
End Class
