Imports common
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Public Class FrmChangePassword
    Public password As String
    Public SSO As String
    Public CheckPassword As Boolean = Nothing
    Public PasswordRules As Boolean = Nothing
    Dim ButtonTooltip As ToolTip = New ToolTip()

    Private Sub FrmChangePassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblusercode.Text = objCommonVar.CurrentUserCode
        lblusername.Text = objCommonVar.CurrentUser
        Dim qry As String = "select password,SSO from TSPL_USER_MASTER where user_code='" + objCommonVar.CurrentUserCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            password = clsCommon.myCstr(dt.Rows(0)("password"))
            SSO = clsCommon.myCstr(dt.Rows(0)("SSO"))
        Else
            clsCommon.MyMessageBoxShow("Invalid user [" + objCommonVar.CurrentUserCode + "]")
            Me.Close()
        End If
        ButtonTooltip.SetToolTip(btnsave, "Press Alt+S for Save")
        ButtonTooltip.SetToolTip(btnclose, "Press Alt+C for Close")
        PasswordRules = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PasswordRules, clsFixedParameterCode.PasswordRules, Nothing)) = "1", True, False))
        If PasswordRules = True Then
            lblLength.Visible = True
            btnclose.Visible = False
        End If
    End Sub



    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Savedata()
    End Sub
    Sub Savedata()
        If allowtosave() = True Then
            Try
                If SavePswd() = True Then

                    clsCommon.MyMessageBoxShow(Me, "Password Changed Successfully", Me.Text)
                    Close()
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

    End Sub

    Public Function SavePswd()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(SSO) > 0 Then
                Dim result As Integer = moduleChangeWindowsUserPwd.NetUserChangePassword(Nothing, SSO, txtcurpassword.Text, txtnewpassword.Text)
                If result <> 0 Then
                    If result = 86 Then
                        Throw New Exception("Current password is wrong")
                    Else
                        Throw New Exception("Failed to change password. Error code: " & result)
                    End If

                End If
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Password", clsCommon.EncryptString(txtnewpassword.Text))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Update, "User_code='" + objCommonVar.CurrentUserCode + "'", trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, objCommonVar.CurrentUserCode, "TSPL_USER_MASTER", "User_Code", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Function ValidatePassword(ByVal pwd As String, Optional ByVal minLength As Integer = 8, Optional ByVal numUpper As Integer = 2, Optional ByVal numLower As Integer = 2, Optional ByVal numNumbers As Integer = 2, Optional ByVal numSpecial As Integer = 2) As Boolean
        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
        ' Special is "none of the above".
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")
        CheckPassword = True
        ' Check the length.
        If Len(pwd) < minLength Then
            CheckPassword = False
            Return False
        End If
        ' Check for minimum number of occurrences.
        If upper.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If

        If lower.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If

        If number.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If
        If special.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If

        ' Passed all checks.
        Return True
    End Function
    Public Function allowtosave() As Boolean
        Try

            If (clsCommon.myLen(txtnewpassword.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Please  fill  New Password", Me.Text)
                txtnewpassword.Text = ""
                txtnewpassword.Focus()
                Return False
            ElseIf (clsCommon.myLen(txtconfpassword.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, " Please  fill  Confirm Password", Me.Text)
                txtconfpassword.Text = ""
                txtconfpassword.Focus()
                Return False
            ElseIf (clsCommon.myLen(txtcurpassword.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, " Please  fill Current Password", Me.Text)
                txtcurpassword.Text = ""
                txtcurpassword.Focus()
                Return False

            End If
            If clsCommon.CompairString(txtnewpassword.Text, txtcurpassword.Text, True) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "New Password should be Different from current password", Me.Text)
                txtconfpassword.Text = ""
                txtnewpassword.Text = ""
                txtnewpassword.Focus()
                Return False
            End If

            Dim qry As String = "select top 3 PWD,(select  max(TSPL_USER_MASTER_Hist_Data.Hist_Version) from TSPL_USER_MASTER_Hist_Data where TSPL_USER_MASTER_Hist_Data.Password=xx.PWD ) as Hist_Version  from (
select distinct Password as PWD from TSPL_USER_MASTER_Hist_Data where User_Code='" + lblusercode.Text + "' 
)xx order by Hist_Version desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(clsCommon.EncryptString(txtnewpassword.Text), clsCommon.myCstr(dr("PWD")), True) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "New Password should be Different from Last 3 Passwords", Me.Text)
                        txtconfpassword.Text = ""
                        txtnewpassword.Text = ""
                        txtnewpassword.Focus()
                        Return False
                    End If
                Next
            End If

            If Not clsCommon.CompairString(txtnewpassword.Text, txtconfpassword.Text, True) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Confirm Password should  be same as new password", Me.Text)
                txtconfpassword.Text = ""
                txtnewpassword.Text = ""
                txtnewpassword.Focus()
                Return False
            End If
            If clsCommon.myLen(SSO) <= 0 Then
                If Not clsCommon.CompairString(clsCommon.EncryptString(txtcurpassword.Text), password, True) = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Incorrect current password", Me.Text)
                    txtcurpassword.Text = ""
                    txtcurpassword.Focus()
                    Return False
                End If
            End If

            'If (clsCommon.myLen(txtconfpassword.Text) < 6) Then
            '    clsCommon.MyMessageBoxShow("Password length should be greater than 6 letters")
            '    txtconfpassword.Text = ""
            '    txtnewpassword.Text = ""
            '    txtnewpassword.Focus()
            '    Return False
            'End If

            If PasswordRules = True Then
                ValidatePassword(txtnewpassword.Text, 8, 2, 2, 2, 2)
                If CheckPassword = False Then
                    clsCommon.MyMessageBoxShow(Me, "Password is invalid. Format not match", Me.Text)
                    Return False
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function


    Private Sub FrmChangePassword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Savedata()
        ElseIf e.KeyCode = Keys.Enter AndAlso clsCommon.myLen(txtcurpassword.Text) > 0 AndAlso clsCommon.myLen(txtnewpassword.Text) > 0 AndAlso clsCommon.myLen(txtconfpassword.Text) Then
            Savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub



    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    ''Not working on DOIT Server but work on others
    'Function MyChangeWindowsPassowrd(ByVal username As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
    '    Try
    '        ' Use the local machine or domain
    '        Using context As New PrincipalContext(ContextType.Machine) ' Use ContextType.Domain for domain users
    '            ' Validate old password
    '            If context.ValidateCredentials(username, oldPassword) Then
    '                ' Change password
    '                Dim user As UserPrincipal = UserPrincipal.FindByIdentity(context, username)
    '                If user IsNot Nothing Then
    '                    user.ChangePassword(oldPassword, newPassword)
    '                    user.Save()
    '                Else
    '                    Throw New Exception("User not found!")
    '                End If
    '            Else
    '                Throw New Exception("Invalid old password!")
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        If ex.Message.Contains("Access is denied") Then
    '            Return True
    '        Else
    '            Throw New Exception("Error: " & ex.Message)
    '        End If
    '    End Try
    '    Return True
    'End Function

End Class

Module moduleChangeWindowsUserPwd
    <DllImport("Netapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function NetUserChangePassword(
        ByVal domain As String,
        ByVal username As String,
        ByVal oldPassword As String,
        ByVal newPassword As String) As Integer
    End Function

    'Sub Main()
    '    Dim domain As String = Nothing ' Use Nothing for local machine, or specify domain name
    '    Dim username As String = "guest123"
    '    Dim oldPassword As String = "guest"
    '    Dim newPassword As String = "balwinder"

    '    Dim result As Integer = NetUserChangePassword(domain, username, oldPassword, newPassword)

    '    If result = 0 Then
    '        clsCommon.MyMessageBoxShow(Me, "Password changed successfully.", Me.Text)()
    '    Else
    '        clsCommon.MyMessageBoxShow(Me, "Failed to change password. Error code: " & result, Me.Text)
    '    End If
    'End Sub
End Module
