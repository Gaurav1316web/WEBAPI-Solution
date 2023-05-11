'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
Imports XpertERPEngine
Imports common
Imports System.Data.SqlClient

Public Class FrmSaleVolumeTracker
    Inherits FrmMainTranScreen

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub FrmSaleVolumeTracker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P for Print ")
        SetUserMgmtNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSaleVolumeTracker)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag

    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical' and Excisable ='f'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Sub LoadRoute()
        Dim qry As String = "select route_no as Route,Route_Desc as 'Route Description' from TSPL_ROUTE_MASTER "
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Sub LoadSalesman()
        Dim qry As String = " select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='logical'"
        cbgsalesman.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgsalesman.ValueMember = "Code"
        cbgsalesman.DisplayMember = "Description"
    End Sub

    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    Sub LoadOutNo()
        Dim qry As String = "select Transfer_No as 'LoadOut No', Transfer_Date as 'Loadout Date', Reference  from TSPL_TRANSFER_HEAD where len(Route_No)>0 and Transfer_Type ='lo'"
        cbgLoadOut.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoadOut.ValueMember = "LoadOut No"
        cbgLoadOut.DisplayMember = "LoadOut Date"
    End Sub

    Sub LoadRootType()
        Dim qry As String = "SELECT Route_Type_Id AS [Route Type], Route_Type_Desc AS Description FROM TSPL_ROUTE_TYPE"
        cbgRootType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRootType.ValueMember = "Route Type"
        cbgRootType.DisplayMember = "Description"
    End Sub
    Sub reset()
        LoadLocation()
        LoadRoute()
        LoadSalesman()
        LoadCompany()
        LoadOutNo()
        LoadRootType()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


        chkLocationAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chksalesmanAll.IsChecked = True
        chkLoadoutAll.IsChecked = True
        chkRootTypeAll.IsChecked = True
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


    Private Sub chksalesmanAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chksalesmanAll.ToggleStateChanged
        cbgsalesman.Enabled = False
    End Sub

    Private Sub chksalesmanSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chksalesmanSelect.ToggleStateChanged
        cbgsalesman.Enabled = True
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkRouteSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub


    Private Sub chkLoadoutAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLoadoutAll.ToggleStateChanged
        cbgLoadOut.Enabled = False
    End Sub

    Private Sub chlLoadoutSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLoadoutSelect.ToggleStateChanged
        cbgLoadOut.Enabled = True
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funprint()
    End Sub
    Sub funprint()
        Dim Fromdate As String = clsCommon.myCDate(dtpFromDate.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
        Dim location As String
        Dim company As String
        Dim route As String
        Dim Loadout As String
        Dim salesman As String
        Dim RootType As String
        Dim Strlocation As String = ""
        Dim Strcompany As String = ""
        Dim Strroute As String = ""
        Dim StrLoadout As String = ""
        Dim Strsalesman As String = ""
        Dim StrRootType As String = ""

        If chkLoadoutSelect.IsChecked = True AndAlso cbgLoadOut.CheckedValue.Count > 0 Then
            Loadout = "'" + clsCommon.GetMulcallString(cbgLoadOut.CheckedValue) + "'"
            StrLoadout = Loadout.Replace("'", "")
        End If
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")
        End If
        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
            route = "'" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + "'"
            Strroute = route.Replace("'", "")
        End If
        If chksalesmanSelect.IsChecked = True AndAlso cbgsalesman.CheckedValue.Count > 0 Then
            salesman = "'" + clsCommon.GetMulcallString(cbgsalesman.CheckedValue) + "'"
            Strsalesman = salesman.Replace("'", "")
        End If
        If chkRootTypeSelect.IsChecked = True AndAlso cbgRootType.CheckedValue.Count > 0 Then
            RootType = "'" + clsCommon.GetMulcallString(cbgRootType.CheckedValue) + "'"
            StrRootType = RootType.Replace("'", "")
        End If
        If rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count > 0 Then
            company = "'" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "'"
            Strcompany = company.Replace("'", "")
        End If

        Try
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If
            If rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Company")
            End If
            If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Route")
            End If
            If chkLoadoutSelect.IsChecked = True AndAlso cbgLoadOut.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one LoadOut No")
            End If
            If chksalesmanSelect.IsChecked = True AndAlso cbgsalesman.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location Or Select All")
            End If
            If chkRootTypeSelect.IsChecked = True AndAlso cbgRootType.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Route Or Select All")
            End If
            Dim qry As String = Nothing  '= "Select '" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd-MMM-yyyy") + "' as StartDate, '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd-MMM-yyyy") + "' as EndDate, * from (  "
            qry += " select '" + Fromdate + "' as FromDate,'" + Todate + "' as ToDate,'" + Strlocation + "' as Strlocation,'" + Strroute + "' as Strroute,'" + Strcompany + "' as Strcompany,'" + StrLoadout + "' as StrLoadout,'" + Strsalesman + "' as Strsalesman,'" + StrRootType + "' as StrRootType, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code as Company, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No as Route, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc as 'Route Description' , " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc  "
            qry += " as Salesman, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No  as 'LoadOut No', CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, 103)  as 'LoadOut Date',CASE WHEN (select count(*) from "
            qry += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD  H WHERE h.Load_Out_No  = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No )=1 THEN 'Yes' else 'Pending' end as  "
            qry += " [LoadIn Created],case when (select top 1 H.Post from  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD  H WHERE h.Load_Out_No  = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD. "
            qry += " Transfer_No )='Y' then 'Completed' else  'Pending'  end as 'Lodin posted',CASE WHEN (select count(*) from  "
            qry += "  " + clsCommon.ReplicateDBString + " tspl_QuickSettleMent where  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No  and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Post='Y') =1 then 'Completed' else 'Pending' end as [Settlement Done]"
            qry += "  ,(select sum(item_qty) from " + clsCommon.ReplicateDBString + "tspl_transfer_detail where " + clsCommon.ReplicateDBString + "tspl_transfer_detail.Transfer_No =" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD .Transfer_No AND " + clsCommon.ReplicateDBString + "tspl_transfer_detail.Uom<>'SH') as [Loadout Qty], "
            qry += " (SELECT ISNULL(SUM((LoadIn_Qty+Burst+Leak+Shortage)/Conversion_Factor), 0) AS qty   from " + clsCommon.ReplicateDBString + "tspl_transfer_detail LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "tspl_transfer_detail.Item_Code AND " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "tspl_transfer_detail.Uom AND " + clsCommon.ReplicateDBString + "tspl_transfer_detail.Uom<>'SH' LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H ON " + clsCommon.ReplicateDBString + "tspl_transfer_detail.Transfer_No=H.Transfer_No   WHERE H.Transfer_Type='LI' AND  H.Load_Out_No= " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No) AS [LoadIn Qty] "
            qry += " from  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD "
            qry += " where  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type ='lo' and LEN(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.route_no)>0 and 2=2"
            If chkLoadoutSelect.IsChecked = True AndAlso cbgLoadOut.CheckedValue.Count > 0 Then
                qry += " And " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No  in (" + clsCommon.GetMulcallString(cbgLoadOut.CheckedValue) + ")"
            End If
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If
            If chksalesmanSelect.IsChecked = True AndAlso cbgsalesman.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location  in (" + clsCommon.GetMulcallString(cbgsalesman.CheckedValue) + ")"
            End If
            If chkRootTypeSelect.IsChecked = True AndAlso cbgRootType.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id  in (" + clsCommon.GetMulcallString(cbgRootType.CheckedValue) + ")"
            End If
            qry += "and  convert(Date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + "',103) and convert(Date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103)"

            qry += " UNION ALL "

            qry += " Select  DISTINCT '" + Fromdate + "' as FromDate,'" + Todate + "' as ToDate,'" + Strlocation + "' as Strlocation,'" + Strroute + "' as Strroute,'" + Strcompany + "' as Strcompany,'" + StrLoadout + "' as StrLoadout,'" + Strsalesman + "' as Strsalesman,'" + StrRootType + "' as StrRootType,  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code as Company, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc as Location , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code as Route, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name as 'Route Description', "
            qry += " " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as Salesman, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as 'LoadOut No', CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) as 'LoadOut Date', CASE WHEN (Select COUNT(*) from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER WHERE ItemType='E' AND "
            qry += " " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' AND Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 Then 'Yes' Else 'Pending' end as   [LoadIn Created], Case When " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post ='Y' THEN 'Completed' else 'Pending' end as 'Lodin posted', "
            qry += " Case When (Select Count(*) from " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL WHERE Document_No =" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 then 'Completed' else 'Pending' end as [Settlement Done], "
            qry += " (SELECT SUM(Invoice_Qty/Conversion_Factor) AS qty   from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code AND " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code  WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS [LoadOut Qty], "
            qry += " 0 as [LoadIn Qty]"
            qry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD "
            qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code"
            qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code= " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE "
            qry += " WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' "
            qry += " and  convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + "',103) and convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103)"

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If
            If chksalesmanSelect.IsChecked = True AndAlso cbgsalesman.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code  in (" + clsCommon.GetMulcallString(cbgsalesman.CheckedValue) + ")"
            End If
            If chkRootTypeSelect.IsChecked = True AndAlso cbgRootType.CheckedValue.Count > 0 Then
                qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id  in (" + clsCommon.GetMulcallString(cbgRootType.CheckedValue) + ")"
            End If

            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, ArrDBName, False)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptSaleVolumeTracker", "Report For Pending Settlement")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RptPendingSettlement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funprint()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        End If
    End Sub

    
    Private Sub ckRootTypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRootTypeAll.ToggleStateChanged
        cbgRootType.Enabled = False
    End Sub

    Private Sub chkRootTypeSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRootTypeSelect.ToggleStateChanged
        cbgRootType.Enabled = True
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = False
    End Sub

    Private Sub rbtnCompanySelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = True
    End Sub
End Class
