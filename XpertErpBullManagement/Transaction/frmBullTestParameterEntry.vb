Imports common
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Public Class frmBullTestParameterEntry
#Region "Variables"

#End Region
    Private Sub frmBullTestParameterEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBullPrmtrGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullPrmtrGroup._MYValidating
        Try
            Dim Qry As String = "select Code,Name from TSPL_BULL_PARAMETER_GROUP_MASTER"
            fndBullPrmtrGroup.Value = clsCommon.ShowSelectForm("PARAMETER_GROUP_MASTER", Qry, "Code", "", fndBullPrmtrGroup.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDisease__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDisease._MYValidating
        Try
            Dim Qry As String = "select Code,Name from TSPL_Disease_Master"
            fndDisease.Value = clsCommon.ShowSelectForm("Disease_Master", Qry, "Code", "", fndDisease.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBullID__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullID._MYValidating
        Try
            Dim Qry As String = "Select Bull_Code As [Code],	Species_Code As [Species Code],	Category_Code As [Category Code],	Sub_Category_Code As [Sub Category Code]	from TSPL_BULL_MASTER"
            fndBullID.Value = clsCommon.ShowSelectForm("Disease_Master", Qry, "Code", "", fndBullID.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class