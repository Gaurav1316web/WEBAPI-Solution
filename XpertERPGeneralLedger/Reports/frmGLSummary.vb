Imports common
Imports System.Data.SqlClient

Public Class FrmGLSummary
    Public strVoucherNo As String
    Public TransType As String

    Private Sub FrmGLSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TransType = clsDBFuncationality.getSingleValue("Select Source_Code from TSPL_JOURNAL_MASTER WHERE Voucher_No='" + strVoucherNo + "'")
            LoadData(TransType, strVoucherNo)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadData(ByVal TransType As String, ByVal strVoucherNo As String)
        Dim qry As String = ""
        qry = "Select Voucher_No, TSPL_GL_SOURCECODE.SourceDescription as [docType], Voucher_Desc as [desc], Source_Narration as [subLedgerDesc], "
        qry += " Total_Debit_Amt as [Amount]  from TSPL_JOURNAL_MASTER "
        qry += " LEFT OUTER JOIN TSPL_GL_SOURCECODE ON TSPL_GL_SOURCECODE.SourceCode=TSPL_JOURNAL_MASTER.Source_Code WHERE Voucher_No='" + strVoucherNo + "'"
        If clsCommon.CompairString(TransType, "AR-PY") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "AR-PI") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "AR-MI") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "AR-OA") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "AR-RF") = CompairStringResult.Equal Then
            qry += " UNION ALL "
            qry += "Select Receipt_No, 'Receipt' as [docType], Entry_Desc as [desc], Narration as [subLedgerDesc], Receipt_Amount As [Amount]  "
            qry += " from TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_RECEIPT_HEADER.Receipt_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No = '" + strVoucherNo + "'"
        ElseIf clsCommon.CompairString(TransType, "AP-PY") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "AP-MI") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += "Select Payment_No, 'Payment' as [docType], Entry_Desc as [desc], Narration as [subLedgerDesc], Payment_Amount As [Amount] from TSPL_PAYMENT_HEADER "
            qry += "LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_PAYMENT_HEADER.Payment_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No = '" + strVoucherNo + "'"
            'Case clsUserMgtCode.LoadOut
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Shipment_No As Voucher_No, 'Shipment' as [docType], Description as [desc], '' as [subLedgerDesc], Shipment_Detail_Total_Amt As [Amount] from TSPL_SHIPMENT_MASTER "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_SHIPMENT_MASTER.Shipment_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            qry += " UNION ALL"
            qry += " Select Sale_Invoice_No As Voucher_No, 'Sale Invoice' as [docType], Description as [desc], '' as [subLedgerDesc], Total_Invoice_Amt As [Amount] from TSPL_SALE_INVOICE_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.FrmVendorService
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Document_No As Voucher_No, 'AP Invoice' as [docType], Description as [desc], TSPL_VENDOR_INVOICE_HEAD.Remarks as [subLedgerDesc], TSPL_VENDOR_INVOICE_HEAD.Document_Total As [Amount] from TSPL_VENDOR_INVOICE_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.bankTransfer
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Transfer_No As Voucher_No, 'Bank Transfer' as [docType], Description as [desc], '' as [subLedgerDesc], Transfer_Amount As [Amount] from TSPL_BANK_TRANSFER "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_BANK_TRANSFER.Transfer_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.mbtnSRN

            'Case clsUserMgtCode.Transfer
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Transfer_No As Voucher_No, 'Transfer' as [docType], Description as [desc], Case When Trans_Type='LI' Then 'Load In' Else 'Load Out' End as [subLedgerDesc], TSPL_TRANSFER_HEAD.Total_Transfer_Amount As [Amount] from TSPL_TRANSFER_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_TRANSFER_HEAD.Transfer_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.reverseTransaction
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Reverse_Code As Voucher_No, 'Bank Reverse' as [docType], Reason as [desc], 'Against-'+ Case When TSPL_BANK_REVERSE.Source_Type='AP' Then 'Payment' Else 'Receipt' End as [subLedgerDesc], TSPL_BANK_REVERSE.Amount As [Amount] from TSPL_BANK_REVERSE "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_BANK_REVERSE.Reverse_Code"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            qry += " UNION ALL"
            qry += " Select Payment_No As Voucher_No, 'Payment' as [docType], Entry_Desc as [desc], Remarks as [subLedgerDesc], Payment_Amount+ISNULL(TDS_Amount,0) As [Amount] from TSPL_BANK_REVERSE"
            qry += " LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No"
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_BANK_REVERSE.Reverse_Code"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "' AND TSPL_BANK_REVERSE.Source_Type='AP'"
            qry += " UNION ALL"
            qry += " Select Receipt_No As Voucher_No, 'Receipt' as [docType], Entry_Desc as [desc], Remarks as [subLedgerDesc], Receipt_Amount As [Amount] from TSPL_BANK_REVERSE"
            qry += " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No"
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_BANK_REVERSE.Reverse_Code"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "' AND TSPL_BANK_REVERSE.Source_Type='AR'"
            'Case clsUserMgtCode.mbtnStoreAdjustment
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select TSPL_ADJUSTMENT_HEADER.Adjustment_No As Voucher_No, 'Adjustment' as [docType], MAX(Description) as [desc], '' as [subLedgerDesc], SUM(Item_Cost) As [Amount] from TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No"
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "' GROUP BY TSPL_ADJUSTMENT_HEADER.Adjustment_No"
            'Case clsUserMgtCode.frmSNSaleInvoice
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Sale_Invoice_No As Voucher_No, 'Sale Invoice' as [docType], Description as [desc], '' as [subLedgerDesc], Total_Invoice_Amt As [Amount] from TSPL_SALE_INVOICE_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.ReceiptAdjustmentEntry
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Adjustment_No As Voucher_No, 'Receipt Adjustment' as [docType], Description as [desc], '' as [subLedgerDesc], Doc_Amount As [Amount] from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Receipt_Adjustment_Header.Adjustment_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.mbtnPurchaseInvoice
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION(ALL)"
            qry += " Select Document_No As Voucher_No, 'AP Invoice' as [docType], Description as [desc], TSPL_VENDOR_INVOICE_HEAD.Remarks as [subLedgerDesc], TSPL_VENDOR_INVOICE_HEAD.Document_Total As [Amount] from TSPL_VENDOR_INVOICE_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            qry += " UNION ALL"
            qry += " Select TSPL_PI_HEAD.PI_No As Voucher_No, 'Purchase Invoice' as [docType], TSPL_PI_HEAD.Description as [desc], '' as [subLedgerDesc], TSPL_PI_HEAD.PI_Total_Amt As [Amount] from TSPL_VENDOR_INVOICE_HEAD"
            qry += " LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No"
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            qry += " UNION ALL"
            qry += " Select TSPL_SRN_HEAD.SRN_No As Voucher_No, 'Store Receipt Note' as [docType], TSPL_SRN_HEAD.Description as [desc], '' as [subLedgerDesc], TSPL_SRN_HEAD.SRN_Total_Amt As [Amount] from TSPL_VENDOR_INVOICE_HEAD"
            qry += " LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No"
            qry += " LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN"
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No "
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.mbtnPurchaseReturn
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select PR_No As Voucher_No, 'Purchase Return' as [docType], Description as [desc], '' as [subLedgerDesc], TSPL_PR_HEAD.PR_Total_Amt As [Amount] from TSPL_PR_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_PR_HEAD.PR_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.saleReturn, clsUserMgtCode.saleReturn
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Sale_Return_No As Voucher_No, 'Sale Return' as [docType], Description as [desc], '' as [subLedgerDesc], TSPL_SALE_RETURN_HEAD.Total_Invoice_Amt As [Amount] from TSPL_SALE_RETURN_HEAD "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            qry += " UNION ALL"
            qry += " Select Document_No As Voucher_No, 'Sale Return Inter' as [docType], Description as [desc], '' as [subLedgerDesc], TSPL_SALE_RETURN_INTER_HEAD.Detail_Total_Amt As [Amount] from TSPL_SALE_RETURN_INTER_HEAD  "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_SALE_RETURN_INTER_HEAD.Document_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case clsUserMgtCode.mbtnARInvoiceEntry
        ElseIf clsCommon.CompairString(TransType, "AP---PY") = CompairStringResult.Equal Then
            qry += " UNION ALL"
            qry += " Select Document_No As Voucher_No, 'AR Invoice' as [docType], Description as [desc], '' as [subLedgerDesc], TSPL_Customer_Invoice_Head.Document_Total As [Amount] from TSPL_Customer_Invoice_Head "
            qry += " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Customer_Invoice_Head.Document_No"
            qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'"
            'Case Else
        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("GL Summary", gv1, Nothing, "glSummary")
            Else
                clsCommon.MyMessageBoxShow("No data found to export.")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
