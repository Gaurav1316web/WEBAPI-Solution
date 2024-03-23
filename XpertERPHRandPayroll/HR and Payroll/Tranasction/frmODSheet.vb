'--31/10/2014--form Add By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmODSheet
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then

                'If clsLTAClaim.CheckPayHead(txtCode.Value, "OD".ToUpper(), clsCommon.GETSERVERDATE()) = True Then
                Dim obj As New clsODSheet()
                obj.Code = txtCode.Value
                obj.EMP_CODE = clsCommon.myCstr(txtEmpCode.Value)
                obj.OD_CODE = clsCommon.myCstr(txtOTCode.Value)
                obj.FROM_Date = clsCommon.myCDate(dtpFrom.Value)
                obj.TO_Date = clsCommon.myCDate(dtpTo.Value)
                obj.PURPOSE = clsCommon.myCstr(txtPurpose.Text)
                obj.MATERIAL_CARRYING = clsCommon.myCstr(txtMaterialCarrying.Text)
                obj.PAY_PERIOD_CODE = clsCommon.myCstr(txtPayPeriod.Value)

                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsODSheet()
        obj = clsODSheet.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            txtCode.Value = obj.Code
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            txtOTCode.Value = clsCommon.myCstr(obj.OD_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.Emp_Name)
            lblOtName.Text = clsCommon.myCstr(obj.OD_Description)
            Me.dtpFrom.Value = clsCommon.myCDate(obj.FROM_Date, "dd/MMM/yyyy hh:mm tt")
            Me.dtpTo.Value = clsCommon.myCDate(obj.TO_Date, "dd/MMM/yyyy hh:mm tt")
            txtPurpose.Text = clsCommon.myCstr(obj.PURPOSE)
            txtMaterialCarrying.Text = clsCommon.myCstr(obj.MATERIAL_CARRYING)
            txtPayPeriod.Value = clsCommon.myCstr(obj.PAY_PERIOD_CODE)
            lblPayPeriod.Text = clsCommon.myCstr(obj.PAY_PERIOD_NAME)
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Employee Code", Me.Text)
            txtEmpCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtOTCode.Value) <= 0 Then
            myMessages.blankValue(Me, "OD Code", Me.Text)
            txtOTCode.Focus()
            Return False
        ElseIf dtpFrom.Value >= dtpTo.Value Then
            myMessages.blankValue(Me, "From Date Time must be less than To Date Time", Me.Text)
            dtpFrom.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
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
                If (clsODSheet.DeleteData(txtCode.Value)) Then
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

    Private Sub frmODSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmODSheet)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        '' Anubhooti 24-July-2014 BM00000003193
        RadMenuItem3.Enabled = MyBase.isModifyFlag
        RadMenu2.Visible = MyBase.isExport
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
        txtEmpCode.Value = ""
        lblEmpName.Text = ""
        txtOTCode.Value = ""
        lblOtName.Text = ""
        Me.dtpFrom.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm tt")
        Me.dtpTo.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm tt")
        txtPayPeriod.Value = Nothing
        lblPayPeriod.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtPurpose.Text = ""
        txtMaterialCarrying.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_OUTDUTY_SHEET where OD_SHEET_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsODSheet.getFinder("Comp_Code='" & objCommonVar.CurrentCompanyCode & "'", txtCode.ValidateChildren, isButtonClicked) 'clsCommon.ShowSelectForm("OT_SHEET", qry, "Code", "", txtCode.Value, "OD_SHEET_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub


    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmODSheet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Employee Code", "OD Code", "From Date", "To Date") Then
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsODSheet()

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Code").Value)) > 0 Then

                        Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                        If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                            Throw New Exception("Code can not be blank or incorrect.")
                        End If
                        obj.Code = strCode
                        Dim str As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("Employee Code can not be blank or incorrect.")
                        End If

                        obj.EMP_CODE = str

                        str = clsCommon.myCstr(grow.Cells("OD Code").Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("OD Code can not be blank or incorrect.")
                        End If
                        obj.OD_CODE = str

                        Dim dtpFromDate As Date = clsCommon.myCDate(grow.Cells("From Date").Value)
                        Dim dtpToDate As Date = clsCommon.myCDate(grow.Cells("To Date").Value)

                        If dtpFromDate > dtpToDate Then
                            Throw New Exception("From Date Time must be less than To Date Time.")
                        End If
                        obj.FROM_Date = dtpFromDate
                        obj.TO_Date = dtpToDate

                        obj.SaveData(obj, obj.CheckOTCodeExist(obj.Code))
                    End If
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

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = " select OD_SHEET_CODE as Code,EMP_CODE as [Employee Code],OD_CODE as [OD Code]," & _
              " From_Date as [From Date], To_Date as [To Date] from TSPL_OUTDUTY_SHEET"
        transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpCode._MYValidating
        txtEmpCode.Value = clsEmployeeMaster.getFinder("", Me.txtEmpCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("EMP_FINDER", Qry, "Code", "", txtCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)
    End Sub

    Private Sub txtOTCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtOTCode._MYValidating
        txtOTCode.Value = clsODMaster.getFinder("Comp_Code='" & objCommonVar.CurrentCompanyCode & "'", txtOTCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("OT_FINDER", qry, "Code", "", txtOTCode.Value, "OD_Code", isButtonClicked)
        lblOtName.Text = clsODMaster.GetName(txtOTCode.Value, Nothing)

    End Sub


    'Private Sub txtPayPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPayPeriod._MYValidating
    '    Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
    '    & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
    '    'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
    '    txtPayPeriod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtPayPeriod.Value, "PAY_PERIOD_CODE", isButtonClicked)
    '    lblPayPeriod.Text = clsPayPeriodMaster.GetName(txtPayPeriod.Value, Nothing)
    'End Sub
    Sub PrintData()
        Dim qry As String = "select Emp_Name,convert(varchar,From_Date,103)as From_Date,convert(varchar,To_Date,103)as To_Date,PURPOSE,MATERIAL_CARRYING,Department_Name,Designation_Desc,Logo_img from TSPL_OUTDUTY_SHEET left join TSPL_EMPLOYEE_MASTER on "
        qry += " TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_OUTDUTY_SHEET.Emp_Code left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.Department_Code=TSPL_EMPLOYEE_MASTER.Department_Code left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_Id=TSPL_EMPLOYEE_MASTER.Designation left join TSPL_COMPANY_MASTER on  TSPL_COMPANY_MASTER.Comp_Code=TSPL_OUTDUTY_SHEET.Comp_code where TSPL_OUTDUTY_SHEET.Emp_Code='" & txtEmpCode.Value & "' and OD_SHEET_CODE='" & txtCode.Value & "'"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "CrptODSheet", "OD Sheet Report")
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
End Class
