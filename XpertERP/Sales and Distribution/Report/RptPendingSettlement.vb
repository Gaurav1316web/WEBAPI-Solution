'---------By vipin for Fully replace and Foc on 23/11/2012

'-------Modified by priti on 30/11/2012
'-21/12/2012-12:15PM--Updation By--Pankaj Kumar---Update In Qry That If Doc No is In Adjustment then It's Seettlement Has Benn Completed
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------


Imports common
Imports Telerik.WinControls
Imports System.IO

Public Class RptPendingSettlement
    Inherits FrmMainTranScreen

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub RptPendingSettlement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P for Print ")

        LoadRouteType()
        Dim arrRouteType As New ArrayList
        arrRouteType.Add("A")
        arrRouteType.Add("D")
        arrRouteType.Add("P")
        arrRouteType.Add("T")
        cbgRouteType.CheckedValue = arrRouteType


        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPendingSettlement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
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
    Sub LoadRouteType()
        Dim qry As String = "select Route_Type_Id as Code,Route_Type_Desc as Name from TSPL_ROUTE_TYPE"
        cbgRouteType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRouteType.ValueMember = "Code"
        cbgRouteType.DisplayMember = "Name"
    End Sub
    Sub LoadSalesman()
        Dim qry As String = " select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='logical'"
        cbgsalesman.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgsalesman.ValueMember = "Code"
        cbgsalesman.DisplayMember = "Description"
    End Sub
    Sub LoadCompany()
        '''''''''''''''''''''''''Fills The Data OF Filter 'Company''''

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

    Sub LoadOutNo()
        Dim qry As String = "select Transfer_No as 'LoadOut No', Transfer_Date as 'Loadout Date', Reference  from TSPL_TRANSFER_HEAD where len(Route_No)>0 and Transfer_Type ='lo'"
        cbgLoadOut.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoadOut.ValueMember = "LoadOut No"
        cbgLoadOut.DisplayMember = "LoadOut Date"
    End Sub
    Sub reset()
        chkPending.IsChecked = True
        LoadLocation()
        LoadRoute()
        LoadSalesman()
        LoadCompany()
        LoadOutNo()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkLocationAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chksalesmanAll.IsChecked = True
        chkLoadoutAll.IsChecked = True
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next

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
        Try
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If
            If rbtnSelectCompany.IsChecked = True AndAlso gvDB.Rows.Count <= 0 Then
                Throw New Exception("Please select at least one Company")
            End If
            If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Route")
            End If
            If chkLoadoutSelect.IsChecked = True AndAlso cbgLoadOut.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one LoadOut No")
            End If
            If chksalesmanSelect.IsChecked = True AndAlso cbgsalesman.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If


            Dim qry As String = Nothing  '= "Select '" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd-MMM-yyyy") + "' as StartDate, '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd-MMM-yyyy") + "' as EndDate, * from (  "
            ''For Crystal Report select outer query 
            '  qry += " select *,([Loadout Qty]-LoadIn_qty) as [Pending_Qty],CONVERT(DECIMAL(18,2),([GrossSale])-[Sale Return],103) as [Net Gross Sale], CONVERT(DECIMAL(18,2),(([Loadout Qty]-LoadIn_qty)-([GrossSale])+[Sale Return] )) AS [Balance Gross Sale Qty]  "
            '' For Excel Sheet select Outer query
            If chkSummary.Checked = True Then
                qry += " select [LoadOut Date] as [LoadOut Date] ,sum([Loadout Qty]) as [Loadout Qty] ,sum([LoadIn_qty]) as [LoadIn Qty] , " & _
                "(sum([Loadout Qty])-sum(LoadIn_qty)) as [Balance Qty], sum(Loadout_Amt) as [Loadout value],sum(LoadIn_Amt) as [LoadIn value], " & _
                "sum(Loadout_Amt - LoadIn_Amt) as [Balance value], " & _
                "CONVERT(DECIMAL(18,2),sum([GrossSale])-sum([Sale Return]),103) as [Cash Memo Enter],sum(isnull(InvAmt,0)) as [Cash Memo value], " & _
                "sum(isnull(RetAmt,0)) as [Cash Memo Return value], " & _
                "CONVERT(DECIMAL(18,2),((sum([Loadout Qty])-sum(LoadIn_qty))-sum([GrossSale])+sum([Sale Return]) )) AS [Pending Memo], " & _
                "case when MAX(type)='Transfer' then sum((Loadout_Amt - LoadIn_Amt) - (isnull(InvAmt,0) - isnull(RetAmt,0))) else sum((isnull(InvAmt,0) - isnull(RetAmt,0))) end as [Pending Memo Amount], " & _
                "max([Settlement Done]) as [Settlement Done] "
            Else
                qry += " select [Company],[Location] ,[Route] ,[Route Description] , [Reference_Doc_No] as [Reference Doc No]," & _
                "[Salesman] ,[LoadOut No] ,[LoadOut Date] ,[Loadout Qty] ,[LoadIn_qty] as [LoadIn Qty] ,([Loadout Qty]-LoadIn_qty) as [Balance Qty], " & _
                "Loadout_Amt as [Loadout value],LoadIn_Amt as [LoadIn value],(Loadout_Amt - LoadIn_Amt) as [Balance value],CONVERT(DECIMAL(18,2), " & _
                "isnull([GrossSale],0)-isnull([Sale Return],0),103) as [Cash Memo Enter],isnull(InvAmt,0) as [Cash Memo value],isnull(RetAmt,0) as [Cash Memo Return value], " & _
                "CONVERT(DECIMAL(18,2),(isnull([Loadout Qty],0)-isnull(LoadIn_qty,0)-isnull([GrossSale],0)+isnull([Sale Return] ,0))) AS [Pending Memo], " & _
                "case when Loadout_Amt > 0 then (Loadout_Amt - LoadIn_Amt) - (isnull(InvAmt,0) - isnull(RetAmt,0)) else (isnull(InvAmt,0) - isnull(RetAmt,0)) end as [Pending Memo Amount], " & _
                "[LoadIn Created] ,[Lodin posted] ,[Settlement Done] "
            End If
            qry += " from(select  'Transfer' as type,(select sum(Total_Item_Amt) from  TSPL_SHIPMENT_MASTER LEFT outer join TSPL_SALE_INVOICE_HEAD on " & _
            "TSPL_SHIPMENT_MASTER.Shipment_No =TSPL_SALE_INVOICE_HEAD.Shipment_No left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No   where " & _
            "TSPL_SHIPMENT_MASTER.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No) as InvAmt," & _
            "(select sum(Total_Item_Amt) from TSPL_SHIPMENT_MASTER LEFT outer join TSPL_SALE_INVOICE_HEAD on  " & _
            "TSPL_SHIPMENT_MASTER.Shipment_No =TSPL_SALE_INVOICE_HEAD.Shipment_No LEFT outer join " & _
            "TSPL_SALE_RETURN_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No left outer join " & _
            "TSPL_SALE_RETURN_DETAIL on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No  " & _
            "where TSPL_SHIPMENT_MASTER.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No) as RetAmt," & _
            "(select sum((BasicPrice_WithTax + TPT_Value) * Item_Qty) from tspl_transfer_detail " & _
            "where tspl_transfer_detail.Transfer_No =TSPL_TRANSFER_HEAD .Transfer_No ) as [Loadout_Amt], " & _
            "isnull((select sum((BasicPrice_WithTax + TPT_Value) * LoadIn_Qty)   from TSPL_TRANSFER_DETAIL left outer join " & _
            "TSPL_TRANSFER_HEAD th on th.Transfer_No=TSPL_TRANSFER_DETAIL .Transfer_No  where  th.Transfer_Type='LI' and " & _
            "th.Load_Out_No=TSPL_TRANSFER_HEAD.Transfer_No ),0)  AS [LoadIn_Amt], " & _
            " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code as Company, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No as Route, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc as 'Route Description' , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc  "
            qry += " as Salesman, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No  as 'LoadOut No', CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, 103)  as 'LoadOut Date',CASE WHEN (select count(*) from "
            qry += " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD  H WHERE h.Load_Out_No  = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No )=1 THEN 'Yes' else 'Pending' end as  "
            qry += " [LoadIn Created],case when (select top 1 H.Post from  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD  H WHERE h.Load_Out_No  = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD. "
            qry += " Transfer_No )='Y' then 'Completed' else  'Pending'  end as 'Lodin posted',CASE WHEN (select count(*) from  "
            qry += "  " + clsCommon.ReplicateDBString + " tspl_QuickSettleMent where  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No  and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Post='Y') =1 then 'Completed' else 'Pending' end as [Settlement Done]"
            qry += "  ,(select sum(item_qty) from " + clsCommon.ReplicateDBString + "tspl_transfer_detail where " + clsCommon.ReplicateDBString + "tspl_transfer_detail.Transfer_No =" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD .Transfer_No ) as [Loadout Qty],isnull((select sum(TSPL_TRANSFER_DETAIL.Total_QtyInCase)   from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL left outer join TSPL_TRANSFER_HEAD th on th.Transfer_No=TSPL_TRANSFER_DETAIL .Transfer_No  where  th.Transfer_Type='LI' and th.Load_Out_No=TSPL_TRANSFER_HEAD.Transfer_No ),0)  AS [LoadIn_qty],'" & dtpFromDate.Value & "' as Fdate,'" & dtpToDate.Value & "' as Tdate ,TSPL_TRANSFER_HEAD.Reference_Doc_No , "
            qry += "( select sum([Shiped]) from (select  ISNULL(case when " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FC'  then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty  when  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FB' then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code =" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as [Shiped] from TSPL_SHIPMENT_DETAILS  left outer join  TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_DETAILS.Shipment_No =TSPL_SHIPMENT_MASTER.Shipment_No where TSPL_SHIPMENT_MASTER.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No ) xx) as [GrossSale], "
            qry += "( select ISNULL( sum(RetQty),0) from  (select ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty, 0)/(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ) as RetQty from TSPL_SALE_RETURN_DETAIL Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD  on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No Left Outer Join TSPL_SHIPMENT_MASTER  on  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No=" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No  WHERE TSPL_SHIPMENT_MASTER.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No  )xxx) as [Sale Return]"
            qry += " from  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD where  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type ='lo' and LEN(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.route_no)>0 and 2=2"

            qry += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ")"

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


            qry += "and  convert(Date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + "',103) and convert(Date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103) and TSPL_TRANSFER_HEAD.Location_Type='Logical'"

            If chkSale.Checked Then

                qry += " UNION ALL "

                qry += " Select 'Sale' as type,(select sum(Total_Item_Amt) from tspl_sale_invoice_detail where " & _
                "tspl_sale_invoice_detail.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as InvAmt, " & _
                "(select sum(Total_Item_Amt) from TSPL_SALE_RETURN_DETAIL where " & _
                "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No) as RetAmt, " & _
                "0 as [Loadout_Amt],0 as [LoadIn_Amt], " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code as Company, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc as Location , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code as Route, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name as 'Route Description', "
                qry += " " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as Salesman, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as 'LoadOut No', CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) as 'LoadOut Date', CASE WHEN (Select COUNT(*) from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER WHERE ItemType='E' AND "
                qry += " " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' AND Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 Then 'Yes' Else 'Pending' end as   [LoadIn Created], Case When (Select COUNT(*) from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER WHERE ItemType='E' AND "
                qry += " " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' AND Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and Posted='Y' )>=1 THEN 'Completed' else 'Pending' end as 'Lodin posted', "
                '     qry += " Case When (Select Count(*) from " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL WHERE Document_No =" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 then 'Completed' Else Case When (Select Count(*) from " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header WHERE Doc_No =" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 Then 'Completed' else 'Pending' end end as [Settlement Done], " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt AS [Loadout Qty],0  AS [LoadIn_qty],'" & dtpFromDate.Value & "' as Fdate,'" & dtpToDate.Value & "' as Tdate ,'' as Reference_Doc_No  "
                qry += " Case When (Select Count(*) from " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL WHERE Document_No =" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 then 'Completed' Else Case When (Select Count(*) from " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header WHERE Doc_No =" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )>=1 Then 'Completed' else 'Pending' end end as [Settlement Done], 0 AS [Loadout Qty],0  AS [LoadIn_qty],'" & dtpFromDate.Value & "' as Fdate,'" & dtpToDate.Value & "' as Tdate ,'' as Reference_Doc_No  "
                '     qry += " ,( select sum([Shiped]) from (select  ISNULL(case when " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FC'  then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty  when  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Unit_code='FB' then  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Shipped_Qty /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code =" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_DETAILS.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as [Shiped] from TSPL_SHIPMENT_DETAILS   left outer join  TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_DETAILS.Shipment_No =TSPL_SHIPMENT_MASTER.Shipment_No where TSPL_SHIPMENT_MASTER.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No  ) xx) as [GrossSale]"
                qry += " ,0 as [GrossSale]"
                '      qry += " , ( select ISNULL( sum(RetQty),0) from  (select ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty, 0)/(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ) as RetQty from TSPL_SALE_RETURN_DETAIL Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   )xxx) as [Sale Return] "
                qry += " , 0 as [Sale Return] "
                qry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD "
                qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No"
                qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code"
                qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code= " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE "
                qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code "
                qry += "WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' and    ISNULL( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_FullyRevrse,0)=0 and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt>0 "
                qry += " and  convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + "',103) and convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103)"

                'qry += " And " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ")"

                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
                    qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
                End If
                If chksalesmanSelect.IsChecked = True AndAlso cbgsalesman.CheckedValue.Count > 0 Then
                    qry += " And  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code  in (" + clsCommon.GetMulcallString(cbgsalesman.CheckedValue) + ")"
                End If
            End If

            qry += " )abc"

            If chkPending.IsChecked Then
                qry += " WHERE [Settlement Done]='Pending' "
            End If

            If chkSummary.Checked = True Then
                qry += "  group by [LoadOut Date],type"
            End If
            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)




            Dim dt As DataTable
            dt = New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(Fromdate, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(Todate, "dd/MM/yyyy"))

            ' SalesReportViewer.funreport(dt, "crptpendingsettlement", "Report For Pending Settlement")
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            If chkSummary.Checked = True Then
                clsCommon.MyExportToExcel("Pending Settlement Summary", gv1, Nothing, "Pending Settlement Summary")
            Else
                clsCommon.MyExportToExcel("Pending Settlement Report", gv1, arrHeader, "Pending Settlement")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
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



    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = False
    End Sub

    Private Sub rbtnSelectCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelectCompany.ToggleStateChanged
        gvDB.Enabled = True
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

   
    
End Class
