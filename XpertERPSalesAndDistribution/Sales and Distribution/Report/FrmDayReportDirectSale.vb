''''' for bug no BM00000000545

Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmDayReportDirectSale
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim refreshGrid As String = "F"
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDayReportDirectSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub FrmDayReportDirectSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkCustomerAll.IsChecked = True
        chkLocAll.IsChecked = True
        LoadCustomer()
        LoadLocation()
        SetDataBaseGrid()
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
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
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master WHERE 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Public Sub gridformat()
        Try

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False

            For index As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(index).ReadOnly = True
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()

            gv1.AllowAddNewRow = False

            gv1.Columns("Sale_Invoice_Date").IsVisible = True
            gv1.Columns("Sale_Invoice_Date").Width = 100
            gv1.Columns("Sale_Invoice_Date").FormatString = "{0:d}"
            gv1.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").Width = 200
            gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv1.Columns("Cust_Code").IsVisible = True
            gv1.Columns("Cust_Code").Width = 100
            gv1.Columns("Cust_Code").HeaderText = "Customer Code"


            gv1.Columns("Cust_Name").IsVisible = True
            gv1.Columns("Cust_Name").Width = 100
            gv1.Columns("Cust_Name").HeaderText = "Distributor No"

            gv1.Columns("Total_Invoice_Amt").Width = 100
            gv1.Columns("Total_Invoice_Amt").IsVisible = True
            gv1.Columns("Total_Invoice_Amt").HeaderText = "Invoice Amount"

            gv1.Columns("TotalDispatch").IsVisible = True
            gv1.Columns("TotalDispatch").Width = 100
            gv1.Columns("TotalDispatch").HeaderText = "Total Dispatch"

            gv1.Columns("GlassDispatch").IsVisible = True
            gv1.Columns("GlassDispatch").Width = 100
            gv1.Columns("GlassDispatch").HeaderText = "Glass Dispatch"

            gv1.Columns("Comp_Name").IsVisible = True
            gv1.Columns("Comp_Name").Width = 100
            gv1.Columns("Comp_Name").HeaderText = "Company Name"
            gv1.Columns("Comp_Name").IsVisible = False

            gv1.Columns("address").IsVisible = True
            gv1.Columns("address").Width = 100
            gv1.Columns("address").HeaderText = "Address"
            gv1.Columns("address").IsVisible = False

            gv1.Columns("Salesman_Code").IsVisible = True
            gv1.Columns("Salesman_Code").Width = 100
            gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            gv1.Columns("Emp_Name").IsVisible = True
            gv1.Columns("Emp_Name").Width = 100
            gv1.Columns("Emp_Name").HeaderText = "Salesman Name"


            gv1.Columns("Vehicle_No").IsVisible = True
            gv1.Columns("Vehicle_No").Width = 100
            gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"


            gv1.ShowGroupPanel = False
            '  gv1.MasterTemplate.AutoExpandGroups = True






            'Dim SumGrossSaleQty As New GridViewSummaryItem("GrossSaleQty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumGrossSaleQty)
            'Dim SumNetSaleQty As New GridViewSummaryItem("Net_Sale", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumNetSaleQty)
            'Dim SumTradeDisc As New GridViewSummaryItem("TradeDiscQty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumTradeDisc)
            'Dim SumTargetDisc As New GridViewSummaryItem("TargetDiscQty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumTargetDisc)

            'Dim SumGrossSaleAMt As New GridViewSummaryItem("GrossAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumGrossSaleAMt)
            'Dim SumNetSaleAmt As New GridViewSummaryItem("NetSaleAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumNetSaleAmt)
            'Dim SumTradeDiscAmt As New GridViewSummaryItem("TradeDiscAMt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumTradeDiscAmt)
            'Dim SumTargetDiscAmt As New GridViewSummaryItem("TargetDiscAMt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumTargetDiscAmt)
            'Dim SumTM As New GridViewSummaryItem("TMMarginAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumTM)
            'Dim SumDM As New GridViewSummaryItem("DMMarginAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumDM)

            'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)




        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print(False, 2)
    End Sub
    Sub print(ByVal chk As Boolean, ByVal exporter As EnumExportTo)
        Dim str As String = "DAY REPORT DIRECT SALE"
        ' Dim head1 As String
        Dim head2 As String = ""
        Try
            Dim postingdata As String = ""
            Dim FinalGridQry As String = ""
            If rdoposted.IsChecked = True Then
                postingdata = "('Y')"
            ElseIf rdoalldata.IsChecked = True Then
                postingdata = "('Y','N')"
            End If

            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least Single 'Customer'")
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least Single 'Location'")
            End If





            Dim qry As String = " select " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name " & _
            " ,Salesman_Code,Emp_Name,Vehicle_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt ,  ( select sum([TotalQty]) from (select  ISNULL(case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC'  then   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty   when   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  /(select Conversion_Factor  from  " & _
            "   " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as [TotalQty] from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
            "  where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD .Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No ) xx) as [TotalDispatch],( select isnull(sum([TotalQty]),0) from (select  ISNULL(case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC'  then   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty " & _
            "  when   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  /(select Conversion_Factor  from  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " & _
            "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as [TotalQty] from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL   left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code  where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD .Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
            " and " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Two_Count_Status ='Y' ) xx) as [GlassDispatch] , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name ," + clsCommon.ReplicateDBString + "TSPL_company_Master.add1 +case when len(" + clsCommon.ReplicateDBString + "TSPL_company_Master.add2)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_company_Master.add2 else '' end +case when LEN(isnull(" + clsCommon.ReplicateDBString + "TSPL_company_Master.Add3,''))>0 then ', '+isnull(" + clsCommon.ReplicateDBString + "TSPL_company_Master.Add3,'') else ' ' end" & _
            " as address from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code=" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            " where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post in " + postingdata + " " & _
            " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "' "

            If chkCustomerSelect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If





            FinalGridQry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)
            'FinalGridQry += " order by Customer,Cust_Code"


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(FinalGridQry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gridformat()
            End If



            ' head2 = "B-42,Lawrence Road, Industrial Area, New Delhi"
            'head2 = CurrentCompAdd


            If refreshGrid = "F" AndAlso chk = True Then



                Dim strTemp As String = ""
                Dim arr As New List(Of String)()
                arr.Add(objCommonVar.CurrentCompanyName)
                arr.Add(head2)
                arr.Add("  From:  " + txtFromDate.Value + "  To: " + txtToDate.Value + "")
                If chkLocSelect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Location Segment : " + strTemp)
                End If

                If chkCustomerSelect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cbgCustomer.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Customer Segment : " + strTemp)
                End If



                If exporter = EnumExportTo.Excel Then

                    clsCommon.MyExportToExcelGrid(str, gv1, arr, "DAY REPORT DIRECT SALE")

                    'Else
                    '    clsCommon.MyExportToPDF(str, gv1, arr, Me.Text, True)
                End If
            ElseIf refreshGrid = "F" Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtgv, "crptDayReportDirectSale", "DAY REPORT DIRECT SALE")

            End If



            refreshGrid = "F"

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try




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

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        refreshGrid = "T"
        print(False, 2)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        formClose()
    End Sub
    Sub formClose()
        Me.Close()
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        print(True, EnumExportTo.Excel)
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked

    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class
