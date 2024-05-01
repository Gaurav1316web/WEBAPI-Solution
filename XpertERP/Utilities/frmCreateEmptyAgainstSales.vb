''-20/06/2012---Updation by --[Pankaj kumar]-- Added FIlters(Location, Route, Transfer No) And Implement it in load-Query---From 'Rakesh Sir'
Imports common
Public Class FrmCreateReceiptAgainstSales
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCreateReceiptAgainstSale)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnSaveAndPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCreateReceiptAgainstSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(True)
        
    End Sub
    Sub LoadData(ByVal isShowMsg As Boolean)
        gv1.DataSource = Nothing
        Dim qry As String = "select CAST(0 as bit) as SEL,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,CONVERT(varchar(11), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,case when TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer' then'Route' else 'Non-Route' end as TransactionType,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc,TSPL_SALE_INVOICE_HEAD.Empty_Value,(TSPL_SALE_INVOICE_HEAD.Empty_Value+TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt) as InvoiceAmt,TSPL_SALE_INVOICE_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc as LocationName    from TSPL_SHIPMENT_MASTER"
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Shipment_No= TSPL_SHIPMENT_MASTER.Shipment_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_HEAD.Location"
        qry += " where   TSPL_SALE_INVOICE_HEAD.Is_Post='Y' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'"
        qry += " and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
        qry += "  and Exists( select 1 from TSPL_SALE_INVOICE_DETAIL where  isnull(TSPL_SALE_INVOICE_DETAIL.Empty_Value,0)>0 and TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)"
        qry += " and Not Exists(select 1 from TSPL_ADJUSTMENT_HEADER where Reference_Document='Sale Invoice' and Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)"
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            qry += " AND TSPL_SALE_INVOICE_HEAD.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
            qry += " AND TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
        End If
        If chkTransferSelect.IsChecked = True AndAlso dgvTransfer.CheckedValue.Count > 0 Then
            qry += " AND TSPL_SHIPMENT_MASTER.Transfer_No in (" + clsCommon.GetMulcallString(dgvTransfer.CheckedValue) + ")"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            If isShowMsg Then
                common.clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
            Exit Sub
        End If
        gv1.DataSource = dt
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
        Next

        gv1.Columns("SEL").ReadOnly = False
        gv1.Columns("SEL").Width = 50
        gv1.Columns("SEL").HeaderText = ""

        gv1.Columns("Sale_Invoice_No").Width = 150
        gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

        gv1.Columns("Sale_Invoice_Date").Width = 100
        gv1.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"

        gv1.Columns("Cust_Code").Width = 100
        gv1.Columns("Cust_Code").HeaderText = "Customer Code"

        gv1.Columns("Cust_Name").Width = 200
        gv1.Columns("Cust_Name").HeaderText = "Customer"

        gv1.Columns("TransactionType").Width = 70
        gv1.Columns("TransactionType").HeaderText = "Transaction Type"

        gv1.Columns("Route_No").Width = 100
        gv1.Columns("Route_No").HeaderText = "Route No"

        gv1.Columns("Route_Desc").Width = 200
        gv1.Columns("Route_Desc").HeaderText = "Route"

        gv1.Columns("Empty_Value").Width = 100
        gv1.Columns("Empty_Value").HeaderText = "Container Depost"

        gv1.Columns("InvoiceAmt").Width = 100
        gv1.Columns("InvoiceAmt").HeaderText = "Invoice Amount"

        gv1.Columns("Location").Width = 100
        gv1.Columns("Location").HeaderText = "Location Code"

        gv1.Columns("LocationName").Width = 150
        gv1.Columns("LocationName").HeaderText = "Location Name"

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 30
    End Sub

    Private Sub btnSaveAndPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAndPost.Click
        Try
            Dim boolIsSalect As Boolean = False
            clsCommon.ProgressBarShow()
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("SEL").Value) Then
                    boolIsSalect = True
                    ClsAdjustments.CreateAndPostEmptyReceiptOfSalesInvoiceOfTransfer(clsCommon.myCstr(gv1.Rows(ii).Cells("Sale_Invoice_No").Value))
                End If
            Next

            If boolIsSalect Then
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Receipt Created Successfully")
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
            LoadData(False)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
            SelectUnselectAll(True)
            btnSelect.Text = "Unselect All"
        Else
            SelectUnselectAll(False)
            btnSelect.Text = "Select All"
        End If
    End Sub

    Sub SelectUnselectAll(ByVal isChecked As Boolean)
        For ii As Integer = 0 To gv1.RowCount - 1
            gv1.Rows(ii).Cells("SEL").Value = isChecked
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ''--Added by--Pankaj kumar------On -----20/06/2012--------------From 'Rakesh Sir '
    Sub LoadRoute()
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type = 'Physical'  order by Location_Code"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub

    Sub LoadTransfer()
        Dim qry As String = "Select Transfer_No as Code, Transfer_Date as Date from TSPL_TRANSFER_HEAD Left Outer Join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code Where TSPL_LOCATION_MASTER.Location_Type='Logical'"
        dgvTransfer.DataSource = clsDBFuncationality.GetDataTable(qry)
        dgvTransfer.ValueMember = "Code"
        dgvTransfer.DisplayMember = "Date"
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkRouteSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub

    Private Sub chkTransferAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTransferAll.ToggleStateChanged
        dgvTransfer.Enabled = False
    End Sub

    Private Sub chkTransferSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTransferSelect.ToggleStateChanged
        dgvTransfer.Enabled = True
    End Sub
    Private Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        LoadTransfer()
        chkTransferAll.IsChecked = True
    End Sub
    ''-------------------------Code Ends Here-----------------------------------------

    Private Sub FrmCreateReceiptAgainstSales_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class
