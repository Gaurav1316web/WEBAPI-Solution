'--26/06/2013--form Add By- Pradeep Sharma ---------
'Updated @ Ticket No :BM00000000719
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLeaveStartingDateSetting
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
            Dim obj As New clsLeaveStartingDateSetting()

            obj.EMP_CODE = txtCode.Value
            obj.AVAIL_STARTDATE = dtpAvailStarting.Value
            obj.LEAVE_CODE = txtLeaveCode.Value
            obj.ALLOT_STARTDATE = dtpAllotStarting.Value
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.EMP_CODE, obj.LEAVE_CODE)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal strLeaveCode As String)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsLeaveStartingDateSetting()
        obj = clsLeaveStartingDateSetting.GetData(strCode, strLeaveCode)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.EMP_CODE
            txtLeaveCode.Value = obj.LEAVE_CODE
            lblEmpName.Text = obj.Emp_Name
            dtpAvailStarting.Value = obj.AVAIL_STARTDATE
            dtpAllotStarting.Value = obj.ALLOT_STARTDATE
        Else
            dtpAvailStarting.Value = Nothing
            dtpAllotStarting.Value = Nothing

        End If

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtLeaveCode.Value) <= 0 Then
            myMessages.blankValue("Leave Code")
            txtLeaveCode.Focus()
            Return False
        ElseIf clsCommon.myLen(dtpAllotStarting.Value) <= 0 Then
            myMessages.blankValue("Allot Starting Date")
            dtpAllotStarting.Focus()
            Return False
        ElseIf clsCommon.myLen(dtpAvailStarting.Value) <= 0 Then
            myMessages.blankValue("Avail Starting Date")
            dtpAvailStarting.Focus()
            Return False
        ElseIf dtpAllotStarting.Value > dtpAvailStarting.Value Then
            clsCommon.MyMessageBoxShow(" Avail date must be greater than or equal to Allot date.")
            dtpAvailStarting.Focus()
            Return False
        End If
        Dim strchk As String = " select Joining_date from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtCode.Value + "' "
        Dim JoiningDate As String = clsDBFuncationality.getSingleValue(strchk)
        If clsCommon.myLen(JoiningDate) > 0 Then
            If clsCommon.myCDate(JoiningDate) > dtpAllotStarting.Value Then
                clsCommon.MyMessageBoxShow("Allot Starting date can not be smaller then Joining date : '" + JoiningDate + "' of Employee.")
                Return False
            End If
            If clsCommon.myCDate(JoiningDate) > dtpAvailStarting.Value Then
                clsCommon.MyMessageBoxShow("Avail Starting date can not be smaller then Joining date : '" + JoiningDate + "' of Employee.")
                Return False
            End If
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
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsLeaveStartingDateSetting.DeleteData(txtCode.Value, txtLeaveCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmLeaveStartingDateSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveStartingDateSetting)
        '--Preeti gupta--ticket no[BM00000003189]
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If btnsave.Visible = True Then
            RadMenuItem3.Enabled = True
            MenuItemExport.Enabled = True
        Else
            RadMenuItem3.Enabled = False
            MenuItemExport.Enabled = False
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtLeaveCode.Value = Nothing
        lblEmpName.Text = ""
        dtpAvailStarting.Value = clsCommon.GETSERVERDATE()
        dtpAllotStarting.Value = clsCommon.GETSERVERDATE()
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
            Dim qry As String = " select EMP_CODE as Code,  Emp_Name as Name from TSPL_EMPLOYEE_MASTER "
            txtCode.Value = clsCommon.ShowSelectForm("EMP_FND", qry, "Code", "", txtCode.Value, "EMP_CODE", isButtonClicked)
            lblEmpName.Text = clsEmployeeMaster.GetName(txtCode.Value, Nothing)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, txtLeaveCode.Value)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub frmLeaveStartingDateSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        Dim qry As String = "select LEAVE_CODE AS Code, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary'  from TSPL_LEAVE_MASTER"
        txtLeaveCode.Value = clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", "", txtCode.Value, "LEAVE_CODE", isButtonClicked)
        If txtLeaveCode.Value <> "" Then
            LoadData(txtCode.Value, txtLeaveCode.Value)
        End If
    End Sub
    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Emp Code", "Leave Code", "Allot Start Date", "Avail Start Date") Then
            ' Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsLeaveStartingDateSetting()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Emp Code can not be blank or incorrect.")
                    End If
                    obj.EMP_CODE = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Leave Code can not be blank or incorrect.")
                    End If
                    obj.LEAVE_CODE = strName

                    Dim strDate As DateTime = clsCommon.GetPrintDate(grow.Cells(2).Value, "dd/MMM/yyyy")
                    If strDate.Year < 2000 Then
                        Throw New Exception("Allot Start Date can not be blank or incorrect.")
                    End If
                    obj.ALLOT_STARTDATE = strDate

                    strDate = clsCommon.GetPrintDate(grow.Cells(3).Value, "dd/MMM/yyyy")
                    If strDate.Year < 2000 Then
                        Throw New Exception("Avail Start Date can not be blank or incorrect.")
                    End If
                    obj.AVAIL_STARTDATE = strDate

                    obj.SaveData(obj, True)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
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
        str = "select EMP_CODE as 'Emp Code', LEAVE_CODE as 'Leave Code', ALLOT_STARTDATE as 'Allot Start Date', AVAIL_STARTDATE as 'Avail Start Date' from TSPL_LEAVE_STARTINGDATE "
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
