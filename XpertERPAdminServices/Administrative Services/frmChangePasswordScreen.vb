Imports common
Public Class frmChangePasswordScreen
    Private Sub frmChangePasswordScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblHeader.Text = "Change Password of Weightment"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnSave_Click(sender As Object, e As EventArgs) Handles rbtnSave.Click
        Try
            If checkPassword() Then
                If UpdatePassword(txtNewPass.Text) Then
                    clsCommon.MyMessageBoxShow(Me, "Password updated successfully!", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "An error occurred while updating the password.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function ValidateOldPassword(oldPassword As String) As Boolean
        Dim currentPassword As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='AllowMilkReceiptAfterSettingsisOn' and TYPE='AllowMilkReceiptAfterSettingsisOn'")
        Return clsCommon.CompairString(oldPassword, currentPassword, True) = CompairStringResult.Equal
    End Function

    Private Function ValidateNewPassword(newPassword As String) As Boolean
        ' Regular expression to check for at least one uppercase letter, one number, and one specific special character
        Dim regex As New System.Text.RegularExpressions.Regex("^(?=.*[A-Z])(?=.*\d)(?=.*[@#$&*]).+$")

        ' Check if the password matches the regular expression pattern
        If Not regex.IsMatch(newPassword) Then
            Return False
        End If

        Return True
    End Function

    Private Function checkPassword() As Boolean
        ' Check if the password meets the minimum length requirement
        If clsCommon.myLen(txtOldPass.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Fill old password !", Me.Text)
            txtOldPass.Focus()
            Return False
        ElseIf Not ValidateOldPassword(txtOldPass.Text) Then
            clsCommon.MyMessageBoxShow(Me, "The old password is incorrect.", Me.Text)
            txtOldPass.Focus()
            Return False
        ElseIf clsCommon.myLen(txtNewPass.Text) < 6 OrElse clsCommon.myLen(txtNewPass.Text) > 12 Then
            clsCommon.MyMessageBoxShow(Me, "New Password length must be in 6 to 12 !", Me.Text)
            txtNewPass.Focus()
            Return False
        ElseIf Not ValidateNewPassword(txtNewPass.Text) Then
            clsCommon.MyMessageBoxShow(Me, "The new password must meet the complexity requirements (Atleast One Upper Case and Lower Case,Special character(@,#,$,&&,*) And Numeric).", Me.Text)
            txtNewPass.Focus()
            Return False
        ElseIf clsCommon.myLen(txtCNFPass.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Fill confirm password !", Me.Text)
            txtCNFPass.Focus()
            Return False
        ElseIf clsCommon.CompairString(txtNewPass.Text, txtCNFPass.Text, True) <> CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "The new password and confirm password do not match.", Me.Text)
            txtCNFPass.Focus()
            Return False
        End If
        Return True
    End Function


    Private Function UpdatePassword(newPassword As String) As Boolean
        Try
            Dim UpdatePass As String = "Update TSPL_FIXED_PARAMETER Set Description='" + clsCommon.myCstr(txtNewPass.Text) + "' Where Code='AllowMilkReceiptAfterSettingsisOn' and TYPE='AllowMilkReceiptAfterSettingsisOn' "
            clsDBFuncationality.ExecuteNonQuery(UpdatePass)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtOldPass.Text = Nothing
            txtNewPass.Text = Nothing
            txtCNFPass.Text = Nothing
            chkShowPass.Checked = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkShowPass_CheckStateChanged(sender As Object, e As EventArgs) Handles chkShowPass.CheckStateChanged
        Try
            ' Toggle password visibility based on CheckBox state
            If chkShowPass.Checked Then
                ' Show the password by setting PasswordChar to nothing
                txtOldPass.PasswordChar = ControlChars.NullChar
                txtNewPass.PasswordChar = ControlChars.NullChar
                txtCNFPass.PasswordChar = ControlChars.NullChar
            Else
                ' Hide the password by setting PasswordChar to an asterisk (*)
                txtOldPass.PasswordChar = "*"c
                txtNewPass.PasswordChar = "*"c
                txtCNFPass.PasswordChar = "*"c
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class