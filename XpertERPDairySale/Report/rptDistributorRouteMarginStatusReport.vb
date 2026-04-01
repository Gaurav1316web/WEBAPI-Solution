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
        Dim strQry As String = ""
        strQry = "select DISTINCT Route_No  as [Code],Route_Desc as [Name] from TSPL_CUSTOMER_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Private Sub txtMultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomer._My_Click
        Dim strQry As String = ""
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtMultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtMultCustomer.arrValueMember, txtMultCustomer.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim qry As String = Nothing
        Dim whr As String = Nothing
        Try
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whr = " and TSPL_CUSTOMER_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
            End If

            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" & clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) & ")" & Environment.NewLine
            End If
            Dim code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 code FROM TSPL_DISTRIBUTOR_ROUTE
            WHERE Convert(Date,Start_Date,103) <='" + clsCommon.GetPrintDate(fromDate.Value) + "'   ORDER BY Start_Date DESC"))

            'If rdbAll.IsChecked Then
            qry = " select Route_Code as Route_Code,TSPL_DISTRIBUTOR_COMMISSION_HEAD.Item_Type as Route_type,convert(varchar,TSPL_DISTRIBUTOR_ROUTE.Start_Date,103) as Start_Date,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Distributor_Code as Party_Code,TSPL_CUSTOMER_MASTER.Customer_Name,"
            If rbdCommission.IsChecked Then
                qry += " TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Rate as Rate,"
            Else
                qry += " Security_Rate as Rate,"
            End If
            If rdbDetail.IsChecked Then
                qry += " TSPL_DISTRIBUTOR_COMMISSION_HEAD.Commision_UOM,TSPL_DISTRIBUTOR_COMMISSION_ITEMS.Item_Code, tspl_item_master.Item_Desc, "
            End If

            qry += "TSPL_DISTRIBUTOR_ROUTE.code as TaggingDoc,TSPL_DISTRIBUTOR_COMMISSION_HEAD.Doc_No as MarginDoc from TSPL_DISTRIBUTOR_COMMISSION_DETAIL
                  left outer join TSPL_DISTRIBUTOR_COMMISSION_HEAD on TSPL_DISTRIBUTOR_COMMISSION_HEAD.Doc_No=TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Doc_No
                  left outer join TSPL_DISTRIBUTOR_ROUTE on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_COMMISSION_HEAD.Distributor_Tagging_Code
                  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Distributor_Code  "
            If rdbDetail.IsChecked Then
                qry += " LEFT OUTER JOIN TSPL_DISTRIBUTOR_COMMISSION_ITEMS ON TSPL_DISTRIBUTOR_COMMISSION_ITEMS.PK_ID=TSPL_DISTRIBUTOR_COMMISSION_DETAIL.PK_ID 
				         left outer join tspl_item_master on  tspl_item_master.Item_Code=TSPL_DISTRIBUTOR_COMMISSION_ITEMS.Item_Code "
            End If
            qry +=" where 2=2 and TSPL_DISTRIBUTOR_ROUTE.Code = '" + code + "'"
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Route_Code in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
            End If

                If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" & clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
            'End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                'SetGridFormationgvData()
                gv2.AutoExpandGroups = True
                gv2.ShowGroupPanel = True
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                FormatGrid()
                View()
                gv2.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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


        gv2.ViewDefinition = view

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

        gv2.Columns("Route_Code").HeaderText = "Route Code"
        gv2.Columns("Route_type").IsVisible = False
        gv2.Columns("Route_type").VisibleInColumnChooser = True
        gv2.Columns("Route_type").HeaderText = "Route Type"
        gv2.Columns("Start_Date").HeaderText = "Date"
        gv2.Columns("Zone_Code").HeaderText = "Zone"
        If rdbDetail.IsChecked Then
            gv2.Columns("Commision_UOM").HeaderText = "UOM"
            gv2.Columns("Item_Code").HeaderText = "Item Code"
            gv2.Columns("Item_Desc").HeaderText = "Item Name"
        End If
        gv2.Columns("Party_Code").HeaderText = "Party Code"
        gv2.Columns("Customer_Name").HeaderText = "Party Name"
        gv2.Columns("Rate").HeaderText = "Margin Rate"
        gv2.Columns("TaggingDoc").HeaderText = "Tagging Doc No."
        gv2.Columns("MarginDoc").HeaderText = "Margin Doc No."

        gv2.ShowGroupPanel = True
        gv2.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtRoute.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
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
End Class