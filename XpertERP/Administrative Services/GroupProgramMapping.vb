Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports common

'created by --> Vipin
'createddate --> 25/05/2011
'modifiedby --> Vipin
'Modified date -->03/06/2011    Ticket no BM00000008566 by balwinder
'Tables Used --> tspl_user_group_master,tspl_group_program_Mapping,tspl_program_Master
'=== Group mapping Print flag setting Ticket No: BM00000009569


Public Class GroupProgramMapping
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Dim loaddttype As Integer = 0
    Public isNewEntry As Boolean = False
    Dim selectedValue As String = Nothing

    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003130
        'MyBase.SetUserMgmt(clsUserMgtCode.groupProgramMapping)
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
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Dim str As String

    Public Sub fnd_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select group_code from tspl_user_group_master where group_code='" + fndgroup.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Or fndgroup.Value = "" Then
            Else : strvalue = ""
                common.clsCommon.MyMessageBoxShow("This Group code does not exist", Me.Text)
                fndgroup.Value = ""
                txtname.Text = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndleave()
        Try
            Dim str As String = "select group_code from tspl_user_group_master where group_code='" + fndgroup.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Or fndgroup.Value = "" Then
            Else : strvalue = ""
                common.clsCommon.MyMessageBoxShow("This Group code does not exist", Me.Text)
                fndgroup.Value = ""
                txtname.Text = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'For filling the grid on load
    Public Sub gridload()
        If loaddttype = 0 Then
            loaddttype = loaddttype + 1
            ddl_type.DataSource = Nothing
            chk_select.Checked = False
            gb_select.Enabled = True

            Dim dttype As DataTable = New DataTable()
            dttype.Columns.Add("Code", GetType(String))
            dttype.Columns.Add("Name", GetType(String))

            If dgvprogram.Columns.Count > 0 Then
                For i As Integer = 0 To dgvprogram.Columns.Count - 1
                    If TypeOf dgvprogram.Columns(i) Is GridViewCheckBoxColumn Then
                        Dim dr As DataRow = dttype.NewRow()
                        dr("Code") = dgvprogram.Columns(i).Name
                        dr("Name") = dgvprogram.Columns(i).HeaderText
                        dttype.Rows.Add(dr)
                    End If
                Next
            End If
            If dttype IsNot Nothing AndAlso dttype.Rows.Count > 0 Then
                ddl_type.DataSource = dttype
                ddl_type.ValueMember = "Code"
                ddl_type.DisplayMember = "Name"
            End If
        End If

        'Dim strquery As String = "select program_Code as [Program Code],program_name as [Program Name] from TSPL_Program_Master where 2=2"
        'strquery += " and Program_Code not in (" + clsCommon.GetMulcallString(MDI.arrExcluded) + ") and Program_Code not in (select Screen_Name as Program_Code from TSPL_MODULE_SCREEN_PERMISSION)"
        'If clsCommon.myLen(cboSubModule.SelectedValue) > 0 Then
        '    strquery += " and Parent_Code='" + clsCommon.myCstr(cboSubModule.SelectedValue) + "'"
        'ElseIf clsCommon.myLen(cboModule.SelectedValue) > 0 Then
        '    strquery += " and Parent_Code in ( select Program_Code from TSPL_Program_Master where Parent_Code ='" + clsCommon.myCstr(cboModule.SelectedValue) + "') "
        'End If
        'strquery += " and not (Type in ('M','SM') or Parent_Code is null) order by sno"

        Dim strquery As String = "select  TSPL_PROGRAM_MASTER.Program_Code as [Program Code],case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as [Program Name]  from TSPL_PROGRAM_MASTER "
        strquery += " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code "
        strquery += " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code "
        strquery += " where  TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM')  "
        strquery += " and TSPL_PROGRAM_MASTER.Program_Code not in (" + clsCommon.GetMulcallString(MDI.arrExcluded) + ")  and TSPL_PROGRAM_MASTER.Program_Code not in ( select Screen_Name from TSPL_MODULE_SCREEN_PERMISSION)"
        If clsCommon.myLen(cboSubModule.SelectedValue) > 0 Then
            strquery += " and TSPL_PROGRAM_MASTER.Parent_Code ='" + clsCommon.myCstr(cboSubModule.SelectedValue) + "'"
        End If
        If clsCommon.myLen(cboModule.SelectedValue) > 0 Then
            strquery += " and TBL_MODULE.Program_Code in ( '" + clsCommon.myCstr(cboModule.SelectedValue) + "') "
        Else
            strquery += " and TBL_MODULE.Program_Code in ( select Module_Name from TSPL_MODULE_PERMISSION ) "
        End If

        strquery += " order by SNo "

        dgvprogram.Columns(0).FieldName = "Program Code"
        dgvprogram.Columns(1).FieldName = "Program Name"
        transportSql.FillGridView(strquery, dgvprogram)

        'dgvprogram.Columns(8).Width = 150
        'dgvprogram.Columns(9).Width = 150
        ' dgvprogram.BestFitColumns()
    End Sub
    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Dim strquery As String = "select group_code,tspl_group_program_Mapping.program_code,read_flag ,modify_flag,delete_flag,authorized_flag, Reverse_Flag, Export_Flag, Print_Flag,Cancel_Flag,Cancel_Flag_After_Posting,QucikExport_Flag,isModifyonPassword,is_Amendment,update_flag from tspl_group_program_Mapping left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=tspl_group_program_Mapping.Program_Code where group_code='" + fndgroup.Value + "'"
        If clsCommon.myLen(cboSubModule.SelectedValue) > 0 Then
            strquery += " and TSPL_PROGRAM_MASTER.Parent_Code='" + clsCommon.myCstr(cboSubModule.SelectedValue) + "'"
        ElseIf clsCommon.myLen(cboModule.SelectedValue) > 0 Then
            strquery += " and TSPL_PROGRAM_MASTER.Parent_Code in ( select Program_Code from TSPL_Program_Master where Parent_Code ='" + clsCommon.myCstr(cboModule.SelectedValue) + "') "
        End If
        strquery += " and not (TSPL_PROGRAM_MASTER.Type in ('M','SM') or TSPL_PROGRAM_MASTER.Parent_Code is null) order by TSPL_PROGRAM_MASTER.SNo "

        Dim da As New SqlDataAdapter(strquery, connectSql.SqlCon())
        Dim dt As New DataTable
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            'dgvprogram.Columns(8).Width = 150
            'dgvprogram.Columns(9).Width = 150
            For i As Integer = 0 To dt.Rows.Count - 1
                For GridCounter As Integer = 0 To dgvprogram.RowCount - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dgvprogram.Rows(GridCounter).Cells(0).Value), clsCommon.myCstr(dt.Rows(i)("program_code"))) = CompairStringResult.Equal Then
                        Dim strreadflag As String = dt.Rows(i)(2).ToString()
                        Dim strmodifyflag As String = dt.Rows(i)(3).ToString()
                        Dim strdeleteflag As String = dt.Rows(i)(4).ToString()
                        Dim strauth As String = dt.Rows(i)(5).ToString()
                        Dim strReverse As String = dt.Rows(i)(6).ToString()
                        Dim strExport As String = dt.Rows(i)(7).ToString()

                        Dim strCancel As String = dt.Rows(i)(8).ToString()
                        Dim strCancel_After_Posting As String = dt.Rows(i)(9).ToString()
                        ''===PArteek====''
                        Dim strPrint As String = dt.Rows(i)(10).ToString()
                        Dim strQuickExport As String = dt.Rows(i)(11).ToString()
                        ''====End===''
                        '==sanjeet(01/02/2017)=====
                        Dim strModifyOnPwd As String = dt.Rows(i)(12).ToString()
                        '==stuti(07/03/2017)=====
                        Dim strAmendment As String = dt.Rows(i)(13).ToString()
                        '============
                        Dim strupdate As String = dt.Rows(i)(14).ToString()
                        If strreadflag = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(2).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(2).Value = False
                        End If
                        If strmodifyflag = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(3).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(3).Value = False
                        End If
                        If strdeleteflag = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(4).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(4).Value = False
                        End If
                        If strauth = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(5).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(5).Value = False
                        End If
                        If strReverse = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(6).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(6).Value = False
                        End If
                        If strExport = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(7).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(7).Value = False
                        End If
                        If strCancel = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(8).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(8).Value = False
                        End If
                        If strCancel_After_Posting = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(9).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(9).Value = False
                        End If
                        ''====Parteek''
                        If strPrint = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(10).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(10).Value = False
                        End If
                        If strQuickExport = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(11).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(11).Value = False
                        End If
                        ''====End====''
                        '===sanjeet(01/02/2017)=====
                        If strModifyOnPwd = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(12).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(12).Value = False
                        End If
                        '===stuti(07/03/2017)=====
                        If strAmendment = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(13).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(13).Value = False
                        End If
                        '===================
                        If strupdate = "1" Then
                            dgvprogram.Rows(GridCounter).Cells(14).Value = True
                        Else
                            dgvprogram.Rows(GridCounter).Cells(14).Value = False
                        End If
                    End If
                Next
            Next
        End If


        'dgvprogram.BestFitColumns()
        'dgvprogram.Columns(8).Width = 150
        'dgvprogram.Columns(9).Width = 150
        btndelete.Enabled = True
        btnsave.Enabled = True
        btnsave.Text = "Update"
    End Sub
    'Public Shared Function HistoryUpdate(ByVal strCode As String) As Boolean
    '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_GROUP_PROGRAM_MAPPING", "Group_Code", Nothing)
    '    Return True
    'End Function
    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            Dim currentdate As Date = Date.Today
            Dim strread As Char
            Dim strmodify As Char
            Dim strdel As Char
            Dim strauth As Char
            Dim strReverse As Char
            Dim strExport As Char
            Dim strPrint As Char
            Dim strQucikExport As Char
            Dim strCancel As Char
            Dim strCancel_After_Posting As Char
            Dim strModifyonPwd As Char
            Dim strAmendment As Char
            Dim strupdate As Char

            For i As Integer = 0 To dgvprogram.Rows.Count - 1

                If CBool(dgvprogram.Rows(i).Cells(2).Value) = True Then
                    strread = "1"
                Else
                    strread = "0"

                End If

                If CBool(dgvprogram.Rows(i).Cells(3).Value) = True Then
                    strmodify = "1"
                Else
                    strmodify = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(4).Value) = True Then
                    strdel = "1"
                Else
                    strdel = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(5).Value) = True Then
                    strauth = "1"
                Else
                    strauth = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(6).Value) = True Then
                    strReverse = "1"
                Else
                    strReverse = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(7).Value) = True Then
                    strExport = "1"
                Else
                    strExport = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(9).Value) = True Then
                    strCancel = "1"
                Else
                    strCancel = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(10).Value) = True Then
                    strCancel_After_Posting = "1"
                Else
                    strCancel_After_Posting = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(8).Value) = True Then
                    strPrint = "1"
                Else
                    strPrint = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(11).Value) = True Then
                    strQucikExport = "1"
                Else
                    strQucikExport = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(12).Value) = True Then
                    strModifyonPwd = "1"
                Else
                    strModifyonPwd = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(13).Value) = True Then
                    strAmendment = "1"
                Else
                    strAmendment = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(14).Value) = True Then
                    strupdate = "1"
                Else
                    strupdate = "0"
                End If
                'connectSql.RunSp("sp_GroupProgramMapping_insert", New SqlParameter("@groupcode", fndgroup.Value), New SqlParameter("@programcode", dgvprogram.Rows(i).Cells(0).Value.ToString()), New SqlParameter("@read", strread), New SqlParameter("@modify", strmodify), New SqlParameter("@delete", strdel), New SqlParameter("@auth", strauth), New SqlParameter("@createby", userCode), New SqlParameter("@createdate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@companycode", companyCode))

                'Dim qry As String = "update TSPL_GROUP_PROGRAM_MAPPING set Reverse_Flag='" + strReverse + "', Export_Flag='" + strExport + "' where Group_Code='" + fndgroup.Value + "' and Program_Code='" + dgvprogram.Rows(i).Cells(0).Value.ToString() + "' "
                'clsDBFuncationality.ExecuteNonQuery(qry)
                'If isNewEntry = True Then
                '    HistoryUpdate(clsCommon.myCstr(fndgroup.Value))
                'End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Group_Code", fndgroup.Value)
                clsCommon.AddColumnsForChange(coll, "Program_Code", dgvprogram.Rows(i).Cells(0).Value.ToString())
                clsCommon.AddColumnsForChange(coll, "Read_Flag", strread)
                clsCommon.AddColumnsForChange(coll, "Modify_Flag", strmodify)
                clsCommon.AddColumnsForChange(coll, "Delete_Flag", strdel)
                clsCommon.AddColumnsForChange(coll, "Authorized_Flag", strauth)
                clsCommon.AddColumnsForChange(coll, "Reverse_Flag", strReverse)
                clsCommon.AddColumnsForChange(coll, "Export_Flag", strExport)
                clsCommon.AddColumnsForChange(coll, "Print_Flag", strPrint)
                clsCommon.AddColumnsForChange(coll, "QucikExport_Flag", strQucikExport)
                clsCommon.AddColumnsForChange(coll, "isModifyonPassword", strModifyonPwd)
                clsCommon.AddColumnsForChange(coll, "is_Amendment", strAmendment)
                clsCommon.AddColumnsForChange(coll, "Cancel_Flag", strCancel)
                clsCommon.AddColumnsForChange(coll, "Cancel_Flag_After_Posting", strCancel_After_Posting)
                clsCommon.AddColumnsForChange(coll, "Update_Flag", strupdate)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                Dim result As Boolean = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GROUP_PROGRAM_MAPPING", OMInsertOrUpdate.Insert, "")
                'HistoryUpdate(clsCommon.myCstr(fndgroup.Value))
            Next
            If txtDashBoardMult.arrValueMember IsNot Nothing AndAlso txtDashBoardMult.arrValueMember.Count > 0 Then
                Dim i As Integer = 0
                For i = 0 To txtDashBoardMult.arrValueMember.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Group_Code", fndgroup.Value)
                    clsCommon.AddColumnsForChange(coll, "Dashboard_Code", clsCommon.myCstr(txtDashBoardMult.arrValueMember(i)))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING", OMInsertOrUpdate.Insert, "")
                Next
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
    'Funtion for updation  of data
    Public Sub funupdate()
        Try
            Dim currentdate As Date = Date.Today
            Dim strread As Char
            Dim strmodify As Char
            Dim strdel As Char
            Dim strauth As Char
            Dim strReverse As Char
            Dim strExport As Char
            Dim strPrint As Char
            Dim strQuickExport As Char
            Dim strCancel As Char
            Dim strCancel_After_Posting As Char
            Dim strModifyonPwd As Char
            Dim strAmendment As Char
            Dim strupdate As Char
            Dim str As String = "delete from tspl_group_program_mapping where group_code='" + fndgroup.Value + "' "
            If clsCommon.myLen(cboSubModule.SelectedValue) > 0 Then
                str += " and tspl_group_program_mapping.Program_Code in (select Program_Code from TSPL_PROGRAM_MASTER where  Parent_Code in ('" + clsCommon.myCstr(cboSubModule.SelectedValue) + "'))"
            ElseIf clsCommon.myLen(cboModule.SelectedValue) > 0 Then
                str += " and tspl_group_program_mapping.Program_Code in (select Program_Code from TSPL_PROGRAM_MASTER where  Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER where 2=2 and Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "'))"
            End If
            Dim trans As SqlTransaction = Nothing


            connectSql.RunSql(str)

            For i As Integer = 0 To dgvprogram.Rows.Count - 1

                If True Then

                End If

                If CBool(dgvprogram.Rows(i).Cells(2).Value) = True Then
                    strread = "1"
                Else
                    strread = "0"

                End If

                If CBool(dgvprogram.Rows(i).Cells(3).Value) = True Then
                    strmodify = "1"
                Else
                    strmodify = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(4).Value) = True Then
                    strdel = "1"
                Else
                    strdel = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(5).Value) = True Then
                    strauth = "1"
                Else
                    strauth = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(6).Value) = True Then
                    strReverse = "1"
                Else
                    strReverse = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(7).Value) = True Then
                    strExport = "1"
                Else
                    strExport = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(9).Value) = True Then
                    strCancel = "1"
                Else
                    strCancel = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(10).Value) = True Then
                    strCancel_After_Posting = "1"
                Else
                    strCancel_After_Posting = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(8).Value) = True Then
                    strPrint = "1"
                Else
                    strPrint = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(11).Value) = True Then
                    strQuickExport = "1"
                Else
                    strQuickExport = "0"
                End If
                If CBool(dgvprogram.Rows(i).Cells(12).Value) = True Then
                    strModifyonPwd = "1"
                Else
                    strModifyonPwd = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(13).Value) = True Then
                    strAmendment = "1"
                Else
                    strAmendment = "0"
                End If

                If CBool(dgvprogram.Rows(i).Cells(14).Value) = True Then
                    strupdate = "1"
                Else
                    strupdate = "0"
                End If
                'If strread = "1" OrElse strmodify = "1" OrElse strdel = "1" OrElse strauth = "1" Then
                '    connectSql.RunSp("sp_GroupProgramMapping_insert", New SqlParameter("@groupcode", fndgroup.Value), New SqlParameter("@programcode", dgvprogram.Rows(i).Cells(0).Value.ToString()), New SqlParameter("@read", strread), New SqlParameter("@modify", strmodify), New SqlParameter("@delete", strdel), New SqlParameter("@auth", strauth), New SqlParameter("@createby", userCode), New SqlParameter("@createdate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@companycode", companyCode))

                'End If
                'Dim qry As String = "update TSPL_GROUP_PROGRAM_MAPPING set Reverse_Flag='" + strReverse + "', Export_Flag='" + strExport + "' where Group_Code='" + fndgroup.Value + "' and Program_Code='" + dgvprogram.Rows(i).Cells(0).Value.ToString() + "' "
                'clsDBFuncationality.ExecuteNonQuery(qry)
                'HistoryUpdate(clsCommon.myCstr(fndgroup.Value))

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Group_Code", fndgroup.Value)
                clsCommon.AddColumnsForChange(coll, "Program_Code", dgvprogram.Rows(i).Cells(0).Value.ToString())
                clsCommon.AddColumnsForChange(coll, "Read_Flag", strread)
                clsCommon.AddColumnsForChange(coll, "Modify_Flag", strmodify)
                clsCommon.AddColumnsForChange(coll, "Delete_Flag", strdel)
                clsCommon.AddColumnsForChange(coll, "Authorized_Flag", strauth)
                clsCommon.AddColumnsForChange(coll, "Reverse_Flag", strReverse)
                clsCommon.AddColumnsForChange(coll, "Export_Flag", strExport)
                clsCommon.AddColumnsForChange(coll, "Print_Flag", strPrint)
                clsCommon.AddColumnsForChange(coll, "QucikExport_Flag", strQuickExport)
                clsCommon.AddColumnsForChange(coll, "isModifyonPassword", strModifyonPwd)
                clsCommon.AddColumnsForChange(coll, "is_Amendment", strAmendment)
                clsCommon.AddColumnsForChange(coll, "Cancel_Flag", strCancel)
                clsCommon.AddColumnsForChange(coll, "Cancel_Flag_After_Posting", strCancel_After_Posting)
                clsCommon.AddColumnsForChange(coll, "Update_Flag", strupdate)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                Dim result As Boolean = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GROUP_PROGRAM_MAPPING", OMInsertOrUpdate.Insert, " Group_Code='" + fndgroup.Value.Trim() + "' and Program_Code='" + dgvprogram.Rows(i).Cells(0).Value.ToString() + "' ")
                'If Not isNewEntry Then

                '    HistoryUpdate(clsCommon.myCstr(fndgroup.Value))
                'End If
                'HistoryUpdate(clsCommon.myCstr(fndgroup.Value))


            Next
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndgroup.Value, "TSPL_GROUP_PROGRAM_MAPPING", "Group_Code", trans)


            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING where Group_Code='" + fndgroup.Value + "'")
            If txtDashBoardMult.arrValueMember IsNot Nothing AndAlso txtDashBoardMult.arrValueMember.Count > 0 Then
                Dim i As Integer = 0
                For i = 0 To txtDashBoardMult.arrValueMember.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Group_Code", fndgroup.Value)
                    clsCommon.AddColumnsForChange(coll, "Dashboard_Code", clsCommon.myCstr(txtDashBoardMult.arrValueMember(i)))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING", OMInsertOrUpdate.Insert, "")
                Next
            End If

            myMessages.update()


        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING where Group_Code='" + fndgroup.Value + "'")
            connectSql.RunSp("sp_GroupProgramMapping_delete", New SqlParameter("@groupcode", fndgroup.Value))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        fndgroup.MyReadOnly = False
        fndgroup.Value = ""
        txtname.Text = ""
        txtDashBoardMult.arrValueMember = Nothing
        ddl_type.DataSource = Nothing
        chk_select.Checked = False
        gb_select.Enabled = True
        gridload()
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        fndgroup.Value = CharacterCasing.Upper
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    'Public Sub keypress()
    '    ' fndgroup.Value = CharacterCasing.Upper
    '    'If (e.KeyChar = Chr(39)) Then
    '    '    e.Handled = True
    '    'End If
    'End Sub
    'It will fill all controls in screen if find any existing data in table 
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    Dim str As String = "select group_code,group_desc  from TSPL_User_Group_Master where group_code = '" + fndgroup.Value + "'"
        '    Dim dr As SqlDataReader
        '    dr = connectSql.RunSqlReturnDR(str)
        '    Dim strvalue As String
        '    Dim strname As String
        '    If dr.Read() Then
        '        strvalue = dr(0).ToString()
        '    End If
        '    If (strvalue <> "") Then
        '        funfill()
        '    Else
        '        txtname.Text = ""
        '        btnsave.Text = "Save"
        '        btndelete.Enabled = False
        '    End If
        'Catch ex As Exception
        '    myMessages.myExceptions(ex.ToString())
        'End Try
        Dim desc As String = "select group_desc from TSPL_USER_Group_MASTER where group_Code='" + fndgroup.Value + "'"
        Dim value1 As String = clsDBFuncationality.getSingleValue(desc)

        'If dr1.Read() Then
        '    value1 = dr1(0).ToString()
        'End If
        Dim str As String = "select group_Code from TSPL_Group_Program_Mapping where group_Code='" + fndgroup.Value + "'"
        Dim value As String = clsDBFuncationality.getSingleValue(str)

        'If dr.Read() Then
        '    value = dr(0).ToString()
        'End If
        If (value <> "") Then
            funfill()

        Else
            gridload()
            btnsave.Text = "Save"
            btndelete.Enabled = False
            txtname.Text = " "
        End If
        txtname.Text = value1

    End Sub
    Public Sub textchangedsub()
        Dim desc As String = "select group_desc from TSPL_USER_Group_MASTER where group_Code='" + fndgroup.Value + "'"
        Dim value1 As String = clsDBFuncationality.getSingleValue(desc)

        'If dr1.Read() Then
        '    value1 = dr1(0).ToString()
        'End If
        Dim str As String = "select group_Code from TSPL_Group_Program_Mapping where group_Code='" + fndgroup.Value + "'"
        Dim value As String = clsDBFuncationality.getSingleValue(str)

        'If dr.Read() Then
        '    value = dr(0).ToString()
        'End If
        If (value <> "") Then
            funfill()

        Else
            gridload()
            btnsave.Text = "Save"
            btndelete.Enabled = False
            txtname.Text = " "
        End If
        txtname.Text = value1
    End Sub

    Private Sub GroupProgramMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub GroupProgramMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")

        gridload()
        dgvprogram.AutoGenerateColumns = False
        ToolTipnew.SetToolTip(btnnew, "New")

        textchangedsub()
        fndleave()


        btndelete.Enabled = False
        btnsave.Enabled = True

        LoadModule()

    End Sub
    Private Sub LoadModule()
        Dim qry As String = "select Program_Code as Code,Program_Name as Name from TSPL_PROGRAM_MASTER where  TYPE='M' and Program_Code not in (" + clsCommon.GetMulcallString(MDI.arrExcluded) + ") and  Program_Code in (Select Module_Name from TSPL_MODULE_PERMISSION)  order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "All"
        dt.Rows.InsertAt(dr, 0)
        cboModule.DataSource = dt
        cboModule.ValueMember = "Code"
        cboModule.DisplayMember = "Name"
    End Sub

    Private Sub LoadSubModule()
        Dim qry As String = "select Program_Code as Code,Program_Name as Name from TSPL_PROGRAM_MASTER where  TYPE='SM' "
        If clsCommon.myLen(cboModule.SelectedValue) > 0 Then
            qry += " and Parent_Code ='" + clsCommon.myCstr(cboModule.SelectedValue) + "'"
        End If
        qry += " order by SNo"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "All"
        dt.Rows.InsertAt(dr, 0)
        cboSubModule.DataSource = dt
        cboSubModule.ValueMember = "Code"
        cboSubModule.DisplayMember = "Name"
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If fndgroup.Value = "" Then
                myMessages.blankValue("Group Code")
                fndgroup.Focus()
            Else
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
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndgroup.Value = "" Then
            myMessages.blankValue("Group Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub
    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        Dim Sql As String
        Sql = "select Group_code AS [Group Code],TSPL_GROUP_PROGRAM_MAPPING.program_code As [Program Code],case when len( isnull( TableName.Re_Name,''))>0 then TableName.Re_Name else TableName.Program_Name end as [Program Name],case when len( isnull( FTableName.Re_Name,''))>0 then FTableName.Re_Name else FTableName.Program_Name end as SubModule,case when len( isnull( GFTableName.Re_Name,''))>0 then GFTableName.Re_Name else GFTableName.Program_Name end as Module,(case when read_flag='1' then 'yes' else 'no' end) as [Read] ,(case when modify_flag='1' then 'yes' else 'no' end) as [Modify],(case when delete_flag='1' then 'yes' else 'no' end) as [Delete],(case when authorized_flag='1' then 'yes' else 'no' end) as [Authorized],(case when Reverse_Flag='1' then 'yes' else 'no' end) as [Reverse] ,(case when Export_Flag='1' then 'yes' else 'no' end) as Export ,(case when Print_Flag='1' then 'yes' else 'no' end) as [Print] ,(case when Cancel_Flag='1' then 'yes' else 'no' end) as Cancel ,(case when Cancel_Flag_After_Posting='1' then 'yes' else 'no' end) as CancelPostTransaction , (case when QucikExport_Flag='1' then 'yes' else 'no' end) as QuickExport , (case when isModifyonPassword='1' then 'yes' else 'no' end) as isModifyonPassword,(case when is_Amendment='1' then 'yes' else 'no' end) as isAmendment ,(case when update_flag='1' then 'yes' else 'no' end) as [Update] from tspl_Group_Program_Mapping left outer join TSPL_PROGRAM_MASTER as TableName on TableName.Program_Code=TSPL_GROUP_PROGRAM_MAPPING.Program_Code left outer join TSPL_PROGRAM_MASTER as FTable on FTable.Program_Code= TableName.Parent_Code left outer join TSPL_PROGRAM_MASTER as FTableName on FTableName.Program_Code= TableName.Parent_Code left outer join TSPL_PROGRAM_MASTER as GFTable on GFTable.Program_Code=FTable.Parent_Code left outer join TSPL_PROGRAM_MASTER as GFTableName on GFTableName.Program_Code=FTable.Parent_Code"
        ListImpExpColumnsMandatory = New List(Of String)({"Group Code", "Program Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Group Code", "Program Code"})
        transportSql.ExporttoExcel(Sql, "", "[Group Code],Module,SubModule", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim readflag As String
        Dim modifyflag As String
        Dim deleteflag As String
        Dim authflag As String

        Dim ReverseFlag As String
        Dim ExportFlag As String
        Dim PrintFlag As String
        Dim CancelFlag As String
        Dim CancelPostTransactionFlag As String
        Dim updateflag As String


        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Group Code", "Program Code", "Program Name", "SubModule", "Module", "Read", "Modify", "Delete", "Authorized", "Reverse", "Export", "Print", "Cancel", "QuickExport", "CancelPostTransaction", "isModifyonPassword", "isAmendment", "Update") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarPercentShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing-1 : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                    Dim strgcode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    If (String.IsNullOrEmpty(strgcode)) Or strgcode.Length > 12 Then
                        Throw New Exception("Group Code have some incorrect value or Blank")
                    End If
                    Dim strpcode As String = clsCommon.myCstr(grow.Cells("Program Code").Value)
                    If (String.IsNullOrEmpty(strpcode)) Or strpcode.Length > 12 Then
                        Throw New Exception(" Program Code have some incorrect value")
                    End If

                    Dim strread As String = clsCommon.myCstr(grow.Cells("Read").Value)
                    If clsCommon.CompairString(strread, "yes") = CompairStringResult.Equal Then
                        readflag = "1"
                    ElseIf clsCommon.CompairString(strread, "no") = CompairStringResult.Equal Then
                        readflag = "0"
                    Else
                        Throw New Exception(" Check the value for Read Flag")
                    End If

                    Dim strmodify As String = clsCommon.myCstr(grow.Cells("Modify").Value)
                    If clsCommon.CompairString(strmodify, "yes") = CompairStringResult.Equal Then
                        modifyflag = "1"
                    ElseIf clsCommon.CompairString(strmodify, "no") = CompairStringResult.Equal Then
                        modifyflag = "0"
                    Else
                        Throw New Exception(" Check the value for Modify Flag")
                    End If

                    Dim strdelete As String = clsCommon.myCstr(grow.Cells("Delete").Value)
                    If clsCommon.CompairString(strdelete, "yes") = CompairStringResult.Equal Then
                        deleteflag = "1"
                    ElseIf clsCommon.CompairString(strdelete, "no") = CompairStringResult.Equal Then
                        deleteflag = "0"
                    Else
                        Throw New Exception(" Check the value for Delete Flag")
                    End If

                    Dim strauth As String = clsCommon.myCstr(grow.Cells("Authorized").Value)
                    If clsCommon.CompairString(strauth, "yes") = CompairStringResult.Equal Then
                        authflag = "1"
                    ElseIf clsCommon.CompairString(strauth, "no") = CompairStringResult.Equal Then
                        authflag = "0"
                    Else
                        Throw New Exception(" Check the value for Authorized Flag")
                    End If






                    Dim strReverse As String = clsCommon.myCstr(grow.Cells("Reverse").Value)
                    If clsCommon.CompairString(strReverse, "yes") = CompairStringResult.Equal Then
                        ReverseFlag = "1"
                    ElseIf clsCommon.CompairString(strReverse, "no") = CompairStringResult.Equal Then
                        ReverseFlag = "0"
                    Else
                        Throw New Exception(" Check the value for Reverse Flag")
                    End If

                    Dim strExport As String = clsCommon.myCstr(grow.Cells("Export").Value)
                    If clsCommon.CompairString(strExport, "yes") = CompairStringResult.Equal Then
                        ExportFlag = "1"
                    ElseIf clsCommon.CompairString(strExport, "no") = CompairStringResult.Equal Then
                        ExportFlag = "0"
                    Else
                        Throw New Exception(" Check the value for Export Flag")
                    End If

                    Dim strPrint As String = clsCommon.myCstr(grow.Cells("Print").Value)
                    If clsCommon.CompairString(strPrint, "yes") = CompairStringResult.Equal Then
                        PrintFlag = "1"
                    ElseIf clsCommon.CompairString(strPrint, "no") = CompairStringResult.Equal Then
                        PrintFlag = "0"
                    Else
                        Throw New Exception(" Check the value for Print Flag")
                    End If
                    Dim strQuickExport As String = clsCommon.myCstr(grow.Cells("QuickExport").Value)
                    If clsCommon.CompairString(strQuickExport, "yes") = CompairStringResult.Equal Then
                        strQuickExport = "1"
                    ElseIf clsCommon.CompairString(strQuickExport, "no") = CompairStringResult.Equal Then
                        strQuickExport = "0"
                    Else
                        Throw New Exception(" Check the value for Quick Export Flag")
                    End If

                    Dim strModifyonPwd As String = clsCommon.myCstr(grow.Cells("isModifyonPassword").Value)
                    If clsCommon.CompairString(strModifyonPwd, "yes") = CompairStringResult.Equal Then
                        strModifyonPwd = "1"
                    ElseIf clsCommon.CompairString(strModifyonPwd, "no") = CompairStringResult.Equal Then
                        strModifyonPwd = "0"
                    Else
                        Throw New Exception(" Check the value for Modify on Password  Flag")
                    End If

                    Dim strAmendment As String = clsCommon.myCstr(grow.Cells("isAmendment").Value)
                    If clsCommon.CompairString(strAmendment, "yes") = CompairStringResult.Equal Then
                        strAmendment = "1"
                    ElseIf clsCommon.CompairString(strAmendment, "no") = CompairStringResult.Equal Then
                        strAmendment = "0"
                    Else
                        Throw New Exception(" Check the value for Amendment  Flag")
                    End If

                    Dim strCancel As String = clsCommon.myCstr(grow.Cells("Cancel").Value)
                    If clsCommon.CompairString(strCancel, "yes") = CompairStringResult.Equal Then
                        CancelFlag = "1"
                    ElseIf clsCommon.CompairString(strCancel, "no") = CompairStringResult.Equal Then
                        CancelFlag = "0"
                    Else
                        Throw New Exception(" Check the value for Cancel Flag")
                    End If

                    Dim strCancelPostTransaction As String = clsCommon.myCstr(grow.Cells("CancelPostTransaction").Value)
                    If clsCommon.CompairString(strCancelPostTransaction, "yes") = CompairStringResult.Equal Then
                        CancelPostTransactionFlag = "1"
                    ElseIf clsCommon.CompairString(strCancelPostTransaction, "no") = CompairStringResult.Equal Then
                        CancelPostTransactionFlag = "0"
                    Else
                        Throw New Exception(" Check the value for Cancel Post Transaction Flag")
                    End If

                    Dim strupdate As String = clsCommon.myCstr(grow.Cells("Update").Value)
                    If clsCommon.CompairString(strupdate, "yes") = CompairStringResult.Equal Then
                        updateflag = "1"
                    ElseIf clsCommon.CompairString(strupdate, "no") = CompairStringResult.Equal Then
                        updateflag = "0"
                    Else
                        Throw New Exception(" Check the value for Modify Flag")
                    End If

                    Dim sql1 As String = "select count(*) from tspl_Group_Program_Mapping where group_code='" + strgcode + "' and program_code='" + strpcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_GroupProgramMapping_insert", New SqlParameter("@groupcode", strgcode), New SqlParameter("@programcode", strpcode), New SqlParameter("@read", readflag), New SqlParameter("@modify", modifyflag), New SqlParameter("@delete", deleteflag), New SqlParameter("@auth", authflag), New SqlParameter("@createby", userCode), New SqlParameter("@createdate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@companycode", companyCode), New SqlParameter("@Reverse_Flag", ReverseFlag), New SqlParameter("@Export_Flag", ExportFlag), New SqlParameter("@Print_Flag", PrintFlag), New SqlParameter("@QucikExport_Flag", strQuickExport), New SqlParameter("@Cancel_Flag", CancelFlag), New SqlParameter("@Cancel_Flag_After_Posting", CancelPostTransactionFlag))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_GroupProgramMapping_update", New SqlParameter("@groupcode", strgcode), New SqlParameter("@programcode", strpcode), New SqlParameter("@read", readflag), New SqlParameter("@modify", modifyflag), New SqlParameter("@delete", deleteflag), New SqlParameter("@auth", authflag), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@companycode", companyCode), New SqlParameter("@Reverse_Flag", ReverseFlag), New SqlParameter("@Export_Flag", ExportFlag), New SqlParameter("@Print_Flag", PrintFlag), New SqlParameter("@QucikExport_Flag", strQuickExport), New SqlParameter("@Cancel_Flag", CancelFlag), New SqlParameter("@Cancel_Flag_After_Posting", CancelPostTransactionFlag))
                    End If
                    Dim sqlUpdate As String = "Update TSPL_GROUP_PROGRAM_MAPPING set Update_flag='" & updateflag & "' where group_code='" + strgcode + "' and program_code='" + strpcode + "'"
                    connectSql.RunSqlTransaction(trans, sqlUpdate)
                    'clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing-2 : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                    'Dim coll As New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "Reverse_Flag", ReverseFlag)
                    'clsCommon.AddColumnsForChange(coll, "Export_Flag", ExportFlag)
                    'clsCommon.AddColumnsForChange(coll, "Cancel_Flag", CancelFlag)
                    'clsCommon.AddColumnsForChange(coll, "Cancel_Flag_After_Posting", CancelPostTransactionFlag)
                    'clsCommon.AddColumnsForChange(coll, "QucikExport_Flag", strQuickExport)
                    'Dim result As Boolean = clsCommonFunctionality.UpdateDataTable(coll, "tspl_Group_Program_Mapping", OMInsertOrUpdate.Update, "group_code='" + strgcode + "' and program_code='" + strpcode + "'", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub


    


    Private Sub fndgroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndgroup._MYValidating
        'If isButtonClicked Then
        Dim qry As String = "select group_Code As [GroupCode],group_desc  as [Group Name] from TSPL_User_Group_Master "
        fndgroup.Value = clsCommon.ShowSelectForm("fmGroup_Code", qry, "GroupCode", "", fndgroup.Value, "", isButtonClicked)
        txtname.Text = clsDBFuncationality.getSingleValue("select group_desc from TSPL_User_Group_Master where group_Code= '" + fndgroup.Value + "'")
        gridload()
        txtDashBoardMult.arrValueMember = DashBoardload()
        dgvprogram.AutoGenerateColumns = False
        ToolTipnew.SetToolTip(btnnew, "New")
        textchangedsub()
        ' keypress()
        fndleave()
        btndelete.Enabled = False

        If fndgroup.MyReadOnly OrElse fndgroup.Value IsNot Nothing Then
            Dim qry1 As String = "Select * from TSPL_User_Group_Master where group_Code ='" + fndgroup.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If dt.Rows.Count <= 0 Then
                txtname.Text = ""
                btnsave.Text = "Save"
                gridload()
            Else
                gridload()
                dgvprogram.AutoGenerateColumns = False
                ToolTipnew.SetToolTip(btnnew, "New")
                textchangedsub()
                ' keypress()
                fndleave()
                btndelete.Enabled = False
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
                qst += "and group_code in (select min(group_code) from TSPL_User_Group_Master where group_code>'" + fndgroup.Value + "' ) "

                'qst += "and job_code in (select min(job_code) from job_master where job_code>'" + txtcode1.Value + "' and assign_to='" + txtassign.Value + "') "
            Case NavigatorType.First
                qst += "and group_code in (select MIN(group_code) from TSPL_User_Group_Master )"

            Case NavigatorType.Last
                qst += "and group_code in (select Max(group_code) from TSPL_User_Group_Master  )"
            Case NavigatorType.Previous
                qst += "and group_code in (select max(group_code) from TSPL_User_Group_Master where group_code<'" + fndgroup.Value + "'  )"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndgroup.Value = clsCommon.myCstr(dt.Rows(0)("Group Code"))
            txtname.Text = clsCommon.myCstr(dt.Rows(0)("Group Name"))
        End If
        '    obj = New Task_Information()
        '    obj.txtassign.Value = clsCommon.myCstr(dt.Rows(0)("assign_to")
        gridload()
        txtDashBoardMult.arrValueMember = DashBoardload()
        dgvprogram.AutoGenerateColumns = False
        ToolTipnew.SetToolTip(btnnew, "New")
        textchangedsub()
        ' keypress()
        fndleave()
        btndelete.Enabled = False
        btnsave.Enabled = True

    End Sub

    Private Function DashBoardload() As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "Select TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING.* from TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING Where Group_Code='" + fndgroup.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Dashboard_Code")))
            Next
        End If
        Return arr
    End Function

    Private Sub cboModule_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboModule.SelectedValueChanged
        LoadSubModule()
    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadSubModule()
    End Sub

    Private Sub cboSubModule_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboSubModule.Validating, cboModule.Validating
        gridload()
        funfill()
    End Sub




    Private Sub gbprogrammapping_Click(sender As Object, e As EventArgs) Handles gbprogrammapping.Click

    End Sub

    Private Sub ddl_type_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddl_type.SelectedIndexChanged
        If dgvprogram.Columns.Count > 0 Then
            If ddl_type.SelectedItems.Count > 0 AndAlso clsCommon.myLen(ddl_type.SelectedValue) > 0 Then
                If chk_select.Checked Then
                    For i As Integer = 0 To dgvprogram.Rows.Count - 1
                        dgvprogram.Rows(i).Cells(ddl_type.SelectedValue).Value = True
                    Next
                Else
                    For i As Integer = 0 To dgvprogram.Rows.Count - 1
                        dgvprogram.Rows(i).Cells(ddl_type.SelectedValue).Value = False

                    Next
                End If
            End If
        End If
    End Sub

    Private Sub chk_select_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chk_select.ToggleStateChanged
        If dgvprogram.Columns.Count > 0 Then
            If ddl_type.SelectedItems.Count > 0 AndAlso clsCommon.myLen(ddl_type.SelectedValue) > 0 Then
                If chk_select.Checked Then
                    For i As Integer = 0 To dgvprogram.Rows.Count - 1
                        dgvprogram.Rows(i).Cells(ddl_type.SelectedValue).Value = True
                    Next
                Else
                    For i As Integer = 0 To dgvprogram.Rows.Count - 1
                        dgvprogram.Rows(i).Cells(ddl_type.SelectedValue).Value = False

                    Next
                End If
            End If
        End If
    End Sub

    Private Sub btnNewHistory_Click(sender As Object, e As EventArgs) Handles btnNewHistory.Click
        Try
            If clsCommon.myLen(fndgroup.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            For i As Integer = 0 To dgvprogram.Rows.Count - 1
                If dgvprogram.Rows.Count > i Then
                    clsERPFuncationalityOLD.ShowHistoryData(fndgroup.Value, "Group_Code", "TSPL_GROUP_PROGRAM_MAPPING", Nothing, Nothing, "Program Code", selectedValue)
                    Exit For

                End If

            Next

            'clsERPFuncationalityOLD.ShowHistoryData(fndgroup.Value, "Group_Code", "TSPL_GROUP_PROGRAM_MAPPING", Nothing, Nothing, Nothing, selectedCellValue)

            'clsERPFuncationalityOLD.ShowTransHistoryData(fndgroup.Value, "Group_Code", "TSPL_GROUP_PROGRAM_MAPPING", "TSPL_GROUP_PROGRAM_MAPPING")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub dgvprogram_CellClick(sender As Object, e As GridViewCellEventArgs) Handles dgvprogram.CellDoubleClick
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                selectedValue = clsCommon.myCstr(dgvprogram.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub txtDashBoardMult__My_Click(sender As Object, e As EventArgs) Handles txtDashBoardMult._My_Click
        Try
            Dim qry = "select distinct Code,Name from TSPL_DASHBOARD_REPORT "
            txtDashBoardMult.arrValueMember = clsCommon.ShowMultipleSelectForm("FND@DASHBOARD", qry, "Code", "Name", txtDashBoardMult.arrValueMember, txtDashBoardMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

Public Class CustomCheckBoxColumn1
    Inherits GridViewCheckBoxColumn
    Public Overrides Function GetCellType(ByVal row As GridViewRowInfo) As Type
        If TypeOf row Is GridViewTableHeaderRowInfo Then
            Return GetType(CheckBoxHeaderCell)
        End If
        Return MyBase.GetCellType(row)
    End Function

End Class
