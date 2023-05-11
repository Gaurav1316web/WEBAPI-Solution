Imports common
Public Class FrmFreeComboBox
    Public ComboSource As DataTable
    Public ComboValueMember As String
    Public ComboDisplayMember As String
    Public strRetValue As String
    Public isAcceptOKOnlyMandatory As Boolean = False
    Public LabelCaption As String
    Private Sub FrmFreeCombo1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadButton1.Visible = Not isAcceptOKOnlyMandatory
        cboFiscalYear.DataSource = ComboSource
        cboFiscalYear.ValueMember = ComboValueMember
        cboFiscalYear.DisplayMember = ComboDisplayMember
        If clsCommon.myLen(LabelCaption) > 0 Then
            RadLabel2.Text = LabelCaption
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        strRetValue = clsCommon.myCstr(cboFiscalYear.SelectedValue)
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class
