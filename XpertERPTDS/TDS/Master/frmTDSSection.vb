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
Imports System.Globalization
Imports System.Threading
Imports common
Imports XpertERPEngine
'created by --> Vipin
'createddate --> 17/06/2011
'modifiedby --> Vipin
'Modified date -->17/06/2011
'Tables Used --> TSPL_TDS_SECTION_MASTER
'--preeti gupta..ticket no.[BM00000003134]

Public Class frmTDSSection
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TDSSection)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            menuImport.Enabled = True
            Export.Enabled = True
        Else
            menuImport.Enabled = False
            Export.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmTDSSection_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    'Main form load
    Private Sub frmTDSSection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fndTDSSection.txtValue, txtdes)
        ToolTipTDSSection.SetToolTip(btnnew, "New")
        fndTdsSectionNew.MyMaxLength = 12
        fndTdsSectionNew.MyReadOnly = False
        'AddHandler fndTDSSection.txtValue.TextChanged, AddressOf fndTDSSection_text_changed
        'AddHandler fndTDSSection.txtValue.KeyPress, AddressOf fndTDSSection_key_press
        fndTdsSectionNew.MyCharacterCasing = CharacterCasing.Upper
        btndelete.Enabled = False
        btnsave.Enabled = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndTdsSectionNew.Value = clsCommon.myCstr(Me.Tag)
            Loaddata()
        End If
    End Sub

    'This will check the value of finder in the database ,if value exist in database then it will call funfill function.
    'Public Sub fndTDSSection_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Loaddata()
    'End Sub
    Public Sub Loaddata()
        Try
            Dim str As String = "select tds_group from TSPL_TDS_SECTION_MASTER where tds_group = '" + fndTdsSectionNew.Value + "'"
            'Dim dr As SqlDataReader
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            'dr = connectSql.RunSqlReturnDR(str)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Then
                funfill()
            Else
                txtdes.Text = ""
                txtreport.Text = ""
                chkcommulative.Checked = False
                chkintax.Checked = False
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    'Public Sub fndTDSSection_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If

    'End Sub
    ''-------------- Added by  Abhishek as on 4 june 2012--------
    'Private Sub fndTDSSection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndTDSSection.Load
    '    fndTDSSection.ConnectionString = connectSql.SqlCon()
    '    fndTDSSection.Query = "select tds_group as [TDS Groups],description as [Description] from TSPL_TDS_SECTION_MASTER "
    '    fndTDSSection.ValueToSelect = "TDS Groups"
    '    fndTDSSection.Caption = "TDS Section Master"
    '    fndTDSSection.ValueToSelect1 = "Description"
    'End Sub

    Private Sub fndTdsSectionNew__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndTdsSectionNew._MYNavigator
        Dim qry As String = "select tds_group as [Code],description as [Description] from TSPL_TDS_SECTION_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_TDS_SECTION_MASTER   .tds_group  in ('" + fndTdsSectionNew.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_TDS_SECTION_MASTER   .tds_group  in (select min(tds_group ) from TSPL_TDS_SECTION_MASTER  where tds_group  >'" + fndTdsSectionNew.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_TDS_SECTION_MASTER  .tds_group  in (select MIN(tds_group ) from TSPL_TDS_SECTION_MASTER )"

            Case NavigatorType.Last
                qry += " and TSPL_TDS_SECTION_MASTER   .tds_group  in (select Max(tds_group ) from TSPL_TDS_SECTION_MASTER )"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_SECTION_MASTER  .tds_group  in (select Max(tds_group  ) from TSPL_TDS_SECTION_MASTER  where tds_group  <'" + fndTdsSectionNew.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndTdsSectionNew.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
        End If
        Loaddata()
    End Sub

    Private Sub fndTdsSectionNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTdsSectionNew._MYValidating
        Dim Qry As String = " Select Count(*) From TSPL_TDS_SECTION_MASTER where tds_group='" & fndTdsSectionNew.Value & "'"
        Dim Count As String = clsDBFuncationality.getSingleValue(Qry)
        If Count = 0 Then
            fndTdsSectionNew.MyReadOnly = False
        Else
            fndTdsSectionNew.MyReadOnly = True
        End If
        If fndTdsSectionNew.MyReadOnly Or isButtonClicked Then

            'Dim qry1 As String = "select tds_group as [Code],description as [Description] from TSPL_TDS_SECTION_MASTER "
            'fndTdsSectionNew.Value = clsCommon.ShowSelectForm("TdsGroupSec", qry1, "Code", "", fndTdsSectionNew.Value, "Code", isButtonClicked)
            fndTdsSectionNew.Value = clsTDSSection.getFinder("", fndTdsSectionNew.Value, isButtonClicked)
            Loaddata()
        End If
                                End Sub

    Private Sub fndTdsSectionNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndTdsSectionNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If

    End Sub

#Region "Function"

    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try
            Dim str As String = "select description,report_Section,cumulative_cutoff,include_tax from TSPL_TDS_SECTION_MASTER where tds_group = '" + fndTdsSectionNew.Value + "'"
            Dim dr As DataTable = clsDBFuncationality.GetDataTable(str)
            'dr = connectSql.RunSqlReturnDR(str)
            'While dr.Read()
            If dr.Rows.Count > 0 Then
                For ii As Integer = 0 To dr.Rows.Count - 1
                    txtdes.Text = dr.Rows(ii)("description").ToString()
                    txtreport.Text = dr.Rows(ii)("report_Section").ToString()
                    Dim strcumulative As String = dr.Rows(ii)("cumulative_cutoff").ToString()
                    If strcumulative = "Y" Then
                        chkcommulative.Checked = True
                    ElseIf strcumulative = "N" Then
                        chkcommulative.Checked = False
                    End If

                    Dim strintax As String = dr.Rows(ii)("include_tax").ToString()
                    If strintax = "Y" Then
                        chkintax.Checked = True
                    ElseIf strintax = "N" Then
                        chkintax.Checked = False
                    End If

                Next
            End If
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
            Dim strchk As String = ""
            If chkcommulative.Checked = True Then
                strchk = "Y"
            ElseIf chkcommulative.Checked = False Then
                strchk = "N"
            End If
            Dim strchktax As String = ""
            If chkintax.Checked = True Then
                strchktax = "Y"
            ElseIf chkintax.Checked = False Then
                strchktax = "N"
            End If
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TDS_SECTION_MASTER where tds_group='" & fndTdsSectionNew.Value & "'")
                If ChkNewEntry = 0 Then
                    fndTdsSectionNew.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.TDSSection, "", "")
                    If clsCommon.myLen(fndTdsSectionNew.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("SP_TDS_SECTION_INSERT", New SqlParameter("@tds_group", fndTdsSectionNew.Value), New SqlParameter("@description", txtdes.Text.ToString()), New SqlParameter("@report", txtreport.Text.ToString()), New SqlParameter("@cumulative", strchk), New SqlParameter("@includetax", strchktax), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
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
            Dim strchk As String = ""
            If chkcommulative.Checked = True Then
                strchk = "Y"
            ElseIf chkcommulative.Checked = False Then
                strchk = "N"
            End If
            Dim strchktax As String = ""
            If chkintax.Checked = True Then
                strchktax = "Y"
            ElseIf chkintax.Checked = False Then
                strchktax = "N"
            End If
            connectSql.RunSp("SP_TDS_SECTION_UPDATE", New SqlParameter("@tds_group", fndTdsSectionNew.Value), New SqlParameter("@description", txtdes.Text.ToString()), New SqlParameter("@report", txtreport.Text.ToString()), New SqlParameter("@cumulative", strchk), New SqlParameter("@includetax", strchktax), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            connectSql.RunSp("SP_TDS_SECTION_DELETE", New SqlParameter("@tds_group", fndTdsSectionNew.Value))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        fndTdsSectionNew.Value = ""
        fndTdsSectionNew.MyReadOnly = False
        txtdes.Text = ""
        txtreport.Text = ""
        chkcommulative.Checked = False                      
        chkintax.Checked = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndTdsSectionNew.MyReadOnly = False
    End Sub


    'This function is for Export functionality
    Public Sub funexport()
        Dim str As String
        str = "select tds_group as [TDS Group],description as [Description],report_Section as [Report Section],cumulative_cutoff as [Cumulative Cutoff],include_tax as [Include Tax] from TSPL_TDS_SECTION_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub
    'This function is for Import functionality
    Public Sub funimport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "TDS Group", "Description", "Report Section", "Cumulative Cutoff", "Include Tax") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)

                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("TDS Group can not be blank or Check the length")
                    End If

                    If (String.IsNullOrEmpty(strdes)) Or clsCommon.myLen(strdes) > 95 Then
                        Throw New Exception(" Description  can not be blank or Check the length")
                    End If

                    Dim strreport As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(strreport) > 95 Then
                        Throw New Exception(" Check the length of Report Section")
                    End If

                    Dim strcumulative As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If (strcumulative = "Y" Or strcumulative = "N") Then
                    Else
                        Throw New Exception("Cumulative should be  Y or N")
                    End If

                    Dim strintax As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If (strintax = "Y" Or strintax = "N") Then
                    Else
                        Throw New Exception("Include Tax should be  Y or N")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_TDS_SECTION_MASTER where tds_group='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "SP_TDS_SECTION_INSERT", New SqlParameter("@tds_group", strcode), New SqlParameter("@description", strdes), New SqlParameter("@report", strreport), New SqlParameter("@cumulative", strcumulative), New SqlParameter("@includetax", strintax), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TDS_SECTION_UPDATE", New SqlParameter("@tds_group", strcode), New SqlParameter("@description", strdes), New SqlParameter("@report", strreport), New SqlParameter("@cumulative", strcumulative), New SqlParameter("@includetax", strintax), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

#End Region

#Region "Buttons Events"
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndTdsSectionNew.Value = "" Then
                myMessages.blankValue(Me, "TDS Group", Me.Text)
                fndTdsSectionNew.Focus()
            ElseIf txtdes.Text = "" Then
                myMessages.blankValue(Me, "Description", Me.Text)
                txtdes.Focus()

            Else

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.TDSSection, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
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

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndTdsSectionNew.Value = "" Then
            myMessages.blankValue(Me, "TDS Group", Me.Text)
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


    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        funexport()
    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        funimport()
    End Sub
#End Region

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TDS-SECTION"
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





End Class
