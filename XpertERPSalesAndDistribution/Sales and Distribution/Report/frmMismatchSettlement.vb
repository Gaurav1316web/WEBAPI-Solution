'                                            Modified by = Priit (28/11/2012   02:50 PM)
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'By vipin for pdf work on 08/02/2013
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

Public Class FrmMismatchSettlement
    Inherits FrmMainTranScreen
    Dim ArrDBName As ArrayList = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmMismatchSettlement)
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
    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub FrmMismatchSettlement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnCLos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLos.Click
        Me.Close()
    End Sub

    Sub Print()
        Try
            Dim strLocAll, strRouteAll, strRouteTypeAll As String
            strLocAll = ""
            strRouteAll = ""
            strRouteTypeAll = ""
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

            Dim strSql As String = "SELECT    tspl_QuickSettleMent.Quick_SettleMent_Id as SettlementNo," & _
            "Quick_Settlement_Date as SettleDate, " & _
            "RouteDescription as RouteDesc, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location_desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode as Salesman_Code,  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Salesman AS SalesName,  " & _
            "0 AS SettleGross, " & _
            "CASE WHEN SettleMent_Type = 'DSC' THEN isnull(Amount,0) ELSE 0 END AS SettleDiscount, " & _
            "CASE WHEN SettleMent_Type = 'CRS' THEN isnull(Amount,0) ELSE 0 END AS SettleCreditSale,  " & _
            "CASE WHEN SettleMent_Type = 'CSH' THEN isnull(Amount,0) ELSE 0 END AS SettleCashAmt,  " & _
            "CASE WHEN SettleMent_Type = 'CHQ' THEN isnull(Amount,0) ELSE 0 END AS SettleCheckAmt, " & _
            "0 as MemoNetsale, " & _
            "0 as MemoDiscount, " & _
            "0 as CreditSale, " & _
            "0 as MemoCashAmt, " & _
            "0 as MemoCheckAmt, " & _
            "0 as MemoShellAmt " & _
            "FROM " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail ON " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  "

            If strLocAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strSql += " Union All "

            '''' loadout detail

            strSql += "SELECT " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id AS SettlementNo," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS SettleDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS RouteDesc, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Salesman AS SalesName, " & _
            "case when uom <> 'SH' then  (BasicPrice_WithTax + TPT_Value) * Item_Qty else 0 end AS SettleGross, " & _
            "0 AS SettleDiscount, 0 AS SettleCreditSale, " & _
            "0 AS SettleCashAmt, 0 AS SettleCheckAmt, 0 AS MemoNetsale, " & _
            "0 AS MemoDiscount, 0 AS CreditSale, 0 AS MemoCashAmt, 0 AS MemoCheckAmt,  " & _
            " 0 AS MemoShellAmt " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  "

            If strLocAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strSql += " Union All "

            '''' loadIn detail
            '''' 
            strSql += "SELECT " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id AS SettlementNo," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS SettleDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS RouteDesc, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Salesman AS SalesName, " & _
            "case when uom <> 'SH' then  -(BasicPrice_WithTax + TPT_Value ) * (LoadIn_Qty + Leak+Breakage+burst+Shortage) else 0 end  AS SettleGross, " & _
            "0 AS SettleDiscount, 0 AS SettleCreditSale, " & _
            "0 AS SettleCashAmt, 0 AS SettleCheckAmt, 0 AS MemoNetsale, " & _
            "0 AS MemoDiscount, 0 AS CreditSale, 0 AS MemoCashAmt, 0 AS MemoCheckAmt,  " & _
            " 0 AS MemoShellAmt " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  "

            If strLocAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strSql += " Union All "

            strSql += "SELECT  '' as SettlementNo,'' as SettleDate," & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc as RouteDesc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No  as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS SettleGross, " & _
            "0 AS SettleDiscount, " & _
            "0 AS SettleCreditSale, " & _
            "0 AS SettleCashAmt, " & _
            "0 AS SettleCheckAmt, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt  AS MemoNetSale, " & _
            "CASE WHEN (Scheme_Item = 'Y' ) THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0)  - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4  + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8  + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9)) WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN (Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END AS MemoDiscount, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt  ELSE 0 END AS CreditSale, " & _
            "0 AS MemoCashAmt, " & _
            "0  AS MemoCheckAmt, " & _
            "(TSPL_SALE_INVOICE_HEAD.Shell_Qty *100) as MemoShellamt  " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code And " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER  on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "where  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer'"


            If strLocAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            strSql += "Union All "

            strSql += "SELECT   '' as SettlementNo,'' as SettleDate," & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc as RouteDesc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No  as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS SettleGross, " & _
            "0 AS SettleDiscount, " & _
            "0 AS SettleCreditSale, " & _
            "0 AS SettleCashAmt, " & _
            "0 AS SettleCheckAmt, " & _
            "0 AS MemoNetSale, " & _
            "0 AS MemoDiscount, " & _
            "0 AS CreditSale, " & _
            "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when Credit_Invoice='Y' then 0 else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0)  end  else 0 end AS MemoCashAmt, " & _
            "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when Credit_Invoice='Y' then 0 else  isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0)  end  else 0 end  AS MemoCheckAmt, " & _
            "(TSPL_SALE_INVOICE_HEAD.Shell_Qty *100) as MemoShellamt  FROM " & _
            "" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No  " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER  on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No=" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "where IsChkReverse='N' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer'"

            If strLocAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strRouteTypeAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            Dim strQuery As String = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)

            Dim strFinalQuery As String = "select SettlementNo,SettleDate,DocNo ,DocDate,SettleGross," & _
            "SettleDiscount,(SettleGross - SettleDiscount) as SettleNet, " & _
            "settleCreditSale, SettleCashAmt, SettleCheckAmt, " & _
            "MemoNetsale + MemoDiscount as MemoGross,MemoDiscount,MemoNetsale, " & _
            "CreditSale, MemoCashAmt, MemoCheckAmt, " & _
            "(SettleGross - (MemoNetsale + MemoDiscount)) as GapGross, " & _
            "SettleDiscount-MemoDiscount as GapDisc, " & _
            "((SettleGross - SettleDiscount) - MemoNetsale) as GapNetsale, " & _
            "settleCreditSale - CreditSale as GapCreditSale,  " & _
            "SettleCashAmt - MemoCashAmt as GapCashAmt, " & _
            "SettleCheckAmt - MemoCheckAmt as GapCheckAMt from( " & _
            "select MAX(SettlementNo) as SettlementNo, " & _
            "max(SettleDate) as SettleDate,DocNo,DocDate, " & _
            "sum(SettleGross) as SettleGross, " & _
            "sum(SettleDiscount) as SettleDiscount, " & _
            "sum(settleCreditSale) as settleCreditSale, " & _
            "SUM(SettleCashAmt) as SettleCashAmt, " & _
            "SUM(SettleCheckAmt) as SettleCheckAmt, " & _
            "SUM(MemoNetsale) + MAX(MemoShellAmt) as MemoNetsale, " & _
            "SUM(MemoDiscount) as MemoDiscount, " & _
            "SUM(CreditSale) as CreditSale, " & _
            "SUM(MemoCashAmt) as MemoCashAmt, " & _
            "SUM(MemoCheckAmt) as MemoCheckAmt from(" & strQuery & "  ) a  group by DocNo,DocDate ) b"

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


    Sub SetGridFormationOFgvReport()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
       

        gv1.Columns("SettlementNo").IsVisible = True
        gv1.Columns("SettlementNo").Width = 100
        gv1.Columns("SettlementNo").HeaderText = "SettlementNo No"

        gv1.Columns("SettleDate").IsVisible = True
        gv1.Columns("SettleDate").Width = 100
        gv1.Columns("SettleDate").FormatString = "{0:d}"
        gv1.Columns("SettleDate").HeaderText = "SettleDate"

        gv1.Columns("DocNo").IsVisible = True
        gv1.Columns("DocNo").Width = 100
        gv1.Columns("DocNo").HeaderText = "DocNo"

        gv1.Columns("DocDate").IsVisible = True
        gv1.Columns("DocDate").Width = 100
        gv1.Columns("DocDate").FormatString = "{0:d}"
        gv1.Columns("DocDate").HeaderText = "DocDate"

        gv1.Columns("SettleGross").IsVisible = True
        gv1.Columns("SettleGross").Width = 100
        gv1.Columns("SettleGross").HeaderText = "Settlement Gross"


        gv1.Columns("SettleDiscount").IsVisible = True
        gv1.Columns("SettleDiscount").Width = 100
        gv1.Columns("SettleDiscount").HeaderText = "Settlement Discount"


        gv1.Columns("SettleNet").IsVisible = True
        gv1.Columns("SettleNet").Width = 100
        gv1.Columns("SettleNet").HeaderText = "Settlement Net"

        gv1.Columns("settleCreditSale").IsVisible = True
        gv1.Columns("settleCreditSale").Width = 100
        gv1.Columns("settleCreditSale").HeaderText = "settlement Credit Sale"

        gv1.Columns("SettleCashAmt").IsVisible = True
        gv1.Columns("SettleCashAmt").Width = 100
        gv1.Columns("SettleCashAmt").HeaderText = "Settlement CashAmt"

        gv1.Columns("SettleCheckAmt").IsVisible = True
        gv1.Columns("SettleCheckAmt").Width = 100
        gv1.Columns("SettleCheckAmt").HeaderText = "Settlement CheckAmt"

        gv1.Columns("MemoGross").IsVisible = True
        gv1.Columns("MemoGross").Width = 100
        gv1.Columns("MemoGross").HeaderText = "Memo Gross"

        gv1.Columns("MemoDiscount").IsVisible = True
        gv1.Columns("MemoDiscount").Width = 100
        gv1.Columns("MemoDiscount").HeaderText = "Memo Discount"

        gv1.Columns("MemoNetsale").IsVisible = True
        gv1.Columns("MemoNetsale").Width = 100
        gv1.Columns("MemoNetsale").HeaderText = "Memo Netsale"

        gv1.Columns("CreditSale").IsVisible = True
        gv1.Columns("CreditSale").Width = 100
        gv1.Columns("CreditSale").HeaderText = "CreditSale"

        gv1.Columns("MemoCashAmt").IsVisible = True
        gv1.Columns("MemoCashAmt").Width = 100
        gv1.Columns("MemoCashAmt").HeaderText = "Memo CashAmt"

        gv1.Columns("MemoCheckAmt").IsVisible = True
        gv1.Columns("MemoCheckAmt").Width = 100
        gv1.Columns("MemoCheckAmt").HeaderText = "Memo CheckAmt"

        gv1.Columns("GapGross").IsVisible = True
        gv1.Columns("GapGross").Width = 100
        gv1.Columns("GapGross").HeaderText = "Gap Gross"

        gv1.Columns("GapDisc").IsVisible = True
        gv1.Columns("GapDisc").Width = 100
        gv1.Columns("GapDisc").HeaderText = "Gap Disc"

        gv1.Columns("GapNetsale").IsVisible = True
        gv1.Columns("GapNetsale").Width = 100
        gv1.Columns("GapNetsale").HeaderText = "Gap Netsale"

        gv1.Columns("GapCreditSale").IsVisible = True
        gv1.Columns("GapCreditSale").Width = 100
        gv1.Columns("GapCreditSale").HeaderText = "GapCreditSale"

        gv1.Columns("GapCashAmt").IsVisible = True
        gv1.Columns("GapCashAmt").Width = 100
        gv1.Columns("GapCashAmt").HeaderText = "GapCashAmt"

        gv1.Columns("GapCheckAMt").IsVisible = True
        gv1.Columns("GapCheckAMt").Width = 100
        gv1.Columns("GapCheckAMt").HeaderText = "GapCheckAMt"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("SettleGross", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("SettleDiscount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SettleNet", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("settleCreditSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SettleCashAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("SettleCheckAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("MemoGross", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("MemoDiscount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("MemoNetsale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("CreditSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("MemoCashAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("MemoCheckAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)

        Dim item13 As New GridViewSummaryItem("GapGross", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("GapDisc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("GapNetsale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("GapCreditSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("GapCashAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("GapCashAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)

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


            'clsCommon.MyExportToExcel("Mismatch Settlement Report", gv1, arrHeader, Me.Text)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Mismatch Settlement Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Mismatch Settlement Report ", gv1, arrHeader, "Mismatch Settlement Report", True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        gv1.EnableFiltering = True
        Print()
    End Sub

    Sub Export()
        'If gv1.Rows.Count > 0 Then
        '    ExportToExcel()
        'Else
        '    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        'End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub brnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles brnExport.Click
        Export()
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

    Private Sub RadButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton5.Click
        Reset()
    End Sub
End Class
