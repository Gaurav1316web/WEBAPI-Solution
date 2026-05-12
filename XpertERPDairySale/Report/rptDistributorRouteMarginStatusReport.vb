Imports System.IO
Imports common
Imports System.Text
Imports common.UserControls
Public Class rptDistributorRouteMarginStatusReport
    Inherits FrmMainTranScreen
    Private Sub rptDistributorRouteMarginStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        rdbDetail.IsChecked = True
        rdbAll.IsChecked = False
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim strQry As String = ""
            strQry = "select DISTINCT Route_No  as [Code],Route_Desc as [Name] from TSPL_CUSTOMER_MASTER"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtMultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomer._My_Click
        Try
            Dim strQry As String = ""
            strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
            txtMultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtMultCustomer.arrValueMember, txtMultCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " select Item_Code as [Code],Item_Desc as [Name] from TSPL_ITEM_MASTER order by Item_Code "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSelItem", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Summarydata(ByVal prints As Boolean)
        Dim qry As String = Nothing
        Try
            qry = "Select xx.Logo_Img,xx.Logo_Img2, XX.SrNo,XX.Start_Date, XX.Route_No,XX.Route_Desc,XX.Type,XX.Zone_Code,XX.Cust_Code,XX.Customer_Name,Rate,XX.dist_code,XX.Dis_comm_code,XX.Comp_Name,XX.Add1,XX.ADD2,XX.UserName from 
(Select '" + objCommonVar.CurrentUserCode + "' as UserName ,  ROW_NUMBER() OVER (ORDER BY TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No) AS SrNo, CONVERT(VARCHAR,Start_Date,103)Start_Date,TSPL_DISTRIBUTOR_ROUTE.Code,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.Type,TSPL_ROUTE_MASTER.Zone_Code,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code,
TSPL_CUSTOMER_MASTER.Customer_Name,Rate,TSPL_DISTRIBUTOR_ROUTE.Code as [dist_code],TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No as [Dis_comm_code]
,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.ADD2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2
from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER 
LEFT OUTER JOIN TSPL_DISTRIBUTOR_ROUTE ON TSPL_DISTRIBUTOR_ROUTE.CODE=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code
left outer join TSPL_DISTRIBUTOR_COMMISSION_HEAD on TSPL_DISTRIBUTOR_COMMISSION_HEAD.Distributor_Tagging_Code=TSPL_DISTRIBUTOR_ROUTE.Code
left outer join TSPL_DISTRIBUTOR_COMMISSION_DETAIL on TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No=TSPL_DISTRIBUTOR_COMMISSION_HEAD.Doc_No 
and TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Distributor_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code AND TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Route_Code =TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No
LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No
LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code
LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
WHERE 2=2 "
            qry += " AND  CONVERT(DATE,TSPL_DISTRIBUTOR_ROUTE.Start_Date,103) = CONVERT(DATE,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "',103)"
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
            End If
            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code in(" & clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) & ")" & Environment.NewLine
            End If
            qry += " )XX"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                gv2.AutoExpandGroups = True
                gv2.ShowGroupPanel = True
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                EnableDisableControl(False)
                FormatGrid()
                ' View()
                gv2.BestFitColumns()
                If prints = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PurchaseOrder, dt, "rptDistMraginstatusSummary", "Dist Mragin status Summary")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Detaildata(ByVal prints As Boolean)
        Try
            Dim qry As String = " SELECT TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, '" + objCommonVar.CurrentUserCode + "' as UserName ,  ROW_NUMBER() OVER (ORDER BY TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No) AS SrNo, convert(varchar,TSPL_DISTRIBUTOR_COMMISSION_HEAD.Document_Date,103) AS DATE,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No 
