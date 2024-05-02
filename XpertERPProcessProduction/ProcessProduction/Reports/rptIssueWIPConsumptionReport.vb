Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No : BHA/17/10/18-000630 by prabhakar - Create new report 
Public Class rptIssueWIPConsumptionReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
#End Region
    
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        chkDiff.Enabled = True
        rdbSummary.Checked = True
    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1

        EnableDisableCtrl(True)

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
        RadGroupBox1.Enabled = val
        chkDiff.Enabled = val
        txtLocation.Enabled = val
        txtBatchNo.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.Checked = True, "S", "D")
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whr As String = " and 2=2"
            Dim dt As New DataTable
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_PP_BATCH_ORDER_HEAD.Location_code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtBatchNo.arrValueMember IsNot Nothing AndAlso txtBatchNo.arrValueMember.Count > 0 Then
                whr += " and batchCode  in (" + clsCommon.GetMulcallString(txtBatchNo.arrValueMember) + ")"
            End If
            Dim mainQry As String = "  select * from ( " & _
                                    "  select (case when Inventory.Trans_Type='PP_ISSUE' then TSPL_PP_ISSUE_HEAD.Batch_Code else case when Inventory.Trans_Type='PP_STD-FQC' then TSPL_PP_STD_FINALQC_HEAD.Main_Batch_Code else  case when Inventory.Trans_Type='PROD_ENTRY' then TSPL_PP_PRODUCTION_ENTRY.Batch_Code else  case when Inventory.Trans_Type='PRD_STG_PROC' then TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code else '' end end end end) as BatchCode, " & _
                                    "  Trans_Type,Source_Doc_No,Source_Doc_Date,Avg_Cost,InOut,item_code   " & _
                                    "  ,(select top 1 CONSM_CODE from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where (TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=Inventory.Source_Doc_No or TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code) and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE=Inventory.Item_Code and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY=Inventory.Qty) as ExistsINConsumption " & _
                                    "  ,(select top 1 CONSM_CODE from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where (TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=Inventory.Source_Doc_No or TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code) and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE=Inventory.Item_Code ) as ExistsINConsumptionOnlyItem,IS_CONSUMPTION " & _
                                    "  from ( " & _
                                    "  select IS_CONSUMPTION,item_code, Trans_Type,Source_Doc_No,convert(varchar, Source_Doc_Date,103) as Source_Doc_Date,Avg_Cost,InOut,Qty  from TSPL_INVENTORY_MOVEMENT where Trans_Type in ('PP_ISSUE','PP_STD-FQC','PROD_ENTRY','PRD_STG_PROC') " & _
                                    "  union all " & _
                                    "  select IS_CONSUMPTION,item_code,Trans_Type,Source_Doc_No,convert(varchar, Source_Doc_Date,103) as Source_Doc_Date,Avg_Cost,InOut,Qty from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type in ('PP_ISSUE','PP_STD-FQC','PROD_ENTRY','PRD_STG_PROC') " & _
                                    "  )Inventory " & _
                                    "  left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=Inventory.Source_Doc_No and Inventory.Trans_Type='PP_ISSUE' and TSPL_PP_ISSUE_HEAD.Is_post=1 " & _
                                    "  left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_HEAD.QC_Code=Inventory.Source_Doc_No and Inventory.Trans_Type='PP_STD-FQC' and TSPL_PP_STD_FINALQC_HEAD.Posted=1 " & _
                                    "  left outer join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE=Inventory.Source_Doc_No and Inventory.Trans_Type='PRD_STG_PROC' and TSPL_PP_STAGE_PROCESS_HEAD.Posted=1  " & _
                                    "  left outer join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=Inventory.Source_Doc_No and Inventory.Trans_Type='PROD_ENTRY' and TSPL_PP_PRODUCTION_ENTRY.Posted=1 " & _
                                    "  )x where not exists(select 1 from TSPL_PP_BATCH_ORDER_bom_detail where TSPL_PP_BATCH_ORDER_bom_detail.batch_Code=x.BatchCode and TSPL_PP_BATCH_ORDER_bom_detail.item_code=x.item_code) " & _
                                    ""

            If rdbDetail.Checked = True Then
                qry = " select convert (varchar, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) as [Batch Date], BatchCode as [Batch Code],TSPL_PP_BATCH_ORDER_HEAD.Location_Code as [Location Code],tspl_location_master.Location_Desc as [Location Desc], Final.item_code as  [Item Code], TSPL_ITEM_MASTER.Item_Desc as [Item Desc],InOut, " & _
                      " case when Trans_Type='PP_ISSUE'   then Source_Doc_No   end as [Issue Doc No], " & _
                      " case when Trans_Type='PP_ISSUE'   then Source_Doc_Date   end as [Issue Doc Date], " & _
                      " (Avg_Cost * case when Trans_Type='PP_ISSUE' and InOut ='I'  then 1 else 0 end ) as [Issue Amount] , " & _
                      " case when Trans_Type='PP_STD-FQC' then Source_Doc_No   end as [Standardize Doc No] ,  " & _
                      " case when Trans_Type='PP_STD-FQC' then Source_Doc_Date   end as [Standardize Doc Date] , " & _
                      " ((Avg_Cost * case when Trans_Type='PP_STD-FQC' and  2=(case when  ExistsINConsumptionOnlyItem is not null and InOut='O' and IS_CONSUMPTION=1 then 2 else 3 end)   then 1 else 0 end ) +(Avg_Cost * case when Trans_Type='PP_STD-FQC' and 2=(case when  ExistsINConsumptionOnlyItem is not null and InOut='I' then 2 else 3 end)   then -1 else 0 end )) as [Standardize Amount], " & _
                       "((Avg_Cost * (case when Trans_Type='PP_STD-FQC' and 2=(case when  ExistsINConsumptionOnlyItem is not null and InOut='O' and IS_CONSUMPTION=0 then 2 else 3 end)   then -1 else 0 end ))) as [Standardize Added Amount] " + Environment.NewLine + _
                      " , case when Trans_Type='PROD_ENTRY' then Source_Doc_No end as [Production Doc No] , " & _
                      " case when Trans_Type='PROD_ENTRY' then Source_Doc_Date end as [Production Doc Date] , " & _
                      "  (Avg_Cost * case when Trans_Type='PROD_ENTRY' and 2=(case when InOut='I' then 2 else case when InOut='O' and ExistsINConsumption is not null then 2 else 3 end end) then 1 else 0 end ) as [Production Entry Amount], " & _
                      "  case when Trans_Type='PRD_STG_PROC' then Source_Doc_No end as [Stage Process DocNo], " & _
                      "  case when Trans_Type='PRD_STG_PROC' then Source_Doc_Date end as [Stage Process Doc Date] " & _
                      "  , (Avg_Cost * case when Trans_Type='PRD_STG_PROC' then 1 else 0 end ) as [Stage Process Amount]  from ( " & _
                      "  " + mainQry + " " & _
                      "  )Final  " & _
                      "  left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code = Final.BatchCode " & _
                      "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code = Final.Item_Code " & _
                      "  left outer join tspl_location_master on tspl_location_master.Location_Code =TSPL_PP_BATCH_ORDER_HEAD.Location_Code " & _
                      "  Where convert(date,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + whr + " " & _
                      "  order by BatchCode  "
            Else
                qry = " select convert (varchar,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) as [Batch Date], x.BatchCode as [Batch Code] ,TSPL_PP_BATCH_ORDER_HEAD.Location_code as [Location code] ,x.IssueDocNo as [Issue Doc No],x.IssueAmount as [Issue Amount] , x.StandardizeDocNo as [Standardize Doc No], StandardizeAmount as [Standardize Amount],StandardizeAddAmount as [Standardize Added Amount] ,x.ProductionDocNo as [Production Doc No] , ProductionEntryAmount as [Production Entry Amount], StageProcessDocNo as [Stage Process Doc No],StageProcessAmount as [Stage Process Amount] , Diff + StageProcessAmount as Diff from (  " & _
                     "  select  xxx.BatchCode,IssueDocNoMax+case when IssueDocNoMin<>IssueDocNoMax and len(IssueDocNoMin)>0 then ' *' else '' end as  IssueDocNo,IssueAmount,StandardizeDocNoMax +case when StandardizeDocNoMin<>StandardizeDocNoMax and len(StandardizeDocNoMin)>0 then ' *' else '' end as StandardizeDocNo ,StandardizeAmount,StandardizeAddAmount,ProductionDocNoMax + case when ProductionDocNoMin<>ProductionDocNoMax and len(ProductionDocNoMin)>0 then ' *' else '' end as ProductionDocNo,ProductionEntryAmount,StageProcessDocNoMax + case when StageProcessDocNoMin<>StageProcessDocNoMax and len(StageProcessDocNoMin)>0 then ' *' else '' end as StageProcessDocNo,StageProcessAmount " & _
                     " ,issueAmount-StandardizeAmount+StandardizeAddAmount-ProductionEntryAmount as Diff from ( " & _
                     " select BatchCode, " & _
                     " Min(case when Trans_Type='PP_ISSUE'   then Source_Doc_No   end ) as IssueDocNoMin, " & _
                     " Max(case when Trans_Type='PP_ISSUE'  then Source_Doc_No   end ) as IssueDocNoMax, " & _
                     " sum(Avg_Cost * case when Trans_Type='PP_ISSUE' and InOut ='I'  then 1 else 0 end ) as IssueAmount, " & _
                     " Min(case when Trans_Type='PP_STD-FQC' then Source_Doc_No   end ) as StandardizeDocNoMin, " & _
                     " Max(case when Trans_Type='PP_STD-FQC' then Source_Doc_No   end ) as StandardizeDocNoMax, " & _
                     " (sum(Avg_Cost * case when Trans_Type='PP_STD-FQC' and 2=(case when  ExistsINConsumptionOnlyItem is not null and IS_CONSUMPTION=1 then 2 else 3 end)   then 1 else 0 end ) +sum(Avg_Cost * case when Trans_Type='PP_STD-FQC' and 2=(case when  ExistsINConsumptionOnlyItem is not null and InOut='I' then 2 else 3 end)   then -1 else 0 end )) as StandardizeAmount, " & _
                     " (sum(Avg_Cost * (case when Trans_Type='PP_STD-FQC' and 2=(case when  ExistsINConsumptionOnlyItem is not null and InOut='O' and IS_CONSUMPTION=0 then 2 else 3 end)   then 1 else 0 end ))) as StandardizeAddAmount ," + Environment.NewLine + _
                     " min(case when Trans_Type='PROD_ENTRY' then Source_Doc_No end ) as ProductionDocNoMin, " & _
                     " max(case when Trans_Type='PROD_ENTRY' then Source_Doc_No end ) as ProductionDocNoMax, " & _
                     " sum(Avg_Cost * case when Trans_Type='PROD_ENTRY' and 2=(case when InOut='I' then 2 else case when InOut='O' and ExistsINConsumption is not null then 2 else 3 end end) then 1 else 0 end ) as ProductionEntryAmount,  " & _
                     " min(case when Trans_Type='PRD_STG_PROC' then Source_Doc_No end ) as StageProcessDocNoMin, " & _
                     " max(case when Trans_Type='PRD_STG_PROC' then Source_Doc_No end ) as StageProcessDocNoMax, " & _
                     " /* sum(Avg_Cost * case when Trans_Type='PRD_STG_PROC' then 1 else 0 end ) as StageProcessAmount */  " & _
                     "  sum(Avg_Cost * case when Trans_Type='PRD_STG_PROC' then (case when InOut ='O' then 1 else -1 end) else 0 end ) as StageProcessAmount " & _
                     " from ( " & _
                     " " + mainQry + " " & _
                     " )Final group by BatchCode  " & _
                     " )xxx   " & _
                     " )x  " & _
                     " left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=x.BatchCode " & _
                     " where 2= 2 and  convert(date,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + whr + "  "
                If chkDiff.Checked = True Then
                    qry = qry + " and  2= (case when Diff=0 then 3 else 2 end) "
                End If
                qry = qry + " order by convert(date,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date ,103), BatchCode asc "

            End If
            dt = clsDBFuncationality.GetDataTable(qry)
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
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Issue Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Standardize Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item5 As New GridViewSummaryItem("Standardize Added Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item3 As New GridViewSummaryItem("Production Entry Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Stage Process Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
            EnableDisableCtrl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
   
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        ' ticket NO :  TEC/25/02/19-000430  By prabhakar 
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub txtBatchNo__My_Click(sender As Object, e As EventArgs) Handles txtBatchNo._My_Click
        Dim qry As String
        qry = " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Description from TSPL_PP_BATCH_ORDER_HEAD order by convert (datetime ,Batch_Date,103) "
        txtBatchNo.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Code", "Description", txtBatchNo.arrValueMember, txtBatchNo.arrDispalyMember)
    End Sub
    
    Private Sub rdbSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSummary.CheckedChanged
        If rdbSummary.Checked = True Then
            chkDiff.Enabled = True
        Else
            chkDiff.Enabled = False
        End If
    End Sub

    Private Sub rdbDetail_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDetail.CheckedChanged
        If rdbDetail.Checked = True Then
            chkDiff.Enabled = False
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtBatchNo.arrValueMember IsNot Nothing AndAlso txtBatchNo.arrValueMember.Count > 0 Then
                arrHeader.Add("Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNo.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Issue WIP Consumption Report", Gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtBatchNo.arrValueMember IsNot Nothing AndAlso txtBatchNo.arrValueMember.Count > 0 Then
                arrHeader.Add("Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNo.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Issue WIP Consumption Report", Gv1, arrHeader, "Issue WIP Consumption Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
