Imports common
Public Class frmMyDateTimePicker2
    Public RetValue As Date?
    Public isAcceptOKOnlyMandatory As Boolean = False
    Public LabelCaption As String
    Public strRmks As String
    'Public dtpDate As DateTime = Nothing

    'Public Property txtDate As Object

    Private Sub FrmFreeCombo1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadButton1.Visible = Not isAcceptOKOnlyMandatory
        If clsCommon.myLen(LabelCaption) > 0 Then
            RadLabel2.Text = LabelCaption
        End If
        If RetValue IsNot Nothing Then
            txtDate.Value = RetValue
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        RetValue = txtDate.Value
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class