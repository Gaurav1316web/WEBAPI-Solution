Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

'' changes by shivani[BM00000007892]
Public Class frmAdjustmentVoucher
    Inherits FrmMainTranScreen
    Const colCheck As String = "Check"
    Const colempCode As String = "empCode"
    Const colempName As String = "empName"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colAdjustType As String = "colAdjustType"
    Const coladjustPlus As String = "AdjustmentPlus"
    Const coladjustMinus As String = "AdjustmentMinus"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsAdjustmentVoucher
    Private ObjList As New List(Of clsAdjustmentVoucherDetail)
    Private isCellValueChangedOpen As Boolean = False


    Sub LoadGridColumns()
        Dim Check As New GridViewCheckBoxColumn
        Dim empCode As New GridViewTextBoxColumn
        Dim empName As New GridViewTextBoxColumn
        Dim payHeadCode As New GridViewTextBoxColumn
        Dim payHeadName As New GridViewTextBoxColumn
        Dim AdjustType As New GridViewComboBoxColumn
        Dim adjustPlus As New GridViewDecimalColumn
        Dim adjustMinus As New GridViewDecimalColumn

        gvAdjustmentVoucher.Rows.Clear()
        gvAdjustmentVoucher.Columns.Clear()

        Check.FormatString = ""

        Check.Name = colCheck
        Check.Width = 50
        Check.ReadOnly = False
        Check.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(Check)

        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = colempCode
        empCode.Width = 100
        empCode.ReadOnly = False
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Code"
        empName.Name = colempName
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(empName)

        payHeadCode.FormatString = ""
        payHeadCode.HeaderText = "Pay Head Code"
        payHeadCode.Name = colpayHeadCode
        payHeadCode.Width = 100
        'payHeadCode.ReadOnly = True
        payHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(payHeadCode)

        payHeadName.FormatString = ""
        payHeadName.HeaderText = "Pay Head Name"
        payHeadName.Name = colpayHeadName
        payHeadName.Width = 100
        payHeadName.ReadOnly = True
        payHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(payHeadName)

        AdjustType.FormatString = ""
        AdjustType.HeaderText = "Adjustment Type"
        AdjustType.Name = colAdjustType
        AdjustType.Width = 100
        AdjustType.ReadOnly = False
        AdjustType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        AdjustType.DataSource = clsAdjustmentVoucher.GetAdjustTypeDataTable()
        AdjustType.ValueMember = "Code"
        AdjustType.DisplayMember = "Name"
        gvAdjustmentVoucher.Columns.Add(AdjustType)

        adjustPlus.FormatString = ""
        adjustPlus.HeaderText = "Adjustment Plus"
        adjustPlus.Name = coladjustPlus
        adjustPlus.Width = 100
        adjustPlus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(adjustPlus)

        adjustMinus.FormatString = ""
        adjustMinus.HeaderText = "Adjustment Minus"
        adjustMinus.Name = coladjustMinus
        adjustMinus.Width = 100
        adjustMinus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAdjustmentVoucher.Columns.Add(adjustMinus)

        gvAdjustmentVoucher.EnableFiltering = True
    End Sub

    Private Sub frmAdjustmentVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        End If
    End Sub

    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        funReset()
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
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAdjustmentVoucher)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenu2.Visible = MyBase.isExport
    End Sub


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            funReset()
        Catch ex As Exception

        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        txtAdjustBy.Value = Nothing
        lblAdjustmentByName.Text = ""
        findPayperiod.Value = Nothing
        lblPayPeriodName.Text = ""
        txtEmpCode.Value = Nothing
        lblEmpName.Text = ""
        dtpAdjustDate.Value = clsCommon.GETSERVERDATE()
        txtDescription.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvAdjustmentVoucher.Rows.Clear()
        Me.gvAdjustmentVoucher.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsAdjustmentVoucher.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ADJUSTMENT_CODE) > 0) Then
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
            txtcode.Value = obj.ADJUSTMENT_CODE
            findPayperiod.Value = obj.PAY_PERIOD_CODE
            txtAdjustBy.Value = obj.ADJUSTMENT_BY_Code
            lblAdjustmentByName.Text = obj.ADJUSTMENT_BY_Name
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.EMP_NAME)
            txtDescription.Text = obj.ADJUSTMENT_REMARK
            lblPayPeriodName.Text = obj.PAY_PERIOD_NAME
            txtcode.MyReadOnly = True
            dtpAdjustDate.Value = obj.ADJUSTMENT_DATE
            gvAdjustmentVoucher.Rows.Clear()
            gvAdjustmentVoucher.Rows.AddNew()
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                For Each objtr As clsAdjustmentVoucherDetail In obj.ObjList

                    gvAdjustmentVoucher.CurrentRow.Cells(colCheck).Value = True
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempCode).Value = objtr.empCode
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from Tspl_Employee_master where emp_code='" & gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempCode).Value & "'"))
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colpayHeadCode).Value = objtr.PayHeadCode
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colpayHeadName).Value = objtr.PayHeadName
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colAdjustType).Value = objtr.ADJUSTMENT_TYPE
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(coladjustPlus).Value = objtr.adjust_plus
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(coladjustMinus).Value = objtr.adjust_minus
                    gvAdjustmentVoucher.Rows.AddNew()
                Next
            Else
                gvAdjustmentVoucher.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_ADJUSTMENT_VOUCHER where ADJUSTMENT_CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtcode.MyReadOnly = True
        End If
        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select ADJUSTMENT_CODE as Code, PAY_PERIOD_CODE,ADJUSTMENT_REMARK from TSPL_ADJUSTMENT_VOUCHER "
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_ADJUSTMENT_VOUCHER", qry, "Code", "", txtcode.Value, "ADJUSTMENT_CODE", isButtonClicked)
            If txtcode.Value <> "" Then
                LoadData(txtcode.Value, NavigatorType.Current)
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
            Dim obj As New clsAdjustmentVoucher
            obj.ADJUSTMENT_CODE = txtcode.Value
            obj.ADJUSTMENT_DATE = Me.dtpAdjustDate.Value
            obj.ADJUSTMENT_BY_Code = Me.txtAdjustBy.Value
            obj.PAY_PERIOD_CODE = findPayperiod.Value
            obj.ADJUSTMENT_REMARK = txtDescription.Text
            obj.EMP_CODE = txtEmpCode.Value

            ObjList = New List(Of clsAdjustmentVoucherDetail)
            Dim objtr As clsAdjustmentVoucherDetail

            For Each grow As GridViewRowInfo In gvAdjustmentVoucher.Rows
                If grow.Cells(colCheck).Value = True Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAdjustType).Value)) > 0 Then
                        objtr = New clsAdjustmentVoucherDetail()
                        objtr.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                        objtr.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                        objtr.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                        objtr.ADJUSTMENT_TYPE = clsCommon.myCstr(grow.Cells(colAdjustType).Value)
                        objtr.adjust_plus = clsCommon.myCdbl(grow.Cells(coladjustPlus).Value)
                        objtr.adjust_minus = clsCommon.myCdbl(grow.Cells(coladjustMinus).Value)
                        ObjList.Add(objtr)
                    End If
                End If
            Next
            obj.ObjList = ObjList
            If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtcode.Value))) Then
                LoadData(obj.ADJUSTMENT_CODE, NavigatorType.Current)
                Return True
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean
        Dim qry As Integer = 0
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_ADJUSTMENT_VOUCHER where ADJUSTMENT_CODE = '" + txtcode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        'If clsCommon.myLen(txtcode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtcode.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            myMessages.blankValue(Me, "Pay Period Code", Me.Text)
            findPayperiod.Focus()
            Return False
        End If
        'If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
        '    myMessages.blankValue("Employee Code")
        '    txtEmpCode.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Remark Code", Me.Text)
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(dtpAdjustDate.Value) <= 0 Then
            myMessages.blankValue(Me, "Adjustment Date", Me.Text)
            dtpAdjustDate.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        Dim arrEmpcode As New List(Of String)
        arrEmpcode = New List(Of String)
        'arrPayHeadcode = New ArrayList(2)
        For Each grow As GridViewRowInfo In gvAdjustmentVoucher.Rows
            If clsCommon.myCBool(grow.Cells(colCheck).Value) = True Then

                '======Update By Preeti Gupta Against Ticket No[BM00000008208]==============
                If ii = 1 Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Enter Employee detail.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill Employee code at Line No " & (ii + 1) & " ", Me.Text)
                    Return False
                End If

                If clsCommon.myLen(grow.Cells(colpayHeadCode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill PayHead code at Line No " & (ii + 1) & "", Me.Text)
                    Return False

                End If

                If clsCommon.myLen(grow.Cells(colAdjustType).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill AdjustType code at Line No " & (ii + 1) & "", Me.Text)
                    Return False
                End If

                If clsCommon.myLen(grow.Cells(coladjustPlus).Value) <= 0 AndAlso clsCommon.myLen(grow.Cells(coladjustMinus).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Fill  Adjustment Plus or Adjustment Minus code at Line No " & (ii + 1) & "")
                    Return False
                End If


                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                    If (clsCommon.myCdbl(grow.Cells(coladjustPlus).Value) > 0 And (clsCommon.myCdbl(grow.Cells(coladjustMinus).Value) > 0)) Then
                        clsCommon.MyMessageBoxShow(Me, "Adjustment can Nagative or Positive Not Both. Condition Fales in Row No : " + ii.ToString() + ".", Me.Text)
                        Return False
                    End If
                End If
                If (clsCommon.myCdbl(grow.Cells(coladjustPlus).Value) > 0 Or (clsCommon.myCdbl(grow.Cells(coladjustMinus).Value) > 0)) Then
                    Dim strCode As String = clsCommon.myCstr(grow.Cells(colAdjustType).Value)
                    If strCode.Length > 30 Or clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Adjustment Type is blank or incorrect.")
                    End If
                    If clsCommon.CompairString(strCode, "PA") <> CompairStringResult.Equal And clsCommon.CompairString(strCode, "AR") <> CompairStringResult.Equal Then
                        Throw New Exception("Adjustment Type must be PA or AR.")
                    End If
                End If
                If clsCommon.myLen(grow.Cells(colempCode).Value) > 0 AndAlso Not arrEmpcode.Contains(clsCommon.myCstr(grow.Cells(colempCode).Value) + clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) Then
                    arrEmpcode.Add(clsCommon.myCstr(grow.Cells(colempCode).Value) + clsCommon.myCstr(grow.Cells(colpayHeadCode).Value))
                Else
                    clsCommon.MyMessageBoxShow(Me, "Duplicate Employee with same PayHead at  row no. " & (ii + 1) & " ", Me.Text)
                    Return False
                    'Throw New Exception("Duplicate Employee at row no. " + clsCommon.myCstr(gvAdjustmentVoucher.CurrentRow.Index) + "")
                End If
                If arrEmpcode Is Nothing OrElse arrEmpcode.Count <= 0 Then
                    Throw New Exception("Select atleast one employee in grid.")
                End If
                qry = clsDBFuncationality.getSingleValue(" select  count(*) from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_GENERATE_Salary on TSPL_GENERATE_Salary.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where EMP_CODE ='" & (clsCommon.myCstr(grow.Cells(colempCode).Value)) & "' and POSTED = 1 and PAY_PERIOD_CODE ='" & findPayperiod.Value & "'")
                If qry > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Salary already posted for this Employee for the payperiod '" & findPayperiod.Value & "' at row no. " & (ii + 1) & "  ", Me.Text)
                    Return False
                End If
                ii += 1
            End If

            If ii = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atlest one check box ", Me.Text)
                Return False
            End If
        Next
        Return True






    End Function

    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        findPayperiod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", " POSTED=1 AND FREEZED=0 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103)", findPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(findPayperiod.Value, Nothing)
    End Sub

    Private Sub findEnteredBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAdjustBy._MYValidating
        Dim qry As String = "SELECT EMP_CODE AS 'Code', EMP_Name as 'Employee Name' FROM TSPL_EMPLOYEE_MASTER "
        txtAdjustBy.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", " Emp_Status<>'Inactive'", txtAdjustBy.Value, "EMP_CODE", isButtonClicked)
        lblAdjustmentByName.Text = clsEmployeeMaster.GetName(txtAdjustBy.Value, Nothing)
    End Sub
    Sub OpenEmpList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            Dim qry As String = "select TSPL_EMPLOYEE_STATUS.EMP_CODE as [Code] ,Emp_Name,TSPL_EMPLOYEE_MASTER.Designation  from TSPL_EMPLOYEE_STATUS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_EMPLOYEE_STATUS.EMP_CODE"
            Dim whrCls As String = "WORKING_STATUS='Working' and TSPL_EMPLOYEE_STATUS.Location_Code='" & FndLocationCode.Value & "'"
            gvAdjustmentVoucher.CurrentRow.Cells(colempCode).Value = clsCommon.ShowSelectForm("fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvAdjustmentVoucher.CurrentRow.Cells(colempCode).Value), "Code", isButtonClick)
            gvAdjustmentVoucher.CurrentRow.Cells(colempName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" & clsCommon.myCstr(gvAdjustmentVoucher.CurrentRow.Cells(colempCode).Value) & "'"))
        End If
    End Sub
    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAdjustmentVoucher.CellEndEdit
        '' changed by shivani against ticket No: BM00000008105
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True

            If e.Column Is gvAdjustmentVoucher.Columns(colpayHeadCode) Then
                Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gvAdjustmentVoucher.CurrentRow.Cells(colpayHeadCode).Value), False)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
                    gvAdjustmentVoucher.CurrentRow.Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
                    gvAdjustmentVoucher.CurrentRow.Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
                    'gvAdjustmentVoucher.CurrentRow.Cells(colempCode).Value = clsCommon.myCstr(txtEmpCode.Value)
                End If
            End If
            If e.Column Is gvAdjustmentVoucher.Columns(colempCode) Then
                OpenEmpList(False)
            End If

            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
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
                If (clsAdjustmentVoucher.DeleteData(txtcode.Value)) Then
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtcode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub txtEmpCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        'Dim qry As String = "SELECT EMP_CODE AS 'Code', EMP_Name as 'Employee Name' FROM TSPL_EMPLOYEE_MASTER "
        txtEmpCode.Value = clsEmployeeMaster.getFinder("", txtEmpCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtEmpCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)


    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsAdjustmentVoucher.PostData(txtcode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        '' export balnk sheet
        'Dim str As String
        'str = " select '' as [Employee Code],'' as [Pay Head Code],'' as [Pay Head Name],0 as [Adjustment Plus]"
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
            str = " select TSPL_EMPLOYEE_MASTER.Emp_Code as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],'' as [Pay Head Code],'' as [Pay Head Name],'' as [Adjustment Type],0 as [Adjustment Plus],0 as [Adjustment Minus] FROM TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.Location_Code "
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
                        WhrCls = " AND ISNULL(TSPL_EMPLOYEE_MASTER.DEVISION_CODE,'') ='" & DivCode & "' "
                    Else
                        clsCommon.MyMessageBoxShow(Me, "First select division code.", Me.Text)
                        Return
                    End If
                Else
                    WhrCls = ""
                End If
                transportSql.ExporttoExcel(str, " AND TSPL_LOCATION_MASTER.Location_Code ='" & LocCode & "'" & WhrCls & "", Me)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "First select location code.", Me.Text)
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        '' import  BY PANCH RAJ AGAINST TICKET NO:BM00000007892
        gvAdjustmentVoucher.Rows.Clear()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Employee Code", "Employee Name", "Pay Head Code", "Adjustment Type", "Pay Head Name", "Adjustment Plus", "Adjustment Minus") Then

            Try
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    gvAdjustmentVoucher.Rows.AddNew()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    'If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                    '    Throw New Exception("Employee Code at line no " & (grow.Index + 1) & " can not be blank or incorrect.")
                    'End If
                    If clsCommon.myLen(clsEmployeeMaster.CheckExistence(strCode, Nothing)) <= 0 Then
                        Throw New Exception("Employee Code " & strCode & " at line no " & (grow.Index + 1) & " does not exist.")
                    End If
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempCode).Value = strCode
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempName).Value = clsCommon.myCstr(grow.Cells("Employee Name").Value)
                    strCode = clsCommon.myCstr(grow.Cells("Pay Head Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Pay Head Code can not be blank or incorrect.")
                    End If

                    If clsPayHeadDefinitions.CheckNewEntry(strCode) = True Then
                        Throw New Exception("Pay Head Code " & strCode & " at line no " & (grow.Index + 1) & " does not exist.")
                    End If

                    'If clsPayHeadDefinitions.GetPayHeadEarningDeductionType(strCode) = 0 Then
                    '    Throw New Exception("Pay Head Code at line no " & (grow.Index + 1) & " is Deduction type.")
                    'End If
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colpayHeadCode).Value = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Pay Head Name").Value)
                    If strCode.Length > 100 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colpayHeadName).Value = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Adjustment Type").Value)
                    If strCode.Length > 30 Or clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Adjustment Type is blank or incorrect.")
                    End If
                    If clsCommon.CompairString(strCode, "PA") <> CompairStringResult.Equal And clsCommon.CompairString(strCode, "AR") <> CompairStringResult.Equal Then
                        Throw New Exception("Adjustment Type must be PA or AR.")
                    End If
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colAdjustType).Value = strCode

                    Dim AmtPlus As Decimal
                    AmtPlus = clsCommon.myCdbl(grow.Cells("Adjustment Plus").Value)
                    Dim AmtMinus As Decimal
                    AmtMinus = clsCommon.myCdbl(grow.Cells("Adjustment Minus").Value)
                    If AmtMinus = 0 And AmtPlus = 0 Then
                        Throw New Exception("Both Adjustment Plus and Adjustment Minus at line no " & (grow.Index + 1) & " can not be zero.")
                    End If
                    If AmtMinus <> 0 And AmtPlus <> 0 Then
                        Throw New Exception("Both Adjustment Plus and Adjustment Minus at line no " & (grow.Index + 1) & " can not be greater or less than zero.")
                    End If

                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(coladjustPlus).Value = AmtPlus
                    gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(coladjustMinus).Value = AmtMinus
                Next
                clsCommon.ProgressBarHide()
                If Save() = True Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK, Me.Text)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub


    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        lblLocationName.Text = clsLocation.GetName(FndLocationCode.Value, Nothing)
    End Sub
    Sub FillEmployeeGrid()
        If clsCommon.myLen(FndLocationCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location.", Me.Text)
            FndLocationCode.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Pay Period.", Me.Text)
            findPayperiod.Focus()
            Exit Sub
        End If

        gvAdjustmentVoucher.Rows.Clear()
        Dim strq As String = ""
        strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
  & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
  & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
  & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
  & " WHERE WORKING_STATUS='Working' and Location_Code='" & FndLocationCode.Value & "' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
  & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE " _
  & " where TT4.Emp_Status<>'Inactive'"


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)
        For Each drEmp As DataRow In dt.Rows
            gvAdjustmentVoucher.Rows.AddNew()
            gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempCode).Value = drEmp.Item("Code")
            gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(colempName).Value = drEmp.Item("Name")
        Next

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        FillEmployeeGrid()
    End Sub

    Private Sub gvAdjustmentVoucher_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAdjustmentVoucher.CellValueChanged


    End Sub

    Private Sub gvAdjustmentVoucher_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvAdjustmentVoucher.CurrentColumnChanged
        If gvAdjustmentVoucher.RowCount > 0 Then
            Dim intCurrRow As Integer = gvAdjustmentVoucher.CurrentRow.Index
            If intCurrRow = gvAdjustmentVoucher.Rows.Count - 1 Then
                gvAdjustmentVoucher.Rows.AddNew()
                gvAdjustmentVoucher.CurrentRow = gvAdjustmentVoucher.Rows(intCurrRow)

            End If
        End If
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvAdjustmentVoucher.Rows
                grow.Cells(colCheck).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gvAdjustmentVoucher.Rows
                grow.Cells(colCheck).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
End Class