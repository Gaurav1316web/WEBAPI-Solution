Imports common
Imports Telerik.Charting
Public Class rptDBTDashboard
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dtDBTSummary As DataTable = Nothing
    Dim dtMismatchqty As DataTable = Nothing
    Dim dtPaymentStatus As DataTable = Nothing
    Dim dtJanAdh As DataTable = Nothing
    Const ReportID As String = "DBTNEFTPaymentDetailReport"

#End Region
    Private Sub rptDBTDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ''ButtonToolTip.SetToolTip(RadButton1, "Press Alt+R Refresh ")
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        dtDBTSummary = Nothing
        gvDBTSummary.DataSource = Nothing
        gvDBTSummary.Rows.Clear()
        gvDBTSummary.Columns.Clear()

        dtMismatchqty = Nothing
        gvMismatchqty.DataSource = Nothing
        gvMismatchqty.Rows.Clear()
        gvMismatchqty.Columns.Clear()

        dtPaymentStatus = Nothing
        gvPaymentStatus.DataSource = Nothing
        gvPaymentStatus.Rows.Clear()

        dtJanAdh = Nothing
        gvJanAdh.DataSource = Nothing
        gvJanAdh.Rows.Clear()
        gvJanAdh.Columns.Clear()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadReport_DBT_JanAdh()
        LoadReport_DBT_Mishmatch_Qty()
        LoadReport_PaymentStatus()
        LoadReport_DBTSummary()
    End Sub

    Public Sub LoadReport_DBT_JanAdh()
        Try
            Dim query As String
            gvJanAdh.DataSource = Nothing

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                gvJanAdh.DataSource = Nothing
            End If

            Dim docNo As String = ""
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    Dim qry As String = "SELECT Top 1 Document_Code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT where 2=2 "
                    qry += " ORDER BY Document_date DESC"
                    docNo = clsDBFuncationality.getSingleValue(qry)

                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],COUNT([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code) AS [No Of Farmer],
    SUM(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.Jan_Aadhar_No_Verified = 1 THEN 1 ELSE 0 END) AS [Jan Aadhar Verified No],
    SUM(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.Aadhar_No_Verified = 1 THEN 1 ELSE 0 END) AS [Addhar Verified],
    CONVERT(varchar, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, 103) + ' - ' + CONVERT(varchar, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date, 103) AS [Last DBT Cycle]
    from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code= '" & docNo & "' group by [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date"

                Next
                dtJanAdh = clsDBFuncationality.GetDataTable(query)
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                    gvJanAdh.DataSource = Nothing
                    gvJanAdh.Rows.Clear()
                    gvJanAdh.Columns.Clear()
                    gvJanAdh.GroupDescriptors.Clear()
                    gvJanAdh.MasterTemplate.SummaryRowsBottom.Clear()
                    gvJanAdh.MasterView.Refresh()
                    gvJanAdh.DataSource = dt2
                    For ii As Integer = 0 To gvJanAdh.Columns.Count - 1
                        gvJanAdh.Columns(ii).ReadOnly = True
                    Next
                    RadPageView.SelectedPage = RadPageViewPage2
                    gvJanAdh.EnableFiltering = True
                    gvJanAdh.AllowAddNewRow = False
                    gvJanAdh.ShowGroupPanel = False
                    'SetGridFormat()

                    gvJanAdh.BestFitColumns()
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub LoadReport_DBT_Mishmatch_Qty()
        Try
            Dim query As String
            gvMismatchqty.DataSource = Nothing

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                gvMismatchqty.DataSource = Nothing
            End If
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                query += "select ROW_NUMBER() over(order by ([Union Name])) as 'SNO.',  [Union Name] ,sum([Farmer count])[Farmer count] , sum([DCS Billed Qty])[DCS Billed Qty], sum([Farmer Qty])[Farmer Qty], sum([Mismatch Qty])[Mismatch Qty]  from ("
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    query += "select * from ( select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],isnull(sum(INCENTIVE.MPCount),0) as [Farmer count],isnull(sum(INCENTIVE.RecoQty),0) as [DCS Billed Qty],isnull(sum(INCENTIVE.Qty),0) as [Farmer Qty] ,isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0) as [Mismatch Qty]
                      from (select yy.Cycle_Year,yy.Cycle_Month ,isnull(sum(yy.Qty),0) as RecoQty ,0 as MPCount,0 as Qty
                      from (select Cycle_Year,Cycle_Month, TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Qty from (select * from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL union all select * from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID)TSPL_DCS_MP_INCENTIVE_RECO_DETAIL LEFT OUTER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code  where CONVERT(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' )yy group by yy.Cycle_Year,yy.Cycle_Month  union all select xx.Cycle_Year,xx.Cycle_Month
                    ,0 as RecoQty
                    ,isnull(count(distinct xx.MP_Code),0) as MPCount
                    ,isnull(sum(xx.Qty),0) as Qty from (select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Year,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Month
                    ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty
                    ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL 
                    left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
                    where CONVERT(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.To_Date,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' )xx group by xx.Cycle_Year,xx.Cycle_Month )INCENTIVE 
                    group by INCENTIVE.Cycle_Year,INCENTIVE.Cycle_Month) final"


                Next
                query += ") xxfinal group by [Union Name]"
                dtMismatchqty = clsDBFuncationality.GetDataTable(query)
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                    gvMismatchqty.DataSource = Nothing
                    gvMismatchqty.Rows.Clear()
                    gvMismatchqty.Columns.Clear()
                    gvMismatchqty.GroupDescriptors.Clear()
                    gvMismatchqty.MasterTemplate.SummaryRowsBottom.Clear()
                    gvMismatchqty.MasterView.Refresh()
                    gvMismatchqty.DataSource = dt2
                    For ii As Integer = 0 To gvMismatchqty.Columns.Count - 1
                        gvMismatchqty.Columns(ii).ReadOnly = True
                    Next
                    RadPageView.SelectedPage = RadPageViewPage2
                    gvMismatchqty.EnableFiltering = True
                    gvMismatchqty.AllowAddNewRow = False
                    gvMismatchqty.ShowGroupPanel = False
                    'SetGridFormat()

                    gvMismatchqty.BestFitColumns()
                Else
                    ''  clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub LoadReport_PaymentStatus()
        Try
            Dim qry As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gvPaymentStatus.DataSource = Nothing
                Exit Sub
            End If
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                qry = "select ROW_NUMBER() over(order by ([Union Name])) as 'SNO.', [Union Name]  , count(MP_Code)as [Farmer Count] ,sum(Amount)[NEFT Amount] ,sum(Success_Farmer)Success_Farmer, SUM(Success_Amount)Success_Amount,sum(Failure_Farmer)Failure_Farmer , SUM(Failure_Amount)Failure_Amount,sum(Null_Farmer_Count)Null_Farmer_Count,  sum(Null_Farmer_Amount)Null_Farmer_Amount from ("
                qry += " select ROW_NUMBER() over(order by ([Union Name])) as 'SNO.',* from ( "
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    qry += " select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code AS MP_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader AS MP_Code_VLC_Uploader , [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Name AS MP_Name
  ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Account_No
               AS MP_Account_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_IFSC_No AS MP_IFSC_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response AS Status, isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount,0) AS Amount,
               case when [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response = 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA' then 1 else 0  end as Success_Farmer, case when 
  ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response  <> 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response IS NOT NULL) then 1  else 0 end as Failure_Farmer,
                case when [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response = 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA' then [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount else 0  end as Success_Amount, case when 
  ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response  <> 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response IS NOT NULL)  then [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount else 0 end as Failure_Amount,
  case when [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response is null then 1 else 0 end as Null_Farmer_Count, case when [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response is null then [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount   else 0 end as Null_Farmer_Amount
    from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code 
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.PK_Id 
                 WHERE ISNULL( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.RCDF_Status,0)=1 and  Convert(Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'01/Feb/2024',103) And 
    Convert(Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'29/Feb/2024',103) "


                Next
                qry += ") xx  ) xxx group by [Union Name]"
            End If
            dtPaymentStatus = clsDBFuncationality.GetDataTable(qry)
            gvPaymentStatus.DataSource = Nothing
            gvPaymentStatus.Rows.Clear()
            gvPaymentStatus.Columns.Clear()
            gvPaymentStatus.GroupDescriptors.Clear()
            gvPaymentStatus.MasterView.Refresh()
            gvPaymentStatus.GroupDescriptors.Clear()
            gvPaymentStatus.EnableFiltering = True
            gvPaymentStatus.MasterTemplate.SummaryRowsBottom.Clear()
            If dtPaymentStatus.Rows.Count > 0 Then
                gvPaymentStatus.DataSource = dt
                gvPaymentStatus.BestFitColumns()
                SetGridFormation()
                ReStoreGridLayout()
                gvPaymentStatus.MasterTemplate.AutoExpandGroups = True
                RadPageView.SelectedPage = RadPageViewPage2
                gvPaymentStatus.BestFitColumns()
            Else
                '' clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadReport_DBTSummary()
        Try
            Dim query As String
            gvDBTSummary.DataSource = Nothing

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                gvDBTSummary.DataSource = Nothing
            End If

            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name], SUM(DISTINCT [DCS Count]) AS [DCS Count] ,SUM(DISTINCT([Farmer Count]))[Farmer Count], IsNull(Sum(final.[DCS Billed Qty]),0)[DCS Billed Qty],IsNull(Sum(final.[Farmer Qty]),0)[Farmer Qty],IsNull(Sum(final.[NEFT Amt]),0)[NEFT Amt] from ( select COUNT(Distinct [DCS Count])  as [DCS Count], SUM(Distinct [Farmer Count])  as [Farmer Count],0 As [DCS Billed Qty],Sum([Farmer Qty])[Farmer Qty],sum(Amount) as [NEFT Amt]  From ( Select sum(TSPL_DBT_NEFT_DETAIL.Amount) as Amount ,sum(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty)[Farmer Qty], COUNT(ISNULL (TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code,0))  as [Farmer Count],(TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code) as [DCS Count] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL  Left Outer JOin [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT On TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
                                Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR   
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code where TSPL_DBT_NEFT_DETAIL.PK_Id Not In (Select Against_DBT_NEFT_TR From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT_DETAIL where TSPL_DBT_NEFT_REJECT_DETAIL.Document_Code Not In (Select Document_Code From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT where TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT Not In (TSPL_DBT_NEFT.Document_Code))) and Convert(Date,TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) And Convert(Date,TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103) GROUP BY [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code	
                                ) x Union All 
                                select  0 as [DCS Count] ,0 As [Farmer Count], Sum([DCS Billed Qty])[DCS Billed Qty], 0 As [FarmerQty],0 As [NEFT Amt] from
                                (select ([DCS Billed Qty])[DCS Billed Qty] from (
								Select TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.[DCS Billed Qty] from (select TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code, (TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Qty)[DCS Billed Qty] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL
								union all select TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code,(TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Qty)[DCS Billed Qty] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID ) as TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
								Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD On TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code where Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) And Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103)<=Convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103) 
								)TSPL_DCS_MP_INCENTIVE_RECO_HEAD  )xx  ) final "
                Next
                dtDBTSummary = clsDBFuncationality.GetDataTable(query)
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                    gvDBTSummary.DataSource = Nothing
                    gvDBTSummary.Rows.Clear()
                    gvDBTSummary.Columns.Clear()
                    gvDBTSummary.GroupDescriptors.Clear()
                    gvDBTSummary.MasterTemplate.SummaryRowsBottom.Clear()
                    gvDBTSummary.MasterView.Refresh()
                    gvDBTSummary.DataSource = dt2
                    For ii As Integer = 0 To gvDBTSummary.Columns.Count - 1
                        gvDBTSummary.Columns(ii).ReadOnly = True
                    Next
                    RadPageView.SelectedPage = RadPageViewPage2
                    gvDBTSummary.EnableFiltering = True
                    gvDBTSummary.AllowAddNewRow = False
                    gvDBTSummary.ShowGroupPanel = False
                    gvDBTSummary.BestFitColumns()
                Else
                    '' clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub SetGridFormation()
        gvPaymentStatus.TableElement.TableHeaderHeight = 40
        gvPaymentStatus.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gvPaymentStatus.Columns.Count - 1
            gvPaymentStatus.Columns(ii).ReadOnly = True
            gvPaymentStatus.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        gvPaymentStatus.ShowGroupPanel = False
        gvPaymentStatus.Columns("SNO.").IsVisible = True '

        gvPaymentStatus.Columns("Union Name").HeaderText = "Union Name"
        gvPaymentStatus.Columns("Union Name").Width = 500
        '' gvPaymentStatus.Columns("Union Name").IsVisible = True
        gvPaymentStatus.Columns("Farmer Count").IsVisible = True
        gvPaymentStatus.Columns("NEFT Amount").IsVisible = True
        gvPaymentStatus.Columns("Success_Farmer").HeaderText = "Farmer Count"
        gvPaymentStatus.Columns("Success_Amount").HeaderText = "NEFT Amount"
        gvPaymentStatus.Columns("Failure_Farmer").HeaderText = "Farmer Count"
        gvPaymentStatus.Columns("Failure_Amount").HeaderText = "NEFT Amount"
        gvPaymentStatus.Columns("Null_Farmer_Count").HeaderText = "Farmer Count"
        gvPaymentStatus.Columns("Null_Farmer_Amount").HeaderText = "NEFT Amount"

        Dim item2 As New GridViewSummaryItem("Farmer Count", "{0:F2}", GridAggregateFunction.Count)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("NEFT Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Success_Farmer", "{0:F2}", GridAggregateFunction.Count)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Success_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Failure_Farmer", "{0:F2}", GridAggregateFunction.Count)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Failure_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Null_Farmer_Count", "{0:F2}", GridAggregateFunction.Count)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Null_Farmer_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)


        gvPaymentStatus.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ''  gvPaymentStatus.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bosttom
        View()
    End Sub
    Sub View()

        If gvPaymentStatus.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup("SNO."))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("SNo.").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Union Name"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Union Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Farmer Count").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("NEFT Amount").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Success"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Success_Farmer").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Success_Amount").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Reject"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Failure_Farmer").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Failure_Amount").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("No Response"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Null_Farmer_Count").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gvPaymentStatus.Columns("Null_Farmer_Amount").Name)

            gvPaymentStatus.ViewDefinition = view
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvPaymentStatus.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gvPaymentStatus.Columns.Count - 1 Step ii + 1
                        gvPaymentStatus.Columns(ii).IsVisible = False
                        gvPaymentStatus.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvPaymentStatus.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Print_DBTSummary(ByVal exporter As EnumExportTo)
        Try
            If gvDBTSummary.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gvDBTSummary, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvDBTSummary, "", "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvDBTSummary, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Printt_DBT_Mishmatch_Qty(ByVal exporter As EnumExportTo)
        Try
            If gvMismatchqty.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gvMismatchqty, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvMismatchqty, "", "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvMismatchqty, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Print_PaymentStatus(ByVal exporter As EnumExportTo)
        Try
            If gvPaymentStatus.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gvPaymentStatus, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvPaymentStatus, "", "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvPaymentStatus, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Jan_Adh_print(ByVal exporter As EnumExportTo)
        Try
            If gvJanAdh.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gvJanAdh, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvJanAdh, "", "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvJanAdh, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BCBExcel_Click(sender As Object, e As EventArgs) Handles BCBExcel.Click
        Print_DBTSummary(EnumExportTo.Excel)
        Printt_DBT_Mishmatch_Qty(EnumExportTo.Excel)
        Print_PaymentStatus(EnumExportTo.Excel)
        Jan_Adh_print(EnumExportTo.Excel)
    End Sub

    Private Sub BCBPDF_Click(sender As Object, e As EventArgs) Handles BCBPDF.Click
        Print_DBTSummary(EnumExportTo.PDF)
        Printt_DBT_Mishmatch_Qty(EnumExportTo.PDF)
        Print_PaymentStatus(EnumExportTo.PDF)
        Jan_Adh_print(EnumExportTo.PDF)
    End Sub
End Class