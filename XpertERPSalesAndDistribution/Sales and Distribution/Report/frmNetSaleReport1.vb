'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 22/10/2012-------------------------------------
'--------------------------------Last modify Time - 10:55 AM -------------------------------------
'--------------------------------Last modify Date - 24/12/2012  12:25 PM -------------------------------------
''''''''''''''  for bug no BM00000000905
Imports XpertERPEngine
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


Public Class FrmNetSaleReport1
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    ' Dim dr As SqlDataReader
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                'dr.Read()
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If

    End Sub
    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Item Description"
    End Sub




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnNetSaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmNetSaleReport1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub FrmNetSaleReport1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        rdbSku.IsChecked = True
        ddlConvert.Text = "Raw"
        ddlUnit.Text = "Percentage"
        ddlValue.Text = "Yes"
        LoadType()
        chktypeAll.IsChecked = True


        LoadCustomer()
        chkCustomerAll.IsChecked = True
        LoadTemplate()
        chktempall.IsChecked = True

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

        rdbClass.IsChecked = True



        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "NET-SL-RPT"
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
    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
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


    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        chkLocatioAll.IsChecked = True
        chkItemAll.IsChecked = True
        LoadLocation()
        LoadItem()
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        rdbSku.IsChecked = True
        ddlConvert.Text = "Converted"
        ddlUnit.Text = "Percentage"
        ddlValue.Text = "Yes"
        LoadType()
        chktypeAll.IsChecked = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()

    End Sub
    Sub print()
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Item or select ALL")
            Return
        ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Class or select ALL")
            Return
        ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Route Class or select ALL")
            Return
        ElseIf chktypeSelect.IsChecked = True AndAlso cbgtype.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Type Class or select ALL")
            Return
        End If

        Dim strSQL3Group, strLocAll, strItemAll, strCustClass, strSQL1Group, strSQL2Group, strOrderColumn, strOrderBy, strSubQry1, strSubQry2, strUnit1, strUnit3, strUnit2, strRouteAll, strcustomer, strtemplate As String
        strOrderColumn = ""
        strSubQry1 = ""
        strSQL2Group = ""
        strSQL1Group = ""
        strSubQry2 = ""
        strSQL3Group = ""
        If rdbSku.IsChecked = True Then
            strSQL1Group = "TSPL_SALE_RETURN_DETAIL.Item_Code"
            strSQL2Group = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            strSQL3Group = "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code"
            strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
        ElseIf rdbPack.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
            strSQL2Group = " TSPL_ITEM_DETAILS.Class_Desc"
            strSQL3Group = " TSPL_ITEM_DETAILS.Class_Desc"
            strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
        ElseIf rdbFlavour.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strSQL2Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strSQL3Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
        End If
        If chkLocatioAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkItemAll.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If
        If chkClassAll.IsChecked = True Then
            strCustClass = "Y"
        Else
            strCustClass = "N"
        End If

        If chkRouteAll.IsChecked = True Then
            strRouteAll = "Y"
        Else
            strRouteAll = "N"
        End If

        If chkCustomerAll.IsChecked = True Then
            strcustomer = "Y"
        Else
            strcustomer = "N"
        End If

        If chktempall.IsChecked = True Then
            strtemplate = "Y"
        Else
            strtemplate = "N"
        End If


        Dim strValue1, strValue2, strType, strConverted, strUnitType, strDiscount, strSubQry3 As String
        strDiscount = ""
        strType = ""
        strValue2 = ""
        strConverted = ""
        strSubQry3 = ""
        strValue1 = ""
        If ddlUnit.Text = "Percentage" Then
            If ddlConvert.Text = "Converted" Then

                strSubQry1 = "(isnull((select TSPL_ITEM_UOM_DETAIL.TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con')"
                strSubQry2 = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con')"
                strSubQry3 = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con')"

                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then

                strSubQry1 = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con') * (select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz')"
                strSubQry2 = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con') * (select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz')"
                strSubQry3 = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con') * (select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz')"

                strConverted = "8oz"
            ElseIf ddlConvert.Text = "Raw" Then

                strSubQry1 = "1"
                strSubQry2 = "1"
                strSubQry3 = "1"

                strConverted = "Raw"
            End If
        Else
            strSubQry1 = "1"
            strSubQry2 = "1"

            If ddlConvert.Text = "Converted" Then
                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then
                strConverted = "8oz"
            ElseIf ddlConvert.Text = "Raw" Then
                strConverted = "Raw"
            End If
        End If

        If ddlUnit.Text = "Percentage" Then
            strUnit1 = "/(TSPL_ITEM_UOM_DETAIL.Conversion_Factor)"
            strUnit2 = "/(TSPL_ITEM_UOM_DETAIL.Conversion_Factor)"
            strUnit3 = "/(TSPL_ITEM_UOM_DETAIL.Conversion_Factor)"

            strUnitType = "Percentage"

        Else
            strUnit1 = " *(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from tspl_item_uom_detail inner join TSPL_UNIT_MASTER on TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code  where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and Create_Price ='Y' and UOM_Code <> TSPL_SALE_INVOICE_DETAIL.Unit_code )"
            strUnit2 = " *(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from tspl_item_uom_detail inner join TSPL_UNIT_MASTER on TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code  where Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and Create_Price ='Y' and UOM_Code <> TSPL_SALE_RETURN_DETAIL.Unit_code )"
            strUnit3 = " *(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from tspl_item_uom_detail inner join TSPL_UNIT_MASTER on TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code  where Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and Create_Price ='Y' and UOM_Code <> TSPL_SALE_RETURN_INTER_DETAIL.Unit_code )"

            strUnitType = "Value"
        End If


        '-----------For UOM Filter------------------
        Dim filter As String = ""
        Dim filter1 As String = ""
        Dim filter2 As String = ""
        If chktypeSelect.IsChecked Then
            filter = " and TSPL_SALE_INVOICE_DETAIL.Unit_Code in  (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ")"
            filter1 = " and TSPL_SALE_RETURN_DETAIL.Unit_Code in  (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ")"
            filter2 = " and TSPL_SALE_RETURN_INTER_DETAIL.Unit_Code in  (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ")"

        End If
        Dim strCustCodeINV, strCustNameINV, strCustCodeREt, strCustNameREt, strCustCodeInter, strCustNameInter, strValue3 As String
        strCustNameINV = ""
        strCustCodeINV = ""
        strCustCodeREt = ""
        strCustNameREt = ""
        strCustNameInter = ""
        strCustCodeInter = ""
        strValue3 = ""
        If ddlValue.Text = "Yes" Then
            strValue1 = "case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else (TSPL_SALE_INVOICE_DETAIL.Basic_Rate+TSPL_SALE_INVOICE_DETAIL.Item_Tax+TSPL_SALE_INVOICE_DETAIL.TPT) end"
            strValue2 = "case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else (TSPL_SALE_RETURN_DETAIL.Basic_Rate+TSPL_SALE_RETURN_DETAIL.Item_Tax+TSPL_SALE_RETURN_DETAIL.TPT) end"
            strValue3 = "case when (scheme_item='Y' ) then 0 else (TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate+TSPL_SALE_RETURN_INTER_DETAIL.Item_Tax+TSPL_SALE_RETURN_INTER_DETAIL.TPT) end"

            strType = "Value"
            strDiscount = "((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  - (Price_Amount1 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))"

        ElseIf ddlValue.Text = "No" Then
            strValue1 = "case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else (TSPL_SALE_INVOICE_DETAIL.Basic_Rate+TSPL_SALE_INVOICE_DETAIL.Item_Tax+TSPL_SALE_INVOICE_DETAIL.TPT) end"
            strValue2 = "case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else (TSPL_SALE_RETURN_DETAIL.Basic_Rate+TSPL_SALE_RETURN_DETAIL.Item_Tax+TSPL_SALE_RETURN_DETAIL.TPT) end"
            strValue3 = "case when (scheme_item='Y' ) then 0 else (TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate+TSPL_SALE_RETURN_INTER_DETAIL.Item_Tax+TSPL_SALE_RETURN_INTER_DETAIL.TPT) end"

            strType = "Qty"
            strDiscount = "((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  - (Price_Amount1 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))"
        ElseIf ddlValue.Text = "Both" Then
            strValue1 = "case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else (TSPL_SALE_INVOICE_DETAIL.Basic_Rate+TSPL_SALE_INVOICE_DETAIL.Item_Tax+TSPL_SALE_INVOICE_DETAIL.TPT) end"
            strValue2 = "case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else (TSPL_SALE_RETURN_DETAIL.Basic_Rate+TSPL_SALE_RETURN_DETAIL.Item_Tax+TSPL_SALE_RETURN_DETAIL.TPT) end"
            strValue3 = "case when (scheme_item='Y' ) then 0 else (TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate+TSPL_SALE_RETURN_INTER_DETAIL.Item_Tax+TSPL_SALE_RETURN_INTER_DETAIL.TPT) end"

            strType = "Both"
            strDiscount = "((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  - (Price_Amount1 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))"

        End If
        Dim strSql1, strSqlInter As String
        Dim strSql2 As String
        If rdbClass.IsChecked Then
            strCustCodeINV = "tspl_customer_master.Customer_Class"
            strCustNameINV = "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc"
            strCustCodeREt = "tspl_customer_master.Customer_Class"
            strCustNameREt = "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc"
            strCustCodeInter = "tspl_customer_master.Customer_Class"
            strCustNameInter = "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc"
        ElseIf rdbCustomer.IsChecked Then
            strCustCodeINV = "TSPL_CUSTOMER_MASTER.Cust_Code"
            strCustNameINV = "TSPL_CUSTOMER_MASTER.Customer_Name"
            strCustCodeREt = "TSPL_CUSTOMER_MASTER.Cust_Code"
            strCustNameREt = "TSPL_CUSTOMER_MASTER.Customer_Name"
            strCustCodeInter = "TSPL_CUSTOMER_MASTER.Cust_Code"
            strCustNameInter = "TSPL_CUSTOMER_MASTER.Customer_Name"
        ElseIf rdbRouteeise.IsChecked Then
            strCustCodeINV = "TSPL_SALE_INVOICE_HEAD.Route_No"
            strCustNameINV = "TSPL_SALE_INVOICE_HEAD.Route_Desc"
            strCustCodeREt = "TSPL_SALE_Return_HEAD.Route_No"
            strCustNameREt = "TSPL_SALE_Return_HEAD.Route_Desc"
            strCustCodeInter = "TSPL_SALE_RETURN_INTER_HEAD.Route_No"
            strCustNameInter = "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc"
        End If

        '''''' added  by priit on 03/05/2013 for both option

        If ddlConvert.Text = "Raw" And ddlUnit.Text = "Percentage" Then

            strSql1 = "SELECT MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "case when TSPL_SALE_INVOICE_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end as MRPBottle,Scheme_Item,Discount_Code,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS date, " & _
                                "" & strSQL2Group & " as Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc,TSPL_SALE_INVOICE_DETAIL.Unit_code as uom,TSPL_SALE_INVOICE_DETAIL.Location, " & _
                                "case when (scheme_item='N') then Total_Item_Amt else 0 end  AS GrossSale,ISNULL(((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strSubQry1 & ") ,0)   AS GrossSaleQty," & _
                                "TSPL_SALE_INVOICE_HEAD.Comp_Code,CONVERT(date, '" & fromDate.Value & "', 103) AS Fdate, " & _
                                "CONVERT(date, '" & ToDate.Value & "', 103) AS tdate," & _
                                " TSPL_LOCATION_MASTER.Location_Desc,case when (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                                "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') then " & _
                                " (TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strDiscount & " else 0 end as Discount, " & _
                                " case when (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                                "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') then " & _
                                " ((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strSubQry1 & ")  else 0 end as DiscountQty, " & _
                                "0 as LoadIn," & strOrderColumn & " as OrderBy,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc " & _
                                "," & strCustCodeINV & " as Customer_Class," & strCustNameINV & " as Cust_Type_Desc,'" & strType & "' as Type,'" & strConverted & "' as Converted,'" & strUnitType & "' as UnitType " & _
                                "FROM TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                                  "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 RIGHT OUTER JOIN " & _
                                  "TSPL_SALE_INVOICE_DETAIL ON TSPL_ITEM_DETAILS_1.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                                  "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code ON  " & _
                                  "TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                                  "TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                                  "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                                  "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
                                  "TSPL_CUSTOMER_MASTER INNER JOIN " & _
                                  "TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
                                  "TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON " & _
                                  "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
                                  "left outer join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                " where (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
                                "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + filter + " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' "

            strSql2 = "SELECT MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "case when TSPL_SALE_Return_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end as MRPBottle,Scheme_Item,Discount_Code,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS date, " & _
       "" & strSQL1Group & " as Item_Code, TSPL_SALE_RETURN_DETAIL.Item_Desc, " & _
       "TSPL_SALE_RETURN_DETAIL.Unit_code AS uom, TSPL_SALE_INVOICE_HEAD.Location, " & _
       "CASE WHEN (TSPL_SALE_RETURN_DETAIL.scheme_item = 'N') THEN - (TSPL_SALE_RETURN_DETAIL.Total_Item_Amt) ELSE 0 END AS GrossSale, " & _
       "-((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ") AS GrossSaleQty, " & _
       "TSPL_SALE_INVOICE_HEAD.Comp_Code, CONVERT(date, '" & fromDate.Value & "', 103) AS Fdate, " & _
       "CONVERT(date, '" & ToDate.Value & "', 103) AS tdate," & _
       "TSPL_LOCATION_MASTER.Location_Desc, CASE WHEN (TSPL_SALE_RETURN_DETAIL.Scheme_Item = 'Y' OR " & _
       "TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Item = 'Y' OR TSPL_SALE_RETURN_DETAIL.Sampling_Item = 'Y') " & _
       "THEN - ((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ") * " & strDiscount & " ELSE 0 END AS Discount, " & _
       "CASE WHEN (TSPL_SALE_RETURN_DETAIL.Scheme_Item = 'Y' OR " & _
       "TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Item = 'Y' OR TSPL_SALE_RETURN_DETAIL.Sampling_Item = 'Y') " & _
       "THEN - ((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ")  ELSE 0 END AS DiscountQty, " & _
       "0 AS LoadIn, " & strOrderColumn & " AS OrderBy, TSPL_SALE_INVOICE_HEAD.Route_No, " & _
       "TSPL_SALE_INVOICE_HEAD.Route_Desc, " & strCustCodeREt & " as Customer_Class," & strCustNameREt & " as Cust_Type_Desc, " & _
       "'" & strType & "' as Type,'" & strConverted & "' as Converted,'" & strUnitType & "' AS UnitType " & _
       "FROM  TSPL_SALE_RETURN_HEAD LEFT OUTER JOIN " & _
       "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " & _
       "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
       "TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
       "TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
       "TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code ON " & _
       "TSPL_SALE_RETURN_HEAD.Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
       "TSPL_ITEM_DETAILS RIGHT OUTER JOIN " & _
       "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
       "TSPL_SALE_RETURN_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code LEFT OUTER JOIN " & _
       "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON " & _
       "TSPL_ITEM_DETAILS.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code LEFT OUTER JOIN " & _
       "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
       "TSPL_SALE_RETURN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code ON  " & _
       "TSPL_SALE_RETURN_HEAD.Sale_Return_No = TSPL_SALE_RETURN_DETAIL.Sale_Return_No " & _
       " left outer join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
       "WHERE (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
       "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
       "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + filter + " and TSPL_SALE_RETURN_HEAD.Is_Post='Y' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' "

            strSqlInter = " Union all SELECT MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end as MRPBottle,Scheme_Item,'' as Discount_Code,TSPL_SALE_RETURN_INTER_HEAD.Document_Date AS date, " & _
            "" & strSQL3Group & " as Item_Code, TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code AS uom, TSPL_SALE_RETURN_INTER_HEAD.Location, " & _
            "CASE WHEN (TSPL_SALE_RETURN_INTER_DETAIL.scheme_item = 'N') THEN - (TSPL_SALE_RETURN_INTER_DETAIL.Total_Item_Amt) ELSE 0 END AS GrossSale, " & _
            "-((TSPL_SALE_RETURN_INTER_DETAIL.Qty " & strUnit3 & ") * " & strSubQry3 & ") AS GrossSaleQty, " & _
            " TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, " & _
            " CONVERT(date, '" & fromDate.Value & "', 103) AS Fdate, " & _
            "CONVERT(date, '" & ToDate.Value & "', 103) AS tdate," & _
            "TSPL_LOCATION_MASTER.Location_Desc, " & _
            " 0 AS Discount, " & _
            " 0 AS DiscountQty, 0 AS LoadIn, " & _
            " " & strOrderColumn & " AS OrderBy, TSPL_SALE_RETURN_INTER_HEAD.Route_No, TSPL_SALE_RETURN_INTER_HEAD.Route_Desc, " & _
            " " & strCustCodeInter & " as Customer_Class," & strCustNameInter & " as Cust_Type_Desc, " & _
            "'" & strType & "' as Type,'" & strConverted & "' as Converted,'" & strUnitType & "' AS UnitType FROM  " & _
            "TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_SALE_RETURN_INTER_DETAIL on  " & _
            "TSPL_SALE_RETURN_INTER_HEAD.Document_No=TSPL_SALE_RETURN_INTER_DETAIL.Document_No " & _
            "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON  TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
            "LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code " & _
            "LEFT OUTER JOIN  TSPL_ITEM_DETAILS on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_DETAILS.Item_Code  " & _
            "left OUTER JOIN TSPL_ITEM_MASTER on  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
            "LEFT OUTER JOIN TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON  " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            " left outer join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "WHERE " & _
            "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
            "convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <=  convert(date, '" & ToDate.Value & "',103)  " + filter2 + " and " & _
            "TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' "


        Else


            strSql1 = "SELECT MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "case when TSPL_SALE_INVOICE_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end as MRPBottle,Scheme_Item,Discount_Code,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS date, " & _
                                "" & strSQL2Group & " as Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc,TSPL_SALE_INVOICE_DETAIL.Unit_code as uom,TSPL_SALE_INVOICE_DETAIL.Location, " & _
                                "ISNULL(((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strSubQry1 & ") * " & strValue1 & ",0)   AS GrossSale," & _
                                "ISNULL(((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strSubQry1 & ") ,0)   AS GrossSaleQty, " & _
                                "TSPL_SALE_INVOICE_HEAD.Comp_Code,CONVERT(date, '" & fromDate.Value & "', 103) AS Fdate, " & _
                                "CONVERT(date, '" & ToDate.Value & "', 103) AS tdate," & _
                                " TSPL_LOCATION_MASTER.Location_Desc,case when (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') then " & _
                                " ((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strSubQry1 & ") * " & strDiscount & " else 0 end as Discount, " & _
                                "case when (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') then " & _
                                " ((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & strUnit1 & ") * " & strSubQry1 & ") else 0 end as DiscountQty, " & _
                                "0 as LoadIn," & strOrderColumn & " as OrderBy,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc " & _
                                 "," & strCustCodeINV & " as Customer_Class," & strCustNameINV & " as Cust_Type_Desc,'" & strType & "' as Type,'" & strConverted & "' as Converted,'" & strUnitType & "' as UnitType " & _
                                "FROM  TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_SALE_INVOICE_DETAIL ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_DETAIL.Location LEFT OUTER JOIN " & _
                      "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON  " & _
                      "TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                      "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                      "TSPL_ITEM_UOM_DETAIL  ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                      "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code LEFT OUTER JOIN " & _
                      "TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
                      "TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON  " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
                      "left outer join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                " where (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
                                "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + filter + " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' "

            strSql2 = "SELECT MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "case when TSPL_SALE_Return_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end as MRPBottle,Scheme_Item,Discount_Code,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS date, " & _
       "" & strSQL1Group & " as Item_Code, TSPL_SALE_RETURN_DETAIL.Item_Desc, " & _
       "TSPL_SALE_RETURN_DETAIL.Unit_code AS uom, TSPL_SALE_INVOICE_HEAD.Location, " & _
       "- ISNULL(((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ") * " & strValue2 & ",0)  AS GrossSale, " & _
       "-((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ") AS GrossSaleQty, " & _
       "TSPL_SALE_INVOICE_HEAD.Comp_Code, CONVERT(date, '" & fromDate.Value & "', 103) AS Fdate, " & _
       "CONVERT(date, '" & ToDate.Value & "', 103) AS tdate," & _
       "TSPL_LOCATION_MASTER.Location_Desc, CASE WHEN (TSPL_SALE_RETURN_DETAIL.Scheme_Item = 'Y' OR " & _
       "TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Item = 'Y' OR TSPL_SALE_RETURN_DETAIL.Sampling_Item = 'Y') " & _
       "THEN - ((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ") * " & strDiscount & " ELSE 0 END AS Discount, " & _
       "CASE WHEN (TSPL_SALE_RETURN_DETAIL.Scheme_Item = 'Y' OR " & _
       "TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Item = 'Y' OR TSPL_SALE_RETURN_DETAIL.Sampling_Item = 'Y') " & _
       "THEN - ((TSPL_SALE_RETURN_DETAIL.Return_Qty " & strUnit2 & ") * " & strSubQry2 & ")  ELSE 0 END AS DiscountQty, " & _
       "0 AS LoadIn, " & strOrderColumn & " AS OrderBy, TSPL_SALE_INVOICE_HEAD.Route_No, " & _
       "TSPL_SALE_INVOICE_HEAD.Route_Desc, " & strCustCodeREt & " as Customer_Class," & strCustNameREt & " as Cust_Type_Desc, " & _
       "'" & strType & "' as Type,'" & strConverted & "' as Converted,'" & strUnitType & "' AS UnitType " & _
       "FROM  TSPL_SALE_RETURN_HEAD LEFT OUTER JOIN " & _
       "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " & _
       "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
       "TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
       "TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
       "TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code ON " & _
       "TSPL_SALE_RETURN_HEAD.Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
       "TSPL_ITEM_DETAILS RIGHT OUTER JOIN " & _
       "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
       "TSPL_SALE_RETURN_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code LEFT OUTER JOIN " & _
       "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON " & _
       "TSPL_ITEM_DETAILS.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code LEFT OUTER JOIN " & _
       "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
       "TSPL_SALE_RETURN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code ON  " & _
       "TSPL_SALE_RETURN_HEAD.Sale_Return_No = TSPL_SALE_RETURN_DETAIL.Sale_Return_No " & _
       " left outer join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
       "WHERE  (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
       "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
       "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + filter + " and TSPL_SALE_RETURN_HEAD.Is_Post='Y'  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' "


            strSqlInter = " Union all SELECT MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end as MRPBottle,Scheme_Item,'' as Discount_Code,TSPL_SALE_RETURN_INTER_HEAD.Document_Date AS date, " & _
          "" & strSQL3Group & " as Item_Code, TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, " & _
          "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code AS uom, TSPL_SALE_RETURN_INTER_HEAD.Location, " & _
          "- ISNULL(((TSPL_SALE_RETURN_INTER_DETAIL.Qty " & strUnit3 & ") * " & strSubQry3 & ") * " & strValue3 & ",0) AS GrossSale, " & _
          "-((TSPL_SALE_RETURN_INTER_DETAIL.Qty " & strUnit3 & ") * " & strSubQry3 & ") AS GrossSaleQty, " & _
          " TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, " & _
          " CONVERT(date, '" & fromDate.Value & "', 103) AS Fdate, " & _
          "CONVERT(date, '" & ToDate.Value & "', 103) AS tdate," & _
          "TSPL_LOCATION_MASTER.Location_Desc, " & _
          " 0 AS Discount, " & _
          " 0 AS DiscountQty, 0 AS LoadIn, " & _
          " " & strOrderColumn & " AS OrderBy, TSPL_SALE_RETURN_INTER_HEAD.Route_No, TSPL_SALE_RETURN_INTER_HEAD.Route_Desc, " & _
          " " & strCustCodeInter & " as Customer_Class," & strCustNameInter & " as Cust_Type_Desc, " & _
          "'" & strType & "' as Type,'" & strConverted & "' as Converted,'" & strUnitType & "' AS UnitType FROM  " & _
          "TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_SALE_RETURN_INTER_DETAIL on  " & _
          "TSPL_SALE_RETURN_INTER_HEAD.Document_No=TSPL_SALE_RETURN_INTER_DETAIL.Document_No " & _
          "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
          "LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON  TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
          "LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code " & _
          "LEFT OUTER JOIN  TSPL_ITEM_DETAILS on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_DETAILS.Item_Code  " & _
          "left OUTER JOIN TSPL_ITEM_MASTER on  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
          "LEFT OUTER JOIN TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " & _
          "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON  " & _
          "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
          "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
          " left outer join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
          "WHERE " & _
          "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
          "convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
          "convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <=  convert(date, '" & ToDate.Value & "',103)  " + filter2 + " and " & _
          "TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' "

        End If


        If strLocAll = "N" Then
            strSql1 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            strSql2 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            strSqlInter += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

        End If

        If strItemAll = "N" Then
            strSql1 += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            strSql2 += " and TSPL_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            strSqlInter += " and TSPL_SALE_RETURN_INTER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If

        If strRouteAll = "N" Then
            strSql1 += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            strSql2 += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            strSqlInter += " and TSPL_SALE_RETURN_INTER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strCustClass = "N" Then
            strSql1 += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql2 += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSqlInter += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
        End If


        If chktempall.IsChecked = True Then
            If chkCustomerSelect.IsChecked = True Then
                strSql1 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                strSql2 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                strSqlInter += " and  TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
            End If
        ElseIf chktempselect.IsChecked = True Then
            strSql1 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSql2 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSqlInter += " and  TSPL_SALE_RETURN_INTER_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            If chkCustomerSelect.IsChecked = True Then
                strSql1 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                strSql2 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                strSqlInter += " and  TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
            End If
        End If



        If chkIC.Checked = False Then
            strSqlInter = ""
        End If

        Dim strSql As String

        strSql = strSql1 & " Union All " & strSql2 & strSqlInter

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
            Dim frmcrystal As New frmCrystalReportViewer()
            If clsCommon.CompairString(ddlValue.Text, "Yes") = CompairStringResult.Equal Then
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptNetSaleValueVizag", "Net Sale Report")
            ElseIf clsCommon.CompairString(ddlValue.Text, "No") = CompairStringResult.Equal Then
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptNetSaleQtyVizag", "Net Sale Report")
            ElseIf clsCommon.CompairString(ddlValue.Text, "Both") = CompairStringResult.Equal Then
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptNetSaleBothVizag", "Net Sale Report")
            End If

        Else
            Dim frmcrystal As New frmCrystalReportViewer()
            If clsCommon.CompairString(ddlValue.Text, "Yes") = CompairStringResult.Equal Then
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptNetSaleValueGuntur", "Net Sale Report")
            ElseIf clsCommon.CompairString(ddlValue.Text, "No") = CompairStringResult.Equal Then
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptNetSaleQtyGuntur", "Net Sale Report")
            ElseIf clsCommon.CompairString(ddlValue.Text, "Both") = CompairStringResult.Equal Then
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptNetSaleBothGuntur", "Net Sale Report")
            End If

        End If
       


    End Sub
    Private Sub btnClose_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub
    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt = GetItemType()
        ' Dim qry As String = "select CUST_CATEGORY_CODE,CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER order by CUST_CATEGORY_CODE "
        'cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgtype.DataSource = dt
        cbgtype.ValueMember = "Code"
        cbgtype.DisplayMember = "Code"
    End Sub

    Public Shared Function GetItemType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "EC"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "FC"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "EB"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "FB"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "SH"
        dt.Rows.Add(dr)
        Return dt
    End Function



    Private Sub chktypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeAll.ToggleStateChanged
        cbgtype.Enabled = Not chktypeAll.IsChecked
    End Sub


    Private Sub btnReset_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        chkLocatioAll.IsChecked = True
        chkItemAll.IsChecked = True
        LoadLocation()
        LoadItem()
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        rdbSku.IsChecked = True
        ddlConvert.Text = "Raw"
        ddlUnit.Text = "Percentage"
        ddlValue.Text = "Yes"
        LoadType()
        chktypeAll.IsChecked = True
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        LoadTemplate()
        chktempall.IsChecked = True
        rdbClass.IsChecked = True

    End Sub

  
End Class
