Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLoanGeneration
    Inherits FrmMainTranScreen
    Const colempCode As String = "empCode"
    Const colempName As String = "EmpName"
    Const colloanCode As String = "LoanCode"
    Const colemiNo As String = "EmiNo"
    Const colpayPeriodCode As String = "PayPeriodCode"
    Const colemiAmount As String = "EmiAmount"
    Const coladjustPlus As String = "adjustPlus"
    Const coladjustMinus As String = "adjusTMinus"
    Const colnetEmiAmount As String = "netemiAmt"
    Const colBankCode As String = "bankcode"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim obj As New clsLoanGeneration
    Dim isInsideLoadData As Boolean = False
    Private ObjList As List(Of clsLoanGenerationDetail)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()
        Dim empCode As New GridViewTextBoxColumn
        Dim empName As New GridViewTextBoxColumn
        Dim loanCode As New GridViewTextBoxColumn
        Dim bankCode As New GridViewTextBoxColumn
        Dim emiNo As New GridViewTextBoxColumn
        Dim payPeriodCode As New GridViewTextBoxColumn
        Dim emiAmount As New GridViewDecimalColumn
        Dim adjustPlus As New GridViewDecimalColumn
        Dim adjustMinus As New GridViewDecimalColumn
        Dim netEmiAmount As New GridViewDecimalColumn

        gvLoanGeneration.Rows.Clear()
        gvLoanGeneration.Columns.Clear()
        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = "empCode"
        empCode.Width = 100
        empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = "EmpName"
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(empName)

        loanCode.FormatString = ""
        loanCode.HeaderText = "Loan Code"
        loanCode.Name = "LoanCode"
        loanCode.Width = 100
        loanCode.ReadOnly = True
        loanCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(loanCode)

        bankCode.FormatString = ""
        bankCode.HeaderText = "Bank Code"
        bankCode.Name = "bankCode"
        bankCode.Width = 100
        bankCode.ReadOnly = True
        bankCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(bankCode)

        payPeriodCode.FormatString = ""
        payPeriodCode.HeaderText = "Pay Period Code"
        payPeriodCode.Name = "PayPeriodCode"
        payPeriodCode.Width = 100
        payPeriodCode.IsVisible = False
        payPeriodCode.ReadOnly = True
        payPeriodCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(payPeriodCode)

        emiNo.FormatString = ""
        emiNo.HeaderText = "EMI No"
        emiNo.Name = "EMINo"
        emiNo.Width = 100
        emiNo.ReadOnly = True
        emiNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(emiNo)

        emiAmount.FormatString = ""
        emiAmount.HeaderText = "EMI Amount"
        emiAmount.Name = "emiAmount"
        emiAmount.Width = 100
        emiAmount.ReadOnly = True
        emiAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(emiAmount)


        adjustPlus.FormatString = ""
        adjustPlus.HeaderText = "Adjustment(+)"
        adjustPlus.Name = coladjustPlus
        adjustPlus.Width = 100
        adjustPlus.ReadOnly = False
        adjustPlus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(adjustPlus)

        adjustMinus.FormatString = ""
        adjustMinus.HeaderText = "Adjustment(-)"
        adjustMinus.Name = coladjustMinus
        adjustMinus.Width = 100
        adjustMinus.ReadOnly = False
        adjustMinus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(adjustMinus)

        netEmiAmount.FormatString = ""
        netEmiAmount.HeaderText = "Net EMI Amount"
        netEmiAmount.Name = "netemiAmt"
        netEmiAmount.Width = 100
        netEmiAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoanGeneration.Columns.Add(netEmiAmount)


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
        dtpGenerateDate.Value = clsCommon.GETSERVERDATE
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLoanGeneration)
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
        findGeneratedBy.Value = Nothing
        lblGeneratedByName.Text = ""
        findPayperiod.Value = Nothing
        lblPayPeriodName.Text = ""
        txtDescription.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvLoanGeneration.Rows.Clear()
        Me.gvLoanGeneration.Rows.AddNew()
        dtpGenerateDate.Value = clsCommon.GETSERVERDATE
        fndLocation.Value = ""
        fndDivision.Value = ""
        lblLocationName.Text = ""
        lblDivisionName.Text = ""
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        obj = clsLoanGeneration.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LOAN_GENERATION_CODE) > 0) Then
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
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.LOAN_GENERATION_CODE
            findPayperiod.Value = obj.PAY_PERIOD_CODE
            lblPayPeriodName.Text = obj.PAY_PERIOD_NAME
            findGeneratedBy.Value = clsCommon.myCstr(obj.GENERATED_BY)
            lblGeneratedByName.Text = clsCommon.myCstr(obj.GENERATED_BY_NAME)
            txtDescription.Text = obj.GENERATE_REMARKS
            Me.dtpGenerateDate.Value = obj.GENERATION_DATE
            fndLocation.Value = obj.LOCATION_CODE
            fndDivision.Value = obj.DEVISION_CODE
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
            lblDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Devision_Name from TSPL_DEVISION_MASTER where Devision_Code='" & fndDivision.Value & "'"))

            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                For Each objtr As clsLoanGenerationDetail In obj.ObjList
                    gvLoanGeneration.Rows.AddNew()
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colempCode).Value = objtr.EMP_CODE
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colempName).Value = objtr.EMP_NAME
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colloanCode).Value = objtr.LOAN_CODE
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colemiNo).Value = objtr.EMI_No
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colemiAmount).Value = objtr.EMI_AMOUNT
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(coladjustPlus).Value = objtr.ADJUSTMENT_PLUS
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(coladjustMinus).Value = objtr.ADJUSTMENT_MINUS
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colnetEmiAmount).Value = objtr.NET_EMI
                    gvLoanGeneration.Rows(gvLoanGeneration.Rows.Count - 1).Cells(colpayPeriodCode).Value = objtr.PAY_PERIOD_CODE
                Next
            Else
                gvLoanGeneration.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_LOAN_GENERATION where LOAN_GENERATION_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select LOAN_GENERATION_CODE as Code, PAY_PERIOD_CODE, GENERATED_BY AS 'Employee Code',GENERATE_REMARKS from TSPL_LOAN_GENERATION "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_LOAN_GENERATION", qry, "Code", "", txtCode.Value, "LOAN_GENERATION_CODE", isButtonClicked)
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
            Dim obj As New clsLoanGeneration
            ObjList = New List(Of clsLoanGenerationDetail)
            obj.LOAN_GENERATION_CODE = Me.txtCode.Value
            obj.PAY_PERIOD_CODE = Me.findPayperiod.Value
            obj.GENERATION_DATE = Me.dtpGenerateDate.Value
            obj.GENERATED_BY = clsCommon.myCstr(Me.findGeneratedBy.Value)
            obj.GENERATE_REMARKS = clsCommon.myCstr(Me.txtDescription.Text)
            obj.LOCATION_CODE = clsCommon.myCstr(Me.fndLocation.Value)
            obj.DEVISION_CODE = clsCommon.myCstr(Me.fndDivision.Value)

            ObjList = New List(Of clsLoanGenerationDetail)
            Dim objtr As clsLoanGenerationDetail
            For Each grow As GridViewRowInfo In gvLoanGeneration.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                    objtr = New clsLoanGenerationDetail()
                    objtr.LOAN_GENERATION_CODE = txtCode.Value
                    objtr.PAY_PERIOD_CODE = findPayperiod.Value
                    objtr.EMP_CODE = clsCommon.myCstr(grow.Cells(colempCode).Value)
                    objtr.LOAN_CODE = clsCommon.myCstr(grow.Cells(colloanCode).Value)
                    objtr.EMI_No = clsCommon.myCstr(grow.Cells(colemiNo).Value)
                    objtr.EMI_AMOUNT = clsCommon.myCstr(grow.Cells(colemiAmount).Value)
                    objtr.ADJUSTMENT_PLUS = clsCommon.myCdbl(grow.Cells(coladjustPlus).Value)
                    objtr.ADJUSTMENT_MINUS = clsCommon.myCdbl(grow.Cells(coladjustMinus).Value)
                    objtr.NET_EMI = clsCommon.myCdbl(grow.Cells(colnetEmiAmount).Value)
                    ObjList.Add(objtr)
                End If
            Next
            obj.ObjList = ObjList
            If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.LOAN_GENERATION_CODE, NavigatorType.Current)
                Return True
            End If
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_LOAN_GENERATION where LOAN_GENERATION_CODE = '" + txtCode.Value + "' "
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
            myMessages.blankValue("Pay Period ")
            findPayperiod.Focus()
            Return False
        End If
        If clsCommon.myLen(findGeneratedBy.Value) <= 0 Then
            myMessages.blankValue("Generated by Code")
            findGeneratedBy.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvLoanGeneration.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colloanCode).Value)) > 0 Then
                ii += 1
            End If
        Next
        '' Anubhooti 10-July-2014 (BM00000002913)
        If CheckSalStructure() = False Then
            Return False
        End If
        Return True
    End Function
    '' Anubhooti 10-July-2014 (BM00000002913)
    Function CheckSalStructure() As Boolean
        For Each grow As GridViewRowInfo In gvLoanGeneration.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colloanCode).Value)) > 0 Then
                If clsLTAClaim.CheckPayHead(clsCommon.myCstr(grow.Cells(colempCode).Value), "Loan".ToUpper(), dtpGenerateDate.Text) = False Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function
    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
        & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        findPayperiod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", findPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        If clsCommon.myLen(findPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
            lblPayPeriodName.Text = clspp.Name
            findPayperiod.Value = clspp.Code
        Else
            lblPayPeriodName.Text = ""

        End If


        '' load existing loan generation for the pay period 
        Dim str As String = "select count(*) from TSPL_LOAN_GENERATION where PAY_PERIOD_CODE ='" + findPayperiod.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            qry = " select top 1 LOAN_GENERATION_CODE as Code, PAY_PERIOD_CODE from TSPL_LOAN_GENERATION WHERE PAY_PERIOD_CODE='" + findPayperiod.Value + "' ORDER BY LOAN_GENERATION_CODE"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Code"))
            End If
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                'funReset()
            End If
        End If
    End Sub

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLoanGeneration.CellEndEdit
        'If Not isCellValueChangedOpen Then
        '    isCellValueChangedOpen = True
        '    If e.Column Is gvLoanGeneration.Columns(colpayHeadCode) Then
        '        Dim strq As String
        '        'strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
        '        '& " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_DAILY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
        '        '& " left join TSPL_HOURLY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"
        '        Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gvLoanGeneration.CurrentRow.Cells(colpayHeadCode).Value), False)
        '        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
        '            gvLoanGeneration.CurrentRow.Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
        '            gvLoanGeneration.CurrentRow.Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
        '            gvLoanGeneration.CurrentRow.Cells(colempCode).Value = clsCommon.myCstr(txtEmpCode.Value)

        '        End If
        '    End If
        '    isCellValueChangedOpen = False
        'End If

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
                If (clsLoanGeneration.DeleteData(txtCode.Value)) Then
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

    Private Sub findGeneratedBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findGeneratedBy._MYValidating
        Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        findGeneratedBy.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findGeneratedBy.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(findGeneratedBy.Value, Nothing)
        If Not clsemp Is Nothing Then
            lblGeneratedByName.Text = clsemp.Emp_Name
        End If

    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Generate_Loan()
    End Sub
    Sub Generate_Loan()
        '=========UpDate by preeti Gupta Against ticket no[ERO/06/02/19-000487]
        Dim strq As String
        strq = "SELECT LA.EMP_CODE,EMP.EMP_NAME,LA.Bank_code,LA.LOAN_CODE,LA.bank_code,LA.LOAN_DATE,LA.EMI_NO,LA.EMI_AMOUNT," _
            & " COALESCE(ADJ.ADJUSTMENT_PLUS,0) AS ADJUSTMENT_PLUS,COALESCE(ADJ.ADJUSTMENT_MINUS,0) AS ADJUSTMENT_MINUS " _
            & " ,(LA.EMI_AMOUNT+COALESCE(ADJ.ADJUSTMENT_PLUS,0)-COALESCE(ADJ.ADJUSTMENT_MINUS,0)) AS NET_EMI FROM ( " _
            & " select T1.EMP_CODE,T1.LOAN_CODE,T1.LOAN_DATE,MIN(T2.EMI_NO) AS EMI_NO,T2.EMI_AMOUNT,T1.bank_code " _
            & " from TSPL_LOAN_APPLICATION T1 JOIN TSPL_LOANEMI_DETAIL T2 ON T1.LOAN_CODE=T2.LOAN_CODE " _
            & " LEFT JOIN (SELECT TT1.LOAN_GENERATION_CODE,TT2.LOAN_CODE,TT2.EMP_CODE,TT2.EMI_NO " _
            & " FROM TSPL_LOAN_GENERATION TT1 JOIN TSPL_LOANGENERATION_DETAIL TT2 " _
            & " ON TT1.LOAN_GENERATION_CODE=TT2.LOAN_GENERATION_CODE WHERE TT1.PAY_PERIOD_CODE!='" & clsCommon.myCstr(Me.findPayperiod.Value) & "') AS T3 " _
            & " ON T2.LOAN_CODE=T3.LOAN_CODE AND T2.EMI_NO=T3.EMI_NO WHERE T3.EMI_NO IS NULL and T1.PAID=1 and T1.POSTED =1 and t1.LOAN_DATE <=(select convert(date,DATE_TO,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & clsCommon.myCstr(Me.findPayperiod.Value) & "')  " _
            & " GROUP BY T1.LOAN_CODE,T1.LOAN_DATE,T1.EMP_CODE,T2.EMI_AMOUNT,T1.bank_code) AS LA " _
            & " LEFT JOIN (select  ADJ.EMP_CODE,ADJ.LOAN_CODE,SUM(ADJ.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS," _
            & " SUM(ADJ.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
            & " from TSPL_LOAN_ADJUSTMENT ADJ WHERE ADJ.PAY_PERIOD_CODE='" & clsCommon.myCstr(Me.findPayperiod.Value) & "' AND GENERATED=0 " _
            & " GROUP BY ADJ.EMP_CODE,ADJ.LOAN_CODE) ADJ ON LA.EMP_CODE=ADJ.EMP_CODE AND LA.LOAN_CODE=ADJ.LOAN_CODE " _
            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON LA.EMP_CODE=EMP.EMP_CODE " _
            & " left join tspl_location_master on tspl_location_master.Location_Code =EMP.LOCATION_CODE left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =emp.DEVISION_CODE " _
            & " where 2=2 "
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            strq += " and emp.LOCATION_CODE ='" & fndLocation.Value & "'"
        End If
        If clsCommon.myLen(fndDivision.Value) > 0 Then
            strq += "and emp.DEVISION_CODE ='" & fndDivision.Value & "'"
        End If
        strq += " ORDER BY  LA.EMP_CODE,LA.LOAN_CODE "
        Dim dt_loan As DataTable
        dt_loan = clsDBFuncationality.GetDataTable(strq)
        gvLoanGeneration.Rows.Clear()
        For intLoop As Integer = 0 To dt_loan.Rows.Count - 1
            gvLoanGeneration.Rows.AddNew()

            gvLoanGeneration.Rows(intLoop).Cells(colempCode).Value = dt_loan.Rows(intLoop).Item("EMP_CODE")
            gvLoanGeneration.Rows(intLoop).Cells(colempName).Value = dt_loan.Rows(intLoop).Item("EMP_NAME")
            gvLoanGeneration.Rows(intLoop).Cells(colloanCode).Value = dt_loan.Rows(intLoop).Item("LOAN_CODE")
            gvLoanGeneration.Rows(intLoop).Cells(colpayPeriodCode).Value = obj.PAY_PERIOD_CODE
            gvLoanGeneration.Rows(intLoop).Cells(colemiNo).Value = dt_loan.Rows(intLoop).Item("EMI_NO")
            gvLoanGeneration.Rows(intLoop).Cells(colemiAmount).Value = dt_loan.Rows(intLoop).Item("EMI_AMOUNT")
            gvLoanGeneration.Rows(intLoop).Cells(coladjustPlus).Value = dt_loan.Rows(intLoop).Item("ADJUSTMENT_PLUS")
            gvLoanGeneration.Rows(intLoop).Cells(coladjustMinus).Value = dt_loan.Rows(intLoop).Item("ADJUSTMENT_MINUS")
            gvLoanGeneration.Rows(intLoop).Cells(colnetEmiAmount).Value = dt_loan.Rows(intLoop).Item("NET_EMI")
            gvLoanGeneration.Rows(intLoop).Cells(colBankCode).Value = dt_loan.Rows(intLoop).Item("Bank_code")

        Next
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsLoanGeneration.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub gvLoanGeneration_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvLoanGeneration.CellValueChanged
        Try
            If Not isInsideLoadData Then
                isInsideLoadData = True
                If e.Column Is gvLoanGeneration.Columns(coladjustMinus) Then
                    gvLoanGeneration.CurrentRow.Cells(colnetEmiAmount).Value = (clsCommon.myCdbl(gvLoanGeneration.CurrentRow.Cells(colemiAmount).Value) - clsCommon.myCdbl(gvLoanGeneration.CurrentRow.Cells(coladjustMinus).Value))
                    gvLoanGeneration.CurrentRow.Cells(coladjustPlus).Value = 0
                    gvLoanGeneration.CurrentRow.Cells(coladjustPlus).ReadOnly = True
                ElseIf e.Column Is gvLoanGeneration.Columns(coladjustPlus) Then
                    gvLoanGeneration.CurrentRow.Cells(colnetEmiAmount).Value = (clsCommon.myCdbl(gvLoanGeneration.CurrentRow.Cells(colemiAmount).Value) + clsCommon.myCdbl(gvLoanGeneration.CurrentRow.Cells(coladjustPlus).Value))
                    gvLoanGeneration.CurrentRow.Cells(coladjustMinus).ReadOnly = True
                    gvLoanGeneration.CurrentRow.Cells(coladjustMinus).Value = 0
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLoanGeneration_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gvLoanGeneration.CellClick
        Dim qry As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HEAD_TYPE  from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE ='LOAN'"))
        If clsCommon.CompairString(clsCommon.myCstr(qry), "UD") = CompairStringResult.Equal Then
            gvLoanGeneration.CurrentRow.Cells(coladjustMinus).ReadOnly = False
            gvLoanGeneration.CurrentRow.Cells(coladjustPlus).ReadOnly = False
        Else
            gvLoanGeneration.CurrentRow.Cells(coladjustMinus).ReadOnly = True
            gvLoanGeneration.CurrentRow.Cells(coladjustPlus).ReadOnly = True
        End If
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocation.Value, isButtonClicked)
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub fndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivision._MYValidating
        fndDivision.Value = clsDevisionMaster.getFinder("", Me.fndDivision.Value, isButtonClicked)
        If clsCommon.myLen(fndDivision.Value) > 0 Then
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & fndDivision.Value & "'")
        Else
            lblDivisionName.Text = ""
        End If
    End Sub
End Class