'--Created By--[Pankaj Kumar Chaudhary]---Against Ticket No--[BM00000000871]
Imports common
Imports System.Data.SqlClient

Public Class FrmOpenningBalance
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Dim IsPrint As Boolean = False
    

    Private Sub FrmCustomerAgeing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmCustomerAgeing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpDate.Value = Date.Today
        cmbType.SelectedText = "Customer"
        LoadACode()
        chkCustomerAll.IsChecked = True
        LoadLocationCode()
        chkLOcAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Ctrl+P for Print ")
        GV1.AllowAddNewRow = False
    End Sub

    Private Sub LoadACode()
        Dim qry As String
        If clsCommon.CompairString(cmbType.Text, "Customer") = CompairStringResult.Equal Then
            qry = "select Cust_Code as [Code], Customer_Name as [Name],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER order by  Cust_Code "
        ElseIf clsCommon.CompairString(cmbType.Text, "Vendor") = CompairStringResult.Equal Then
            qry = "Select Vendor_Code as [Code], Vendor_Name as [Name] from TSPL_VENDOR_MASTER order by Vendor_Code "
        Else
            qry = "Select Account_Code as [Code], Description as [Name] from TSPL_GL_ACCOUNTS order by Account_Code "
        End If
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmOpeningBalance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Private Sub LoadLocationCode()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        print()
    End Sub

    Sub print()
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single " + cmbType.SelectedText + ".")
            End If
            If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single Location.")
            End If
            Dim rptDate As String = clsCommon.GetPrintDate(dtpDate.Value)
            Dim runDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
            Dim StrQuery As String
            If clsCommon.CompairString(cmbType.Text, "Customer") = CompairStringResult.Equal Then
                StrQuery = " select '" + runDate + "' as RunDate, '" + rptDate + "' as rptDate, 'Customer Code' as HeadCode, 'Customer Name' As HeadDesc, 'Opening Balance [Customer]' as rptHead, "
                StrQuery += " ACode, MAX(AName) as AName, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, Case When SUM(DrAmt)>SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DebitAmt, Case When SUM(DrAmt)<SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CreditAmt from ("
                StrQuery += " Select TSPL_SALE_INVOICE_HEAD.Cust_Code as ACode ,cust_name as AName, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate, (Empty_Value+Total_Invoice_Amt) as DrAmt, 0 as CrAmt, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location from TSPL_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location  WHERE  TSPL_SALE_INVOICE_HEAD.Is_Post='y'   "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " Select Cust_Code as ACode,Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, case when Receipt_Type='F' then Receipt_Amount else 0 end as DrAmt, case when Receipt_Type='F' then 0 else Receipt_Amount end as CrAmt , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location] from TSPL_RECEIPT_HEADER   where  TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Receipt_Type not in ('M')     "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " SELECT Customer_CODE as ACode, max(Customer_NAME ) as AName, Final.Adjustment_No AS DocNo,  MAX(Adjustment_Date) AS DocDate, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt , case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt,max(Final.Location) as [Location] FROM (SELECT TSPL_ADJUSTMENT_HEADER.Customer_CODE, TSPL_ADJUSTMENT_HEADER.Customer_NAME, TSPL_ADJUSTMENT_HEADER.Adjustment_No, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost, TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location], TSPL_ADJUSTMENT_HEADER.Trans_Type FROM TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No Left OUTER JOIN TSPL_LOCATION_MASTER on TSPL_ADJUSTMENT_DETAIL.Location_Code=TSPL_LOCATION_MASTER.Location_Code inner join tspl_customer_master on tspl_adjustment_header.customer_code=tspl_customer_master.cust_code WHERE ((TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND (TSPL_ADJUSTMENT_HEADER.Document_No= '' and TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '')   or   TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and TSPL_ADJUSTMENT_HEADER.Posted='Y') AS Final GROUP BY Adjustment_No, Customer_CODE "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_Customer_Invoice_Head.Customer_Code as ACode ,TSPL_Customer_Invoice_Head.Customer_Name AS AName, case when len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 then  TSPL_Customer_Invoice_Head.AgainstScrap else  TSPL_Customer_Invoice_Head.Document_No  end DocNo, TSPL_Customer_Invoice_Head.Document_Date as DocDate, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt], TSPL_Customer_Invoice_Head.Loc_Code from   TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.Status=1  "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_SALE_RETURN_HEAD.Cust_Code as ACode, cust_name as AName, Sale_Return_No as DocNo, Sale_Return_Date as DocDate, 0 as DrAmt, (Empty_Value+Total_Invoice_Amt) as CrAmt , TSPL_LOCATION_MASTER.Loc_Segment_Code as Location from TSPL_SALE_RETURN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_HEAD.Location  where TSPL_SALE_RETURN_HEAD.Is_Post='Y' "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_SALE_RETURN_INTER_HEAD.Cust_Code as ACode , TSPL_SALE_RETURN_INTER_HEAD.cust_name as AName, TSPL_SALE_RETURN_INTER_HEAD.Document_No  as DocNo, TSPL_SALE_RETURN_INTER_HEAD.Document_Date as DocDate, 0 as DrAmt, (TSPL_SALE_RETURN_INTER_HEAD.Empty_Value+TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt) as CrAmt, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location from  TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_INTER_HEAD.Location  where TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_Receipt_Adjustment_Header.Customer_No as ACode, TSPL_CUSTOMER_MASTER.Customer_Name as AName, Adjustment_No as DocNo, Adjustment_Date as DocDate, 0 as DrAmt, TSPL_Receipt_Adjustment_Header.Adjustment_Amount as CrAmt, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location from TSPL_Receipt_Adjustment_Header left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Receipt_Adjustment_Header.Customer_No left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_Receipt_Adjustment_Header.Doc_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location where TSPL_Receipt_Adjustment_Header.Is_Post='Y' "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_BANK_REVERSE.Cust_Code as ACode, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo ,TSPL_BANK_REVERSE.Reversal_Date as DocDate, TSPL_BANK_REVERSE.Amount as  DrAmt, 0 as CrAmt , RIGHT(TSPL_BANK_MASTER.BANKACC,3) as [Location] from TSPL_BANK_REVERSE  left outer join TSPL_BANK_MASTER on TSPL_BANK_REVERSE .Bank_Code =TSPL_BANK_MASTER.BANK_CODE     where TSPL_BANK_REVERSE.Source_Type='AR' and TSPL_BANK_REVERSE.post='P'   "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo, TSPL_VCGL_Head.Document_Date as DocDate, case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount else 0 end  as DrAmt ,case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount else 0 end as CrAmt, TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Head  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1  "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_VCGL_Detail.VCGL_Code as ACode, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo, TSPL_VCGL_Head.Document_Date as DocDate, TSPL_VCGL_Detail.Dr_Amount as DrAmt, TSPL_VCGL_Detail.Cr_Amount as CrAmt, TSPL_VCGL_Head.Location_Segment as Location from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' "
                StrQuery += " ) Final where  CONVERT(DATE,final.DocDate,103) <= '" + clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy") + "' AND ISNULL(ACode,'')<>'' "
            ElseIf clsCommon.CompairString(cmbType.Text, "Vendor") = CompairStringResult.Equal Then
                StrQuery = "select '" + runDate + "' as RunDate, '" + rptDate + "' as rptDate, 'Vendor Code' as HeadCode, 'Vendor Name' As HeadDesc, 'Opening Balance [Vendor]' as rptHead, "
                StrQuery += " Acode, MAX(AName) as AName, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, Case When SUM(DrAmt)>SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End AS DebitAmt, Case When SUM(DrAmt)<SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End AS CreditAmt from( "
                StrQuery += " select vendor_code as ACode, vendor_name as AName, TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo, convert(date,Invoice_Entry_Date,103) as DocDate, ((case when Document_Type IN('I','C') AND TAX1_Amt<0 then (-1*TAX1_Amt) else 0 end + case when Document_Type IN('I','C') AND TAX2_Amt<0 then (-1*TAX2_Amt)  else 0   end + case when Document_Type IN('I','C') AND TAX3_Amt<0 then (-1*TAX3_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX4_Amt<0 then (-1*TAX4_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX5_Amt<0 then (-1*TAX5_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX6_Amt<0 then (-1*TAX6_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX7_Amt<0 then (-1*TAX7_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX8_Amt<0 then (-1*TAX8_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX9_Amt<0 then (-1*TAX9_Amt) else  0  end + case when Document_Type IN('I','C') AND TAX10_Amt<0 then (-1*TAX10_Amt) else 0 end)+case when Document_Type IN('I','C') then document_total else 0 end  ) as CrAmt, case when Document_Type IN('D') then Document_Total else 0 end as DrAmt, tspl_vendor_invoice_head.Loc_Code as Location from tspl_vendor_invoice_head  left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0 "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select vendor_code as ACode, vendor_name as AName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo, convert(date,Invoice_Entry_Date, 103) as DocDate, 0 as CrAmt, (-1* TAX1_Amt) as DrAmt, tspl_vendor_invoice_head.Loc_Code as Location from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX1  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX1_Amt<0  "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_PI_HEAD.Vendor_Code as ACode, TSPL_PI_HEAD.Vendor_Name as AName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo, TSPL_PI_HEAD.PI_Date as DocDate, TSPL_VENDOR_INVOICE_HEAD.Document_Total as CrAmt, 0  as DrAmt, TSPL_PI_HEAD.Bill_To_Location as Location from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1 "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_REMITTANCE.Vendor_Code as ACode, TSPL_REMITTANCE.Vendor_Name As AName, TSPL_REMITTANCE.Document_No as DocNo, CONVERT(Date,Document_Date,103) as DocDate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then Actual_Total_TDS else 0 END AS DrAmt, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as Location from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_REMITTANCE.Vendor_Code As ACode, TSPL_REMITTANCE.Vendor_Name As AName, TSPL_REMITTANCE.Document_No as DocNo, TSPL_BANK_REVERSE.Reversal_Date as Document_Date, 0 AS CrAmt, case when (TSPL_PAYMENT_HEADER.Cheque_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end  as DrAmt, RIGHT(TSPL_PAYMENT_HEADER.Bank_Code,3) As Location from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Cheque_No=TSPL_REMITTANCE.Document_No  inner join  TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P'  and   Branch_GL_AC  is not null and Actual_Total_TDS<>0 "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_BANK_REVERSE.vendor_code as ACode, TSPL_BANK_REVERSE.vendor_name as AName, Reverse_Code as DocNo, Reversal_Date as DocDate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, RIGHT(TSPL_PAYMENT_HEADER.Bank_Code,3) As Location from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y'  "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select  VC_Code as ACode, VC_Name as AName, Document_No as DocNo, Document_Date as DocDate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, TSPL_VCGL_Head.Location_Segment as Location from TSPL_VCGL_Head where Document_Type='v' and TSPL_VCGL_Head.Status='1' "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select TSPL_VCGL_Detail.VCGL_Code as ACode, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Head.Document_Date as DocDate, TSPL_VCGL_Detail.Cr_Amount as CrAmt, TSPL_VCGL_Detail.Dr_Amount as DrAmt, TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' "
                StrQuery += Environment.NewLine + " UNION ALL " + Environment.NewLine
                StrQuery += " select vendor_code as ACode, vendor_name as AName, payment_no as DocNo, payment_date as DocDate, case when payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when payment_type IN('PY','OA','AV','MI') then Payment_Amount else 0 end as DrAmt, RIGHT(tspl_payment_header.Bank_Code,3) As Location from tspl_payment_header Where (Posted='P' or Posted='1') AND ISNULL(Vendor_Code,'')<>''"
                StrQuery += " ) final WHERE DocDate< '" + clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy") + "' "
            Else
                StrQuery = "Select '" + runDate + "' as RunDate, '" + rptDate + "' as rptDate, 'Account Code' as HeadCode, 'Account Description' As HeadDesc, 'Opening Balance [GL Account]' as rptHead, "
                StrQuery += " ACode, MAX(AName) as AName, SUM(DrAmt) as DrAmt, SUM(CrAmt) As CrAmt, Case When SUM(DrAmt)>SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End As DebitAmt, Case When SUM(DrAmt)<SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End As CreditAmt, MAX(Location) as Location From "
                StrQuery += " (Select TSPL_JOURNAL_DETAILS.Voucher_No, TSPL_JOURNAL_DETAILS.Account_code as ACode, TSPL_JOURNAL_DETAILS.Account_Desc As AName, Case When Amount<0 Then Amount*-1 Else 0 End as CrAmt, Case When Amount>0 Then Amount Else 0 End as DrAmt, TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as Location from TSPL_JOURNAL_DETAILS"
                StrQuery += " ) final Left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=Final.Voucher_No "
                StrQuery += " WHERE Voucher_Date< '" + clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy") + "' "
            End If
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                StrQuery += " AND Final.ACode in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                StrQuery += " AND Final.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            StrQuery += " Group By ACode order by ACode  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
            GV1.DataSource = Nothing
            If dt.Rows.Count > 0 Then
                GV1.DataSource = dt
                FormatGrid()
                If IsPrint Then

                    Dim FRMcrys As New frmCrystalReportViewer
                    FRMcrys.funreport(CrystalReportFolder.CommonServices, dt, "crptOpeningBalance", "Opening Balance Report")
                End If
            Else
                Throw New Exception("No data found.")
            End If
            IsPrint = False
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
            IsPrint = False
        End Try
    End Sub

    Private Sub FormatGrid()
        For Each coll As GridViewColumn In GV1.Columns
            coll.ReadOnly = True
            coll.IsVisible = False
        Next
        GV1.Columns("ACode").HeaderText = clsCommon.myCstr(cmbType.SelectedText) + "Code"
        GV1.Columns("ACode").IsVisible = True
        GV1.Columns("ACode").Width = 200

        GV1.Columns("AName").HeaderText = clsCommon.myCstr(cmbType.SelectedText) + "Name"
        GV1.Columns("AName").IsVisible = True
        GV1.Columns("AName").Width = 350

        GV1.Columns("DebitAmt").HeaderText = "Debit Amount"
        GV1.Columns("DebitAmt").IsVisible = True
        GV1.Columns("DebitAmt").Width = 200

        GV1.Columns("CreditAmt").HeaderText = "Credit Amount"
        GV1.Columns("CreditAmt").IsVisible = True
        GV1.Columns("CreditAmt").Width = 200
    End Sub

    Private Sub chkLOcAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLOcSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = False
    End Sub

    Private Sub chkCustomerSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = True
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbType.SelectedIndexChanged
        LoadACode()
    End Sub

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            IsPrint = True
            print()
        Catch ex As Exception

        End Try
    End Sub
End Class
