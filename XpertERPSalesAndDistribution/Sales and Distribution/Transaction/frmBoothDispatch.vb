Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class frmBoothDispatch
    Inherits FrmMainTranScreen
    Dim BooFromDate As String = Nothing
    Dim BooToDate As String = Nothing
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvData
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        BooFromDate = clsCommon.myCstr(txtToDate.Value.AddDays(-1).ToShortDateString())
        BooToDate = clsCommon.myCstr(txtToDate.Value.ToShortDateString())
        Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        'Sanjay ,Add Customer Category
        'Sanjay BHA/15/08/18-000429 Add Route Code and Name
        'Sanjay Ticket No-TEC/19/06/19-000552 Short Close Qty=(DELIVERY_NOTE_Qty-SHIPMENT_Qty)
        ' Ticket No : VIJ/09/12/19-000104 By Prabhakar Add customer Group and Booking Amount And Separate Scheme Query because Qty came double 
        Dim qry As String = "select ((Convert (VARCHAR(10),TSPL_SD_SHIPMENT_HEAD.Document_Date,103)))AS [Document Date] ,(TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No) as [Invoice No],((Distributor.Cust_Code)) as [Distributor Code],((Distributor.Customer_Name)) as [Distributor Name],(Booth.Cust_Code) As [Booth Code],(Booth.Customer_Name) As [Booth Name],
(TSPL_DEMAND_BOOKING_DETAIL.Item_Code) as [Item Code],(TSPL_ITEM_MASTER.Item_Desc) As [Item Desc],(TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty)Qty,(TSPL_DEMAND_BOOKING_DETAIL.Unit_code) As [Unit Code]
from   TSPL_SD_SHIPMENT_BOOKING_DETAIL
left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_BOOKING_DETAIL.Document_Code=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE
left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.TR_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
left outer join (Select Cust_code,Customer_Name from TSPL_CUSTOMER_MASTER where IsDistributor='N')Booth  on Booth.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
left outer join (Select Cust_code,Customer_Name from TSPL_CUSTOMER_MASTER where IsDistributor='Y')Distributor  on Distributor.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where 2=2 
      and  CONVERT(date,TSPL_SD_SHIPMENT_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine &
                    "CONVERT(date,TSPL_SD_SHIPMENT_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103)"
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and Booth.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SHIPMENT_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtBooth.arrValueMember IsNot Nothing AndAlso txtBooth.arrValueMember.Count > 0 Then
            qry += " and Distributor.Cust_Code in(" + clsCommon.GetMulcallString(txtBooth.arrValueMember) + ")" + Environment.NewLine
        End If

        'qry += " Group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        Else

            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt
            gvData.Columns("Document Date").IsVisible = True

            gvData.Columns("Invoice No").IsVisible = True
            gvData.Columns("Distributor Code").IsVisible = True
            gvData.Columns("Distributor Name").IsVisible = True
            gvData.Columns("Booth Code").IsVisible = True
            gvData.Columns("Booth Name").IsVisible = True

            gvData.Columns("Item Code").IsVisible = True
            gvData.Columns("Item Desc").IsVisible = True

            gvData.Columns("Qty").IsVisible = True
            gvData.Columns("Unit Code").IsVisible = True
            'SetGridFormationOFGV1()

            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()
            Exit Sub
        End If
    End Sub

    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        ' gvData.GroupDescriptors.Add(New GridGroupByExpression("Route as RouteName format ""{0}: {1}"" Group By Route"))

        For i As Integer = 9 To gvData.Columns.Count - 1
            Dim aa = gvData.Columns(i).HeaderText()
            Dim item8 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

        Next

        gvData.ShowGroupPanel = True
        gvData.MasterTemplate.AutoExpandGroups = True

        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterTemplate.ShowTotals = True
        'ReStoreGridLayout()

    End Sub
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master WHERE isDistrIbutor='N' and Distributor_Code in  (" + clsCommon.GetMulcallString(txtBooth.arrValueMember) + ")"
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub


    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim strQry As String = ""
        strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Private Sub txtBooth__My_Click(sender As Object, e As EventArgs) Handles txtBooth._My_Click
        Dim strQry1 As String = "select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master WHERE isDistrIbutor='Y'"
        txtBooth.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBoothsale@Master", strQry1, "Code", "", txtBooth.arrValueMember, txtBooth.arrDispalyMember)

    End Sub
    Private Sub frmBoothDispatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        txtBooth.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtMultiCustomer.arrValueMember = Nothing



    End Sub
End Class