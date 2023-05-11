Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmEmployee_Status
    Inherits FrmMainTranScreen

#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colWKHOLIDAYSelect As String = "colWKHOLIDAYSelect"
    Const colWKHOLIDAY_CODE As String = "colWKHOLIDAY_CODE"
    Const colWeeklyHolidayDesc As String = "colWeeklyHolidayDesc"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub frmLeaveMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        LoadWeekOffColumns()
        LoadPFCalculationType()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadWeekOffColumns()
        gvLoanGeneration.Rows.Clear()
        gvLoanGeneration.Columns.Clear()

        Dim WKHOLIDAYSelect As New GridViewCheckBoxColumn
        Dim WKHOLIDAY_CODE As New GridViewTextBoxColumn
        Dim WKHOLIDAY_Desc As New GridViewTextBoxColumn

        WKHOLIDAYSelect.FormatString = ""
        WKHOLIDAYSelect.HeaderText = "Select"
        WKHOLIDAYSelect.Name = colWKHOLIDAYSelect
        WKHOLIDAYSelect.Width = 100
        WKHOLIDAYSelect.ReadOnly = False
        gvLoanGeneration.Columns.Add(WKHOLIDAYSelect)

        WKHOLIDAY_CODE.FormatString = ""
        WKHOLIDAY_CODE.HeaderText = "Weekly Off Code"
        WKHOLIDAY_CODE.Name = colWKHOLIDAY_CODE
        WKHOLIDAY_CODE.Width = 100
        WKHOLIDAY_CODE.ReadOnly = True
        gvLoanGeneration.Columns.Add(WKHOLIDAY_CODE)

        WKHOLIDAY_Desc.FormatString = ""
        WKHOLIDAY_Desc.HeaderText = "Descrition"
        WKHOLIDAY_Desc.Name = colWeeklyHolidayDesc
        WKHOLIDAY_Desc.Width = 100
        WKHOLIDAY_Desc.ReadOnly = True
        gvLoanGeneration.Columns.Add(WKHOLIDAY_Desc)


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsEmployeeStatus()
            obj.Code = txtCode.Value
            obj.EMP_CODE = clsCommon.myCstr(Me.txtEmpCode.Value)
            obj.APPLICABLE_FROM = Format(Me.dtpApplicableFrom.Value, "dd MMM yyyy")
            obj.DESIGNATION_ID = clsCommon.myCstr(finddesignation.Value)
            obj.DEPARTMENT_CODE = clsCommon.myCstr(findDepartment.Value)
            obj.WORKING_STATUS = clsCommon.myCstr(cboWorkingStatus.Text)
            obj.REPORTING_PERSON = clsCommon.myCstr(findEmployee.Value)
            obj.BANK_ACC_NO = clsCommon.myCstr(txtaccno.Text)
            obj.PAYMENT_MODE = clsCommon.myCstr(Me.cboPaymentMode.Text)
            obj.LOCATION_CODE = clsCommon.myCstr(findBranch.Value)
            obj.DEVISION_CODE = clsCommon.myCstr(findDevision.Value)
            obj.GRADE_CODE = clsCommon.myCstr(findGrade.Value)
            obj.ATTENDANCE_CODE = clsCommon.myCstr(findAttendance.Value)
            obj.NAME_IN_ACC = clsCommon.myCstr(txtNameinAccount.Text)
            obj.BANK_CODE = clsCommon.myCstr(findBank.Value)
            obj.PF_NO = clsCommon.myCstr(txtPfNo.Text)
            obj.ESI_NO = clsCommon.myCstr(txtEsiNo.Text)
            obj.OT_CODE = clsCommon.myCstr(findOT.Value)
            obj.BONUS_CODE = clsCommon.myCstr(findBonus.Value)
            obj.REVISION_NO = clsCommon.myCdbl(txtRevisionNo.Text)
            obj.PF_APPLICABLE = Me.chkPFApplicable.Checked
            obj.ESI_APPLICABLE = Me.chkEsiApplicable.Checked
            obj.OT_APPLICABLE = Me.chkOtApplicable.Checked
            obj.Professional_Tax_Applicable = Me.chkProfessionalTaxApplicable.Checked
            obj.BONUS_APPLICABLE = Me.chkBonusApplicable.Checked
            obj.EPS_TO_EPF = Me.chkEPStoEPF.Checked
            obj.SHIFT_CODE = Me.fndShift.Value
            obj.SHIFT_CHANG_TYPE = cboShiftChangeType.SelectedValue
            obj.CONV_TYPE = cboConveyanceType.SelectedValue
            obj.CONV_RATE_CODE = fndConveyanceRate.Value
            obj.IS_OD_APPL = chkODApplicable.Checked
            obj.Max_Amount_EPF = txtEPFMaxLimit.Text
            obj.Max_Amount_ESI = txtESIMaxLim.Text
            obj.EPF_Rate = txtEPFRate.Text
            obj.ESI_Rate = txtESIRate.Text
            obj.Pf_Calculation_Type = cboPFCalculatnType.SelectedValue
            Dim objTr As clsEmployeeStatusWeeklyOff
            For Each grow As GridViewRowInfo In gvLoanGeneration.Rows
                If grow.Cells(colWKHOLIDAYSelect).Value = True Then
                    objTr = New clsEmployeeStatusWeeklyOff
                    objTr.EMP_STATUS_CODE = Me.txtCode.Value
                    objTr.EMP_CODE = Me.txtEmpCode.Value
                    objTr.WKHOLIDAY_CODE = grow.Cells(colWKHOLIDAY_CODE).Value
                    obj.objList.Add(objTr)
                End If
            Next

            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Code, NavigatorType.Current)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsEmployeeStatus()
        obj = clsEmployeeStatus.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            Me.txtCode.Value = obj.Code
            Me.txtEmpCode.Value = obj.EMP_CODE
            Me.lblEmpName.Text = obj.EMP_NAME
            Me.dtpApplicableFrom.Value = obj.APPLICABLE_FROM
            Me.finddesignation.Value = obj.DESIGNATION_ID
            Me.lblDesignationName.Text = obj.DESIGNATION_NAME
            Me.findDepartment.Value = obj.DEPARTMENT_CODE
            Me.cboWorkingStatus.SelectedValue = obj.WORKING_STATUS
            Me.lblDepartmentName.Text = obj.DEPARTMENT_NAME
            Me.findEmployee.Value = obj.REPORTING_PERSON
            Me.findBank.Value = obj.BANK_ACC_NO
            Me.cboPaymentMode.Text = obj.PAYMENT_MODE
            Me.findBranch.Value = obj.LOCATION_CODE
            Me.lblBranchName.Text = obj.LOCATION_DESC
            Me.findDevision.Value = obj.DEVISION_CODE
            Me.lblDevisionName.Text = obj.DEVISION_NAME
            Me.findGrade.Value = obj.GRADE_CODE
            Me.lblGradeName.Text = obj.GRADE_NAME
            Me.findAttendance.Value = obj.ATTENDANCE_CODE
            Me.lblAttendanceName.Text = obj.ATTENDANCE_NAME
            Me.txtaccno.Text = obj.NAME_IN_ACC
            Me.findBank.Value = obj.BANK_CODE
            Me.lblBankName.Text = obj.BANK_NAME
            Me.txtPfNo.Text = obj.PF_NO
            Me.txtEsiNo.Text = obj.ESI_NO
            Me.findOT.Value = obj.OT_CODE
            Me.findBonus.Value = obj.BONUS_CODE
            Me.txtRevisionNo.Text = obj.REVISION_NO
            Me.chkPFApplicable.Checked = clsCommon.myCBool(obj.PF_APPLICABLE)
            Me.chkEsiApplicable.Checked = clsCommon.myCBool(obj.ESI_APPLICABLE)
            Me.chkOtApplicable.Checked = clsCommon.myCBool(obj.OT_APPLICABLE)
            chkProfessionalTaxApplicable.Checked = obj.Professional_Tax_Applicable
            Me.chkBonusApplicable.Checked = clsCommon.myCBool(obj.BONUS_APPLICABLE)
            Me.chkEPStoEPF.Checked = clsCommon.myCBool(obj.EPS_TO_EPF)
            Me.fndShift.Value = obj.SHIFT_CODE
            cboShiftChangeType.SelectedValue = obj.SHIFT_CHANG_TYPE
            cboConveyanceType.SelectedValue = obj.CONV_TYPE
            fndConveyanceRate.Value = obj.CONV_RATE_CODE
            chkODApplicable.Checked = obj.IS_OD_APPL
            txtEPFMaxLimit.Text = clsCommon.myCdbl(obj.Max_Amount_EPF)
            txtESIMaxLim.Text = clsCommon.myCdbl(obj.Max_Amount_ESI)
            txtEPFRate.Text = clsCommon.myCdbl(obj.EPF_Rate)
            txtESIRate.Text = clsCommon.myCdbl(obj.ESI_Rate)
            cboPFCalculatnType.SelectedValue = obj.Pf_Calculation_Type
            Dim objTr As clsEmployeeStatusWeeklyOff
            For Each objTr In obj.objList
                For Each grow As GridViewRowInfo In gvLoanGeneration.Rows
                    If objTr.WKHOLIDAY_CODE = grow.Cells(colWKHOLIDAY_CODE).Value Then
                        grow.Cells(colWKHOLIDAYSelect).Value = True
                    End If
                Next
            Next
            txtCode.MyReadOnly = True
            txtEmpCode.Enabled = False
        End If
    End Sub

    Sub LoadGrid(ByVal gv As RadGridView)
        gv.Rows.Clear()
        Dim objList As List(Of clsWeeklyHolidays)
        objList = clsWeeklyHolidays.GetWeeklyHolidayList(Nothing)
        For Each objTr As clsWeeklyHolidays In objList
            gv.Rows.AddNew()
            gv.Rows(gv.Rows.Count - 1).Cells(colWKHOLIDAYSelect).Value = False
            gv.Rows(gv.Rows.Count - 1).Cells(colWKHOLIDAY_CODE).Value = objTr.WKHOLIDAY_CODE
            gv.Rows(gv.Rows.Count - 1).Cells(colWeeklyHolidayDesc).Value = objTr.WKHOLIDAY_NAME
        Next
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(finddesignation.Value) <= 0 Then
            myMessages.blankValue("Designation ")
            finddesignation.Focus()
            Return False
        ElseIf clsCommon.myLen(findDepartment.Value) <= -1 Then
            myMessages.blankValue("Departmrnt ")
            findDepartment.Focus()
            Return False
        ElseIf cboWorkingStatus.SelectedIndex = -1 Then
            myMessages.blankValue("Working Status ")
            cboWorkingStatus.Focus()
            Return False
        ElseIf clsCommon.myLen(txtRevisionNo.Text) <= -1 Then
            myMessages.blankValue("Revision No ")
            findDepartment.Focus()
            Return False
        ElseIf clsCommon.myLen(txtPfNo.Text) <= 0 And chkPFApplicable.Checked Then
            myMessages.blankValue("PF No ")
            txtPfNo.Focus()
            Return False

        ElseIf clsCommon.myLen(txtEsiNo.Text) <= 0 And chkEsiApplicable.Checked Then
            myMessages.blankValue("ESI No ")
            txtEsiNo.Focus()
            Return False

        ElseIf clsCommon.myLen(findOT.Value) <= 0 And chkOtApplicable.Checked Then
            myMessages.blankValue("OT Code ")
            findOT.Focus()
            Return False

        ElseIf clsCommon.myLen(findBonus.Value) <= 0 And chkBonusApplicable.Checked = True Then
            myMessages.blankValue("Bonus Code ")
            findBonus.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsEmployeeStatus.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtEmpCode.Enabled = True
        txtCode.Value = Nothing
        txtCode.Focus()
        LoadGrid(gvLoanGeneration)
        Me.txtEmpCode.Value = Nothing
        Me.lblEmpName.Text = ""
        Me.dtpApplicableFrom.Value = Today
        Me.finddesignation.Value = Nothing
        Me.lblDesignationName.Text = ""
        Me.findDepartment.Value = Nothing
        Me.lblDepartmentName.Text = ""
        Me.findEmployee.Value = Nothing
        Me.findBank.Value = Nothing
        Me.cboPaymentMode.Text = ""
        Me.findBranch.Value = Nothing
        Me.lblBranchName.Text = ""
        Me.findDevision.Value = Nothing
        Me.lblDevisionName.Text = ""
        Me.findGrade.Value = Nothing
        Me.lblGradeName.Text = ""
        Me.findAttendance.Value = Nothing
        Me.lblAttendanceName.Text = ""
        Me.txtaccno.Text = ""
        Me.findBank.Value = Nothing
        Me.lblBankName.Text = ""
        Me.txtPfNo.Text = ""
        Me.txtEsiNo.Text = ""
        Me.findOT.Value = Nothing
        Me.findBonus.Value = Nothing
        Me.txtRevisionNo.Text = ""
        Me.chkPFApplicable.Checked = False
        Me.chkEsiApplicable.Checked = False
        Me.chkOtApplicable.Checked = False
        Me.chkBonusApplicable.Checked = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        txtEPFMaxLimit.ReadOnly = False
        txtESIMaxLim.ReadOnly = False
        txtEPFRate.ReadOnly = False
        txtESIRate.ReadOnly = False
        txtEPFMaxLimit.Text = 0
        txtESIMaxLim.Text = 0
        txtEPFRate.Text = 0
        txtESIRate.Text = 0
        Me.dtpApplicableFrom.Value = clsCommon.GETSERVERDATE
        cboPFCalculatnType.SelectedValue = "N"
        cboPFCalculatnType.Visible = False
        txtEPFRate.Enabled = False
        txtEPFMaxLimit.Enabled = False
        fndShift.Value = Nothing
        fndConveyanceRate.Value = Nothing
        cboConveyanceType.DataSource = clsConveyanceRateMaster.GetCboConvTypeDataTable()
        cboConveyanceType.ValueMember = "Code"
        cboConveyanceType.DisplayMember = "Name"
        cboConveyanceType.SelectedValue = "None"
        cboShiftChangeType.DataSource = clsEmployeeStatus.GetCboShiftChangeTypeDataTable()
        cboShiftChangeType.ValueMember = "Code"
        cboShiftChangeType.DisplayMember = "Name"
        cboShiftChangeType.SelectedValue = "Never"
        Me.chkODApplicable.Checked = False
        chkRevisionNo.Checked = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
        'Dim x As Decimal = 12.234
        'Dim a As Decimal = clsERPFuncationality.myFloor(x, 1)
        'Dim b As Decimal = clsERPFuncationality.myFloor(x, 2)
        'Dim c As Decimal = clsERPFuncationality.myFloor(x, 3)
        'x = 12.234
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_EMPLOYEE_STATUS where EMP_STATUS_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            txtEmpCode.Enabled = True
        Else
            txtCode.MyReadOnly = True
            txtEmpCode.Enabled = False
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            If chkRevisionNo.Checked = True Then
                qry = "select * from (select max(T1.EMP_STATUS_CODE) AS Code,T1.EMP_CODE,max(T2.EMP_NAME) AS EMPLOYEE_NAME,max(T1.APPLICABLE_FROM) as APPLICABLE_FROM  from TSPL_EMPLOYEE_STATUS T1  "
                qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE"
                qry += " group by t1.EMP_CODE)as final "
            Else
                qry = "select T1.EMP_STATUS_CODE AS Code,T1.EMP_CODE,T2.EMP_NAME AS EMPLOYEE_NAME,T1.APPLICABLE_FROM  from TSPL_EMPLOYEE_STATUS T1 " _
           & " LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE"
            End If
            txtCode.Value = clsCommon.ShowSelectForm("EMP_STATUS", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmLeaveMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        txtEmpCode.Value = clsEmployeeMaster.getFinder("", txtEmpCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtEmpCode.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
        If Not clsemp Is Nothing Then
            lblEmpName.Text = clsemp.Emp_Name
        End If
        If clsCommon.myLen(txtEmpCode.Value) > 0 Then
            LoadExistingStatus(txtEmpCode.Value)
        End If
        If isNewEntry = True Then
            Try
                Me.txtRevisionNo.Text = clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no),0)+1) AS revision_no from TSPL_EMPLOYEE_STATUS where EMP_CODE='" & Me.txtEmpCode.Value & "'").Rows(0).Item("revision_no")
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub finddesignation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles finddesignation._MYValidating
        Dim qry As String = "SELECT DESIGNATION_ID as Code,DESIGNATION_DESC as Name FROM TSPL_DESIGNATION_MASTER "
        finddesignation.Value = clsCommon.ShowSelectForm("TSPL_DESIGNATION_MASTER", qry, "Code", "", finddesignation.Value, "", isButtonClicked)
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Select("Code='" & finddesignation.Value & "'").Length > 0 Then
            lblDesignationName.Text = dt.Select("Code='" & finddesignation.Value & "'")(0).Item("Name")
        End If
    End Sub

    Private Sub findDepartment__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findDepartment._MYValidating
        Try
            Dim qry As String = "SELECT DEPARTMENT_CODE as Code,DEPARTMENT_NAME as Name FROM TSPL_DEPARTMENT_MASTER "
            findDepartment.Value = clsCommon.ShowSelectForm("TSPL_DEPARTMENT_MASTER", qry, "Code", "", findDepartment.Value, "", isButtonClicked)
            Dim clsemp As New clsDepartmentMaster
            clsemp = clsDepartmentMaster.GetData(findDepartment.Value, isButtonClicked)
            lblDepartmentName.Text = clsemp.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findEmployee__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findEmployee._MYValidating
        Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        findEmployee.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findEmployee.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(findEmployee.Value, Nothing)
        lblReportingPersonName.Text = clsemp.Emp_Name
    End Sub

    Sub LoadExistingStatus(ByVal EMP_Code As String)
        Dim CheckSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveMultipleEmployeeStatus, clsFixedParameterCode.AllowToSaveMultipleEmployeeStatus, Nothing))
        If clsCommon.CompairString(CheckSetting, "1") <> CompairStringResult.Equal Then
            Dim Emp_Status_Code As String = clsEmployeeStatus.GetEmployeeLatestStatus(EMP_Code)
            If clsCommon.myLen(Emp_Status_Code) > 0 Then
                LoadData(Emp_Status_Code, NavigatorType.Current)
            End If
        End If
    End Sub

    Private Sub findBranch__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findBranch._MYValidating
        Try
            findBranch.Value = clsLocation.getFinder("Location_Type='Physical'", Me.findBranch.Value, isButtonClicked)
            lblBranchName.Text = clsLocation.GetName(findBranch.Value, Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub findDevision__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findDevision._MYValidating
        Try
            Dim qry As String = "SELECT DEVISION_CODE as Code,DEVISION_NAME as Name FROM TSPL_DEVISION_MASTER "
            findDevision.Value = clsCommon.ShowSelectForm("TSPL_DEVISION_MASTER", qry, "Code", "", findDevision.Value, "", isButtonClicked)
            Dim clsemp As clsDevisionMaster
            clsemp = clsDevisionMaster.GetData(findDevision.Value, Nothing)
            lblDevisionName.Text = clsemp.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findGrade__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findGrade._MYValidating
        Try
            Dim qry As String = "SELECT GRADE_CODE as Code,GRADE_NAME as Name FROM TSPL_GRADE_MASTER "
            findGrade.Value = clsCommon.ShowSelectForm("TSPL_GRADE_MASTER", qry, "Code", "", findGrade.Value, "", isButtonClicked)
            Dim clsemp As clsGradeMaster
            clsemp = clsGradeMaster.GetData(findGrade.Value, Nothing)
            lblGradeName.Text = clsemp.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findAttendance__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findAttendance._MYValidating
        Try
            Dim qry As String = "SELECT ATTENDANCE_CODE as Code,ATTENDANCE_NAME as Name FROM TSPL_ATTENDANCE_MASTER "
            findAttendance.Value = clsCommon.ShowSelectForm("TSPL_ATTENDANCE_MASTER", qry, "Code", "", findAttendance.Value, "", isButtonClicked)
            Dim clsemp As clsAttendanceMaster
            clsemp = clsAttendanceMaster.GetData(findAttendance.Value, Nothing)
            lblAttendanceName.Text = clsemp.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findBank__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findBank._MYValidating
        Try
            Dim qry As String = "SELECT BANK_CODE as Code,BANK_NAME as Name FROM TSPL_BANK_MASTER "
            findBank.Value = clsCommon.ShowSelectForm("TSPL_BANK_MASTER", qry, "Code", "", findBank.Value, "", isButtonClicked)

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                lblBankName.Text = dt.Rows(0).Item("name")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findOT__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findOT._MYValidating
        Try
            Dim qry As String = "SELECT OT_CODE as Code,OT_NAME as Name FROM TSPL_OT_MASTER "
            findOT.Value = clsCommon.ShowSelectForm("TSPL_OT_MASTER", qry, "Code", "", findOT.Value, "", isButtonClicked)
            Dim clsemp As clsOTMaster
            clsemp = clsOTMaster.GetData(findOT.Value, Nothing)
            lblOTName.Text = clsemp.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findBonus__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findBonus._MYValidating
        Try
            Dim qry As String = "SELECT BONUS_CODE as Code,BONUS_NAME as Name FROM TSPL_BONUS_MASTER "
            findBonus.Value = clsCommon.ShowSelectForm("TSPL_BONUS_MASTER", qry, "Code", "", findBonus.Value, "", isButtonClicked)
            Dim clsemp As clsBonusMaster
            clsemp = clsBonusMaster.GetData(findBonus.Value, Nothing)
            lblBonusName.Text = clsemp.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim str As String
        str = "select emp_status.Status_Code as [Code],emp_status.EMP_CODE as [Employee Code],WORKING_STATUS as [Working Status],REVISION_NO as [Revision No],convert(varchar,APPLICABLE_FROM,103) as [Applicable From],DESIGNATION_ID as [Designation Code],DEVISION_CODE as [Devision Code],DEPARTMENT_CODE as [Department Code],GRADE_CODE as [Grade Code],REPORTING_PERSON_CODE as [Reporting Person Code]," & _
              " ATTENDANCE_CODE as [Attendance Code],BANK_ACC_NO as [Bank Account No],NAME_IN_ACCOUNT as [Name In Account],PAYMENT_MODE as [Payment Mode],BANK_CODE as [Bank Code],IS_PF_APPL as [Is PF Applicable],EPF_RATE AS [EPF RATE],Max_Amount_EPF as [Max Amount PF],PF_NO as [PF No],IS_ESI_APPL as [Is ESI Applicable],ESI_RATE AS [ESI RATE],Max_Amount_ESI as [Max Amount ESI],ESI_NO as [ESI No],IS_OT_APPL as [Is OT Applicable],OT_CODE as [OT Code],IS_BONUS_APPL as [Is Bonus Applicable],BONUS_CODE as [Bonus Code], " & _
              " EPS_TO_EPF as [EPS To EPF],SHIFT_CODE as [Shift Code],SHIFT_CHANG_TYPE as [Shift Change Type],CONV_RATE_CODE as [Conveyance Rate Code],CONV_TYPE as [Conveyance Type],IS_OD_APPL as [Is OD Applicable] ,PF_Calculation_Type as [PF Calculation Type],Professional_Tax_Applicable as [Professional Tax Applicable(Y/N)] from (select max(EMP_STATUS_CODE) as Status_Code,EMP_CODE from TSPL_EMPLOYEE_STATUS group by EMP_CODE) as emp_status " & _
              " left join ( " & _
              " select EMP_STATUS_CODE,EMP_CODE,REVISION_NO,APPLICABLE_FROM,DESIGNATION_ID,DEVISION_CODE,DEPARTMENT_CODE,GRADE_CODE,REPORTING_PERSON_CODE, " & _
              " ATTENDANCE_CODE,BANK_ACC_NO,NAME_IN_ACCOUNT,PAYMENT_MODE,BANK_CODE,IS_PF_APPL,EPF_RATE,Max_Amount_EPF,PF_NO,IS_ESI_APPL,ESI_RATE,Max_Amount_ESI,ESI_NO,IS_OT_APPL,OT_CODE,IS_BONUS_APPL,BONUS_CODE, " & _
              " WORKING_STATUS,EPS_TO_EPF,SHIFT_CODE,SHIFT_CHANG_TYPE,CONV_RATE_CODE,CONV_TYPE,IS_OD_APPL,PF_Calculation_Type,case when Professional_Tax_Applicable=1 then 'Y' else 'N' end as Professional_Tax_Applicable from TSPL_EMPLOYEE_STATUS) as Emp  on emp_status.Status_Code=Emp.EMP_STATUS_CODE "
        transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)

    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim CheckSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveMultipleEmployeeStatus, clsFixedParameterCode.AllowToSaveMultipleEmployeeStatus, Nothing))
        If transportSql.importExcel(gv, "Code", "Employee Code", "Working Status", "Revision No", "Applicable From", "Designation Code", "Devision Code", "Department Code", "Grade Code", "Reporting Person Code", "Attendance Code", "Bank Account No", "Name In Account", "Payment Mode", "Bank Code", "Is PF Applicable", "EPF RATE", "Max Amount PF", "PF No", "Is ESI Applicable", "ESI RATE", "Max Amount ESI", "ESI No", "Is OT Applicable", "OT Code", "Is Bonus Applicable", "Bonus Code", "EPS To EPF", "Shift Code", "Shift Change Type", "Conveyance Rate Code", "Conveyance Type", "Is OD Applicable", "PF Calculation Type", "Professional Tax Applicable(Y/N)") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsEmployeeStatus()
                    Dim IsNewEntry As Boolean = False
                    obj.Code = ""
                    Dim strValue As String = ""
                    strValue = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    If strValue.Length > 30 Or (String.IsNullOrEmpty(strValue)) Then
                        Throw New Exception("Employee Code can not be blank or incorrect.")
                    End If
                    obj.EMP_CODE = strValue
                    If clsCommon.CompairString(CheckSetting, "1") = CompairStringResult.Equal Then
                        obj.Code = ""
                        IsNewEntry = True
                        obj.REVISION_NO = clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no),0)+1) AS revision_no from TSPL_EMPLOYEE_STATUS where EMP_CODE='" & obj.EMP_CODE & "'").Rows(0).Item("revision_no")
                    Else
                        Dim Code As String = clsEmployeeStatus.GetEmployeeLatestStatus(obj.EMP_CODE)
                        If clsCommon.myLen(Code) > 0 Then
                            obj.Code = Code
                            IsNewEntry = False
                        Else
                            obj.Code = ""
                            IsNewEntry = True
                            obj.REVISION_NO = clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no),0)+1) AS revision_no from TSPL_EMPLOYEE_STATUS where EMP_CODE='" & obj.EMP_CODE & "'").Rows(0).Item("revision_no")
                        End If
                    End If
                    strValue = clsCommon.myCstr(grow.Cells("Working Status").Value)
                    If strValue.Length > 30 Or (String.IsNullOrEmpty(strValue)) Then
                        Throw New Exception("Working Status can not be blank or incorrect.")
                    End If
                    obj.WORKING_STATUS = strValue
                    If clsCommon.myLen(grow.Cells("Applicable From").Value) <= 0 Then
                        Throw New Exception("Applicable From can not be blank or incorrect.")
                    End If
                    Dim applicableFrom As Date
                    applicableFrom = clsCommon.myCDate(grow.Cells("Applicable From").Value)
                    obj.APPLICABLE_FROM = applicableFrom

                    obj.DEVISION_CODE = clsCommon.myCstr(grow.Cells("Devision Code").Value)
                    obj.DESIGNATION_ID = clsCommon.myCstr(grow.Cells("Designation Code").Value)
                    obj.DEPARTMENT_CODE = clsCommon.myCstr(grow.Cells("Department Code").Value)
                    obj.GRADE_CODE = clsCommon.myCstr(grow.Cells("Grade Code").Value)
                    obj.REPORTING_PERSON = clsCommon.myCstr(grow.Cells("Reporting Person Code").Value)
                    obj.ATTENDANCE_CODE = clsCommon.myCstr(grow.Cells("Attendance Code").Value)
                    obj.BANK_ACC_NO = clsCommon.myCstr(grow.Cells("Bank Account No").Value)
                    obj.NAME_IN_ACC = clsCommon.myCstr(grow.Cells("Name In Account").Value)
                    obj.PAYMENT_MODE = clsCommon.myCstr(grow.Cells("Payment Mode").Value)
                    obj.BANK_CODE = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                    obj.PF_APPLICABLE = clsCommon.myCstr(grow.Cells("Is PF Applicable").Value)
                    obj.PF_NO = clsCommon.myCstr(grow.Cells("PF No").Value)
                    obj.ESI_APPLICABLE = clsCommon.myCstr(grow.Cells("Is ESI Applicable").Value)
                    obj.ESI_NO = clsCommon.myCstr(grow.Cells("ESI No").Value)
                    obj.OT_APPLICABLE = clsCommon.myCstr(grow.Cells("Is OT Applicable").Value)
                    obj.OT_CODE = clsCommon.myCstr(grow.Cells("OT Code").Value)

                    obj.BONUS_APPLICABLE = clsCommon.myCstr(grow.Cells("Is Bonus Applicable").Value)
                    obj.BONUS_CODE = clsCommon.myCstr(grow.Cells("Bonus Code").Value)
                    obj.EPS_TO_EPF = clsCommon.myCstr(grow.Cells("EPS To EPF").Value)
                    '' for kdil and viney
                    obj.SHIFT_CODE = clsCommon.myCstr(grow.Cells("Shift Code").Value)
                    obj.SHIFT_CHANG_TYPE = clsCommon.myCstr(grow.Cells("Shift Change Type").Value)
                    obj.CONV_TYPE = clsCommon.myCstr(grow.Cells("Conveyance Type").Value)
                    obj.CONV_RATE_CODE = clsCommon.myCstr(grow.Cells("Conveyance Rate Code").Value)
                    obj.IS_OD_APPL = clsCommon.myCstr(grow.Cells("Is OD Applicable").Value)
                    obj.Max_Amount_ESI = clsCommon.myCdbl(grow.Cells("Max Amount ESI").Value)
                    obj.ESI_Rate = clsCommon.myCdbl(grow.Cells("ESI Rate").Value)
                    obj.Professional_Tax_Applicable = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Professional Tax Applicable(Y/N)").Value), "Y") = CompairStringResult.Equal)
                    Dim pfCal As String = clsCommon.myCstr(grow.Cells("PF Calculation Type").Value)
                    If clsCommon.CompairString(pfCal, "C") = CompairStringResult.Equal Then
                        obj.Pf_Calculation_Type = pfCal
                        obj.Max_Amount_EPF = clsCommon.myCdbl(grow.Cells("Max Amount PF").Value)
                        obj.EPF_Rate = clsCommon.myCdbl(grow.Cells("EPF Rate").Value)
                    Else
                        obj.Pf_Calculation_Type = pfCal
                        obj.Max_Amount_EPF = 0
                        obj.EPF_Rate = 0
                    End If
                    obj.SaveData(obj, IsNewEntry, obj.Code, Nothing)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub fndShift__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndShift._MYValidating
        Me.fndShift.Value = clsShiftMaster.getFinder("", fndShift.Value, isButtonClicked)
    End Sub

    Private Sub fndConveyanceRate__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndConveyanceRate._MYValidating
        Me.fndConveyanceRate.Value = clsConveyanceRateMaster.getFinder("CONV_TYPE='" & Me.cboConveyanceType.SelectedValue & "'", fndConveyanceRate.Value, isButtonClicked)
    End Sub

    Sub LoadPFCalculationType()
        Try
            isInsideLoadData = True
            Dim dt As DataTable = New DataTable
            dt.Columns.Add("Code")
            dt.Columns.Add("Name")

            Dim dr As DataRow = dt.NewRow
            dr("Code") = "PR"
            dr("Name") = "PF Rule"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Code") = "FA"
            dr("Name") = "Formula Amount"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Code") = "C"
            dr("Name") = "Custom"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Code") = "N"
            dr("Name") = "None"
            dt.Rows.Add(dr)
            cboPFCalculatnType.DataSource = dt
            cboPFCalculatnType.ValueMember = "Code"
            cboPFCalculatnType.DisplayMember = "Name"
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkPFApplicable_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkPFApplicable.ToggleStateChanged
        If chkPFApplicable.Checked = True Then
            cboPFCalculatnType.Visible = True
            txtPfNo.Enabled = True
        Else
            cboPFCalculatnType.Visible = False
            txtPfNo.Enabled = False
            txtEPFRate.Enabled = False
            txtEPFMaxLimit.Enabled = False
            txtEPFRate.Text = 0
            txtEPFMaxLimit.Text = 0
        End If
    End Sub

    Private Sub cboPFCalculatnType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboPFCalculatnType.SelectedValueChanged
        If isInsideLoadData Then
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboPFCalculatnType.SelectedValue), "C") = CompairStringResult.Equal Then
            txtEPFRate.Enabled = True
            txtEPFMaxLimit.Enabled = True
        Else
            txtEPFRate.Enabled = False
            txtEPFMaxLimit.Enabled = False
            txtEPFRate.Text = 0
            txtEPFMaxLimit.Text = 0
        End If
    End Sub
End Class