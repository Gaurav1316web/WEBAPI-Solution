Imports common

Public Class FrmloadoutVSvechileCapacity2
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Sub loadVehicle()
        Dim qry As String = "select Vehicle_Id as [VehicleId] ,Number  from tspl_vehicle_master"
        cbgVechile.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVechile.ValueMember = "VehicleId"
        cbgVechile.DisplayMember = "VehicleId"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.LO_vs_Vechile)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    
    Private Sub FrmloadoutVSvechileCapacity2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkVechileAll.IsChecked = True
        loadVehicle()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")


    End Sub

    Private Sub FrmloadoutVSvechileCapacity2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            chkVechileAll.IsChecked = True
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        funPrint()
    End Sub
    Sub funPrint()
        Dim query As String
        Dim Fdate As String = clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy")
        Dim Tdate As String = clsCommon.myCDate(ToDate.Value, "dd/MM/yyyy")
        query = " Select '" + Fdate + "' as FromDate, '" + Tdate + "' as Todate, '" + clsCommon.GETSERVERDATE() + "' as Rundate, Shipment_Date, MAX(TSPL_VEHICLE_MASTER.Number) as VehicleNo, "
        query += " MAX(Capacity ) as VehicleCapacity, Trip_No, SUM(Weight) as LoadOutWeight, MAX(Capacity)-SUM(Weight ) as Diff from ("
        query += " Select TSPL_SHIPMENT_MASTER.Shipment_No, TSPL_SHIPMENT_MASTER.Shipment_Date, TSPL_SHIPMENT_MASTER.Vehicle_Code, "
        query += " TSPL_SHIPMENT_MASTER.Trip_No, TSPL_ITEM_MASTER.Item_Code , TSPL_SHIPMENT_DETAILS.Unit_code, (TSPL_ITEM_UOM_DETAIL .weight*Shipped_Qty) as Weight "
        query += " from TSPL_SHIPMENT_DETAILS "
        query += " LEFT OUTER JOIN  TSPL_SHIPMENT_MASTER ON TSPL_SHIPMENT_DETAILS.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No "
        query += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_SHIPMENT_DETAILS.Item_Code=TSPL_ITEM_MASTER.Item_Code"
        query += " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code "
        query += " AND TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code "
        query += " WHERE convert(varchar,TSPL_SHIPMENT_MASTER.Shipment_Date,103) >='" + Fdate + "' and convert(varchar,TSPL_SHIPMENT_MASTER.Shipment_Date,103)<='" + Tdate + "' "
        If chkVechileSelect.IsChecked And cbgVechile.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vehicle")
            Exit Sub

        ElseIf chkVechileSelect.IsChecked And cbgVechile.CheckedValue.Count > 0 Then

            query += " and TSPL_SHIPMENT_MASTER.Vehicle_Code in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgVechile.CheckedValue)) + ")"
        End If
        query += " ) XXX LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON XXX.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  "
        query += " GROUP BY XXX.Shipment_Date, TSPL_VEHICLE_MASTER.Vehicle_Id, Trip_No Order By Shipment_Date"

       


        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptLO_vs_VehicleCapacity", "Loadout V/S Vehicle Capacity")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

   
   
  
    Private Sub chkVechileAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVechileAll.ToggleStateChanged
        cbgVechile.Enabled = False
    End Sub

    Private Sub chkVechileSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVechileSelect.ToggleStateChanged
        cbgVechile.Enabled = True
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
End Class
