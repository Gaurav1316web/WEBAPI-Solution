Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptCustomerSetOff
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Const colDocType As String = "DocType"
    Const colSINo As String = "SaleInvoice"
    Const colDocNo As String = "DocNo"
    Const colDocDate As String = "DocDate"
    Const colVendorInvNo As String = "VendorInvNo"
    Const colFilledTotal As String = "FilledTotal"
    Const colEmptyTotal As String = "EmptyTotal"
    Const colOrgnlAmt As String = "OrgnlAmount"
    Const colBalAmt As String = "BalAmt"
    Const colTemp As String = "TempAmt"
    Const colTemp1 As String = "TempAmt1"
    Const colAppliedAmt As String = "AppliedAmt"
    Const colTDSAmt As String = "TDSAmt"
    Const colAdjNo As String = "AdjNo"
    Const colAdjAmt As String = "AdjAmt"


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptVendorSecurity)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport

    End Sub

    Sub LoadCustomer()
        Dim qry As String = " Select * from ( Select distinct Cust_Code as Code, Customer_Name as Name from  (select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  ,Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date,  (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo, (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt,   EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code  from ( select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," &
         " (TSPL_Customer_Invoice_Head.Document_Total  " &
         "  -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " &
         " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " &
         " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " &
         " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " &
         " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " &
         " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " &
         " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " &
         "  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " &
         " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " &
         " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " &
         " , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1   order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and Balance_Amt>0  and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" &
         " ) XXX WHERE [Balance Amount]>0 )Final "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            qry += " where Final.Code in (" + objCommonVar.strCurrUserCustomers + ")"
        End If
        qry += " order by Final.Code "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"

    End Sub
    Sub LoadBlankGrid()

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False


        Dim docType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docType.FormatString = ""
        docType.HeaderText = "Document Type"
        docType.Name = colDocType
        docType.Width = 100
        docType.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(docType)

        Dim SiNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SiNo.FormatString = ""
        SiNo.HeaderText = "Document Invoice No"
        SiNo.Name = colSINo
        SiNo.Width = 100
        SiNo.ReadOnly = True
        SiNo.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(SiNo)

        Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        docNo.HeaderText = "Document No"
        docNo.Name = colDocNo
        docNo.Width = 150
        docNo.ReadOnly = True
        docNo.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(docNo)

        Dim docDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docDate.FormatString = ""
        docDate.HeaderText = "Document Date"
        docDate.Name = colDocDate
        docDate.Width = 150
        docDate.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(docDate)

        Dim FilledTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
        FilledTotal.FormatString = ""
        FilledTotal.HeaderText = "Filled"
        FilledTotal.Name = colFilledTotal
        FilledTotal.Width = 70
        FilledTotal.ReadOnly = True
        FilledTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(FilledTotal)


        Dim originalInvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        originalInvAmt.FormatString = ""
        originalInvAmt.HeaderText = "Original Amount"
        originalInvAmt.Name = colOrgnlAmt
        originalInvAmt.Width = 100
        originalInvAmt.ReadOnly = True
        originalInvAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(originalInvAmt)

        Dim BalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        BalAmt.FormatString = ""
        BalAmt.DecimalPlaces = 2
        BalAmt.HeaderText = "Balance Amt"
        BalAmt.Name = colBalAmt
        BalAmt.Width = 100
        BalAmt.ReadOnly = True
        BalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(BalAmt)

        Dim appliedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        appliedAmt.FormatString = ""
        appliedAmt.DecimalPlaces = 2
        appliedAmt.HeaderText = "Applied Amount"
        appliedAmt.Name = colAppliedAmt
        appliedAmt.Width = 100
        appliedAmt.ReadOnly = False
        appliedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(appliedAmt)

        Dim tdsAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        tdsAmt.FormatString = ""
        tdsAmt.HeaderText = "TDS Amount"
        tdsAmt.Name = colTDSAmt
        tdsAmt.Width = 100
        tdsAmt.ReadOnly = True
        tdsAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(tdsAmt)

        Dim adjNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        adjNo.FormatString = ""
        adjNo.HeaderText = "Adjustment No"
        adjNo.Width = 100
        adjNo.Name = colAdjNo
        adjNo.ReadOnly = True
        adjNo.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(adjNo)

        Dim adjAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        adjAmt.FormatString = ""
        adjAmt.HeaderText = "Adjustment Amt"
        adjAmt.Name = colAdjAmt
        adjAmt.Width = 100
        adjAmt.ReadOnly = True
        adjAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(adjAmt)

        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False

    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        LoadBlankGrid()
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            fromDate.Focus()
            Exit Sub
        End If

        If ChkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Customer or select all.", Me.Text)
            Exit Sub
        End If


        Dim qry As String = ""

        Dim dtgv As New DataTable
        qry = " select convert(varchar,TSPL_RECEIPT_HEADER.Created_Date,103) as Date,TSPL_RECEIPT_HEADER.Receipt_No,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Date,103) as Receipt_Date,TSPL_RECEIPT_DETAIL.Document_No,convert(varchar,TSPL_RECEIPT_DETAIL.Document_Date,103) as Document_Date,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_RECEIPT_HEADER.Customer_Name,TSPL_RECEIPT_HEADER.Receipt_Type,TSPL_RECEIPT_DETAIL.Original_Amt,TSPL_RECEIPT_DETAIL.Pending_Balance,TSPL_RECEIPT_DETAIL.Applied_Amount,TSPL_RECEIPT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION,TSPL_RECEIPT_HEADER.Entry_Desc from TSPL_RECEIPT_HEADER "
        qry += " left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No"
        qry += " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code"
        qry += " where 2=2 and TSPL_RECEIPT_HEADER.IsApplyDocAuto=1 "
        If ChkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
            qry += " and TSPL_RECEIPT_HEADER.Cust_Code  IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
        ElseIf objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            qry += " and TSPL_RECEIPT_HEADER.Cust_Code  IN (" + objCommonVar.strCurrUserCustomers + ") "
        End If
        qry += " and convert(date,TSPL_RECEIPT_HEADER.Created_Date,103)>=convert(date,'" & fromDate.Value & "',103) and convert(date,TSPL_RECEIPT_HEADER.Created_Date,103)<=convert(date,'" & ToDate.Value & "',103) "


        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv

            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGridDetails()

            RadPageView1.SelectedPage = RadPageViewPage2

            Gv1.MasterTemplate.AllowAddNewRow = False
            Gv1.BestFitColumns()

        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub
    Sub FormatGridDetails()


        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        'Gv1.Columns("Entry_Desc").IsVisible = True
        'Gv1.Columns("Entry_Desc").Width = 30
        'Gv1.Columns("Entry_Desc").HeaderText = "Receipt Description"

        Gv1.Columns("Date").IsVisible = True
        Gv1.Columns("Date").Width = 30
        Gv1.Columns("Date").HeaderText = "Date"

        Gv1.Columns("Receipt_No").IsVisible = True
        Gv1.Columns("Receipt_No").Width = 100
        Gv1.Columns("Receipt_No").HeaderText = "Receipt No"

        Gv1.Columns("Receipt_Date").IsVisible = True
        Gv1.Columns("Receipt_Date").Width = 100
        Gv1.Columns("Receipt_Date").HeaderText = "Receipt Date"

        Gv1.Columns("Entry_Desc").IsVisible = False
        Gv1.Columns("Entry_Desc").Width = 30
        Gv1.Columns("Entry_Desc").HeaderText = "Receipt Description"

        Gv1.Columns("Document_No").IsVisible = True
        Gv1.Columns("Document_No").Width = 100
        Gv1.Columns("Document_No").HeaderText = "Doc No"

        Gv1.Columns("Document_Date").IsVisible = True
        Gv1.Columns("Document_Date").Width = 100
        Gv1.Columns("Document_Date").HeaderText = "Doc Date"

        Gv1.Columns("Cust_Code").IsVisible = True
        Gv1.Columns("Cust_Code").Width = 100
        Gv1.Columns("Cust_Code").HeaderText = "Customer Code"

        Gv1.Columns("Customer_Name").IsVisible = True
        Gv1.Columns("Customer_Name").Width = 100
        Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        Gv1.Columns("Receipt_Type").IsVisible = True
        Gv1.Columns("Receipt_Type").Width = 100
        Gv1.Columns("Receipt_Type").HeaderText = "Receipt Type"


        Gv1.Columns("Original_Amt").IsVisible = True
        Gv1.Columns("Original_Amt").Width = 200
        Gv1.Columns("Original_Amt").HeaderText = "Original Amt"

        Gv1.Columns("Pending_Balance").IsVisible = True
        Gv1.Columns("Pending_Balance").Width = 100
        Gv1.Columns("Pending_Balance").HeaderText = "Pending Balance"

        Gv1.Columns("Applied_Amount").IsVisible = True
        Gv1.Columns("Applied_Amount").Width = 150
        Gv1.Columns("Applied_Amount").HeaderText = "Applied Amount"

        Gv1.Columns("Bank_Code").IsVisible = True
        Gv1.Columns("Bank_Code").Width = 100
        Gv1.Columns("Bank_Code").HeaderText = "Bank Code"


        Gv1.Columns("DESCRIPTION").IsVisible = True
        Gv1.Columns("DESCRIPTION").Width = 100
        Gv1.Columns("DESCRIPTION").HeaderText = "DESCRIPTION"

      
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Original_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Applied_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Pending_Balance", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ReStoreGridLayout()


    End Sub
   
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadCustomer()
        ChkVendorAll.CheckState = CheckState.Checked
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click

        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub FrmVendorSecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCustomer()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N ")
        Reset()
    End Sub

    Private Sub FrmVendorSecurity_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ChkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not ChkVendorAll.IsChecked
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptCustomersSetOff & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkVendorSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Customer: " + strLocationName + " "))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Customer Set Off Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer Set Off Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class