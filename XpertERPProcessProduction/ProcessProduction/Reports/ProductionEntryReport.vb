Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class ProductionEntryReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub ProductionEntryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            clsCommon.MyExportToExcelGrid("Production Entry Report", Gv1, arrHeader, Me.Text)
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
            clsCommon.MyExportToPDF("Production EntryReport", Gv1, arrHeader, "Production Entry Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whr As String = ""
            Dim dt As New DataTable

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_PP_PRODUCTION_ENTRY.Location_Code In(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr += " and TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code In(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If

            qry = " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BOM_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,
                    TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,TSPL_PP_PRODUCTION_ENTRY_DETAIL.REJ_HEAD,
                    TSPL_PP_PRODUCTION_ENTRY_DETAIL.REJ_QTY,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BREAKAGE_HEAD,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BREAKAGE_QTY,TSPL_PP_PRODUCTION_ENTRY_DETAIL.LAB_TESTING,TSPL_PP_PRODUCTION_ENTRY_DETAIL.START_TIME,
                    TSPL_PP_PRODUCTION_ENTRY_DETAIL.END_TIME,TSPL_PP_PRODUCTION_ENTRY_DETAIL.Section_Code,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per,
                    TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG,TSPL_PP_PRODUCTION_ENTRY_DETAIL.Fat_Rate,TSPL_PP_PRODUCTION_ENTRY_DETAIL.Fat_Amt,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Rate,
                    TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Amt,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY,TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.Main_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc as Main_Item_Desc,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.BOM_CODE,
                    TSPL_PP_CONSUMPTION_WITHOUT_BATCH.MAIN_UOM,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.CONSM_QTY,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.LOCATION_CODE as PP_LOCATION_CODE,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.UNIT_CODE,
                    TSPL_PP_CONSUMPTION_WITHOUT_BATCH.FIFO_Cost,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.LIFO_Cost,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.Avg_Cost,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.FAT_Per as PP_FAT_Per,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.FAT_KG as PP_FAT_KG,
                    TSPL_PP_CONSUMPTION_WITHOUT_BATCH.Fat_Amt as PP_Fat_Amt,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.Fat_Rate as PP_Fat_Rate,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.SNF_Per as PP_SNF_Per,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.SNF_KG as PP_SNF_KG,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.SNF_Amt as PP_SNF_Amt,TSPL_PP_CONSUMPTION_WITHOUT_BATCH.SNF_Rate as PP_SNF_Rate
                    from TSPL_PP_PRODUCTION_ENTRY_DETAIL
                    LEFT OUTER JOIN TSPL_PP_PRODUCTION_ENTRY ON TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                    LEFT OUTER JOIN TSPL_PP_CONSUMPTION_WITHOUT_BATCH ON TSPL_PP_CONSUMPTION_WITHOUT_BATCH.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                    left outer join TSPL_PP_COST_WITHOUT_BATCH on TSPL_PP_COST_WITHOUT_BATCH.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                    left outer join TSPL_ITEM_MASTER ON TSPL_PP_CONSUMPTION_WITHOUT_BATCH.Main_ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE
                    left outer join TSPL_LOCATION_MASTER ON TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE
                    WHERE convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,103) <=convert(date,'" + ToDate.Value + "',103) 
                     " + whr + " union all
 select MAX(case when (final.InOut)='I' then final.Document_No end) as Document_No ,
MAX(case when (final.InOut)='I' then final.Document_Date end) as PROD_DATE ,
MAX(case when (final.InOut)='I' then final.BOM_Code end) as BOM_CODE ,
MAX(case when (final.InOut)='I' then final.ITEM_CODE end) as ITEM_CODE ,
MAX(case when (final.InOut)='I' then final.Item_Desc end) as Item_Desc ,
MAX(case when (final.InOut)='I' then final.BATCH_QTY end) as BATCH_QTY ,
MAX(case when (final.InOut)='I' then final.Qty end) as Qty ,
MAX(case when (final.InOut)='I' then final.REJ_HEAD end) as REJ_HEAD ,
MAX(case when (final.InOut)='I' then final.REJ_QTY end) as REJ_QTY ,
MAX(case when (final.InOut)='I' then final.BREAKAGE_HEAD end) as BREAKAGE_HEAD ,
MAX(case when (final.InOut)='I' then final.BREAKAGE_QTY end) as BREAKAGE_QTY ,
MAX(case when (final.InOut)='I' then final.LAB_TESTING end) as LAB_TESTING ,
MAX(case when (final.InOut)='I' then final.START_TIME end) as START_TIME ,
MAX(case when (final.InOut)='I' then final.END_TIME end) as END_TIME ,
MAX(case when (final.InOut)='I' then final.Section_Code end) as Section_Code ,
MAX(case when (final.InOut)='I' then final.Fat_Per end) as Fat_Per ,
MAX(case when (final.InOut)='I' then final.SNF_Per end) as SNF_Per ,
MAX(case when (final.InOut)='I' then final.FAT_KG end) as FAT_KG ,
MAX(case when (final.InOut)='I' then final.SNF_KG end) as SNF_KG ,
MAX(case when (final.InOut)='I' then final.Fat_Rate end) as Fat_Rate ,
MAX(case when (final.InOut)='I' then final.Fat_Amt end) as Fat_Amt ,
MAX(case when (final.InOut)='I' then final.SNF_Rate end) as SNF_Rate ,
MAX(case when (final.InOut)='I' then final.SNF_Amt end) as SNF_Amt ,
MAX(case when (final.InOut)='I' then final.FINAL_PRODUCTION_QTY end) as FINAL_PRODUCTION_QTY ,
MAX(case when (final.InOut)='I' then final.Location_Code end) as Location_Code ,
MAX(case when (final.InOut)='O' then final.Item_Code end) as Main_ITEM_CODE ,
MAX(case when (final.InOut)='O' then final.Item_Desc end) as Main_Item_Desc ,
MAX(case when (final.InOut)='O' then final.BOM_CODE end) as BOM_CODE ,
MAX(case when (final.InOut)='O' then final.UOM end) as MAIN_UOM ,
MAX(case when (final.InOut)='O' then final.Qty end )as CONSM_QTY ,
MAX(case when (final.InOut)='O' then final.Location_Code end) as LOCATION_CODE, 
MAX(case when (final.InOut)='O' then final.UNIT_CODE end) as UNIT_CODE ,
MAX(case when (final.InOut)='O' then final.FIFO_Cost end) as FIFO_Cost ,
MAX(case when (final.InOut)='O' then final.LIFO_Cost end) as LIFO_Cost ,
MAX(case when (final.InOut)='O' then final.Avg_Cost end) as Avg_Cost ,
MAX(case when (final.InOut)='O' then final.PP_FAT_Per end) as PP_FAT_Per ,
MAX(case when (final.InOut)='O' then final.PP_FAT_KG end) as PP_FAT_KG,
MAX(case when (final.InOut)='O' then final.PP_Fat_Amt end) as PP_Fat_Amt ,
MAX(case when (final.InOut)='O' then final.PP_Fat_Rate end )as PP_Fat_Rate ,
MAX(case when (final.InOut)='O' then final.PP_SNF_Per end) as PP_SNF_Per ,
MAX(case when (final.InOut)='O' then final.PP_SNF_KG end) as PP_SNF_KG ,
MAX(case when (final.InOut)='O' then final.PP_SNF_Amt end )as PP_SNF_Amt ,
MAX(case when (final.InOut)='O' then final.PP_SNF_Rate end) as PP_SNF_Rate 
 
from( 
  select TSPL_INVENTORY_MOVEMENT.InOut,TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No,TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date,TSPL_PRODUCTION_UPLOADER_DETAIL.BOM_CODE,TSPL_PRODUCTION_UPLOADER_DETAIL.ITEM_CODE, TSPL_INVENTORY_MOVEMENT.Item_Desc,0 as BATCH_QTY,TSPL_INVENTORY_MOVEMENT.Qty,0 as REJ_HEAD, 0 as REJ_QTY,0 as BREAKAGE_HEAD,0 as BREAKAGE_QTY,'' as LAB_TESTING,'' as START_TIME,'' as END_TIME,'' as Section_Code,TSPL_INVENTORY_MOVEMENT.Fat_Per as FAT_Per,TSPL_INVENTORY_MOVEMENT.SNF_Per as SNF_Per, TSPL_INVENTORY_MOVEMENT.FAT_KG, TSPL_INVENTORY_MOVEMENT.SNF_KG,
  0 as Fat_Rate,0 as Fat_Amt,0 as SNF_Rate,0 as SNF_Amt,0 as FINAL_PRODUCTION_QTY,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_PRODUCTION_UPLOADER_DETAIL.UOM,TSPL_INVENTORY_MOVEMENT.UOM AS UNIT_CODE,TSPL_INVENTORY_MOVEMENT.FIFO_Cost,TSPL_INVENTORY_MOVEMENT.LIFO_Cost,TSPL_INVENTORY_MOVEMENT.Avg_Cost,TSPL_INVENTORY_MOVEMENT.FAT_Per as PP_FAT_Per,TSPL_INVENTORY_MOVEMENT.FAT_KG as PP_FAT_KG,
                    TSPL_INVENTORY_MOVEMENT.Fat_Amt as PP_Fat_Amt,TSPL_INVENTORY_MOVEMENT.Fat_Rate as PP_Fat_Rate,TSPL_INVENTORY_MOVEMENT.SNF_Per as PP_SNF_Per,TSPL_INVENTORY_MOVEMENT.SNF_KG as PP_SNF_KG,TSPL_INVENTORY_MOVEMENT.SNF_Amt as PP_SNF_Amt,TSPL_INVENTORY_MOVEMENT.SNF_Rate as PP_SNF_Rate
  from TSPL_INVENTORY_MOVEMENT
  left join TSPL_PRODUCTION_UPLOADER_DETAIL on TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID = TSPL_INVENTORY_MOVEMENT.Source_Doc_No 
  LEFT OUTER JOIN TSPL_PRODUCTION_UPLOADER_HEAD ON TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No
  WHERE convert(date,TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date ,103) <=convert(date,'" + ToDate.Value + "',103) AND TSPL_INVENTORY_MOVEMENT.Trans_Type='DRY-PRO-UPL' )final GROUP BY Document_No
union all select TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No,TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date,TSPL_PRODUCTION_UPLOADER_DETAIL.BOM_CODE,TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc,0 as BATCH_QTY,TSPL_INVENTORY_MOVEMENT.Qty,0 as REJ_HEAD, 0 as REJ_QTY,0 as BREAKAGE_HEAD,0 as BREAKAGE_QTY,'' as LAB_TESTING,'' as START_TIME,'' as END_TIME,'' as Section_Code,TSPL_INVENTORY_MOVEMENT.Fat_Per as FAT_Per,TSPL_INVENTORY_MOVEMENT.SNF_Per as SNF_Per, TSPL_INVENTORY_MOVEMENT.FAT_KG, TSPL_INVENTORY_MOVEMENT.SNF_KG,
  0 as Fat_Rate,0 as Fat_Amt,0 as SNF_Rate,0 as SNF_Amt,0 as FINAL_PRODUCTION_QTY,TSPL_INVENTORY_MOVEMENT.Location_Code
					
					,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code as Main_ITEM_CODE,TSPL_INVENTORY_MOVEMENT_NEW.Item_Desc as Main_Item_Desc,TSPL_PRODUCTION_UPLOADER_DETAIL.BOM_CODE,
                    TSPL_PRODUCTION_UPLOADER_DETAIL.UOM as MAIN_UOM,TSPL_INVENTORY_MOVEMENT_NEW.Qty as CONSM_QTY,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code as PP_LOCATION_CODE,TSPL_INVENTORY_MOVEMENT_NEW.UOM as UNIT_CODE,
                    TSPL_INVENTORY_MOVEMENT_NEW.FIFO_Cost,TSPL_INVENTORY_MOVEMENT_NEW.LIFO_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.FAT_Per as PP_FAT_Per,TSPL_INVENTORY_MOVEMENT_NEW.FAT_KG as PP_FAT_KG,
                    TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt as PP_Fat_Amt,TSPL_INVENTORY_MOVEMENT_NEW.Fat_Rate as PP_Fat_Rate,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per as PP_SNF_Per,TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as PP_SNF_KG,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt as PP_SNF_Amt,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Rate as PP_SNF_Rate
                    from TSPL_INVENTORY_MOVEMENT_NEW
                   left join TSPL_PRODUCTION_UPLOADER_DETAIL on CAST(TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID AS varchar) = TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No 
                    LEFT OUTER JOIN TSPL_PRODUCTION_UPLOADER_HEAD ON TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No
                     left join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No = TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No
                    WHERE convert(date,TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date ,103) <=convert(date,'" + ToDate.Value + "',103) and TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' and TSPL_INVENTORY_MOVEMENT.InOut='I'"

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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("PROD_ENTRY_CODE").IsVisible = True
        Gv1.Columns("PROD_ENTRY_CODE").Width = 100
        Gv1.Columns("PROD_ENTRY_CODE").HeaderText = "Code"

        Gv1.Columns("PROD_DATE").IsVisible = True
        Gv1.Columns("PROD_DATE").Width = 100
        Gv1.Columns("PROD_DATE").HeaderText = "Date"

        Gv1.Columns("BOM_CODE").IsVisible = True
        Gv1.Columns("BOM_CODE").Width = 100
        Gv1.Columns("BOM_CODE").HeaderText = "Bom Code"

        Gv1.Columns("ITEM_CODE").IsVisible = True
        Gv1.Columns("ITEM_CODE").Width = 100
        Gv1.Columns("ITEM_CODE").HeaderText = "Item Code"

        Gv1.Columns("ITEM_DESCRIPTION").IsVisible = True
        Gv1.Columns("ITEM_DESCRIPTION").Width = 100
        Gv1.Columns("ITEM_DESCRIPTION").HeaderText = "Item Description"

        Gv1.Columns("RECEIPT_QTY").IsVisible = True
        Gv1.Columns("RECEIPT_QTY").Width = 100
        Gv1.Columns("RECEIPT_QTY").HeaderText = "Receipt Quantity"

        Gv1.Columns("Section_Code").IsVisible = True
        Gv1.Columns("Section_Code").Width = 100
        Gv1.Columns("Section_Code").HeaderText = "Section Code"

        Gv1.Columns("FAT_Per").IsVisible = True
        Gv1.Columns("FAT_Per").Width = 100
        Gv1.Columns("FAT_Per").HeaderText = "FAT%"

        Gv1.Columns("FAT_KG").IsVisible = True
        Gv1.Columns("FAT_KG").Width = 100
        Gv1.Columns("FAT_KG").HeaderText = "FAT KG"

        Gv1.Columns("Fat_Rate").IsVisible = True
        Gv1.Columns("Fat_Rate").Width = 100
        Gv1.Columns("Fat_Rate").HeaderText = "FAT Rate"

        Gv1.Columns("Fat_Amt").IsVisible = True
        Gv1.Columns("Fat_Amt").Width = 100
        Gv1.Columns("Fat_Amt").HeaderText = "FAT Amt"

        Gv1.Columns("SNF_Per").IsVisible = True
        Gv1.Columns("SNF_Per").Width = 100
        Gv1.Columns("SNF_Per").HeaderText = "SNF%"

        Gv1.Columns("SNF_KG").IsVisible = True
        Gv1.Columns("SNF_KG").Width = 100
        Gv1.Columns("SNF_KG").HeaderText = "SNF KG"

        Gv1.Columns("SNF_Rate").IsVisible = True
        Gv1.Columns("SNF_Rate").Width = 100
        Gv1.Columns("SNF_Rate").HeaderText = "SNF Rate"

        Gv1.Columns("SNF_Amt").IsVisible = True
        Gv1.Columns("SNF_Amt").Width = 100
        Gv1.Columns("SNF_Amt").HeaderText = "SNF Amt"

        Gv1.Columns("FINAL_PRODUCTION_QTY").IsVisible = True
        Gv1.Columns("FINAL_PRODUCTION_QTY").Width = 100
        Gv1.Columns("FINAL_PRODUCTION_QTY").HeaderText = "Production Quantity"

        Gv1.Columns("LOCATION_CODE").IsVisible = True
        Gv1.Columns("LOCATION_CODE").Width = 100
        Gv1.Columns("LOCATION_CODE").HeaderText = "Location Code"

        Gv1.Columns("Main_ITEM_CODE").IsVisible = True
        Gv1.Columns("Main_ITEM_CODE").Width = 100
        Gv1.Columns("Main_ITEM_CODE").HeaderText = "Item Code"

        Gv1.Columns("Main_Item_Desc").IsVisible = True
        Gv1.Columns("Main_Item_Desc").Width = 100
        Gv1.Columns("Main_Item_Desc").HeaderText = "Item Description"

        Gv1.Columns("MAIN_UOM").IsVisible = True
        Gv1.Columns("MAIN_UOM").Width = 100
        Gv1.Columns("MAIN_UOM").HeaderText = "UOM"

        Gv1.Columns("CONSM_QTY").IsVisible = True
        Gv1.Columns("CONSM_QTY").Width = 100
        Gv1.Columns("CONSM_QTY").HeaderText = "Consumption Quantity"

        Gv1.Columns("PP_LOCATION_CODE").IsVisible = True
        Gv1.Columns("PP_LOCATION_CODE").Width = 100
        Gv1.Columns("PP_LOCATION_CODE").HeaderText = "Location Code"

        Gv1.Columns("UNIT_CODE").IsVisible = True
        Gv1.Columns("UNIT_CODE").Width = 100
        Gv1.Columns("UNIT_CODE").HeaderText = "Unit Code"

        Gv1.Columns("FIFO_Cost").IsVisible = True
        Gv1.Columns("FIFO_Cost").Width = 100
        Gv1.Columns("FIFO_Cost").HeaderText = "FIFO Cost"

        Gv1.Columns("LIFO_Cost").IsVisible = True
        Gv1.Columns("LIFO_Cost").Width = 100
        Gv1.Columns("LIFO_Cost").HeaderText = "LIFO Cost"

        Gv1.Columns("Avg_Cost").IsVisible = True
        Gv1.Columns("Avg_Cost").Width = 100
        Gv1.Columns("Avg_Cost").HeaderText = "Average Cost"

        Gv1.Columns("PP_FAT_Per").IsVisible = True
        Gv1.Columns("PP_FAT_Per").Width = 100
        Gv1.Columns("PP_FAT_Per").HeaderText = "FAT%"

        Gv1.Columns("PP_FAT_KG").IsVisible = True
        Gv1.Columns("PP_FAT_KG").Width = 100
        Gv1.Columns("PP_FAT_KG").HeaderText = "FAT KG"

        Gv1.Columns("PP_Fat_Amt").IsVisible = True
        Gv1.Columns("PP_Fat_Amt").Width = 100
        Gv1.Columns("PP_Fat_Amt").HeaderText = "FAT Amt"

        Gv1.Columns("PP_Fat_Rate").IsVisible = True
        Gv1.Columns("PP_Fat_Rate").Width = 100
        Gv1.Columns("PP_Fat_Rate").HeaderText = "FAT Rate"

        Gv1.Columns("PP_SNF_Per").IsVisible = True
        Gv1.Columns("PP_SNF_Per").Width = 100
        Gv1.Columns("PP_SNF_Per").HeaderText = "SNF%"

        Gv1.Columns("PP_SNF_KG").IsVisible = True
        Gv1.Columns("PP_SNF_KG").Width = 100
        Gv1.Columns("PP_SNF_KG").HeaderText = "SNF KG"

        Gv1.Columns("PP_SNF_Amt").IsVisible = True
        Gv1.Columns("PP_SNF_Amt").Width = 100
        Gv1.Columns("PP_SNF_Amt").HeaderText = "SNF Amt"

        Gv1.Columns("PP_SNF_Rate").IsVisible = True
        Gv1.Columns("PP_SNF_Rate").Width = 100
        Gv1.Columns("PP_SNF_Rate").HeaderText = "SNF Rate"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        View()
    End Sub

    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("Production Data"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_ENTRY_CODE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("PROD_DATE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("BOM_CODE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("LOCATION_CODE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("ITEM_CODE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("ITEM_DESCRIPTION").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("RECEIPT_QTY").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Section_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FAT_Per").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FAT_KG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Fat_Rate").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Fat_Amt").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_Per").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_KG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_Rate").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_Amt").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FINAL_PRODUCTION_QTY").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Consumption Data"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Main_ITEM_CODE").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Main_Item_Desc").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MAIN_UOM").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("CONSM_QTY").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_LOCATION_CODE").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("UNIT_CODE").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("FIFO_Cost").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("LIFO_Cost").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Avg_Cost").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_FAT_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_FAT_KG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_Fat_Amt").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_Fat_Rate").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_SNF_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_SNF_KG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_SNF_Amt").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("PP_SNF_Rate").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select TSPL_ITEM_MASTER.Item_Code as Code , TSPL_ITEM_MASTER.Item_Desc as Description from TSPL_ITEM_MASTER  "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@ItemcodeForProdStatRPT", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
End Class