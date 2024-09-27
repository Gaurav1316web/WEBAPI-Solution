Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmDailyDemand
    Inherits FrmMainTranScreen
    Private Sub frmDailyDemand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LoadUnion()
    End Sub

    'Private Sub txtUnion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim whlsc As String = Nothing
    '    Dim Qry As String = "SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] "
    '    whlsc = " DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') "
    '    txtUnion.Value = clsCommon.ShowSelectForm("fndUnion", Qry, "code", whlsc, txtUnion.Value, "code", isButtonClicked)

    'End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub
End Class