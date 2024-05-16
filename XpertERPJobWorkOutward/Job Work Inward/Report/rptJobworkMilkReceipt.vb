'Sanjay Ticket No-  BHA/13/11/18-000676 Date  13/Nov/2018
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class rptJobworkMilkReceipt
    Inherits FrmMainTranScreen
    Dim strQry As String = ""
    Dim IsFormLoad As Boolean = False
    Dim SummaryData As Boolean = False
    Dim dt As DataTable
    Private Sub rptJobworkMilkReceipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IsFormLoad = False
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
        IsFormLoad = True

    End Sub
    Sub Reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
        btnGo.Enabled = True
        btnBack.Enabled = False
        'btnReset.Enabled = False
        RadSplitButton1.Enabled = False
        txtCustomer.arrValueMember = Nothing
        txtMultLocation.arrValueMember = Nothing
        'LoadDisplayMethod()
        LoadTypes()
        SummaryData = False
    End Sub
    'Sub LoadDisplayMethod()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = "Summary"
    '    dr("Name") = "Summary"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Detail"
    '    dr("Name") = "Detail"
    '    dt.Rows.Add(dr)

    '    ddlDisplayType.DataSource = dt
    '    ddlDisplayType.ValueMember = "Code"
    '    ddlDisplayType.DisplayMember = "Name"
    '    ddlDisplayType.SelectedValue = "Summary"
    'End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        TemplateGridview = Gv1
        loaddata()
    End Sub
    Sub FormatGrid()
        Try

            Dim summaryItem As New GridViewSummaryItem()
        'Gv1.TableElement.TableHeaderHeight = 150
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).BestFit()
        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim itemQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(itemQty)
        Dim itemFATKG As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(itemFATKG)
        Dim itemSNFKG As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemSNFKG)
            ' [TS KG]
            Dim itemTSKG As New GridViewSummaryItem("TS KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemTSKG)

        Dim itemEstimatedFATKG As New GridViewSummaryItem("Estimated FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(itemEstimatedFATKG)
        Dim itemEstimatedSNFKG As New GridViewSummaryItem("Estimated SNF KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemEstimatedSNFKG)
            'Estimated TS KG
            Dim itemEstimatedTSKG As New GridViewSummaryItem("Estimated TS KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemEstimatedTSKG)

            Dim FATPer As New GridViewSummaryItem("FAT %", "{0:F2}", GridAggregateFunction.Avg)
            summaryRowItem.Add(FATPer)
            Dim SNFPer As New GridViewSummaryItem("SNF %", "{0:F2}", GridAggregateFunction.Avg)
            summaryRowItem.Add(SNFPer)
            Dim TSPer As New GridViewSummaryItem("TS%", "{0:F2}", GridAggregateFunction.Avg)
            summaryRowItem.Add(TSPer)

            If clsCommon.CompairString(ddlReportType.Text, "Doc Wise") = CompairStringResult.Equal Then

                Dim FATPerG As New GridViewSummaryItem("GATE FAT%", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(FATPerG)
                Dim SNFPerG As New GridViewSummaryItem("GATE SNF%", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(SNFPerG)
                Dim TSPerG As New GridViewSummaryItem("GATE TS%", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(TSPerG)
                

                Dim itemFATKGG As New GridViewSummaryItem("GATE FAT KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemFATKGG)
                Dim itemSNFKGG As New GridViewSummaryItem("GATE SNF KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemSNFKGG)
                Dim itemTSKGG As New GridViewSummaryItem("GATE TS KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemTSKGG)


                Dim FATPerD As New GridViewSummaryItem("DIFF FAT", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(FATPerD)
                Dim SNFPerD As New GridViewSummaryItem("DIFF SNF", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(SNFPerD)
                Dim TSPerD As New GridViewSummaryItem("DIFF TS%", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(TSPerD)


                Dim itemFATKGD As New GridViewSummaryItem("DIFF FAT KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemFATKGD)
                Dim itemSNFKGD As New GridViewSummaryItem("DIFF SNF KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemSNFKGD)
                Dim itemTSKGD As New GridViewSummaryItem("DIFF TS KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemTSKGD)
            End If

        Gv1.ShowGroupPanel = False
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Jobwork Milk Receipt Report")

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtMultLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Jobwork Milk Receipt Report", Gv1, arrHeader, "Jobwork Milk Receipt Report")
            End If
           
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : ERO/05/06/19-000632 by Prabhakar -  add [TS KG] and [TS%] Column
    '====update by preeti gupta Against ticket no[ERO/16/07/19-000950]
    Sub loaddata()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim dtqry As DataTable = Nothing
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim WhrCls As String = ""
            WhrCls = " and 2=2 "

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_LOCATION_MASTER.Jobwork_Vendor in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If
            ' Ticket No : ERO/05/06/19-000632 By Prabhakar - Add Total solid kg [TS KG]
            WhrCls += " and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >= convert(date,('" + txtfDate.Value + "'),103) and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,('" & txtToDate.Value & "'),103)"

            If clsCommon.CompairString(ddlReportType.Text, "All") = CompairStringResult.Equal Then
                qry = "select convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) as [Date],TSPL_LOCATION_MASTER.Jobwork_Vendor as [Vendor Code],TSPL_VENDOR_master.Vendor_Name as [Vendor Name]" & _
                 ",TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight as [Qty], " & _
                 "'" + clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GrossWeightUnit, clsFixedParameterCode.GrossWeightUnit, Nothing)) + "' as [UOM],xx.FAT_PER as [FAT %],xx.SNF_PER as [SNF %],xx.FAT_PER +xx.SNF_PER as [TS%]" & _
                ",xx.FAT_KG as [FAT KG],xx.SNF_KG as [SNF KG],isnull(xx.FAT_KG,0) + isnull( xx.SNF_KG,0)  as [TS KG] " & _
                ",xx.Estimated_FAT_KG as [Estimated FAT KG],xx.Estimated_SNF_KG as [Estimated SNF KG],isnull(xx.Estimated_FAT_KG,0)+ isnull(xx.Estimated_SNF_KG,0) as [Estimated TS KG]" & _
                " from TSPL_GENERAL_WEIGHMENT_DETAIL " & _
                " left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code " & _
                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code " & _
                " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor " & _
               " left join (select TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_PER ,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_PER" & _
                " from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no" & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No " & _
                " where TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork=1 and TSPL_GENERAL_WEIGHMENT_DETAIL.Posted=1 "
                qry += WhrCls
                qry += "order by TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Vendor Wise") = CompairStringResult.Equal Then
                qry = "select TSPL_LOCATION_MASTER.Jobwork_Vendor as [Vendor Code],TSPL_VENDOR_master.Vendor_Name as [Vendor Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code as [Item Code],tspl_item_master.Item_Desc as [Item Name],sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight) as [Qty],  " & _
                  "'" + clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GrossWeightUnit, clsFixedParameterCode.GrossWeightUnit, Nothing)) + "' as [UOM] " & _
                 ",sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG],isnull(sum(xx.FAT_KG),0) + isnull( sum(xx.SNF_KG),0)  as [TS KG] " & _
                 ",sum(xx.Estimated_FAT_KG) as [Estimated FAT KG],sum(xx.Estimated_SNF_KG) as [Estimated SNF KG],isnull(sum(xx.Estimated_FAT_KG),0) + isnull(sum(xx.Estimated_SNF_KG),0) as [Estimated TS KG] from TSPL_GENERAL_WEIGHMENT_DETAIL  " & _
                 " left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code  " & _
                 " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code  " & _
                 " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor  " & _
                 " left join (select TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG" & _
                " from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no" & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No " & _
                " where  TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork = 1 And TSPL_GENERAL_WEIGHMENT_DETAIL.Posted = 1 "
                qry += WhrCls
                qry += " group by TSPL_LOCATION_MASTER.Jobwork_Vendor,TSPL_VENDOR_master.Vendor_Name " & _
                  ",TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code,tspl_item_master.Item_Desc order by TSPL_LOCATION_MASTER.Jobwork_Vendor "
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Location Wise") = CompairStringResult.Equal Then
                qry = "select TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name] " & _
                  ",TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code as [Item Code],tspl_item_master.Item_Desc as [Item Name],sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight) as [Qty],  " & _
                   "'" + clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GrossWeightUnit, clsFixedParameterCode.GrossWeightUnit, Nothing)) + "' as [UOM] " & _
                  ",sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG],isnull(sum(xx.FAT_KG),0) + isnull( sum(xx.SNF_KG),0)  as [TS KG] " & _
                  ",sum(xx.Estimated_FAT_KG) as [Estimated FAT KG],sum(xx.Estimated_SNF_KG) as [Estimated SNF KG],isnull(sum(xx.Estimated_FAT_KG),0) +isnull(sum(xx.Estimated_SNF_KG),0)  as [Estimated TS KG] from TSPL_GENERAL_WEIGHMENT_DETAIL  " & _
                  " left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code  " & _
                  " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code  " & _
                  " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor  " & _
                  " left join (select TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG" & _
                " from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no" & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No " & _
" where  TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork = 1 And TSPL_GENERAL_WEIGHMENT_DETAIL.Posted = 1 "
                qry += WhrCls
                qry += " group by TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code,TSPL_LOCATION_MASTER.Location_Desc " & _
                  ",TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code,tspl_item_master.Item_Desc order by TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Date Wise") = CompairStringResult.Equal Then
                qry = "select convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) as [Date]" & _
                 ",TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code as [Item Code],tspl_item_master.Item_Desc as [Item Name],sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight) as [Qty],  " & _
                  "'" + clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GrossWeightUnit, clsFixedParameterCode.GrossWeightUnit, Nothing)) + "' as [UOM] " & _
                 ",sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG], isnull(sum(xx.FAT_KG),0) + isnull( sum(xx.SNF_KG),0)  as [TS KG] " & _
                 ",sum(xx.Estimated_FAT_KG) as [Estimated FAT KG],sum(xx.Estimated_SNF_KG) as [Estimated SNF KG],isnull(sum(xx.Estimated_FAT_KG),0)+ isnull(sum(xx.Estimated_SNF_KG),0) as [Estimated TS KG] from TSPL_GENERAL_WEIGHMENT_DETAIL  " & _
                 " left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code  " & _
                 " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code  " & _
                 " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor  " & _
                " left join (select TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG" & _
                " from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no" & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No " & _
   " where  TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork = 1 And TSPL_GENERAL_WEIGHMENT_DETAIL.Posted = 1 "
                qry += WhrCls
                qry += " group by TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date " & _
                  ",TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code,tspl_item_master.Item_Desc  "
                qry += "order by TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Doc Wise") = CompairStringResult.Equal Then 'added by preeti gupta Against ticket no[ERO/22/04/19-000565],Ticket No-ERO/14/11/19-001103
                qry = "select row_number() over ( order by Weighment_Date) as SNO ,tspl_gate_entry_details.challan_no as [Challan No],convert(varchar,tspl_gate_entry_details.challan_Date,103) as [Challan Date],convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) as [Doc Date] " & _
                ",TSPL_GENERAL_WEIGHMENT_DETAIL.Vehicle_No_Manual as [Tanker No], 'Kg' as [UOM] " & _
                ",TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight as [Qty] " & _
                ",xx.FAT_PER as [FAT%],xx.SNF_PER as [SNF%], isnull(xx.FAT_PER,0)+ isnull(xx.SNF_PER,0) as [TS%] " & _
                ",xx.FAT_KG as [FAT KG],xx.SNF_KG as [SNF KG], isnull(xx.FAT_KG,0) + isnull(xx.SNF_KG,0) as [TS KG] " & _
                ",tspl_gate_entry_details.fat_per as [GATE FAT%] " & _
                ",tspl_gate_entry_details.snf_Per as [GATE SNF%] " & _
                ",tspl_gate_entry_details.fat_per+tspl_gate_entry_details.snf_Per AS [GATE TS%] " & _
                ",Cast(tspl_gate_entry_details.fat_per*tspl_gate_entry_details.Qty_In_Kg/100 as decimal(18,2)) as [GATE FAT KG] " & _
                ",Cast(tspl_gate_entry_details.snf_Per*tspl_gate_entry_details.Qty_In_Kg/100 as decimal(18,2)) AS [GATE SNF KG] " & _
                ",cast((tspl_gate_entry_details.fat_per*tspl_gate_entry_details.Qty_In_Kg/100)+(tspl_gate_entry_details.snf_Per*tspl_gate_entry_details.Qty_In_Kg/100) as decimal(18,2)) AS [GATE TS KG]" & _
                ", cast(isnull(tspl_gate_entry_details.fat_per,0)-isnull(xx.FAT_PER,0) as decimal(18,2)) as [DIFF FAT] " & _
                ", cast(isnull(tspl_gate_entry_details.snf_Per,0)-isnull(xx.SNF_PER,0) as decimal(18,2)) as [DIFF SNF] " & _
                ", cast((isnull(tspl_gate_entry_details.fat_per,0)+isnull(tspl_gate_entry_details.snf_Per,0))-(isnull(xx.FAT_PER,0)+ isnull(xx.SNF_PER,0)) as decimal(18,2)) [DIFF TS%] " & _
                ", Cast(tspl_gate_entry_details.fat_per*tspl_gate_entry_details.Qty_In_Kg/100 as decimal(18,2)) - isnull(xx.FAT_KG,0) as [DIFF FAT KG] " & _
                ",Cast(tspl_gate_entry_details.snf_Per*tspl_gate_entry_details.Qty_In_Kg/100 as decimal(18,2)) - isnull(xx.SNF_KG,0) as [DIFF SNF KG] " & _
                ",cast((tspl_gate_entry_details.fat_per*tspl_gate_entry_details.Qty_In_Kg/100)+(tspl_gate_entry_details.snf_Per*tspl_gate_entry_details.Qty_In_Kg/100) as decimal(18,2)) - (isnull(xx.FAT_KG,0) + isnull(xx.SNF_KG,0))as [DIFF TS KG]" & _
                " from TSPL_GENERAL_WEIGHMENT_DETAIL  " & _
                "left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code  left join TSPL_LOCATION_MASTER on  " & _
                "TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code  left join TSPL_VENDOR_master on  " & _
                "TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor  left join  " & _
                "(select TSPL_JWI_ESTIMATION_HEAD.Document_NO,TSPL_JWI_ESTIMATION_HEAD.Document_Date, " & _
                "TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_PER,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_PER " & _
                ",TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG ,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG  " & _
                " from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no where  " & _
                " TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on  " & _
                " xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No left join tspl_gate_entry_details on tspl_gate_entry_details.gate_entry_no=TSPL_GENERAL_WEIGHMENT_DETAIL.gate_entry_no where TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork=1  " & _
                " and TSPL_GENERAL_WEIGHMENT_DETAIL.Posted=1   "

                qry += WhrCls
                qry += " order by TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date"
            End If

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry
            SummaryData = True
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.BestFitColumns()
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
            ReStoreGridLayout()
            'btnGo.Enabled = False
            'btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            btnGo.Enabled = True
            btnBack.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub




    Private Sub txtMultLocation__My_Click(sender As Object, e As EventArgs) Handles txtMultLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtMultLocation.arrValueMember, txtMultLocation.arrDispalyMember)
    End Sub


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Vendor Wise")
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Date Wise")
        dt.Rows.Add("Doc Wise")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
        ddlReportType.SelectedValue = "All"
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        loaddata()
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub
    Sub DrillDown()
        Try
            If SummaryData = False Then
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim dtqry As DataTable = Nothing
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "

            If clsCommon.CompairString(ddlReportType.Text, "All") = CompairStringResult.Equal Then
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value) + "' "
                WhrCls += " and TSPL_LOCATION_MASTER.Jobwork_Vendor ='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Code").Value) + "' "
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                WhrCls += " and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >= convert(date,('" + clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) + "'),103) and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,('" & clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) & "'),103)"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Vendor Wise") = CompairStringResult.Equal Then
                WhrCls += " and TSPL_LOCATION_MASTER.Jobwork_Vendor ='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Code").Value) + "' "
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
                End If
                WhrCls += " and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >= convert(date,('" + txtfDate.Value + "'),103) and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,('" & txtToDate.Value & "'),103)"

            ElseIf clsCommon.CompairString(ddlReportType.Text, "Location Wise") = CompairStringResult.Equal Then
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value) + "' "
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_LOCATION_MASTER.Jobwork_Vendor in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                WhrCls += " and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >= convert(date,('" + txtfDate.Value + "'),103) and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,('" & txtToDate.Value & "'),103)"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Date Wise") = CompairStringResult.Equal Then
                WhrCls += " and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >= convert(date,('" + clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) + "'),103) and convert(date,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,('" & clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) & "'),103)"
                WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_LOCATION_MASTER.Jobwork_Vendor in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
                End If
            End If

            qry = "select TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No as [Doc No], " & _
                "convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) as [Doc Date] " & _
                ",TSPL_GENERAL_WEIGHMENT_DETAIL.Vehicle_No_Manual as [Tanker No] " & _
                ",TSPL_LOCATION_MASTER.Jobwork_Vendor as [Vendor Code],TSPL_VENDOR_master.Vendor_Name as [Vendor Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code  " & _
                " as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code as [Item Code] " & _
                ",tspl_item_master.Item_Desc as [Item Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight as [Qty], 'Kg' as [UOM] " & _
                ",xx.Document_NO as [JWE No],xx.Document_Date as [JWE Date],xx.FAT_PER as [FAT%],xx.SNF_PER as [SNF%] " & _
                ",xx.FAT_KG as [FAT KG],xx.SNF_KG as [SNF KG],xx.Estimated_FAT_KG as [Estimated FAT KG],xx.Estimated_SNF_KG as [Estimated SNF KG] from TSPL_GENERAL_WEIGHMENT_DETAIL  " & _
                "left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code  left join TSPL_LOCATION_MASTER on  " & _
                "TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code  left join TSPL_VENDOR_master on  " & _
                "TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor  left join  " & _
                "(select TSPL_JWI_ESTIMATION_HEAD.Document_NO,TSPL_JWI_ESTIMATION_HEAD.Document_Date, " & _
                "TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_PER,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_PER " & _
                ",TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG ,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG  " & _
                " from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no where  " & _
                " TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on  " & _
                " xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No  where TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork=1  " & _
                " and TSPL_GENERAL_WEIGHMENT_DETAIL.Posted=1   "

            qry += WhrCls
            qry += " order by TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date"

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry
            SummaryData = False
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2

            'btnGo.Enabled = False
            'btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
            btnGo.Enabled = False
            btnBack.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Jobwork Milk Receipt Report")

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If clsCommon.myLen(ddlReportType.Text) > 0 Then
                    arrHeader.Add("Report Type : " + ddlReportType.Text)
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtMultLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Jobwork Milk Receipt Report", Gv1, arrHeader, "Jobwork Milk Receipt Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Deleted successfully", "Information", Me.Text)
        End If
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
End Class