,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.Type ,TSPL_ROUTE_MASTER.Zone_Code,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code   ,TSPL_CUSTOMER_MASTER.Customer_Name ,
TSPL_DISTRIBUTOR_COMMISSION_ITEMS.Item_Code,tspl_item_master.Item_Desc,TSPL_DISTRIBUTOR_COMMISSION_HEAD.Commision_UOM,
TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Rate ,TSPL_DISTRIBUTOR_ROUTE.Code as [dist_code],TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No as [Dis_comm_code]
,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.ADD2
FROM  TSPL_DISTRIBUTOR_ROUTE
LEFT OUTER JOIN TSPL_DISTRIBUTOR_ROUTE_CUSTOMER ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.CODE=TSPL_DISTRIBUTOR_ROUTE.Code
LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No
LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code
left outer join TSPL_DISTRIBUTOR_COMMISSION_HEAD on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_COMMISSION_HEAD.Distributor_Tagging_Code
left outer join TSPL_DISTRIBUTOR_COMMISSION_DETAIL on TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No=TSPL_DISTRIBUTOR_COMMISSION_HEAD.Doc_No 
and TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Distributor_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code AND TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Route_Code =TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No
left outer join TSPL_DISTRIBUTOR_COMMISSION_ITEMS on TSPL_DISTRIBUTOR_COMMISSION_ITEMS.Doc_No=TSPL_DISTRIBUTOR_COMMISSION_HEAD.Doc_No
left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_DISTRIBUTOR_COMMISSION_ITEMS.Item_Code 
LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
where 2=2 
and convert (date,TSPL_DISTRIBUTOR_COMMISSION_HEAD.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "',103)"

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_ROUTE_MASTER.Route_No in (" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
            End If

            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) & ")" & Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += "  and tspl_item_master.Item_code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                gv2.AutoExpandGroups = True
                gv2.ShowGroupPanel = True
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                EnableDisableControl(False)
                FormatGrid()
                'View()
                gv2.BestFitColumns()
                If prints = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PurchaseOrder, dt, "rptDistMraginstatusDetail", "Dist Mragin status Detail")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If rdbDetail.IsChecked Then
            Detaildata(False)
        ElseIf rdbAll.IsChecked = True Then
            Summarydata(False)
        End If
    End Sub
    Sub View()

        Dim view As New ColumnGroupsViewDefinition()
        view.ColumnGroups.Add(New GridViewColumnGroup(" "))
        view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
        'If rdbDetails.Checked = True Then
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Route_Code").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Route_type").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Start_Date").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Zone_Code").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Party_Code").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Customer_Name").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Rate").Name)
        If rdbDetail.IsChecked Then
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Commision_UOM").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Item_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("Item_Desc").Name)
        End If
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("TaggingDoc").Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("MarginDoc").Name)
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Rate As New GridViewSummaryItem("Rate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Rate)
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        gv2.AutoSizeRows = True
        gv2.BestFitColumns()
        gv2.MasterTemplate.AutoExpandGroups = True

    End Sub
    Sub FormatGrid()
        gv2.AutoExpandGroups = False
        gv2.ShowGroupPanel = False
        gv2.ShowRowHeaderColumn = False
        gv2.AllowAddNewRow = False
        gv2.AllowDeleteRow = False
        gv2.EnableFiltering = True
        gv2.ShowFilteringRow = True
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).BestFit()
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If rdbAll.IsChecked Then
            gv2.Columns("UserName").IsVisible = False
            gv2.Columns("Logo_Img").IsVisible = False
            gv2.Columns("Logo_Img2").IsVisible = False
            gv2.Columns("Start_Date").HeaderText = "Date"
            gv2.Columns("Route_No").HeaderText = "Route Code"
            gv2.Columns("Route_Desc").HeaderText = "Route Name"
            gv2.Columns("Type").HeaderText = "Route Type"
            gv2.Columns("Cust_Code").HeaderText = "Party Code"
            gv2.Columns("Customer_Name").HeaderText = "Party Name"
            gv2.Columns("dist_code").IsVisible = False
            gv2.Columns("dist_code").VisibleInColumnChooser = True
            gv2.Columns("dist_code").HeaderText = "Tagging Doc No."
            gv2.Columns("Dis_comm_code").IsVisible = False
            gv2.Columns("Dis_comm_code").VisibleInColumnChooser = True
            gv2.Columns("Dis_comm_code").HeaderText = "Margin Doc No."
            gv2.Columns("Zone_Code").HeaderText = "Zone code"
            gv2.Columns("Rate").HeaderText = "Rate"
            gv2.Columns("Comp_Name").IsVisible = False
            gv2.Columns("ADD2").IsVisible = False
            gv2.Columns("Add1").IsVisible = False
        End If
        If rdbDetail.IsChecked Then
            gv2.Columns("UserName").IsVisible = False
            gv2.Columns("Logo_Img").IsVisible = False
            gv2.Columns("Logo_Img2").IsVisible = False
            gv2.Columns("DATE").HeaderText = "Date"
            gv2.Columns("Route_No").HeaderText = "Route Code"
            gv2.Columns("Route_Desc").HeaderText = "Route Name"
            gv2.Columns("Type").HeaderText = "Route Type"
            gv2.Columns("Zone_Code").HeaderText = "Zone code"
            gv2.Columns("Cust_Code").HeaderText = "Party Code"
            gv2.Columns("Customer_Name").HeaderText = "Party Name"
            gv2.Columns("Item_Code").HeaderText = "Item Code"
            gv2.Columns("Item_Desc").HeaderText = "Item Name"
            gv2.Columns("Commision_UOM").HeaderText = "UOM"
            gv2.Columns("Rate").HeaderText = "Rate"
            gv2.Columns("dist_code").IsVisible = False
            gv2.Columns("dist_code").VisibleInColumnChooser = True
            gv2.Columns("dist_code").HeaderText = "Tagging Doc No."
            gv2.Columns("Dis_comm_code").IsVisible = False
            gv2.Columns("Dis_comm_code").VisibleInColumnChooser = True
            gv2.Columns("Dis_comm_code").HeaderText = "Margin Doc No."
            gv2.Columns("Comp_Name").IsVisible = False
            gv2.Columns("ADD2").IsVisible = False
            gv2.Columns("Add1").IsVisible = False
        End If
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Rate As New GridViewSummaryItem("Rate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Rate)
        gv2.ShowGroupPanel = True
        gv2.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        RadGroupBox4.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        EnableDisableControl(True)

        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox1.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtMultCustomer.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        rdbDetail.IsChecked = True
        rdbAll.IsChecked = False
        rbdCommission.IsChecked = True
        rbdTPT.IsChecked = False
    End Sub

    Private Sub btnExcle_Click(sender As Object, e As EventArgs) Handles btnExcle.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDistributorRouteMarginStatusReport & "'"))
                transportSql.QuickExportToExcel(gv2, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rdbAll_Click(sender As Object, e As EventArgs) Handles rdbAll.Click
        Try
            lblItem.Visible = False
            txtItem.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rdbDetail_Click(sender As Object, e As EventArgs) Handles rdbDetail.Click
        Try
            lblItem.Visible = True
            txtItem.Visible = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If rdbDetail.IsChecked Then
            Detaildata(True)
        ElseIf rdbAll.IsChecked = True Then
            Summarydata(True)
        End If
    End Sub
End Class