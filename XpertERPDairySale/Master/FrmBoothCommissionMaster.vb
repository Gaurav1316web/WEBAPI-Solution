Public Class FrmBoothCommissionMaster
#Region "Variables"

#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmBoothCommissionMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()


    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        FrmClose()
    End Sub
    Private Sub FrmClose()
        Me.Close()
    End Sub
End Class