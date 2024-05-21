Imports common
Imports System.Windows.Forms

Public Class frmFreeTxtNumBox
    Public strRmks As String = ""
    Public NumValue As Integer
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOKPressed()
    End Sub

    Sub btnOKPressed()
        If clsCommon.myLen(txtRemarks.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Plese enter remarks", Me.Text)
            txtRemarks.Focus()
        Else
            strRmks = txtRemarks.Text
            NumValue = txtConversionRate.Value
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancePressed()
    End Sub

    Sub btnCancePressed()
        strRmks = ""
        Me.Close()
    End Sub

    Private Sub FrmFreeTxtBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancePressed()
        End If
    End Sub


    Private Sub FrmFreeTxtBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtRemarks.Text = strRmks
        txtConversionRate.Value = NumValue
    End Sub
End Class
