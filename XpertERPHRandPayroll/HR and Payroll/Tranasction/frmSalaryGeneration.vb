Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls


Public Class frmSalaryGeneration
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim AllowtoSelDateandBankforPayEntryOnSalaryGeneration As Boolean = False
    Dim obj As New clsSalaryGeneration
    Public Proceed As Boolean = False
    Public SalaryPayableAmt As Decimal
    Public OpenTransDocument As String = Nothing
#End Region

    Private Sub frmSalaryGeneration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AllowtoSelDateandBankforPayEntryOnSalaryGeneration = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSelDateandBankforPayEntryOnSalaryGeneration, clsFixedParameterCode.AllowtoSelDateandBankforPayEntryOnSalaryGeneration, Nothing)) = 1, True, False)
            SetUserMgmtNew()
            isNewEntry = True
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
            funReset()
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            End If
            Dim SendMail As String = clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where Code ='SendSalarySlipMailToEmployee'")
            If SendMail = "1" Then 'Working'
                btnSendMail.Visible = True
            Else
                btnSendMail.Visible = False
            End If
            btnReverse.Visible = False
            If OpenTransDocument IsNot Nothing AndAlso OpenTransDocument.Length > 0 Then
                txtCode.Value = OpenTransDocument
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
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
                If clsSalaryGeneration.ReverseAndUnpost(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current, Nothing, True)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryGeneration)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Try
            findPayperiod.Value = clsPayPeriodMaster.getFinder("Posted=1 and FREEZED=0", findPayperiod.Value, isButtonClicked)
            SetPayperiodDtl()
            txtEmp.arrValueMember = Nothing
            If clsCommon.myLen(findPayperiod.Value) > 0 Then
                dtpGenerateDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select dateadd(DAY,1,DATE_TO) as dd from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & findPayperiod.Value & "'"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetPayperiodDtl(Optional ByVal trans As SqlTransaction = Nothing)

        If clsCommon.myLen(findPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current, trans)
            lblPayPeriodName.Text = clspp.Name
            dtpFrom.Value = clspp.DATE_FROM
            dtpTo.Value = clspp.DATE_TO
            Me.txtPayPeriodDays.Text = DateDiff(DateInterval.Day, clspp.DATE_FROM, clspp.DATE_TO) + 1
        Else
            lblPayPeriodName.Text = ""
        End If
    End Sub

    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                Throw New Exception("Transection already posted")
            End If
        End If
        If clsCommon.myLen(txtBranch.Value) <= 0 Then
            txtBranch.Focus()
            Throw New Exception("Select Location")
        End If
        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            findPayperiod.Focus()
            Throw New Exception("Pay Period Code")
        End If
        If clsCommon.myLen(txtGeneratedBy.Value) <= 0 Then
            txtGeneratedBy.Focus()
            Throw New Exception("Generated By Code")
        End If
        If txtEmp.arrValueMember Is Nothing OrElse txtEmp.arrValueMember.Count <= 0 Then
            txtEmp.Focus()
            Throw New Exception("Please Select Employees to process salary.")
        End If
        If AllowtoSelDateandBankforPayEntryOnSalaryGeneration Then
            If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                fndBankCode.Focus()
                Throw New Exception("Select Bank")
            End If
        End If
        Return True
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        Dim logFile As String = "salgenlog.txt"
        clsCommon.ProgressBarUpdate("Checking for log file...")
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Try
            AllowToSave()
            Dim obj As clsSalaryGeneration
            obj = New clsSalaryGeneration()
            obj.EmpList = txtEmp.arrValueMember
            obj.Code = clsCommon.myCstr(txtCode.Value)
            obj.LOCATION_CODE = clsCommon.myCstr(txtBranch.Value)
            obj.DEVISION_CODE = clsCommon.myCstr(fndDivision.Value)
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(findPayperiod.Value)
            obj.PAYPERIOD_DAYS = clsCommon.myCdbl(txtPayPeriodDays.Text)
            obj.GENERATE_DATE = dtpGenerateDate.Value 'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
            obj.GENERATED_BY = clsCommon.myCstr(txtGeneratedBy.Value)
            obj.GENERATE_REMARKS = clsCommon.myCstr(txtRemark.Text)
            obj.Todate = dtpTo.Value
            ''''''
            obj.SALARY_PAYABLE_ACC = clsCommon.myCstr(txtSalaryPayableAccount.Text)
            obj.GL_Employer_ESI_PAYABLE = clsCommon.myCstr(txtESIPayableAcc.Text)
            obj.GL_Employer_PF_PAYABLE = clsCommon.myCstr(txtPFPayableAcc.Text)
            obj.SAL_ACCOUNT_SET = clsCommon.myCstr(fndSalaryAccountSett.Value)
            obj.GL_EMPLOYER_OTHERS_PAYABLE = clsCommon.myCstr(txtOthrPayableAcc.Text)
            obj.CHEQUE_NO = clsCommon.myCstr(txtChequeNo.Text)
            If clsCommon.myLen(clsCommon.myCstr(dtpChequeDated.Value)) <= 0 Then
                obj.CHEQUE_DATED = Nothing
            Else
                obj.CHEQUE_DATED = dtpChequeDated.Value
            End If
            obj.GL_SALARY_PAYABLE_AMOUNT = SalaryPayableAmt
            If clsCommon.myLen(clsCommon.myCstr(txtpaymentDate.Value)) <= 0 Then
                obj.Payment_Date = Nothing
            Else
                obj.Payment_Date = txtpaymentDate.Value
            End If
            obj.Payment_Bank_Code = fndBankCode.Value

            obj.CREATE_FE = chkCreateFE.Checked
            If obj.SaveData(obj, isNewEntry) Then
                LoadData(obj.Code, NavigatorType.Current)
                ListOfEmployeeSalaryNotGenrate()
                Return True
            Else
                System.Diagnostics.Process.Start("salgenlog.txt")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Dim objreader As New System.IO.StringReader("salgenlog.txt")
            If objreader IsNot Nothing AndAlso clsCommon.myLen(objreader) > 0 Then
                Dim str As String = clsCommon.myCstr(System.IO.File.ReadAllText("salgenlog.txt"))
                If clsCommon.myLen(str) > 0 Then
                    System.Diagnostics.Process.Start("salgenlog.txt")
                End If
            End If

        End Try
        Return False
    End Function

    Sub ListOfEmployeeSalaryNotGenrate()
        Try


            Dim strLog As String = ""
            Dim logFile As String = "salgenlog.txt"
            If System.IO.File.Exists(logFile) Then
                Dim stream As New IO.StreamWriter(logFile, False)
                stream.WriteLine("")
                stream.Close()
            Else
                Dim fs As IO.FileStream = System.IO.File.Create(logFile)
                fs.Close()
            End If
            Dim condDivision As String = ""
            If clsCommon.myLen(fndDivision.Value) > 0 Then
                condDivision = " and T1.DEVISION_CODE='" & fndDivision.Value & "'"
            End If
            Dim Qry As String = " select XFinal.[Employee Code], XFinal.[Employee Name] from ( SELECT T1.EMP_STATUS_CODE AS [Status Code],T1.EMP_CODE AS [Employee Code],emp.Emp_Name as [Employee Name],emp.SEX as [Gender],emp.FATHERS_NAME as [Gurdian Name],emp.MARITAL_STATUS as [Marital Status],T1.REVISION_NO as [Status Revision],T1.DESIGNATION_ID as [Designation Code],T1.IS_PF_APPL as [PF Applicable]," _
       & " T1.PF_NO as [PF No], T1.IS_ESI_APPL as [ESI Applicable], T1.ESI_NO as [ESI No], T1.IS_BONUS_APPL as [Bonus Applicable], T1.BONUS_CODE as [Bonus Code], T1.IS_OT_APPL as [OT Applicable], T1.OT_CODE, T1.WORKING_STATUS as [Working Status],T1.DEVISION_CODE AS [Division Code] " _
       & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
       & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(Me.dtpTo.Value, "dd MMM yyyy") & "' " _
       & " GROUP BY EMP_CODE HAVING MAX(applicable_from)<='" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' ) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE inner join TSPL_EMPLOYEE_MASTER emp on T2.EMP_CODE=emp.EMP_CODE " _
       & " where emp.emp_status='Active' and emp.EMP_CODE not in (select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE from TSPL_GENERATE_SALARY " _
       & " inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE " _
       & " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "' ) AND emp.SALARY_ACCOUNT_CODE='" & fndSalaryAccountSett.Value & "' AND T1.LOCATION_CODE='" & txtBranch.Value & "' " & condDivision & " " _
       & " and 2=(case when  emp.RELIEVING_DATE is null then (case when  len( emp.Joining_date) <=0 then 3 else (case when convert(date,emp.Joining_date,103) <='" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "'  then 2 else 3 end) end) else (case when  (convert(date,emp.RELIEVING_DATE,103) >='" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "'  or convert(date,emp.RELIEVING_DATE,103) between '" + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MMM/yyyy") + "'  and '" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "'  ) then 2 else 3 end) end) " _
       & " Union All " _
       & " SELECT T1.EMP_STATUS_CODE AS [Status Code],T1.EMP_CODE AS [Employee Code],emp.Emp_Name as [Employee Name],emp.SEX as [Gender],emp.FATHERS_NAME as [Gurdian Name],emp.MARITAL_STATUS as [Marital Status],T1.REVISION_NO as [Status Revision],T1.DESIGNATION_ID as [Designation Code],T1.IS_PF_APPL as [PF Applicable]," _
       & " T1.PF_NO as [PF No], T1.IS_ESI_APPL as [ESI Applicable], T1.ESI_NO as [ESI No], T1.IS_BONUS_APPL as [Bonus Applicable], T1.BONUS_CODE as [Bonus Code], T1.IS_OT_APPL as [OT Applicable], T1.OT_CODE, T1.WORKING_STATUS as [Working Status],T1.DEVISION_CODE AS [Division Code] " _
       & " FROM TSPL_EMPLOYEE_STATUS T1  JOIN ( " _
       & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(Me.dtpTo.Value, "dd MMM yyyy") & "' " _
       & " GROUP BY EMP_CODE HAVING MAX(applicable_from)<='" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' ) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE inner join TSPL_EMPLOYEE_MASTER emp on T2.EMP_CODE=emp.EMP_CODE " _
       & " where emp.emp_status='Active' and emp.EMP_CODE  in (select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE from TSPL_GENERATE_SALARY inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "' AND TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & clsCommon.myCstr(Me.txtCode.Value) & "')  ) XFinal  " _
       & "  left outer Join (select EMP_CODE,MAX(EMP_SAL_CODE) AS EMP_SAL_CODE,MAX(REVISION_NO) AS REVISION_NO   from TSPL_EMPLOYEE_SALARY WHERE  APPLICABLE_FROM<='" & Format(Me.dtpTo.Value, "dd MMM yyyy") & "'   GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' ) XSalary on XSalary.EMP_CODE = XFinal.[Employee Code] where XSalary.EMP_SAL_CODE is  null  OR XFinal.[Employee Code] not in (select TSPL_MONTHLY_ATTENDANCE_DETAIL.Emp_Code from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join  TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_Code= TSPL_MONTHLY_ATTENDANCE.MTA_CODE  where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE='" + findPayperiod.Value + "') "

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim objWriter As New System.IO.StreamWriter(logFile, True)
                objWriter.WriteLine("List of Employees not having the salary definitions:")
                For Each dr As DataRow In dt.Rows
                    objWriter.WriteLine(dr.Item("Employee Code") + " : " + dr.Item("Employee Name"))
                Next
                objWriter.Close()
                Throw New Exception("Some Working Employee's Salary is not defined or Unapproved !")
            End If
        Catch ex As Exception
            Dim objreader As New System.IO.StringReader("salgenlog.txt")
            If objreader IsNot Nothing AndAlso clsCommon.myLen(objreader) > 0 Then
                Dim str As String = clsCommon.myCstr(System.IO.File.ReadAllText("salgenlog.txt"))
                If clsCommon.myLen(str) > 0 Then
                    System.Diagnostics.Process.Start("salgenlog.txt")
                End If
            End If
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset(Optional ByVal trans As SqlTransaction = Nothing, Optional isReverse As Boolean = False)
        isNewEntry = True
        txtCode.MyReadOnly = False
        findPayperiod.Enabled = True
        lblPayPeriodName.Text = ""
        txtBranch.Enabled = True
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            txtBranch.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
        Else
            txtBranch.Value = ""
            lblLocationDesc.Text = ""
        End If
        txtCode.Value = Nothing
        txtCode.Focus()
        findPayperiod.Value = Nothing
        btnsave.Text = "Generate"
        btnsave.Enabled = True
        btndelete.Enabled = True
        If isReverse = True Then
        Else
            txtEmp.arrValueMember = Nothing
        End If


        Dim serverdate As Date
        serverdate = clsCommon.GETSERVERDATE(trans)
        dtpGenerateDate.Value = serverdate
        Me.dtpFrom.Value = serverdate
        Me.dtpTo.Value = serverdate
        Me.dtpChequeDated.Value = serverdate
        Me.txtChequeNo.Text = ""
        Me.txtPFPayableAcc.Text = ""
        Me.txtESIPayableDesc.Text = ""
        Me.txtESIPayableAcc.Text = ""
        Me.txtPFPayableAccDesc.Text = ""
        Me.txtChequeNo.Text = ""
        Me.txtGeneratedBy.Value = Nothing
        Me.txtPayPeriodDays.Text = ""
        Me.txtRemark.Text = ""
        Me.txtSalaryPayableAccDesc.Text = ""
        Me.txtSalaryPayableAccount.Text = ""
        Me.txtOthrPayableAcc.Text = ""
        Me.txtOthrPayableDesc.Text = ""
        fndSalaryAccountSett.Value = Nothing
        SalaryPayableAmt = 0
        If AllowtoSelDateandBankforPayEntryOnSalaryGeneration Then
            lblPaymentDate.Visible = True
            txtpaymentDate.Visible = True
            lblBankCode.Visible = True
            fndBankCode.Visible = True
        Else
            lblPaymentDate.Visible = False
            txtpaymentDate.Visible = False
            lblBankCode.Visible = False
            fndBankCode.Visible = False
        End If
        Me.txtpaymentDate.Value = serverdate
        fndBankCode.Value = ""
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isReverse As Boolean = False)
        obj = clsSalaryGeneration.GetData(strCode, NavTyep, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset(trans, isReverse)
            isNewEntry = False
            txtCode.MyReadOnly = True
            findPayperiod.Enabled = False
            txtBranch.Enabled = False
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
            txtCode.Value = obj.Code
            txtBranch.Value = obj.LOCATION_CODE
            lblLocationDesc.Text = obj.LOCATION_DESC

            fndDivision.Value = obj.DEVISION_CODE
            lblDivisionDesc.Text = obj.DEVISION_NAME

            findPayperiod.Value = clsCommon.myCstr(obj.PAY_PERIOD_CODE)
            txtPayPeriodDays.Text = clsCommon.myCdbl(obj.PAYPERIOD_DAYS)
            '' 
            SetPayperiodDtl(trans)
            dtpGenerateDate.Value = obj.GENERATE_DATE
            txtGeneratedBy.Value = clsCommon.myCstr(obj.GENERATED_BY)
            txtRemark.Text = clsCommon.myCstr(obj.GENERATE_REMARKS)

            ''
            txtSalaryPayableAccount.Text = obj.SALARY_PAYABLE_ACC
            txtSalaryPayableAccDesc.Text = obj.SALARY_PAYABLE_ACC_Desc

            txtESIPayableAcc.Text = obj.GL_Employer_ESI_PAYABLE
            txtESIPayableDesc.Text = obj.GL_Employer_ESI_PAYABLE_Desc

            txtPFPayableAcc.Text = obj.GL_Employer_PF_PAYABLE
            txtPFPayableAccDesc.Text = obj.GL_Employer_PF_PAYABLE_Desc

            txtOthrPayableAcc.Text = obj.GL_EMPLOYER_OTHERS_PAYABLE
            txtOthrPayableDesc.Text = obj.GL_EMPLOYER_OTHERS_PAYABLE_Desc
            SalaryPayableAmt = obj.GL_SALARY_PAYABLE_AMOUNT
            fndSalaryAccountSett.Value = obj.SAL_ACCOUNT_SET
            chkCreateFE.Checked = obj.CREATE_FE
            txtChequeNo.Text = obj.CHEQUE_NO
            If clsCommon.myLen(obj.CHEQUE_DATED) <= 0 Then
            Else
                Me.dtpChequeDated.Value = obj.CHEQUE_DATED
            End If

            If clsCommon.myLen(obj.Payment_Date) <= 0 Then
            Else
                Me.txtpaymentDate.Value = obj.Payment_Date
            End If

            fndBankCode.Value = obj.Payment_Bank_Code


            If obj.POSTED = True Then
                btnCreateFE.Enabled = True
            Else
                btnCreateFE.Enabled = False
            End If
            If isReverse = True Then
            Else
                txtEmp.arrValueMember = obj.arrEMP
            End If

        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing

        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = "  TSPL_EMPLOYEE_MASTER.LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "  select SALARY_GENERATION_CODE as Code, PAY_PERIOD_CODE,PAYPERIOD_DAYS,TSPL_GENERATE_SALARY.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GENERATE_SALARY.Devision_Code as [Division Code],GENERATE_DATE,GENERATED_BY,TSPL_EMPLOYEE_MASTER.Emp_Name as [GENERATED BY NAME],GENERATE_REMARKS,POSTED,Posting_Date from TSPL_GENERATE_SALARY Left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_GENERATE_SALARY.Location_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY.GENERATED_BY=TSPL_EMPLOYEE_MASTER.EMP_CODE"
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_GENERATE_SALARY", qry, "Code", whrcls, txtCode.Value, "SALARY_GENERATION_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
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
                If (clsSalaryGeneration.DeleteData(txtCode.Value)) Then
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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        If (myMessages.postConfirm()) = False Then
            Exit Sub
        End If
        If SavingData(True) = False Then
            Exit Sub
        End If
        Try
            Dim obj As clsSalaryGeneration = clsSalaryGeneration.GetData(txtCode.Value, NavigatorType.Current)
            If obj.POSTED = True Then
                clsCommon.MyMessageBoxShow(Me, "Document already posted !", Me.Text)
                Exit Sub
            End If
            If clsSalaryGeneration.AllowToPost(obj, Nothing) = True AndAlso clsSalaryGeneration.updateGLAccInGenSalary(obj, Nothing) = True Then
                Dim frm As New frmSalaryGLAccounts
                frm.Location_Code = txtBranch.Value
                frm.Division_Code = fndDivision.Value
                frm.txtSalaryPayableAccount.Text = Me.txtSalaryPayableAccount.Text
                frm.SalaryPayableAccount = Me.txtSalaryPayableAccount.Text
                frm.txtESIPayableAcc.Text = Me.txtESIPayableAcc.Text
                frm.GL_Employer_PF_PAYABLE = txtPFPayableAcc.Text
                frm.Sal_Gen_Code = Me.txtCode.Value
                frm.SalaryPayableAccountDesc = Me.txtSalaryPayableAccDesc.Text
                frm.GL_Employer_ESI_PAYABLE = Me.txtESIPayableAcc.Text
                frm.GL_Employer_ESI_PAYABLE_Desc = Me.txtESIPayableDesc.Text
                frm.GL_EMPLOYER_OTHERS_PAYABLE = Me.txtOthrPayableAcc.Text
                frm.GL_EMPLOYER_OTHERS_PAYABLE_Desc = Me.txtOthrPayableDesc.Text
                frm.PAY_PERIOD_CODE = clsCommon.myCstr(Me.findPayperiod.Value)
                frm.Generate_Date = dtpGenerateDate.Value 'clsCommon.GETSERVERDATE()
                frm.Remarks = Me.txtRemark.Text
                frm.ChequeNo = Me.txtChequeNo.Text
                frm.ChequeDated = Me.dtpChequeDated.Value

                If chkCreateFE.Checked Then
                    frm.ShowDialog()
                    Proceed = frm.Proceed
                    If Proceed = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Salary not Posted", Me.Text)
                        Exit Sub
                    End If
                End If
                If (clsSalaryGeneration.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            Else
                System.Diagnostics.Process.Start("salgenlog.txt")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            System.Diagnostics.Process.Start("salgenlog.txt")
        End Try
    End Sub

    Sub CreateFinancialEntry()
        Dim qryCheck As String = "select count(*) as Total from TSPL_JOURNAL_MASTER where Source_Doc_No='" & txtCode.Value & "'"
        Dim total As Integer = clsDBFuncationality.getSingleValue(qryCheck)
        If total > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Financial entry of this document already created.", Me.Text)
            Exit Sub
        End If
        If common.clsCommon.MyMessageBoxShow(Me, "Do You Want To Create Financial Entry ?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Try
            Dim obj As clsSalaryGeneration = clsSalaryGeneration.GetData(txtCode.Value, NavigatorType.Current)
            obj.CREATE_FE = True
            If clsSalaryGeneration.AllowToPost(obj, Nothing) = True AndAlso clsSalaryGeneration.updateGLAccInGenSalary(obj, Nothing) = True Then
                Dim frm As New frmSalaryGLAccounts
                frm.Location_Code = txtBranch.Value
                frm.Division_Code = fndDivision.Value
                frm.txtSalaryPayableAccount.Text = Me.txtSalaryPayableAccount.Text
                frm.SalaryPayableAccount = Me.txtSalaryPayableAccount.Text
                frm.txtESIPayableAcc.Text = Me.txtESIPayableAcc.Text
                frm.GL_Employer_PF_PAYABLE = txtPFPayableAcc.Text
                frm.Sal_Gen_Code = Me.txtCode.Value
                frm.SalaryPayableAccountDesc = Me.txtSalaryPayableAccDesc.Text
                frm.GL_Employer_ESI_PAYABLE = Me.txtESIPayableAcc.Text
                frm.GL_Employer_ESI_PAYABLE_Desc = Me.txtESIPayableDesc.Text
                frm.GL_EMPLOYER_OTHERS_PAYABLE = Me.txtOthrPayableAcc.Text
                frm.GL_EMPLOYER_OTHERS_PAYABLE_Desc = Me.txtOthrPayableDesc.Text
                frm.PAY_PERIOD_CODE = clsCommon.myCstr(Me.findPayperiod.Value)
                frm.Generate_Date = dtpGenerateDate.Value ' clsCommon.GETSERVERDATE()
                frm.Remarks = Me.txtRemark.Text
                frm.ChequeNo = Me.txtChequeNo.Text
                frm.ChequeDated = Me.dtpChequeDated.Value
                frm.ShowDialog()
                Proceed = frm.Proceed
                If Proceed = False Then
                    common.clsCommon.MyMessageBoxShow(Me, "Salary not Posted", Me.Text)
                    Exit Sub
                End If
                If clsSalaryGeneration.PostData(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            Else
                System.Diagnostics.Process.Start("salgenlog.txt")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            System.Diagnostics.Process.Start("salgenlog.txt")
        End Try
    End Sub

    Private Function AllowToPost(Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isFE As Boolean = False) As Boolean
        If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            txtCode.Focus()
            Throw New Exception("Select Salary Generation Code")
        End If
        If clsCommon.myLen(txtBranch.Value) <= 0 Then
            txtBranch.Focus()
            Throw New Exception("Select Location")
        End If
        If isFE Then
            Dim obj As clsSalaryGeneration = clsSalaryGeneration.GetData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.POSTED = 0) Then
                Throw New Exception("Document " & clsCommon.myCstr(txtCode.Value) & " not posted")
            End If
        End If


        If clsCommon.myLen(clsCommon.myCstr(fndSalaryAccountSett.Value)) <= 0 Then
            fndSalaryAccountSett.Focus()
            Throw New Exception("Please Fill AccountSet Code")
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtSalaryPayableAccount.Text)) <= 0 Then
            txtSalaryPayableAccount.Focus()
            Throw New Exception("Salary Payable Account is empty !")
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtPFPayableAcc.Text)) <= 0 Then
            txtPFPayableAcc.Focus()
            Throw New Exception("Employer PF Payable Account is empty !")
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtESIPayableAcc.Text)) <= 0 Then
            txtESIPayableAcc.Focus()
            Throw New Exception("Employer ESI Payable Account is empty !")
        End If

        If clsCommon.myLen(clsCommon.myCstr(txtOthrPayableAcc.Text)) <= 0 Then
            txtOthrPayableAcc.Focus()
            Throw New Exception("Employer Other Payable Account is empty !")
        End If

        Dim strLog As String = ""
        Dim logFile As String = "salgenlog.txt"
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Dim objWriter As New System.IO.StreamWriter(logFile, True)
        Dim strq As String

        strq = " SELECT * FROM ( " &
               " select TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE,(case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' " &
               " then TSPL_EMPLOYEE_MASTER.ADVANCE_TO_STAFF else TSPL_GENERATE_SALARY_PAYHEADS.Account_Code end) AS SalAccount_Code," &
               " (case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' " &
               " then TSPL_EMPLOYEE_MASTER.ADVANCE_TO_STAFF else TSPL_PAYHEAD_MASTER.Account_Code end) AS PHAccount_Code, " &
               " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)=1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Debit, " &
               " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)<>1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Credit, " &
               " (case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE " &
               " from TSPL_GENERATE_SALARY_PAYHEADS " &
               " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
               " INNER JOIN TSPL_EMPLOYEE_MASTER ON  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " &
               " left join TSPL_PAYHEAD_MASTER on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " &
               " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code " &
               " where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'  ) as final " &
               " where ((SalAccount_Code is null or PHAccount_Code is null) and EMP_CODE is null) or ((SalAccount_Code is null or PHAccount_Code is null) and EMP_CODE is not null and Credit>0)"
        Dim dtValid As DataTable
        dtValid = clsDBFuncationality.GetDataTable(strq, trans)
        If dtValid.Rows.Count > 0 Then
            objWriter.WriteLine("List of Pay heads/ employee loan advance gl are not mapped to GL Accounts:")
            For Each drAC As DataRow In dtValid.Rows
                objWriter.WriteLine((dtValid.Rows.IndexOf(drAC) + 1) & ". " & drAC.Item("PAY_HEAD_CODE") & "-Emp Code:" & clsCommon.myCstr(drAC.Item("EMP_CODE")))
            Next
            objWriter.Close()
            Throw New Exception("Some Pay heads are not mapped to GL Accounts !")
        End If

        '' check for Employer GL Account
        strq = " SELECT * FROM ( " &
              " select TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account AS SalAccount_Code," &
              " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)=1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Debit, " &
              " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)<>1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Credit, " &
              " (case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE " &
              " from TSPL_GENERATE_SALARY_PAYHEADS " &
              " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
              " INNER JOIN TSPL_EMPLOYEE_MASTER ON  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " &
              " left join TSPL_PAYHEAD_MASTER on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " &
              " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code " &
              " where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'  and TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EPF','EMPESI','LWF')) as final " &
              " where ((SalAccount_Code is null) and EMP_CODE is null) or ((SalAccount_Code is null) and EMP_CODE is not null and Credit>0)"
        Dim dtEmplValid As DataTable
        dtEmplValid = clsDBFuncationality.GetDataTable(strq, trans)
        If dtValid.Rows.Count > 0 Then
            objWriter.WriteLine("List of Pay heads(Employer) are not mapped to GL Accounts:")
            For Each drAC As DataRow In dtValid.Rows
                objWriter.WriteLine((dtValid.Rows.IndexOf(drAC) + 1) & ". " & drAC.Item("PAY_HEAD_CODE") & "-Emp Code:" & clsCommon.myCstr(drAC.Item("EMP_CODE")))
            Next
            objWriter.Close()
            Throw New Exception("Some Pay head employer account are not mapped !")
        End If

        '' location wise account code check
        Dim LocSeg As String
        Dim LocSep As String
        Dim FinalQry As String
        LocSep = txtESIPayableAcc.Text.Substring(txtESIPayableAcc.Text.Length - 4, 1)
        If clsCommon.CompairString(LocSep, "-") = CompairStringResult.Equal Then
            LocSeg = txtESIPayableAcc.Text.Substring(txtESIPayableAcc.Text.Length - 3, 3)
        Else
            LocSeg = ""
        End If
        If clsCommon.myLen(LocSeg) <= 0 Then
            strq = " SELECT distinct (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'') " &
                   " ELSE Account_Code+ '' END) AS Account_Code,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else null end ) as ACTUAL_AMOUNT  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'"
        Else
            strq = " SELECT distinct (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'-" & LocSeg & "') " &
                   " ELSE Account_Code+ '-" & LocSeg & "' END) AS Account_Code,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else null end ) as ACTUAL_AMOUNT  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'"
        End If
        FinalQry = "select NewACC.Account_Code,TSPL_GL_ACCOUNTS.Account_Code as OldAccCode from (" & strq & ") as NewACC left join TSPL_GL_ACCOUNTS on NewACC.Account_Code=TSPL_GL_ACCOUNTS.Account_Code  where (TSPL_GL_ACCOUNTS.Account_Code is null and EMP_CODE is null) or (EMP_CODE is not null and ACTUAL_AMOUNT>0)"
        Dim dtACCode As DataTable
        dtACCode = clsDBFuncationality.GetDataTable(FinalQry, trans)

        If dtACCode.Rows.Count > 0 Then
            objWriter.WriteLine("List of Account Code that does not exist in GL account:")
            For Each drAC As DataRow In dtACCode.Rows
                objWriter.WriteLine((dtACCode.Rows.IndexOf(drAC) + 1) & ". " & drAC.Item("Account_Code"))
            Next
            objWriter.Close()
            Throw New Exception("Some Account Code that does not exist in GL account !")
        End If
        Return True
    End Function

    Function updateGLAccInGenSalary(ByVal trans As SqlTransaction, Optional ByVal isFe As Boolean = False) As Boolean
        '' location wise account code check
        If isFe = False Then
            If chkCreateFE.Checked = False Then
                Return True
            End If
        End If

        Dim logFile As String = "salgenlog.txt"
        If System.IO.File.Exists(logFile) Then
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If

        Dim LocSeg As String
        Dim LocSep As String
        Dim strq As String
        Dim FinalQry As String
        Dim QryLoc As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" & txtBranch.Value & "'"
        LocSep = "-"
        LocSeg = clsDBFuncationality.getSingleValue(QryLoc, trans)
        If clsCommon.myLen(LocSeg) <= 0 Then
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'') " &
                   " ELSE Account_Code+ '' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'"
        Else
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'-" & LocSeg & "') " &
                   " ELSE Account_Code+ '-" & LocSeg & "' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'"
        End If
        Dim qryCheck As String = "select TSPL_GL_ACCOUNTS.Account_Code,gla_new.New_Account_Code from TSPL_GL_ACCOUNTS right outer join (select distinct New_Account_Code from (" & strq & ") GLA) gla_new on TSPL_GL_ACCOUNTS.Account_Code=gla_new.New_Account_Code " &
            " where gla_new.New_Account_Code is not null and TSPL_GL_ACCOUNTS.Account_Code is null"

        Dim dtCGlNA As DataTable = clsDBFuncationality.GetDataTable(qryCheck, trans)
        If dtCGlNA.Rows.Count > 0 Then

            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Accounts that does not exists for current location:")
            For Each drGL As DataRow In dtCGlNA.Rows
                objWriter.WriteLine((dtCGlNA.Rows.IndexOf(drGL) + 1) & ". " & clsCommon.myCstr(drGL.Item("New_Account_Code")))
            Next
            objWriter.Close()
            Throw New Exception("Some General Accounts does not exists for current location !")
        End If

        FinalQry = "update TSPL_GENERATE_SALARY_PAYHEADS set Account_Code=newAcc.New_Account_Code from (" & strq & ") as NewACC where newAcc.Account_Code=TSPL_GENERATE_SALARY_PAYHEADS.Account_Code and SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "'"
        Dim isSaved As Boolean
        isSaved = clsDBFuncationality.ExecuteNonQuery(FinalQry, trans)

        '' update employer account
        If clsCommon.myLen(LocSeg) <= 0 Then
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Employer_Account,4),1,1)='-' THEN REPLACE(Employer_Account,RIGHT(Employer_Account,4),'') " &
                   " ELSE Employer_Account+ '' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account as Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF')"
        Else
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Employer_Account,4),1,1)='-' THEN REPLACE(Employer_Account,RIGHT(Employer_Account,4),'-" & LocSeg & "') " &
                   " ELSE Employer_Account+ '-" & LocSeg & "' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account as Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF')"
        End If
        Dim qryCheckEmpl As String = "select TSPL_GL_ACCOUNTS.Account_Code,gla_new.New_Account_Code from TSPL_GL_ACCOUNTS right outer join (select distinct New_Account_Code from (" & strq & ") GLA) gla_new on TSPL_GL_ACCOUNTS.Account_Code=gla_new.New_Account_Code " &
            " where gla_new.New_Account_Code is not null and TSPL_GL_ACCOUNTS.Account_Code is null"

        Dim dtCGlEmplNA As DataTable = clsDBFuncationality.GetDataTable(qryCheck, trans)
        If dtCGlNA.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Accounts that does not exists for current location:")
            For Each drGL As DataRow In dtCGlNA.Rows
                objWriter.WriteLine((dtCGlNA.Rows.IndexOf(drGL) + 1) & ". " & clsCommon.myCstr(drGL.Item("New_Account_Code")))
            Next
            objWriter.Close()
            Throw New Exception("Some General Accounts does not exists for current location !")
        End If

        FinalQry = "update TSPL_GENERATE_SALARY_PAYHEADS set Employer_Account=newAcc.New_Account_Code from (" & strq & ") as NewACC where newAcc.Account_Code=TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account and SALARY_GENERATION_CODE='" & clsCommon.myCstr(txtCode.Value) & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF')"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(FinalQry, trans)
        Return isSaved
    End Function

    Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
            Return True
        Else
            Return False
        End If
        Return True
    End Function

    Private Sub txtGeneratedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGeneratedBy._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        txtGeneratedBy.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtGeneratedBy.Value, "", isButtonClicked)
    End Sub

    Private Sub fndSalaryAccountSett__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSalaryAccountSett._MYValidating
        Dim qry As String = "select TSPL_PAYROLL_ACCOUNTSETS.ACCOUNT_SET_CODE AS Code,TSPL_PAYROLL_ACCOUNTSETS.DESCRIPTION, " &
        " TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_PF_PAYABLE,TSPL_GL_ACCOUNTS1.description as GL_Employer_PF_PAYABLE_Desc,TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_ESI_PAYABLE,TSPL_GL_ACCOUNTS2.Description as GL_Employer_ESI_PAYABLE_Desc, " &
        " TSPL_PAYROLL_ACCOUNTSETS.GL_SALARY_PAYABLE,TSPL_GL_ACCOUNTS.Description as GL_SALARY_PAYABLE_Desc ,TSPL_PAYROLL_ACCOUNTSETS.GL_EMPLOYER_OTHERS_PAYABLE,TSPL_GL_ACCOUNTS3.Description as GL_EMPLOYER_OTHERS_PAYABLE_Desc from TSPL_PAYROLL_ACCOUNTSETS  " &
        " LEFT JOIN TSPL_BANK_MASTER ON TSPL_PAYROLL_ACCOUNTSETS.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE " &
        " left join TSPL_GL_ACCOUNTS on TSPL_PAYROLL_ACCOUNTSETS.GL_SALARY_PAYABLE=TSPL_GL_ACCOUNTS.account_code " &
        " left join TSPL_GL_ACCOUNTS AS  TSPL_GL_ACCOUNTS1 on TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_PF_PAYABLE=TSPL_GL_ACCOUNTS1.account_code " &
        " left join TSPL_GL_ACCOUNTS AS  TSPL_GL_ACCOUNTS2 on TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_ESI_PAYABLE=TSPL_GL_ACCOUNTS2.account_code " &
        " left join TSPL_GL_ACCOUNTS AS  TSPL_GL_ACCOUNTS3 on TSPL_PAYROLL_ACCOUNTSETS.GL_EMPLOYER_OTHERS_PAYABLE=TSPL_GL_ACCOUNTS3.account_code " &
        " left join TSPL_GL_SOURCECODE on TSPL_PAYROLL_ACCOUNTSETS.SourceCode=TSPL_GL_SOURCECODE.SourceCode "
        fndSalaryAccountSett.Value = clsCommon.ShowSelectForm("AccountSett", qry, "Code", "", fndSalaryAccountSett.Value, "", isButtonClicked)

        Dim dt As DataTable
        qry = qry & " where TSPL_PAYROLL_ACCOUNTSETS.ACCOUNT_SET_CODE='" & clsCommon.myCstr(fndSalaryAccountSett.Value) & "'"

        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            txtSalaryPayableAccount.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_SALARY_PAYABLE"))
            txtSalaryPayableAccDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_SALARY_PAYABLE_Desc"))
            txtPFPayableAcc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_Employer_PF_PAYABLE"))
            txtPFPayableAccDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_Employer_PF_PAYABLE_Desc"))
            txtESIPayableAcc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_Employer_ESI_PAYABLE"))
            txtESIPayableDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_Employer_ESI_PAYABLE_Desc"))
            txtOthrPayableAcc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_EMPLOYER_OTHERS_PAYABLE"))
            txtOthrPayableDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("GL_EMPLOYER_OTHERS_PAYABLE_Desc"))
        Else
            txtSalaryPayableAccount.Text = ""
            txtSalaryPayableAccDesc.Text = ""
            txtPFPayableAcc.Text = ""
            txtPFPayableAccDesc.Text = ""
            txtESIPayableAcc.Text = ""
            txtESIPayableDesc.Text = ""
        End If
    End Sub

    Private Sub btnCreateFE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateFE.Click
        CreateFinancialEntry()
    End Sub

    Private Sub txtBranch__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBranch._MYValidating
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
        txtEmp.arrValueMember = Nothing
    End Sub

    Private Sub fndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivision._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        fndDivision.Value = clsDevisionMaster.getFinder(whrcls, Me.fndDivision.Value, isButtonClicked)
        lblDivisionDesc.Text = clsDevisionMaster.GetName(fndDivision.Value, Nothing)
        txtEmp.arrValueMember = Nothing
    End Sub

    Private Sub btnSendMail_Click(sender As Object, e As EventArgs) Handles btnSendMail.Click
        Try
            Dim SendMail As String = clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where Code ='SendSalarySlipMailToEmployee'")
            If SendMail = "1" Then
                clsCommon.ProgressBarShow()
                clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
                clsSalaryGeneration.sendSalarySlipToMail(txtCode.Value, Application.StartupPath)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub ViewFinancialEntry()
        Try
            If updateGLAccInGenSalary(Nothing, True) = False Then
                System.Diagnostics.Process.Start("salgenlog.txt")
                Exit Sub
            End If
            Dim frm As New frmSalaryGLAccounts
            frm.Location_Code = txtBranch.Value
            frm.Division_Code = fndDivision.Value
            frm.txtSalaryPayableAccount.Text = Me.txtSalaryPayableAccount.Text
            frm.SalaryPayableAccount = Me.txtSalaryPayableAccount.Text
            frm.txtESIPayableAcc.Text = Me.txtESIPayableAcc.Text
            frm.GL_Employer_PF_PAYABLE = txtPFPayableAcc.Text
            frm.Sal_Gen_Code = Me.txtCode.Value
            frm.SalaryPayableAccountDesc = Me.txtSalaryPayableAccDesc.Text
            frm.txtESIPayableAcc.Text = Me.txtESIPayableAcc.Text
            frm.GL_Employer_ESI_PAYABLE = txtESIPayableAcc.Text
            frm.GL_Employer_ESI_PAYABLE_Desc = Me.txtESIPayableDesc.Text
            frm.GL_EMPLOYER_OTHERS_PAYABLE = Me.txtOthrPayableAcc.Text
            frm.GL_EMPLOYER_OTHERS_PAYABLE_Desc = Me.txtOthrPayableDesc.Text
            frm.PAY_PERIOD_CODE = clsCommon.myCstr(Me.findPayperiod.Value)
            frm.Generate_Date = dtpGenerateDate.Value 'clsCommon.GETSERVERDATE()
            frm.Remarks = Me.txtRemark.Text
            frm.ChequeNo = Me.txtChequeNo.Text
            frm.ChequeDated = Me.dtpChequeDated.Value
            frm.btnPost.Enabled = False
            frm.ShowDialog()
            SalaryPayableAmt = clsCommon.myCdbl(frm.txtSalariesPayableAmt.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnViewFE_Click(sender As Object, e As EventArgs) Handles btnViewFE.Click
        ViewFinancialEntry()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_No from TSPL_PAYMENT_HEADER where Against_Salary_Generation_Code='" + txtCode.Value + "'"))
            If clsCommon.myLen(strDocNo) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDocNo)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ' Ticket No : TEC/16/04/19-000468  by prabhakar for open salary generation register report
    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            Dim strCode As String = txtCode.Value
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No document found", Me.Text)
                Return
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalaryGenerationRegister, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    ''BHA/16/04/19-000860 by balwinder on 03/05/2019
    Private Sub txtEmp__My_Click(sender As Object, e As EventArgs) Handles txtEmp._My_Click
        Try
            Dim qry As String
            If clsCommon.myLen(txtBranch.Value) <= 0 Then
                txtBranch.Focus()
                Throw New Exception("Please first select Location")
            End If

            If clsCommon.myLen(findPayperiod.Value) <= 0 Then
                findPayperiod.Focus()
                Throw New Exception("Please first select pay period")
            End If

            If clsCommon.myLen(fndSalaryAccountSett.Value) <= 0 Then
                fndSalaryAccountSett.Focus()
                Throw New Exception("Please first select seleary account setting")
            End If
            '======================================================================================
            ' clsCommon.myCstr(findPayperiod.Value)

            '======================================================================================

            Dim condDivision As String = ""
            If clsCommon.myLen(fndDivision.Value) > 0 Then
                condDivision = " and T1.DEVISION_CODE='" & fndDivision.Value & "'"
            End If
            qry = " select XFinal.* from ( SELECT T1.EMP_STATUS_CODE AS [Status Code],T1.EMP_CODE AS [Employee Code],emp.Emp_Name as [Employee Name],emp.SEX as [Gender],emp.FATHERS_NAME as [Gurdian Name],emp.MARITAL_STATUS as [Marital Status],T1.REVISION_NO as [Status Revision],T1.DESIGNATION_ID as [Designation Code],T1.IS_PF_APPL as [PF Applicable]," _
       & " T1.PF_NO as [PF No], T1.IS_ESI_APPL as [ESI Applicable], T1.ESI_NO as [ESI No], T1.IS_BONUS_APPL as [Bonus Applicable], T1.BONUS_CODE as [Bonus Code], T1.IS_OT_APPL as [OT Applicable], T1.OT_CODE, T1.WORKING_STATUS as [Working Status],T1.DEVISION_CODE AS [Division Code] " _
       & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
       & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(Me.dtpTo.Value, "dd MMM yyyy") & "' " _
       & " GROUP BY EMP_CODE HAVING MAX(applicable_from)<='" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' ) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE inner join TSPL_EMPLOYEE_MASTER emp on T2.EMP_CODE=emp.EMP_CODE " _
       & " where emp.emp_status='Active' and emp.EMP_CODE not in (select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE from TSPL_GENERATE_SALARY " _
       & " inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE " _
       & " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "' ) AND emp.SALARY_ACCOUNT_CODE='" & fndSalaryAccountSett.Value & "' AND T1.LOCATION_CODE='" & txtBranch.Value & "' " & condDivision & " " _
       & " and 2=(case when  emp.RELIEVING_DATE is null then (case when  len( emp.Joining_date) <=0 then 3 else (case when convert(date,emp.Joining_date,103) <='" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "'  then 2 else 3 end) end) else (case when  (convert(date,emp.RELIEVING_DATE,103) >='" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "'  or convert(date,emp.RELIEVING_DATE,103) between '" + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MMM/yyyy") + "'  and '" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "'  ) then 2 else 3 end) end) " _
       & " Union All " _
       & " SELECT T1.EMP_STATUS_CODE AS [Status Code],T1.EMP_CODE AS [Employee Code],emp.Emp_Name as [Employee Name],emp.SEX as [Gender],emp.FATHERS_NAME as [Gurdian Name],emp.MARITAL_STATUS as [Marital Status],T1.REVISION_NO as [Status Revision],T1.DESIGNATION_ID as [Designation Code],T1.IS_PF_APPL as [PF Applicable]," _
       & " T1.PF_NO as [PF No], T1.IS_ESI_APPL as [ESI Applicable], T1.ESI_NO as [ESI No], T1.IS_BONUS_APPL as [Bonus Applicable], T1.BONUS_CODE as [Bonus Code], T1.IS_OT_APPL as [OT Applicable], T1.OT_CODE, T1.WORKING_STATUS as [Working Status],T1.DEVISION_CODE AS [Division Code] " _
       & " FROM TSPL_EMPLOYEE_STATUS T1  JOIN ( " _
       & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(Me.dtpTo.Value, "dd MMM yyyy") & "' " _
       & " GROUP BY EMP_CODE HAVING MAX(applicable_from)<='" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' ) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE inner join TSPL_EMPLOYEE_MASTER emp on T2.EMP_CODE=emp.EMP_CODE " _
       & " where emp.emp_status='Active' and emp.EMP_CODE  in (select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE from TSPL_GENERATE_SALARY inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "' AND TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & clsCommon.myCstr(Me.txtCode.Value) & "')  ) XFinal  " _
       & "  left outer Join (select EMP_CODE,MAX(EMP_SAL_CODE) AS EMP_SAL_CODE,MAX(REVISION_NO) AS REVISION_NO   from TSPL_EMPLOYEE_SALARY WHERE  APPLICABLE_FROM<='" & Format(Me.dtpTo.Value, "dd MMM yyyy") & "'   GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' ) XSalary on XSalary.EMP_CODE = XFinal.[Employee Code]   inner Join (select TSPL_MONTHLY_ATTENDANCE_DETAIL.Emp_Code from TSPL_MONTHLY_ATTENDANCE_DETAIL " _
       & " left outer Join  TSPL_MONTHLY_ATTENDANCE On TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_Code= TSPL_MONTHLY_ATTENDANCE.MTA_CODE  " _
       & " where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE='" + clsCommon.myCstr(findPayperiod.Value) + "' " _
       & " union   " _
       & " select TSPL_DAILY_ATTENDANCE_DETAIL.emp_code from TSPL_DAILY_ATTENDANCE_DETAIL  " _
       & " left outer join TSPL_DAILY_ATTENDANCE on TSPL_DAILY_ATTENDANCE.DLA_CODE=TSPL_DAILY_ATTENDANCE_DETAIL.DLA_CODE " _
       & " where TSPL_DAILY_ATTENDANCE.PAY_PERIOD_CODE='" + clsCommon.myCstr(findPayperiod.Value) + "') XAttandance on XAttandance.Emp_Code = XFinal.[Employee Code]   where XSalary.EMP_SAL_CODE is not null  "

            txtEmp.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "EMP@SalGn", qry, "Employee Code", "", txtEmp.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        Dim Qry As String = clsERPFuncationality.glbankqueryNew("")

        fndBankCode.Value = clsCommon.ShowSelectForm("SGBanFilter", Qry, "Code", "  TSPL_bank_master.INACTIVE ='Active'", fndBankCode.Value, "Code", isButtonClicked)

    End Sub
End Class