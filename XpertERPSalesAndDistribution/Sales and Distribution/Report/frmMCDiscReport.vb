'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 20/06/2012-------------------------------------
'--------------------------------Last modify Time - 01:30 pm -------------------------------------
'--------------------------------Last modify date - 10/01/2013-------------------------------------

Imports Microsoft.VisualBasic
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 29/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
''''''''''''' for bug no BM00000000371
''''''''''''' for bug no BM00000000549
''''''''''''' for bug no BM00000000555

Imports XpertERPEngine
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
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Public Class FrmMCDiscReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    Dim dt As DataTable
    Dim ArrDBName As ArrayList = Nothing
    Dim strLocation As String
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

    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Item Code"
        cbgCustomer.DisplayMember = "Item Description"
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMCDiscountReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmMCDiscReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
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

    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        'Dim qry As String = "select Loc_Segment_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub FrmMCDiscReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadItem()

        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbSku.IsChecked = True
        LoadCompany()
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName
        LoadCustomer()
        LoadLocation()
        LoadRoute()
        chkLocationAll.IsChecked = True
        chkAllRoute.IsChecked = True
        chkCustomerAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnExportToExcel, "Press Alt+P for Export ")
        rdbNone.IsChecked = True
        rdbSku.IsChecked = True
    End Sub


    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master WHERE 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub reset()
        LoadItem()
        chkCustomerAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbSku.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        chkAllRoute.IsChecked = True
        rdbNone.IsChecked = True
        rdbSku.IsChecked = True
    End Sub

    'Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Me.Close()
    'End Sub

    '''' <summary>
    ''''  Customer class <> F
    '''' </summary>
    '''' <remarks> pending </remarks>
    Private Sub Print()
        Try

            Dim dt As New DataTable
            Dim strSeq, strGrp, strOrder, strGrpRet, strGrpInter As String

            strLocation = ""

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If

            If rdbSku.IsChecked Then
                strSeq = "Sku_Seq"
                strGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strGrpRet = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code"
                strGrpInter = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code"
                strOrder = "Order by Sku_Seq asc"
            Else
                strSeq = "Pack_Seq"
                strGrp = "(Class_Desc ) as Item_Code"
                strGrpRet = "(Class_Desc ) as Item_Code"
                strGrpInter = "(Class_Desc ) as Item_Code"
                strOrder = "Order by Pack_Seq asc"
            End If

            Dim str1 As String = "SELECT " & strSeq & ", TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS CaseConF," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id," & strGrp & " ," + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Salesman_Desc,  " & _
            "convert(decimal(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS ConsumerPrice,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS LessTP,(MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1) as TP ,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.DMC, 0) AS DMC,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VMOH, 0) AS VMOH,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Royalty, 0) AS Royalty,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Primary_Freight, 0) AS Primary_Freight,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Secondary_Freight, 0) AS Secondary_Freight,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VS_D_Router, 0) AS VS_D_Router,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VS_D_Loading_ULoading, 0) AS VS_D_Loading_ULoading,  " & _
            "convert(decimal(18,2),CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN   " & _
            "((Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  * ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS FOCAMt,  " & _
            "convert(decimal (18,2),CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Invoice_Qty * Cust_Discount) ELSE 0 END) AS Cust_DiscAmt,  " & _
            "convert(decimal(18,2),CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN ((isnull(Price_Amount2, 0) + isnull(Price_Amount3, 0)) * Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Acctamt,  " & _
            "convert(decimal(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS Planned_PackMix,  " & _
            "convert(decimal(18,2),case when Excisable='T' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3_Amt) * Invoice_Qty else 0 end) as ExciseTax,  " & _
            "convert(decimal(18,2),case when Excisable='T' then case when Scheme_Item='Y' then 0 else  (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX5_Amt) * Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end else case when Scheme_Item='Y' then 0 else  (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Amt) * Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end end) as Saletax,  " & _
            "convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN CONVERT(decimal(18, 2),(Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS DiscAmt,  " & _
            "convert(decimal(18,2),CASE WHEN Price_Amount5 <> 0 THEN Price_Amount5 * (Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Amount,  " & _
            "convert(decimal(18,2),CASE WHEN Price_Amount4 <> 0 THEN Price_Amount4 * (Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Amount,  " & _
            "convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN CONVERT(decimal(18, 2),(Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS VSDDiscAmt,  " & _
            "convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND Sampling = 'Y' THEN CONVERT(decimal(18, 2),(Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS SamplingDiscAmt,  " & _
            "convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN CONVERT(decimal(18, 2),(Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS OtherDiscAmt,  " & _
            "convert(decimal(18,2),case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' and Excisable='T' then Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as ExciseQty,  " & _
            "convert(decimal(18,2),case when Scheme_Item='Y' then 0 else Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Qty   " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Item_code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code = " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code  " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No=" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
            "WHERE (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) >= CONVERT(date, '" & fromDate.Value & "', 103)) AND   " & _
            "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) <= CONVERT(date, '" & ToDate.Value & "', 103))  AND  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_UOM_DETAIL_1.UOM_Code = 'FB')  and Is_Post='Y' "

            Dim str2 As String = "SELECT " & strSeq & ", TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS CaseConF," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Sale_Return_Id as Sale_Invoice_Id," & strGrpRet & " ," + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Salesman_Desc,  " & _
            "convert(decimal(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS ConsumerPrice,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount1 AS LessTP,(MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1) as TP ,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.DMC, 0) AS DMC,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VMOH, 0) AS VMOH,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Royalty, 0) AS Royalty,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Primary_Freight, 0) AS Primary_Freight,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Secondary_Freight, 0) AS Secondary_Freight,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VS_D_Router, 0) AS VS_D_Router,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VS_D_Loading_ULoading, 0) AS VS_D_Loading_ULoading,  " & _
            "-convert(decimal(18,2),CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN   " & _
            "((Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  * ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS FOCAMt,  " & _
            "-convert(decimal (18,2),CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Return_Qty * Cust_Discount) ELSE 0 END) AS Cust_DiscAmt,  " & _
            "-convert(decimal(18,2),CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN ((isnull(Price_Amount2, 0) + isnull(Price_Amount3, 0)) * Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Acctamt,  " & _
            "-convert(decimal(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS Planned_PackMix,  " & _
            "-convert(decimal(18,2),case when Excisable='T' and " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Unit_code='FC' then (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX1_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX2_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX3_Amt) * Return_Qty else 0 end) as ExciseTax,  " & _
            "-convert(decimal(18,2),case when Excisable='T' then case when Scheme_Item='Y' then 0 else  (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX4_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX5_Amt) * Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end else case when Scheme_Item='Y' then 0 else  (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX1_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.TAX2_Amt) * Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end end) as Saletax,  " & _
            "-convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN CONVERT(decimal(18, 2),(Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS DiscAmt,  " & _
            "-convert(decimal(18,2),CASE WHEN Price_Amount5 <> 0 THEN Price_Amount5 * (Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Amount,  " & _
            "-convert(decimal(18,2),CASE WHEN Price_Amount4 <> 0 THEN Price_Amount4 * (Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Amount,  " & _
            "-convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN CONVERT(decimal(18, 2),(Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS VSDDiscAmt,  " & _
            "-convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND Sampling = 'Y' THEN CONVERT(decimal(18, 2),(Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS SamplingDiscAmt,  " & _
            "-convert(decimal(18,2),CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN CONVERT(decimal(18, 2),(Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS OtherDiscAmt,  " & _
            "-convert(decimal(18,2),case when " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Unit_code='FC' and Excisable='T' then Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as ExciseQty,  " & _
            "-convert(decimal(18,2),case when Scheme_Item='Y' then 0 else Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Qty   " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Item_code = " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master ON " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Discount_Code = " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Sale_Return_No LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code  " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No=" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
            "WHERE (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) >= CONVERT(date, '" & fromDate.Value & "', 103)) AND   " & _
            "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) <= CONVERT(date, '" & ToDate.Value & "', 103))   AND  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_UOM_DETAIL_1.UOM_Code = 'FB')  and Is_Post='Y' "

            Dim str3 As String = " Union all SELECT " & strSeq & ", TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS MrpBottleConvRate, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No,  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc,  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Line_No as Sale_Invoice_Id, " & _
            "" & strGrpInter & " ," + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSpl_Route_master.Route_Desc,  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name,  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code,  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Salesman_Desc, " & _
            "convert(decimal(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS ConsumerPrice, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS LessTP," & _
            "(MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1) as TP , " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.DMC, 0) AS DMC,  ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VMOH, 0) AS VMOH,  " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Royalty, 0) AS Royalty,  ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Primary_Freight, 0) AS Primary_Freight, " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Secondary_Freight, 0) AS Secondary_Freight, " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VS_D_Router, 0) AS VS_D_Router, " & _
            "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.VS_D_Loading_ULoading, 0) AS VS_D_Loading_ULoading,  0 AS FOCAMt,  0 AS Cust_DiscAmt, " & _
            "- convert(decimal(18,2),CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN ((isnull(Price_Amount2, 0) + isnull(Price_Amount3, 0)) * Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Acctamt, " & _
            "-  convert(decimal(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS Planned_PackMix, " & _
            "- convert(decimal(18,2),case when Excisable='T' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt) * Qty else 0 end) as ExciseTax, " & _
            "- convert(decimal(18,2),case when Excisable='T' then case when Scheme_Item='Y' then 0 else  (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt) * qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end else case when Scheme_Item='Y' then 0 else  (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt) * Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end end) as Saletax, " & _
            "0 AS DiscAmt,  " & _
            "-convert(decimal(18,2),CASE WHEN Price_Amount5 <> 0 THEN Price_Amount5 * (Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Amount, " & _
            "- convert(decimal(18,2),CASE WHEN Price_Amount4 <> 0 THEN Price_Amount4 * (Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Amount, 0 AS VSDDiscAmt, 0 AS SamplingDiscAmt, 0 AS OtherDiscAmt, " & _
            "- convert(decimal(18,2),case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' and Excisable='T' then " & _
            "Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as ExciseQty,  " & _
            "-convert(decimal(18,2),case when Scheme_Item='Y' then 0 else Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Qty " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MC_MAPPING.Item_code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code " & _
            "LEFT OUTER JOIN  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No=" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
            "WHERE (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) >= CONVERT(date, '" & fromDate.Value & "', 103)) AND  " & _
            "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) <= CONVERT(date, '" & ToDate.Value & "', 103))   AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_UOM_DETAIL_1.UOM_Code = 'FB')  and Is_Post=1"

            If strLocation = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str3 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkSelectRoute.IsChecked = True Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ") "
                str3 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ") "
            End If

            If chkCustomerSelect.IsChecked = True Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                str3 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(str1 & " Union all " & str2 & str3, ArrDBName, False)

            Dim dt1 As New DataTable
            Dim decTotal As Decimal

            Dim strTotal As String = "select sum(Planned_PackMix) as Planned_PackMix from ( " & strQuery & " ) xx"
            dt1 = clsDBFuncationality.GetDataTable(strTotal)
            For Each dr As DataRow In dt1.Rows
                decTotal = clsCommon.myCdbl(dr("Planned_PackMix"))
            Next

            strQuery = "select " & strSeq & ",Item_Code,Route_No,Route_Desc,Cust_Code,Customer_Name,Percentage,Planned_PackMix,ConsumerPrice,MRPBottle,LessTP,price_To_Trade, " & _
          "Saletax,ExciseTax,NetRevenueBeforeDisc, Discount,NetRevenueAfterDisc, " & _
          "DMC,VMOH,Royalty,Primary_Freight,Secondary_Freight,VSandD,VSandRoute,VSandDLoading,OtherDisc, " & _
          "SamplingDisc,TotalCost,(NetRevenueAfterDisc-TotalCost) as Contribution from ( " & _
          "select " & strSeq & ",Route_No,Route_Desc,Cust_Code,Customer_Name,Item_Code,case when " & decTotal & "  > 0 then Planned_PackMix/ " & decTotal & " else 0 end as Percentage,Planned_PackMix,ConsumerPrice,MRPBottle,LessTP,price_To_Trade, " & _
          "Saletax,ExciseTax,NetRevenueBeforeDisc, Discount,(NetRevenueBeforeDisc - Discount) as NetRevenueAfterDisc, " & _
          "DMC,VMOH,Royalty,Primary_Freight,Secondary_Freight,VSandD,VSandRoute,VSandDLoading,OtherDisc, " & _
          "SamplingDisc,(VMOH+DMC+Royalty+Primary_Freight+Secondary_Freight+VSandD+ " & _
          "VSandRoute+VSandDLoading+OtherDisc+SamplingDisc) as TotalCost from ( " & _
          "select " & strSeq & ",Route_No,Route_Desc,Cust_Code,Customer_Name,Item_Code,Planned_PackMix,ConsumerPrice,MRPBottle,LessTP,price_To_Trade, " & _
          "Saletax,ExciseTax,(price_To_Trade - (ExciseTax + Saletax)) as NetRevenueBeforeDisc, " & _
          "case when Planned_PackMix > 0 then Disc/Planned_PackMix else 0 end as Discount,DMC,VMOH,Royalty,Primary_Freight,Secondary_Freight, " & _
          "case when Planned_PackMix > 0 then VSandD/Planned_PackMix else 0 end as VSandD,VSandRoute,VSandDLoading, " & _
          "case when Planned_PackMix > 0 then  OtherDiscAmt/Planned_PackMix else 0 end as OtherDisc, " & _
          "case when Planned_PackMix > 0 then SamplingDiscAmt/Planned_PackMix else 0 end as SamplingDisc from ( " & _
          "select " & strSeq & ",Route_No,Route_Desc,Cust_Code,Customer_Name,Item_Code,sum(Planned_PackMix) as Planned_PackMix,ConsumerPrice, " & _
          "ConsumerPrice/MrpBottleConvRate as MRPBottle,LessTP,TP as price_To_Trade, " & _
          "case when SUM(Qty) > 0 then  sum(Saletax)/SUM(Qty) else 0 end as Saletax, " & _
          "case when sum(ExciseQty) > 0 then sum(ExciseTax)/sum(ExciseQty) else 0 end as ExciseTax, " & _
          "sum(Cust_DiscAmt + Acctamt + FOCAMt + DiscAmt) as Disc,(DMC) as DMC,(VMOH) as VMOH, " & _
          "(Royalty) as Royalty,(Primary_Freight) as [Primary_Freight],(Secondary_Freight) as [Secondary_Freight], " & _
          "sum( VSDDiscAmt + Agency_Gross_Amount + Distributor_Gross_Amount ) as VSandD, " & _
          "(VS_D_Router) as VSandRoute,(VS_D_Loading_ULoading) as VSandDLoading,SUM(OtherDiscAmt) as OtherDiscAmt, " & _
          "SUM(SamplingDiscAmt) as SamplingDiscAmt from ( " & strQuery & " ) xx group by Item_Code,Route_No,Route_Desc,Cust_Code,Customer_Name,ConsumerPrice, " & _
          "MrpBottleConvRate,LessTP,TP,DMC,VMOH,Royalty,Primary_Freight,Secondary_Freight,VS_D_Loading_ULoading,VS_D_Router," & strSeq & " ) b ) bb)cc " & strOrder & ""




            dt = clsDBFuncationality.GetDataTable(strQuery)


            gvReport.DataSource = Nothing
            gvReport.Columns.Clear()
            gvReport.Rows.Clear()
            gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gvReport.DataSource = dt
                SetGridFormationOFgvReport()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try



        'MsgBox("successfully")
    End Sub



    Sub SetGridFormationOFgvReport()
        ' Dim strItemCode, head2 As String
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next

        If rdbSku.IsChecked = True Then
            gvReport.Columns("Item_Code").IsVisible = True
            gvReport.Columns("Item_Code").Width = 50
            gvReport.Columns("Item_Code").HeaderText = "Item Code"
        Else
            gvReport.Columns("Item_Code").IsVisible = True
            gvReport.Columns("Item_Code").Width = 50
            gvReport.Columns("Item_Code").HeaderText = "Pack Code"
        End If

        gvReport.Columns("Percentage").IsVisible = True
        gvReport.Columns("Percentage").Width = 50
        gvReport.Columns("Percentage").HeaderText = "Percentage"

        gvReport.Columns("Planned_PackMix").IsVisible = True
        gvReport.Columns("Planned_PackMix").Width = 50
        gvReport.Columns("Planned_PackMix").HeaderText = "PLANNED PACKMIX"

        gvReport.Columns("MRPBottle").IsVisible = True
        gvReport.Columns("MRPBottle").Width = 50
        gvReport.Columns("MRPBottle").HeaderText = "MRP Bottle"

        gvReport.Columns("ConsumerPrice").IsVisible = True
        gvReport.Columns("ConsumerPrice").Width = 50
        gvReport.Columns("ConsumerPrice").HeaderText = "Consumer Price"

        gvReport.Columns("LessTP").IsVisible = True
        gvReport.Columns("LessTP").Width = 50
        gvReport.Columns("LessTP").HeaderText = "Less Trade margin"

        gvReport.Columns("price_To_Trade").IsVisible = True
        gvReport.Columns("price_To_Trade").Width = 70
        gvReport.Columns("price_To_Trade").HeaderText = "Price to Trade"

        gvReport.Columns("Saletax").IsVisible = True
        gvReport.Columns("Saletax").Width = 70
        gvReport.Columns("Saletax").HeaderText = "Sales tax"

        gvReport.Columns("ExciseTax").IsVisible = True
        gvReport.Columns("ExciseTax").Width = 50
        gvReport.Columns("ExciseTax").HeaderText = "Excise Duty"


        gvReport.Columns("NetRevenueBeforeDisc").IsVisible = True
        gvReport.Columns("NetRevenueBeforeDisc").Width = 50
        gvReport.Columns("NetRevenueBeforeDisc").HeaderText = "NET REVENUE Before Discounts"

        gvReport.Columns("Discount").IsVisible = True
        gvReport.Columns("Discount").Width = 50
        gvReport.Columns("Discount").HeaderText = "Discounts"

        gvReport.Columns("NetRevenueAfterDisc").IsVisible = True
        gvReport.Columns("NetRevenueAfterDisc").Width = 80
        gvReport.Columns("NetRevenueAfterDisc").HeaderText = "NET REVENUE After Discounts"

        gvReport.Columns("DMC").IsVisible = True
        gvReport.Columns("DMC").Width = 80
        gvReport.Columns("DMC").HeaderText = "DMC"

        gvReport.Columns("VMOH").IsVisible = True
        gvReport.Columns("VMOH").Width = 80
        gvReport.Columns("VMOH").HeaderText = "VMOH"

        gvReport.Columns("ROYALTY").IsVisible = True
        gvReport.Columns("ROYALTY").Width = 80
        gvReport.Columns("ROYALTY").HeaderText = "ROYALTY"

        gvReport.Columns("Primary_Freight").IsVisible = True
        gvReport.Columns("Primary_Freight").Width = 80
        gvReport.Columns("Primary_Freight").HeaderText = "PRIMARY FREIGHT"

        gvReport.Columns("Secondary_Freight").IsVisible = True
        gvReport.Columns("Secondary_Freight").Width = 80
        gvReport.Columns("Secondary_Freight").HeaderText = "SECONDARY FREIFGHT"

        gvReport.Columns("VSandD").IsVisible = True
        gvReport.Columns("VSandD").Width = 80
        gvReport.Columns("VSandD").HeaderText = "VS and D"

        gvReport.Columns("VSandRoute").IsVisible = True
        gvReport.Columns("VSandRoute").Width = 80
        gvReport.Columns("VSandRoute").HeaderText = "VS and D ROUTE HELPER"

        gvReport.Columns("VSandDLoading").IsVisible = True
        gvReport.Columns("VSandDLoading").Width = 80
        gvReport.Columns("VSandDLoading").HeaderText = "VS and D LOADING AND UNLOADING"

        gvReport.Columns("OtherDisc").IsVisible = True
        gvReport.Columns("OtherDisc").Width = 80
        gvReport.Columns("OtherDisc").HeaderText = "Others"

        gvReport.Columns("SamplingDisc").IsVisible = True
        gvReport.Columns("SamplingDisc").Width = 80
        gvReport.Columns("SamplingDisc").HeaderText = "Sampling"

        gvReport.Columns("TotalCost").IsVisible = True
        gvReport.Columns("TotalCost").Width = 80
        gvReport.Columns("TotalCost").HeaderText = "TotalCost"

        gvReport.Columns("Contribution").IsVisible = True
        gvReport.Columns("Contribution").Width = 80
        gvReport.Columns("Contribution").HeaderText = "CONTRIBUTION"

        gvReport.Columns("Route_No").IsVisible = True
        gvReport.Columns("Route_No").Width = 80
        gvReport.Columns("Route_No").HeaderText = "Route No"

        gvReport.Columns("Route_Desc").IsVisible = True
        gvReport.Columns("Route_Desc").Width = 80
        gvReport.Columns("Route_Desc").HeaderText = "Route Desc"

        gvReport.Columns("Cust_Code").IsVisible = True
        gvReport.Columns("Cust_Code").Width = 80
        gvReport.Columns("Cust_Code").HeaderText = "Cust Code"

        gvReport.Columns("Customer_Name").IsVisible = True
        gvReport.Columns("Customer_Name").Width = 80
        gvReport.Columns("Customer_Name").HeaderText = "Customer Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Salestax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("ExciseTax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("NetRevenueBeforeDisc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("NetRevenueAfterDisc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item7 As New GridViewSummaryItem("VSandD", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("OtherDisc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("SamplingDisc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("TotalCost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("CONTRIBUTION", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)

        If rdbRoutewise.IsChecked Then
            gvReport.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Route format ""{0}: {1}"" Group By Route_No"))
            gvReport.GroupDescriptors.Add(New GridGroupByExpression("Route_Desc as Route format ""{0}: {1}"" Group By Route_Desc"))
            gvReport.ShowGroupPanel = False
            gvReport.MasterTemplate.AutoExpandGroups = True
        ElseIf rdbDistwise.IsChecked Then
            gvReport.GroupDescriptors.Add(New GridGroupByExpression("Cust_Code as Customer format ""{0}: {1}"" Group By Cust_Code"))
            gvReport.GroupDescriptors.Add(New GridGroupByExpression("Customer_Name as Customer format ""{0}: {1}"" Group By Customer_Name"))
            gvReport.ShowGroupPanel = False
            gvReport.MasterTemplate.AutoExpandGroups = True
        Else


        End If
        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvReport.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub


    

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked

    End Sub

    Private Sub btnClose_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbSku.IsChecked = True Then
            VarID += "_SK"
        Else
            rdbPack.IsChecked = True
            VarID += "_PA"
        End If
        gvReport.VarID = VarID
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GetReportGridID()
        Print()
    End Sub

    Private Sub btnReset_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub ExportToExcel()
        Try
            Dim strReportTitle As String = Nothing

            If rdbSku.IsChecked = True Then
                strReportTitle = "MARGINAL CONTRIBUTION Sku wise"
            ElseIf rdbPack.IsChecked = True Then
                strReportTitle = "MARGINAL CONTRIBUTION Pack wise"
            ElseIf rdbFlavour.IsChecked = True Then
                strReportTitle = "MARGINAL CONTRIBUTION Flavour wise"
            End If


            Dim saveDialog1 As New SaveFileDialog()
            saveDialog1.FileName = strReportTitle
            saveDialog1.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
            Dim Fullpath As String

            Dim path = "C:\\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(path)
            If IsExists = False Then
                System.IO.Directory.CreateDirectory(path)
            End If

            Fullpath = path + "\" + saveDialog1.FileName
            Dim i As Integer = 0
            For i = 0 To gvReport.ColumnCount - 1
                Dim grow As GridViewRowInfo = TryCast(gvReport.Rows(0), GridViewRowInfo)
                If TypeOf grow.Cells(i).Value Is DateTime Then
                    Dim datecol As GridViewDateTimeColumn = TryCast(gvReport.Columns(i), GridViewDateTimeColumn)
                    datecol.ExcelExportType = DisplayFormatType.ShortDate
                End If
            Next i
            Dim exporter As New ExportToExcelML(gvReport)
            exporter.SummariesExportOption = SummariesOption.ExportAll
            exporter.ExportHierarchy = True
            exporter.HiddenColumnOption = HiddenOption.DoNotExport
            exporter.SheetMaxRows = ExcelMaxRows._1048576
            AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            exporter.SheetName = strReportTitle
            exporter.RunExport(Fullpath)
            Me.Controls.Remove(gvReport)
            Dim xlsApp As Microsoft.Office.Interop.Excel.Application
            Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
            xlsApp = New Microsoft.Office.Interop.Excel.Application
            xlsApp.Visible = True
            xlsWB = xlsApp.Workbooks.Open(Fullpath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try

        ''saveDialog1.Filter = "Excel (*.xls)|*.xls"
        ''If saveDialog1.ShowDialog(Me) = System.System.Windows.Forms.DialogResult.OK Then
        ''    ''Me.RadProgressBar1.Text = "Exporting to ExcelML..."
        ''    ''Me.RadProgressBar1.Value1 = 0
        ''    ''Me.RadProgressBar1.Visible = True

        ''    Dim thread2 As New Thread(New ParameterizedThreadStart(AddressOf RunExportToExcelML))
        ''    thread2.Start(saveDialog1.FileName)
        ''End If
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As Object)
        ''    Try
        ''        Dim exporter As New ExportToExcelML(gvReport)
        ''        exporter.SummariesExportOption = SummariesOption.ExportAll
        ''        'If rdbSummary.IsChecked = True Then
        ''        '    exporter.ExportVisualSetting = True
        ''        'End If
        ''        exporter.ExportHierarchy = True
        ''        exporter.HiddenColumnOption = HiddenOption.DoNotExport
        ''        exporter.SheetMaxRows = ExcelMaxRows._1048576
        ''        AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
        ''        AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
        ''        exporter.RunExport(fileName.ToString())
        ''        Dim text As String = "Export finished successfully!"

        ''        Dim xlApp As Excel.Application
        ''        xlApp = New Excel.ApplicationClass
        ''        Process.Start(fileName.ToString())

        ''        common.clsCommon.MyMessageBoxShow(text)
        ''    Catch ex As Exception
        ''        common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        ''    End Try
    End Sub


    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        Dim strReportTitle, strOrderby As String
        strReportTitle = ""
        If rdbSku.IsChecked = True Then
            strReportTitle = "MARGINAL CONTRIBUTION Sku wise"
            strOrderby = "SKU - WISE Figures"

        ElseIf rdbPack.IsChecked = True Then
            strReportTitle = "MARGINAL CONTRIBUTION Pack wise"
            strOrderby = "PACK - WISE Figures"

        ElseIf rdbFlavour.IsChecked = True Then
            strReportTitle = "MARGINAL CONTRIBUTION Flavour wise"
            strOrderby = "FLAVOUR - WISE Figures"

        End If


        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, strReportTitle)
            Dim style2 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy"))
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))

            'If rbtnCompanySelect.IsChecked Then
            '    Dim strCompany As String = ""
            '    For Each Str As String In cbgCompany.CheckedDisplayMember
            '        If clsCommon.myLen(strCompany) > 0 Then
            '            strCompany += ", "
            '        End If
            '        strCompany += Str
            '    Next
            '    If strCompany = "" Then
            '        strCompany = "All"
            '    End If
            '    Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Company : " + strCompany)
            'End If

            'If chkLocationSelect.IsChecked Then
            '    Dim strLoca As String = ""
            '    For Each Str As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    Dim style6 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)
            'End if
        End If


    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Export()
    End Sub

    Sub Export()
        If gvReport.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    
    Private Sub chkAllRoute_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgroute.Enabled = Not chkAllRoute.IsChecked
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub
End Class
