Imports common
Imports XpertERPEngine

Public Class frmLoanAdjustment
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As New clsLoanAdjustment()
            obj.LOANADJUSTMENT_CODE = txtCode.Value
            obj.EMP_CODE = clsCommon.myCstr(Me.txtEmpCode.Value)
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(Me.findPayperiod.Value)

            obj.ADJUSTMENT_DATE = Format(Me.dtpLoanAdjustDate.Value, "dd MMM yyyy")
            obj.LOAN_CODE = clsCommon.myCstr(txtLoanCode.Value)
            obj.ADJUSTMENT_BY_CODE = clsCommon.myCstr(findLoanAdjustby.Value)
            obj.ADJUSTMENT_PLUS = clsCommon.myCdbl(txtAdjustPlus.Text)
            obj.ADJUSTMENT_MINUS = clsCommon.myCdbl(txtAdjustMinus.Text)
            obj.ADJUSTMENT_REASON = clsCommon.myCstr(txtDescription.Text)
            If (obj.SaveData(obj, txtCode.Value, isNewEntry)) Then
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.LOANADJUSTMENT_CODE, NavigatorType.Current)
                Return True
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.LOANADJUSTMENT_CODE & "' already exist ")
            End If
        End If
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ''txtCode.MyReadOnly = True
        'btnsave.Enabled = True
        'btndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsLoanAdjustment()
        obj = clsLoanAdjustment.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LOANADJUSTMENT_CODE) > 0) Then
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
            Me.txtCode.Value = obj.LOANADJUSTMENT_CODE
            Me.txtEmpCode.Value = obj.EMP_CODE
            Me.lblEmpName.Text = obj.EMP_NAME
            Me.txtLoanCode.Value = obj.LOAN_CODE
            Me.findPayperiod.Value = obj.PAY_PERIOD_CODE
            Me.lblPayPeriodName.Text = obj.PAY_PERIOD_NAME
            Me.dtpLoanAdjustDate.Value = obj.ADJUSTMENT_DATE
            Me.txtAdjustPlus.Text = clsCommon.myFormat(obj.ADJUSTMENT_PLUS, False, True, True)
            Me.txtAdjustMinus.Text = clsCommon.myFormat(obj.ADJUSTMENT_MINUS, False, True, True)
            Me.txtDescription.Text = obj.ADJUSTMENT_REASON
            findLoanAdjustby.Value = obj.ADJUSTMENT_BY_CODE
            lblAdjustedByName.Text = obj.ADJUSTMENT_BY_NAME
            txtCode.MyReadOnly = True
        End If
    End Sub

    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_LOAN_ADJUSTMENT where LOANADJUSTMENT_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue(" LOAN ADJUSTMENT CODE ")
        '    txtCode.Focus()
        '    Return False
        'Else
        If clsCommon.myLen(txtLoanCode.Value) <= 0 Then
            myMessages.blankValue("Loan Code ")
            txtLoanCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtEmpCode.Value) <= -1 Then
            myMessages.blankValue("Employee Code ")
            txtEmpCode.Focus()
            Return False
        ElseIf (clsCommon.myCdbl(txtAdjustPlus.Text) = 0 And clsCommon.myCdbl(txtAdjustMinus.Text) = 0) Or (clsCommon.myCdbl(txtAdjustPlus.Text) > 0 And clsCommon.myCdbl(txtAdjustMinus.Text) > 0) Then
            myMessages.blankValue("Invaild Adjustment Amount")
            txtAdjustPlus.Focus()
            Return False
        ElseIf txtDescription.Text = "" Then
            myMessages.blankValue("Loan Adjustment Reason")
            txtDescription.Focus()
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
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select LOANADJUSTMENT_CODE  from TSPL_SHIPMENT_DETAILS  where LOANADJUSTMENT_CODE ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If
        '' LOANADJUSTMENT_CODE Ends 
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
                If (clsLoanAdjustment.DeleteData(txtCode.Value)) Then
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtcode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub frmLeaveMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        Me.txtEmpCode.Value = Nothing
        Me.lblEmpName.Text = ""
        Me.dtpLoanAdjustDate.Value = Today
        Me.txtDescription.Text = Nothing
        Me.txtLoanCode.Value = Nothing
        Me.findPayperiod.Value = Nothing
        Me.lblPayPeriodName.Text = ""
        Me.txtAdjustPlus.Text = ""
        Me.txtAdjustMinus.Text = ""
        Me.findLoanAdjustby.Value = Nothing
        Me.lblAdjustedByName.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_LOAN_ADJUSTMENT where LOANADJUSTMENT_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "select T1.LOANADJUSTMENT_CODE AS LOANADJUSTMENT_CODE,T1.EMP_CODE,T2.EMP_NAME AS EMPLOYEE_NAME,T1.ADJUSTMENT_DATE,T1.PAY_PERIOD_CODE  from TSPL_LOAN_ADJUSTMENT T1 " _
            & " LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE"
            txtCode.Value = clsCommon.ShowSelectForm("EMP_STATUS", qry, "LOANADJUSTMENT_CODE", "", txtCode.Value, "LOANADJUSTMENT_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Sub funFill()

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

    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Try
            Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
            & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
            'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
            findPayperiod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", findPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
            If clsCommon.myLen(findPayperiod.Value) > 0 Then
                Dim clspp As clsPayPeriodMaster
                clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
                lblPayPeriodName.Text = clspp.Name
            Else
                lblPayPeriodName.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub findLoanAdjustby__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findLoanAdjustby._MYValidating

        Try
            Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
            findLoanAdjustby.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findLoanAdjustby.Value, "", isButtonClicked)
            Dim clsemp As clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(findLoanAdjustby.Value, Nothing)
            lblAdjustedByName.Text = clsemp.Emp_Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtLoanCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLoanCode._MYValidating

        Try
            Dim qry As String = "SELECT LOAN_CODE as Code,LOAN_DATE as LOAN_DATE,EMP_CODE FROM TSPL_LOAN_APPLICATION "
            txtLoanCode.Value = clsCommon.ShowSelectForm("TSPL_LOAN_APPLICATION", qry, "Code", "", txtLoanCode.Value, "", isButtonClicked)
            Dim clsemp As clsApplyLoan
            clsemp = clsApplyLoan.GetData(txtLoanCode.Value, NavigatorType.Current)
            lblLoanName.Text = clsemp.LOAN_DESCRIPTION
            txtEmpCode.Value = clsemp.EMP_CODE
            lblEmpName.Text = clsemp.EMP_NAME
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsLoanAdjustment.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class