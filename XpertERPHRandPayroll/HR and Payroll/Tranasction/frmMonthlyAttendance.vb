'' work done against ticket no. BHA/01/02/19-000799
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.IO

Public Class frmMonthlyAttendance
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsMonthAttendance
    Const ColSNo As String = "SNo"
    Const colempCode As String = "empCode"
    Const colempName As String = "EmpName"
    Const colDOJ As String = "colDOJ"
    Const colPayPeriodDays As String = "PayPeriodDays"
    Const colPresentDays As String = "PresentDays"
    Const colAbsentDays As String = "AbsentDays"
    'Const colLeaveDays As String = "LeaveDays"
    Const colEL As String = "colEL"
    Const colCL As String = "colCL"
    Const colMATL As String = "colMATL"
    Const colMEDL As String = "colMEDL"
    Const colCOFF As String = "colCOFF"
    Const colOther As String = "colOther"
    Const colHolidayDays As String = "HolidayDays"
    Const colWeeklyOffDays As String = "WeeklyOffDays"
    Const colPayDays As String = "PayDays"
    'Const colPayableDays As String = "PayableDays"
    'Const colLOPDays As String = "LOPDays"
    Private ObjList As New List(Of clsMonthAttendanceDetail)
    Private isCellValueChangedOpen As Boolean = False
    Dim sQuery As String = String.Empty

    Dim AbsentDays As Double = 0
    Dim PresentDays As Double = 0
    Dim CL As Double = 0
    Dim EL As Double = 0
    Dim Coff As Double = 0
    Dim WeeklyOff As Double = 0
    Dim MatL As Double = 0
    Dim MedL As Double = 0
    Dim Other As Double = 0
    Dim Holidays As Double = 0
    Dim alertType As String = "0"
    Dim EL_Leave_Code As String = ""
    Dim CL_Leave_Code As String = ""
    Dim COFF_Leave_Code As String = ""
    Dim MATRL_Leave_Code As String = ""
    Dim MED_Leave_Code As String = ""
    Dim OTHER_Leave_Code As String = ""
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpenSummary As Boolean = False
    Private Shared QryAttendanceCodeSelection As String = ""
    Sub LoadGridColumns()
        Dim SNo As New GridViewTextBoxColumn
        Dim empCode As New GridViewTextBoxColumn
        Dim empName As New GridViewTextBoxColumn
        Dim payPeriodDays As New GridViewDecimalColumn
        Dim PayDays As New GridViewDecimalColumn
        Dim presentDays As New GridViewDecimalColumn
        Dim absentDays As New GridViewDecimalColumn
        'Dim leaveDays As New GridViewDecimalColumn
        Dim EarnedLeave As New GridViewDecimalColumn
        Dim CasualLeave As New GridViewDecimalColumn
        Dim COff As New GridViewDecimalColumn
        Dim MaternityLeave As New GridViewDecimalColumn
        Dim MadicalLeave As New GridViewDecimalColumn
        Dim OtherLeave As New GridViewDecimalColumn
        Dim holidayDays As New GridViewDecimalColumn
        Dim weeklyOffDays As New GridViewDecimalColumn
        'Dim payableDays As New GridViewDecimalColumn
        'Dim LOPtDays As New GridViewDecimalColumn

        gvMonthlyAttendance.Rows.Clear()
        gvMonthlyAttendance.Columns.Clear()

        SNo.FormatString = ""
        SNo.HeaderText = "SNo"
        SNo.Name = ColSNo
        SNo.Width = 50
        SNo.ReadOnly = True
        SNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(SNo)

        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = colempCode
        empCode.Width = 100
        'empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMonthlyAttendance.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = colempName
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMonthlyAttendance.Columns.Add(empName)

        empName = New GridViewTextBoxColumn
        empName.FormatString = ""
        empName.HeaderText = "DOJ"
        empName.Name = colDOJ
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMonthlyAttendance.Columns.Add(empName)

        payPeriodDays.FormatString = ""
        'payPeriodDays.HeaderText = "Pay Period Days"
        payPeriodDays.HeaderText = "Total Days"
        payPeriodDays.Name = colPayPeriodDays
        payPeriodDays.Width = 100
        payPeriodDays.ReadOnly = True

        payPeriodDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(payPeriodDays)

        PayDays.FormatString = ""
        PayDays.HeaderText = "Pay Days"
        PayDays.Name = colPayDays
        PayDays.Width = 100
        PayDays.ReadOnly = True
        PayDays.Minimum = 0
        PayDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(PayDays)

        presentDays.FormatString = ""
        presentDays.HeaderText = "Present Days"
        presentDays.Name = colPresentDays
        presentDays.Width = 100
        'presentDays.ReadOnly = True
        presentDays.Minimum = 0
        presentDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(presentDays)


        absentDays.FormatString = ""
        absentDays.HeaderText = "Absent Days"
        absentDays.Name = colAbsentDays
        absentDays.Width = 100
        'absentDays.ReadOnly = True
        absentDays.Minimum = 0
        absentDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(absentDays)



        'leaveDays.FormatString = ""
        'leaveDays.HeaderText = "Leave Days"
        'leaveDays.Name = colLeaveDays
        'leaveDays.Width = 100
        'leaveDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvMonthlyAttendance.Columns.Add(leaveDays)

        EarnedLeave.FormatString = ""
        EarnedLeave.HeaderText = "Earned Leave"
        EarnedLeave.Name = colEL
        EarnedLeave.Width = 100
        EarnedLeave.Minimum = 0
        EarnedLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(EarnedLeave)

        CasualLeave.FormatString = ""
        CasualLeave.HeaderText = "Casual Leave"
        CasualLeave.Name = colCL
        CasualLeave.Width = 100
        CasualLeave.Minimum = 0
        CasualLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(CasualLeave)

        COff.FormatString = ""
        COff.HeaderText = "COff"
        COff.Name = colCOFF
        COff.Width = 100
        COff.Minimum = 0
        COff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(COff)

        MaternityLeave.FormatString = ""
        MaternityLeave.HeaderText = "Maternity Leave"
        MaternityLeave.Name = colMATL
        MaternityLeave.Width = 100
        MaternityLeave.Minimum = 0
        MaternityLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(MaternityLeave)

        MadicalLeave.FormatString = ""
        MadicalLeave.HeaderText = "Medical Leave"
        MadicalLeave.Name = colMEDL
        MadicalLeave.Width = 100
        MadicalLeave.Minimum = 0
        MadicalLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(MadicalLeave)

        OtherLeave.FormatString = ""
        OtherLeave.HeaderText = "Other Leave"
        OtherLeave.Name = colOther
        OtherLeave.Width = 100
        OtherLeave.Minimum = 0
        OtherLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(OtherLeave)






        holidayDays.FormatString = ""
        holidayDays.HeaderText = "Holiday Days"
        holidayDays.Name = colHolidayDays
        holidayDays.Width = 100
        holidayDays.Minimum = 0
        holidayDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(holidayDays)

        weeklyOffDays.FormatString = ""
        weeklyOffDays.HeaderText = "Weekly Off Days"
        weeklyOffDays.Name = colWeeklyOffDays
        weeklyOffDays.Width = 100
        weeklyOffDays.Minimum = 0
        weeklyOffDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMonthlyAttendance.Columns.Add(weeklyOffDays)

        'payableDays.FormatString = ""
        'payableDays.HeaderText = "Payable Days"
        'payableDays.Name = colPayableDays
        'payableDays.Width = 100
        'payableDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvMonthlyAttendance.Columns.Add(payableDays)

        'LOPtDays.FormatString = ""
        'LOPtDays.HeaderText = "LOP Days"
        'LOPtDays.Name = colLOPDays
        'LOPtDays.Width = 100
        'LOPtDays.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvMonthlyAttendance.Columns.Add(LOPtDays)
        gvMonthlyAttendance.EnableFiltering = True
        ReStoreGridLayout()
    End Sub

    Private Sub frmMonthlyAttendance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMonthAttendance.ReverseAndUnpost(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            txtBranch.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
        Else
            txtBranch.Value = ""
            lblLocationDesc.Text = ""
        End If
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        alertType = clsFixedParameter.GetData(clsFixedParameterType.LeaveBalanceAlertTypeOnAttendance, clsFixedParameterCode.LeaveBalanceAlertTypeOnAttendance, Nothing)
        btnReverse.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        btnPost.Enabled = False
        RadButton1.Visible = False
        ShowLeaveCode()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMonthlyAttendance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadSplitButton1.Visible = MyBase.isExport
        If MyBase.isExport = True Then
            Export.Enabled = True
            Import.Enabled = True
        Else
            Export.Enabled = False
            Import.Enabled = False
        End If
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnReverse.Visible = False

    End Sub

    Private Sub ShowLeaveCode()
        Try
            QryAttendanceCodeSelection += "select 'P' as Code union all select 'HD'
             union all select 'A'  union all select 'HO' union all select 'WO'"

            EL_Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("EL", Nothing)
            If clsCommon.myLen(EL_Leave_Code) > 0 Then
                lblEarnedLeave.Text = "Earned Leave (" + EL_Leave_Code + ")"
                QryAttendanceCodeSelection += " union all select '" + EL_Leave_Code + "' "
            Else
                lblEarnedLeave.Visible = False
                lblELBal.Visible = False
            End If
            CL_Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("CL", Nothing)
            If clsCommon.myLen(CL_Leave_Code) > 0 Then
                lblCasualLeave.Text = "Casual Leave (" + CL_Leave_Code + ")"
                QryAttendanceCodeSelection += " union all select '" + CL_Leave_Code + "' "
            Else
                lblCasualLeave.Visible = False
                lblCLBal.Visible = False
            End If
            COFF_Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("COFF", Nothing)
            If clsCommon.myLen(COFF_Leave_Code) > 0 Then
                lblCOff.Text = "COff (" + COFF_Leave_Code + ")"
                QryAttendanceCodeSelection += " union all select '" + COFF_Leave_Code + "' "
            Else
                lblCOff.Visible = False
                lblCOFFBal.Visible = False
            End If
            MATRL_Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MATRL", Nothing)
            If clsCommon.myLen(MATRL_Leave_Code) > 0 Then
                lblMaternityLeave.Text = "Maternity Leave (" + MATRL_Leave_Code + ")"
                QryAttendanceCodeSelection += " union all select '" + MATRL_Leave_Code + "' "
            Else
                lblMaternityLeave.Visible = False
                lblMATRLBal.Visible = False
            End If
            MED_Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MED", Nothing)
            If clsCommon.myLen(MED_Leave_Code) > 0 Then
                lblMedicalLeave.Text = "Medical Leave (" + MED_Leave_Code + ")"
                QryAttendanceCodeSelection += " union all select '" + MED_Leave_Code + "' "
            Else
                lblMedicalLeave.Visible = False
                lblMEDBal.Visible = False
            End If
            OTHER_Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("OTHER", Nothing)
            If clsCommon.myLen(OTHER_Leave_Code) > 0 Then
                lblOtherLeave.Text = "Other Leave (" + OTHER_Leave_Code + ")"
                QryAttendanceCodeSelection += " union all select '" + OTHER_Leave_Code + "' "
            Else
                lblOtherLeave.Visible = False
                lblOTHERBal.Visible = False
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub
    Sub funClose()
        Me.Close()
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        findEnteredBy.Value = Nothing
        findPayperiod.Value = Nothing
        txtDescription.Text = ""
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            txtBranch.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
        Else
            txtBranch.Value = ""
            lblLocationDesc.Text = ""
        End If
        lblFromDate.Text = ""
        lblToDate.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        btnPost.Enabled = False
        isCellValueChangedOpenSummary = False
        GroupBox1.Text = ""
        lblELBal.Text = "0.0"
        lblCLBal.Text = "0.0"
        lblCOFFBal.Text = "0.0"
        lblMATRLBal.Text = "0.0"
        lblMEDBal.Text = "0.0"
        lblOTHERBal.Text = "0.0"
        Me.gvMonthlyAttendance.Rows.Clear()
        Me.gvMonthlyAttendance.Rows.AddNew()
        gvAttendanceDetail.DataSource = Nothing
        gvAttendanceSummary.DataSource = Nothing
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        obj = clsMonthAttendance.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.MTA_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.MTA_CODE
            findPayperiod.Value = obj.PAY_PERIOD_CODE
            findEnteredBy.Value = clsCommon.myCstr(obj.ENTEREDBY_EMP_CODE)
            txtDescription.Text = obj.DESCRIPTION
            txtPayPeriodName.Text = obj.PAY_PERIOD_NAME
            txtBranch.Value = obj.LOCATION_CODE
            lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
            lblFromDate.Text = obj.DATE_FROM
            lblToDate.Text = obj.DATE_TO

            txtCode.MyReadOnly = True
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                For Each objtr As clsMonthAttendanceDetail In obj.ObjList
                    gvMonthlyAttendance.Rows.AddNew()
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(ColSNo).Value = objtr.SNo
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colempCode).Value = objtr.empCode
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colempName).Value = objtr.empName
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colDOJ).Value = objtr.DOJ
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colPayPeriodDays).Value = objtr.PayPeriodDays
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colPresentDays).Value = objtr.PresentDays
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colAbsentDays).Value = objtr.AbsentDays
                    'gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colLeaveDays).Value = objtr.LeaveDays
                    '==========Add By Rohit==========
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colEL).Value = objtr.EarnedLeave
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colCL).Value = objtr.CasualLeave
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colCOFF).Value = objtr.Coff
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colMATL).Value = objtr.MaternityLeave
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colMEDL).Value = objtr.MedicalLeave
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colOther).Value = objtr.OtherLeave
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colHolidayDays).Value = objtr.HolidayDays
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colWeeklyOffDays).Value = objtr.WEEKLY_OFF_DAYS
                    gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colPayDays).Value = objtr.PayDays
                    'gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colPayableDays).Value = objtr.PayableDays
                    'gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colLOPDays).Value = objtr.LOPDays
                Next
            Else
                gvMonthlyAttendance.Rows.AddNew()
            End If
            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(obj.MTA_CODE)
            End If
            clsCustomFieldGrid.FillDataInGrid(obj.MTA_CODE, MyBase.Form_ID, gvMonthlyAttendance)
            UcAttachment1.Form_ID = MyBase.Form_ID
            UcAttachment1.LoadData(obj.MTA_CODE)
            LoadBiometricAttendance(False)
        End If
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " TSPL_LOCATION_MASTER.LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_MONTHLY_ATTENDANCE where MTA_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select MTA_CODE as Code, PAY_PERIOD_CODE as [Pay Period Code], ENTEREDBY_EMP_CODE AS 'Entered By',DESCRIPTION as Description,TSPL_MONTHLY_ATTENDANCE.LOCATION_CODE as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name] from TSPL_MONTHLY_ATTENDANCE left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MONTHLY_ATTENDANCE.LOCATION_CODE  "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MONTHLY_ATTENDANCE", qry, "Code", whrcls, txtCode.Value, "MTA_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As New clsMonthAttendance
            obj.MTA_CODE = txtCode.Value
            obj.PAY_PERIOD_CODE = findPayperiod.Value
            obj.ENTEREDBY_EMP_CODE = findEnteredBy.Value
            obj.DESCRIPTION = txtDescription.Text
            obj.PAY_PERIOD_NAME = txtPayPeriodName.Text
            obj.LOCATION_CODE = txtBranch.Value

            ObjList = New List(Of clsMonthAttendanceDetail)
            For Each grow As GridViewRowInfo In gvMonthlyAttendance.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                    Dim objtr As New clsMonthAttendanceDetail()
                    objtr.MTA_CODE = txtCode.Value
                    objtr.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                    objtr.PayPeriodDays = clsCommon.myCdbl(grow.Cells(colPayPeriodDays).Value)
                    objtr.PresentDays = clsCommon.myCdbl(grow.Cells(colPresentDays).Value)
                    objtr.AbsentDays = clsCommon.myCdbl(grow.Cells(colAbsentDays).Value)
                    objtr.HolidayDays = clsCommon.myCdbl(grow.Cells(colHolidayDays).Value)
                    objtr.WEEKLY_OFF_DAYS = clsCommon.myCdbl(grow.Cells(colWeeklyOffDays).Value)
                    'objtr.LeaveDays = clsCommon.myCdbl(grow.Cells(colLeaveDays).Value)
                    'objtr.PayableDays = clsCommon.myCdbl(grow.Cells(colPayableDays).Value)
                    'objtr.LOPDays = clsCommon.myCdbl(grow.Cells(colLOPDays).Value)
                    objtr.EarnedLeave = clsCommon.myCdbl(grow.Cells(colEL).Value)
                    objtr.CasualLeave = clsCommon.myCdbl(grow.Cells(colCL).Value)
                    objtr.MaternityLeave = clsCommon.myCdbl(grow.Cells(colMATL).Value)
                    objtr.MedicalLeave = clsCommon.myCdbl(grow.Cells(colMEDL).Value)
                    objtr.Coff = clsCommon.myCdbl(grow.Cells(colCOFF).Value)
                    objtr.OtherLeave = clsCommon.myCdbl(grow.Cells(colOther).Value)
                    objtr.PayDays = clsCommon.myCdbl(grow.Cells(colPayDays).Value)
                    ObjList.Add(objtr)
                End If
            Next
            obj.ObjList = ObjList
            ''For Custom Fields
            obj.Form_ID = MyBase.Form_ID
            obj.arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(obj.arrCustomFields)
            End If
            'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
            '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gvJob, MyBase.ArrDetailFields, colICode)
            'End If
            ''End of For Custom Fields
            ' Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
            ' UcAttachment1.SaveData(obj.MTA_CODE)

            If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                UcAttachment1.SaveData(obj.MTA_CODE)
                LoadData(obj.MTA_CODE, NavigatorType.Current)
                Return True
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MONTHLY_ATTENDANCE where MTA_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            myMessages.blankValue("Pay Period Code")
            findPayperiod.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvMonthlyAttendance.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                ii += 1
                If (clsCommon.myCdbl(grow.Cells(colPresentDays).Value) + clsCommon.myCdbl(grow.Cells(colAbsentDays).Value) _
                + clsCommon.myCdbl(grow.Cells(colHolidayDays).Value) + clsCommon.myCdbl(grow.Cells(colWeeklyOffDays).Value) + clsCommon.myCdbl(grow.Cells(colEL).Value) + clsCommon.myCdbl(grow.Cells(colCL).Value) + clsCommon.myCdbl(grow.Cells(colMATL).Value) + clsCommon.myCdbl(grow.Cells(colMEDL).Value) + clsCommon.myCdbl(grow.Cells(colCOFF).Value) + clsCommon.myCdbl(grow.Cells(colOther).Value)) <>
                clsCommon.myCdbl(grow.Cells(colPayPeriodDays).Value) Then
                    'And (clsCommon.myCdbl(grow.Cells(colPayableDays).Value) + clsCommon.myCdbl(grow.Cells(colLOPDays).Value)) <> clsCommon.myCdbl(grow.Cells(colPayPeriodDays).Value) Then
                    clsCommon.MyMessageBoxShow(Me, "No of days is not equal to total days in row : " + ii.ToString() + ".")
                    Return False
                End If
                '' check attendance code of the employee
                If clsDailyAttendance.CheckEmployeeAttendanceType(clsCommon.myCstr(grow.Cells(colempCode).Value), findPayperiod.Value, "MT") = False Then
                    clsCommon.MyMessageBoxShow(Me, "Employee Code " & clsCommon.myCstr(grow.Cells(colempCode).Value) & " at row number : " & (grow.Index + 1) & " does not belong to Monthly Attendance. Update Attendance type in Employee Status.")
                    Return False
                End If
            End If
        Next
        Dim countRecord As Integer = 0
        For jj As Integer = 0 To gvMonthlyAttendance.Rows.Count - 1
            Dim strEmpCode As String = clsCommon.myCstr(gvMonthlyAttendance.Rows(jj).Cells(colempCode).Value)
            If clsCommon.myLen(strEmpCode) > 0 Then 'Ticket No : BHA/02/05/19-000879 By Prabhakar
                countRecord += 1
                For jjq As Integer = 0 To gvMonthlyAttendance.Rows.Count - 1
                    If jjq = jj Then
                        Continue For
                    End If
                    Dim strInnerEmpCode As String = clsCommon.myCstr(gvMonthlyAttendance.Rows(jjq).Cells(colempCode).Value)
                    If clsCommon.CompairString(strEmpCode, strInnerEmpCode) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Employee Exist at Row No " + clsCommon.myCstr(jj + 1) + " And " + clsCommon.myCstr(jjq + 1)
                        Msg = Msg + Environment.NewLine + "Employee: " + strEmpCode + ""
                        common.clsCommon.MyMessageBoxShow(Me, Msg)
                        Return False
                    End If
                Next
            End If

        Next
        If countRecord <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Employee Not Available for Save.", Me.Text) ' Ticket No : BHA/02/05/19-000880 by prabhakar
            Return False
        End If
        If RaiseLeaveBalanceAlert(True) = False Then
            Return False
        End If
        Return True
    End Function
    ' Ticket No : BHA/01/03/19-000827 By Prabhakar
    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
        & " PAY_PERIOD_NAME as 'Pay Period Name',date_from as [From Date],date_to as [Date To] FROM TSPL_PAYPERIOD_MASTER  "
        findPayperiod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", " POSTED=1 AND FREEZED=0 and convert(date, date_to,103) <= Convert (date,SYSDATETIME(),103)", findPayperiod.Value, "", isButtonClicked)
        If clsCommon.myLen(findPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
            txtPayPeriodName.Text = clspp.Name
            Me.txtPayPeriodDays.Text = (DateDiff(DateInterval.Day, clspp.DATE_FROM, clspp.DATE_TO) + 1)
            lblFromDate.Text = clsCommon.GetPrintDate(clspp.DATE_FROM, "dd/MMM/yyyy")
            lblToDate.Text = clsCommon.GetPrintDate(clspp.DATE_TO, "dd/MMM/yyyy")
        Else
            txtPayPeriodName.Text = ""
            Me.txtPayPeriodDays.Text = 0
            lblFromDate.Text = ""
            lblToDate.Text = ""
        End If
    End Sub

    Private Sub findEnteredBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findEnteredBy._MYValidating
        Dim whrcls As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " LOCATION_CODE in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim qry As String = "SELECT EMP_CODE AS 'Code',EMP_Name as 'Employee Name' FROM TSPL_EMPLOYEE_MASTER "
        findEnteredBy.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", whrcls, findEnteredBy.Value, "", isButtonClicked)
    End Sub

    Private Sub gvMonthlyAttendance_CellBeginEdit(sender As Object, e As GridViewCellCancelEventArgs) Handles gvMonthlyAttendance.CellBeginEdit
        AbsentDays = clsCommon.myCdbl(e.Row.Cells(colAbsentDays).Value)
        PresentDays = clsCommon.myCdbl(e.Row.Cells(colPresentDays).Value)
        CL = clsCommon.myCdbl(e.Row.Cells(colCL).Value)
        EL = clsCommon.myCdbl(e.Row.Cells(colEL).Value)
        Coff = clsCommon.myCdbl(e.Row.Cells(colCOFF).Value)
        WeeklyOff = clsCommon.myCdbl(e.Row.Cells(colWeeklyOffDays).Value)
        Holidays = clsCommon.myCdbl(e.Row.Cells(colHolidayDays).Value)
        MatL = clsCommon.myCdbl(e.Row.Cells(colMATL).Value)
        MedL = clsCommon.myCdbl(e.Row.Cells(colMEDL).Value)
        Other = clsCommon.myCdbl(e.Row.Cells(colOther).Value)
    End Sub

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMonthlyAttendance.CellEndEdit
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True

            If e.Column Is gvMonthlyAttendance.Columns(colempCode) Then
                Dim strq As String
                'strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
                '& " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_MONTHLY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
                '& " left join TSPL_HOURLY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"

                strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation,TT4.Joining_date,TT4.PF_NO as [PF No] FROM (" _
    & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
    & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
    & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
    & " WHERE Location_Code='" & txtBranch.Value & "' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
    & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
    & " LEFT JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
    & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE "

                Dim cond As String
                cond = " WORKING_STATUS='Working' and 2=(case when  TT4.RELIEVING_DATE is null then (case when  len( TT4.Joining_date) <=0 then 3 else (case when convert(date,TT4.Joining_date,103) <='" + lblToDate.Text + "'  then 2 else 3 end) end) else (case when  (convert(date,TT4.RELIEVING_DATE,103) >='" + lblToDate.Text + "'  or convert(date,TT4.RELIEVING_DATE,103) between '" + lblFromDate.Text + "'  and '" + lblToDate.Text + "'  ) then 2 else 3 end) end) and (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') " _
       & " AND TT1.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_MONTHLY_ATTENDANCE_DETAIL T1 " _
       & " JOIN TSPL_monthly_ATTENDANCE T2 ON T1.MTA_CODE=T2.MTA_CODE WHERE T2.PAY_PERIOD_CODE='" & Me.findPayperiod.Value & "')"



                Dim obj As clsEmployeeMaster = clsMonthAttendance.FinderForEmployee(clsCommon.myCstr(gvMonthlyAttendance.CurrentRow.Cells(colempCode).Value), False, strq, cond)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                    gvMonthlyAttendance.CurrentRow.Cells(colempCode).Value = obj.EMP_CODE
                    gvMonthlyAttendance.CurrentRow.Cells(colempName).Value = obj.Emp_Name
                    gvMonthlyAttendance.CurrentRow.Cells(colDOJ).Value = clsCommon.GetPrintDate(clsEmployeeMaster.GetDateofJoining(obj.EMP_CODE, Nothing), "dd/MMM/yyyy")
                    gvMonthlyAttendance.CurrentRow.Cells(colPayPeriodDays).Value = clsCommon.myCdbl(txtPayPeriodDays.Text) - GetNotJoinedDays(lblFromDate.Text, gvMonthlyAttendance.CurrentRow.Cells(colDOJ).Value)

                End If
            Else
                '' DONE by panch raj against ticket no:BM00000008224
                updateAttendance(gvMonthlyAttendance.CurrentRow)
                If RaiseLeaveBalanceAlert(False) = False Then
                    gvMonthlyAttendance.CurrentRow.Cells(colPresentDays).Value = PresentDays
                    gvMonthlyAttendance.CurrentRow.Cells(colAbsentDays).Value = AbsentDays
                    gvMonthlyAttendance.CurrentRow.Cells(colEL).Value = EL
                    gvMonthlyAttendance.CurrentRow.Cells(colCL).Value = CL

                    gvMonthlyAttendance.CurrentRow.Cells(colCOFF).Value = Coff
                    gvMonthlyAttendance.CurrentRow.Cells(colWeeklyOffDays).Value = WeeklyOff
                    gvMonthlyAttendance.CurrentRow.Cells(colHolidayDays).Value = Holidays

                    gvMonthlyAttendance.CurrentRow.Cells(colMEDL).Value = MedL
                    gvMonthlyAttendance.CurrentRow.Cells(colMATL).Value = MatL
                    gvMonthlyAttendance.CurrentRow.Cells(colOther).Value = Other

                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    'Private Sub gvMonthlyAttendance_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMonthlyAttendance.CellValueChanged
    '    'If Not isCellValueChangedOpen Then
    '    '    isCellValueChangedOpen = True
    '    '    If e.Column Is gvMonthlyAttendance.Columns(colempCode) Then
    '    '        Dim strq As String
    '    '        strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
    '    '        & " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_DAILY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
    '    '        & " left join TSPL_HOURLY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"
    '    '        Dim obj As clsEmployeeMaster = clsMonthAttendance.FinderForEmployee(clsCommon.myCstr(gvMonthlyAttendance.CurrentRow.Cells(colempCode).Value), False, strq)
    '    '        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
    '    '            gvMonthlyAttendance.CurrentRow.Cells(colempCode).Value = obj.EMP_CODE
    '    '            gvMonthlyAttendance.CurrentRow.Cells(colempName).Value = obj.Emp_Name
    '    '            gvMonthlyAttendance.CurrentRow.Cells(colPayPeriodDays).Value = clsCommon.myCdbl(txtPayPeriodDays.Text)

    '    '        End If
    '    '    End If
    '    '    isCellValueChangedOpen = False
    '    'End If
    'End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
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
                If (clsMonthAttendance.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsMonthAttendance.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles Export.Click
        Try
            '' Against Ticket No:BM00000008077 by Preeti Gupta on 15/10/2015
            Dim LocCode As String = String.Empty
            Dim DivCode As String = String.Empty
            Dim str1 As String = String.Empty
            Dim qry As String = String.Empty
            Dim Divqry As String = String.Empty
            Dim WhrCls As String = String.Empty
            Dim DivWhrCls As String = String.Empty
            Dim LocWhrCls As String = String.Empty
            Dim dtgv As New DataTable
            Dim DTLoc As New DataTable
            Dim str As String = ""
            If clsCommon.myLen(txtCode.Value) > 0 Then
                str = "select ROW_NUMBER ()over (order by [Employee Code]) as SNo,* from (select MA.PAY_PERIOD_CODE as [Payperiod Code],MAD.EMP_CODE as [Employee Code],EMP.Emp_Name as [Employee Name],EMP.PF_NO as [PF No], " &
               " MAD.TOTAL_DAYS as [Total Days],MAD.PAYABLE_DAYS AS [Pay Days] ,MAD.PRESENT_DAYS as [Present Days],MAD.ABSENT_DAYS as [Absent Days],MAD.Earned_Leave as [Earned Leave], " &
               " MAD.Casual_Leave as [Casual Leave],MAD.Maternity_Leave as [Maternity Leave],MAD.Medical_Leave as [Medical Leave],MAD.Coff as [Coff], " &
               " MAD.Other_Leave as [Other Leave],MAD.HOLIDAYS_DAYS as [Holidays],MAD.WEEKLY_OFF_DAYS as [Weekly Off Days],MA.ENTEREDBY_EMP_CODE as [Entered By], " &
               " MA.ENTEREDBY_EMP_CODE as [Entered By Name]  " &
               " from TSPL_MONTHLY_ATTENDANCE_DETAIL MAD inner join TSPL_MONTHLY_ATTENDANCE MA on MAD.MTA_CODE=MA.MTA_CODE " &
               " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON MAD.EMP_CODE=EMP.EMP_CODE  where MA.MTA_CODE='" & txtCode.Value & "') as Final "

                transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)
            Else
                'str = "select 'OCT/2014' as [Payperiod Code],'Dummy' as [Employee Code],'Dummy' as [Employee Name],31 as [Payperiod Days],21 as [Present Days]," _
                '      & " 2 as [Absent Days],1 as [Earned Leave],1 as [Casual Leave],0 as [Maternity Leave],0 as [Medical Leave],0 as [Coff],0 as [Other Leave],4 as [Holidays],1 as [Weekly Off Days],'' as [Entered By],'' as [Entered By Name] "

                ''''''''''''''''''''''''''''''''''''''''''

                qry = " SELECT location_code As Code,Location_Desc As [Description],Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As [Address],Division_Code AS [Division Code],Division_Name AS [Division Name],City_Code As [City Code],State ,Pin_Code AS [Pin Code],Location_Type AS [Location Type],Loc_Status AS [Loc Status],Loc_Segment_Code As [Loc Segment Code] FROM TSPL_location_master "
                LocCode = clsCommon.ShowSelectForm("Loc", qry, "Code", "", LocCode, "Code", True)

                If clsCommon.myLen(LocCode) > 0 Then
                    If clsCommon.myLen(findPayperiod.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please select payperiod code", Me.Text)
                        Return
                    End If


                    str1 = "select ROW_NUMBER ()over (order by TSPL_EMPLOYEE_MASTER.emp_code) as SNo, '" & findPayperiod.Value & "' as [Payperiod Code],TSPL_EMPLOYEE_MASTER.Emp_Code as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],TSPL_EMPLOYEE_MASTER.PF_NO as [PF No]," & txtPayPeriodDays.Text & " as [Total Days]," & txtPayPeriodDays.Text & " - 2 AS [Pay Days],21 as [Present Days]," _
                          & " 2 as [Absent Days],1 as [Earned Leave],1 as [Casual Leave],0 as [Maternity Leave],0 as [Medical Leave],0 as [Coff],0 as [Other Leave],4 as [Holidays],1 as [Weekly Off Days],'' as [Entered By],'' as [Entered By Name]  FROM TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code "
                    LocWhrCls = str1 + " Where  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' "
                    DTLoc = clsDBFuncationality.GetDataTable(LocWhrCls)
                    If DTLoc IsNot Nothing AndAlso DTLoc.Rows.Count > 0 Then

                        Divqry = " SELECT DISTINCT ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') AS [Code],ISNULL(TSPL_DEVISION_MASTER.DEVISION_NAME,'') AS [Division Name] FROM TSPL_EMPLOYEE_MASTER " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code " &
                              " LEFT OUTER JOIN TSPL_DEVISION_MASTER ON TSPL_DEVISION_MASTER.DEVISION_CODE = TSPL_EMPLOYEE_MASTER.DEVISION_CODE " &
                              " left JOIN (select CURRENT_STATUS.Emp_Code,CURRENT_STATUS.WORKING_STATUS,CURRENT_STATUS.REVISION_NO from  TSPL_EMPLOYEE_STATUS CURRENT_STATUS inner join (" &
                              " select EMP_CODE,MAX(REVISION_NO) AS REVISION_NO " &
                              " from TSPL_EMPLOYEE_STATUS GROUP BY EMP_CODE " &
                              " ) AS lAST_STATUS ON  CURRENT_STATUS.EMP_CODE=lAST_STATUS.EMP_CODE " &
                              " AND CURRENT_STATUS.REVISION_NO=lAST_STATUS.REVISION_NO) AS CURRENT_STATUS ON TSPL_EMPLOYEE_MASTER.EMP_CODE=CURRENT_STATUS.EMP_CODE  "

                        DivWhrCls = Divqry + " Where  CURRENT_STATUS.WORKING_STATUS='Working' and TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' AND LEN( ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'')) >0 "

                        dtgv = clsDBFuncationality.GetDataTable(DivWhrCls)

                        If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then

                            DivCode = clsCommon.ShowSelectForm("LocDiv", Divqry, "Code", "  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' AND LEN( ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'')) >0 ", DivCode, "Code", True)

                            If clsCommon.myLen(DivCode) > 0 Then
                                WhrCls = " AND ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') ='" & DivCode & "' and (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')))) "
                            Else
                                clsCommon.MyMessageBoxShow(Me, "First select division code.", Me.Text)
                                Return
                            End If
                        Else
                            ' KUNAL > TICKET : BM00000009910 > DATE : 3 - OCTOBER - 2016
                            WhrCls = " AND (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')))) "
                        End If
                        transportSql.ExporttoExcel(str1, " AND TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "'" & WhrCls & "", Me)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "First select location code.", Me.Text)
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Import_Click(sender As Object, e As EventArgs) Handles Import.Click
        Try
            ImportMonthlyAttendance()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ImportMonthlyAttendance()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "SNo", "Payperiod Code", "Employee Code", "Employee Name", "PF No", "Total Days", "Pay Days", "Present Days", "Absent Days", "Earned Leave", "Casual Leave", "Maternity Leave", "Medical Leave", "Coff", "Other Leave", "Holidays", "Weekly Off Days", "Entered By", "Entered By Name") Then
            Try
                clsCommon.ProgressBarShow()
                Dim obj As New clsMonthAttendance
                ObjList = New List(Of clsMonthAttendanceDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    i = i + 1
                    Dim strPayperiod As String = clsCommon.myCstr(grow.Cells("Payperiod Code").Value)
                    If clsCommon.myLen(strPayperiod) <= 0 Then
                        Throw New Exception("Payperiod Code Can Not Be Left Blank")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & strPayperiod & "'") = 0 Then
                        Throw New Exception("Invalid Payperiod Code. Code Not Found In Master")
                    End If
                    If clsCommon.myLen(txtBranch.Value) <= 0 Then
                        Throw New Exception("Location Code can not be left blank")
                    End If
                    If i = 1 Then
                        obj.PAY_PERIOD_CODE = strPayperiod
                        obj.ENTEREDBY_EMP_CODE = clsCommon.myCstr(grow.Cells("Entered By").Value)
                        obj.DESCRIPTION = ""
                        obj.PAY_PERIOD_NAME = clsDBFuncationality.getSingleValue("select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & strPayperiod & "'")
                        obj.LOCATION_CODE = txtBranch.Value
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Employee Code").Value)) > 0 Then
                        Dim objtr As New clsMonthAttendanceDetail()
                        objtr.MTA_CODE = txtCode.Value
                        objtr.empCode = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                        sQuery = "select count(EMP_CODE) as emp from (select (EMP_CODE),year(DATE_TO) as Years from TSPL_MONTHLY_ATTENDANCE_DETAIL inner join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE.MTA_Code= TSPL_MONTHLY_ATTENDANCE_DETAIL.mta_code  inner join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE where EMP_CODE='" & objtr.empCode & "' and TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE='" & strPayperiod & "'and YEAR(DATE_TO)=year('" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "')) as xx "
                        Dim check As Integer = clsDBFuncationality.getSingleValue(sQuery)
                        If check > 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Please Check Attendance Employee [" & clsCommon.myCstr(grow.Cells("Employee Name").Value) & "] in Line no '" & i & "' is Already Exits.")
                            Exit Sub
                        End If
                        '' check attendance code of the employee
                        If clsDailyAttendance.CheckEmployeeAttendanceType(clsCommon.myCstr(objtr.empCode), strPayperiod, "MT") = False Then
                            Throw New Exception("Employee Code " & clsCommon.myCstr(objtr.empCode) & " at row number : " & (grow.Index + 1) & " does not belong to Monthly Attendance. Update Attendance type in Employee Status.")
                            Exit Sub
                        End If
                        '=======================================
                        Dim Checkcount As String = "select count(EMP_CODE) from TSPL_MONTHLY_ATTENDANCE_DETAIL inner join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE.MTA_Code= TSPL_MONTHLY_ATTENDANCE_DETAIL.mta_code   where EMP_CODE='" & objtr.empCode & "' and PAY_PERIOD_CODE='" & strPayperiod & "' "
                        Dim checkEmp As Integer = clsDBFuncationality.getSingleValue(Checkcount)
                        If checkEmp > 0 Then
                            Dim AttendanceCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select TSPL_MONTHLY_ATTENDANCE.MTA_Code from TSPL_MONTHLY_ATTENDANCE_DETAIL inner join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE.MTA_Code= TSPL_MONTHLY_ATTENDANCE_DETAIL.mta_code   where EMP_CODE='" & objtr.empCode & "' and PAY_PERIOD_CODE='" & strPayperiod & "' "))
                            Throw New Exception("Please Check Attendance Code (" + AttendanceCode + "),(" + objtr.empCode + ") Same Employee is Already Exits.")
                            Exit Sub
                        End If
                        '=====================================

                        '======================Added by preeti gupta[BHA/16/05/19-000893,BHA/16/05/19-000892,BHA/09/05/19-000884,BHA/03/05/19-000881] ============
                        Dim strq As String = ""
                        strq = "SELECT distinct  TT1.EMP_CODE as Code FROM (" _
                  & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
                  & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
                  & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
                  & " WHERE Location_Code='" & txtBranch.Value & "' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
                  & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
                  & " LEFT JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
                  & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE where WORKING_STATUS='Working' " _
                         & " and 2=(case when  TT4.RELIEVING_DATE is null then (case when  len( TT4.Joining_date) <=0 then 3 else (case when convert(date,TT4.Joining_date,103) <='" + lblToDate.Text + "'  then 2 else 3 end) end) else (case when  (convert(date,TT4.RELIEVING_DATE,103) >='" + lblToDate.Text + "'  or convert(date,TT4.RELIEVING_DATE,103) between '" + lblFromDate.Text + "'  and '" + lblToDate.Text + "'  ) then 2 else 3 end) end) and TT1.EMP_CODE='" & objtr.empCode & "' "
                        Dim cond As String
                        cond = " (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') " _
                               & " AND coalesce(TT3.EMP_CODE,'') NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_MONTHLY_ATTENDANCE_DETAIL T1 " _
                               & " JOIN TSPL_monthly_ATTENDANCE T2 ON T1.MTA_CODE=T2.MTA_CODE WHERE T2.PAY_PERIOD_CODE='" & Me.findPayperiod.Value & "')"
                        strq = strq & " and " & cond
                        Dim NewEmpCode As String = clsDBFuncationality.getSingleValue(strq)
                        'Ticket No-BHA/16/05/19-000891 Change Message
                        If clsCommon.CompairString(objtr.empCode, NewEmpCode) <> CompairStringResult.Equal Then
                            Throw New Exception("EMP CODE [" & clsCommon.myCstr(grow.Cells("Employee Name").Value) & "] has been resigned/Join after selected PayPeriod")
                            Exit Sub
                        End If

                        '=========================================================
                        objtr.SNo = clsCommon.myCdbl(grow.Cells("SNo").Value)
                        'objtr.PayPeriodDays = clsCommon.myCdbl(grow.Cells("Total Days").Value)
                        objtr.PayPeriodDays = clsCommon.myCdbl(txtPayPeriodDays.Text) - GetNotJoinedDays(lblFromDate.Text, clsEmployeeMaster.GetDateofJoining(objtr.empCode, Nothing))
                        objtr.PayDays = clsCommon.myCdbl(grow.Cells("Pay Days").Value)
                        objtr.PresentDays = clsCommon.myCdbl(grow.Cells("Present Days").Value)
                        objtr.AbsentDays = clsCommon.myCdbl(grow.Cells("Absent Days").Value)
                        objtr.HolidayDays = clsCommon.myCdbl(grow.Cells("Holidays").Value)
                        objtr.WEEKLY_OFF_DAYS = clsCommon.myCdbl(grow.Cells("Weekly Off Days").Value)
                        'objtr.LeaveDays = clsCommon.myCdbl(grow.Cells("Leave Days").Value)
                        '==============Rohit
                        objtr.EarnedLeave = clsCommon.myCdbl(grow.Cells("Earned Leave").Value)
                        objtr.CasualLeave = clsCommon.myCdbl(grow.Cells("Casual Leave").Value)
                        objtr.MaternityLeave = clsCommon.myCdbl(grow.Cells("Maternity Leave").Value)
                        objtr.MedicalLeave = clsCommon.myCdbl(grow.Cells("Medical Leave").Value)
                        objtr.Coff = clsCommon.myCdbl(grow.Cells("coff").Value)
                        objtr.OtherLeave = clsCommon.myCdbl(grow.Cells("Other Leave").Value)
                        '==================================================================
                        'objtr.PayableDays = clsCommon.myCdbl(grow.Cells("Payable Days").Value)
                        'objtr.LOPDays = clsCommon.myCdbl(grow.Cells("LOP Days").Value)
                        If objtr.PayPeriodDays <> (objtr.PresentDays + objtr.AbsentDays + objtr.HolidayDays + objtr.WEEKLY_OFF_DAYS + objtr.EarnedLeave + objtr.CasualLeave + objtr.Coff + objtr.MaternityLeave + objtr.MedicalLeave + objtr.OtherLeave + objtr.LOPDays) Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Please Check Attendance in Line no '" & i & "' Employee Code: " & objtr.empCode & "")
                            Exit Sub
                        End If
                        RaiseLeaveBalanceAlertLeaveEnter(objtr, strPayperiod, grow.Index + 1)
                        ObjList.Add(objtr)
                    End If
                Next
                obj.ObjList = ObjList

                obj.SaveData(obj, True)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, ", Me.Text, MessageBoxButtons.OK)
                LoadData(obj.MTA_CODE, NavigatorType.Current)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub
    Function RaiseLeaveBalanceAlert(ByVal OnSave As Boolean) As Boolean
        '' get LeaveBalanceAlertType 
        'alertType= clsFixedParameter.GetSpecification(clsFixedParameterType.LeaveBalanceAlertTypeOnAttendance, clsFixedParameterCode.LeaveBalanceAlertTypeOnAttendance, Nothing)
        Try
            If clsCommon.CompairString(alertType, "0") = CompairStringResult.Equal Then
                Return True
            ElseIf clsCommon.CompairString(alertType, "1") = CompairStringResult.Equal Then
                If OnSave = True Then
                    RaiseLeaveBalanceAlertSave()
                End If

            ElseIf clsCommon.CompairString(alertType, "2") = CompairStringResult.Equal Then
                RaiseLeaveBalanceAlertLeaveEnter()
            ElseIf clsCommon.CompairString(alertType, "3") = CompairStringResult.Equal Then
                If OnSave Then
                    RaiseLeaveBalanceAlertSave()
                Else
                    RaiseLeaveBalanceAlertLeaveEnter()
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

    End Function
    Sub RaiseLeaveBalanceAlertSave()
        Dim Pay_Period_Code As String = findPayperiod.Value
        For Each gRow As GridViewRowInfo In gvMonthlyAttendance.Rows
            Dim EMP_Code As String = clsCommon.myCstr(gRow.Cells(colempCode).Value)
            Dim Leave_Code As String = ""
            Dim Balance As Decimal = 0
            For Each gcol As GridViewColumn In gvMonthlyAttendance.Columns
                Dim CurrentEntered As Decimal = 0
                If gcol.Name = colEL Then
                    Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("EL", Nothing)
                    Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
                    CurrentEntered = gRow.Cells(colEL).Value
                    If CurrentEntered > 0 Then
                        If CurrentEntered > Balance Then
                            Throw New Exception("Available EL Balance: " & Balance & " " & Environment.NewLine & "Entered EL: " & CurrentEntered & " at Line No:" & (gRow.Index + 1) & "")
                        End If
                    End If

                ElseIf gcol.Name = colCL Then
                    Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("CL", Nothing)
                    Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
                    CurrentEntered = gRow.Cells(colCL).Value
                    If CurrentEntered > 0 Then
                        If CurrentEntered > Balance Then
                            Throw New Exception("Available CL Balance: " & Balance & " " & Environment.NewLine & "Entered CL: " & CurrentEntered & " at Line No:" & (gRow.Index + 1) & "")
                        End If
                    End If

                ElseIf gcol.Name = colCOFF Then
                    Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("COFF", Nothing)
                    Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
                    CurrentEntered = gRow.Cells(colCOFF).Value
                    If CurrentEntered > 0 Then
                        If CurrentEntered > Balance Then
                            Throw New Exception("Available COFF Balance: " & Balance & " " & Environment.NewLine & "Entered COFF: " & CurrentEntered & " at Line No:" & (gRow.Index + 1) & "")
                        End If
                    End If

                ElseIf gcol.Name = colMEDL Then
                    Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MED", Nothing)
                    Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
                    CurrentEntered = gRow.Cells(colMEDL).Value
                    If CurrentEntered > 0 Then
                        If CurrentEntered > Balance Then
                            Throw New Exception("Available Medical Balance: " & Balance & " " & Environment.NewLine & "Entered Medical: " & CurrentEntered & " at Line No:" & (gRow.Index + 1) & "")
                        End If
                    End If

                ElseIf gcol.Name = colMATL Then
                    Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MATRL", Nothing)
                    Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
                    CurrentEntered = gRow.Cells(colMATL).Value
                    If CurrentEntered > 0 Then
                        If CurrentEntered > Balance Then
                            Throw New Exception("Available Maternity Balance: " & Balance & " " & Environment.NewLine & "Entered Maternity: " & CurrentEntered & " at Line No:" & (gRow.Index + 1) & "")
                        End If
                    End If

                End If

            Next

        Next
    End Sub
    Sub RaiseLeaveBalanceAlertLeaveEnter()
        If gvMonthlyAttendance.CurrentCell Is Nothing Then
            Exit Sub
        End If
        Dim Pay_Period_Code As String = findPayperiod.Value
        Dim EMP_Code As String = clsCommon.myCstr(gvMonthlyAttendance.CurrentRow.Cells(colempCode).Value)
        Dim Leave_Code As String = ""
        Dim Balance As Decimal = 0
        Dim CurrentEntered As Decimal = gvMonthlyAttendance.CurrentCell.Value
        If gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colEL Then
            Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("EL", Nothing)
            Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
            If CurrentEntered > 0 Then
                If CurrentEntered > Balance Then
                    Throw New Exception("Available EL Balance: " & Balance & " " & Environment.NewLine & "Entered EL: " & CurrentEntered & "")
                End If
            End If

        ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colCL Then
            Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("CL", Nothing)
            Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
            If CurrentEntered > 0 Then
                If CurrentEntered > Balance Then
                    Throw New Exception("Available CL Balance: " & Balance & " " & Environment.NewLine & "Entered CL: " & CurrentEntered & "")
                End If
            End If

        ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colCOFF Then
            Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("COFF", Nothing)
            Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
            If CurrentEntered > Balance Then
                Throw New Exception("Available COFF Balance: " & Balance & " " & Environment.NewLine & "Entered COFF: " & CurrentEntered & "")
            End If
        ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colMEDL Then
            Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MED", Nothing)
            Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
            If CurrentEntered > 0 Then
                If CurrentEntered > Balance Then
                    Throw New Exception("Available Medical Balance: " & Balance & " " & Environment.NewLine & "Entered Medical: " & CurrentEntered & "")
                End If
            End If

        ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colMATL Then
            Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MATRL", Nothing)
            Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
            If CurrentEntered > 0 Then
                If CurrentEntered > Balance Then
                    Throw New Exception("Available Maternity Balance: " & Balance & " " & Environment.NewLine & "Entered Maternity: " & CurrentEntered & "")
                End If
            End If

        End If


    End Sub
    Function RaiseLeaveBalanceAlertLeaveEnter(ByVal objTr As clsMonthAttendanceDetail, ByVal strPayperiod As String, ByVal Line_No As Integer) As Boolean
        If Not (clsCommon.CompairString(alertType, "2") = CompairStringResult.Equal Or clsCommon.CompairString(alertType, "3") = CompairStringResult.Equal) Then
            Return True
        End If
        If objTr Is Nothing Then
            Return True
        End If

        Dim Pay_Period_Code As String = strPayperiod 'findPayperiod.Value
        Dim EMP_Code As String = objTr.empCode 'clsCommon.myCstr(gvMonthlyAttendance.CurrentRow.Cells(colempCode).Value)
        Dim Leave_Code As String = ""
        Dim Balance As Decimal = 0
        Dim CurrentEntered As Decimal = 0 'gvMonthlyAttendance.CurrentCell.Value
        'If gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colEL Then
        Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("EL", Nothing)
        Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
        CurrentEntered = objTr.EarnedLeave
        If CurrentEntered > 0 Then
            If CurrentEntered > Balance Then
                Throw New Exception("Available EL Balance: " & Balance & " " & Environment.NewLine & "Entered EL: " & CurrentEntered & " at line no:" & Line_No & "")
            End If
        End If

        'ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colCL Then
        Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("CL", Nothing)
        Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
        CurrentEntered = objTr.CasualLeave
        If CurrentEntered > 0 Then
            If CurrentEntered > Balance Then
                Throw New Exception("Available CL Balance: " & Balance & " " & Environment.NewLine & "Entered CL: " & CurrentEntered & " at line no:" & Line_No & "")
            End If
        End If

        'ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colCOFF Then
        Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("COFF", Nothing)
        Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
        CurrentEntered = objTr.Coff
        If CurrentEntered > Balance Then
            Throw New Exception("Available COFF Balance: " & Balance & " " & Environment.NewLine & "Entered COFF: " & CurrentEntered & " at line no:" & Line_No & "")
        End If
        'ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colMEDL Then
        Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MED", Nothing)
        Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
        CurrentEntered = objTr.MedicalLeave
        If CurrentEntered > 0 Then
            If CurrentEntered > Balance Then
                Throw New Exception("Available Medical Balance: " & Balance & " " & Environment.NewLine & "Entered Medical: " & CurrentEntered & " at line no:" & Line_No & "")
            End If
        End If

        'ElseIf gvMonthlyAttendance.CurrentCell.ColumnInfo.Name = colMATL Then
        Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("MATRL", Nothing)
        Balance = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, Leave_Code)
        CurrentEntered = objTr.MaternityLeave
        If CurrentEntered > 0 Then
            If CurrentEntered > Balance Then
                Throw New Exception("Available Maternity Balance: " & Balance & " " & Environment.NewLine & "Entered Maternity: " & CurrentEntered & " at line no:" & Line_No & "")
            End If
        End If
        Return True


    End Function

    Private Sub txtBranch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBranch._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " Location_Type='Physical' And LOCATION_CODE='" + LocCode + "'"
            Else
                whrcls = " Location_Type='Physical' "
            End If
        End If
        txtBranch.Value = clsLocation.getFinder(whrcls, Me.txtBranch.Value, isButtonClicked)
        lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
    End Sub

    Private Sub txtGo_Click(sender As Object, e As EventArgs) Handles txtGo.Click
        FillEmployeeGrid()
    End Sub
    Sub FillEmployeeGrid()
        If clsCommon.myLen(txtBranch.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location.", Me.Text)
            txtBranch.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Pay Period.", Me.Text)
            findPayperiod.Focus()
            Exit Sub
        End If

        gvMonthlyAttendance.Rows.Clear()
        Dim strq As String = ""
        '      strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation,convert(date,TT4.Joining_date,103) as DOJ FROM (" _
        '& " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
        '& " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
        '& " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
        '& " WHERE Location_Code='" & txtBranch.Value & "' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
        '& " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
        '& " LEFT JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
        '& " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE where WORKING_STATUS='Working' " _
        '       & " and 2=(case when  TT4.RELIEVING_DATE is null then (case when  len( TT4.Joining_date) <=0 then 3 else (case when convert(date,TT4.Joining_date,103) <='" + lblToDate.Text + "'  then 2 else 3 end) end) else (case when  (convert(date,TT4.RELIEVING_DATE,103) >='" + lblToDate.Text + "'  or convert(date,TT4.RELIEVING_DATE,103) between '" + lblFromDate.Text + "'  and '" + lblToDate.Text + "'  ) then 2 else 3 end) end) "
        strq = EmpQuery()

        Dim cond As String
        'cond = " (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') " _
        cond = " And coalesce(TT3.EMP_CODE,'') NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_MONTHLY_ATTENDANCE_DETAIL T1 " _
               & " JOIN TSPL_monthly_ATTENDANCE T2 ON T1.MTA_CODE=T2.MTA_CODE WHERE T2.PAY_PERIOD_CODE='" & Me.findPayperiod.Value & "')"

        strq = strq & cond
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)
        Dim From_PP As Date
        Dim To_PP As Date
        Dim qry As String = "select DATE_FROM,DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & findPayperiod.Value & "'"
        Dim dtPP As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtPP.Rows.Count > 0 Then
            From_PP = dtPP.Rows(0).Item("DATE_FROM")
            To_PP = dtPP.Rows(0).Item("DATE_TO")
        End If
        For Each drEmp As DataRow In dt.Rows
            gvMonthlyAttendance.Rows.AddNew()
            gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(ColSNo).Value = gvMonthlyAttendance.Rows.Count
            gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colempCode).Value = drEmp.Item("Code")
            gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colempName).Value = drEmp.Item("Name")
            gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colDOJ).Value = clsCommon.GetPrintDate(drEmp.Item("DOJ"), "dd/MMM/yyyy")
            gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colPayPeriodDays).Value = clsCommon.myCdbl(txtPayPeriodDays.Text) - GetNotJoinedDays(From_PP, clsCommon.myCDate(drEmp.Item("DOJ")))
            If dtPP.Rows.Count > 0 Then
                gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colHolidayDays).Value = clsSalaryGeneration.TotalGLHL(From_PP, To_PP, Nothing)
                gvMonthlyAttendance.Rows(gvMonthlyAttendance.Rows.Count - 1).Cells(colWeeklyOffDays).Value = clsSalaryGeneration.TotalWKHL(From_PP, To_PP, Nothing)
            End If

        Next
        updateAttendance()
        LoadBiometricAttendance(False)
    End Sub

    Function EmpQuery() As String
        Dim strq As String = ""
        strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation,convert(date,TT4.Joining_date,103) as DOJ FROM (" _
  & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
  & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
  & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
  & " WHERE Location_Code='" & txtBranch.Value & "' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
  & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
  & " LEFT JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
  & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE where WORKING_STATUS='Working' " _
         & " and 2=(case when  TT4.RELIEVING_DATE is null then (case when  len( TT4.Joining_date) <=0 then 3 else (case when convert(date,TT4.Joining_date,103) <='" + lblToDate.Text + "'  then 2 else 3 end) end) else (case when  (convert(date,TT4.RELIEVING_DATE,103) >='" + lblToDate.Text + "'  or convert(date,TT4.RELIEVING_DATE,103) between '" + lblFromDate.Text + "'  and '" + lblToDate.Text + "'  ) then 2 else 3 end) end) 
         and (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') "
        Return strq
    End Function

    Sub RefreshAttendance()
        Try
            Dim dtgv As DataTable = CType(gvAttendanceSummary.DataSource, DataTable)
            Dim StrEmpCode As String = ""
            Dim AbsentDays1 As Double = 0
            Dim PresentDays1 As Double = 0
            Dim CL1 As Double = 0
            Dim EL1 As Double = 0
            Dim Coff1 As Double = 0
            Dim WeeklyOff1 As Double = 0
            Dim MatL1 As Double = 0
            Dim MedL1 As Double = 0
            Dim Other1 As Double = 0
            Dim Holidays1 As Double = 0
            For irow As Int16 = 0 To gvMonthlyAttendance.Rows.Count - 1
                AbsentDays1 = 0
                PresentDays1 = 0
                CL1 = 0
                EL1 = 0
                Coff1 = 0
                WeeklyOff1 = 0
                MatL1 = 0
                MedL1 = 0
                Other1 = 0
                Holidays1 = 0
                StrEmpCode = clsCommon.myCstr(gvMonthlyAttendance.Rows(irow).Cells(colempCode).Value)
                Dim dr As DataRow() = dtgv.Select("[Employee Code]='" + StrEmpCode + "'")
                If dr IsNot Nothing AndAlso dr.Length > 0 Then
                    Dim dtEmpData As DataTable = dr.CopyToDataTable()

                    For i As Int16 = 3 To dtEmpData.Columns.Count - 1
                        If clsCommon.myLen(CL_Leave_Code) > 0 AndAlso clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), CL_Leave_Code) = CompairStringResult.Equal Then
                            CL1 = CL1 + 1
                        ElseIf clsCommon.myLen(EL_Leave_Code) > 0 AndAlso clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), EL_Leave_Code) = CompairStringResult.Equal Then
                            EL1 = EL1 + 1
                        ElseIf clsCommon.myLen(MED_Leave_Code) > 0 AndAlso clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), MED_Leave_Code) = CompairStringResult.Equal Then
                            MedL1 = MedL1 + 1
                        ElseIf clsCommon.myLen(COFF_Leave_Code) > 0 AndAlso clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), COFF_Leave_Code) = CompairStringResult.Equal Then
                            Coff1 = Coff1 + 1
                        ElseIf clsCommon.myLen(OTHER_Leave_Code) > 0 AndAlso clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), OTHER_Leave_Code) = CompairStringResult.Equal Then
                            Other1 = Other1 + 1
                        ElseIf clsCommon.myLen(MATRL_Leave_Code) > 0 AndAlso clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), MATRL_Leave_Code) = CompairStringResult.Equal Then
                            MatL1 = MatL1 + 1
                        ElseIf clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), "WO") = CompairStringResult.Equal Then
                            WeeklyOff1 = WeeklyOff1 + 1
                        ElseIf clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), "HO") = CompairStringResult.Equal Then
                            Holidays1 = Holidays1 + 1
                        ElseIf clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), "P") = CompairStringResult.Equal Then
                            PresentDays1 = PresentDays1 + 1
                        ElseIf clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), "HD") = CompairStringResult.Equal Then
                            PresentDays1 = PresentDays1 + 0.5
                            AbsentDays1 = AbsentDays1 + 0.5
                        ElseIf clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(dtEmpData.Rows(0).Item(i).ToString(), "") = CompairStringResult.Equal Then
                            AbsentDays1 = AbsentDays1 + 1
                        End If
                    Next
                    gvMonthlyAttendance.Rows(irow).Cells(colCL).Value = CL1
                    gvMonthlyAttendance.Rows(irow).Cells(colEL).Value = EL1
                    gvMonthlyAttendance.Rows(irow).Cells(colCOFF).Value = Coff1
                    gvMonthlyAttendance.Rows(irow).Cells(colMATL).Value = MatL1
                    gvMonthlyAttendance.Rows(irow).Cells(colMEDL).Value = MedL1
                    gvMonthlyAttendance.Rows(irow).Cells(colOther).Value = Other1
                    gvMonthlyAttendance.Rows(irow).Cells(colWeeklyOffDays).Value = WeeklyOff1
                    gvMonthlyAttendance.Rows(irow).Cells(colHolidayDays).Value = Holidays1
                    gvMonthlyAttendance.Rows(irow).Cells(colPresentDays).Value = PresentDays1
                    gvMonthlyAttendance.Rows(irow).Cells(colAbsentDays).Value = AbsentDays1
                End If

                gvMonthlyAttendance.Rows(irow).Cells(colPayDays).Value = clsCommon.myCdbl(gvMonthlyAttendance.Rows(irow).Cells(colPayPeriodDays).Value) - clsCommon.myCdbl(gvMonthlyAttendance.Rows(irow).Cells(colAbsentDays).Value)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub updateAttendance(Optional ByVal row As GridViewRowInfo = Nothing)
        'Dim AbsentDays As Double = 0
        'Dim PresentDays As Double = 0
        'Dim CL As Double = 0
        'Dim EL As Double = 0
        'Dim Coff As Double = 0
        'Dim WeeklyOff As Double = 0
        'Dim MatL As Double = 0
        'Dim MedL As Double = 0
        'Dim Other As Double = 0

        If Not row Is Nothing Then

            'AbsentDays = clsCommon.myCdbl(row.Cells(colAbsentDays).Value)
            'PresentDays = clsCommon.myCdbl(row.Cells(colPresentDays).Value)
            'CL = clsCommon.myCdbl(row.Cells(colCL).Value)
            'EL = clsCommon.myCdbl(row.Cells(colEL).Value)
            'Coff = clsCommon.myCdbl(row.Cells(colCOFF).Value)
            'WeeklyOff = clsCommon.myCdbl(row.Cells(colWeeklyOffDays).Value)
            'MatL = clsCommon.myCdbl(row.Cells(colMATL).Value)
            'MedL = clsCommon.myCdbl(row.Cells(colMEDL).Value)
            'Other = clsCommon.myCdbl(row.Cells(colOther).Value)

            row.Cells(colAbsentDays).Value = row.Cells(colPayPeriodDays).Value - (clsCommon.myCdbl(row.Cells(colPresentDays).Value) _
                   + clsCommon.myCdbl(row.Cells(colHolidayDays).Value) + clsCommon.myCdbl(row.Cells(colWeeklyOffDays).Value) + clsCommon.myCdbl(row.Cells(colEL).Value) + clsCommon.myCdbl(row.Cells(colCL).Value) + clsCommon.myCdbl(row.Cells(colMATL).Value) + clsCommon.myCdbl(row.Cells(colMEDL).Value) + clsCommon.myCdbl(row.Cells(colCOFF).Value) + clsCommon.myCdbl(row.Cells(colOther).Value))

            If clsCommon.myCdbl(row.Cells(colAbsentDays).Value) < 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please check absent days can not be negative", Me.Text)
                row.Cells(colAbsentDays).Value = AbsentDays
                row.Cells(colPresentDays).Value = PresentDays
                row.Cells(colCL).Value = CL
                row.Cells(colEL).Value = EL
                row.Cells(colCOFF).Value = Coff
                row.Cells(colWeeklyOffDays).Value = WeeklyOff
                row.Cells(colHolidayDays).Value = Holidays
                row.Cells(colMATL).Value = MatL
                row.Cells(colMEDL).Value = MedL
                row.Cells(colOther).Value = Other
                Return
            End If

            row.Cells(colPayDays).Value = clsCommon.myCdbl(row.Cells(colPayPeriodDays).Value) - clsCommon.myCdbl(row.Cells(colAbsentDays).Value)
            If row.Cells(colPayPeriodDays).Value < clsCommon.myCdbl(row.Cells(colCL).Value + row.Cells(colEL).Value + row.Cells(colAbsentDays).Value + row.Cells(colPresentDays).Value + row.Cells(colCOFF).Value + row.Cells(colMATL).Value + row.Cells(colMEDL).Value + row.Cells(colOther).Value + row.Cells(colHolidayDays).Value + row.Cells(colWeeklyOffDays).Value) Then
                clsCommon.MyMessageBoxShow(Me, "Please check total of all leaves should not be more than from total days", Me.Text)
                Return
            End If
        Else
            For Each grow As GridViewRowInfo In gvMonthlyAttendance.Rows
                grow.Cells(colAbsentDays).Value = grow.Cells(colPayPeriodDays).Value - (clsCommon.myCdbl(grow.Cells(colPresentDays).Value) _
                    + clsCommon.myCdbl(grow.Cells(colHolidayDays).Value) + clsCommon.myCdbl(grow.Cells(colWeeklyOffDays).Value) + clsCommon.myCdbl(grow.Cells(colEL).Value) + clsCommon.myCdbl(grow.Cells(colCL).Value) + clsCommon.myCdbl(grow.Cells(colMATL).Value) + clsCommon.myCdbl(grow.Cells(colMEDL).Value) + clsCommon.myCdbl(grow.Cells(colCOFF).Value) + clsCommon.myCdbl(grow.Cells(colOther).Value))
            Next
        End If

    End Sub

    Private Sub LoadBiometricAttendance(ByVal LoadMachineData As Boolean)
        Try
            isInsideLoadData = True

            Dim StrTable As String = ""
            Dim strqEmp As String = EmpQuery()
            If LoadMachineData = True Then
                StrTable = "TSPL_BIOMETRIC_RAW_DATA"
            Else
                Dim Strqry As String = "select count(1) as aa from TSPL_BIOMETRIC_RAW_DATA_UPDATED
                                   left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=TSPL_BIOMETRIC_RAW_DATA_UPDATED.Emp_ID
             where Convert(Date, TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,103) >= Convert(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103)  
             And TSPL_EMPLOYEE_MASTER.LOCATION_CODE = '" + txtBranch.Value + "'  "
                Dim RecordCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Strqry))
                If RecordCount > 0 Then
                    StrTable = "TSPL_BIOMETRIC_RAW_DATA_UPDATED"
                Else
                    StrTable = "TSPL_BIOMETRIC_RAW_DATA"
                End If
            End If

            If clsCommon.myLen(strqEmp) <= 0 Then
                Throw New Exception("Load Employee First")
            End If

            Dim strDateColumnForIN As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STUFF(a.strr,1,1,'') from (select (select +',['+Format(thedate,'dd/MMM/yyy')+'(IN)]'  from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr )a"))
            Dim strDateColumnForOUT As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STUFF(a.strr,1,1,'') from ( select (select +',['+Format(thedate,'dd/MMM/yyy')+'(OUT)]'  from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr )a"))
            Dim strDateColumnForIN_OUT As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select +',['+Format(thedate,'dd/MMM/yyy')+'(IN)]' + ',' + '['+Format(thedate,'dd/MMM/yyy')+'(OUT)]' from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr"))
            Dim strDateColumnforIN_OUT_MAX As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select +',max(['+Format(thedate,'dd/MMM/yyy')+'(IN)]) as '+ '['+Format(thedate,'dd/MMM/yyy')+'(IN)]'+',' + 'max(['+Format(thedate,'dd/MMM/yyy')+'(OUT)]) as ' + '['+Format(thedate,'dd/MMM/yyy')+'(OUT)]'  from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr "))
            Dim qry As String = " select [BioMetric Emp ID],[Employee Code],max([Employee Name]) as [Employee Name] " + strDateColumnforIN_OUT_MAX + "  from ( select [BioMetric Emp ID],[Employee Code],[Employee Name] "
            qry += " " + strDateColumnForIN_OUT + " "
            qry += " from ( select TSPL_EMPLOYEE_MASTER.BioMetricEmpID AS [BioMetric Emp ID],TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code] ,max(TSPL_EMPLOYEE_MASTER.Emp_Name) as [Employee Name] "
            qry += " ,Format(" + StrTable + ".In_Out_Date,'dd/MMM/yyy')+'(IN)' as AttendanceDate,Format(" + StrTable + ".In_Out_Date,'dd/MMM/yyy')+'(OUT)' as AttendanceDate2 , min( Format(" + StrTable + ".In_Out_Date,'HH:mm')) as AttendanceTimeIn ,(case when min( Format(" + StrTable + ".In_Out_Date,'HH:mm:ss'))=max( Format(" + StrTable + ".In_Out_Date,'HH:mm:ss')) then null else max( Format(" + StrTable + ".In_Out_Date,'HH:mm')) end) as AttendanceTimeOut "
            qry += " from ( " + strqEmp + " )tt
                     left outer join TSPL_EMPLOYEE_MASTER on tt.Code = TSPL_EMPLOYEE_MASTER.Emp_Code "
            qry += " left outer join " + StrTable + " on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=" + StrTable + ".Emp_ID "
            qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE =  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE "
            qry += " where TSPL_EMPLOYEE_MASTER.LOCATION_CODE = '" + txtBranch.Value + "'  "
            qry += "  and ((CONVERT(Date, " + StrTable + ".In_Out_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, " + StrTable + ".In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103)) or " + StrTable + ".In_Out_Date is null)  "

            qry += " Group by TSPL_EMPLOYEE_MASTER.BioMetricEmpID,TSPL_EMPLOYEE_MASTER.EMP_CODE , Format(" + StrTable + ".In_Out_Date,'dd/MMM/yyy') "
            qry += " ) XXX "
            qry += " PIVOT "
            qry += " ( "
            qry += " min(AttendanceTimeIn) "
            qry += " FOR AttendanceDate IN ( "
            qry += " " + strDateColumnForIN + " "
            qry += " ) "
            qry += " ) AS PivotTable "
            qry += " PIVOT "
            qry += " ( "
            qry += " max(AttendanceTimeOut) "
            qry += " FOR AttendanceDate2 IN ( "
            qry += " " + strDateColumnForOUT + ""
            qry += " )) AS PivotTable2 ) PPPP group by [BioMetric Emp ID],[Employee Code] order by [Employee Code] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvAttendanceDetail.DataSource = Nothing
                gvAttendanceDetail.Rows.Clear()
                gvAttendanceDetail.Columns.Clear()
                gvAttendanceDetail.DataSource = dt
                gvAttendanceDetail.GroupDescriptors.Clear()
                gvAttendanceDetail.MasterTemplate.SummaryRowsBottom.Clear()
                gvAttendanceDetail.EnableGrouping = False
                gvAttendanceDetail.EnableFiltering = True
                gvAttendanceDetail.MasterTemplate.AllowAddNewRow = False
                For ii As Integer = 0 To gvAttendanceDetail.Columns.Count - 1
                    gvAttendanceDetail.Columns(ii).BestFit()
                Next
                gvAttendanceDetail.BestFitColumns()
                gvAttendanceDetail.Columns("BioMetric Emp ID").ReadOnly = True
                gvAttendanceDetail.Columns("Employee Code").ReadOnly = True
                gvAttendanceDetail.Columns("Employee Name").ReadOnly = True
            End If

            If LoadMachineData = True Then
                gvAttendanceSummary.DataSource = Nothing
                Exit Sub
            End If

            'Summary
            Dim strDateColumn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STUFF(a.strr,1,1,'') from (select (select +',['+Format(thedate,'dd/MMM/yyy')+']'  from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr )a"))
            Dim strDateColumnforMAX As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select +',max(['+Format(thedate,'dd/MMM/yyy')+']) as '+ '['+Format(thedate,'dd/MMM/yyy')+']' from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr "))
            qry = " select BioMetricEmpID as [BioMetric Emp ID],Emp_Code as [Employee Code],max([Emp_Name]) as [Employee Name]"
            qry += " " + strDateColumnforMAX + " "

            qry += " from(
                    SELECT TSPL_EMPLOYEE_MASTER.BioMetricEmpID,
                    TSPL_EMPLOYEE_MASTER.Emp_Code,TSPL_EMPLOYEE_MASTER.Emp_Name,Format(DateTable.thedate,'dd/MMM/yyy') as Attendance_Date
                    ,TempBioMetric.Attendance_Status as AttendanceStatus
                    FROM ( " + strqEmp + " )tt
                    left outer join TSPL_EMPLOYEE_MASTER on tt.Code = TSPL_EMPLOYEE_MASTER.Emp_Code
                    inner JOIN (select * from 
                    dbo.ExplodeDates( CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103), CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103))
                    )DateTable ON  1=1 AND Emp_Status='Active'
                    LEFT JOIN
                    ( select TSPL_EMPLOYEE_MASTER.BioMetricEmpID AS [BioMetric Emp ID],TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code] 
                    ,Format(TSPL_MONTHLY_ATTENDANCE_SUMMARY.Attendance_Date,'dd/MMM/yyy') as AttendanceDate
                    ,TSPL_MONTHLY_ATTENDANCE_SUMMARY.Attendance_Status
                    from TSPL_EMPLOYEE_MASTER 
                    left outer join TSPL_MONTHLY_ATTENDANCE_SUMMARY on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=TSPL_MONTHLY_ATTENDANCE_SUMMARY.Emp_ID                     
					where TSPL_EMPLOYEE_MASTER.LOCATION_CODE = '" + txtBranch.Value + "'  and 
                    (CONVERT(Date, TSPL_MONTHLY_ATTENDANCE_SUMMARY.Attendance_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103) AND CONVERT(Date,TSPL_MONTHLY_ATTENDANCE_SUMMARY.Attendance_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103)) 
                    Group by TSPL_EMPLOYEE_MASTER.BioMetricEmpID,TSPL_EMPLOYEE_MASTER.EMP_CODE,TSPL_MONTHLY_ATTENDANCE_SUMMARY.Attendance_Status 
					, Format(TSPL_MONTHLY_ATTENDANCE_SUMMARY.Attendance_Date,'dd/MMM/yyy')  
                    )TempBioMetric ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TempBioMetric.[Employee Code] and DateTable.thedate=TempBioMetric.AttendanceDate "

            qry += " ) XXX  PIVOT  ( MAX(AttendanceStatus)  FOR Attendance_Date IN ( "
            qry += " " + strDateColumn + ""
            qry += " ) )as PivotTable group by BioMetricEmpID,Emp_Code order by [Emp_Code] "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvAttendanceSummary.DataSource = Nothing
                gvAttendanceSummary.Rows.Clear()
                gvAttendanceSummary.Columns.Clear()
                gvAttendanceSummary.DataSource = dt
                gvAttendanceSummary.GroupDescriptors.Clear()
                gvAttendanceSummary.MasterTemplate.SummaryRowsBottom.Clear()
                gvAttendanceSummary.EnableGrouping = False
                gvAttendanceSummary.EnableFiltering = True
                gvAttendanceSummary.MasterTemplate.AllowAddNewRow = False

                For ii As Integer = 0 To gvAttendanceSummary.Columns.Count - 1
                    gvAttendanceSummary.Columns(ii).BestFit()
                Next

                gvAttendanceSummary.BestFitColumns()
                gvAttendanceSummary.Columns("BioMetric Emp ID").ReadOnly = True
                gvAttendanceSummary.Columns("Employee Code").ReadOnly = True
                gvAttendanceSummary.Columns("Employee Name").ReadOnly = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    ''against[BM00000008104]
    Private Sub gvMonthlyAttendance_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvMonthlyAttendance.CurrentColumnChanged
        If gvMonthlyAttendance.RowCount > 0 Then
            Dim intCurrRow As Integer = gvMonthlyAttendance.CurrentRow.Index
            If intCurrRow = gvMonthlyAttendance.Rows.Count - 1 Then
                gvMonthlyAttendance.Rows.AddNew()
                gvMonthlyAttendance.CurrentRow = gvMonthlyAttendance.Rows(intCurrRow)

            End If
        End If
    End Sub
    Function GetNotJoinedDays(ByVal From_date As Date, ByVal DOJ As Date) As Integer
        Dim NJ As Integer = 0
        If From_date.Month = DOJ.Month AndAlso From_date.Year = DOJ.Year Then
            NJ = DateDiff(DateInterval.Day, From_date.Date, DOJ.Date)
        End If
        Return NJ
    End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            For ii As Integer = 0 To gvMonthlyAttendance.Rows.Count - 1
                Dim qry As String = "select  In_Out_Date from (" + Environment.NewLine +
                "select convert(date, In_Out_Date,103) as In_Out_Date from TSPL_BIOMETRIC_RAW_DATA where Emp_ID in (select BioMetricEmpID from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + clsCommon.myCstr(gvMonthlyAttendance.Rows(ii).Cells(colempCode).Value) + "') " + Environment.NewLine +
                "and convert(date, In_Out_Date,103)>='" + lblFromDate.Text + "'" + Environment.NewLine +
                "and convert(date, In_Out_Date,103)<='" + lblToDate.Text + "'" + Environment.NewLine +
                ")x group by In_Out_Date"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvMonthlyAttendance.Rows(ii).Cells(colPresentDays).Value = dt.Rows.Count
                    updateAttendance(gvMonthlyAttendance.Rows(ii))
                End If
                dt = Nothing
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub Export_Grid_Click(sender As Object, e As EventArgs) Handles Export_Grid.Click
        Try

            Dim strq As String = ""
            Dim cond As String = ""

            If clsCommon.myLen(txtBranch.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location.", Me.Text)
                txtBranch.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(findPayperiod.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Pay Period.", Me.Text)
                findPayperiod.Focus()
                Exit Sub
            End If

            strq = "select ROW_NUMBER ()over (order by cc.EMP_CODE) as SNo, '" & findPayperiod.Value & "' as [Payperiod Code],cc.EMP_CODE as [Employee Code],cc.Emp_Name as [Employee Name],cc.PF_NO as [PF No]" _
            & "," & txtPayPeriodDays.Text & "- case when month('" + lblFromDate.Text + "') = month(cc.DOJ) and year('" + lblFromDate.Text + "') = year(cc.DOJ) then DATEDIFF(DAY, '" + lblFromDate.Text + "', cc.DOJ) else 0 end as [Total Days]" _
            & ", 0 AS [Pay Days],0 as [Present Days]," _
            & " 0 as [Absent Days],0 as [Earned Leave],0 as [Casual Leave],0 as [Maternity Leave],0 as [Medical Leave],0 as [Coff],0 as [Other Leave],0 as [Holidays],0 as [Weekly Off Days],'' as [Entered By],'' as [Entered By Name]"
            strq += " from ( SELECT distinct  TT1.EMP_CODE,TT4.Emp_Name,TT4.PF_NO ,TT4.Designation,convert(date,TT4.Joining_date,103) as DOJ " &
                     " FROM (" _
                    & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
                    & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
                    & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
                    & " WHERE Location_Code='" & txtBranch.Value & "' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
                    & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
                    & " LEFT JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
                    & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE where WORKING_STATUS='Working' " _
                        & " and 2=(case when  TT4.RELIEVING_DATE is null then (case when  len( TT4.Joining_date) <=0 then 3 else (case when convert(date,TT4.Joining_date,103) <='" + lblToDate.Text + "'  then 2 else 3 end) end) else (case when  (convert(date,TT4.RELIEVING_DATE,103) >='" + lblToDate.Text + "'  or convert(date,TT4.RELIEVING_DATE,103) between '" + lblFromDate.Text + "'  and '" + lblToDate.Text + "'  ) then 2 else 3 end) end) "

            cond = " (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') " _
                   & " AND coalesce(TT3.EMP_CODE,'') NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_MONTHLY_ATTENDANCE_DETAIL T1 " _
                   & " JOIN TSPL_monthly_ATTENDANCE T2 ON T1.MTA_CODE=T2.MTA_CODE WHERE T2.PAY_PERIOD_CODE='" & Me.findPayperiod.Value & "')"
            strq = strq & " and " & cond & " )cc "

            transportSql.ExporttoExcel(strq, "", Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Save_Layout_Click(sender As Object, e As EventArgs) Handles Save_Layout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvMonthlyAttendance.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvMonthlyAttendance.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvMonthlyAttendance.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub Delete_Layout_Click(sender As Object, e As EventArgs) Handles Delete_Layout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvMonthlyAttendance.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvMonthlyAttendance.Columns.Count - 1 Step ii + 1
                        gvMonthlyAttendance.Columns(ii).IsVisible = False
                        gvMonthlyAttendance.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvMonthlyAttendance.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnUpdateTime_Click(sender As Object, e As EventArgs) Handles btnUpdateTime.Click
        Try
            Dim obj As New clsBiometricAttendance()
            obj.FromDate = clsCommon.myCDate(lblFromDate.Text)
            obj.ToDate = clsCommon.myCDate(lblToDate.Text)
            obj.LocationCode = txtBranch.Value

            Dim objgenset As New clsBiometricAttendanceDetail
            obj.arr = New List(Of clsBiometricAttendanceDetail)
            Dim objgensetSummary As New clsBiometricAttendanceSummary
            obj.arrSummary = New List(Of clsBiometricAttendanceSummary)
            Dim TempDate As String
            Dim TempTime As String
            'Dim TempDateTime As DateTime?
            For irow As Integer = 0 To gvAttendanceDetail.Rows.Count - 1
                For icol As Integer = 3 To gvAttendanceDetail.Columns.Count - 1

                    If clsCommon.myLen(gvAttendanceDetail.Rows(irow).Cells(icol).Value) > 0 Then
                        objgenset = New clsBiometricAttendanceDetail
                        objgenset.Machine_Sr_No = ""
                        objgenset.Emp_ID = clsCommon.myCstr(gvAttendanceDetail.Rows(irow).Cells("BioMetric Emp ID").Value)
                        TempDate = clsCommon.myCstr(gvAttendanceDetail.Columns(icol).HeaderText.Substring(0, 11))
                        TempTime = clsCommon.myCstr(gvAttendanceDetail.Rows(irow).Cells(icol).Value)
                        objgenset.In_Out_Date = clsCommon.myCDate(TempDate + " " + TempTime)
                        If clsCommon.myLen(objgenset.Emp_ID) > 0 Then
                            obj.arr.Add(objgenset)
                        End If
                    End If

                Next
            Next

            For irow As Integer = 0 To gvAttendanceSummary.Rows.Count - 1
                For icol As Integer = 3 To gvAttendanceSummary.Columns.Count - 1
                    ' If clsCommon.myLen(gvAttendanceSummary.Rows(irow).Cells(icol).Value) > 0 Then
                    If clsCommon.myLen(gvAttendanceSummary.Rows(irow).Cells("BioMetric Emp ID").Value) > 0 Then
                        objgensetSummary = New clsBiometricAttendanceSummary
                        objgensetSummary.Emp_ID = clsCommon.myCstr(gvAttendanceSummary.Rows(irow).Cells("BioMetric Emp ID").Value)
                        TempDate = clsCommon.myCstr(gvAttendanceSummary.Columns(icol).HeaderText.Substring(0, 11))
                        objgensetSummary.Attendance_Date = clsCommon.myCDate(TempDate)
                        objgensetSummary.Attendance_Status = IIf(clsCommon.myLen(gvAttendanceSummary.Rows(irow).Cells(icol).Value) > 0, clsCommon.myCstr(gvAttendanceSummary.Rows(irow).Cells(icol).Value), "A")
                        'If clsCommon.myLen(objgensetSummary.Emp_ID) > 0 Then
                        obj.arrSummary.Add(objgensetSummary)
                        'End If

                    End If

                Next
            Next


            If clsBiometricAttendance.SaveData(obj) Then
                RadMessageBox.Show(Me, "Biometric Detail/Summary Data Save Successfully.")
            Else
                Throw New Exception("Error in Biometric Detail/Summary Data Save.")
            End If
        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message)
        End Try

    End Sub

    Private Sub btnAttendanceDetailExport_Click(sender As Object, e As EventArgs) Handles btnAttendanceDetailExport.Click
        Try
            RefreshBiometricAttendanceSummary()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExcelAttendanceDetail_Click(sender As Object, e As EventArgs) Handles ExcelAttendanceDetail.Click
        Try
            transportSql.QuickExportToExcel(gvAttendanceDetail, "", "Monthly Attendance Detail")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExcelAttendanceSummary_Click(sender As Object, e As EventArgs) Handles ExcelAttendanceSummary.Click
        Try
            transportSql.QuickExportToExcel(gvAttendanceSummary, "", "Monthly Attendance Summary")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RefreshBiometricAttendanceSummary()
        Try
            'Dim StrTable As String = ""
            'Dim Strqry As String = "select count(1) as aa from TSPL_BIOMETRIC_RAW_DATA_UPDATED
            '                       left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=TSPL_BIOMETRIC_RAW_DATA_UPDATED.Emp_ID
            ' where Convert(Date, TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,103) >= Convert(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103)  
            ' And TSPL_EMPLOYEE_MASTER.LOCATION_CODE = '" + txtBranch.Value + "'  "
            'Dim RecordCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Strqry))
            'If RecordCount > 0 Then
            '    StrTable = "TSPL_BIOMETRIC_RAW_DATA_UPDATED"
            'Else
            '    StrTable = "TSPL_BIOMETRIC_RAW_DATA"
            'End If
            Dim strqEmp As String = EmpQuery()
            Dim strDateColumn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STUFF(a.strr,1,1,'') from (select (select +',['+Format(thedate,'dd/MMM/yyy')+']'  from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr )a"))
            Dim strDateColumnforMAX As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select +',max(['+Format(thedate,'dd/MMM/yyy')+']) as '+ '['+Format(thedate,'dd/MMM/yyy')+']' from ExplodeDates('" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "') for xml path ('')) as strr "))
            Dim qry As String = " select BioMetricEmpID as [BioMetric Emp ID],Emp_Code as [Employee Code],max([Emp_Name]) as [Employee Name]"
            qry += " " + strDateColumnforMAX + " "

            qry += " from(
                    SELECT TSPL_EMPLOYEE_MASTER.BioMetricEmpID,
                    TSPL_EMPLOYEE_MASTER.Emp_Code,TSPL_EMPLOYEE_MASTER.Emp_Name,Format(DateTable.thedate,'dd/MMM/yyy') as Attendance_Date
                    ,case when dbo.IsWeeklyOff(convert(varchar(20),DateTable.thedate),TSPL_EMPLOYEE_MASTER.Emp_Code)=1 then 'WO' 
                    when TempHoliday.HOLIDAY_DATE is not null then 'HO'
                    when TempLEAVE.LEAVE_CODE is not null then TempLEAVE.LEAVE_CODE
                    WHEN DATEDIFF(MINUTE,TempBioMetric.AttendanceTimeIn,TempBioMetric.AttendanceTimeOut) >=TS.FDAY THEN 'P'
                    WHEN DATEDIFF(MINUTE,TempBioMetric.AttendanceTimeIn,TempBioMetric.AttendanceTimeOut) >=TS.HFDAY THEN 'HD'
                    ELSE 'A' END AttendanceStatus
                    FROM ( " + strqEmp + " )tt
                    left outer join TSPL_EMPLOYEE_MASTER on tt.Code = TSPL_EMPLOYEE_MASTER.Emp_Code
                    inner JOIN (select * from 
                    dbo.ExplodeDates( CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103), CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103))
                    )DateTable ON  1=1 AND Emp_Status='Active'
                    LEFT JOIN
                    ( select TSPL_EMPLOYEE_MASTER.BioMetricEmpID AS [BioMetric Emp ID],TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code] 
                    ,Format(TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,'dd/MMM/yyy') as AttendanceDate
                    , min( Format(TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,'dd/MMM/yyyy HH:mm')) as AttendanceTimeIn 
                    ,(case when min( Format(TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,'HH:mm:ss'))=max( Format(TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,'HH:mm:ss')) then null else max( Format(TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,'dd/MMM/yyyy HH:mm')) end) as AttendanceTimeOut  
                    from TSPL_EMPLOYEE_MASTER 
                    left outer join TSPL_BIOMETRIC_RAW_DATA_UPDATED on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=TSPL_BIOMETRIC_RAW_DATA_UPDATED.Emp_ID  left outer join TSPL_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE =  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  where TSPL_EMPLOYEE_MASTER.LOCATION_CODE = '" + txtBranch.Value + "'  and 
                    (CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103)) 
                      Group by TSPL_EMPLOYEE_MASTER.BioMetricEmpID,TSPL_EMPLOYEE_MASTER.EMP_CODE , Format(TSPL_BIOMETRIC_RAW_DATA_UPDATED.In_Out_Date,'dd/MMM/yyy')  
                      )TempBioMetric
                       ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TempBioMetric.[Employee Code] and DateTable.thedate=TempBioMetric.AttendanceDate
                    inner join
                    (SELECT MIN_HOURS_HFDAY*60 as HFDAY,WORKING_HOURS*60+INTERVAL_MINUTES as FDAY
                     FROM TSPL_PAYROLL_SETTING)TS ON 1=1
                    LEFT OUTER JOIN (SELECT DateTable.thedate,EMP_CODE,TSPL_LEAVE_MASTER.LEAVE_CODE FROM TSPL_LEAVE_APPLICATION
                    LEFT JOIN TSPL_LEAVE_MASTER ON TSPL_LEAVE_MASTER.LEAVE_CODE=TSPL_LEAVE_APPLICATION.LEAVE_CODE
                    left outer JOIN
                     (select * from 
                    dbo.ExplodeDates( CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103), CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103))
                    )DateTable ON  DateTable.thedate>=TSPL_LEAVE_APPLICATION.FROM_DATE
                    and  DateTable.thedate<=TSPL_LEAVE_APPLICATION.TO_DATE
                    where  convert(DATE,TSPL_LEAVE_APPLICATION.FROM_DATE,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(lblFromDate.Text, "dd/MMM/yyyy") + "',103)
                    and   convert(DATE,TSPL_LEAVE_APPLICATION.TO_DATE,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(lblToDate.Text, "dd/MMM/yyyy") + "',103)
                    and TSPL_LEAVE_APPLICATION.posted=1
                    )TempLEAVE ON TempLEAVE.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE and TempLEAVE.thedate=DateTable.thedate
                    LEFT OUTER JOIN (SELECT TSPL_GENERAL_HOLIDAYS.HOLIDAY_DATE FROM TSPL_GENERAL_HOLIDAYS where TSPL_GENERAL_HOLIDAYS.Location_Code='" + txtBranch.Value + "'  
                    )TempHoliday ON TempHoliday.HOLIDAY_DATE=DateTable.thedate "

            qry += " ) XXX  PIVOT  ( MAX(AttendanceStatus)  FOR Attendance_Date IN ( "
            qry += " " + strDateColumn + ""
            qry += " ) )as PivotTable group by BioMetricEmpID,Emp_Code order by [Employee Name] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvAttendanceSummary.DataSource = Nothing
                gvAttendanceSummary.Rows.Clear()
                gvAttendanceSummary.Columns.Clear()
                gvAttendanceSummary.DataSource = dt
                gvAttendanceSummary.GroupDescriptors.Clear()
                gvAttendanceSummary.MasterTemplate.SummaryRowsBottom.Clear()
                gvAttendanceSummary.EnableGrouping = False
                gvAttendanceSummary.EnableFiltering = True
                gvAttendanceSummary.MasterTemplate.AllowAddNewRow = False
                gvAttendanceSummary.BestFitColumns()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnFillAttendance_Click(sender As Object, e As EventArgs) Handles btnFillAttendance.Click
        RefreshAttendance()
    End Sub

    Private Sub gvAttendanceSummary_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gvAttendanceSummary.CurrentRowChanged
        If gvAttendanceSummary.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gvAttendanceSummary.CurrentRow.Cells("Employee Code").Value)) > 0 Then
            setBalance()
        End If
    End Sub
    Private Sub setBalance()
        Try
            Dim Pay_Period_Code As String = findPayperiod.Value
            Dim EMP_Code As String = clsCommon.myCstr(gvAttendanceSummary.CurrentRow.Cells("Employee Code").Value)
            GroupBox1.Text = clsEmployeeMaster.GetName(EMP_Code, Nothing) + " (" + EMP_Code + ")"
            If clsCommon.myLen(EL_Leave_Code) > 0 Then
                lblELBal.Text = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, EL_Leave_Code)
            End If
            If clsCommon.myLen(CL_Leave_Code) > 0 Then
                lblCLBal.Text = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, CL_Leave_Code)
            End If
            If clsCommon.myLen(COFF_Leave_Code) > 0 Then
                lblCOFFBal.Text = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, COFF_Leave_Code)
            End If
            If clsCommon.myLen(MATRL_Leave_Code) > 0 Then
                lblMATRLBal.Text = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, MATRL_Leave_Code)
            End If
            If clsCommon.myLen(MED_Leave_Code) > 0 Then
                lblMEDBal.Text = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, MED_Leave_Code)
            End If
            If clsCommon.myLen(OTHER_Leave_Code) > 0 Then
                lblOTHERBal.Text = clsMonthAttendance.GetLeaveBalance(Pay_Period_Code, EMP_Code, OTHER_Leave_Code)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDeleteDS_Click(sender As Object, e As EventArgs) Handles btnDeleteDS.Click
        Try
            If clsCommon.myLen(txtBranch.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Location", Me.Text)
                txtBranch.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(findPayperiod.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Payperiod", Me.Text)
                findPayperiod.Focus()
                Exit Sub
            End If
            If (myMessages.deleteConfirm()) Then
                If (clsBiometricAttendance.DeleteData(txtBranch.Value, findPayperiod.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Biometric Detail/Summary Data Deleted Successfully ", Me.Text)
                    GroupBox1.Text = ""
                    lblELBal.Text = "0.0"
                    lblCLBal.Text = "0.0"
                    lblCOFFBal.Text = "0.0"
                    lblMATRLBal.Text = "0.0"
                    lblMEDBal.Text = "0.0"
                    lblOTHERBal.Text = "0.0"
                    gvAttendanceDetail.DataSource = Nothing
                    gvAttendanceSummary.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub gvAttendanceSummary_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAttendanceSummary.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpenSummary Then
                    isCellValueChangedOpenSummary = True
                    If e.Column.Index > 2 AndAlso clsCommon.myLen(gvAttendanceSummary.Columns("BioMetric Emp ID")) > 0 Then
                        Dim ExistingVal As String = clsCommon.myCstr(gvAttendanceSummary.CurrentRow.Cells(e.Column.Index).Value)
                        gvAttendanceSummary.CurrentRow.Cells(e.Column.Index).Value = AttendanceCodeSelection(ExistingVal)

                        'If clsCommon.myLen(gvAttendanceSummary.CurrentRow.Cells(e.Column.Index).Value) <= 0 Then
                        '    common.clsCommon.MyMessageBoxShow(Me, "Set Attendance")
                        '    gvAttendanceSummary.CurrentRow.Cells(e.Column.Index).IsSelected = True
                        'End If

                    End If
                    isCellValueChangedOpenSummary = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function AttendanceCodeSelection(ByVal curcode As String)
        Dim str As String = ""
        Dim qry As String = "select xx.Code from (" + QryAttendanceCodeSelection + ")xx"
        'str = clsCommon.ShowSelectForm("RPTTXGRPFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        str = clsCommon.ShowSelectForm("AttCode", qry, "Code", "", curcode)
        Return str
    End Function

    Private Sub btnBiometric_Click(sender As Object, e As EventArgs) Handles btnBiometric.Click
        Try
            LoadBiometricAttendance(True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class