Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'========================shivani===============================
Public Class RptCashAgainstDocs
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptCashAgainstDocs)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnGo.Visible = MyBase.isModifyFlag
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub TxtMultiSellerFinder1__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSellerFinder1._My_Click
        Dim qry As String = "select Vendor_Code as [Code],Vendor_Name as [Name] from tspl_vendor_master where tspl_vendor_master.Status ='N'and  TSPL_VENDOR_MASTER.CURRENCY_CODE<>(select isnull(BaseCurrencyCode,'')  from TSPL_COMPANY_MASTER where Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' )"
        TxtMultiSellerFinder1.arrValueMember = clsCommon.ShowMultipleSelectForm("Vendor", qry, "Code", "Name", TxtMultiSellerFinder1.arrValueMember, TxtMultiSellerFinder1.arrDispalyMember)
    End Sub

    Private Sub TxtMultiBuyerFinder2__My_Click(sender As Object, e As EventArgs) Handles TxtMultiBuyerFinder2._My_Click
        Dim qry As String = "select Cust_Code as [Code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        TxtMultiBuyerFinder2.arrValueMember = clsCommon.ShowMultipleSelectForm("Customer", qry, "Code", "Name", TxtMultiBuyerFinder2.arrValueMember, TxtMultiBuyerFinder2.arrDispalyMember)
    End Sub

    Private Sub TxtMultiBankFinder3__My_Click(sender As Object, e As EventArgs) Handles TxtMultiBankFinder3._My_Click
        Dim qry As String = " select distinct tspl_bank_master.BANK_CODE as [Code] ,tspl_bank_master.DESCRIPTION as [Name]  from tspl_bank_master where BANK_CODE<>''"
        TxtMultiBankFinder3.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", TxtMultiBankFinder3.arrValueMember, TxtMultiBankFinder3.arrDispalyMember)

    End Sub

    Private Sub TxtMultiLocationFinder4__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocationFinder4._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Type ='Virtual'"
        TxtMultiLocationFinder4.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", TxtMultiLocationFinder4.arrValueMember, TxtMultiLocationFinder4.arrDispalyMember)
    End Sub
    Public Sub Load_Report()
        Try

            Dim sQuery As String

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            sQuery = "select 1 as [S No.],isnull(TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,'') as [Seller Code], isnull(TSPL_VENDOR_MASTER.Vendor_Name,'') as [Seller Name],isnull (TSPL_VENDOR_MASTER.Country,'') as [Seller Country] ,isnull(TSPL_PURCHASE_ORDER_HEAD.BANK_CODE,'') as [Bank Code], isnull(TSPL_BANK_MASTER.Description,'') as [Bank Name], isnull(TSPL_PURCHASE_ORDER_HEAD.Payment_Code,'') as [Account Type], isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_Po_No,'') as [Corres. LC/PI No.], convert(varchar,isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_Po_Date,''),103) as [LC/PI Date], isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') as [PO No], convert(varchar,isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,''),103) as [PO Date],isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0) as [PO Qty], isnull(TSPL_PURCHASE_ORDER_DETAIL.Unit_Code,'') as [Unit], isnull(TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,0) as [Unit Cost], isnull(TSPL_PURCHASE_ORDER_HEAD.Po_Total_Amt,0) as [Total Value], isnull(TSPL_PURCHASE_ORDER_DETAIL.Item_Code,'') as [Item Code], isnull(TSPL_ITEM_MASTER.Item_Desc,'') as [Item Description], isnull(TSPL_PAYMENT_DETAIL.Applied_Amount,0) as [Amount Paid], case when isnull(TSPL_PAYMENT_DETAIL.Reverse_Code,'')<>'' then 0 ELSE (isnull(TSPL_PURCHASE_ORDER_HEAD.Po_Total_Amt,0)-isnull(TSPL_PAYMENT_DETAIL.Applied_Amount,0)) END as [Balance Outstanding],case when isnull(TSPL_PAYMENT_DETAIL.Reverse_Code,'')='' then '' else 'Payment Reverse' end as [Payment Status] ,isnull(TSPL_PAYMENT_DETAIL.Bank_Charges,0) as [Bank Charges], isnull(TSPL_EX_PI_HEAD.Ref_No,'') as [Reference No], isnull(TSPL_PAYMENT_DETAIL.ConvRate,1) as [Conversion Rate],convert(decimal(18,2),(isnull(TSPL_PURCHASE_ORDER_HEAD.Po_Total_Amt,0)*isnull(TSPL_PAYMENT_DETAIL.ConvRate,0))) as [Amount in (INR)], isnull(ladingno.Value,'') as [Bill of Lading No], convert(varchar,isnull(ladingdate.Value,''),103) as [Bill of Lading Date], isnull(TSPL_EX_PI_HEAD.Airway_Line,'') as [Shipping/Airway Line], isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.Loading_Port,'') as [Port of Loading], isnull(TSPL_EX_PI_HEAD.Discharge_Port,'') as [Port of Discharge], isnull(TSPL_EX_PI_HEAD.Customer_Code,'') as [Buyer Code], isnull(TSPL_CUSTOMER_MASTER.Customer_Name,'') as [Buyer Name],isnull(TSPL_CUSTOMER_MASTER.Country,'') as [Buyer Country], isnull(TSPL_EX_PI_HEAD.Document_Code,'') as [Proforma Invoice No.], convert(varchar,TSPL_EX_PI_HEAD.Document_Date,103) as [Proforma Invoice Date], isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code,'') as [Commercial Invoice No.], convert(varchar,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,103) as [Commercial Invoice Date], isnull(TSPL_EX_PI_DETAIL.Qty,0) as [Inv Qty], isnull(TSPL_EX_PI_DETAIL.Unit_Code,'') as [Inv Unit], isnull(TSPL_EX_PI_DETAIL.Item_Cost,0) as [Inv Unit Cost], isnull(TSPL_EX_PI_HEAD.Total_Amt,0) as [Inv Total Value], isnull(TSPL_EX_PI_HEAD.BANK_CODE,'') as [Inv Bank Code], isnull(BM.Description,'') as [Inv Bank Name], convert(varchar,TSPL_EX_PI_HEAD.Due_Date,103) as [Due Date], isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.Ref_No,'') as [Inv Ref No], isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) as [Received Amount], convert(varchar,TSPL_RECEIPT_DETAIL.Receipt_Date,103) as [Received Date], case when isnull(TSPL_RECEIPT_DETAIL.Reverse_Code,'')<>'' then 0 else (isnull(TSPL_EX_PI_HEAD.Total_Amt,0)-isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0)) end as [Balance Remaining], isnull(TSPL_RECEIPT_DETAIL.Bank_Charges,0) as [Collection Charges],case when isnull(TSPL_RECEIPT_DETAIL.Reverse_Code,'')='' then '' else 'Receipt Reverse' end as [Receipt Status] from TSPL_PURCHASE_ORDER_HEAD" & _
                    " LEFT OUTER JOIN TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No" & _
                    " LEFT OUTER JOIN TSPL_EX_PI_HEAD on case when isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_PO_No,'')='' then TSPL_EX_PI_HEAD.MT_AGAINST_PO else TSPL_EX_PI_HEAD.Cust_Po_No end = case when isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_PO_No,'')='' then TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No else TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_PO_No end" & _
                    " LEFT OUTER JOIN TSPL_EX_PI_DETAIL on TSPL_EX_PI_DETAIL.Document_Code = TSPL_EX_PI_HEAD.Document_Code AND TSPL_EX_PI_DETAIL.Item_Code = TSPL_PURCHASE_ORDER_DETAIL.ITEM_CODE" & _
                    " LEFT OUTER JOIN TSPL_EX_COMMERCIAL_INVOICE_HEAD on case when isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_PO_No,'')='' then TSPL_EX_COMMERCIAL_INVOICE_HEAD.MT_AGAINST_PO else TSPL_EX_COMMERCIAL_INVOICE_HEAD.Cust_Po_No end = case when isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_PO_No,'')='' then TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No else TSPL_PURCHASE_ORDER_HEAD.MT_Buyer_PO_No end" & _
                    " LEFT OUTER JOIN TSPL_SRN_HEAD on TSPL_SRN_HEAD.AGAINST_PO = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No" & _
                    " LEFT OUTER JOIN TSPL_PI_HEAD on TSPL_PI_HEAD.AGAINST_SRN = TSPL_SRN_HEAD.SRN_No" & _
                    " LEFT OUTER JOIN TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_CODE = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code" & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_EX_PI_HEAD.Customer_Code" & _
                    " LEFT OUTER JOIN TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = TSPL_PURCHASE_ORDER_HEAD.BANK_CODE" & _
                    " LEFT OUTER JOIN TSPL_BANK_MASTER BM on BM.BANK_CODE = TSPL_EX_PI_HEAD.BANK_CODE" & _
                    " LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PURCHASE_ORDER_DETAIL.ITEM_CODE" & _
                    " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_NO" & _
                    " LEFT OUTER JOIN (SELECT TSPL_PAYMENT_DETAIL.Document_No,sum(ISNULL(TSPL_PAYMENT_DETAIL.Applied_Amount,0)) as Applied_Amount,max(ISNULL(TSPL_PAYMENT_HEADER.Bank_Charges,0)) as Bank_Charges,max(ISNULL(TSPL_PAYMENT_HEADER.ConvRate,1)) as ConvRate,MAX(TSPL_BANK_REVERSE.Reverse_Code) as Reverse_Code from  TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No LEFT OUTER JOIN TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No  group by TSPL_PAYMENT_DETAIL.Document_No )as TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No" & _
                    " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Com_Inv_No = TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code" & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_INVOICE_HEAD on TSPL_CUSTOMER_INVOICE_HEAD.AGAINST_SALE_NO = TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
                    " LEFT OUTER JOIN (SELECT TSPL_RECEIPT_DETAIL.Document_No,sum(ISNULL(TSPL_RECEIPT_DETAIL.Applied_Amount,0)) as Applied_Amount,max(ISNULL(TSPL_RECEIPT_HEADER.Bank_Charges_Amt,0)) as Bank_Charges,max(ISNULL(TSPL_RECEIPT_HEADER.Receipt_Date,'')) as Receipt_Date,MAX(TSPL_BANK_REVERSE.Reverse_Code) as Reverse_Code from  TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No LEFT OUTER JOIN TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No group by TSPL_RECEIPT_DETAIL.Document_No )as TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No=TSPL_CUSTOMER_INVOICE_HEAD.Document_No" & _
                    " LEFT OUTER JOIN TSPL_CUSTOM_FIELD_VALUES ladingno on ladingno.Transaction_Code = TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code and ladingno.Program_Code='" + clsUserMgtCode.frmCommercialInvoiceMT + "' and ladingno.Custom_Field_Code='CF00000030' " & _
                    " LEFT OUTER JOIN TSPL_CUSTOM_FIELD_VALUES ladingdate on ladingdate.Transaction_Code = TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code and ladingdate.Program_Code='" + clsUserMgtCode.frmCommercialInvoiceMT + "' and ladingdate.Custom_Field_Code='CF00000033'" & _
                    " where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=1 and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <=convert(date,'" & txtToDate.Value & "' ,103) and  MT_Is_Merchant_Trade =1 "
            If TxtMultiSellerFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSellerFinder1.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiSellerFinder1.arrValueMember) + ") "
            End If
            If TxtMultiBuyerFinder2.arrValueMember IsNot Nothing AndAlso TxtMultiBuyerFinder2.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_EX_PI_HEAD.Customer_Code in  (" + clsCommon.GetMulcallString(TxtMultiBuyerFinder2.arrValueMember) + ") "
            End If
            If TxtMultiLocationFinder4.arrValueMember IsNot Nothing AndAlso TxtMultiBuyerFinder2.arrValueMember.Count > 0 Then
                sQuery += " TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in   (" + clsCommon.GetMulcallString(TxtMultiBuyerFinder2.arrValueMember) + ") "
            End If
            'If TxtMultiBankFinder3.arrValueMember IsNot Nothing AndAlso TxtMultiBankFinder3.arrValueMember.Count > 0 Then
            '    sQuery += " and TSPL_PAYMENT_header.Bank_Code in   (" + clsCommon.GetMulcallString(TxtMultiBankFinder3.arrValueMember) + ") "
            'End If
            Dim SemiQry As String = sQuery

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(SemiQry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True
                FormatGrid()
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
            ReStoreGridLayout()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        For ii As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(ii).Cells(0).Value = ii + 1
        Next
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.DataSource = Nothing
    End Sub
    
    Private Sub RptCashAgainstDocs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptCashAgainstDocs & "'"))
            'If TxtMultiBankFinder3.arrValueMember IsNot Nothing AndAlso TxtMultiBankFinder3.arrValueMember.Count > 0 Then
            '    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(TxtMultiBankFinder3.arrValueMember)
            '    arrHeader.Add((" Bank : " + strLocationName + " "))
            'Else
            '    arrHeader.Add((" Bank : All"))
            'End If
            If TxtMultiBuyerFinder2.arrValueMember IsNot Nothing AndAlso TxtMultiBuyerFinder2.arrValueMember.Count > 0 Then
                arrHeader.Add("Buyer : " + clsCommon.GetMulcallStringWithComma(TxtMultiBuyerFinder2.arrValueMember))
            Else
                arrHeader.Add(("Buyer: All"))
            End If
            If TxtMultiLocationFinder4.arrValueMember IsNot Nothing AndAlso TxtMultiLocationFinder4.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocationFinder4.arrValueMember))
            Else
                arrHeader.Add(("Location : All"))
            End If
            If TxtMultiSellerFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSellerFinder1.arrValueMember.Count > 0 Then
                arrHeader.Add("Seller : " + clsCommon.GetMulcallStringWithComma(TxtMultiSellerFinder1.arrValueMember))
            Else
                arrHeader.Add(("Seller : All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Cash Against Docs", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Cash Against Docs", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click

    End Sub

    'Private Sub RadSplitButton1_Click(sender As Object, e As EventArgs) Handles RadSplitButton1.Click
    '    print(EnumExportTo.Excel)
    'End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)

        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptCashAgainstDocs & "'"))

                If TxtMultiBankFinder3.arrValueMember IsNot Nothing AndAlso TxtMultiBankFinder3.arrValueMember.Count > 0 Then
                    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(TxtMultiBankFinder3.arrValueMember)
                    arrHeader.Add((" Bank : " + strLocationName + " "))
                Else
                    arrHeader.Add((" Bank : All"))
                End If
                If TxtMultiBuyerFinder2.arrValueMember IsNot Nothing AndAlso TxtMultiBuyerFinder2.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Buyer : " + clsCommon.GetMulcallStringWithComma(TxtMultiBuyerFinder2.arrValueMember))
                Else
                    arrHeader.Add((" Buyer: All"))
                End If
                If TxtMultiLocationFinder4.arrValueMember IsNot Nothing AndAlso TxtMultiLocationFinder4.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocationFinder4.arrValueMember))
                Else
                    arrHeader.Add(("Location : All"))
                End If
                If TxtMultiSellerFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSellerFinder1.arrValueMember.Count > 0 Then
                    arrHeader.Add("Seller : " + clsCommon.GetMulcallStringWithComma(TxtMultiSellerFinder1.arrValueMember))
                Else
                    arrHeader.Add(("Seller : All"))
                End If
                If exporter = EnumExportTo.Excel Then
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
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Cash Against Docs", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        'print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        ExportGrid(EnumExportTo.PDF)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
