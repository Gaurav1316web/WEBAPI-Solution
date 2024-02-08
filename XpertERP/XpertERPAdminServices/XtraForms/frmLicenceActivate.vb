Imports common
Imports System.Data.SqlClient
Public Class FrmLicenceActivate

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim Qry As String = "update TSPL_FIXED_PARAMETER set Description='" + txtProductKeyA.Text + "', Specification='" + txtProductKeyB.Text + "' where Type='" + clsFixedParameterType.LicenceExpiryDate + "' and Code='" + clsFixedParameterCode.LicenceExpiryDate + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "update TSPL_FIXED_PARAMETER set Description='" + txtProductKeyC.Text + "' where Type='" + clsFixedParameterType.LicenceNoOfExeConnection + "' and Code='" + clsFixedParameterCode.LicenceNoOfExeConnection + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "update TSPL_FIXED_PARAMETER set Description='" + txtProductKeyD.Text + "' where Type='" + clsFixedParameterType.LicenceNoOfJournalEntry + "' and Code='" + clsFixedParameterCode.LicenceNoOfJournalEntry + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "update TSPL_FIXED_PARAMETER set Description='" + txtProductKeyE.Text + "' where Type='" + clsFixedParameterType.LicenceNoOfUser + "' and Code='" + clsFixedParameterCode.LicenceNoOfUser + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Xpert ERP licence Activated successfully" + Environment.NewLine, Me.Text)
                objCommonVar.RefreshCommonVar()
                Me.Close()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Else
            clsCommon.MyMessageBoxShow(Me, "Not a valid product key" + Environment.NewLine, Me.Text)
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtProductKeyA.Text) <= 0 Then
            Return False
        End If
        If clsCommon.myLen(txtProductKeyB.Text) <= 0 Then
            Return False
        End If
        If clsCommon.myLen(txtProductKeyC.Text) <= 0 Then
            Return False
        End If
        If clsCommon.myLen(txtProductKeyD.Text) <= 0 Then
            Return False
        End If
        If clsCommon.myLen(txtProductKeyE.Text) <= 0 Then
            Return False
        End If
        Try
            Dim dt As Date = clsCommon.DecryptString(txtProductKeyA.Text, objCommonVar.CurrentCompanyCode + "A")
            Dim int As Integer = clsCommon.DecryptString(txtProductKeyB.Text, objCommonVar.CurrentCompanyCode + "B")
            int = clsCommon.DecryptString(txtProductKeyC.Text, objCommonVar.CurrentCompanyCode + "C")
            int = clsCommon.DecryptString(txtProductKeyD.Text, objCommonVar.CurrentCompanyCode + "D")
            int = clsCommon.DecryptString(txtProductKeyE.Text, objCommonVar.CurrentCompanyCode + "E")
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class
