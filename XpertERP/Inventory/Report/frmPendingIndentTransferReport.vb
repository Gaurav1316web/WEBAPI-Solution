'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 27/06/2013-------------------------------------
'--------------------------------Last modify Time - 12:30 AM -------------------------------------


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
Public Class FrmPendingIndentTransferReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing



    Sub LoadRoute()
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Sub LoadSalesPerson()
        Dim qry As String = "Select EMP_CODE as [SalesPerson Code],Emp_Name as [SalesPerson Name] from TSPL_EMPLOYEE_MASTER"
        cbgSalesPerson.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesPerson.ValueMember = "SalesPerson Code"
        cbgSalesPerson.DisplayMember = "SalesPerson Name"
    End Sub
    Private Sub FrmPendingIndentTransferReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadRoute()
        LoadSalesPerson()
        LoadLocation()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rdbAll.IsChecked = True
        rdbDetail.Visible = True
        rdbSummary.IsChecked = True
    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.DailySettlement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isPrintFlag
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
            gv1.Columns("Customer_Code").HeaderText = "Route No"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Route Desc"

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
            gv1.Columns("OrderQty").HeaderText = "Indent Qty"

            gv1.Columns("ShippedQty").IsVisible = True
            gv1.Columns("ShippedQty").Width = 70
            gv1.Columns("ShippedQty").HeaderText = "Transfer Qty"

            gv1.Columns("OutstandingQty").IsVisible = True
            gv1.Columns("OutstandingQty").Width = 70
            gv1.Columns("OutstandingQty").HeaderText = "Outstanding Qty"

            gv1.Columns("rate").IsVisible = True
            gv1.Columns("rate").Width = 100
            gv1.Columns("rate").HeaderText = "Rate"

            gv1.Columns("TotalOrderAmt").IsVisible = True
            gv1.Columns("TotalOrderAmt").Width = 80
            gv1.Columns("TotalOrderAmt").HeaderText = "Total Indent Amount"

            gv1.Columns("TotalShippedAmt").IsVisible = True
            gv1.Columns("TotalShippedAmt").Width = 80
            gv1.Columns("TotalShippedAmt").HeaderText = "Total Transfer Amount"


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
            gv1.Columns("Customer_Code").HeaderText = "Route No"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Route Desc"

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

          
            gv1.Columns("TotalOrderAmt").IsVisible = True
            gv1.Columns("TotalOrderAmt").Width = 80
            gv1.Columns("TotalOrderAmt").HeaderText = "Total Indent Amount"

            gv1.Columns("TotalShippedAmt").IsVisible = True
            gv1.Columns("TotalShippedAmt").Width = 80
            gv1.Columns("TotalShippedAmt").HeaderText = "Total Transfer Amount"


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

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim strRoute As String = ""
        Dim strSalesman As String = ""
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

        If cbgRoute.CheckedValue.Count > 0 Then
            strRoute += " and TSPL_INDENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        Else
            strRoute = ""

        End If

        If cbgSalesPerson.CheckedValue.Count > 0 Then
            strSalesman += " and TSPL_INDENT_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        Else
            strSalesman = ""

        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            strloc += " and TSPL_INDENT_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        Else
            strloc = ""

        End If
        Dim str As String = "select case when SUM(OrderQty) = sum(OrderQty-ShippedQty) then 'Pending' " & _
                    "when  SUM(OrderQty) > sum(OrderQty-ShippedQty) and sum(OrderQty-ShippedQty) <> 0 then 'Partial Transferred' " & _
                    "when (sum(OrderQty-ShippedQty)=0 Or sum(OrderQty-ShippedQty)< 0) then 'Complete' end as Status, " & _
                    "Document_Code,Document_Date,Customer_Code,Customer_Name,Salesman_Code,Salesman_Name,Location,Location_Desc, " & _
                    "Item_Code,Item_Desc,Unit_code,sum(OrderQty) as OrderQty,sum(ShippedQty) as ShippedQty, " & _
                    "sum(OrderQty-ShippedQty) as OutstandingQty,max(rate) as rate, " & _
                    "sum(TotalOrderAmt) as TotalOrderAmt,sum(totalShipAmt) as TotalShippedAmt, " & _
                    "SUM(TotalOrderAmt -totalShipAmt)  as OutstandingAmt from " & _
                    "( SELECT TSPL_INDENT_HEAD.From_Location as  Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INDENT_HEAD.Indent_No AS Document_Code, " & _
                    "TSPL_INDENT_HEAD.Indent_Date AS Document_Date, TSPL_INDENT_HEAD.Route_No AS Customer_Code, TSPL_INDENT_HEAD.Route_Desc AS Customer_Name, " & _
                    "TSPL_INDENT_HEAD.Salesmancode as Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman_Name, '' AS Delivery_date, " & _
                    "TSPL_INDENT_DETAIL.Item_Code, TSPL_INDENT_DETAIL.Item_Desc, TSPL_INDENT_DETAIL.Item_Qty AS OrderQty, 0 AS ShippedQty, " & _
                    "TSPL_INDENT_DETAIL.Uom as Unit_code, TSPL_INDENT_DETAIL.BasicPrice_WithTax AS rate, TSPL_INDENT_DETAIL.Basic_Amt AS Amount, " & _
                    "0 AS Disc_Amt, 0 AS TPTAmt, 0 as Total_Tax_Amt, Basic_Amt AS TotalOrderAmt, 0 AS totalShipAmt FROM  " & _
                    "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN TSPL_INDENT_HEAD ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_INDENT_HEAD.Salesmancode LEFT OUTER JOIN " & _
                    "TSPL_INDENT_DETAIL ON TSPL_INDENT_HEAD.Indent_No = TSPL_INDENT_DETAIL.Indent_No  left outer join TSPL_LOCATION_MASTER on " & _
                    "TSPL_INDENT_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                     " where TSPL_INDENT_HEAD.Indent_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                     " TSPL_INDENT_HEAD.Indent_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strRoute & "  " & strloc & "  " & strSalesman & " " & _
                     " union all " & _
                    "SELECT  TSPL_INDENT_HEAD.From_Location as Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INDENT_HEAD.Indent_No AS Document_Code, " & _
                    "TSPL_INDENT_HEAD.Indent_Date AS Document_Date, TSPL_INDENT_HEAD.Route_No AS Customer_Code, TSPL_INDENT_HEAD.Route_Desc AS Customer_Name, " & _
                    "TSPL_INDENT_HEAD.Salesmancode as Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman_Name, " & _
                    "'' AS Delivery_date, TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc, 0 AS OrderQty, " & _
                    "TSPL_TRANSFER_DETAIL.Item_Qty AS ShippedQty, TSPL_TRANSFER_DETAIL.Uom as Unit_code, " & _
                    "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax AS rate, 0 AS amount, 0 AS Disc_Amt, 0 AS Amt_Less_Discount, " & _
                    "0 AS Total_Tax_Amt, 0 AS TotalOrderAmt, TSPL_TRANSFER_DETAIL.Total_Item_Amt AS totalShipAmt FROM " & _
                    "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN TSPL_TRANSFER_HEAD INNER JOIN " & _
                    "TSPL_INDENT_HEAD ON TSPL_TRANSFER_HEAD.against_indent_no = TSPL_INDENT_HEAD.Indent_No ON " & _
                    "TSPL_TRANSFER_DETAIL.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_INDENT_HEAD.Salesmancode " & _
                    "left outer join TSPL_LOCATION_MASTER on TSPL_INDENT_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                     " where TSPL_INDENT_HEAD.Indent_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                     " TSPL_INDENT_HEAD.Indent_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strRoute & " " & strloc & "  " & strSalesman & ") " & _
                     " xx group by Item_Code,Document_Code,Document_Date,Customer_Name,Customer_Code,Salesman_Name,Delivery_date, " & _
                     " Item_Code,Item_Desc,Unit_code,Salesman_Code,Location,Location_Desc "
        Dim qry As String

        If rdbSummary.IsChecked Then
            qry = "select  " & _
            "case when SUM(OrderQty) = SUM(OutstandingQty) then 'Pending' " & _
            "when  SUM(OrderQty) > SUM(OutstandingQty) and SUM(OutstandingQty) <> 0 then 'Partially Transferred' " & _
            "when ( SUM(OutstandingQty)=0 or SUM(OutstandingQty)< 0 ) then 'Complete' end   as status, " & _
            "Document_Code,Document_Date,Customer_Code,Customer_Name,Salesman_Code,Salesman_Name,Location,Location_Desc, " & _
            "SUM(TotalOrderAmt) as TotalOrderAmt,SUM(TotalShippedAmt) as TotalShippedAmt, " & _
            "SUM(TotalOrderAmt -TotalShippedAmt)  " & _
            " as OutstandingAmt  from ( " & str & " ) aa group by Document_Code, " & _
            "Document_Date,Customer_Code,Customer_Name,Salesman_Name,Salesman_Code,Location,Location_Desc"

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
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv1.DataSource = dt
            SetGridFormationOFGV1()
        End If

    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        rdbAll.IsChecked = True
        rdbSummary.IsChecked = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Print(EnumExportTo.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            ''ExportToExcel()
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))

            Dim strRoute As String = ""
            Dim strSalesman As String = ""
            Dim strLoc As String = ""
            For Each Str As String In cbgRoute.CheckedDisplayMember
                If clsCommon.myLen(strRoute) > 0 Then
                    strRoute += ", "
                End If
                strRoute += Str
            Next
            If cbgRoute.CheckedValue.Count = 0 Then
                strRoute = "All"
            End If
            arrHeader.Add("Route : " + strRoute)

            For Each Str As String In cbgSalesPerson.CheckedDisplayMember
                If clsCommon.myLen(strSalesman) > 0 Then
                    strSalesman += ", "
                End If
                strSalesman += Str
            Next
            If cbgSalesPerson.CheckedValue.Count = 0 Then
                strSalesman = "All"
            End If
            arrHeader.Add("Salesman : " + strSalesman)


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
                clsCommon.MyExportToExcel("Pending Indent Transfer Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Pending Indent Transfer Report", gv1, arrHeader, Me.Text, True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        print(EnumExportTo.PDF)
    End Sub

    
End Class
