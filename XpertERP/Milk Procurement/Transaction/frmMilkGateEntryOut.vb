Imports common
''By balwinder againt ticket no BM00000009497
Public Class frmMilkGateEntryOut
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
            SetUserMgmtNew()
            LoadShift()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(BtnPost, "Press Alt+P Post Trasnaction")
            AddNew()
            RadGroupBox1.Enabled = False
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                btnPrint.Visible = True
            End If
            txtGWDate.ReadOnly = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickServerDateWithNoChange, clsFixedParameterCode.PickServerDateWithNoChange, Nothing)) = 1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkGateEntryOut)
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
                          "TSPL_MILK_GATE_ENTRY_OUT" + Environment.NewLine + _
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
            chkGateOutWithoutMilkReceipt.Checked = False
            txtGateOutWithoutMilkReceipt.Text = ""
            txtGateOutWithoutMilkReceipt.Enabled = False
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
        Catch ex As Exception
            If clsCommon.CompairString(ex.Message, "Gate entry not required") = CompairStringResult.Equal Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtGWDate.Value = clsCommon.GETSERVERDATE()
        txtGateEntryNo.Value = ""
        UsGrossWeight.Status = ERPTransactionStatus.Pending
        txtEmptyCansOut.Text = ""
        txtFilledCansOut.Text = ""
        txtRemarksOut.Text = ""
    End Sub

    Private Function AllowToSave() As Boolean
        '===============Preeti Gupta==================================
        If AllowFutureDateTransaction(txtGWDate.Value, Nothing) = False Then
            txtGWDate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(txtGateEntryNo.Value) <= 0 Then
            txtGateEntryNo.Focus()
            errorControl.SetError(txtGateEntryNo, "Please select Gate Entry No")
            Throw New Exception("Please select Gate Entry No")
        End If

        If txtEmptyCansOut.Value <= 0 Then
            txtEmptyCansOut.Focus()
            errorControl.SetError(txtEmptyCansOut, "Please enter empty cans")
            Throw New Exception("Please enter empty cans")
        End If
        Dim qry As String = "select 1 from TSPL_MILK_GATE_ENTRY_IN where exists(select 1 from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine + _
           " where CONVERT(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)=  CONVERT(date, TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) and TSPL_MILK_RECEIPT_HEAD.SHIFT=TSPL_MILK_GATE_ENTRY_IN.Entry_Shift and TSPL_MILK_GATE_ENTRY_IN.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE= TSPL_MILK_GATE_ENTRY_IN.Route_Code) and TSPL_MILK_GATE_ENTRY_IN.Entry_Code='" + txtGateEntryNo.Value + "'"
        Dim dtMilkReceipt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If chkGateOutWithoutMilkReceipt.Checked Then
            If clsCommon.myLen(txtGateOutWithoutMilkReceipt.Text) <= 0 Then
                txtGateOutWithoutMilkReceipt.Focus()
                Throw New Exception("Please enter reson for gate out without milk receipt")
            End If
            If dtMilkReceipt IsNot Nothing AndAlso dtMilkReceipt.Rows.Count > 0 Then
                Throw New Exception("Milk receipt is created so no need to check on gate out without milk receipt")
            End If
        Else
            If Not (dtMilkReceipt IsNot Nothing AndAlso dtMilkReceipt.Rows.Count > 0) Then
                Throw New Exception("Milk receipt is created/Gross weigth is not done.So cannot gate out")
            End If
        End If
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMilkGateEntryOut()
                obj.Gate_Out_Code = txtCode.Value
                obj.Gate_Out_Date = txtGWDate.Value
                obj.Against_Gate_Entry_No = txtGateEntryNo.Value
                obj.Cans_Empty = txtEmptyCansOut.Value
                obj.Cans_Filled = txtFilledCansOut.Value
                obj.Remarks = txtRemarks.Text
                obj.Is_Gateout_Without_Milk_Receipt = chkGateOutWithoutMilkReceipt.Checked
                obj.Reason_Gateout_Without_Milk_Receipt = txtGateOutWithoutMilkReceipt.Text

                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Gate_Out_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            txtGWDate.Enabled = True
            Dim obj As clsMilkGateEntryOut = clsMilkGateEntryOut.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Gate_Out_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                UsGrossWeight.Status = obj.Status
                txtCode.Value = obj.Gate_Out_Code
                txtGWDate.Value = obj.Gate_Out_Date
                txtGateEntryNo.Value = obj.Against_Gate_Entry_No
                If obj.Cans_Empty = 0 Then
                    txtEmptyCansOut.Text = ""
                Else
                    txtEmptyCansOut.Value = obj.Cans_Empty
                End If

                If obj.Cans_Filled = 0 Then
                    txtFilledCansOut.Text = ""
                Else
                    txtFilledCansOut.Value = obj.Cans_Filled
                End If
                chkGateOutWithoutMilkReceipt.Checked = obj.Is_Gateout_Without_Milk_Receipt
                txtGateOutWithoutMilkReceipt.Text = obj.Reason_Gateout_Without_Milk_Receipt

                txtRemarksOut.Text = obj.Remarks
                LoadGateEntryData(obj.Against_Gate_Entry_No)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                clsMilkGateEntryOut.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                clsMilkGateEntryOut.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
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
        Dim qry As String = " select TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code,TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No as GateEntryno,TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Date ,case when TSPL_MILK_GATE_ENTRY_OUT.Status=1 then 'Posted' else 'Pending' end as Status ,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No_Other,TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MILK_GATE_ENTRY_IN.MCC_CODE" + Environment.NewLine + _
        " from TSPL_MILK_GATE_ENTRY_OUT " + Environment.NewLine + _
        " left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No  "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_MILK_GATE_ENTRY_IN.MCC_CODE in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        LoadData(clsCommon.ShowSelectForm("MGEWOFin", qry, "Gate_Out_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_GATE_ENTRY_OUT where Gate_Out_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtGateEntryNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateEntryNo._MYValidating
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_USER_MASTER.Default_Location,TSPL_Mcc_MASTER.MCC_NAME,TSPL_MCC_MASTER.is_Reuired_Gate_Entry from TSPL_USER_MASTER  inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' and isnull( is_Reuired_Gate_Entry,0)=1")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Gate entry not required")
            End If
            Dim strMCC As String = clsCommon.myCstr(dt.Rows(0)("Default_Location"))
            If clsCommon.myLen(strMCC) > 0 Then
                Dim qry As String = ""
                Dim whrClas As String = ""
                 
                If chkGateOutWithoutMilkReceipt.Checked OrElse WeighmentNotMandatoryInMCC Then
                    qry = "select TSPL_MILK_GATE_ENTRY_IN.Entry_Code,convert (varchar(10), TSPL_MILK_GATE_ENTRY_IN.Entry_Date ,103) as  Entry_Date,TSPL_MILK_GATE_ENTRY_IN.Entry_Shift,TSPL_MILK_GATE_ENTRY_IN.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No_Other,TSPL_MILK_GATE_ENTRY_IN.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name,TSPL_MILK_GATE_ENTRY_IN.Cans_Filled,TSPL_MILK_GATE_ENTRY_IN.Cans_Empty,case when TSPL_MILK_GATE_ENTRY_IN.Status=1 then 'Approved' else 'Pending' end as Status,TSPL_MILK_GATE_ENTRY_IN.Remarks " + Environment.NewLine + _
                                  " from  TSPL_MILK_GATE_ENTRY_IN " + Environment.NewLine + _
                                  " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  " + Environment.NewLine + _
                                  " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code " + Environment.NewLine + _
                                  " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_GATE_ENTRY_IN.Transporter_Code  "
                    whrClas = " TSPL_MILK_GATE_ENTRY_IN.MCC_CODE = '" + strMCC + "' and  TSPL_MILK_GATE_ENTRY_IN.Status=1  and not exists(select 1 from TSPL_MILK_GATE_ENTRY_OUT where Against_Gate_Entry_No = TSPL_MILK_GATE_ENTRY_IN.Entry_Code and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code not in ('" + txtCode.Value + "')) "
                    If Not WeighmentNotMandatoryInMCC Then
                        whrClas += " and not exists(select 1 from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine + _
                         " where CONVERT(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)=  CONVERT(date, TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) and TSPL_MILK_RECEIPT_HEAD.SHIFT=TSPL_MILK_GATE_ENTRY_IN.Entry_Shift and TSPL_MILK_GATE_ENTRY_IN.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE= TSPL_MILK_GATE_ENTRY_IN.Route_Code)"
                    End If
                    LoadGateEntryData(clsCommon.ShowSelectForm("OMGsdEIFin", qry, "Entry_Code", whrClas, txtGateEntryNo.Value, "", isButtonClicked))
                Else
                    qry = "select TSPL_MILK_GATE_ENTRY_IN.Entry_Code,convert (varchar(10), TSPL_MILK_GATE_ENTRY_IN.Entry_Date ,103) as  Entry_Date,TSPL_MILK_GATE_ENTRY_IN.Entry_Shift,TSPL_MILK_GATE_ENTRY_IN.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No_Other,TSPL_MILK_GATE_ENTRY_IN.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name,TSPL_MILK_GATE_ENTRY_IN.Cans_Filled,TSPL_MILK_GATE_ENTRY_IN.Cans_Empty,case when TSPL_MILK_GATE_ENTRY_IN.Status=1 then 'Approved' else 'Pending' end as Status,TSPL_MILK_GATE_ENTRY_IN.Remarks,TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code   " + Environment.NewLine + _
                                   " from TSPL_MILK_GATE_ENTRY_WEIGHTMENT" + Environment.NewLine + _
                                   " left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code = TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No" + Environment.NewLine + _
                                   " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  " + Environment.NewLine + _
                                   " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code " + Environment.NewLine + _
                                   " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_GATE_ENTRY_IN.Transporter_Code  "
                    whrClas = "  TSPL_MILK_GATE_ENTRY_IN.MCC_CODE = '" + strMCC + "'  and not exists(select 1 from TSPL_MILK_GATE_ENTRY_OUT where Against_Gate_Entry_No = TSPL_MILK_GATE_ENTRY_IN.Entry_Code and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code not in ('" + txtCode.Value + "')) "
                    whrClas += " and  TSPL_MILK_GATE_ENTRY_WEIGHTMENT.TW_Status=1"
                    LoadGateEntryData(clsCommon.ShowSelectForm("OTMGEIFin", qry, "Entry_Code", whrClas, txtGateEntryNo.Value, "", isButtonClicked))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    '' added by shivani tyagi against ticket no [BM00000009842]
    ' KUNAL > TICKET : BM00000010191 > DATE : 21 - NOV - 2016

    Sub funPrint()
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim qry As String = "select entry_code,SUBSTRING(CONVERT(VARCHAR, Entry_Date, 100),13,2) + ':' + SUBSTRING(CONVERT(VARCHAR, Entry_Date, 100),16,2) + '' + SUBSTRING(CONVERT(VARCHAR, Entry_Date, 100),18,2) " & _
            " as  [timein],convert(varchar,Shift_Date,103) as Shift_Date  ,case when entry_shift = 'M' then 'Morning' when entry_shift = 'E' then 'Evening' end as entry_shift ,TSPL_MILK_GATE_ENTRY_IN.mcc_code,MCC_NAME ,TSPL_MILK_GATE_ENTRY_IN.route_code,Route_Name  ,vehicle_no,TSPL_MILK_GATE_ENTRY_IN . cans_filled  as Cans_Filld_in,TSPL_MILK_GATE_ENTRY_IN.Cans_Empty as Cans_Empty_in,convert(varchar,Entry_Date,103) as  Entry_Date, " & _
            " SUBSTRING(CONVERT(VARCHAR, Gate_Out_Date, 100),13,2) + ':' " & _
            " + SUBSTRING(CONVERT(VARCHAR, Gate_Out_Date, 100),16,2) + ''" & _
            " +SUBSTRING(Convert(VARCHAR, Gate_Out_Date, 100), 18, 2) " & _
            " as  [timeOut],isnull(TSPL_MILK_GATE_ENTRY_OUT.Cans_Empty,0)  as Cans_empty_Out,isnull(TSPL_MILK_GATE_ENTRY_OUT.Cans_Filled,0) as Cans_Filled_Out,Gate_Out_Code ,Comp_Name " & _
                " from TSPL_MILK_GATE_ENTRY_IN left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_GATE_ENTRY_IN.MCC_CODE " & _
            " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_MILK_GATE_ENTRY_IN.Route_Code " & _
            " left join TSPL_MILK_GATE_ENTRY_OUT on TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No = TSPL_MILK_GATE_ENTRY_IN.Entry_Code " & _
            " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MCC_MASTER.Comp_Code " & _
            " where Gate_Out_Code= '" + txtCode.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptGateEntryOut", "Milk Gate Entry Out")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No document for print", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
     
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub

    Private Sub chkGateOutWithoutMilkReceipt_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkGateOutWithoutMilkReceipt.ToggleStateChanged
        txtGateOutWithoutMilkReceipt.Enabled = chkGateOutWithoutMilkReceipt.Checked
    End Sub
End Class