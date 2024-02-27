'' Work to be done agaist ticket no. ERO/04/07/18-000366 by Parteek
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmHourlyAttendance
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsHourlyAttendance

    Const colempCode As String = "empCode"
    Const colempName As String = "EmpName"
    Const colAttendanceDate As String = "AttendanceDate"
    Const colInTime As String = "InTime"
    Const colOutTime As String = "OutTime"
    Const colFirstHalf As String = "FirstHalf"
    Const colSecondHalf As String = "SecondHalf"

    Dim isInsideLoadData As Boolean = False
    Private ObjList As New List(Of clsHourlyAttendance)
    Private isCellValueChangedOpen As Boolean = False
    Dim dtShift As DataTable

    Sub LoadGridColumns()

        Dim empCode As New GridViewTextBoxColumn
        Dim empName As New GridViewTextBoxColumn
        Dim attendanceDate As New GridViewDateTimeColumn

        Dim inTime As New GridViewDateTimeColumn
        Dim outTime As New GridViewDateTimeColumn
        Dim firstHalf As New GridViewComboBoxColumn
        Dim secondHalf As New GridViewComboBoxColumn

        Dim lstAtt As ArrayList = clsDailyAttendance.GetAttendanceStatus()
        'lstAtt.Add("A")
        'lstAtt.Add("P")
        'lstAtt.Add("H")
        'lstAtt.Add("WO")
        'lstAtt.Add("CL")
        'lstAtt.Add("PL")
        'lstAtt.Add("OD")
        'lstAtt.Add("T")
        'lstAtt.Add("COFF")
        'lstAtt.Add("NJ") '' NOT JOINED
        'lstAtt.Add("SEP") '' SEPARATED OR LEFT


        firstHalf.DataSource = lstAtt
        secondHalf.DataSource = lstAtt

        gvHourlyAttendance.Rows.Clear()
        gvHourlyAttendance.Columns.Clear()
        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = colempCode
        empCode.Width = 100
        'empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = colempName
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(empName)

        attendanceDate.FormatString = DateTimePickerFormat.Custom
        attendanceDate.CustomFormat = "dd-MM-yyyy"
        attendanceDate.FormatString = "{0:d}"
        attendanceDate.HeaderText = "Attendance Date"
        attendanceDate.Name = colAttendanceDate
        attendanceDate.Width = 100
        attendanceDate.ReadOnly = True
        attendanceDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(attendanceDate)

        'inTime.FormatString = "hh mi ss"
        inTime.Format = DateTimePickerFormat.Custom
        inTime.CustomFormat = "hh:mi:tt"
        inTime.FormatString = "{0:hh:mm tt}"
        inTime.HeaderText = "In Time"
        inTime.Name = colInTime
        inTime.EditorType = GridViewDateTimeEditorType.TimePicker
        inTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(inTime)

        'outTime.FormatString = "hh mi ss"
        outTime.Format = DateTimePickerFormat.Custom
        outTime.CustomFormat = "hh:mi:tt"
        outTime.FormatString = "{0:hh:mm tt}"
        outTime.HeaderText = "Out Time"
        outTime.Name = colOutTime
        outTime.EditorType = GridViewDateTimeEditorType.TimePicker
        outTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(outTime)

        firstHalf.FormatString = ""
        firstHalf.HeaderText = "First Half"
        firstHalf.Name = colFirstHalf
        firstHalf.Width = 100
        firstHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(firstHalf)

        secondHalf.FormatString = ""
        secondHalf.HeaderText = "Second Half"
        secondHalf.Name = colSecondHalf
        secondHalf.Width = 100
        secondHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvHourlyAttendance.Columns.Add(secondHalf)


    End Sub

    Private Sub frmHourlyAttendance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        End If
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub frmHourlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        Try
            dtShift = clsDBFuncationality.GetDataTable("select SHIFT_CODE,FROM_Time,TO_Time,INTERVAL_Time,FSTHALF_ADJUST_MIN,SECHALF_ADJUST_MIN from TSPL_SHIFT_MASTER")
        Catch ex As Exception

        End Try
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        btnPost.Enabled = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHourlyAttendance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenu2.Visible = MyBase.isExport
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_HOURLY_ATTENDANCE where DLA_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select DLA_CODE as Code, PAY_PERIOD_CODE, ENTEREDBY_EMP_CODE AS 'Entered By',DESCRIPTION from TSPL_HOURLY_ATTENDANCE "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_HOURLY_ATTENDANCE", qry, "Code", "", txtCode.Value, "DLA_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        findEnteredBy.Value = Nothing
        findPayperiod.Value = Nothing
        txtDescription.Text = ""
        btnsave.Text = "Save"
        txtPayPeriodDays.Text = ""
        txtPayPeriodName.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = False
        dtpAttendanceDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        Me.gvHourlyAttendance.Rows.Clear()
        Me.gvHourlyAttendance.Rows.AddNew()

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        'btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsHourlyAttendance.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DLA_CODE) > 0) Then
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
            txtCode.Value = obj.DLA_CODE
            findPayperiod.Value = obj.PAY_PERIOD_CODE
            findEnteredBy.Value = obj.ENTEREDBY_EMP_CODE
            txtDescription.Text = obj.DESCRIPTION
            txtPayPeriodName.Text = obj.PAY_PERIOD_NAME
            txtPayPeriodDays.Text = obj.PP_TOTAL_DAYS
            txtCode.MyReadOnly = True
            If (clsHourlyAttendance.ObjList IsNot Nothing AndAlso clsHourlyAttendance.ObjList.Count > 0) Then
                For Each obj As clsHourlyAttendance In clsHourlyAttendance.ObjList
                    gvHourlyAttendance.Rows.AddNew()
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colempCode).Value = obj.empCode
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colempName).Value = obj.empName
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colAttendanceDate).Value = obj.attendanceDate

                    If clsCommon.myLen(obj.IN_TIME) > 0 Then
                        gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colInTime).Value = obj.IN_TIME
                    End If
                    If clsCommon.myLen(obj.OUT_TIME) > 0 Then
                        gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colOutTime).Value = obj.OUT_TIME
                    End If
                

                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colFirstHalf).Value = obj.firstHalf
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colSecondHalf).Value = obj.secondHalf
                Next
            Else
                gvHourlyAttendance.Rows.AddNew()
            End If
            gvHourlyAttendance.BestFitColumns()
        End If
    End Sub

    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Total_Days, " _
        & " PAY_PERIOD_NAME as Pay_period_Name FROM TSPL_PAYPERIOD_MASTER "
        findPayperiod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", findPayperiod.Value, "", isButtonClicked)
        Dim clspp As clsPayPeriodMaster
        clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
        If Not clspp Is Nothing Then
            txtPayPeriodName.Text = clspp.Name
            Me.txtPayPeriodDays.Text = (DateDiff(DateInterval.Day, clspp.DATE_FROM, clspp.DATE_TO) + 1)
        End If

    End Sub

    Private Sub findEnteredBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findEnteredBy._MYValidating
        Dim qry As String = "SELECT EMP_CODE AS 'Code',EMP_Name as 'Employee Name' FROM TSPL_EMPLOYEE_MASTER "
        findEnteredBy.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findEnteredBy.Value, "", isButtonClicked)

    End Sub

    Private Sub gvHourlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvHourlyAttendance.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvHourlyAttendance.Columns(colempCode) Then
                Dim strq As String
                'strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
                '& " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_DAILY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
                '& " left join TSPL_HOURLY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"
                'strq = "SELECT TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
                '& " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
                '& " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
                '& " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
                '& " WHERE WORKING_STATUS='Working' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
                '& " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
                '& " LEFT JOIN TSPL_HOURLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
                '& " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE "

                strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
       & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
       & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
       & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
       & " WHERE WORKING_STATUS='Working' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
       & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
       & " LEFT JOIN TSPL_HOURLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
       & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE "

                Dim cond As String
                cond = " (TT2.ATTN_REGISTER_TYPE='HOURLY' OR TT2.ATTN_REGISTER_TYPE='HR') " _
       & " AND TT1.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_HOURLY_ATTENDANCE_DETAIL WHERE ATTENDANCE_DATE='" & Format(Me.dtpAttendanceDate.Value, "dd MMM yyyy") & "')"

                Dim obj As clsEmployeeMaster = clsMonthAttendance.FinderForEmployee(clsCommon.myCstr(gvHourlyAttendance.CurrentRow.Cells(colempCode).Value), False, strq, cond)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                    gvHourlyAttendance.CurrentRow.Cells(colempCode).Value = obj.EMP_CODE
                    gvHourlyAttendance.CurrentRow.Cells(colempName).Value = obj.Emp_Name
                    gvHourlyAttendance.CurrentRow.Cells(colAttendanceDate).Value = Me.dtpAttendanceDate.Value
                End If
            End If
            If e.Column Is gvHourlyAttendance.Columns(colInTime) Then
                If clsCommon.myLen(gvHourlyAttendance.CurrentRow.Cells(colInTime).Value) > 0 Then
                    gvHourlyAttendance.CurrentRow.Cells(colFirstHalf).Value = "P"
                Else
                    gvHourlyAttendance.CurrentRow.Cells(colFirstHalf).Value = "L"
                End If
            End If
            If e.Column Is gvHourlyAttendance.Columns(colOutTime) Then
                If clsCommon.myLen(gvHourlyAttendance.CurrentRow.Cells(colOutTime).Value) > 0 Then
                    gvHourlyAttendance.CurrentRow.Cells(colSecondHalf).Value = "P"
                Else
                    gvHourlyAttendance.CurrentRow.Cells(colSecondHalf).Value = "L"
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub
    Function ReturnShiftAtt(ByVal row As GridViewRowInfo) As String
        If clsCommon.myLen(row.Cells(colInTime).Value) > 0 Then
            row.Cells(colFirstHalf).Value = "P"
        Else
            row.Cells(colFirstHalf).Value = "L"
        End If
        If clsCommon.myLen(row.Cells(colOutTime).Value) > 0 Then
            row.Cells(colOutTime).Value = "P"
        Else
            row.Cells(colOutTime).Value = "L"
        End If
        Return Nothing
    End Function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As clsHourlyAttendance = Nothing
            ObjList = New List(Of clsHourlyAttendance)
            For Each grow As GridViewRowInfo In gvHourlyAttendance.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                    obj = New clsHourlyAttendance()
                    obj.DLA_CODE = txtCode.Value
                    obj.PAY_PERIOD_CODE = findPayperiod.Value
                    obj.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                    obj.attendanceDate = clsCommon.myCstr(grow.Cells(colAttendanceDate).Value)
                    obj.IN_TIME = grow.Cells(colInTime).Value
                    obj.OUT_TIME = grow.Cells(colOutTime).Value
                    obj.firstHalf = grow.Cells(colFirstHalf).Value
                    obj.secondHalf = grow.Cells(colSecondHalf).Value
                    obj.ENTEREDBY_EMP_CODE = findEnteredBy.Value
                    obj.DESCRIPTION = Me.txtDescription.Text
                    ObjList.Add(obj)
                End If
            Next
            If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                LoadData(obj.DLA_CODE, NavigatorType.Current)
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Return True
            End If
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_HOURLY_ATTENDANCE where DLA_CODE = '" + txtCode.Value + "' "
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
            myMessages.blankValue("Pay Period Code")
            findPayperiod.Focus()
            Return False
        End If


        Dim ii As Int16 = 0
        Dim status As Boolean = True
        Dim lstAttd As ArrayList = clsDailyAttendance.GetAttendanceStatus
        For Each grow As GridViewRowInfo In gvHourlyAttendance.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFirstHalf).Value)) = 0 Then
                '    grow.ErrorText = "?"
                '    'grow.Cells(colFirstHalf).ErrorText = "?"
                '    status = False
                'End If
                'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSecondHalf).Value)) = 0 Then
                '    grow.ErrorText = "?"
                '    status = False
                'End If
                '' check attendance code of the employee
                If clsDailyAttendance.CheckEmployeeAttendanceType(clsCommon.myCstr(grow.Cells(colempCode).Value), findPayperiod.Value, "HR") = False Then
                    clsCommon.MyMessageBoxShow("Employee Code " & clsCommon.myCstr(grow.Cells(colempCode).Value) & " at row number : " & (grow.Index + 1) & " does not belong to Hourly Attendance. Update Attendance type in Employee Status.")
                    Return False
                End If
                If Not (lstAttd.Contains(clsCommon.myCstr(grow.Cells(colFirstHalf).Value)) = True) Then
                    clsCommon.MyMessageBoxShow("Invalid attendance status " & clsCommon.myCstr(grow.Cells(colFirstHalf).Value) & " at row number :" & (grow.Index + 1) & " at column First Half.")
                    Return False
                End If
                If Not (lstAttd.Contains(clsCommon.myCstr(grow.Cells(colSecondHalf).Value)) = True) Then
                    clsCommon.MyMessageBoxShow("Invalid attendance status " & clsCommon.myCstr(grow.Cells(colSecondHalf).Value) & " at row number :" & (grow.Index + 1) & " at column Second Half.")
                    Return False
                End If
                ii += 1

                ObjList.Add(obj)
            End If

        Next
        If ObjList Is Nothing Or status = False Then
            'Return False
        End If
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
                If (clsHourlyAttendance.DeleteData(txtCode.Value)) Then
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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            funReset()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpAttendanceDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtpAttendanceDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpAttendanceDate.LostFocus
        If isNewEntry = False Then
            For Intloop As Integer = 0 To gvHourlyAttendance.Rows.Count - 1
                gvHourlyAttendance.Rows(Intloop).Cells(colAttendanceDate).Value = Me.dtpAttendanceDate.Value
            Next

            Exit Sub
        End If
        Dim strq As String
        strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
        & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
        & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
        & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
        & " WHERE WORKING_STATUS='Working' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
        & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
        & " LEFT JOIN TSPL_HOURLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
        & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE " _
        & " WHERE (TT2.ATTN_REGISTER_TYPE='HOURLY' OR TT2.ATTN_REGISTER_TYPE='HR') " _
        & " AND TT1.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_HOURLY_ATTENDANCE_DETAIL WHERE ATTENDANCE_DATE='" & Format(Me.dtpAttendanceDate.Value, "dd MMM yyyy") & "')"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        gvHourlyAttendance.Rows.Clear()
        For Intloop As Integer = 0 To dt.Rows.Count - 1
            gvHourlyAttendance.Rows.Add(1)

            gvHourlyAttendance.Rows(Intloop).Cells(colempCode).Value = dt.Rows(Intloop).Item("Code")
            gvHourlyAttendance.Rows(Intloop).Cells(colempName).Value = dt.Rows(Intloop).Item("Name")
            gvHourlyAttendance.Rows(Intloop).Cells(colAttendanceDate).Value = Me.dtpAttendanceDate.Value

        Next

    End Sub

    Private Sub dtpAttendanceDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpAttendanceDate.Validating
        'Dim strq As String

        'strq = "SELECT TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
        '& " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
        '& " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
        '& " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
        '& " WHERE WORKING_STATUS='Working' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
        '& " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
        '& " LEFT JOIN TSPL_HOURLY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
        '& " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE " _
        '& " WHERE (TT2.ATTN_REGISTER_TYPE='HOURLY' OR TT2.ATTN_REGISTER_TYPE='HR') AND TT3.ATTENDANCE_DATE!='" & Format(Me.dtpAttendanceDate.Value, "dd MMM yyyy") & "'"

        'Dim dt As DataTable
        'dt = clsDBFuncationality.GetDataTable(strq)
        'For Intloop As Integer = 0 To dt.Rows.Count - 1
        '    gvHourlyAttendance.Rows.Add(1)

        '    gvHourlyAttendance.Rows(Intloop).Cells(colempCode).Value = dt.Rows(Intloop).Item("Code")
        '    gvHourlyAttendance.Rows(Intloop).Cells(colempName).Value = dt.Rows(Intloop).Item("Name")
        '    gvHourlyAttendance.Rows(Intloop).Cells(colAttendanceDate).Value.Value = Me.dtpAttendanceDate.Value

        'Next

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsHourlyAttendance.PostData(txtCode.Value, True)) Then
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

    Private Sub gvHourlyAttendance_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvHourlyAttendance.CurrentColumnChanged
        If gvHourlyAttendance.RowCount > 0 Then
            Dim intCurrRow As Integer = gvHourlyAttendance.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvHourlyAttendance.Rows.Count - 1 Then
                If (clsCommon.myLen(txtCode.Value) > 0) Then
                    gvHourlyAttendance.Rows.AddNew()
                    'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                    gvHourlyAttendance.CurrentRow = gvHourlyAttendance.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select '' as [EmployeeCode],'' as [Employee Name],'' as [Pay Period],'' as [1 st],'' as [2 nd],'' as [3 rd],'' as [4 th]," & _
        " '' as [5 th],'' as [6 th],'' as [7 th], '' as [8 th],'' as [9 th],'' as [10 th],'' as [11 th],'' as [12 th],'' as [13 th],'' as [14 th], " & _
        " '' as [15 th],'' as [16 th],'' as [17 th],'' as [18 th]," & _
        " '' as [19 th],'' as [20 th],'' as [21 st],'' as [22 nd],'' as [23 rd],'' as [24 th],'' as [25 th],'' as [26 th],'' as [27 th]," & _
        " '' as [28 th],'' as [29 th],'' as [30 th],'' as [31 st]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub
    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            Dim query As String = "Select '' as [Log Date],'' as [Direction],'' as [Employee Code],'' as [Employee Name],'' as [Company],'' as [Department] "
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If clsCommon.myLen(findPayperiod.Value) <= 0 Then
                findPayperiod.Focus()
                Throw New Exception("Please select Pay Period")
            End If
            If clsCommon.myLen(findEnteredBy.Value) <= 0 Then
                findEnteredBy.Focus()
                Throw New Exception("Please select Entered By")
            End If
            If Not isNewEntry Then
                Throw New Exception("Please first select New Button")
            End If
            LoadGridColumns()
            If transportSql.importExcel(dgv, "Log Date", "Direction", "Employee Code", "Employee Name", "Company", "Department") Then
                Dim LineNo As Integer = 0
                Dim InTimeLine As Integer = 0
                Dim OutTimeLine As Integer = 0
                Try
                    Dim arr As New List(Of clsHourlyAttendanceDetail)
                    Dim arrcollect As New List(Of String)
                    clsCommon.ProgressBarPercentShow()
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Validating  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        LineNo += 1
                        OutTimeLine += 1

                        Dim EmpCode As String = clsCommon.myCstr(dgrv.Cells("Employee Code").Value)

                        Dim obj As New clsHourlyAttendanceDetail
                        Dim qry1 As String = ""
                        Dim dt As DataTable = Nothing
                        Dim dt1 As DataTable = Nothing
                        obj.empCode = clsCommon.myCstr(dgrv.Cells("Employee Code").Value)
                        If clsCommon.myLen(obj.empCode) <= 0 Then
                            Continue For
                        End If
                        Dim qry As String = "select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_CODE='" + obj.empCode + "'"
                        obj.empName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(obj.empName) <= 0 Then
                            Throw New Exception("No a valid Employee code")
                        End If
                        obj.attendanceDate = clsCommon.myCstr(dgrv.Cells("Log Date").Value)
                        If clsCommon.myLen(obj.attendanceDate) <= 0 Then
                            Throw New Exception("No a valid Attendance Date Time")
                        End If

                        Dim QryPresentMonth As String = " SELECT Month(convert(varchar,'" + clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy") + "',103)) AS [MonthDay] "
                        dt1 = clsDBFuncationality.GetDataTable(QryPresentMonth)
                        QryPresentMonth = dt1.Rows(0)("MonthDay")

                        Dim PayPeriodMonth As String = (clsDBFuncationality.getSingleValue(" SELECT Month(DATE_FROM) AS [Month] from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" + findPayperiod.Value + "'"))

                        If clsCommon.CompairString(QryPresentMonth, PayPeriodMonth) = CompairStringResult.Equal Then



                            qry1 = "select CONVERT(varchar(15),CAST('" + clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy hh:mm tt") + "' AS TIME),100) as InTime,right(convert(varchar(20),'" + clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy hh:mm tt") + "',100),2) as AMPM "
                            dt = clsDBFuncationality.GetDataTable(qry1)
                            ' If clsCommon.CompairString(dt.Rows(0)("AMPM"), "AM") = CompairStringResult.Equal Then
                            obj.InTime = clsCommon.myCDate(dt.Rows(0)("InTime"))
                            If clsCommon.myLen(obj.InTime) <= 0 Then
                                Throw New Exception("No a valid In Time")
                            End If
                            '  End If



                            '' Out Time calculate

                            If clsCommon.CompairString(obj.empCode, EmpCode) = CompairStringResult.Equal Then
                                qry1 = "select CONVERT(varchar(15),CAST('" + clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy hh:mm tt") + "' AS TIME),100) as OutTime,right(convert(varchar(20),'" + clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy hh:mm tt") + "',100),2) as AMPM "
                                dt = clsDBFuncationality.GetDataTable(qry1)

                                obj.OutTime = clsCommon.myCDate(dt.Rows(0)("OutTime"))
                                If clsCommon.myLen(obj.OutTime) <= 0 Then
                                    Throw New Exception("No a valid In Time")
                                End If
                                'End If

                            End If

                            arr.Add(obj)

                            '  End If
                        Else
                            Throw New Exception("Pay Period Month not equal to Import Excel Sheet Month.")
                        End If



                    Next
                    Try
                        isInsideLoadData = True
                        LoadDetailData(arr, True)
                        UpdateAllRows()
                        LoadData12()
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                        isInsideLoadData = True
                    End Try

                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no" + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub
    Sub LoadDetailData(ByVal Arr As List(Of clsHourlyAttendanceDetail), ByVal isAddMasterCode As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsHourlyAttendanceDetail In Arr
                Dim empcode As String = ""
                Dim Counted As Integer = 0
                gvHourlyAttendance.Rows.AddNew()

                gvHourlyAttendance.CurrentRow = gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1)
                Counted = gvHourlyAttendance.Rows.Count - 1
                If Counted > 0 Then
                    empcode = gvHourlyAttendance.Rows(gvHourlyAttendance.CurrentRow.Index - 1).Cells(colempCode).Value
                End If


                If clsCommon.CompairString(objtr.empCode, empcode) <> CompairStringResult.Equal Then
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colempCode).Value = objtr.empCode
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colempName).Value = objtr.empName
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colAttendanceDate).Value = clsCommon.myCDate(objtr.attendanceDate)
                    gvHourlyAttendance.Rows(gvHourlyAttendance.Rows.Count - 1).Cells(colInTime).Value = objtr.InTime
                Else
                    gvHourlyAttendance.Rows(gvHourlyAttendance.CurrentRow.Index - 1).Cells(colOutTime).Value = objtr.OutTime
                End If

            Next
            gvHourlyAttendance.BestFitColumns()
        End If
    End Sub
    Sub LoadData12()
        For Each grow As GridViewRowInfo In gvHourlyAttendance.Rows
            If clsCommon.myLen(grow.Cells(colempCode).Value) > 0 Then
                If clsCommon.myLen(grow.Cells(colInTime).Value) <= 0 Then
                    grow.Cells(colFirstHalf).Value = "A"
                Else
                    grow.Cells(colFirstHalf).Value = "P"
                End If

                If clsCommon.myLen(grow.Cells(colOutTime).Value) <= 0 Then
                    grow.Cells(colSecondHalf).Value = "A"
                Else
                    grow.Cells(colSecondHalf).Value = "P"
                End If
            End If

        Next
        For jj As Integer = gvHourlyAttendance.Rows.Count - 1 To 0 Step -1
            If clsCommon.myLen(gvHourlyAttendance.Rows(jj).Cells(colempCode).Value) <= 0 Then
                gvHourlyAttendance.Rows.RemoveAt(jj)
            End If
        Next
    End Sub
  
    Sub UpdateAllRows()
        Try
            If gvHourlyAttendance.RowCount > 0 AndAlso gvHourlyAttendance.ColumnCount > 0 Then
                Dim oldCurrentRow As Integer = IIf(gvHourlyAttendance.CurrentRow.Index < 0, 0, gvHourlyAttendance.CurrentRow.Index)
                Dim oldCurrentColumne As Integer = IIf(gvHourlyAttendance.CurrentColumn.Index < 0, 0, gvHourlyAttendance.CurrentColumn.Index)
                For ii As Integer = 0 To gvHourlyAttendance.RowCount - 1
                    If clsCommon.myLen(gvHourlyAttendance.Rows(ii).Cells(colempCode).Value) > 0 Then
                        gvHourlyAttendance.CurrentRow = gvHourlyAttendance.Rows(ii)
                        'UpdateCurrentRow()
                    End If
                Next
                gvHourlyAttendance.CurrentRow = gvHourlyAttendance.Rows(oldCurrentRow)
                gvHourlyAttendance.CurrentColumn = gvHourlyAttendance.Columns(oldCurrentColumne)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class