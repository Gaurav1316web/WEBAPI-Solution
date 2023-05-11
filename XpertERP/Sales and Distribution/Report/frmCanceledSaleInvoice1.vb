Imports common
Imports System.Data.SqlClient

Public Class FrmCanceledSaleInvoice1
    Inherits FrmMainTranScreen
    Private Sub FrmCanceledSaleInvoice1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadLocation()
        Reset()
        SetUserMgmtNew()
    End Sub

    Private Sub Reset()
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rbtnLocationAll.IsChecked = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCanceledSaleInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub
    Private Sub loadLocation()
        Dim qry As String = "SELECT Location_Code AS Location, Location_Desc AS Description FROM dbo.TSPL_LOCATION_MASTER WHERE Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Description"
    End Sub


    Public Sub funExport()
        Dim location As String = ""
        Dim StrLocation As String = ""
        If cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            StrLocation = location.Replace("'", "")
        End If
        If rbtnLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select atleast Single location Or Select All")
        End If
        Try

            'Dim strCmd As String = " SELECT '" + dtpFdate.Value.Date + "' AS [From Date], '" + dtpToDate.Value.Date + "' AS [To Date], '" + clsCommon.GETSERVERDATE() + "' AS [Run Date],'" + StrLocation + "' as StrLocation, TSPL_LOCATION_MASTER.Location_Desc AS [Location], "
            'strCmd += " TSPL_SALE_INVOICE_HEAD_CANCEL.Cust_Name AS [Customer], TSPL_SALE_INVOICE_HEAD_CANCEL.Sale_Invoice_No AS [Invoice No], "
            'strCmd += " TSPL_SALE_INVOICE_HEAD_CANCEL.Sale_Invoice_Date AS [Invoice Date], TSPL_SALE_INVOICE_HEAD_CANCEL.Invoice_Type AS [Invoice Type], "
            'strCmd += " TSPL_SALE_INVOICE_HEAD_CANCEL.Total_Invoice_Amt AS [Invoice Amount] ,TSPL_SALE_INVOICE_HEAD_CANCEL.cancel_by as [Cancel By], convert(varchar, TSPL_SALE_INVOICE_HEAD_CANCEL.cancel_date,103) as [Cancel Date],TSPL_SALE_INVOICE_HEAD_CANCEL.cancel_Remarks as [Cancel Remarks] FROM TSPL_SALE_INVOICE_HEAD_CANCEL "
            'strCmd += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD_CANCEL.Location= TSPL_LOCATION_MASTER.Location_Code "
            'strCmd += " WHERE CONVERT(DATE, TSPL_SALE_INVOICE_HEAD_CANCEL.Sale_Invoice_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MMM/yyyy") + "', 103) "
            'strCmd += "AND CONVERT(DATE, TSPL_SALE_INVOICE_HEAD_CANCEL.Sale_Invoice_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103) "
            
            Dim strCmd As String = " SELECT '" + dtpFdate.Value.Date + "' AS [From Date], '" + dtpToDate.Value.Date + "' AS [To Date], '" + clsCommon.GETSERVERDATE() + "' AS [Run Date],'" + StrLocation + "' as StrLocation, "
            strCmd += "     * from  (SELECT  'Sale Invoice' as TransferType,  TSPL_LOCATION_MASTER.Location_Desc AS [Location],  TSPL_SALE_INVOICE_HEAD_CANCEL.Cust_Name AS [Customer], TSPL_SALE_INVOICE_HEAD_CANCEL.Sale_Invoice_No AS [Invoice No],  TSPL_SALE_INVOICE_HEAD_CANCEL.Sale_Invoice_Date AS [Invoice Date], TSPL_SALE_INVOICE_HEAD_CANCEL.Invoice_Type AS [Invoice Type],  TSPL_SALE_INVOICE_HEAD_CANCEL.Total_Invoice_Amt AS [Invoice Amount] ,TSPL_SALE_INVOICE_HEAD_CANCEL.cancel_by as [Cancel By], convert(varchar, TSPL_SALE_INVOICE_HEAD_CANCEL.cancel_date,103) as [Cancel Date],TSPL_SALE_INVOICE_HEAD_CANCEL.cancel_Remarks as [Cancel Remarks] FROM TSPL_SALE_INVOICE_HEAD_CANCEL  LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD_CANCEL.Location= TSPL_LOCATION_MASTER.Location_Code"
            strCmd += "       union all "
            strCmd += "  SELECT  'Transfer' as TransferType,  TSPL_LOCATION_MASTER.Location_Desc AS [Location],  '' AS [Customer], TSPL_TRANSFER_HEAD_cancel.transfer_no AS [Invoice No],  TSPL_TRANSFER_HEAD_cancel.transfer_date AS [Invoice Date], TSPL_TRANSFER_HEAD_cancel.transfer_type AS [Invoice Type],  TSPL_TRANSFER_HEAD_cancel.Total_transfer_Amount AS [Invoice Amount] ,TSPL_TRANSFER_HEAD_cancel.cancel_by as [Cancel By], convert(varchar, TSPL_TRANSFER_HEAD_cancel.cancel_date,103) as [Cancel Date],TSPL_TRANSFER_HEAD_cancel.cancel_Remarks as [Cancel Remarks] FROM TSPL_TRANSFER_HEAD_cancel  LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD_cancel.from_location= TSPL_LOCATION_MASTER.Location_Code  "
            strCmd += "     union all "
            strCmd += "  SELECT  'Material Sale' as TransferType,  TSPL_LOCATION_MASTER.Location_Desc AS [Location],  '' AS [Customer], TSPL_SCRAPINVOICE_HEAD_CANCEL.shipment_no AS [Invoice No],  TSPL_SCRAPINVOICE_HEAD_CANCEL.shipment_date AS [Invoice Date], TSPL_SCRAPINVOICE_HEAD_CANCEL.invoice_type AS [Invoice Type],  TSPL_SCRAPINVOICE_HEAD_CANCEL.ship_total_amt AS [Invoice Amount] ,TSPL_SCRAPINVOICE_HEAD_CANCEL.cancel_by as [Cancel By], convert(varchar, TSPL_SCRAPINVOICE_HEAD_CANCEL.cancel_date,103) as [Cancel Date],TSPL_SCRAPINVOICE_HEAD_CANCEL.cancel_Remarks as [Cancel Remarks] FROM TSPL_SCRAPINVOICE_HEAD_CANCEL  LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_SCRAPINVOICE_HEAD_CANCEL.loc_code= TSPL_LOCATION_MASTER.Location_Code) as Cancel Where   2=2 "
            strCmd += "AND CONVERT(DATE, [Invoice Date], 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MMM/yyyy") + "', 103) "
            strCmd += "AND CONVERT(DATE, [Invoice Date], 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103) "

            If cbgLocation.CheckedValue.Count > 0 Then
                strCmd += "AND Location IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strCmd)
            If dt.Rows.Count <= 0 Then
                Throw New Exception("No Record Found")
            Else

                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strCmd), "crptCanceledSaleInvoice", "Canceleld Sale Invoice")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub rbtnLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        funExport()
    End Sub

    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub
End Class
