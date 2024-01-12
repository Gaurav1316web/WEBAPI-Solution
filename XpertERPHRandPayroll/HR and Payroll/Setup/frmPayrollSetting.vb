'--03/11/2014--form Added By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmPayrollSetting
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    '' ctc grid cols
    Const colCTCPayHeadCodeSelect As String = "colCTCPayHeadCodeSelect"
    Const colCTCPayHeadCode As String = "colCTCPayHeadCode"
    Const colCTCPayHeadDesc As String = "colCTCPayHeadDesc"
    Const colCTCPHType As String = "colCTCPHType"
    Const colCTCPHSubHeadType As String = "colCTCPHSubHeadType"

    '' gross grid cols
    Const colGrossPayHeadCodeSelect As String = "colGrossPayHeadCodeSelect"
    Const colGrossPayHeadCode As String = "colGrossPayHeadCode"
    Const colGrossPayHeadDesc As String = "colGrossPayHeadDesc"
    Const colGrossPHType As String = "colGrossPHType"
    Const colGrossPHSubHeadType As String = "colGrossPHSubHeadType"

    '' In Hand grid cols
    Const colInHandPayHeadCodeSelect As String = "colInHandPayHeadCodeSelect"
    Const colInHandPayHeadCode As String = "colInHandPayHeadCode"
    Const colInHandPayHeadDesc As String = "colInHandPayHeadDesc"
    Const colInHandPHType As String = "colInHandPHType"
    Const colInHandPHSubHeadType As String = "colInHandPHSubHeadType"

