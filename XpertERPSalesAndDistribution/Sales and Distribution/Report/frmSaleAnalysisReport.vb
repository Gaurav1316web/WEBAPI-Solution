Imports common
Imports System.IO
Public Class frmSaleAnalysisReport
    Private Sub frmSaleAnalysisReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If rbtnDaily.Checked Then
                fromDate.Value = clsCommon.GETSERVERDATE()
                ToDate.Value = fromDate.Value
                ToDate.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Reset()
        Enable()
    End Sub
    Sub Enable()
        RadGroupBox1.Enabled = True
        RadGroupBox3.Enabled = True
        txtZone.Enabled = True
        TxtRoute.Enabled = True
        txtMultBooth.Enabled = True
        txtMultDistributor.Enabled = True
        txtItemCode.Enabled = True
    End Sub
    Sub Disable()
        RadGroupBox1.Enabled = False
        RadGroupBox3.Enabled = False
        txtZone.Enabled = False
        TxtRoute.Enabled = False
        txtMultBooth.Enabled = False
        txtMultDistributor.Enabled = False
        txtItemCode.Enabled = False
    End Sub

    Private Sub fromDate_Validated(sender As Object, e As EventArgs) Handles fromDate.Validated
        Try
            If rbtnDaily.Checked Then
                If clsCommon.myLen(fromDate.Value) > 0 Then
                    ToDate.Value = fromDate.Value
                    ToDate.ReadOnly = True
                End If
            ElseIf rbtnWeekly.Checked Then
                If clsCommon.myLen(fromDate.Value) > 0 Then
                    ToDate.Value = fromDate.Value.AddDays(7)
                    ToDate.ReadOnly = True
                End If
            Else
                If clsCommon.myLen(fromDate.Value) > 0 Then
                    fromDate.Value = clsCommon.GetPrintDate(fromDate.Value, "MM/yyyy")
                    ToDate.Value = clsCommon.GetPrintDate(fromDate.Value, "MM/yyyy")
                    ToDate.ReadOnly = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnMonthly_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnMonthly.CheckedChanged
        Try
            fromDate.CustomFormat = "MM/yyyy"
            ToDate.CustomFormat = "MM/yyyy"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnWeekly_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnWeekly.CheckedChanged
        Try
            fromDate.CustomFormat = "dd/MM/yyyy"
            ToDate.Value = fromDate.Value.AddDays(7)
            ToDate.CustomFormat = "dd/MM/yyyy"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnDaily_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDaily.CheckedChanged
        Try
            fromDate.CustomFormat = "dd/MM/yyyy"
            ToDate.Value = fromDate.Value
            ToDate.CustomFormat = "dd/MM/yyyy"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class