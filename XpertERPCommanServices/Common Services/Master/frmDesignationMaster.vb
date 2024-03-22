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
'createddate --> 19/05/2011
'modifiedby --> Vipin
'Modified date -->03/06/2011
'Tables Used --> tspl_designation_master
Public Class frmDesignationMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim userCode, companyCode As String
    Dim sQuery As String = String.Empty
    Dim strDesgMastPass As String = ""

    'Private isInsideLoadData As Boolean = False
    Public Sub New(ByVal user As String, ByVal company As String, ByVal formid As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        formtype = formid
    End Sub

    Sub LoadLevel()
        Dim dt As New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")


        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Level1"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Level2"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "Level3"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "Level4"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "Level5"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "6"
        dr("Name") = "Level6"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "7"
        dr("Name") = "Level7"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "8"
        dr("Name") = "Level8"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "9"
        dr("Name") = "Level9"
        dt.Rows.Add(dr)

        'Ticket No-TEC/11/07/19-000940 sanjay
        'dr = dt.NewRow()
        'dr("Code") = "10"
        'dr("Name") = "Level10"
        'dt.Rows.Add(dr)

        CmbLevelCode.DataSource = dt
        CmbLevelCode.ValueMember = "Code"
        CmbLevelCode.DisplayMember = "Name"

    End Sub
    Dim formtype As String = Nothing
    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003130

        ' KUNAL > TICKET : BM00000009879 > 30 - SEP - 2016
        If clsCommon.CompairString(formtype, clsUserMgtCode.frmPayrollDesignationMaster) = CompairStringResult.Equal Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmPayrollDesignationMaster)
            strDesgMastPass = clsUserMgtCode.frmPayrollDesignationMaster
        ElseIf clsCommon.CompairString(formtype, clsUserMgtCode.DesignationMaster) = CompairStringResult.Equal Then
            'MyBase.SetUserMgmt(clsUserMgtCode.DesignationMaster)
            strDesgMastPass = clsUserMgtCode.DesignationMaster
        End If

        ''MyBase.SetUserMgmt(clsUserMgtCode.DesignationMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            desimport.Enabled = True
            desexport.Enabled = True
        Else
            desimport.Enabled = False
            desexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmDesignationMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
       
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
    End Sub 'Main Form Load
    Private Sub frmDesignationMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)
        ToolTipdesig.SetToolTip(btnnew, "New")
        'fnddesig.txtValue.MaxLength = 12
        'AddHandler fnddesig.txtValue.TextChanged, AddressOf text_changed
        'AddHandler fnddesig.txtValue.KeyPress, AddressOf key_press
        'fnddesig.txtValue.CharacterCasing = CharacterCasing.Upper
        textchangedsub()
        btndelete.Enabled = False
        btnsave.Enabled = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        LoadLevel()
    End Sub

    'It will fill all controls in screen if find any existing data in table 
    Sub textchangedsub()
        Try
            'isInsideLoadData = True
            Dim str As String = "select designation_id from TSPL_DESIGNATION_MASTER where designation_id = '" + fnddesig.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Then
                funfill()
            Else
                txtdes.Text = ""
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        Finally
            ' isInsideLoadData = False
        End Try
    End Sub
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)


        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If

    End Sub
    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try
            Dim str As String = "select designation_desc from TSPL_DESIGNATION_MASTER where designation_id = '" + fnddesig.Value + "'"
            'Dim dr As SqlDataReader
            txtdes.Text = clsDBFuncationality.getSingleValue(str)
            str = "select Level_Code from TSPL_DESIGNATION_MASTER where designation_id = '" + fnddesig.Value + "'"
            'Dim dr As SqlDataReader
            CmbLevelCode.SelectedValue = clsDBFuncationality.getSingleValue(str)
            'While dr.Read()
            '  = dr(0).ToString()

            'End While
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnsave.Text = "Update"
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from tspl_designation_Master where designation_id='" & fnddesig.Value & "'")
                If ChkNewEntry = 0 Then
                    fnddesig.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.DesignationMaster, "", "")
                    If clsCommon.myLen(fnddesig.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_DesignationMaster_insert", New SqlParameter("@desigid", fnddesig.Value), New SqlParameter("@description", txtdes.Text.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.insert()
            sQuery = "update tspl_designation_Master set Level_Code='" & clsCommon.myCstr(CmbLevelCode.SelectedValue) & "' where designation_id='" & fnddesig.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery)
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
            connectSql.RunSp("sp_DesignationMaster_update", New SqlParameter("@desigid", fnddesig.Value), New SqlParameter("@description", txtdes.Text.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.update()
            sQuery = "update tspl_designation_Master set Level_Code='" & clsCommon.myCstr(CmbLevelCode.SelectedValue) & "' where designation_id='" & clsCommon.myCstr(fnddesig.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            connectSql.RunSp("sp_DesignationMaster_delete", New SqlParameter("@desigid", fnddesig.Value))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        fnddesig.MyReadOnly = False
        fnddesig.Value = ""
        txtdes.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        CmbLevelCode.SelectedValue = Nothing
    End Sub
    'closing of current window form
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click

        Me.Close()
    End Sub
    'Validation on save button click and calling funinsert,funupdate
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(fnddesig.Value) <= 0 Then
                myMessages.blankValue(Me, "Designation Code", Me.Text)
                fnddesig.Focus()
                Return
                'If fnddesig.Value = "" Then
                '    myMessages.blankValue("Designation Code")
                '    fnddesig.Focus()
            ElseIf txtdes.Text = "" Then
                myMessages.blankValue(Me, "Description", Me.Text)
                txtdes.Focus()
                Return
            ElseIf CmbLevelCode.SelectedValue = "" Then
                myMessages.blankValue(Me, "Level Code", Me.Text)
                CmbLevelCode.Focus()
                Return
            Else

                If clsCommon.myLen(strDesgMastPass) > 0 Then
                    If MyBase.isModifyonPasswordFlag Then
                        If clsPasswordCheckForMasters.CheckMasterPwd(strDesgMastPass, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                        Else
                            Return
                        End If
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
        If fnddesig.Value = "" Then
            myMessages.blankValue(Me, "Designation Code", Me.Text)
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub
    Private Sub fnddesig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fnddesig.ConnectionString = connectSql.SqlCon()
        'fnddesig.Query = " select designation_id As [Designation Code],designation_desc  as [Description]from TSPL_Designation_MASTER "
        'fnddesig.ValueToSelect = "Designation Code"
        'fnddesig.Caption = "Designation Master"
        'fnddesig.ValueToSelect1 = "Description"
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub


    'For Export functionality 
    Private Sub desexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles desexport.Click
        Dim str As String
        str = "select Designation_id As [Designation Code],Designation_desc as [Description] from tspl_Designation_master"

        ListImpExpColumnsMandatory = New List(Of String)({"Designation Code", "Description"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Designation Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    'For Import functionality 
    Private Sub desimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles desimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Designation Code", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strcode As String = grow.Cells(0).Value.ToString()
                    Dim strdes As String = grow.Cells(1).Value.ToString()

                    If (String.IsNullOrEmpty(strcode)) Or strcode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Designation Code can not be blank or incorrect", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    If (String.IsNullOrEmpty(strdes)) Or strdes.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow(Me, " Designation Description  can not be blank or incorrect", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim sql1 As String = "select count(*) from tspl_Designation_master where designation_id='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "sp_DesignationMaster_insert", New SqlParameter("@desigid", strcode), New SqlParameter("@description", strdes), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_DesignationMaster_update", New SqlParameter("@desigid", strcode), New SqlParameter("@description", strdes), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))

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

    Private Sub desclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles desclose.Click
        Me.Close()
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DESIGN-M"
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

    Private Sub fnddesig__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddesig._MYValidating
        Dim str As String = "select count(*) from TSPL_DESIGNATION_MASTER where Designation_id ='" + fnddesig.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fnddesig.MyReadOnly = False
        Else
            fnddesig.MyReadOnly = True
        End If

        If fnddesig.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = " select Designation_id As [DesignationCode],Designation_desc As[Designation]  from TSPL_DESIGNATION_MASTER  "
            'fnddesig.Value = clsCommon.ShowSelectForm("fmdesig", qry, "DesignationCode", "", fnddesig.Value, "", isButtonClicked)
            fnddesig.Value = clsDesignationMaster.getFinder("", fnddesig.Value, isButtonClicked)
            txtdes.Text = clsDBFuncationality.getSingleValue("select Designation_desc from TSPL_DESIGNATION_MASTER where Designation_id = '" + fnddesig.Value + "'")
            CmbLevelCode.SelectedValue = clsDBFuncationality.getSingleValue("select Level_Code from TSPL_DESIGNATION_MASTER where Designation_id = '" + fnddesig.Value + "'")

            textchangedsub()
            ' fndUser_NameLeave()

            '    textchanged()

            '    If fnddesig.Value IsNot Nothing Then
            '        btndelete.Enabled = True
            '    Else
            '        btndelete.Enabled = False
            '    End If

            '    btnsave.Enabled = True
        End If
    End Sub

    Private Sub fnddesig__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fnddesig._MYNavigator
        Dim qst As String = "select Designation_id As[Designation Code],Designation_desc As[Designation Name],Level_code  from TSPL_DESIGNATION_MASTER   where  2=2"
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and Designation_id in (select min(Designation_id) from TSPL_DESIGNATION_MASTER where Designation_id>'" + fnddesig.Value + "' ) "
            Case NavigatorType.First
                qst += "and Designation_id in (select MIN(Designation_id) from TSPL_DESIGNATION_MASTER )"
            Case NavigatorType.Last
                qst += "and Designation_id in (select Max(Designation_id) from TSPL_DESIGNATION_MASTER  )"
            Case NavigatorType.Previous
                qst += "and Designation_id in (select max(Designation_id) from TSPL_DESIGNATION_MASTER where Designation_id<'" + fnddesig.Value + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fnddesig.Value = clsCommon.myCstr(dt.Rows(0)("Designation Code"))
            txtdes.Text = clsCommon.myCstr(dt.Rows(0)("Designation Name"))
            CmbLevelCode.Text = Nothing
            CmbLevelCode.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Level_Code"))
        End If
        textchangedsub()
    End Sub

    Private Sub fnddesig_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fnddesig.Leave
        Dim qry As String = "Select * from TSPL_DESIGNATION_MASTER Where Designation_id='" + fnddesig.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count <= 0 Then
            txtdes.Text = ""
            btnsave.Text = "Save"
        Else
            textchangedsub()
        End If
    End Sub

   
  
End Class