#End Region
    Sub LoadCTCGridColumns()
        gvCTC.Rows.Clear()
        gvCTC.Columns.Clear()

        Dim CTCPayHeadCodeSelect As New GridViewCheckBoxColumn
        Dim CTCPayHeadCode As New GridViewTextBoxColumn
        Dim CTCPayHeadDesc As New GridViewTextBoxColumn
        Dim CTCPHType As New GridViewTextBoxColumn
        Dim CTCPHSubHeadType As New GridViewTextBoxColumn

        CTCPayHeadCodeSelect.FormatString = ""
        CTCPayHeadCodeSelect.HeaderText = "Select"
        CTCPayHeadCodeSelect.Name = colCTCPayHeadCodeSelect
        CTCPayHeadCodeSelect.Width = 100
        CTCPayHeadCodeSelect.ReadOnly = False
        gvCTC.Columns.Add(CTCPayHeadCodeSelect)

        CTCPayHeadCode.FormatString = ""
        CTCPayHeadCode.HeaderText = "Pay Head Code"
        CTCPayHeadCode.Name = colCTCPayHeadCode
        CTCPayHeadCode.Width = 100
        CTCPayHeadCode.ReadOnly = True
        gvCTC.Columns.Add(CTCPayHeadCode)

        CTCPayHeadDesc.FormatString = ""
        CTCPayHeadDesc.HeaderText = "Pay Head Description"
        CTCPayHeadDesc.Name = colCTCPayHeadDesc
        CTCPayHeadDesc.Width = 150
        CTCPayHeadDesc.ReadOnly = True
        gvCTC.Columns.Add(CTCPayHeadDesc)

        CTCPHType.FormatString = ""
        CTCPHType.HeaderText = "Pay Head Type"
        CTCPHType.Name = colCTCPHType
        CTCPHType.Width = 100
        CTCPHType.ReadOnly = True
        gvCTC.Columns.Add(CTCPHType)

        CTCPHSubHeadType.FormatString = ""
        CTCPHSubHeadType.HeaderText = "Sub Type"
        CTCPHSubHeadType.Name = colCTCPHSubHeadType
        CTCPHSubHeadType.Width = 100
        CTCPHSubHeadType.ReadOnly = True
        gvCTC.Columns.Add(CTCPHSubHeadType)

    End Sub

    Sub LoadGrossGridColumns()
        gvGross.Rows.Clear()
        gvGross.Columns.Clear()

        Dim GrossPayHeadCodeSelect As New GridViewCheckBoxColumn
        Dim GrossPayHeadCode As New GridViewTextBoxColumn
        Dim GrossPayHeadDesc As New GridViewTextBoxColumn
        Dim GrossPHType As New GridViewTextBoxColumn
        Dim GrossPHSubHeadType As New GridViewTextBoxColumn

        GrossPayHeadCodeSelect.FormatString = ""
        GrossPayHeadCodeSelect.HeaderText = "Select"
        GrossPayHeadCodeSelect.Name = colGrossPayHeadCodeSelect
        GrossPayHeadCodeSelect.Width = 100
        GrossPayHeadCodeSelect.ReadOnly = False
        gvGross.Columns.Add(GrossPayHeadCodeSelect)

        GrossPayHeadCode.FormatString = ""
        GrossPayHeadCode.HeaderText = "Pay Head Code"
        GrossPayHeadCode.Name = colGrossPayHeadCode
        GrossPayHeadCode.Width = 100
        GrossPayHeadCode.ReadOnly = True
        gvGross.Columns.Add(GrossPayHeadCode)

        GrossPayHeadDesc.FormatString = ""
        GrossPayHeadDesc.HeaderText = "Pay Head Description"
        GrossPayHeadDesc.Name = colGrossPayHeadDesc
        GrossPayHeadDesc.Width = 150
        GrossPayHeadDesc.ReadOnly = True
        gvGross.Columns.Add(GrossPayHeadDesc)

        GrossPHType.FormatString = ""
        GrossPHType.HeaderText = "Pay Head Type"
        GrossPHType.Name = colGrossPHType
        GrossPHType.Width = 100
        GrossPHType.ReadOnly = True
        gvGross.Columns.Add(GrossPHType)

        GrossPHSubHeadType.FormatString = ""
        GrossPHSubHeadType.HeaderText = "Sub Type"
        GrossPHSubHeadType.Name = colGrossPHSubHeadType
        GrossPHSubHeadType.Width = 100
        GrossPHSubHeadType.ReadOnly = True
        gvGross.Columns.Add(GrossPHSubHeadType)

    End Sub

    Sub LoadInHandGridColumns()
        gvInHand.Rows.Clear()
        gvInHand.Columns.Clear()

        Dim InHandPayHeadCodeSelect As New GridViewCheckBoxColumn
        Dim InHandPayHeadCode As New GridViewTextBoxColumn
        Dim InHandPayHeadDesc As New GridViewTextBoxColumn
        Dim InHandPHType As New GridViewTextBoxColumn
        Dim InHandPHSubHeadType As New GridViewTextBoxColumn

        InHandPayHeadCodeSelect.FormatString = ""
        InHandPayHeadCodeSelect.HeaderText = "Select"
        InHandPayHeadCodeSelect.Name = colInHandPayHeadCodeSelect
        InHandPayHeadCodeSelect.Width = 100
        InHandPayHeadCodeSelect.ReadOnly = False
        gvInHand.Columns.Add(InHandPayHeadCodeSelect)

        InHandPayHeadCode.FormatString = ""
        InHandPayHeadCode.HeaderText = "Pay Head Code"
        InHandPayHeadCode.Name = colInHandPayHeadCode
        InHandPayHeadCode.Width = 100
        InHandPayHeadCode.ReadOnly = True
        gvInHand.Columns.Add(InHandPayHeadCode)

        InHandPayHeadDesc.FormatString = ""
        InHandPayHeadDesc.HeaderText = "Pay Head Description"
        InHandPayHeadDesc.Name = colInHandPayHeadDesc
        InHandPayHeadDesc.Width = 150
        InHandPayHeadDesc.ReadOnly = True
        gvInHand.Columns.Add(InHandPayHeadDesc)

        InHandPHType.FormatString = ""
        InHandPHType.HeaderText = "Pay Head Type"
        InHandPHType.Name = colInHandPHType
        InHandPHType.Width = 100
        InHandPHType.ReadOnly = True
        gvInHand.Columns.Add(InHandPHType)

        InHandPHSubHeadType.FormatString = ""
        InHandPHSubHeadType.HeaderText = "Sub Type"
        InHandPHSubHeadType.Name = colInHandPHSubHeadType
        InHandPHSubHeadType.Width = 100
        InHandPHSubHeadType.ReadOnly = True
        gvInHand.Columns.Add(InHandPHSubHeadType)

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPayrollSetting, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsPayrollSetting()
                obj.PAY_SETTING_CODE = txtCode.Value
                obj.ATTENDANCE_AUTO_GENERATE = chkAutoAttendance.Checked
                obj.EARLY_ARRIVAL_MINUTES_OT = txtEarlyArrivalMintOT.Text
                obj.INTERVAL_MINUTES = txtIntervalMinutes.Text
                obj.LEAVE_ALLOTMENT = IIf(rdbLAMonthly.CheckState = CheckState.Checked, "Monthly", "Yearly")
                obj.APPLY_COMMON_SETTINGS_ALL_LOCATIONS = RadCheckBox1.Checked
                obj.LOCATION_CODE = fndLocationCode.Value
                obj.MAX_HOURS_OT = txtMaximumHoursOT.Text
                obj.MAX_MINT_LTCOMING = txtMaximumMintLateComming.Text
                obj.MIN_HOURS_HFDAY = txtMinHoursHFDay.Text
                obj.MIN_HOURS_OT = txtMinHoursHFDay.Text
                obj.MIN_PRESENT_DAYS_FOR_WEEK_OFF = txtMinimumPdaysWKOff.Text
                obj.NO_LTCOMING_SL = txtNoLcommingSL.Text
                obj.NO_SHORTLEAVE_HFDAY = txtNoSLForHFDay.Text
                obj.REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS = chkRemoveAttdList.Checked
                obj.STATUTORY_DAILY_WORKING_HOURS = txtStatutoryWK_WH.Text
                obj.Gratuity_Period = txtGratuityPeriod.Text
                obj.STATUTORY_WEEK_WORKING_HOURS = txtStatutoryDLWorkingHours.Text
                obj.TREAT_WKOFF_LEAVE_CONTINUOUS = chkTreatWOffLeaveContinuous.Checked
                obj.WORKING_HOURS = txtWorkingHours.Text

                obj.fromTime = clsCommon.myCstr(RtimeFrom.Value)
                obj.ToTime = clsCommon.myCstr(RTimeTo.Value)

                '' ctc saving
                Dim objCTCTr As clsPayrollSetting_CTC_Detail
                For Each grow As GridViewRowInfo In gvCTC.Rows
                    If grow.Cells(colCTCPayHeadCodeSelect).Value = True Then
                        objCTCTr = New clsPayrollSetting_CTC_Detail
                        objCTCTr.PAY_SETTING_CODE = Me.txtCode.Value
                        objCTCTr.PAY_HEAD_CODE = grow.Cells(colCTCPayHeadCode).Value
                        obj.objListCTC.Add(objCTCTr)
                    End If
                Next

                '' gross saving
                Dim objGrossTr As clsPayrollSetting_GROSS_Detail
                For Each grow As GridViewRowInfo In gvGross.Rows
                    If grow.Cells(colGrossPayHeadCodeSelect).Value = True Then
                        objGrossTr = New clsPayrollSetting_GROSS_Detail
                        objGrossTr.PAY_SETTING_CODE = Me.txtCode.Value
                        objGrossTr.PAY_HEAD_CODE = grow.Cells(colGrossPayHeadCode).Value
                        obj.objListGROSS.Add(objGrossTr)
                    End If
                Next

                '' In hand saving
                Dim objInHandTr As clsPayrollSetting_SAL_IN_HAND_Detail
                For Each grow As GridViewRowInfo In gvInHand.Rows
                    If grow.Cells(colInHandPayHeadCodeSelect).Value = True Then
                        objInHandTr = New clsPayrollSetting_SAL_IN_HAND_Detail
                        objInHandTr.PAY_SETTING_CODE = Me.txtCode.Value
                        objInHandTr.PAY_HEAD_CODE = grow.Cells(colInHandPayHeadCode).Value
                        obj.objListInHand.Add(objInHandTr)
                    End If
                Next

                If (obj.SaveData(txtCode.Value, obj, clsPayrollSetting.CheckNewEntry(Me.txtCode.Value))) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.PAY_SETTING_CODE, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ''txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsPayrollSetting()
        obj = clsPayrollSetting.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_SETTING_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.PAY_SETTING_CODE
            chkAutoAttendance.Checked = obj.ATTENDANCE_AUTO_GENERATE
            txtEarlyArrivalMintOT.Text = obj.EARLY_ARRIVAL_MINUTES_OT
            txtIntervalMinutes.Text = obj.INTERVAL_MINUTES
            If obj.LEAVE_ALLOTMENT = "Monthly" Then
                rdbLAMonthly.CheckState = CheckState.Checked
            Else
                rdbLAYearly.CheckState = CheckState.Checked
            End If
            RadCheckBox1.Checked = obj.APPLY_COMMON_SETTINGS_ALL_LOCATIONS
            fndLocationCode.Value = obj.LOCATION_CODE
            txtMaximumHoursOT.Text = obj.MAX_HOURS_OT
            txtMaximumMintLateComming.Text = obj.MAX_MINT_LTCOMING
            txtMinHoursHFDay.Text = obj.MIN_HOURS_HFDAY
            txtMinHoursHFDay.Text = obj.MIN_HOURS_OT
            txtMinimumPdaysWKOff.Text = obj.MIN_PRESENT_DAYS_FOR_WEEK_OFF
            txtNoLcommingSL.Text = obj.NO_LTCOMING_SL
            txtNoSLForHFDay.Text = obj.NO_SHORTLEAVE_HFDAY
            chkRemoveAttdList.Checked = obj.REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS
            txtStatutoryWK_WH.Text = obj.STATUTORY_DAILY_WORKING_HOURS
            txtStatutoryDLWorkingHours.Text = obj.STATUTORY_WEEK_WORKING_HOURS
            chkTreatWOffLeaveContinuous.Checked = obj.TREAT_WKOFF_LEAVE_CONTINUOUS
            txtWorkingHours.Text = obj.WORKING_HOURS
            txtGratuityPeriod.Text = obj.Gratuity_Period
            fndLocationCode.Value = obj.LOCATION_CODE

            RtimeFrom.Value = clsCommon.myCDate(obj.fromTime)
            RTimeTo.Value = clsCommon.myCDate(obj.ToTime)

            lblLocationDesc.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
            '' ctc saving
            Dim objCTCTr As clsPayrollSetting_CTC_Detail
            For Each objCTCTr In obj.objListCTC
                For Each grow As GridViewRowInfo In gvCTC.Rows
                    If objCTCTr.PAY_HEAD_CODE = grow.Cells(colCTCPayHeadCode).Value Then
                        grow.Cells(colCTCPayHeadCodeSelect).Value = True
                    End If
                Next
            Next


            '' gross saving
            Dim objGrossTr As clsPayrollSetting_GROSS_Detail
            For Each objGrossTr In obj.objListGROSS
                For Each grow As GridViewRowInfo In gvGross.Rows
                    grow.Cells(colGrossPayHeadCodeSelect).Value = True
                Next
            Next

            '' In hand saving
            Dim objInHandTr As clsPayrollSetting_SAL_IN_HAND_Detail
            For Each objInHandTr In obj.objListInHand
                For Each grow As GridViewRowInfo In gvInHand.Rows
                    If objInHandTr.PAY_HEAD_CODE = grow.Cells(colInHandPayHeadCode).Value Then
                        grow.Cells(colInHandPayHeadCodeSelect).Value = True
                    End If
                Next
            Next

        End If

    End Sub

    Function AllowToSave() As Boolean

        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsPayrollSetting.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmPayrollSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        LoadCTCGridColumns()
        LoadGrossGridColumns()
        LoadInHandGridColumns()
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPayrollSetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        lblLocationDesc.Text = ""
        fndLocationCode.Value = ""
        txtCode.Focus()
        FunSetDefault()
        LoadGrid(gvCTC)
        LoadGrid(gvGross)
        LoadGrid(gvInHand)
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub
    Sub LoadGrid(ByVal gv As RadGridView)
        gv.Rows.Clear()
        Dim objList As List(Of clsPayHeadDefinitions)
        objList = clsPayHeadDefinitions.GetPayHeadList(Nothing)
        For Each objTr As clsPayHeadDefinitions In objList
            gv.Rows.AddNew()
            gv.Rows(gv.Rows.Count - 1).Cells(0).Value = False
            gv.Rows(gv.Rows.Count - 1).Cells(1).Value = objTr.PAY_HEAD_CODE
            gv.Rows(gv.Rows.Count - 1).Cells(2).Value = objTr.PAY_HEAD_NAME
            gv.Rows(gv.Rows.Count - 1).Cells(3).Value = objTr.HEAD_TYPE
            gv.Rows(gv.Rows.Count - 1).Cells(4).Value = objTr.SUB_HEAD_TYPE
        Next
    End Sub

    Sub FunSetDefault()
        chkAutoAttendance.Checked = False
        chkTreatWOffLeaveContinuous.Checked = False
        chkRemoveAttdList.Checked = False
        txtEarlyArrivalMintOT.Text = 30
        txtIntervalMinutes.Text = 30
        txtMaximumHoursOT.Text = 8
        txtMaximumMintLateComming.Text = 10
        txtMinHoursHFDay.Text = 4
        txtMinHoursOT.Text = 4
        txtMinimumPdaysWKOff.Text = 4
        txtNoLcommingSL.Text = 4
        txtNoSLForHFDay.Text = 4
        txtStatutoryDLWorkingHours.Text = 8
        txtStatutoryWK_WH.Text = 50
        txtWorkingHours.Text = 8
        txtGratuityPeriod.Text = 5
        RadCheckBox1.Checked = True
        Me.fndLocationCode.Enabled = False
        rdbLAMonthly.CheckState = CheckState.Checked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_PAYROLL_SETTING where PAY_SETTING_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = " select PAY_SETTING_CODE AS Code, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary'  from TSPL_PAYROLL_SETTING"
            txtCode.Value = clsPayrollSetting.getFinder("", txtCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", "", txtCode.Value, "PAY_SETTING_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmPayrollSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub RadCheckBox1_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadCheckBox1.CheckStateChanged
        If RadCheckBox1.Checked = True Then
            fndLocationCode.Enabled = False
        Else
            fndLocationCode.Enabled = True
        End If
    End Sub

    Private Sub rdbSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSetDefault.Click
        FunSetDefault()
    End Sub

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationDesc.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            lblLocationDesc.Text = ""
        End If

    End Sub
End Class
