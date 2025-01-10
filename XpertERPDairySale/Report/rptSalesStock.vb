Imports common
Public Class rptSalesStock
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Stock Journal")
        dt.Rows.Add("Stock Summary")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub
    Sub reset()
        txtfromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.Value = ""
        txtMultiItem.arrValueMember = Nothing
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        gvData.Columns.Clear()
        gvData.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Private Sub rptSalesStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
        LoadTypes()
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim whrcls As String = ""
            whrcls = "    Location_Type='Physical' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ") "
            End If
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code],IsSubLocationWise as [Is Sub Location Wise] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code "
            txtLocation.Value = clsCommon.ShowSelectForm("Location", qry, "Code", " ", txtLocation.Value, "code", isButtonClicked)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal Print As Boolean)
        Try
            Dim Qry As String = ""
            Dim Whr As String = ""
            Dim dt As DataTable = Nothing

            If txtMultiItem.arrValueMember IsNot Nothing AndAlso txtMultiItem.arrValueMember.Count > 0 Then
                Whr = " and Item_Code in (" + clsCommon.GetMulcallString(txtMultiItem.arrValueMember) + ")"
            End If
            If txtMultiStructureCode.arrValueMember IsNot Nothing AndAlso txtMultiStructureCode.arrValueMember.Count > 0 Then
                Whr = " and TSPL_ITEM_MASTER.Structure_Code in (" + clsCommon.GetMulcallString(txtMultiStructureCode.arrValueMember) + ")"
            End If
            If ddlReportType.SelectedValue = "Stock Journal" Then
                If clsCommon.myLen(txtLocation.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select location")
                    Exit Sub
                End If
                Qry = "Select SUM([INWARDQTYReportUom]) as [INWARDQTYReportUom],Sum([OUTWARDQTYReportUom]) as [OUTWARDQTYReportUom],Max(uom_code)UOM,Sum(Qty) as Qty,Item_Code,max(Item_Desc) as Item_Desc ,Sum(INWARDQTY) as INWARDQTY,SUM(OUTWARDQTY) as OUTWARDQTY,max(From_Location) as From_Location,max(To_Location) as To_Location,MAX(location_desc) as location_desc,max(structure_code) AS structure_code,'" + clsCommon.GetPrintDate(txtfromDate.Value, "dd-MMM-yyyy") + "' As  From_Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'  as To_Date,MAX(City_code) as City_code ,Max(INUOM)INUOM from 
                If txtMultiItem.arrValueMember IsNot Nothing AndAlso txtMultiItem.arrValueMember.Count > 0 Then
                    Whr = " and Item_Code in (" + clsCommon.GetMulcallString(txtMultiItem.arrValueMember) + ")"
                End If
                Qry = "Select SUM([INWARDQTYReportUom]) as [INWARDQTYReportUom],Sum([OUTWARDQTYReportUom]) as [OUTWARDQTYReportUom],Sum(Qty) as Qty,Item_Code,max(Item_Desc) as Item_Desc ,Max(uom_code)UOM,Sum(INWARDQTY) as INWARDQTY,SUM(OUTWARDQTY) as OUTWARDQTY,max(From_Location) as From_Location,max(To_Location) as To_Location,MAX(location_desc) as location_desc,max(structure_code) AS structure_code,'" + clsCommon.GetPrintDate(txtfromDate.Value, "dd-MMM-yyyy") + "' As  From_Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'  as To_Date,MAX(City_code) as City_code ,Max(INUOM)INUOM from 
                (select TSPL_ITEM_master.structure_code,tspl_location_master.location_desc,City_code,TSPL_TRANSFER_ORDER_HEAD.document_no,TSPL_INVENTORY_MOVEMENT.Trans_Type,source_doc_date,TSPL_TRANSFER_ORDER_HEAD.From_Location,TSPL_TRANSFER_ORDER_HEAD.To_Location,Inout,ItemConvinUOM.Conversion_Factor,ItemConvReportUOM.uom_code,
            Case when Inout='I' then cast((TSPL_INVENTORY_MOVEMENT.Qty*ItemConvinUOM.Conversion_Factor/ItemConvReportUOM.Conversion_Factor) as Decimal(18,2)) else 0 end as [INWARDQTYReportUom],Case when Inout='O' then cast((TSPL_INVENTORY_MOVEMENT.Qty*ItemConvinUOM.Conversion_Factor/ItemConvReportUOM.Conversion_Factor) as Decimal(18,2)) else 0 end as [OUTWARDQTYReportUom],TSPL_INVENTORY_MOVEMENT.item_code,TSPL_ITEM_master.item_desc,Case when TSPL_INVENTORY_MOVEMENT.Inout='I' then TSPL_INVENTORY_MOVEMENT.qty else 0 end as INWARDQTY ,Case when TSPL_INVENTORY_MOVEMENT.Inout='O' then TSPL_INVENTORY_MOVEMENT.qty else 0 end as OUTWARDQTY,TSPL_INVENTORY_MOVEMENT.Qty,tspl_location_master.IsMainPlant,TSPL_ITEM_master.Item_Type,Case when TSPL_INVENTORY_MOVEMENT.Inout='I' then TSPL_INVENTORY_MOVEMENT.uom else '' end as INUOM
                     from TSPL_INVENTORY_MOVEMENT
                    left join TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.DOCUMENT_No=TSPL_INVENTORY_MOVEMENT.Source_Doc_No 
                    left join tspl_location_master on tspl_location_master.location_code=TSPL_TRANSFER_ORDER_HEAD.from_location or tspl_location_master.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location
                    inner join TSPL_ITEM_master  on TSPL_ITEM_master.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code 
                    left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code and ItemConvReportUOM.Report_UOM=1
                    left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_INVENTORY_MOVEMENT.Item_Code = ItemConvinUOM.Item_Code and TSPL_INVENTORY_MOVEMENT.uom = ItemConvinUOM.UOM_Code 
                    where trans_type in ('ITransfer','Trasnfer') ) XX  where convert(date,source_doc_date,103)>='" + clsCommon.GetPrintDate(txtfromDate.Value, "dd-MMM-yyyy") + "' and convert(date,source_doc_date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'  
                    and xx.IsMainPlant=1 
                    and xx.Item_Type='F' and From_Location='" + txtLocation.Value + "' " + Whr + "
                    Group by item_code
                    union all
                    Select  SUM([OUTWARDQTYReportUom]) as [OUTWARDQTYReportUom],Sum([OUTWARDQTYReportUom]) as [OUTWARDQTYReportUom],Sum(Qty) as Qty,Item_Code,max(Item_Desc) as Item_Desc,Max(uom_code)UOM,Sum(INWARDQTY) as INWARDQTY,SUM(OUTWARDQTY) as OUTWARDQTY,max(From_Location) as From_Location,max(To_Location) as To_Location ,MAX(location_desc) as location_desc,MAX(structure_code) AS structure_code,'" + clsCommon.GetPrintDate(txtfromDate.Value, "dd-MMM-yyyy") + "'  as  From_Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'  as To_Date,MAX(City_code) as City_code ,MAX(INUOM)INUOM from (select TSPL_ITEM_master.structure_code,Location_Desc,City_code,TSPL_TRANSFER_ORDER_HEAD.document_no,TSPL_INVENTORY_MOVEMENT.Trans_Type,source_doc_date,TSPL_TRANSFER_ORDER_HEAD.From_Location,TSPL_TRANSFER_ORDER_HEAD.To_Location,Inout,ItemConvinUOM.Conversion_Factor,ItemConvReportUOM.uom_code,Case when Inout='I' then cast((TSPL_INVENTORY_MOVEMENT.Qty*ItemConvinUOM.Conversion_Factor/ItemConvReportUOM.Conversion_Factor) as Decimal(18,2)) else 0 end as [INWARDQTYReportUom],Case when Inout='O' then cast((TSPL_INVENTORY_MOVEMENT.Qty*ItemConvinUOM.Conversion_Factor/ItemConvReportUOM.Conversion_Factor) as Decimal(18,2)) else 0 end as [OUTWARDQTYReportUom],TSPL_INVENTORY_MOVEMENT.item_code,TSPL_ITEM_master.item_desc,Case when TSPL_INVENTORY_MOVEMENT.Inout='I' then TSPL_INVENTORY_MOVEMENT.qty else 0 end as INWARDQTY ,Case when TSPL_INVENTORY_MOVEMENT.Inout='O' then TSPL_INVENTORY_MOVEMENT.qty else 0 end as OUTWARDQTY,TSPL_INVENTORY_MOVEMENT.Qty,tspl_location_master.IsMainPlant,TSPL_ITEM_master.Item_Type,Case when TSPL_INVENTORY_MOVEMENT.Inout='I' then TSPL_INVENTORY_MOVEMENT.uom else '' end as INUOM
                     from TSPL_INVENTORY_MOVEMENT
                    left join TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.DOCUMENT_No=TSPL_INVENTORY_MOVEMENT.Source_Doc_No 
                    left join tspl_location_master on tspl_location_master.location_code=TSPL_TRANSFER_ORDER_HEAD.from_location or tspl_location_master.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location
                    inner join TSPL_ITEM_master  on TSPL_ITEM_master.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code 
                    left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code and ItemConvReportUOM.Report_UOM=1
                    left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_INVENTORY_MOVEMENT.Item_Code = ItemConvinUOM.Item_Code and TSPL_INVENTORY_MOVEMENT.uom = ItemConvinUOM.UOM_Code 
                    where trans_type in ('ITransfer','Trasnfer') ) XX  where convert(date,source_doc_date,103)>='" + clsCommon.GetPrintDate(txtfromDate.Value, "dd-MMM-yyyy") + "'  and convert(date,source_doc_date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'  and xx.IsMainPlant=1 and xx.Item_Type='F'  and To_Location='" + txtLocation.Value + "' " + Whr + "
                    Group by item_code "
            ElseIf ddlReportType.SelectedValue = "Stock Summary" Then
                Dim Location As String = ""
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    Location = " and Location_Code='" + txtLocation.Value + "'"
                End If
                Qry = " select TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Item_Code,max(TSPL_ITEM_MASTER.Short_Description)Item_Desc,MAX(I.UOM_Code)Report_UOM,
                 convert(decimal(18,2), (SUM(isnull((Stock_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtfromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))))  AS [OPBal]
                 ,convert(decimal(18,2), (SUM(isnull((Stock_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtfromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else 0 end))))  AS Received_Qty
                , convert(decimal(18,2), (SUM(isnull((Stock_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtfromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 0 else 1.00 end) )))  AS Issued_Qty,convert(decimal(18,2), (SUM(isnull((Stock_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end)) )) AS [Balance_Qty],max(TSPL_COMPANY_MASTER.Comp_Name)Comp_Name,max(TSPL_COMPANY_MASTER.City_Code)City_Code,'" + clsCommon.GetPrintDate(txtfromDate.Value, "dd-MMM-yyyy") + "' as fromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "' as Todate
                 from TSPL_INVENTORY_MOVEMENT
                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                 left outer join  TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_INVENTORY_MOVEMENT.Stock_UOM
                 left join ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_INVENTORY_MOVEMENT.Item_Code = I.item_code
                 left outer join TSPL_COMPANY_MASTER on 2=2
                where TSPL_ITEM_MASTER.Item_Type= 'F' " + Location + " " + Whr + "
                Group by TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Structure_Code order by Structure_Code "

            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                If Print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If ddlReportType.SelectedValue = "Stock Summary" Then
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptStockSummaryALW", "")
                        frmCRV = Nothing
                    ElseIf ddlReportType.SelectedValue = "Stock Journal" Then
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptJournalStockALW", "")
                        frmCRV = Nothing
                    End If
                Else
                    gvData.DataSource = Nothing
                    gvData.Rows.Clear()
                    gvData.Columns.Clear()
                    gvData.GroupDescriptors.Clear()
                    gvData.MasterTemplate.SummaryRowsBottom.Clear()
                    gvData.MasterView.Refresh()
                    gvData.DataSource = dt
                    For ii As Integer = 0 To gvData.Columns.Count - 1
                        gvData.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gvData.EnableFiltering = True
                    SetGridFormat()
                    gvData.BestFitColumns()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtMultiItem__My_Click(sender As Object, e As EventArgs) Handles txtMultiItem._My_Click
        Dim qry As String = " select Item_Code as Code, Item_desc as Name,Short_Description  from TSPL_ITEM_MASTER  where Item_Type='F'  "
        txtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", txtMultiItem.arrValueMember, txtMultiItem.arrDispalyMember)
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print(True)
    End Sub
    Sub SetGridFormat()
        Try



            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            For ii As Integer = 0 To gvData.Columns.Count - 1
                gvData.Columns(ii).ReadOnly = True
                gvData.Columns(ii).BestFit()
            Next
            If ddlReportType.SelectedValue = "Stock Journal" Then
                gvData.Columns("Structure_Code").HeaderText = "Structure Code"
                gvData.Columns("Structure_Code").Width = 250
                gvData.Columns("Structure_Code").IsVisible = True

                gvData.Columns("Item_Code").Name = "Item Code"
                gvData.Columns("Item_Code").IsVisible = True
                gvData.Columns("Item_Desc").HeaderText = "Item Name"
                gvData.Columns("Item_Desc").Width = 250
                gvData.Columns("Item_Desc").IsVisible = True
                gvData.Columns("Report_UOM").HeaderText = "Report UOM"
                gvData.Columns("Report_UOM").Width = 500
                gvData.Columns("INWARDQTYReportUom").HeaderText = "Inward Qty"
                gvData.Columns("INWARDQTYReportUom").Width = 250
                gvData.Columns("INWARDQTYReportUom").FormatString = "{0:n2}"
                gvData.Columns("INWARDQTYReportUom").HeaderText = "Report UOM Inward Qty"
                gvData.Columns("INWARDQTYReportUom").Width = 250
                gvData.Columns("INWARDQTYReportUom").FormatString = "{0:n2}"
                gvData.Columns("INWARDQTYReportUom").IsVisible = False



                gvData.Columns("OUTWARDQTYReportUom").HeaderText = "Report UOM Outward Qty"
                gvData.Columns("OUTWARDQTYReportUom").Width = 500
                gvData.Columns("OUTWARDQTYReportUom").IsVisible = False

                gvData.Columns("Qty").HeaderText = "Total Qty"
                gvData.Columns("Qty").Width = 500
                gvData.Columns("Qty").IsVisible = False

                'gvData.Columns("Item_Code").Name = "Item Code"
                'gvData.Columns("Item_Code").IsVisible = True

                gvData.Columns("Item_Desc").HeaderText = "Item Name"
                gvData.Columns("Item_Desc").Width = 250
                gvData.Columns("Item_Desc").IsVisible = True

                'gvData.Columns("VLC_Name").FormatString = "{0:n2}"
                gvData.Columns("UOM").HeaderText = "UOM"
                gvData.Columns("UOM").Width = 500

                gvData.Columns("INWARDQTY").HeaderText = "Inward QTY"
                gvData.Columns("INWARDQTY").Width = 250
                gvData.Columns("INWARDQTY").FormatString = "{0:n2}"
                gvData.Columns("INWARDQTY").IsVisible = True

                gvData.Columns("OUTWARDQTY").HeaderText = "Outward QTY"
                gvData.Columns("OUTWARDQTY").Width = 250
                gvData.Columns("OUTWARDQTY").FormatString = "{0:n2}"
                gvData.Columns("OUTWARDQTY").IsVisible = True

                gvData.Columns("From_Location").HeaderText = "From Location"
                gvData.Columns("From_Location").Width = 250
                gvData.Columns("From_Location").IsVisible = False

                gvData.Columns("To_Location").HeaderText = "To Location"
                gvData.Columns("To_Location").Width = 250
                gvData.Columns("To_Location").IsVisible = False

                gvData.Columns("location_desc").HeaderText = "location Name"
                gvData.Columns("location_desc").Width = 250
                gvData.Columns("location_desc").IsVisible = True

                gvData.Columns("structure_code").HeaderText = "Structure Code"
                gvData.Columns("structure_code").Width = 250
                gvData.Columns("structure_code").IsVisible = False

                gvData.Columns("From_Date").HeaderText = "From Date"
                gvData.Columns("From_Date").Width = 250
                gvData.Columns("From_Date").IsVisible = False

                gvData.Columns("To_Date").HeaderText = "To Date"
                gvData.Columns("To_Date").Width = 250
                gvData.Columns("To_Date").IsVisible = False

                gvData.Columns("City_code").HeaderText = "City code"
                gvData.Columns("City_code").Width = 250
                gvData.Columns("City_code").IsVisible = False

                gvData.Columns("INUOM").HeaderText = "INUOM"
                gvData.Columns("INUOM").Width = 250
                gvData.Columns("INUOM").IsVisible = False

                gvData.Columns("OUTWARDQTYReportUom").HeaderText = "Outward Qty"
            gvData.Columns("OUTWARDQTYReportUom").Width = 500
            gvData.Columns("OUTWARDQTYReportUom").IsVisible = True

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("INWARDQTYReportUom", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("OUTWARDQTYReportUom", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            ElseIf ddlReportType.SelectedValue = "Stock Summary" Then

                gvData.Columns("Structure_Code").HeaderText = "Structure Code"
                gvData.Columns("Structure_Code").IsVisible = True
                gvData.Columns("Item_Type").IsVisible = False
                gvData.Columns("Item_Code").HeaderText = "Item Code"
                gvData.Columns("Item_Code").IsVisible = True
                gvData.Columns("Item_Desc").HeaderText = "Item Name"
                gvData.Columns("Item_Desc").Width = 250
                gvData.Columns("Item_Desc").IsVisible = True
                'gvData.Columns("VLC_Name").FormatString = "{0:n2}"
                gvData.Columns("Report_UOM").HeaderText = "UOM"
                gvData.Columns("Report_UOM").Width = 500
                gvData.Columns("OPBal").HeaderText = "Opening Balance"
                gvData.Columns("OPBal").Width = 250
                gvData.Columns("OPBal").FormatString = "{0:n2}"

                gvData.Columns("Received_Qty").HeaderText = "Inwards Qty"
                gvData.Columns("Received_Qty").Width = 500
                gvData.Columns("Received_Qty").IsVisible = True

                gvData.Columns("Issued_Qty").HeaderText = "Outwards Qty"
                gvData.Columns("Issued_Qty").Width = 500
                gvData.Columns("Issued_Qty").IsVisible = True

                gvData.Columns("Balance_Qty").HeaderText = "Closing Balance"
                gvData.Columns("Balance_Qty").Width = 500
                gvData.Columns("Balance_Qty").IsVisible = True

                gvData.Columns("Comp_Name").IsVisible = False
                gvData.Columns("City_Code").IsVisible = False
                gvData.Columns("fromDate").IsVisible = False
                gvData.Columns("Todate").IsVisible = False


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gvData.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSalesStock & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(gvData, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub txtMultiStructureCode__My_Click(sender As Object, e As EventArgs) Handles txtMultiStructureCode._My_Click
        Dim qry As String = " select Structure_Code as Code, Structure_Descq as Name  from TSPL_STRUCTURE_MASTER "
        txtMultiStructureCode.arrValueMember = clsCommon.ShowMultipleSelectForm("StrucMulSel", qry, "Code", "Name", txtMultiStructureCode.arrValueMember, txtMultiStructureCode.arrDispalyMember)
    End Sub
End Class