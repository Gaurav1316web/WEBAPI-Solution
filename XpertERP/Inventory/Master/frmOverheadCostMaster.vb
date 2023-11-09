Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmOverheadCostMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim isIncludeRatePerHoursIn As Boolean = False
#End Region
    Private Sub frmOverheadCostMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ' Ticket No : BHA/03/08/18-000387 By prabhakar for include Rate Per Hours
        isIncludeRatePerHoursIn = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IncludeRatePerHoursIn & "'")) = 0, False, True)
        funReset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")

        If isIncludeRatePerHoursIn = True Then
            lblRatePerHours.Visible = True
            txtRatePerHour.Visible = True
        Else
            lblRatePerHours.Visible = False
            txtRatePerHour.Visible = False
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCode.MyReadOnly = False
        txtGlAccount.Value = ""
        lblGlAccount.Text = ""
        txtRatePerHour.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        funClose()
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()

        If AllowToSave() Then

            Dim obj As New clsOverheadCost
            obj.COST_CODE = txtCode.Value
            obj.Description = txtDesc.Text
            obj.GLAccount = txtGlAccount.Value
            ' Ticket No : BHA/03/08/18-000387 By prabhakar
            If isIncludeRatePerHoursIn = True Then
                obj.RatePerHour = txtRatePerHour.Text
            End If
            If (clsOverheadCost.SaveData(obj, isNewEntry, Nothing)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                LoadData(obj.COST_CODE, NavigatorType.Current)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If

        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False

        ElseIf clsCommon.myLen(txtDesc.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDesc.Focus()
            Return False
        ElseIf isIncludeRatePerHoursIn = True AndAlso String.IsNullOrEmpty(txtRatePerHour.Text) = True Then
            myMessages.blankValue("Rate/Hour")
            txtRatePerHour.Focus()
            Return False
        End If
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsOverheadCost()
        obj = clsOverheadCost.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.COST_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtCode.Value = obj.COST_CODE
            txtDesc.Text = obj.Description
            txtGlAccount.Value = obj.GLAccount
            lblGlAccount.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_gl_accounts where Account_Code ='" + txtGlAccount.Value + "'"))
            txtCode.MyReadOnly = True
            txtRatePerHour.Text = obj.RatePerHour
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsOverheadCost.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_OVERHEAD_COST where Cost_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsOverheadCost.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If

    End Sub

    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub MenuItemExport_Click(sender As Object, e As EventArgs) Handles MenuItemExport.Click
        Dim str As String
        If isIncludeRatePerHoursIn = True Then
            str = "Select Cost_Code as [Cost Code] ,Description,GL_ACC AS GLAccount,RatePerHour from TSPL_OVERHEAD_COST"
        Else
            str = "Select Cost_Code as [Cost Code] ,Description,GL_ACC AS GLAccount   from TSPL_OVERHEAD_COST"
        End If
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub MenuItemImport_Click(sender As Object, e As EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim strColumn As Boolean = False
        If isIncludeRatePerHoursIn = True Then
            strColumn = transportSql.importExcel(gv, "Cost Code", "Description", "GLAccount", "RatePerHour")
        Else
            strColumn = transportSql.importExcel(gv, "Cost Code", "Description", "GLAccount")
        End If
        If strColumn Then
            Try
                clsCommon.ProgressBarShow()
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsOverheadCost()
                        obj.COST_CODE = clsCommon.myCstr(grow.Cells("Cost Code").Value)
                        If clsCommon.myLen(obj.COST_CODE) <= 0 Then
                            Throw New Exception(" Cost Code can not be blank or incorrect.")
                        End If
                        If clsCommon.myLen(obj.COST_CODE) > 30 Then
                            Throw New Exception(" Cost Code length can not be greater then 30.")
                        End If
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception(" Description can not be blank or incorrect.")
                        End If
                        If clsCommon.myLen(obj.Description) > 200 Then
                            Throw New Exception(" Description length can not be greater then 200.")
                        End If
                        obj.GLAccount = clsCommon.myCstr(grow.Cells("GLAccount").Value)
                        If clsCommon.myLen(obj.GLAccount) > 0 Then
                            Dim Qry As String = " select Account_Code from tspl_gl_accounts where Account_Code ='" + obj.GLAccount + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)
                            If (dt.Rows.Count <= 0) Then
                                Throw New Exception(" Please Provide Valid GLAccount")
                            End If
                        End If
                        If isIncludeRatePerHoursIn = True Then
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("RatePerHour").Value)) <= 0 Then
                                Throw New Exception("RatePerHour can not be blank or incorrect.")
                            End If

                            If IsNumeric(clsCommon.myCstr(grow.Cells("RatePerHour").Value)) = False Then
                                Throw New Exception("RatePerHour value is incorrect for Cost code : " + clsCommon.myCstr(obj.COST_CODE) + " .")
                            End If
                            obj.RatePerHour = clsCommon.myCstr(grow.Cells("RatePerHour").Value)
                        End If
                        clsOverheadCost.SaveData(obj, clsOverheadCost.CheckNewEntry(obj.COST_CODE, tran), tran)
                    Next
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception("At Row No " + clsCommon.myCstr(ii) + ex.Message)
                End Try
                tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub frmOverheadCostMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub txtGlAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGlAccount._MYValidating
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        'txtGLAccount.Value = clsCommon.ShowSelectForm("DisountMasterAccount", qry, "Account", "", txtGLAccount.Value, "Account", isButtonClicked)
        txtGlAccount.Value = clsGLAccount.getFinder("", txtGlAccount.Value, isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + txtGlAccount.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblGlAccount.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            lblGlAccount.Text = ""
        End If
    End Sub
End Class
