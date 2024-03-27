'--11/10/2013--form Add By- Pradeep Sharma ---------
'' work done against ticket no.  BHA/22/02/19-000817 
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
'Checkin by prabhakar 22/06/2020
Public Class frmOTSheet
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
                '' Anubhooti 10-July-2014 (BM00000002913)
                If clsLTAClaim.CheckPayHead(txtEmpCode.Value, "OT".ToUpper(), clsCommon.GETSERVERDATE()) = True Then
                    Dim obj As New clsOTSheet()
                    obj.Code = txtCode.Value
                    obj.EMP_CODE = clsCommon.myCstr(txtEmpCode.Value)
                    obj.OT_CODE = clsCommon.myCstr(txtOTCode.Value)
                    obj.OT_RATE = clsCommon.myCdbl(txtOtRate.Text)
                    obj.OT_HOURS = clsCommon.myCdbl(txtOTHours.Text)
                    obj.OT_TOTAL_AMOUNT = clsCommon.myCdbl(txtOTAmount.Text)
                    obj.PAY_PERIOD_CODE = clsCommon.myCstr(txtPayPeriod.Value)
                    obj.Location_Code = clsCommon.myCstr(fndLocation.Value)

                    If (obj.SaveData(obj, isNewEntry)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.Code, NavigatorType.Current)
                        'Else
                        '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
                    End If
                End If
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
        Dim obj As New clsOTSheet()
        obj = clsOTSheet.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            If obj.POSTED Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            If clsCommon.myLen(clsCommon.myCstr(obj.Location_Code)) > 0 Then
                fndLocation.Value = clsCommon.myCstr(obj.Location_Code)
                lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            Else
                fndLocation.Value = ""
                lblLocationName.Text = ""
            End If
            txtCode.Value = obj.Code
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            txtOTCode.Value = clsCommon.myCstr(obj.OT_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.Emp_Name)
            lblOtName.Text = clsCommon.myCstr(obj.OT_NAME)
            txtOtRate.Text = clsCommon.myCdbl(obj.OT_RATE)
            txtOTHours.Text = clsCommon.myCdbl(obj.OT_HOURS)
            txtOTAmount.Text = clsCommon.myCdbl(obj.OT_TOTAL_AMOUNT)
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
            myMessages.blankValue(Me, "OT Code", Me.Text)
            txtOTCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtOtRate.Text) <= 0 Then
            myMessages.blankValue(Me, "OT Rate ", Me.Text)
            txtOtRate.Focus()
            Return False
        ElseIf clsCommon.myLen(txtOTHours.Text) <= 0 Then
            myMessages.blankValue(Me, "OT Hours ", Me.Text)
            txtOTHours.Focus()
            Return False
        ElseIf clsCommon.myLen(txtOTAmount.Text) <= 0 Then
            myMessages.blankValue(Me, "OT Amount ", Me.Text)
            txtOTAmount.Focus()
            Return False
        ElseIf clsCommon.myLen(txtPayPeriod.Value) <= 0 Then
            myMessages.blankValue(Me, "Pay Period Code ", Me.Text)
            txtPayPeriod.Focus()
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
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select OT_SHEET_CODE  from TSPL_SHIPMENT_DETAILS  where OT_SHEET_CODE ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If
        '' Code Ends 
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
                If (clsOTSheet.DeleteData(txtCode.Value)) Then
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

    Private Sub frmOTSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
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
        btnPost.Enabled = False
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmOTSheet)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
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
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtEmpCode.Value = ""
        lblEmpName.Text = ""
        txtOTCode.Value = ""
        lblOtName.Text = ""
        txtOtRate.Text = ""
        txtOTHours.Text = ""
        txtOTAmount.Text = ""
        txtPayPeriod.Value = Nothing
        lblPayPeriod.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsOTSheet.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_OT_SHEET where OT_SHEET_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist "),
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = " select OT_SHEET_CODE AS Code, EMP_CODE as 'Employee Code', OT_CODE AS 'OT Code',OT_RATE as 'OT Rate' , OT_HOURS as 'OT Hours', OT_TOTAL_AMOUNT as 'Total OT Amount', Pay_Period_Code as 'Pay Period Code'  from TSPL_OT_SHEET"
            txtCode.Value = clsCommon.ShowSelectForm("OT_SHEET", qry, "Code", whrcls, txtCode.Value, "OT_SHEET_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Sub funFill()

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

    Private Sub frmOTSheet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        If transportSql.importExcel(gv, "Code", "Employee Code", "OT Code", "OT Rate", "OT Hours", "Total OT Amount", "Pay Period Code") Then
            'Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsOTSheet()

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(0).Value)) > 0 Then


                        Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                            Throw New Exception("Code can not be blank or incorrect.")
                        End If
                        obj.Code = strCode
                        Dim str As String = clsCommon.myCstr(grow.Cells(1).Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("Employee Code can not be blank or incorrect.")
                        End If

                        obj.EMP_CODE = str

                        str = clsCommon.myCstr(grow.Cells(2).Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("OT Code can not be blank or incorrect.")
                        End If
                        obj.OT_CODE = str

                        str = clsCommon.myCstr(grow.Cells(3).Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("OT Rate can not be blank or incorrect.")
                        End If
                        obj.OT_RATE = str

                        str = clsCommon.myCstr(grow.Cells(4).Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("OT Hours Code can not be blank or incorrect.")
                        End If
                        obj.OT_HOURS = str

                        str = clsCommon.myCstr(grow.Cells(5).Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("Total OT Amount Code can not be blank or incorrect.")
                        End If
                        obj.OT_TOTAL_AMOUNT = str

                        str = clsCommon.myCstr(grow.Cells(6).Value)
                        If str.Length > 30 Or (String.IsNullOrEmpty(str)) Then
                            Throw New Exception("Pay Period Code can not be blank or incorrect.")
                        End If
                        obj.PAY_PERIOD_CODE = str
                        '' CHECK FOR EXISTING CODE

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
        str = " select OT_SHEET_CODE AS Code, EMP_CODE as 'Employee Code', OT_CODE AS 'OT Code',OT_RATE as 'OT Rate' , OT_HOURS as 'OT Hours', OT_TOTAL_AMOUNT as 'Total OT Amount' ,PAY_PERIOD_CODE  AS 'Pay Period Code' from TSPL_OT_SHEET"
        transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim qry As String = "select EMP_CODE AS Code, Emp_Name AS Name ,Designation  from TSPL_EMPLOYEE_MASTER"
        txtEmpCode.Value = clsCommon.ShowSelectForm("EMP_FINDER", qry, "Code", whrcls, txtCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)
    End Sub

    Private Sub txtOTCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtOTCode._MYValidating
        Dim qry As String = " select OT_CODE as Code, OT_NAME as Name, DESCRIPTION as Description  ,HOUR_MULTIPLIER as 'Hours Multiplier', OT_RATE as 'OT Rate', IS_ASPER_ACTUAL_CALC as 'Is as per actual calculation' from TSPL_OT_MASTER"
        txtOTCode.Value = clsCommon.ShowSelectForm("OT_FINDER", qry, "Code", "", txtOTCode.Value, "OT_CODE", isButtonClicked)
        lblOtName.Text = clsOTMaster.GetName(txtOTCode.Value, Nothing)
        txtOtRate.Text = clsOTMaster.GetOTRate_ByOTCode(txtOTCode.Value, Nothing)
    End Sub
    Private Sub CalculateTotalAmount(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOtRate.Validated, txtOTHours.Validated
        If clsCommon.myLen(txtOtRate.Text) > 0 And clsCommon.myLen(txtOTHours.Text) > 0 Then
            txtOTAmount.Text = clsCommon.myCstr(clsCommon.myCdbl(txtOtRate.Text) * clsCommon.myCdbl(txtOTHours.Text))
        End If
    End Sub

    Private Sub txtPayPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPayPeriod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
        & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtPayPeriod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtPayPeriod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblPayPeriod.Text = clsPayPeriodMaster.GetName(txtPayPeriod.Value, Nothing)
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " Location_Type='Physical' And LOCATION_CODE='" + LocCode + "'"
                Else
                    whrcls = " Location_Type='Physical' "
                End If
            End If
            fndLocation.Value = clsLocation.getFinder(whrcls, Me.fndLocation.Value, isButtonClicked)
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex, Me.Text)
        End Try

    End Sub
End Class
