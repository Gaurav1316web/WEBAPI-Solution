
Imports common
Imports XpertERPEngine
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngineFine
Imports Telerik
Public Class frmBullMovement
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub frmBullMovement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New")
        funReset()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        RadMenuItem1.Enabled = MyBase.isModifyFlag ' For Import
        RadMenuItem2.Enabled = MyBase.isModifyFlag ' For Export
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmBullMovement_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnsave.Enabled AndAlso MyBase.isDeleteFlag Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtBullCode.Enabled = True
        txtBullCode.Value = ""
        LblBullName.Text = ""
        TxtShed.Enabled = True
        TxtShed.Value = ""
        LblShed.Text = ""
        txtPeriod.Text = ""
        txtMovementType.Value = ""
        txtMovementType.Enabled = True
        lbleMovementType.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        btnReverse.Visible = False
        UsLock1.Status = ERPTransactionStatus.Pending
        btndelete.Enabled = True
        btnPost.Enabled = True
        btnsave.Enabled = True
    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBullMovement, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
            End If
            Dim obj As New clsBullMovement()
            obj.Document_Code = txtCode.Value
            obj.Document_Date = txtDate.Value
            obj.Perid = txtPeriod.Value
            obj.Bull_Code = txtBullCode.Value
            obj.Bull_Shed = TxtShed.Value
            obj.Bull_Movement_Type = txtMovementType.Value

            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtBullCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Bull Code", Me.Text)
                txtBullCode.Focus()
                txtBullCode.Select()
                ErrorControl.SetError(txtBullCode, "Please Select Bull Code")
                Return False
            Else
                ErrorControl.ResetError(txtBullCode)
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = True
            btnsave.Enabled = True
            btnsave.Text = "Update"
            txtCode.MyReadOnly = False
            Dim obj As clsBullMovement = clsBullMovement.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtCode.Value = obj.Document_Code
                txtDate.Text = obj.Document_Date
                txtBullCode.Value = obj.Bull_Code
                TxtShed.Value = obj.Bull_Shed
                txtMovementType.Value = obj.Bull_Movement_Type
                txtPeriod.Text = obj.Perid
                txtCode.MyReadOnly = True
                LblBullName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bull_Alia_Name from TSPL_BULL_MASTER where Bull_Code='" + obj.Bull_Code + "'"))
                LblShed.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_SHED_MASTER where Code='" + obj.Bull_Shed + "'"))
                lbleMovementType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_MOVEMENT_TYPE where Code='" + obj.Bull_Movement_Type + "'"))
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                Else
                    btndelete.Enabled = True
                    btnsave.Text = "Update"
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                End If
                UsLock1.Status = obj.Status
            Else

                clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtBullCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBullCode._MYValidating
        Dim qry As String = "select Bull_Code as Code , Bull_Alia_Name as [Bull Name] from TSPL_BULL_MASTER"
        txtBullCode.Value = clsCommon.ShowSelectForm("BULLFND", qry, "Code", "", txtBullCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtBullCode.Value) > 0 Then
            LblBullName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bull_Alia_Name from TSPL_BULL_MASTER where Bull_Code='" + txtBullCode.Value + "'"))
        Else
            txtBullCode.Value = ""
            LblBullName.Text = ""
        End If
    End Sub

    Private Sub TxtShed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtShed._MYValidating
        Dim qry As String = "select Code , Name  from TSPL_BULL_SHED_MASTER"
        TxtShed.Value = clsCommon.ShowSelectForm("BULLSHEDFND", qry, "Code", "", TxtShed.Value, "Code", isButtonClicked)

        If clsCommon.myLen(TxtShed.Value) > 0 Then
            LblShed.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_SHED_MASTER where Code='" + TxtShed.Value + "'"))
        Else
            TxtShed.Value = ""
            LblShed.Text = ""
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                ErrorControl.SetError(txtCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete")
            Else
                ErrorControl.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsBullMovement.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    funReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim Sqlqry As String = "select Document_Code,Document_Date,Bull_Code,Bull_Movement_Type,Bull_Shed,Period,Status from TSPL_BULL_MOVEMENT where Document_Code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Document_Code as [Document Code],Document_Date as [Document Date],Bull_Code as [Bull Code],Bull_Movement_Type as [Bull Movement Type],Bull_Shed as [Bull Shed],Period,Status from TSPL_BULL_MOVEMENT"
            txtCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Document Code", whrClas, txtCode.Value, "TSPL_BULL_MOVEMENT.Document_Code asc", isButtonClicked, Nothing)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then

                If (clsBullMovement.PostData(txtCode.Value)) Then
                    msg = "Successfully Posted"

                End If
                If clsCommon.myLen(msg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(msg)
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        ReverseData()
    End Sub
    Sub ReverseData()

        Try

            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                'Dim Reason As String = ""
                'Dim frm As New FrmFreeTxtBox1
                'frm.Text = "Remarks for Reverse"
                'frm.ShowDialog()
                'If clsCommon.myLen(frm.strRmks) <= 0 Then
                '    Exit Sub
                'Else
                '    Reason = frm.strRmks
                'End If
                If clsBullMovement.ReverseData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtMovementType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMovementType._MYValidating
        Dim qry As String = "select Code  , Name ,Peridocity from TSPL_BULL_MOVEMENT_TYPE"
        txtMovementType.Value = clsCommon.ShowSelectForm("MVMNTFND", qry, "Code", "", txtMovementType.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtMovementType.Value) > 0 Then
            lbleMovementType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_MOVEMENT_TYPE where Code='" + txtMovementType.Value + "'"))
            Dim periodic As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Peridocity from TSPL_BULL_MOVEMENT_TYPE where Code='" + txtMovementType.Value + "'"))
            txtPeriod.Value = txtDate.Value.AddDays(periodic)
        Else
            txtMovementType.Value = ""
            lbleMovementType.Text = ""
        End If
    End Sub

End Class