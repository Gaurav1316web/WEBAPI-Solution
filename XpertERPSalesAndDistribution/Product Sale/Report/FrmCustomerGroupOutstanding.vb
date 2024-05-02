Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop


Public Class FrmCustomerGroupOutstanding
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrTransaction As ArrayList
   
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        ToDate.Value = clsCommon.GETSERVERDATE()
    End Sub


    Private Sub SetUserMgmtNew()

        ' MyBase.SetUserMgmt(clsUserMgtCode.stockRecoBatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport

    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    
    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True

            ' Ticket No : ERO/23/04/19-000571 By Prabhakar richa ERO/01/07/19-000664 3 July,2019,ERO/05/08/19-000983 deduct Sale return amount from Invoice amount if created,ERO/07/08/19-000988
            Dim StrQuery As String = Nothing
            StrQuery = " Select * from ( select TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ,innINVHead.Document_No as [AR Invoice No], CONVERT(VARCHAR(15),innINVHead.Document_Date,103) AS [AR Invocie Date] ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_LOCATION_MASTER.Location_Desc AS [Location],TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Invoice No.]," & _
                " CONVERT(VARCHAR(15),TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) AS [Date],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Invoice Amt (Rs)],isnull(TSPL_SD_SALE_RETURN_HEAD.SaleReturnAmt,0) as [Sale Return Amt (Rs)] ,isnull(Advance_Payment.Applied_Amount,0) as [Apply Amt (Rs)] ,isnull(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,0)-isnull(Advance_Payment.Applied_Amount,0)-isnull(TSPL_SD_SALE_RETURN_HEAD.SaleReturnAmt ,0) as [Balanced Amt (Rs)]," & _
                 " isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period],Convert(varchar(15),DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),103) as [DUE ON],DATEDIFF(DAY,DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),GETDATE())  AS [Overdue by (no. Of days)] " & _
                 " from TSPL_CUSTOMER_MASTER INNER join TSPL_SD_SALE_INVOICE_HEAD on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                " left outer join (select sum(isnull(Total_Amt,0)) as SaleReturnAmt,Against_Invoice_No from TSPL_SD_SALE_RETURN_HEAD  group by Against_Invoice_No) as TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & _
                 " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                 " left outer join ( select TSPL_RECEIPT_DETAIL.Document_No,isnull(SUM(TSPL_RECEIPT_DETAIL.Applied_Amount),0) AS Applied_Amount from TSPL_RECEIPT_HEADER " & _
                 " left join TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
                 " where TSPL_RECEIPT_HEADER.Posted='Y' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse ,'N') ='N' group by TSPL_RECEIPT_DETAIL.Document_No) as Advance_Payment on Advance_Payment.Document_No=innINVHead.Document_No " & _
                " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=innINVHead.Terms_Code " & _
                " left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
            " WHERE TSPL_SD_SALE_INVOICE_HEAD.Status =1 and (isnull(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,0)-isnull(TSPL_SD_SALE_RETURN_HEAD.SaleReturnAmt ,0))<>0 and CONVERT (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' And CONVERT (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"


            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultLocation.arrValueMember IsNot Nothing AndAlso TxtMultLocation.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(TxtMultLocation.arrValueMember) + ") " + Environment.NewLine
            End If


            StrQuery += Environment.NewLine & " ----------------------------to Misc Sale with its Return( only of Scrap type)------------------------------- " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " select 'Misc' as Trans_Type,innINVHead.Document_No as [AR Invoice No], CONVERT(VARCHAR(15),innINVHead.Document_Date,103) AS [AR Invocie Date] , TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_LOCATION_MASTER.Location_Desc AS [Location],TSPL_SCRAPINVOICE_HEAD.invoice_No as [Invoice No.], CONVERT(VARCHAR(15),TSPL_SCRAPINVOICE_HEAD.shipment_Date ,103) AS [Date],TSPL_SCRAPINVOICE_HEAD.Doc_Amt  as [Invoice Amt (Rs)],isnull(TSPL_SCRAPSALE_HEAD_RETURN.SaleReturnAmt,0) as [Sale Return Amt (Rs)] ,isnull(Advance_Payment.Applied_Amount,0) as [Apply Amt (Rs)] ,isnull(TSPL_SCRAPINVOICE_HEAD.Doc_Amt,0)-isnull(Advance_Payment.Applied_Amount,0)-isnull(TSPL_SCRAPSALE_HEAD_RETURN.SaleReturnAmt ,0) as [Balanced Amt (Rs)], isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period],Convert(varchar(15),DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),103) as [DUE ON],DATEDIFF(DAY,DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),GETDATE())  AS [Overdue by (no. Of days)] from TSPL_CUSTOMER_MASTER INNER join TSPL_SCRAPINVOICE_HEAD on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code  left outer join (select sum(isnull(Doc_Amt ,0)) as SaleReturnAmt,invoice_No  from TSPL_SCRAPSALE_HEAD_RETURN  group by invoice_No) as TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPINVOICE_HEAD.Invoice_No=TSPL_SCRAPSALE_HEAD_RETURN.invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " & Environment.NewLine & _
            " left outer join ( select TSPL_RECEIPT_DETAIL.Document_No,isnull(SUM(TSPL_RECEIPT_DETAIL.Applied_Amount),0) AS Applied_Amount from TSPL_RECEIPT_HEADER  left join TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No where TSPL_RECEIPT_HEADER.Posted='Y' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse ,'N') ='N' group by TSPL_RECEIPT_DETAIL.Document_No) as Advance_Payment on Advance_Payment.Document_No=innINVHead.Document_No " & Environment.NewLine & _
            " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=innINVHead.Terms_Code  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code" & Environment.NewLine & _
            " WHERE TSPL_SCRAPINVOICE_HEAD.ispost =1 and TSPL_SCRAPINVOICE_HEAD.Doc_Type ='S'  and (isnull(TSPL_SCRAPINVOICE_HEAD.Doc_Amt ,0)-isnull(TSPL_SCRAPSALE_HEAD_RETURN.SaleReturnAmt ,0))<>0  and CONVERT (date, TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' And CONVERT (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"

            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and 'Misc' in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_SCRAPINVOICE_HEAD.cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultLocation.arrValueMember IsNot Nothing AndAlso TxtMultLocation.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_SCRAPINVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultLocation.arrValueMember) + ") " + Environment.NewLine
            End If

            StrQuery += Environment.NewLine & " ----------------------------Bulk Sale ------------------------------- " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            "  select 'BS' as [Trans_Type], innINVHead.Document_No as [AR Invoice No], CONVERT(VARCHAR(15),innINVHead.Document_Date,103) AS [AR Invocie Date] ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_LOCATION_MASTER.Location_Desc AS [Location],TSPL_INVOICE_MASTER_BULKSALE.Document_No as [Invoice No.], CONVERT(VARCHAR(15),TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) AS [Date],TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as [Invoice Amt (Rs)],isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.SaleReturnAmt,0) as [Sale Return Amt (Rs)] ,isnull(Advance_Payment.Applied_Amount,0) as [Apply Amt (Rs)] ,isnull(TSPL_INVOICE_MASTER_BULKSALE.Total_Amt,0)-isnull(Advance_Payment.Applied_Amount,0)-isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.SaleReturnAmt ,0) as [Balanced Amt (Rs)], isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period],Convert(varchar(15),DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),103) as [DUE ON],DATEDIFF(DAY,DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),GETDATE())  AS [Overdue by (no. Of days)]  from TSPL_CUSTOMER_MASTER INNER join TSPL_INVOICE_MASTER_BULKSALE on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  left outer join (select sum(isnull(Total_Amt,0)) as SaleReturnAmt,InvoiceNo from TSPL_SALE_RETURN_MASTER_BULKSALE  group by InvoiceNo) as TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No left outer join ( select TSPL_RECEIPT_DETAIL.Document_No,isnull(SUM(TSPL_RECEIPT_DETAIL.Applied_Amount),0) AS Applied_Amount from TSPL_RECEIPT_HEADER  left join TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No where TSPL_RECEIPT_HEADER.Posted='Y' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse ,'N') ='N'  group by TSPL_RECEIPT_DETAIL.Document_No) as Advance_Payment on Advance_Payment.Document_No=innINVHead.Document_No  left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=innINVHead.Terms_Code  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " & Environment.NewLine & _
            " WHERE TSPL_INVOICE_MASTER_BULKSALE.Posted  =1 and (isnull(TSPL_INVOICE_MASTER_BULKSALE.Total_Amt,0)-isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.SaleReturnAmt ,0))<>0 and CONVERT (date, TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) >='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' And CONVERT (date,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) <='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"

            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and 'BS' in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_INVOICE_MASTER_BULKSALE.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultLocation.arrValueMember IsNot Nothing AndAlso TxtMultLocation.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_INVOICE_MASTER_BULKSALE.Location_Code in (" + clsCommon.GetMulcallString(TxtMultLocation.arrValueMember) + ") " + Environment.NewLine
            End If

            StrQuery += Environment.NewLine & " ----------------------------Can Sale ------------------------------- " & Environment.NewLine & _
           " Union All " & Environment.NewLine & _
           "  select 'CS' as [Trans_Type], innINVHead.Document_No as [AR Invoice No], CONVERT(VARCHAR(15),innINVHead.Document_Date,103) AS [AR Invocie Date] ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_LOCATION_MASTER.Location_Desc AS [Location],TSPL_CANSALE_INVOICE_HEAD.Document_No as [Invoice No.], CONVERT(VARCHAR(15),TSPL_CANSALE_INVOICE_HEAD.Document_Date,103) AS [Date],TSPL_CANSALE_INVOICE_HEAD.DocumentAmount as [Invoice Amt (Rs)],0 as [Sale Return Amt (Rs)] ,isnull(Advance_Payment.Applied_Amount,0) as [Apply Amt (Rs)] ,isnull(TSPL_CANSALE_INVOICE_HEAD.DocumentAmount,0)-isnull(Advance_Payment.Applied_Amount,0) as [Balanced Amt (Rs)], isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period],Convert(varchar(15),DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),103) as [DUE ON],DATEDIFF(DAY,DATEADD(DAY,isnull(TSPL_TERMS_MASTER.No_Days,0),innINVHead.Document_Date),GETDATE())  AS [Overdue by (no. Of days)]  from TSPL_CUSTOMER_MASTER INNER join TSPL_CANSALE_INVOICE_HEAD on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CANSALE_INVOICE_HEAD.Customer_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_CANSALE_INVOICE_HEAD.Document_No left outer join ( select TSPL_RECEIPT_DETAIL.Document_No,isnull(SUM(TSPL_RECEIPT_DETAIL.Applied_Amount),0) AS Applied_Amount from TSPL_RECEIPT_HEADER  left join TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No  where TSPL_RECEIPT_HEADER.Posted='Y' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse ,'N') ='N' group by TSPL_RECEIPT_DETAIL.Document_No) as Advance_Payment on Advance_Payment.Document_No=innINVHead.Document_No  left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=innINVHead.Terms_Code  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_CANSALE_INVOICE_HEAD.Location_Code " & Environment.NewLine & _
           " WHERE TSPL_CANSALE_INVOICE_HEAD.Posted  =1 and CONVERT (date, TSPL_CANSALE_INVOICE_HEAD.Document_Date,103) >='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' And CONVERT (date,TSPL_CANSALE_INVOICE_HEAD.Document_Date,103) <='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"

            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and 'CS' in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_CANSALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultLocation.arrValueMember IsNot Nothing AndAlso TxtMultLocation.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_CANSALE_INVOICE_HEAD.Location_Code in (" + clsCommon.GetMulcallString(TxtMultLocation.arrValueMember) + ") " + Environment.NewLine
            End If

            StrQuery += "  ) Final order by convert(date, Final.[Date],103) "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.AutoExpandGroups = True
                gv1.ShowGroupPanel = False
                gv1.ShowRowHeaderColumn = False
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.ReadOnly = True
                gv1.BestFitColumns()
                FormatGrid()

            End If
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
       
        End Try
       
    End Sub
    Private Sub FormatGrid()
        gv1.AllowAddNewRow = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        gv1.Columns("Trans_Type").IsVisible = False

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim ColumnTotal As New GridViewSummaryItem("Invoice Amt (Rs)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("Apply Amt (Rs)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("Balanced Amt (Rs)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData(0)

            Dim arrHeader As List(Of String) = New List(Of String)()
            '  arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Customer Group Outstanding", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Customer Group Outstanding", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCustomerGroupOutstanding & "'"))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCustomerGroupOutstanding & "'"))

            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If TxtMultLocation.arrDispalyMember IsNot Nothing AndAlso TxtMultLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultLocation.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        TxtMultLocation.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        RadPageViewPage2.Text = "Report"

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)

        txtTransaction.Enabled = val

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub
    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click

        Dim qry As String = " select 'FS'''+',''PS'''+',''DS' as Code,'Sale' as Name " & Environment.NewLine & _
       " UNION ALL" & Environment.NewLine & _
" select 'BS' as Code,'Bulk Sale' as Name " & Environment.NewLine & _
" UNION ALL" & Environment.NewLine & _
" select 'CS' as Code,'Can Sale' as Name" & Environment.NewLine & _
" UNION ALL" & Environment.NewLine & _
" select 'MCC' as Code,'MCC Sale' as Name " & Environment.NewLine & _
" UNION ALL" & Environment.NewLine & _
" select 'Misc' as Code,'Misc Sale' as Name" & Environment.NewLine
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs)
        Try
            LoadData(2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Shared Sub ExportBulkData(ByVal qry As String, ByVal arrVisibleColumAndCaption As Dictionary(Of String, String), ByVal strReportNameInSaveDialog As String)
        'clsCommon.ProgressBarPercentShow()
        clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            If arrVisibleColumAndCaption Is Nothing OrElse arrVisibleColumAndCaption.Count <= 0 Then
                Throw New Exception("Please provice column and caption for export")
            End If

            Dim rowsPerSheet As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QuickExport, clsFixedParameterCode.MaxRowsForQuickExport, Nothing))

            Dim FilePath As String = "C:\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(FilePath)
            If Not IsExists Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If
            strReportNameInSaveDialog += clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss")
            FilePath = "C:\ERPTempFolder\" + strReportNameInSaveDialog.Replace("/", "_").Replace("\\", "_") + ".xlsx"

            Dim intTotalRows As Integer = 0
            Dim intSheetCounter As Integer = 1
            Dim intReaderCounter As Integer = 0
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            Dim ResultsData As DataTable = Nothing
            Dim c As Integer = 0
            Dim firstTime As Boolean = True

            'Get the Columns names, types, this will help when we need to format the cells in the excel sheet.
            Dim dtSchema As DataTable = reader.GetSchemaTable()
            'Dim listCols = New List(Of DataColumn)()

            If dtSchema IsNot Nothing Then
                ResultsData = New DataTable()
                For Each drow As DataRow In dtSchema.Rows
                    Dim columnName As String = clsCommon.myCstr(drow("ColumnName"))
                    If arrVisibleColumAndCaption.ContainsKey(columnName) Then
                        Dim column = New DataColumn(columnName, DirectCast(drow("DataType"), Type))
                        column.Unique = CBool(drow("IsUnique"))
                        column.AllowDBNull = CBool(drow("AllowDBNull"))
                        column.AutoIncrement = CBool(drow("IsAutoIncrement"))
                        column.Caption = arrVisibleColumAndCaption(columnName)
                        'listCols.Add(column)
                        ResultsData.Columns.Add(column)
                    End If
                Next
            End If
            Dim rowData(rowsPerSheet, ResultsData.Columns.Count) As Object
            Dim workBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

            While reader.Read()
                intReaderCounter += 1
                clsCommon.ProgressBarUpdate("Fatching Data for Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                For i As Integer = 0 To ResultsData.Columns.Count - 1
                    rowData(c, i) = reader(ResultsData.Columns(i).ColumnName)
                Next
                c += 1
                If c = rowsPerSheet Then
                    clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                    workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
                    c = 0
                    ResultsData.Clear()
                    firstTime = False
                    intSheetCounter += 1
                End If
            End While

            If c <> 0 Then

                clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
            End If

            workBook = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If intReaderCounter > 0 Then
                clsCommon.ProgressBarUpdate("Data exported.Opening File " + FilePath + ".Please wait...")
                Process.Start(FilePath)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Shared Function ExportToOxml(ByVal intSheetNo As Integer, ByVal firstTime As Boolean, ByVal RowsToWrite As Integer, ByVal Schema As DataTable, ByVal rawData(,) As Object, ByVal FilePath As String, ByRef wbook As Microsoft.Office.Interop.Excel.Workbook) As Microsoft.Office.Interop.Excel.Workbook
        Try
            Dim dt As New System.Data.DataTable()
            For i As Integer = 0 To Schema.Columns.Count - 1
                dt.Columns.Add("Column" & (i + 1))
                If clsCommon.myLen(Schema.Columns(i).Caption) > 0 Then
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).Caption
                Else
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).ColumnName
                End If
            Next

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            If wbook Is Nothing Then
                Dim wBook1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
                wbook = wBook1
                wbook = excel.Workbooks.Add()
            Else
                wbook = excel.Workbooks.Open(FilePath)
            End If
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim GridCurrentRowIndex As Int64 = -1
            Dim GridLastSavedRowIndex As Int64 = -1
            wSheet = wbook.Sheets.Add(, , 1)
            wbook.ActiveSheet.Move(After:=wbook.Sheets(wbook.Sheets.Count))
            If firstTime Then
                Try
                    CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet2"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet3"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                Catch ex As Exception
                End Try
            End If
            wSheet.Name = "Sheet" & intSheetNo

            Dim jk As Integer = 0
            For i As Integer = 0 To Schema.Columns.Count - 1
                jk += 1
                Dim MyType As TypeCode = Type.GetTypeCode(Schema.Columns(i).DataType)
                If MyType = TypeCode.String Then
                    wSheet.Range(ColumnIndexToColumnLetter(jk) & ":" & ColumnIndexToColumnLetter(jk)).Cells.NumberFormat = "@"
                End If
            Next

            Dim colnum As Integer = -1
            Dim PrevCol As Integer = -1
            Dim ColNums(0 To Schema.Columns.Count - 1) As Integer

            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            Dim dc As System.Data.DataColumn
            colIndex = 0
            For Each dc In Schema.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex, colIndex) = dc.Caption
            Next

            Dim LastColumn As String = ColumnIndexToColumnLetter(Schema.Columns.Count)
            Dim Lastrow As Integer = RowsToWrite

            Dim row As Integer = 0
            Dim col As Integer = 0

            wSheet.Range("A2", LastColumn & (Lastrow + 1)).Value2 = rawData
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            wSheet.Columns.AutoFit()
            CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Select()
            excel.DisplayAlerts = False
            wbook.SaveAs(FilePath, , , , , , Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive)
            wbook.Close(True)

            excel.Quit()
            excel = Nothing
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return wbook
    End Function

    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function


    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
        'clsCommon.ProgressBarPercentShow()
        'clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            If reader Is Nothing OrElse Not reader.HasRows Then
                Throw New Exception("No Data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dtTest As New DataTable
            dtTest.Load(reader)
            gv1.DataSource = dtTest
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            gv1.EnableFiltering = True
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultLocation._My_Click
        Dim qry As String = " select Location_Code as [Code], Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub RadMenuItemSett1_Click(sender As Object, e As EventArgs) Handles RadMenuItemSett1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
End Class
