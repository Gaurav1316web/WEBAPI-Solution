Imports common
Imports System.IO
Public Class SaleEinvoiceReport

    Private Sub RmSecurityDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Private Sub reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Sale EInvoice Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Sale EInvoice Report", gvData, arrHeader, "Sale EInvoice Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkB2C.Checked = True Then
            VarID += "_BC"
        ElseIf ChkB2B.Checked = True Then
            VarID += "_BB"
        ElseIf ChkBoth.Checked = True Then
            VarID += "_BO"
        End If
        gvData.VarID = VarID
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim strtxtfDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        GetReportGridID()
        Dim qry As String = "SELECT DISTINCT 
                           Bill_To_Location AS [Location],
                           TSPL_SD_SALE_INVOICE_HEAD.Sub_Location_code AS [Sub Location],
                           TSPL_LOCATION_MASTER.GSTNO AS [GST No],
                           TSPL_STATE_MASTER.GST_STATE_Code AS [State Code],
                           TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Code],
                           TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],
                           TSPL_STATE_MASTER.GST_STATE_Code AS [Party State], 
                           Ack_No AS [Ack No],
                           Ack_Date AS [Ack Date],
                           TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS [Invoice No],
                           Document_Date AS [Invoice Date],
                           CASE 
                              WHEN Is_Taxable = 1 THEN 'Taxable'  
                              WHEN Is_Taxable = 0 THEN 'Non-Taxable' 
                           END AS [Invoice Type],
                           TSPL_SD_SALE_INVOICE_HEAD.Total_Amt AS [Invoice Value],
                           TSPL_CUSTOMER_MASTER.GSTNO AS [Recipient Gst No], 
                           IRN_No AS [IRN No],
                           TSPL_SD_SALE_INVOICE_HEAD.EwayBillNo AS [EwayBillNo],
                           TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate AS [EwayBillDate],
                           TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt AS [KKF],
                           TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt AS [Mandi Tax],
                           TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt AS [CGST Amt],
                           TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt AS [SGST Amt],
                           TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt AS [TCS Amt]
                        FROM TSPL_SD_SALE_INVOICE_HEAD
                        LEFT OUTER JOIN TSPL_LOCATION_MASTER 
                           ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                        LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL 
                           ON TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
                        LEFT OUTER JOIN TSPL_TAX_MASTER 
                           ON TSPL_TAX_MASTER.Tax_Code = TSPL_SD_SALE_INVOICE_DETAIL.TAX1
                        LEFT OUTER JOIN TSPL_CUSTOMER_MASTER 
                           ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        LEFT OUTER JOIN TSPL_STATE_MASTER 
                           ON TSPL_CUSTOMER_MASTER.State = TSPL_STATE_MASTER.STATE_CODE
                            where Convert( Date, TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= Convert( Date,'" + strtxtfDate + "',103) AND 
                            Convert( Date, TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= Convert(Date,'" + strToDate + "',103) "

        If ChkB2B.Checked = True Then
            qry += " and TSPL_CUSTOMER_MASTER.GST_Registered=1 "
        ElseIf chkB2C.Checked = True Then
            qry += " and TSPL_CUSTOMER_MASTER.GST_Registered=0 "
        End If

        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and tspl_customer_master.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt

            'gvData.Columns("Customer_Code").IsVisible = False
            'gvData.Columns("Customerqty").IsVisible = False
            'gvData.Columns("CAN_Qty").IsVisible = False
            'gvData.Columns("CRATE_Qty").IsVisible = False
            'gvData.Columns("BOX_Qty").IsVisible = False
            'gvData.Columns("CarteQtyLtr").IsVisible = False
            'gvData.Columns("CanQtyLtr").IsVisible = False


            'SetGridFormationOFGV1()
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()
        End If
    End Sub

    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        ' gvData.GroupDescriptors.Add(New GridGroupByExpression("Route as RouteName format ""{0}: {1}"" Group By Route"))

        For i As Integer = 9 To gvData.Columns.Count - 1
            Dim aa = gvData.Columns(i).HeaderText()
            Dim item8 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

        Next

        gvData.ShowGroupPanel = True
        gvData.MasterTemplate.AutoExpandGroups = True

        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gvData.MasterTemplate.ShowTotals = True
        'ReStoreGridLayout()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub chkB2C_CheckStateChanged(sender As Object, e As EventArgs) Handles chkB2C.CheckStateChanged
        If chkB2C.Checked Then
            ChkB2B.Checked = False
            ChkBoth.Checked = False
        End If
    End Sub

    Private Sub ChkB2B_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkB2B.CheckStateChanged
        If ChkB2B.Checked Then
            chkB2C.Checked = False
            ChkBoth.Checked = False
        End If
    End Sub

    Private Sub ChkBoth_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkBoth.CheckStateChanged
        If ChkBoth.Checked Then
            ChkB2B.Checked = False
            chkB2C.Checked = False
        End If
    End Sub
End Class
