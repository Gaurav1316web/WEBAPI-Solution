Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmDailyAttendance
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsDailyAttendance

    Const colempCode As String = "empCode"
    Const colempName As String = "EmpName"
    Const colAttendanceDate As String = "AttendanceDate"
    Const colFirstHalf As String = "FirstHalf"
    Const colSecondHalf As String = "SecondHalf"
    Private ObjList As New List(Of clsDailyAttendanceDetail)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()

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


        Dim empCode As New GridViewTextBoxColumn
        gvDailyAttendance.Rows.Clear()
        gvDailyAttendance.Columns.Clear()
        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = "empCode"
        empCode.Width = 100
        'empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDailyAttendance.Columns.Add(empCode)

        Dim empName As New GridViewTextBoxColumn
        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = "EmpName"
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDailyAttendance.Columns.Add(empName)

        Dim attendanceDate As New GridViewDateTimeColumn
        attendanceDate.FormatString = "dd/MM/yyyy"
        attendanceDate.FormatString = "{0:d}"
        attendanceDate.HeaderText = "Attendance Date"
        attendanceDate.Name = "AttendanceDate"
        attendanceDate.Width = 100
        attendanceDate.ReadOnly = False
        attendanceDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDailyAttendance.Columns.Add(attendanceDate)

        Dim firstHalf As New GridViewComboBoxColumn
        firstHalf.DataSource = lstAtt
        firstHalf.FormatString = ""
        firstHalf.HeaderText = "First Half"
        firstHalf.Name = "FirstHalf"
        firstHalf.Width = 100
        firstHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDailyAttendance.Columns.Add(firstHalf)

        Dim secondHalf As New GridViewComboBoxColumn
        secondHalf.DataSource = lstAtt
        secondHalf.FormatString = ""
        secondHalf.HeaderText = "Second Half"
        secondHalf.Name = "SecondHalf"
        secondHalf.Width = 100
        secondHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDailyAttendance.Columns.Add(secondHalf)

    End Sub

    Private Sub frmDailyAttendance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub frmDailyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        funReset()
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        btnPost.Enabled = False

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDailyAttendance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

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
        Dim str As String = "select count(*) from TSPL_DAILY_ATTENDANCE where DLA_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select DLA_CODE as Code, PAY_PERIOD_CODE, ENTEREDBY_EMP_CODE AS 'Entered By',DESCRIPTION from TSPL_DAILY_ATTENDANCE "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_DAILY_ATTENDANCE", qry, "Code", "", txtCode.Value, "DLA_CODE", isButtonClicked)
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
        findLocation.Value = Nothing
        findPayperiod.Value = Nothing
        txtDescription.Text = ""
        txtPayPeriodDays.Text = ""
        txtPayPeriodName.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = False
        Me.gvDailyAttendance.Rows.Clear()
        Me.gvDailyAttendance.Rows.AddNew()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        'btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsDailyAttendance.GetData(strCode, NavTyep)
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
            findLocation.Value = obj.Location_Code
            txtDescription.Text = obj.DESCRIPTION
            txtPayPeriodName.Text = obj.PAY_PERIOD_NAME
            txtPayPeriodDays.Text = obj.PP_TOTAL_DAYS
            dtpAttendanceDate.Value = obj.attendanceDate
            txtCode.MyReadOnly = True
            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objTr As clsDailyAttendanceDetail In obj.Arr
                    gvDailyAttendance.Rows.AddNew()
                    gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempCode).Value = objTr.empCode
                    gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempName).Value = objTr.empName
                    gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colAttendanceDate).Value = objTr.attendanceDate
                    gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colFirstHalf).Value = objTr.firstHalf
                    gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colSecondHalf).Value = objTr.secondHalf
                Next
            Else
                gvDailyAttendance.Rows.AddNew()
            End If
        End If
    End Sub

    Private Sub findPayperiod__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
        & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        findPayperiod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", findPayperiod.Value, "", isButtonClicked)
        Dim clspp As clsPayPeriodMaster
        clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)

        If Not clspp Is Nothing Then
            txtPayPeriodName.Text = clspp.Name
            Me.txtPayPeriodDays.Text = (DateDiff(DateInterval.Day, clspp.DATE_FROM, clspp.DATE_TO) + 1)
            dtpAttendanceDate.Value = clspp.DATE_TO
        End If

        'txtPayPeriodName.Text = clsPayPeriodMaster.GetData(findPayperiod.Value, Nothing)
    End Sub

    Private Sub findEnteredBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles findLocation._MYValidating
        'Dim qry As String = "SELECT EMP_CODE AS 'Code',EMP_Name as 'Employee Name' FROM TSPL_EMPLOYEE_MASTER "
        findLocation.Value = clsLocation.getFinder("", findLocation.Value, isButtonClicked) 'clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", findLocation.Value, "", isButtonClicked)
    End Sub

    Private Sub gvDailyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDailyAttendance.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvDailyAttendance.Columns(colempCode) Then
                Dim strq As String
                'strq = "select EMP.EMP_CODE as Code,EMP.Emp_Name as Name,EMP.Designation  from TSPL_EMPLOYEE_MASTER EMP left join " _
                '& " TSPL_MONTHLY_ATTENDANCE_DETAIL MA ON EMP.EMP_CODE=MA.EMP_CODE left join TSPL_DAILY_ATTENDANCE_DETAIL DA ON EMP.EMP_CODE=DA.EMP_CODE " _
                '& " left join TSPL_DAILY_ATTENDANCE_DETAIL HA ON EMP.EMP_CODE=HA.EMP_CODE"
                strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
     & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
     & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
     & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
     & " WHERE WORKING_STATUS='Working' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
     & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
     & " LEFT JOIN TSPL_DAILY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
     & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE "

                Dim cond As String
                cond = " (TT2.ATTN_REGISTER_TYPE='DAILY' OR TT2.ATTN_REGISTER_TYPE='DL') and TT4.Location_Code='" & findLocation.Value & "'" _
       & " AND TT1.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_DAILY_ATTENDANCE_DETAIL WHERE ATTENDANCE_DATE='" & Format(Me.dtpAttendanceDate.Value, "dd MMM yyyy") & "')"


                Dim obj As clsEmployeeMaster = clsMonthAttendance.FinderForEmployee(clsCommon.myCstr(gvDailyAttendance.CurrentRow.Cells(colempCode).Value), False, strq, cond)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                    gvDailyAttendance.CurrentRow.Cells(colempCode).Value = obj.EMP_CODE
                    gvDailyAttendance.CurrentRow.Cells(colempName).Value = obj.Emp_Name
                    gvDailyAttendance.CurrentRow.Cells(colAttendanceDate).Value = Me.dtpAttendanceDate.Value

                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        Try
            clsCommon.ProgressBarShow()
            If AllowToSave() Then
                obj = New clsDailyAttendance
                obj.DLA_CODE = txtCode.Value
                obj.DESCRIPTION = Me.txtDescription.Text
                obj.Location_Code = findLocation.Value
                obj.DLA_CODE = txtCode.Value
                obj.PAY_PERIOD_CODE = findPayperiod.Value
                obj.attendanceDate = dtpAttendanceDate.Value
                Dim objTr As clsDailyAttendanceDetail
                ObjList = New List(Of clsDailyAttendanceDetail)
                For Each grow As GridViewRowInfo In gvDailyAttendance.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                        objTr = New clsDailyAttendanceDetail()
                        objTr.DLA_CODE = txtCode.Value
                        objTr.PAY_PERIOD_CODE = findPayperiod.Value
                        objTr.empCode = clsCommon.myCstr(grow.Cells(colempCode).Value)
                        objTr.attendanceDate = clsCommon.myCstr(grow.Cells(colAttendanceDate).Value)
                        objTr.firstHalf = clsCommon.myCstr(grow.Cells(colFirstHalf).Value)
                        objTr.secondHalf = clsCommon.myCstr(grow.Cells(colSecondHalf).Value)
                        ObjList.Add(objTr)
                    End If
                Next
                obj.Arr = ObjList
                If clsDailyAttendance.SaveData(obj, isNewEntry) Then
                    LoadData(obj.DLA_CODE, NavigatorType.Current)
                End If
            Else
                clsCommon.ProgressBarHide()
                Return False
            End If
            clsCommon.ProgressBarHide()
            Return True
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_DAILY_ATTENDANCE where DLA_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                Throw New Exception("Transection already posted")
            End If
        End If

        If clsCommon.myLen(findPayperiod.Value) <= 0 Then
            findPayperiod.Focus()
            Throw New Exception("Pay Period Code")
        End If
        Dim ii As Int16 = 0
        Dim lstAttd As ArrayList = clsDailyAttendance.GetAttendanceStatus
        For Each grow As GridViewRowInfo In gvDailyAttendance.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colempCode).Value)) > 0 Then
                '' check attendance code of the employee
                If clsDailyAttendance.CheckEmployeeAttendanceType(clsCommon.myCstr(grow.Cells(colempCode).Value), findPayperiod.Value, "DL") = False Then
                    Throw New Exception("Employee Code " & clsCommon.myCstr(grow.Cells(colempCode).Value) & " at row number : " & (grow.Index + 1) & " does not belong to Daily Attendance. Update Attendance type in Employee Status.")
                End If
                If Not (lstAttd.Contains(clsCommon.myCstr(grow.Cells(colFirstHalf).Value)) = True) Then
                    Throw New Exception("Invalid attendance status " & clsCommon.myCstr(grow.Cells(colFirstHalf).Value) & " at row number :" & (grow.Index + 1) & " at column First Half.")
                End If
                If Not (lstAttd.Contains(clsCommon.myCstr(grow.Cells(colSecondHalf).Value)) = True) Then
                    Throw New Exception("Invalid attendance status " & clsCommon.myCstr(grow.Cells(colSecondHalf).Value) & " at row number :" & (grow.Index + 1) & " at column Second Half.")
                End If
                ii += 1
                'ObjList.Add(obj)
            End If
        Next
        If ii = 0 Then
            Return False
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
                If (clsDailyAttendance.DeleteData(txtCode.Value)) Then
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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsDailyAttendance.PostData(txtCode.Value, True)) Then
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

    Private Sub dtpAttendanceDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpAttendanceDate.LostFocus
        If isNewEntry = False Then
            'For Intloop As Integer = 0 To gvDailyAttendance.Rows.Count - 1
            '    gvDailyAttendance.Rows(Intloop).Cells(colAttendanceDate).Value = Me.dtpAttendanceDate.Value
            'Next

            Exit Sub
        End If
        Dim strq As String
        strq = "SELECT distinct  TT1.EMP_CODE as Code,TT4.Emp_Name as Name,TT4.Designation FROM (" _
        & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS " _
        & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
        & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS " _
        & " WHERE WORKING_STATUS='Working' GROUP BY EMP_CODE) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
        & " LEFT JOIN TSPL_ATTENDANCE_MASTER TT2 ON TT1.ATTENDANCE_CODE=TT2.ATTENDANCE_CODE " _
        & " LEFT JOIN TSPL_DAILY_ATTENDANCE_DETAIL TT3 ON TT1.EMP_CODE=TT3.EMP_CODE " _
        & " LEFT JOIN TSPL_EMPLOYEE_MASTER TT4 ON TT1.EMP_CODE=TT4.EMP_CODE " _
        & " WHERE (TT2.ATTN_REGISTER_TYPE='DAILY' OR TT2.ATTN_REGISTER_TYPE='DL') and TT4.Location_Code='" & findLocation.Value & "'" _
        & " AND TT1.EMP_CODE NOT IN (SELECT DISTINCT EMP_CODE FROM TSPL_DAILY_ATTENDANCE_DETAIL WHERE ATTENDANCE_DATE='" & Format(Me.dtpAttendanceDate.Value, "dd MMM yyyy") & "')"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        gvDailyAttendance.Rows.Clear()
        For Intloop As Integer = 0 To dt.Rows.Count - 1
            gvDailyAttendance.Rows.Add(1)

            gvDailyAttendance.Rows(Intloop).Cells(colempCode).Value = dt.Rows(Intloop).Item("Code")
            gvDailyAttendance.Rows(Intloop).Cells(colempName).Value = dt.Rows(Intloop).Item("Name")
            gvDailyAttendance.Rows(Intloop).Cells(colAttendanceDate).Value = Me.dtpAttendanceDate.Value

        Next

    End Sub

    Private Sub MenuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub

    Private Sub gvDailyAttendance_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvDailyAttendance.CurrentColumnChanged
        If gvDailyAttendance.RowCount > 0 Then
            Dim intCurrRow As Integer = gvDailyAttendance.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvDailyAttendance.Rows.Count - 1 Then
                'If (clsCommon.myLen(txtCode.Value) > 0) Then
                gvDailyAttendance.Rows.AddNew()
                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gvDailyAttendance.CurrentRow = gvDailyAttendance.Rows(intCurrRow)
                'End If
            End If
        End If
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles MIImportSingle.Click
        If clsCommon.myCstr(findPayperiod.Value) = "" Then
            clsCommon.MyMessageBoxShow(Me, "Select Pay Period to import the attendance!", Me.Text)
            Exit Sub
        End If
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today


            If transportSql.importExcel(gv, "EmployeeCode", "Employee Name", "Pay Period", "1 st", "2 nd", "3 rd", "4 th", "5 th", "6 th", "7 th", "8 th", "9 th", "10 th", "11 th", "12 th", "13 th", "14 th", "15 th", "16 th", "17 th", "18 th", "19 th", "20 th", "21 st", "22 nd", "23 rd", "24 th", "25 th", "26 th", "27 th", "28 th", "29 th", "30 th", "31 st") Then
                gvDailyAttendance.DataSource = Nothing
                gvDailyAttendance.Rows.Clear()
                gvDailyAttendance.Columns.Clear()
                LoadGridColumns()
                Dim dt_pp As DataTable
                dt_pp = clsDBFuncationality.GetDataTable("select * from tspl_payperiod_master where pay_period_code='" & Me.findPayperiod.Value & "'")
                If dt_pp.Rows.Count = 0 Then
                    Me.Controls.Remove(gv)
                    Exit Sub
                End If
                clsCommon.ProgressBarShow()
                Dim from_day As Integer
                Dim end_day As Integer
                from_day = clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Day
                end_day = clsCommon.myCDate(dt_pp.Rows(0).Item("date_to")).Day
                gvDailyAttendance.Columns(colempCode).ReadOnly = True

                For Each rw As GridViewRowInfo In gv.Rows
                    If IsDBNull(rw.Cells("Pay Period").Value) = True Then
                        Continue For
                    End If
                    If rw.Cells("Pay Period").Value <> clsCommon.myCstr(Me.findPayperiod.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Pay Period not matched for row no. " & rw.Index + 1 & " !", Me.Text)
                        Me.Controls.Remove(gv)
                        Exit Sub
                        'Continue For
                    End If

                    For Each gvcol As GridViewDataColumn In gv.Columns
                        If (gvcol.Index + 1) < (from_day + 3) Or (gvcol.Index + 1) > (end_day + 3) Then
                            Continue For
                        End If

                        Me.gvDailyAttendance.Rows.AddNew()
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempCode).Value = rw.Cells("EmployeeCode").Value
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempName).Value = rw.Cells("Employee Name").Value
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colAttendanceDate).Value = clsCommon.myCDate((gvcol.Index + 1 - 3) & " " & MonthName(clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Month) & " " & clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Year)
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colFirstHalf).Value = rw.Cells(gvcol.Index).Value
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colSecondHalf).Value = rw.Cells(gvcol.Index).Value

                    Next
                Next

                Me.Controls.Remove(gv)
                clsCommon.ProgressBarHide()
            End If

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub MIImportDual_Click(sender As Object, e As EventArgs) Handles MIImportDual.Click

        If clsCommon.myCstr(findPayperiod.Value) = "" Then
            clsCommon.MyMessageBoxShow(Me, "Select Pay Period to Import the attendance!", Me.Text)
            Exit Sub
        End If
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today

            If transportSql.importExcel(gv, "EmployeeCode", "Employee Name", "Pay Period", "1 st(FH)", "1 st(SH)", "2 nd(FH)", "2 nd(SH)", "3 rd(FH)", "3 rd(SH)", "4 th(FH)", "4 th(SH)", "5 th(FH)", "5 th(SH)", "6 th(FH)", "6 th(SH)", "7 th(FH)", "7 th(SH)", "8 th(FH)", "8 th(SH)", "9 th(FH)", "9 th(SH)", "10 th(FH)", "10 th(SH)", "11 th(FH)", "11 th(SH)", "12 th(FH)", "12 th(SH)", "13 th(FH)", "13 th(SH)", "14 th(FH)", "14 th(SH)", "15 th(FH)", "15 th(SH)", "16 th(FH)", "16 th(SH)", "17 th(FH)", "17 th(SH)", "18 th(FH)", "18 th(SH)", "19 th(FH)", "19 th(SH)", "20 th(FH)", "20 th(SH)", "21 st(FH)", "21 st(SH)", "22 nd(FH)", "22 nd(SH)", "23 rd(FH)", "23 rd(SH)", "24 th(FH)", "24 th(SH)", "25 th(FH)", "25 th(SH)", "26 th(FH)", "26 th(SH)", "27 th(FH)", "27 th(SH)", "28 th(FH)", "28 th(SH)", "29 th(FH)", "29 th(SH)", "30 th(FH)", "30 th(SH)", "31 st(FH)", "31 st(SH)") Then
                Dim dt_pp As DataTable
                dt_pp = clsDBFuncationality.GetDataTable("select * from tspl_payperiod_master where pay_period_code='" & Me.findPayperiod.Value & "'")
                If dt_pp.Rows.Count = 0 Then
                    Me.Controls.Remove(gv)
                    Exit Sub
                End If
                clsCommon.ProgressBarShow()
                Dim from_day As Integer
                Dim end_day As Integer
                from_day = clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Day
                end_day = clsCommon.myCDate(dt_pp.Rows(0).Item("date_to")).Day
                gvDailyAttendance.Columns(colempCode).ReadOnly = True
                For Each rw As GridViewRowInfo In gv.Rows
                    If rw.Cells("Pay Period").Value <> Me.findPayperiod.Value Then
                        clsCommon.MyMessageBoxShow(Me, "Pay Period not matched for row no. " & rw.Index + 1 & " !", Me.Text)
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If

                    'For Each gvcol As GridViewDataColumn In gv.Columns
                    '    If (gvcol.Index + 1) < (from_day + 3) Or (gvcol.Index + 1) > (end_day + 3) Then
                    '        Continue For
                    '    End If
                    '    If (gvcol.Index + 1 - 3) Mod 2 = 0 Then
                    '        Me.gvDailyAttendance.Rows.AddNew()
                    '        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempCode).Value = rw.Cells("EmployeeCode").Value
                    '        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempName).Value = rw.Cells("Employee Name").Value
                    '        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colAttendanceDate).Value = clsCommon.myCDate(CInt((gvcol.Index + 1 - 3)) / 2 & " " & MonthName(clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Month) & " " & clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Year)
                    '    End If
                    '    If (gvcol.Index + 1 - 3) Mod 2 = 0 Then
                    '        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colFirstHalf).Value = rw.Cells(gvcol.Index).Value
                    '    Else
                    '        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colSecondHalf).Value = rw.Cells(gvcol.Index).Value
                    '    End If
                    'Next
                    For Each gvcol As GridViewDataColumn In gv.Columns
                        If (gvcol.Index + 1) < (from_day + 3) Or (gvcol.Index + 1) > (end_day + 3) Then
                            Continue For
                        End If

                        Me.gvDailyAttendance.Rows.AddNew()
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempCode).Value = rw.Cells("EmployeeCode").Value
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colempName).Value = rw.Cells("Employee Name").Value
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colAttendanceDate).Value = clsCommon.myCDate((gvcol.Index + 1 - 3) & " " & MonthName(clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Month) & " " & clsCommon.myCDate(dt_pp.Rows(0).Item("date_from")).Year)
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colFirstHalf).Value = rw.Cells(gvcol.Index).Value
                        gvDailyAttendance.Rows(gvDailyAttendance.Rows.Count - 1).Cells(colSecondHalf).Value = rw.Cells(gvcol.Index).Value

                    Next
                Next
                Me.Controls.Remove(gv)
                clsCommon.ProgressBarHide()
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub MIExportSingle_Click(sender As Object, e As EventArgs) Handles MIExportSingle.Click
        '' export attendance(SINGLE)
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Document Code to export.", Me.Text)
            txtCode.Focus()
            Exit Sub
        End If

        Dim qry As String = " select [EmployeeCode],[Employee Name],[Pay Period],[1 st],[2 nd],[3 rd],[4 th],[5 th],[6 th],[7 th],[8 th],[9 th],[10 th],  " & _
                            " [11 th],[12 th],[13 th],[14 th],[15 th],[16 th],[17 th],[18 th],[19 th],[20 th],[21 st],[22 nd],[23 rd],[24 th],[25 th],[26 th], " & _
                            " [27 th],[28 th],[29 th],[30 th],[31 st] from (" & _
                            " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],da.PAY_PERIOD_CODE as [Pay Period]," & _
                            " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st' " & _
                            " when day(dad.Attendance_date) in (2,22) then ' nd' when day(dad.Attendance_date) in (3,23) then ' rd' " & _
                            " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th' end) as Attendance_Day, " & _
                            " DAD.FIRST_HALF from TSPL_DAILY_ATTENDANCE_DETAIL DAD inner join TSPL_DAILY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
                            " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
                            " where DA.DLA_CODE='" & txtCode.Value & "' " & _
                            " ) as Attd " & _
                            " pivot ( max(attd.First_Half) for Attd.Attendance_Day in  ([1 st],[2 nd],[3 rd],[4 th],[5 th],[6 th],[7 th],[8 th],[9 th],[10 th],  " & _
                            " [11 th],[12 th],[13 th],[14 th],[15 th],[16 th],[17 th],[18 th],[19 th],[20 th],[21 st],[22 nd],[23 rd],[24 th],[25 th],[26 th], " & _
                            " [27 th],[28 th],[29 th],[30 th],[31 st])) Pvt "

        transportSql.ExporttoExcelNew(qry, Me)
    End Sub

    Private Sub MiExportDual_Click(sender As Object, e As EventArgs) Handles MiExportDual.Click
        '' export attendance(DUAL)
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Document Code to export.", Me.Text)
            txtCode.Focus()
            Exit Sub
        End If

        Dim qry As String = " select FH.EmployeeCode,FH.[Employee Name],FH.[Pay Period], [1 st(FH)], [1 st(SH)], [2 nd(FH)], [2 nd(SH)], " & _
        " [3 rd(FH)], [3 rd(SH)], [4 th(FH)], [4 th(SH)],  [5 th(FH)], [5 th(SH)], [6 th(FH)], [6 th(SH)], [7 th(FH)], [7 th(SH)] , " & _
        " [8 th(FH)], [8 th(SH)], [9 th(FH)], [9 th(SH)], [10 th(FH)], [10 th(SH)], [11 th(FH)], [11 th(SH)], [12 th(FH)], [12 th(SH)], " & _
        " [13 th(FH)], [13 th(SH)], [14 th(FH)], [14 th(SH)],  [15 th(FH)], [15 th(SH)], [16 th(FH)], [16 th(SH)], [17 th(FH)], [17 th(SH)]," & _
        " [18 th(FH)], [18 th(SH)], [19 th(FH)], [19 th(SH)], [20 th(FH)], [20 th(SH)], [21 st(FH)], [21 st(SH)], [22 nd(FH)], [22 nd(SH)], " & _
        " [23 rd(FH)], [23 rd(SH)], [24 th(FH)], [24 th(SH)], [25 th(FH)], [25 th(SH)], [26 th(FH)], [26 th(SH)], [27 th(FH)], [27 th(SH)], " & _
        " [28 th(FH)], [28 th(SH)], [29 th(FH)], [29 th(SH)], [30 th(FH)], [30 th(SH)], [31 st(FH)], [31 st(SH)] from " & _
        " ( " & _
        " select * from ( " & _
        " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],da.PAY_PERIOD_CODE as [Pay Period], " & _
        " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st(FH)' when day(dad.Attendance_date) in (2,22) " & _
        " then ' nd(FH)' when day(dad.Attendance_date) in (3,23) then ' rd(FH)' " & _
        " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th(FH)' end) as Attendance_Day_FH, " & _
        " DAD.FIRST_HALF from TSPL_DAILY_ATTENDANCE_DETAIL DAD " & _
        " inner join TSPL_DAILY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
        " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
        " where DA.DLA_CODE='" & txtCode.Value & "' " & _
        " ) as Attd  " & _
        " pivot ( max(attd.First_Half) for Attd.Attendance_Day_FH in  ([1 st(FH)],[2 nd(FH)],[3 rd(FH)],[4 th(FH)],[5 th(FH)],[6 th(FH)],[7 th(FH)],[8 th(FH)],[9 th(FH)],[10 th(FH)], " & _
        " [11 th(FH)],[12 th(FH)],[13 th(FH)],[14 th(FH)], " & _
        " [15 th(FH)],[16 th(FH)],[17 th(FH)],[18 th(FH)],[19 th(FH)],[20 th(FH)],[21 st(FH)],[22 nd(FH)],[23 rd(FH)],[24 th(FH)],[25 th(FH)],[26 th(FH)],[27 th(FH)],[28 th(FH)],[29 th(FH)],[30 th(FH)],[31 st(FH)])) Pvt " & _
        " ) as FH " & _
        " left join  " & _
        " ( " & _
        " select * from ( " & _
        " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],da.PAY_PERIOD_CODE as [Pay Period], " & _
        " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st(SH)' when day(dad.Attendance_date) in (2,22) then ' nd(SH)' when day(dad.Attendance_date) in (3,23) then ' rd(SH)' " & _
        " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th(SH)' end) as Attendance_Day_SH, " & _
        " DAD.SECOND_HALF from TSPL_DAILY_ATTENDANCE_DETAIL DAD " & _
        " inner join TSPL_DAILY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
        " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
        " where DA.DLA_CODE='" & txtCode.Value & "'  " & _
        " ) as Attd " & _
        " pivot ( max(attd.SECOND_HALF) for Attd.Attendance_Day_SH in  ([1 st(SH)],[2 nd(SH)],[3 rd(SH)],[4 th(SH)],[5 th(SH)],[6 th(SH)],[7 th(SH)],[8 th(SH)],[9 th(SH)],[10 th(SH)], " & _
        " [11 th(SH)],[12 th(SH)],[13 th(SH)],[14 th(SH)], " & _
        " [15 th(SH)],[16 th(SH)],[17 th(SH)],[18 th(SH)],[19 th(SH)],[20 th(SH)],[21 st(SH)],[22 nd(SH)],[23 rd(SH)],[24 th(SH)],[25 th(SH)],[26 th(SH)],[27 th(SH)],[28 th(SH)],[29 th(SH)],[30 th(SH)],[31 st(SH)])) Pvt) as SH on FH.EmployeeCode=sh.EmployeeCode and fh.[Pay Period]=sh.[Pay Period]"

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub MIExportBlankSingle_Click(sender As Object, e As EventArgs) Handles MIExportBlankSingle.Click
        Dim qry As String = "select '' as [EmployeeCode],'' as [Employee Name],'' as [Pay Period],'' as [1 st],'' as [2 nd],'' as [3 rd],'' as [4 th]," & _
       " '' as [5 th],'' as [6 th],'' as [7 th], '' as [8 th],'' as [9 th],'' as [10 th],'' as [11 th],'' as [12 th],'' as [13 th],'' as [14 th], " & _
       " '' as [15 th],'' as [16 th],'' as [17 th],'' as [18 th]," & _
       " '' as [19 th],'' as [20 th],'' as [21 st],'' as [22 nd],'' as [23 rd],'' as [24 th],'' as [25 th],'' as [26 th],'' as [27 th]," & _
       " '' as [28 th],'' as [29 th],'' as [30 th],'' as [31 st]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub MIExportBlankDual_Click(sender As Object, e As EventArgs) Handles MIExportBlankDual.Click
        Dim qry As String = "select '' as [EmployeeCode],'' as [Employee Name],'' as [Pay Period],'' as [1 st(FH)],'' as [1 st(SH)],'' as [2 nd(FH)],'' as [2 nd(SH)], " & _
      " '' as [3 rd(FH)],'' as [3 rd(SH)],'' as [4 th(FH)],'' as [4 th(SH)], '' as [5 th(FH)],'' as [5 th(SH)],'' as [6 th(FH)],'' as [6 th(SH)],'' as [7 th(FH)],'' as [7 th(SH)] ," & _
      " '' as [8 th(FH)],'' as [8 th(SH)],'' as [9 th(FH)],'' as [9 th(SH)],'' as [10 th(FH)],'' as [10 th(SH)],'' as [11 th(FH)],'' as [11 th(SH)],'' as [12 th(FH)],'' as [12 th(SH)], " & _
      " '' as [13 th(FH)],'' as [13 th(SH)],'' as [14 th(FH)],'' as [14 th(SH)], '' as [15 th(FH)],'' as [15 th(SH)],'' as [16 th(FH)],'' as [16 th(SH)],'' as [17 th(FH)],'' as [17 th(SH)]," & _
      " '' as [18 th(FH)],'' as [18 th(SH)],'' as [19 th(FH)],'' as [19 th(SH)],'' as [20 th(FH)],'' as [20 th(SH)],'' as [21 st(FH)],'' as [21 st(SH)],'' as [22 nd(FH)],'' as [22 nd(SH)], " & _
      " '' as [23 rd(FH)],'' as [23 rd(SH)],'' as [24 th(FH)],'' as [24 th(SH)],'' as [25 th(FH)],'' as [25 th(SH)],'' as [26 th(FH)],'' as [26 th(SH)],'' as [27 th(FH)],'' as [27 th(SH)]," & _
      " '' as [28 th(FH)],'' as [28 th(SH)],'' as [29 th(FH)],'' as [29 th(SH)],'' as [30 th(FH)],'' as [30 th(SH)],'' as [31 st(FH)],'' as [31 st(SH)]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub
End Class