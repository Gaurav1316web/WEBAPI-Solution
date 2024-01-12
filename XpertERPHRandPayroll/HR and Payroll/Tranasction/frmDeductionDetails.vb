Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmDeductionDetails
    Inherits FrmMainTranScreen
    Const colCheck As String = "Check"
    Const colempCode As String = "empCode"
    Const colempName As String = "empName"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colDeductionAmount As String = "DeductionAmount"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsDeductionDetails
    Private ObjList As New List(Of clsDeductionPayHeadDetails)
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim dtpFrom As Date
    Dim dtpTo As Date

    Sub LoadGridColumns()
        gvDeduction.Rows.Clear()
        gvDeduction.Columns.Clear()
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
        gvDeduction.Columns.Add(Check)

        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = colempCode
        empCode.Width = 100
        empCode.ReadOnly = False
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = colempName
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(empName)

        payHeadCode.FormatString = ""
        payHeadCode.HeaderText = "Pay Head Code"
        payHeadCode.Name = colpayHeadCode
        payHeadCode.Width = 100
        'payHeadCode.ReadOnly = True
        payHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(payHeadCode)

        payHeadName.FormatString = ""
        payHeadName.HeaderText = "Pay Head Name"
        payHeadName.Name = colpayHeadName
        payHeadName.Width = 100
        payHeadName.ReadOnly = True
        payHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(payHeadName)


        allowanceamount.FormatString = ""
        allowanceamount.HeaderText = "Deduction Amount"
        allowanceamount.Name = colDeductionAmount
        allowanceamount.Width = 100
        allowanceamount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(allowanceamount)

        gvDeduction.EnableFiltering = True
    End Sub

    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            txtBranch.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
        Else
            txtBranch.Value = ""
            lblLocationDesc.Text = ""
        End If
        findAllowancegiveby.Value = ""
        txtDescription.Text = ""
        lblDedByName.Text = ""
        lblPayPeriodName.Text = ""
        dtpDeductionDate.Value = clsCommon.GETSERVERDATE
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = False
        LblTotal.Text = 0
        Me.gvDeduction.Rows.Clear()
        Me.gvDeduction.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            txtCode.MyReadOnly = True
            obj = clsDeductionDetails.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DEDUCTION_CODE) > 0) Then
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
                txtCode.Value = obj.DEDUCTION_CODE
                findPayperiod.Value = obj.PAY_PERIOD_CODE
                txtBranch.Value = clsCommon.myCstr(obj.LOCATION_CODE)
                lblLocationDesc.Text = clsLocation.GetName(obj.LOCATION_CODE, Nothing)
                txtDescription.Text = obj.DEDUCTION_REMARKS
                lblPayPeriodName.Text = obj.PAY_PERIOD_NAME
                findAllowancegiveby.Value = obj.DEDUCTION_BY_CODE
                lblDedByName.Text = obj.DEDUCTION_BY_NAME
                dtpDeductionDate.Value = obj.DEDUCTION_DATE
                gvDeduction.Rows.Clear()
                gvDeduction.Rows.AddNew()
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objTr As clsDeductionPayHeadDetails In obj.Arr
                        gvDeduction.CurrentRow.Cells(colCheck).Value = True
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colempCode).Value = objTr.empCode
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colempName).Value = objTr.EMP_NAME 'clsEmployeeMaster.GetName(objTr.empCode, Nothing)
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colpayHeadCode).Value = objTr.PayHeadCode
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colpayHeadName).Value = objTr.PayHeadName
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colDeductionAmount).Value = objTr.DEDUCTION_AMOUNT
                        gvDeduction.Rows.AddNew()
                    Next
                Else
                    gvDeduction.Rows.AddNew()
                End If

                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_DEDUCTION where DEDUCTION_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select DEDUCTION_CODE as Code, PAY_PERIOD_CODE,DEDUCTION_REMARKS from TSPL_DEDUCTION "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_DEDUCTION", qry, "Code", whrcls, txtCode.Value, "DEDUCTION_CODE", isButtonClicked)
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
                Dim obj As New clsDeductionDetails
                Dim objTr As clsDeductionPayHeadDetails
                ObjList = New List(Of clsDeductionPayHeadDetails)

                obj.DEDUCTION_DATE = Me.dtpDeductionDate.Value
                obj.DEDUCTION_REMARKS = Me.txtDescription.Text
                obj.DEDUCTION_BY_CODE = findAllowancegiveby.Value
                obj.LOCATION_CODE = clsCommon.myCstr(txtBranch.Value)
                obj.DEDUCTION_CODE = txtCode.Value
                obj.PAY_PERIOD_CODE = findPayperiod.Value

                For Each grow As GridViewRowInfo In gvDeduction.Rows
                    If grow.Cells(colCheck).Value = True Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 And clsCommon.myLen(grow.Cells(colpayHeadCode).Value) > 0 And clsCommon.myCdbl(grow.Cells(colDeductionAmount).Value) > 0 Then
                            objTr = New clsDeductionPayHeadDetails
                            objTr.DEDUCTION_CODE = txtCode.Value
                            objTr.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                            objTr.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                            objTr.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                            objTr.DEDUCTION_AMOUNT = clsCommon.myCdbl(grow.Cells(colDeductionAmount).Value)
                            ObjList.Add(objTr)
                        End If
                    End If

                Next
                obj.Arr = ObjList
                If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                    LoadData(obj.DEDUCTION_CODE, NavigatorType.Current)
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        '===============update by preeti gupta Against Ticket no [BM00000008223]
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_DEDUCTION where DEDUCTION_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
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
        For Each grow As GridViewRowInfo In gvDeduction.Rows
            If clsCommon.myCBool(grow.Cells(colCheck).Value) = True Then
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill Employee code at Line No " & (ii + 1) & " ", Me.Text)
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colpayHeadCode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill PayHead code at Line No " & (ii + 1) & "", Me.Text)
                    Return False

                End If
                If clsCommon.myCdbl(grow.Cells(colDeductionAmount).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Deduction Amount at Line No " & (ii + 1) & " is zero.", Me.Text)
                    Return False
                End If
                ii += 1
            End If
        Next
        If ii = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atlest one check box ", Me.Text)
            Return False
        End If
        If ii <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Deduction Amount is zero in all rows.", Me.Text)
            Return False
        End If
        Return True
    End Function
    Sub OpenEmpList(ByVal isButtonClick As Boolean)

        If clsCommon.myLen(txtBranch.Value) > 0 Then
            Dim qry As String = "select TSPL_EMPLOYEE_STATUS.EMP_CODE as [Code] ,Emp_Name,TSPL_EMPLOYEE_MASTER.Designation,TSPL_EMPLOYEE_MASTER.PF_NO as [PF No]  from TSPL_EMPLOYEE_STATUS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_EMPLOYEE_STATUS.EMP_CODE"
            Dim whrCls As String = "WORKING_STATUS='Working' and TSPL_EMPLOYEE_STATUS.Location_Code='" & txtBranch.Value & "'and 2=(case when  TSPL_EMPLOYEE_MASTER.RELIEVING_DATE is null then (case when  len( TSPL_EMPLOYEE_MASTER.Joining_date) <=0 then 3 else (case when convert(date,TSPL_EMPLOYEE_MASTER.Joining_date,103) <=convert(date,'" + dtpTo + "',103)  then 2 else 3 end) end) else (case when  (convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) >=convert(date,'" + dtpTo + "',103)  or convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) between convert(date,'" + dtpFrom + "',103)  and convert(date,'" + dtpTo + "',103)  ) then 2 else 3 end) end)"
            gvDeduction.CurrentRow.Cells(colempCode).Value = clsCommon.ShowSelectForm("fndnder32", qry, "Code", whrCls, clsCommon.myCstr(gvDeduction.CurrentRow.Cells(colempCode).Value), "Code", isButtonClick)
            gvDeduction.CurrentRow.Cells(colempName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" & clsCommon.myCstr(gvDeduction.CurrentRow.Cells(colempCode).Value) & "'"))
        End If
    End Sub
    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDeduction.CellEndEdit
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvDeduction.Columns(colpayHeadCode) Then

                Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gvDeduction.CurrentRow.Cells(colpayHeadCode).Value), False, "ISEARNING =0")
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
                    gvDeduction.CurrentRow.Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvDeduction.CurrentRow.Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                    'gvDeduction.CurrentRow.Cells(colempCode).Value = clsCommon.myCstr(txtEmpCode.Value)
                    'gvDeduction.CurrentRow.Cells(colempName).Value = clsEmployeeMaster.GetName(gvDeduction.CurrentRow.Cells(colempCode).Value, Nothing)

                End If
            ElseIf e.Column Is gvDeduction.Columns(colempCode) Then
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
                If (clsDeductionDetails.DeleteData(txtCode.Value)) Then
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

    'Private Sub txtEmpCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
    '    txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtEmpCode.Value, "", isButtonClicked)
    '    Dim clsemp As clsEmployeeMaster
    '    clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
    '    lblEmpName.Text = clsemp.Emp_Name

    'End Sub

    Private Sub findAllowancegiveby__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findAllowancegiveby._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        findAllowancegiveby.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", whrcls, findAllowancegiveby.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(findAllowancegiveby.Value, Nothing)
        If Not clsemp Is Nothing Then
            lblDedByName.Text = clsemp.Emp_Name
        Else
            lblDedByName.Text = ""
        End If
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
                    If (clsDeductionDetails.PostData(txtCode.Value, True)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        '' import 
        gvDeduction.Rows.Clear()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Employee Code", "Employee Name", "Pay Head Code", "Pay Head Name", "Deduction Amount") Then
            'Dim trans As SqlTransaction
            Try
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                ''RICHA 
                isInsideLoadData = True
                ''--------------------
                For Each grow As GridViewRowInfo In gv.Rows
                    gvDeduction.Rows.AddNew()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    'If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                    '    Throw New Exception("Employee Code at line no " & (grow.Index + 1) & " can not be blank or incorrect.")
                    'End If
                    If clsCommon.myLen(clsEmployeeMaster.CheckExistence(strCode, Nothing)) <= 0 Then
                        Throw New Exception("Employee Code " & strCode & " at line no " & (grow.Index + 1) & " does not exist.")
                    End If
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colempCode).Value = strCode
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colempName).Value = clsCommon.myCstr(grow.Cells("Employee Name").Value)

                    strCode = clsCommon.myCstr(grow.Cells("Pay Head Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Pay Head Code at line no " & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsPayHeadDefinitions.CheckNewEntry(strCode) = True Then
                        Throw New Exception("Pay Head Code at line no " & (grow.Index + 1) & " does not exist.")
                    End If
                    If clsPayHeadDefinitions.GetPayHeadEarningDeductionType(strCode) = 1 Then
                        Throw New Exception("Pay Head Code at line no " & (grow.Index + 1) & " is Earning type.")
                    End If
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colpayHeadCode).Value = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Pay Head Name").Value)
                    If strCode.Length > 100 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colpayHeadName).Value = strCode



                    Dim Amt As Decimal
                    Amt = clsCommon.myCdbl(grow.Cells("Deduction Amount").Value)
                    If Amt <= 0 Then
                        Throw New Exception("Deduction Amount at line no " & (grow.Index + 1) & " must be greater than zero.")
                    End If
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colDeductionAmount).Value = Amt
                Next
                clsCommon.ProgressBarHide()
                ''RICHA -------------
                isInsideLoadData = False
                UpdateAllTotals()
                ''---------------------------
                If Save() = True Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                isInsideLoadData = False
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        '' export balnk sheet
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

        qry = " SELECT location_code As Code,Location_Desc As [Description],Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As [Address],Division_Code AS [Division Code],Division_Name AS [Division Name],City_Code As [City Code],State ,Pin_Code AS [Pin Code],Location_Type AS [Location Type],Loc_Status AS [Loc Status],Loc_Segment_Code As [Loc Segment Code] FROM TSPL_location_master  "
        LocCode = clsCommon.ShowSelectForm("Loc", qry, "Code", "Location_Type ='Physical'", LocCode, "Code", True)

        If clsCommon.myLen(LocCode) > 0 Then


            'str = " select '' as [Employee Code],'' as [Employee Name],'' as [Pay Head Code],'' as [Pay Head Name],0 as [Deduction Amount]"
            str = " select TSPL_EMPLOYEE_MASTER.Emp_Code as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],'' as [Pay Head Code],'' as [Pay Head Name],0 as [Deduction Amount] FROM TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code "
            LocWhrCls = str + " Where  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' "
            DTLoc = clsDBFuncationality.GetDataTable(LocWhrCls)
            If DTLoc IsNot Nothing AndAlso DTLoc.Rows.Count > 0 Then

                Divqry = " SELECT DISTINCT ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') AS [Code],ISNULL(TSPL_DEVISION_MASTER.DEVISION_NAME,'') AS [Division Name] FROM TSPL_EMPLOYEE_MASTER " &
                      " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code " &
                      " LEFT OUTER JOIN TSPL_DEVISION_MASTER ON TSPL_DEVISION_MASTER.DEVISION_CODE = TSPL_EMPLOYEE_MASTER.DEVISION_CODE "

                DivWhrCls = Divqry + " Where  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' AND LEN( ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'')) >0 "

                dtgv = clsDBFuncationality.GetDataTable(DivWhrCls)

                If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then

                    DivCode = clsCommon.ShowSelectForm("LocDiv", Divqry, "Code", "  TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "' AND LEN( ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'')) >0 ", DivCode, "Code", True)

                    If clsCommon.myLen(DivCode) > 0 Then
                        WhrCls = " AND ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') ='" & DivCode & "' AND  (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')))) "
                    Else
                        clsCommon.MyMessageBoxShow(Me, "First select division code.", Me.Text)
                        Return
                    End If
                Else
                    ' KUNAL > TICKET : BM00000009910 > DATE : 3 - OCTOBER - 2016
                    WhrCls = " AND (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')))) "
                End If
                transportSql.ExporttoExcel(str, " AND TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "'" & WhrCls & "", Me)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "First select location code.", Me.Text)
        End If
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub



    Private Sub gvDeduction_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvDeduction.CellValueChanged
        'Try
        '    Dim Total As Double = 0

        '    If e.Column Is gvDeduction.Columns(colDeductionAmount) Then
        '        For Each grow As GridViewRowInfo In gvDeduction.Rows
        '            Total += clsCommon.myCdbl(grow.Cells(colDeductionAmount).Value)
        '            LblTotal.Text = Total
        '        Next
        '    End If

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

        ''RICHA ================================= 5 FEB,2016
        Try
            If Not isInsideLoadData Then
                If e.Column Is gvDeduction.Columns(colDeductionAmount) Then
                    UpdateAllTotals()
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''RICHA ==================5 FEB,2016
    Private Sub UpdateAllTotals()
        Try
            Dim Total As Double = 0

            For ii As Integer = 0 To gvDeduction.Rows.Count - 1
                Total = Total + clsCommon.myCdbl(gvDeduction.Rows(ii).Cells(colDeductionAmount).Value)
            Next

            LblTotal.Text = clsCommon.myFormat(Total)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''-----------------------



    Private Sub gvDeduction_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvDeduction.CurrentColumnChanged
        If gvDeduction.RowCount > 0 Then
            Dim intCurrRow As Integer = gvDeduction.CurrentRow.Index
            'gvDeduction.CurrentRow.Cells(colCheck).Value = True
            If intCurrRow = gvDeduction.Rows.Count - 1 Then
                gvDeduction.Rows.AddNew()
                gvDeduction.CurrentRow = gvDeduction.Rows(intCurrRow)

            End If
        End If
    End Sub

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

        gvDeduction.Rows.Clear()
        Dim strq As String = ""
        strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
  & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
  & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
  & " select TSPL_EMPLOYEE_STATUS.EMP_CODE,MAX(TSPL_EMPLOYEE_STATUS.EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(TSPL_EMPLOYEE_STATUS.REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_EMPLOYEE_STATUS.EMP_CODE " _
  & " WHERE TSPL_EMPLOYEE_STATUS.WORKING_STATUS='Working' and TSPL_EMPLOYEE_STATUS.Location_Code='" & txtBranch.Value & "' and 2=(case when  TSPL_EMPLOYEE_MASTER.RELIEVING_DATE is null then (case when  len( TSPL_EMPLOYEE_MASTER.Joining_date) <=0 then 3 else (case when convert(date,TSPL_EMPLOYEE_MASTER.Joining_date,103) <=convert(date,'" + dtpTo + "',103)  then 2 else 3 end) end) else (case when  (convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) >=convert(date,'" + dtpTo + "',103)  or convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) between convert(date,'" + dtpFrom + "',103)  and convert(date,'" + dtpTo + "',103)  ) then 2 else 3 end) end) GROUP BY TSPL_EMPLOYEE_STATUS.EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
  & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE "

        'Dim cond As String
        'cond = " (TT2.ATTN_REGISTER_TYPE='MONTHLY' OR TT2.ATTN_REGISTER_TYPE='MT') " _
        '       & " AND TT3.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_MONTHLY_ATTENDANCE_DETAIL T1 " _
        '       & " JOIN TSPL_monthly_ATTENDANCE T2 ON T1.MTA_CODE=T2.MTA_CODE WHERE T2.PAY_PERIOD_CODE='" & Me.findPayperiod.Value & "')"
        'strq = strq & " where " & cond
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)
        For Each drEmp As DataRow In dt.Rows
            gvDeduction.Rows.AddNew()
            'gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colsno).Value = gvDeduction.Rows.Count
            gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colempCode).Value = drEmp.Item("Code")
            gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colempName).Value = drEmp.Item("Name")
            'gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(col).Value = clsCommon.myCdbl(txtPayPeriodDays.Text)

        Next

    End Sub

    Private Sub txtGo_Click(sender As Object, e As EventArgs) Handles txtGo.Click
        FillEmployeeGrid()
    End Sub




    ''RICHA ==============================================5 FEB,2016
    'Private Sub gvDeduction_ViewRowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvDeduction.ViewRowFormatting
    '    Try
    '        Dim Total As Double = 0

    '        'If e.RowElement Is gvDeduction.Columns(colDeductionAmount) Then
    '        For Each grow As GridViewRowInfo In gvDeduction.ChildRows
    '            Total += clsCommon.myCdbl(grow.Cells(colDeductionAmount).Value)
    '            LblTotal.Text = Total
    '        Next
    '        'End If

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    ''==============================================
    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        isInsideLoadData = True
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvDeduction.Rows
                grow.Cells(colCheck).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gvDeduction.Rows
                grow.Cells(colCheck).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
        isInsideLoadData = False
    End Sub
End Class