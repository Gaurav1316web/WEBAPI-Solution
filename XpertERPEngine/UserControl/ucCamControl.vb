Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.IO.File
Imports System.Drawing
Public Class ucCamControl
    Private Sub btnTakePic_Click(sender As Object, e As EventArgs) Handles btnTakePic.Click
        Dim frm As New frmTakePicFromCamera
        frm.MaximizeBox = False
        frm.MinimizeBox = False
        frm.ControlBox = False
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
        If frm.isSavedPic Then
            PicBox.Image = frm.picCapture.Image
            PicBox.Tag = frm.PicPath
        Else

            PicBox.Image = Nothing
        End If
        frm.Dispose()
    End Sub

    Private Sub btnPickSavedPic_Click(sender As Object, e As EventArgs) Handles btnPickSavedPic.Click
        Dim Ofd As New OpenFileDialog
        If Ofd.ShowDialog = DialogResult.OK Then
            Dim path As String = Ofd.FileName
            PicBox.Load(path)
            PicBox.Tag = path
        End If
    End Sub

    Private Sub ucCamControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
