Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class frmPromptMsgNotification
    'Inherits FrmMainTranScreen
    Dim strQ As String
    Dim Ds As DataSet
    Dim NotificatonLastCount As Integer = 0
    Private Sub frmPromptMsgNotification_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Interval = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SetNotificationRefreshTimeInMinutes, clsFixedParameterCode.SetNotificationRefreshTimeInMinutes, Nothing)) * 60 * 1000
        Timer1.Start()
        LoadData()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub LoadData()

        Dim dt As New DataTable()
        Dim qry As String = "SELECT isnull(TSPL_GL_SEGMENT_CODE.Description,'Others') as Department,(case when  ISNULL(TSPL_NOTIFICATION_HEAD.Notification_Tanker_Doc_Type,'')='MccProc' then 'Plant/MCC' " &
         " when  ISNULL(TSPL_NOTIFICATION_HEAD.Notification_Tanker_Doc_Type,'')='BulkProc' then 'Contractor' ELSE '' END) as [Tanker Type],COUNT(*) AS No FROM TSPL_NOTIFICATION_DETAIL LEFT OUTER JOIN TSPL_NOTIFICATION_HEAD ON TSPL_NOTIFICATION_HEAD.Code = TSPL_NOTIFICATION_DETAIL.Code LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_NOTIFICATION_DETAIL.User_Name " &
         " Left OUTER JOIN TSPL_USER_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_USER_MASTER.EmployeeCode " &
         " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_Notification_HEAD.Notification_From_Department_Code and TSPL_GL_SEGMENT_CODE.Seg_No=3 " &
          " where TSPL_NOTIFICATION_DETAIL.Sender_Replay=0 and TSPL_USER_MASTER.user_code='" + objCommonVar.CurrentUserCode + "'" &
          " GROUP BY TSPL_GL_SEGMENT_CODE.Description ,TSPL_NOTIFICATION_HEAD.Notification_Tanker_Doc_Type order by TSPL_GL_SEGMENT_CODE.Description"
        dt = Nothing
        dt = clsDBFuncationality.GetDataTable(qry)
        grdLoginInfo.MasterTemplate.SummaryRowsBottom.Clear()
        grdLoginInfo.DataSource = Nothing
        grdLoginInfo.GroupDescriptors.Clear()
        grdLoginInfo.MasterView.Refresh()
        grdLoginInfo.AllowAddNewRow = False
        grdLoginInfo.ShowGroupPanel = False
        grdLoginInfo.AllowColumnReorder = False
        grdLoginInfo.AllowRowReorder = False
        'grdLoginInfo.EnableSorting = False
        'grdLoginInfo.BestFitColumns()
        grdLoginInfo.EnableFiltering = True
        grdLoginInfo.ShowFilteringRow = True

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        Else
            grdLoginInfo.DataSource = dt
            NotificatonLastCount = clsCommon.myCdbl(dt.Compute("sum(No)", ""))
            grdLoginInfo.Columns(0).Width = 120
            grdLoginInfo.Columns(1).Width = 100
            grdLoginInfo.Columns(2).Width = 53

            For ii As Integer = 0 To grdLoginInfo.Columns.Count - 1
                grdLoginInfo.Columns(ii).ReadOnly = True
                'grdLoginInfo.Columns(ii).BestFit()
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("No", "", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            grdLoginInfo.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            grdLoginInfo.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
    End Sub

    Private Sub grdLoginInfo_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles grdLoginInfo.CellDoubleClick
        Dim frmNotificationDetail11 As New frmNotificationDetail(clsCommon.myCstr(grdLoginInfo.CurrentRow.Cells("Department").Value), clsCommon.myCstr(grdLoginInfo.CurrentRow.Cells("Tanker Type").Value))
        frmNotificationDetail11.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim PendingNotification As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) as Notification from TSPL_NOTIFICATION_DETAIL LEFT OUTER JOIN TSPL_NOTIFICATION_HEAD ON TSPL_NOTIFICATION_HEAD.Code = TSPL_NOTIFICATION_DETAIL.Code LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  =  TSPL_NOTIFICATION_DETAIL.User_Name   LEFT OUTER JOIN TSPL_USER_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_USER_MASTER.EmployeeCode where TSPL_NOTIFICATION_DETAIL.Sender_Replay=0 and TSPL_USER_MASTER.user_code='" + objCommonVar.CurrentUserCode + "'"))
        If PendingNotification <> NotificatonLastCount Then
            LoadData()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub
End Class
