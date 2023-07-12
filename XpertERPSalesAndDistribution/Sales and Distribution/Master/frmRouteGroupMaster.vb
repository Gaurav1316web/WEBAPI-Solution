'Created By---> Mayank
'Created Date--->25/may/2011
'Modified By--> Mayank
'Last Modify Date-->03/june/2011
'Tables Used-->TSPL_ROUTE_GROUP_MASTER,TSPL_Route_Master
'--preeti gupta-ticket no.[BM00000003133]
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Threading
Imports common

Public Class frmRouteGroupMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub GroupMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N Adding New ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        SetDataBaseGrid()
        SetUserMgmtNew()
        rbtnDelete.Enabled = False
        fndRoute_id_Leave()
        text_changed1()
        ' globalFunc.mandatoryText(fndRoute_id.txtValue, fndGroup_Id.txtValue, rtxtDescription)
        ' AddHandler fndRoute_id.txtValue.Leave, AddressOf fndRoute_id_Leave
        ' AddHandler fndGroup_Id.ValueChanged, AddressOf text_changed1
        '  AddHandler fndRoute_id.txtValue.KeyPress, AddressOf fndRoute_id_KeyPress
        '  AddHandler fndGroup_Id.txtValue.KeyPress, AddressOf fndGroup_Id_KeyPress
        '  fndGroup_Id.txtValue.CharacterCasing = CharacterCasing.Upper
        ' fndGroup_Id.txtValue.CharacterCasing = CharacterCasing.Upper
        ToolTipgp_master.SetToolTip(rbtnReset, "New")
        '    fndGroup_Id.txtValue.MaxLength = 15
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        dtpStart_date.Value = Date.Today()
        'dtpEnd_date.Value = Date.Today()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        SetLength()
    End Sub

    Private Sub SetLength()
        fndGroup_Id.MyMaxLength = 12
        rtxtDescription.MaxLength = 50
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("GRP-ROUTE-M")
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    'It Is Used To Fill Or Clear All Fields of Current Windows Form Bassed On Group Id(fndGroup_id) From TSPL_ROUTE_GROUP_MASTER
    Sub text_changed1()
        Dim strGroup_Id As String = "select Group_Id from TSPL_ROUTE_GROUP_MASTER where Group_Id='" + fndGroup_Id.Value + "'"
        'Dim dr As SqlDataReader
        'dr = connectSql.RunSqlReturnDR(strGroup_Id)
        Dim strcheck As String = clsDBFuncationality.getSingleValue(strGroup_Id)
        'If dr.Read() Then
        '    strcheck = dr(0).ToString()
        'End If
        If (strcheck <> "") Then
            funFill()
        Else
            rbtnActive.IsChecked = False
            btn_Close.IsChecked = False
            fndRoute_id.Value = ""
            dtpStart_date.Value = Date.Today
            'dtpEnd_date.Value = Date.Today
            rtxtDescription.Text = ""
            rchkMonday.Checked = False
            rchkTuesday.Checked = False
            rchkwednesday.Checked = False
            rchkThursday.Checked = False
            rchkfriday.Checked = False
            rchksaturday.Checked = False
            rchkSunday.Checked = False
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
            fndRoute_id.Enabled = True
        End If

    End Sub
    Public Sub text_changed1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'It Is Used To Fill The Route No From TSPL_Route_Master
    Private Sub fndRoute_id_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndRoute_id.ConnectionString = connectSql.SqlCon()
        'fndRoute_id.Query = "select distinct Route_No as [Route No],Route_Desc as [Route Desc],Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District,Length from TSPL_Route_Master"
        'fndRoute_id.ValueToSelect = "Route No"
        'fndRoute_id.Caption = "Route Master"
        'fndRoute_id.ValueToSelect1 = "Route Desc"
        'fndRoute_id.txtValue.MaxLength = 8
    End Sub
    'It Is Used To Save And Update All Record To TSPL_ROUTE_GROUP_MASTER
    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndGroup_Id.Value = "" Then
            myMessages.blankValue("Group Code")
            fndGroup_Id.Focus()
            Return
        ElseIf rbtnActive.IsChecked = False And btn_Close.IsChecked = False Then
            myMessages.blankValue("Status")
            rbtnActive.Focus()
        ElseIf fndRoute_id.Value = "" Then
            myMessages.blankValue("Route Code")
            fndRoute_id.Focus()
        ElseIf rtxtDescription.Text = "" Then
            myMessages.blankValue("Description")
            rtxtDescription.Focus()
        ElseIf rbtnSave.Text = "Save" Then
            funInsert()
        Else
            funUpdate()
        End If
    End Sub
    'It is Used To Clear All Fields Of Current Windows Form
    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        funReset()
    End Sub
    'It Is Used To Fill The Group Id From TSPL_ROUTE_GROUP_MASTER
    Private Sub fndGroup_Id_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndGroup_Id.ConnectionString = connectSql.SqlCon()
        'fndGroup_Id.Query = "select distinct Group_Id as [Group Id] ,Status,Route_Code as [Route Code],Description from  TSPL_ROUTE_GROUP_MASTER"
        'fndGroup_Id.ValueToSelect = "Group Id"
        'fndGroup_Id.Caption = "Group Master"
        'fndGroup_Id.ValueToSelect1 = "Description"
        'fndGroup_Id.txtValue.MaxLength = 12
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub rbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnclose.Click
        Me.Close()
    End Sub
    'It Is Used To Delete The Record From TSPL_ROUTE_GROUP_MASTER
    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndGroup_Id.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            funDelete()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
        End If
    End Sub

    'This is Reset Function Used To Clear All Fields Of Current Windows Form
    Private Sub funReset()
        Try
            fndGroup_Id.Value = ""
            fndGroup_Id.MyReadOnly = False
            rbtnActive.IsChecked = False
            btn_Close.IsChecked = False
            fndRoute_id.Value = ""
            dtpStart_date.Value = Date.Today
            ' dtpEnd_date.Value = Date.Today
            rtxtDescription.Text = ""
            rchkMonday.Checked = False
            rchkTuesday.Checked = False
            rchkwednesday.Checked = False
            rchkThursday.Checked = False
            rchkfriday.Checked = False
            rchksaturday.Checked = False
            rchkSunday.Checked = False
            rbtnSave.Text = "Save"
            fndRoute_id.Enabled = True
            rbtnDelete.Enabled = False
            SetDataBaseGrid()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Delete Function Used To Delete Records From TSPL_ROUTE_GROUP_MASTER
    Private Sub funDelete()
        Try
            clsDBFuncationality.SaveAStorePorcedure("SP_TSPL_ROUTE_GROUP_MASTER_DELETE", New SqlParameter("@Group_Id", fndGroup_Id.Value))
            myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Insert Function Used To Insert Values In TSPL_ROUTE_GROUP_MASTER
    Private Sub funInsert()
        Try

            Dim status As Char
            If rbtnActive.IsChecked = True Then
                status = "T"
            Else
                status = "F"
            End If


            Dim mon As Char
            If rchkMonday.Checked = True Then
                mon = "T"
            Else
                mon = "F"
            End If


            Dim tuesday As Char
            If rchkTuesday.Checked = True Then
                tuesday = "T"
            Else
                tuesday = "F"
            End If


            Dim wed As Char
            If rchkwednesday.Checked = True Then
                wed = "T"
            Else
                wed = "F"
            End If


            Dim thursday As Char
            If rchkThursday.Checked = True Then
                thursday = "T"
            Else
                thursday = "F"
            End If


            Dim friday As Char
            If rchkfriday.Checked = True Then
                friday = "T"
            Else
                friday = "F"
            End If


            Dim sat As Char
            If rchksaturday.Checked = True Then
                sat = "T"
            Else
                sat = "F"
            End If


            Dim sun As Char
            If rchkSunday.Checked = True Then
                sun = "T"
            Else
                sun = "F"
            End If
            Dim strsdate As String = Format(dtpStart_date.Value, "dd/MM/yyyy")
            'Dim stredate As String = Format(dtpEnd_date.Value, "dd/MM/yyyy")
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ROUTE_GROUP_MASTER where Group_Id='" & fndGroup_Id.Value & "'")
                If ChkNewEntry = 0 Then
                    fndGroup_Id.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.RouteGroupMaster, "", "")
                    If clsCommon.myLen(fndGroup_Id.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            clsDBFuncationality.SaveAStorePorcedure("SP_TSPL_ROUTE_GROUP_MASTER_INSERT", New SqlParameter("@Group_Id", fndGroup_Id.Value), New SqlParameter("@Status", status), New SqlParameter("@Route_Code", fndRoute_id.Value), New SqlParameter("@Start_date", strsdate), New SqlParameter("@Monday", mon), New SqlParameter("@Tuesday", tuesday), New SqlParameter("@Wednesday", wed), New SqlParameter("@Thursday", thursday), New SqlParameter("@Friday", friday), New SqlParameter("@Saturday", sat), New SqlParameter("@Sunday", sun), New SqlParameter("@Description", rtxtDescription.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.insert()
            rbtnSave.Text = "Update"
            rbtnDelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funFill()
        Try
            Dim strQuery As String = "select Status,Route_Code,Start_date,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,Description from TSPL_ROUTE_GROUP_MASTER where Group_Id='" + fndGroup_Id.Value + "'"
            Dim dr As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            ' dr = connectSql.RunSqlReturnDR(strQuery)
            'If dr.Read() Then
            If dr.Rows.Count > 0 Then
                Dim Status As Char
                Status = dr.Rows(0)("Status").ToString()
                If Status = "T" Then
                    rbtnActive.IsChecked = True
                Else
                    btn_Close.IsChecked = True
                End If

                fndRoute_id.Value = dr.Rows(0)("Route_Code").ToString()
                dtpStart_date.Value = CDate(dr.Rows(0)("Start_date").ToString())
                'dtpEnd_date.Value = CDate(dr(3).ToString())


                Dim mon As Char
                mon = dr.Rows(0)("Monday").ToString()
                If mon = "T" Then
                    rchkMonday.Checked = True
                Else
                    rchkMonday.Checked = False
                End If


                Dim tue As Char
                tue = dr.Rows(0)("Tuesday").ToString()
                If tue = "T" Then
                    rchkTuesday.Checked = True
                Else
                    rchkTuesday.Checked = False
                End If


                Dim wed As Char
                wed = dr.Rows(0)("Wednesday").ToString()
                If wed = "T" Then
                    rchkwednesday.Checked = True
                Else
                    rchkwednesday.Checked = False
                End If


                Dim thu As Char
                thu = dr.Rows(0)("Thursday").ToString()
                If thu = "T" Then
                    rchkThursday.Checked = True
                Else
                    rchkThursday.Checked = False
                End If


                Dim fri As Char
                fri = dr.Rows(0)("Friday").ToString()
                If fri = "T" Then
                    rchkfriday.Checked = True
                Else
                    rchkfriday.Checked = False
                End If


                Dim sat As Char
                sat = dr.Rows(0)("Saturday").ToString()
                If sat = "T" Then
                    rchksaturday.Checked = True
                Else
                    rchksaturday.Checked = False
                End If


                Dim sun As Char
                sun = dr.Rows(0)("Sunday").ToString()
                If sun = "T" Then
                    rchkSunday.Checked = True
                Else
                    rchkSunday.Checked = False
                End If


                rtxtDescription.Text = dr.Rows(0)("Description").ToString()

                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
                fndRoute_id.Enabled = True
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Delete Function Used To Delete Records From TSPL_ROUTE_GROUP_MASTER
    Private Sub funUpdate()
        Try
            Dim status As Char
            If rbtnActive.IsChecked = True Then
                status = "T"
            Else
                status = "F"
            End If


            Dim mon As Char
            If rchkMonday.Checked = True Then
                mon = "T"
            Else
                mon = "F"
            End If


            Dim tuesday As Char
            If rchkTuesday.Checked = True Then
                tuesday = "T"
            Else
                tuesday = "F"
            End If


            Dim wed As Char
            If rchkwednesday.Checked = True Then
                wed = "T"
            Else
                wed = "F"
            End If


            Dim thursday As Char
            If rchkThursday.Checked = True Then
                thursday = "T"
            Else
                thursday = "F"
            End If


            Dim friday As Char
            If rchkfriday.Checked = True Then
                friday = "T"
            Else
                friday = "F"
            End If


            Dim sat As Char
            If rchksaturday.Checked = True Then
                sat = "T"
            Else
                sat = "F"
            End If


            Dim sun As Char
            If rchkSunday.Checked = True Then
                sun = "T"
            Else
                sun = "F"
            End If
            Dim strsdate As String = Format(dtpStart_date.Value, "dd/MM/yyyy")
            'Dim stredate As String = Format(dtpEnd_date.Value, "dd/MM/yyyy")
            clsDBFuncationality.SaveAStorePorcedure("SP_TSPL_ROUTE_GROUP_MASTER_UPDATE", New SqlParameter("@Group_Id", fndGroup_Id.Value), New SqlParameter("@Status", status), New SqlParameter("@Route_Code", fndRoute_id.Value), New SqlParameter("@Start_date", strsdate), New SqlParameter("@Monday", mon), New SqlParameter("@Tuesday", tuesday), New SqlParameter("@Wednesday", wed), New SqlParameter("@Thursday", thursday), New SqlParameter("@Friday", friday), New SqlParameter("@Saturday", sat), New SqlParameter("@Sunday", sun), New SqlParameter("@Description", rtxtDescription.Text), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Check The Value Of Finder(fndRoute_id),Is Present In Database Or Not
    Sub fndRoute_id_Leave()
        Dim strRoute_No As String = "select Route_No from TSPL_Route_Master where Route_No='" + fndRoute_id.Value + "'"
        'Dim dr As sqldatareader
        'dr = connectSql.RunSqlReturnDR(strRoute_No)
        Dim strcheck As String = clsDBFuncationality.getSingleValue(strRoute_No)
        'If dr.Read() Then
        '    strcheck = dr(0).ToString()
        'End If
        If (strcheck <> "" Or fndRoute_id.Value = "") Then
            fndRoute_id.Value = strcheck
        Else
            common.clsCommon.MyMessageBoxShow("This Route Code does not exist in master table")
            fndRoute_id.Value = ""
            fndRoute_id.Focus()
        End If
    End Sub
    Private Sub fndRoute_id_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'It Validate User To Press The Keys 
    Private Sub fndRoute_id_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    'It Validate User To Press The Keys 
    Private Sub fndGroup_Id_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    'It Is Used To Export The Records From TSPL_ROUTE_GROUP_MASTER
    Private Sub RadMenuItem_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Export.Click
        Dim Sql As String = "Select Group_Id as [Group Id],Status, Route_Code as [Route Code],Start_date as [Start date],Monday,Tuesday,Wednesday,Thursday ,Friday,Saturday,Sunday,Description from TSPL_ROUTE_GROUP_MASTER"
        transportSql.ExporttoExcel(Sql, Me)
    End Sub
    'It Is Used To Import The Records From TSPL_ROUTE_GROUP_MASTER
    Private Sub RadMenuItem_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Group Id", "Status", "Route Code", "Start date", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strsdate As String
                    ' Dim stredate As String
                    Dim strGroup_Id As String = grow.Cells(0).Value.ToString()
                    If String.IsNullOrEmpty(strGroup_Id) Or strGroup_Id.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Group Id can not be left blank or size can not be grater than 12")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strStatus As String = grow.Cells(1).Value.ToString()
                    If strStatus = "T" Then
                        strStatus = "T"
                    ElseIf strStatus = "F" Then
                        strStatus = "F"
                    Else
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Status")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strRouteCode As String = grow.Cells(2).Value.ToString()
                    If String.IsNullOrEmpty(strRouteCode) Or strRouteCode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Route Code can not be left blank or size can not be grater than 12")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim strs_date As String = grow.Cells(3).Value.ToString()
                    If String.IsNullOrEmpty(strs_date) Then
                        common.clsCommon.MyMessageBoxShow("Start Date has some incorrect values", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    Else
                        strsdate = clsCommon.GetPrintDate(strs_date)
                    End If
                    'Dim strE_date As String = grow.Cells(4).Value.ToString()
                    'If String.IsNullOrEmpty(strE_date) Then
                    '    common.clsCommon.MyMessageBoxShow("End Date has some incorrect values", Me.Text)
                    '    trans.Rollback()
                    '    Exit Sub
                    'Else
                    '    stredate = CDate(strE_date)
                    'End If
                    Dim strMonday As String = grow.Cells(4).Value.ToString()
                    If strMonday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Monday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strTuesday As String = grow.Cells(5).Value.ToString()
                    If strTuesday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Tuesday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strWednesday As String = grow.Cells(6).Value.ToString()
                    If strWednesday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Wednesday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strThursday As String = grow.Cells(7).Value.ToString()
                    If strThursday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Thursday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strFriday As String = grow.Cells(8).Value.ToString()
                    If strFriday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Friday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strSaturday As String = grow.Cells(9).Value.ToString()
                    If strSaturday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Saturday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strSunday As String = grow.Cells(10).Value.ToString()
                    If strSunday.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("You must enter the value either T or F for Sunday")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strdescription As String = grow.Cells(11).Value.ToString()
                    If String.IsNullOrEmpty(strdescription) Or strdescription.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("description can not be left blank or size can not be grater than 50")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strquery As String = "select count(*) from TSPL_ROUTE_GROUP_MASTER where Group_Id='" + strGroup_Id + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, strquery))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_ROUTE_GROUP_MASTER_INSERT", New SqlParameter("@Group_Id", strGroup_Id), New SqlParameter("@Status", strStatus), New SqlParameter("@Route_Code", strRouteCode), New SqlParameter("@Start_date", strsdate), New SqlParameter("@Monday", strMonday), New SqlParameter("@Tuesday", strTuesday), New SqlParameter("@Wednesday", strWednesday), New SqlParameter("@Thursday", strThursday), New SqlParameter("@Friday", strFriday), New SqlParameter("@Saturday", strSaturday), New SqlParameter("@Sunday", strSunday), New SqlParameter("@Description", strdescription), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_ROUTE_GROUP_MASTER_UPDATE", New SqlParameter("@Group_Id", strGroup_Id), New SqlParameter("@Status", strStatus), New SqlParameter("@Route_Code", strRouteCode), New SqlParameter("@Start_date", strsdate), New SqlParameter("@Monday", strMonday), New SqlParameter("@Tuesday", strTuesday), New SqlParameter("@Wednesday", strWednesday), New SqlParameter("@Thursday", strThursday), New SqlParameter("@Friday", strFriday), New SqlParameter("@Saturday", strSaturday), New SqlParameter("@Sunday", strSunday), New SqlParameter("@Description", strdescription), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub RadMenuItem_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Close.Click
        Me.Close()
    End Sub
    'It Is Used To Give The Authority To User,To Access This Form (Route Group Master) Or Not.(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "GRP-ROUTE-M"
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
    '            rbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rbtnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        Dim arrDBName As New List(Of String)
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub


    Private Sub frmRouteGroupMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub


    Private Sub fndGroup_Id__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndGroup_Id._MYValidating

        Dim str As String = "select count(*) from TSPL_ROUTE_GROUP_MASTER where Group_Id ='" + fndGroup_Id.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndGroup_Id.MyReadOnly = False
        Else
            fndGroup_Id.MyReadOnly = True
        End If
        If fndGroup_Id.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select distinct Group_Id as [GroupCode] ,Status,Route_Code as [Route Code],Description from  TSPL_ROUTE_GROUP_MASTER"
            fndGroup_Id.Value = clsCommon.ShowSelectForm("GrpCodMastrFND", qry, "GroupCode", "", fndGroup_Id.Value, "", isButtonClicked)
            If fndGroup_Id.Value IsNot Nothing Then
                rbtnDelete.Enabled = True
            Else
                rbtnDelete.Enabled = False
            End If

            text_changed1()
        End If
    End Sub

    Private Sub fndGroup_Id__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndGroup_Id._MYNavigator
        Dim qst As String = "select distinct Group_Id as [Group Code] ,Status,Route_Code as [Route Code],Description from  TSPL_ROUTE_GROUP_MASTER    where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and Group_Id in (select min(Group_Id) from TSPL_ROUTE_GROUP_MASTER where Group_Id>'" + fndGroup_Id.Value + "'   ) "
            Case NavigatorType.First
                qst += "and Group_Id in (select MIN(Group_Id) from TSPL_ROUTE_GROUP_MASTER  )"
            Case NavigatorType.Last
                qst += "and Group_Id in (select Max(Group_Id) from TSPL_ROUTE_GROUP_MASTER  )"
            Case NavigatorType.Previous
                qst += "and Group_Id in (select max(Group_Id) from TSPL_ROUTE_GROUP_MASTER where Group_Id<'" + fndGroup_Id.Value + "'   )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndGroup_Id.Value = clsCommon.myCstr(dt.Rows(0)("Group Code"))

        End If
        'TextChanged()
        If fndGroup_Id.Value IsNot Nothing Then
            rbtnDelete.Enabled = True
        Else
            rbtnDelete.Enabled = False

        End If

        text_changed1()
    End Sub

    Private Sub fndRoute_id__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRoute_id._MYValidating
        Dim qry As String = "select distinct Route_No as [Route No],Route_Desc as [Route Desc],Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District,Length from TSPL_Route_Master"
        fndRoute_id.Value = clsCommon.ShowSelectForm("GpCodeMasterFND", qry, "Route No", " Status='A' ", fndRoute_id.Value, "", isButtonClicked)
        fndRoute_id_Leave()
    End Sub
End Class
