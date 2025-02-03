''Created By--richa agarwal-TEC/10/09/19-001006----on- 10/09/2019---------------------
Imports common
Imports System.Data.SqlClient



Public Class frmCancelledTransactions_DairySale
    Inherits FrmMainTranScreen
    Dim arrUser As New ArrayList()
    Dim ButtonToolTip As New ToolTip()
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public fromdate As DateTime
    Public Todate As DateTime
    Dim dr As DataRow
    Dim dt As DataTable
    Dim strNoOfRecord As String
    Dim qry As String
    Dim arrSelectedUser As New ArrayList()
    Dim arrLoc As String = Nothing
    Dim RecordCount As Integer = 0

    Private Sub FrmPendingAproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        LoadLocation()
        chkLocAll.CheckState = CheckState.Checked
        ChkUserAll.CheckState = CheckState.Checked
        LoadUsers()
        arrUser = GetSubbordinateUsers(objCommonVar.CurrentUserCode)
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadModuleType()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadModuleProduction()
        If clsCommon.myLen(ModuleName) > 0 Then
            cboModule.SelectedValue = ModuleName
            cboTransaction.SelectedValue = Transaction
            dtpFromDate.Value = fromdate
            dtpToDate.Value = Todate
            ShowData()
        End If
        If clsCommon.CompairString(cboTransaction.Text, "Dispatch") = CompairStringResult.Equal Then
            btnPrintCancel.Visible = False
            lblPrintMsg.Visible = True
        Else
            lblPrintMsg.Visible = False
            btnPrintCancel.Visible = False
        End If
    End Sub
    Public Sub LoadModuleType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Dairy Sale"
        dr("Name") = "Dairy Sale"
        dt.Rows.Add(dr)

        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"

    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then

                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Sub LoadModuleProduction()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))


        dr = dt1.NewRow()
        dr("Code") = "Dispatch"
        dr("Name") = "Dispatch"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sale Invoice"
        dr("Name") = "Sale Invoice"
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Code") = "Dairy GatePass"
        dr("Name") = "Dairy GatePass"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Dairy Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModuleProduction()
        End If

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False
    End Sub
    Sub gv1Format()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Dairy GatePass") = CompairStringResult.Equal Then
            Me.gv1.MasterTemplate.Columns("GatePass No").Width = 120      ''First Column
            Me.gv1.MasterTemplate.Columns("Gate Pass Date").Width = 150
        Else
            Me.gv1.MasterTemplate.Columns("Document Id").Width = 120      ''First Column
            Me.gv1.MasterTemplate.Columns("Document Date").Width = 150    ''Second Column
        End If

        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 2
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i
        Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        For j As Integer = 0 To count - 1
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

#Region "Showing Details on GRID"
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If dtpFromDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "'From date' Can't Be Greater Than 'To Date'", Me.Text)
        Else
            qry = Nothing
            ShowData()
        End If
    End Sub
    Function GetSubbordinateUsers(ByVal strUserCode As String) As ArrayList
        Dim arrUser As New ArrayList
        Try
            Dim qry As String
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                qry = "Select User_Code from TSPL_User_MASTER Where 1=1"
            Else
                qry = "Select User_Code from TSPL_User_MASTER Where Level4_Code='" + strUserCode + "' OR User_Code='" + strUserCode + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                arrUser.Add(clsCommon.myCstr(dr("User_Code")))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrUser
    End Function
    Sub ShowData()
        Try
            If cbgUser.CheckedValue.Count > 0 Then
                arrSelectedUser = cbgUser.CheckedValue
            Else
                arrSelectedUser = arrUser
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Dairy Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillDairySale()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub







    Sub FillDairySale()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Dispatch") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "  Select TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code as [Document Id],convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date ,103) as [Document Date]," &
                " TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_delivery_Code as [Against Delivery No], TSPL_CUSTOMER_MASTER.Cust_Code As [Distributor Code],TSPL_CUSTOMER_MASTER.Customer_Name As [Distributor Name] , TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Route_No As [Route No],TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Route_Desc As [Route Name],Convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Supply_Date,103) As [Supply Date]," &
                " Case When TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Shift_Type='AM' Then 'Morning' Else 'Evening' End As [Supply Shift],Case When TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Is_Taxable=0 Then 'Non-Taxable' Else 'Taxable' End As [Invoice Type],TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Bill_To_Location as [Location Code]," &
                " TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_By as [Created By]," &
                " (convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_Date,103)+' '+convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_Date,108)) as [Created Date],'' as Description,Sale_Invoice_No as [Invoice No],
