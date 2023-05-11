Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmAllowanceDetails
    Inherits FrmMainTranScreen
    Const colempCode As String = "empCode"
    Const colempName As String = "colempName"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colAllowanceAmount As String = "AllowanceAmount"
    Const colCheck As String = "Check"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo

    Dim obj As New clsAllowanceDetails
    Private ObjList As New List(Of clsAllowancePayHeadDetails)
    Private isCellValueChangedOpen As Boolean = False
    Dim dtpFrom As Date
    Dim dtpTo As Date

    Sub LoadGridColumns()
        gvAllowance.Rows.Clear()
        gvAllowance.Columns.Clear()
        Dim Check As New GridViewCheckBoxColumn
        Dim empCode As New GridViewTextBoxColumn()
        Dim empName As New GridViewTextBoxColumn()
        Dim payHeadCode As New GridViewTextBoxColumn
        Dim payHeadName As New GridViewTextBoxColumn
        Dim allowanceamount As New GridViewDecimalColumn

        Check.FormatString = ""
        Check.Name = colCheck
        Check.Width = 50
        Check.ReadOnly = False
        Check.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAllowance.Columns.Add(Check)

        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = colempCode
        empCode.Width = 100
        empCode.ReadOnly = False
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = colempName
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(empName)

        payHeadCode.FormatString = ""
        payHeadCode.HeaderText = "Pay Head Code"
        payHeadCode.Name = colpayHeadCode
        payHeadCode.Width = 100
        'payHeadCode.ReadOnly = True
        payHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(payHeadCode)

        payHeadName.FormatString = ""
        payHeadName.HeaderText = "Pay Head Name"
        payHeadName.Name = colpayHeadName
        payHeadName.Width = 100
        payHeadName.ReadOnly = True
        payHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(payHeadName)


        allowanceamount.FormatString = ""
        allowanceamount.HeaderText = "Allowance Amount"
        allowanceamount.Name = colAllowanceAmount
        allowanceamount.Width = 100
        allowanceamount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAllowance.Columns.Add(allowanceamount)

        gvAllowance.EnableFiltering = True
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
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
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        btnPost.Enabled = False

    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAllowanceDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
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
        'txtAdjustBy.Value = Nothing
        findPayperiod.Value = Nothing
        txtBranch.Value = Nothing
        txtDescription.Text = ""
        lblTotRAmt1.Text = ""
        lblAllowanceByName.Text = ""
        lblLocationDesc.Text = ""
        lblPayPeriodName.Text = ""
        txtDescription.Text = ""
        dtpAllowanceDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = False
        Me.gvAllowance.Rows.Clear()
        Me.gvAllowance.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dblTotalDocAmt As Decimal = 0
        txtCode.MyReadOnly = True
        ' btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsAllowanceDetails.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ALLOWANCE_CODE) > 0) Then
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
            txtCode.Value = obj.ALLOWANCE_CODE
            findPayperiod.Value = obj.PAY_PERIOD_CODE
            dtpAllowanceDate.Value = obj.ALLOWANCE_DATE
            'txtAdjustBy.Value = obj.ADJUSTMENT_BY_Code
            'lblAdjustmentByName.Text = obj.ADJUSTMENT_BY_Name
            txtBranch.Value = clsCommon.myCstr(obj.LOCATION_CODE)
            lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
            txtDescription.Text = obj.ALLOWANCE_REMARKS
            lblPayPeriodName.Text = obj.PAY_PERIOD_NAME
            findAllowancegiveby.Value = obj.ALLOWANCE_BY_CODE
            lblAllowanceByName.Text = obj.ALLOWANCE_BY_NAME
            gvAllowance.Rows.Clear()
            gvAllowance.Rows.AddNew()
            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objTr As clsAllowancePayHeadDetails In obj.Arr
                    gvAllowance.CurrentRow.Cells(colCheck).Value = True
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempCode).Value = objTr.empCode
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempName).Value = objTr.empName
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colpayHeadCode).Value = objTr.PayHeadCode
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colpayHeadName).Value = objTr.PayHeadName
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colAllowanceAmount).Value = objTr.ALLOWANCE_AMOUNT
                    dblTotalDocAmt = dblTotalDocAmt + clsCommon.myCdbl(gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colAllowanceAmount).Value)
                    gvAllowance.Rows.AddNew()
                Next
                lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2)
            Else
                gvAllowance.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_ALLOWANCE where ALLOWANCE_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select ALLOWANCE_CODE as Code, PAY_PERIOD_CODE, EMP_CODE AS 'Employee Name',ALLOWANCE_REMARKS from TSPL_ALLOWANCE "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_ALLOWANCE", qry, "Code", "", txtCode.Value, "ALLOWANCE_CODE", isButtonClicked)
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
        Try

            If AllowToSave() Then
                Dim obj As New clsAllowanceDetails
                Dim objTr As clsAllowancePayHeadDetails
                ObjList = New List(Of clsAllowancePayHeadDetails)

                obj.ALLOWANCE_DATE = Me.dtpAllowanceDate.Value
                obj.ALLOWANCE_REMARKS = Me.txtDescription.Text
                obj.ALLOWANCE_BY_CODE = findAllowancegiveby.Value
                obj.LOCATION_CODE = clsCommon.myCstr(txtBranch.Value)
                obj.ALLOWANCE_CODE = txtCode.Value
                obj.PAY_PERIOD_CODE = findPayperiod.Value
                For Each grow As GridViewRowInfo In gvAllowance.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 And clsCommon.myCdbl(grow.Cells(colAllowanceAmount).Value) Then
                        objTr = New clsAllowancePayHeadDetails
                        objTr.ALLOWANCE_CODE = txtCode.Value
                        objTr.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                        objTr.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                        objTr.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                        objTr.ALLOWANCE_AMOUNT = clsCommon.myCdbl(grow.Cells(colAllowanceAmount).Value)

                        ObjList.Add(objTr)
                    End If
                Next
                obj.Arr = ObjList

                If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                    LoadData(obj.ALLOWANCE_CODE, NavigatorType.Current)
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_ALLOWANCE where ALLOWANCE_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
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
        For Each grow As GridViewRowInfo In gvAllowance.Rows
            If clsCommon.myCBool(grow.Cells(colCheck).Value) = True Then
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow("Fill Employee code at Line No " & (ii + 1) & " ")
                    Return False
                End If

                If clsCommon.myLen(grow.Cells(colpayHeadCode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Fill PayHead code at Line No " & (ii + 1) & "")
                    Return False

                End If

                If clsCommon.myCdbl(grow.Cells(colAllowanceAmount).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Allowance Amount at Line No " & (ii + 1) & " is zero.")
                    Return False
                End If

                'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then

                '    If clsCommon.myCdbl(grow.Cells(colAllowanceAmount).Value) > 0 And clsCommon.myLen(grow.Cells(colpayHeadCode).Value) > 0 Then
                '        'clsCommon.MyMessageBoxShow("Deduction Amount at Line No " & (grow.Index) & " is zero.")
                '        'Return False
                '        ii += 1
                '    End If
                '    'ObjList.Add(obj)
                'End If
                ii += 1
            End If
        Next
        If ii = 0 Then
            clsCommon.MyMessageBoxShow("Please select atlest one check box ")
            Return False
        End If
        If ii <= 0 Then
            clsCommon.MyMessageBoxShow("Allowance Amount is zero in all rows.")
            Return False
        End If
        Return True
    End Function
    '    Dim p As Int16 = 0
    '    For Each grow As GridViewRowInfo In gvAllowance.Rows
    '        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
    '            p += 1
    '            'If clsCommon.myCdbl(grow.Cells(colAllowanceAmount).Value) = 0 Then
    '            '    Return False
    '            'End If
    '            'ObjList.Add(obj)
    '        End If
    '    Next
    '    If p <= 0 Then
    '        myMessages.blankValue("Employee Code")
    '        Return False
    '    End If

    '    Dim arrICode As New List(Of String)()
    '    For ii As Integer = 0 To gvAllowance.Rows.Count - 1
    '        If clsCommon.myLen(gvAllowance.Rows(ii).Cells(colpayHeadCode).Value) <= 0 Then
    '            Continue For
    '        End If
    '        Dim strICode As String = clsCommon.myCstr(gvAllowance.Rows(ii).Cells(colpayHeadCode).Value)
    '        Dim strIName As String = clsCommon.myCstr(gvAllowance.Rows(ii).Cells(colpayHeadName).Value)
    '        If clsCommon.myLen(colpayHeadCode) > 0 Then
    '            For jj As Integer = 0 To gvAllowance.Rows.Count - 1
    '                If (ii = jj) Then
    '                    Continue For
    '                End If
    '                If (clsCommon.CompairString(strICode, clsCommon.myCstr(gvAllowance.Rows(jj).Cells(colpayHeadCode).Value)) = CompairStringResult.Equal) Then
    '                    common.clsCommon.MyMessageBoxShow("Already selected Pay Head " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
    '                    Return False
    '                End If
    '            Next
    '            If clsCommon.myCdbl(gvAllowance.Rows(ii).Cells(colAllowanceAmount).Value) <= 0 Then
    '                clsCommon.MyMessageBoxShow("Allowance Amount can not be 0 in Row No " + (ii + 1).ToString())
    '                Return False
    '            End If
    '            If Not arrICode.Contains(strICode) Then
    '                arrICode.Add(strICode)
    '            End If
    '        End If
    '    Next


    '    Return True
    'End Function
    Sub OpenEmpList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtBranch.Value) > 0 Then
            Dim qry As String = "select TSPL_EMPLOYEE_STATUS.EMP_CODE as [Code] ,Emp_Name,TSPL_EMPLOYEE_MASTER.Designation,TSPL_EMPLOYEE_MASTER.PF_NO as [PF No]  from TSPL_EMPLOYEE_STATUS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_EMPLOYEE_STATUS.EMP_CODE"
            Dim whrCls As String = "WORKING_STATUS='Working' and TSPL_EMPLOYEE_STATUS.Location_Code='" & txtBranch.Value & "' and 2=(case when  TSPL_EMPLOYEE_MASTER.RELIEVING_DATE is null then (case when  len( TSPL_EMPLOYEE_MASTER.Joining_date) <=0 then 3 else (case when convert(date,TSPL_EMPLOYEE_MASTER.Joining_date,103) <=convert(date,'" + dtpTo + "',103)  then 2 else 3 end) end) else (case when  (convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) >=convert(date,'" + dtpTo + "',103)  or convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) between convert(date,'" + dtpFrom + "',103)  and convert(date,'" + dtpTo + "',103)  ) then 2 else 3 end) end)"
            gvAllowance.CurrentRow.Cells(colempCode).Value = clsCommon.ShowSelectForm("fndnder21", qry, "Code", whrCls, clsCommon.myCstr(gvAllowance.CurrentRow.Cells(colempCode).Value), "Code", isButtonClick)
            gvAllowance.CurrentRow.Cells(colempName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" & clsCommon.myCstr(gvAllowance.CurrentRow.Cells(colempCode).Value) & "'"))
        End If
    End Sub

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAllowance.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvAllowance.Columns(colpayHeadCode) Then
                'Dim strq As String

                Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gvAllowance.CurrentRow.Cells(colpayHeadCode).Value), False, "ISEARNING=1")
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
                    gvAllowance.CurrentRow.Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvAllowance.CurrentRow.Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                    'If clsCommon.myLen(txtEmpCode.Value) > 0 Then
                    '    gvAllowance.CurrentRow.Cells(colempCode).Value = clsCommon.myCstr(txtEmpCode.Value)
                    'End If
                End If
            ElseIf e.Column Is gvAllowance.Columns(colempCode) Then
                OpenEmpList(False)


            End If

            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
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
                If (clsAllowanceDetails.DeleteData(txtCode.Value)) Then
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
    'Private Sub txtEmpCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
    '    txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtEmpCode.Value, "", isButtonClicked)
    '    Dim clsemp As clsEmployeeMaster
    '    clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
    '    lblEmpName.Text = clsemp.Emp_Name

    'End Sub

    Private Sub findAllowancegiveby__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findAllowancegiveby._MYValidating
        Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        findAllowancegiveby.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findAllowancegiveby.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(findAllowancegiveby.Value, Nothing)
        lblAllowanceByName.Text = clsemp.Emp_Name

    End Sub

    Private Sub findPayperiod__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
        & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        findPayperiod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", findPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        If clsCommon.myLen(findPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
            lblPayPeriodName.Text = clspp.Name
            dtpFrom = clspp.DATE_FROM
            dtpTo = clspp.DATE_TO
        Else
            lblPayPeriodName.Text = ""
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If SavingData(True) Then
                    If (clsAllowanceDetails.PostData(txtCode.Value, True)) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Posted")
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        '' import 
        gvAllowance.Rows.Clear()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Employee Code", "Employee Name", "Pay Head Code", "Pay Head Name", "Allowance Amount") Then
            'Dim trans As SqlTransaction
            Try
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    gvAllowance.Rows.AddNew()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    'If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                    '    Throw New Exception("Employee Code at line no " & (grow.Index + 1) & " can not be blank or incorrect.")
                    'End If
                    If clsCommon.myLen(clsEmployeeMaster.CheckExistence(strCode, Nothing)) <= 0 Then
                        Throw New Exception("Employee Code " & strCode & " at line no " & (grow.Index + 1) & " does not exist.")
                    End If
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempCode).Value = strCode
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempName).Value = clsCommon.myCstr(grow.Cells("Employee Name").Value)
                    strCode = clsCommon.myCstr(grow.Cells("Pay Head Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Pay Head Code can not be blank or incorrect.")
                    End If

                    If clsPayHeadDefinitions.CheckNewEntry(strCode) = True Then
                        Throw New Exception("Pay Head Code " & strCode & " at line no " & (grow.Index + 1) & " does not exist.")
                    End If

                    If clsPayHeadDefinitions.GetPayHeadEarningDeductionType(strCode) = 0 Then
                        Throw New Exception("Pay Head Code at line no " & (grow.Index + 1) & " is Deduction type.")
                    End If
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colpayHeadCode).Value = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Pay Head Name").Value)
                    If strCode.Length > 100 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colpayHeadName).Value = strCode

                    Dim Amt As Decimal
                    Amt = clsCommon.myCdbl(grow.Cells("Allowance Amount").Value)
                    If Amt <= 0 Then
                        Throw New Exception("Allowance Amount at line no " & (grow.Index + 1) & " must be greater than zero.")
                    End If
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colAllowanceAmount).Value = Amt
                Next
                clsCommon.ProgressBarHide()
                If Save() = True Then
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        '' export balnk sheet
        'Dim str As String
        'str = " select '' as [Employee Code],'' as [Pay Head Code],'' as [Pay Head Name],0 as [Allowance Amount]"
        'transportSql.ExporttoExcel(str, Me)
        Dim LocCode As String = String.Empty
        Dim DivCode As String = String.Empty
        Dim str As String = String.Empty
        Dim qry As String = String.Empty
        Dim Divqry As String = String.Empty
        Dim WhrCls As String = String.Empty
        Dim DivWhrCls As String = String.Empty
        Dim LocWhrCls As String = String.Empty
        Dim dtgv As New DataTable
        Dim DTLoc As New DataTable

        qry = " SELECT location_code As Code,Location_Desc As [Description],Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As [Address],Division_Code AS [Division Code],Division_Name AS [Division Name],City_Code As [City Code],State ,Pin_Code AS [Pin Code],Location_Type AS [Location Type],Loc_Status AS [Loc Status],Loc_Segment_Code As [Loc Segment Code] FROM TSPL_location_master "
        LocCode = clsCommon.ShowSelectForm("Loc", qry, "Code", "Location_Type ='Physical'", LocCode, "Code", True)

        If clsCommon.myLen(LocCode) > 0 Then


            'str = " select '' as [Employee Code],'' as [Employee Name],'' as [Pay Head Code],'' as [Pay Head Name],0 as [Deduction Amount]"
            str = " select TSPL_EMPLOYEE_MASTER.Emp_Code as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],'' as [Pay Head Code],'' as [Pay Head Name],0 as [Allowance Amount] FROM TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code "
            LocWhrCls = str + " Where  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' "
            DTLoc = clsDBFuncationality.GetDataTable(LocWhrCls)
            If DTLoc IsNot Nothing AndAlso DTLoc.Rows.Count > 0 Then

                Divqry = " SELECT DISTINCT ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') AS [Code],ISNULL(TSPL_DEVISION_MASTER.DEVISION_NAME,'') AS [Division Name] FROM TSPL_EMPLOYEE_MASTER " & _
                      " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code " & _
                      " LEFT OUTER JOIN TSPL_DEVISION_MASTER ON TSPL_DEVISION_MASTER.DEVISION_CODE = TSPL_EMPLOYEE_MASTER.DEVISION_CODE "

                DivWhrCls = Divqry + " Where  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' AND LEN( ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'')) >0 "

                dtgv = clsDBFuncationality.GetDataTable(DivWhrCls)

                If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then

                    DivCode = clsCommon.ShowSelectForm("LocDiv", Divqry, "Code", "  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' AND LEN( ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'')) >0 ", DivCode, "Code", True)

                    If clsCommon.myLen(DivCode) > 0 Then
                        'WhrCls = " AND ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') ='" & DivCode & "' "
                        WhrCls = " AND ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') ='" & DivCode & "'  AND (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')))) "
                    Else
                        clsCommon.MyMessageBoxShow("First select division code.")
                        Return
                    End If
                Else
                    ' KUNAL > TICKET : BM00000009910 > DATE : 3 - OCTOBER - 2016
                    WhrCls = " AND (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')))) "

                End If
                transportSql.ExporttoExcel(str, " AND TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "'" & WhrCls & "", Me)
            Else
                clsCommon.MyMessageBoxShow("No data found to export")
            End If
        Else
            clsCommon.MyMessageBoxShow("First select location code.")
        End If
    End Sub

    Private Sub gvAllowance_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvAllowance.CurrentColumnChanged
        If gvAllowance.RowCount > 0 Then
            Dim intCurrRow As Integer = gvAllowance.CurrentRow.Index
            If intCurrRow = gvAllowance.Rows.Count - 1 Then
                gvAllowance.Rows.AddNew()
                gvAllowance.CurrentRow = gvAllowance.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub txtBranch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBranch._MYValidating
        txtBranch.Value = clsLocation.getFinder("Location_Type='Physical'", Me.txtBranch.Value, isButtonClicked)
        lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
    End Sub
    Sub FillEmployeeGrid()
        If clsCommon.myLen(txtBranch.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Location.")
            txtBranch.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Pay Period.")
            findPayperiod.Focus()
            Exit Sub
        End If

        gvAllowance.Rows.Clear()
        Dim strq As String = ""
        strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
  & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
  & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
  & " select TSPL_EMPLOYEE_STATUS.EMP_CODE,MAX(TSPL_EMPLOYEE_STATUS.EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(TSPL_EMPLOYEE_STATUS.REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_EMPLOYEE_STATUS.EMP_CODE  " _
  & " WHERE TSPL_EMPLOYEE_STATUS.WORKING_STATUS='Working' and TSPL_EMPLOYEE_STATUS.Location_Code='" & txtBranch.Value & "'  and 2=(case when  TSPL_EMPLOYEE_MASTER.RELIEVING_DATE is null then (case when  len( TSPL_EMPLOYEE_MASTER.Joining_date) <=0 then 3 else (case when convert(date,TSPL_EMPLOYEE_MASTER.Joining_date,103) <=convert(date,'" + dtpTo + "',103)  then 2 else 3 end) end) else (case when  (convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) >=convert(date,'" + dtpTo + "',103)  or convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) between convert(date,'" + dtpFrom + "',103)  and convert(date,'" + dtpTo + "',103)  ) then 2 else 3 end) end) GROUP BY TSPL_EMPLOYEE_STATUS.EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
  & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE "

        'Dim cond As String
        'cond = " (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') " _
        '       & " AND TT3.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_MONTHLY_ATTENDANCE_DETAIL T1 " _
        '       & " JOIN TSPL_monthly_ATTENDANCE T2 ON T1.MTA_CODE=T2.MTA_CODE WHERE T2.PAY_PERIOD_CODE='" & Me.findPayperiod.Value & "')"
        'strq = strq & " where " & cond
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)
        For Each drEmp As DataRow In dt.Rows
            gvAllowance.Rows.AddNew()
            'gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colsno).Value = gvDeduction.Rows.Count
            gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempCode).Value = drEmp.Item("Code")
            gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempName).Value = drEmp.Item("Name")
            'gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(col).Value = clsCommon.myCdbl(txtPayPeriodDays.Text)

        Next

    End Sub

    Private Sub txtGo_Click(sender As Object, e As EventArgs) Handles txtGo.Click
        FillEmployeeGrid()
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvAllowance.Rows
                grow.Cells(colCheck).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gvAllowance.Rows
                grow.Cells(colCheck).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
 
End Class