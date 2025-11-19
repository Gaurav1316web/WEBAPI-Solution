Imports common
Imports System.IO
Public Class FrmAPSSaleReport
    Private Sub FrmAPSSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtfDate.Value
        chkGSTSaleRpt.Checked = True
        chkProdRpt.Checked = True
        ChkTenderTrackRpt.Checked = True
    End Sub

    Private Sub chkGSTSaleRpt_CheckedChanged(sender As Object, e As EventArgs) Handles chkGSTSaleRpt.CheckedChanged
        If chkGSTSaleRpt.Checked Then
            chkProdRpt.Checked = False
            ChkTenderTrackRpt.Checked = False
        End If
    End Sub

    Private Sub chkProdRpt_CheckedChanged(sender As Object, e As EventArgs) Handles chkProdRpt.CheckedChanged
        If chkProdRpt.Checked Then
            chkGSTSaleRpt.Checked = False
            ChkTenderTrackRpt.Checked = False
        End If
    End Sub

    Private Sub ChkTenderTrackRpt_CheckedChanged(sender As Object, e As EventArgs) Handles ChkTenderTrackRpt.CheckedChanged
        If ChkTenderTrackRpt.Checked Then
            chkGSTSaleRpt.Checked = False
            chkProdRpt.Checked = False
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Private Sub reset()
        chkGSTSaleRpt.Checked = True
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtfDate.Value
        txtMultiCustomer.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        chkProdRpt.Checked = False
        ChkTenderTrackRpt.Checked = False
    End Sub

    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gvData.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        If gvData.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
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
                clsCommon.MyExportToExcelGrid("APS Sales Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("APS Sales Report", gvData, arrHeader, "APS Sales  Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvData
        LoadData()
    End Sub
    Public Sub LoadData()
        Try
            Dim Whr As String = ""
            Dim dt As DataTable = Nothing
            Dim strtxtfDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = ""
            Dim whrclsDate As String = ""
            If strtxtfDate > strToDate Then
                Throw New Exception("From Date is greater then To Date")
            End If
            whrclsDate += " where Convert( Date, TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= Convert( Date,'" + strtxtfDate + "',103) AND 
                                Convert( Date, TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= Convert(Date,'" + strToDate + "',103) and TSPL_SD_SALE_INVOICE_HEAD.Status='1' and  TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='CT' "
            If chkGSTSaleRpt.Checked Then
                qry = " select Convert(varchar(20),TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Invoice Date], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Invoice No],TSPL_CUSTOMER_MASTER.Customer_Name as [Party Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_CUSTOMER_MASTER.State as [State Code],TSPL_ITEM_MASTER.Short_Description as [Product Name],TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Qty Measure],TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Product Qty],TSPL_ITEM_MASTER.HSN_Code as [HSN Code],
 case when TSPL_SD_SALE_INVOICE_DETAIL.TAX1='KKF' or TSPL_SD_SALE_INVOICE_DETAIL.TAX2='KKF' then  TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate + TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate else case when TSPL_SD_SALE_INVOICE_DETAIL.tax1='CGST' or TSPL_SD_SALE_INVOICE_DETAIL.tax2='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate + TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate else 0 end end  as [GST Rate],
TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt as [Basic Amount],
case when TSPL_SD_SALE_INVOICE_DETAIL.tax1='MNDTAX' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax2='MNDTAX' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end end  as [Mandi Tax],
 case when TSPL_SD_SALE_INVOICE_DETAIL.tax1='KKF' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax2='KKF' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end end  as [Krishk Kalyan Fee],
case when TSPL_SD_SALE_INVOICE_DETAIL.tax2='TCS' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax3='TCS' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax5='TCS' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end  end end as [Party TCS],
case when TSPL_SD_SALE_INVOICE_DETAIL.tax1='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax3='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end end as [CGST],
case when TSPL_SD_SALE_INVOICE_DETAIL.tax2='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax4='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end end as [SGST],
case when TSPL_SD_SALE_INVOICE_DETAIL.tax1='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else case when TSPL_SD_SALE_INVOICE_DETAIL.tax3='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end end as [IGST],
TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt  as [Total Amount],
TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms as [Pay Mode],
TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type as [Invoice Type] "
            ElseIf chkProdRpt.Checked Then
                Throw New Exception("Product Wise Report under progress..")
            ElseIf ChkTenderTrackRpt.Checked Then
                Throw New Exception("Tender Tacking Report under progress..")

            End If
            qry += " from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " + whrclsDate
            dt = clsDBFuncationality.GetDataTable(qry)
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = Nothing
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.DataSource = dt


                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = True
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ShowFilteringRow = True
                SetGridFormat()
                SetGridFormationOFGV1()
                gvData.BestFitColumns()

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        ' gvData.GroupDescriptors.Add(New GridGroupByExpression("Route as RouteName format ""{0}: {1}"" Group By Route"))

        'For i As Integer = 10 To gvData.Columns.Count - 1
        '    Dim aa = gvData.Columns(i).HeaderText()
        '    Dim item8 As New GridViewSummaryItem("[Total Amount]", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item8)

        'Next
        'Dim aa = gvData.Columns(i).HeaderText()
        Dim item81 As New GridViewSummaryItem("Basic Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item81)

        Dim item82 As New GridViewSummaryItem("Mandi Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item82)

        Dim item83 As New GridViewSummaryItem("Krishk Kalyan Fee", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item83)

        Dim item84 As New GridViewSummaryItem("Party TCS", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item84)

        Dim item85 As New GridViewSummaryItem("CGST", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item85)

        Dim item86 As New GridViewSummaryItem("SGST", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item86)

        Dim item87 As New GridViewSummaryItem("IGST", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item87)

        Dim item88 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item88)

        Dim item89 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item89)

        'Dim item90 As New GridViewSummaryItem("Base Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item90)

        'Dim item91 As New GridViewSummaryItem("Invoice Value", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item91)

        gvData.ShowGroupPanel = True
        gvData.MasterTemplate.AutoExpandGroups = True

        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gvData.MasterTemplate.ShowTotals = True
        'ReStoreGridLayout()

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub SetGridFormat()
        Try
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            For ii As Integer = 0 To gvData.Columns.Count - 1
                gvData.Columns(ii).ReadOnly = True
                gvData.Columns(ii).BestFit()
            Next
            If chkGSTSaleRpt.Checked Then

                gvData.Columns("Invoice Date").HeaderText = "Invoice Date"
                gvData.Columns("Invoice Date").Width = 100
                gvData.Columns("Invoice Date").IsVisible = True

                gvData.Columns("Invoice No").HeaderText = "Invoice No"
                gvData.Columns("Invoice No").Width = 100
                gvData.Columns("Invoice No").IsVisible = True

                gvData.Columns("Party Name").HeaderText = "Party Name"
                gvData.Columns("Party Name").Width = 100
                gvData.Columns("Party Name").IsVisible = True

                gvData.Columns("GST No").HeaderText = "GST No"
                gvData.Columns("GST No").Width = 100
                gvData.Columns("GST No").IsVisible = True

                gvData.Columns("State Code").HeaderText = "State Code"
                gvData.Columns("State Code").Width = 100
                gvData.Columns("State Code").IsVisible = True


                gvData.Columns("Product Name").HeaderText = "Product Name"
                gvData.Columns("Product Name").Width = 100
                gvData.Columns("Product Name").IsVisible = True

                gvData.Columns("Qty Measure").HeaderText = "Qty Measure"
                gvData.Columns("Qty Measure").Width = 150
                gvData.Columns("Qty Measure").IsVisible = True

                gvData.Columns("Product Qty").HeaderText = "Product Qty"
                gvData.Columns("Product Qty").Width = 100
                gvData.Columns("Product Qty").IsVisible = True

                gvData.Columns("HSN Code").HeaderText = "HSN Code"
                gvData.Columns("HSN Code").Width = 100
                gvData.Columns("HSN Code").IsVisible = True

                gvData.Columns("GST Rate").HeaderText = "GST Rate"
                gvData.Columns("GST Rate").Width = 100
                gvData.Columns("GST Rate").IsVisible = True

                gvData.Columns("Basic Amount").HeaderText = "Basic Amount"
                gvData.Columns("Basic Amount").Width = 100
                gvData.Columns("Basic Amount").IsVisible = True

                gvData.Columns("Mandi Tax").HeaderText = "Mandi Tax"
                gvData.Columns("Mandi Tax").Width = 100
                gvData.Columns("Mandi Tax").IsVisible = True

                gvData.Columns("Krishk Kalyan Fee").HeaderText = "Krishk Kalyan Fee"
                gvData.Columns("Krishk Kalyan Fee").Width = 100
                gvData.Columns("Krishk Kalyan Fee").IsVisible = True

                gvData.Columns("Party TCS").HeaderText = "Party TCS"
                gvData.Columns("Party TCS").Width = 100
                gvData.Columns("Party TCS").IsVisible = True

                gvData.Columns("CGST").HeaderText = "CGST"
                gvData.Columns("CGST").Width = 100
                gvData.Columns("CGST").IsVisible = True

                gvData.Columns("SGST").HeaderText = "SGST"
                gvData.Columns("SGST").Width = 100
                gvData.Columns("SGST").IsVisible = True

                gvData.Columns("IGST").HeaderText = "IGST"
                gvData.Columns("IGST").Width = 100
                gvData.Columns("IGST").IsVisible = True

                gvData.Columns("Total Amount").HeaderText = "Total Amount"
                gvData.Columns("Total Amount").Width = 100
                gvData.Columns("Total Amount").IsVisible = True
                gvData.Columns("Pay Mode").HeaderText = "Pay Mode"
                gvData.Columns("Pay Mode").Width = 100
                gvData.Columns("Pay Mode").IsVisible = True

                'gvData.Columns("Invoice Value").HeaderText = "Invoice Value"
                'gvData.Columns("Invoice Value").Width = 100
                'gvData.Columns("Invoice Value").IsVisible = True

                gvData.Columns("Invoice Type").HeaderText = "B2B/B2C"
                gvData.Columns("Invoice Type").Width = 100
                gvData.Columns("Invoice Type").IsVisible = True

                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim item1 As New GridViewSummaryItem("INWARDQTYReportUom", "{0:n2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item1)
                'Dim item2 As New GridViewSummaryItem("OUTWARDQTYReportUom", "{0:n2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item2)
                'gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

            ElseIf chkProdRpt.Checked Then

                gvData.Columns("Supply Date").HeaderText = "Supply Date"
                gvData.Columns("Supply Date").Width = 100
                gvData.Columns("Supply Date").IsVisible = True

                gvData.Columns("Shift Type").HeaderText = "Shift Type"
                gvData.Columns("Shift Type").Width = 100
                gvData.Columns("Shift Type").IsVisible = True

                gvData.Columns("Location").HeaderText = "Location"
                gvData.Columns("Location").Width = 100
                gvData.Columns("Location").IsVisible = True

                gvData.Columns("Sub Location").HeaderText = "Sub Location"
                gvData.Columns("Sub Location").Width = 100
                gvData.Columns("Sub Location").IsVisible = True

                gvData.Columns("GST No").HeaderText = "GST No"
                gvData.Columns("GST No").Width = 100
                gvData.Columns("GST No").IsVisible = True

                gvData.Columns("State Code").HeaderText = "State Code"
                gvData.Columns("State Code").Width = 100
                gvData.Columns("State Code").IsVisible = True

                gvData.Columns("Customer Code").HeaderText = "Customer Code"
                gvData.Columns("Customer Code").Width = 100
                gvData.Columns("Customer Code").IsVisible = True

                gvData.Columns("Customer Name").HeaderText = "Customer Name"
                gvData.Columns("Customer Name").Width = 100
                gvData.Columns("Customer Name").IsVisible = True

                gvData.Columns("Party State").HeaderText = "Party State"
                gvData.Columns("Party State").Width = 100
                gvData.Columns("Party State").IsVisible = True

                gvData.Columns("Recipient Gst No").HeaderText = "Recipient Gst No"
                gvData.Columns("Recipient Gst No").Width = 100
                gvData.Columns("Recipient Gst No").IsVisible = True

                gvData.Columns("E Invoice Type").HeaderText = "E Invoice Type"
                gvData.Columns("E Invoice Type").Width = 100
                gvData.Columns("E Invoice Type").IsVisible = True

                gvData.Columns("Ack No").HeaderText = "Ack No"
                gvData.Columns("Ack No").Width = 100
                gvData.Columns("Ack No").IsVisible = True

                gvData.Columns("Ack Date").HeaderText = "Ack Date"
                gvData.Columns("Ack Date").Width = 100
                gvData.Columns("Ack Date").IsVisible = True

                gvData.Columns("IRN No").HeaderText = "IRN No"
                gvData.Columns("IRN No").Width = 100
                gvData.Columns("IRN No").IsVisible = True

                gvData.Columns("Invoice No").HeaderText = "Invoice No"
                gvData.Columns("Invoice No").Width = 100
                gvData.Columns("Invoice No").IsVisible = True

                gvData.Columns("Invoice Date").HeaderText = "Invoice Date"
                gvData.Columns("Invoice Date").Width = 100
                gvData.Columns("Invoice Date").IsVisible = True

                gvData.Columns("Invoice Type").HeaderText = "Invoice Type"
                gvData.Columns("Invoice Type").Width = 100
                gvData.Columns("Invoice Type").IsVisible = True
                gvData.Columns("Zone_Code").HeaderText = "Zone Code"
                gvData.Columns("Zone_Code").Width = 100
                gvData.Columns("Zone_Code").IsVisible = True

                gvData.Columns("Route No").HeaderText = "Route No"
                gvData.Columns("Route No").Width = 100
                gvData.Columns("Route No").IsVisible = True

                gvData.Columns("Item Code").HeaderText = "Item Code"
                gvData.Columns("Item Code").Width = 100
                gvData.Columns("Item Code").IsVisible = True

                gvData.Columns("Item Name").HeaderText = "Item Name"
                gvData.Columns("Item Name").Width = 100
                gvData.Columns("Item Name").IsVisible = True

                gvData.Columns("UOM").HeaderText = "UOM"
                gvData.Columns("UOM").Width = 100
                gvData.Columns("UOM").IsVisible = True

                gvData.Columns("Qty").HeaderText = "Qty"
                gvData.Columns("Qty").Width = 100
                gvData.Columns("Qty").IsVisible = True

                gvData.Columns("Item Amount").HeaderText = "Item Amount"
                gvData.Columns("Item Amount").Width = 100
                gvData.Columns("Item Amount").IsVisible = True

                gvData.Columns("HSN Code").HeaderText = "HSN Code"
                gvData.Columns("HSN Code").Width = 100
                gvData.Columns("HSN Code").IsVisible = True

                gvData.Columns("EwayBillNo").HeaderText = "EwayBillNo"
                gvData.Columns("EwayBillNo").Width = 100
                gvData.Columns("EwayBillNo").IsVisible = True

                gvData.Columns("EwayBillDate").HeaderText = "EwayBillDate"
                gvData.Columns("EwayBillDate").Width = 100
                gvData.Columns("EwayBillDate").IsVisible = True


                gvData.Columns("KKF %").HeaderText = "KKF %"
                gvData.Columns("KKF %").Width = 100
                gvData.Columns("KKF %").IsVisible = True

                gvData.Columns("KKF Amt").HeaderText = "KKF Amt"
                gvData.Columns("KKF Amt").Width = 100
                gvData.Columns("KKF Amt").IsVisible = True

                gvData.Columns("Mandi Tax %").HeaderText = "Mandi Tax %"
                gvData.Columns("Mandi Tax %").Width = 100
                gvData.Columns("Mandi Tax %").IsVisible = True

                gvData.Columns("Mandi Tax Amt").HeaderText = "Mandi Tax Amt"
                gvData.Columns("Mandi Tax Amt").Width = 100
                gvData.Columns("Mandi Tax Amt").IsVisible = True

                gvData.Columns("CGST %").HeaderText = "CGST %"
                gvData.Columns("CGST %").Width = 100
                gvData.Columns("CGST %").IsVisible = True

                gvData.Columns("CGST Amt").HeaderText = "CGST Amt"
                gvData.Columns("CGST Amt").Width = 100
                gvData.Columns("CGST Amt").IsVisible = True

                gvData.Columns("IGST %").HeaderText = "IGST %"
                gvData.Columns("IGST %").Width = 100
                gvData.Columns("IGST %").IsVisible = True

                gvData.Columns("IGST Amt").HeaderText = "IGST Amt"
                gvData.Columns("IGST Amt").Width = 100
                gvData.Columns("IGST Amt").IsVisible = True

                gvData.Columns("SGST %").HeaderText = "SGST %"
                gvData.Columns("SGST %").Width = 100
                gvData.Columns("SGST %").IsVisible = True

                gvData.Columns("SGST Amt").HeaderText = "SGST Amt"
                gvData.Columns("SGST Amt").Width = 100
                gvData.Columns("SGST Amt").IsVisible = True

                gvData.Columns("TCS %").HeaderText = "TCS %"
                gvData.Columns("TCS %").Width = 100
                gvData.Columns("TCS %").IsVisible = True

                gvData.Columns("TCS Amt").HeaderText = "TCS Amt"
                gvData.Columns("TCS Amt").Width = 100
                gvData.Columns("TCS Amt").IsVisible = True

                gvData.Columns("Total Amount").HeaderText = "Total Amount"
                gvData.Columns("Total Amount").Width = 100
                gvData.Columns("Total Amount").IsVisible = True

                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim item1 As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item1)
                'Dim item2 As New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item2)
                'Dim item3 As New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item3)
                'Dim item4 As New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item4)
                'gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class