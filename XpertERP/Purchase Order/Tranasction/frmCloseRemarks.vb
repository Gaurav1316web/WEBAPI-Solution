Imports common
Imports System.Windows.Forms

Public Class frmCloseRemarks
    Public strRmks As String = ""
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOKPressed()
    End Sub

    Sub btnOKPressed()
        'If clsCommon.myLen(txtRemarks.Text) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Plese enter remarks", Me.Text)
        '    txtRemarks.Focus()
        'Else
        strRmks = clsCommon.myCstr(txtRemarks.Text).Trim()
        Me.Close()
        'End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancePressed()
    End Sub

    Sub btnCancePressed()
        strRmks = ""
        Me.Close()
    End Sub

    Private Sub frmCloseRemarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancePressed()
        End If
    End Sub


    Private Sub frmCloseRemarks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtRemarks.Text = strRmks
        btnCancel.Visible = False
    End Sub
End Class
