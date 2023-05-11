Imports common
Imports System.Windows.Forms

Public Class frmVersion

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOKPressed()
    End Sub

    Sub btnOKPressed()
        Me.Close()
    End Sub

     

    Private Sub FrmFreeTxtBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        End If
    End Sub


    Private Sub FrmFreeTxtBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RichTextBox1.ReadOnly = True
        Dim strVersion As New System.Text.StringBuilder
        strVersion.Append("Version 1.0.0.3" + Environment.NewLine)
        strVersion.Append("Work no 1" + Environment.NewLine)
        strVersion.Append("Work no 2" + Environment.NewLine)
        strVersion.Append("Work no 3" + Environment.NewLine)
        strVersion.Append("Work no 4" + Environment.NewLine)
        strVersion.Append("Work no 5" + Environment.NewLine)
        strVersion.Append("Work no 6" + Environment.NewLine)
        strVersion.Append("Work no 7" + Environment.NewLine)
        strVersion.Append("Work no 8" + Environment.NewLine)
        strVersion.Append("Work no 9" + Environment.NewLine)
        strVersion.Append("Work no 10" + Environment.NewLine)
        strVersion.Append("Work no 11" + Environment.NewLine)

        strVersion.Append(Environment.NewLine + "Version 1.0.0.2" + Environment.NewLine)
        strVersion.Append("Work no 1" + Environment.NewLine)
        strVersion.Append("Work no 2" + Environment.NewLine)
        strVersion.Append("Work no 3" + Environment.NewLine)
        strVersion.Append("Work no 4" + Environment.NewLine)
        strVersion.Append("Work no 5" + Environment.NewLine)
        strVersion.Append("Work no 6" + Environment.NewLine)
        strVersion.Append("Work no 7" + Environment.NewLine)
        strVersion.Append("Work no 8" + Environment.NewLine)
        strVersion.Append("Work no 9" + Environment.NewLine)
        strVersion.Append("Work no 10" + Environment.NewLine)
        strVersion.Append("Work no 11" + Environment.NewLine)

        strVersion.Append(Environment.NewLine + "Version 1.0.0.1" + Environment.NewLine)
        strVersion.Append("Work no 1" + Environment.NewLine)
        strVersion.Append("Work no 2" + Environment.NewLine)
        strVersion.Append("Work no 3" + Environment.NewLine)
        strVersion.Append("Work no 4" + Environment.NewLine)
        strVersion.Append("Work no 5" + Environment.NewLine)
        strVersion.Append("Work no 6" + Environment.NewLine)
        strVersion.Append("Work no 7" + Environment.NewLine)
        strVersion.Append("Work no 8" + Environment.NewLine)
        strVersion.Append("Work no 9" + Environment.NewLine)
        strVersion.Append("Work no 10" + Environment.NewLine)
        strVersion.Append("Work no 11" + Environment.NewLine)
        RichTextBox1.Text = strVersion.ToString()

        'Dim sb = New System.Text.StringBuilder()
        'sb.Append("{\rtf1\ansi")
        'sb.Append("This number is bold: \b 123\b0 \n ! Yes, it is...")
        'sb.Append("}")
        'RichTextBox1.Rtf = sb.ToString()

        'RichTextBox1.Rtf = "{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Calibri;}} hello my name is \i Bob\i0 \par}"
        'Dim strVersion As New System.Text.StringBuilder

        'strVersion.Append("{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Calibri;}}")
        'strVersion.Append("{\*\generator Riched20 10.0.16299}\viewkind4\uc1 ")
        'strVersion.Append("\pard\sa200\sl276\slmult1\f0\fs22\lang9 1.0.0.1\par")
        'strVersion.Append("1)Work on milk srn \par")
        'strVersion.Append("2)Work on Sample scrren\par")
        'strVersion.Append("3)Work on Shift end screddn.\par")
        'strVersion.Append("}")
        'RichTextBox1.Rtf = strVersion.ToString()

    End Sub
End Class
