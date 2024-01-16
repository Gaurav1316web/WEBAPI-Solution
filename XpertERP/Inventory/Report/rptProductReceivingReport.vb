Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptProductReceivingReport
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
        'txtItem.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        'txtTransaction.arrValueMember = Nothing
        'txtItemType.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(chkDetail.Checked = True, "D", "")
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            Dim strItemType As String = ""


            If chkDetail.Checked = True Then
                qry = "  select convert (varchar,Production.Adjustment_Date,103) as [Production Document Date] " &
                          ",Production.[Production Document No] , Production.Item_code as [Item Code], TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,Production.Unit_Code as [UOM] " &
                        ",Production.Item_Quantity  as [Quantity],Production.[Created Date],Production.[Created By] " &
                        ",QC.[QC Document No],QC.[QC Document Date],QC.[QC Quantity],QC.[QC Status] ,QC.[QC By],QC.[QC Posted Date],QC.[QC Posted By] " &
                        ",STORE.[Store Entry Document No],STORE.[Store Entry Document Date],STORE.[Store Quantity],STORE.[Store Entry By],STORE.[Store Entry Posted Date],STORE.[Store Entry Posted By] " &
                        " FROM ( " &
                        " select TSPL_ADJUSTMENT_Detail.Adjustment_No as [Production Document No],TSPL_ADJUSTMENT_Detail.Item_code,TSPL_ADJUSTMENT_Detail.Unit_Code,TSPL_ADJUSTMENT_Detail.Item_Quantity " &
                        ", convert (varchar,TSPL_ADJUSTMENT_HEADER.Created_Date,103) as [Created Date],TSPL_USER_MASTER_Created_By.User_Name as [Created By] " &
                        ",TSPL_ADJUSTMENT_HEADER.Adjustment_Type,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,TSPL_ADJUSTMENT_HEADER.Loc_Code " &
                        " from TSPL_ADJUSTMENT_Detail " &
                        " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_Detail.Adjustment_No " &
                        " Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_ADJUSTMENT_HEADER.Created_By " &
                        " )Production left outer Join  " &
                        " (SELECT TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No as [QC Document No], Convert (varchar,TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date,103) as [QC Document Date] " &
                        " ,TSPL_ADJUSTMENT_DETAIL_QC.Item_Code,TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code,TSPL_ADJUSTMENT_DETAIL_QC.Item_Quantity as [QC Quantity] " &
                        " , TSPL_USER_MASTER_QC_By.User_Name as[QC By] " &
                        " , case when TSPL_ADJUSTMENT_HEADER_QC.Posted = 'Y'  then Convert (varchar,TSPL_ADJUSTMENT_HEADER_QC.Posting_Date,103) else '' end [QC Posted Date] " &
                        " , case when TSPL_ADJUSTMENT_HEADER_QC.Posted = 'Y'  then TSPL_USER_MASTER_QC_Posted_By.User_Name else '' end as [QC Posted By],TSPL_ADJUSTMENT_DETAIL_QC.QC_Status as [QC Status],TSPL_ADJUSTMENT_HEADER_QC.Production_Entry " &
                        " FROM TSPL_ADJUSTMENT_HEADER_QC " &
                        " left outer Join TSPL_ADJUSTMENT_DETAIL_QC on TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No = TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No " &
                        "   Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_QC_By on TSPL_USER_MASTER_QC_By.User_Code = TSPL_ADJUSTMENT_HEADER_QC.Created_By " &
                        "  Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_QC_Posted_By on TSPL_USER_MASTER_QC_Posted_By.User_Code = TSPL_ADJUSTMENT_HEADER_QC.Posted_By " &
                        " )QC on QC.Production_Entry = Production.[Production Document No] And QC.Item_Code=Production.Item_Code and QC.Unit_Code=Production.Unit_Code " &
                        " left outer Join  " &
                        " (SELECT TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo as [Store Entry Document No] " &
                        " , Convert (varchar,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,103) as [Store Entry Document Date] " &
                        " ,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code " &
                        " ,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Quantity as [Store Quantity] " &
                        " , TSPL_USER_MASTER_STORE_By.User_Name as [Store Entry By] " &
                        " , case when TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted = 'Y'  then Convert (varchar,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posting_Date,103) else '' end " &
                        " [Store Entry Posted Date],case when TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted = 'Y'  then TSPL_USER_MASTER_STORE_Posted_By.User_Name else '' end as [Store Entry Posted By] " &
                        ",TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry " &
                        " FROM TSPL_ADJUSTMENT_STORE_ENTRY_HEAD " &
                        " Left outer join TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL On TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo " &
                        " = TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo " &
                        " Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_STORE_By on TSPL_USER_MASTER_STORE_By.User_Code = TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Created_By " &
                        "  Left Outer Join TSPL_USER_MASTER As TSPL_USER_MASTER_STORE_Posted_By On TSPL_USER_MASTER_STORE_Posted_By.User_Code = TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted_By " &
                        " )STORE on STORE.Against_Production_Entry = Production.[Production Document No] And STORE.Item_Code=Production.Item_Code and STORE.Unit_Code=Production.Unit_Code " &
                        " And STORE.Against_Production_Entry = QC.Production_Entry And STORE.Against_Production_Entry_QC = QC.[QC Document No] And STORE.Item_Code=QC.Item_Code " &
                        " And STORE.Unit_Code = QC.Unit_Code left outer join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.ITEM_Code = Production.Item_code " &
                        " where Production.Adjustment_Type = 'PRE'  " &
                        " and Convert (date,Production.Adjustment_Date,103) > =  Convert (date,'" + fromDate.Value + "',103) and Convert (date,Production.Adjustment_Date,103) <=  Convert (date,'" + ToDate.Value + "',103) "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and Production.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                qry += "  order by   Convert (date,Production.Adjustment_Date,103), Production.[Production Document No]   asc "
            Else
                qry = " select TSPL_ADJUSTMENT_Detail.Adjustment_No as [Document No] , TSPL_ADJUSTMENT_Detail.Item_code as [Item Code], TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_ADJUSTMENT_Detail.Unit_Code as [UOM], case when len (isnull  (TSPL_BATCH_ITEM.Batch_No,''))> 0  then TSPL_BATCH_ITEM.Qty  else TSPL_ADJUSTMENT_Detail.Item_Quantity  end as [Quantity] ,isnull (TSPL_BATCH_ITEM.Batch_No,'') as [Batch No], convert (varchar,TSPL_ADJUSTMENT_HEADER.Created_Date,103) as [Created Date],TSPL_USER_MASTER_Created_By.User_Name as [Created By], Convert (varchar,TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date,103) as [QC Date], TSPL_USER_MASTER_QC_By.User_Name as[QC By] , case when TSPL_ADJUSTMENT_HEADER_QC.Posted = 'Y'  then Convert (varchar,TSPL_ADJUSTMENT_HEADER_QC.Posting_Date,103) else '' end [QC Posted Date], TSPL_USER_MASTER_Posted_By.User_Name as [Posted By]  from TSPL_ADJUSTMENT_Detail " &
                      " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_Detail.Adjustment_No " &
          " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_Code = TSPL_ADJUSTMENT_Detail.Item_code  " &
    " left outer Join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_HEADER_QC.Production_Entry = TSPL_ADJUSTMENT_HEADER.Adjustment_No " &
    " Left Outer Join TSPL_BATCH_ITEM on TSPL_BATCH_ITEM.Document_Code =TSPL_ADJUSTMENT_Detail.Adjustment_No and  TSPL_BATCH_ITEM.Item_Code = TSPL_ADJUSTMENT_Detail.Item_Code " &
     " Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_ADJUSTMENT_HEADER.Created_By " &
     "  Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_QC_By on TSPL_USER_MASTER_QC_By.User_Code = TSPL_ADJUSTMENT_HEADER_QC.Created_By " &
     "  Left Outer Join TSPL_USER_MASTER as TSPL_USER_MASTER_Posted_By on TSPL_USER_MASTER_Posted_By.User_Code = TSPL_ADJUSTMENT_HEADER_QC.Posted_By " &
                " where TSPL_ADJUSTMENT_HEADER.Adjustment_Type = 'PRE' " &
    " and Convert (date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) > =  Convert (date,'" + fromDate.Value + "',103) and Convert (date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <=  Convert (date,'" + ToDate.Value + "',103) "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_HEADER.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                qry += " order by   Convert (date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103), TSPL_ADJUSTMENT_Detail.Adjustment_No   asc "
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

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim itemQty As New GridViewSummaryItem("Qty in Stocking UOM", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(itemQty)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.BestFitColumns()
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
    'Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

    '    txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    'End Sub
    'Private Sub txtItem__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String
    '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
    '    txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    'End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSeQC@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductReceivingReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            'If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            'End If
            'If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Product Receiving Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Product Receiving Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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

    'Private Sub txtItemType__My_Click(sender As Object, e As EventArgs)
    '    txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypForBatchItemRep", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    'End Sub
End Class
