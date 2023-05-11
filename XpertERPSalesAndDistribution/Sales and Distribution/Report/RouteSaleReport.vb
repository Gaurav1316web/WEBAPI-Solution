''----08/06/2012--updation by shipra------on location's filter locations are being displayed from Segment Table
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls
Imports System.IO
Public Class RouteSaleReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnRouteSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub RouteSaleReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funprint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
       
        End If
    End Sub
    Private Sub RouteSaleReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadVendor()
        LoadLocation()
        LoadSales()
        txtStart.Value = clsCommon.GETSERVERDATE()
        txtend.Value = clsCommon.GETSERVERDATE()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")




    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        funprint()
    End Sub
    Sub funprint()
        Dim Fromdate As String = clsCommon.myCDate(txtStart.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(txtend.Value, "dd/MM/yyyy")
        Dim Compqry As String = "select ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE from tspl_Company_master where comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim addres As String = clsDBFuncationality.getSingleValue(Compqry)
        Dim Companyname As String = objCommonVar.CurrentCompanyName

        Try
            Dim qry As String = " select '" & Fromdate & "' as FromDate,'" & Todate & "' as ToDate,'" & addres & "' as Company_address,'" & Companyname & "'as Company_Name,Transfer_No,MAX(Transfer_Date) as Transfer_Date,MAX(Route_No) as Route_No,MAX(Route_Desc) as Route_Desc,MAX(Salesmancode) as Salesmancode,MAX(SalesmanName) as SalesmanName,MAX(From_Location) as From_Location,MAX(FromLoc_Desc) as FromLoc_Desc"
            qry += " ,SUM(Amt * case when RI=1 then 1 else 0 end ) as LoadoutAmt"
            qry += " ,SUM(Amt * case when RI=2 then 1 else 0 end ) as LoadInAmt"
            qry += " ,SUM(Amt * case when RI=1 then 1 else case when RI=2 then -1 else 0 end end ) as ProvSaleAmt"
            qry += " ,SUM(Amt * case when RI=4 then 1 else 0 end ) as SettlementAmt"
            qry += " ,SUM(Amt * case when RI=3 then 1 else 0 end ) as SaleAmt"
            qry += " ,SUM(Amt * case when RI=1 then 1 else case when RI in (2,3) then -1 else 0 end end  ) as SaleToBeDoneAmt"
            qry += " ,SUM(ReceiptAmt ) as ReceiptAmt"
            qry += " ,SUM(Amt * case when RI=5 then 1 else 0 end ) as EmptyReceiptAmt"
            qry += " ,SUM(Amt * case when RI=6 then 1 else 0 end ) as EmptySalesManAmt "
            qry += " ,SUM(Amt * case when RI=3 then 1 else case when RI in ( 5,4) then -1 else 0 end end - ReceiptAmt) as BalanceAmt"
            qry += " from ("
            qry += " select TSPL_TRANSFER_HEAD.Transfer_No,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,Convert(varchar(11),TSPL_TRANSFER_HEAD.Transfer_Date,103) as Transfer_Date, TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Route_Desc,TSPL_TRANSFER_HEAD.Salesmancode ,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName,TSPL_TRANSFER_HEAD.From_Location,TSPL_TRANSFER_HEAD.FromLoc_Desc,TSPL_TRANSFER_HEAD.Total_Transfer_Amount as Amt,1 as RI,1 as Chk, 0 as ReceiptAmt"
            qry += " from TSPL_TRANSFER_HEAD"
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_TRANSFER_HEAD.Salesmancode"
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.To_Location"
            qry += " where TSPL_TRANSFER_HEAD.Transfer_Type='LO' and TSPL_TRANSFER_HEAD.Post='Y'"
            qry += " and TSPL_TRANSFER_HEAD.Transfer_Date>='" + clsCommon.GetPrintDate(txtStart.Value, "dd/MMM/yyyy") + "' and TSPL_TRANSFER_HEAD.Transfer_Date<='" + clsCommon.GetPrintDate(txtend.Value, "dd/MMM/yyyy") + "'  and  TSPL_LOCATION_MASTER.Location_Type='Logical' "
            If rbtnselect.IsChecked Then
                If gvlocation.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one Location")
                End If
                qry += "and TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(gvlocation.CheckedValue) + ")) "
            End If
            If rbtnselectroute.IsChecked Then
                If gvroute.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one Route")
                End If
                qry += "and TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(gvroute.CheckedValue) + ") "
            End If
            If rbtnselectsales.IsChecked Then
                If gvsales.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one Salesman")
                End If
                qry += "and TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(gvsales.CheckedValue) + ") "
            End If
            qry += " union all "
            qry += " select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No, TSPL_TRANSFER_HEAD.Transfer_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,TSPL_TRANSFER_HEAD.Total_Transfer_Amount as Amt,2 as RI,0 as Chk, 0 as ReceiptAmt"
            qry += " from TSPL_TRANSFER_HEAD "
            qry += " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Post='Y'"
            qry += " union all "
            qry += " select TSPL_SHIPMENT_MASTER.Transfer_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt+TSPL_SALE_INVOICE_HEAD.Empty_Value as Amt,3 as RI,0 as Chk,0 as ReceiptAmt"
            qry += " from TSPL_SHIPMENT_MASTER "
            qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No"
            qry += " where TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
            qry += " union all "
            qry += " select TSPL_PAYMENT_HEADER.LoadOutNo as Transfer_No,TSPL_PAYMENT_HEADER.Payment_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,TSPL_PAYMENT_HEADER.Payment_Amount as Amt,4 as RI,0 as Chk,0 as ReceiptAmt"
            qry += "     from TSPL_PAYMENT_HEADER "
            qry += " where TSPL_PAYMENT_HEADER.Posted='P' and len(ISNULL(LoadOutNo,''))>0 and Payment_Type='MI' "
            qry += "  union all "
            qry += " select TSPL_SHIPMENT_MASTER.Transfer_No ,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,TSPL_ADJUSTMENT_DETAIL.Item_Cost as Amt,5 as RI,0 as Chk,0 as ReceiptAmt"
            qry += " from TSPL_ADJUSTMENT_DETAIL "
            qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
            qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_ADJUSTMENT_HEADER.Document_No"
            qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No"
            qry += " where TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' and LEN(ISNULL(TSPL_ADJUSTMENT_HEADER.Document_No,''))>0 and TSPL_ADJUSTMENT_HEADER.Posted='Y'"
            qry += " union all"
            qry += " select TSPL_ADJUSTMENT_HEADER.Document_No as Transfer_No ,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,TSPL_ADJUSTMENT_DETAIL.Item_Cost as Amt,6 as RI,0 as Chk,0 as ReceiptAmt from TSPL_ADJUSTMENT_DETAIL"
            qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No  "
            qry += "  where Reference_Document='Load Out/Transfer' "
            qry += " union all"
            qry += " select TSPL_SHIPMENT_MASTER.Transfer_No,TSPL_RECEIPT_DETAIL.Receipt_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,0 as Amt,7 as RI,0 as Chk,TSPL_RECEIPT_DETAIL.Applied_Amount as ReceiptAmt from TSPL_RECEIPT_DETAIL"
            qry += " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No"
            qry += " inner join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_RECEIPT_DETAIL.Document_No"
            qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No"
            qry += " where TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' and TSPL_RECEIPT_HEADER.Posted='Y' and LEN(ISNULL(TSPL_SHIPMENT_MASTER.Transfer_No,''))>0"
            qry += " union all"
            qry += " select TSPL_SHIPMENT_MASTER.Transfer_No,TSPL_Receipt_Adjustment_Header.Adjustment_No as DocNo,null as Transfer_Date,'' as Route_No,'' as Route_Desc,'' as Salesmancode ,'' as SalesmanName,'' as From_Location,'' as FromLoc_Desc,0 as Amt,8 as RI,0 as Chk,TSPL_Receipt_Adjustment_Header.Adjustment_Amount as ReceiptAmt from TSPL_Receipt_Adjustment_Header"
            qry += " inner join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_Receipt_Adjustment_Header.Doc_No"
            qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No"
            qry += " where TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' and TSPL_Receipt_Adjustment_Header.Is_Post='Y' "
            qry += " and LEN(ISNULL(TSPL_SHIPMENT_MASTER.Transfer_No,''))>0"
            qry += " )xxx"
            qry += " Group by Transfer_No"
            qry += " having (SUM(Chk) > 0)"
            qry += " order by Transfer_No,Transfer_Date"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "RouteSale", "Report For Route Sales")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'gvlocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'gvlocation.ValueMember = "Code"
        'gvlocation.DisplayMember = "Description"
        gvlocation.DataSource = clsLocation.GetLocationSegments()
        gvlocation.ValueMember = "Code"
        gvlocation.DisplayMember = "Name"
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        gvroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvroute.ValueMember = "route_no"
        gvroute.DisplayMember = "route_desc"

    End Sub
    Sub LoadSales()
        Dim qry As String = "select EMP_CODE as Code, Emp_Name as Name from TSPL_EMPLOYEE_MASTER where Emp_type='Salesman'"
        gvsales.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvsales.ValueMember = "Code"
        gvsales.DisplayMember = "Name"
    End Sub

    Private Sub rbtnall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnall.ToggleStateChanged, rbtnselect.ToggleStateChanged
        gvlocation.Enabled = rbtnselect.IsChecked
    End Sub

    Private Sub rbtnallroute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnallroute.ToggleStateChanged, rbtnselectroute.ToggleStateChanged
        gvroute.Enabled = rbtnselectroute.IsChecked
    End Sub

    Private Sub rbtnallsales_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnallsales.ToggleStateChanged, rbtnselectsales.ToggleStateChanged
        gvsales.Enabled = rbtnselectsales.IsChecked
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
End Class
