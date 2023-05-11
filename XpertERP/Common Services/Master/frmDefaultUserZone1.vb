Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class frmDefaultUserZone1
    'Inherits FrmMainTranScreen
    Dim strQ As String
    Dim Ds As DataSet
    Dim NotificatonLastCount As Integer = 0
    Private Sub frmDefaultUserZone1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'frmDefaultUserZone1.ControlBox = False
        LoadData()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(cboZone.Text) > 0 AndAlso clsCommon.CompairString(cboZone.Text, "Select") <> CompairStringResult.Equal Then
                Dim isSave As Boolean = clsDBFuncationality.ExecuteNonQuery("update tspl_user_master set Default_Zone_Code = '" + cboZone.Text + "' where user_code = '" + objCommonVar.CurrentUserCode + "' ")
            Else
                clsCommon.MyMessageBoxShow("Please Select Login Zone", Me.Text)
                Return
            End If
            Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadData()
        Try
            Dim dt As New DataTable()
            Dim zoneCount As Integer = clsDBFuncationality.getSingleValue("select count (Zone_Code) as Zone_Code from TSPL_USER_CUSTOMER_ZONE where user_code = '" + objCommonVar.CurrentUserCode + "'")
            Dim qry As String = "select Zone_Code  as Code from TSPL_USER_CUSTOMER_ZONE where user_code = '" + objCommonVar.CurrentUserCode + "'"
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(qry)
            cboZone.DataSource = dt
            cboZone.ValueMember = "Code"
            cboZone.DisplayMember = "Code"
            If zoneCount > 1 Then
                cboZone.Text = "Select"
            End If
        Catch ex As Exception

        End Try

    End Sub



    'Private Sub grdLoginInfo_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
    '    Dim frmNotificationDetail11 As New frmNotificationDetail(clsCommon.myCstr(grdLoginInfo.CurrentRow.Cells("Department").Value), clsCommon.myCstr(grdLoginInfo.CurrentRow.Cells("Tanker Type").Value))
    '    frmNotificationDetail11.ShowDialog()
    'End Sub

    'Private Sub Timer1_Tick(sender As Object, e As EventArgs)
    '    Dim PendingNotification As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) as Notification from TSPL_NOTIFICATION_DETAIL LEFT OUTER JOIN TSPL_NOTIFICATION_HEAD ON TSPL_NOTIFICATION_HEAD.Code = TSPL_NOTIFICATION_DETAIL.Code LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  =  TSPL_NOTIFICATION_DETAIL.User_Name   LEFT OUTER JOIN TSPL_USER_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_USER_MASTER.EmployeeCode where TSPL_NOTIFICATION_DETAIL.Sender_Replay=0 and TSPL_USER_MASTER.user_code='" + objCommonVar.CurrentUserCode + "'"))
    '    If PendingNotification <> NotificatonLastCount Then
    '        LoadData()
    '        Me.WindowState = FormWindowState.Normal
    '    End If
    'End Sub
End Class
