''''              Modified by = Priti (23/10/2012) 09:45 am

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
Public Class FrmDailySettlementActualAndProvisionalReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String

    Public strReportType As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String, Optional ByVal ReportType As String = Nothing)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        'dr = connectSql.RunSqlReturnDR(sql)
        'dr.Read()
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
                strReportType = ReportType
            Next
        End If
    End Sub

    Sub LoadLocation()
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
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DailySettlement)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub

    Private Sub FrmDailySettlementActualAndProvisionalReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Sub LoadSalesPerson()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "Select EMP_CODE as [SalesPerson Code],Emp_Name as [SalesPerson Name] from TSPL_EMPLOYEE_MASTER"
        cbgSalesPerson.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesPerson.ValueMember = "SalesPerson Code"
        cbgSalesPerson.DisplayMember = "SalesPerson Name"
    End Sub
    Private Sub FrmDailySettlementActualAndProvisionalReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        chkRouteAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

    End Sub
    Sub Reset()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        rdbAll.IsChecked = True
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub
    Private Sub ChkSalesAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkSalesAll.ToggleStateChanged
        cbgSalesPerson.Enabled = Not ChkSalesAll.IsChecked
    End Sub
    Sub Print()
        Dim Route As String
        Dim location As String
        Dim company As String
        Dim sales As String
        Dim strRoute As String = ""
        Dim strlocation As String = ""
        Dim strCompany As String = ""
        Dim strSales As String = ""
        Dim strLocAll, strRouteAll, strSalesAll, strInvoicePost, strTransferPost, strReportType As String
        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one route Category or select ALL ")
            Return
        ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one location or select ALL ")
            Return
        ElseIf chlSalesSelect.IsChecked = True AndAlso cbgSalesPerson.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Salesperson or select ALL ")
            Return
        ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one company or select ALL ")
            Return

        End If
        If chkLocationSelect.IsChecked = True Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            strlocation = location.Replace("'", "")
        End If
        If chkRouteSelect.IsChecked = True Then
            Route = "'" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + "'"
            strRoute = Route.Replace("'", "")
        End If
        If chlSalesSelect.IsChecked = True Then
            sales = "'" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + "'"
            strSales = sales.Replace("'", "")
        End If
        If rbtnCompanySelect.IsChecked Then
            company = "'" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "'"
            strCompany = company.Replace("'", "")
        End If

        If chkLocatioAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkRouteAll.IsChecked = True Then
            strRouteAll = "Y"
        Else
            strRouteAll = "N"
        End If
        If ChkSalesAll.IsChecked = True Then
            strSalesAll = "Y"
        Else
            strSalesAll = "N"
        End If

        If rdbAll.IsChecked Then
            strInvoicePost = ""
            strTransferPost = ""
            strReportType = "All"
        Else
            strInvoicePost = " and Is_Post='Y' "
            strTransferPost = " and  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Post='Y' "
            strReportType = "Posted"
        End If
        Dim strMainQuery As String = " select '" + strlocation + "' as strlocation,'" + strRoute + "' as strRoute,'" + strCompany + "' as strCompany,'" + strSales + "' as strSales, Route_No,DocNo,SalesName, " & _
                 "case when TransType='Sale' then  sum(NetLoad) + SUM(Discount) else MAX(NetLoad) end   as NetLoad," & _
                 "case when TransType='Sale' then sum(NetSale) + MAX(Shellamt) else MAX(netsale) - SUM (discount) end as NetSale, " & _
                 "sum(Discount)   as Discount, " & _
                 "case when TransType='Sale' then SUM(CreditSale) else SUM(creditSale) end  as CreditSale," & _
                 "case when TransType='Sale' then max(CashAmt) else SUM(CashAmt) end as CashAmt, " & _
                 "case when TransType='Sale' then max(CheckAmt) else SUM(CheckAmt) end as CheckAmt, " & _
                 "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else sum(NetSale)   - ( max(CashAmt) + max(CheckAmt) + sum(Empty_Value) ) end else sum(CashSortage) end as cashShortage, " & _
                 "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else sum(Empty_Value) + max(Shellamt) - max(adjustEmpty) end else sum(EmptyShortage) end  as EmptyShortage, " & _
                 "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else max(Balance_Amt) end else sum(CashSortage) + sum(EmptyShortage) end  as Balance,Location,Location_Desc,max(FDate) as FDate,max(TDate) as TDate from( "
        Dim str1 As String = "SELECT  '" + strlocation + "' as strlocation,'" + strRoute + "' as strRoute,'" + strCompany + "' as strCompany,'" + strSales + "' as strSales, " + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,  " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
                "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS NetLoad, " & _
                "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
                "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS NetSale," & _
                "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9))  WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
                "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END AS Discount, " & _
                "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS CreditSale, " & _
                "case when Credit_Invoice='Y' then 0 else  (SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount), 0) AS Expr1 FROM  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No  inner join " & _
                "" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code  WHERE " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque') and " & _
                "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Posted='Y' and IsChkReverse='N') +  " & _
                "ISNULL((SELECT isnull(Adjustment_Amount,0) FROM " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header WHERE " & _
                "Doc_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and  Is_Post='Y'),0) end AS CashAmt, " & _
                "case when Credit_Invoice='Y' then 0 else  (SELECT  ISNULL(SUM(TSPL_RECEIPT_DETAIL_1.Applied_Amount), 0) AS Expr1 FROM " & _
                "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL AS TSPL_RECEIPT_DETAIL_1 INNER JOIN  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER AS TSPL_RECEIPT_HEADER_1 ON " & _
                "TSPL_RECEIPT_DETAIL_1.Receipt_No = TSPL_RECEIPT_HEADER_1.Receipt_No  inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " & _
                "TSPL_RECEIPT_HEADER_1.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code WHERE " & _
                "(TSPL_RECEIPT_DETAIL_1.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) And " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque') and TSPL_RECEIPT_HEADER_1.Posted='Y' and IsChkReverse='N')  -  " & _
                "ISNULL((SELECT  sum(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount) FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE.Document_No WHERE (" + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE.Source_Type = 'AR') AND " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)),0) end AS CheckAmt, " & _
                "case when Credit_Invoice='Y' then 0 else  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value end as Empty_Value, " & _
                "case when Credit_Invoice='Y' then 0 else  (SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost), 0) AS Expr1 FROM  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE  " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') AND (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI' ) and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Posted='Y') end AS adjustEmpty, " & _
                "'" & fromDate.Value & "' AS FDate, '" & ToDate.Value & "' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,(Shell_Qty *100) as Shellamt, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice,Balance_Amt,'Sale' as TransType,0 as CashSortage,0 as EmptyShortage,'" & strReportType & "' as ReportType FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code AND  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE where " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  " & strInvoicePost & " and " & _
                "Shipment_Type='sale' and not exists (select Invoice_No  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD WHERE " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)  "
        Dim str2 As String = "select '" + strlocation + "' as strlocation,'" + strRoute + "' as strRoute,'" + strCompany + "' as strCompany,'" + strSales + "' as strSales,  Transfer_Number as DocNo,From_Location,Route_No,Salesman as SalesName, " & _
                "(Transfer_Amount - Load_In_Amount) as NetLoad,(Transfer_Amount - Load_In_Amount) as NetSale, " & _
                "case when SettleMent_Type='DSC' then Amount else 0 end as Discount, " & _
                "case when SettleMent_Type='CRS' then Amount else 0 end as CreditSale, " & _
                "case when SettleMent_Type='CSH' then Amount else 0 end as CashAmt, " & _
                "case when SettleMent_Type='CHQ' then Amount else 0 end as CheckAmt, " & _
                "Empty_Load_In as Empty_Value,0 as adjustEmpty,'" & fromDate.Value & "' AS FDate, " & _
                "'" & ToDate.Value & "' AS TDate, FromLoc_Desc,0 as Shellamt, 'N' as Credit_Invoice, " & _
                "0 as Balance_Amt,'Transfer' as TransType, " & _
                "case when SettleMent_Type='CSE' then Amount else 0 end as CashSortage, " & _
                "case when SettleMent_Type='ESE' then Amount else 0 end as EmptyShortage,'" & strReportType & "'  as ReportType  from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent inner join " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail on " & _
                "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id=" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id inner join " & _
                "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code=" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode inner join " & _
                "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No where " & _
                "convert(date," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date,103) <= convert(date, '" & ToDate.Value & "',103) " & strTransferPost & "    "

        If strLocAll = "N" Then
            str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            str2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

        End If
        If strRouteAll = "N" Then
            str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            str2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "

        End If
        If strSalesAll = "N" Then
            str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            str2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        Dim strUnion As String = "Union All "
        Dim strSql As String = strMainQuery & str1 & strUnion & str2 & ") a group by DocNo,Route_No,Location,TransType,SalesName,Location_Desc"

        Dim ArrDBName As ArrayList = Nothing
        Dim dt As New DataTable

        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)
        dt = clsDBFuncationality.GetDataTable(strQuery)

        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptDailyQuickSettlement", "Daily Quick Settlement Report")

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

 

End Class
