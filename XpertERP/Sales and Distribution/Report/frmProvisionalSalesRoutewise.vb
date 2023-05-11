
''''              Modified by = Priti (10/04/2012)

Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common


Public Class FrmProvisionalSalesRoutewise
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim userCode, companyCode As String
    Dim sql As String
    Dim dt As DataTable


    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dt = clsDBFuncationality.GetDataTable(sql)
        'dr.Read()
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
        
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        '  Dim qry As String = "select Location_Code as [Location],Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ProvSaleDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmProvisionalSalesRoutewise_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            'resetForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnprint.Enabled Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub



    Private Sub FrmProvisionalSalesRoutewise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        ddlConvert.Text = "Raw"
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        ddlType.Text = "Both"
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")



        chkExcelOption.Checked = False '' Added by abhishek as on 29 june 2012
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PRV-SL-DT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged, rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        LoadLocation()
        chkExcelOption.Checked = False
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        ddlConvert.Text = "Raw"
        LoadCompany()
        rbtnCompanyAll.IsChecked = True '' Added by abhishek as on 29 june 2012
        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        ddlType.Text = "Both"
    End Sub
    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub
    Function LoadClass() As DataTable

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "Select"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "AGENCY"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "DIRECT ROUTE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "FRANCHISEE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "MODERN TRADE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "PRE-SALE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "SUPER DISTRIBUTOR"
        dt.Rows.Add(dr)


        Return dt
    End Function
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
     
        print()


    End Sub
    Sub print()
        Dim strSQL1Group, strReportTitle, strOrderColumn, strOrderBy, strConverted, head2, TDMCOdecolumn, group1, additional As String
        strSQL1Group = ""
        head2 = ""
        TDMCOdecolumn = ""
        strReportTitle = ""
        strOrderColumn = ""
        strConverted = ""
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
            Return
        ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Class or select ALL")
            Return
        ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
            Return
        End If


        If rdbSku.IsChecked = True Then
            strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code"
            strReportTitle = "Provisional Sale Sku wise"
            strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
        ElseIf rdbPack.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
            strReportTitle = "Provisional Sale Pack wise"
            strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
        ElseIf rdbFlavour.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strReportTitle = "Provisional Sale Flavour wise"
            strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
        End If

        Dim strSubQry1 As String = ""
        If ddlConvert.Text = "Converted" Then
            strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
            strConverted = "Converted"
        ElseIf ddlConvert.Text = "8oz" Then
            strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1) * isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz'),0)"
            strConverted = "8oz"
        ElseIf ddlConvert.Text = "Raw" Then
            'strSubQry1 = "isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Raw'),0)"
            strSubQry1 = "1"
            strConverted = "Raw"
        End If





        If ddlcategory.Text = "HOS" Then
            head2 = "HOS"
            TDMCOdecolumn = "HOS"
            group1 = "HOS"
            additional = "(HOS)"
        ElseIf ddlcategory.Text = "TDM" Then
            head2 = "TDM"
            TDMCOdecolumn = "TDM"
            group1 = "TDM"
            additional = "(TDM)"
        ElseIf ddlcategory.Text = "ADC" Then
            head2 = "ADC"
            TDMCOdecolumn = "ADC"
            group1 = "ADC"
            additional = "(ADC)"
        ElseIf ddlcategory.Text = "CE" Then
            head2 = "CE"
            TDMCOdecolumn = "CE"
            group1 = "CE"
            additional = "(CE)"

        ElseIf ddlcategory.Text = "CE" Then
            head2 = "CE"
            TDMCOdecolumn = "CE"
            group1 = "CE"
            additional = "(CE)"

        ElseIf ddlcategory.Text = "SalesMan" Then
            head2 = "SalesMan"
            TDMCOdecolumn = "Salesmancode"
            group1 = "Salesmancode"
            additional = "(SalesMan)"

        End If




        '---------------By vipin for hierarchry--------------------------------
        'Dim StrSql As String = "select " & strSQL1Group & " as Group1,'" + head2 + "' as heading2," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " as HierCode,TSPL_EMPLOYEE_MASTER_1.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Desc ," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No, " & _
        '           "   case when TEMP_PROVISIONAL_SALES.unit_code <> 'SH' then (LoadOutQty/Conversion_Factor - (isnull(LoadInQty/Conversion_Factor,0) + isnull(TEMP_PROVISIONAL_SALES.Breakage,0)+isnull(TEMP_PROVISIONAL_SALES.Leak,0) + isnull(TEMP_PROVISIONAL_SALES.Shortage,0))) *  " & strSubQry1 & " else 0 end as sale, " & _
        '           "'" & dtpFdate.Value & "' as fromDate,'" & dtpToDate.Value & "' as ToDate,'" & strReportTitle & "' as ReportTitle," & strOrderColumn & " as OrderBy, " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location, " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name,'" & strConverted & "' AS Convertion,isnull(((BasicPrice_WithTax + Empty_Value + TPT_Value) * LoadoutQty),0) - " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Amount as Value FROM " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER  as TSPL_EMPLOYEE_MASTER_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " = TSPL_EMPLOYEE_MASTER_1.EMP_CODE INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code AND  " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Pack_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Flavour_Code = TSPL_ITEM_DETAILS_1.Class_Code AND " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No AND " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No AND " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code  AND " & _
        '           " " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.MRP = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP LEFT OUTER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
        '           "where " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date < = CONVERT(date,'" & dtpToDate.Value & "',103) "

        Dim strSql1 As String = "SELECT     " & strSQL1Group & " AS Group1, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No, " & _
        " (case when Uom <> 'SH' then (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end) *  " & strSubQry1 & " AS Sale, 0 AS LoadIn, '" & dtpFdate.Value & "' AS fromDate, '" & dtpToDate.Value & "' AS Todate, " & _
        "'" & strReportTitle & "' AS ReportTitle, " & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, '" & strConverted & "' AS Convertion, " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty *  " & strSubQry1 & ") * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax +  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value) " & _
        "AS Value, 0 AS Inamt, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
        "FROM   " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code AND " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ON  " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
        " WHERE     (TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
        "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and Post='Y' "
        Dim Un1 As String = "Union All "
        Dim strSql2 As String = "SELECT     " & strSQL1Group & " AS Group1, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No, " & _
        " 0 AS Sale, case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then ((" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) + Burst/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Leak/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Shortage/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor ) *  " & strSubQry1 & "  else 0 end AS LoadIn, '" & dtpFdate.Value & "' AS fromDate, '" & dtpToDate.Value & "' AS Todate, " & _
        "'" & strReportTitle & "' AS ReportTitle, " & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, '" & strConverted & "' AS Convertion, " & _
        "0 AS Value, ((LoadIn_Qty+ Burst + Leak + Shortage )*  " & strSubQry1 & ")  * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value) AS Inamt, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
        "FROM   " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code AND  " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ON  " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code" & _
        " WHERE     (TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and Post='Y'"







        Dim strLocAll, strRouteAll As String
        If chkLocationAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkRouteAll.IsChecked = True Then
            strRouteAll = "Y"
        Else
            strRouteAll = "N"
        End If


        If strLocAll = "N" Then
            strSql1 += " and From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            strSql2 += " and To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

        End If
        If strRouteAll = "N" Then
            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "

        End If

        Dim StrSql As String = strSql1 & Un1 & strSql2
        Dim ArrDBName As ArrayList = Nothing
        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        Dim strQuery As String = clsCommon.GetQueryWithAllSelectedDataBase(StrSql, ArrDBName, False)
        If ddlType.Text = "Both" Then
        ElseIf ddlType.Text = "Quantity" Then
        ElseIf ddlType.Text = "Value" Then
        End If
        Dim strReportName As String = ""

        If rdbSummary.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesRoutewiseBoth"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesRoutewiseQty"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesRoutewiseValue"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                frmCrystalReportViewer.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If

        ElseIf rdbDetail.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesItemwiseBoth"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesItemwiseQty"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesItemwiseValue"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                frmCrystalReportViewer.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If

        ElseIf rdbHierwise.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesHierBoth"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesHierQtywise"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesHierwisevalue"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                frmCrystalReportViewer.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If
        ElseIf rdbRoutewise.IsChecked = True Then
                If ddlType.Text = "Both" Then
                    strReportName = "crptProvisionalSalesRoutewiseBothSummary"
                ElseIf ddlType.Text = "Quantity" Then
                    strReportName = "crptProvisionalSalesRoutewiseQtySummary"
                ElseIf ddlType.Text = "Value" Then
                    strReportName = "crptProvisionalSalesRoutewiseValueSummary"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                frmCrystalReportViewer.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If
            End If
    End Sub
    
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            Dim dt1 As DataTable
            ' Dim strSql4 As String
            Dim strItemCodestring, strItemCode As String
            strItemCodestring = ""
            dt1 = clsDBFuncationality.GetDataTable("SELECT distinct item_code FROM TSPL_TRANSFER_DETAIL")
            ' While dr.Read
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr As DataRow In dt1.Rows
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                Next
                'End While
            End If
            strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
            'Dim StrQuery As String = "SELECT  transfer_no, " & strItemCodestring & " " & _
            '                         "FROM (SELECT transfer_no,item_code,Item_Qty FROM TSPL_TRANSFER_DETAIL) up " & _
            '                         "PIVOT (SUM(item_qty) FOR item_code IN (" & strItemCodestring & ")) AS pvt " & _
            '                         "ORDER BY transfer_no"
            'clsDBFuncationality.ExecuteNonQuery(StrQuery)

            Dim strSQL1Group, strReportTitle, strOrderColumn, strOrderBy, strConverted, head2, TDMCOdecolumn, group1, additional As String
            strSQL1Group = ""
            head2 = ""
            TDMCOdecolumn = ""
            strConverted = ""
            strReportTitle = ""
            strOrderColumn = ""
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
                Return
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Class or select ALL")
                Return
            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
                Return
            End If


            If rdbSku.IsChecked = True Then
                strSQL1Group = "TEMP_PROVISIONAL_SALES.Item_Code"
                strReportTitle = "Provisional Sale Sku wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
                strReportTitle = "Provisional Sale Pack wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strReportTitle = "Provisional Sale Flavour wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
            End If

            Dim strSubQry1 As String = ""
            If ddlConvert.Text = "Converted" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1) * isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz'),0)"
                strConverted = "8oz"
            ElseIf ddlConvert.Text = "Raw" Then
                'strSubQry1 = "isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Raw'),0)"
                strSubQry1 = "1"
                strConverted = "Raw"
            End If





            If ddlcategory.Text = "HOS" Then
                head2 = "HOS"
                TDMCOdecolumn = "HOS"
                group1 = "HOS"
                additional = "(HOS)"
            ElseIf ddlcategory.Text = "TDM" Then
                head2 = "TDM"
                TDMCOdecolumn = "TDM"
                group1 = "TDM"
                additional = "(TDM)"
            ElseIf ddlcategory.Text = "ADC" Then
                head2 = "ADC"
                TDMCOdecolumn = "ADC"
                group1 = "ADC"
                additional = "(ADC)"
            ElseIf ddlcategory.Text = "CE" Then
                head2 = "CE"
                TDMCOdecolumn = "CE"
                group1 = "CE"
                additional = "(CE)"

            ElseIf ddlcategory.Text = "CE" Then
                head2 = "CE"
                TDMCOdecolumn = "CE"
                group1 = "CE"
                additional = "(CE)"

            ElseIf ddlcategory.Text = "SalesMan" Then
                head2 = "SalesMan"
                TDMCOdecolumn = "Salesmancode"
                group1 = "Salesmancode"
                additional = "(SalesMan)"

            End If

            Dim StrSql As String = "select " & strSQL1Group & " as Group1,'" + head2 + "' as heading2," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " as HierCode,TSPL_EMPLOYEE_MASTER_1.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Desc ," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No, " & _
                   "(LoadOutQty/Conversion_Factor - (isnull(LoadInQty/Conversion_Factor,0) + isnull(TEMP_PROVISIONAL_SALES.Breakage,0)+isnull(TEMP_PROVISIONAL_SALES.Leak,0) + isnull(TEMP_PROVISIONAL_SALES.Shortage,0))) *  " & strSubQry1 & " as sale, " & _
                   "'" & dtpFdate.Value & "' as fromDate,'" & dtpToDate.Value & "' as ToDate,'" & strReportTitle & "' as ReportTitle," & strOrderColumn & " as OrderBy, " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location, " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name,'" & strConverted & "' AS Convertion,((BasicPrice_WithTax + Empty_Value + TPT_Value) * LoadoutQty) - " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Amount as Value FROM " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER  as TSPL_EMPLOYEE_MASTER_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " = TSPL_EMPLOYEE_MASTER_1.EMP_CODE INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code AND  " & _
                   "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Pack_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Flavour_Code = TSPL_ITEM_DETAILS_1.Class_Code AND " & _
                   "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code INNER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No AND " & _
                   "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No AND " & _
                   "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code  AND " & _
                   " " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.MRP = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP LEFT OUTER JOIN " & _
                   "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
                   "where " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date < = CONVERT(date,'" & dtpToDate.Value & "',103) "
            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            Dim strQuery As String = clsCommon.GetQueryWithAllSelectedDataBase(StrSql, ArrDBName, False)
            clsDBFuncationality.ExecuteNonQuery(strQuery)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
