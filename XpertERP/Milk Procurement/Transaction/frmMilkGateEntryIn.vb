Imports common
''By balwinder againt ticket no BM00000009493
Public Class frmMilkGateEntryIn
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim isPickServerDateWithNoChange As Boolean = False
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            LoadShift()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(BtnPost, "Press Alt+P Post Trasnaction")
            isPickServerDateWithNoChange = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickServerDateWithNoChange, clsFixedParameterCode.PickServerDateWithNoChange, Nothing)) = 1
            AddNew()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                btnPrint.Visible = True
            End If
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowFatAndSnfPercentageFields,
                                                                clsFixedParameterType.ShowFatAndSnfPercentageFields,
                                                                Nothing)) = 1, True, False) Then
                LblManualFAT_Per.Show()
                TxtManualFat_Per.Show()
                LblManualSNF_Per.Show()
                TxtManualSNF_Per.Show()
            Else
                LblManualFAT_Per.Hide()
                TxtManualFat_Per.Hide()
                LblManualSNF_Per.Hide()
                TxtManualSNF_Per.Hide()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            If clsCommon.CompairString(ex.Message, "Gate entry not required") = CompairStringResult.Equal Then
                CloseForm()
            End If
        End Try
    End Sub

    Private Sub LoadDefaultData()
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_USER_MASTER.Default_Location,TSPL_Mcc_MASTER.MCC_NAME,TSPL_MCC_MASTER.is_Reuired_Gate_Entry from TSPL_USER_MASTER  inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' and isnull( is_Reuired_Gate_Entry,0)=1")
        'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '    Throw New Exception("Gate entry not required")
        'End If
        'txtMCC.Value = clsCommon.myCstr(dt.Rows(0)("Default_Location"))
        'lblMcc.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
        'If clsCommon.myCstr(txtMCC.Value) <> "" Then
        '    Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(txtMCC.Value)
        '    If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
        '        'Throw New Exception("No shift is opened. one Shift Must be Opened..")
        '    ElseIf DTShift.Rows.Count > 1 Then
        '        'Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
        '    Else
        '        txtShiftDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
        '        cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
        '    End If
        'End If
    End Sub

    Private Sub LoadShift()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)
        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkGateEntryIn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub chkOther_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkOther.ToggleStateChanged
        SetVehicleToggle()
    End Sub

    Sub SetVehicleToggle()
        spltVehicle.Panel1Collapsed = chkOther.Checked
        spltVehicle.Panel2Collapsed = Not chkOther.Checked
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        txtMCC.Value = clsMccMaster.getFinder(" is_Reuired_Gate_Entry=1 ", txtMCC.Value, isButtonClicked)

        If txtMCC.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC.Value) > 0 Then
            lblMcc.Text = clsDBFuncationality.getSingleValue(" select MCC_NAME from TSPL_Mcc_MASTER where MCC_Code = '" + txtMCC.Value + "'", Nothing)
        End If


        'Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(txtMCC.Value)
        'If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("No shifts is opened.Atleats one Shift should be Opened..")
        '    btnSave.Enabled = False
        'ElseIf DTShift.Rows.Count > 1 Then
        '    clsCommon.MyMessageBoxShow("There are more then one shifts are opened.Only one Shift can be Opened..")
        '    Me.Close()
        'Else
        '    btnSave.Enabled = True
        '    txtShiftDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
        '    cboShift.SelectedValue = DTShift.Rows(0).Item("Shift")
        'End If
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description],Mcc_Name,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_Primary_Vehicle_Master.* " _
                & " from TSPL_MCC_ROUTE_MASTER inner join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.vehicle_COde=TSPL_MCC_ROUTE_MASTER.vehicle_Code and TSPL_MCC_ROUTE_MASTER.mcc_code='" & txtMCC.Value & "' and active=1 inner join tspl_mcc_Master on tspl_mcc_Master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code left outer join TSPL_VENDOR_MASTER on TSPL_Primary_Vehicle_Master.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND54", qry)
            If dr IsNot Nothing Then
                txtRoute.Value = clsCommon.myCstr(dr("code"))
                lblRoute.Text = clsCommon.myCstr(dr("Route Description"))
                lblVehicleNo.Text = clsCommon.myCstr(dr("Vehicle_Code"))
                lblTransporterCode.Text = clsCommon.myCstr(dr("Vendor_Code"))
                lblTransporterName.Text = clsCommon.myCstr(dr("Vendor_Name"))
            Else
                txtRoute.Value = ""
                lblRoute.Text = ""
                lblVehicleNo.Text = ""
                lblTransporterCode.Text = ""
                lblTransporterName.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmMilkGateEntryIn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso BtnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                      "========Table Name=========" + Environment.NewLine + _
                      "TSPL_MILK_GATE_ENTRY_IN" + Environment.NewLine + _
                      "=========Setting Name======" + Environment.NewLine + _
                      "PickServerDateWithNoChange")
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        Try
            btnSave.Enabled = False
            'txtShiftDate.Enabled = False
            'cboShift.Enabled = False
            txtDate.ReadOnly = isPickServerDateWithNoChange
            cboShift.Enabled = Not isPickServerDateWithNoChange
            Dim OldMCCCode As String = txtMCC.Value
            Dim OldMCCName As String = lblMcc.Text
            BlankAllControls()
            SetVehicleToggle()
            LoadDefaultData()
            If clsCommon.myLen(OldMCCCode) > 0 Then
                txtMCC.Value = OldMCCCode '
                lblMcc.Text = OldMCCName
            End If
            isNewEntry = True
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
        Catch ex As Exception
            If clsCommon.CompairString(ex.Message, "Gate entry not required") = CompairStringResult.Equal Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtShiftDate.Value = txtDate.Value
        cboShift.SelectedIndex = -1
        SetShiftAuto()
        txtMCC.Value = ""
        lblMcc.Text = ""
        txtRoute.Value = ""
        lblRoute.Text = ""
        lblVehicleNo.Text = ""
        txtVehicleNo.Text = ""
        lblTransporterCode.Text = ""
        lblTransporterName.Text = ""
        txtFilledCans.Text = ""
        txtEmpryCans.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtRemarks.Text = ""
        TxtManualFat_Per.Text = ""
        TxtManualSNF_Per.Text = ""
        numKmReading.Text = ""
        lblLatePenaltyAmt.Text = ""
    End Sub

    Sub SetShiftAuto()
        If isPickServerDateWithNoChange Then
            Dim dtTempFrom As New Date(txtShiftDate.Value.Year, txtShiftDate.Value.Month, txtShiftDate.Value.Day, 6, 0, 0)
            Dim dtTempTo As New Date(txtShiftDate.Value.Year, txtShiftDate.Value.Month, txtShiftDate.Value.Day, 18, 0, 0)
            If txtDate.Value >= dtTempFrom AndAlso txtDate.Value < dtTempTo Then
                cboShift.SelectedValue = "M"
            Else
                cboShift.SelectedValue = "E"
            End If
            If txtShiftDate.Value.Hour >= 0 AndAlso txtShiftDate.Value.Hour < 6 Then
                txtShiftDate.Value = txtShiftDate.Value.AddDays(-1)
            End If
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        '===============Preeti Gupta==================================
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(txtMCC.Value) <= 0 Then
            txtMCC.Focus()
            errorControl.SetError(txtMCC, "Please enter MCC Code")
            Throw New Exception("Please enter MCC Code")
        End If
        If clsCommon.myLen(txtRoute.Value) <= 0 Then
            txtRoute.Focus()
            errorControl.SetError(txtRoute, "Please enter Route Code")
            Throw New Exception("Please enter Route Code")
        End If
        If chkOther.Checked AndAlso clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
            txtVehicleNo.Focus()
            errorControl.SetError(txtVehicleNo, "Please enter other Vehicle Code")
            Throw New Exception("Please enter other Vehicle Code")
        End If
        If txtFilledCans.Value <= 0 Then
            txtFilledCans.Focus()
            errorControl.SetError(txtFilledCans, "Please enter Filled Cans")
            Throw New Exception("Please enter Filled Cans")
        End If
        If clsCommon.myLen(cboShift.SelectedValue) <= 0 Then
            cboShift.Focus()
            errorControl.SetError(cboShift, "Please select shift")
            Throw New Exception("Please select shift")
        End If
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                If isNewEntry AndAlso isPickServerDateWithNoChange Then
                    txtDate.Value = clsCommon.GETSERVERDATE()
                    txtShiftDate.Value = txtDate.Value
                    SetShiftAuto()
                End If

                Dim obj As New clsMilkGateEntryIn()
                obj.Entry_Code = txtCode.Value
                obj.Entry_Date = txtDate.Value
                obj.Shift_Date = txtShiftDate.Value
                obj.Entry_Shift = clsCommon.myCstr(cboShift.SelectedValue)
                obj.MCC_Code = txtMCC.Value
                obj.Route_Code = txtRoute.Value
                obj.Vehicle_No = lblVehicleNo.Text
                If chkOther.Checked Then
                    obj.Vehicle_No_Other = txtVehicleNo.Text
                End If
                obj.Transporter_Code = lblTransporterCode.Text
                obj.Cans_Filled = txtFilledCans.Value
                obj.Cans_Empty = txtEmpryCans.Value
                obj.Remarks = txtRemarks.Text

                obj.KMReading = clsCommon.myCdbl(numKmReading.Value)

                'DATE :23-JAN-17 > CLIENT : SAHAYOG > TICKET/REQUEST NO : SCMPLREQ000005

                If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowFatAndSnfPercentageFields,
                                                                  clsFixedParameterType.ShowFatAndSnfPercentageFields,
                                                                  Nothing)) = 1, True, False) Then
                    obj.Manual_FAT_Per = clsCommon.myCdbl(IIf(clsCommon.myLen(TxtManualFat_Per.Text) > 0, TxtManualFat_Per.Text, 0))
                    obj.Manual_SNF_Per = clsCommon.myCdbl(IIf(clsCommon.myLen(TxtManualSNF_Per.Text) > 0, TxtManualSNF_Per.Text, 0))
                End If

                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data saved successfully")
                LoadData(obj.Entry_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            Dim obj As clsMilkGateEntryIn = clsMilkGateEntryIn.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Entry_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                UsLock1.Status = obj.Status
                txtCode.Value = obj.Entry_Code
                txtDate.Value = obj.Entry_Date
                txtShiftDate.Value = obj.Shift_Date
                cboShift.SelectedValue = obj.Entry_Shift
                txtMCC.Value = obj.MCC_Code
                lblMcc.Text = obj.MCC_Name
                txtRoute.Value = obj.Route_Code
                lblRoute.Text = obj.Route_Name
                lblVehicleNo.Text = obj.Vehicle_No
                If clsCommon.myLen(obj.Vehicle_No_Other) > 0 Then
                    chkOther.Checked = True
                    txtVehicleNo.Text = obj.Vehicle_No_Other
                Else
                    chkOther.Checked = False
                End If
                lblTransporterCode.Text = obj.Transporter_Code
                lblTransporterName.Text = obj.Transporter_Name
                If obj.Cans_Filled = 0 Then
                    txtFilledCans.Text = ""
                Else
                    txtFilledCans.Value = obj.Cans_Filled
                End If

                If obj.Cans_Empty = 0 Then
                    txtEmpryCans.Text = ""
                Else
                    txtEmpryCans.Value = obj.Cans_Empty
                End If
                lblLatePenaltyAmt.Text = clsCommon.myFormat(obj.Penalty_Amount)

                txtRemarks.Text = obj.Remarks
                numKmReading.Text = obj.KMReading

                If Not IsDBNull(obj.Manual_FAT_Per) AndAlso clsCommon.myLen(obj.Manual_FAT_Per) > 0 Then
                    TxtManualFat_Per.Text = obj.Manual_FAT_Per
                End If

                If Not IsDBNull(obj.Manual_SNF_Per) AndAlso clsCommon.myLen(obj.Manual_SNF_Per) > 0 Then
                    TxtManualSNF_Per.Text = obj.Manual_SNF_Per
                End If


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                clsMilkGateEntryIn.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                clsMilkGateEntryIn.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub BtnPost_Click(sender As Object, e As EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_MILK_GATE_ENTRY_IN.Entry_Code,convert (varchar(10), TSPL_MILK_GATE_ENTRY_IN.Entry_Date ,103) as  Entry_Date,TSPL_MILK_GATE_ENTRY_IN.Entry_Shift,TSPL_MILK_GATE_ENTRY_IN.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No_Other,TSPL_MILK_GATE_ENTRY_IN.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name,TSPL_MILK_GATE_ENTRY_IN.Cans_Filled,TSPL_MILK_GATE_ENTRY_IN.Cans_Empty,case when TSPL_MILK_GATE_ENTRY_IN.Status=1 then 'Approved' else 'Pending' end as Status,TSPL_MILK_GATE_ENTRY_IN.Remarks  " + Environment.NewLine + _
        " from TSPL_MILK_GATE_ENTRY_IN " + Environment.NewLine + _
        " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  " + Environment.NewLine + _
        " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code " + Environment.NewLine + _
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_GATE_ENTRY_IN.Transporter_Code  "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_MILK_GATE_ENTRY_IN.MCC_CODE in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        LoadData(clsCommon.ShowSelectForm("MGEIFin", qry, "Entry_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_GATE_ENTRY_IN where Entry_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    '' added by shivani tyagi against ticket no [BM00000009842]
    'KUNAL > TICKET : BM00000010105 > DATE :21-NOV-2016

    Sub funPrint()
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim qry As String = "select entry_code,SUBSTRING(CONVERT(VARCHAR, Entry_Date, 100),13,2) + ':' " & _
                " + SUBSTRING(CONVERT(VARCHAR, Entry_Date, 100),16,2) + '' + SUBSTRING(CONVERT(VARCHAR, Entry_Date, 100),18,2) " & _
                " as  [time],convert(varchar,Shift_Date,103) as Shift_Date  ,case when entry_shift = 'M' then 'Morning' when entry_shift = 'E' then 'Evening' end as entry_shift ,TSPL_MILK_GATE_ENTRY_IN.mcc_code,MCC_NAME ,TSPL_MILK_GATE_ENTRY_IN.route_code,Route_Name  ,vehicle_no,cans_filled,Cans_Empty,convert(varchar,Entry_Date,103) as  Entry_Date ,Comp_Name  from TSPL_MILK_GATE_ENTRY_IN " & _
                " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  " & _
                " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_MILK_GATE_ENTRY_IN.Route_Code left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MCC_MASTER. Comp_Code  where entry_code= '" + txtCode.Value + "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptGateEntryIn", "Milk Gate Entry In")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No document for print")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub

End Class