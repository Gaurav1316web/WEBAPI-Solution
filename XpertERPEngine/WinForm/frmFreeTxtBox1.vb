Imports common
Imports System.Windows.Forms

Public Class FrmFreeTxtBox1
    Public strRmks As String = ""
    Public strLabelName As String = ""
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOKPressed()
    End Sub

    Sub btnOKPressed()
        If clsCommon.myLen(txtRemarks.Text) <= 0 Then
            If clsCommon.CompairString(strLabelName, "") <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Plese enter GSTIN No.", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "Plese enter remarks", Me.Text)
            End If
            txtRemarks.Focus()
        Else
            strRmks = txtRemarks.Text
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
        If clsCommon.CompairString(strLabelName, "") <> CompairStringResult.Equal Then
            lblRemark.Text = strLabelName
        End If
        txtRemarks.Text = strRmks
    End Sub
End Class
