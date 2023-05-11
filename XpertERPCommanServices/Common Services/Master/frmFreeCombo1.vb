Imports common
Public Class FrmFreeCombo1
    Public strFiscalYear As String = ""
    Private Sub FrmFreeCombo1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboFiscalYear.DataSource = FrmPrefixGenerationNew.GetFiscalYears()
        cboFiscalYear.ValueMember = "Code"
        cboFiscalYear.DisplayMember = "Name"

        cboFiscalYear.SelectedValue = clsCommon.myCstr(DateTime.Now.Year)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        strFiscalYear = clsCommon.myCstr(cboFiscalYear.SelectedValue)
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class
