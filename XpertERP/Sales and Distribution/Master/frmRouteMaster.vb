'Created By---> Mayank
'Created Date--->25/may/2011
'Modified By--> mayank
'Last Modify Date-->03/june/2011
'Tables Used-->TSPL_EMPLOYEE_MASTER ,TSPL_Route_Master,TSPL_ZONE_MASTER,TSPL_CITY_MASTER
'PREETI GUPTA TICKET NO[BM00000004749]
'=========BM00000007849===============
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class frmRouteMaster
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
    Private Sub RouteMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetDataBaseGrid()
        FunAddHandler()
        SetUserMgmtNew()
        ' globalFunc.mandatoryText(fndRouteid.txtValue, rtxtdescription, fndSalesman_code.txtValue, fndPriceCode.txtValue, txtpricecodedescription)
        '  globalFunc.mandatoryDropdown(ddltype, rddl_route_offday, rddl_category)

        ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+R for Print Preview")

        'fndRouteid.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndcity_id.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndDepot.txtValue.CharacterCasing = CharacterCasing.Upper
        ' fndvcode.txtValue.CharacterCasing = CharacterCasing.Upper
        'AddHandler fndDepot.txtValue.Leave, AddressOf fndDepot_textChanged
        'AddHandler fndZone_inid.txtValue.Leave, AddressOf fndZone_inid_Leave
        'AddHandler fndZone_inid.txtValue.KeyPress, AddressOf fndZone_inid_KeyPress
        '  fndRouteid.txtValue.MaxLength = 12
        rbtnDelete.Enabled = False
        fun_ddl_category()
        fun_ddl_Type()
        fun_ddl_Routeoffday()
        ToolTipGP_Route_Master.SetToolTip(rbtnReset, "New")
        rtxtSalesman_name.ReadOnly = True
        rdoAC.IsChecked = True

        dtpAcIn.Value = clsCommon.GETSERVERDATE()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.routeMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rbtnSave.Visible = True Then
            RadMenuItem_import.Enabled = True
            RadMenuItem_Export.Enabled = True
        Else
            RadMenuItem_import.Enabled = False
            RadMenuItem_Export.Enabled = False
        End If
        '--------------------------------------------------
        ' btnPost.Visible = MyBase.isPostFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FunAddHandler()
        text_changed()
        fndSalesman_code_Leave()
        text_changed1()
        fndcity_id_Leave()
        fndDepot_Leave()
        fndPriceCode_text_changed()
        fndPriceCode_leave()
        NonPrice_Textchanged()
        RoutePrice_Textchanged()
        fndvcode_text_changed()
        fndvcode_leave()
        ' AddHandler fndRouteid.ValueChanged, AddressOf text_changed
        'AddHandler fndRouteid.txtValue.KeyPress, AddressOf fndRouteid_KeyPress
        '  AddHandler fndnonprice.ValueChanged, AddressOf NonPrice_Textchanged
        '   AddHandler fndcity_id.txtValue.Leave, AddressOf fndcity_id_Leave
        'AddHandler fndcity_id.txtValue.KeyPress, AddressOf fndcity_id_KeyPress
        '  AddHandler fndSalesman_code.txtValue.Leave, AddressOf fndSalesman_code_Leave
        ' AddHandler fndSalesman_code.ValueChanged, AddressOf text_changed1
        ' AddHandler fndSalesman_code.txtValue.KeyPress, AddressOf fndSalesman_code_KeyPress
        '  AddHandler fndPriceCode.ValueChanged, AddressOf fndPriceCode_text_changed
        ' AddHandler fndPriceCode.txtValue.Leave, AddressOf fndPriceCode_leave
        ' AddHandler fndPriceCode.txtValue.KeyPress, AddressOf fndPriceCode_KeyPress
        '   AddHandler fndvcode.ValueChanged, AddressOf fndvcode_text_changed
        '  AddHandler fndvcode.txtValue.Leave, AddressOf fndvcode_leave
        '   AddHandler fndvcode.txtValue.KeyPress, AddressOf fndvcode_KeyPress
        '  AddHandler fndDepot.txtValue.Leave, AddressOf fndDepot_Leave
        ' AddHandler fndDepot.txtValue.KeyPress, AddressOf fndDepot_KeyPress
    End Sub

    'It Is Used To Fill The City Code in fndcity_id From TSPL_CITY_MASTER
    Private Sub fndcity_id_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndcity_id.ConnectionString = connectSql.SqlCon()
        'fndcity_id.Query = "select City_Code as [City Code],City_Name as [City Name] from TSPL_CITY_MASTER"
        'fndcity_id.ValueToSelect = "City Code"
        'fndcity_id.Caption = "City Master"
        'fndcity_id.ValueToSelect1 = "City Name"
        'fndcity_id.txtValue.MaxLength = 12
    End Sub
    'It Is Used To Save And Update All Record In TSPL_Route_Master
    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If fndRouteid.Value = "" Then
            myMessages.blankValue("Route Code")
            fndRouteid.Focus()
            'ElseIf ddltype.Text = "" Then
            '    myMessages.blankValue("type")
            '    ddltype.Focus()
            'ElseIf rddl_route_offday.Text = "" Then
            '    myMessages.blankValue("route off day")
            '    rddl_route_offday.Focus()
        ElseIf rtxtdescription.Text = "" Then
            myMessages.blankValue("Description")
            rtxtdescription.Focus()
            'ElseIf fndSalesman_code.Value = "" Then
            '    myMessages.blankValue(" Salesman Code")
            '    fndSalesman_code.Focus()
            'ElseIf rddl_category.Text = "" Then
            '    myMessages.blankValue("Category")
            '    rddl_category.Focus()
            'ElseIf fndPriceCode.Value = "" Then
            '    myMessages.blankValue("Price Code")
            '    fndPriceCode.Focus()

            'ElseIf txtpricecodedescription.Text = "" Then
            'myMessages.blankValue("Price Code Description")
            'txtpricecodedescription.Focus()
        ElseIf rbtnSave.Text = "Save" Then
            funInsert()
        Else
            funUpdate()
        End If
    End Sub
    'It Is Used To Delete The Record From TSPL_Route_Master
    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        funDelete()
    End Sub

    'It Is Used To Fill The EMP CODE and Emp Name in fndSalesman_code and rtxtsalesman_name Respectively from TSPL_EMPLOYEE_MASTER where Emp_type='SalesMan'
    Private Sub fndSalesman_code_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndSalesman_code.ConnectionString = connectSql.SqlCon()
        'fndSalesman_code.Query = "select distinct EMP_CODE as [EMP CODE],Emp_Name as [Emp Name], Designation,Pin_Code as [Pin Code],Phone,Card_No as [Card No],Cash from TSPL_EMPLOYEE_MASTER where Emp_type='SalesMan'"
        'fndSalesman_code.ValueToSelect = "EMP CODE"
        'fndSalesman_code.Caption = "Employee Master"
        'fndSalesman_code.txtValue.MaxLength = 12
        'fndSalesman_code.ValueToSelect1 = "Emp Name"
    End Sub
    'It Is Used To Fill The Route No In fndRouteid from TSPL_Route_Master
    Private Sub fndRouteid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndRouteid.ConnectionString = connectSql.SqlCon()
        'fndRouteid.Query = "select Route_No as [Route No],Route_Desc as [Route Desc],Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District,Category_Code as [Category Code],Length from TSPL_Route_Master"
        'fndRouteid.ValueToSelect = "Route No"
        'fndRouteid.Caption = "Route Master"
        'fndRouteid.txtValue.MaxLength = 12
        'fndRouteid.ValueToSelect1 = "Route Desc"
    End Sub
    'It Validate User To Press The Keys 
    Private Sub rtxtroute_length_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rtxtroute_length.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub
    Sub NonPrice_Textchanged()
        txtnonprice.Text = connectSql.RunScalar("select distinct Price_Code_Desc  from TSPL_PRICE_COMPONENT_MAPPING where Price_Code= '" + Convert.ToString(fndnonprice.Value) + "'")

    End Sub

    Sub RoutePrice_Textchanged()
        txtRoutePrice.Text = connectSql.RunScalar("select distinct Price_Code_Desc  from TSPL_ITEM_PRICE_MASTER where Price_Code= '" + Convert.ToString(fndRoutePrice.Value) + "'")
    End Sub
    Private Sub NonPrice_Textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    'It Is Used To Fill Or Clear All Fields of Current Windows Form Bassed On Route No(fndRouteid) From TSPL_Route_Master
    Sub text_changed()
        Try
            Dim strRoute_no As String = "select Route_No from TSPL_Route_Master where Route_No='" + fndRouteid.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strRoute_no)
            If (strvalue <> "") Then
                funfill()
            Else
                ddltype.Text = ""
                rddl_route_offday.Text = ""
                rddl_category.Text = ""
                ' fndZone_outid.txtValue.Text = ""
                rtxtDistrict.Text = ""
                rtxtdescription.Text = ""
                rtxtroute_length.Text = ""
                fndSalesman_code.Value = ""
                rtxtSalesman_name.Text = ""
                fndPriceCode.Value = ""
                txtpricecodedescription.Text = ""
                fndcity_id.Value = ""
                fndDepot.Value = ""
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
                fndRouteid.Enabled = True
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funfill()
        Try
            Dim strQuery As String = "select Route_Desc,Type,Employee_Code,Off_Day,City_Code,District,Category_Code,Length,Employee_Name,Depot_Id,Price_Code,Price_Code_Desc ,vehicle_code,NonPrice_Code,status,SDate,RoutePrice_Code from TSPL_Route_Master where Route_No='" + fndRouteid.Value + "'"


            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    rtxtdescription.Text = dt.Rows(i)("Route_Desc")
                    ddltype.Text = dt.Rows(i)("Type")
                    fndSalesman_code.Value = dt.Rows(i)("Employee_Code")
                    rddl_route_offday.Text = dt.Rows(i)("Off_Day")
                    fndcity_id.Value = dt.Rows(i)("City_Code")
                    rtxtDistrict.Text = dt.Rows(i)("District")
                    rddl_category.Text = dt.Rows(i)("Category_Code")
                    rtxtroute_length.Text = dt.Rows(i)("Length")
                    rtxtSalesman_name.Text = dt.Rows(i)("Employee_Name")
                    fndDepot.Value = dt.Rows(i)("Depot_Id")
                    fndPriceCode.Value = dt.Rows(i)("Price_Code")
                    txtpricecodedescription.Text = dt.Rows(i)("Price_Code_Desc")
                    fndvcode.Value = dt.Rows(i)("vehicle_code")
                    fndnonprice.Value = Convert.ToString(dt.Rows(i)("NonPrice_Code"))
                    txtnonprice.Text = clsDBFuncationality.getSingleValue("select distinct Price_Code_Desc  from TSPL_PRICE_COMPONENT_MAPPING Where Price_Code='" + fndnonprice.Value + "'")
                    txtvcodedesc.Text = clsDBFuncationality.getSingleValue("select description from tspl_vehicle_master where vehicle_id='" + fndvcode.Value + "' ")
                    Dim StrCk As String = dt.Rows(i)("Status").ToString()
                    If (StrCk = "I") Then
                        rdoIN.IsChecked = True
                    Else
                        rdoAC.IsChecked = True
                    End If
                    dtpAcIn.Value = dt.Rows(i)("SDate")
                    If IsDBNull(dt.Rows(i)("RoutePrice_Code")) = False Then
                        fndRoutePrice.Value = dt.Rows(i)("RoutePrice_Code")

                    End If
                    rbtnDelete.Enabled = True
                    rbtnSave.Text = "Update"
                    'If userCode <> "ADMIN" Then
                    '    If funSetUserAccess() = False Then Exit Sub
                    'End If
                Next
            End If


            ''Dim dr As SqlDataReader
            ''dr = connectSql.RunSqlReturnDR(strQuery)
            ''If dr.Read() Then
            ''    rtxtdescription.Text = dr(0).ToString()
            ''    ddltype.Text = dr(1).ToString()
            ''    fndSalesman_code.Value = dr(2).ToString()
            ''    rddl_route_offday.Text = dr(3).ToString()
            ''    fndcity_id.Value = dr(4).ToString()
            ''    rtxtDistrict.Text = dr(5).ToString()
            ''    rddl_category.Text = dr(6).ToString()
            ''    rtxtroute_length.Text = dr(7).ToString()
            ''    rtxtSalesman_name.Text = dr(8).ToString()
            ''    fndDepot.Value = dr(9).ToString()
            ''    fndPriceCode.Value = dr(10).ToString()
            ''    txtpricecodedescription.Text = dr(11).ToString()
            ''    fndvcode.Value = dr(12).ToString()
            ''    fndnonprice.Value = Convert.ToString(dr("NonPrice_Code"))
            ''    txtnonprice.Text = clsDBFuncationality.getSingleValue("select distinct Price_Code_Desc  from TSPL_PRICE_COMPONENT_MAPPING Where Price_Code='" + fndnonprice.Value + "'")
            ''    txtvcodedesc.Text = clsDBFuncationality.getSingleValue("select description from tspl_vehicle_master where vehicle_id='" + fndvcode.Value + "' ")
            ''    Dim StrCk As String = dr("Status").ToString()
            ''    If (StrCk = "I") Then
            ''        rdoIN.IsChecked = True
            ''    Else
            ''        rdoAC.IsChecked = True
            ''    End If
            ''    dtpAcIn.Value = dr("SDate")
            ''    rbtnDelete.Enabled = True
            ''    rbtnSave.Text = "Update"
            ''    'If userCode <> "ADMIN" Then
            ''    '    If funSetUserAccess() = False Then Exit Sub
            ''    'End If
            ''End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It is Used To Clear All Fields Of Current Windows Form
    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        funReset()
    End Sub
    'This is Reset Function Used To Clear All Fields Of Current Windows Form
    Private Sub funReset()
        Try
            fndRouteid.Value = ""
            fndRouteid.MyReadOnly = False
            ddltype.Text = ""
            rddl_route_offday.Text = ""
            rddl_category.Text = ""
            'fndZone_outid.txtValue.Text = ""
            rtxtDistrict.Text = ""
            rtxtdescription.Text = ""
            rtxtroute_length.Text = ""
            fndPriceCode.Value = ""
            txtpricecodedescription.Text = ""
            fndSalesman_code.Value = ""
            ' fndZone_inid.txtValue.Text = ""
            fndcity_id.Value = ""
            rbtnSave.Text = "Save"
            rtxtSalesman_name.Text = ""
            fndDepot.Value = ""
            fndSalesman_code.Enabled = True
            rbtnDelete.Enabled = False
            fndRouteid.Enabled = True
            fndvcode.Value = ""
            txtvcodedesc.Text = ""
            fndnonprice.Value = String.Empty
            txtnonprice.Text = String.Empty
            fndRoutePrice.Value = String.Empty
            txtRoutePrice.Text = String.Empty
            rdoAC.IsChecked = True
            dtpAcIn.Value = clsCommon.GETSERVERDATE()
            SetDataBaseGrid()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Delete Function Used To Delete Records From TSPL_ROUTE_MASTER
    Private Sub funDelete()
        Try
            If fndRouteid.Value = "" Then
                myMessages.blankValue("Route Code")
            ElseIf myMessages.deleteConfirm() Then
                Dim qry = "select count(*) from tspl_transfer_head where route_no='" + fndRouteid.Value + "'"
                Dim count As Integer = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "SP_TSPL_ROUTE_MASTER_DELETE", New SqlParameter("@Route_No", fndRouteid.Value))
                    myMessages.delete()
                    fndRouteid.Enabled = True
                    rbtnSave.Text = "Save"
                    rbtnDelete.Enabled = False
                Else
                    common.clsCommon.MyMessageBoxShow("This Record Cannot be deleted.It is used by another process")
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Insert Function Used To Insert Values In TSPL_ROUTE_MASTER
    Private Sub funInsert()
        Try
            Dim strcatcode As String = IIf(rddl_category.Text.ToString() = "Select", "", rddl_category.Text.ToString())
            Dim strtype As String = IIf(ddltype.Text.ToString() = "Select", "", ddltype.Text.ToString())
            Dim stroffday As String = IIf(rddl_route_offday.Text.ToString() = "Select", "", rddl_route_offday.Text.ToString())
            Dim strActive As Char

            If rdoAC.IsChecked = True Then
                strActive = "A"
            ElseIf rdoIN.IsChecked = True Then
                strActive = "I"
            End If


            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "SP_TSPL_ROUTE_MASTER_INSERT", New SqlParameter("@Route_No", fndRouteid.Value), New SqlParameter("@Route_Desc", rtxtdescription.Text), New SqlParameter("@Type", strtype), New SqlParameter("@Employee_Code", fndSalesman_code.Value), New SqlParameter("@Employee_Name", rtxtSalesman_name.Text), New SqlParameter("@Depot_Id", fndDepot.Value), New SqlParameter("@Off_Day", stroffday), New SqlParameter("@City_Code", fndcity_id.Value), New SqlParameter("@District", rtxtDistrict.Text), New SqlParameter("@Category_Code", strcatcode), New SqlParameter("@Length", rtxtroute_length.Text), New SqlParameter("@Price_Code", fndPriceCode.Value), New SqlParameter("@Price_Code_Desc", txtpricecodedescription.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Status", strActive), New SqlParameter("@SDate", dtpAcIn.Value))

            Dim strqry As String = "update tspl_route_master set RoutePrice_Code='" & fndRoutePrice.Value & "',vehicle_code='" + fndvcode.Value + "', NonPrice_Code = '" + Convert.ToString(fndnonprice.Value) + "',Distance=" & clsCommon.myCdbl(txtDistance.Value) & " where route_no='" + fndRouteid.Value + "'"
            clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(strqry, GetReplecateCompaniesDataBase(), Nothing)
            'connectSql.RunSql(strqry)
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
    'This is Update Function Used To Update Records In TSPL_ROUTE_MASTER
    Private Sub funUpdate()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim ADate As Date? = Nothing
            Dim IDate As Date? = Nothing
            'Dim str As String = "select status,SDate from tspl_route_master where route_No='" + fndRouteid.Value + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            'Dim value As String = dt.Rows(0)("status")
            'Dim Strdate As String = dt.Rows(0)("SDate")

            Dim str As String = "select * from tspl_route_master where route_No='" + fndRouteid.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str, trans)
            Dim Distance As Decimal = clsCommon.myCdbl(dt.Rows(0)("Distance"))
            Dim value As String = dt.Rows(0)("status")
            Dim Strdate As String = dt.Rows(0)("SDate")
            Dim Route_desc As String = dt.Rows(0)("Route_desc")
            Dim Type As String = dt.Rows(0)("Type")
            Dim Employee_Code As String = dt.Rows(0)("Employee_Code")
            Dim Off_Day As String = dt.Rows(0)("Off_Day")
            Dim City_Code As String = dt.Rows(0)("City_Code")
            Dim District As String = dt.Rows(0)("District")
            Dim Category_Code As String = dt.Rows(0)("Category_Code")
            Dim Length As Integer = dt.Rows(0)("Length")
            Dim Employee_Name As String = dt.Rows(0)("Employee_Name")
            Dim Depot_Id As String = dt.Rows(0)("Depot_Id")
            Dim Price_Code As String = dt.Rows(0)("Price_Code")
            Dim Price_Code_Desc As String = dt.Rows(0)("Price_Code_Desc")
            Dim Created_By As String = dt.Rows(0)("Created_By")
            Dim Created_Date As String = dt.Rows(0)("Created_Date")
            Dim Modify_By As String = dt.Rows(0)("Modify_By")
            Dim Modify_Date As String = dt.Rows(0)("Modify_Date")
            Dim Comp_Code As String = dt.Rows(0)("Comp_Code")
            Dim vehicle_code As String = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
            Dim NonPrice_Code As String = clsCommon.myCstr(dt.Rows(0)("NonPrice_Code"))

            Dim strActive As Char

            If rdoAC.IsChecked = True Then
                strActive = "A"
            ElseIf rdoIN.IsChecked = True Then
                strActive = "I"
            End If
            clsDBFuncationality.UpdateInSelectedDatabase(trans, GetReplecateCompaniesDataBase(), "SP_TSPL_ROUTE_MASTER_UPDATE", New SqlParameter("@Route_No", fndRouteid.Value), New SqlParameter("@Route_Desc", rtxtdescription.Text), New SqlParameter("@Type", ddltype.Text), New SqlParameter("@Employee_Code", fndSalesman_code.Value), New SqlParameter("@Employee_Name", rtxtSalesman_name.Text), New SqlParameter("@Depot_Id", fndDepot.Value), New SqlParameter("@Off_Day", rddl_route_offday.Text), New SqlParameter("@City_Code", fndcity_id.Value), New SqlParameter("@District", rtxtDistrict.Text), New SqlParameter("@Category_Code", rddl_category.Text), New SqlParameter("@Length", rtxtroute_length.Text), New SqlParameter("@Price_Code", fndPriceCode.Value), New SqlParameter("@Price_Code_Desc", txtpricecodedescription.Text), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Status", strActive), New SqlParameter("@SDate", dtpAcIn.Value))
            Dim strqry As String = "update tspl_route_master set  RoutePrice_Code='" & fndRoutePrice.Value & "',vehicle_code='" + fndvcode.Value + "', NonPrice_Code = '" + Convert.ToString(fndnonprice.Value) + "',Distance=" & clsCommon.myCdbl(txtDistance.Value) & " where route_no='" + fndRouteid.Value + "'"
            clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(strqry, GetReplecateCompaniesDataBase(), trans)

            '--Updates Salesman Name Against Same Root in Customer Master---Pankaj Kumar
            Dim SalesManDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + fndSalesman_code.Value + "'", trans))
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_CUSTOMER_MASTER SET Salesman_Code='" + fndSalesman_code.Value + "', Salesman_Desc='" + SalesManDesc + "' WHERE Route_No='" + fndRouteid.Value + "'", trans)
            '-------------------------------------

            'connectSql.RunSql(strqry)
            '------------------------------------------------------------------------------------------


            If strActive <> value Then
                If rdoAC.IsChecked = True Then
                    ADate = Nothing
                    IDate = clsCommon.GetPrintDate(Strdate, "yyyy-MM-dd")
                ElseIf rdoIN.IsChecked = True Then
                    ADate = clsCommon.GetPrintDate(Strdate, "yyyy-MM-dd")
                    IDate = Nothing
                End If

                'connectSql.RunSp("SP_TSPL_ROUTE_MASTER_HISTORY_INSERT", New SqlParameter("@Route_No", fndRouteid.Value), New SqlParameter("@Route_Desc", rtxtdescription.Text), New SqlParameter("@Type", ddltype.Text), New SqlParameter("@Employee_Code", fndSalesman_code.Value), New SqlParameter("@Employee_Name", rtxtSalesman_name.Text), New SqlParameter("@Depot_Id", fndDepot.Value), New SqlParameter("@Off_Day", rddl_route_offday.Text), New SqlParameter("@City_Code", fndcity_id.Value), New SqlParameter("@District", rtxtDistrict.Text), New SqlParameter("@Category_Code", rddl_category.Text), New SqlParameter("@Length", rtxtroute_length.Text), New SqlParameter("@Price_Code", fndPriceCode.Value), New SqlParameter("@Price_Code_Desc", txtpricecodedescription.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@AcDate", ADate), New SqlParameter("@InDate", IDate), New SqlParameter("@HisDate", clsCommon.GETSERVERDATE()))
                'Dim strqry1 As String = "update tspl_route_master_History set vehicle_code='" + fndvcode.Value + "', NonPrice_Code = '" + Convert.ToString(fndnonprice.Value) + "' where route_no='" + fndRouteid.Value + "'"


                clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_ROUTE_MASTER_HISTORY_INSERT", New SqlParameter("@Route_No", fndRouteid.Value), New SqlParameter("@Route_Desc", Route_desc), New SqlParameter("@Type", Type), New SqlParameter("@Employee_Code", Employee_Code), New SqlParameter("@Employee_Name", Employee_Name), New SqlParameter("@Depot_Id", Depot_Id), New SqlParameter("@Off_Day", Off_Day), New SqlParameter("@City_Code", City_Code), New SqlParameter("@District", District), New SqlParameter("@Category_Code", Category_Code), New SqlParameter("@Length", Length), New SqlParameter("@Price_Code", Price_Code), New SqlParameter("@Price_Code_Desc", Price_Code_Desc), New SqlParameter("@Created_By", Created_By), New SqlParameter("@Created_Date", Created_Date), New SqlParameter("@Modify_By", Modify_By), New SqlParameter("@Modify_Date", Modify_Date), New SqlParameter("@Comp_Code", Comp_Code), New SqlParameter("@vehicle_code", vehicle_code), New SqlParameter("@NonPrice_Code", NonPrice_Code), New SqlParameter("@AcDate", ADate), New SqlParameter("@InDate", IDate), New SqlParameter("@HisDate", clsCommon.GETSERVERDATE(trans)))
                ' Dim strqry1 As String = "update tspl_route_master_History set vehicle_code='" + vehicle_code + "', NonPrice_Code = '" + NonPrice_Code + "' where route_no='" + fndRouteid.Value + "'"



                'connectSql.RunSql(strqry1)


                Dim qrycount As String = "select COUNT (*) from TSPL_CUSTOMER_MASTER where Route_No='" + fndRouteid.Value + "'"
                Dim strroute As Integer = clsDBFuncationality.getSingleValue(qrycount, trans)
                If strroute > 0 Then
                    Dim SalesManDescCust As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT salesman_desc FROM tspl_customer_master WHERE salesman_code='" + fndSalesman_code.Value + "'", trans))
                    Dim strqrysales As String = "UPDATE TSPL_CUSTOMER_MASTER SET Salesman_Code='" + fndSalesman_code.Value + "', Salesman_Desc='" + SalesManDescCust + "' WHERE Route_No='" + fndRouteid.Value + "'"
                    clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(strqrysales, GetReplecateCompaniesDataBase(), trans)

                End If


            End If
            myMessages.update()
            fndRouteid.Enabled = True
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Check The Value Of Finder(fndcity_id),Is Present In Database Or Not
    Sub fndcity_id_Leave()
        Dim strCitycode As String = "select City_Code from TSPL_CITY_MASTER where City_Code='" + fndcity_id.Value + "'"
            Dim strcheck As String = clsDBFuncationality.getSingleValue(strCitycode)
            If (strcheck <> "" Or fndcity_id.Value = "") Then
            fndcity_id.Value = strcheck
        Else
            common.clsCommon.MyMessageBoxShow("City Id does not exist in master table")
            fndcity_id.Value = ""
            fndcity_id.Focus()
        End If
    End Sub
    Private Sub fndcity_id_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'It Validate User To Press The Keys 
    Private Sub fndRouteid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True

        End If
    End Sub
    'It Validate User To Press The Keys 
    Private Sub fndSalesman_code_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    'It Validate User To Press The Keys 
    Private Sub fndcity_id_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    'This is fun_ddl_category Function Used To Fill rddl_category From TSPL_FIXED_PARAMETER Where Description='Category'
    Private Sub fun_ddl_category()
        Try
            Dim strcategory As String = "select Type from TSPL_FIXED_PARAMETER where Description='Category'"
            transportSql.FillComboBox(strcategory, rddl_category, "type", "type")
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is fun_ddl_Type Function Used To Fill ddltype From TSPL_FIXED_PARAMETER Where Description='Type'
    Private Sub fun_ddl_Type()
        Try
            Dim strtype As String = "select Route_Type_Id from TSPL_ROUTE_TYPE"
            transportSql.FillComboBox(strtype, ddltype, "Route_Type_Id", "Route_Type_Id")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'This is fun_ddl_Routeoffday Function Used To Fill rddl_route_offday From TSPL_FIXED_PARAMETER Where Description='Route Off Day'
    Private Sub fun_ddl_Routeoffday()
        Try
            Dim strroute_offday As String = "select Code from TSPL_FIXED_PARAMETER where Description='Route Off Day'"
            transportSql.FillComboBox(strroute_offday, rddl_route_offday, "Code", "Code")
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub RadMenuItem_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Close.Click
        Me.Close()
    End Sub
    'It Is Used To Export The Records From TSPL_ROUTE_MASTER
    Private Sub RadMenuItem_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Export.Click
        Dim strSql As String = "Select Route_No as [Route No],Route_Desc as [Route Desc], Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District ,Category_Code as [Category Code],Length,Employee_Name as [Employee Name],Depot_Id as [Depot Id],Price_Code as [Price Code],Price_Code_Desc as[Price Code Desc],NONPrice_Code as [Non Price Code],vehicle_code as [Vehicle Code],Status as [Status],SDate as [Status Date],RoutePrice_Code as [Route Price Code] from TSPL_ROUTE_MASTER"
        transportSql.ExporttoExcel(strSql, Me)
    End Sub
    'It Is Used To Import The Records From TSPL_ROUTE_MASTER
    Private Sub RadMenuItem_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv, "Route No", "Route Desc", "Type", "Employee Code", "Off Day", "City Code", "District", "Category Code", "Length", "Employee Name", "Depot Id", "Price Code", "Price Code Desc", "Non Price Code", "Vehicle Code", "Status", "Status Date", "Route Price Code") Then

            clsCommon.ProgressBarShow()
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strRoute_no As String = clsCommon.myCstr(grow.Cells(0).Value)

                    If clsCommon.myLen(strRoute_no) > 0 Then
                        If clsCommon.myLen(strRoute_no) > 12 Then
                            Throw New Exception("Route Code Can not be left Blank or size can not be grater than 12")
                        End If
                        Dim strstrRoute_desc As String = clsCommon.myCstr(grow.Cells(1).Value)
                        If String.IsNullOrEmpty(strstrRoute_desc) Or clsCommon.myLen(strstrRoute_desc) > 50 Then
                            Throw New Exception("Route Description Can not be left Blank or size can not be grater than 50")
                        End If
                        Dim strtypeTemp As String = clsCommon.myCstr(grow.Cells(2).Value)
                        'If clsCommon.myLen(strtypeTemp) <= 0 Then
                        '    Throw New Exception("Route type Can not be left Blank")
                        'End If
                        Dim qry As String = "select  * from TSPL_ROUTE_TYPE where Route_Type_Id='" + strtypeTemp + "'"
                        Dim strtype As String = clsCommon.myCstr(connectSql.RunScalar(trans, qry))
                        If clsCommon.myLen(strtype) <= 0 And clsCommon.myLen(strtypeTemp) > 0 Then
                            Throw New Exception("Route type " + strtypeTemp + " is not Exist")
                        End If
                        ' ''If strtype = "Agency" Then
                        ' ''    strtype = "Agency"
                        ' ''ElseIf strtype = "Distributors" Then
                        ' ''    strtype = "Distributors"
                        ' ''Else
                        ' ''    Throw New Exception("Route type Can not be left Blank or You must enter the value either Agency Or Distributors")
                        ' ''End If
                        Dim stremp_code As String = clsCommon.myCstr(grow.Cells(3).Value)
                        If clsCommon.myLen(stremp_code) > 12 Then 'String.IsNullOrEmpty(stremp_code) Or 
                            Throw New Exception("Employee Code Can not be left Blank or size can not be grater than 12")
                        End If
                        Dim stroffday As String = clsCommon.myCstr(grow.Cells(4).Value)
                        If clsCommon.myLen(stroffday) > 12 Then 'String.IsNullOrEmpty(stroffday) Or 
                            Throw New Exception("Off Day Can not be left Blank or size can not be grater than 12")
                        End If
                        Dim strCitycode As String = clsCommon.myCstr(grow.Cells(5).Value)
                        If clsCommon.myLen(strCitycode) > 12 Then
                            Throw New Exception("City code can not be greater than 12")
                        End If
                        Dim strDistrict As String = clsCommon.myCstr(grow.Cells(6).Value)
                        If clsCommon.myLen(strDistrict) > 50 Then
                            Throw New Exception("District can not be greater than 50")
                        End If
                        Dim strcat_code As String = clsCommon.myCstr(grow.Cells(7).Value)
                        If clsCommon.myLen(strcat_code) > 12 Then 'String.IsNullOrEmpty(strcat_code) Or 
                            Throw New Exception("Category Code Can not be left Blank or size can not be grater than 12")
                        End If
                        Dim re As Regex = New Regex("(^[0-9]*[1-9]+[0-9]*\.[0-9]*$)|(^[0-9]*\.[0-9]*[1-9]+[0-9]*$)|(^[0-9]*[1-9]+[0-9]*$)")
                        Dim strLength As String = clsCommon.myCdbl(grow.Cells(8).Value)
                        'Or Not re.IsMatch(strLength)
                        If clsCommon.myLen(strLength) > 8 Or Not IsNumeric(strLength) Then
                            Throw New Exception("Length can not be greater than 8 and You must Enter only Numeric Values")
                        End If
                        Dim stremp_name As String = clsCommon.myCstr(grow.Cells(9).Value)
                        If clsCommon.myLen(stremp_name) > 50 Then
                            Throw New Exception("Employee Name can not be greater than 50")
                        End If
                        Dim strDepoetID As String = clsCommon.myCstr(grow.Cells(10).Value)
                        If clsCommon.myLen(strDepoetID) > 12 Then
                            Throw New Exception("Depot ID can not be grater than 12")
                        End If
                        Dim strprice_code As String = clsCommon.myCstr(grow.Cells(11).Value)
                        If clsCommon.myLen(strprice_code) > 12 Then 'String.IsNullOrEmpty(strprice_code) Or 
                            Throw New Exception("Price Code Can not be left Blank or size can not be grater than 12")
                        End If
                        Dim strprice_code_desc As String = clsCommon.myCstr(grow.Cells(12).Value)
                        If clsCommon.myLen(strprice_code_desc) > 100 Then 'String.IsNullOrEmpty(strprice_code_desc) Or 
                            Throw New Exception("Price Code Description Can not be left Blank or size can not be grater than 100")
                        End If
                        Dim NOnPriceCode As String = clsCommon.myCstr(grow.Cells("Non Price Code").Value)

                        Dim RoutePriceCode As String = clsCommon.myCstr(grow.Cells("Route Price Code").Value)

                        If clsCommon.myLen(RoutePriceCode) > 0 Then
                            Dim ExistRoutepricecode = clsDBFuncationality.getSingleValue("select 1 from TSPL_ITEM_PRICE_MASTER where Price_Code='" & RoutePriceCode & "'", trans)
                            If clsCommon.myLen(ExistRoutepricecode) <= 0 Then
                                Throw New Exception("Route Price Code does not exist")
                            End If
                        End If
                        Dim VCode As String = clsCommon.myCstr(grow.Cells("Vehicle Code").Value)
                        Dim StrStatus As String = IIf(clsCommon.myCstr(grow.Cells("Status").Value) = "", "A", clsCommon.myCstr(grow.Cells("Status").Value))


                        Dim strDate As String = clsCommon.GetPrintDate(IIf(clsCommon.myLen(grow.Cells("Status Date").Value.ToString) <= 0, clsCommon.GetPrintDate(Now(), "yyyy-MM-dd"), grow.Cells("Status Date").Value), "yyyy-MM-dd")


                        'If String.IsNullOrEmpty(NOnPriceCode) Or clsCommon.myLen(NOnPriceCode) > 12 Then
                        '    Throw New Exception("Non Price Code  Can not be left Blank or size can not be grater than 12")
                        'End If
                        Dim strquery As String = "select count(*) from TSPL_Route_Master where Route_No='" + strRoute_no + "'"
                        Dim i As Integer = CInt(connectSql.RunScalar(trans, strquery))

                        If (i = 0) Then
                            connectSql.RunSpTransaction(trans, "SP_TSPL_ROUTE_MASTER_INSERT", New SqlParameter("@Route_No", strRoute_no), New SqlParameter("@Route_Desc", strstrRoute_desc), New SqlParameter("@Type", strtype), New SqlParameter("@Employee_Code", stremp_code), New SqlParameter("@Off_Day", stroffday), New SqlParameter("@City_Code", strCitycode), New SqlParameter("@District", strDistrict), New SqlParameter("@Category_Code", strcat_code), New SqlParameter("@Length", strLength), New SqlParameter("@Employee_Name", stremp_name), New SqlParameter("@Depot_Id", fndDepot.Value), New SqlParameter("@Price_Code", strprice_code), New SqlParameter("@Price_Code_Desc", strprice_code_desc), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Status", StrStatus), New SqlParameter("@SDate", strDate))
                            connectSql.RunSqlTransaction(trans, "update TSPL_ROUTE_MASTER set RoutePrice_Code = '" + RoutePriceCode + "',NonPrice_Code = '" + NOnPriceCode + "',vehicle_code='" + VCode + "' where Route_No = '" + strRoute_no + "'")

                        Else
                            connectSql.RunSpTransaction(trans, "SP_TSPL_ROUTE_MASTER_UPDATE", New SqlParameter("@Route_No", strRoute_no), New SqlParameter("@Route_Desc", strstrRoute_desc), New SqlParameter("@Type", strtype), New SqlParameter("@Employee_Code", stremp_code), New SqlParameter("@Off_Day", stroffday), New SqlParameter("@City_Code", strCitycode), New SqlParameter("@District", strDistrict), New SqlParameter("@Category_Code", strcat_code), New SqlParameter("@Length", strLength), New SqlParameter("@Employee_Name", stremp_name), New SqlParameter("@Depot_Id", fndDepot.Value), New SqlParameter("@Price_Code", strprice_code), New SqlParameter("@Price_Code_Desc", strprice_code_desc), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Status", StrStatus), New SqlParameter("@SDate", strDate))
                            connectSql.RunSqlTransaction(trans, "update TSPL_ROUTE_MASTER set RoutePrice_Code = '" + RoutePriceCode + "',NonPrice_Code = '" + NOnPriceCode + "',vehicle_code='" + VCode + "' where Route_No = '" + strRoute_no + "'")
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
    'It Is Used To Fill Or Clear The Records Bassed On EMP CODE( fndSalesman_code) From TSPL_EMPLOYEE_MASTER

    Sub text_changed1()
        Try
            Dim stremp_code As String = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndSalesman_code.Value + "'"

            Dim strvalue As String = clsDBFuncationality.getSingleValue(stremp_code)
           
            If strvalue <> "" Then
                funfill1_name()
            Else
                rtxtSalesman_name.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub text_changed1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This is funfill1_name Function Used To Fill rtxtsalesman_name Bassed On EMP CODE From TSPL_EMPLOYEE_MASTER 
    Public Sub funfill1_name()
        Try
            Dim stremp_name As String = "select  Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndSalesman_code.Value + "'"
          
            rtxtSalesman_name.Text = clsDBFuncationality.getSingleValue(stremp_name)

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Check The Value Of Finder(fndSalesman_code),Is Present In Database Or Not
    Sub fndSalesman_code_Leave()
        If fndSalesman_code.Value = "" Then
        Else
            Try
                Dim stremp_code As String = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndSalesman_code.Value + "'"

                Dim strvalue As String = clsDBFuncationality.getSingleValue(stremp_code)
              
                If strvalue <> "" Then
                Else : stremp_code = ""
                    rtxtSalesman_name.Text = ""
                    common.clsCommon.MyMessageBoxShow("Salesman Code does not exist in the master table")
                    fndSalesman_code.Value = ""
                    fndSalesman_code.Focus()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Private Sub fndSalesman_code_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'It Is Used To Give The Authority To User,To Access This Form (Route Master) Or Not.(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ROUTE-M"
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

    Private Sub fndPriceCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndPriceCode.ConnectionString = connectSql.SqlCon()
        'fndPriceCode.Query = "select distinct Price_Code as [Price Code],Price_Code_Desc as [Price Code Desc] from TSPL_PRICE_COMPONENT_MAPPING"
        'fndPriceCode.ValueToSelect = "Price Code"
        'fndPriceCode.Caption = "price Component Mapping"
        'fndPriceCode.txtValue.MaxLength = 12
        'fndPriceCode.ValueToSelect1 = "Price Code Desc"
    End Sub
    Sub fndvcode_text_changed()
        Try
            Dim strprice_code As String = "select vehicle_id  from tspl_vehicle_master where vehicle_id='" + fndvcode.Value + "'"

            Dim strvalue As String = clsDBFuncationality.getSingleValue(strprice_code)
           
            If strvalue <> "" Then
                fillvcodeDescription()
            Else
                txtvcodedesc.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fndvcode_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub


    Sub fndPriceCode_text_changed()
        Try
            Dim strprice_code As String = "select Price_Code from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + fndPriceCode.Value + "'"

            Dim strvalue As String = clsDBFuncationality.getSingleValue(strprice_code)
           
            If strvalue <> "" Then
                fillPriceCodeDescription()
            Else
                txtpricecodedescription.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fndPriceCode_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Public Sub fillPriceCodeDescription()
        Try
            Dim stremp_name As String = "select Price_Code_Desc from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + fndPriceCode.Value + "'"
          txtpricecodedescription.Text = clsDBFuncationality.getSingleValue(stremp_name)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub fillvcodeDescription()
        Try
            Dim stremp_name As String = "select description  from tspl_vehicle_master where vehicle_id='" + fndvcode.Value + "'"
           
            txtvcodedesc.Text = clsDBFuncationality.getSingleValue(stremp_name)

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub fndPriceCode_leave()
        If fndPriceCode.Value = "" Then
        Else
            Try
                Dim strprice_code As String = "select Price_Code from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + fndPriceCode.Value + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(strprice_code)               
                If strvalue <> "" Then
                Else : strprice_code = ""
                    txtpricecodedescription.Text = ""
                    common.clsCommon.MyMessageBoxShow("Price Code does not exist in the master table")
                    fndPriceCode.Value = ""
                    fndPriceCode.Focus()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub


    Sub fndvcode_leave()
        If fndvcode.Value = "" Then
        Else
            Try
                Dim strprice_code As String = "select vehicle_id from tspl_vehicle_master where vehicle_id='" + fndvcode.Value + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(strprice_code)
               
                If strvalue <> "" Then
                Else : strprice_code = ""
                    txtvcodedesc.Text = ""
                    common.clsCommon.MyMessageBoxShow("Vehicle Code does not exist in the master table")
                    fndvcode.Value = ""
                    fndvcode.Focus()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
 



    Private Sub fndPriceCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndvcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    'It Is Used To Fill The Depot No In fndDepot from Table
    Private Sub fndDepot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndDepot.ConnectionString = connectSql.SqlCon()
        'fndDepot.Query = "select Location_Code as [Location Code],Location_Desc as [Location Desc] from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        'fndDepot.ValueToSelect = "Location Code"
        'fndDepot.Caption = "Location Master"
        'fndDepot.txtValue.MaxLength = 12
        'fndDepot.ValueToSelect1 = "Location Desc"
    End Sub
    'It Is Used To Fill depot Number from TSPL_LOCATION_MASTER
    Sub fndDepot_Leave()
        If fndDepot.Value = "" Then
        Else
            Dim strlocationcode As String = "select Location_Code from TSPL_LOCATION_MASTER where Location_Code='" + fndDepot.Value + "'"
           
            Dim strcheck As String = clsDBFuncationality.getSingleValue(strlocationcode)

         
            If (strcheck <> "") Then
                fndDepot.Value = strcheck
            Else
                common.clsCommon.MyMessageBoxShow("Depot Id does not exist in Master Table")
                fndDepot.Value = ""
                fndDepot.Focus()
            End If
        End If

    End Sub
    Private Sub fndDepot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub
    Private Sub fndDepot_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

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

    Private Sub fndvcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndvcode.ConnectionString = connectSql.SqlCon()
        'fndvcode.Query = "select vehicle_id as [Vehicle Code],description as [Description] from tspl_vehicle_master"
        'fndvcode.ValueToSelect = "Vehicle Code"
        'fndvcode.Caption = "Vehicle Code"
        'fndvcode.txtValue.MaxLength = 12
        'fndvcode.ValueToSelect1 = "Description"
    End Sub

    Private Sub RadGroupBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox4.Click

    End Sub

    Private Sub fndnonprice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndnonprice.ConnectionString = connectSql.SqlCon()
        'fndnonprice.Query = "select distinct Price_Code as [Price Code],Price_Code_Desc as [Price Code Desc] from TSPL_PRICE_COMPONENT_MAPPING"
        'fndnonprice.ValueToSelect = "Price Code"
        'fndnonprice.Caption = "price Component Mapping"
        'fndnonprice.txtValue.MaxLength = 12
        'fndnonprice.ValueToSelect1 = "Price Code Desc"

    End Sub

    Private Sub frmRouteMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            printHistory()
        End If
    End Sub

    Private Sub fndRouteid__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRouteid._MYValidating

        Dim str As String = "select count(*) from TSPL_Route_Master where Route_No ='" + fndRouteid.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndRouteid.MyReadOnly = False
        Else
            fndRouteid.MyReadOnly = True
        End If
        If fndRouteid.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select Route_No as [RouteNo],Route_Desc as [Route Desc],Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District,Category_Code as [Category Code],Length from TSPL_Route_Master"
            'fndRouteid.Value = clsCommon.ShowSelectForm("GpCodFND", qry, "RouteNo", "", fndRouteid.Value, "", isButtonClicked)
            fndRouteid.Value = clsRouteMaster.getFinder("", fndRouteid.Value, isButtonClicked)
            If fndRouteid.Value IsNot Nothing Then
                rbtnDelete.Enabled = True
            Else
                rbtnDelete.Enabled = False
            End If

            text_changed()
        End If
    End Sub

    Private Sub fndRouteid__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndRouteid._MYNavigator
        Dim qst As String = "select Route_No as [Route No],Route_Desc as [Route Desc],Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District,Category_Code as [Category Code],Length from TSPL_Route_Master   where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and Route_No in (select min(Route_No) from TSPL_Route_Master where Route_No>'" + fndRouteid.Value + "'   ) "
            Case NavigatorType.First
                qst += "and Route_No in (select MIN(Route_No) from TSPL_Route_Master  )"
            Case NavigatorType.Last
                qst += "and Route_No in (select Max(Route_No) from TSPL_Route_Master  )"
            Case NavigatorType.Previous
                qst += "and Route_No in (select max(Route_No) from TSPL_Route_Master where Route_No<'" + fndRouteid.Value + "'   )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndRouteid.Value = clsCommon.myCstr(dt.Rows(0)("Route No"))

        End If
        'TextChanged()
        If fndRouteid.Value IsNot Nothing Then
            rbtnDelete.Enabled = True
        Else
            rbtnDelete.Enabled = False

        End If

        text_changed()
    End Sub

    Private Sub fndSalesman_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSalesman_code._MYValidating
        Dim qry As String = "select distinct EMP_CODE as [EMPCODE],Emp_Name as [Emp Name], Designation,Pin_Code as [Pin Code],Phone,Card_No as [Card No],Cash from TSPL_EMPLOYEE_MASTER "
        fndSalesman_code.Value = clsCommon.ShowSelectForm("RouteMaCodFND", qry, "EMPCODE", " Emp_type='SalesMan' ", fndSalesman_code.Value, "", isButtonClicked)
        fndSalesman_code_Leave()
        text_changed1()
    End Sub

    Private Sub fndcity_id__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcity_id._MYValidating
        Dim qry As String = "select City_Code as [CityCode],City_Name as [City Name] from TSPL_CITY_MASTER "
        fndcity_id.Value = clsCommon.ShowSelectForm("RotMastrCode2", qry, "CityCODE", "", fndcity_id.Value, "", isButtonClicked)
        fndcity_id_Leave()
    End Sub

    Private Sub fndDepot__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndDepot._MYValidating
        Dim qry As String = "select Location_Code as [LocationCode],Location_Desc as [Location Desc] from TSPL_LOCATION_MASTER "
        fndDepot.Value = clsCommon.ShowSelectForm("RouteMasterCode3", qry, "LocationCODE", "Location_Type='Physical'", fndDepot.Value, "", isButtonClicked)
        fndDepot_Leave()
    End Sub

    Private Sub fndPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPriceCode._MYValidating
        Dim qry As String = "select distinct Price_Code as [PriceCode],Price_Code_Desc as [Price Code Desc] from TSPL_PRICE_COMPONENT_MAPPING "
        fndPriceCode.Value = clsCommon.ShowSelectForm("RouteMastID", qry, "PriceCode", "", fndPriceCode.Value, "", isButtonClicked)
        fndPriceCode_text_changed()
        fndPriceCode_leave()
    End Sub


    Private Sub fndnonprice__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndnonprice._MYValidating
        Dim qry As String = "select distinct Price_Code as [PriceCode],Price_Code_Desc as [Price Code Desc] from TSPL_PRICE_COMPONENT_MAPPING"
        fndnonprice.Value = clsCommon.ShowSelectForm("RouFND", qry, "PriceCode", "", fndnonprice.Value, "", isButtonClicked)
        NonPrice_Textchanged()
    End Sub

    Private Sub fndvcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvcode._MYValidating
        Dim qry As String = "select vehicle_id as [VehicleCode],description as [Description] from tspl_vehicle_master"
        fndvcode.Value = clsCommon.ShowSelectForm("RouteMFND", qry, "VehicleCode", "", fndvcode.Value, "", isButtonClicked)
        fndvcode_text_changed()
        fndvcode_leave()
    End Sub

   Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printHistory()
    End Sub

    Public Sub printHistory()
        Try
            Dim qry As String = Nothing
            If clsCommon.myLen(fndcity_id.ValidateChildren) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select the Route Code")
            Else
                qry = "select Route_No,Route_Desc,Employee_Name,Off_Day,Price_Code_Desc,AcDate,InDate  ,Comp_Name,Add1 from TSPL_ROUTE_MASTER_HISTORY left outer join TSPL_COMPANY_MASTER on TSPL_ROUTE_MASTER_HISTORY.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where Route_No='" + fndRouteid.Value + "'"
            End If





            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptRouteHistory", "Route History Report")
            End If

           



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub




   
    Private Sub fndRoutePrice__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRoutePrice._MYValidating
        Dim qry As String = "select distinct Price_Code as [PriceCode],Price_Code_Desc as [Price Code Desc],Tax_group from TSPL_ITEM_PRICE_MASTER inner join TSPL_TAX_GROUP_MASTER on TSPL_ITEM_PRICE_MASTER.Tax_group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
        fndRoutePrice.Value = clsCommon.ShowSelectForm("RouFND", qry, "PriceCode", "TSPL_TAX_GROUP_MASTER.Is_Transfer=1 ", fndRoutePrice.Value, "", isButtonClicked)
        RoutePrice_Textchanged()
    End Sub

    Private Sub MyLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyLabel1.Click

    End Sub
End Class
