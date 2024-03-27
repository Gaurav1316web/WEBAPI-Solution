Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmReimbursementDetails
    Inherits FrmMainTranScreen
    Const colempCode As String = "empCode"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colReimbursementAmount As String = "ReimbursementAmount"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsReimbursementDetails
    Private ObjList As New List(Of clsReimbursementDetails)
    Private isCellValueChangedOpen As Boolean = False


    Sub LoadGridColumns()
        gvReimbursement.Rows.Clear()
        gvReimbursement.Columns.Clear()

        Dim empCode As New GridViewTextBoxColumn()
        Dim payHeadCode As New GridViewTextBoxColumn
        Dim payHeadName As New GridViewTextBoxColumn
        Dim reimbursement As New GridViewDecimalColumn

        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = "empCode"
        empCode.Width = 100
        empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvReimbursement.Columns.Add(empCode)

        payHeadCode.FormatString = ""
        payHeadCode.HeaderText = "Pay Head Code"
        payHeadCode.Name = "PayHeadCode"
        payHeadCode.Width = 100
        'payHeadCode.ReadOnly = True
        payHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvReimbursement.Columns.Add(payHeadCode)

        payHeadName.FormatString = ""
        payHeadName.HeaderText = "Pay Head Name"
        payHeadName.Name = "PayHeadName"
        payHeadName.Width = 100
        payHeadName.ReadOnly = True
        payHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvReimbursement.Columns.Add(payHeadName)


        reimbursement.FormatString = ""
        reimbursement.HeaderText = "Reimbursement Amount"
        reimbursement.Name = "ReimbursementAmount"
        reimbursement.Width = 100
        reimbursement.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvReimbursement.Columns.Add(reimbursement)

    End Sub

    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SavingData(False)
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
                If clsReimbursementDetails.ReverseAndUnpost(txtCode.Value) Then
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
        btnReverse.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmReimbursementDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnReverse.Visible = False

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
        txtDescription.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvReimbursement.Rows.Clear()
        Me.gvReimbursement.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ' btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsReimbursementDetails.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.REIMBURSEMENT_CODE) > 0) Then
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
            txtCode.Value = obj.REIMBURSEMENT_CODE
            findPayperiod.Value = obj.PAY_PERIOD_CODE
            'txtAdjustBy.Value = obj.ADJUSTMENT_BY_Code
            'lblAdjustmentByName.Text = obj.ADJUSTMENT_BY_Name
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.EMP_NAME)
            txtDescription.Text = obj.REIMBURSEMENT_REMARK
            lblPayPeriodName.Text = obj.PAY_PERIOD_NAME
            txtCode.MyReadOnly = True

            If (clsReimbursementDetails.ObjList IsNot Nothing AndAlso clsReimbursementDetails.ObjList.Count > 0) Then
                For Each obj As clsReimbursementDetails In clsReimbursementDetails.ObjList
                    gvReimbursement.Rows.AddNew()

                    gvReimbursement.Rows(gvReimbursement.Rows.Count - 1).Cells(colempCode).Value = obj.empCode
                    gvReimbursement.Rows(gvReimbursement.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PayHeadCode
                    gvReimbursement.Rows(gvReimbursement.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PayHeadName
                    gvReimbursement.Rows(gvReimbursement.Rows.Count - 1).Cells(colReimbursementAmount).Value = obj.REIMBURSEMENT_AMOUNT
                    'gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(coladjustMinus).Value = obj.AdjustMinus

                Next
            Else
                gvReimbursement.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_EMP_REIMBURSEMENT where REIMBURSEMENT_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select REIMBURSEMENT_CODE as Code, PAY_PERIOD_CODE, EMP_CODE AS 'Employee Name',REIMBURSEMENT_REMARK from TSPL_EMP_REIMBURSEMENT "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_EMP_REIMBURSEMENT", qry, "Code", "", txtCode.Value, "REIMBURSEMENT_CODE", isButtonClicked)
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
                Dim obj As clsReimbursementDetails = Nothing
                ObjList = New List(Of clsReimbursementDetails)
                For Each grow As GridViewRowInfo In gvReimbursement.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                        obj = New clsReimbursementDetails()

                        obj.REIMBURSEMENT_CODE = txtCode.Value
                        obj.PAY_PERIOD_CODE = findPayperiod.Value
                        obj.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                        obj.EMP_CODE = clsCommon.myCstr(grow.Cells(colempCode).Value)
                        obj.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                        obj.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                        obj.REIMBURSEMENT_AMOUNT = clsCommon.myCdbl(grow.Cells(colReimbursementAmount).Value)
                        obj.REIMBURSEMENT_DATE = Me.dtpReimbursementDate.Value
                        obj.REIMBURSEMENT_REMARK = Me.txtDescription.Text
                        ObjList.Add(obj)
                    End If
                Next
                If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                    LoadData(obj.REIMBURSEMENT_CODE, NavigatorType.Current)
                    Return True
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
            Return False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_EMP_REIMBURSEMENT where REIMBURSEMENT_CODE = '" + txtCode.Value + "' "
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
            myMessages.blankValue(Me, "Pay Period Code", Me.Text)
            findPayperiod.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvReimbursement.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                ii += 1
                If clsCommon.myCdbl(grow.Cells(colReimbursementAmount).Value) = 0 Then

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

    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
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



    End Sub

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvReimbursement.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvReimbursement.Columns(colpayHeadCode) Then
                ' Dim strq As String
                'strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
                '& " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_DAILY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
                '& " left join TSPL_HOURLY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"
                Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gvReimbursement.CurrentRow.Cells(colpayHeadCode).Value), False)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
                    gvReimbursement.CurrentRow.Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvReimbursement.CurrentRow.Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                    gvReimbursement.CurrentRow.Cells(colempCode).Value = clsCommon.myCstr(txtEmpCode.Value)

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
                If (clsReimbursementDetails.DeleteData(txtCode.Value)) Then
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

    Private Sub txtEmpCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
        txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtEmpCode.Value, "", isButtonClicked)
        Dim clsemp As clsEmployeeMaster
        clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
        lblEmpName.Text = clsemp.Emp_Name

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsReimbursementDetails.PostData(txtCode.Value, True)) Then
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

End Class