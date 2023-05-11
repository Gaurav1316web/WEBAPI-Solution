'-28/11/2012-3:07PM--Updation By--Pankaj Kumar--Viewed New Transaction [VCGL Entry] in Module [General Ledger]---
''''05/12/2012-12:57PM--Updation by --[Pankaj kumar]-- Viewed New Transaction [Empty Transactions] in Module [Material Management]-----From-Ranjana Sinha
''''06/12/2012-01:25PM--Updation by --[Pankaj kumar]-- Viewed New Transaction [Sale return (Inter Company)] in Module [Sales n Disribution]-----From-Ranjana Sinha
Imports common

Public Class FrmBulkPostingNew
    Inherits FrmMainTranScreen
    Const Modul As String = "Module"
    Const Transaction As String = "Transaction"
    Const ApprovedDoc As String = "No of Approved Doc"
    Const UnApprovedDoc As String = "No of UnApproved Doc"
    Dim ButtonTooltip As New ToolTip()
    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBulkPostingNew)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmBulkPostingNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnShow, "Press Alt+R for Refresh the Data")
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False
        UserInformation()

    End Sub
    Public Sub UserInformation()
        Dim qry As String
        Dim CurrentUser As String = objCommonVar.CurrentUserCode
        If (clsCommon.CompairString(clsCommon.myCstr(CurrentUser), "ADMIN") = CompairStringResult.Equal) Then
            qry = " select User_Code as Code, User_Name as Name from TSPL_USER_MASTER "
        Else
            qry = " select User_Code as Code, User_Name as Name from TSPL_USER_MASTER where user_code='" + CurrentUser + "'"
            cbgUser.Enabled = False
        End If
        cbgUser.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgUser.ValueMember = "Code"
        cbgUser.DisplayMember = "Name"
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        ShowAll()
    End Sub
    Sub ShowAll()
        Try
            If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
                objCommonVar.SelectedUser = "All"
                If (cbgUser.CheckedValue.Count > 0) Then
                    objCommonVar.SelectedUser = clsCommon.GetMulcallString(cbgUser.CheckedValue)
                Else
                    Throw New Exception("Select at least one user.")
                End If
            Else
                objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
            End If
            Dim dt As DataTable
            Dim qry As String = "Select 'Common Services' as [Module],'Bank Transfer' as [Transaction],count(case when TSPL_BANK_TRANSFER.Post ='p' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_BANK_TRANSFER.Post ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_BANK_TRANSFER WHERE  convert(date,TSPL_BANK_TRANSFER.Transfer_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BANK_TRANSFER.Transfer_Date ,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += " union All"
            qry += " Select 'Common Services' as [Module],'Reverse Transaction' as [Transaction],count(case when TSPL_BANK_REVERSE.Post ='P' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_BANK_REVERSE.Post ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_BANK_REVERSE WHERE  convert(date,TSPL_BANK_REVERSE.Reversal_Date  ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += "  union All "
            qry += " Select 'Receivables' as [Module],'Receipt Entry' as [Transaction],count(case when TSPL_RECEIPT_HEADER.Posted ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_RECEIPT_HEADER.Posted ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_RECEIPT_HEADER WHERE  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += "  union All "
            qry += " Select 'Receivables' as [Module],'Adjustment Entry' as [Transaction],count(case when TSPL_Receipt_Adjustment_Header.Is_Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_Receipt_Adjustment_Header.Is_Post is NULL OR TSPL_Receipt_Adjustment_Header.Is_Post ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_Receipt_Adjustment_Header WHERE  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += " union All "
            qry += "Select 'Receivables' as [Module],'AR Invoice Entry' as [Transaction],count(case when TSPL_Customer_Invoice_Head.status=1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_Customer_Invoice_Head.status =0 then 0 end)as [No of UnPosted Doc] from  TSPL_Customer_Invoice_Head WHERE  convert(date,TSPL_Customer_Invoice_Head.Document_Date  ,103) >= convert(date ,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += "union All"
            qry += " Select 'Payables' as [Module],'Payment Entry' as [Transaction],count(case when TSPL_PAYMENT_HEADER.Posted ='1' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_PAYMENT_HEADER.Posted <> '1' then 0 end)as [No of UnPosted Doc] from  TSPL_PAYMENT_HEADER WHERE  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Payables' as [Module],'AP Invoice Entry' as [Transaction],count(case when TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS not null then 1 end)as [No of Posted Doc],COUNT(case when TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS NULL  then 0 end)as [No of UnPosted Doc] from  TSPL_VENDOR_INVOICE_HEAD WHERE  convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Tax Deducted At Source' as [Module],'Remittance Entry' as [Transaction],count(case when TSPL_REMITTANCE_ENTRY.Posted ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_REMITTANCE_ENTRY.Posted ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_REMITTANCE_ENTRY WHERE  convert(date,TSPL_REMITTANCE_ENTRY.Remittance_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.Remittance_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'General Ledger' as [Module],'Journal Entry' as [Transaction],count(case when TSPL_JOURNAL_MASTER.Authorized ='A' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_JOURNAL_MASTER.Authorized ='N' OR TSPL_JOURNAL_MASTER.Authorized IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_JOURNAL_MASTER WHERE  convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " Union All"
            qry += " Select 'General Ledger' as [Module],'VCGL Entry' as [Transaction],count(case when TSPL_VCGL_Head.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_VCGL_Head.Status  =0 OR TSPL_VCGL_Head.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_VCGL_Head WHERE  convert(date,TSPL_VCGL_Head.Document_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_VCGL_Head.Document_Date ,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Sales And Distribution' as [Module],'Sale Order' as [Transaction],count(case when TSPL_SALES_ORDER_HEAD.Is_Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SALES_ORDER_HEAD.Is_Post ='N' OR TSPL_SALES_ORDER_HEAD.Is_Post IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SALES_ORDER_HEAD WHERE  convert(date,TSPL_SALES_ORDER_HEAD.Order_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Sales And Distribution' as [Module],'Shipment/Sale Invoice' as [Transaction],count(case when TSPL_SHIPMENT_MASTER.Is_Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SHIPMENT_MASTER.Is_Post ='N' OR TSPL_SHIPMENT_MASTER.Is_Post IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SHIPMENT_MASTER WHERE  convert(date,TSPL_SHIPMENT_MASTER.Shipment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SHIPMENT_MASTER.Shipment_Date,103) <= convert(date,'" + txtToDate.Value + "',103)  " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + "  and TSPL_SHIPMENT_MASTER.Shipment_Type='Sale'"
            qry += " union All "
            qry += " Select 'Sales And Distribution' as [Module],'Sale Return' as [Transaction],count(case when TSPL_SALE_RETURN_HEAD.Is_Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SALE_RETURN_HEAD.Is_Post ='N' OR TSPL_SALE_RETURN_HEAD.Is_Post IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SALE_RETURN_HEAD WHERE  convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " Union ALL"
            qry += " Select 'Sales And Distribution' as [Module],'Sale Return (Inter Company)' as [Transaction],count(case when TSPL_SALE_RETURN_INTER_HEAD.Is_Post =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SALE_RETURN_INTER_HEAD.Is_Post =0 OR TSPL_SALE_RETURN_INTER_HEAD.Is_Post IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SALE_RETURN_INTER_HEAD WHERE  convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            'qry += " union All "
            'qry += "      Select 'Sales And Distribution' as [Module],'Quick Settlement' as [Transaction], "
            'qry += " (Select Count(Distinct Quick_SettleMent_Id) from tspl_QuickSettleMent where convert(date,tspl_QuickSettleMent.Quick_Settlement_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_QuickSettleMent.Quick_Settlement_Date,103) <= convert(date,'" + txtToDate.Value + "',103) AND tspl_QuickSettleMent.Post ='Y') as [No of Posted Doc], "
            'qry += " (Select Count(Distinct Quick_SettleMent_Id) from tspl_QuickSettleMent where convert(date,tspl_QuickSettleMent.Quick_Settlement_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_QuickSettleMent.Quick_Settlement_Date,103) <= convert(date,'" + txtToDate.Value + "',103) AND (tspl_QuickSettleMent.Post is NULL OR tspl_QuickSettleMent.Post = 'N')) as [No of Unposetd Doc]"
            'qry += " Select 'Sales And Distribution' as [Module],'Quick Settlement' as [Transaction],count(case when tspl_QuickSettleMent.Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when tspl_QuickSettleMent.Post ='N' OR tspl_QuickSettleMent.Post IS NULL then 0 end)as [No of UnPosted Doc] from  tspl_QuickSettleMent WHERE  convert(date,tspl_QuickSettleMent.Quick_Settlement_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_QuickSettleMent.Quick_Settlement_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " select 'Material Management' as [Module],'Transfer(Load-In)' as [Transaction],count(case when TSPL_TRANSFER_HEAD.Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_TRANSFER_HEAD.Post ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_TRANSFER_HEAD WHERE  convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date,'" + txtToDate.Value + "',103) and Transfer_Type='LI' " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += " UNION ALL"
            qry += " select 'Material Management' as [Module],'Transfer(Load-Out)' as [Transaction],count(case when TSPL_TRANSFER_HEAD.Post ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_TRANSFER_HEAD.Post ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_TRANSFER_HEAD WHERE  convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date,'" + txtToDate.Value + "',103)and Transfer_Type='LO' " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            'qry += " Union ALL "
            'qry += " SELECT 'Material Management' as [Module],'Adjustment Entry' as [Transaction],count(case when TSPL_ADJUSTMENT_HEADER.Posted ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_ADJUSTMENT_HEADER.Posted ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_ADJUSTMENT_HEADER WHERE  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date,'" + txtToDate.Value + "',103)"
            qry += " Union All"
            qry += " Select 'Material Management' as [Module],'Empty Transactions' as [Transaction], ISNULL(SUM(PostedDoc),0) as [No of Posted Doc], ISNULL(SUM(UnpostedDoc),0) as [No of UnPosted Doc] from (Select Adjustment_No, MAX(Posted) as PostedDoc, MAX(Unposted) as UnpostedDoc  from (SELECT TSPL_ADJUSTMENT_DETAIL.Adjustment_No, Case WHEN Posted='Y' Then 1 Else 0 end as posted,  Case WHEN Posted='N' Then 1 Else 0 end as Unposted   FROM TSPL_ADJUSTMENT_HEADER Left Outer Join TSPL_ADJUSTMENT_DETAIL on TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No  LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_ADJUSTMENT_HEADER.Document_No  left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER .Shipment_No =TSPL_SALE_INVOICE_HEAD.Shipment_No  WHERE  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and TSPL_ADJUSTMENT_HEADER.Created_By in (" + objCommonVar.SelectedUser + ") ", " and TSPL_ADJUSTMENT_HEADER.Created_By ='" + objCommonVar.CurrentUserCode + "'") + "   AND (TSPL_ADJUSTMENT_HEADER.ItemType='E' AND ISNULL(Reference_Document, '')='' or TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale') ) AAA Group By Adjustment_No ) XXX"
            qry += " union All "
            qry += " Select 'Material Management' as [Module],'Production Entry' as [Transaction],count(case when TSPL_ADJUSTMENT_HEADER.Posted ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_ADJUSTMENT_HEADER.Posted ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_ADJUSTMENT_HEADER WHERE  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) and ItemType IN ('FT', 'FM') " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += " union All "
            qry += " Select 'Material Management' as [Module],'Store Adjustment' as [Transaction],count(case when TSPL_ADJUSTMENT_HEADER.Posted ='Y' then 1 end)as [No of Posted Doc],COUNT(case when TSPL_ADJUSTMENT_HEADER.Posted ='N' then 0 end)as [No of UnPosted Doc] from  TSPL_ADJUSTMENT_HEADER WHERE  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) and ItemType ='OT' " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + " "
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Purchase Requisition' as [Transaction],count(case when TSPL_REQUISITION_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_REQUISITION_HEAD.Status =0 OR TSPL_REQUISITION_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_REQUISITION_HEAD WHERE  convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Purchase Order' as [Transaction],count(case when TSPL_PURCHASE_ORDER_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_PURCHASE_ORDER_HEAD.Status=0 OR TSPL_PURCHASE_ORDER_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_PURCHASE_ORDER_HEAD WHERE  convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Gate Receipt Note' as [Transaction],count(case when TSPL_GRN_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_GRN_HEAD.Status =0 OR TSPL_GRN_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_GRN_HEAD WHERE  convert(date,TSPL_GRN_HEAD.GRN_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Material Receipt Note' as [Transaction],count(case when TSPL_MRN_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_MRN_HEAD.Status=0 OR TSPL_MRN_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_MRN_HEAD WHERE  convert(date,TSPL_MRN_HEAD.MRN_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Store Receipt Note' as [Transaction],count(case when TSPL_SRN_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SRN_HEAD.Status=0 OR TSPL_SRN_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SRN_HEAD WHERE  convert(date,TSPL_SRN_HEAD.SRN_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Purchase Invoice' as [Transaction],count(case when TSPL_PI_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_PI_HEAD.Status=0 OR TSPL_PI_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_PI_HEAD WHERE  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PI_HEAD.PI_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Purchase Return' as [Transaction],count(case when TSPL_PR_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_PR_HEAD.Status=0 OR TSPL_PR_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_PR_HEAD WHERE  convert(date,TSPL_PR_HEAD.PR_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PR_HEAD.PR_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'RGP/NRGP' as [Transaction],count(case when TSPL_RGP_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_RGP_HEAD.Status=0 OR TSPL_RGP_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_RGP_HEAD WHERE  convert(date,TSPL_RGP_HEAD.RGP_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_RGP_HEAD.RGP_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Issue/Return/Transfer' as [Transaction],count(case when TSPL_IssueReturn_HEAD.Status =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_IssueReturn_HEAD.Status=0 OR TSPL_IssueReturn_HEAD.Status IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_IssueReturn_HEAD WHERE  convert(date,TSPL_IssueReturn_HEAD.Doc_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Scrap LoadOut' as [Transaction],count(case when TSPL_SCRAPSALE_HEAD.ispost =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SCRAPSALE_HEAD.ispost=0 OR TSPL_SCRAPSALE_HEAD.ispost IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SCRAPSALE_HEAD WHERE  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""
            qry += " union All "
            qry += " Select 'Purchase Order' as [Module],'Scrap Invoice' as [Transaction],count(case when TSPL_SCRAPINVOICE_HEAD.ispost =1 then 1 end)as [No of Posted Doc],COUNT(case when TSPL_SCRAPINVOICE_HEAD.ispost=0 OR TSPL_SCRAPINVOICE_HEAD.ispost IS NULL then 0 end)as [No of UnPosted Doc] from  TSPL_SCRAPINVOICE_HEAD WHERE  convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date ,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " + IIf(clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal, " and Created_By in (" + objCommonVar.SelectedUser + ") ", " and Created_By ='" + objCommonVar.CurrentUserCode + "'") + ""


            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt

            gv1.MasterTemplate.Columns("Module").Width = 201
            gv1.MasterTemplate.Columns("Module").ReadOnly = True

            gv1.MasterTemplate.Columns("Transaction").Width = 301
            gv1.MasterTemplate.Columns("Transaction").ReadOnly = True

            gv1.MasterTemplate.Columns("No of Posted Doc").Width = 151
            gv1.MasterTemplate.Columns("No of Posted Doc").ReadOnly = True

            gv1.MasterTemplate.Columns("No of UnPosted Doc").Width = 151
            gv1.MasterTemplate.Columns("No of UnPosted Doc").ReadOnly = True

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub




    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try

            Dim IsOpenPsted As Boolean = False
            Dim Fromdate As DateTime = clsCommon.myCDate(txtFromDate.Value)
            Dim Todate As DateTime = clsCommon.myCDate(txtToDate.Value)
            If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then

                If (cbgUser.CheckedValue.Count > 0) Then
                    objCommonVar.SelectedUser = clsCommon.GetMulcallString(cbgUser.CheckedValue)
                End If
            Else
                objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
            End If
            If gv1.CurrentColumn Is gv1.Columns("No of Posted Doc") AndAlso (clsCommon.myCdbl(gv1.CurrentRow.Cells("No of Posted Doc").Value)) > 0 Then
                Dim frm As New FrmPendingAproval
                frm.IsOpenPsted = True
                frm.ModuleName = clsCommon.myCstr(gv1.CurrentRow.Cells("Module").Value)
                frm.Transaction = clsCommon.myCstr(gv1.CurrentRow.Cells("Transaction").Value)
                frm.SetUserMgmt(clsUserMgtCode.mbtnPendingApproval1)
                frm.fromdate = Fromdate
                frm.Todate = Todate
                frm.IsPostBack = True
                frm.ShowDialog()
            ElseIf gv1.CurrentColumn IsNot gv1.Columns("No of Posted Doc") AndAlso (clsCommon.myCdbl(gv1.CurrentRow.Cells("No of UnPosted Doc").Value)) > 0 Then
                Dim frm As New FrmPendingAproval
                frm.ModuleName = clsCommon.myCstr(gv1.CurrentRow.Cells("Module").Value)
                frm.Transaction = clsCommon.myCstr(gv1.CurrentRow.Cells("Transaction").Value)
                frm.SetUserMgmt(clsUserMgtCode.mbtnPendingApproval1)
                frm.fromdate = Fromdate
                frm.Todate = Todate
                frm.IsPostBack = True
                frm.ShowDialog()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmBulkPostingNew_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnShow.Enabled Then
            ShowAll()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
