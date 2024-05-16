''''              Modified by = Priti (24/12/2012)11:40 AM
'''' ''''          Modified by = Priti (27/11/2013)05:20 PM

Imports common
Imports XpertERPEngine
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML

Public Class FrmRptEmptyTransaction
    Inherits FrmMainTranScreen

    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value
        SetUserMgmtNew()
        Loadlocation()
        LoadCompany()
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        LoadTransType()
        LoadDocumentType()
        chkDocAll.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


        ButtonToolTip.SetToolTip(RadButton3, "Press Alt+P Print")
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        rdbSummary.IsChecked = True
        rdbSKU.IsChecked = True
        grpSelect.Visible = False
    End Sub
    Sub LoadTransType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "In"
        dr("Name") = "In"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Out"
        dr("Name") = "Out"
        dt.Rows.Add(dr)

        cboTransType.DataSource = dt
        cboTransType.ValueMember = "Code"
        cboTransType.DisplayMember = "Name"
    End Sub
    Sub LoadDocumentType()
        Dim strquery As String = "select 'SI' as code,'Sale Invoice' as Name union " & _
        "select 'SR' as code,'Sale Return' as Name union  " & _
        "select 'SR-IC' as code,'Sale Return InterCompany' as Name union " & _
        "select 'AD-IN' as code,'Adjustment In' as Name union " & _
        "select 'AD-Out' as code,'Adjustment Out' as Name union " & _
        "select 'LI' as code,'Load In' as Name union " & _
        "select 'LO' as code,'Load Out' as Name union " & _
        "select 'PI' as code,'Purchase Invoice' as Name union " & _
        "select 'PR' as code,'Purchase Return' as Name "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Name"
    End Sub
    Sub LoadCompany()
        Dim qry As String = " SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
        cbgCompany.DisplayMember = "Comp_Name"
    End Sub
    Sub LoadSalesPerson()
        Dim qry As String = "Select EMP_CODE as [SalesPerson Code],Emp_Name as [SalesPerson Name] from TSPL_EMPLOYEE_MASTER"
        cbgSalesPerson.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesPerson.ValueMember = "SalesPerson Code"
        cbgSalesPerson.DisplayMember = "SalesPerson Name"
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub
    Private Sub ChkSalesAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkSalesAll.ToggleStateChanged
        cbgSalesPerson.Enabled = Not ChkSalesAll.IsChecked
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmEmptyTransactionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            
        End If
    End Sub

    Sub Loadlocation()
        cbgLocationSegment.DataSource = clsLocation.GetLocationSegments()
        cbgLocationSegment.ValueMember = "Code"
        cbgLocationSegment.DisplayMember = "Name"
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(0)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            gv1.EnableFiltering = True
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

   

    Private Sub LoadData()
        If chkLocationSegSelect.IsChecked AndAlso cbgLocationSegment.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Location Segment")
        End If
        If cbgCompany.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Company")
        End If
        Dim strSQL1Group, strOrderColumn, itemcount As String
        strOrderColumn = ""
        strSQL1Group = ""
        itemcount = ""
        If rdbSKU.IsChecked Then
            strOrderColumn = " TSPL_ITEM_MASTER.Sku_Seq"
            strSQL1Group = "tspl_item_details.item_code"
        ElseIf rdbPack.IsChecked Then
            strOrderColumn = " TSPL_ITEM_MASTER.Pack_Seq"
            strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
        ElseIf rdbFlavour.IsChecked Then
            strOrderColumn = " TSPL_ITEM_MASTER.Flavour_Seq"
            strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  "
        End If

        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")

        Dim strFromDateWithTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt")
        Dim strToDateWithTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt")

        Dim BaseQry As String = "--Sale Invoice " + Environment.NewLine

        BaseQry += " select TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id, " + strOrderColumn + " as  OrderBy," & strSQL1Group & " as grouping,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_SALE_INVOICE_HEAD.DESCRIPTION,TSPL_SALE_INVOICE_HEAD.Remarks," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No,-1 as RI,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty else 0 end ) as ECQty ,"
        BaseQry += " (case when   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value_Bottle+" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value_Shell else 0 end ) as ECRate ,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value else 0 end ) as ECAmt,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty else 0 end ) as EBQty,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value_Bottle+" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value_Shell else 0 end ) as EBRate,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value else 0 end ) as EBAmt "
        BaseQry += " , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shell_Qty   as SHQty"
        BaseQry += " ,100 as SHRate"
        BaseQry += " ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shell_Qty*100 as SHAmt,'SI' as TC,'Filled' as UNITTYPE,'Sale Invoice' as Document"
        BaseQry += " ,(select sum(Amount) from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='SD-IN' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A' ) as GLContainerDepositAmt"
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code"
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code "
        BaseQry += " where 2=2 and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value>0 and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + strFromDate + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + strToDate + "' "
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"
        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If
        If chkCustomerSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If
        If chlSalesSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ")"
        End If



        BaseQry += Environment.NewLine + " union all " + Environment.NewLine

        BaseQry += " --Sale Return " + Environment.NewLine
        BaseQry += " select TSPL_SALE_RETURN_DETAIL.Sale_Return_Id," + strOrderColumn + " as  OrderBy," & strSQL1Group & " as grouping,TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_SALE_RETURN_HEAD.Description,TSPL_SALE_RETURN_HEAD.Comments," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No as DocNo," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date as DocDate," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Name," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Vehicle_No,1 as RI,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty else 0 end ) as ECQty ,"
        BaseQry += " (case when   " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value_Bottle+" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value_Shell else 0 end ) as ECRate ,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value else 0 end ) as ECAmt,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty else 0 end ) as EBQty,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value_Bottle+" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value_Shell else 0 end ) as EBRate,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value else 0 end ) as EBAmt "
        BaseQry += " , 0   as SHQty"
        BaseQry += " ,100 as SHRate"
        BaseQry += " ,0 as SHAmt,'SR' as TC,'Filled' as UNITTYPE,'Sale Return' as Document "
        BaseQry += " ,(select sum(Amount) from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='SD-SR' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A') as GLContainerDepositAmt"
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code"
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code "
        BaseQry += " where 2=2 and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value>0 and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date>='" + strFromDateWithTime + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date<='" + strToDateWithTime + "' "
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If
        If chkCustomerSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If
        If chlSalesSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ")"
        End If


        BaseQry += Environment.NewLine + " union all " + Environment.NewLine

        BaseQry += " --Sale Return Inter Company" + Environment.NewLine
        BaseQry += " select TSPL_SALE_RETURN_INTER_DETAIL.Line_No," + strOrderColumn + " as  OrderBy," & strSQL1Group & " as grouping,TSPL_SALE_RETURN_INTER_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_SALE_RETURN_INTER_HEAD.Description,TSPL_SALE_RETURN_INTER_HEAD.Remarks," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No as DocNo," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date as DocDate," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Name ," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Vehicle_No,1 as RI,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty else 0 end ) as ECQty ,"
        BaseQry += " (case when   " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value_Bottle+" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value_Shell else 0 end ) as ECRate ,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value else 0 end ) as ECAmt,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty else 0 end ) as EBQty,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value_Bottle+" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value_Shell else 0 end ) as EBRate,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value else 0 end ) as EBAmt "
        BaseQry += " , 0   as SHQty"
        BaseQry += " ,100 as SHRate"
        BaseQry += " ,0 as SHAmt,'SR-IC' as TC,'Filled' as UNITTYPE,'Sale Return' as Document "
        BaseQry += " ,(select sum(Amount) from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='SD-SR' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No  and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A') as GLContainerDepositAmt"
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code"
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code "
        BaseQry += " where 2=2 and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value>0 and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Is_Post='1' "
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date>='" + strFromDateWithTime + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date<='" + strToDateWithTime + "' "
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If
        If chkCustomerSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If
        If chlSalesSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ")"
        End If


        BaseQry += Environment.NewLine + " union all " + Environment.NewLine

        BaseQry += " --Adjustment " + Environment.NewLine
        BaseQry += " select TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No," + strOrderColumn + " as  OrderBy," & strSQL1Group & " as grouping,TSPL_ADJUSTMENT_DETAIL.Item_Code ,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_ADJUSTMENT_HEADER.Description,TSPL_ADJUSTMENT_HEADER.Reference," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Location_Code as Location," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,convert(date," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate ,'' as Route_No,'' as Route_Desc," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE as Cust_Code," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as Cust_Name," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Vehicle_No,(case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Trans_Type='In' then 1 else -1 end) as RI , "
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Quantity else 0 end ) as ECQty,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.mrp else 0 end ) as ECRate,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost else 0 end ) as ECAmt,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='EB' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Quantity else 0 end ) as EBQty,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='EB' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.mrp else 0 end ) as EBRate,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='EB' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost else 0 end ) as EBAmt,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='SH' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Quantity else 0 end ) as SHQty, "
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='SH' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.mrp else 0 end ) as SHRate,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Unit_Code='SH' then " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost else 0 end ) as SHAmt,"
        BaseQry += " 'AD-'+" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Trans_Type as TC ,'Empty' as UNITTYPE ,'Adjustment' as Document"
        BaseQry += " ,(select sum(Amount) from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='IC-AD' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A') as GLContainerDepositAmt"
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER on " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Location_Code"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE"
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code "
        BaseQry += " where 2=2 and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType in ('E')  and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No is not null"
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Posted='Y'"
        BaseQry += " and convert(date," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>='" + strFromDate + "' and convert(date," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<='" + strToDate + "' and Reference_Document <>'Load out/Transfer'"
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If
        If chkCustomerSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If
        If chlSalesSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ")"
        End If



        BaseQry += Environment.NewLine + " union all " + Environment.NewLine

        BaseQry += " --Transfer " + Environment.NewLine
        BaseQry += " select TSPL_TRANSFER_DETAIL.Line_No," + strOrderColumn + " as  OrderBy," & strSQL1Group & " as grouping,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_TRANSFER_HEAD.description,TSPL_TRANSFER_HEAD.Reference,(case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location else  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location end) as Location," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No as DocNo," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date as DocDate," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc,'' as  Cust_Code,'' as Cust_Name,''as Salesman_Code,'' as SalesmanName," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Vehicle_Desc as Vehicle_No,(case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LI' then 1 else -1 end) as RI,   "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in ('FC','EC') then ( case when  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Leak end ) else 0 end ) as ECQty ,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in ('FC','EC') then (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Item_Type='Empty' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value end) else 0 end ) as ECRate ,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in ('FC','EC') then (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Item_Type='Empty' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value end) *( case when  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Leak end ) else 0 end ) as ECAmt,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in ('FB','EB') then ( case when  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Leak end ) else 0 end ) as EBQty,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in ('FB','EB') then (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Item_Type='Empty' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value end)  else 0 end ) as EBRate,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in ('FB','EB') then (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Item_Type='Empty' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value end) *( case when  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Leak end ) else 0 end ) as EBAmt,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ='SH' then ( case when  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Leak end ) else 0 end ) as SHQty,"
        BaseQry += " 100 as SHRate,"
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ='SH' then ( case when  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty else " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty+" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Leak end ) else 0 end )*100 as SHAmt, "
        BaseQry += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type as TC,(case when TSPL_TRANSFER_HEAD.Item_Type='Empty' then 'Empty' else 'Filled' end) as UNITTYPE,'Transfer' as Document "
        BaseQry += "  ,(select sum(Amount) from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='MM-TF' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A') as GLContainerDepositAmt"
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No= " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=(case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' then " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location else  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location end)"
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code "
        BaseQry += " where " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Location_Type='Physical'  and (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value>0  or  (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom in('SH','EB','EC') and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax>0))  and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Post='Y'"
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date>='" + strFromDate + "' and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date<='" + strToDate + "' "
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If



        BaseQry += Environment.NewLine + " union all " + Environment.NewLine

        BaseQry += " --Purchase Invoice" + Environment.NewLine
        BaseQry += " select  TSPL_PI_DETAIL.line_no," + strOrderColumn + " as  OrderBy," & strSQL1Group & " as grouping,TSPL_PI_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_PI_HEAD.Description,TSPL_PI_HEAD.Remarks," + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.Bill_To_Location as Location," + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.PI_No  as DocNo," + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.PI_Date as DocDate,'' as Route_No,'' as  Route_Desc ," + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.Vendor_Code as Cust_Code ," + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.Vendor_Name as Cust_Name ,'' as  Salesman_Code,'' as SalesManName,'' as  Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.VehicleNo as Vehicle_No,1 as RI,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_Qty  else 0 end ) as ECQty , "
        BaseQry += " (case when   " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount/" + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_Qty else 0 end ) as ECRate , "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount else 0 end ) as ECAmt, "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_Qty else 0 end ) as EBQty, "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount/" + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_Qty else 0 end ) as EBRate, "
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount else 0 end ) as EBAmt  , "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='SH' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_Qty else 0 end ) as SHQty, "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='SH' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount/" + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_Qty else 0 end ) as SHRate, "
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Unit_code='SH' then " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount else 0 end ) as SHAmt  , "
        BaseQry += " 'PI' as TC,'Filled' as UNITTYPE,'Purchase Invoice' as Document ,"
        BaseQry += " (select sum(Amount) "
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_VENDOR_INVOICE_HEAD on  " + clsCommon.ReplicateDBString + "TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=" + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.PI_No"
        BaseQry += " where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='AP-IN' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_VENDOR_INVOICE_HEAD.Document_No and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A' ) as GLContainerDepositAmt "
        BaseQry += " from  " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL "
        BaseQry += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_PI_HEAD on " + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.PI_No=" + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.PI_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.Bill_To_Location "
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_PI_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_PI_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_PI_DETAIL.Item_Code "
        BaseQry += " where(2 = 2 And " + clsCommon.ReplicateDBString + "TSPL_PI_DETAIL.Empty_Amount > 0 And " + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.Status = 1)"
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.PI_Date>='" + strFromDateWithTime + "' and " + clsCommon.ReplicateDBString + "TSPL_PI_HEAD.PI_Date<='" + strToDateWithTime + "'"
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If


        BaseQry += Environment.NewLine + " union all " + Environment.NewLine


        BaseQry += " --Purchase Return" + Environment.NewLine
        BaseQry += " select tspl_pr_detail.line_no," + strOrderColumn + " as  OrderBy, " & strSQL1Group & " as grouping,TSPL_PR_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,TSPL_PR_HEAD.Description,TSPL_PR_HEAD.Remarks," + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.Bill_To_Location as Location," + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.PR_No  as DocNo," + clsCommon.ReplicateDBString + "TSPL_PR_HEAD. PR_Date as DocDate,'' as Route_No,'' as  Route_Desc ," + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.Vendor_Code as Cust_Code ," + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.Vendor_Name as Cust_Name ,'' as  Salesman_Code,'' as SalesManName,'' as  Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.VehicleNo as Vehicle_No,-1 as RI,"
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_Qty  else 0 end ) as ECQty , "
        BaseQry += " (case when   " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount/" + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_Qty else 0 end ) as ECRate , "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount else 0 end ) as ECAmt, "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_Qty else 0 end ) as EBQty, "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount/" + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_Qty else 0 end ) as EBRate, "
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='FB' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount else 0 end ) as EBAmt  , "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='SH' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_Qty else 0 end ) as SHQty, "
        BaseQry += " (case when " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='SH' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount/" + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_Qty else 0 end ) as SHRate, "
        BaseQry += " (case when  " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Unit_code='SH' then " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount else 0 end ) as SHAmt  , "
        BaseQry += " 'PR' as TC,'Filled' as UNITTYPE,'Purchase Return' as Document ,"
        BaseQry += " (select sum(Amount) "
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER on " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Voucher_No=" + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Voucher_No "
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_VENDOR_INVOICE_HEAD on  " + clsCommon.ReplicateDBString + "TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No=" + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.PR_No"
        BaseQry += " where " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Code='AP-IN' and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Source_Doc_No=" + clsCommon.ReplicateDBString + "TSPL_VENDOR_INVOICE_HEAD.Document_No and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_DETAILS.Account_code='410003-'+" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code and " + clsCommon.ReplicateDBString + "TSPL_JOURNAL_MASTER.Authorized='A' ) as GLContainerDepositAmt "
        BaseQry += " from " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL "
        BaseQry += " left outer join  " + clsCommon.ReplicateDBString + "TSPL_PR_HEAD on " + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.PR_No=" + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.PR_No"
        BaseQry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.Bill_To_Location "
        BaseQry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_PR_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on TSPL_ITEM_DETAILS_1.Item_Code=TSPL_PR_DETAIL.Item_Code "
        BaseQry += " left outer join TSPL_ITEM_Master on TSPL_ITEM_Master.Item_Code=TSPL_PR_DETAIL.Item_Code "
        BaseQry += " where(2 = 2 And " + clsCommon.ReplicateDBString + "TSPL_PR_DETAIL.Empty_Amount > 0 And " + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.Status = 1)"
        BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.PR_Date>='" + strFromDateWithTime + "' and " + clsCommon.ReplicateDBString + "TSPL_PR_HEAD.PR_Date<='" + strToDateWithTime + "'"
        BaseQry += " and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        If chkLocationSegSelect.IsChecked Then
            BaseQry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + ")"
        End If

        BaseQry = clsCommon.GetQueryWithAllSelectedDataBase(BaseQry, cbgCompany.CheckedValue, False)
        BaseQry = BaseQry & " where 2=2 "
        If chkDocSelect.IsChecked Then
            BaseQry += " and TC in (" + clsCommon.GetMulcallString(cbgDocument.CheckedValue) + ")"
        End If

        If clsCommon.CompairString(cboTransType.SelectedValue, "All") <> CompairStringResult.Equal Then
            If clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal Then
                BaseQry += " and RI = 1"
            Else
                BaseQry += " and RI = -1"
            End If
        End If


        Dim qry As String = ""


        If rdbSummary.IsChecked Then


            qry = "select Location,DocNo,convert(varchar(11), DocDate,103) as DocDate,xxxx.Route_No,xxxx.Route_Desc, xxxx.Cust_Code,xxxx.Cust_Name,xxxx.Salesman_Code,xxxx.SalesmanName, xxxx.Vehicle_Code,xxxx.Vehicle_No, (RI*ECQty) as ECQty, ECRate, (RI*ECAmt) as ECAmt, (RI*EBQty) as EBQty, EBRate,(RI*EBAmt) as EBAmt,(RI*(SHQtySI+SHQty)) as SHQty,SHRate, (RI*(SHAmtSI+SHAmt)) as SHAmt,TC,(RI*(ECAmt+EBAmt+SHAmtSI+SHAmt)) as TotAmt,UNITTYPE,(Case when RI=1 then 'In' else case when RI=-1 then 'Out' else '' end end) as TRANSTYPE,Document,GLContainerDepositAmt "
            qry += " from("
            qry += " select MAX(Location) as Location,DocNo,MAX(DocDate) as DocDate,MAX(Route_No) as Route_No,MAX(Route_Desc) as Route_Desc,MAX(Cust_Code) as Cust_Code,MAX(Cust_Name)as Cust_Name ,MAX(Salesman_Code) as Salesman_Code,MAX(SalesManName) as SalesManName ,MAX(Vehicle_Code) as Vehicle_Code,MAX(Vehicle_No) as Vehicle_No ,SUM(ECQty) as ECQty,MAX(ECRate) as ECRate,sum(ECAmt) as ECAmt,SUM(EBQty) as EBQty,MAX(EBRate) as EBRate,sum(EBAmt) as EBAmt"
            qry += " ,max(case when TC='SI' then SHQty else 0 end) as SHQtySI,max(case when TC='SI' then SHAmt else 0 end) as SHAmtSI"
            qry += " ,sum(case when TC<>'SI' then SHQty else 0 end) as SHQty,MAX(SHRate) as SHRate,sum(case when TC<>'SI' then SHAmt else 0 end) as SHAmt,max(TC) as TC,max(UNITTYPE) as UNITTYPE,max(RI) as RI,max(Document) as Document,max(GLContainerDepositAmt) as GLContainerDepositAmt from "
            qry += " ( " + BaseQry + ")"
            qry += " xxx group by DocNo"
            qry += " )xxxx order by convert(date, DocDate,103) "
        ElseIf rdbDetail.IsChecked Then
            qry = "select Location,DocNo,convert(varchar(11), DocDate,103) as DocDate,xxxx.Route_No,xxxx.Route_Desc,Description,Remarks, xxxx.Cust_Code,xxxx.Cust_Name,xxxx.Salesman_Code,xxxx.SalesmanName, xxxx.Vehicle_Code,xxxx.Vehicle_No,Flavour,Pack, (RI*ECQty) as ECQty, ECRate, (RI*ECAmt) as ECAmt, (RI*EBQty) as EBQty, EBRate,(RI*EBAmt) as EBAmt,(RI*(case  when TC='SI' and Sale_Invoice_Id=1 then SHQtySI else 0 end+SHQty)) as SHQty,SHRate, (RI*(case  when TC='SI' and Sale_Invoice_Id=1 then SHAmtSI else 0 end+SHAmt)) as SHAmt,TC,(RI*(ECAmt+EBAmt+case  when TC='SI' and Sale_Invoice_Id=1 then SHAmtSI else 0 end+SHAmt)) as TotAmt,UNITTYPE,(Case when RI=1 then 'In' else case when RI=-1 then 'Out' else '' end end) as TRANSTYPE,Document,GLContainerDepositAmt "
            qry += " from("
            qry += " select Sale_Invoice_Id,Pack,Flavour,MAX(Description) as Description,MAX(Remarks) as Remarks,MAX(Location) as Location,DocNo,MAX(DocDate) as DocDate,MAX(Route_No) as Route_No,MAX(Route_Desc) as Route_Desc,MAX(Cust_Code) as Cust_Code,MAX(Cust_Name)as Cust_Name ,MAX(Salesman_Code) as Salesman_Code,MAX(SalesManName) as SalesManName ,MAX(Vehicle_Code) as Vehicle_Code,MAX(Vehicle_No) as Vehicle_No ,SUM(ECQty) as ECQty,MAX(ECRate) as ECRate,sum(ECAmt) as ECAmt,SUM(EBQty) as EBQty,MAX(EBRate) as EBRate,sum(EBAmt) as EBAmt"
            qry += " ,max(case when TC='SI' then SHQty else 0 end) as SHQtySI,max(case when TC='SI' then SHAmt else 0 end) as SHAmtSI"
            qry += " ,sum(case when TC<>'SI' then SHQty else 0 end) as SHQty,MAX(SHRate) as SHRate,sum(case when TC<>'SI' then SHAmt else 0 end) as SHAmt,max(TC) as TC,max(UNITTYPE) as UNITTYPE,max(RI) as RI,max(Document) as Document,max(GLContainerDepositAmt) as GLContainerDepositAmt from "
            qry += " ( " + BaseQry + ")"
            qry += " xxx group by DocNo,Pack,Flavour,Sale_Invoice_Id"
            qry += " )xxxx order by convert(date, DocDate,103) "
        ElseIf rdbRowwise.IsChecked Then
            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strsum As String
            strItemCode = ""
            strItemCodestring = ""
            strmainItemCodeString = ""
            strsum = ""
            If rdbSKU.IsChecked Then
                itemcount = " select  distinct Item_coDE,OrderBy  from (" + BaseQry + ") abc order by OrderBy "

            ElseIf rdbFlavour.IsChecked Then
                itemcount = " select  distinct grouping ,OrderBy from (" + BaseQry + ") abc order by OrderBy "

            ElseIf rdbPack.IsChecked Then
                itemcount = " select  distinct grouping,OrderBy  from (" + BaseQry + ") abc order by OrderBy "
            End If

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(itemcount)
            Dim arritem As New ArrayList
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr As DataRow In dt1.Rows
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                    arritem.Add(strItemCode)
                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    strsum = strsum & "  isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                Next
            End If

            If strItemCode <> "" Then
                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If


            qry = "select Sale_Invoice_Id,Description,Remarks,Item_Code,Location,DocNo,convert(varchar(11), DocDate,103) as DocDate,xxxx.Route_No,xxxx.Route_Desc, xxxx.Cust_Code,xxxx.Cust_Name,xxxx.Salesman_Code,xxxx.SalesmanName, xxxx.Vehicle_Code,xxxx.Vehicle_No, (RI*ECQty) as ECQty, ECRate, (RI*ECAmt) as ECAmt, (RI*EBQty) as EBQty, EBRate,(RI*EBAmt) as EBAmt,(RI*(case  when TC='SI' and Sale_Invoice_Id=1 then SHQtySI else 0 end+SHQty)) as SHQty,SHRate, (RI*(case  when TC='SI' and Sale_Invoice_Id=1 then SHAmtSI else 0 end+SHAmt)) as SHAmt,TC,(RI*(ECAmt+EBAmt+case  when TC='SI' and Sale_Invoice_Id=1 then SHAmtSI else 0 end+SHAmt)) as TotAmt,UNITTYPE,(Case when RI=1 then 'In' else case when RI=-1 then 'Out' else '' end end) as TRANSTYPE,Document,GLContainerDepositAmt "
            qry += " from("
            qry += " select Sale_Invoice_Id,MAX(Description) AS Description,MAX(Remarks) AS Remarks,Item_Code,MAX(Location) as Location,DocNo,MAX(DocDate) as DocDate,MAX(Route_No) as Route_No,MAX(Route_Desc) as Route_Desc,MAX(Cust_Code) as Cust_Code,MAX(Cust_Name)as Cust_Name ,MAX(Salesman_Code) as Salesman_Code,MAX(SalesManName) as SalesManName ,MAX(Vehicle_Code) as Vehicle_Code,MAX(Vehicle_No) as Vehicle_No ,SUM(ECQty) as ECQty,MAX(ECRate) as ECRate,sum(ECAmt) as ECAmt,SUM(EBQty) as EBQty,MAX(EBRate) as EBRate,sum(EBAmt) as EBAmt"
            qry += " ,max(case when TC='SI' then SHQty else 0 end) as SHQtySI,max(case when TC='SI' then SHAmt else 0 end) as SHAmtSI"
            qry += " ,sum(case when TC<>'SI' then SHQty else 0 end) as SHQty,MAX(SHRate) as SHRate,sum(case when TC<>'SI' then SHAmt else 0 end) as SHAmt,max(TC) as TC,max(UNITTYPE) as UNITTYPE,max(RI) as RI,max(Document) as Document,max(GLContainerDepositAmt) as GLContainerDepositAmt from "
            qry += " ( " + BaseQry + ")"
            qry += " xxx group by DocNo,Item_Code,Sale_Invoice_Id"
            qry += " )xxxx  "
            BaseQry = qry

            qry = "select Location,DocNo,DocDate,Route_No,Route_Desc,Description,Remarks,Cust_Code,Cust_Name,Salesman_Code,SalesmanName,Vehicle_No, "
            qry += "max(ECRate) as ECRate, sum(ECAmt) as ECAmt, sum(EBQty) as EBQty, max(EBRate) as EBRate,sum(EBAmt) as EBAmt, "
            qry += "sum(SHQty) as SHQty, max(SHRate) as SHRate,  sum(SHAmt) as SHAmt, "
            qry += "sum(TotAmt) as TotAmt,UNITTYPE,TRANSTYPE,Document,sum(GLContainerDepositAmt) AS GLContainerDepositAmt "
            qry += " ," & strmainItemCodeString & " ,SUM(" + strsum + ")as Total from( " & BaseQry & " ) down  "
            qry += "pivot (SUM(ECQty) FOR Item_Code IN ( " & strItemCodestring & ")) AS pvt1 group by Description,Remarks,Document,UNITTYPE,TRANSTYPE,Location,DocNo,DocDate,Route_No,Route_Desc,Cust_Code,Cust_Name,Salesman_Code,SalesmanName, Vehicle_Code,Vehicle_No "
        End If
        dt = clsDBFuncationality.GetDataTable(qry)

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
        End If
        gv1.DataSource = dt
        SetGridFormationOFGV1()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        If rdbSummary.IsChecked OrElse rdbDetail.IsChecked Then
            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 50
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 100
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Document Date"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 100
            gv1.Columns("Route_No").HeaderText = "Route Code"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 150
            gv1.Columns("Route_Desc").HeaderText = "Route"

            gv1.Columns("Cust_Code").IsVisible = True
            gv1.Columns("Cust_Code").Width = 70
            gv1.Columns("Cust_Code").HeaderText = "Customer Code"

            gv1.Columns("Cust_Name").IsVisible = True
            gv1.Columns("Cust_Name").Width = 150
            gv1.Columns("Cust_Name").HeaderText = "Customer"

            gv1.Columns("Salesman_Code").IsVisible = True
            gv1.Columns("Salesman_Code").Width = 70
            gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            gv1.Columns("SalesmanName").IsVisible = True
            gv1.Columns("SalesmanName").Width = 100
            gv1.Columns("SalesmanName").HeaderText = "Salesman"

            gv1.Columns("Vehicle_Code").Width = 70
            gv1.Columns("Vehicle_Code").HeaderText = "Vehicle code"

            gv1.Columns("Vehicle_No").IsVisible = True
            gv1.Columns("Vehicle_No").Width = 100
            gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"

            gv1.Columns("ECQty").IsVisible = True
            gv1.Columns("ECQty").Width = 80
            gv1.Columns("ECQty").HeaderText = "EC Qty"
            gv1.Columns("ECQty").FormatString = "{0:F2}"

            gv1.Columns("ECRate").IsVisible = True
            gv1.Columns("ECRate").Width = 80
            gv1.Columns("ECRate").HeaderText = "EC Rate"
            gv1.Columns("ECRate").FormatString = "{0:F2}"

            gv1.Columns("ECAmt").IsVisible = True
            gv1.Columns("ECAmt").Width = 80
            gv1.Columns("ECAmt").HeaderText = "EC Amount"
            gv1.Columns("ECAmt").FormatString = "{0:F2}"

            gv1.Columns("EBQty").IsVisible = True
            gv1.Columns("EBQty").Width = 80
            gv1.Columns("EBQty").HeaderText = "EB Qty"
            gv1.Columns("EBQty").FormatString = "{0:F2}"

            gv1.Columns("EBRate").IsVisible = True
            gv1.Columns("EBRate").Width = 80
            gv1.Columns("EBRate").HeaderText = "EB Rate"
            gv1.Columns("EBRate").FormatString = "{0:F2}"

            gv1.Columns("EBAmt").IsVisible = True
            gv1.Columns("EBAmt").Width = 80
            gv1.Columns("EBAmt").HeaderText = "EB Amount"
            gv1.Columns("EBAmt").FormatString = "{0:F2}"

            gv1.Columns("SHQty").IsVisible = True
            gv1.Columns("SHQty").Width = 80
            gv1.Columns("SHQty").HeaderText = "Shell Qty"
            gv1.Columns("SHQty").FormatString = "{0:F2}"

            gv1.Columns("SHRate").IsVisible = True
            gv1.Columns("SHRate").Width = 80
            gv1.Columns("SHRate").HeaderText = "Shell Rate"
            gv1.Columns("SHRate").FormatString = "{0:F2}"

            gv1.Columns("SHAmt").IsVisible = True
            gv1.Columns("SHAmt").Width = 50
            gv1.Columns("SHAmt").HeaderText = "Shell Amount"
            gv1.Columns("SHAmt").FormatString = "{0:F2}"

            gv1.Columns("TotAmt").IsVisible = True
            gv1.Columns("TotAmt").Width = 80
            gv1.Columns("TotAmt").HeaderText = "Total Amount"
            gv1.Columns("TotAmt").FormatString = "{0:F2}"

            gv1.Columns("UNITTYPE").IsVisible = True
            gv1.Columns("UNITTYPE").Width = 80
            gv1.Columns("UNITTYPE").HeaderText = "Type"

            gv1.Columns("TRANSTYPE").IsVisible = True
            gv1.Columns("TRANSTYPE").Width = 80
            gv1.Columns("TRANSTYPE").HeaderText = "Transaction Type"

            gv1.Columns("Document").IsVisible = True
            gv1.Columns("Document").Width = 80
            gv1.Columns("Document").HeaderText = "Document"

            gv1.Columns("GLContainerDepositAmt").IsVisible = True
            gv1.Columns("GLContainerDepositAmt").Width = 80
            gv1.Columns("GLContainerDepositAmt").HeaderText = "GL Container Deposit Amount"

            If rdbDetail.IsChecked Then
                gv1.Columns("Remarks").IsVisible = True
                gv1.Columns("Remarks").Width = 80
                gv1.Columns("Remarks").HeaderText = "Remarks"

                gv1.Columns("Description").IsVisible = True
                gv1.Columns("Description").Width = 80
                gv1.Columns("Description").HeaderText = "Description"

                gv1.Columns("Pack").IsVisible = True
                gv1.Columns("Pack").Width = 80
                gv1.Columns("Pack").HeaderText = "Pack"

                gv1.Columns("Flavour").IsVisible = True
                gv1.Columns("Flavour").Width = 80
                gv1.Columns("Flavour").HeaderText = "Brand"
            End If
            ''gv1.GroupDescriptors.Add(New GridGroupByExpression("Transfer_No as Transfer_No format ""{0}: {1}"" Group By Transfer_No"))
            ''gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            ''gv1.MasterTemplate.AutoExpandGroups = True


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("ECQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("ECAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("EBQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("EBAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SHQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("SHAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("TotAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("GLContainerDepositAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Else
            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 50
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 100
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Document Date"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 100
            gv1.Columns("Route_No").HeaderText = "Route Code"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 150
            gv1.Columns("Route_Desc").HeaderText = "Route"

            gv1.Columns("Remarks").IsVisible = True
            gv1.Columns("Remarks").Width = 80
            gv1.Columns("Remarks").HeaderText = "Remarks"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 80
            gv1.Columns("Description").HeaderText = "Description"

            gv1.Columns("Cust_Code").IsVisible = True
            gv1.Columns("Cust_Code").Width = 70
            gv1.Columns("Cust_Code").HeaderText = "Customer Code"

            gv1.Columns("Cust_Name").IsVisible = True
            gv1.Columns("Cust_Name").Width = 150
            gv1.Columns("Cust_Name").HeaderText = "Customer"

            gv1.Columns("Salesman_Code").IsVisible = True
            gv1.Columns("Salesman_Code").Width = 70
            gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            gv1.Columns("SalesmanName").IsVisible = True
            gv1.Columns("SalesmanName").Width = 100
            gv1.Columns("SalesmanName").HeaderText = "Salesman"

            gv1.Columns("Vehicle_No").IsVisible = True
            gv1.Columns("Vehicle_No").Width = 100
            gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"


            gv1.Columns("ECRate").IsVisible = True
            gv1.Columns("ECRate").Width = 80
            gv1.Columns("ECRate").HeaderText = "EC Rate"
            gv1.Columns("ECRate").FormatString = "{0:F2}"

            gv1.Columns("ECAmt").IsVisible = True
            gv1.Columns("ECAmt").Width = 80
            gv1.Columns("ECAmt").HeaderText = "EC Amount"
            gv1.Columns("ECAmt").FormatString = "{0:F2}"

            gv1.Columns("EBQty").IsVisible = True
            gv1.Columns("EBQty").Width = 80
            gv1.Columns("EBQty").HeaderText = "EB Qty"
            gv1.Columns("EBQty").FormatString = "{0:F2}"

            gv1.Columns("EBRate").IsVisible = True
            gv1.Columns("EBRate").Width = 80
            gv1.Columns("EBRate").HeaderText = "EB Rate"
            gv1.Columns("EBRate").FormatString = "{0:F2}"

            gv1.Columns("EBAmt").IsVisible = True
            gv1.Columns("EBAmt").Width = 80
            gv1.Columns("EBAmt").HeaderText = "EB Amount"
            gv1.Columns("EBAmt").FormatString = "{0:F2}"

            gv1.Columns("SHQty").IsVisible = True
            gv1.Columns("SHQty").Width = 80
            gv1.Columns("SHQty").HeaderText = "Shell Qty"
            gv1.Columns("SHQty").FormatString = "{0:F2}"

            gv1.Columns("SHRate").IsVisible = True
            gv1.Columns("SHRate").Width = 80
            gv1.Columns("SHRate").HeaderText = "Shell Rate"
            gv1.Columns("SHRate").FormatString = "{0:F2}"

            gv1.Columns("SHAmt").IsVisible = True
            gv1.Columns("SHAmt").Width = 50
            gv1.Columns("SHAmt").HeaderText = "Shell Amount"
            gv1.Columns("SHAmt").FormatString = "{0:F2}"

            gv1.Columns("TotAmt").IsVisible = True
            gv1.Columns("TotAmt").Width = 80
            gv1.Columns("TotAmt").HeaderText = "Total Amount"
            gv1.Columns("TotAmt").FormatString = "{0:F2}"

            gv1.Columns("UNITTYPE").IsVisible = True
            gv1.Columns("UNITTYPE").Width = 80
            gv1.Columns("UNITTYPE").HeaderText = "Type"

            gv1.Columns("TRANSTYPE").IsVisible = True
            gv1.Columns("TRANSTYPE").Width = 80
            gv1.Columns("TRANSTYPE").HeaderText = "Transaction Type"

            gv1.Columns("Document").IsVisible = True
            gv1.Columns("Document").Width = 80
            gv1.Columns("Document").HeaderText = "Document"

            gv1.Columns("GLContainerDepositAmt").IsVisible = True
            gv1.Columns("GLContainerDepositAmt").Width = 80
            gv1.Columns("GLContainerDepositAmt").HeaderText = "GL Container Deposit Amount"

            Dim intCount As Integer = 0
            Dim strItemCode As String

            For ii As Integer = 25 To gv1.Columns.Count - 1
                strItemCode = gv1.Columns(ii).FieldName
                gv1.Columns("" & strItemCode & "").IsVisible = True
                gv1.Columns("" & strItemCode & "").Width = 80
                gv1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                'gvReport.Columns("" & strItemCode & "").BestFit()
            Next


            Dim summaryRowItem As New GridViewSummaryRowItem()
           
            Dim item2 As New GridViewSummaryItem("ECAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("EBQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("EBAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SHQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("SHAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("TotAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("GLContainerDepositAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

           
            For ii As Integer = 25 To gv1.Columns.Count - 1
                intCount = intCount + 1
                strItemCode = gv1.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        End If
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        'print()
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData()
            Dim location As String
            Dim Customer As String
            Dim sales As String
            Dim StrLocation As String
            Dim StrCustomer As String = ""
            Dim Strsales As String = ""

            If chkLocationSegSelect.IsChecked Then
                location = "'" + clsCommon.GetMulcallString(cbgLocationSegment.CheckedValue) + "'"
                StrLocation = location.Replace("'", "")
            End If
            If chkCustomerSelect.IsChecked Then
                Customer = "'" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + "'"
                StrCustomer = Customer.Replace("'", "")
            End If
            If chlSalesSelect.IsChecked Then
                sales = "'" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + "'"
                Strsales = sales.Replace("'", "")
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            For Each Str As String In cbgCompany.CheckedDisplayMember
                If clsCommon.myLen(strTemp) > 0 Then
                    strTemp += ", "
                End If
                strTemp += Str
            Next
            arrHeader.Add("Company : " + strTemp)

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            If chkLocationSegSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocationSegment.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location Segment : " + strTemp)
                If StrCustomer <> "" Then
                    arrHeader.Add("Customer : " + StrCustomer)
                End If
                If Strsales <> "" Then
                    arrHeader.Add("Sales : " + Strsales)
                End If
            End If

            ' clsCommon.MyExportToExcel("Empty Transaction", gv1, arrHeader, Me.Text)


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Empty Transaction", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Empty Transaction", gv1, arrHeader, "Empty Transaction", True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkLocationSegSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSegSelect.ToggleStateChanged, chkLocationSegAll.ToggleStateChanged
        cbgLocationSegment.Enabled = chkLocationSegSelect.IsChecked
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDocAll.ToggleStateChanged
        cbgDocument.Enabled = chkDocSelect.IsChecked
    End Sub

   
   
    Private Sub rdbRowwise_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbRowwise.ToggleStateChanged
        If rdbRowwise.IsChecked Then
            grpSelect.Visible = False
            rdbSKU.IsChecked = True
        Else
            grpSelect.Visible = False
        End If
    End Sub
End Class
