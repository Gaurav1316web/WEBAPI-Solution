
Public Class PB
    Dim _Value As Integer = 0

    <STAThread()> _
        Public Sub UpdateProgressBarValue(ByVal value As Integer)
        Try
            _Value = value
            Pbar.Invoke(New EventHandler(AddressOf DoDisplayProgressBarValue))
            If value >= 100 Then
                ProgressBarHide()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DoDisplayProgressBarValue(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Pbar.Value1 = _Value
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ProgressBarShow()
        UpdateProgressBarValue(1)
        Me.Show()
    End Sub
    Sub ProgressBarHide()
        UpdateProgressBarValue(1)
        Me.Hide()
    End Sub
End Class
