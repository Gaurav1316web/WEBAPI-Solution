'' tickett done against ticket no. BHA/01/02/19-000800 
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmEmployee_Salary
    Inherits FrmMainTranScreen
    Const colLineNo As String = "LineNo"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colPayHeadFormula As String = "Formula"
    Const colRateAmount As String = "RateAmount"
    Const colHiddenComponent As String = "HiddenComponent"
    Const colMax_Amount As String = "colMax_Amount"
    Const colPAYPERIOD_Amount As String = "colPAYPERIOD_Amount"
    Const ColPayheadtype As String = "ColPayheadtype"
    Const ColPayhead As String = "ColPayhead"
    Public sal_structure_code As String = String.Empty

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsEmployeeSalary
    Private ObjList As New List(Of clsEmpSalaryPayHeadDetails)
    Private isCellValueChangedOpen As Boolean = False
    Public isInsideLoadData As Boolean = False
    Public isCellValueChanged As Boolean = False


    Sub LoadGridColumns()
        gvSalary.Rows.Clear()
        gvSalary.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim payHeadCode As New GridViewTextBoxColumn
        Dim payHeadtype As New GridViewTextBoxColumn
        Dim Payhead As New GridViewTextBoxColumn
        Dim PayHeadName As New GridViewTextBoxColumn
        Dim Formula As New GridViewTextBoxColumn
        Dim RateAmount As New GridViewDecimalColumn
        Dim IsHiddenComponent As New GridViewCheckBoxColumn
        Dim Max_Amount As New GridViewDecimalColumn
        Dim PAYPERIOD_Amount As New GridViewDecimalColumn
        Dim LocationCode As New GridViewDecimalColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 100
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(LineNo)

        payHeadCode.FormatString = ""
        payHeadCode.HeaderText = "Pay Head Code"
        payHeadCode.Name = colpayHeadCode
        payHeadCode.Width = 100
        payHeadCode.ReadOnly = True
        payHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(payHeadCode)

        PayHeadName.FormatString = ""
        PayHeadName.HeaderText = "Pay Head Name"
        PayHeadName.Name = colpayHeadName
        PayHeadName.Width = 100
        PayHeadName.ReadOnly = True
        PayHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(PayHeadName)

        Formula.FormatString = ""
        Formula.HeaderText = "Formula"
        Formula.Name = colPayHeadFormula
        Formula.Width = 100
        Formula.ReadOnly = True
        Formula.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(Formula)

        RateAmount.FormatString = ""
        RateAmount.HeaderText = "Rate/Amount"
        RateAmount.Name = colRateAmount
        RateAmount.Width = 100
        RateAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(RateAmount)

        IsHiddenComponent = New GridViewCheckBoxColumn()
        IsHiddenComponent.HeaderText = "Is Hidden Component"
        IsHiddenComponent.Name = colHiddenComponent
        IsHiddenComponent.Width = 50
        IsHiddenComponent.ReadOnly = True
        gvSalary.Columns.Add(IsHiddenComponent)

        Max_Amount.FormatString = ""
        Max_Amount.HeaderText = "Maximum Amount Limit"
        Max_Amount.Name = colMax_Amount
        Max_Amount.Width = 100
        Max_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(Max_Amount)

        Payheadtype.FormatString = ""
        payHeadtype.HeaderText = "Pay Head Mode"
        payHeadtype.Name = ColPayheadtype
        payHeadtype.Width = 100
        payHeadtype.ReadOnly = True
        payHeadtype.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(Payheadtype)

        PAYPERIOD_Amount.FormatString = ""
        PAYPERIOD_Amount.HeaderText = "Pay Period Amount"
        PAYPERIOD_Amount.Name = colPAYPERIOD_Amount
        PAYPERIOD_Amount.Width = 100
        PAYPERIOD_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        PAYPERIOD_Amount.ReadOnly = True
        gvSalary.Columns.Add(PAYPERIOD_Amount)

        Payhead.FormatString = ""
        Payhead.HeaderText = "Pay Head Type"
        Payhead.Name = ColPayhead
        Payhead.Width = 100
        Payhead.ReadOnly = True
        Payhead.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(Payhead)
    End Sub


    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                If clsEmployeeSalary.ReverseAndUnpost(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(sal_structure_code) > 0 Then
            LoadData(sal_structure_code, NavigatorType.Current)
            txtRevisionNo.Text = clsCommon.myCdbl(txtRevisionNo.Text) + 1
            txtCode.Value = Nothing
            btnsave.Text = "Save"
            isNewEntry = True
        End If

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            txtRevisionNo.Text = clsCommon.myCdbl(txtRevisionNo.Text) + 1
            If isPosted = False Then
                txtCode.Value = Me.Tag.ToString()
                btnsave.Text = "Update"
                isNewEntry = False
            Else
                txtCode.Value = Nothing
                btnsave.Text = "Save"
                isNewEntry = True
            End If

        End If
        btnReverse.Visible = False
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
    End Sub
    '' changes by shivani against ticket no [BM00000008846]
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmEmpSalary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'RadMenu2.Visible = MyBase.isExport
        btnReverse.Visible = False

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
        If clsCommon.myLen(sal_structure_code) > 0 Then
            FrmEmployeeTransfer.save_structure_code = clsCommon.myCstr(obj.EMP_SAL_CODE)
            ' FrmEmployeeTransfer.lblSalaryCode.Text = clsCommon.myCstr(obj.EMP_SAL_CODE)

        End If
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
        UsLock1.Status = ERPTransactionStatus.Pending
        'txtAdjustBy.Value = Nothing
        Me.txtSalaryStruct.Value = Nothing
        Me.lblSalStructName.Text = ""
        lblEmpName.Text = ""
        Me.txtEmpCode.Value = Nothing
        Me.txtEmpCode.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvSalary.Rows.Clear()
        Me.gvSalary.Rows.AddNew()
        Me.dtpApplicableFrom.Value = clsCommon.GETSERVERDATE
        txtCopySalaryCode.Value = ""
        Me.txtRevisionNo.Text = 0
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Dim isPosted As Boolean = False
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        isInsideLoadData = True
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsEmployeeSalary.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.SALARY_STRUCT_CODE) > 0) Then

            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                isPosted = obj.POSTED
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
                isPosted = obj.POSTED
            End If
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.EMP_SAL_CODE
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.EMP_NAME)
            txtSalaryStruct.Value = clsCommon.myCstr(obj.SALARY_STRUCT_CODE)
            txtSalaryStruct.Text = clsCommon.myCstr(obj.SALARY_STRUCT_NAME)
            lblSalStructName.Text = clsCommon.myCstr(obj.SALARY_STRUCT_NAME)
            txtRevisionNo.Text = clsCommon.myCdbl(obj.REVISION_NO)
            dtpApplicableFrom.Value = obj.APPLICABLE_FROM
            If clsCommon.myLen(obj.Location_Code) > 0 Then
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            Else
                lblLocationName.Text = ""
            End If
            If (clsEmployeeSalary.ObjList IsNot Nothing AndAlso clsEmployeeSalary.ObjList.Count > 0) Then
                For Each obj1 As clsEmpSalaryPayHeadDetails In clsEmployeeSalary.ObjList
                    gvSalary.Rows.AddNew()
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = obj1.Line_No
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadCode).Value = obj1.PayHeadCode
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadName).Value = obj1.PayHeadName
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj1.Formula
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colRateAmount).Value = obj1.Rate_Amount
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colHiddenComponent).Value = obj1.IsHiddenComponent
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colMax_Amount).Value = obj1.MAX_AMOUNT
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPAYPERIOD_Amount).Value = obj1.PAYPERIOD_AMOUNT
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(ColPayhead).Value = obj1.Payhead
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(ColPayheadtype).Value = obj1.PayheadMode
                Next
            Else
                gvSalary.Rows.AddNew()
            End If
        End If
        gvSalary.Rows.AddNew()
        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = "TOTAL"
        setGridTotalRate()
        isInsideLoadData = False
    End Sub
    Sub Show_salary_struct(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        'btnsave.Enabled = True
        'btndelete.Enabled = True
        Dim obj1 As clsMapPayHeadsToSalaStructure
        obj1 = clsMapPayHeadsToSalaStructure.GetData(strCode, NavTyep)
        If (obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.SALARY_STRUCTURE_CODE) > 0) Then
            'funReset()
            'isNewEntry = False
            'btnsave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGridColumns()
            'txtSalaryStruct.Value = obj1.SALARY_STRUCTURE_CODE
            lblSalStructName.Text = obj1.SALARY_STRUCTURE_NAME
            If (clsMapPayHeadsToSalaStructure.ObjList IsNot Nothing AndAlso clsMapPayHeadsToSalaStructure.ObjList.Count > 0) Then
                For Each obj As clsMapPayHeadsToSalaStructure In clsMapPayHeadsToSalaStructure.ObjList
                    gvSalary.Rows.AddNew()
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = obj.LINE_NO
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj.PAYHEAD_FORMULA
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colRateAmount).Value = obj.RATE_AMOUNT
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(ColPayhead).Value = obj.HEAD_TYPE
                    Dim PayheadType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select ISEARNING from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE='" + obj.PAY_HEAD_CODE + "' "))
                    If PayheadType = 1 Then
                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(ColPayheadtype).Value = "A"
                    Else
                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(ColPayheadtype).Value = "D"
                    End If
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
                Next
            Else
                gvSalary.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select EMP_SAL_CODE as Code, EMP_CODE AS 'Employee Code',applicable_from from TSPL_EMPLOYEE_SALARY "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_SALARY", qry, "Code", "", txtCode.Value, "EMP_SAL_CODE", isButtonClicked)
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
            Dim obj As New clsEmployeeSalary
            obj.EMP_SAL_CODE = Me.txtCode.Value
            obj.EMP_CODE = Me.txtEmpCode.Value
            obj.REVISION_NO = clsCommon.myCdbl(Me.txtRevisionNo.Text)
            obj.APPLICABLE_FROM = clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy")
            obj.SALARY_STRUCT_CODE = Me.txtSalaryStruct.Value
            obj.Location_Code = fndLocation.Value
            Dim obj1 As clsEmpSalaryPayHeadDetails
            ObjList = New List(Of clsEmpSalaryPayHeadDetails)
            For Each grow As GridViewRowInfo In gvSalary.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
                    obj1 = New clsEmpSalaryPayHeadDetails()
                    'obj1.EMP_SAL_CODE = txtCode.Value
                    'obj1.EMP_CODE = clsCommon.myCstr(txtEmpCode.Value)
                    obj1.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                    obj1.Formula = clsCommon.myCstr(grow.Cells(colPayHeadFormula).Value)
                    obj1.Rate_Amount = clsCommon.myCdbl(grow.Cells(colRateAmount).Value)
                    obj1.IsHiddenComponent = clsCommon.myCdbl(grow.Cells(colHiddenComponent).Value)
                    obj1.MAX_AMOUNT = clsCommon.myCdbl(grow.Cells(colMax_Amount).Value)
                    obj1.PAYPERIOD_AMOUNT = clsCommon.myCdbl(grow.Cells(colPAYPERIOD_Amount).Value)
                    obj1.Payhead = clsCommon.myCstr(grow.Cells(ColPayhead).Value)
                    obj1.PayheadMode = clsCommon.myCstr(grow.Cells(ColPayheadtype).Value)
                    ObjList.Add(obj1)
                End If
            Next
            If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                LoadData(obj.EMP_SAL_CODE, NavigatorType.Current)
                If clsCommon.myLen(sal_structure_code) > 0 Then
                    FrmEmployeeTransfer.save_structure_code = clsCommon.myCstr(obj.EMP_SAL_CODE)
                End If
                Return True
                '  common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
            Return False
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean

        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Employee Code", Me.Text)
            txtEmpCode.Focus()
            Return False
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Salary Code")
        '    txtCode.Focus()
        '    Return False
        'End If
        If isNewEntry Then
            Dim qry As String = "select max(applicable_from) as max_app_date from tspl_employee_salary where emp_code='" & txtEmpCode.Value & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0).Item("max_app_date")) Then
                    If dtpApplicableFrom.Value < dt.Rows(0).Item("max_app_date") Then
                        clsCommon.MyMessageBoxShow(Me, "Maximum applicable from date must be greater than or equal to " & clsCommon.GetPrintDate(dt.Rows(0).Item("max_app_date"), "dd/MMM/yyyy") & "", Me.Text)
                        Return False
                    End If
                End If
            End If
        End If
        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvSalary.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
                ii += 1
                'If clsCommon.myCdbl(grow.Cells(colAllowanceAmount).Value) = 0 Then
                'Return False
                'End If
                'ObjList.Add(obj)
            End If

        Next
        If ii = 0 Then
            Return False
        End If
        Return True
    End Function


    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvSalary.Columns(colpayHeadCode) Then
                'Dim strq As String
                'strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
                '& " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_DAILY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
                '& " left join TSPL_HOURLY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"
                Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gvSalary.CurrentRow.Cells(colpayHeadCode).Value), False)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
                    gvSalary.CurrentRow.Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvSalary.CurrentRow.Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

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
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsEmployeeSalary.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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

    Private Sub txtEmpCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "' and Emp_Status<>'Inactive'"
                End If
            Else
                whrcls += " Emp_Status<>'Inactive'"
            End If

            Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name,TSPL_EMPLOYEE_MASTER.PF_NO as [PF No] FROM TSPL_EMPLOYEE_MASTER "
            txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER1", qry, "Code", whrcls, txtEmpCode.Value, "", isButtonClicked)
            Dim clsemp As clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
            If Not clsemp Is Nothing Then
                lblEmpName.Text = clsemp.Emp_Name
            End If
            If isNewEntry = True Then
                Try
                    Me.txtRevisionNo.Text = clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no),0)+1) AS revision_no from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & Me.txtEmpCode.Value & "'").Rows(0).Item("revision_no")
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSalaryStruct__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtSalaryStruct._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            isInsideLoadData = True
            Dim qry As String = "SELECT SALARY_STRUCTURE_CODE as Code,SALARY_STRUCTURE_NAME as Name FROM TSPL_SALARY_STRUCTURE "
            txtSalaryStruct.Value = clsCommon.ShowSelectForm("TSPL_SALARY_STRUCTURE", qry, "Code", whrcls, txtSalaryStruct.Value, "", isButtonClicked)
            'Dim clsemp As clsSalaryStructure
            'clsemp = clsSalaryStructure.GetData(txtSalaryStruct.Value, Nothing)
            'lblSalStructName.Text = clsemp.SALARY_STRUCTURE_NAME

            Show_salary_struct(txtSalaryStruct.Value, NavigatorType.Current)
            gvSalary.Rows.AddNew()
            gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = "TOTAL"
            setGridTotalRate()
            isInsideLoadData = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " Emp.LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select T1.EMP_SAL_CODE AS Code,T3.SALARY_STRUCTURE_NAME,T1.EMP_CODE,T2.EMP_NAME AS EMPLOYEE_NAME,T1.APPLICABLE_FROM,T1.REVISION_NO AS [Revision No], T1.POSTED  from TSPL_EMPLOYEE_SALARY T1 " _
            '& " LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE LEFT JOIN TSPL_SALARY_STRUCTURE T3 ON T1.SALARY_STRUCTURE_CODE=T3.SALARY_STRUCTURE_CODE"

            txtCode.Value = clsEmployeeSalary.GetFinder(whrcls, Me.chkShowAll.Checked, txtCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("EMP_SALARY", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsEmployeeSalary.PostData(txtCode.Value, True)) Then
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
    ' Ticket No : ERO/29/08/19-001009 by Prabhakar
    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        If clsCommon.myLen(Me.txtSalaryStruct.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select any Salary Structure.", Me.Text)
            Exit Sub
        End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim strSelect As String
        strSelect = clsEmployeeSalary.GetPayHeadCodeString(Me.txtSalaryStruct.Value)
        strSelect = strSelect.Replace("[", "")
        strSelect = strSelect.Replace("]", "")
        If clsCommon.myLen(strSelect) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Pay Head not Found for the Selected Salary Structure " & Me.txtSalaryStruct.Value & "")
            Exit Sub
        End If
        ''"Emp ID", "Employee Name", "Salary Structure Code", "REVISION NO", "APPLICABLE FROM", "BASIC", "HRA", "CONVEYANCE", "CONV-REIMB", "CH EDU ALL", "FOOD", "NEWSPAPERALL", "TELEPHONE", "SPECIAL ALL", "LIC", "LOAN", "ONEDAYSAL", "WALT INSU", "EPF", "ESI", "PT", "TDS", "ADV REC"
        Dim arr() As String
        Dim arrParam() As String = {"Emp ID", "Employee Name", "Salary Structure Code", "Revision No", "Applicable Date", "Copy Salary Code"}
        arr = strSelect.Split(",")
        For Each strarr As String In arr
            ReDim Preserve arrParam(arrParam.Length)
            arrParam(arrParam.Length - 1) = strarr
        Next


        If importExcelSalary(gv, arrParam) Then
            Try
                clsCommon.ProgressBarShow()

                Dim obj As clsEmployeeSalary
                Dim obj1 As clsEmpSalaryPayHeadDetails
                Dim EMP_SAL_Code As New List(Of String)

                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsEmployeeSalary
                    ObjList = New List(Of clsEmpSalaryPayHeadDetails)

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Emp ID").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Emp_id not be blank or incorrect.")
                    End If
                    obj.EMP_CODE = strCode

                    Dim strSalStruct As String = clsCommon.myCstr(grow.Cells("Salary Structure Code").Value)
                    If strSalStruct.Length > 100 Or (String.IsNullOrEmpty(strSalStruct)) Then
                        Throw New Exception("Salary Structure Code can not be blank or incorrect.")
                    End If
                    If clsCommon.CompairString(strSalStruct, txtSalaryStruct.Value) <> CompairStringResult.Equal Then
                        Throw New Exception("In Screen Selected [Salary Structure Code] should be same Import Sheet.")
                    End If
                    obj.SALARY_STRUCT_CODE = strSalStruct

                    obj.REVISION_NO = clsEmployeeSalary.GetRevisionNo(obj.EMP_CODE) ''clsCommon.myCstr(grow.Cells("REVISION NO").Value)

                    Dim strDate As Date = clsCommon.myCDate(grow.Cells("Applicable Date").Value)
                    If strDate.Year < 2000 Or (String.IsNullOrEmpty(strDate)) Then
                        Throw New Exception("Applicable Date can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                    End If
                    obj.APPLICABLE_FROM = clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy")
                    obj.Location_Code = clsCommon.myCstr(grow.Cells("Location_Code").Value)

                    ''''''''''''''''''''''''''Detail part''''''''''''''''''''''''''''''''''
                    Dim intLoop As Integer = 1
                    For Each strarr As String In arr
                        'strarr = strarr.Replace("_", ".")
                        obj1 = New clsEmpSalaryPayHeadDetails
                        'obj1.EMP_CODE = strCode
                        obj1.PayHeadCode = strarr '.Replace("_", ".")
                        obj1.Line_No = intLoop
                        obj1.Formula = clsMapPayHeadsToSalaStructure.GetFormula(strSalStruct, strarr.Replace("_", "."))
                        obj1.Rate_Amount = clsCommon.myCdbl(grow.Cells(strarr.Replace(".", "_")).Value)
                        obj1.PAYPERIOD_AMOUNT = obj1.Rate_Amount
                        ObjList.Add(obj1)
                        intLoop = intLoop + 1
                    Next

                    obj.SaveData(obj, ObjList, True, "")
                    EMP_SAL_Code.Add(obj.EMP_SAL_CODE)

                Next

                Dim UP_QRY As String = ""

                UP_QRY = " UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS SET ISHIDDENCOMPONENT=T1.ISHIDDENCOMPONENT FROM TSPL_PAYHEAD_MASTER T1 "
                UP_QRY += " WHERE T1.PAY_HEAD_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE and TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ") "

                clsDBFuncationality.ExecuteNonQuery(UP_QRY)

                UP_QRY = ""
                UP_QRY = " UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS SET TSPL_EMPLOYEE_SALARY_PAYHEADS.LINE_NO=TSPL_SALSTRUCT_PAYHEADS.LINE_NO FROM " &
                         " TSPL_SALSTRUCT_PAYHEADS INNER JOIN TSPL_EMPLOYEE_SALARY " &
                         " ON TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE=TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE " &
                         " WHERE(TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE = TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE) " &
                         " AND TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE AND TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE='" & txtSalaryStruct.Value & "' and TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ")"
                clsDBFuncationality.ExecuteNonQuery(UP_QRY)
                UP_QRY = String.Empty
                'UP_QRY = "update TSPL_EMPLOYEE_SALARY_PAYHEADS set PAYPERIOD_AMOUNT=RATE_AMOUNT WHERE LEN(PAYHEAD_FORMULA)=0 AND EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ") "
                'clsDBFuncationality.ExecuteNonQuery(UP_QRY)
                'UP_QRY = String.Empty
                'UP_QRY = " SELECT DISTINCT LINE_NO FROM TSPL_EMPLOYEE_SALARY_PAYHEADS WHERE LINE_NO IS NOT NULL AND EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ")  ORDER BY LINE_NO"

                'Dim dtSeq As DataTable
                'dtSeq = clsDBFuncationality.GetDataTable(UP_QRY)
                'UP_QRY = String.Empty


                'For Each drSeq As DataRow In dtSeq.Rows
                '    UP_QRY = "UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS " _
                '            & " SET FORMULA_AMT = '(' + REPLACE(PAYHEAD_FORMULA,'[' + T5.PAY_HEAD_CODE + ']',COALESCE(T5.FORMULA_AMT, '0')) + ')*(' + CAST(RATE_AMOUNT as VARCHAR(10)) + '/100.00)' " _
                '            & " FROM " _
                '            & " ( " _
                '            & " SELECT TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE, EMP_CODE,FORMULA_AMT,PAY_HEAD_CODE,LINE_NO " _
                '            & " FROM TSPL_EMPLOYEE_SALARY_PAYHEADS " _
                '            & " INNER JOIN TSPL_EMPLOYEE_SALARY ON TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE " _
                '            & " WHERE LINE_NO = " & drSeq.Item("LINE_NO") & " " _
                '            & " ) AS T5 " _
                '            & " WHERE TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE = T5.EMP_SAL_CODE AND TSPL_EMPLOYEE_SALARY_PAYHEADS.LINE_NO > " & drSeq.Item("LINE_NO") & "" _
                '            & " AND LEN(TSPL_EMPLOYEE_SALARY_PAYHEADS.PAYHEAD_FORMULA)>0  AND EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ")"

                '    clsDBFuncationality.GetDataTable(UP_QRY)
                'Next

                'UP_QRY = "UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS SET FORMULA_AMT = '0' WHERE (LTRIM(PAYHEAD_FORMULA) = '' OR PAYHEAD_FORMULA IS NULL) AND EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ")"
                'clsDBFuncationality.GetDataTable(UP_QRY)

                'Dim dtSal As DataTable
                'dtSal = clsDBFuncationality.GetDataTable("select * from TSPL_EMPLOYEE_SALARY_PAYHEADS WHERE LINE_NO IS NOT NULL and HEAD_TYPE='F' AND EMP_SAL_CODE in (" & clsCommon.GetMulcallString(EMP_SAL_Code) & ") ORDER BY EMP_SAL_CODE")
                'For Each drSal As DataRow In dtSal.Rows
                '    UP_QRY = "UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS SET FORMULA_VALUE = (select " & drSal.Item("FORMULA_AMOUNT") & ")," _
                '    & " HEAD_VALUE=(select " & drSal.Item("PAYHEAD_FORMULA") & ") where SALARY_CALCULATION_CODE= " & drSal.Item("SALARY_CALCULATION_CODE") & ""

                '    If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
                '        'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
                '    Else
                '        clsCommon.MyMessageBoxShow("Error in Updating Formula Pay Heads !")
                '        Me.ProgressBar1.Value = 100
                '        Return False
                '        Exit Sub
                '    End If

                'Next



                'strq = "UPDATE TSPL_SALARY_CALCULATION SET FORMULA_AMOUNT=0 where COALESCE(FORMULA_AMOUNT,'')='' or FORMULA_AMOUNT=''"
                'If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
                '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
                '    If (Me.ProgressBar1.Value + 1) > 100 Then
                '        Me.ProgressBar1.Value = 100
                '    Else
                '        Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
                '    End If
                'Else
                '    clsCommon.MyMessageBoxShow("Error in Updating Formula Pay Heads !")
                '    Me.ProgressBar1.Value = 100
                '    Return False
                '    Exit Sub
                'End If



                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub
    Public Function importExcelSalary(ByVal gv As RadGridView, ByVal ParamArray fieldNames As String()) As Boolean
        Try

            'Dim ofd As OpenFileDialog = New OpenFileDialog()
            'Dim filePath As String
            ''ofd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
            'If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = ofd.FileName
            'Else
            '    Return False
            'End If
            'Dim Extension As String = Path.GetExtension(filePath)
            'Dim conStr As String = ""


            ''Dim oApp As Excel.Application
            ''Dim oWB As Excel.Workbook
            ''oApp = New Excel.Application
            ''oWB = oApp.Workbooks.Open(filePath)
            ''MessageBox.Show(oWB.FileFormat.ToString)



            'Select Case Extension
            '    Case ".xls"
            '        '        'Excel 97-03 
            '        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
            '        Exit Select
            '    Case ".xlsx"
            '        '        'Excel 07  
            '        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";"
            '        Exit Select
            'End Select
            ''conStr = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties=""Excel 8.0;HDR=NO;IMEX=1"";"
            'conStr = [String].Format(conStr, filePath)

            'Dim connExcel As New System.Data.OleDb.OleDbConnection(conStr)
            'Dim cmdExcel As New System.Data.OleDb.OleDbCommand()
            'Dim oda As New System.Data.OleDb.OleDbDataAdapter()
            'Dim ds As New DataTable()
            'cmdExcel.Connection = connExcel

            ''Get the name of First Sheet  
            'connExcel.Open()
            'Dim dtExcelSchema As DataTable
            'dtExcelSchema = connExcel.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
            'Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
            'connExcel.Close()

            ''Read Data from First Sheet  
            'connExcel.Open()
            'cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
            'oda.SelectCommand = cmdExcel
            'oda.Fill(ds)
            'connExcel.Close()
            'gv.DataSource = ds.DefaultView
            'gv.AllowColumnReorder = True
            If Not transportSql.LoadDocument(gv, "", fieldNames) Then
                Return False
            End If
            Dim fieldCount As Integer = fieldNames.Length
            Dim strfields As String = ""
            For Each field As String In fieldNames
                strfields = strfields + field + ","
            Next

            If gv.ColumnCount > 4 Then
                Dim i As Integer = 0
                Dim arr As ArrayList = New ArrayList()
                Dim arrExtraPayHead As ArrayList = New ArrayList()
                For Each GC As GridViewColumn In gv.Columns
                    arr.Add(GC.HeaderText.ToString.Replace("_", "."))
                Next
                For Each field As String In fieldNames
                    If arr.Contains(field.Trim()) Then
                        'For Each GC As GridViewColumn In gv.Columns
                        '    If GC.HeaderText = field Then
                        '        gv.Columns.Move(GC.Index, i)
                        '        Exit For
                        '    End If
                        'Next
                    ElseIf Array.IndexOf(fieldNames, field) <= 4 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format.It should have the Minimum columns named - " + "Emp ID, Employee Name, Salary Structure Code, Revision No, Applicable Date")
                        Return False
                    Else
                        arrExtraPayHead.Add(field)
                    End If
                    i = i + 1
                Next

                '' adding extra columns 
                For Each payhead As String In arrExtraPayHead
                    For Each gvColumn As GridViewColumn In gv.Columns
                        If gvColumn.HeaderText.Contains(clsCommon.myCstr(payhead).Trim()) Then
                            If clsCommon.CompairString(clsCommon.myCstr(gvColumn.HeaderText), clsCommon.myCstr(payhead).Trim) = CompairStringResult.Equal Then
                                Continue For
                            End If
                            gv.Columns.Add(clsCommon.myCstr(payhead).Replace(".", "_"))
                        End If
                    Next
                    '' update amount in extra payheads
                    For Each item As GridViewRowInfo In gv.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(item.Cells("Applicable Date").Value), "") = CompairStringResult.Equal Or IsDate(clsCommon.myCstr(item.Cells("Applicable Date").Value)) = False Then
                            Throw New Exception("Applicable Date can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(item.Cells("Emp ID").Value) + "")
                            Exit Function
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(item.Cells("Salary Structure Code").Value), "") = CompairStringResult.Equal Then
                            Throw New Exception("Salary Structure Code can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(item.Cells("Emp ID").Value) + "")
                            Exit Function
                        End If
                        item.Cells(payhead.Replace(".", "_")).Value = clsEmployeeSalary.getPayHeadAmount(clsCommon.myCstr(item.Cells("Emp ID").Value), clsCommon.myCstr(item.Cells("Salary Structure Code").Value), clsCommon.myCstr(payhead), clsCommon.myCstr(item.Cells("Applicable Date").Value), clsCommon.myCstr(item.Cells("Copy Salary Code").Value))
                    Next
                Next
                Return True
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format. It should have the columns named - " + strfields, Me.Text)
                Return False
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
        Return True
    End Function


    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        'clsCommon.MyMessageBoxShow("Option under Development !")
        If clsCommon.myLen(Me.txtSalaryStruct.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select any Salary Structure.", Me.Text)
            Exit Sub
        End If

        Dim str As String
        str = clsEmployeeSalary.ExportEmployeeSalary(Me.txtSalaryStruct.Value, Me, dtpApplicableFrom.Text)
        'transportSql.ExporttoExcelNew(str, Me)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub
    Private Sub setGridTotalRate()
        Try


            Dim intCurrRow As Integer? = Nothing
            Dim dblAmount As Double = 0
            Dim DedAmount As Double = 0
            Dim dblPayHeadAmount As Double = 0
            For ii As Integer = 0 To gvSalary.Rows.Count - 1

                If (clsCommon.myLen(gvSalary.Rows(ii).Cells(colLineNo).Value) > 0) AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Rows(ii).Cells(colLineNo).Value), "TOTAL") <> CompairStringResult.Equal Then
                    If clsCommon.myCstr(gvSalary.Rows(ii).Cells(colPayHeadFormula).Value) = "" Then
                        dblAmount = dblAmount + clsCommon.myCdbl(gvSalary.Rows(ii).Cells(colRateAmount).Value)
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gvSalary.Rows(ii).Cells(ColPayheadtype).Value), "A") = CompairStringResult.Equal Then
                        dblPayHeadAmount = dblPayHeadAmount + clsCommon.myCdbl(gvSalary.Rows(ii).Cells(colPAYPERIOD_Amount).Value)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gvSalary.Rows(ii).Cells(ColPayheadtype).Value), "D") = CompairStringResult.Equal Then
                        DedAmount = DedAmount + clsCommon.myCdbl(gvSalary.Rows(ii).Cells(colPAYPERIOD_Amount).Value)
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvSalary.Rows(ii).Cells(colLineNo).Value), "TOTAL") = CompairStringResult.Equal Then
                    intCurrRow = ii
                End If
            Next
            ' gvSalary.Rows(intCurrRow).Cells(colRateAmount).Value = dblAmount
            Dim finalamt As Double = dblPayHeadAmount - DedAmount
            gvSalary.Rows(intCurrRow).Cells(colPAYPERIOD_Amount).Value = finalamt
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvSalary_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSalary.CellValueChanged
        Try
            If Not isInsideLoadData Then

                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If e.Column Is gvSalary.Columns(colRateAmount) Then

                        gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value = GetPayperiodAmount(gvSalary.CurrentRow.Index)
                        setGridTotalRate()

                    End If
                    isCellValueChanged = False
                End If
            End If

        Catch ex As Exception
            isCellValueChanged = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function GetPayperiodAmount(ByVal Rowno As Integer) As Decimal
        Dim PayPeriodAmount As Decimal = 0
        If clsCommon.myLen(gvSalary.Rows(Rowno).Cells(colPayHeadFormula).Value) <= 0 Then
            PayPeriodAmount = clsCommon.myCdbl(gvSalary.Rows(Rowno).Cells(colRateAmount).Value)
        Else
            Dim strFormula As String = gvSalary.Rows(Rowno).Cells(colPayHeadFormula).Value
            'Dim arrFormula = strFormula.Split("+")
            For Each row As GridViewRowInfo In gvSalary.Rows
                If row.Index < Rowno Then
                    strFormula = strFormula.Replace(row.Cells(colpayHeadCode).Value, clsCommon.myCstr(clsCommon.myCDecimal(row.Cells(colPAYPERIOD_Amount).Value)))
                Else
                    strFormula = strFormula.Replace("[", "")
                    strFormula = strFormula.Replace("]", "")
                    PayPeriodAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select " & strFormula & "")) * row.Cells(colRateAmount).Value / 100
                    Return PayPeriodAmount
                End If
            Next
        End If
        Return PayPeriodAmount
    End Function

    Private Sub gvSalary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSalary.DoubleClick
        If gvSalary.Rows.Count > 0 AndAlso gvSalary.CurrentColumn.Name Is gvSalary.Columns(colPayHeadFormula).Name Then
            Try
                Dim ListOp As New List(Of String)
                For kk As Int16 = 0 To gvSalary.CurrentRow.Index - 1
                    ListOp.Add(clsCommon.myCstr(gvSalary.Rows(kk).Cells(colpayHeadCode).Value))
                Next

                Dim FFS As New frmFormulaSelection
                FFS.ListOperand = ListOp
                FFS.OldFormula = gvSalary.CurrentRow.Cells(colPayHeadFormula).Value
                FFS.txtFormula.Text = gvSalary.CurrentRow.Cells(colPayHeadFormula).Value
                FFS.ShowDialog()
                gvSalary.CurrentRow.Cells(colPayHeadFormula).Value = FFS.txtFormula.Text
            Catch ex As Exception
            End Try
        End If
    End Sub
    ' Ticket No : BHA/13/02/19-000814,ERO/29/08/19-001008 by Prabhakar 
    Private Sub txtCopySalaryCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCopySalaryCode._MYValidating
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            txtCopySalaryCode.Value = clsEmployeeSalary.GetFinder("", False, txtCode.ValidateChildren, isButtonClicked)
            '--------------------------------------------------------------------------------------------------------------------------------------------------------------
            Dim strCopySalaryStructureCode As String = Nothing
            If clsCommon.myLen(txtSalaryStruct.Value) > 0 Then
                strCopySalaryStructureCode = txtSalaryStruct.Value
            Else
                Dim qry As String = "select TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE from TSPL_EMPLOYEE_SALARY where TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE = '" + txtCopySalaryCode.Value + "'"
                strCopySalaryStructureCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            End If


            Fill_salary_struct_for_Copy_Button(strCopySalaryStructureCode, NavigatorType.Current)
            '---------------------------------------------------------------------------------------------------------------------------------------------------------------
            LoadData_For_Copy(txtCopySalaryCode.Value, NavigatorType.Current)
            txtCode.Value = ""
            Me.txtRevisionNo.Text = clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no),0)+1) AS revision_no from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & Me.txtEmpCode.Value & "'").Rows(0).Item("revision_no")
            Me.dtpApplicableFrom.Value = clsCommon.GETSERVERDATE
            btnsave.Text = "Save"
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnPost.Enabled = True
            isNewEntry = True
            UsLock1.Status = ERPTransactionStatus.Pending
        End If
    End Sub

    Sub Fill_salary_struct_for_Copy_Button(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj1 As clsMapPayHeadsToSalaStructure
        obj1 = clsMapPayHeadsToSalaStructure.GetData(strCode, NavTyep)
        If (obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.SALARY_STRUCTURE_CODE) > 0) Then
            Dim ii As Int16 = 0
            LoadGridColumns()
            lblSalStructName.Text = obj1.SALARY_STRUCTURE_NAME
            If (clsMapPayHeadsToSalaStructure.ObjList IsNot Nothing AndAlso clsMapPayHeadsToSalaStructure.ObjList.Count > 0) Then
                For Each obj As clsMapPayHeadsToSalaStructure In clsMapPayHeadsToSalaStructure.ObjList
                    gvSalary.Rows.AddNew()
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = obj.LINE_NO
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                    'gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj.PAYHEAD_FORMULA
                    'gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colRateAmount).Value = obj.RATE_AMOUNT
                    'gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
                Next
            Else
                gvSalary.Rows.AddNew()
            End If
        End If
    End Sub

    Public Sub LoadData_For_Copy(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        isInsideLoadData = True
        'funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsEmployeeSalary.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.SALARY_STRUCT_CODE) > 0) Then

            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                isPosted = obj.POSTED
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
                isPosted = obj.POSTED
            End If
            Dim ii As Int16 = 0
            ' LoadGridColumns()
            txtCode.Value = obj.EMP_SAL_CODE
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.EMP_NAME)
            If clsCommon.myLen(txtSalaryStruct.Value) <= 0 Then
                txtSalaryStruct.Value = clsCommon.myCstr(obj.SALARY_STRUCT_CODE)
                txtSalaryStruct.Text = clsCommon.myCstr(obj.SALARY_STRUCT_NAME)
                lblSalStructName.Text = clsCommon.myCstr(obj.SALARY_STRUCT_NAME)
            End If
            txtRevisionNo.Text = clsCommon.myCdbl(obj.REVISION_NO)
            dtpApplicableFrom.Value = obj.APPLICABLE_FROM
            If (clsEmployeeSalary.ObjList IsNot Nothing AndAlso clsEmployeeSalary.ObjList.Count > 0) Then
                For Each obj1 As clsEmpSalaryPayHeadDetails In clsEmployeeSalary.ObjList
                    'gvSalary.Rows.AddNew()

                    'gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = obj1.Line_No
                    'gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadCode).Value = obj1.PayHeadCode
                    'gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadName).Value = obj1.PayHeadName
                    For iii As Integer = 0 To gvSalary.Rows.Count - 1
                        If clsCommon.myCstr(gvSalary.Rows(iii).Cells(colpayHeadCode).Value) = obj1.PayHeadCode Then
                            gvSalary.Rows(iii).Cells(colPayHeadFormula).Value = obj1.Formula
                            gvSalary.Rows(iii).Cells(colRateAmount).Value = obj1.Rate_Amount
                            gvSalary.Rows(iii).Cells(colHiddenComponent).Value = obj1.IsHiddenComponent
                            gvSalary.Rows(iii).Cells(colMax_Amount).Value = obj1.MAX_AMOUNT
                        End If
                    Next
                    ' gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj1.Formula
                    ' gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colRateAmount).Value = obj1.Rate_Amount
                    ' gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colHiddenComponent).Value = obj1.IsHiddenComponent
                    ' gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colMax_Amount).Value = obj1.MAX_AMOUNT

                Next
            Else
                gvSalary.Rows.AddNew()
            End If
        End If
        gvSalary.Rows.AddNew()
        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = "TOTAL"
        setGridTotalRate()
        isInsideLoadData = False
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