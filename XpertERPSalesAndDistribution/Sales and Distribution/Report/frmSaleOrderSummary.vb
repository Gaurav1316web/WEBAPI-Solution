'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 23/05/2013-------------------------------------
'--------------------------------Last modify Time - 03:55 PM -------------------------------------

Imports XpertERPEngine
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class FrmSaleOrderSummaryDemo
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleOrderSummary)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Sub LoadLocation()
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Private Sub FrmSaleOrderSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        LoadLocation()
        LoadDocType()
        rdbAll.IsChecked = True
        rdbDetail.Visible = True
        rdbSummary.IsChecked = True
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadDocType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Complete"
        dr("Name") = "Complete"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Pending"
        dr("Name") = "Pending"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Partial Shipped"
        dr("Name") = "Partial Shipped"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Sale"
        'dr("Name") = "Sale"
        'dt.Rows.Add(dr)

        ddlStatus.DataSource = dt
        ddlStatus.ValueMember = "Code"
        ddlStatus.DisplayMember = "Name"
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        rdbAll.IsChecked = True
        rdbSummary.IsChecked = True
    End Sub
    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked Then
            gv1.Columns("Status").IsVisible = True
            gv1.Columns("Status").Width = 80
            gv1.Columns("Status").HeaderText = "Status"

            gv1.Columns("Document_Code").IsVisible = True
            gv1.Columns("Document_Code").Width = 80
            gv1.Columns("Document_Code").HeaderText = "Document No"

            gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").Width = 80
            gv1.Columns("Document_Date").HeaderText = "Document date"

            gv1.Columns("Customer_Code").IsVisible = True
            gv1.Columns("Customer_Code").Width = 80
            gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            gv1.Columns("Salesman_Code").IsVisible = True
            gv1.Columns("Salesman_Code").Width = 80
            gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            gv1.Columns("Salesman_Name").IsVisible = True
            gv1.Columns("Salesman_Name").Width = 80
            gv1.Columns("Salesman_Name").HeaderText = "Salesman Name"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            gv1.Columns("Delivery_date").IsVisible = True
            gv1.Columns("Delivery_date").Width = 80
            gv1.Columns("Delivery_date").HeaderText = "Expected Date"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 80
            gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            gv1.Columns("Unit_Code").IsVisible = True
            gv1.Columns("Unit_Code").Width = 50
            gv1.Columns("Unit_Code").HeaderText = "Unit Code"

            gv1.Columns("OrderQty").IsVisible = True
            gv1.Columns("OrderQty").Width = 70
            gv1.Columns("OrderQty").HeaderText = "Order Qty"

            gv1.Columns("ShippedQty").IsVisible = True
            gv1.Columns("ShippedQty").Width = 70
            gv1.Columns("ShippedQty").HeaderText = "Shipped Qty"

            gv1.Columns("OutstandingQty").IsVisible = True
            gv1.Columns("OutstandingQty").Width = 70
            gv1.Columns("OutstandingQty").HeaderText = "Outstanding Qty"

            gv1.Columns("rate").IsVisible = True
            gv1.Columns("rate").Width = 100
            gv1.Columns("rate").HeaderText = "Rate"

            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").Width = 100
            gv1.Columns("Amount").HeaderText = "Item Amount"

            gv1.Columns("Disc_Amt").IsVisible = True
            gv1.Columns("Disc_Amt").Width = 100
            gv1.Columns("Disc_Amt").HeaderText = "Discount Amount"

            gv1.Columns("TPTAmt").IsVisible = True
            gv1.Columns("TPTAmt").Width = 80
            gv1.Columns("TPTAmt").HeaderText = "TPT Amount"

            gv1.Columns("Total_Tax_Amt").IsVisible = True
            gv1.Columns("Total_Tax_Amt").Width = 80
            gv1.Columns("Total_Tax_Amt").HeaderText = "Tax Amount"

            gv1.Columns("TotalOrderAmt").IsVisible = True
            gv1.Columns("TotalOrderAmt").Width = 80
            gv1.Columns("TotalOrderAmt").HeaderText = "Total Order Amount"

            gv1.Columns("TotalShippedAmt").IsVisible = True
            gv1.Columns("TotalShippedAmt").Width = 80
            gv1.Columns("TotalShippedAmt").HeaderText = "Total Shipped Amount"


            gv1.Columns("OutstandingAmt").IsVisible = True
            gv1.Columns("OutstandingAmt").Width = 80
            gv1.Columns("OutstandingAmt").HeaderText = "Outstanding Amount"


        ElseIf rdbSummary.IsChecked Then
            gv1.Columns("Status").IsVisible = True
            gv1.Columns("Status").Width = 80
            gv1.Columns("Status").HeaderText = "Status"

            gv1.Columns("Document_Code").IsVisible = True
            gv1.Columns("Document_Code").Width = 80
            gv1.Columns("Document_Code").HeaderText = "Document No"

            gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").Width = 80
            gv1.Columns("Document_Date").HeaderText = "Document date"

            gv1.Columns("Customer_Code").IsVisible = True
            gv1.Columns("Customer_Code").Width = 80
            gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            gv1.Columns("Salesman_Code").IsVisible = True
            gv1.Columns("Salesman_Code").Width = 80
            gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            gv1.Columns("Salesman_Name").IsVisible = True
            gv1.Columns("Salesman_Name").Width = 80
            gv1.Columns("Salesman_Name").HeaderText = "Salesman Name"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            gv1.Columns("Delivery_date").IsVisible = True
            gv1.Columns("Delivery_date").Width = 80
            gv1.Columns("Delivery_date").HeaderText = "Expected Date"

            gv1.Columns("TotalOrderAmt").IsVisible = True
            gv1.Columns("TotalOrderAmt").Width = 80
            gv1.Columns("TotalOrderAmt").HeaderText = "Total Order Amount"

            gv1.Columns("TotalShippedAmt").IsVisible = True
            gv1.Columns("TotalShippedAmt").Width = 80
            gv1.Columns("TotalShippedAmt").HeaderText = "Total Shipped Amount"


            gv1.Columns("OutstandingAmt").IsVisible = True
            gv1.Columns("OutstandingAmt").Width = 80
            gv1.Columns("OutstandingAmt").HeaderText = "Outstanding Amount"



        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("OrderQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("ShippedQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("OutstandingQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("TPTAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("TotalOrderAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("TotalShippedAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("OutstandingAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)


        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub


    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim strCustomer As String = ""
        Dim strloc As String = ""
        Dim strStatus As String = ""
        If rdbAll.IsChecked Then
            strStatus = "All"
        ElseIf rdbpending.IsChecked Then
            strStatus = "Pending"
        ElseIf rdbPartial.IsChecked Then
            strStatus = "Partial Shipped"
        ElseIf rdbComplete.IsChecked Then
            strStatus = "Complete"
        End If

        If cbgCustomer.CheckedValue.Count > 0 Then
            strCustomer += " and TSPL_SALES_ORDER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        Else
            strCustomer = ""

        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            strloc += " and TSPL_SALES_ORDER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        Else
            strloc = ""

        End If
        Dim str As String = "select case when SUM(OrderQty) = sum(OrderQty-ShippedQty) then 'Pending' " & _
        "when  SUM(OrderQty) > sum(OrderQty-ShippedQty) and sum(OrderQty-ShippedQty) <> 0 then 'Partial Shipped' " & _
        "when sum(OrderQty-ShippedQty)=0 then 'Complete' end as Status,'" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate, " & _
        "Document_Code,Document_Date,Customer_Code,Customer_Name,Salesman_Code,Salesman_Name,Location,Location_Desc,Delivery_date, " & _
        "Item_Code,Item_Desc,Unit_code,sum(OrderQty) as OrderQty,sum(ShippedQty) as ShippedQty, " & _
        "sum(OrderQty-ShippedQty) as OutstandingQty,max(rate) as rate,sum(Amount) as Amount, " & _
        "sum(Disc_Amt) as Disc_Amt,sum(TPTAmt) as TPTAmt,sum(Total_Tax_Amt) as Total_Tax_Amt, " & _
        "sum(TotalOrderAmt) as TotalOrderAmt,sum(totalShipAmt) as TotalShippedAmt, " & _
        "case when (SUM(OrderQty) - SUM(shippedqty) )=0 then 0 else SUM(TotalOrderAmt -totalShipAmt) end as OutstandingAmt from " & _
        "(SELECT TSPL_SALES_ORDER_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SALES_ORDER_HEAD.Order_No AS Document_Code, TSPL_SALES_ORDER_HEAD.Order_Date AS Document_Date, " & _
                      "TSPL_SALES_ORDER_HEAD.Cust_Code AS Customer_Code, TSPL_SALES_ORDER_HEAD.Cust_Name AS Customer_Name, " & _
                      "TSPL_SALES_ORDER_HEAD.Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman_Name, " & _
                      "TSPL_SALES_ORDER_HEAD.Expected_Ship_Date AS Delivery_date, TSPL_SALES_ORDER_DETAIL.Item_Code, " & _
                      "TSPL_SALES_ORDER_DETAIL.Item_Desc, TSPL_SALES_ORDER_DETAIL.Order_Qty AS OrderQty, 0 AS ShippedQty, " & _
                      "TSPL_SALES_ORDER_DETAIL.Unit_code, TSPL_SALES_ORDER_DETAIL.Basic_Rate AS rate, TSPL_SALES_ORDER_DETAIL.Total_net_Amt AS Amount, " & _
                      "TSPL_SALES_ORDER_DETAIL.Total_Disc_Amt AS Disc_Amt, TSPL_SALES_ORDER_DETAIL.Total_TPT AS TPTAmt, " & _
                      "TSPL_SALES_ORDER_DETAIL.Total_Tax_Amt, TSPL_SALES_ORDER_DETAIL.Total_Item_Amt AS TotalOrderAmt, 0 AS totalShipAmt " & _
                      "FROM  TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_SALES_ORDER_HEAD ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_SALES_ORDER_HEAD.Salesman_Code LEFT OUTER JOIN " & _
                      "TSPL_SALES_ORDER_DETAIL ON TSPL_SALES_ORDER_HEAD.Order_No = TSPL_SALES_ORDER_DETAIL.Order_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALES_ORDER_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code " & _
                     " where convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                     " convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strCustomer & "  " & strloc & " " & _
                     " union all " & _
                "SELECT  TSPL_SALES_ORDER_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SALES_ORDER_HEAD.Order_No AS Document_Code, TSPL_SALES_ORDER_HEAD.Order_Date AS Document_Date, " & _
                      "TSPL_SALES_ORDER_HEAD.Cust_Code AS Customer_Code, TSPL_SALES_ORDER_HEAD.Cust_Name AS Customer_Name, " & _
                      "TSPL_SALES_ORDER_HEAD.Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman_Name,  " & _
                      "TSPL_SALES_ORDER_HEAD.Expected_Ship_Date AS Delivery_date, TSPL_SHIPMENT_DETAILS.Item_Code, TSPL_SHIPMENT_DETAILS.Item_Desc, " & _
                      "0 AS OrderQty, TSPL_SHIPMENT_DETAILS.Shipped_Qty AS ShippedQty, TSPL_SHIPMENT_DETAILS.Unit_code, " & _
                      "TSPL_SHIPMENT_DETAILS.Basic_Rate AS rate, 0 AS amount, 0 AS Disc_Amt, 0 AS Amt_Less_Discount, 0 AS Total_Tax_Amt, 0 AS TotalOrderAmt, " & _
                      "TSPL_SHIPMENT_DETAILS.Total_Item_Amt AS totalShipAmt " & _
                      "FROM TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_SHIPMENT_DETAILS RIGHT OUTER JOIN " & _
                      "TSPL_SHIPMENT_MASTER INNER JOIN " & _
                      "TSPL_SALES_ORDER_HEAD ON TSPL_SHIPMENT_MASTER.Order_No = TSPL_SALES_ORDER_HEAD.Order_No ON  " & _
                      "TSPL_SHIPMENT_DETAILS.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No ON " & _
                      "TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_SALES_ORDER_HEAD.Salesman_Code left outer join TSPL_LOCATION_MASTER on TSPL_SALES_ORDER_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code " & _
                     " where convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                     " convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strCustomer & " " & strloc & " ) " & _
                     " xx group by Item_Code,Document_Code,Document_Date,Customer_Name,Customer_Code,Salesman_Name,Delivery_date, " & _
                     " Item_Code,Item_Desc,Unit_code,Salesman_Code,Location,Location_Desc "
        Dim qry As String

        If rdbSummary.IsChecked Then
            qry = "select max(Fdate) as Fdate,max(Tdate) as Tdate, " & _
            "case when SUM(OrderQty) = SUM(OutstandingQty) then 'Pending' " & _
            "when  SUM(OrderQty) > SUM(OutstandingQty) and SUM(OutstandingQty) <> 0 then 'Partially shipped' " & _
            "when SUM(OutstandingQty)=0 then 'Complete' end   as status, " & _
            "Document_Code,Document_Date,Customer_Code,Customer_Name,Salesman_Code,Salesman_Name,Location,Location_Desc, " & _
            "Delivery_date,SUM(TotalOrderAmt) as TotalOrderAmt,SUM(TotalShippedAmt) as TotalShippedAmt, " & _
            "case when (SUM(OrderQty) - SUM(shippedqty) )=0 then 0 else SUM(TotalOrderAmt -TotalShippedAmt) end " & _
            " as OutstandingAmt  from ( " & str & " ) aa group by Document_Code, " & _
            "Document_Date,Customer_Code,Customer_Name,Salesman_Name,Delivery_date,Salesman_Code,Location,Location_Desc"

            If rdbAll.IsChecked Then
                qry = qry & " order by Document_Code"
            ElseIf rdbpending.IsChecked Then
                qry = qry & " having SUM(OrderQty) = SUM(OutstandingQty) order by Document_Code"
            ElseIf rdbPartial.IsChecked Then
                qry = qry & " having SUM(OrderQty) > SUM(OutstandingQty) and SUM(OutstandingQty) <> 0 order by Document_Code"
            ElseIf rdbComplete.IsChecked Then
                qry = qry & " having SUM(OutstandingQty)=0  order by Document_Code"

            End If
        Else
            qry = str
            If rdbAll.IsChecked Then
                qry = qry & " order by Document_Code"
            ElseIf rdbpending.IsChecked Then
                qry = qry & " having SUM(OrderQty) = sum(OrderQty-ShippedQty) order by Document_Code"
            ElseIf rdbPartial.IsChecked Then
                qry = qry & " having SUM(OrderQty) > sum(OrderQty-ShippedQty) and sum(OrderQty-ShippedQty) <> 0 order by Document_Code"
            ElseIf rdbComplete.IsChecked Then
                qry = qry & " having sum(OrderQty-ShippedQty)=0  order by Document_Code"

            End If

        End If




        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv1.DataSource = dt
            SetGridFormationOFGV1()
        End If

    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Print(EnumExportTo.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            ''ExportToExcel()
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))

            Dim strCustomer As String = ""
            Dim strLoc As String = ""
            For Each Str As String In cbgCustomer.CheckedDisplayMember
                If clsCommon.myLen(strCustomer) > 0 Then
                    strCustomer += ", "
                End If
                strCustomer += Str
            Next
            If cbgCustomer.CheckedValue.Count = 0 Then
                strCustomer = "All"
            End If
            arrHeader.Add("Customer : " + strCustomer)


            For Each Str As String In cbgLocation.CheckedDisplayMember
                If clsCommon.myLen(strLoc) > 0 Then
                    strLoc += ", "
                End If
                strLoc += Str
            Next
            If cbgLocation.CheckedValue.Count = 0 Then
                strLoc = "All"
            End If
            arrHeader.Add("Location : " + strLoc)


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Pending Order Details", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Pending Order Details", gv1, arrHeader, Me.Text, True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub
End Class
