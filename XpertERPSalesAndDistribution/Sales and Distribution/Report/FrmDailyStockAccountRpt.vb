Imports common
Imports XpertERPEngine

Public Class FrmDailyStockAccountRpt
    Inherits FrmMainTranScreen
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click

    End Sub






    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("DAL-STK-AC")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub FrmDailyStockAccountRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()

    End Sub
End Class
