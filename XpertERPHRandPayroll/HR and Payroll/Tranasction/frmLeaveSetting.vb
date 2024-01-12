'--24/06/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLeaveSetting
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim formLoading As Boolean = False
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    '' salary slab grid cols
    Const colLINE_NO As String = "colLINE_NO"
    Const colSALARY_FROM As String = "colSALARY_FROM"
    Const colSALARY_TO As String = "colSALARY_TO"
    Const colALLOTED_LEAVE As String = "colALLOTED_LEAVE"

#End Region
    Sub LoadSalarySlabColumns()
        gvCTC.Rows.Clear()
        gvCTC.Columns.Clear()

        Dim LINE_NO As New GridViewTextBoxColumn
        Dim SALARY_FROM As New GridViewDecimalColumn
        Dim SALARY_TO As New GridViewTextBoxColumn
        Dim ALLOTED_LEAVE As New GridViewTextBoxColumn

        LINE_NO.FormatString = ""
        LINE_NO.HeaderText = "Line No"
        LINE_NO.Name = colLINE_NO
        LINE_NO.Width = 100
        LINE_NO.ReadOnly = True
        gvCTC.Columns.Add(LINE_NO)

        SALARY_FROM.FormatString = ""
        SALARY_FROM.HeaderText = "Salary From"
        SALARY_FROM.Name = colSALARY_FROM
        SALARY_FROM.Width = 100
        SALARY_FROM.ReadOnly = False
        gvCTC.Columns.Add(SALARY_FROM)

        SALARY_TO.FormatString = ""
        SALARY_TO.HeaderText = "Salary To"
        SALARY_TO.Name = colSALARY_TO
        SALARY_TO.Width = 100
        SALARY_TO.ReadOnly = False
        gvCTC.Columns.Add(SALARY_TO)

        ALLOTED_LEAVE.FormatString = ""
        ALLOTED_LEAVE.HeaderText = "Alloted Leave"
        ALLOTED_LEAVE.Name = colALLOTED_LEAVE
        ALLOTED_LEAVE.Width = 100
        ALLOTED_LEAVE.ReadOnly = False
        gvCTC.Columns.Add(ALLOTED_LEAVE)

    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsLeaveSetting()
            obj.Code = txtCode.Value
            obj.Location_Code = fndLocation.Value
            If rdbJoiningDate.IsChecked Then
                obj.LEAVE_ALLOT_TYPE = 1
            ElseIf rdbconfirmDate.IsChecked Then
                obj.LEAVE_ALLOT_TYPE = 2
            ElseIf rdbppcompDate.IsChecked Then
                obj.LEAVE_ALLOT_TYPE = 3
            ElseIf rdbAllotafter.IsChecked Then
                obj.LEAVE_ALLOT_TYPE = 4
                obj.ALLOT_AFTER_MONTHS = txtmonths.Value
                obj.ALLOT_AFTER_DAYS = txtdays.Value
            Else
                obj.LEAVE_ALLOT_TYPE = 0
            End If

            If rdbAvailJoinDate.IsChecked Then
                obj.LEAVE_AVAIL_TYPE = 1
            ElseIf rdbAvailConfirmDate.IsChecked Then
                obj.LEAVE_AVAIL_TYPE = 2
            ElseIf rdbAvailPPCompDate.IsChecked Then
                obj.LEAVE_AVAIL_TYPE = 3
            ElseIf rdbAvailAfter.IsChecked Then
                obj.LEAVE_AVAIL_TYPE = 4
                obj.AVAIL_AFTER_MONTHS = txtAvailMonths.Value
                obj.AVAIL_AFTER_DAYS = txtAvailDays.Value
            Else
                obj.LEAVE_AVAIL_TYPE = 0
            End If

            If chkCarryForward.Checked Then
                obj.CARRY_OVER = 1
                obj.CARRY_LOWER_LIM = txtCFLower.Value
                obj.CARRY_UPPER_LIM = txtCFUpper.Value
            Else
                obj.CARRY_OVER = 0
            End If

            If chkLapseUnAvailed.Checked Then
                obj.LAPSE_UNAVAILED = 1
                obj.LAPSE_MONTH = cboMonth.SelectedValue

                If chkLapseNegativeLeaves.Checked Then
                    obj.LAPSE_NEGATIVE = 1
                    obj.LAPSE_EXCEEDING = txtExceeding.Value
                Else
                    obj.LAPSE_NEGATIVE = 0
                End If
            Else
                obj.LAPSE_UNAVAILED = 0
            End If

            If chkLeaveEncash.Checked Then
                obj.LEAVE_ENCASHED = 1
                obj.BAL_ROUND_OFF_TYPE = cboRoundoffType.SelectedValue
                obj.MIN_BAL = txtminleavebal.Value
            Else
                obj.LEAVE_ENCASHED = 0
            End If
            '' new colums
            obj.Allot_Periodicity = clsCommon.myCstr(cboPeriodicity.SelectedValue)
            obj.Allot_Type = clsCommon.myCstr(cboAllotType.SelectedValue)
            obj.Alloted_Days = clsCommon.myCdbl(txtAllotedDays.Text)
            obj.PerPresentDays = clsCommon.myCdbl(txtPresentDays.Text)
            If chkAutoAllot.Checked Then
                obj.AutoAllotDuringSalaryGen = 1
            Else
                obj.AutoAllotDuringSalaryGen = 0
            End If
            Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_SETTING where LEAVE_CODE= '" & obj.Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                isNewEntry = True
            Else
                isNewEntry = False
            End If
            Dim objTr As clsLeaveSettingSalSlabLeaveAlloted

            For Each grow As GridViewRowInfo In gvCTC.Rows
                If clsCommon.myCdbl(grow.Cells(colALLOTED_LEAVE).Value) > 0 Then
                    objTr = New clsLeaveSettingSalSlabLeaveAlloted
                    objTr.LINE_NO = grow.Cells(colLINE_NO).Value
                    objTr.SALARY_FROM = grow.Cells(colSALARY_FROM).Value
                    objTr.SALARY_TO = grow.Cells(colSALARY_TO).Value
                    objTr.ALLOTED_LEAVE = grow.Cells(colALLOTED_LEAVE).Value
                    obj.objList.Add(objTr)
                End If
            Next
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ''txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsLeaveSetting()
        obj = clsLeaveSetting.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.Code
            txtName.Text = obj.Name
            If clsCommon.myLen(obj.Location_Code) > 0 Then
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            Else
                fndLocation.Value = ""
                lblLocationName.Text = ""
            End If
            If obj.LEAVE_ALLOT_TYPE = 1 Then
                rdbJoiningDate.IsChecked = True
            ElseIf obj.LEAVE_ALLOT_TYPE = 2 Then
                rdbconfirmDate.IsChecked = True
            ElseIf obj.LEAVE_ALLOT_TYPE = 3 Then
                rdbppcompDate.IsChecked = True
            ElseIf obj.LEAVE_ALLOT_TYPE = 4 Then
                rdbAllotafter.IsChecked = True
                txtmonths.Value = obj.ALLOT_AFTER_MONTHS
                txtdays.Value = obj.ALLOT_AFTER_DAYS
            Else
                rdbJoiningDate.IsChecked = False
                rdbconfirmDate.IsChecked = False
                rdbppcompDate.IsChecked = False
                rdbAllotafter.IsChecked = False
                txtmonths.Value = Nothing
                txtdays.Value = Nothing
            End If

            If obj.LEAVE_AVAIL_TYPE = 1 Then
                rdbAvailJoinDate.IsChecked = True
            ElseIf obj.LEAVE_AVAIL_TYPE = 2 Then
                rdbAvailConfirmDate.IsChecked = True
            ElseIf obj.LEAVE_AVAIL_TYPE = 3 Then
                rdbAvailPPCompDate.IsChecked = True
            ElseIf obj.LEAVE_AVAIL_TYPE = 4 Then
                rdbAvailAfter.IsChecked = True
                txtAvailMonths.Value = obj.AVAIL_AFTER_MONTHS
                txtAvailDays.Value = obj.AVAIL_AFTER_DAYS
            Else
                rdbAvailJoinDate.IsChecked = False
                rdbAvailConfirmDate.IsChecked = False
                rdbAvailPPCompDate.IsChecked = False
                rdbAvailAfter.IsChecked = False
                txtAvailMonths.Value = Nothing
                txtAvailDays.Value = Nothing
            End If

            If obj.CARRY_OVER = 1 Then
                chkCarryForward.Checked = True
                txtCFLower.Value = obj.CARRY_LOWER_LIM
                txtCFUpper.Value = obj.CARRY_UPPER_LIM
            Else
                chkCarryForward.Checked = False
                txtCFLower.Value = Nothing
                txtCFUpper.Value = Nothing
            End If

            If obj.LAPSE_UNAVAILED = 1 Then
                chkLapseUnAvailed.Checked = True
                cboMonth.SelectedValue = obj.LAPSE_MONTH

                If obj.LAPSE_NEGATIVE = 1 Then
                    chkLapseNegativeLeaves.Checked = True
                    txtExceeding.Value = obj.LAPSE_EXCEEDING
                Else
                    chkLapseNegativeLeaves.Checked = False
                    txtExceeding.Value = Nothing
                End If
            Else
                chkLapseUnAvailed.Checked = False
                cboMonth.SelectedIndex = -1
                chkLapseNegativeLeaves.Checked = False
                txtExceeding.Value = Nothing
            End If

            If obj.LEAVE_ENCASHED = 1 Then
                chkLeaveEncash.Checked = True
                txtminleavebal.Value = obj.MIN_BAL
                cboRoundoffType.SelectedValue = obj.BAL_ROUND_OFF_TYPE

            Else
                chkLeaveEncash.Checked = False
                cboRoundoffType.SelectedIndex = -1
                txtminleavebal.Value = Nothing
            End If
            If clsCommon.myLen(obj.Allot_Periodicity) > 0 Then
                cboPeriodicity.SelectedValue = obj.Allot_Periodicity
            End If
            If clsCommon.myLen(obj.Allot_Type) > 0 Then
                cboAllotType.SelectedValue = obj.Allot_Type
            End If
            txtAllotedDays.Text = obj.Alloted_Days
            txtPresentDays.Text = obj.PerPresentDays
            If obj.AutoAllotDuringSalaryGen = 1 Then
                chkAutoAllot.Checked = True
            Else
                chkAutoAllot.Checked = False
            End If
            '' load salary slab grid
            gvCTC.Rows.Clear()
            For Each objTr As clsLeaveSettingSalSlabLeaveAlloted In obj.objList
                gvCTC.Rows.AddNew()
                gvCTC.Rows(gvCTC.Rows.Count - 1).Cells(colLINE_NO).Value = objTr.LINE_NO
                gvCTC.Rows(gvCTC.Rows.Count - 1).Cells(colSALARY_FROM).Value = objTr.SALARY_FROM
                gvCTC.Rows(gvCTC.Rows.Count - 1).Cells(colSALARY_TO).Value = objTr.SALARY_TO
                gvCTC.Rows(gvCTC.Rows.Count - 1).Cells(colALLOTED_LEAVE).Value = objTr.ALLOTED_LEAVE
            Next
            gvCTC.Rows.AddNew()
        End If

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(" Code ")
            txtCode.Focus()
            Return False
        ElseIf Not (rdbJoiningDate.IsChecked Or rdbconfirmDate.IsChecked Or rdbppcompDate.IsChecked Or rdbAllotafter.IsChecked) Then
            clsCommon.MyMessageBoxShow(Me, "Choose any one from Leave Allotment Setting. ")
            txtName.Focus()
            Return False
        ElseIf rdbAllotafter.IsChecked And (clsCommon.myLen(txtmonths.Text) <= 0 Or clsCommon.myLen(txtdays.Text) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Enter Month and Day on selection of Allot After.")
            txtmonths.Focus()
            Return False
        ElseIf Not (rdbAvailJoinDate.IsChecked Or rdbAvailConfirmDate.IsChecked Or rdbAvailPPCompDate.IsChecked Or rdbAvailAfter.IsChecked) Then
            clsCommon.MyMessageBoxShow(Me, "Choose any one from Leave Avail Setting.", Me.Text)
            txtName.Focus()
            Return False
        ElseIf rdbAllotafter.IsChecked And (clsCommon.myLen(txtAvailMonths.Text) <= 0 Or clsCommon.myLen(txtAvailDays.Text) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Enter Month and Day on selection of Avail After.", Me.Text)
            txtmonths.Focus()
            Return False
        ElseIf chkCarryForward.Checked And (clsCommon.myLen(txtCFLower.Text) <= 0 Or clsCommon.myLen(txtCFUpper.Text) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Carry Forward is Checked But Upper Limit or Lower Limit or both are Blank.", Me.Text)
            chkCarryForward.Focus()
            Return False
        ElseIf txtCFLower.Value > 999.99 Then
            clsCommon.MyMessageBoxShow(Me, "Lower Limit Can be MAX 999.99.", Me.Text)
            txtCFLower.Focus()
            Return False
        ElseIf txtCFUpper.Value > 999.99 Then
            clsCommon.MyMessageBoxShow(Me, "Upper Limit Can be MAX 999.99.", Me.Text)
            txtCFUpper.Focus()
            Return False
        ElseIf chkLapseUnAvailed.Checked And cboMonth.SelectedIndex < 0 Then
            clsCommon.MyMessageBoxShow(Me, " Lapse Unavailed On is Checked But Lapse in Month is Blank.", Me.Text)
            cboMonth.Focus()
            Return False
        ElseIf chkLapseNegativeLeaves.Checked And clsCommon.myLen(txtExceeding.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Lapse Negative Leaves is Checked But Lapse Exceeding is Blank.", Me.Text)
            cboMonth.Focus()
            Return False
        ElseIf txtExceeding.Value > 999.99 Then
            clsCommon.MyMessageBoxShow(Me, "Lapse Exceeding Can be MAX 999.99.", Me.Text)
            txtExceeding.Focus()
            Return False
        ElseIf chkLeaveEncash.Checked And (clsCommon.myLen(txtminleavebal.Text) <= 0 Or cboRoundoffType.SelectedIndex < 0) Then
            clsCommon.MyMessageBoxShow("Leave Encashment is Checked But Minimum Balance or Balance Round off Type or both are Blank.", Me.Text)
            chkCarryForward.Focus()
            Return False
        ElseIf txtminleavebal.Value > 999.99 Then
            clsCommon.MyMessageBoxShow(Me, "Minimum Balance Can be MAX 999.99.", Me.Text)
            txtminleavebal.Focus()
            Return False
        ElseIf clsCommon.myLen(cboPeriodicity.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Allotment Periodicity.", Me.Text)
            cboPeriodicity.Focus()
            Return False
        ElseIf clsCommon.myLen(cboAllotType.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Allotment Type.", Me.Text)
            cboAllotType.Focus()
            Return False
        End If
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
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select LEAVE_CODE  from TSPL_SHIPMENT_DETAILS  where LEAVE_CODE ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If
        '' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsLeaveSetting.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmLeaveSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        formLoading = True
        SetUserMgmtNew()
        isNewEntry = True
        LoadSalarySlabColumns()
        funReset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        formLoading = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveSetting)
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
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""

        rdbJoiningDate.IsChecked = False
        rdbconfirmDate.IsChecked = False
        rdbppcompDate.IsChecked = False
        rdbAllotafter.IsChecked = False
        txtmonths.Value = Nothing
        txtdays.Value = Nothing

        rdbAvailJoinDate.IsChecked = False
        rdbAvailConfirmDate.IsChecked = False
        rdbAvailPPCompDate.IsChecked = False
        rdbAvailAfter.IsChecked = False
        txtAvailMonths.Value = Nothing
        txtAvailDays.Value = Nothing

        chkCarryForward.Checked = False
        txtCFLower.Value = Nothing
        txtCFUpper.Value = Nothing

        chkLapseUnAvailed.Checked = False
        cboMonth.DataSource = GetcboMonthDataTable()
        cboMonth.ValueMember = "Code"
        cboMonth.DisplayMember = "Name"
        cboMonth.SelectedIndex = -1

        cboPeriodicity.DataSource = GetAllotPeriodicity()
        cboPeriodicity.ValueMember = "Code"
        cboPeriodicity.DisplayMember = "Name"
        cboPeriodicity.SelectedIndex = -1

        cboAllotType.DataSource = GetAllotType()
        cboAllotType.ValueMember = "Code"
        cboAllotType.DisplayMember = "Name"
        cboAllotType.SelectedIndex = -1

        chkLapseNegativeLeaves.Checked = False
        txtExceeding.Value = Nothing

        chkLeaveEncash.Checked = False
        txtminleavebal.Value = Nothing
        cboRoundoffType.DataSource = GetcboRoundOfTypeDataTable()
        cboRoundoffType.ValueMember = "Code"
        cboRoundoffType.DisplayMember = "Name"
        cboRoundoffType.SelectedIndex = -1
        txtAllotedDays.Text = 0
        txtPresentDays.Text = 0

        chkCarryForward_Validated(Nothing, Nothing)
        chkLapseUnAvailed_Validated(Nothing, Nothing)
        chkLeaveEncash_Validated(Nothing, Nothing)
        rdbAllot_Clicked(Nothing, Nothing)
        rdbAvail_Clicked(Nothing, Nothing)
        chkLapseNegativeLeaves_Validated(Nothing, Nothing)
        gvCTC.Rows.Clear()
        gvCTC.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " TSPL_LEAVE_SETTING.LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "Select count(*) from TSPL_LEAVE_MASTER where LEAVE_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = " select TSPL_LEAVE_MASTER.LEAVE_CODE AS Code, TSPL_LEAVE_MASTER.LEAVE_NAME as Name,TSPL_LEAVE_MASTER.PRINT_NAME as 'Print Name', TSPL_LEAVE_MASTER.AFFECTS_SALARY as 'Is Affects Salary',TSPL_LEAVE_SETTING.Location_code As 'Location Code'  from TSPL_LEAVE_MASTER Left outer Join TSPL_LEAVE_SETTING ON TSPL_LEAVE_SETTING.LEAVE_CODE=TSPL_LEAVE_MASTER.LEAVE_CODE"
            txtCode.Value = clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", whrcls, txtCode.Value, "TSPL_LEAVE_MASTER.LEAVE_CODE", isButtonClicked)
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

    Private Sub frmLeaveSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub cboMonth_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Selected_val As String = String.Empty

        If cboMonth.SelectedIndex > -1 Then
            Selected_val = cboMonth.SelectedValue
        End If

        cboMonth.DataSource = GetcboMonthDataTable()
        cboMonth.ValueMember = "Code"
        cboMonth.DisplayMember = "Name"

        If Selected_val.Length > 0 Then
            cboMonth.SelectedValue = Selected_val
        Else
            cboMonth.SelectedIndex = -1

        End If

    End Sub

    Public Shared Function GetcboMonthDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "January"
        DR("Code") = "January"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "February"
        DR("Code") = "February"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "March"
        DR("Code") = "March"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "April"
        DR("Code") = "April"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "May"
        DR("Code") = "May"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "June"
        DR("Code") = "June"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "July"
        DR("Code") = "July"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "August"
        DR("Code") = "August"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "September"
        DR("Code") = "September"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "October"
        DR("Code") = "October"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "November"
        DR("Code") = "November"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "December"
        DR("Code") = "December"
        DT.Rows.Add(DR)

        DT.AcceptChanges()
        Return DT
    End Function

    Public Shared Function GetcboRoundOfTypeDataTable() As DataTable
        Dim DT_AttReg As DataTable = New DataTable
        DT_AttReg.Columns.Add("Code", GetType(String))
        DT_AttReg.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_AttReg.NewRow()
        DR("Name") = "Lower Half Day"
        DR("Code") = "Lower_Half_Day"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Higher Half Day"
        DR("Code") = "Higher_Half_Day"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Lower Full Day"
        DR("Code") = "Lower_Full_Day"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Higher Full Day"
        DR("Code") = "Higher_Full_Day"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Nearest Half Day"
        DR("Code") = "Nearest_Half_Day"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Nearest Full Day"
        DR("Code") = "Nearest_Full_Day"
        DT_AttReg.Rows.Add(DR)

        DT_AttReg.AcceptChanges()

        Return DT_AttReg
    End Function
    Public Shared Function GetAllotPeriodicity() As DataTable
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = dt.NewRow()
        DR("Name") = "Monthly"
        DR("Code") = "M"
        dt.Rows.Add(DR)

        DR = dt.NewRow()
        DR("Name") = "Yearly"
        DR("Code") = "Y"
        dt.Rows.Add(DR)
        dt.AcceptChanges()
        Return dt
    End Function
    Public Shared Function GetAllotType() As DataTable
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = dt.NewRow()
        DR("Name") = "Fixed"
        DR("Code") = "F"
        dt.Rows.Add(DR)

        DR = dt.NewRow()
        DR("Name") = "Attendance Based"
        DR("Code") = "Attn"
        dt.Rows.Add(DR)

        DR = dt.NewRow()
        DR("Name") = "Salary Slab"
        DR("Code") = "Salary"
        dt.Rows.Add(DR)

        dt.AcceptChanges()
        Return dt
    End Function

    Private Sub chkCarryForward_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCarryForward.ToggleStateChanged
        If chkCarryForward.Checked Then
            txtCFLower.Enabled = True
            txtCFUpper.Enabled = True
        Else
            txtCFLower.Enabled = False
            txtCFUpper.Enabled = False
        End If
    End Sub

    Private Sub chkLapseUnAvailed_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLapseUnAvailed.ToggleStateChanged
        If chkLapseUnAvailed.Checked Then
            cboMonth.Enabled = True
            chkLapseNegativeLeaves.Enabled = True

        Else
            cboMonth.Enabled = False
            chkLapseNegativeLeaves.Enabled = False
            chkLapseNegativeLeaves.Checked = False
        End If


    End Sub

    Private Sub chkLeaveEncash_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLeaveEncash.ToggleStateChanged
        If chkLeaveEncash.Checked Then
            txtminleavebal.Enabled = True
            cboRoundoffType.Enabled = True
        Else
            txtminleavebal.Enabled = False
            cboRoundoffType.Enabled = False
        End If

    End Sub

    Private Sub rdbAllot_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllotafter.ToggleStateChanged, rdbconfirmDate.ToggleStateChanged, rdbJoiningDate.ToggleStateChanged, rdbppcompDate.ToggleStateChanged

        If rdbAllotafter.IsChecked Then
            txtmonths.Enabled = True
            txtdays.Enabled = True
        Else
            txtmonths.Enabled = False
            txtdays.Enabled = False
        End If

    End Sub

    Private Sub rdbAvail_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAvailAfter.ToggleStateChanged, rdbAvailConfirmDate.ToggleStateChanged, rdbAvailJoinDate.ToggleStateChanged, rdbAvailPPCompDate.ToggleStateChanged
        If rdbAvailAfter.IsChecked Then
            txtAvailMonths.Enabled = True
            txtAvailDays.Enabled = True
        Else
            txtAvailMonths.Enabled = False
            txtAvailDays.Enabled = False
        End If

    End Sub

    Private Sub chkLapseNegativeLeaves_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLapseNegativeLeaves.ToggleStateChanged
        If chkLapseNegativeLeaves.Checked Then
            txtExceeding.Enabled = True
        Else
            txtExceeding.Enabled = False
        End If

    End Sub

    Private Sub cboRoundoffType_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoundoffType.Enter
        Dim Selected_val As String = String.Empty

        If cboRoundoffType.SelectedIndex > -1 Then
            Selected_val = cboRoundoffType.SelectedValue
        End If

        cboRoundoffType.DataSource = GetcboRoundOfTypeDataTable()
        cboRoundoffType.ValueMember = "Code"
        cboRoundoffType.DisplayMember = "Name"

        If Selected_val.Length > 0 Then
            cboRoundoffType.SelectedValue = Selected_val
        Else
            cboRoundoffType.SelectedIndex = -1
        End If

    End Sub

    Private Sub gvCTC_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvCTC.CurrentColumnChanged
        If gvCTC.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCTC.CurrentRow.Index
            If intCurrRow = gvCTC.Rows.Count - 1 Then
                gvCTC.Rows.AddNew()
                gvCTC.CurrentRow = gvCTC.Rows(intCurrRow)
                gvCTC.CurrentRow.Cells(colLINE_NO).Value = (intCurrRow + 1)
            End If
        End If
    End Sub



    Private Sub cboAllotType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboAllotType.SelectedValueChanged
        If formLoading = False Then
            If clsCommon.CompairString(cboAllotType.SelectedValue, "F") = CompairStringResult.Equal Then
                txtAllotedDays.Enabled = True
                txtPresentDays.Enabled = False
                txtPresentDays.Text = 0
                gvCTC.Enabled = False
            ElseIf clsCommon.CompairString(cboAllotType.SelectedValue, "Attn") = CompairStringResult.Equal Then
                txtAllotedDays.Enabled = True
                txtPresentDays.Enabled = True
                gvCTC.Enabled = False
            ElseIf clsCommon.CompairString(cboAllotType.SelectedValue, "Salary") = CompairStringResult.Equal Then
                txtAllotedDays.Enabled = False
                txtPresentDays.Enabled = False
                txtPresentDays.Text = 0
                gvCTC.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkLapseUnAvailed_Validated(sender As Object, args As StateChangedEventArgs) Handles chkLapseUnAvailed.ToggleStateChanged

    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            Dim Qry As String = "select Location_Code As [Location Code],Location_Desc As [Description] from TSPL_LOCATION_MASTER "
            fndLocation.Value = clsLocation.getFinder(whrcls, Me.fndLocation.Value, isButtonClicked)
            ''fndLocation.Value = clsCommon.ShowSelectForm("SalaryLocation", Qry, "Location_Code", whrcls, "", "Location_Code", isButtonClicked)
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
