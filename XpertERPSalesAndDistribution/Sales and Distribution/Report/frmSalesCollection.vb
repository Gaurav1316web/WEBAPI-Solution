''''              Modified by = Priti (16/04/2012)
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


Public Class FrmSalesCollection
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    'Dim dr As SqlDataReader
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SalesCollection)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                ' dr.Read()
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
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub

    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub

    Sub LoadInvoice()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Sale_Invoice_No as [SaleInvoice No],Sale_Invoice_Date as [SaleInvoice Date] from TSPL_SALE_INVOICE_HEAD"
        cbgInvoiceNo.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgInvoiceNo.ValueMember = "SaleInvoice No"
        cbgInvoiceNo.DisplayMember = "SaleInvoice Date"
    End Sub

    Sub LoadSalesPerson()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "Select EMP_CODE as [SalesPerson Code],Emp_Name as [SalesPerson Name] from TSPL_EMPLOYEE_MASTER"
        cbgSalesPerson.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesPerson.ValueMember = "SalesPerson Code"
        cbgSalesPerson.DisplayMember = "SalesPerson Name"
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    Private Sub FrmSalesCollection_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            'resetForm()
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnPrint.Enabled Then
            'PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()


        End If
    End Sub
    Private Sub FrmSalesCollection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        LoadInvoice()
        chkInvoiceAll.IsChecked = True
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbSummary.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")



    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()

    End Sub
    Sub reset()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        LoadInvoice()
        chkInvoiceAll.IsChecked = True
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbSummary.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub chkInvoiceAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInvoiceAll.ToggleStateChanged
        cbgInvoiceNo.Enabled = Not chkInvoiceAll.IsChecked
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub

    Private Sub ChkSalesAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkSalesAll.ToggleStateChanged
        cbgSalesPerson.Enabled = Not ChkSalesAll.IsChecked
    End Sub


    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkInvoiceSelect.IsChecked = True AndAlso cbgInvoiceNo.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Invoice or select ALL")
            Return
        ElseIf chlSalesSelect.IsChecked = True AndAlso cbgSalesPerson.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one SalesPerson or select ALL")
            Return
        ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
            Return
        ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
            Return
        End If

        Dim strLocAll, strRouteAll, strSalesAll, strInvoiceAll, Floc, Froute, Fsaleperson, Fcomp As String

        strLocAll = ""
        strRouteAll = ""
        strSalesAll = ""
        strInvoiceAll = ""
        Floc = ""
        Froute = ""
        Fsaleperson = ""
        Fcomp = ""

        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            Floc = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Floc = Floc.Replace("'", "")
        End If

        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
            Froute = "'" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + "'"
            Froute = Froute.Replace("'", "")
        End If

        If chlSalesSelect.IsChecked = True AndAlso cbgSalesPerson.CheckedValue.Count > 0 Then
            Fsaleperson = "'" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + "'"
            Fsaleperson = Fsaleperson.Replace("'", "")
        End If

        If rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count > 0 Then
            Fcomp = "'" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "'"
            Fcomp = Fcomp.Replace("'", "")
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
        If chkInvoiceAll.IsChecked = True Then
            strInvoiceAll = "Y"
        Else
            strInvoiceAll = "N"
        End If
        If ChkSalesAll.IsChecked = True Then
            strSalesAll = "Y"
        Else
            strSalesAll = "N"
        End If

        If strInvoiceAll = "N" Then
            strQuery = "SELECT  '" + Floc + "' as Floc,'" + Froute + "' as Froute,'" + Fsaleperson + "' as Fsaleperson,'" + Fcomp + "' as Fcomp ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name," & _
                     "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * Conversion_Factor as MRP_Amt, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS GrossSale, CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR " & _
                     "Discount_Code IS NULL) THEN Invoice_Qty / Conversion_Factor ELSE 0 END AS TradeQty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, " & _
                     "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate+ " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Tax+ " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TPT) AS TP, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice," & _
                     "CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') " & _
                     "THEN (Invoice_Qty * (Cust_Discount + Disc_Amt)) else 0  END AS DiscAmt," & _
                     "(SELECT ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount), 0) AS Expr1 " & _
                     "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No " & _
                     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
                     "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque'))  AS Cash_amt," & _
                     "(SELECT ISNULL(SUM(TSPL_RECEIPT_DETAIL_1.Applied_Amount), 0) AS Expr1 " & _
                     " FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL AS TSPL_RECEIPT_DETAIL_1 INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER AS TSPL_RECEIPT_HEADER_1 ON TSPL_RECEIPT_DETAIL_1.Receipt_No = TSPL_RECEIPT_HEADER_1.Receipt_No " & _
                     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on TSPL_RECEIPT_HEADER_1.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
                     "WHERE (TSPL_RECEIPT_DETAIL_1.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque')) " & _
                     " - ISNULL((SELECT  sum(TSPL_RECEIPT_DETAIL.Applied_Amount) FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_DETAIL.Receipt_No = TSPL_RECEIPT_HEADER.Receipt_No INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " & _
                     "WHERE (TSPL_BANK_REVERSE.Source_Type = 'AR') AND (TSPL_RECEIPT_DETAIL.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)),0) AS Check_amt, " & _
                     "'" & fromDate.Value & "' AS FDate, '" & ToDate.Value & "' AS TDate,case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else Total_Item_Amt end as Total_Item_Amt " & _
                     " FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code = " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code " & _
                     "where  Is_Post='Y' and   (select Invoice_No  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) IS NULL   "
        Else
            strQuery = "SELECT  '" + Floc + "' as Floc,'" + Froute + "' as Froute,'" + Fsaleperson + "' as Fsaleperson,'" + Fcomp + "' as Fcomp , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name," & _
                     "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * Conversion_Factor as MRP_Amt, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS GrossSale, CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR " & _
                     "Discount_Code IS NULL) THEN Invoice_Qty / Conversion_Factor ELSE 0 END AS TradeQty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, " & _
                     "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate+ " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Tax+ " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TPT) AS TP, " & _
                     "case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' then 'Y' else 'N' end as Credit_Invoice ," & _
                     "CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') " & _
                     "THEN (Invoice_Qty * (Cust_Discount + Disc_Amt)) else 0 END AS DiscAmt," & _
                      "(SELECT ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount), 0) AS Expr1 " & _
                     "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No " & _
                     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
                     "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque')) AS Cash_amt," & _
                     "(SELECT ISNULL(SUM(TSPL_RECEIPT_DETAIL_1.Applied_Amount), 0) AS Expr1 " & _
                     " FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL AS TSPL_RECEIPT_DETAIL_1 INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER AS TSPL_RECEIPT_HEADER_1 ON TSPL_RECEIPT_DETAIL_1.Receipt_No = TSPL_RECEIPT_HEADER_1.Receipt_No " & _
                     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on TSPL_RECEIPT_HEADER_1.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
                     "WHERE (TSPL_RECEIPT_DETAIL_1.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque')) " & _
                     " - ISNULL((SELECT  sum(TSPL_RECEIPT_DETAIL.Applied_Amount) FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_DETAIL.Receipt_No = TSPL_RECEIPT_HEADER.Receipt_No INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " & _
                     "WHERE (TSPL_BANK_REVERSE.Source_Type = 'AR') AND (TSPL_RECEIPT_DETAIL.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)),0) AS Check_amt, " & _
                     "'" & fromDate.Value & "' AS FDate, '" & ToDate.Value & "' AS TDate,case when (scheme_item='Y' or Promo_Scheme_Item='y' or Sampling_Item='Y') then 0 else Total_Item_Amt end as Total_Item_Amt " & _
                     "FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code = " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code " & _
                     "where  Is_Post='Y' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and  (select Invoice_No  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) IS NULL   "
        End If


        If strLocAll = "N" Then
            strQuery += " and TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If

        If strRouteAll = "N" Then
            strQuery += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strInvoiceAll = "N" Then
            strQuery += " and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No in (" + clsCommon.GetMulcallString(cbgInvoiceNo.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strQuery += " and TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If
        Dim ArrDBName As ArrayList = Nothing
        Dim dt As New DataTable

        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)
        dt = clsDBFuncationality.GetDataTable(strQuery)

        If rdbSummary.IsChecked = True Then
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptSalesCollectionSummary", "Sales v/s Collection")
        Else
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptSalesCollectionDetail", "Sales v/s Collection")
        End If
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    ''This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SALVsCOLL"
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

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

End Class
