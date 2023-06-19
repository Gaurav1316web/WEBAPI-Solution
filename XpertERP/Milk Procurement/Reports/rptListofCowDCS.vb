Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPHRandPayroll
Public Class rptListofCowDCS
    Private Sub rptListofCowDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtMCCOwnBMC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCCOwnBMC._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code  from TSPL_MCC_MASTER "
            txtMCCOwnBMC.Value = clsCommon.ShowSelectForm("MCCFND@VLCVSPM", qry, "Code", "", txtMCCOwnBMC.Value, "Code", isButtonClicked)
            lblMCCOwnBMC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + txtMCCOwnBMC.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtMCCOwnBMC.Value = ""
            lblMCCOwnBMC.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
End Class