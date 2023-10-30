Imports common

Public Class FrmPopup1
    Public Remarks As String
    


    Private Sub FrmPopup1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtRemarks.Text = Remarks
        
    End Sub

    Private Sub btnRemindLater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemindLater.Click

        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
