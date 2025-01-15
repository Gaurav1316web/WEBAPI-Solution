Imports common
Imports System.IO
Public Class rptBookingVsDispatchReport
    Inherits FrmMainTranScreen
    Dim strQry As String = ""

    Private Sub rptBookingVsDispatchReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtMultiCustomer.Visible = True
        txtMultiItem.Visible = True
        'rbtnDocumentDetail.Visible = False
        'rbtnBatchDetail.Visible = False
        lblCustomer.Visible = True
        lblItem.Visible = True
        RadGroupBox2.Visible = True
        rbtnBooking.IsChecked = True
        'rbtnDocumentDetail.Visible = True
        rbtnBatchDetail.Visible = True
        RadGroupBox2.Enabled = True
        rbtnDocumentDetail.IsChecked = True



    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSelect", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub

    Private Sub txtMultiRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultiRoute._My_Click
        Dim qry As String = " select Route_No as [Code], Route_Desc as [Name] from TSPL_ROUTE_MASTER "
        txtMultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSelect", qry, "Code", "Name", txtMultiRoute.arrValueMember, txtMultiRoute.arrDispalyMember)

    End Sub



    Private Sub txtMultiItem__My_Click(sender As Object, e As EventArgs) Handles txtMultiItem._My_Click
        Dim qry As String = " select Item_Code as Code, Item_desc as Name,Short_Description  from TSPL_ITEM_MASTER  where Item_Type='F'  "
        txtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", txtMultiItem.arrValueMember, txtMultiItem.arrDispalyMember)

    End Sub


    Private Sub rbtnBooking_Click(sender As Object, e As EventArgs) Handles rbtnBooking.Click
        Try
            rbtnDocumentDetail.IsChecked = True
            txtMultiCustomer.Visible = True
            txtMultiItem.Visible = True
            rbtnBooking.Visible = True
            rbtnBatchDetail.Visible = True
            rbtnDocumentDetail.Visible = True
            lblCustomer.Visible = True
            lblItem.Visible = True
            RadGroupBox2.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rbtnDocument_Click(sender As Object, e As EventArgs) Handles rbtnDocument.Click

        rbtnDocumentDetail.Visible = False
            txtMultiCustomer.Visible = False
            txtMultiItem.Visible = False
            rbtnBatchDetail.Visible = False
            lblCustomer.Visible = False
            lblItem.Visible = False
            lblRoute.Visible = True
        RadGroupBox2.Visible = False
    End Sub

    Private Sub rbtnDocumentDetail_Click(sender As Object, e As EventArgs) Handles rbtnDocumentDetail.Click

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            'RadGroupBox1.Enabled = False
            'RadGroupBox2.Enabled = False
            'txtMultiRoute.Enabled = False
            'txtMultiCustomer.Enabled = False
            'txtMultiItem.Enabled = False
            Dim strQry As String = ""
            Dim dt As New DataTable
            If rbtnDocument.IsChecked = True Then
                strQry = "select max(TSPL_DEMAND_BOOKING_MASTER.Route_No)Route_No, TSPL_DEMAND_BOOKING_MASTER.Document_No,max(Convert(varchar,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Document_Date,max(TSPL_DEMAND_BOOKING_MASTER.ShiftType)ShiftType from TSPL_DEMAND_BOOKING_MASTER
                                        left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
                                        where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
                If txtMultiRoute.arrValueMember IsNot Nothing AndAlso txtMultiRoute.arrValueMember.Count > 0 Then
                    strQry += " and TSPL_DEMAND_BOOKING_MASTER.route_no in (" + clsCommon.GetMulcallString(txtMultiRoute.arrValueMember) + ") "
                End If
                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.TR_Code not in(select TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code from TSPL_SD_SHIPMENT_BOOKING_DETAIL)
group by TSPL_DEMAND_BOOKING_MASTER.Document_No"
            ElseIf rbtnBooking.IsChecked = True Then
                If rbtnDocumentDetail.IsChecked = True Then
                    strQry = " select max(TSPL_BOOKING_DETAIL.route_no)Route_no ,
                               max(convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103)) as Document_Date ,
                               TSPL_BOOKING_MATSER.Document_No,max(TSPL_BOOKING_DETAIL.Cust_Code)Cust_Code, max(TSPL_BOOKING_MATSER.location_code)location_code,  max(Sub_Location_code)Sub_Location_code,TSPL_BOOKING_MATSER.Posted
                               from TSPL_BOOKING_MATSER
                               left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No
                               where TSPL_BOOKING_MATSER.Is_Cancelled='0' and convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
and TSPL_BOOKING_MATSER.Document_No not in( select Against_Booking_No from TSPL_SD_SHIPMENT_HEAD where Against_Booking_No is not null)"
                    If txtMultiRoute.arrValueMember IsNot Nothing AndAlso txtMultiRoute.arrValueMember.Count > 0 Then
                        strQry += "  and TSPL_BOOKING_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(txtMultiRoute.arrValueMember) + ")  "
                    End If
                    If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                        strQry += "  and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")  "
                    End If
                    If txtMultiItem.arrValueMember IsNot Nothing AndAlso txtMultiItem.arrValueMember.Count > 0 Then
                        strQry += "  and TSPL_BOOKING_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtMultiItem.arrValueMember) + ")  "
                    End If

                    strQry += "group by TSPL_BOOKING_MATSER.Document_No ,TSPL_BOOKING_MATSER.Posted"
                ElseIf rbtnBatchDetail.IsChecked = True Then
                    strQry = "select max(convert (varchar,TSPL_BATCH_ITEM.Document_Date,103))Document_Date,TSPL_BOOKING_MATSER.Document_No,max(TSPL_BATCH_ITEM.Batch_No)Batch_No,max(TSPL_BOOKING_DETAIL.Cust_Code)Cust_Code,max(TSPL_BOOKING_DETAIL.route_no)route_no,(TSPL_BATCH_ITEM.Item_Code)Item_Code,max(UOM)UOM,(qty)qty,max(TSPL_BATCH_ITEM.Location_Code)Location_Code,TSPL_BOOKING_MATSER.Posted
