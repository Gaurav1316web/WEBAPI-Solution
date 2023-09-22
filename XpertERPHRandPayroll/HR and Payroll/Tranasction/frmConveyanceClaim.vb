'--01/11/2014--form Add By- Panch Raj ---------
Imports XpertERPEngine
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmConveyanceClaim
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
                Dim obj As New clsConveyanceClaim
                obj.Code = txtCode.Value
                obj.EMP_CODE = clsCommon.myCstr(txtEmpCode.Value)
                obj.CONV_RATE_CODE = clsCommon.myCstr(fndRateCode.Value)
                obj.CONV_TYPE = lblConvType.Text
                obj.CONV_RATE = clsCommon.myCdbl(lblConvRate.Text)
                obj.CLAIM_DISTANCE = clsCommon.myCdbl(txtDist.Text)
                obj.CLAIM_AMOUNT = clsCommon.myCdbl(lblClaimAmount.Text)
                obj.Pay_Period_Code = clsCommon.myCstr(txtPayPeriod.Value)
                obj.Location_Code = fndLocation.Value
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsConveyanceClaim()
        obj = clsConveyanceClaim.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            txtCode.Value = obj.Code
            txtEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            fndRateCode.Value = clsCommon.myCstr(obj.CONV_RATE_CODE)
            lblEmpName.Text = clsCommon.myCstr(obj.Emp_Name)
            lblRateDesc.Text = clsCommon.myCstr(obj.Conv_Rate_Desc)
            lblConvType.Text = obj.CONV_TYPE
            lblConvRate.Text = obj.CONV_RATE
            lblClaimAmount.Text = obj.CLAIM_AMOUNT
            txtDist.Text = obj.CLAIM_DISTANCE
            txtPayPeriod.Value = clsCommon.myCstr(obj.Pay_Period_Code)
            lblPayPeriod.Text = clsCommon.myCstr(obj.PAY_PERIOD_NAME)
            If clsCommon.myLen(obj.Location_Code) > 0 Then
                fndLocation.Value = obj.Location_Code
                lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            End If
        End If

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtEmpCode.Focus()
            Return False
        ElseIf clsCommon.myCdbl(txtDist.Text) <= 0 Then
            myMessages.blankValue("Distance")
            txtDist.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
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
                If (clsConveyanceClaim.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
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

    Private Sub frmConveyanceClaim_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmConveyanceClaim)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        '' Anubhooti 24-July-2014 BM00000003193
        RadMenuItem3.Enabled = MyBase.isModifyFlag
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
        fndRateCode.Value = ""
        lblRateDesc.Text = ""
        lblClaimAmount.Text = ""
        lblConvRate.Text = ""
        lblConvType.Text = ""
        txtPayPeriod.Value = Nothing
        lblPayPeriod.Text = ""
        txtDist.Text = 0
        fndRateCode.Value = Nothing
        lblRateDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocation.Text = ""
        End If
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
                whrcls = " TSPL_CONVEYANCE_CLAIM.Comp_Code='" & objCommonVar.CurrentCompanyCode & "' And TSPL_EMPLOYEE_MASTER.LOCATION_CODE='" + LocCode + "'"
            Else
                whrcls = " TSPL_CONVEYANCE_CLAIM.Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_CONVEYANCE_CLAIM where CLAIM_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsConveyanceClaim.getFinder(whrcls, txtCode.ValidateChildren, isButtonClicked) 'clsCommon.ShowSelectForm("OT_SHEET", qry, "Code", "", txtCode.Value, "CLAIM_CODE", isButtonClicked)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmConveyanceClaim_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        If transportSql.importExcel(gv, "Code", "Employee Code", "Distance") Then
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsConveyanceClaim()

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

                        str = clsCommon.myCdbl(grow.Cells("Distance").Value)
                        If clsCommon.myCdbl(str) <= 0 Then
                            Throw New Exception("Distance must be greater than zero.")
                        End If
                        obj.CLAIM_DISTANCE = str

                        obj.CONV_TYPE = clsDBFuncationality.getSingleValue("select Conv_Type from tspl_employee_master where emp_code='" & obj.EMP_CODE & "'")


                        obj.SaveData(obj, obj.CheckOTCodeExist(obj.Code))
                    End If
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

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = " select CLAIM_CODE as Code,EMP_CODE as [Employee Code],Distance as [Distance] from TSPL_CONVEYANCE_CLAIM"
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
        txtEmpCode.Value = clsEmployeeMaster.getFinder(whrcls, Me.txtEmpCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("EMP_FINDER", Qry, "Code", "", txtCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)
        lblConvType.Text = clsDBFuncationality.getSingleValue("select Conv_Type from tspl_employee_master where emp_code='" & txtEmpCode.Value & "'")
    End Sub

    Private Sub txtOTCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRateCode._MYValidating
        fndRateCode.Value = clsConveyanceRateMaster.getFinder("Comp_Code='" & objCommonVar.CurrentCompanyCode & "'", fndRateCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("OT_FINDER", qry, "Code", "", txtOTCode.Value, "CONV_RATE_CODE", isButtonClicked)
        Dim obj As clsConveyanceRateMaster = clsConveyanceRateMaster.GetData(fndRateCode.Value, NavigatorType.Current)
        If Not obj Is Nothing Then
            lblRateDesc.Text = obj.Description
            lblConvRate.Text = obj.CONV_RATE
            lblClaimAmount.Text = clsCommon.myCdbl(lblConvRate.Text) * clsCommon.myCdbl(txtDist.Text)
        Else
            lblRateDesc.Text = ""
            lblConvRate.Text = 0
            lblClaimAmount.Text = 0
        End If

    End Sub


    Private Sub txtDist_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDist.TextChanged
        lblClaimAmount.Text = clsCommon.myCdbl(lblConvRate.Text) * clsCommon.myCdbl(txtDist.Text)
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " TSPL_Location_MASTER.LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            fndLocation.Value = clsLocation.getFinder(whrcls, Me.fndLocation.Value, isButtonClicked)
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex, Me.Text)
        End Try
    End Sub
End Class
