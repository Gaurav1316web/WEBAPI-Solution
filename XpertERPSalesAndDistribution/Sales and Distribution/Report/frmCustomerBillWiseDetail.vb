'--------------------BM00000003413
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.IO

'----------------BM00000003385

Public Class frmCustomerBillWiseDetail
    Inherits FrmMainTranScreen
    Friend WithEvents gv1 As New common.UserControls.MyRadGridView

    Public Sub loadCustomerCode()
        Dim qry11 As String = "SELECT  Cust_Code,Customer_Name FROM TSPL_CUSTOMER_MASTER where Parent_Customer_YN='N'"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            qry11 += " and tspl_customer_master.cust_code in (" + objCommonVar.strCurrUserCustomers + ")"
        End If
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerBillWiseDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        tvCategory.ExpandAll()
    End Sub
    Private Sub CustomerBillWiseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadCustomerCode()
        LoadLocation()
        LoadCategory()
        reset()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Loc_Segment_Code , Location_desc  from TSPL_LOCATION_MASTER where 1=1 "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_desc"
    End Sub
    Private Sub reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkChapterAll.IsChecked = True
        MyRadioButton4.IsChecked = True
        cbgCustomer.CheckedAll()
        cbgLocation.CheckedAll()
        cbgCustomer.Enabled = False
        cbgLocation.Enabled = False
        rbtnCategoryAll.IsChecked = True
        chkcatewise.Checked = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "Select '" & Format(dtpFrmDate.Value(), "dd/MM/yyyy").ToString() + " To " + Format(dtpToDate.Value(), "dd/MM/yyyy").ToString() & "' as dtrng, '" & objCommonVar.CurrentUser & "' as User_Name ,  tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, case when ISNULL(TSPL_COMPANY_MASTER.Add1,'')<>'' then TSPL_COMPANY_MASTER.Add1 else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Add2,'')<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end +case when ISNULL(TSPL_COMPANY_MASTER.Add3,'')<>'' then ','+TSPL_COMPANY_MASTER.Add3 else '' end as'address', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, isnull(TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,'')  as Po_no,case when isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Sale_return_No,'')='' then TSPL_Customer_Invoice_Head.Document_No  when  isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' then  TSPL_Customer_Invoice_Head.Against_Sale_return_No else TSPL_Customer_Invoice_Head.Against_Sale_No end   as Ref_No,  TSPL_Customer_Invoice_Head.Due_Date as Due_On,TSPL_Customer_Invoice_Head.Document_Date as Bill_Date, TSPL_Customer_Invoice_Head.Document_Total as Open_amt, TSPL_Customer_Invoice_Head.Balance_Amt as Pending_Amt, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,case when ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,'')='' then replace(TSPL_CUSTOMER_MASTER.Add1+','+TSPL_CUSTOMER_MASTER.Add2+',' +TSPL_CUSTOMER_MASTER.Add3,',,',',') else replace(TSPL_SHIP_TO_LOCATION.Add1 +',' + TSPL_SHIP_TO_LOCATION.Add2 + ',' + TSPL_SHIP_TO_LOCATION.Add3 + ',' +TSPL_SHIP_TO_LOCATION.Add4,',,',',' ) end   as InvAddress , TSPL_CUSTOMER_MASTER.Parent_Customer_No, isnull(Parent.Customer_Name,'') as ParentName from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code   LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent ON Parent.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No  left outer join tspl_company_master on tspl_company_master.comp_code=tspl_customer_invoice_head.Comp_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_to_code=TSPL_SD_SALE_INVOICE_HEAD.ship_to_location    "
            ' qry = "Select tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, case when ISNULL(TSPL_COMPANY_MASTER.Add1,'')<>'' then TSPL_COMPANY_MASTER.Add1 else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Add2,'')<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end +case when ISNULL(TSPL_COMPANY_MASTER.Add3,'')<>'' then ','+TSPL_COMPANY_MASTER.Add3 else '' end as'address', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, '" + Format(dtpFrmDate.Value(), "dd/MM/yyyy").ToString() + " To " + Format(dtpToDate.Value(), "dd/MM/yyyy").ToString() + "' as dtrng, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Po_no,TSPL_Customer_Invoice_Head.Document_No as Ref_No,  TSPL_Customer_Invoice_Head.Due_Date as Due_On,TSPL_Customer_Invoice_Head.Document_Date as Bill_Date, TSPL_Customer_Invoice_Head.Document_Total as Open_amt, TSPL_Customer_Invoice_Head.Balance_Amt as Pending_Amt, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CUSTOMER_MASTER.Parent_Customer_No, Parent.Customer_Name as ParentName from  TSPL_Customer_Invoice_Head "
            'qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code "
            'qry += "  LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Document_No "
            'qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent ON Parent.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No  left outer join tspl_company_master on tspl_company_master.comp_code=tspl_customer_invoice_head.Comp_Code where "


            If (cbgCustomer.CheckedValue IsNot Nothing AndAlso cbgCustomer.CheckedValue.Count > 0) Then
                strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If (cbgLocation.CheckedDisplayMember IsNot Nothing AndAlso cbgLocation.CheckedDisplayMember.Count > 0) Then
                strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedDisplayMember) + ") "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
                End If
            End If

            strDateRange = " where CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strLoc
            qry += strCustomer
            qry += " ORDER BY TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Cust_Code "

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptCustBillWise", "Customer Bill Wise Detail")
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgLocation.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Location")
            cbgLocation.Focus()
            Exit Sub
        End If
        print()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
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
            cbgLocation.UnCheckedAll()
            cbgLocation.Enabled = True
        Else
            cbgLocation.UnCheckedAll()
            cbgLocation.Enabled = False
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
            cbgLocation.CheckedAll()
            cbgLocation.Enabled = False
        Else
            cbgLocation.UnCheckedAll()

            cbgLocation.Enabled = True
        End If
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "Start Date Can Not Be Greater Then End Date", Me.Text)
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgLocation.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Location")
            cbgLocation.Focus()
            Exit Sub
        End If

        If chkcatewise.Checked Then
            CategoryWiseData()
        Else
            RefreshData()
        End If

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub RefreshData()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "Select '" & Format(dtpFrmDate.Value(), "dd/MM/yyyy").ToString() + " To " + Format(dtpToDate.Value(), "dd/MM/yyyy").ToString() & "' as dtrng,  tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, case when ISNULL(TSPL_COMPANY_MASTER.Add1,'')<>'' then TSPL_COMPANY_MASTER.Add1 else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Add2,'')<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end +case when ISNULL(TSPL_COMPANY_MASTER.Add3,'')<>'' then ','+TSPL_COMPANY_MASTER.Add3 else '' end as'address', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, isnull(TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,'')  as Po_no,case when isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Sale_return_No,'')='' then TSPL_Customer_Invoice_Head.Document_No  when  isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' then  TSPL_Customer_Invoice_Head.Against_Sale_return_No else TSPL_Customer_Invoice_Head.Against_Sale_No end   as Ref_No,  TSPL_Customer_Invoice_Head.Due_Date as Due_On,TSPL_Customer_Invoice_Head.Document_Date as Bill_Date, TSPL_Customer_Invoice_Head.Document_Total as Open_amt, TSPL_Customer_Invoice_Head.Balance_Amt as Pending_Amt,TSPL_CUSTOMER_MASTER.Parent_Customer_No, isnull(Parent.Customer_Name,'') as ParentName ,TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,case when ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,'')='' then replace(TSPL_CUSTOMER_MASTER.Add1+','+TSPL_CUSTOMER_MASTER.Add2+',' +TSPL_CUSTOMER_MASTER.Add3,',,',',') else replace(TSPL_SHIP_TO_LOCATION.Add1 +',' + TSPL_SHIP_TO_LOCATION.Add2 + ',' + TSPL_SHIP_TO_LOCATION.Add3 + ',' +TSPL_SHIP_TO_LOCATION.Add4,',,',',' ) end   as InvAddress  from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code   LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent ON Parent.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No  left outer join tspl_company_master on tspl_company_master.comp_code=tspl_customer_invoice_head.Comp_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_to_code=TSPL_SD_SALE_INVOICE_HEAD.ship_to_location    "

            If (chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0) Then
                strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If (MyRadioButton3.IsChecked AndAlso cbgLocation.CheckedDisplayMember.Count > 0) Then
                strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedDisplayMember) + ") "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
                End If
            End If

            'If chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            '    'If chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            '    strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            'Else
            '    strCustomer = ""
            'End If
            'If MyRadioButton3.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '    strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'Else
            '    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            '        strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            '    End If
            'End If

            strDateRange = " where CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strLoc
            qry += strCustomer

            '-------------------------N-Level Category-------04/08/2014-----BM00000003354----------------------------------
            Dim whrcate As String = ""
            Dim Strr As String = Nothing

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True

                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            whrcate += " or "
                        End If

                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                Strr += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                Strr = ""
                Dim query As String = ""
                query = qry
                qry = ""

                whrcate = "and (" + whrcate + ")"

                Strr = "select distinct axs.Against_Sale_No,TSPL_SD_SALE_INVOICE_DETAIL.item_code,TSPL_ITEM_MASTER_CATEGORY.item_category_code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values "
                Strr += "from (select distinct TSPL_Customer_Invoice_Head.Against_Sale_No from TSPL_Customer_Invoice_Head " + strDateRange + " " + strLoc + " " + strCustomer + ")axs left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=axs.Against_Sale_No "
                Strr += "left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_sd_sale_invoice_detail.item_code "
                Strr += "where isnull(axs.Against_Sale_No,'')<>'' " + whrcate + ""

                qry = "select ada.*,(select distinct ','+a.Item_Category_Code,','+a.Item_Cagetory_Values from (" + Strr + ")a where a.Against_Sale_No=ada.ref_no  for XML path ('')) as Category"
                qry += " from (" + query + ")ada where ada.ref_no in (select distinct Against_Sale_No from (" + Strr + ")a)"
            End If

            '---------------------------------------------------------------------------------------------

            'qry += " ORDER BY TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Cust_Code "

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                Throw New Exception("No data found")
            End If
            gv.DataSource = dt
            FormatGrid()
            gv.MasterTemplate.AllowAddNewRow = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub CategoryWiseData()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "Select '" & Format(dtpFrmDate.Value(), "dd/MM/yyyy").ToString() + " To " + Format(dtpToDate.Value(), "dd/MM/yyyy").ToString() & "' as dtrng, TSPL_COMPANY_MASTER.Comp_Name as compname, case when ISNULL(TSPL_COMPANY_MASTER.Add1,'')<>'' then TSPL_COMPANY_MASTER.Add1 else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Add2,'')<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end +case when ISNULL(TSPL_COMPANY_MASTER.Add3,'')<>'' then ','+TSPL_COMPANY_MASTER.Add3 else '' end as'address', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, isnull(TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,'')  as Po_no,case when isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Sale_return_No,'')='' then TSPL_Customer_Invoice_Head.Document_No  when  isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' then  TSPL_Customer_Invoice_Head.Against_Sale_return_No else TSPL_Customer_Invoice_Head.Against_Sale_No end   as Ref_No,  TSPL_Customer_Invoice_Head.Due_Date as Due_On,TSPL_Customer_Invoice_Head.Document_Date as Bill_Date, TSPL_Customer_Invoice_Head.Document_Total as Open_amt, TSPL_Customer_Invoice_Head.Balance_Amt as Pending_Amt,TSPL_CUSTOMER_MASTER.Parent_Customer_No, isnull(Parent.Customer_Name,'') as ParentName ,TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,case when ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,'')='' then replace(TSPL_CUSTOMER_MASTER.Add1+','+TSPL_CUSTOMER_MASTER.Add2+',' +TSPL_CUSTOMER_MASTER.Add3,',,',',') else replace(TSPL_SHIP_TO_LOCATION.Add1 +',' + TSPL_SHIP_TO_LOCATION.Add2 + ',' + TSPL_SHIP_TO_LOCATION.Add3 + ',' +TSPL_SHIP_TO_LOCATION.Add4,',,',',' ) end   as InvAddress  from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code   LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent ON Parent.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No  left outer join tspl_company_master on tspl_company_master.comp_code=tspl_customer_invoice_head.Comp_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_to_code=TSPL_SD_SALE_INVOICE_HEAD.ship_to_location    "

            If chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""
            End If
            If MyRadioButton3.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
                End If
            End If

            strDateRange = " where CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strLoc
            qry += strCustomer

            '-------------------------N-Level Category-------04/08/2014-----BM00000003354----------------------------------
            Dim whrcate As String = ""
            Dim Strr As String = Nothing
            Dim PivotHeader As String = ""

            Strr = "select distinct (select distinct ',['+ITEM_CATEGORY_CODE+']' from TSPL_ITEM_CATEGORY_LEVEL where category_level='1' for xml path('')) as xvalue"
            PivotHeader = clsDBFuncationality.getSingleValue(Strr)
            If clsCommon.myLen(PivotHeader) > 0 Then
                Try
                    If PivotHeader.Substring(0, 1) = "," Then
                        PivotHeader = PivotHeader.Substring(1, PivotHeader.Length - 1)
                    End If
                Catch exx As Exception

                End Try
            End If

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True

                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            whrcate += " or "
                        End If

                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                Strr += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                Strr = "select distinct (select distinct ',['+ITEM_CATEGORY_CODE+']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('')) as xvalue"
                PivotHeader = clsDBFuncationality.getSingleValue(Strr)
                If clsCommon.myLen(PivotHeader) > 0 Then
                    Try
                        If PivotHeader.Substring(0, 1) = "," Then
                            PivotHeader = PivotHeader.Substring(1, PivotHeader.Length - 1)
                        End If
                    Catch exx As Exception

                    End Try
                End If

                Strr = ""
                Dim query As String = ""
                query = qry
                qry = ""

                whrcate = "and (" + whrcate + ")"

                Strr = "select distinct axs.Against_Sale_No,TSPL_SD_SALE_INVOICE_DETAIL.item_code,TSPL_ITEM_MASTER_CATEGORY.item_category_code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values "
                Strr += "from (select distinct TSPL_Customer_Invoice_Head.Against_Sale_No from TSPL_Customer_Invoice_Head " + strDateRange + " " + strLoc + " " + strCustomer + ")axs left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=axs.Against_Sale_No "
                Strr += "left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_sd_sale_invoice_detail.item_code "
                Strr += "where isnull(axs.Against_Sale_No,'')<>'' " + whrcate + ""

                qry = "select * from (select aad.dtrng, aad.compname,aad.tel,aad.fax,aad.Po_no,aad.Ref_No,aad.Due_On,aad.Bill_Date,aad.Parent_Customer_No,aad.ParentName,aad.Cust_Code,aad.Customer_Name,aad.InvAddress ,aad.Open_amt,aad.Pending_Amt,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,sum(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt) as [Total Taxable Amt] from (" + query + ")aad left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=aad.ref_no left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code where 1=1 " + whrcate + " group by aad.dtrng, aad.compname,aad.tel,aad.fax,aad.Po_no,aad.Ref_No,aad.Due_On,aad.Bill_Date,aad.Open_amt,aad.Pending_Amt,aad.Cust_Code,aad.Customer_Name,aad.InvAddress ,aad.Parent_Customer_No,aad.ParentName,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values) as s pivot(max(Item_Cagetory_Values) for Item_Category_Code in (" + PivotHeader + "))t"
            Else
                qry = "select * from (select aad.dtrng, aad.compname,aad.tel,aad.fax,aad.Po_no,aad.Ref_No,aad.Due_On,aad.Bill_Date,aad.Parent_Customer_No,aad.ParentName,aad.Cust_Code,aad.Customer_Name,aad.InvAddress ,aad.Open_amt,aad.Pending_Amt,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,sum(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt) as [Total Taxable Amt] from (" + qry + ")aad left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=aad.ref_no left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code where TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code in (select distinct Item_Category_Code from TSPL_ITEM_CATEGORY_LEVEL where CATEGORY_LEVEL='1') group by aad.dtrng, aad.compname,aad.tel,aad.fax,aad.Po_no,aad.Ref_No,aad.Due_On,aad.Bill_Date,aad.Open_amt,aad.Pending_Amt,aad.Cust_Code,aad.Customer_Name,aad.InvAddress ,aad.Parent_Customer_No,aad.ParentName,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values) as s pivot(max(Item_Cagetory_Values) for Item_Category_Code in (" + PivotHeader + "))t"
            End If

            '---------------------------------------------------------------------------------------------

            'qry += " ORDER BY TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Cust_Code "

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                Throw New Exception("No data found")
            End If
            gv.DataSource = dt
            FormatGrid()
            gv.MasterTemplate.AllowAddNewRow = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()

        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.ShowFilteringRow = True

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            If ii > 14 Then
                gv.Columns(ii).IsVisible = True
                gv.Columns(ii).Width = 150
            End If
        Next
        gv.Columns("dtrng").IsVisible = True
        gv.Columns("dtrng").Width = 200
        gv.Columns("dtrng").HeaderText = "Period"
        gv.Columns("dtrng").FormatString = ""


        gv.Columns("po_no").IsVisible = True
        gv.Columns("po_no").Width = 120
        gv.Columns("po_no").HeaderText = "PO No"
        gv.Columns("po_no").FormatString = ""


        gv.Columns("ref_no").IsVisible = True
        gv.Columns("ref_no").Width = 120
        gv.Columns("ref_no").HeaderText = "Bill No"


        gv.Columns("due_on").IsVisible = True
        gv.Columns("due_on").Width = 100
        gv.Columns("due_on").HeaderText = "Due Date"
        gv.Columns("due_on").FormatString = "{0:d}"


        gv.Columns("bill_date").IsVisible = True
        gv.Columns("bill_date").Width = 100
        gv.Columns("bill_date").HeaderText = "Bill Date"
        gv.Columns("bill_date").FormatString = "{0:d}"


        gv.Columns("open_amt").Width = 120
        gv.Columns("open_amt").HeaderText = " Bill Amount"
        gv.Columns("open_amt").IsVisible = True

        gv.Columns("pending_amt").IsVisible = True
        gv.Columns("pending_amt").Width = 120
        gv.Columns("pending_amt").HeaderText = " Pending Amount"

        gv.Columns("Parent_Customer_No").IsVisible = True
        gv.Columns("Parent_Customer_No").Width = 120
        gv.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

        gv.Columns("ParentName").IsVisible = True
        gv.Columns("ParentName").Width = 200
        gv.Columns("ParentName").HeaderText = " Parent Name"



        gv.Columns("cust_code").IsVisible = True
        gv.Columns("cust_code").Width = 120
        gv.Columns("cust_code").HeaderText = " Customer Code"

        gv.Columns("customer_name").IsVisible = True
        gv.Columns("customer_name").Width = 200
        gv.Columns("customer_name").HeaderText = " Customer Name"

        gv.Columns("invaddress").IsVisible = True
        gv.Columns("invaddress").Width = 200
        gv.Columns("invaddress").HeaderText = " Address"

        gv.Columns("parent_customer_no").IsVisible = True
        gv.Columns("parent_customer_no").Width = 120
        gv.Columns("parent_customer_no").HeaderText = " Parent Code"

        gv.Columns("parentname").IsVisible = True
        gv.Columns("parentname").Width = 200
        gv.Columns("parentname").HeaderText = " Parent Name"

        Try
            gv.Columns("Category").IsVisible = True
            gv.Columns("Category").Width = 200
            gv.Columns("Category").HeaderText = "Category"
        Catch exx As Exception
        End Try

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("open_amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Pending_amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("Customer_Code as Item format ""{0}: {1}"" Group By Customer_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("Location_Desc as Item format ""{0}: {1}"" Group By Location_Desc"))


        gv.ShowGroupPanel = False
        'gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ReStoreGridLayout()
    End Sub

    Private Sub btnsavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavelayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btndeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletelayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnexport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim arr As New List(Of String)()
        arr.Add("Customer Bill Wise Detail")
        arr.Add(" From :  " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MM/yyyy") + "  To : " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
        If gv.Rows.Count > 0 Then
            clsCommon.MyExportToExcelGrid("Customer Bill wise Detail", gv, arr, "Detail")
        Else
            clsCommon.MyMessageBoxShow("No data found.")
        End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Dim arr As New List(Of String)()
        arr.Add("Customer Bill Wise Detail")
        arr.Add(" From :  " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MM/yyyy") + "  To : " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
        If gv.Rows.Count > 0 Then
            clsCommon.MyExportToPDF("Customer Bill wise Detail", gv, arr, "Detail")
        Else
            clsCommon.MyMessageBoxShow("No data found.")
        End If
    End Sub
End Class
