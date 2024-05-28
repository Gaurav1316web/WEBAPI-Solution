Imports common
Public Class Enter_password

    Public strMCCCode As String = ""
    Public Form_ID As String = ""

    Dim frmPWD As New FrmPWD(Nothing)
    Dim Enter_password As New FrmPWD(Nothing)


    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'Dim obj As New clsMccMaster
        'obj.Recipt_Password = txtpan.Text
        'obj.Sample_Password = txtsample.Text
        'Dim MCC_Code As String = Nothing
        Enter_password.strType = clsFixedParameterType.SettlementBankOnlyPWD
        Enter_password.strCode = clsFixedParameterCode.SettlementBankOnlyPWD
        'obj.MCC_Code = strMCCCode
        If clsCommon.myLen(txtpan.Text) > 0 OrElse clsCommon.myLen(txtsample.Text) > 0 Then
            clsMccMaster.UpdatePassword(txtsample.Text, txtpan.Text, strMCCCode, Nothing)
            clsCommon.MyMessageBoxShow(Me, "Password Updated Successfully", Me.Text)
        End If

        'Dim frmPWD As New FrmPWD(Nothing)
        'frmPWD.strType = clsFixedParameterType.SettlementBankOnlyPWD
        'frmPWD.strCode = clsFixedParameterCode.SettlementBankOnlyPWD
        'frmPWD.ShowDialog()
        'If frmPWD.isPasswordCorrect Then
        '    Dim frm As New frmSetting
        '    frm.strFormID = Me.Form_ID
        '    frm.ShowDialog()
        '    If frm.isDataSaved Then
        '        clsCommon.MyMessageBoxShow(Me, "Setting saved successfully." + Environment.NewLine + Me.Text + " will close automatic For apply new settings")
        '        clsERPFuncationality.closeForm(Me)
        '    End If
        'End If
        'clsMccMaster.updatepassword(obj)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub


End Class
