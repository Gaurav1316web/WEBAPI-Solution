Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No : BHA/31/08/18-000501 by prabhakar - Create new report 
' Ticket No- BHA/12/11/18-000675 by Sanjay - show stocking Uom for qty in stocking unit
'Ticket No  BHA/23/08/19-000923 ,Sanjay
Public Class rptBatchItemReport1
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
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        txtItem.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            ' Ticket No : BHA/04/02/19-000806 by prabhakar - For Transation Document Status Column
            ' Ticket No : TEC/27/06/19-000570 By Prabhakar
            Dim strItemType As String = ""
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                strItemType = " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
            End If
            qry = " Select * from ( " & _
                  " select  TSPL_BATCH_ITEM.Batch_No  as [Batch No],convert (varchar, TSPL_BATCH_ITEM.Manufacture_Date,103) as [Manufacture Date]," & _
                  " convert(varchar,TSPL_BATCH_ITEM.Expiry_Date,103) as [Expiry Date], TSPL_BATCH_ITEM.MRP , TSPL_BATCH_ITEM.Qty , TSPL_BATCH_ITEM.UOM , TSPL_BATCH_ITEM.Item_Code as [Item Code] , " & _
                  " TSPL_ITEM_MASTER.Item_Desc as [Item Desc] , TSPL_BATCH_ITEM.Document_Code as [Document Code] , convert(varchar,TSPL_BATCH_ITEM.Document_Date,103) as [Document Date] , " & _
                  " TSPL_BATCH_ITEM.Document_Type as [Document Type Code],TSPL_INVENTORY_SOURCE_CODE.Name as [Document Type Name],   " & _
                  " Case " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'PS-SH' then iif ( convert (varchar, TSPL_SD_SHIPMENT_HEAD.STATUS)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'IC-AD' then iif ( convert (varchar, TSPL_ADJUSTMENT_HEADER.Posted)  = 'Y'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'FS-SH' then iif ( convert (varchar, TSPL_SD_SHIPMENT_HEAD.STATUS)  = '1'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'PROD_ENTRY' then iif ( convert (varchar, TSPL_PP_PRODUCTION_ENTRY.Posted)  = '1'  , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'Disassembly' then iif ( convert (varchar, TSPL_PROD_ASSEMBLIES.Posted)  = '1'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'FS-SR' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'MCC-MSALE' then iif ( convert (varchar, TSPL_SD_SHIPMENT_HEAD.STATUS)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'MCC-MSR' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1'   , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'PRO-ENT-FQC' then iif ( convert (varchar, TSPL_PE_FINALQC_HEAD.Posted)  = '1'  , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'PS-SR' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'Transfer' then iif ( convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'EX_SALE_IN' then iif ( convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'MT_SALE_IN' then iif ( convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'SD-CSATRANS' then iif ( convert (varchar, TSPL_CSA_TRANSFER_HEAD.STATUS)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'SD-CSATRANS-RETURN' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'JW-IN' then iif ( convert (varchar, TSPL_ADJUSTMENT_HEADER.Posted)  = 'Y'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'ITransfer' then iif ( convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'ScrapIn' then iif ( convert (varchar, TSPL_SCRAPINVOICE_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'ISSTRAN' then iif ( convert (varchar, TSPL_IssueReturn_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM.Document_Type = 'MS-SR' then iif ( convert (varchar, TSPL_SCRAPSALE_HEAD_Return.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " else 'NA' end as [Document Status],  " & _
                  " " & _
                  " TSPL_BATCH_ITEM.In_Out_Type as [In Out Type], TSPL_BATCH_ITEM.Against_Inv_Movement_Trans_Id as [Against Inv Movement Trans Id], " & _
                  " TSPL_BATCH_ITEM.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Location_Desc, TSPL_BATCH_ITEM.Manual_BatchNo as [Manual Batch No] " & _
                " ,SU.UOM_CODE as [Stocking UOM]" & _
                " ,round((case when TSPL_BATCH_ITEM.In_Out_Type='O' then (-1 * isnull(TSPL_BATCH_ITEM.Qty,0)) else isnull(TSPL_BATCH_ITEM.Qty,0) end *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)),2) as [Qty in Stocking UOM] " & _
                " from TSPL_BATCH_ITEM " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BATCH_ITEM.Item_Code " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_BATCH_ITEM.Location_Code  " & _
                  " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.Code = TSPL_BATCH_ITEM.Document_Type " & _
                  " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=TSPL_BATCH_ITEM.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM.UOM" & _
                  " left join ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL WHERE Stocking_Unit='Y') SU ON SU.item_code=TSPL_BATCH_ITEM.Item_Code" & _
                  "  " & _
                  " Left Outer Join TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_BATCH_ITEM.Document_Code " & _
                  " LEft Outer Join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_no = TSPL_BATCH_ITEM.Document_Code  " & _
                  " Left Outer Join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = TSPL_BATCH_ITEM.Document_Code  " & _
                  " Left Outer Join TSPL_PROD_ASSEMBLIES on TSPL_PROD_ASSEMBLIES.CODE = TSPL_BATCH_ITEM.Document_Code  " & _
                  " Left Outer Join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code = TSPL_BATCH_ITEM.Document_Code  " & _
                  " Left Outer Join TSPL_PE_FINALQC_HEAD on TSPL_PE_FINALQC_HEAD.QC_Code = TSPL_BATCH_ITEM.Document_Code  " & _
                  " Left Outer Join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_BATCH_ITEM.Document_Code   " & _
                  " Left Outer Join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_BATCH_ITEM.Document_Code   " & _
                  " Left Outer Join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE = TSPL_BATCH_ITEM.Document_Code   " & _
                  " Left Outer Join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_no = TSPL_BATCH_ITEM.Document_Code   " & _
                  " Left Outer Join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.Invoice_No = TSPL_BATCH_ITEM.Document_Code   " & _
                  " Left Outer Join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No = TSPL_BATCH_ITEM.Document_Code   " & _
                  " Left Outer Join TSPL_SCRAPSALE_HEAD_Return on TSPL_SCRAPSALE_HEAD_Return.Document_No = TSPL_BATCH_ITEM.Document_Code   " & _
                  "  " & _
                  " where convert(date,TSPL_BATCH_ITEM.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_BATCH_ITEM.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strItemType + "  " & _
                  " Union All " & _
                  " " & _
                  "  select  TSPL_BATCH_ITEM_New.Batch_No  as [Batch No],'' as [Manufacture Date]," & _
                  " '' as [Expiry Date], 0 as MRP , TSPL_BATCH_ITEM_New.Qty , TSPL_BATCH_ITEM_New.UOM , TSPL_BATCH_ITEM_New.Item_Code as [Item Code] , " & _
                  " TSPL_ITEM_MASTER.Item_Desc as [Item Desc] , TSPL_BATCH_ITEM_New.Document_Code as [Document Code] , convert(varchar,TSPL_BATCH_ITEM_New.Document_Date,103) as [Document Date] , " & _
                  " TSPL_BATCH_ITEM_New.Document_Type as [Document Type Code],TSPL_INVENTORY_SOURCE_CODE.Name as [Document Type Name],   " & _
                  " Case " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'PS-SH' then iif ( convert (varchar, TSPL_SD_SHIPMENT_HEAD.STATUS)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'IC-AD' then iif ( convert (varchar, TSPL_ADJUSTMENT_HEADER.Posted)  = 'Y'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'FS-SH' then iif ( convert (varchar, TSPL_SD_SHIPMENT_HEAD.STATUS)  = '1'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'PROD_ENTRY' then iif ( convert (varchar, TSPL_PP_PRODUCTION_ENTRY.Posted)  = '1'  , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'Disassembly' then iif ( convert (varchar, TSPL_PROD_ASSEMBLIES.Posted)  = '1'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'FS-SR' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'MCC-MSALE' then iif ( convert (varchar, TSPL_SD_SHIPMENT_HEAD.STATUS)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'MCC-MSR' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1'   , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'PRO-ENT-FQC' then iif ( convert (varchar, TSPL_PE_FINALQC_HEAD.Posted)  = '1'  , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'PS-SR' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'Transfer' then iif ( convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'EX_SALE_IN' then iif ( convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'MT_SALE_IN' then iif ( convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'SD-CSATRANS' then iif ( convert (varchar, TSPL_CSA_TRANSFER_HEAD.STATUS)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'SD-CSATRANS-RETURN' then iif ( convert (varchar, TSPL_SD_SALE_RETURN_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'JW-IN' then iif ( convert (varchar, TSPL_ADJUSTMENT_HEADER.Posted)  = 'Y'  , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'ITransfer' then iif ( convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'ScrapIn' then iif ( convert (varchar, TSPL_SCRAPINVOICE_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'ISSTRAN' then iif ( convert (varchar, TSPL_IssueReturn_HEAD.Status)  = '1' , 'Posted' , 'Not Posted' )  " & _
                  " when TSPL_BATCH_ITEM_New.Document_Type = 'MS-SR' then iif ( convert (varchar, TSPL_SCRAPSALE_HEAD_Return.Status)  = '1' , 'Posted' , 'Not Posted' )   " & _
                  " else 'NA' end as [Document Status],  " & _
                  " " & _
                  " TSPL_BATCH_ITEM_New.In_Out_Type as [In Out Type], TSPL_BATCH_ITEM_New.Against_Inv_Movement_New_Trans_Id as [Against Inv Movement Trans Id], " & _
                  " TSPL_BATCH_ITEM_New.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Location_Desc, '' as [Manual Batch No] " & _
                " ,SU.UOM_CODE as [Stocking UOM]" & _
                " ,round((case when TSPL_BATCH_ITEM_New.In_Out_Type='O' then (-1 * isnull(TSPL_BATCH_ITEM_New.Qty,0)) else isnull(TSPL_BATCH_ITEM_New.Qty,0) end *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)),2) as [Qty in Stocking UOM] " & _
                " from TSPL_BATCH_ITEM_New " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BATCH_ITEM_New.Item_Code " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_BATCH_ITEM_New.Location_Code  " & _
                  " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.Code = TSPL_BATCH_ITEM_New.Document_Type " & _
                  " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=TSPL_BATCH_ITEM_New.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM_New.UOM" & _
                  " left join ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL WHERE Stocking_Unit='Y') SU ON SU.item_code=TSPL_BATCH_ITEM_New.Item_Code" & _
                  "  " & _
                  " Left Outer Join TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_BATCH_ITEM_New.Document_Code " & _
                  " LEft Outer Join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_no = TSPL_BATCH_ITEM_New.Document_Code  " & _
                  " Left Outer Join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = TSPL_BATCH_ITEM_New.Document_Code  " & _
                  " Left Outer Join TSPL_PROD_ASSEMBLIES on TSPL_PROD_ASSEMBLIES.CODE = TSPL_BATCH_ITEM_New.Document_Code  " & _
                  " Left Outer Join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code = TSPL_BATCH_ITEM_New.Document_Code  " & _
                  " Left Outer Join TSPL_PE_FINALQC_HEAD on TSPL_PE_FINALQC_HEAD.QC_Code = TSPL_BATCH_ITEM_New.Document_Code  " & _
                  " Left Outer Join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_BATCH_ITEM_New.Document_Code   " & _
                  " Left Outer Join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_BATCH_ITEM_New.Document_Code   " & _
                  " Left Outer Join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE = TSPL_BATCH_ITEM_New.Document_Code   " & _
                  " Left Outer Join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_no = TSPL_BATCH_ITEM_New.Document_Code   " & _
                  " Left Outer Join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.Invoice_No = TSPL_BATCH_ITEM_New.Document_Code   " & _
                  " Left Outer Join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No = TSPL_BATCH_ITEM_New.Document_Code   " & _
                  " Left Outer Join TSPL_SCRAPSALE_HEAD_Return on TSPL_SCRAPSALE_HEAD_Return.Document_No = TSPL_BATCH_ITEM_New.Document_Code   " & _
                  "  " & _
                  " where convert(date,TSPL_BATCH_ITEM_New.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_BATCH_ITEM_New.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strItemType + "  " & _
                  " ) Final where 2= 2 "


            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and Final.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and Final.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and Final.[Document Type Code] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
            End If

            'If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
            '    qry += " and Final.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
            'End If

            If ChkPosted.IsChecked = True Then
                qry += " and Final.[Document Status] = 'Posted' "
            ElseIf ChkUnPosted.IsChecked = True Then
                qry += " and Final.[Document Status] = 'Not Posted'"
            End If

            qry += " order by   Final.[Batch No]  asc "

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
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim itemQty As New GridViewSummaryItem("Qty in Stocking UOM", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemQty)
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

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
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBatchItemReport1 & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Batch Item Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Batch Item Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypForBatchItemRep", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub
End Class
