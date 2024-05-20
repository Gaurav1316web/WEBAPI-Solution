'--Updation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001230]

'''' last modified by priti on 14/02/2013 for fast export
'''' last modified by priti on 18/02/2013 for fast export common function
'''' modified by priti for bug no BM00000000432
Imports common
Imports XpertERPEngine
Imports System.Threading
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmRptSales
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Varibales"
    Dim ReportID As String = "SaleRegister"
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim dt As DataTable
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.nrptSales)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        ''clsLocation()
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmRptSales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            If pnlAdminSetting.Visible Then
                pnlAdminSetting.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    pnlAdminSetting.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")





        SetUserMgmtNew()
        chkCustomerAll.IsChecked = True
        rbtnAllCompany.IsChecked = True
        chktempall.IsChecked = True
        chkLocAll.IsChecked = True
        'LoadCustomerType()
        LoadCustomer()
        LoadLocation()
        LoadCustomerCategory()
        LoadTemplate()
        chkcategoryall.IsChecked = True
        SetDataBaseGrid()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        btnExportToExcel.Visible = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        'If clsCommon.myLen(cboCustomerClass.SelectedValue) > 0 Then
        '    strquery += " and Customer_Class='" + clsCommon.myCstr(cboCustomerClass.SelectedValue) + "'"
        'End If

        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadLocation()
        '     Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            print()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            gvReport.EnableFiltering = True
            reset()
            RefreshData()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RefreshData()
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer")
            ElseIf chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One category")
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One Location  ")
            End If

            gvReport.DataSource = Nothing
            Dim customername As String = clsCommon.GetMulcallString(cbgCustomer.CheckedDisplayMember)
            Dim qry As String = ""
            ''Sale Invoice
            Dim baseQuery As String = "select case when Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt end AS MRPBottle," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date," + Environment.NewLine
            baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRP," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code,(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N' then  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
            baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt as  Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name,case when scheme_item='N' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt else 0 end as Inv_Tax_Amt,case when TSPL_SALE_INVOICE_HEAD.Total_Disc_Percent=100 then 0 else " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt end  as Total_Invoice_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_TPT as TPT," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX5," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX6," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX7," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX8," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX9," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX10," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt * " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX1_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX2_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX3_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX4_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX5_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX5_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX6_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX6_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX7_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX7_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX8_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX8_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX9_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX9_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX10_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX10_Amt" + Environment.NewLine
            baseQuery += " ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Account_Amount as Tot_Sale_Account_Amount," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class,'SD-IN' as DocType" + Environment.NewLine
            baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " + Environment.NewLine
            baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine
            baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " + Environment.NewLine
            baseQuery += " where TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date  >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
            baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='y' AND " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N'" + Environment.NewLine
            If chktempall.IsChecked = True Then
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If
            If chkLocSelect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkcategoryselect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If
            ''End of Sale Invoice

            ''Sale Return
            baseQuery += " Union all "
            baseQuery += "select case when Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt end AS MRPBottle," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Sale_Invoice_Date," + Environment.NewLine
            baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as MRP," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code,-1*(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item='N' then  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt) as Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Name,case when TSPL_SALE_RETURN_DETAIL.scheme_item='N' then -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt) else 0 end as Inv_Tax_Amt, -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt)  as Total_Invoice_Amt,-1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_TPT) as TPT,-1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Disc_Amt) as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX5," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX6," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX7," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX8," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX9," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX10," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Amt * " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX1_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX2_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX3_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX4_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX5_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX5_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX6_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX6_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX7_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX7_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX8_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX8_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX9_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX9_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX10_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX10_Amt" + Environment.NewLine
            baseQuery += " ," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code,-1*(isnull( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Account_Amount,0)) as Tot_Sale_Account_Amount," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class,'SD-SR' as DocType" + Environment.NewLine
            baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No" + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code " + Environment.NewLine
            baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code" + Environment.NewLine
            baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " + Environment.NewLine
            baseQuery += " where TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date  >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  " + Environment.NewLine
            baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='y' AND " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item='N'" + Environment.NewLine

            If chktempall.IsChecked = True Then
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If
            If chkLocSelect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkcategoryselect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If
            ''End of Sale Return

            ''Transfer
            If chkIncludeTransfer.Checked Then
                baseQuery += " union all " + Environment.NewLine

                baseQuery += " select case when UOM='FC' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP end AS MRPBottle," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No as Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date as Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRP," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom as Unit_code," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Total_Item_Amt as Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location as Cust_Code,ToLoc.Location_Desc as Cust_Name," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Total_Tax as Inv_Tax_Amt, (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value)*" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty  as Total_Invoice_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value as TPT,0 as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX1," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX2," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX3," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX4," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX5," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX6," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX7," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX8," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX9," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX10," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX1_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX2_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX3_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX4_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX5_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX6_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX7_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX8_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX9_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX10_Amt ," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location ," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code,0 as Tot_Sale_Account_Amount,'' as Customer_Class,'MM-TF' as DocType" + Environment.NewLine
                baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL" + Environment.NewLine
                baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location"
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " + Environment.NewLine
                baseQuery += " where TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Excisable='T' and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Post='Y' and " + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                If chkLocSelect.IsChecked Then
                    baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
                End If

                chkIncludeTransfer.Checked = False
                chkIncludeTransfer.Enabled = False
            End If
            ''End of Transfer

            ''Sale Return InterCompany
            If chkIC.Checked Then
                baseQuery += " union all " + Environment.NewLine

                baseQuery += " select case when Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt end AS MRPBottle," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date as Sale_Invoice_Date," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, " + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as MRP," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code,-1*(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item='N' then  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt) as Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Name,case when TSPL_SALE_RETURN_INTER_DETAIL.scheme_item='N' then -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Total_Tax_Amt) else 0 end as Inv_Tax_Amt," + Environment.NewLine
                baseQuery += "  -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Total_Item_Amt)  as Total_Invoice_Amt,-1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT) as TPT," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Total_Disc_Amt) as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX1," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX2, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX3, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX4, " + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX5, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX6, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX7, " + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX8, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX9, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX10, " + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt * " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX1_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX2_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX3_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX4_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX5_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX6_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX7_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX7_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX8_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX8_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX9_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX9_Amt," + Environment.NewLine
                baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.TAX10_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty) as TAX10_Amt" + Environment.NewLine
                baseQuery += " ," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Comp_Code," + Environment.NewLine
                baseQuery += " -1*(isnull( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Sale_Account_Amount,0)) as Tot_Sale_Account_Amount," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class,'SD-SR' as DocType " + Environment.NewLine
                baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
                baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No " + Environment.NewLine
                baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code " + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and " + Environment.NewLine
                baseQuery += "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code "
                baseQuery += " where TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)  >='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and "
                baseQuery += " convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item='N'"
                If chktempall.IsChecked = True Then
                    If chkCustomerSelect.IsChecked = True Then
                        baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    End If
                ElseIf chktempselect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                    If chkCustomerSelect.IsChecked = True Then
                        baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    End If
                End If
                If chkLocSelect.IsChecked Then
                    baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
                End If
                If chkcategoryselect.IsChecked Then
                    baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
                End If

                chkIC.Checked = False
                chkIC.Enabled = False
            End If
            ''End of InterCompany

            baseQuery = clsCommon.GetQueryWithAllSelectedDataBase(baseQuery, GetSelectedDatabase(), False)

            qry = "select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, '" + objCommonVar.CurrentCompanyName + "' as CurrentComp,  xxxxx.Sale_Invoice_No," + Environment.NewLine
            qry += " CONVERT(Date, xxxxx.Sale_Invoice_Date,103) as Sale_Invoice_Date,xxxxx.Inv_Detail_Total_Amt,xxxxx.Cust_Code,xxxxx.Cust_Name, xxxxx.Inv_Tax_Amt, xxxxx.Total_Invoice_Amt, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name, xxxxx.Customer_Class,((-ISNULL(xxxxx.Inv_Discount_Amt, 0)+ISNULL(xxxxx.Inv_Detail_Total_Amt,0))+ xxxxx.Inv_Tax_Amt)as TotalAmount, xxxxx.TPT, xxxxx.Inv_Discount_Amt as [Customer Discount Amt], (-ISNULL(xxxxx.Inv_Discount_Amt, 0)+ISNULL(xxxxx.Inv_Detail_Total_Amt,0)) as [Gross Amount],  (case when  TaxM1.Type='A' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='A' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='A' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='A' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='A' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='A' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM7.Type='A' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM8.Type='A' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM9.Type='A' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='A' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as AddTaxAmt," + Environment.NewLine
            qry += "  (case when  TaxM1.Type='V' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='V' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='V' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='V' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='V' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='V' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += "  +case when  TaxM7.Type='V' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += "  +case when  TaxM8.Type='V' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM9.Type='V' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='V' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += "  ) as VatTaxAmt," + Environment.NewLine
            qry += " (case when  TaxM1.Type='O' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='O' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='O' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='O' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='O' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='O' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM7.Type='O' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM8.Type='O' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += "  +case when  TaxM9.Type='O' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='O' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as OtherTaxAmt," + Environment.NewLine
            qry += " (case when  TaxM1.Type='C' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='C' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='C' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='C' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='C' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='C' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM7.Type='C' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM8.Type='C' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM9.Type='C' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='C' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as CSTTaxAmt," + Environment.NewLine
            qry += " (case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as ExciseAmt," + Environment.NewLine
            qry += " (case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as ECessAmt," + Environment.NewLine
            qry += " (case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as HCessAmt" + Environment.NewLine
            qry += " , isnull(xxxxx.Tot_Sale_Account_Amount,0) as Tot_Sale_Account_Amount,xxxxx.Item_Code,xxxxx.Item_Desc,xxxxx.MRP,xxxxx.MRPBottle,xxxxx.Qty,xxxxx.DocType " + Environment.NewLine

            qry += " from (" + Environment.NewLine + baseQuery + Environment.NewLine + " ) xxxxx" + Environment.NewLine

            qry += " Left Outer Join TSPL_LOCATION_MASTER on xxxxx.Location=TSPL_LOCATION_MASTER.Location_Code " + Environment.NewLine
            qry += " Left Outer Join TSPL_COMPANY_MASTER on xxxxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=xxxxx.TAX1" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=xxxxx.TAX2" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=xxxxx.TAX3" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=xxxxx.TAX4" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=xxxxx.TAX5" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=xxxxx.TAX6" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=xxxxx.TAX7" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=xxxxx.TAX8" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=xxxxx.TAX9" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=xxxxx.TAX10" + Environment.NewLine
            Dim FinQry As String = ""
            If rbtnItemWise.IsChecked Then
                FinQry = "select (xxxxxx.Sale_Invoice_No),max(xxxxxx.Sale_Invoice_Date) as Sale_Invoice_Date,max(xxxxxx.Cust_Code) as Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class,max(xxxxxx.Item_Code) as Item_Code,max(xxxxxx.Item_Desc) as Item_Desc, xxxxxx.MRP ,xxxxxx.MRPBottle ,sum(xxxxxx.Qty) as Qty ,sum(xxxxxx.Inv_Detail_Total_Amt) as Inv_Detail_Total_Amt,sum(xxxxxx.Inv_Tax_Amt) as Inv_Tax_Amt,sum(xxxxxx.Total_Invoice_Amt) as Total_Invoice_Amt,sum(xxxxxx.TotalAmount) as TotalAmount,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Gross Amount],sum(xxxxxx.AddTaxAmt) as AddTaxAmt,sum(xxxxxx.ExciseAmt) as ExciseAmt,sum(xxxxxx.ECessAmt) as ECessAmt,sum(xxxxxx.HCessAmt) as HCessAmt,sum(xxxxxx.VatTaxAmt) as VatTaxAmt,sum(xxxxxx.CSTTaxAmt) as CSTTaxAmt,sum(xxxxxx.OtherTaxAmt) as OtherTaxAmt,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name,MAX( xxxxxx.RunDate) as RunDate,max(xxxxxx.FromDate) as FromDate,max(xxxxxx.ToDate) as ToDate,max(xxxxxx.CurrentComp) as CurrentComp from("
                FinQry += qry
                FinQry += " )xxxxxx Group by xxxxxx.Sale_Invoice_No,xxxxxx.Item_Code,xxxxxx.MRP,xxxxxx.MRPBottle  order by convert(date,max(xxxxxx.Sale_Invoice_Date),103 ) "
            ElseIf rbtnInvoiceWise.IsChecked Then
                FinQry = "select (xxxxxx.Sale_Invoice_No),max(xxxxxx.Sale_Invoice_Date) as Sale_Invoice_Date,max(xxxxxx.Cust_Code) as Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class ,sum(xxxxxx.Inv_Detail_Total_Amt) as Inv_Detail_Total_Amt,sum(xxxxxx.Inv_Tax_Amt) as Inv_Tax_Amt,sum(xxxxxx.Total_Invoice_Amt) as Total_Invoice_Amt,sum(xxxxxx.TotalAmount) as TotalAmount,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Gross Amount],sum(xxxxxx.AddTaxAmt) as AddTaxAmt,sum(xxxxxx.ExciseAmt) as ExciseAmt,sum(xxxxxx.ECessAmt) as ECessAmt,sum(xxxxxx.HCessAmt) as HCessAmt,sum(xxxxxx.VatTaxAmt) as VatTaxAmt,sum(xxxxxx.CSTTaxAmt) as CSTTaxAmt,sum(xxxxxx.OtherTaxAmt) as OtherTaxAmt,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name,MAX( xxxxxx.RunDate) as RunDate,max(xxxxxx.FromDate) as FromDate,max(xxxxxx.ToDate) as ToDate,max(xxxxxx.CurrentComp) as CurrentComp,DocType from("
                FinQry += qry
                FinQry += " )xxxxxx Group by xxxxxx.Sale_Invoice_No,DocType  order by convert(date,max(xxxxxx.Sale_Invoice_Date),103 ) "
            ElseIf rbtnCustomerWise.IsChecked Then
                FinQry = "select   xxxxxx.Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class,sum(xxxxxx.Inv_Detail_Total_Amt) as Inv_Detail_Total_Amt,sum(xxxxxx.Inv_Tax_Amt) as Inv_Tax_Amt,sum(xxxxxx.Total_Invoice_Amt) as Total_Invoice_Amt,sum(xxxxxx.TotalAmount) as TotalAmount,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Gross Amount],sum(xxxxxx.AddTaxAmt) as AddTaxAmt,sum(xxxxxx.ExciseAmt) as ExciseAmt,sum(xxxxxx.ECessAmt) as ECessAmt,sum(xxxxxx.HCessAmt) as HCessAmt,sum(xxxxxx.VatTaxAmt) as VatTaxAmt,sum(xxxxxx.CSTTaxAmt) as CSTTaxAmt,sum(xxxxxx.OtherTaxAmt) as OtherTaxAmt,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name,MAX( xxxxxx.RunDate) as RunDate,max(xxxxxx.FromDate) as FromDate,max(xxxxxx.ToDate) as ToDate,max(xxxxxx.CurrentComp) as CurrentComp from("
                FinQry += qry
                FinQry += " )xxxxxx Group by xxxxxx.Cust_Code order by xxxxxx.Cust_Code "
            Else
                Throw New Exception("Plese select correct type")
            End If
            dt = clsDBFuncationality.GetDataTable(FinQry)

            If rbtnInvoiceWise.IsChecked AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
                Dim strComponent As String = ""
                Dim strColName As String = ""
                If rbtnCST.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRegisterCST
                    strColName = "CSTTaxAmt"
                ElseIf rbtnVAT.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRegisterVat
                    strColName = "VatTaxAmt"
                Else
                    Throw New Exception("Not a Valid Option for Reconcilation")
                End If


                Dim dtAS As DataTable = clsReconciliationSetting.GetAccounts(clsRecoSettingReportName.SaleRegister, strComponent, txtFromDate.Value, txtToDate.Value)
                Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
                dt.Columns.Add("SubledgerAmt", GetType(Double))
                If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                    arr = New Dictionary(Of String, clsTempDrCrAmt)
                    For Each dr As DataRow In dtAS.Rows
                        Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                        obj.DrAmt = -1 * clsCommon.myCdbl(dr("SubledgerAmt"))
                        arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("Sale_Invoice_No")) + clsCommon.myCstr(dt.Rows(ii)("DocType"))).ToUpper()
                        If arr.ContainsKey(strSourceDocNo) Then
                            dt.Rows(ii)("SubledgerAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)
                        End If
                    Next
                End If
                If chkMismatch.Checked Then
                    Dim dtView As DataView = dt.DefaultView
                    dtView.RowFilter = "  (SubledgerAmt<>" + strColName + ")"
                End If
            End If


            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.DataSource = dt
                FormatGrid()
                EnableDisableControls(False)
            End If

            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub ExportRefreshData()
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer")
            ElseIf chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One category")
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One Location  ")
            End If

            'gvReport.DataSource = Nothing
            Dim customername As String = clsCommon.GetMulcallString(cbgCustomer.CheckedDisplayMember)
            Dim qry As String = ""
            ''Sale Invoice
            Dim baseQuery As String = "select " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date," + Environment.NewLine
            baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRP," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code,(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N' then  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
            baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt as  Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name,case when scheme_item='N' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt else 0 end as Inv_Tax_Amt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt  as Total_Invoice_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_TPT as TPT," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX5," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX6," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX7," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX8," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX9," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX10," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt * " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX1_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX2_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX3_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX4_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX5_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX5_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX6_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX6_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX7_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX7_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX8_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX8_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX9_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX9_Amt," + Environment.NewLine
            baseQuery += " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX10_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX10_Amt" + Environment.NewLine
            baseQuery += " ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Account_Amount as Tot_Sale_Account_Amount," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class" + Environment.NewLine
            baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " + Environment.NewLine
            baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine
            baseQuery += " where  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date  >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  " + Environment.NewLine
            baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='y' AND " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N'" + Environment.NewLine
            If chktempall.IsChecked = True Then
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If
            If chkLocSelect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkcategoryselect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If
            ''End of Sale Invoice

            ''Sale Return
            baseQuery += " Union all "
            baseQuery += "select " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Sale_Invoice_Date," + Environment.NewLine
            baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as MRP," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code,-1*(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item='N' then  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt) as Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Name,case when TSPL_SALE_RETURN_DETAIL.scheme_item='N' then  -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt) else 0 end as Inv_Tax_Amt, -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt)  as Total_Invoice_Amt,-1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_TPT) as TPT,-1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Disc_Amt) as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX5," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX6," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX7," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX8," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX9," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX10," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Amt * " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX1_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX2_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX3_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX4_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX5_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX5_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX6_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX6_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX7_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX7_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX8_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX8_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX9_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX9_Amt," + Environment.NewLine
            baseQuery += " -1*(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX10_Amt* " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX10_Amt" + Environment.NewLine
            baseQuery += " ," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code,-1*(isnull( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Account_Amount,0)) as Tot_Sale_Account_Amount," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class" + Environment.NewLine
            baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No" + Environment.NewLine
            baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code " + Environment.NewLine
            baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code" + Environment.NewLine
            baseQuery += " where  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date  >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  " + Environment.NewLine
            baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='y' AND " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item='N'" + Environment.NewLine

            If chktempall.IsChecked = True Then
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkCustomerSelect.IsChecked = True Then
                    baseQuery += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If
            If chkLocSelect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkcategoryselect.IsChecked Then
                baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If
            ''End of Sale Return

            ''Transfer
            If chkIncludeTransfer.Checked Then
                baseQuery += " union all " + Environment.NewLine

                baseQuery += " select " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No as Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date as Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP*" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRP," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom as Unit_code," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Total_Item_Amt as Inv_Detail_Total_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location as Cust_Code,ToLoc.Location_Desc as Cust_Name," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Total_Tax as Inv_Tax_Amt, (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value)*" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty  as Total_Invoice_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value as TPT,0 as Inv_Discount_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX1," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX2," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX3," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX4," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX5," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX6," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX7," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX8," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX9," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX10," + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX1_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX2_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX3_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX4_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX5_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX6_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX7_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX8_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX9_Amt," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TAX10_Amt ," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location ," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code,0 as Tot_Sale_Account_Amount,'' as Customer_Class" + Environment.NewLine
                baseQuery += " from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL" + Environment.NewLine
                baseQuery += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
                baseQuery += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location"
                baseQuery += " where " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Excisable='T' and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Post='Y' and " + Environment.NewLine
                baseQuery += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                If chkLocSelect.IsChecked Then
                    baseQuery += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
                End If

            End If
            ''End of Transfer

            baseQuery = clsCommon.GetQueryWithAllSelectedDataBase(baseQuery, GetSelectedDatabase(), False)

            qry = "select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, '" + objCommonVar.CurrentCompanyName + "' as CurrentComp,  xxxxx.Sale_Invoice_No," + Environment.NewLine
            qry += " CONVERT(varchar(10), xxxxx.Sale_Invoice_Date,103) as Sale_Invoice_Date,xxxxx.Inv_Detail_Total_Amt,xxxxx.Cust_Code,xxxxx.Cust_Name, xxxxx.Inv_Tax_Amt, xxxxx.Total_Invoice_Amt, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name, xxxxx.Customer_Class,((-ISNULL(xxxxx.Inv_Discount_Amt, 0)+ISNULL(xxxxx.Inv_Detail_Total_Amt,0))+ xxxxx.Inv_Tax_Amt)as TotalAmount, xxxxx.TPT, xxxxx.Inv_Discount_Amt as [Customer Discount Amt], (-ISNULL(xxxxx.Inv_Discount_Amt, 0)+ISNULL(xxxxx.Inv_Detail_Total_Amt,0)) as [Gross Amount],  (case when  TaxM1.Type='A' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='A' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='A' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='A' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='A' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='A' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM7.Type='A' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM8.Type='A' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM9.Type='A' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='A' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as AddTaxAmt," + Environment.NewLine
            qry += "  (case when  TaxM1.Type='V' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='V' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='V' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='V' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='V' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='V' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += "  +case when  TaxM7.Type='V' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += "  +case when  TaxM8.Type='V' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM9.Type='V' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='V' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += "  ) as VatTaxAmt," + Environment.NewLine
            qry += " (case when  TaxM1.Type='O' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='O' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='O' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='O' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='O' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='O' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM7.Type='O' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM8.Type='O' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += "  +case when  TaxM9.Type='O' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='O' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as OtherTaxAmt," + Environment.NewLine
            qry += " (case when  TaxM1.Type='C' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM2.Type='C' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM3.Type='C' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM4.Type='C' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM5.Type='C' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM6.Type='C' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM7.Type='C' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM8.Type='C' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM9.Type='C' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
            qry += " +case when  TaxM10.Type='C' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as CSTTaxAmt," + Environment.NewLine
            qry += " (case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as ExciseAmt," + Environment.NewLine
            qry += " (case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as ECessAmt," + Environment.NewLine
            qry += " (case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
            qry += " ) as HCessAmt" + Environment.NewLine
            qry += " , isnull(xxxxx.Tot_Sale_Account_Amount,0) as Tot_Sale_Account_Amount,xxxxx.Item_Code,xxxxx.Item_Desc,xxxxx.MRP,xxxxx.Qty " + Environment.NewLine

            qry += " from (" + Environment.NewLine + baseQuery + Environment.NewLine + " ) xxxxx" + Environment.NewLine

            qry += " Left Outer Join TSPL_LOCATION_MASTER on xxxxx.Location=TSPL_LOCATION_MASTER.Location_Code " + Environment.NewLine
            qry += " Left Outer Join TSPL_COMPANY_MASTER on xxxxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=xxxxx.TAX1" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=xxxxx.TAX2" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=xxxxx.TAX3" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=xxxxx.TAX4" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=xxxxx.TAX5" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=xxxxx.TAX6" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=xxxxx.TAX7" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=xxxxx.TAX8" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=xxxxx.TAX9" + Environment.NewLine
            qry += " left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=xxxxx.TAX10" + Environment.NewLine

            Dim FinQry As String = ""
            If rbtnItemWise.IsChecked Then
                FinQry = "select (xxxxxx.Sale_Invoice_No) as Invoice_No,max(xxxxxx.Sale_Invoice_Date) as Sale_Invoice_Date,max(xxxxxx.Cust_Code) as Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class,max(xxxxxx.Item_Code) as Item_Code,max(xxxxxx.Item_Desc) as Item_Desc, xxxxxx.MRP ,sum(xxxxxx.Qty) as Qty ,sum(xxxxxx.Inv_Detail_Total_Amt) as Basic_Amount,sum(xxxxxx.Inv_Tax_Amt) as Tax_Amount,sum(xxxxxx.Total_Invoice_Amt) as Invoice_Amount,sum(xxxxxx.TotalAmount) as Amount_With_Tax,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Net_Amount],sum(xxxxxx.AddTaxAmt) as Additional_Tax,sum(xxxxxx.ExciseAmt) as Excise,sum(xxxxxx.ECessAmt) as ECess,sum(xxxxxx.HCessAmt) as HCess,sum(xxxxxx.VatTaxAmt) as Vat,sum(xxxxxx.CSTTaxAmt) as CST,sum(xxxxxx.OtherTaxAmt) as Other_Tax,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name from("
                FinQry += qry
                FinQry += " )xxxxxx Group by xxxxxx.Sale_Invoice_No,xxxxxx.Item_Code,xxxxxx.MRP order by Sale_Invoice_Date"
            ElseIf rbtnInvoiceWise.IsChecked Then
                FinQry = "select (xxxxxx.Sale_Invoice_No) as Invoice_No,max(xxxxxx.Sale_Invoice_Date) as Sale_Invoice_Date,max(xxxxxx.Cust_Code) as Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class ,sum(xxxxxx.Inv_Detail_Total_Amt) as Basic_Amount,sum(xxxxxx.Inv_Tax_Amt) as Tax_Amount,sum(xxxxxx.Total_Invoice_Amt) as Invoice_Amount,sum(xxxxxx.TotalAmount) as Amount_With_Tax,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Net_Amount],sum(xxxxxx.AddTaxAmt) as Additional_Tax,sum(xxxxxx.ExciseAmt) as Excise,sum(xxxxxx.ECessAmt) as ECess,sum(xxxxxx.HCessAmt) as HCess,sum(xxxxxx.VatTaxAmt) as Vat,sum(xxxxxx.CSTTaxAmt) as CST,sum(xxxxxx.OtherTaxAmt) as Other_Tax,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name from("
                FinQry += qry
                FinQry += " )xxxxxx Group by xxxxxx.Sale_Invoice_No  order by Sale_Invoice_Date"
            ElseIf rbtnCustomerWise.IsChecked Then
                FinQry = "select   xxxxxx.Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class,sum(xxxxxx.Inv_Detail_Total_Amt) as Basic_Amount,sum(xxxxxx.Inv_Tax_Amt) as Tax_Amount,sum(xxxxxx.Total_Invoice_Amt) as Invoice_Amount,sum(xxxxxx.TotalAmount) as Amount_With_Tax,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Net_Amount],sum(xxxxxx.AddTaxAmt) as Additional_Tax,sum(xxxxxx.ExciseAmt) as Excise,sum(xxxxxx.ECessAmt) as ECess,sum(xxxxxx.HCessAmt) as HCess,sum(xxxxxx.VatTaxAmt) as Vat,sum(xxxxxx.CSTTaxAmt) as CST,sum(xxxxxx.OtherTaxAmt) as Other_Tax,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name from("
                FinQry += qry
                FinQry += " )xxxxxx Group by xxxxxx.Cust_Code order by xxxxxx.Cust_Code"
            Else
                Throw New Exception("Plese select correct type")
            End If

            dt = clsDBFuncationality.GetDataTable(FinQry)

            '''' export code start here

            Dim strs As String = String.Empty
            Dim strLocation As String = ""
            Dim strCustomer As String = ""
            Dim strCustType As String = ""

            'File name and path
            Dim filePath As String = "C:\\ERPTempFolder\Sale Register.xls"

            'Stream Writer object to write the stream to file
            Dim writer As New IO.StreamWriter(IO.File.Create(filePath))
            Dim dRow As DataRow


            strs = "Report Name  :  Sale Register Report"
            strs += vbNewLine
            writer.Write(strs)
            writer.Flush()

            strs = ""

            ''''' For location Filter
            If chkLocSelect.IsChecked Then

                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocation) > 0 Then
                        strLocation += ", "
                    End If
                    strLocation += Str
                Next
                If strLocation = "" Then
                    strLocation = "All"
                End If
                strs = "Location :          " + strLocation
                strs += vbNewLine
                writer.Write(strs)
                writer.Flush()
            End If

            strs = ""

            ''''' For Customer Filter
            If chkCustomerSelect.IsChecked Then

                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strCustomer) > 0 Then
                        strCustomer += ", "
                    End If
                    strCustomer += Str
                Next
                If strCustomer = "" Then
                    strCustomer = "All"
                End If
                strs = "Customer :          " + strCustomer
                strs += vbNewLine
                writer.Write(strs)
                writer.Flush()
            End If

            strs = ""

            ''''' For Customer Type Filter
            If chkcategoryselect.IsChecked Then

                For Each Str As String In cbgcategory.CheckedDisplayMember
                    If clsCommon.myLen(strCustType) > 0 Then
                        strCustType += ", "
                    End If
                    strCustType += Str
                Next
                If strCustType = "" Then
                    strCustType = "All"
                End If
                strs = "Customer Type :          " + strCustType
                strs += vbNewLine
                writer.Write(strs)
                writer.Flush()
            End If

            strs = ""


            ''For Heading
            For i As Integer = 0 To dt.Columns.Count - 1
                strs += dt.Columns.Item(i).Caption & vbTab
            Next
            strs = Replace(strs, "_", " ")
            'Write stream to file adding a new line to stream
            strs += vbNewLine
            writer.Write(strs)
            writer.Flush()


            'Dim DecTotal As Decimal

            For Each dRow In dt.Rows
                strs = ""
                For col As Integer = 0 To dt.Columns.Count - 1

                    strs += dRow(col).ToString & vbTab

                Next
                strs += vbNewLine
                writer.Write(strs)
                writer.Flush()

            Next
            'Close the stream writer object
            writer.Close()
            'MsgBox("Data Exported Successfully.")

            Dim xlsApp As Microsoft.Office.Interop.Excel.Application
            Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
            xlsApp = New Microsoft.Office.Interop.Excel.Application
            xlsApp.Visible = True
            xlsWB = xlsApp.Workbooks.Open("C:\\ERPTempFolder\Sale Register.xls")
            '''' 

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As GridViewSummaryItem

        gvReport.Columns("Cust_Code").IsVisible = True
        gvReport.Columns("Cust_Code").Width = 51
        gvReport.Columns("Cust_Code").HeaderText = "Customer Code"

        gvReport.Columns("Cust_Name").IsVisible = True
        gvReport.Columns("Cust_Name").Width = 151
        gvReport.Columns("Cust_Name").HeaderText = "Customer Name"

        gvReport.Columns("Customer_Class").IsVisible = True
        gvReport.Columns("Customer_Class").Width = 51
        gvReport.Columns("Customer_Class").HeaderText = "Cust Class"

        gvReport.Columns("Inv_Detail_Total_Amt").Width = 71
        gvReport.Columns("Inv_Detail_Total_Amt").HeaderText = "Basic Amount"
        gvReport.Columns("Inv_Detail_Total_Amt").IsVisible = True
        gvReport.Columns("Inv_Detail_Total_Amt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Inv_Detail_Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        gvReport.Columns("Inv_Tax_Amt").Width = 71
        gvReport.Columns("Inv_Tax_Amt").HeaderText = "Tax Amount"
        gvReport.Columns("Inv_Tax_Amt").IsVisible = True
        gvReport.Columns("Inv_Tax_Amt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Inv_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        gvReport.Columns("Total_Invoice_Amt").Width = 71
        gvReport.Columns("Total_Invoice_Amt").HeaderText = "Invoice Amount"
        gvReport.Columns("Total_Invoice_Amt").IsVisible = True
        gvReport.Columns("Total_Invoice_Amt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Total_Invoice_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        gvReport.Columns("TotalAmount").Width = 71
        gvReport.Columns("TotalAmount").HeaderText = "Amount With Tax"
        gvReport.Columns("TotalAmount").IsVisible = True
        gvReport.Columns("TotalAmount").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("TotalAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("TPT").IsVisible = True
        gvReport.Columns("TPT").Width = 51
        gvReport.Columns("TPT").HeaderText = "TPT"
        gvReport.Columns("TPT").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("TPT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Customer Discount Amt").IsVisible = True
        gvReport.Columns("Customer Discount Amt").Width = 101
        gvReport.Columns("Customer Discount Amt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Customer Discount Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Gross Amount").IsVisible = True
        gvReport.Columns("Gross Amount").Width = 101
        gvReport.Columns("Gross Amount").FormatString = "{0:F2}"
        gvReport.Columns("Gross Amount").HeaderText = "Net Amount"
        item1 = New GridViewSummaryItem("Gross Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("AddTaxAmt").IsVisible = True
        gvReport.Columns("AddTaxAmt").Width = 71
        gvReport.Columns("AddTaxAmt").HeaderText = "Additional Tax"
        gvReport.Columns("AddTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("AddTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("VatTaxAmt").IsVisible = True
        gvReport.Columns("VatTaxAmt").Width = 71
        gvReport.Columns("VatTaxAmt").HeaderText = "Vat"
        gvReport.Columns("VatTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("VatTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("OtherTaxAmt").IsVisible = True
        gvReport.Columns("OtherTaxAmt").Width = 71
        gvReport.Columns("OtherTaxAmt").HeaderText = "Other Tax"
        gvReport.Columns("OtherTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("OtherTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("CSTTaxAmt").IsVisible = True
        gvReport.Columns("CSTTaxAmt").Width = 71
        gvReport.Columns("CSTTaxAmt").HeaderText = "CST"
        gvReport.Columns("CSTTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("CSTTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("ExciseAmt").IsVisible = True
        gvReport.Columns("ExciseAmt").Width = 71
        gvReport.Columns("ExciseAmt").HeaderText = "Excise"
        gvReport.Columns("ExciseAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("ExciseAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("ECessAmt").IsVisible = True
        gvReport.Columns("ECessAmt").Width = 71
        gvReport.Columns("ECessAmt").HeaderText = "ECess"
        gvReport.Columns("ECessAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("ECessAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("HCessAmt").IsVisible = True
        gvReport.Columns("HCessAmt").Width = 71
        gvReport.Columns("HCessAmt").HeaderText = "HCess"
        gvReport.Columns("HCessAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("HCessAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Comp_Name").IsVisible = True
        gvReport.Columns("Comp_Name").Width = 151
        gvReport.Columns("Comp_Name").HeaderText = "Company Name"

        gvReport.Columns("Tot_Sale_Account_Amount").IsVisible = True
        gvReport.Columns("Tot_Sale_Account_Amount").Width = 100
        gvReport.Columns("Tot_Sale_Account_Amount").HeaderText = "GL Account Amount"
        gvReport.Columns("Tot_Sale_Account_Amount").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Tot_Sale_Account_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Location_Code").IsVisible = True
        gvReport.Columns("Location_Code").Width = 51
        gvReport.Columns("Location_Code").HeaderText = "Location"

        gvReport.Columns("Location_Desc").IsVisible = True
        gvReport.Columns("Location_Desc").Width = 101
        gvReport.Columns("Location_Desc").HeaderText = "Location Desc"

        If rbtnItemWise.IsChecked Then
            gvReport.Columns("Sale_Invoice_No").IsVisible = True
            gvReport.Columns("Sale_Invoice_No").Width = 151
            gvReport.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

            gvReport.Columns("Sale_Invoice_Date").IsVisible = True
            gvReport.Columns("Sale_Invoice_Date").Width = 81
            gvReport.Columns("Sale_Invoice_Date").FormatString = "{0:d}"
            gvReport.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"

            gvReport.Columns("Item_Code").IsVisible = True
            gvReport.Columns("Item_Code").Width = 100
            gvReport.Columns("Item_Code").HeaderText = "Item Code"

            gvReport.Columns("Item_Desc").IsVisible = True
            gvReport.Columns("Item_Desc").Width = 100
            gvReport.Columns("Item_Desc").HeaderText = "Item Description"

            gvReport.Columns("MRP").IsVisible = True
            gvReport.Columns("MRP").Width = 100
            gvReport.Columns("MRP").HeaderText = "MRP"
            gvReport.Columns("MRP").FormatString = "{0:F2}"

            gvReport.Columns("MRPBottle").IsVisible = True
            gvReport.Columns("MRPBottle").Width = 100
            gvReport.Columns("MRPBottle").HeaderText = "MRPBottle"
            gvReport.Columns("MRPBottle").FormatString = "{0:F2}"

            gvReport.Columns("Qty").IsVisible = True
            gvReport.Columns("Qty").Width = 100
            gvReport.Columns("Qty").HeaderText = "Quantity"
            gvReport.Columns("Qty").FormatString = "{0:F2}"
            item1 = New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            gvReport.Columns("Sale_Invoice_No").IsVisible = True
            gvReport.Columns("Sale_Invoice_No").Width = 151
            gvReport.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

            gvReport.Columns("Sale_Invoice_Date").IsVisible = True
            gvReport.Columns("Sale_Invoice_Date").Width = 81
            gvReport.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"

            ''gvReport.GroupDescriptors.Add(New GridGroupByExpression("Sale_Invoice_No as Sale_Invoice_No format ""{0}: {1}"" Group By Sale_Invoice_No"))

            gvReport.ShowGroupPanel = False
            gvReport.MasterTemplate.AutoExpandGroups = True


        ElseIf rbtnInvoiceWise.IsChecked Then
            gvReport.Columns("Sale_Invoice_No").IsVisible = True
            gvReport.Columns("Sale_Invoice_No").Width = 151
            gvReport.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

            gvReport.Columns("Sale_Invoice_Date").IsVisible = True
            gvReport.Columns("Sale_Invoice_Date").Width = 81
            gvReport.Columns("Sale_Invoice_Date").FormatString = "{0:d}"
            gvReport.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"


        ElseIf rbtnCustomerWise.IsChecked Then

        End If
        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvReport.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        If rbtnInvoiceWise.IsChecked AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
            gvReport.Columns("SubledgerAmt").IsVisible = True
            gvReport.Columns("SubledgerAmt").Width = 100
            gvReport.Columns("SubledgerAmt").HeaderText = "Subledger Amt"

            Dim emptydr As New GridViewSummaryItem("SubledgerAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(emptydr)

        End If


    End Sub

    Sub print()
        RefreshData()

        If rbtnInvoiceWise.IsChecked = True AndAlso dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptSaleRegisterDetailNew", "Sales Register")
        ElseIf rbtnCustomerWise.IsChecked = True AndAlso dt.Rows.Count > 0 Then
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptSaleRegisterSmryNew", "Sales Register")
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()

    End Sub

    Sub reset()
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        EnableDisableControls(True)

    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        RadGroupBox2.Enabled = Val
        chkIncludeTransfer.Enabled = Val
        grpCustomer.Enabled = Val
        grpCustomerType.Enabled = Val
        grpLocation.Enabled = Val
        grpCompany.Enabled = Val
        grpTemplate.Enabled = Val
        chkIC.Enabled = Val

    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged, rbtnSelectCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub cboCustomerClass_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        LoadCustomer()
        chkCustomerAll.IsChecked = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Sub LoadCustomerCategory()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        cbgcategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcategory.ValueMember = "Code"
        cbgcategory.DisplayMember = "Name"
    End Sub

    Private Sub chkcategoryall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryall.ToggleStateChanged
        cbgcategory.Enabled = Not chkcategoryall.IsChecked
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            If rbtnSelectCompany.IsChecked Then
                strTemp = ""
                For ii As Integer = 0 To gvDB.Rows.Count - 1
                    If clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value) Then
                        Dim Str As String = clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompName).Value)
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str
                    End If


                Next
                arrHeader.Add("Company : " + strTemp)
            End If
            If chkcategoryselect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgcategory.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer Category : " + strTemp)
            End If
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location Segment : " + strTemp)
            End If
            If chkCustomerSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer : " + strTemp)
            End If
            If chktempselect.IsChecked Then
                strTemp = ""
                For Each Str As String In cgvtemplate.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Template : " + strTemp)
            End If
            Dim ReportType As String = ""
            If rbtnItemWise.IsChecked Then
                ReportType = "Item Wise"
            ElseIf rbtnInvoiceWise.IsChecked Then
                ReportType = "Invocie Wise"
            ElseIf rbtnCustomerWise.IsChecked Then
                ReportType = "Customer Wise"
            End If
            clsCommon.MyExportToExcelGrid("Sale Register ( " + ReportType + ")", gvReport, arrHeader, Me.Text)


            '''' commented by priti
            '''' Code for Fast Export


            ''Dim strLocation As String = ""
            ''Dim strCustomer As String = ""
            ''Dim strCustType As String = ""
            ''Dim strs As String = String.Empty
            ''Dim dRow As DataRow

            ''Dim filePath As String = "C:\\ERPTempFolder\Sale Register.xls"
            ''Dim writer As New IO.StreamWriter(IO.File.Create(filePath))

            ''strs = "Report Name  :  Sale Register Report"
            ''strs += vbNewLine
            ''writer.Write(strs)
            ''writer.Flush()

            ''strs = ""

            '''''''' For Date Range
            ''strs = "Date Range    :  " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "    To   " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            ''strs += vbNewLine
            ''writer.Write(strs)
            ''writer.Flush()

            ''strs = ""

            '''''''' For location Filter
            ''If chkLocSelect.IsChecked Then

            ''    For Each Str As String In cbgLocation.CheckedDisplayMember
            ''        If clsCommon.myLen(strLocation) > 0 Then
            ''            strLocation += ", "
            ''        End If
            ''        strLocation += Str
            ''    Next
            ''    If strLocation = "" Then
            ''        strLocation = "All"
            ''    End If
            ''    strs = "Location :          " + strLocation
            ''    strs += vbNewLine
            ''    writer.Write(strs)
            ''    writer.Flush()
            ''End If

            ''strs = ""

            '''''''' For Customer Filter
            ''If chkCustomerSelect.IsChecked Then

            ''    For Each Str As String In cbgCustomer.CheckedDisplayMember
            ''        If clsCommon.myLen(strCustomer) > 0 Then
            ''            strCustomer += ", "
            ''        End If
            ''        strCustomer += Str
            ''    Next
            ''    If strCustomer = "" Then
            ''        strCustomer = "All"
            ''    End If
            ''    strs = "Customer :          " + strCustomer
            ''    strs += vbNewLine
            ''    writer.Write(strs)
            ''    writer.Flush()
            ''End If

            ''strs = ""

            '''''''' For Customer Type Filter
            ''If chkcategoryselect.IsChecked Then

            ''    For Each Str As String In cbgcategory.CheckedDisplayMember
            ''        If clsCommon.myLen(strCustType) > 0 Then
            ''            strCustType += ", "
            ''        End If
            ''        strCustType += Str
            ''    Next
            ''    If strCustType = "" Then
            ''        strCustType = "All"
            ''    End If
            ''    strs = "Customer Type :          " + strCustType
            ''    strs += vbNewLine
            ''    writer.Write(strs)
            ''    writer.Flush()
            ''End If

            ''strs = ""

            '''''''' For header
            ''For i As Integer = 0 To gvReport.Columns.Count - 1
            ''    If gvReport.Columns(i).IsVisible = True Then
            ''        strs += gvReport.Columns(i).HeaderText.ToString & vbTab
            ''    End If
            ''Next
            ''strs += vbNewLine
            ''writer.Write(strs)
            ''writer.Flush()


            ''''''''' to export data
            ''For ii As Integer = 0 To gvReport.Rows.Count - 1
            ''    strs = ""
            ''    For col As Integer = 0 To gvReport.Columns.Count - 1
            ''        If gvReport.Columns(col).IsVisible = True Then
            ''            strs += gvReport.Rows(ii).Cells(col).Value.ToString & vbTab
            ''        End If
            ''    Next
            ''    strs += vbNewLine
            ''    writer.Write(strs)
            ''    writer.Flush()
            ''Next
            ''strs = ""


            '''''''' to export grid footer
            ''For col As Integer = 0 To gvReport.Columns.Count - 1
            ''    Dim summary As Object
            ''    If gvReport.Columns(col).IsVisible Then
            ''        For ii As Integer = 0 To gvReport.MasterTemplate.SummaryRowsBottom(0).Count - 1
            ''            If clsCommon.CompairString(gvReport.Columns(col).Name, gvReport.MasterTemplate.SummaryRowsBottom(0)(ii).Name) = CompairStringResult.Equal Then
            ''                Dim summaryItem As GridViewSummaryItem = gvReport.SummaryRowsBottom(0)(ii)
            ''                summary = summaryItem.Evaluate(gvReport.MasterTemplate)
            ''                Exit For
            ''            End If
            ''        Next
            ''    End If
            ''    strs += summary & vbTab
            ''    summary = ""
            ''Next
            ''strs += vbNewLine
            ''writer.Write(strs)
            ''writer.Flush()


            ' ''Close the stream writer object
            ''writer.Close()

            ''Dim xlsApp As Microsoft.Office.Interop.Excel.ApplicationClass
            ''Dim xlsWB As Microsoft.Office.Interop.Excel.WorkbookClass
            ''xlsApp = New Microsoft.Office.Interop.Excel.ApplicationClass
            ''xlsApp.Visible = True
            ''xlsWB = xlsApp.Workbooks.Open("C:\\ERPTempFolder\Sale Register.xls")

            '''' comments ends here


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try

    End Sub

    Private Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            If rbtnSelectCompany.IsChecked Then
                strTemp = ""
                For ii As Integer = 0 To gvDB.Rows.Count - 1
                    If clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value) Then
                        Dim Str As String = clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompName).Value)
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str
                    End If


                Next
                arrHeader.Add("Company : " + strTemp)
            End If
            If chkcategoryselect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgcategory.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer Category : " + strTemp)
            End If
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location Segment : " + strTemp)
            End If
            If chkCustomerSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer : " + strTemp)
            End If
            If chktempselect.IsChecked Then
                strTemp = ""
                For Each Str As String In cgvtemplate.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Template : " + strTemp)
            End If
            Dim ReportType As String = ""
            If rbtnItemWise.IsChecked Then
                ReportType = "Item Wise"
            ElseIf rbtnInvoiceWise.IsChecked Then
                ReportType = "Invocie Wise"
            ElseIf rbtnCustomerWise.IsChecked Then
                ReportType = "Customer Wise"
            End If
            clsCommon.MyExportToExcel("Sale Register ( " + ReportType + ")", gvReport, arrHeader, Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportIDNew()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvReport.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Function GetReportIDNew() As String
        Dim str As String = ""
        If rbtnItemWise.IsChecked Then
            str = ReportID + "I"
        ElseIf rbtnInvoiceWise.IsChecked Then
            str = ReportID + "D"
        ElseIf rbtnCustomerWise.IsChecked Then
            str = ReportID + "S"
        End If
        Return str
    End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportIDNew(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportIDNew(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub

    Private Sub gvReport_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvReport.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub cbgcategory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgcategory.Load

    End Sub

    Private Sub chkcategoryselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryselect.ToggleStateChanged

    End Sub

    Private Sub chkReconcile_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReconcile.ToggleStateChanged
        If chkReconcile.Checked Then
            rbtnInvoiceWise.IsChecked = True

        End If
    End Sub

    Private Sub gvReport_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvReport.DoubleClick
        Try
            If rbtnInvoiceWise.IsChecked Then
                Dim strDocNo As String = clsCommon.myCstr(gvReport.CurrentRow.Cells("Sale_Invoice_No").Value)
                Dim strTransCode As String = clsCommon.myCstr(gvReport.CurrentRow.Cells("DocType").Value)
                If clsCommon.myLen(strDocNo) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    If clsCommon.CompairString(strTransCode, "SD-IN") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDocNo)
                    ElseIf clsCommon.CompairString(strTransCode, "SD-SR") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strDocNo)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class

