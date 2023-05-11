'-Updation by Pankaj Kumar Chaudhary -- [BM00000000642, BM00000000723, BM00000001292, BM00000001300]
Imports common
Public Class RptInvoiceAgainstInward
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptInvoiceAgainstInward)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub loadCustomer()
        Dim qry As String = "select Cust_Code ,Customer_Name  from TSPL_CUSTOMER_MASTER "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Cust_Code"
    End Sub
    Private Sub RptInvoiceAgainstInward_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        chkInvoiceAll.IsChecked = True
        rdbtnAll.IsChecked = True
        chkAllRoute.IsChecked = True
        LoadType()
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")
    End Sub

    Private Sub LoadType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Both")
        dt.Rows.Add("Against Invoice")
        dt.Rows.Add("Direct")
        ddlType.DataSource = dt
        ddlType.SelectedValue = "Code"
        ddlType.DisplayMember = "Code"
    End Sub

    Private Sub RptInvoiceAgainstInward_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            chkInvoiceAll.IsChecked = True
        End If
    End Sub
    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub
    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        funPrint()
    End Sub
    Sub funPrint()
        Dim query As String = ""
        Dim Fdate As String = clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy")
        Dim Tdate As String = clsCommon.myCDate(ToDate.Value, "dd/MM/yyyy")
        query += "Select * from ( "
        query += " Select max(Type) as Type,max(Route_No) as Route_No, '" + Fdate + "' as FromDate, '" + Tdate + "' as Todate, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "' as Rundate, CONVERT(VARCHAR,max(Sale_Invoice_Date),103) as [Sale_Invoice_Date],Sale_Invoice_No,max(Cust_Name) as [Cust_Name],max(Vehicle_No)as [Vehicle_No],sum(Invoice_Qty) as [Invoice_Qty] "
        query += " ,SUM(InvGlassQty ) as [InvGlassQty],SUM(InvPetQty)as [InvPetQty],MAX(Unit_code) as [Unit_code]"
        query += " ,CONVERT(VARCHAR,MAX(Adjustment_Date),103) as Adjustment_Date, MAX(Adjustment_No )as Adjustment_No, " & _
        "case when max(Type) <> 'Return' then (select ISNULL(sum(Item_Quantity),0) from TSPL_ADJUSTMENT_DETAIL where Unit_Code<>'SH' AND TSPL_ADJUSTMENT_DETAIL.Adjustment_No= xxx.Adjustment_No ) else 0 end as AdjGlassQty "
        query += " ,0 as AdjPetQty,MAX(compname )as compname, MAX(address1)as address1,MAX(Transporter_Name ) as Transporter_Name   from( "

        query += " Select  'Invoice' as Type,TSPL_SALE_INVOICE_HEAD.Route_No,Sale_Invoice_Date ,TSPL_SALE_INVOICE_HEAD .Sale_Invoice_No ,Cust_Name ,TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty "
        query += " ,(case when Empty_Value_Bottle >0 then Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as InvGlassQty , "
        query += " (case when Empty_Value_Bottle <=0 then Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as InvPetQty,TSPL_SALE_INVOICE_DETAIL. Unit_code,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,105) as [Adjustment_Date]  ,TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_SALE_INVOICE_HEAD.Cust_Code ,TSPL_SALE_INVOICE_HEAD.Location  "
        query += " , TSPL_SALE_INVOICE_HEAD.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address1,TSPL_TRANSPORT_MASTER.Transporter_Name "
        query += "  from TSPL_SALE_INVOICE_HEAD  "
        query += " left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No "
        query += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
        query += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SALE_INVOICE_HEAD.Comp_Code "
        query += " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SALE_INVOICE_HEAD.Vehicle_Code  "
        query += " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  "
        query += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code"
        query += " where  TSPL_SALE_INVOICE_HEAD.Is_Post='Y' and convert(date,Sale_Invoice_Date,103) >=convert(date,'" + Fdate + "',103) and convert(date,Sale_Invoice_Date,103)<=convert(date,'" + Tdate + "',103) "
        'query += " AND  TSPL_SALE_INVOICE_HEAD.Empty_Value >0"

        query += " UNION ALL"
        query += " Select 'Adjustment' as Type,'' as Route_No,      NULL as Sale_Invoice_Date ,'' as Sale_Invoice_No , Case When ISNULL(Customer_CODE,'')='' Then TSPL_LOCATION_MASTER.Location_Desc Else Customer_NAME End as Cust_Name, "
        query += " TSPL_ADJUSTMENT_HEADER.Vehicle_No, 0 as Invoice_Qty  , 0 as InvGlassQty , 0 as InvPetQty, TSPL_ADJUSTMENT_DETAIL.Unit_code, convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,105) as [Adjustment_Date],"
        query += " TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_ADJUSTMENT_HEADER.Customer_CODE as [Cust_Code] ,TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, TSPL_ADJUSTMENT_HEADER.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address1, TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_ADJUSTMENT_HEADER   "
        query += " left outer join TSPL_ADJUSTMENT_DETAIL On TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No "
        query += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_ADJUSTMENT_HEADER.Comp_Code"
        query += " Left Outer Join TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code=TSPL_LOCATION_MASTER.Location_Code "
        query += " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_ADJUSTMENT_HEADER.Vehicle_Code "
        query += " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id   "
        query += " where convert(date,Adjustment_Date ,103) >=convert(date,'" + Fdate + "',103) and convert(date,Adjustment_Date,103)<=convert(date,'" + Tdate + "',103) AND TSPL_ADJUSTMENT_HEADER.ItemType='E' AND ISNULL(Document_No,'')='' AND ISNULL(Reference_Document,'')=''"

        query += " UNION ALL"
        query += "  Select  'Return' as Type,TSPL_SALE_RETURN_HEAD.Route_No,Sale_Return_Date ,TSPL_SALE_RETURN_HEAD .Sale_Return_No ,"
        query += " TSPL_SALE_RETURN_HEAD.Cust_Name ,TSPL_SALE_RETURN_HEAD.Vehicle_No,-TSPL_SALE_RETURN_DETAIL.Return_Qty  , "
        query += " -(case when Empty_Value_Bottle >0 then Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as InvGlassQty , "
        query += " -(case when Empty_Value_Bottle <=0 then Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as InvPetQty, "
        query += " TSPL_SALE_RETURN_DETAIL. Unit_code,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,105) as [Adjustment_Date]  ,"
        query += " TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_SALE_RETURN_HEAD.Cust_Code ,TSPL_SALE_RETURN_HEAD.Location   , "
        query += " TSPL_SALE_RETURN_HEAD.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,"
        query += " tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address1, "
        query += " TSPL_TRANSPORT_MASTER.Transporter_Name   from TSPL_SALE_RETURN_HEAD   "
        query += " left outer join TSPL_SALE_RETURN_DETAIL on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No "
        query += "  left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No  "
        query += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  "
        query += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SALE_RETURN_HEAD.Comp_Code  "
        query += " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SALE_RETURN_HEAD.Vehicle_Code  "
        query += "  left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id   "
        query += "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_RETURN_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code"
        query += " where   TSPL_SALE_RETURN_HEAD.Is_Post='Y' and  convert(date,Sale_Return_Date,103) >=convert(date,'" + Fdate + "',103) and "
        query += " convert(date,Sale_Return_Date,103)<=convert(date,'" + Tdate + "',103) "

      
        query += " )xxx WHERE 1=1 "
        If clsCommon.CompairString(ddlType.Text, "Against Invoice") = CompairStringResult.Equal Then
            query += " AND ISNULL(Sale_Invoice_No,'')<>''"
        ElseIf clsCommon.CompairString(ddlType.Text, "Direct") = CompairStringResult.Equal Then
            query += " AND ISNULL(Sale_Invoice_No,'')=''"
        End If

        If chkInvoiceSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Customer ")
            Exit Sub

        ElseIf chkInvoiceSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then

            query += " and Cust_Code  in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ")"
        End If
        If rdbtnSelect.IsChecked And cbgLoc.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location ")
            Exit Sub
        ElseIf rdbtnSelect.IsChecked And cbgLoc.CheckedValue.Count > 0 Then

            query += " and Location in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgLoc.CheckedValue)) + ")"
        End If
        If chkSelectRoute.IsChecked And cbgroute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Route ")
            Exit Sub
        ElseIf chkSelectRoute.IsChecked And cbgroute.CheckedValue.Count > 0 Then

            query += " and Route_No in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgroute.CheckedValue)) + ")"
        End If
        query += " group by Sale_Invoice_No,Adjustment_No "
        query += " ) YYY  "
        query += " ORDER BY CONVERT(DATE,Sale_Invoice_Date,103)"



        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If (chkTransporter.Checked = True) Then
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptInvoiceAgainstInwardTransporter", "Dispatch Details Transporter Wise")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptInvoiceAgainstInward", "Invoice Against Inward Report")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoc.ValueMember = "Location_Code"
        cbgLoc.DisplayMember = "Location_Desc"

    End Sub
    Private Sub chkInvoiceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInvoiceAll.ToggleStateChanged
        cbgCustomer.Enabled = False
    End Sub

    Private Sub chkInvoiceSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInvoiceSelect.ToggleStateChanged
        cbgCustomer.Enabled = True
    End Sub

    Private Sub rdbtnAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnAll.ToggleStateChanged
        cbgLoc.Enabled = False
    End Sub


    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    Sub Reset()
        LoadLocation()
        loadCustomer()
        LoadRoute()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rdbtnSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnSelect.ToggleStateChanged
        cbgLoc.Enabled = True
    End Sub

    Private Sub chkSelectRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelectRoute.ToggleStateChanged
        cbgroute.Enabled = True
    End Sub

    Private Sub chkAllRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgroute.Enabled = False
    End Sub
End Class
