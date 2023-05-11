Imports common
''By balwinder againt ticket no BM00000009493
Public Class frmMilkGateEntryWeighment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim WeighmentNotMandatoryInMCC As Boolean = False
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            WeighmentNotMandatoryInMCC = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.WeighmentNotMandatoryInMCC, clsFixedParameterCode.WeighmentNotMandatoryInMCC, Nothing)) = 1)
            If WeighmentNotMandatoryInMCC Then
                Throw New Exception("Gate entry not required")
            End If
            SetUserMgmtNew()
            LoadShift()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(BtnPost, "Press Alt+P Post Trasnaction")
            AddNew()
            UcWeighing1.form_ID = MyBase.Form_ID
            UcWeighing1.LoadPortAndMachine()
            UcWeighing1.LoadSettingAndStart()
            RadGroupBox1.Enabled = False
            txtGWDate.ReadOnly = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickServerDateWithNoChange, clsFixedParameterCode.PickServerDateWithNoChange, Nothing)) = 1
            txtTWDate.ReadOnly = txtGWDate.ReadOnly
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            If clsCommon.CompairString(ex.Message, "Gate entry not required") = CompairStringResult.Equal Then
                CloseForm()
            End If
        End Try
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkGateEntryWeightment)
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

    Private Sub frmMilkGateEntryIn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            Try
                Dim dclReading As Decimal = UcWeighing1.LiveReading
                If dclReading > 0 Then
                    If UsGrossWeight.Status = ERPTransactionStatus.Pending AndAlso UsTareWeight.Status = ERPTransactionStatus.Pending Then
                        txtGrossWeight.Value = dclReading
                    ElseIf UsGrossWeight.Status = ERPTransactionStatus.Approved AndAlso UsTareWeight.Status = ERPTransactionStatus.Pending Then
                        TxtTareWeight.Value = dclReading
                        CalculateNetWeight()
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
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
                      "TSPL_MILK_GATE_ENTRY_WEIGHTMENT" + Environment.NewLine + _
                      "=========Setting Name======" + Environment.NewLine + _
                      "WeighmentNotMandatoryInMCC" + Environment.NewLine + _
                      "PickServerDateWithNoChange")
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        Try
            isNewEntry = True
            BlankAllControls()
            BlankGetEntryAllControls()
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            txtGateEntryNo.Enabled = True
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
        txtGWDate.Value = clsCommon.GETSERVERDATE()
        txtTWDate.Value = txtGWDate.Value
        txtGateEntryNo.Value = ""
        txtGrossWeight.Text = ""
        TxtTareWeight.Text = ""
        txtNetWeight.Text = ""
        UsGrossWeight.Status = ERPTransactionStatus.Pending
        UsTareWeight.Status = ERPTransactionStatus.Pending
        txtNetWeight.Enabled = False
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtGateEntryNo.Value) <= 0 Then
            txtGateEntryNo.Focus()
            errorControl.SetError(txtGateEntryNo, "Please select Gate Entry No")
            Throw New Exception("Please select Gate Entry No")
        End If

        If UsGrossWeight.Status = ERPTransactionStatus.Pending Then
            If txtGrossWeight.Value <= 0 Then
                txtGrossWeight.Focus()
                errorControl.SetError(txtGrossWeight, "Please enter Gross Weight")
                Throw New Exception("Please enter Gross Weight")
            End If
        End If

        If txtNetWeight.Value < 0 Then
            txtNetWeight.Focus()
            errorControl.SetError(txtNetWeight, "Net Weight cant be negative")
            Throw New Exception("Net Weight cant be negative")
        End If

        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMilkGateEntryWeighment()
                obj.Weighment_Code = txtCode.Value
                obj.GW_Weighment_Date = txtGWDate.Value
                obj.TW_Weighment_Date = txtTWDate.Value
                obj.Against_Gate_Entry_No = txtGateEntryNo.Value
                obj.Gross_Weight = txtGrossWeight.Value
                obj.Tare_Weight = TxtTareWeight.Value
                obj.Net_Weight = txtNetWeight.Value
                If UsGrossWeight.Status = ERPTransactionStatus.Pending Then
                    obj.SaveDataGW(obj, isNewEntry)
                ElseIf UsGrossWeight.Status = ERPTransactionStatus.Approved AndAlso UsTareWeight.Status = ERPTransactionStatus.Pending Then
                    obj.SaveDataTW(obj)
                End If
                clsCommon.MyMessageBoxShow("Data saved successfully")
                LoadData(obj.Weighment_Code, NavigatorType.Current)
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
            txtGWDate.Enabled = True
            txtTWDate.Enabled = True
            txtGateEntryNo.Enabled = True
            Dim obj As clsMilkGateEntryWeighment = clsMilkGateEntryWeighment.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Weighment_Code) > 0) Then
                isNewEntry = False
                If obj.TW_Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                ElseIf obj.GW_Status = ERPTransactionStatus.Approved Then
                    btndelete.Enabled = False
                    txtGWDate.Enabled = False
                    txtGateEntryNo.Enabled = False
                Else
                    txtTWDate.Enabled = False
                End If
                UsGrossWeight.Status = obj.GW_Status
                UsTareWeight.Status = obj.TW_Status
                txtCode.Value = obj.Weighment_Code
                txtGWDate.Value = obj.GW_Weighment_Date
                If obj.TW_Weighment_Date IsNot Nothing Then
                    txtTWDate.Value = obj.TW_Weighment_Date
                End If
                txtGateEntryNo.Value = obj.Against_Gate_Entry_No
                LoadGateEntryData(obj.Against_Gate_Entry_No)
                If obj.Gross_Weight = 0 Then
                    txtGrossWeight.Text = ""
                Else
                    txtGrossWeight.Value = obj.Gross_Weight
                End If
                If obj.Tare_Weight = 0 Then
                    TxtTareWeight.Text = ""
                Else
                    TxtTareWeight.Value = obj.Tare_Weight
                End If
                If obj.Net_Weight = 0 Then
                    txtNetWeight.Text = ""
                Else
                    txtNetWeight.Value = obj.Net_Weight
                End If
                If obj.GW_Status = ERPTransactionStatus.Approved AndAlso obj.TW_Status = ERPTransactionStatus.Pending Then
                    ForucWeightment()
                    If clsCommon.myLen(obj.TW_Modified_By) <= 0 Then
                        BtnPost.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If UsGrossWeight.Status = ERPTransactionStatus.Pending Then
                    clsMilkGateEntryWeighment.PostDataGW(txtCode.Value)
                ElseIf UsGrossWeight.Status = ERPTransactionStatus.Approved AndAlso UsTareWeight.Status = ERPTransactionStatus.Pending Then
                    clsMilkGateEntryWeighment.PostDataTW(txtCode.Value)
                Else
                    Throw New Exception("Wrong Combination")
                End If
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
            If (myMessages.deleteConfirm()) Then
                clsMilkGateEntryWeighment.DeleteData(txtCode.Value)
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
        UcWeighing1.CloseCOMPORT()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub btnPendingTareWeight_Click(sender As Object, e As EventArgs) Handles btnPendingTareWeight.Click
        CodeFinder(True, " TW_Status =0 ")
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        CodeFinder(isButtonClicked, "")
    End Sub

    Sub CodeFinder(ByVal isButtonClicked As Boolean, ByVal strExtraFilter As String)
        Dim qry As String = " select Weighment_Code,Against_Gate_Entry_No as GateEntryno,GW_Weighment_Date as GrossWeightDate,Gross_Weight as GrossWeight,case when GW_Status=1 then 'Posted' else 'Pending' end as GrossWeightStatus,TW_Weighment_Date as TareWeightDate,Tare_Weight as TareWeight,case when TW_Status =1 then 'Posted' else 'Pending' end as TareWeightStatus,Net_Weight as NetWeight from TSPL_MILK_GATE_ENTRY_WEIGHTMENT " + _
       " left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No  "
        Dim whrClas As String = " 2=2 "
        If clsCommon.myLen(strExtraFilter) > 0 Then
            whrClas += " and " + strExtraFilter
        End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_MILK_GATE_ENTRY_IN.MCC_CODE in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        LoadData(clsCommon.ShowSelectForm("MGEWIFin", qry, "Weighment_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where Weighment_Code='" + txtCode.Value + "'"
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

    Private Sub txtGateEntryNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateEntryNo._MYValidating
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_USER_MASTER.Default_Location,TSPL_Mcc_MASTER.MCC_NAME,TSPL_MCC_MASTER.is_Reuired_Gate_Entry from TSPL_USER_MASTER  inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' and isnull( is_Reuired_Gate_Entry,0)=1")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Gate entry not required")
            End If
            Dim strMCC As String = clsCommon.myCstr(dt.Rows(0)("Default_Location"))
            If clsCommon.myCstr(strMCC) <> "" Then
                Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(strMCC)
                If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                    Throw New Exception("No shift is opened. one Shift Must be Opened..")
                ElseIf DTShift.Rows.Count > 1 Then
                    Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
                Else
                    Dim qry As String = "select TSPL_MILK_GATE_ENTRY_IN.Entry_Code,convert (varchar(10), TSPL_MILK_GATE_ENTRY_IN.Entry_Date ,103) as  Entry_Date,TSPL_MILK_GATE_ENTRY_IN.Entry_Shift,TSPL_MILK_GATE_ENTRY_IN.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No_Other,TSPL_MILK_GATE_ENTRY_IN.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name,TSPL_MILK_GATE_ENTRY_IN.Cans_Filled,TSPL_MILK_GATE_ENTRY_IN.Cans_Empty,case when TSPL_MILK_GATE_ENTRY_IN.Status=1 then 'Approved' else 'Pending' end as Status,TSPL_MILK_GATE_ENTRY_IN.Remarks  " + Environment.NewLine + _
                    " from TSPL_MILK_GATE_ENTRY_IN " + Environment.NewLine + _
                    " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  " + Environment.NewLine + _
                    " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code " + Environment.NewLine + _
                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_GATE_ENTRY_IN.Transporter_Code  "
                    Dim whrClas As String = "  TSPL_MILK_GATE_ENTRY_IN.MCC_CODE = '" + strMCC + "' and TSPL_MILK_GATE_ENTRY_IN.Shift_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(DTShift.Rows(0).Item("MCC_Shift_date")), "dd/MMM/yyyy") + "' and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='" + clsCommon.myCstr(DTShift.Rows(0).Item("Shift")) + "'  and  TSPL_MILK_GATE_ENTRY_IN.Status=1 and not exists(select 1 from TSPL_MILK_GATE_ENTRY_Weightment where Against_Gate_Entry_No =TSPL_MILK_GATE_ENTRY_IN.Entry_Code and TSPL_MILK_GATE_ENTRY_Weightment.Weighment_Code not in ('" + txtCode.Value + "')) "
                    LoadGateEntryData(clsCommon.ShowSelectForm("WeMGEIFin", qry, "Entry_Code", whrClas, txtGateEntryNo.Value, "", isButtonClicked))
                    ForucWeightment()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub ForucWeightment()
        Try
            txtGrossWeight.Enabled = False
            TxtTareWeight.Enabled = False
            UcWeighing1.Enabled = False
            txtGrossWeight.ReadOnly = True
            TxtTareWeight.ReadOnly = True
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_USER_MASTER.Default_Location,TSPL_Mcc_MASTER.MCC_NAME,TSPL_MCC_MASTER.is_Reuired_Gate_Entry from TSPL_USER_MASTER  inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' and isnull( is_Reuired_Gate_Entry,0)=1")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Gate entry not required")
            End If
            Dim strMCC As String = clsCommon.myCstr(dt.Rows(0)("Default_Location"))
            If clsCommon.myCstr(strMCC) <> "" Then
                Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(strMCC)
                If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                    Throw New Exception("No shift is opened. one Shift Must be Opened..")
                ElseIf DTShift.Rows.Count > 1 Then
                    Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
                Else
                    If clsCommon.CompairString("T", clsCommon.myCstr(DTShift.Rows(0)("Is_Allow_Manual_Gate_Entry_Weighment"))) = CompairStringResult.Equal Then
                        If UsGrossWeight.Status = ERPTransactionStatus.Approved Then
                            TxtTareWeight.Enabled = True
                            TxtTareWeight.ReadOnly = False
                        Else
                            txtGrossWeight.Enabled = True
                            txtGrossWeight.ReadOnly = False
                        End If
                    Else
                        UcWeighing1.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            btnSave.Enabled = False
        End Try
    End Sub


    Sub LoadGateEntryData(ByVal strCode As String)
        Try
            BlankGetEntryAllControls()
            If clsCommon.myLen(strCode) > 0 Then
                Dim obj As clsMilkGateEntryIn = clsMilkGateEntryIn.GetData(strCode, NavigatorType.Current, Nothing)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Entry_Code) > 0) Then
                    txtGateEntryNo.Value = obj.Entry_Code
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
                    SetVehicleToggle()
                    lblTransporterCode.Text = obj.Transporter_Code
                    lblTransporterName.Text = obj.Transporter_Name
                    txtFilledCans.Value = obj.Cans_Filled
                    txtEmpryCans.Value = obj.Cans_Empty
                    txtRemarks.Text = obj.Remarks
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankGetEntryAllControls()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtShiftDate.Value = txtDate.Value
        cboShift.SelectedIndex = -1
        txtMCC.Value = ""
        lblMcc.Text = ""
        txtRoute.Value = ""
        lblRoute.Text = ""
        lblVehicleNo.Text = ""
        txtVehicleNo.Text = ""
        lblTransporterCode.Text = ""
        lblTransporterName.Text = ""
        txtFilledCans.Value = 0
        txtEmpryCans.Value = 0
        txtRemarks.Text = ""
    End Sub

    Private Sub TxtTareWeight_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtTareWeight.Validating
        CalculateNetWeight()
    End Sub

    Sub CalculateNetWeight()
        txtNetWeight.Value = txtGrossWeight.Value - TxtTareWeight.Value
    End Sub


    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
    End Sub
End Class