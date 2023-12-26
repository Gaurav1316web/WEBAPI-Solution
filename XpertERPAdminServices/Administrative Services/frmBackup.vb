Imports common
Imports System.Data.SqlClient

Public Class FrmBackup
    Inherits FrmMainTranScreen
    Private Sub FrmBackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadServerPath()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnBackup.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub LoadServerPath()
        Dim qry As String = "Select * from TSPL_SERVER_PATH"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtServerPath.Text = clsCommon.myCstr(dt.Rows(0)("SERVER_PATH"))
        End If
    End Sub

   
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        Try
            If clsCommon.myLen(txtServerPath.Text) <= 0 Then
                txtServerPath.Focus()
                Throw New Exception("Please Enter Server Path")
            End If

            clsCommon.ProgressBarShow()

            If TakeBakup() Then
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Backup taken Sucessfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function TakeBakup() As Boolean ''TEC/18/06/19-000543 by balwinder on 24/06/2019
        Dim qry As String = ""
        Dim strdt As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "yyyy_MM_dd_hh_mm_tt")
        If clsCommon.myLen(objCommonVar.CurrDatabase) > 0 Then
            Dim strFileName As String = txtServerPath.Text.Trim() + "\" + clsCommon.myCstr(objCommonVar.CurrDatabase) + "_" + strdt + ".bak "
            Try
                clsCommon.ProgressBarUpdate("Taking Backup...")
                qry = "BACKUP DATABASE " + clsCommon.myCstr(objCommonVar.CurrDatabase) + " TO DISK = '" + strFileName + "'  "
                clsDBFuncationality.ExecuteNonQuery(qry)
            Catch ex As Exception
                Throw New Exception("Invalid Server Path for BackUp")
                Return False
            End Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Backup_Code", strFileName)
            clsCommon.AddColumnsForChange(coll, "Backup_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Backup", OMInsertOrUpdate.Insert, "", Nothing)
        End If

        qry = "delete from TSPL_SERVER_PATH"
        clsDBFuncationality.ExecuteNonQuery(qry)

        Dim coll1 As New Hashtable()
        clsCommon.AddColumnsForChange(coll1, "Server_Path", txtServerPath.Text.Trim())
        Dim result As Boolean = clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_SERVER_PATH", OMInsertOrUpdate.Insert, "")

        Return True
    End Function

End Class

