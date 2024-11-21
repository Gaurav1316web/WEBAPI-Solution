Imports common
Imports System.IO
Public Class rptTankerGainLossReport
    Inherits FrmMainTranScreen

    Private Sub rptTankerGainLossReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        ' Griddata(False, False)
    End Sub
End Class