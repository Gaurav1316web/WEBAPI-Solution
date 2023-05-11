Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML

Public Class FrmSalemanSaleOrder
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value
        SetUserMgmtNew()
       

        LoadType()
        LoadCustomer()
        LoadSalesman()

        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1

        rbtnSalesmanAll.IsChecked = True
        rbtnCustomerAll.IsChecked = True
        cboType.SelectedValue = "Detail"
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Detail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Customer Wise Summary"

        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Salesman Wise Summary"
        dt.Rows.Add(dr)

        cboType.DataSource = clsItemMaster.GetItemType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = " SELECT Cust_Code,Customer_Name  FROM TSPL_CUSTOMER_MASTER  "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub

    Sub LoadSalesman()
        Dim qry As String = " SELECT Emp_Code,Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_Type='SalesMan' "
        cbgSalesman.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesman.ValueMember = "Emp_Code"
        cbgSalesman.DisplayMember = "Emp_Name"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SalesmanSalesOrderReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
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
        If rbtnSalesmanSelect.IsChecked AndAlso cbgSalesman.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one salesman")
        End If
        If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Customer")
        End If

        
        Dim qry As String = "select TSPL_SALES_ORDER_HEAD.Order_No, TSPL_SALES_ORDER_HEAD.Order_Date,TSPL_SALES_ORDER_HEAD.Cust_Code,TSPL_SALES_ORDER_HEAD.Cust_Name,TSPL_SALES_ORDER_HEAD.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName, TSPL_SALES_ORDER_DETAIL.Item_Code,TSPL_SALES_ORDER_DETAIL.Item_Desc, TSPL_SALES_ORDER_DETAIL.Order_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as OrderQty ,TSPL_SALES_ORDER_DETAIL.Tonnage,TSPL_SALES_ORDER_DETAIL.Total_Item_Amt,Payment_Amount " + Environment.NewLine
        qry += " from TSPL_SALES_ORDER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALES_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALES_ORDER_DETAIL.Unit_code " + Environment.NewLine
        qry += " left outer join TSPL_SALES_ORDER_HEAD on TSPL_SALES_ORDER_HEAD.Order_No=TSPL_SALES_ORDER_DETAIL.Order_No" + Environment.NewLine
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_SALES_ORDER_HEAD.Salesman_Code" + Environment.NewLine
        qry += " where TSPL_SALES_ORDER_HEAD.Order_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
        qry += " and TSPL_SALES_ORDER_HEAD.Order_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine
        If cbgSalesman.CheckedValue.Count Then
            qry += " and TSPL_SALES_ORDER_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesman.CheckedValue) + ")"
        End If
        If cbgCustomer.CheckedValue.Count Then
            qry += " and TSPL_SALES_ORDER_HEAD.Cust_Code in ( " + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If

        Dim FinalQty As String = ""
        If rdbSummary.IsChecked Then
            FinalQty = "select   Order_No,Order_Date,Cust_Code ,MAX(Cust_Name) as Cust_Name, Salesman_Code  ,MAX(SalesmanName) as  SalesmanName,convert(decimal(18,2), SUM(OrderQty)) as OrderQty,SUM(isnull(Tonnage,0)) as Tonnage,sum(Total_Item_Amt) as Total_Item_Amt, MAX(Payment_Amount) as Payment_Amount from ( " + qry + " )xxx Group by xxx.Cust_Code,xxx.Salesman_Code,Order_No,Order_Date order by Order_No,Order_Date "
        Else
            FinalQty = "select   Cust_Code ,MAX(Cust_Name) as Cust_Name, Salesman_Code  ,MAX(SalesmanName) as  SalesmanName,Item_Code,MAX(Item_Desc) as Item_Desc,convert(decimal(18,2), SUM(OrderQty)) as OrderQty,SUM(isnull(Tonnage,0)) as Tonnage,sum(Total_Item_Amt) as Total_Item_Amt from ( " + qry + " )xxx Group by xxx.Cust_Code,xxx.Salesman_Code,xxx.Item_Code order by xxx.Cust_Code,xxx.Salesman_Code,xxx.Item_Code"
        End If




        dt = clsDBFuncationality.GetDataTable(FinalQty)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.ShowGroupPanel = False
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
        End If
        gv1.DataSource = dt
        SetGridFormationOFGV1()
        RadPageView1.SelectedPage = RadPageViewPage2
        EnableDisableCtrl(False)
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Cust_Code").IsVisible = True
        gv1.Columns("Cust_Code").Width = 100
        gv1.Columns("Cust_Code").HeaderText = "Customer Code"
        ' gv1.MasterTableView.Columns[3].OrderIndex=5

        gv1.Columns("Cust_Name").IsVisible = True
        gv1.Columns("Cust_Name").Width = 150
        gv1.Columns("Cust_Name").HeaderText = "Customer"

        gv1.Columns("Salesman_Code").IsVisible = True
        gv1.Columns("Salesman_Code").Width = 100
        gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

        gv1.Columns("SalesmanName").IsVisible = True
        gv1.Columns("SalesmanName").Width = 150
        gv1.Columns("SalesmanName").HeaderText = "Salesman"

        If rdbSummary.IsChecked Then
            gv1.Columns("Order_No").IsVisible = True
            gv1.Columns("Order_No").Width = 100
            gv1.Columns("Order_No").HeaderText = "Order No"

            gv1.Columns("Order_Date").IsVisible = True
            gv1.Columns("Order_Date").Width = 150
            gv1.Columns("Order_Date").HeaderText = "Order Date"

            gv1.Columns("Payment_Amount").IsVisible = True
            gv1.Columns("Payment_Amount").Width = 100
            gv1.Columns("Payment_Amount").HeaderText = "Payment Amount"

        Else
            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item Desc"
        End If
        

        gv1.Columns("OrderQty").IsVisible = True
        gv1.Columns("OrderQty").Width = 100
        gv1.Columns("OrderQty").HeaderText = "Order Qty"


        gv1.Columns("Tonnage").IsVisible = True
        gv1.Columns("Tonnage").Width = 100
        gv1.Columns("Tonnage").HeaderText = "Tonnage"


        gv1.Columns("Total_Item_Amt").IsVisible = True
        gv1.Columns("Total_Item_Amt").Width = 100
        gv1.Columns("Total_Item_Amt").HeaderText = "Amount"

         


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("OrderQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Tonnage", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Total_Item_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData()

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            If rbtnSalesmanSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgSalesman.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                arrHeader.Add("Salesman : " + strLoca)
            End If

            If rbtnCustomerSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                arrHeader.Add("Customer : " + strLoca)
            End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)

    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub
 
    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged, rbtnCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub

    Private Sub rbtnSalesmanAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSalesmanAll.ToggleStateChanged, rbtnSalesmanSelect.ToggleStateChanged
        cbgSalesman.Enabled = rbtnSalesmanSelect.IsChecked
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        cboType.Enabled = val
        RadGroupBox3.Enabled = val
        RadGroupBox1.Enabled = val
    End Sub
End Class
