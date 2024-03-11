Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmOwnBMCGainLossRate
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region
    Private Sub frmOwnBMCGainLossRate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        funReset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P  for Post ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        MyLabel10.Visible = False
        MyLabel7.Visible = False
        MyLabel8.Visible = False
        MyLabel9.Visible = False
        txtGainFATPer.Visible = False
        txtGainSNFPer.Visible = False
        txtLossFATPer.Visible = False
        txtLossSNFPer.Visible = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsOwnBMCGainLossRate()
                obj.code = txtCode.Value
                obj.Description = txtDescription.Text
                obj.Start_Date = dtstrDate.Value
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                End If
                obj.Gain_FAT_Rate = txtFat.Value
                obj.Gain_SNF_Rate = txtSNF.Value
                obj.Gain_FAT_Allow = txtGainFATPer.Value
                obj.Gain_SNF_Allow = txtGainSNFPer.Value


                obj.Loss_FAT_Rate = txtLFat.Value
                obj.Loss_SNF_Rate = txtLSnf.Value
                obj.Loss_FAT_Allow = txtLossFATPer.Value
                obj.Loss_SNF_Allow = txtLossSNFPer.Value
                If obj.SaveData(obj, isNewEntry) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isInsideLoadData = True
            funreset()
            Dim obj As clsOwnBMCGainLossRate = clsOwnBMCGainLossRate.GetData(strCode, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0) Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.code
                txtDescription.Text = obj.Description
                dtstrDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.End_Date
                Else
                    dtpEndDate.Checked = False
                End If
                txtFat.Value = obj.Gain_FAT_Rate
                txtSNF.Value = obj.Gain_SNF_Rate
                txtLFat.Value = obj.Loss_FAT_Rate
                txtLSnf.Value = obj.Loss_SNF_Rate
                UsLock1.Status = obj.Posted
                chkInactive.Checked = obj.Inactive



                txtLossFATPer.Value = obj.Loss_FAT_Allow
                txtLossSNFPer.Value = obj.Loss_SNF_Allow

                txtGainFATPer.Value = obj.Gain_FAT_Allow
                txtGainSNFPer.Value = obj.Gain_SNF_Allow

                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    chkInactive.Enabled = Not obj.Inactive
                    If chkInactive.Enabled Then
                        chkInactive.Enabled = MyBase.isPostFlag
                    End If
                End If
                btnSave.Text = "Update"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Function AllowToSave() As Boolean
        If txtFat.Value < 0 Then
            myMessages.blankValue("GFAT")
            txtFat.Focus()
            Return False
        End If

        If txtSNF.Value < 0 Then
            myMessages.blankValue("GSNF")
            txtSNF.Focus()
            Return False
        End If

        If txtLFat.Value < 0 Then
            myMessages.blankValue("LFAT")
            txtLFat.Focus()
            Return False
        End If

        If txtLSnf.Value < 0 Then
            myMessages.blankValue("LSNF")
            txtLSnf.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtDescription.Focus()
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
            If (myMessages.deleteConfirm()) Then
                If (clsOwnBMCGainLossRate.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_OWN_BMC_GAIN_LOSS_RATE where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsOwnBMCGainLossRate.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Focus()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        txtFat.Text = ""
        txtSNF.Text = ""
        txtLFat.Text = ""
        txtLSnf.Text = ""
        txtLossFATPer.Value = 0
        txtLossSNFPer.Value = 0
        txtGainFATPer.Value = 0
        txtGainSNFPer.Value = 0
        txtCode.Value = Nothing
        txtDescription.Text = Nothing
        dtpEndDate.Value = clsCommon.GETSERVERDATE()
        dtstrDate.Value = dtpEndDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub frmOwnBMCGainLossRate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub

    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Current code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (clsOwnBMCGainLossRate.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow(Me, "Successfully Inactivated", Me.Text)
                            End If
                        End If
                    End If
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (clsOwnBMCGainLossRate.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub


End Class