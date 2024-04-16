Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI


Public Class frmEmployeeShiftChange
    Inherits FrmMainTranScreen
    Const colempCode As String = "colempCode"
    Const colempName As String = "colempName"
    Const colShiftCode As String = "colShiftCode"
    Const colShiftDesc As String = "colShiftDesc"
    Const colShiftStartTime As String = "colShiftStartTime"
    Const colShiftEndTime As String = "colShiftEndTime"

    Dim isCellValueChanged As Boolean = False
    Dim isInsideLoadData As Boolean = False

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim obj As New clsEmployeeShiftChange
    Private ObjList As New List(Of clsEmployeeShiftChangeDetail)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()
        gvAllowance.Rows.Clear()
        gvAllowance.Columns.Clear()

        Dim empCode As New GridViewTextBoxColumn()
        Dim empName As New GridViewTextBoxColumn()
        Dim ShiftCode As New GridViewTextBoxColumn
        Dim ShiftDesc As New GridViewTextBoxColumn
        Dim ShiftStartTime As New GridViewTextBoxColumn
        Dim ShiftEndTime As New GridViewTextBoxColumn

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
        empName.Width = 200
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(empName)

        ShiftCode.FormatString = ""
        ShiftCode.HeaderText = "Shift Code"
        ShiftCode.Name = colShiftCode
        ShiftCode.Width = 100
        'ShiftCode.ReadOnly = True
        ShiftCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(ShiftCode)

        ShiftDesc.FormatString = ""
        ShiftDesc.HeaderText = "Shift Description"
        ShiftDesc.Name = colShiftDesc
        ShiftDesc.Width = 100
        ShiftDesc.ReadOnly = True
        ShiftDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(ShiftDesc)


        ShiftStartTime.FormatString = ""
        ShiftStartTime.HeaderText = "Start Time"
        ShiftStartTime.Name = colShiftStartTime
        ShiftStartTime.Width = 100
        ShiftStartTime.ReadOnly = True
        ShiftStartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(ShiftStartTime)

        ShiftEndTime.FormatString = ""
        ShiftEndTime.HeaderText = "End Time"
        ShiftEndTime.Name = colShiftEndTime
        ShiftEndTime.Width = 100
        ShiftEndTime.ReadOnly = True
        ShiftEndTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAllowance.Columns.Add(ShiftEndTime)

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmEmployeeShiftChange)
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
        txtDescription.Text = ""

        btnsave.Text = "Save"
        Me.dtpAllowanceDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvAllowance.Rows.Clear()
        Me.gvAllowance.Rows.AddNew()
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
        isInsideLoadData = True

        obj = clsEmployeeShiftChange.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_SHIFT_CODE) > 0) Then
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
            txtCode.Value = obj.EMP_SHIFT_CODE
            txtDescription.Text = obj.DESCRIPTION
            dtpAllowanceDate.Value = obj.SHIFT_APP_DATE

            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                For Each objTr As clsEmployeeShiftChangeDetail In obj.ObjList
                    gvAllowance.Rows.AddNew()

                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempCode).Value = objTr.EMP_CODE
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colempName).Value = objTr.EMP_NAME
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colShiftCode).Value = objTr.SHIFT_CODE
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colShiftDesc).Value = objTr.SHIFT_Desc
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colShiftStartTime).Value = objTr.SHIFT_Start_Time
                    gvAllowance.Rows(gvAllowance.Rows.Count - 1).Cells(colShiftEndTime).Value = objTr.SHIFT_End_Time
                Next
            Else
                gvAllowance.Rows.AddNew()
            End If
        End If
        isInsideLoadData = False
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD where EMP_SHIFT_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsEmployeeShiftChange.GetFinder("", txtCode.Value, isButtonClicked)
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
                Dim obj As New clsEmployeeShiftChange
                obj.EMP_SHIFT_CODE = Me.txtCode.Value
                obj.SHIFT_APP_DATE = Me.dtpAllowanceDate.Value
                obj.DESCRIPTION = Me.txtDescription.Text

                Dim objTr As clsEmployeeShiftChangeDetail
                ObjList = New List(Of clsEmployeeShiftChangeDetail)
                For Each grow As GridViewRowInfo In gvAllowance.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                        objTr = New clsEmployeeShiftChangeDetail()
                        objTr.EMP_CODE = clsCommon.myCstr(grow.Cells(colempCode).Value)
                        objTr.SHIFT_CODE = clsCommon.myCstr(grow.Cells(colShiftCode).Value)
                        ObjList.Add(objTr)
                    End If
                Next
                obj.ObjList = ObjList

                If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                    LoadData(obj.EMP_SHIFT_CODE, NavigatorType.Current)
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
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD where EMP_SHIFT_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        Dim intCount As Integer = 0
        For Each grow As GridViewRowInfo In gvAllowance.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                intCount += 1
            End If
        Next
        If intCount = 0 Then
            myMessages.blankValue(Me, "Employee Code is blank in all rows.", Me.Text)
            Return False
        End If
        'Math.Round(3.4999,1,MidpointRounding.AwayFromZero)
        Dim arrICode As New List(Of String)()
        For ii As Integer = 0 To gvAllowance.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gvAllowance.Rows(ii).Cells(colempCode).Value)
            Dim strIName As String = clsCommon.myCstr(gvAllowance.Rows(ii).Cells(colempName).Value)
            If clsCommon.myLen(strICode) > 0 Then
                For jj As Integer = 0 To gvAllowance.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strICode, clsCommon.myCstr(gvAllowance.Rows(jj).Cells(colempCode).Value)) = CompairStringResult.Equal) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Employee Name " + strICode.Trim() + "( " + strIName.Trim() + " ) already selected at line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If
                Next
                If clsCommon.myLen(gvAllowance.Rows(ii).Cells(colShiftCode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Select Shift Code for employee code " & gvAllowance.Rows(ii).Cells(colempCode).Value & " at line No: " & ii & "")
                    Return False
                End If
                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
            End If
        Next


        Return True
    End Function


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
                If (clsEmployeeShiftChange.DeleteData(txtCode.Value)) Then
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
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsEmployeeShiftChange.PostData(txtCode.Value, True)) Then
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

    Private Sub gvAllowance_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAllowance.CellValueChanged
        If Not isInsideLoadData Then

            If Not isCellValueChanged Then
                If e.Column Is gvAllowance.Columns(colempCode) Then
                    isCellValueChanged = True
                    Dim whrcls As String = Nothing
                    If clsCommon.myLen(txtCode.Value) <= 0 Then
                        whrcls = " TSPL_EMPLOYEE_MASTER.Emp_Status<>'Inactive'"
                    End If
                    gvAllowance.CurrentRow.Cells(colempCode).Value = clsEmployeeMaster.getFinder(whrcls, gvAllowance.CurrentRow.Cells(colempCode).Value, False)
                    gvAllowance.CurrentRow.Cells(colempName).Value = clsEmployeeMaster.GetName(gvAllowance.CurrentRow.Cells(colempCode).Value, Nothing)
                    isCellValueChanged = False
                End If

                If e.Column Is gvAllowance.Columns(colShiftCode) Then
                    isCellValueChanged = True
                    gvAllowance.CurrentRow.Cells(colShiftCode).Value = clsShiftMaster.getFinder("", gvAllowance.CurrentRow.Cells(colShiftCode).Value, False)
                    Dim objShift As clsShiftMaster = clsShiftMaster.GetData(gvAllowance.CurrentRow.Cells(colShiftCode).Value, Nothing)
                    If Not objShift Is Nothing Then
                        gvAllowance.CurrentRow.Cells(colShiftDesc).Value = objShift.Name
                        gvAllowance.CurrentRow.Cells(colShiftStartTime).Value = objShift.FromTime
                        gvAllowance.CurrentRow.Cells(colShiftEndTime).Value = objShift.ToTime
                    End If
                    isCellValueChanged = False
                End If

            End If
        End If
    End Sub
End Class