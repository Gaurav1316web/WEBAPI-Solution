Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports XpertERPEngine
Imports common

Public Class FrmProvionalSalesReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode As String
    Dim sql As String


    Private preInvQty As Decimal = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()


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




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ProvisionalSaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmProvionalSalesReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        
        End If
    End Sub



    Private Sub FrmProvionalSalesReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")





    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    Sub print()
        'If fndTransfer.txtValue.Text = "" Then
        '    common.clsCommon.MyMessageBoxShow("Select Transfer No", "Proviosional Sales Report", MessageBoxButtons.OK)
        'End If
        proShowReport("Proviosional Sales", fndTransfer.Value)
    End Sub

    Public Shared Sub proShowReport(ByVal strReport As String, Optional ByVal StrClause As String = vbNullString, Optional ByVal StrClause1 As String = vbNullString, Optional ByVal StrClause2 As String = vbNullString, Optional ByVal StrClause3 As String = vbNullString, Optional ByVal StrClause4 As String = vbNullString, Optional ByVal StrClause5 As String = vbNullString, Optional ByVal StrClause6 As String = vbNullString, Optional ByVal StrClause7 As String = vbNullString, Optional ByVal StrClause8 As String = vbNullString, Optional ByVal strReportTitle As String = vbNullString)
        Select Case strReport
            Case "Trial balance"
                funTrialBalanceReport(StrClause, StrClause1)
            Case "Salesman Sales Report"
                funSalesmanSalesReport(StrClause, StrClause1, StrClause2, StrClause3)
            Case "receipt"
                'funreceiptreport(strReport, StrClause, StrClause1, StrClause2, StrClause3, StrClause4, StrClause5)
                ''Case "Customer Ledger"
                ''    funCustomerLedger(StrClause, StrClause1, StrClause2, StrClause3, StrClause4, StrClause5)
            Case "Settlement"
                funSettlementReport(StrClause, StrClause1, StrClause2, StrClause3)
            Case "Proviosional Sales"
                funprovSalesReport(StrClause)
            Case "Customer Route History"
                funCustomerRouteHistory(StrClause, StrClause1, StrClause2)
        End Select
    End Sub

    Public Shared Sub funSalesmanSalesReport(ByVal fromdate As String, ByVal todate As String, ByVal strSalesmanCode As String, ByVal strSalesmanNew As String)
        Dim subQry As String = "select Salesman_Code FROM TSPL_SALESMAN_MAPPING where Salesman_Code='" + strSalesmanCode + "' or Level2_Code='" + strSalesmanCode + "' or Level3_Code='" + strSalesmanCode + "' or Level4_Code='" + strSalesmanCode + "'"
        Dim StrQuery As String = "select TSPL_SALE_INVOICE_HEAD.Salesman_Code,RTRIM(TSPL_SALE_INVOICE_HEAD.Salesman_Code)+' - '+RTRIM(TSPL_EMPLOYEE_MASTER.Emp_Name) as Emp_Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Shipment_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Inv_Detail_Total_Amt,TSPL_SALE_INVOICE_HEAD.Empty_Value,(select isnull(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL where Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as ReceiptAmt ,ISNULL(TSPL_SALE_INVOICE_HEAD.Balance_Amt,0) as Balance_Amt,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name, TSPL_SALE_INVOICE_HEAD.Inv_Tax_Amt, TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,'" + fromdate + "' as FilterFromDate,'" + todate + "' as FilterToDate,'" + strSalesmanCode.Trim() + " - " + strSalesmanNew.Trim() + "' as FilterSalesman from TSPL_SALE_INVOICE_HEAD left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code where convert(date,Sale_Invoice_Date,103) >= convert(date,'" + fromdate + "',103) and convert(date,Sale_Invoice_Date,103) <= convert(date,'" + todate + "',103) and TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + subQry + ")"
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(StrQuery), "rptSalesman", "Salesman Sales Report")
    End Sub

    Public Shared Sub funTrialBalanceReport(ByVal fromdate As String, ByVal todate As String)
        Dim StrQuery As String = "select account_code,Account_Desc,Amount,Account_Type,Voucher_Date,'" & fromdate & "' as Fdate,'" & todate & "' as TDate from  TSPL_JOURNAL_DETAILS " & _
                   "WHERE convert(date,Voucher_Date,103) > =convert(date, '" & fromdate & "',103) and " & _
                   "convert(date,Voucher_Date,103) < =convert(date, '" & todate & "',103) "
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(StrQuery), "crptTrialBalance", "Trial Balance Report")
    End Sub

    Public Shared Sub funCustomerRouteHistory(ByVal strCustCode As String, ByVal fromdate As String, ByVal todate As String)
        Dim StrQuery As String = "select '" + strCustCode + "' as scustcode,  Cust_Code ,Customer_Name ,Add1+','+ Add2+','+Add3 as Address ,Channel_Code ,Visi_Id ,Route_No ,Route_Desc,Created_Date ,Modify_Date from TSPL_CUSTOMER_MASTER_HISTORY where 2=2"
        If (clsCommon.myLen(strCustCode) > 0 And fromdate = Date.Today() And todate = Date.Today()) Then
            StrQuery += " and Cust_Code='" + strCustCode + "'"
        End If
        If (fromdate <> Date.Today() And todate <> Date.Today()) Then
            ' StrQuery = "select '" + strCustCode + "' as scustcode,  Cust_Code ,Customer_Name ,Add1+','+ Add2+','+Add3 as Address ,Channel_Code ,Visi_Id ,Route_No ,Route_Desc,Created_Date ,Modify_Date from TSPL_CUSTOMER_MASTER_HISTORY"
        End If



        'StrQuery = "select account_code,Account_Desc,Amount,Account_Type,Voucher_Date,'" & fromdate & "' as Fdate,'" & todate & "' as TDate from  TSPL_JOURNAL_DETAILS " & _
        '           "WHERE convert(date,Voucher_Date,103) > =convert(date, '" & fromdate & "',103) and " & _
        '           "convert(date,Voucher_Date,103) < =convert(date, '" & todate & "',103) "
        'StrQuery += " and convert(date,Created_Date,103) >=convert(date,'" + fromdate + "',103) and convert(date,Created_Date,103)<=convert(date,'" + todate + "',103)"
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(StrQuery), "CrpyCustomerRouteHistory", "Customer Route History")
    End Sub

    Public Shared Sub funprovSalesReport(ByVal strTransferNo As String)
        Dim StrQuery As String = ""
        If strTransferNo = "" Then
            StrQuery = "SELECT  TSPL_TRANSFER_DETAIL.Item_Desc, TSPL_TRANSFER_HEAD.Transfer_No, " & _
                     "case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then   TSPL_TRANSFER_DETAIL.Item_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS Item_Qty, 0 AS LoadInQty, '' AS fromDate, '' AS Todate, " & _
                     "  TSPL_ITEM_MASTER.Sku_Seq AS Sku_Seq, TSPL_TRANSFER_HEAD.Route_No, TSPL_TRANSFER_HEAD.Transfer_Date, " & _
                     " TSPL_TRANSFER_HEAD.Salesmancode, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_TRANSFER_HEAD.Route_Desc, " & _
                     " TSPL_TRANSFER_HEAD.From_Location AS LoadOut_Location, TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_Desc, TSPL_TRANSFER_HEAD.Comp_Code, " & _
                     " TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, " & _
                     " TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.Empty_Value + TSPL_TRANSFER_DETAIL.TPT_Value) AS Value, " & _
                     " 0 AS Inamt, TSPL_ITEM_DETAILS.Item_Code,TSPL_TRANSFER_HEAD.Vehicle_Code,(Breakage + Leak + Burst + Shortage) as Breakage, " & _
                     " TSPL_TRANSFER_HEAD.vehicle_no,TSPL_TRANSFER_DETAIL.MRP " & _
                     " FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                     " TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                     " TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     " TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                     " TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                     " TSPL_EMPLOYEE_MASTER ON TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                     " TSPL_ITEM_MASTER ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
                     " TSPL_COMPANY_MASTER ON TSPL_TRANSFER_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code INNER JOIN " & _
                     " TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                     " TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                     "WHERE (TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and " & _
                     "TSPL_ITEM_DETAILS.Class_Name='pack' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' " & _
                     " union all " & _
                    "SELECT  TSPL_TRANSFER_DETAIL.Item_Desc, TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,0 AS Item_Qty, " & _
                    "case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS LoadInQty, " & _
                    "'' AS fromDate, '' AS Todate, TSPL_ITEM_MASTER.Sku_Seq AS Sku_Seq, TSPL_TRANSFER_HEAD.Route_No, TSPL_TRANSFER_HEAD.Transfer_Date," & _
                    "TSPL_TRANSFER_HEAD.Salesmancode, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_TRANSFER_HEAD.Route_Desc, " & _
                    "TSPL_TRANSFER_HEAD.To_Location AS LoadOut_Location, TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_Desc, TSPL_TRANSFER_HEAD.Comp_Code, " & _
                    "TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, 0 AS Value, " & _
                    "(LoadIn_Qty+ Burst + Leak + Shortage ) * ( BasicPrice_WithTax + Empty_Value + TPT_Value) AS Inamt, TSPL_ITEM_DETAILS.Item_Code, " & _
                    "TSPL_TRANSFER_HEAD.Vehicle_Code,(Breakage/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Leak/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Burst/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Shortage/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Breakage,TSPL_TRANSFER_HEAD.vehicle_no,TSPL_TRANSFER_DETAIL.MRP " & _
                    "FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                    "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                    "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                    "TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                    "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                    "TSPL_EMPLOYEE_MASTER ON TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                    "TSPL_ITEM_MASTER ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
                    "TSPL_COMPANY_MASTER ON TSPL_TRANSFER_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                    "WHERE  (TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and  " & _
                    "TSPL_ITEM_DETAILS.Class_Name='pack' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' order by Sku_Seq"
        Else
            StrQuery = "SELECT  TSPL_TRANSFER_DETAIL.Item_Desc, TSPL_TRANSFER_HEAD.Transfer_No, " & _
                     "case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.Item_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS Item_Qty, 0 AS LoadInQty, '' AS fromDate, '' AS Todate, " & _
                     "  TSPL_ITEM_MASTER.Sku_Seq AS Sku_Seq, TSPL_TRANSFER_HEAD.Route_No, TSPL_TRANSFER_HEAD.Transfer_Date, " & _
                     " TSPL_TRANSFER_HEAD.Salesmancode, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_TRANSFER_HEAD.Route_Desc, " & _
                     " TSPL_TRANSFER_HEAD.From_Location AS LoadOut_Location, TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_Desc, TSPL_TRANSFER_HEAD.Comp_Code, " & _
                     " TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, " & _
                     " TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.Empty_Value + TSPL_TRANSFER_DETAIL.TPT_Value) AS Value, " & _
                     " 0 AS Inamt, TSPL_ITEM_DETAILS.Item_Code,TSPL_TRANSFER_HEAD.Vehicle_Code,(Breakage + Leak + Burst + Shortage) as Breakage, " & _
                     " TSPL_TRANSFER_HEAD.vehicle_no,TSPL_TRANSFER_DETAIL.MRP " & _
                     " FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                     " TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                     " TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     " TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                     " TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                     " TSPL_EMPLOYEE_MASTER ON TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                     " TSPL_ITEM_MASTER ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
                     " TSPL_COMPANY_MASTER ON TSPL_TRANSFER_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code INNER JOIN " & _
                     " TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                     " TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                     "WHERE (TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and " & _
                     "TSPL_ITEM_DETAILS.Class_Name='pack' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'  and TSPL_TRANSFER_HEAD.Transfer_No='" & strTransferNo & "'" & _
                     " union all " & _
                    "SELECT  TSPL_TRANSFER_DETAIL.Item_Desc, TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,0 AS Item_Qty, " & _
                    "case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS LoadInQty, " & _
                    "'' AS fromDate, '' AS Todate, TSPL_ITEM_MASTER.Sku_Seq AS Sku_Seq, TSPL_TRANSFER_HEAD.Route_No, TSPL_TRANSFER_HEAD.Transfer_Date," & _
                    "TSPL_TRANSFER_HEAD.Salesmancode, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_TRANSFER_HEAD.Route_Desc, " & _
                    "TSPL_TRANSFER_HEAD.To_Location AS LoadOut_Location, TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_Desc, TSPL_TRANSFER_HEAD.Comp_Code, " & _
                    "TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, 0 AS Value, " & _
                    "(LoadIn_Qty+ Burst + Leak + Shortage ) * ( BasicPrice_WithTax + Empty_Value + TPT_Value)  AS Inamt, TSPL_ITEM_DETAILS.Item_Code, " & _
                    "TSPL_TRANSFER_HEAD.Vehicle_Code,(Breakage/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Leak/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Burst/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Shortage/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Breakage,TSPL_TRANSFER_HEAD.vehicle_no,TSPL_TRANSFER_DETAIL.MRP " & _
                    "FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                    "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                    "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                    "TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                    "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                    "TSPL_EMPLOYEE_MASTER ON TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                    "TSPL_ITEM_MASTER ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
                    "TSPL_COMPANY_MASTER ON TSPL_TRANSFER_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                    "WHERE  (TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and  " & _
                    "TSPL_ITEM_DETAILS.Class_Name='pack' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'  and TSPL_TRANSFER_HEAD.Load_Out_No='" & strTransferNo & "' order by Sku_Seq"
        End If
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(StrQuery), "crptProvisionalSales", "Proviosional Sales Report")
    End Sub
    Public Shared Sub funSettlementReport(ByVal fromdate As String, ByVal todate As String, ByVal strTransfer As String, ByVal strLoadOut As String)
        Dim StrQuery, StrQuery1, StrQuery2, StrQuery3, StrQuery4 As String
        If strTransfer <> "" Then
            StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, a.Transfer_No, a.Transfer_Date, " & _
            "a.Vehicle_Code, a.Salesmancode, c.Emp_Name AS salesmanName, " & _
            "b.Item_Code, b.Item_Desc,b.Item_Qty,(b.Item_Qty * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value ) ) as LoadOutamt," & _
    " 0 as Shipped_Qty,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull(Invoice_Qty,0) END AS InvQty, " & _
    " CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
    " TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
    "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
    " TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_net_Amt+TSPL_SALE_INVOICE_DETAIL.total_tpt+TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value),0) END AS InvAMt, " & _
    "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
    "TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN case " & _
    "when ((select excisable from tspl_location_master where TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_DETAIL.Location)='T' ) " & _
    " then 0 else TSPL_SALE_INVOICE_DETAIL.Empty_Value end  ELSE 0 END AS FOCamt, 0 AS LoadInQty, " & _
    "0 AS LoadInAMt, " & _
    "(b.MRP * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) as mrp, case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then b.Uom else " & _
    "TSPL_SALE_INVOICE_DETAIL.Unit_code end as Unit_code,b.Basic_Price, " & _
    " case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then " & _
    "(select top 1 Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=b.Item_Code and " & _
    " TSPL_ITEM_UOM_DETAIL.UOM_Code=b.Uom) else (select top 1  Conversion_Factor from TSPL_ITEM_UOM_DETAIL where " & _
    "TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and " & _
    " TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code) end as Conversion_Factor," & _
    " b.Empty_Value,b.BasicPrice_WithTax,b.TPT_Value,(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
    "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = b.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = b.Uom) as Transfer_Convert_F " & _
    ",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'T' as Type " & _
   " FROM  TSPL_TRANSFER_HEAD AS a INNER JOIN  " & _
"TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN  " & _
"TSPL_EMPLOYEE_MASTER AS c ON a.Salesmancode = c.EMP_CODE INNER JOIN  " & _
"TSPL_LOCATION_MASTER ON a.To_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN  " & _
"TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON b.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code AND  " & _
"b.Uom = TSPL_ITEM_UOM_DETAIL_1.UOM_Code LEFT OUTER JOIN  " & _
"TSPL_SALE_INVOICE_DETAIL INNER JOIN  " & _
"TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No INNER JOIN  " & _
"TSPL_SHIPMENT_MASTER AS d ON TSPL_SALE_INVOICE_HEAD.Shipment_No = d.Shipment_No ON a.Transfer_No = d.Transfer_No AND  " & _
"b.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code  " & _
"WHERE (a.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical') and a.Transfer_No='" & strTransfer & "'" & _
    "union all " & _
    "SELECT '' AS Location,a.Load_Out_No as Transfer_No, a.Transfer_Date, a.Vehicle_Code, a.Salesmancode," & _
    " '' AS salesmanName, b.Item_Code, b.Item_Desc, 0 as Item_Qty, " & _
    " 0 AS LoadOutamt, 0 AS Shipped_Qty, 0 AS InvQty, 0 AS FOCqty, 0 AS InvAMt, 0 AS FOCamt, (b.LoadIn_Qty/Conversion_Factor + b.Burst + b.Leak  +  b.shortage) AS LoadInQty, " & _
    "((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) AS LoadInAMt, (b.MRP*TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
    " b.Uom AS Unit_code, b.Basic_Price,TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS Conversion_Factor, b.Empty_Value, b.BasicPrice_WithTax, b.TPT_Value," & _
    "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE (Item_Code = b.Item_Code) AND (UOM_Code = b.Uom)) AS Transfer_Convert_F " & _
    ",0 as ShellQty,'T' as Type FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
    "TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
    "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No ON TSPL_ITEM_UOM_DETAIL_1.Item_Code = b.Item_Code AND " & _
    "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = b.Uom WHERE (a.Load_Out_No = '" & strTransfer & "')"
            StrQuery1 = "SELECT  c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                       "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                       "ISNULL((select isnull(Adjustment_Amount,0) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                       "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                       "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                       "TSPL_SHIPMENT_MASTER.Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                       "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                       "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty, " & _
                       " c.Empty_Value  as Empty_Value,c.TPT, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                       "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                       "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                       "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                       "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                       "where TSPL_SHIPMENT_MASTER.Transfer_No='" & strTransfer & "'"
            StrQuery2 = "select a.Transfer_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
                        "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) + b.Burst + b.Leak  +  b.shortage as LoadIn_Qty,b.Uom,b.MRP, " & _
                        "case when b.LoadIn_Qty > 0 then ((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) else 0 end as Loadinamt " & _
                        "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
                        "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
                        "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                        "Transfer_Type='LI' and a.Load_Out_No='" & strTransfer & "'"
            StrQuery3 = "select a.Payment_No,convert(date,a.Payment_Date,103) as Payment_Date,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where a.Apply_By='Load Out/Transfer'  and a.Apply_To='" & strTransfer & "'"

            StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
            "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Transfer_No,b.Unit_Code from " & _
                        "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                        "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No and  TSPL_SHIPMENT_MASTER.Transfer_No='" & strTransfer & "'"
            '"union all " & _
            '"SELECT TSPL_Receipt_Adjustment_Header.Adjustment_No, TSPL_Receipt_Adjustment_Header.Adjustment_Date," & _
            '"'' AS Adjustment_Type, TSPL_Receipt_Adjustment_Detail.Account_No AS Item_Code, " & _
            '"TSPL_Receipt_Adjustment_Detail.Account_Description AS Item_Description, " & _
            '"0 AS Item_Quantity, TSPL_Receipt_Adjustment_Header.Doc_No AS Document_No, TSPL_Receipt_Adjustment_Detail.Amount AS Item_cost, " & _
            '"0 AS Breakage, 0 AS Breakage_Cost, TSPL_SALE_INVOICE_HEAD.Shipment_No AS Transfer_No,'' as  Unit_Code " & _
            '"FROM TSPL_Receipt_Adjustment_Header INNER JOIN  " & _
            '"TSPL_Receipt_Adjustment_Detail on " & _
            '"TSPL_Receipt_Adjustment_Header.Adjustment_No = TSPL_Receipt_Adjustment_Detail.Adjustment_No INNER JOIN " & _
            '"TSPL_SALE_INVOICE_HEAD ON TSPL_Receipt_Adjustment_Header.Doc_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
            '"where TSPL_SALE_INVOICE_HEAD.Shipment_No='" & strTransfer & "'"
        Else
            StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, a.Transfer_No, a.Transfer_Date, " & _
"a.Vehicle_Code, a.Salesmancode, c.Emp_Name AS salesmanName, " & _
"b.Item_Code, b.Item_Desc,b.Item_Qty,(b.Item_Qty * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value ) ) as LoadOutamt," & _
" 0 as Shipped_Qty,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull(Invoice_Qty,0) END AS InvQty, " & _
" CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
" TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
"CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
" TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_net_Amt+TSPL_SALE_INVOICE_DETAIL.total_tpt+TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value),0) END AS InvAMt, " & _
"CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
"TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN case " & _
"when ((select excisable from tspl_location_master where TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_DETAIL.Location)='T' ) " & _
" then 0 else TSPL_SALE_INVOICE_DETAIL.Empty_Value end  ELSE 0 END AS FOCamt, 0 AS LoadInQty, " & _
"0 AS LoadInAMt, " & _
"(b.MRP * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) as mrp, case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then b.Uom else " & _
"TSPL_SALE_INVOICE_DETAIL.Unit_code end as Unit_code,b.Basic_Price, " & _
" case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then " & _
"(select top 1 Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=b.Item_Code and " & _
" TSPL_ITEM_UOM_DETAIL.UOM_Code=b.Uom) else (select top 1  Conversion_Factor from TSPL_ITEM_UOM_DETAIL where " & _
"TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and " & _
" TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code) end as Conversion_Factor," & _
" b.Empty_Value,b.BasicPrice_WithTax,b.TPT_Value,(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
"WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = b.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = b.Uom) as Transfer_Convert_F " & _
",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'T' as Type " & _
" FROM  TSPL_TRANSFER_HEAD AS a INNER JOIN  " & _
"TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN  " & _
"TSPL_EMPLOYEE_MASTER AS c ON a.Salesmancode = c.EMP_CODE INNER JOIN  " & _
"TSPL_LOCATION_MASTER ON a.To_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN  " & _
"TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON b.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code AND  " & _
"b.Uom = TSPL_ITEM_UOM_DETAIL_1.UOM_Code LEFT OUTER JOIN  " & _
"TSPL_SALE_INVOICE_DETAIL INNER JOIN  " & _
"TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No INNER JOIN  " & _
"TSPL_SHIPMENT_MASTER AS d ON TSPL_SALE_INVOICE_HEAD.Shipment_No = d.Shipment_No ON a.Transfer_No = d.Transfer_No AND  " & _
"b.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code  " & _
"WHERE (a.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical') " & _
"union all " & _
"SELECT '' AS Location,a.Load_Out_No as Transfer_No, a.Transfer_Date, a.Vehicle_Code, a.Salesmancode," & _
" '' AS salesmanName, b.Item_Code, b.Item_Desc, (b.LoadIn_Qty /Conversion_Factor) + b.Burst + b.Leak  +  b.shortage as LoadIn_Qty, " & _
" 0 AS LoadOutamt, 0 AS Shipped_Qty, 0 AS InvQty, 0 AS FOCqty, 0 AS InvAMt, 0 AS FOCamt, b.LoadIn_Qty AS LoadInQty, " & _
"((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) AS LoadInAMt, (b.MRP*TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
" b.Uom AS Unit_code, b.Basic_Price,TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS Conversion_Factor, b.Empty_Value, b.BasicPrice_WithTax, b.TPT_Value," & _
"(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE (Item_Code = b.Item_Code) AND (UOM_Code = b.Uom)) AS Transfer_Convert_F " & _
",0 as ShellQty,'T' as Type FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
"TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
"TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No ON TSPL_ITEM_UOM_DETAIL_1.Item_Code = b.Item_Code AND " & _
"TSPL_ITEM_UOM_DETAIL_1.UOM_Code = b.Uom and a.Transfer_Type='LI'  "
            StrQuery1 = "SELECT  c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                       "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                       "ISNULL((select isnull(Adjustment_Amount,0) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                       "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                       "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                       "TSPL_SHIPMENT_MASTER.Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                       "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                       "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty, " & _
                       " c.Empty_Value  as Empty_Value,c.TPT, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                       "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                       "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                       "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                       "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code "
            StrQuery2 = "select a.Load_Out_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
                        "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) + b.Burst + b.Leak  +  b.shortage as LoadIn_Qty,b.Uom,b.MRP, " & _
                        "case when b.LoadIn_Qty > 0 then ((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) else 0 end as Loadinamt " & _
                        "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
                        "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
                        "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                        "Transfer_Type='LI' "
            StrQuery3 = "select a.Payment_No,convert(date,a.Payment_Date,103) as Payment_Date ,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where (a.Apply_By='Load Out/Transfer'  or LoadOutNo <> '')  "

            StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
            "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Transfer_No,b.Unit_Code from " & _
                        "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                        "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No "
        End If
        If strLoadOut <> "" Then
            StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, d.Shipment_No AS Transfer_No, " & _
                     "d.Shipment_Date AS Transfer_Date, d.Vehicle_Code, d.Salesman_Code, " & _
                      "c.Emp_Name AS salesmanName, TSPL_SHIPMENT_DETAILS.Item_Code, " & _
                      "TSPL_SHIPMENT_DETAILS.Item_Desc,TSPL_SHIPMENT_DETAILS.Shipped_Qty AS Item_Qty, " & _
                      " CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
                      "THEN isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)  + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE (isnull(TSPL_SHIPMENT_DETAILS.Total_Item_Amt,0) + isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)) end AS LoadOutamt, " & _
                      "TSPL_SHIPMENT_DETAILS.Shipped_Qty, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN 0 ELSE isnull(Invoice_Qty, 0) END AS InvQty, " & _
                      "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
                      "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
                      "THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_net_Amt + TSPL_SALE_INVOICE_DETAIL.total_tpt + TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt " & _
                      " + TSPL_SALE_INVOICE_DETAIL.Empty_Value), 0) END AS InvAMt, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN CASE WHEN " & _
                      "((SELECT excisable FROM tspl_location_master " & _
                      "WHERE TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_DETAIL.Location) = 'T') " & _
                      "THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) " & _
                      "END ELSE 0 END AS FOCamt, 0 AS LoadInQty, 0 AS LoadInAMt, (TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
                      "CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL " & _
                      "THEN TSPL_SHIPMENT_DETAILS.Unit_code ELSE TSPL_SALE_INVOICE_DETAIL.Unit_code END AS Unit_code, " & _
                      "TSPL_SALE_INVOICE_DETAIL.Basic_Rate, CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL THEN " & _
                      "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code) ELSE " & _
                      "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SALE_INVOICE_DETAIL.Unit_code) END AS Conversion_Factor, 0 AS Empty_Value," & _
                      "0 AS BasicPrice_WithTax, TSPL_SHIPMENT_DETAILS.TPT AS TPT_Value," & _
                      "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE (Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code) AND (UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code)) AS Transfer_Convert_F " & _
                      ",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'L' as Type " & _
                      "FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
                      "TSPL_SHIPMENT_DETAILS INNER JOIN " & _
                      "TSPL_SHIPMENT_MASTER AS d ON TSPL_SHIPMENT_DETAILS.Shipment_No = d.Shipment_No INNER JOIN " & _
                      "TSPL_EMPLOYEE_MASTER AS c ON d.Salesman_Code = c.EMP_CODE ON " & _
                      "TSPL_ITEM_UOM_DETAIL_1.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code INNER JOIN " & _
                      "TSPL_SALE_INVOICE_DETAIL INNER JOIN " & _
                      "TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
                      "TSPL_SHIPMENT_DETAILS.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
                      "TSPL_SHIPMENT_DETAILS.Unit_code = TSPL_SALE_INVOICE_DETAIL.Unit_code And " & _
                      "TSPL_SHIPMENT_DETAILS.Location = TSPL_SALE_INVOICE_DETAIL.Location AND " & _
                      "d.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No AND " & _
                      "TSPL_SHIPMENT_DETAILS.Shipment_Id = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id where d.Shipment_No='" & strLoadOut & "'"
            StrQuery1 = "SELECT  c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                     "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                       "ISNULL((select isnull(Adjustment_Amount,0) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                       "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                       "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                       "TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                       "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                       "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty," & _
                       "c.Empty_Value as Empty_Value, " & _
                       "c.TPT,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                       "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                       "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                       "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                       "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                       "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                       "where TSPL_SHIPMENT_MASTER.Shipment_No='" & strLoadOut & "'"
            StrQuery2 = "select a.Transfer_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
                       "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) as LoadIn_Qty,b.Uom,b.MRP,(b.LoadIn_Qty * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) as Loadinamt " & _
                       "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
                       "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
                       "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                       "Transfer_Type='LI' and a.Load_Out_No='" & strLoadOut & "'"
            StrQuery3 = "select a.Payment_No,a.Payment_Date,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where a.Apply_By='Load Out'  and a.Apply_To='" & strLoadOut & "'" & _
                        "union all " & _
                        "SELECT TSPL_Receipt_Adjustment_Header.Adjustment_No as Payment_No, TSPL_Receipt_Adjustment_Header.Adjustment_Date as Payment_Date," & _
                        "TSPL_Receipt_Adjustment_Detail.Amount AS Payment_Amount,TSPL_Receipt_Adjustment_Header.Doc_No AS Apply_To FROM TSPL_Receipt_Adjustment_Header INNER JOIN " & _
                        "TSPL_Receipt_Adjustment_Detail on TSPL_Receipt_Adjustment_Header.Adjustment_No = TSPL_Receipt_Adjustment_Detail.Adjustment_No INNER JOIN " & _
                        "TSPL_SALE_INVOICE_HEAD ON TSPL_Receipt_Adjustment_Header.Doc_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
                        "where TSPL_SALE_INVOICE_HEAD.Shipment_No='" & strLoadOut & "'"
            StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
            "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No,b.Unit_Code from " & _
                        "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                        "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and " & _
                        "TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No and  TSPL_SHIPMENT_MASTER.Shipment_No='" & strLoadOut & "'"

            'Else

            '    StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, d.Shipment_No AS Transfer_No, " & _
            '  "d.Shipment_Date AS Transfer_Date, d.Vehicle_Code, d.Salesman_Code, " & _
            '   "c.Emp_Name AS salesmanName, TSPL_SHIPMENT_DETAILS.Item_Code, " & _
            '   "TSPL_SHIPMENT_DETAILS.Item_Desc,TSPL_SHIPMENT_DETAILS.Shipped_Qty AS Item_Qty, " & _
            '   " CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
            '   "THEN isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)  + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE (isnull(TSPL_SHIPMENT_DETAILS.Total_Item_Amt,0) + isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)) end AS LoadOutamt, " & _
            '   "TSPL_SHIPMENT_DETAILS.Shipped_Qty, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN 0 ELSE isnull(Invoice_Qty, 0) END AS InvQty, " & _
            '   "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
            '   "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
            '   "THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_net_Amt + TSPL_SALE_INVOICE_DETAIL.total_tpt + TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt " & _
            '   " + TSPL_SALE_INVOICE_DETAIL.Empty_Value), 0) END AS InvAMt, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN CASE WHEN " & _
            '   "((SELECT excisable FROM tspl_location_master " & _
            '   "WHERE TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_DETAIL.Location) = 'T') " & _
            '   "THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) " & _
            '   "END ELSE 0 END AS FOCamt, 0 AS LoadInQty, 0 AS LoadInAMt, (TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
            '   "CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL " & _
            '   "THEN TSPL_SHIPMENT_DETAILS.Unit_code ELSE TSPL_SALE_INVOICE_DETAIL.Unit_code END AS Unit_code, " & _
            '   "TSPL_SALE_INVOICE_DETAIL.Basic_Rate, CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL THEN " & _
            '   "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
            '   "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
            '   "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code) ELSE " & _
            '   "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
            '   "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
            '   "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SALE_INVOICE_DETAIL.Unit_code) END AS Conversion_Factor, 0 AS Empty_Value," & _
            '   "0 AS BasicPrice_WithTax, TSPL_SHIPMENT_DETAILS.TPT AS TPT_Value," & _
            '   "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
            '   "WHERE (Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code) AND (UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code)) AS Transfer_Convert_F " & _
            '   ",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'L' as Type " & _
            '   "FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
            '   "TSPL_SHIPMENT_DETAILS INNER JOIN " & _
            '   "TSPL_SHIPMENT_MASTER AS d ON TSPL_SHIPMENT_DETAILS.Shipment_No = d.Shipment_No INNER JOIN " & _
            '   "TSPL_EMPLOYEE_MASTER AS c ON d.Salesman_Code = c.EMP_CODE ON " & _
            '   "TSPL_ITEM_UOM_DETAIL_1.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
            '   "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code INNER JOIN " & _
            '   "TSPL_SALE_INVOICE_DETAIL INNER JOIN " & _
            '   "TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            '   "TSPL_SHIPMENT_DETAILS.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
            '   "TSPL_SHIPMENT_DETAILS.Unit_code = TSPL_SALE_INVOICE_DETAIL.Unit_code And " & _
            '   "TSPL_SHIPMENT_DETAILS.Location = TSPL_SALE_INVOICE_DETAIL.Location AND " & _
            '   "d.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No AND " & _
            '   "TSPL_SHIPMENT_DETAILS.Shipment_Id = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id "
            '    StrQuery1 = "SELECT  c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
            '             "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
            '               "ISNULL((select isnull(Adjustment_Amount,0) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
            '               "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
            '               "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
            '               "TSPL_SHIPMENT_MASTER.Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
            '               "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '               "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
            '               "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty," & _
            '               "c.Empty_Value as Empty_Value, " & _
            '               "c.TPT,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
            '               "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
            '               "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
            '               "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
            '               "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            '               "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code "
            '    StrQuery2 = "select a.Transfer_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
            '               "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) as LoadIn_Qty,b.Uom,b.MRP,(b.LoadIn_Qty * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) as Loadinamt " & _
            '               "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
            '               "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
            '               "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
            '               "Transfer_Type='LI' "
            '    StrQuery3 = "select a.Payment_No,a.Payment_Date,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where a.Apply_By='Load Out' " 
            '    StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
            '    "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No,b.Unit_Code from " & _
            '                "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
            '                "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and " & _
            '                "TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No "

        End If
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funsubreport(CrystalReportFolder.SalesReport, StrQuery, "crptSettlement", "Settlement Report", "crptSettlementReceiptDetails.rpt", "crptSettlementLoadin.rpt", "crptSettlementpayment.rpt", "crptSettlementAdjustment.rpt")
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub fndTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndTransfer.ConnectionString = connectSql.SqlCon()
    '    fndTransfer.Query = "select Load_Out_No as [Transfer No],convert(date,transfer_date,103) as [Transfer Date] from TSPL_TRANSFER_HEAD where Transfer_Type='LI'"
    '    fndTransfer.ValueToSelect = "Transfer No"
    '    fndTransfer.ValueToSelect1 = "Transfer No"
    '    fndTransfer.Caption = "Transfer No"
    'End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        fndTransfer.Value = ""
    End Sub


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PRO-SAL-RPT"
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

    Private Sub fndTransfer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTransfer._MYValidating
        Dim qry As String = "select Load_Out_No as [Transfer No],convert(date,transfer_date,103) as [Transfer Date] from TSPL_TRANSFER_HEAD where Transfer_Type='LI'"
        fndTransfer.Value = clsCommon.ShowSelectForm("fndtrms@", qry, "Transfer No", "", fndTransfer.Value, "", isButtonClicked)
    End Sub

End Class