CONVERT(varchar, TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Sale_Invoice_Date,103) AS [Invoice Date],TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Total_Amt as [Invoice Amount],IRN_No as[IRN No] ,TSPL_SD_SALE_INVOICe_HEAD_Cancel_Data.Ack_No as [Ack No],
TSPL_SD_SALE_INVOICe_HEAD_Cancel_Data.Ack_Date as [Ack Date],TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Cancel_By AS [Canceled By],(CONVERT(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Cancel_On,103)+' '+CONVERT(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Cancel_On,108)) AS [Canceled Date]
                 from TSPL_SD_SHIPMENT_HEAD_Cancel_Data left outer join TSPL_SD_SALE_INVOICe_HEAD_Cancel_Data on TSPL_SD_SALE_INVOICe_HEAD_Cancel_Data.Document_Code=TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Sale_Invoice_No " &
                " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Customer_Code
 Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " &
            " WHERE  convert(date,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
            " and convert(date,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103) and  TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Trans_Type IN ('FS', 'PS') and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Screen_Type='DS'"

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Sale Invoice") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "  Select TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_code  as [Document Id],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date ,103) as [Document Date],  " &
                " TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Against_Shipment_No as [Against Shipment No],TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  as [Location Code], " &
                " TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By as [Created By], " &
                " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description from TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA  " &
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  =TSPL_LOCATION_MASTER.Location_Code " &
                  " WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103) " &
                  " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Trans_Type IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Screen_Type='DS' "


            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += "  ORDER BY TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Dairy GatePass") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "select GPCode as [GatePass No],convert(varchar,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.GPDate ,103) as [Gate Pass Date], Vehicle_Number as [Vehicle Number],Loading_Slip as [Loading Slip],TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Created_By as [Created By] 
,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Created_Date  as [Created Date] ,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Location_Code as [Location Code],Location_Desc  as [Location Name],Cancel_By as [Canceled By] ,Cancel_On as [Canceled Date] ,Remarks as Description,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Route_No,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.DistributorNamE as [Distributor Name]
 from TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data 
            left outer join  TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.ROUTE_NO = TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Route_No 
            WHERE  convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.GatePass_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.GatePass_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Location_Code  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += "order by TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.GPDate , TSPL_DAIRYSALE_GATEPASS_MASTER_Cancel_Data.GPCode"
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            LoadBlankGrid()
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1Format()

            If dt.Rows.Count <= 0 Then
                lblNoOfRecords.Text = "No Record Found"
            Else
                strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
            End If
        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

#End Region


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub


    Private Sub FrmPendingAproval_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub

    Private Sub chkLOcAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLOcSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = True
    End Sub
    Public Sub LoadLocation()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = " Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical' and Location_Code in (" + arrLoc + ")"
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Public Sub LoadUsers()
        Dim qry As String = clsUserMaster.GetSubbordinateUsersQry(objCommonVar.CurrentUserCode)
        cbgUser.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgUser.ValueMember = "User_Code"
        cbgUser.DisplayMember = "User_Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(sender As Object, args As StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub ChkUserAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkUserAll.ToggleStateChanged
        cbgUser.Enabled = Not ChkUserAll.IsChecked
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkLocAll.CheckState = CheckState.Checked
        ChkUserAll.CheckState = CheckState.Checked
        gv1.DataSource = Nothing
    End Sub

    Private Sub btnPrintCancel_Click(sender As Object, e As EventArgs) Handles btnPrintCancel.Click
        printCanceInvoice()
    End Sub

    Sub printCanceInvoice()
        Try
            If clsCommon.CompairString(cboTransaction.Text, "Dispatch") = CompairStringResult.Equal Then
                Dim Doc_Code As String = Nothing
                Dim Doc_Date As DateTime = Nothing
                Dim Inv_Code As String = Nothing
                Dim Cust_Code As String = Nothing
                Dim objPrintInvoice As New frmShipmentDairy
                If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                    Doc_Code = clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value)
                    Doc_Date = clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value)
                    Inv_Code = clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Invoice No").Value)
                    Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Code from TSPL_SD_SHIPMENT_HEAD_Cancel_Data where Document_Code='" + Doc_Code + "'"))
                    objPrintInvoice.PrintInvoiveForAll(Doc_Code, Doc_Date, Inv_Code, True)
                Else
                    Throw New Exception("Data not found !")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub cboTransaction_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTransaction.SelectedValueChanged
        Try
            If clsCommon.CompairString(cboTransaction.Text, "Dispatch") = CompairStringResult.Equal Then
                btnPrintCancel.Visible = False
                lblPrintMsg.Visible = True
            Else
                btnPrintCancel.Visible = False
                lblPrintMsg.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        printCanceInvoice()
    End Sub

    'Private Sub gv1_CellEditorInitialized(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellEditorInitialized
    '    If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
    '        Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
    '        editor.AutoSizeDropDownTofrdfy = True
    '        editor.EditorControl.MasterTemplate.BestFitColumns()
    '        editor.DropDownStyle = RadDropDownStyle.DropDown
    '        editor.AutoFilter = True
    '        'If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
    '        '    Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
    '        '    autoFilter.IsFilterEditor = True
    '        '    editor.EditorControl.FilterDescriptors.Add(autoFilter)
    '        'End If
    '    End If
    'End Sub
End Class




