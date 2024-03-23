'--23/04/2014--form Add By- Ashwani Raghav ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Public Class FrmEmployeeGratuity
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsEmployeeGratuity()

            obj.EMP_CODE = txtCode.Value
            obj.DOJ = MyDateTimePicker1.Value
            obj.DOL = MyDateTimePicker2.Value
            obj.LASTDRAWNSALARY = txtLastDrawnSalary.Value
            obj.NOOFYEARS = txtNOF.Value
            obj.GRATUITYAMT = txtGratuity.Value
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.EMP_CODE, NavigatorType.Current)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsEmployeeGratuity()
        obj = clsEmployeeGratuity.GetData(strCode, NavTyep)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.EMP_CODE
            lblEmpName.Text = obj.Emp_Name
            MyDateTimePicker1.Value = obj.DOJ
            MyDateTimePicker2.Value = obj.DOL
            txtLastDrawnSalary.Value = obj.LASTDRAWNSALARY
            txtNOF.Value = obj.NOOFYEARS
            txtGratuity.Value = obj.GRATUITYAMT
        End If

    End Sub

    Function AllowToSave() As Boolean
        Dim DOJ As Date
        ' Dim DOL As Date
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Employee Code", Me.Text)
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtLastDrawnSalary.Value) <= 0 Then
            myMessages.blankValue(Me, "Last Drawn Salary", Me.Text)
            txtLastDrawnSalary.Focus()
            Return False
        ElseIf clsCommon.myLen(txtNOF.Value) <= 0 Then
            myMessages.blankValue(Me, "No Of Year", Me.Text)
            txtLastDrawnSalary.Focus()
            Return False
        ElseIf clsCommon.myLen(txtGratuity.Value) <= 0 Then
            myMessages.blankValue(Me, "Gratuity", Me.Text)
            txtLastDrawnSalary.Focus()
            Return False
        ElseIf Date.TryParse(MyDateTimePicker1.Text, DOJ) = False Then
            clsCommon.MyMessageBoxShow(Me, "Invalid DOJ.", Me.Text)
            MyDateTimePicker1.Focus()
            Return False
        ElseIf Date.TryParse(MyDateTimePicker2.Text, DOJ) = False Then
            clsCommon.MyMessageBoxShow(Me, "Invalid DOL.", Me.Text)
            MyDateTimePicker2.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

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
                If (clsEmployeeGratuity.DeleteData(txtCode.Value)) Then
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

    Private Sub frmLeaveOpeningBalance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveOpeningBalance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        lblEmpName.Text = ""
        MyDateTimePicker1.Value = Nothing
        MyDateTimePicker2.Value = Nothing
        txtLastDrawnSalary.Value = Nothing
        txtNOF.Value = Nothing
        txtGratuity.Value = Nothing
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select EMP_CODE as Code,  Emp_Name as Name,Joining_date DOJ,RELIEVING_DATE DOL from TSPL_EMPLOYEE_MASTER "
            txtCode.Value = clsCommon.ShowSelectForm("EMP_FND", qry, "Code", "RELIEVING_DATE>convert(datetime,Joining_date,103)", txtCode.Value, "EMP_CODE", isButtonClicked)
            lblEmpName.Text = clsEmployeeMaster.GetName(txtCode.Value, Nothing)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmLeaveOpeningBalance_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    'Sub PrintData()
    '    Try
    '        Dim Qry As String = ""
    '        Qry = " select EMP_CODE,(select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE=TSPL_GRATUITY.EMP_CODE)Emp_Name,"
    '        Qry += " DOJ,DOL,LASTDRAWNSALARY,NOOFYEARS,GRATUITYAMT,"
    '        Qry += " (select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "')Comp_Name"
    '        Qry += " from TSPL_GRATUITY where EMP_CODE='" & txtCode.Value & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        If dt.Rows.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("No Data Found")
    '        Else
    '            PayRoll_HR_ReportViewer.funreport(dt, "crptEmployeeGratuity", "Employee Gratuity")
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Sub PrintData()
        Try
            Dim Qry As String = ""
            Qry = "select '' as Blank1,'' as Blank2,'' as Blank3,'' as Blank4,'' as Blank5,'' as Blank6,'' as Blank7,TSPL_GRATUITY.Emp_Code,Emp_Name,(Add1+Add2)as Address,GRATUITYAMT,convert(varchar,TSPL_GRATUITY.Created_By,103)as Created_By,convert(varchar,TSPL_GRATUITY.Created_Date,103)as Created_Date,(DATEDIFF(dd,convert(Date,'" & MyDateTimePicker1.Value & "',103),convert(Date,'" & MyDateTimePicker2.Value & "',103))/365) as DateYear,(DATEDIFF(dd,convert(Date,'" & MyDateTimePicker1.Value & "',103),convert(Date,'" & MyDateTimePicker2.Value & "',103))%365)/30 AS DateMonth from TSPL_GRATUITY left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_GRATUITY.Emp_Code"
            Qry += " where TSPL_GRATUITY.Emp_Code='" & txtCode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
9:              common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrsytal As New frmCrystalReportViewer
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dt, "crptEmployeeGratuityReport", "Employee Gratuity")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

