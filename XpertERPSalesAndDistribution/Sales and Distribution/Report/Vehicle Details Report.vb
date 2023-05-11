Imports common
Imports XpertERPEngine

Public Class Vehicle_Details_Report
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub Vehicle_Details_Report_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            chkdocAll1.IsChecked = True
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.vehicle_Details_Report1)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub Vehicle_Details_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkdocAll1.IsChecked = True
        loadVehicle()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")



        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VEH-DT-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    Sub loadVehicle()
        Dim qry As String = "select Vehicle_Id as [VehicleId] ,Number  from tspl_vehicle_master"
        cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgdoc.ValueMember = "VehicleId"
        cbgdoc.DisplayMember = "VehicleId"
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub chkdocAll1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll1.ToggleStateChanged, chkDoc_select1.ToggleStateChanged
        cbgdoc.Enabled = Not chkdocAll1.IsChecked
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click

        funPrint()
    End Sub
    Sub funPrint()
        Dim query As String
        If chkdocAll1.IsChecked Then
            query = "select Vehicle_Id ,model   ,Number  ,Description ,Vehicle_Reg_No  ,Vehicle_Chesis_No ,Capacity ,Insurance ,Pollution_Check,Trans_Type ,Road_Tax ,Transport_Id ,Logo_Img ,Logo_Img2   from tspl_vehicle_master  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_vehicle_master.Comp_Code  "
        Else
            If chkDoc_select1.IsChecked And cbgdoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Vehicle")
                Exit Sub
            Else
                query = "select Vehicle_Id ,model   ,Number  ,Description ,Vehicle_Reg_No  ,Vehicle_Chesis_No ,Capacity ,Insurance ,Pollution_Check,Trans_Type ,Road_Tax ,Transport_Id ,Logo_Img ,Logo_Img2   from tspl_vehicle_master  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_vehicle_master.Comp_Code  where Vehicle_Id in (" + clsCommon.myCstr(clsCommon.GetMulcallString(cbgdoc.CheckedValue)) + ")"
            End If

        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "VehicleDetails", "VehicleDetails")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        chkdocAll1.IsChecked = True
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub
End Class
