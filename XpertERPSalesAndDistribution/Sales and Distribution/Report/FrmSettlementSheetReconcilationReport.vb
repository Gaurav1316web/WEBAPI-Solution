
'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 31/12/2012-------------------------------------
'--------------------------------Last modify Time - 10:50 pm -------------------------------------
'--------------------------------Last modify date - 02/01/2013 11:15 AM-------------------------------------
'--------------------------------Last modify date - 02/01/2013 02:45 AM-------------------------------------
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'By vipin for pdf on 11/02/2013
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common





Public Class FrmSettlementSheetReconcilationReport
    Inherits FrmMainTranScreen
    Dim ArrDBName As ArrayList = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSettlementSheetReconcilationeport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadLocation()

        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadRouteType()
        Dim qry As String = "select Route_Type_Id as Code,Route_Type_Desc as Name from TSPL_ROUTE_TYPE"
        cbgRouteType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRouteType.ValueMember = "Code"
        cbgRouteType.DisplayMember = "Name"
    End Sub
    Sub LoadRoute()
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

   
    Private Sub FrmSettlementSheetReconcilationReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        LoadLocation()
        chkLocatioAll.IsChecked = True

        LoadRoute()
        chkRouteAll.IsChecked = True

        LoadRouteType()
        chkRouteTypeAll.IsChecked = True

        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()

        LoadCompany()
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


        rdbBoth.IsChecked = True
        Me.Text = "Settlement Reconcilation Report "
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub

    Private Sub chkRouteTypeAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteTypeAll.ToggleStateChanged
        cbgRouteType.Enabled = Not chkRouteTypeAll.IsChecked
    End Sub

    Sub Reset()
        chkLocatioAll.IsChecked = True
        chkRouteAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rbtnCompanyAll.IsChecked = True
        chkRouteTypeAll.IsChecked = True
        rdbBoth.IsChecked = True
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnCLos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLos.Click
        Me.Close()
    End Sub
    Sub SetGridFormationOFgvReport()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False


        gv1.Columns("DocNo").IsVisible = True
        gv1.Columns("DocNo").Width = 100
        gv1.Columns("DocNo").HeaderText = "LoadOut No/Direct Invoice"

        gv1.Columns("loadoutqty").IsVisible = True
        gv1.Columns("loadoutqty").Width = 100
        gv1.Columns("loadoutqty").HeaderText = "Load Out"

        gv1.Columns("loadInqty").IsVisible = True
        gv1.Columns("loadInqty").Width = 100
        gv1.Columns("loadInqty").HeaderText = "Load In"

        gv1.Columns("NetSaleQty").IsVisible = True
        gv1.Columns("NetSaleQty").Width = 100
        gv1.Columns("NetSaleQty").HeaderText = "Net Sale Qty"

        gv1.Columns("NetSaleAmt").IsVisible = True
        gv1.Columns("NetSaleAmt").Width = 100
        gv1.Columns("NetSaleAmt").HeaderText = "Net Sale Amount"


        gv1.Columns("CashCollection").IsVisible = True
        gv1.Columns("CashCollection").Width = 100
        gv1.Columns("CashCollection").HeaderText = "Cash Collection"


        gv1.Columns("ChequeCollection").IsVisible = True
        gv1.Columns("ChequeCollection").Width = 100
        gv1.Columns("ChequeCollection").HeaderText = "Cheque Collection"

        gv1.Columns("NetShortExcess").IsVisible = True
        gv1.Columns("NetShortExcess").Width = 100
        gv1.Columns("NetShortExcess").HeaderText = "Net Short/Excess"

        gv1.Columns("RecAmt").IsVisible = True
        gv1.Columns("RecAmt").Width = 100
        gv1.Columns("RecAmt").HeaderText = "Amount Collected As Per Memo"

        gv1.Columns("GAP").IsVisible = True
        gv1.Columns("GAP").Width = 100
        gv1.Columns("GAP").HeaderText = "Gap"

        gv1.Columns("TotalExpense").IsVisible = True
        gv1.Columns("TotalExpense").Width = 100
        gv1.Columns("TotalExpense").HeaderText = "Other Exp and Shortage"

        gv1.Columns("NetGap").IsVisible = True
        gv1.Columns("NetGap").Width = 100
        gv1.Columns("NetGap").HeaderText = "Net Gap"

        Dim strItemCode As String
        Dim intCount As Integer
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If rdnNonRoutewise.IsChecked = False Then

            For ii As Integer = 5 To gv1.Columns.Count - 8
                strItemCode = gv1.Columns(ii).FieldName
                gv1.Columns("" & strItemCode & "").IsVisible = True
                gv1.Columns("" & strItemCode & "").Width = 80
                gv1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next

            For ii As Integer = 5 To gv1.Columns.Count - 8
                intCount = intCount + 1
                strItemCode = gv1.Columns(ii).FieldName
                Dim item20 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item20)
            Next
        End If

        Dim item1 As New GridViewSummaryItem("loadoutqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("loadInqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("NetSaleQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("NetSaleAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("CashCollection", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("ChequeCollection", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("NetShortExcess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("RecAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("GAP", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("TotalExpense", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("NetGap", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)


        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            If chkRouteTypeSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgRouteType.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Route Type : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If


            ' clsCommon.MyExportToExcel("Mismatch Settlement Report", gv1, arrHeader, Me.Text)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Mismatch Settlement Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Mismatch Settlement Report", gv1, arrHeader, "Mismatch Settlement Report", True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        gv1.EnableFiltering = True
        Print()
    End Sub

    Sub Print()
        Try
            Dim strLocAll, strRouteAll, strRouteTypeAll, strTransfer, strSale, strQuery, strFinalQuery, strType As String
            strLocAll = ""
            strRouteAll = ""
            strRouteTypeAll = ""
            strTransfer = ""
            strSale = ""
            strQuery = ""
            strFinalQuery = ""
            strType = ""
            Dim dt As New DataTable

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
                Return
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
                Return
            ElseIf chkRouteTypeSelect.IsChecked = True AndAlso cbgRouteType.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route Type or select ALL")
                Return
            End If

            '' Quick settlement detail
            strTransfer = "select Transfer_Number as DocNo,0 as loadoutqty,0 as loadInqty,Amount, " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.Description as SettDesc," & _
            "case when SettleMent_Type='CSH' then Amount else 0 end as CashCollection, " & _
            "case when SettleMent_Type='CHQ' then Amount else 0 end as ChequeCollection, " & _
            "case when SettleMent_Type='CSE' then case when Calculate='A' then " & _
            "Amount * (-1) else Amount end else 0 end as NetShortExcess, " & _
            "0 as RecAmt,0 as LoadoutAmt,0 as LoadInAmt,0 as Disc,0 as SettleAmt,'Transfer' as Type from " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent  left outer join " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail on " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id=" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id  " & _
            "left outer join " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master " & _
            "on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code=" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  "

            If strLocAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strTransfer += " Union All "

            '' transfer Loadout detail

            strTransfer += "select Transfer_Number  as DocNo,Item_Qty/Conversion_Factor  AS loadoutqty, " & _
            "0 as loadInqty,0 as Amount,''  as SettDesc,0 as CashCollection,0  as ChequeCollection, " & _
            "0 as NetShortExcess,0 as RecAmt,(BasicPrice_WithTax + TPT_Value) * Item_Qty as LoadoutAmt, " & _
            "0 as LoadInAmt,0 as Disc,0 as SettleAmt,'Transfer' as Type from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent  left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code " & _
            "and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            "where Transfer_Type='LO' and Uom not in ('sh') " & _
            " and   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  "


            If strLocAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strTransfer += " Union All "


            '' transfer LoadIn detail
            strTransfer += "select Transfer_Number as DocNo,0 as loadoutqty, " & _
            "(LoadIn_Qty+Leak+Breakage+Burst+Shortage)/Conversion_Factor  AS loadInqty,0 as Amount, " & _
            "''  as SettDesc,0 as CashCollection,0  as ChequeCollection,0 as NetShortExcess,0 as RecAmt, " & _
            "0 as LoadoutAmt,(BasicPrice_WithTax + TPT_Value) * (LoadIn_Qty + Leak + Burst + Breakage + Shortage) as LoadInAmt, " & _
            "0 as Disc,0 as SettleAmt,'Transfer' as Type from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent  left outer join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code where Transfer_Type='LI' and Uom not in ('sh')" & _
            " and   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  "


            If strLocAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strTransfer += " Union All "

           
            '' Receipt amount for  transfer 

            strTransfer += "SELECT  case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then " & _
            "Transfer_No else Sale_Invoice_No end as DocNo,0 as loadoutqty,0 AS loadInqty,0 as Amount, " & _
            "''  as SettDesc,0 as CashCollection,0  as ChequeCollection,0 as NetShortExcess, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount AS RecAmt,0 as LoadoutAmt,0 as LoadInAmt, " & _
            "0 as Disc,0 as SettleAmt,'Transfer' as Type FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No  " & _
            " where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Document_No <> ''  and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='transfer' "

            If strLocAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strTransfer += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If


            '' Sale amount for  Sale 

            strSale = "select " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo, " & _
            "Invoice_Qty/Conversion_Factor as loadoutqty,0 AS loadInqty,0 as Amount, " & _
            "''  as SettDesc,0 as CashCollection,0  as ChequeCollection,0 as NetShortExcess, " & _
            "0 AS RecAmt,Total_Item_Amt as LoadoutAmt,0 as LoadInAmt, " & _
            "case when Scheme_Item='Y' then Invoice_Qty/Conversion_Factor else 0 end as Disc, " & _
            "0 as SettleAmt,'Sale' as Type from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            " where Shipment_Type='Sale' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"

            If strLocAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strSale += " Union All "

            '' Settlement amount for  Sale 

            strSale += "select Doc_No as DocNo,0 as loadoutqty,0 AS loadInqty, " & _
            "0 as Amount,''  as SettDesc,0 as CashCollection,0  as ChequeCollection, " & _
            "0 as NetShortExcess, 0 AS RecAmt,0 as LoadoutAmt,0 as LoadInAmt, " & _
            "0 as Disc,Adjustment_Amount as SettleAmt,'Sale' as Type from " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No= " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  " & _
            " where Shipment_Type='Sale' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"

            If strLocAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strSale += " Union All "

            '' Receipt amount for  Sale 

            strSale += "SELECT  case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then " & _
            "Transfer_No else Sale_Invoice_No end as DocNo,0 as loadoutqty,0 AS loadInqty,0 as Amount, " & _
            "''  as SettDesc,0 as CashCollection,0  as ChequeCollection,0 as NetShortExcess, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount AS RecAmt,0 as LoadoutAmt,0 as LoadInAmt, " & _
            "0 as Disc,0 as SettleAmt,'Sale' as Type FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No  " & _
            " where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Document_No <> ''  and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' "

            If strLocAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSale += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If


            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strsum As String
            strItemCodestring = ""
            strItemCode = ""
            strMainItemCode = ""
            strmainItemCodeString = ""
            strsum = ""
            'Dim str1 As String

            dt = clsDBFuncationality.GetDataTable("select distinct tspl_SettleMent_Master.Description from   tspl_QuickSettleMent_Detail LEFT OUTER JOIN " & _
                 "tspl_QuickSettleMent ON tspl_QuickSettleMent_Detail.Quick_SettleMent_Id = tspl_QuickSettleMent.Quick_SettleMent_Id LEFT OUTER JOIN " & _
                 "tspl_SettleMent_Master ON tspl_QuickSettleMent_Detail.SettleMent_Code = tspl_SettleMent_Master.SettleMentCode " & _
                 "WHERE (tspl_QuickSettleMent_Detail.Amount <> 0) and  TSPLERP.dbo.tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
                " TSPLERP.dbo.tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  and SettleMent_Type not in ('CSE','CSH','CHQ')  ")

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    'While dr.Read
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","

                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    strsum = strsum & " isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)" & "+"
                Next
            End If
            'End While
            If strItemCode <> "" Then

                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            If rdbBoth.IsChecked Then
                strQuery = strTransfer & " Union All " & strSale
            ElseIf rdbRouteWise.IsChecked Then
                strQuery = strTransfer
            ElseIf rdnNonRoutewise.IsChecked Then
                strQuery = strSale
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)

            strFinalQuery = "select DocNo,sum(loadoutqty) as loadoutqty,sum(loadInqty) as loadInqty, " & _
            "sum(Amount) as Amount,(SettDesc),sum(CashCollection) as CashCollection, " & _
            "sum(ChequeCollection) as ChequeCollection,sum(NetShortExcess) as NetShortExcess, " & _
            "sum(RecAmt) as RecAmt,sum(LoadoutAmt) as LoadoutAmt,sum(LoadInAmt) as LoadInAmt, " & _
            "sum(Disc) as Disc,sum(SettleAmt) as SettleAmt,Type from ( " & strQuery & " )abc group by DocNo,Type,SettDesc "

            strQuery = strFinalQuery

            If rdbBoth.IsChecked Then
                strFinalQuery = "select DocNo,loadoutqty,loadInqty,NetSaleQty,NetSaleAmt, " & _
                "" & strItemCodestring & ",CashCollection,ChequeCollection,NetShortExcess,RecAmt, " & _
                "case when type='Sale' then (LoadoutAmt-RecAmt) else (RecAmt - CashCollection - ChequeCollection) end as GAP,TotalExpense, " & _
                "((case when type='Sale' then (LoadoutAmt-RecAmt) else (RecAmt - CashCollection - ChequeCollection) end) - TotalExpense) as NetGap from ( " & _
                "select DocNo,convert(decimal(18,2),sum(loadoutqty)) as loadoutqty, " & _
                "convert(decimal(18,2),sum(loadInqty)) as loadInqty, " & _
                "convert(decimal(18,2),sum(Loadoutqty-loadInqty - Disc)) as NetSaleQty, " & _
                "convert(decimal(18,2),SUM(LoadoutAmt)-sum(LoadInAmt)) as NetSaleAmt," & strmainItemCodeString & ", " & _
                "sum(CashCollection) as CashCollection,sum(ChequeCollection) as ChequeCollection, " & _
                "SUM(NetShortExcess) as NetShortExcess,SUM(RecAmt) as RecAmt, " & _
                "(sum(NetShortExcess) + sum(SettleAmt) + " & strsum & ") as TotalExpense,Type,SUM(LoadoutAmt) as  LoadoutAmt   from " & _
                "(" & strQuery & ") a pivot ( sum(amount) for SettDesc in (" & strItemCodestring & ")) as  Pvt1 group by DocNo,Type ) b"

            ElseIf rdbRouteWise.IsChecked Then
                strFinalQuery = "select DocNo,loadoutqty,loadInqty,NetSaleQty,NetSaleAmt, " & _
                 "" & strItemCodestring & ",CashCollection,ChequeCollection,NetShortExcess,RecAmt, " & _
                 "case when type='Sale' then (LoadoutAmt-RecAmt) else (RecAmt - CashCollection - ChequeCollection) end as GAP,TotalExpense, " & _
                 "((case when type='Sale' then (LoadoutAmt-RecAmt) else (RecAmt - CashCollection - ChequeCollection) end) - TotalExpense) as NetGap from ( " & _
                 "select DocNo,convert(decimal(18,2),sum(loadoutqty)) as loadoutqty, " & _
                 "convert(decimal(18,2),sum(loadInqty)) as loadInqty, " & _
                 "convert(decimal(18,2),sum(Loadoutqty-loadInqty - Disc)) as NetSaleQty, " & _
                 "convert(decimal(18,2),SUM(LoadoutAmt)-sum(LoadInAmt)) as NetSaleAmt," & strmainItemCodeString & ", " & _
                 "sum(CashCollection) as CashCollection,sum(ChequeCollection) as ChequeCollection, " & _
                 "SUM(NetShortExcess) as NetShortExcess,SUM(RecAmt) as RecAmt, " & _
                 "(sum(NetShortExcess) + sum(SettleAmt) + " & strsum & ") as TotalExpense,Type,SUM(LoadoutAmt) as  LoadoutAmt   from " & _
                 "(" & strQuery & ") a pivot ( sum(amount) for SettDesc in (" & strItemCodestring & ")) as  Pvt1 group by DocNo,Type ) b"
            ElseIf rdnNonRoutewise.IsChecked Then
                strFinalQuery = "select DocNo,loadoutqty,loadInqty,NetSaleQty,NetSaleAmt, " & _
               " CashCollection,ChequeCollection,NetShortExcess,RecAmt, " & _
               " case when type='Sale' then (LoadoutAmt-RecAmt) else (RecAmt - CashCollection - ChequeCollection) end as GAP,TotalExpense, " & _
               "((case when type='Sale' then (LoadoutAmt-RecAmt) else (RecAmt - CashCollection - ChequeCollection) end) - TotalExpense) as NetGap from ( " & _
               "select DocNo,convert(decimal(18,2),sum(loadoutqty)) as loadoutqty, " & _
               "convert(decimal(18,2),sum(loadInqty)) as loadInqty, " & _
               "convert(decimal(18,2),sum(Loadoutqty-loadInqty - Disc)) as NetSaleQty, " & _
               "convert(decimal(18,2),SUM(LoadoutAmt)-sum(LoadInAmt)) as NetSaleAmt, " & _
               "sum(CashCollection) as CashCollection,sum(ChequeCollection) as ChequeCollection, " & _
               "SUM(NetShortExcess) as NetShortExcess,SUM(RecAmt) as RecAmt, " & _
               "(sum(NetShortExcess) + sum(SettleAmt) ) as TotalExpense,Type,SUM(LoadoutAmt) as  LoadoutAmt  from " & _
               "(" & strQuery & ") a  group by DocNo,Type ) b"
            End If


            dt = clsDBFuncationality.GetDataTable(strFinalQuery)
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
                SetGridFormationOFgvReport()
                RadPageView1.SelectedPage = RadPageViewPage2

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    'Sub Export()
    '    If gv1.Rows.Count > 0 Then
    '        ExportToExcel()
    '    Else
    '        common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '    End If
    'End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Export()
    End Sub

    Private Sub brnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Export()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub
End Class
