Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class frmVendorGroupWiseSaleReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVendorGroupWiseSaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Public Sub loadCustomerCode()
        Dim qry11 As String = "SELECT  Cust_Code,Customer_Name FROM TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub
    Private Sub CustomerBillWiseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadCustomerCode()
        LoadVendorCode()
        LoadBills()
        reset()
    End Sub
    Sub LoadVendorCode()
        Dim qry As String = "select Vendor_Code , Vendor_Name  from TSPL_VENDOR_MASTER"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub
    Sub LoadBills()

            
        Dim strDateRange As String = " where  CONVERT(DATE, TSPL_SD_SALE_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103) "
        Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [DocumentCode],TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [CustomerCode],tspl_customer_master.Customer_Name as [CustomerName]  from TSPL_SD_SALE_INVOICE_HEAD  left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code "
        qry += strDateRange
        cbgBills.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBills.ValueMember = "DocumentCode"
        cbgBills.DisplayMember = "DocumentCode"
        cbgBills.CheckedAll()
        cbgBills.Enabled = False

    End Sub
    Private Sub reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkChapterAll.IsChecked = True
        MyRadioButton4.IsChecked = True
        MyRadioButton2.IsChecked = True
        cbgCustomer.CheckedAll()
        cbgVendor.CheckedAll()
        cbgBills.CheckedAll()
        cbgCustomer.Enabled = False
        cbgVendor.Enabled = False
        cbgBills.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Columns.Clear()

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strVendor As String = ""
        Dim strBills As String = ""
        Dim strDateRange As String = ""
        Dim strdtrng As String = ""

        Try
            strdtrng = "'" & Format(dtpFrmDate.Value, "dd/MM/yyyy") & " TO " & Format(dtpToDate.Value, "dd/MM/yyyy") & "'"
            qry = "select " & strdtrng & " as Dt_Range, tspl_company_master.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_SD_SALE_INVOICE_HEAD.Document_Code ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,case when TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='T' then 'TAX INVOICE' else case when TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='R' then 'RETAIL INVOICE' else TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type end end as 'Invoice_Type' ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,convert(varchar,convert(integer,TSPL_SD_SALE_INVOICE_DETAIL.Qty))+ ' ' + TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as 'QTY' ,convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost) + '/' + TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as 'Item_Cost',TSPL_SD_SALE_INVOICE_DETAIL.Amount ,TSPL_VENDOR_ITEM_DETAIL.vendor_code,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code   left outer join TSPL_VENDOR_ITEM_DETAIL on TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_ITEM_DETAIL.vendor_code   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code    "

            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALE_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If cbgVendor.CheckedValue.Count > 0 Then
                strVendor += " and TSPL_VENDOR_ITEM_DETAIL.Vendor_Code in  (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
            Else
                strVendor = ""

            End If

            If cbgBills.CheckedValue.Count > 0 Then
                strBills += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in  (" + clsCommon.GetMulcallString(cbgBills.CheckedValue) + ") "
            Else
                strBills = ""

            End If
            strDateRange = " where  CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strVendor
            qry += strCustomer
            qry += strBills
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptVendorGroupWiseSale", "Vendor Group Wise Sale Report")
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgVendor.CheckedDisplayMember.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please Select At least One Vendor")
            cbgVendor.Focus()
            Exit Sub
        End If


        If cbgBills.CheckedDisplayMember.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please Select At least One Bill !!! " + Environment.NewLine + "There May be no bills in selected Date Range")
            cbgBills.Focus()
            Exit Sub
        End If
        print()


    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub RadPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub chkChapterSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterSelect.ToggleStateChanged
        If chkChapterSelect.IsChecked() Then
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        Else
            cbgCustomer.UnCheckedAll()
            cbgCustomer.Enabled = False
        End If

    End Sub

    Private Sub MyRadioButton3_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton3.ToggleStateChanged
        If MyRadioButton3.IsChecked() Then
            cbgVendor.UnCheckedAll()
            cbgVendor.Enabled = True
        Else
            cbgVendor.UnCheckedAll()
            cbgVendor.Enabled = False
        End If
    End Sub

    Private Sub chkChapterAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterAll.ToggleStateChanged
        If chkChapterAll.IsChecked() Then
            cbgCustomer.CheckedAll()
            cbgCustomer.Enabled = False
        Else
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        End If

    End Sub

    Private Sub MyRadioButton4_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton4.ToggleStateChanged
        If MyRadioButton4.IsChecked() Then
            cbgVendor.CheckedAll()
            cbgVendor.Enabled = False
        Else
            cbgVendor.UnCheckedAll()

            cbgVendor.Enabled = True
        End If
    End Sub
    Private Sub MyRadioButton2_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton2.ToggleStateChanged
        If MyRadioButton2.IsChecked() Then
            cbgBills.CheckedAll()
            cbgBills.Enabled = False
        Else
            cbgBills.UnCheckedAll()

            cbgBills.Enabled = True
        End If
    End Sub
    Private Sub MyRadioButton1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton1.ToggleStateChanged
        If MyRadioButton1.IsChecked() Then
            cbgBills.UnCheckedAll()
            cbgBills.Enabled = True
        Else
            cbgBills.CheckedAll()

            cbgBills.Enabled = False
        End If
    End Sub

    Private Sub dtpFrmDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrmDate.ValueChanged
        LoadBills()
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        LoadBills()
    End Sub

    Private Sub cbgCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgCustomer.Load

    End Sub
    Sub referesh()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strVendor As String = ""
        Dim strBills As String = ""
        Dim strDateRange As String = ""
        Dim strdtrng As String = ""
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Try
            strdtrng = "'" & Format(dtpFrmDate.Value, "dd/MM/yyyy") & " TO " & Format(dtpToDate.Value, "dd/MM/yyyy") & "'"
            qry = "select " & strdtrng & " as Dt_Range, tspl_company_master.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Document_Code ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code+',Customer Name:'+CONVERT(Varchar,Customer_Name )+',Invoce type :'+(case when TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='T' then 'TAX INVOICE' else case when TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='R' then 'RETAIL INVOICE' else TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type end end) as Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,case when TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='T' then 'TAX INVOICE' else case when TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='R' then 'RETAIL INVOICE' else TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type end end as 'Invoice_Type' ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,convert(varchar,convert(integer,TSPL_SD_SALE_INVOICE_DETAIL.Qty))+ ' ' + TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as 'QTY' ,convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost) + '/' + TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as 'Item_Cost',TSPL_SD_SALE_INVOICE_DETAIL.Amount ,TSPL_VENDOR_ITEM_DETAIL.vendor_code+',Vendor Name:'+CONVERT(Varchar,Vendor_Name)as vendor_code,TSPL_VENDOR_MASTER.Vendor_Name+','+CONVERT(Varchar,'" & Format(dtpFrmDate.Value, "dd/MM/yyyy") & " TO " & Format(dtpToDate.Value, "dd/MM/yyyy") & "') as Vendor_Name from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code   left outer join TSPL_VENDOR_ITEM_DETAIL on TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_ITEM_DETAIL.vendor_code   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code    "

            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALE_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If cbgVendor.CheckedValue.Count > 0 Then
                strVendor += " and TSPL_VENDOR_ITEM_DETAIL.Vendor_Code in  (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
            Else
                strVendor = ""

            End If

            If cbgBills.CheckedValue.Count > 0 Then
                strBills += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in  (" + clsCommon.GetMulcallString(cbgBills.CheckedValue) + ") "
            Else
                strBills = ""

            End If
            strDateRange = " where  CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strVendor
            qry += strCustomer
            qry += strBills
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)

            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True

            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                SetGridFormatOFGV1()

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormatOFGV1()
        'Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("dt_range").IsVisible = False
        gv1.Columns("dt_range").Width = 200
        gv1.Columns("dt_range").HeaderText = "dt_range"

        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Document_Date").Width = 120
        gv1.Columns("Document_Date").HeaderText = "Document_Date"

        gv1.Columns("Comp_Code").IsVisible = False
        gv1.Columns("Comp_Code").Width = 200
        gv1.Columns("Comp_Code").HeaderText = "Comp_Code"

        gv1.Columns("Comp_Name").IsVisible = False
        gv1.Columns("Comp_Name").Width = 70
        gv1.Columns("Comp_Name").HeaderText = "Comp_Name"

        gv1.Columns("Add1").IsVisible = False
        gv1.Columns("Add1").Width = 100
        gv1.Columns("Add1").HeaderText = "Add1"

        gv1.Columns("Add2").IsVisible = False
        gv1.Columns("Add2").Width = 100
        gv1.Columns("Add2").HeaderText = "Add2"

        gv1.Columns("Add3").IsVisible = False
        gv1.Columns("Add3").Width = 100
        gv1.Columns("Add3").HeaderText = "Add3"

        gv1.Columns("Pincode").IsVisible = False
        gv1.Columns("Pincode").Width = 120
        gv1.Columns("Pincode").HeaderText = "Pincode"

        gv1.Columns("Phone1").IsVisible = False
        gv1.Columns("Phone1").Width = 120
        gv1.Columns("Phone1").HeaderText = "Phone1"

        gv1.Columns("Phone2").IsVisible = False
        gv1.Columns("Phone2").Width = 120
        gv1.Columns("Phone2").HeaderText = "Phone2"

        gv1.Columns("Logo_Img").IsVisible = False
        gv1.Columns("Logo_Img").Width = 120
        gv1.Columns("Logo_Img").HeaderText = "Logo_Img"

        gv1.Columns("Logo_Img2").IsVisible = False
        gv1.Columns("Logo_Img2").Width = 120
        gv1.Columns("Logo_Img2").HeaderText = "Logo_Img2"

        gv1.Columns("Document_Code").IsVisible = False
        gv1.Columns("Document_Code").Width = 120
        gv1.Columns("Document_Code").HeaderText = "Document Code"

        gv1.Columns("Customer_Code").IsVisible = False
        gv1.Columns("Customer_Code").Width = 120
        gv1.Columns("Customer_Code").HeaderText = "Customer Code"

        gv1.Columns("Customer_Name").IsVisible = False
        gv1.Columns("Customer_Name").Width = 120
        gv1.Columns("Customer_Name").HeaderText = "Customer_Name"

       
        gv1.Columns("Invoice_Type").IsVisible = False
        gv1.Columns("Invoice_Type").Width = 120
        gv1.Columns("Invoice_Type").HeaderText = "Invoice_Type"

        gv1.Columns("Item_Code").IsVisible = False
        gv1.Columns("Item_Code").Width = 120
        gv1.Columns("Item_Code").HeaderText = "Item_Code"

        gv1.Columns("Item_Desc").IsVisible = True
        gv1.Columns("Item_Desc").Width = 120
        gv1.Columns("Item_Desc").HeaderText = "Particular"

        gv1.Columns("QTY").IsVisible = True
        gv1.Columns("QTY").Width = 120
        gv1.Columns("QTY").HeaderText = "Vch Type"

        gv1.Columns("Item_Cost").IsVisible = True
        gv1.Columns("Item_Cost").Width = 120
        gv1.Columns("Item_Cost").HeaderText = "Vch No"

        gv1.Columns("Amount").IsVisible = True
        gv1.Columns("Amount").Width = 120
        gv1.Columns("Amount").HeaderText = "Debit"

        gv1.Columns("vendor_code").IsVisible = False
        gv1.Columns("vendor_code").Width = 120
        gv1.Columns("vendor_code").HeaderText = "vendor code"

        gv1.Columns("Vendor_Name").IsVisible = False
        gv1.Columns("Vendor_Name").Width = 120
        gv1.Columns("Vendor_Name").HeaderText = "Vendor_Name"

        gv1.Columns("Customer_Name").IsVisible = False
        gv1.Columns("Customer_Name").Width = 120
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item5 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        gv1.GroupDescriptors.Add(New GridGroupByExpression("vendor_code as Item format ""{0}: {1}"" Group By vendor_code"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Customer_Code as Item format ""{0}: {1}"" Group By Customer_Code"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Document_Code as Item format ""{0}: {1}"" Group By Document_Code"))

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnreferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreferesh.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgVendor.CheckedDisplayMember.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please Select At least One Vendor")
            cbgVendor.Focus()
            Exit Sub
        End If


        If cbgBills.CheckedDisplayMember.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please Select At least One Bill !!! " + Environment.NewLine + "There May be no bills in selected Date Range")
            cbgBills.Focus()
            Exit Sub
        End If
        referesh()
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If clsCommon.myLen(gv1.CurrentRow.Cells("Document_Code").Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, gv1.CurrentRow.Cells("Document_Code").Value)

        End If
    End Sub
End Class
