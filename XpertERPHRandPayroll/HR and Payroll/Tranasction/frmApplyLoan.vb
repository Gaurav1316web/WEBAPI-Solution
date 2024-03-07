Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmApplyLoan
    Inherits FrmMainTranScreen
    Const colEMI_NO As String = "EMINo"
    Const colEMI_AMOUNT As String = "EMIAmount"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsApplyLoan
    Private ObjList As New List(Of clsApplyLoan)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()
        Dim EMINo As New GridViewTextBoxColumn
        Dim EMIAmount As New GridViewDecimalColumn

        gvEMI.Rows.Clear()
        gvEMI.Columns.Clear()
        EMINo.FormatString = ""
        EMINo.HeaderText = "EMI No"
        EMINo.Name = "EMINo"
        EMINo.Width = 100
        EMINo.ReadOnly = True
        EMINo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEMI.Columns.Add(EMINo)

        EMIAmount.FormatString = ""
        EMIAmount.HeaderText = "EMI Amount"
        EMIAmount.Name = "EMIAmount"
        EMIAmount.Width = 100
        EMIAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEMI.Columns.Add(EMIAmount)

    End Sub

    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            '    PostData()
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
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        cboLoanStatus.Text = "Open"
        'btnPost.Visible = False
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmApplyLoan)
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
        findLoanBy.Value = Nothing
        txtEmpCode.Value = Nothing
        txtDescription.Text = ""
        dtpLoanDate.Value = Today
        chkApplyInterest.Checked = False
        cboInterestType.SelectedIndex = -1
        txtInterestAmt.Text = 0
        cboLoanType.SelectedIndex = -1
        txtLoanAmount.Text = 0
        txtMonth.Text = 0
        txtDays.Text = 0
        dtpPaymentStartDate.Value = Today
        txtNoofEmi.Text = 0
        txtInterestAmt.Text = 0
        txtInterestRate.Text = 0
        txtTotalPayableAmount.Text = 0
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        'btnPost.Enabled = True
        Me.gvEMI.Rows.Clear()
        dtpLoanDate.Value = clsCommon.GETSERVERDATE
        lblLocationCode.Text = ""
        lblDevision.Text = ""
        lblGrossSalary.Text = ""
        dtpEndMonth.Text = Today
        cboLoanStatus.Text = "Open"
        lblEmpName.Text = ""
        fndbankcode.Value = ""
        lblBankCode.Text = ""
        'btnPost.Visible = False

        'Me.gvEMI.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        'btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsApplyLoan.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LOAN_CODE) > 0) Then
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
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            txtCode.Value = obj.LOAN_CODE

            LoadGridColumns()


            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.EMP_NAME)
            findLoanBy.Value = clsCommon.myCstr(obj.LOAN_BY_CODE)
            lblLoanBy.Text = clsCommon.myCstr(obj.LOAN_BY_NAME)
            dtpLoanDate.Value = clsCommon.myCDate(obj.LOAN_DATE)
            cboLoanType.Text = clsCommon.myCstr(obj.LOAN_TYPE)
            txtLoanAmount.Text = clsCommon.myCdbl(obj.LOAN_AMOUNT)
            txtMonth.Text = clsCommon.myCdbl(obj.PERIOD_MONTH)
            txtDays.Text = clsCommon.myCdbl(obj.PERIOD_DAY)
            dtpPaymentStartDate.Value = clsCommon.myCDate(obj.PAYMENT_STARTDATE)
            txtNoofEmi.Text = clsCommon.myCdbl(obj.NO_OF_EMI)
            chkApplyInterest.Checked = clsCommon.myCdbl(obj.INTEREST_APPLIED)
            cboInterestType.Text = clsCommon.myCstr(obj.INTEREST_TYPE)
            cboInterestPeriodicity.Text = clsCommon.myCstr(obj.INTEREST_PERIODICITY)
            txtInterestRate.Text = clsCommon.myCdbl(obj.INTEREST_RATE)
            txtInterestAmt.Text = clsCommon.myCdbl(obj.INTEREST_AMOUNT)
            txtTotalPayableAmount.Text = clsCommon.myCdbl(obj.TOTALPAYABLE_AMOUNT)
            txtDescription.Text = obj.LOAN_DESCRIPTION
            txtCode.MyReadOnly = True
            lblLocationCode.Text = obj.Location
            lblDevision.Text = obj.Division
            lblGrossSalary.Text = obj.Gross_Salary
            dtpEndMonth.Text = obj.Payment_EndDate
            cboLoanStatus.Text = obj.Loan_Status
            fndbankcode.Value = obj.Bank_code
            lblBankCode.Text = obj.Bank_Name
            If (clsApplyLoan.ObjList IsNot Nothing AndAlso clsApplyLoan.ObjList.Count > 0) Then
                For Each obj As clsApplyLoan In clsApplyLoan.ObjList
                    gvEMI.Rows.AddNew()

                    gvEMI.Rows(gvEMI.Rows.Count - 1).Cells(colEMI_NO).Value = obj.EMI_NO
                    gvEMI.Rows(gvEMI.Rows.Count - 1).Cells(colEMI_AMOUNT).Value = obj.EMI_AMOUNT

                Next
            Else
                gvEMI.Rows.AddNew()
            End If
            'Dim qry As String = "select posted  from TSPL_PAYMENT_HEADER where Loan_Code ='" & txtCode.Value & "'"
            'Dim count As Integer = clsDBFuncationality.getSingleValue(qry)
            'If count = 0 Then
            '    btnsave.Enabled = True
            '    btndelete.Enabled = True
            '    btnPost.Enabled = True
            '    UsLock1.Status = ERPTransactionStatus.Pending
            'Else
            '    btnsave.Enabled = False
            '    btnPost.Enabled = False
            '    btndelete.Enabled = False
            '    UsLock1.Status = ERPTransactionStatus.Approved
            '    cboLoanStatus.Text = "Approve"
            '    'SavingData(True)e
            '    Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            '    Dim qry1 As String = "Update TSPL_LOAN_APPLICATION set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "',Loan_Status='Approve' where LOAN_CODE ='" + txtCode.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(qry1)
            'End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_LOAN_APPLICATION where LOAN_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select LOAN_CODE as Code, EMP_CODE AS 'Employee Name',LOAN_DESCRIPTION from TSPL_LOAN_APPLICATION "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_LOAN_APPLICATION", qry, "Code", "", txtCode.Value, "LOAN_CODE", isButtonClicked)
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
            Dim obj As New clsApplyLoan
            ObjList = New List(Of clsApplyLoan)
            obj.LOAN_CODE = Me.txtCode.Value
            obj.LOAN_DATE = Format(Me.dtpLoanDate.Value, "dd MMM yyyy")
            obj.EMP_CODE = txtEmpCode.Value
            obj.LOAN_BY_CODE = clsCommon.myCstr(findLoanBy.Value)
            obj.LOAN_TYPE = Me.cboLoanType.Text
            obj.LOAN_AMOUNT = clsCommon.myCdbl(Me.txtLoanAmount.Text)
            obj.PERIOD_MONTH = clsCommon.myCdbl(Me.txtMonth.Text)
            obj.PERIOD_DAY = clsCommon.myCdbl(Me.txtDays.Text)
            obj.PAYMENT_STARTDATE = clsCommon.myCDate(Me.dtpPaymentStartDate.Value)
            obj.NO_OF_EMI = clsCommon.myCdbl(Me.txtNoofEmi.Text)
            obj.INTEREST_APPLIED = clsCommon.myCBool(Me.chkApplyInterest.Checked)
            obj.INTEREST_TYPE = clsCommon.myCstr(Me.cboInterestType.Text)
            obj.INTEREST_RATE = clsCommon.myCdbl(Me.txtInterestRate.Text)
            obj.INTEREST_PERIODICITY = clsCommon.myCstr(Me.cboInterestPeriodicity.Text)
            obj.INTEREST_AMOUNT = clsCommon.myCdbl(Me.txtInterestAmt.Text)
            obj.TOTALPAYABLE_AMOUNT = clsCommon.myCdbl(Me.txtTotalPayableAmount.Text)
            obj.LOAN_DESCRIPTION = clsCommon.myCstr(Me.txtDescription.Text)
            obj.Location = clsCommon.myCstr(Me.lblLocationCode.Text)
            obj.Division = clsCommon.myCstr(Me.lblDevision.Text)
            obj.Gross_Salary = clsCommon.myCstr(Me.lblGrossSalary.Text)
            obj.Payment_EndDate = clsCommon.myCstr(Me.dtpEndMonth.Text)
            obj.Loan_Status = clsCommon.myCstr(Me.cboLoanStatus.Text)
            obj.PAID = 1
            obj.Bank_code = clsCommon.myCstr(Me.fndbankcode.Value)
            For Each grow As GridViewRowInfo In gvEMI.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colEMI_NO).Value)) > 0 Then
                    Dim obj1 As clsApplyLoan = New clsApplyLoan()
                    obj1.EMI_NO = clsCommon.myCdbl(grow.Cells(colEMI_NO).Value)
                    obj1.EMI_AMOUNT = clsCommon.myCdbl(grow.Cells(colEMI_AMOUNT).Value)

                    ObjList.Add(obj1)
                End If
            Next
            If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                LoadData(obj.LOAN_CODE, NavigatorType.Current)
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Return True
            End If
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_LOAN_ADJUSTMENT where LOAN_CODE = '" + txtCode.Value + "' "
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

        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtEmpCode.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvEMI.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colEMI_NO).Value)) > 0 Then
                ii += 1
                If clsCommon.myCdbl(grow.Cells(colEMI_AMOUNT).Value) = 0 Then
                    Return False
                End If
                ObjList.Add(obj)
            End If
        Next
        If ObjList Is Nothing Then
            Return False
        End If
        Return True
    End Function

    Private Sub txtEmpcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Try
            Dim qry As String = "	 SELECT EMP_CODE AS Code,EMP_Name as Name,Location_Desc as Location ,DEVISION_NAME as Division FROM TSPL_EMPLOYEE_MASTER left join tspl_location_master on tspl_location_master.Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE = TSPL_EMPLOYEE_MASTER.DEVISION_CODE"
            txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtEmpCode.Value, "", isButtonClicked)
            Dim clsemp As New clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
            lblEmpName.Text = clsemp.Emp_Name
            lblLocationCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_Employee_master left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Employee_master.LOCATION_CODE where Emp_Code='" & txtEmpCode.Value & "'"))
            lblDevision.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME   from TSPL_Employee_master left join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_Employee_master.DEVISION_CODE  where Emp_Code='" & txtEmpCode.Value & "'"))
            lblGrossSalary.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum (isnull(PAYPERIOD_AMOUNT,0) ) as PAYPERIOD_AMOUNT from TSPL_EMPLOYEE_SALARY  INNER join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE =TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE  INNER JOIN  (SELECT MAX(REVISION_NO) AS REVISION_NO FROM TSPL_EMPLOYEE_SALARY where emp_code='" & txtEmpCode.Value & "' and posted='1') TT ON TT.REVISION_NO= TSPL_EMPLOYEE_SALARY.REVISION_NO where emp_code='" & txtEmpCode.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


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
                If (clsApplyLoan.DeleteData(txtCode.Value)) Then
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

    Private Sub findLoanBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findLoanBy._MYValidating
        Dim qry As String = "SELECT EMP_CODE AS Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        findLoanBy.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findLoanBy.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(findLoanBy.Value, Nothing)
        lblLoanBy.Text = clsemp.Emp_Name
    End Sub
    Sub CalculateEMI()
        If isNewEntry = False Then
            Exit Sub
        End If
        gvEMI.Rows.Clear()
        Dim InterestAmount As Decimal = 0
        Dim TotalAmount As Decimal = 0
        Dim EMI As Decimal = 0
        If Me.chkApplyInterest.Checked = False Then
            InterestAmount = 0
        Else
            If Me.cboInterestType.Text = "Simple" Then
                If cboInterestPeriodicity.Text = "Monthly" Then
                    InterestAmount = clsCommon.myCdbl(txtLoanAmount.Text) * ((clsCommon.myCdbl(txtMonth.Text) * 30 + clsCommon.myCdbl(txtDays.Text)) / 30) * clsCommon.myCdbl(txtInterestRate.Text) / 100
                ElseIf cboInterestPeriodicity.Text = "Quarterly" Then
                    InterestAmount = clsCommon.myCdbl(txtLoanAmount.Text) * ((clsCommon.myCdbl(txtMonth.Text) * 30 + clsCommon.myCdbl(txtDays.Text)) / 90) * clsCommon.myCdbl(txtInterestRate.Text) / 100
                ElseIf cboInterestPeriodicity.Text = "Yearly" Then
                    InterestAmount = clsCommon.myCdbl(txtLoanAmount.Text) * ((clsCommon.myCdbl(txtMonth.Text) * 30 + clsCommon.myCdbl(txtDays.Text)) / 365) * clsCommon.myCdbl(txtInterestRate.Text) / 100
                End If
            Else
                If cboInterestPeriodicity.Text = "Monthly" Then
                    InterestAmount = clsCommon.myCdbl(txtLoanAmount.Text) * Math.Pow((1 + (clsCommon.myCdbl(Me.txtInterestRate.Text) / 100) / 12), ((clsCommon.myCdbl(txtMonth.Text) * 30 + clsCommon.myCdbl(txtDays.Text)) / 365) * 12)
                ElseIf cboInterestPeriodicity.Text = "Quarterly" Then
                    InterestAmount = clsCommon.myCdbl(txtLoanAmount.Text) * Math.Pow((1 + (clsCommon.myCdbl(Me.txtInterestRate.Text) / 100) / 4), ((clsCommon.myCdbl(txtMonth.Text) * 30 + clsCommon.myCdbl(txtDays.Text)) / 365) * 4)
                ElseIf cboInterestPeriodicity.Text = "Yearly" Then
                    InterestAmount = clsCommon.myCdbl(txtLoanAmount.Text) * Math.Pow((1 + (clsCommon.myCdbl(Me.txtInterestRate.Text) / 100) / 1), ((clsCommon.myCdbl(txtMonth.Text) * 30 + clsCommon.myCdbl(txtDays.Text)) / 365) * 1)
                End If
            End If
        End If
        TotalAmount = clsCommon.myCdbl(Me.txtLoanAmount.Text) + InterestAmount
        EMI = TotalAmount / IIf(clsCommon.myCdbl(Me.txtNoofEmi.Text) = 0, 1, clsCommon.myCdbl(Me.txtNoofEmi.Text))

        Me.txtInterestAmt.Text = Format(InterestAmount, "###0.00")
        Me.txtTotalPayableAmount.Text = Format(TotalAmount, "###0.00")

        For intloop As Integer = 0 To Val(Me.txtNoofEmi.Text) - 1
            Me.gvEMI.Rows.Add(1)
            Me.gvEMI.Rows(intloop).Cells(colEMI_NO).Value = intloop + 1
            Me.gvEMI.Rows(intloop).Cells(colEMI_AMOUNT).Value = Format(EMI, "###0.00")
        Next
    End Sub

    Private Sub txtNoofEmi_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoofEmi.LostFocus
        CalculateEMI()
    End Sub

    Private Sub cboInterestType_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInterestType.LostFocus
        CalculateEMI()
    End Sub

    Private Sub txtInterestRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInterestRate.LostFocus
        CalculateEMI()
    End Sub

    Private Sub cboInterestPeriodicity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInterestPeriodicity.LostFocus
        CalculateEMI()
    End Sub

    Private Sub txtMonth_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.LostFocus
        Me.dtpPaymentStartDate.Value = Me.dtpLoanDate.Value.AddMonths(clsCommon.myCdbl(Me.txtMonth.Text)).AddDays(clsCommon.myCdbl(Me.txtDays.Text))
    End Sub

    Private Sub txtDays_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDays.LostFocus
        Me.dtpPaymentStartDate.Value = Me.dtpLoanDate.Value.AddMonths(clsCommon.myCdbl(Me.txtMonth.Text)).AddDays(clsCommon.myCdbl(Me.txtDays.Text))
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    '======Added by preeti Gupta Against ticket no[BHA/06/02/19-000808]
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsApplyLoan.PostData(txtCode.Value, True)) Then
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

    Private Sub findLoanBy_Load(sender As Object, e As EventArgs) Handles findLoanBy.Load

    End Sub
    Private Sub lblGivenBy_Click(sender As Object, e As EventArgs) Handles lblGivenBy.Click

    End Sub

    Private Sub txtNoofEmi_TextChanged(sender As Object, e As EventArgs) Handles txtNoofEmi.TextChanged
        If (clsCommon.myCdbl(txtNoofEmi.Text)) > 0 Then

            dtpEndMonth.Text = dtpPaymentStartDate.Value.AddMonths(clsCommon.myCdbl(txtNoofEmi.Text)) 'qry1

        End If
    End Sub

    Private Sub dtpPaymentStartDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpPaymentStartDate.ValueChanged
        If (clsCommon.myCdbl(txtNoofEmi.Text)) > 0 Then

            dtpEndMonth.Text = dtpPaymentStartDate.Value.AddMonths(clsCommon.myCdbl(txtNoofEmi.Text))

        End If
    End Sub

    Private Sub cboLoanStatus_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboLoanStatus.SelectedIndexChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboLoanStatus.Text), "Approve") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(txtCode.Value), "") = CompairStringResult.Equal Then
            'If clsCommon.myCstr(cboLoanStatus.DisplayMember) = "Approve" And txtCode.Value = "" Then
            clsCommon.MyMessageBoxShow(Me, "Cannot Select Approve Loan Status", Me.Text)
        End If
    End Sub

    Private Sub txtEmpCode_Load(sender As Object, e As EventArgs) Handles txtEmpCode.Load

    End Sub

    Private Sub MyLabel6_Click(sender As Object, e As EventArgs) Handles MyLabel6.Click

    End Sub

    Private Sub fndbankcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndbankcode._MYValidating
        Try
            Dim qry As String = "select Bank_Code as Code,Bank_Name as Name,City_Code from tspl_vendor_bank_master"
            fndbankcode.Value = clsCommon.ShowSelectForm("bnkcode", qry, "Code", "", fndbankcode.Value, "Code", isButtonClicked)
            lblBankCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Name from tspl_vendor_bank_master where Bank_Code='" & fndbankcode.Value & "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
End Class