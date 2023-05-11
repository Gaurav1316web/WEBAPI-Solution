Imports common
Imports XpertERPEngine

Public Class RptGatePassVSActual
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.GatePass_Vs_actual)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub RptGatePassVSActual_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkLocationAll.IsChecked = True
        chkcustomerAll.IsChecked = True
        rdbtnGateAll.IsChecked = True
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")
        LoadCustomer()
        LoadLocation()
        GatepassNo()
    End Sub

    Private Sub RptGatePassVSActual_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            chkLocationAll.IsChecked = True
            chkcustomerAll.IsChecked = True
            rdbtnGateAll.IsChecked = True
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        funPrint()
    End Sub
    Sub funPrint()
        Dim query As String
        Dim Fdate As String = clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy")
        Dim Tdate As String = clsCommon.myCDate(ToDate.Value, "dd/MM/yyyy")
        query = " select fdate,Tdate,Customer ,GPCode ,GPDate, saleqty,returnQty ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,comp_name,(isnull(TSPL_COMPANY_MASTER.Add1,'')+ (case when LEN(ISNULL(TSPL_COMPANY_MASTER.Add2,''))>0 then ','else '' end ) +isnull(TSPL_COMPANY_MASTER.Add2,'')+(case when LEN(ISNULL(TSPL_COMPANY_MASTER.Add3,''))>0 then ','else '' end ) ) as Address,Vehicle_Number   from ( select  '" + Fdate + "' as fdate,'" + Tdate + "' as Tdate,"
        If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
            Dim customername As String = ""

            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then

                If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                    For ii As Integer = 0 To cbgCustomer.CheckedDisplayMember.Count - 1
                        If clsCommon.myLen(customername) > 0 Then
                            customername += ", "
                        End If
                        customername += clsCommon.myCstr(cbgCustomer.CheckedDisplayMember(ii))
                    Next
                End If
            End If
            query += " '" + customername + "' "
        Else
            query += " 'All' "
        End If
        query += " as Customer, max(GPCode) as GPCode,max(GPDate) as GPDate,max(saleqty) as saleqty,max(returnQty) as returnQty ,MAX(Vehicle_Number) as Vehicle_Number from (select max(GPCode) as GPCode,max(GPDate) as GPDate"
        query += " ,case when type='sale' then sum(Invoice_Qty) else 0 end as SaleQty, case when type='return' then sum(Invoice_Qty) else 0 end as returnQty ,MAX(Location) as Location ,MAX(Cust_Code ) as Cust_code,MAX (Vehicle_Number ) as Vehicle_Number "
        query += " from  ("
        query += " select TSPL_GATEPASS_MASTER.GPCode,TSPL_GATEPASS_MASTER.GPDate ,TSPL_SALE_INVOICE_HEAD.cust_code,TSPL_SALE_INVOICE_HEAD.Location,"
        query += " ISNULL(case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC'  then  TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  when  TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then  TSPL_SALE_INVOICE_DETAIL.Invoice_Qty /(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as Invoice_Qty, 'sale' as type,TSPL_GATEPASS_MASTER.Vehicle_Number from TSPL_GATEPASS_MASTER"
        query += " left outer join  TSPL_GATEPASS_DETAIL on TSPL_GATEPASS_DETAIL.GPCode =TSPL_GATEPASS_MASTER.GPCode "
        query += "  left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =TSPL_GATEPASS_DETAIL.DocNo "
        query += " left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
        query += " where TSPL_GATEPASS_DETAIL.Type ='sale' "
        query += "  union all"
        query += " select TSPL_GATEPASS_MASTER.GPCode,TSPL_GATEPASS_MASTER.GPDate ,TSPL_SALE_RETURN_HEAD.cust_code "
        query += " , TSPL_SALE_RETURN_HEAD.Location,"
        query += " ISNULL(case when TSPL_SALE_RETURN_DETAIL.Unit_code='FC'  then  TSPL_SALE_RETURN_DETAIL.Return_Qty    when  TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then  TSPL_SALE_RETURN_DETAIL.Return_Qty /(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) end, 0) as Invoice_Qty"
        query += " ,'return' as type,TSPL_GATEPASS_MASTER.Vehicle_Number   from TSPL_GATEPASS_MASTER"
        query += " left outer join  TSPL_GATEPASS_DETAIL on TSPL_GATEPASS_DETAIL.GPCode =TSPL_GATEPASS_MASTER.GPCode "
        query += " left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Invoice_No =TSPL_GATEPASS_DETAIL .DocNo  "
        query += "  left outer join TSPL_SALE_RETURN_DETAIL  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No =TSPL_SALE_RETURN_HEAD.Sale_Return_No "
        query += " where TSPL_GATEPASS_DETAIL.Type ='sale'"
        query += " ) GATEPASS  group by type, GPCode) gate "
        query += " WHERE convert(date,GPDate,103) >= convert(date,'" + Fdate + "',103) and convert(date,GPDate,103)<= convert(date,'" + Tdate + "',103) "
        If chkLocationSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Exit Sub
        ElseIf chkLocationSelect.IsChecked And cbgLoaction.CheckedValue.Count > 0 Then
            query += " and location in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgLoaction.CheckedValue)) + ")"
        End If
        If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
            Exit Sub
        ElseIf chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
            query += " and cust_code in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ")"
        End If
        If rdbtnGateSelect.IsChecked And cbgGatePass.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one GatePass")
            Exit Sub
        ElseIf rdbtnGateSelect.IsChecked And cbgGatePass.CheckedValue.Count > 0 Then
            query += " and GPCode in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgGatePass.CheckedValue)) + ")"
        End If
        query += "  group by GPCode)GP left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "' "
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptGatePassVsActual", "GatePass V/S Actual")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        chkLocationAll.IsChecked = True
        chkcustomerAll.IsChecked = True
        rdbtnGateAll.IsChecked = True
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as [Location],Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        cbgLoaction.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoaction.ValueMember = "Location"
        cbgLoaction.DisplayMember = "Location Description"
    End Sub
    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub GatepassNo()
        Dim qry As String = "Select GPCode as [GatePass Code],GPDate as [GatePass Date] from TSPL_GATEPASS_MASTER "
        cbgGatePass.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgGatePass.ValueMember = "GatePass Code"
        cbgGatePass.DisplayMember = "GatePass Date"
    End Sub

    Private Sub chkcustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkcustomerAll.IsChecked
    End Sub

    Private Sub rdbtnGateAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnGateAll.ToggleStateChanged
        cbgGatePass.Enabled = Not rdbtnGateAll.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLoaction.Enabled = Not chkLocationAll.IsChecked
    End Sub
End Class
