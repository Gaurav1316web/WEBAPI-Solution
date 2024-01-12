'--25/06/2013--form Add By- Pradeep Sharma ---------
'Updated @ Ticket No.: BM00000000718
Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmLeaveOpeningBalance
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
    '' changes by shivani against[7953]
    Public Sub Save()

        If AllowToSave() Then
            Dim obj As New clsLeaveOpeningBalance()

            obj.CODE = txtCode.Value
            obj.EMP_CODE = txtEmpCode.Value
            obj.PAY_PERIOD_CODE = fndPayPeriod.Value
            obj.OPENING_DATE = clsCommon.GetPrintDate(dtpOpeningDate.Value, "dd/MM/yyyy")
            obj.LEAVE_CODE = txtLeaveCode.Value
            obj.OPENING_BAL = txtOpeningBalance.Value
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.CODE, NavigatorType.Current)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsLeaveOpeningBalance()
        obj = clsLeaveOpeningBalance.GetData(strCode, NavTyep)
        'funReset()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
            ''funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.CODE
            txtEmpCode.Value = obj.EMP_CODE
            txtLeaveCode.Value = obj.LEAVE_CODE
            lblLeaveDesc.Text = clsLeaveMaster.GetName(txtLeaveCode.Value, Nothing)
            lblEmpName.Text = obj.Emp_Name
            fndPayPeriod.Value = obj.PAY_PERIOD_CODE
            lblPayPeriodName.Text = clsDBFuncationality.getSingleValue("select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" & fndPayPeriod.Value & "'")
            dtpOpeningDate.Value = clsCommon.GetPrintDate(obj.OPENING_DATE, "dd/MM/yyyy")
            txtOpeningBalance.Value = obj.OPENING_BAL
        End If

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtEmpCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtLeaveCode.Value) <= 0 Then
            myMessages.blankValue("Leave Code")
            txtLeaveCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtOpeningBalance.Value) <= 0 Or clsCommon.myCdbl(Me.txtOpeningBalance.Value) > 366 Then
            clsCommon.MyMessageBoxShow(Me, "Opening Balance must be between 0 and 366 !", Me.Text)
            txtOpeningBalance.Focus()
            Return False
        End If
        Dim strchk As String = " select Joining_date from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtCode.Value + "' "
        Dim JoiningDate As String = clsDBFuncationality.getSingleValue(strchk)
        If clsCommon.myLen(JoiningDate) > 0 And clsCommon.myCDate(JoiningDate) > dtpOpeningDate.Value Then
            clsCommon.MyMessageBoxShow(Me, "Opening date can not be smaller then Joining date : '" + JoiningDate + "' of Employee.")
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
                If (clsLeaveOpeningBalance.DeleteData(txtCode.Value)) Then
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

    Private Sub frmLeaveOpeningBalance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        'dtpOpeningDate.Enabled = False
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveOpeningBalance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        '' Anubhooti 24-July-2014 BM00000003187
        RadMenuItem3.Enabled = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = True
        txtCode.Value = Nothing
        txtEmpCode.Value = Nothing
        txtCode.Focus()
        txtLeaveCode.Value = Nothing
        lblLeaveDesc.Text = ""
        lblEmpName.Text = ""
        dtpOpeningDate.Value = clsCommon.GETSERVERDATE()
        'dtpOpeningDate.Enabled = False
        fndPayPeriod.Value = ""
        lblPayPeriodName.Text = ""
        txtOpeningBalance.Value = Nothing
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

    Private Sub txtEmpCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpCode._MYValidating

        txtEmpCode.Value = clsEmployeeMaster.getFinder("", Me.txtEmpCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("EMP_FND", qry, "Code", "", txtEmpCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)

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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub


    Private Sub txtLeaveCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLeaveCode._MYValidating
        'Dim qry As String = "select LEAVE_CODE AS Code, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary',lEAVE_tYPE  from TSPL_LEAVE_MASTER"
        txtLeaveCode.Value = clsLeaveMaster.getFinder("", txtLeaveCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", "", txtCode.Value, "LEAVE_CODE", isButtonClicked)
        lblLeaveDesc.Text = clsLeaveMaster.GetName(txtLeaveCode.Value, Nothing)
    End Sub
    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Emp Code", "Leave Code", "Opening Date", "Opening Balance") Then
            ' Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsLeaveOpeningBalance()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj.CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Emp Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Emp Code can not be blank or incorrect.")
                    End If
                    obj.EMP_CODE = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Leave Code").Value)
                    If strName.Length > 30 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Leave Code can not be blank or incorrect.")
                    End If
                    obj.LEAVE_CODE = strName

                    Dim strDate As DateTime = clsCommon.GetPrintDate(grow.Cells("Opening Date").Value, "dd/MMM/yyyy")
                    If strDate.Year < 2000 Then
                        Throw New Exception("Opening Date can not be blank or incorrect.")
                    End If
                    obj.OPENING_DATE = strDate

                    Dim strDes As Double = clsCommon.myCdbl(grow.Cells("Opening Balance").Value)
                    If strDes > 999.99 Then
                        Throw New Exception("Opening Balance can not be blank or incorrect.")
                    End If
                    obj.OPENING_BAL = strDes

                    obj.SaveData(obj, True)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = "select Code,EMP_CODE as 'Emp Code', LEAVE_CODE as 'Leave Code', OPENING_DATE as 'Opening Date', OPENING_BAL as 'Opening Balance' from TSPL_LEAVE_OPENINGBAL "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        'Dim str As String = "select count(*) from TSPL_LEAVE_OPENINGBAL where CODE ='" + txtCode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtCode.MyReadOnly = True
        '    'txtCode.Value = ""
        '    '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        'Else
        '    txtCode.MyReadOnly = True
        'End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = " select EMP_CODE as Code,  Emp_Name as Name from TSPL_EMPLOYEE_MASTER "
            txtCode.Value = clsLeaveOpeningBalance.getFinder("", txtCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("EMP_FND", qry, "Code", "", txtCode.Value, "EMP_CODE", isButtonClicked)

            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub fndPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPayPeriod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
        & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        fndPayPeriod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", fndPayPeriod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        If clsCommon.myLen(fndPayPeriod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(fndPayPeriod.Value, NavigatorType.Current)
            lblPayPeriodName.Text = clspp.Name
            fndPayPeriod.Value = clspp.Code
            dtpOpeningDate.Value = clspp.DATE_FROM
            'dtpOpeningDate.Enabled = False
            '' Else
            'lblPayPeriodName.Text = ""
            ''  dtpOpeningDate.Value = clsCommon.GETSERVERDATE()
        End If
    End Sub
End Class
