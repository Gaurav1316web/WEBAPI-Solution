Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No :TEC/08/07/19-000934  by prabhakar - Create new report 
Public Class rptProductionStatusReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub Reset()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        txtBatchNo.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        'Ticket No-ERO/07/08/19-000989,Sanjay,Add Item Description
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(rdbFGItem.Checked = True, "S", "D")
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whr As String = " and 2=2"
            Dim dt As New DataTable

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_PP_PRODUCTION_PLAN_HEAD.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtBatchNo.arrValueMember IsNot Nothing AndAlso txtBatchNo.arrValueMember.Count > 0 Then
                whr += " and TSPL_PP_BATCH_ORDER_HEAD.Batch_Code  in (" + clsCommon.GetMulcallString(txtBatchNo.arrValueMember) + ")"
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr += " and TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code  in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If
            If rdbSFGItem.Checked = True Then
                whr += " and TSPL_ITEM_MASTER.Item_Category_Struct_Code ='SFG'"
            Else
                whr += " and TSPL_ITEM_MASTER.Item_Category_Struct_Code ='FG'"
            End If

            qry = " Select  Plan_Date, Plan_Code ,Item_Code,Item_Desc, case when RowNumberByPlanCode = 1 then   Plan_Qty else 0 end  as Plan_Qty, Unit_Code , Plan_Status," &
                  " Batch_Date, Batch_Code ,  Batch_Order_Item_Code, case when RowNumberByBatchCode = 1 then  Batch_Order_Qty else 0 end Batch_Order_Qty  , Batch_Order_Unit_Code, Batch_Order_Status, " &
                  " Issue_Date,Issue_Code, Issue_Item_Code,  case when RowNumberByIssueCode = 1 then  Issue_Qty else 0 end as Issue_Qty,  Issue_Unit_Code, Issue_Status,  " &
                  " Standardization_Date,Standardization_Code,  Standardization_Item_Code, case when RowNumberByStandardizationCode = 1 then Standardization_Qty else 0 end as Standardization_Qty, Standardization_Unit_Code,Standardization_Status," &
                  " QC_Date, QC_Code,QC_Item_Code, case when RowNumberByQCCode = 1 then QC_Qty else 0 end as QC_Qty , QC_Unit_Code,QC_Status, " &
                  " STAGE_Date,STAGE_PROCESS_CODE,STAGE_Item_Code ,case when RowNumberByStageProcessCode =1 then  STAGE_Qty else 0 end as STAGE_Qty ,STAGE_Unit_Code,STAGE_Status, " &
                  " PROD_Date,PROD_ENTRY_CODE,PROD_Item_Code,case when RowNumberByProdEntry =1 then PROD_Qty else 0 end PROD_Qty,PROD_Unit_Code,PROD_Status  " &
                  "  from ( " &
                  " select row_number() over (partition by TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code order by TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code) as RowNumberByPlanCode," &
                  " row_number() over (partition by  TSPL_PP_BATCH_ORDER_HEAD.Batch_Code order by TSPL_PP_BATCH_ORDER_HEAD.Batch_Code) as RowNumberByBatchCode, " &
                  " row_number() over (partition by TSPL_PP_ISSUE_HEAD.Issue_Code order by TSPL_PP_ISSUE_HEAD.Issue_Code) as RowNumberByIssueCode, " &
                  " row_number() over (partition by TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code order by TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code) as RowNumberByStandardizationCode, " &
                  " row_number() over (partition by TSPL_PP_STD_FINALQC_HEAD.QC_Code order by TSPL_PP_STD_FINALQC_HEAD.QC_Code) as RowNumberByQCCode, " &
                  " row_number() over (partition by TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE order by TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE) as RowNumberByStageProcessCode, " &
                  " row_number() over (partition by  TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE  order by  TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE ) as RowNumberByProdEntry, " &
                  " convert (varchar, TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103) as Plan_Date ,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code ,TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PP_PRODUCTION_PLAN_DETAIL.Final_Qty as Plan_Qty, TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code, case when  TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post  = 1 then 'APPROVED' else 'UN-APPROVED' end  as Plan_Status, " &
                  " case when len (TSPL_PP_BATCH_ORDER_HEAD.Batch_Code) > 0 then convert (varchar, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) else '' end  as  Batch_Date , TSPL_PP_BATCH_ORDER_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code as Batch_Order_Item_Code ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity as Batch_Order_Qty, TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code as Batch_Order_Unit_Code, case when len (TSPL_PP_BATCH_ORDER_HEAD.Batch_Code) > 0 then  case when  TSPL_PP_BATCH_ORDER_HEAD.Is_Post  = 1 then 'APPROVED' else 'UN-APPROVED' end  else '' end as Batch_Order_Status  " &
                  " , Case when len (TSPL_PP_ISSUE_HEAD.Issue_Code ) > 0 then Convert (varchar,TSPL_PP_ISSUE_HEAD.Issue_Date,103) else '' end  as Issue_Date , TSPL_PP_ISSUE_HEAD.Issue_Code ,Case when len (TSPL_PP_ISSUE_HEAD.Issue_Code ) > 0 then TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code else '' end as Issue_Item_Code ,Case when len (TSPL_PP_ISSUE_HEAD.Issue_Code ) > 0 then  TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity else '' end as Issue_Qty, Case when len (TSPL_PP_ISSUE_HEAD.Issue_Code ) > 0 then  TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code else '' end as Issue_Unit_Code, Case when len (TSPL_PP_ISSUE_HEAD.Issue_Code ) > 0  then  case when  TSPL_PP_ISSUE_HEAD.Is_Post  = 1 then 'APPROVED' else 'UN-APPROVED' end else '' end as Issue_Status  " &
                  " , case when len (TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code)> 0 then Convert (varchar, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) else '' end as Standardization_Date , TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  , TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code as Standardization_Item_Code , TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty as Standardization_Qty, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code as Standardization_Unit_Code, case when len (TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code)> 0 then  case when  TSPL_PP_STANDARDIZATION_HEAD.Posted  = 1 then 'APPROVED' else 'UN-APPROVED' end else '' end  as Standardization_Status  " &
                  " , Case when len(TSPL_PP_STD_FINALQC_HEAD.QC_Code) > 0 then Convert (varchar, TSPL_PP_STD_FINALQC_HEAD.QC_Date,103) else '' end  QC_Date, TSPL_PP_STD_FINALQC_HEAD.QC_Code ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code as QC_Item_Code  ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_Qty as QC_Qty , TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code as QC_Unit_Code , Case when len(TSPL_PP_STD_FINALQC_HEAD.QC_Code) > 0 then  case when  TSPL_PP_STD_FINALQC_HEAD.Posted  = 1 then 'APPROVED' else 'UN-APPROVED' end else '' end as QC_Status  " &
                  " , Case when len(TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE) > 0 then Convert (varchar, TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,103) else '' end  STAGE_Date, TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE,TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code as STAGE_Item_Code  ,TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.FINAL_PROD_Qty as STAGE_Qty , TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code as STAGE_Unit_Code , Case when len(TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE) > 0 then  case when  TSPL_PP_STAGE_PROCESS_HEAD.Posted  = 1 then 'APPROVED' else 'UN-APPROVED' end else '' end  as STAGE_Status " &
                  " , Case when len(TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE) > 0 then Convert (varchar, TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) else '' end  PROD_Date, TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code as PROD_Item_Code  ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.Final_Production_Qty as PROD_Qty , TSPL_PP_PRODUCTION_ENTRY_DETAIL.Unit_Code as PROD_Unit_Code , Case when len(TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE) > 0 then  case when  TSPL_PP_PRODUCTION_ENTRY.Posted  = 1 then 'APPROVED' else 'UN-APPROVED' end  else '' end as PROD_Status " &
                  " from TSPL_PP_PRODUCTION_PLAN_DETAIL " &
                  " inner join TSPL_PP_PRODUCTION_PLAN_HEAD on TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code = TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code " &
                  " left outer Join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Plan_Code = TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code " &
                  " left outer Join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " &
                  " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Batch_Code = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " &
                  " left outer join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Child_Batch_Code = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " &
                  " left Outer Join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  " &
                  " left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code And TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  " &
                  " left Outer Join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code = TSPL_PP_STD_FINALQC_HEAD.QC_Code " &
                  " left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " &
                  " left outer join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " &
                  " left outer join TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE " &
                  " left outer join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.Batch_Code = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " &
                  " left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
                  " where convert(date,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date ,103) <=convert(date,'" + ToDate.Value + "' ,103) " &
                  " " + whr + "  ) final Order by convert (date,final.Plan_Date ,103),final.Plan_Code,final.Batch_Code,final.Issue_Code,final.Standardization_Code,final.QC_Code,final.STAGE_PROCESS_CODE,final.PROD_ENTRY_CODE  asc  "




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
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            FormatGrid()
            ReStoreGridLayout()
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

    ' ticket NO :  TEC/25/02/19-000430  By prabhakar 
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
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
            ''---------------
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
            clsCommon.MyExportToExcelGrid("Production Status Report", Gv1, arrHeader, Me.Text)
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
            clsCommon.MyExportToPDF("Production Status Report", Gv1, arrHeader, "Issue WIP Consumption Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select TSPL_ITEM_MASTER.Item_Code as Code , TSPL_ITEM_MASTER.Item_Desc as Description from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Category_Struct_Code in ('SFG','FG') "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@ItemcodeForProdStatRPT", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub


    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).IsVisible = False
        Next
        'Gv1.Columns("RowNumber").IsVisible = False
        'Gv1.Columns("RowNumber").Width = 100
        'Gv1.Columns("RowNumber").HeaderText = "RowNumber"
        ' Plan_Date, Plan_Code , Plan_Qty, Item_Code, Unit_Code , Plan_Status
        Gv1.Columns("Plan_Date").IsVisible = True
        Gv1.Columns("Plan_Date").Width = 100
        Gv1.Columns("Plan_Date").HeaderText = "Date"

        Gv1.Columns("Item_Code").IsVisible = True
        Gv1.Columns("Item_Code").Width = 100
        Gv1.Columns("Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Item_Desc").IsVisible = True
        Gv1.Columns("Item_Desc").Width = 140
        Gv1.Columns("Item_Desc").HeaderText = "Item Description"

        Gv1.Columns("Plan_Code").IsVisible = True
        Gv1.Columns("Plan_Code").Width = 140
        Gv1.Columns("Plan_Code").HeaderText = "Code"

       


        Gv1.Columns("Plan_Qty").IsVisible = True
        Gv1.Columns("Plan_Qty").Width = 100
        Gv1.Columns("Plan_Qty").HeaderText = "Qty"

        

        Gv1.Columns("Unit_Code").IsVisible = True
        Gv1.Columns("Unit_Code").Width = 100
        Gv1.Columns("Unit_Code").HeaderText = "Unit"

        Gv1.Columns("Plan_Status").IsVisible = True
        Gv1.Columns("Plan_Status").Width = 100
        Gv1.Columns("Plan_Status").HeaderText = "Status"

        ' Batch_Date, Batch_Code , Batch_Order_Qty , Batch_Order_Item_Code , Batch_Order_Unit_Code, Batch_Order_Status
        Gv1.Columns("Batch_Date").IsVisible = True
        Gv1.Columns("Batch_Date").Width = 100
        Gv1.Columns("Batch_Date").HeaderText = "Date"

        Gv1.Columns("Batch_Code").IsVisible = True
        Gv1.Columns("Batch_Code").Width = 140
        Gv1.Columns("Batch_Code").HeaderText = "Code"

        Gv1.Columns("Batch_Order_Item_Code").IsVisible = True
        Gv1.Columns("Batch_Order_Item_Code").Width = 100
        Gv1.Columns("Batch_Order_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Batch_Order_Qty").IsVisible = True
        Gv1.Columns("Batch_Order_Qty").Width = 100
        Gv1.Columns("Batch_Order_Qty").HeaderText = "Qty"

        

        Gv1.Columns("Batch_Order_Unit_Code").IsVisible = True
        Gv1.Columns("Batch_Order_Unit_Code").Width = 100
        Gv1.Columns("Batch_Order_Unit_Code").HeaderText = "Unit"

        Gv1.Columns("Batch_Order_Status").IsVisible = True
        Gv1.Columns("Batch_Order_Status").Width = 100
        Gv1.Columns("Batch_Order_Status").HeaderText = "Status"

        ' Issue_Date, Issue_Code, Issue_Qty, Issue_Item_Code, Issue_Unit_Code, Issue_Status
        Gv1.Columns("Issue_Date").IsVisible = True
        Gv1.Columns("Issue_Date").Width = 100
        Gv1.Columns("Issue_Date").HeaderText = "Date"

        Gv1.Columns("Issue_Code").IsVisible = True
        Gv1.Columns("Issue_Code").Width = 140
        Gv1.Columns("Issue_Code").HeaderText = "Code"

        Gv1.Columns("Issue_Item_Code").IsVisible = True
        Gv1.Columns("Issue_Item_Code").Width = 100
        Gv1.Columns("Issue_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Issue_Qty").IsVisible = True
        Gv1.Columns("Issue_Qty").Width = 100
        Gv1.Columns("Issue_Qty").HeaderText = "Qty"

       


        Gv1.Columns("Issue_Unit_Code").IsVisible = True
        Gv1.Columns("Issue_Unit_Code").Width = 100
        Gv1.Columns("Issue_Unit_Code").HeaderText = "Unit"

        Gv1.Columns("Issue_Status").IsVisible = True
        Gv1.Columns("Issue_Status").Width = 100
        Gv1.Columns("Issue_Status").HeaderText = "Status"

        ' Standardization_Date, Standardization_Code,Standardization_Qty,Standardization_Item_Code, Standardization_Unit_Code,Standardization_Status
        Gv1.Columns("Standardization_Date").IsVisible = True
        Gv1.Columns("Standardization_Date").Width = 100
        Gv1.Columns("Standardization_Date").HeaderText = "Date"

        Gv1.Columns("Standardization_Code").IsVisible = True
        Gv1.Columns("Standardization_Code").Width = 140
        Gv1.Columns("Standardization_Code").HeaderText = "Code"

        Gv1.Columns("Standardization_Item_Code").IsVisible = True
        Gv1.Columns("Standardization_Item_Code").Width = 100
        Gv1.Columns("Standardization_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Standardization_Qty").IsVisible = True
        Gv1.Columns("Standardization_Qty").Width = 100
        Gv1.Columns("Standardization_Qty").HeaderText = "Qty"

        

        Gv1.Columns("Standardization_Unit_Code").IsVisible = True
        Gv1.Columns("Standardization_Unit_Code").Width = 100
        Gv1.Columns("Standardization_Unit_Code").HeaderText = "Unit"

        Gv1.Columns("Standardization_Status").IsVisible = True
        Gv1.Columns("Standardization_Status").Width = 100
        Gv1.Columns("Standardization_Status").HeaderText = "Status"

        ' QC_Date, QC_Code,QC_Qty , QC_Item_Code, QC_Unit_Code,QC_Status
        Gv1.Columns("QC_Date").IsVisible = True
        Gv1.Columns("QC_Date").Width = 100
        Gv1.Columns("QC_Date").HeaderText = "Date"

        Gv1.Columns("QC_Code").IsVisible = True
        Gv1.Columns("QC_Code").Width = 140
        Gv1.Columns("QC_Code").HeaderText = "Code"

        Gv1.Columns("QC_Item_Code").IsVisible = True
        Gv1.Columns("QC_Item_Code").Width = 100
        Gv1.Columns("QC_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("QC_Qty").IsVisible = True
        Gv1.Columns("QC_Qty").Width = 100
        Gv1.Columns("QC_Qty").HeaderText = "Qty"

        


        Gv1.Columns("QC_Unit_Code").IsVisible = True
        Gv1.Columns("QC_Unit_Code").Width = 100
        Gv1.Columns("QC_Unit_Code").HeaderText = "Unit"

        Gv1.Columns("QC_Status").IsVisible = True
        Gv1.Columns("QC_Status").Width = 100
        Gv1.Columns("QC_Status").HeaderText = "Status"

        ' STAGE_Date,STAGE_PROCESS_CODE ,STAGE_Qty, STAGE_Item_Code,STAGE_Unit_Code,STAGE_Status
        Gv1.Columns("STAGE_Date").IsVisible = True
        Gv1.Columns("STAGE_Date").Width = 100
        Gv1.Columns("STAGE_Date").HeaderText = "Date"


        Gv1.Columns("STAGE_PROCESS_CODE").IsVisible = True
        Gv1.Columns("STAGE_PROCESS_CODE").Width = 140
        Gv1.Columns("STAGE_PROCESS_CODE").HeaderText = "Code"


        Gv1.Columns("STAGE_Item_Code").IsVisible = True
        Gv1.Columns("STAGE_Item_Code").Width = 100
        Gv1.Columns("STAGE_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("STAGE_Qty").IsVisible = True
        Gv1.Columns("STAGE_Qty").Width = 100
        Gv1.Columns("STAGE_Qty").HeaderText = "Qty"


        Gv1.Columns("STAGE_Unit_Code").IsVisible = True
        Gv1.Columns("STAGE_Unit_Code").Width = 100
        Gv1.Columns("STAGE_Unit_Code").HeaderText = "Unit"

        Gv1.Columns("STAGE_Status").IsVisible = True
        Gv1.Columns("STAGE_Status").Width = 100
        Gv1.Columns("STAGE_Status").HeaderText = "Status"

        ' PROD_Date,PROD_ENTRY_CODE,PROD_Qty,PROD_Item_Code,PROD_Unit_Code,PROD_Status
        Gv1.Columns("PROD_Date").IsVisible = True
        Gv1.Columns("PROD_Date").Width = 100
        Gv1.Columns("PROD_Date").HeaderText = "Date"

        Gv1.Columns("PROD_ENTRY_CODE").IsVisible = True
        Gv1.Columns("PROD_ENTRY_CODE").Width = 140
        Gv1.Columns("PROD_ENTRY_CODE").HeaderText = "Code"

        Gv1.Columns("PROD_Item_Code").IsVisible = True
        Gv1.Columns("PROD_Item_Code").Width = 100
        Gv1.Columns("PROD_Item_Code").HeaderText = "Item Code"


        Gv1.Columns("PROD_Qty").IsVisible = True
        Gv1.Columns("PROD_Qty").Width = 100
        Gv1.Columns("PROD_Qty").HeaderText = "Qty"

        

        Gv1.Columns("PROD_Unit_Code").IsVisible = True
        Gv1.Columns("PROD_Unit_Code").Width = 100
        Gv1.Columns("PROD_Unit_Code").HeaderText = "Unit"

        Gv1.Columns("PROD_Status").IsVisible = True
        Gv1.Columns("PROD_Status").Width = 100
        Gv1.Columns("PROD_Status").HeaderText = "Status"






        'Gv1.Columns("OpencrateQty").IsVisible = True
        'Gv1.Columns("OpencrateQty").Width = 100
        'Gv1.Columns("OpencrateQty").HeaderText = "Crate"
        'Gv1.Columns("OpencrateQty").FormatString = "{0:F0}"

        

        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0

        'If chkCustomerWise.Checked = True Then
        '    Dim item1 As New GridViewSummaryItem("OpencrateQty", "{0:F0}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item1)
        '    Dim item16 As New GridViewSummaryItem("OpenCanQty", "{0:F0}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item16)
        'ElseIf chkcustomerWithDateWise.Checked = True Then

        '    Dim TotalCrateOpening As New GridViewSummaryItem()
        '    TotalCrateOpening.FormatString = "{0:F0}"
        '    TotalCrateOpening.Name = "OpencrateQty"
        '    TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySum)"
        '    summaryRowItem.Add(TotalCrateOpening)

        '    Dim TotalCanOpening As New GridViewSummaryItem()
        '    TotalCanOpening.FormatString = "{0:F0}"
        '    TotalCanOpening.Name = "OpenCanQty"
        '    TotalCanOpening.AggregateExpression = "sum(OpenCanQtySum)"
        '    summaryRowItem.Add(TotalCanOpening)
        'Else
        '    Dim TotalCrateOpening As New GridViewSummaryItem()
        '    TotalCrateOpening.FormatString = "{0:F0}"
        '    TotalCrateOpening.Name = "OpencrateQty"
        '    TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySumOfVehicleWise)"
        '    summaryRowItem.Add(TotalCrateOpening)

        '    Dim TotalCanOpening As New GridViewSummaryItem()
        '    TotalCanOpening.FormatString = "{0:F0}"
        '    TotalCanOpening.Name = "OpenCanQty"
        '    TotalCanOpening.AggregateExpression = "sum(OpencanQtySumOfVehicleWise)"
        '    summaryRowItem.Add(TotalCanOpening)
        'End If
        ' ''---------------------
        'Dim item2 As New GridViewSummaryItem("OpenJaaliQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("OpenBoxQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'Dim item4 As New GridViewSummaryItem("CrateQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        'Dim item5 As New GridViewSummaryItem("JaaliQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("BoxQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)

        'Dim item17 As New GridViewSummaryItem("CanQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item17)

        'Dim item7 As New GridViewSummaryItem("CrateOutQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        'Dim item8 As New GridViewSummaryItem("jaaliOutQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item8)
        'Dim item9 As New GridViewSummaryItem("boxOutQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)

        'Dim item18 As New GridViewSummaryItem("CanOutQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item18)

        'If chkCustomerWise.Checked = True Then
        '    Dim item10 As New GridViewSummaryItem("CrateQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item10)
        '    Dim item19 As New GridViewSummaryItem("CanQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item19)
        'ElseIf chkcustomerWithDateWise.Checked = True Then

        '    Dim TotalCrateOpening As New GridViewSummaryItem()
        '    TotalCrateOpening.FormatString = "{0:F0}"
        '    TotalCrateOpening.Name = "CrateQtyClosing"
        '    TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySum)+sum(CrateOutQty)-sum(CrateQtyRecd)-sum(CrateAdjQty)"
        '    summaryRowItem.Add(TotalCrateOpening)

        '    Dim TotalCanOpening As New GridViewSummaryItem()
        '    TotalCanOpening.FormatString = "{0:F0}"
        '    TotalCanOpening.Name = "CanQtyClosing"
        '    TotalCanOpening.AggregateExpression = "sum(OpenCanQtySum)+sum(CanOutQty)-sum(CanQtyRecd)-sum(CanAdjQty)"
        '    summaryRowItem.Add(TotalCanOpening)
        'Else
        '    Dim TotalCrateOpening As New GridViewSummaryItem()
        '    TotalCrateOpening.FormatString = "{0:F0}"
        '    TotalCrateOpening.Name = "CrateQtyClosing"
        '    TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySumOfVehicleWise)+sum(CrateOutQty)-sum(CrateQtyRecd)-sum(CrateAdjQty)"
        '    summaryRowItem.Add(TotalCrateOpening)

        '    Dim TotalCanOpening As New GridViewSummaryItem()
        '    TotalCanOpening.FormatString = "{0:F0}"
        '    TotalCanOpening.Name = "CanQtyClosing"
        '    TotalCanOpening.AggregateExpression = "sum(OpencanQtySumOfVehicleWise)+sum(CanOutQty)-sum(CanQtyRecd)-sum(CanAdjQty)"
        '    summaryRowItem.Add(TotalCanOpening)
        'End If
        ' ''---------------------
        'Dim item11 As New GridViewSummaryItem("JaaliQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item11)
        'Dim item12 As New GridViewSummaryItem("BoxQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item12)



        'Dim item13 As New GridViewSummaryItem("CrateAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item13)
        'Dim item14 As New GridViewSummaryItem("JaaliAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item14)
        'Dim item15 As New GridViewSummaryItem("BoxAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item15)

        'Dim item20 As New GridViewSummaryItem("CanAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item20)

        Dim item21 As New GridViewSummaryItem("Plan_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("Batch_Order_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("Issue_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item23)
        Dim item24 As New GridViewSummaryItem("Standardization_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item24)
        Dim item25 As New GridViewSummaryItem("QC_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item25)
        Dim item26 As New GridViewSummaryItem("STAGE_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item26)
        Dim item27 As New GridViewSummaryItem("PROD_Qty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item27)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        View()
    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("Production Planning"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            'Plan_Date, Plan_Code , Plan_Qty, Item_Code, Unit_Code , Plan_Status
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Plan_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Plan_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Item_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Item_Desc").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Plan_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Unit_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Plan_Status").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Batch Order"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            ' Batch_Date, Batch_Code , Batch_Order_Qty , Batch_Order_Item_Code , Batch_Order_Unit_Code, Batch_Order_Status
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_Date").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_Order_Item_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_Order_Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_Order_Unit_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_Order_Status").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Production Issue Entry"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            ' Issue_Date, Issue_Code, Issue_Qty, Issue_Item_Code, Issue_Unit_Code, Issue_Status
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Date").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Item_Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Unit_Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Status").Name)


            If rdbSFGItem.Checked = True Then
                ' Standardization_Date, Standardization_Code,Standardization_Qty,Standardization_Item_Code, Standardization_Unit_Code,Standardization_Status
                view.ColumnGroups.Add(New GridViewColumnGroup("Production Standardization"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Standardization_Date").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Standardization_Code").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Standardization_Item_Code").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Standardization_Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Standardization_Unit_Code").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Standardization_Status").Name)

                ' QC_Date, QC_Code,QC_Qty , QC_Item_Code, QC_Unit_Code,QC_Status
                view.ColumnGroups.Add(New GridViewColumnGroup("Production Standardization Final QC"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("QC_Date").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("QC_Code").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("QC_Item_Code").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("QC_Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("QC_Unit_Code").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("QC_Status").Name)
            Else
                ' STAGE_Date,STAGE_PROCESS_CODE ,STAGE_Qty, STAGE_Item_Code,STAGE_Unit_Code,STAGE_Status
                view.ColumnGroups.Add(New GridViewColumnGroup("Stage Process"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("STAGE_Date").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("STAGE_PROCESS_CODE").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("STAGE_Item_Code").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("STAGE_Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("STAGE_Unit_Code").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("STAGE_Status").Name)

                ' ' PROD_Date,PROD_ENTRY_CODE,PROD_Qty,PROD_Item_Code,PROD_Unit_Code,PROD_Status
                view.ColumnGroups.Add(New GridViewColumnGroup("Production Entry"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_Date").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_ENTRY_CODE").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_Item_Code").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_Unit_Code").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_Status").Name)
            End If

            Gv1.ViewDefinition = view
        End If

    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                If e.Column Is Gv1.Columns("Plan_Code") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionPlanningDairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("Plan_Code").Value))
                ElseIf e.Column Is Gv1.Columns("Batch_Code") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBatchOrderDairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("Batch_Code").Value))
                ElseIf e.Column Is Gv1.Columns("Issue_Code") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Issue_Code").Value))
                ElseIf e.Column Is Gv1.Columns("Standardization_Code") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(Gv1.CurrentRow.Cells("Standardization_Code").Value))
                ElseIf e.Column Is Gv1.Columns("QC_Code") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(Gv1.CurrentRow.Cells("QC_Code").Value))
                ElseIf e.Column Is Gv1.Columns("STAGE_PROCESS_CODE") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(Gv1.CurrentRow.Cells("STAGE_PROCESS_CODE").Value))
                ElseIf e.Column Is Gv1.Columns("PROD_ENTRY_CODE") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("PROD_ENTRY_CODE").Value))
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
