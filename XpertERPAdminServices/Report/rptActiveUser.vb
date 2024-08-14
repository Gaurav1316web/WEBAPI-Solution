Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' By balwinder on 18/04/2019
Public Class rptActiveUser
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtPurchasedLicence.Value = clsCommon.myCdbl(clsCommon.DecryptString(clsFixedParameter.GetData(clsFixedParameterType.LicenceNoOfExeConnection, clsFixedParameterCode.LicenceNoOfExeConnection, Nothing), objCommonVar.CurrentCompanyCode + "C"))
        If txtPurchasedLicence.Value <= 0 Then
            Panel1.Visible = False
        Else
            Dim qry As String = "select TSPL_DOCK_MASTER.Code,TSPL_MCC_MASTER.MCC_Code" + Environment.NewLine + _
               "from TSPL_MCC_MASTER" + Environment.NewLine + _
               "left outer join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.MCC_Code=TSPL_MCC_MASTER.MCC_Code" + Environment.NewLine + _
               "where TSPL_MCC_MASTER.In_active = 0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtReserveForMCC.Value = dt.Rows.Count * 2
            txtPurchasedLicence.Value += txtReserveForMCC.Value
        End If
        'LoadData()
        GridData(False, "")
    End Sub

    Sub Reset()

        txtActiveUser.Value = 0
        txtRemainingUser.Value = 0

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
    End Sub

    Sub LoadData()
        Try
            Reset()

            Dim qry As String = clsLoginInfo.funGetActiveUserQuery(clsCommon.CompairString(clsCommon.myCstr(MyBase.Tag), "$#$#") = CompairStringResult.Equal)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            dt = clsMilkUnion.UnionDBName()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                Gv1.BestFitColumns()
                Gv1.AutoSizeRows = True
                Gv1.EnableFiltering = True

                txtActiveUser.Value = dt.Rows.Count
                txtRemainingUser.Value = txtPurchasedLicence.Value - txtReserveForMCC.Value - txtActiveUser.Value
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GridData(ByVal isNotConsiderCurrentSPID As Boolean, ByVal strCurrentUserCode As String)
        Try
            Reset()
            Dim FinalQry As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            dt = clsMilkUnion.UnionDBName()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    qry += " Select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],count(Login_Code) as [Login Count],max(Connection_SP_ID) as [SP ID],max(replace(Window_User_Name,Machine_Name+'\','')) as [Window User Name],max(IP_Address) as [IP Address],max(MAC_Address) as [MAC Address],max(Machine_Name) as [Machine Name],max(User_Code) as [ERP User Code],max(Login_DateTime) as [Login At] from ( 
                    select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_USERLOGIN_INFO. *, (ROW_NUMBER() over (Partition by connection_sp_id,machine_name order by Login_DateTime desc))as SNo     " + Environment.NewLine +
            " from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_USERLOGIN_INFO " + Environment.NewLine +
            "inner join(SELECT 	spid,hostname FROM sys.sysprocesses WHERE  dbid > 0   and DB_NAME(dbid) in (select DataBase_Name from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "') and sys.sysprocesses.program_name='.Net SqlClient Data Provider') ActiveConn on ActiveConn.spid=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_USERLOGIN_INFO.Connection_SP_ID  and ActiveConn.hostname=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_USERLOGIN_INFO.Machine_Name COLLATE SQL_Latin1_General_CP1_CI_AS" + Environment.NewLine +
            ")xx where SNo=1 and User_Code not in ('XpertSMSApp','XpertSyncApp','XpertAlertApp','XpertBioMetric','XpertBookingSchedularApp','XpertDispatchSchedularApp') and DATEDIFF(hour,Login_DateTime,GETDATE())<24"
                    If isNotConsiderCurrentSPID Then
                        qry += " and Connection_SP_ID not in (select @@SPID)"
                    End If
                    If clsCommon.myLen(strCurrentUserCode) > 0 Then
                        qry += " and User_Code ='" + strCurrentUserCode + "'"
                    End If
                    If clsCommon.myLen(strCurrentUserCode) <= 0 AndAlso (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SameuserCanNotloginmultipletimes, clsFixedParameterCode.SameuserCanNotloginmultipletimes, Nothing)) = 1) Then ''ERO/14/11/19-001100 by balwinder on 19/11/2019
                        FinalQry = "select * from (" + qry + Environment.NewLine + " union " + Environment.NewLine + " select xxx.SNo, case when xxx.[Login Code] is null then 'Reserved Licence' else xxx.[Login Code] end as  [Login Code],xxx.[SP ID],xxx.[Window User Name] , xxx.[IP Address],xxx.[MAC Address],xxx.[Machine Name],TSPL_USER_MASTER.User_Code as [ERP User Code],xxx.[Login At]" + Environment.NewLine +
                " from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_USER_MASTER" + Environment.NewLine +
                " left outer join  (" + qry + ")xxx on xxx.[ERP User Code]=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_USER_MASTER.User_Code where Licence_Reserved=1)xxxxx"
                    Else
                        FinalQry = qry
                    End If
                    'FinalQry += " order by [Login At] "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(FinalQry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                If dt2 Is Nothing OrElse dt2.Rows.Count > 0 Then
                    Gv1.DataSource = dt2
                    For ii As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).ReadOnly = True
                    Next
                    Gv1.BestFitColumns()
                    Gv1.AutoSizeRows = True
                    Gv1.EnableFiltering = True
                    txtActiveUser.Value = dt.Rows.Count
                    txtRemainingUser.Value = txtPurchasedLicence.Value - txtReserveForMCC.Value - txtActiveUser.Value
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GridData(False, "")
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Active User", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Active User", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class