from TSPL_BATCH_ITEM
LEFT OUTER JOIN TSPL_BOOKING_DETAIL ON TSPL_BOOKING_DETAIL.Document_No=TSPL_BATCH_ITEM.Document_Code
left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
and TSPL_BOOKING_MATSER.Document_No not in( select Against_Booking_No from TSPL_SD_SHIPMENT_HEAD where Against_Booking_No is not null)
where 2=2  AND TSPL_BOOKING_MATSER.Is_Cancelled='0' and 
                              convert(date,TSPL_BATCH_ITEM.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BATCH_ITEM.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
                    If txtMultiRoute.arrValueMember IsNot Nothing AndAlso txtMultiRoute.arrValueMember.Count > 0 Then
                        strQry += "  and TSPL_BOOKING_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(txtMultiRoute.arrValueMember) + ")  "
                    End If
                    If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                        strQry += "  and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")  "
                    End If
                    If txtMultiItem.arrValueMember IsNot Nothing AndAlso txtMultiItem.arrValueMember.Count > 0 Then
                        strQry += "  and TSPL_BATCH_ITEM.Item_Code in (" + clsCommon.GetMulcallString(txtMultiItem.arrValueMember) + ")  "
                    End If
                    strQry += " group by  TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Posted,TSPL_BATCH_ITEM.Item_Code,qty"
                End If

            End If
            dt = clsDBFuncationality.GetDataTable(strQry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                'SetGridFormat1()
                SetGridFormationOFGV1Collection()
                'View()
                gv1.BestFitColumns()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub SetGridFormationOFGV1Collection()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next


        'gv1.Columns("Document_Date").IsVisible = False
        'gv1.Columns("Document_No").IsVisible = False
        'gv1.Columns("Route_No").IsVisible = False
        'gv1.Columns("ShiftType").IsVisible = False
        'gv1.Columns("location_code").IsVisible = False
        'gv1.Columns("Item_Code").IsVisible = False

        If rbtnDocument.IsChecked = True Then
                gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").HeaderText = "Document Date"
            gv1.Columns("Document_Date").Width = 100
            gv1.Columns("Document_No").IsVisible = True
            gv1.Columns("Document_No").HeaderText = "Document No"
            gv1.Columns("Document_No").Width = 100
            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").HeaderText = "Route No"
            gv1.Columns("Route_No").Width = 100

            gv1.Columns("ShiftType").IsVisible = True
            gv1.Columns("ShiftType").HeaderText = "Shift Type"
            gv1.Columns("ShiftType").Width = 100


        ElseIf rbtnDocumentDetail.IsChecked = True Then
                gv1.Columns("Document_Date").IsVisible = True
                gv1.Columns("Document_Date").HeaderText = "Document Date"
                gv1.Columns("Document_No").IsVisible = True
                gv1.Columns("Document_No").HeaderText = "Document No"
                gv1.Columns("Route_no").IsVisible = True
                gv1.Columns("Route_no").HeaderText = "Route No"
                gv1.Columns("Cust_Code").IsVisible = True
                gv1.Columns("Cust_Code").HeaderText = "Customer Code"
                gv1.Columns("location_code").IsVisible = True
            gv1.Columns("location_code").HeaderText = "Location code"
            gv1.Columns("Sub_Location_code").IsVisible = True
            gv1.Columns("Sub_Location_code").HeaderText = "Sub Location"


        ElseIf rbtnBatchDetail.IsChecked = True Then
                gv1.Columns("Document_Date").IsVisible = True
                gv1.Columns("Document_Date").HeaderText = "Document Date"
            gv1.Columns("Document_No").IsVisible = True
            gv1.Columns("Document_No").HeaderText = "Document Code"

            gv1.Columns("Batch_No").IsVisible = True
                gv1.Columns("Batch_No").HeaderText = "Batch No"
                gv1.Columns("Route_no").IsVisible = True
                gv1.Columns("Route_no").HeaderText = "Route No"
                gv1.Columns("Cust_Code").IsVisible = True
                gv1.Columns("Cust_Code").HeaderText = "Customer Code"
                gv1.Columns("location_code").IsVisible = True
            gv1.Columns("location_code").HeaderText = "Location"
            gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").HeaderText = "Item Code"


                gv1.Columns("UOM").IsVisible = True
                gv1.Columns("UOM").HeaderText = "UOM"

                gv1.Columns("qty").IsVisible = True
                gv1.Columns("qty").HeaderText = "Quantity"
            End If


        'Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim qty As New GridViewSummaryItem("qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(qty)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub rbtnBatchDetail_Click(sender As Object, e As EventArgs) Handles rbtnBatchDetail.Click
        Try

            txtMultiItem.Visible = True
            lblItem.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtMultiRoute.arrValueMember = Nothing
        txtMultiCustomer.arrValueMember = Nothing
        txtMultiItem.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox1.Enabled = True
        RadGroupBox2.Enabled = True
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class