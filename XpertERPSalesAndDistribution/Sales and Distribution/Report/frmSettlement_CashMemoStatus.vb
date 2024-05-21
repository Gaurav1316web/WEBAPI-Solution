'-06/11/2012-11:51AM--Updation By--[Pankaj Kumar]--Added New Column [Return Sale] And Total Gross Sale=(Gross Sale-Sale return) AND Net Sale=(Net Sale-Sale  Return)----Fwd By--Ranjan Mam
'-19/11/2012-05:15PM--Updation By--[Pankaj Kumar]--WHile Selecting Data of 9 Sep in SAN, (Subqry returns More Than One Values) Error was Occuring Due to the Same Item of FOC In Sale Return----Fwd By--Ranjan Mam
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------

Imports XpertERPEngine
Imports common
Imports System.Data.SqlClient

Public Class FrmSettlement_CashMemoStatus
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Private Sub FrmSettlement_CashMemoStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        LoadRoute()
        LoadRouteType()
        SetDataBaseGrid()
        reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSettlement_CashMemoStatus)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        chkLocAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkRouteTypeAll.IsChecked = True
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next

        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
    End Sub


    Sub LoadLocation()
        Dim strquery As String = "SELECT Location_Code AS [Code], Location_Desc AS [Description] FROM TSPL_LOCATION_MASTER where Location_Type='Physical' AND Excisable='F'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadRoute()
        Dim strquery As String = "select Route_No,Route_Desc from TSPL_ROUTE_MASTER order by Route_Desc"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgRoute.ValueMember = "Route_No"
        cbgRoute.DisplayMember = "Route_Desc"
    End Sub

    Sub LoadRouteType()
        Dim strquery As String = " Select DISTINCT TSPL_TRANSFER_HEAD.Route_Type_Id as [Route Type], TSPL_ROUTE_TYPE.Route_Type_Desc as Description  from TSPL_TRANSFER_HEAD Left Outer Join TSPL_ROUTE_TYPE On TSPL_ROUTE_TYPE.Route_Type_Id = TSPL_TRANSFER_HEAD.Route_Type_Id WHere ISNULL(TSPL_TRANSFER_HEAD.Route_Type_Id, '') <> ''"
        cbgRouteType.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgRouteType.ValueMember = "Route Type"
        cbgRouteType.DisplayMember = "Description"
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = False
    End Sub

    Private Sub rbtnSelectCompany_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelectCompany.ToggleStateChanged
        gvDB.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkRouteSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub

    Private Sub chkRouteTypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteTypeAll.ToggleStateChanged
        cbgRouteType.Enabled = False
    End Sub

    Private Sub chkRouteTypeSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteTypeSelect.ToggleStateChanged
        cbgRouteType.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ' ExportToExcel()
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            RefreshData()
            If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV(exporter)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshData()
    End Sub

    Private Sub RefreshData()
        Try
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select at least Single Location or Select All")
                Return
            End If
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select at least Single Route Or Select All")
                Return
            End If
            If chkRouteTypeSelect.IsChecked AndAlso cbgRouteType.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select at least Single RouteType Or Select All")
                Return
            End If
            Dim qry As String = ""
            Dim group1 As String = ""
            Dim sku As String = ""
            Dim ec As String = ""
            Dim value As String = ""

            qry = " SELECT [Load Out/Transfer No], MAX([TransferDate]) AS [LoadOut/Transfer Date], MAX([Route No]) AS [Route No], MAX([Route Name]) AS [Route Name], " & _
        " MAX([Salesman Name]) AS [Salesman Name], MAX([LoadOut Qty]) AS [LoadOut Qty], MAX([LoadIn Qty]) AS[LoadIn Qty], MAX([CashmemoatQuickSettlement]) " & _
        " AS [Cash Memo At Quick Settlement] , MAX([NetLoad(ProvisionalSales)Qty]) AS [Net Load(Provisional Sales) Qty], MAX([ProvisionalNetSaleQty]) AS " & _
        " [Provisional Net Sale Qty], MAX([PostedCashMemo]) AS [Posted Punch], MAX([PendingCashMemo]) AS [Pending Punch], CONVERT(DECIMAL(18,2),SUM([GrossSale])) AS [Gross Sale Qty], CONVERT(DECIMAL(18,2),SUM([Sale Return]), 103) as [Sale Return], CONVERT(DECIMAL(18,2),(SUM([GrossSale])-SUM([Sale Return])),103) as [Net Gross Sale], CONVERT(DECIMAL(18,2),(SUM([Net Sales])-SUM([Sale Return]))) AS " & _
        " [Net Sale], (CONVERT(DECIMAL(18,2),SUM([GrossSale]))-SUM(CONVERT(DECIMAL(18,2),[Net Sales]))) AS Discount, CONVERT(DECIMAL(18,2),(MAX([NetLoad(ProvisionalSales)Qty])-SUM([GrossSale])+ SUM([Sale Return]))) AS [Balance Gross Sale Qty], CONVERT(DECIMAL(18,2),(MAX([ProvisionalNetSaleQty])-SUM([Net Sales]))) AS " & _
        " [Balance Net Sale Qty],convert(decimal(18,0),CASE WHEN ISNULL(MAX([NetLoad(ProvisionalSales)Qty]), 0)=0 THEN 0 Else CONVERT(DECIMAL(18,2),SUM([GrossSale]))*100/MAX([NetLoad(ProvisionalSales)Qty]) end ) AS [% Complete]   FROM ( " & _
        " SELECT " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS [Load Out/Transfer No], Convert(Varchar(12), " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date, 103) AS [TransferDate], " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteNo AS [Route No], " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS [Route Name], " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Salesman AS [Salesman Name], ISNULL(" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Net_LoadOutQty, 0) AS [LoadOut Qty], ISNULL(" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Net_LoadInQty, 0) AS [LoadIn Qty], ISNULL(" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.CashMemo, 0) AS [CashmemoatQuickSettlement], ISNULL(" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Net_ProvisionalQty, 0)  AS [NetLoad(ProvisionalSales)Qty],  ISNULL(" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Net_SalesQty, 0)  AS [ProvisionalNetSaleQty],  " & _
        " (SELECT COUNT(*) FROM  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER WHERE  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No=" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Is_Post='Y') AS [PostedCashMemo]," & _
        " (SELECT COUNT(*) FROM  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER WHERE  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No= " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Is_Post<>'Y') AS [PendingCashMemo], " & _
        " ISNULL(case when " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FC'  then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty  when  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FB' then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code =" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as [GrossSale]," & _
        " (select ISNULL( sum(RetQty),0) from (Select ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty, 0)/(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ) as RetQty from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD  on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No Where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No=" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No=" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL .Item_Code =" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS .Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code =" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS .Unit_code) xxx) as [Sale Return] ," & _
        " ISNULL(case when (" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FC' AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Scheme_Item='N')   then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty  WHEN (  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FB' AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Scheme_Item='N') then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code =" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) AS [Net Sales]" & _
        " FROM " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent LEFT OUTER JOIN" & _
        " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
        " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No " & _
        " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS ON  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipment_No= " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No" & _
        " WHERE CONVERT(DATE,tspl_QuickSettleMent.Transfer_Date, 103)>=CONVERT(DATE, '" + txtFromDate.Value.Date + "', 103) AND CONVERT(DATE,tspl_QuickSettleMent.Transfer_Date, 103)<=CONVERT(DATE, '" + txtToDate.Value.Date + "', 103) and  Net_LoadOutQty > 0 AND tspl_QuickSettleMent.Post='Y' "
            If chkRouteSelect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteNo in (" + (clsCommon.GetMulcallString(cbgRoute.CheckedValue)) + ") "
            End If
            If chkLocSelect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in(" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ") "
            End If
            If chkRouteTypeSelect.IsChecked Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in(" + (clsCommon.GetMulcallString(cbgRouteType.CheckedValue)) + ") "
            End If

            qry += " ) XXX GROUP BY [Load Out/Transfer No] "

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)

            '--------------------------------------------------------------------------

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.MasterTemplate.SummaryRowsBottom.Clear()
                gvReport.DataSource = Nothing
                gvReport.Rows.Clear()
                gvReport.Columns.Clear()
                gvReport.DataSource = dt
                FormatGrid()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")
            If rbtnSelectCompany.IsChecked Then
                strTemp = ""
                For ii As Integer = 0 To gvDB.Rows.Count - 1
                    If clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value) Then
                        Dim Str As String = clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompName).Value)
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str
                    End If
                Next
                arrHeader.Add("Company : " + strTemp)
            End If
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location Segment : " + strTemp)
            End If

            If chkRouteSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgRoute.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Route : " + strTemp)
            End If
            If chkRouteTypeSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgRouteType.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("RouteType : " + strTemp)
            End If
            ' clsCommon.MyExportToExcel("Settlement/Cash Memo Status ", gvReport, arrHeader, Me.Text)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Settlement/Cash Memo Status ", gvReport, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Settlement/Cash Memo Status  ", gvReport, arrHeader, "Settlement/Cash Memo Status", True)
            End If

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next

        gvReport.Columns("Load Out/Transfer No").IsVisible = True
        gvReport.Columns("Load Out/Transfer No").Width = 121
        gvReport.Columns("Load Out/Transfer No").HeaderText = "Load Out/Transfer No"

        gvReport.Columns("LoadOut/Transfer Date").IsVisible = True
        gvReport.Columns("LoadOut/Transfer Date").Width = 121
        gvReport.Columns("LoadOut/Transfer Date").HeaderText = "LoadOut/Transfer Date"

        gvReport.Columns("Route No").IsVisible = True
        gvReport.Columns("Route No").Width = 71
        gvReport.Columns("Route No").HeaderText = "Route No"

        gvReport.Columns("Route Name").IsVisible = True
        gvReport.Columns("Route Name").Width = 201
        gvReport.Columns("Route Name").HeaderText = "Route Name"

        gvReport.Columns("Salesman Name").IsVisible = True
        gvReport.Columns("Salesman Name").Width = 151
        gvReport.Columns("Salesman Name").HeaderText = "Salesman Name "

        gvReport.Columns("LoadOut Qty").IsVisible = True
        gvReport.Columns("LoadOut Qty").Width = 71
        gvReport.Columns("LoadOut Qty").HeaderText = "LoadOut Qty "

        gvReport.Columns("LoadIn Qty").Width = 71
        gvReport.Columns("LoadIn Qty").HeaderText = "LoadIn Qty"
        gvReport.Columns("LoadIn Qty").IsVisible = True

        gvReport.Columns("Cash Memo At Quick Settlement").Width = 151
        gvReport.Columns("Cash Memo At Quick Settlement").HeaderText = "Cash Memo At Quick Settlement"
        gvReport.Columns("Cash Memo At Quick Settlement").IsVisible = True

        gvReport.Columns("Net Load(Provisional Sales) Qty").IsVisible = True
        gvReport.Columns("Net Load(Provisional Sales) Qty").Width = 151
        gvReport.Columns("Net Load(Provisional Sales) Qty").HeaderText = "Net Load(Provisional Sales) Qty"

        gvReport.Columns("Provisional Net Sale Qty").IsVisible = True
        gvReport.Columns("Provisional Net Sale Qty").Width = 151
        gvReport.Columns("Provisional Net Sale Qty").HeaderText = "Provisional Net Sale Qty"

        gvReport.Columns("Posted Punch").IsVisible = True
        gvReport.Columns("Posted Punch").Width = 101

        gvReport.Columns("Pending Punch").IsVisible = True
        gvReport.Columns("Pending Punch").Width = 101

        gvReport.Columns("Gross Sale Qty").IsVisible = True
        gvReport.Columns("Gross Sale Qty").Width = 151

        gvReport.Columns("Sale Return").IsVisible = True
        gvReport.Columns("Sale Return").Width = 151

        gvReport.Columns("Net Gross Sale").IsVisible = True
        gvReport.Columns("Net Gross Sale").Width = 151

        gvReport.Columns("Net Sale").IsVisible = True
        gvReport.Columns("Net Sale").Width = 71
        gvReport.Columns("Net Sale").HeaderText = "Net Sale"

        gvReport.Columns("Discount").IsVisible = True
        gvReport.Columns("Discount").Width = 71

        gvReport.Columns("Balance Gross Sale Qty").IsVisible = True
        gvReport.Columns("Balance Gross Sale Qty").Width = 121
        gvReport.Columns("Balance Gross Sale Qty").HeaderText = "Mismatched Gross Sale"

        'gvReport.Columns("Balance Net Sale Qty").IsVisible = True
        'gvReport.Columns("Balance Net Sale Qty").Width = 121
        'gvReport.Columns("Balance Net Sale Qty").HeaderText = "Mismatched Net Sale"

        gvReport.Columns("% Complete").IsVisible = True
        gvReport.Columns("% Complete").Width = 71
        gvReport.Columns("% Complete").HeaderText = "% Complete"

        '---------------Total of Container Deposite, Invoice Amt, Total Invoice Amt,Balance Amt----- 
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SumLOQty As New GridViewSummaryItem("LoadOut Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumLOQty)
        Dim SumLIQty As New GridViewSummaryItem("LoadIn Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumLIQty)
        Dim SumCshMmQSt As New GridViewSummaryItem("Cash Memo At Quick Settlement", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumCshMmQSt)
        Dim SumBalAmt As New GridViewSummaryItem("Net Load(Provisional Sales) Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumBalAmt)

        Dim SumPNSQ As New GridViewSummaryItem("Provisional Net Sale Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumPNSQ)
        Dim SUMPOP As New GridViewSummaryItem("Posted Punch", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMPOP)
        Dim SUMPEP As New GridViewSummaryItem("Pending Punch", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMPEP)
        Dim SumGSQ As New GridViewSummaryItem("Gross Sale Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumGSQ)
        Dim SumSR As New GridViewSummaryItem("Sale Return", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumSR)
        Dim SumNGS As New GridViewSummaryItem("Net Gross Sale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumNGS)
        Dim SumDiscount As New GridViewSummaryItem("Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumDiscount)
        Dim SumNS As New GridViewSummaryItem("Net Sale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumNS)
        Dim SUMBGSQ As New GridViewSummaryItem("Balance Gross Sale Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMBGSQ)
        Dim TTL As New GridViewSummaryItem("% Complete", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(TTL)
        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvReport.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        '--------------------------------------------------------------------------------------------
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
    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Private Sub gvReport_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvReport.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        ExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class

