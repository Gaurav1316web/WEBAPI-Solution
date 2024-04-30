Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Preeti Gupta against ticket no[BM00000008459,BM00000008476]-==============
'Sanjay Ticket No-ERO/05/06/19-000633 Add TSKG
Public Class RptItemConsumptionReport
#Region "Class Variables"
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
#End Region
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtBatchNoMult.arrDispalyMember IsNot Nothing AndAlso txtBatchNoMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrDispalyMember IsNot Nothing AndAlso txtItemMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            Dim qry As String = Nothing
            Dim uom As String = Nothing
            Dim uomGroup As String = Nothing
            Dim prdqty As String = Nothing
            Dim conqty As String = Nothing
            Dim issqty As String = Nothing
            If chk_stockingunit.Checked Then
                uom = "(stockunitconv.UOM_Code) AS UOM"
                uomGroup = "stockunitconv.UOM_Code"
                prdqty = "sum(((isnull(unitconv.Conversion_Factor,1)*(isnull(final.Prd_Qty,0)))/isnull(stockunitconv.Conversion_Factor,1))) as [Prd Qty]"
                conqty = "sum(((isnull(unitconv.Conversion_Factor,1)*(isnull(final.Consm_qty,0)))/isnull(stockunitconv.Conversion_Factor,1))) as [Con Qty]"
                issqty = "max(((isnull(unitconv.Conversion_Factor,1)*(isnull(final.Issue_Qty,0)))/isnull(stockunitconv.Conversion_Factor,1))) as [Issue Qty]"
            Else
                uom = "(final.Unit_Code) AS UOM"
                uomGroup = "final.Unit_Code"
                prdqty = "sum(final.Prd_Qty) as [Prd Qty]"
                conqty = "sum(final.Consm_qty) as [Con Qty]"
                issqty = "max(final.Issue_Qty) as [Issue Qty]"
            End If
            Dim ProdQry As String = ""
            Dim ConsmQry As String = ""
            Dim upperQry As String = ""
            Dim Baseqry As String = ""
            ''richa BHA/16/07/18-000169 show manual batch no 
            upperQry = " select final.Type,max(final.Section_Code) as [Section],final.Batch_Code as [Batch],max(convert(varchar,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103)) as [Batch Date],  max(isnull(TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo,'')) as ManualBatchNo, " & _
                                    " max(convert(varchar,Standardization_Date,103)) as [Standardization Date],TSPL_PP_BATCH_ORDER_HEAD.Location_Code as [Location Code],max(tspl_location_master.Location_Desc) as [Location Name]," & _
                                    " max(final.Std_Code) as [Std Code],( final.Prd_Item_Code ) as [Item],max(tspl_item_master.Item_Desc) as [Item Name]," + uom + ", " & _
                                    " sum(coalesce(final.[Avg Cost],0)) as [Avg Cost]," & _
                                    " " + prdqty + " ,convert(decimal(18, 2),case when sum ((final.Prd_Qty)) = 0 then 0 else (sum (final.Prd_SNF_Kg)*100) / sum ((final.Prd_Qty)) end ) as [Prd SNF % ] ," & _
                                    " convert(decimal(18, 2), case when sum ((final.Prd_Qty)) = 0 then 0 else (sum(final.Prd_Fat_Kg )*100) / sum ((final.Prd_Qty)) end ) as [Prd FAT % ] ," & _
                                    " convert(decimal(18, 2),case when sum ((final.Prd_Qty)) = 0 then 0 else (sum (final.Prd_SNF_Kg)*100) / sum ((final.Prd_Qty)) end ) " & _
                                    " +convert(decimal(18, 2), case when sum ((final.Prd_Qty)) = 0 then 0 else (sum(final.Prd_Fat_Kg )*100) / sum ((final.Prd_Qty)) end ) as [Prd TS % ] ," & _
                                    " sum(final.Prd_Fat_Kg) as [Prd FAT Kg] ,sum(final.Prd_SNF_Kg) as [Prd SNF kg] " & _
                                    " ,sum(final.Prd_Fat_Kg) + sum(final.Prd_SNF_Kg) as [Prd TS kg] " & _
                                    ", " + conqty + " " & _
                                    " , convert(decimal(18, 2),case when sum(final.Consm_qty) = 0 then 0 else (sum(final.Consm_FAT_Kg)*100) / sum(final.Consm_qty) end ) as [Cons FAT % ] " & _
                                    " ,convert(decimal(18, 2),case when sum(final.Consm_qty) = 0 then 0 else (sum(final.Consm_SNf_Kg)*100) / sum(final.Consm_qty) end ) as [Cons SNF % ] " & _
                                    " , convert(decimal(18, 2),case when sum(final.Consm_qty) = 0 then 0 else (sum(final.Consm_FAT_Kg)*100) / sum(final.Consm_qty) end )  " & _
                                    " +convert(decimal(18, 2),case when sum(final.Consm_qty) = 0 then 0 else (sum(final.Consm_SNf_Kg)*100) / sum(final.Consm_qty) end ) as [Cons TS % ] " & _
                                    " ,sum(final.Consm_FAT_Kg) as [Con FAT kg] ,sum(final.Consm_SNF_Kg) as [Con SNF kg] " & _
                                    " ,sum(final.Consm_FAT_Kg)+sum(final.Consm_SNF_Kg) as [Con TS kg] " & _
                                    ", max(final.Issue_Item)as [Issue Item] , " + issqty + " " & _
                                    " , convert(decimal(18, 2),case when sum(final.Issue_Qty) = 0 then 0 else (sum(final.Issue_FAt_Kg)*100) / sum(final.Issue_Qty) end ) as [Issue Fat % ] " & _
                                    " , convert(decimal(18, 2),case when sum(final.Issue_Qty) = 0 then 0 else (sum(final.Issue_SNF_kg )*100) / sum(final.Issue_Qty) end ) as [Issue SNF % ] " & _
                                    " , convert(decimal(18, 2),case when sum(final.Issue_Qty) = 0 then 0 else (sum(final.Issue_FAt_Kg)*100) / sum(final.Issue_Qty) end )  " & _
                                    " + convert(decimal(18, 2),case when sum(final.Issue_Qty) = 0 then 0 else (sum(final.Issue_SNF_kg )*100) / sum(final.Issue_Qty) end ) as [Issue TS % ] " & _
                                    " , sum(final.Issue_FAt_Kg) as [Issue FAT kg] ,sum(final.Issue_SNF_kg) as [Issue SNF kg] " & _
                                    " ,sum(final.Issue_FAt_Kg)+sum(final.Issue_SNF_kg) as [Issue TS Kg]" & _
                                    " from ( "

            'upperQry = " select final.Type,max(final.Section_Code) as [Section],final.Batch_Code as [Batch],max(convert(varchar,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103)) as [Batch Date],  max(isnull(TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo,'')) as ManualBatchNo, " & _
            '                      " max(convert(varchar,Standardization_Date,103)) as [Standardization Date],TSPL_PP_BATCH_ORDER_HEAD.Location_Code as [Location Code],max(tspl_location_master.Location_Desc) as [Location Name]," & _
            '                      " max(final.Std_Code) as [Std Code],( final.Prd_Item_Code ) as [Item],max(tspl_item_master.Item_Desc) as [Item Name]," + uom + ", " & _
            '                      " sum(coalesce(final.[Avg Cost],0)) as [Avg Cost]," & _
            '                      " " + prdqty + " ,convert(decimal(18, 2),case when sum ((final.Prd_Qty)) = 0 then 0 else (sum (final.Prd_SNF_Kg)*100) / sum ((final.Prd_Qty)) end ) as [Prd SNF % ] ," & _
            '                      " convert(decimal(18, 2), case when sum ((final.Prd_Qty)) = 0 then 0 else (sum(final.Prd_Fat_Kg )*100) / sum ((final.Prd_Qty)) end ) as [Prd FAT % ] ," & _
            '                      " sum(final.Prd_Fat_Kg) as [Prd FAT Kg] ,sum(final.Prd_SNF_Kg) as [Prd SNF kg] , " + conqty + " , convert(decimal(18, 2)," & _
            '                      " case when sum(final.Consm_qty) = 0 then 0 else (sum(final.Consm_FAT_Kg)*100) / sum(final.Consm_qty) end ) as [Cons FAT % ] ,convert(decimal(18, 2)," & _
            '                      " case when sum(final.Consm_qty) = 0 then 0 else (sum(final.Consm_SNf_Kg)*100) / sum(final.Consm_qty) end ) as [Cons SNF % ] ,sum(final.Consm_FAT_Kg) as [Con FAT kg] ," & _
            '                      " sum(final.Consm_SNF_Kg) as [Con SNF kg], max(final.Issue_Item)as [Issue Item] , " + issqty + " , convert(decimal(18, 2)," & _
            '                      " case when sum(final.Issue_Qty) = 0 then 0 else (sum(final.Issue_FAt_Kg)*100) / sum(final.Issue_Qty) end ) as [Issue Fat % ] , convert(decimal(18, 2)," & _
            '                      " case when sum(final.Issue_Qty) = 0 then 0 else (sum(final.Issue_SNF_kg )*100) / sum(final.Issue_Qty) end ) as [Issue SNF % ] , sum(final.Issue_FAt_Kg) as [Issue FAT kg] ," & _
            '                      " sum(final.Issue_SNF_kg) as [Issue SNF kg] from ( "


            ProdQry = " select 'Production' as Type, TSPL_PP_BATCH_ORDER_HEAD.Section_Code, TSPL_PP_BATCH_ORDER_HEAD.Batch_Code," & _
                                    " TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,BOProduction.Standardization_Date,BOProduction.Std_Code,BOProduction.std_Item_Code as Prd_Item_Code,BOProduction.Unit_Code,BOProduction.Avg_Cost as [Avg Cost]," & _
                                    " isnull(BOProduction.Prd_Qty, 0) as Prd_Qty, isnull(BOProduction.Prd_SNF_Per, 0) as Prd_SNF_Per, isnull(BOProduction.Prd_FAT_Per, 0) as Prd_FAT_Per," & _
                                    " isnull(BOProduction.Prd_Fat_Kg, 0 ) as Prd_Fat_Kg, isnull(BOProduction.Prd_SNF_Kg, 0) as Prd_SNF_Kg, '' as Consm_qty, 0 as Consm_FAT_Per, 0 as COnsm_SNF_Per," & _
                                    " 0 as Consm_FAT_Kg, 0 as Consm_SNF_Kg, '' as Issue_Item, 0 as Issue_Qty, 0 as Issue_Fat_per, 0 as Issue_SNF_Per, 0 as Issue_FAt_Kg, 0 as Issue_SNF_kg from TSPL_PP_BATCH_ORDER_HEAD " & _
                                    " left join (select TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code as Std_Code,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date," & _
                                    " TSPL_PP_STANDARDIZATION_HEAD.Child_Batch_Code as Std_Batch, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code as std_Item_Code,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Avg_Cost as [Avg_Cost], " & _
                                    " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Quantity as Batch_Qty, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty as Prd_Qty, " & _
                                    " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_per as Prd_FAT_Per, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_per as Prd_SNF_Per, " & _
                                    " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG as Prd_Fat_Kg, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG as Prd_SNF_Kg from " & _
                                    " TSPL_PP_STANDARDIZATION_HEAD left join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code " & _
                                    " where 2=2 and TSPL_PP_STANDARDIZATION_HEAD.Posted=1 and  convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE as prd_Code, TSPL_PP_PRODUCTION_ENTRY.PROD_DATE, TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch," & _
                                    " TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as prd_Item_code,TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE, TSPL_PP_PRODUCTION_ENTRY_DETAIL.Avg_Cost as [Avg_Cost], TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY as Batch_Qty, TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY as Prd_Qty," & _
                                    " TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per as prd_FAT_Per, TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per as Prd_SNF_Per, TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG as Prd_FAT_KG," & _
                                    " TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as Prd_SNF_Kg from TSPL_PP_PRODUCTION_ENTRY left join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE where TSPL_PP_PRODUCTION_ENTRY.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE as prd_Code, TSPL_PP_PRODUCTION_RETURN.RETURN_DATE, TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch," & _
                                    " TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as prd_Item_code,TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE, -TSPL_PP_PRODUCTION_ENTRY_DETAIL.Avg_Cost as [Avg_Cost], TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY as Batch_Qty, -TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY as Prd_Qty," & _
                                    " TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per as prd_FAT_Per, TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per as Prd_SNF_Per, -TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG as Prd_FAT_KG," & _
                                    " -TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as Prd_SNF_Kg from TSPL_PP_PRODUCTION_ENTRY inner join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE left join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE where TSPL_PP_PRODUCTION_RETURN.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code as Std_Code, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,TSPL_PP_STANDARDIZATION_HEAD.Main_Batch_Code as Std_Batch," & _
                                    " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code as std_Item_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Unit_Code, (TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Fat_Amt+TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.SNF_Amt) as [Avg_Cost], TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY as Batch_Qty, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY as Prd_Qty," & _
                                    " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_Per as Prd_FAT_Per, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_Per as Prd_SNF_Per, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG as Prd_Fat_Kg," & _
                                    " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG as Prd_SNF_Kg from TSPL_PP_STANDARDIZATION_HEAD left join TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code" & _
                                    " WHERE TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='REMOVE'  and TSPL_PP_STANDARDIZATION_HEAD.Posted=1 and  convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE as prd_Code, TSPL_PP_PRODUCTION_ENTRY.PROD_DATE, TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch," & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE as prd_Item_code,TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code, (TSPL_PP_PE_WRECKAGE_FLASHING.Fat_Amt+TSPL_PP_PE_WRECKAGE_FLASHING.SNF_Amt) as [Avg_Cost] , TSPL_PP_PE_WRECKAGE_FLASHING.BACK_QTY as Batch_Qty, TSPL_PP_PE_WRECKAGE_FLASHING.BACK_QTY as Prd_Qty, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_Per as prd_FAT_Per," & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_Per as Prd_SNF_Per, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_KG as Prd_FAT_KG, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_KG as Prd_SNF_Kg from TSPL_PP_PRODUCTION_ENTRY " & _
                                    " left join TSPL_PP_PE_WRECKAGE_FLASHING on TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE WHERE TSPL_PP_PE_WRECKAGE_FLASHING.BACK_QTY>0 and TSPL_PP_PRODUCTION_ENTRY.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE as prd_Code, TSPL_PP_PRODUCTION_RETURN.RETURN_DATE, TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch," & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE as prd_Item_code,TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code, -(TSPL_PP_PE_WRECKAGE_FLASHING.Fat_Amt+TSPL_PP_PE_WRECKAGE_FLASHING.SNF_Amt) as [Avg_Cost] , TSPL_PP_PE_WRECKAGE_FLASHING.BACK_QTY as Batch_Qty, -TSPL_PP_PE_WRECKAGE_FLASHING.BACK_QTY as Prd_Qty, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_Per as prd_FAT_Per," & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_Per as Prd_SNF_Per, -TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_KG as Prd_FAT_KG, -TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_KG as Prd_SNF_Kg from TSPL_PP_PRODUCTION_ENTRY inner join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE " & _
                                    " left join TSPL_PP_PE_WRECKAGE_FLASHING on TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE WHERE TSPL_PP_PE_WRECKAGE_FLASHING.BACK_QTY>0 and TSPL_PP_PRODUCTION_RETURN.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)) as " & _
                                    " BOProduction on BOProduction.Std_Batch = TSPL_PP_BATCH_ORDER_HEAD.Batch_Code  where 2=2 "

            
            ConsmQry = " union all select 'Consumption' as Type, consumption.CONSM_SECTION_CODE, Child_Batch_Code as Batch_Code, Null as Batch_date, " & _
                                    " consumption.Standardization_Date, consumption.Stand_code, consumption.CONSM_ITEM_CODE,consumption.Consm_Unit_COde as Unit_Code, consumption.Avg_Cost as [Avg Cost], 0 as Prd_qty, 0 as Prd_SNF_Per, 0 as Prd_Fat_per, 0 as Prd_FAT_Kg, 0 as Prd_SNF_Kg," & _
                                    " consumption.CONSM_QTY, consumption.consm_Fat_Per, consumption.Consm_SNF_Per, consumption.Consm_FAT_Kg, consumption.Consm_SNf_Kg, consumption.Issue_Item, consumption.Issue_Qty," & _
                                    " consumption.Issue_Fat_Per, consumption.Issue_SNF_per, consumption.Issue_Fat_Kg, consumption.Issue_SNF_Kg from " & _
                                    " ( select TSPL_PP_STANDARDIZATION_HEAD.Child_Batch_Code, TSPL_PP_STANDARDIZATION_HEAD.CONSM_SECTION_CODE, std.*, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date, " & _
                                    " TSPL_PP_STD_ISSUE_ITEM_DETAIL.Item_Code as Issue_Item, TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_Qty as Issue_Qty, TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Issue_Fat_Per, " & _
                                    " TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Issue_SNF_per, TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Issue_Fat_Kg, TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Issue_SNF_Kg from " & _
                                    " ( select std1.* from (SELECT TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code as Stand_code, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY as CONSM_QTY, " & _
                                    " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE as CONSM_ITEM_CODE, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE as Consm_Unit_COde, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per as consm_Fat_Per," & _
                                    " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per as Consm_SNF_Per, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG as Consm_FAT_Kg, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Avg_Cost as [Avg_Cost], TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG as Consm_SNf_Kg  FROM  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL" & _
                                    " union all SELECT TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code as Stand_code, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.add_remove_qty as CONSM_QTY, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.item_code as CONSM_ITEM_CODE," & _
                                    " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE as Consm_Unit_COde, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_FAT_Per as consm_Fat_Per, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_SNF_Per as Consm_SNF_Per," & _
                                    " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_FAT_KG as Consm_FAT_Kg, (TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Fat_Amt+TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.SNF_Amt) as [Avg_Cost], TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_SNF_KG as Consm_SNf_Kg FROM  TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL" & _
                                    " where TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='ADD' )std1 where std1.Stand_code is not null ) std left join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL on " & _
                                    " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code = std.Stand_code inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code = std.Stand_code " & _
                                    " left join TSPL_PP_STD_ISSUE_ITEM_DETAIL on TSPL_PP_STD_ISSUE_ITEM_DETAIL.Standardization_Code = std.Stand_code and TSPL_PP_STD_ISSUE_ITEM_DETAIL.Item_Code = std.CONSM_ITEM_CODE WHERE TSPL_PP_STANDARDIZATION_HEAD.Posted=1 AND  convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Batch_Code, TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE as CONSM_SECTION_CODE, std.*, TSPL_PP_PRODUCTION_ENTRY.PROD_DATE," & _
                                    " TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code as Issue_Item, TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_Qty as Issue_Qty, TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Issue_Fat_Per," & _
                                    " TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Issue_SNF_per, TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Issue_Fat_Kg, TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Issue_SNF_Kg from " & _
                                    " ( select std2.* from ( select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE as Stand_code, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," & _
                                    " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE as Consm_Unit_COde, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per as consm_Fat_Per, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per as Consm_SNF_Per," & _
                                    " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG as Consm_FAT_Kg, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Avg_Cost as [Avg_Cost], TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG as Consm_SNf_Kg from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL inner join " & _
                                    " TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE WHERE TSPL_PP_PRODUCTION_ENTRY.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE as Stand_code, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY as CONSM_QTY, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," & _
                                    " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE as Consm_Unit_COde, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per as consm_Fat_Per, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per as Consm_SNF_Per," & _
                                    " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG as Consm_FAT_Kg, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Avg_Cost as [Avg_Cost], TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG as Consm_SNf_Kg from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL inner join " & _
                                    " TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE inner join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE WHERE TSPL_PP_PRODUCTION_RETURN.Posted=1 and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_RETURN_CODE is not null and  convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE as Stand_code, TSPL_PP_PE_WRECKAGE_FLASHING.wreckage_qty as CONSM_QTY, TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE as CONSM_ITEM_CODE," & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.UNIT_CODE as Consm_Unit_COde, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_Per as consm_Fat_Per, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_Per as Consm_SNF_Per, " & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_KG as Consm_FAT_Kg, (TSPL_PP_PE_WRECKAGE_FLASHING.Fat_Amt+TSPL_PP_PE_WRECKAGE_FLASHING.SNF_Amt) as [Avg_Cost], TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_KG as Consm_SNf_Kg from TSPL_PP_PE_WRECKAGE_FLASHING inner join TSPL_PP_PRODUCTION_ENTRY on " & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE where TSPL_PP_PE_WRECKAGE_FLASHING.WRECKAGE_QTY>0 and TSPL_PP_PRODUCTION_ENTRY.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " union all select TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE as Stand_code, -TSPL_PP_PE_WRECKAGE_FLASHING.wreckage_qty as CONSM_QTY, TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE as CONSM_ITEM_CODE," & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.UNIT_CODE as Consm_Unit_COde, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_Per as consm_Fat_Per, TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_Per as Consm_SNF_Per, " & _
                                    " -TSPL_PP_PE_WRECKAGE_FLASHING.Avail_FAT_KG as Consm_FAT_Kg, -(TSPL_PP_PE_WRECKAGE_FLASHING.Fat_Amt+TSPL_PP_PE_WRECKAGE_FLASHING.SNF_Amt) as [Avg_Cost], -TSPL_PP_PE_WRECKAGE_FLASHING.Avail_SNF_KG as Consm_SNf_Kg from TSPL_PP_PE_WRECKAGE_FLASHING inner join TSPL_PP_PRODUCTION_ENTRY on " & _
                                    " TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE inner join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE where TSPL_PP_PE_WRECKAGE_FLASHING.WRECKAGE_QTY>0 and TSPL_PP_PRODUCTION_RETURN.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                                    " )std2 where std2.Stand_code is not null ) std " & _
                                    " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = std.Stand_code left join TSPL_PP_PE_ISSUE_ITEM_DETAIL on TSPL_PP_PE_ISSUE_ITEM_DETAIL.PROD_ENTRY_CODE = std.Stand_code and " & _
                                    " TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code = std.CONSM_ITEM_CODE WHERE TSPL_PP_PRODUCTION_ENTRY.Posted=1 and  convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) ) as consumption "
            Baseqry = " " & upperQry & " " & ProdQry & "  " & ConsmQry & ") as final " & _
                                    " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = final.Prd_Item_Code inner join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code = final .Batch_Code " & _
                                    " left join TSPL_LOCATION_MASTER on TSPL_PP_BATCH_ORDER_HEAD.Location_Code = TSPL_LOCATION_MASTER.Location_Code "
            If chk_stockingunit.Checked Then
                Baseqry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=final.Prd_Item_Code" & _
                           " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=final.Prd_Item_Code and unitconv.UOM_Code=final.Unit_Code"
            End If


            '" left join (select Source_Doc_No,Item_Code,sum(Avg_Cost) as Avg_Cost from (" & _
            '" select (case when TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null then  TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No else TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE end) as Source_Doc_No,Item_Code,(case when InOut='I' then 1 else -1 end) *Avg_Cost as Avg_Cost  from TSPL_INVENTORY_MOVEMENT_NEW left join TSPL_PP_PRODUCTION_RETURN on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE " & _
            '" where cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' and Trans_Type in ('PP_ISSUE','PP_PR','PP_STDN','PP-PR','PRD_STG_PROC','PROD_ENTR_WB','PROD_ENTRY','PP-PR')) as Inv group by Source_Doc_No,Item_Code  " & _
            '" ) TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.source_doc_no = final.Std_Code and " & _
            '" TSPL_INVENTORY_MOVEMENT_NEW.item_code = final.Prd_Item_Code " & _
            '" left join (select Source_Doc_No,Item_Code,sum(Avg_Cost) as Avg_Cost from ( select (case when TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null then  TSPL_INVENTORY_MOVEMENT.Source_Doc_No else TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE end) as Source_Doc_No,Item_Code,(case when InOut='I' then 1 else -1 end) *Avg_Cost as Avg_Cost  from TSPL_INVENTORY_MOVEMENT " & _
            '" left join TSPL_PP_PRODUCTION_RETURN on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE " & _
            '" where cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' and Trans_Type in ('PP_ISSUE','PP_PR','PP_STDN','PP-PR','PRD_STG_PROC','PROD_ENTR_WB','PROD_ENTRY','PP-PR') " & _
            '" ) as Inv group by Source_Doc_No,Item_Code ) TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.source_doc_no = final.Std_Code and TSPL_INVENTORY_MOVEMENT.item_code = final.Prd_Item_Code " & _
            Baseqry += " where 2=2 and  convert(date,Final.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Final.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                Baseqry += " and  final.Batch_Code in (" + clsCommon.GetMulcallString(txtBatchNoMult.arrValueMember) + ")  "
                    End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                Baseqry += " and  Final.Prd_Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
                    End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                Baseqry += " and  Final.Section_Code in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
                    End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                Baseqry += " and TSPL_PP_BATCH_ORDER_HEAD.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
                    End If
            Baseqry += " group by final.type,final.Batch_Code,Prd_Item_Code,TSPL_PP_BATCH_ORDER_HEAD.Location_Code," & uomGroup & " "

            Baseqry += " order by  final.Type desc, final.Batch_Code   "


                    'Dim Baseqry As String = "  select case when ROWNUM = 1 then [Section Code]   else '' end as [Section Code],case when ROWNUM = 1 then [Batch Code]    else '' end as [Batch Code], [Batch Date], [Prd Item Code] ,  [Prd Qty], [Prd SNF Per], [Prd FAT Per]"
                    'Baseqry += ", [Prd Fat Kg], [Prd SNF Kg], [Consumtion Item Code],[Consumtion Qty] ,[Consumtion FAT Per] ,[Consumtion SNF Per] ,[Consumtion FAT KG] ,[Consumtion SNF KG] ,[Issued Item Code] ,[Issued Qty] ,[Issued  FAT Per] ,[Issued SNF Per] ,[Issued FAT KG] ,[Issued SNF KG]    from ( select Final.*, ROW_NUMBER() over(partition by [Batch Code] order by [Batch Code]) as ROWNUM from (select"
                    'Baseqry += " BO.Section_Code as [Section Code],convert(varchar,bo.Batch_Code,103) as [Batch Code],bo.Batch_Date as [Batch Date] ,"
                    'Baseqry += " coalesce(STD_Main. Sta_Pro_Item_Code,Production.Pro_Item_Code) [Prd Item Code],"
                    'Baseqry += " coalesce(STD_Main. Sta_Pro_Qty,Production.FINAL_PRODUCTION_QTY) [Prd Qty],"
                    'Baseqry += " coalesce(STD_Main. Sta_Pro_SNF_Per,Production.Prod_SNF_Per) [Prd SNF Per],"
                    'Baseqry += " coalesce(STD_Main. Sta_Pro_Fat_Per,Production.pro_Fat_per) [Prd FAT Per],  "
                    'Baseqry += " coalesce(STD_Main. Sta_Fat_Kg,Production.Prod_Fat_Kg) [Prd Fat Kg], "
                    'Baseqry += " coalesce(STD_Main. Sta_SNF_Kg,Production.Pro_SNF_Kg) [Prd SNF Kg] ,"
                    'Baseqry += " coalesce(STD_Main. sta_Consm_Item_code,Production.CONSM_ITEM_CODE) [Consumtion Item Code] ,"
                    'Baseqry += " coalesce(STD_Main. Sta_consm_qty,Production.CONSM_QTY) [Consumtion Qty],"
                    'Baseqry += " coalesce(STD_Main. sta_Cons_Fat_Per,Production.Cons_FatPer) [Consumtion FAT Per] ,"
                    'Baseqry += " coalesce(STD_Main. Sta_Con_Snf_per,Production.Cons_snfPer) [Consumtion SNF Per] ,"
                    'Baseqry += "coalesce(STD_Main. Sta_Cons_Fat_Kg,Production.Cons_Fat_kg) [Consumtion FAT KG]  ,"
                    'Baseqry += " coalesce(STD_Main. Sta_Cons_SNF_Kg,Production.Cons_snf_Kg) [Consumtion SNF KG] ,"
                    'Baseqry += " coalesce(STD_Main. Sta_Iss_item_code,Production.Prod_Iss_Item_Code) [Issued Item Code], "
                    'Baseqry += " coalesce(STD_Main. Sta_Iss_Qty,Production.Prod_Iss_Qty) [Issued Qty], "
                    'Baseqry += " coalesce(STD_Main. Sta_Iss_Fat_Per,Production.Pro_Iss_Fat_per) [Issued  FAT Per], "
                    'Baseqry += " coalesce(STD_Main. Sta_Iss_SNF_Per,Production.Prod_iss_snf_per) [Issued SNF Per], "
                    'Baseqry += " coalesce(STD_Main. Sta_Iss_FAT_Kg,Production.Prod_Iss_Fat_Kg) [Issued FAT KG], "
                    'Baseqry += " coalesce(STD_Main. Sat_iss_SNF_Kg,Production.Pro_issu_SNF_KG) [Issued SNF KG]"
                    'Baseqry += " from TSPL_PP_BATCH_ORDER_HEAD BO "

                    'Baseqry += " left join ("
                    'Baseqry += " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as Pro_Item_Code ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY as FINAL_PRODUCTION_QTY ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per as pro_Fat_per ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per as Prod_SNF_Per,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG as Prod_Fat_Kg,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as Pro_SNF_Kg, Consm.CONSM_CODE ,Consm.CONSM_ITEM_CODE ,Consm.CONSM_QTY,Consm.FAT_Per as Cons_FatPer,Consm.SNF_Per as Cons_snfPer,Consm.FAT_KG as Cons_Fat_kg,Consm.SNF_KG as Cons_snf_Kg ,PE.Batch_Code"
                    'Baseqry += " ,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code as Prod_Iss_Item_Code,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_Qty as Prod_Iss_Qty,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Pro_Iss_Fat_per,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Prod_iss_snf_per, TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Prod_Iss_Fat_Kg,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_KG  as Pro_issu_SNF_KG"
                    'Baseqry += " from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL Consm "
                    'Baseqry += " inner join TSPL_PP_PRODUCTION_ENTRY PE on Consm.PROD_ENTRY_CODE=PE.PROD_ENTRY_CODE  "
                    'Baseqry += " left join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE =pe.PROD_ENTRY_CODE"
                    'Baseqry += " left join TSPL_PP_PE_ISSUE_ITEM_DETAIL on TSPL_PP_PE_ISSUE_ITEM_DETAIL.PROD_ENTRY_CODE  = Consm.PROD_ENTRY_CODE and TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code =Consm.CONSM_ITEM_CODE "

                    'Baseqry += " where consm.PROD_ENTRY_CODE is not null) Production on BO.Batch_Code=Production.Batch_Code"
                    'Baseqry += " left join ("
                    'Baseqry += " select TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code as Sta_Pro_Item_Code,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Quantity as Sta_Pro_Qty,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_FAT_per as Sta_Pro_Fat_Per,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_SNF_Per as Sta_Pro_SNF_Per,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG as Sta_Fat_Kg,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG as Sta_SNF_Kg,"
                    'Baseqry += " std.Child_Batch_Code,Consm.CONSM_ITEM_CODE as sta_Consm_Item_code,consm.CONSM_QTY as Sta_consm_qty,consm.FAT_Per as sta_Cons_Fat_Per,consm.SNF_Per as Sta_Con_Snf_per,consm.FAT_KG as Sta_Cons_Fat_Kg,consm.SNF_KG as Sta_Cons_SNF_Kg,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Item_Code as Sta_Iss_item_code,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_Qty as Sta_Iss_Qty,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Sta_Iss_Fat_Per,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Sta_Iss_SNF_Per,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Sta_Iss_FAT_Kg,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Sat_iss_SNF_Kg"


                    'Baseqry += "  from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL Consm inner join TSPL_PP_STANDARDIZATION_HEAD STD on Consm.Standardization_Code=STD.Standardization_Code"
                    'Baseqry += " left join TSPL_PP_STD_ISSUE_ITEM_DETAIL on TSPL_PP_STD_ISSUE_ITEM_DETAIL.Standardization_Code  =std.Standardization_Code "
                    'Baseqry += " left join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code  = Consm.Standardization_Code and TSPL_PP_STD_ISSUE_ITEM_DETAIL.Item_Code =Consm.CONSM_ITEM_CODE "
                    'Baseqry += " where CONSM.Standardization_Code is not null) STD_Main on bo.Batch_Code=STD_Main.Child_Batch_Code) as Final"
                    'Baseqry += " where 2=2 and  convert(date,Final.[Batch Date],103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Final.[Batch Date],103) <=convert(date,'" + txtToDate.Value + "' ,103)"


                    'If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                    '    Baseqry += " and  Final.[Batch Code] in (" + clsCommon.GetMulcallString(txtBatchNoMult.arrValueMember) + ")  "
                    'End If
                    'If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    '    Baseqry += " and  Final.[Prd Item Code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
                    'End If
                    'If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                    '    Baseqry += " and  Final.[Section Code] in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
                    'End If
                    'Baseqry += "   ) xx"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Baseqry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = False

                gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", "Item Consumption Report Batch-wise")
                    End If
            gv1.DataSource = dt
            gv1.Columns("std Code").IsVisible = False
            gv1.Columns("Standardization Date").IsVisible = False
                    'SetGridFormationOFGV1()
            gv1.BestFitColumns()
            chk_stockingunit.Enabled = False
            'FindAndRestoreGridLayout(Me)
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Batch_Code").Width = 100
        gv1.Columns("Batch_Code").IsVisible = True
        gv1.Columns("Batch_Code").HeaderText = "Batch Code"

        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("Item_Desc").Width = 100
        gv1.Columns("Item_Desc").IsVisible = True
        gv1.Columns("Item_Desc").HeaderText = "Item Name"

        gv1.Columns("Required_Qty").Width = 100
        gv1.Columns("Required_Qty").IsVisible = True
        gv1.Columns("Required_Qty").HeaderText = "Batch Required Qty"

        gv1.Columns("Issue_Qty").Width = 100
        gv1.Columns("Issue_Qty").IsVisible = True
        gv1.Columns("Issue_Qty").HeaderText = "Issue Qty"

        gv1.Columns("Unit_Code").Width = 100
        gv1.Columns("Unit_Code").IsVisible = True
        gv1.Columns("Unit_Code").HeaderText = "Unit Code"

        gv1.Columns("Pending").Width = 100
        gv1.Columns("Pending").IsVisible = True
        gv1.Columns("Pending").HeaderText = "Pending"

        gv1.Columns("Status").Width = 100
        gv1.Columns("Status").IsVisible = True
        gv1.Columns("Status").HeaderText = "Status"

        gv1.Columns("Issue_Code").Width = 100
        gv1.Columns("Issue_Code").IsVisible = True
        gv1.Columns("Issue_Code").HeaderText = "Issue Code"

        gv1.Columns("Issue_Code").Width = 100
        gv1.Columns("Issue_Code").IsVisible = True
        gv1.Columns("Issue_Code").HeaderText = "Issue Code"

        gv1.Columns("FAT_Pers").Width = 100
        gv1.Columns("FAT_Pers").IsVisible = True
        gv1.Columns("FAT_Pers").HeaderText = "FAT %"

        gv1.Columns("SNF_Pers").Width = 100
        gv1.Columns("SNF_Pers").IsVisible = True
        gv1.Columns("SNF_Pers").HeaderText = "SNF %"

        gv1.Columns("Section_Code").Width = 100
        gv1.Columns("Section_Code").IsVisible = True
        gv1.Columns("Section_Code").HeaderText = "Section Code"

        gv1.Columns("Location Code").Width = 100
        gv1.Columns("Location Code").IsVisible = True
        gv1.Columns("Location Code").HeaderText = "Location Code"

        gv1.Columns("Location Name").Width = 100
        gv1.Columns("Location Name").IsVisible = True
        gv1.Columns("Location Name").HeaderText = "Location Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As New GridViewSummaryItem("Stock_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        'ReStoreGridLayout()
    End Sub
    Sub Reset()
        chk_stockingunit.Enabled = True
        chk_stockingunit.Checked = False
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        ddlReportsType.SelectedIndex = 0
        txtItemMult.arrValueMember = Nothing
        txtSectionMult.arrValueMember = Nothing
        txtBatchNoMult.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        txtBOMNo.arrValueMember = Nothing

    End Sub
    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub
    Private Sub txtSectionMult__My_Click(sender As Object, e As EventArgs) Handles txtSectionMult._My_Click
        Dim qry As String = " select distinct Section_Code as Code , Section_Code as Name from TSPL_PP_BATCH_ORDER_HEAD where Section_Code is not null "
        txtSectionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtSectionMult", qry, "Code", "Name", txtSectionMult.arrValueMember, txtSectionMult.arrDispalyMember)
    End Sub
    Private Sub txtBatchNoMult__My_Click(sender As Object, e As EventArgs) Handles txtBatchNoMult._My_Click
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Name  from TSPL_PP_BATCH_ORDER_HEAD "
        txtBatchNoMult.arrValueMember = clsCommon.ShowMultipleSelectForm("multBatchNo", qry, "Code", "Name", txtBatchNoMult.arrValueMember, txtBatchNoMult.arrDispalyMember)
    End Sub
    Private Sub Load_BOMWise_Report()
        Dim qry As String = ""
        Dim qtyuom As String = Nothing
        Dim stocking As String = Nothing
        If chk_stockingunit.Checked Then
            qtyuom = " stockunitconv.Uom_CODE as [MAIN ITEM UOM], ((isnull(unitconv.Conversion_Factor,1)*(isnull(outterQry1.[PRODUCED QTY.],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [PRODUCED QTY.]"
            stocking = " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=outterQry1.[MAIN ITEM] left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=outterQry1.[MAIN ITEM] and unitconv.UOM_Code=outterQry1.[MAIN ITEM UOM]"
        Else
            qtyuom = " outterQry1.[MAIN ITEM UOM], outterQry1.[PRODUCED QTY.]"
            stocking = ""
        End If
        Dim dt As DataTable = Nothing
        Try
            qry += " ;WITH cte_itemconsumption_bom_wise AS (SELECT outterQry1.[raw item order] [S.No], outterQry1.[Main Item], outterQry1.[main item uom] AS [UOM(Main Item's)], "
            qry += qtyuom
            qry += ", outterQry1.[bom code] AS [BOM Code], CONVERT(varchar, outterQry1.[bom created date], 103) AS [BOM CREATED DATE],outterQry1.[Production Date], outterQry1.[produced qty.] AS [Produced Qty], " & _
                   " CASE WHEN COALESCE(outterQry1.[main item desc], '') = '' THEN (SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE Item_Code = outterQry1.[main item]) ELSE outterQry1.[main item desc] END AS [Main Item Desc], " & _
                   " outterQry1.[raw item desc] AS [Items Desc], outterQry1.[raw items] AS [Items Code], outterQry1.[item uom (std.)] AS [Unit (std)], outterQry1.[fat % (std.)]," & _
                   " outterQry1.[snf % (std.)], outterQry1.[total (std.)], outterQry1.[item uom (actual)], outterQry1.[fat % (actual)], outterQry1.[snf % (actual)]," & _
                   " outterQry1.[total (actual)], outterQry1.[item uom (loss-gain)], outterQry1.[fat % (loss-gain)], outterQry1.[snf % (loss-gain)]," & _
                   " outterQry1.[total (loss-gain)], outterQry1.[item uom (std.)] AS [ITEM UOM (VARIANCE)], " & _
                   " (outterQry1.[fat % (std.)] - (outterQry1.[fat % (actual)] + outterQry1.[fat % (loss-gain)])) AS [FAT % (VARIANCE)], " & _
                   " (outterQry1.[snf % (std.)] - (outterQry1.[snf % (actual)] + outterQry1.[snf % (loss-gain)])) AS [SNF % (VARIANCE)], " & _
                   " (outterQry1.[total (std.)] - (outterQry1.[total (actual)] + outterQry1.[total (loss-gain)])) AS [TOTAL (VARIANCE)] " & _
                   " FROM (SELECT innerQry1.[main item] AS [MAIN ITEM], innerQry1.[main item desc] AS [MAIN ITEM DESC], innerQry1.[main item uom] AS [MAIN ITEM UOM]," & _
                   " innerQry1.[produced qty.] AS [PRODUCED QTY.], [actual Prod Qty], innerQry1.Produced_FAT_KG AS [Fat(kg)], innerQry1.Produced_SNF_KG AS [SNF(kg)]," & _
                   " innerQry1.conversion_factor, innerQry1.[con_factor uom], innerQry1.[cv], innerQry1.[bom code] AS [BOM CODE], " & _
                   " innerQry1.[bom created date] AS [BOM CREATED DATE],innerQry1.Prod_Date as [Production Date], innerQry1.[bom status] AS [BOM STATUS], innerQry1.[raw items] AS [RAW ITEMS], " & _
                   " innerQry1.[line_no] AS [RAW ITEM ORDER], innerQry1.[raw item desc] AS [RAW ITEM DESC], innerQry1.[raw item uom] AS [RAW ITEM UOM], " & _
                   " innerQry1.[item uom (std.)] AS [ITEM UOM (STD.)], CONVERT(decimal(18, 2), innerQry1.[fat % (std.)]) AS [FAT % (STD.)], " & _
                   " CONVERT(decimal(18, 2), innerQry1.[snf % (std.)]) AS [SNF % (STD.)], CONVERT(decimal(18, 2), innerQry1.[total (std.)]) AS [TOTAL (STD.)]," & _
                   " innerQry1.[item uom (actual)] AS [ITEM UOM (ACTUAL)], CONVERT(decimal(18, 2), innerQry1.[fat % (actual)]) AS [FAT % (ACTUAL)]," & _
                   " CONVERT(decimal(18, 2), innerQry1.[snf % (actual)]) AS [SNF % (ACTUAL)], CONVERT(decimal(18, 2), innerQry1.[total (actual)]) AS [TOTAL (ACTUAL)], " & _
                   " innerQry1.[item uom (std.)] AS [ITEM UOM (Loss-Gain)], CONVERT(decimal(18, 2), (innerQry1.[fat % (std.)] - innerQry1.[fat % (actual)])) AS [FAT % (Loss-Gain)], " & _
                   " CONVERT(decimal(18, 2), (innerQry1.[snf % (std.)] - innerQry1.[snf % (actual)])) AS [SNF % (Loss-Gain)], CONVERT(decimal(18, 2), " & _
                   " (innerQry1.[total (std.)] - innerQry1.[total (actual)])) AS [TOTAL (Loss-Gain)] FROM (SELECT DISTINCT tspl_pp_bom_head.prod_item_code AS [MAIN ITEM], " & _
                   " tspl_pp_bom_head.description AS [MAIN ITEM DESC], tspl_pp_bom_head.prod_item_unit_code AS [MAIN ITEM UOM], " & _
                   " tspl_pp_bom_head.prod_quantity AS [PRODUCED QTY.], (coalesce(tspl_pp_batch_item_production_detail.Produced_Qty,0)+coalesce(FINAL_PRODUCTION_QTY,0)) AS [actual Prod Qty], " & _
                   " (coalesce(tspl_pp_batch_item_production_detail.Produced_FAT_KG,0)+coalesce(TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG,0)) as Produced_FAT_KG, (coalesce(tspl_pp_batch_item_production_detail.Produced_SNF_KG,0)+coalesce(TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG,0)) as Produced_SNF_KG, " & _
                   " tspl_item_uom_detail.conversion_factor, tspl_item_uom_detail.uom_code AS [Con_Factor UOM], kgconv.conversion_factor AS CV, " & _
                   " tspl_pp_bom_head.bom_code AS [BOM CODE], tspl_pp_bom_head.bom_date AS [BOM CREATED DATE],COALESCE(TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE) AS Prod_Date, tspl_pp_bom_head.status AS [BOM STATUS], " & _
                   " tspl_pp_bom_item_detail.item_code AS [RAW ITEMS], tspl_pp_bom_item_detail.line_no, tspl_item_master.item_desc AS [RAW ITEM DESC], " & _
                   " tspl_pp_bom_item_detail.unit_code AS [RAW ITEM UOM], tspl_pp_bom_item_detail.unit_code AS [ITEM UOM (STD.)], COALESCE(pvtQry.[fat %], 0) AS [FAT % (STD.)], " & _
                   " COALESCE(pvtQry.[snf %], 0) AS [SNF % (STD.)], COALESCE(tspl_pp_bom_item_detail.quantity, 0) AS [TOTAL (STD.)], " & _
                   " tspl_pp_bom_item_detail.unit_code AS [ITEM UOM (ACTUAL)], COALESCE(tspl_pp_bom_item_detail.fat, 0) AS [FAT % (ACTUAL)], " & _
                   " COALESCE(tspl_pp_bom_item_detail.snf, 0) AS [SNF % (ACTUAL)], COALESCE(tspl_pp_bom_item_detail.quantity, 0) AS [TOTAL (ACTUAL)] " & _
                   " FROM tspl_pp_bom_head LEFT JOIN tspl_pp_bom_item_detail ON tspl_pp_bom_head.bom_code = tspl_pp_bom_item_detail.bom_code " & _
                   " LEFT JOIN tspl_item_master ON tspl_pp_bom_item_detail.item_code = tspl_item_master.item_code " & _
                   " LEFT JOIN tspl_item_qc_parameter_master ON tspl_item_master.item_code = tspl_item_qc_parameter_master.item_code " & _
                   " LEFT JOIN tspl_parameter_master ON tspl_item_qc_parameter_master.code = tspl_parameter_master.code " & _
                   " LEFT JOIN tspl_pp_batch_item_production_detail ON tspl_pp_bom_head.prod_item_code = tspl_pp_batch_item_production_detail.item_code " & _
                   " AND tspl_pp_bom_head.PROD_ITEM_UNIT_CODE = tspl_pp_batch_item_production_detail.Unit_Code " & _
                   " LEFT JOIN tspl_pp_standardization_head ON tspl_pp_batch_item_production_detail.standardization_code = tspl_pp_standardization_head.standardization_code " & _
                   " LEFT JOIN TSPL_PP_PRODUCTION_ENTRY_DETAIL ON tspl_pp_bom_head.prod_item_code = TSPL_PP_PRODUCTION_ENTRY_DETAIL.item_code " & _
                   " AND tspl_pp_bom_head.PROD_ITEM_UNIT_CODE = TSPL_PP_PRODUCTION_ENTRY_DETAIL.Unit_Code " & _
                   " LEFT JOIN TSPL_PP_PRODUCTION_ENTRY ON TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
                   " LEFT JOIN TSPL_PP_PRODUCTION_RETURN ON TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE " & _
                   " LEFT JOIN tspl_item_uom_detail ON tspl_item_uom_detail.item_code = tspl_pp_bom_head.prod_item_code " & _
                   " AND tspl_item_uom_detail.uom_code = tspl_pp_bom_head.prod_item_unit_code " & _
                   " LEFT JOIN tspl_item_uom_detail kgconv ON kgconv.item_code = tspl_pp_bom_head.prod_item_code AND kgconv.uom_code = 'KG' " & _
                   " LEFT JOIN (SELECT item_code, MAX([fat %]) AS [FAT %], MAX([snf %]) AS [SNF %] FROM (SELECT tspl_item_qc_parameter_master.item_code, " & _
                   " tspl_item_qc_parameter_master.code, tspl_item_qc_parameter_master.lower_range, tspl_item_qc_parameter_master.upper_range, " & _
                   " tspl_parameter_master.description, tspl_item_qc_parameter_master.status, tspl_item_qc_parameter_master.value1, " & _
                   " tspl_item_qc_parameter_master.value2, tspl_item_qc_parameter_master.actual_range, tspl_item_qc_parameter_master.actual_value, " & _
                   " tspl_item_qc_parameter_master.actual_status FROM tspl_item_qc_parameter_master " & _
                   " LEFT OUTER JOIN tspl_parameter_master ON tspl_item_qc_parameter_master.code = tspl_parameter_master.code WHERE 1 = 1 ) AS sourceQry" & _
                   " PIVOT (MAX(actual_range) FOR description IN ([FAT %], [SNF %])) AS pvt WHERE 1 = 1 GROUP BY item_code) AS pvtQry " & _
                   " ON tspl_pp_bom_item_detail.item_code = pvtQry.item_code where  TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null) AS innerQry1) AS outterQry1 "
            qry += stocking + ") "
            'qry += " SELECT DISTINCT CONCAT(' Main Item :-   ', cte1.[MAIN ITEM], ' | UOM :-  ', cte1.[UOM(Main Item's)], ' | Procduced Qty :-  ', cte1.[Produced Qty] , ' | Item Desc :-  ' ,  cte1.[Main Item Desc] ) AS [Header],  cte1.[S.No], cte1.[BOM Code], cte1.[BOM CREATED DATE],cte1.[Items Desc],cte1.[Items Code],cte1.[Unit (std)],cte1.[FAT % (STD.)],cte1.[SNF % (STD.)],cte1.[TOTAL (STD.)],cte1.[ITEM UOM (ACTUAL)],cte1.[FAT % (ACTUAL)],cte1.[SNF % (ACTUAL)],cte1.[TOTAL (ACTUAL)],cte1.[ITEM UOM (Loss-Gain)],cte1.[FAT % (Loss-Gain)],cte1.[SNF % (Loss-Gain)],cte1.[TOTAL (Loss-Gain)],cte1.[ITEM UOM (VARIANCE)],cte1.[FAT % (VARIANCE)],cte1.[SNF % (VARIANCE)],cte1.[TOTAL (VARIANCE)] FROM cte_itemconsumption_bom_wise cte1 WHERE 1 = 1 "
            qry += " SELECT DISTINCT CONCAT(' Main Item :-   ', cte1.[MAIN ITEM], ' | UOM :-  ', cte1.[UOM(Main Item's)], ' | Procduced Qty :-  ', cte1.[Produced Qty] , ' | Item Desc :-  ' ,  cte1.[Main Item Desc] ) AS [Header],  cte1.[S.No], cte1.[BOM Code], cte1.[BOM CREATED DATE],cte1.[Items Desc],cte1.[Items Code],cte1.[Unit (std)],cte1.[FAT % (STD.)],cte1.[SNF % (STD.)],cte1.[FAT % (STD.)]+cte1.[SNF % (STD.)] as [TS % (STD.)],cte1.[TOTAL (STD.)],cte1.[ITEM UOM (ACTUAL)],cte1.[FAT % (ACTUAL)],cte1.[SNF % (ACTUAL)],cte1.[FAT % (ACTUAL)]+cte1.[SNF % (ACTUAL)] as [TS % (ACTUAL)],cte1.[TOTAL (ACTUAL)],cte1.[ITEM UOM (Loss-Gain)],cte1.[FAT % (Loss-Gain)],cte1.[SNF % (Loss-Gain)],cte1.[FAT % (Loss-Gain)]+cte1.[SNF % (Loss-Gain)] AS [TS % (Loss-Gain)],cte1.[TOTAL (Loss-Gain)],cte1.[ITEM UOM (VARIANCE)],cte1.[FAT % (VARIANCE)],cte1.[SNF % (VARIANCE)],cte1.[FAT % (VARIANCE)]+cte1.[SNF % (VARIANCE)] AS [TS % (VARIANCE)],cte1.[TOTAL (VARIANCE)] FROM cte_itemconsumption_bom_wise cte1 WHERE 1 = 1 "
            qry += " AND convert(date,[Production Date],103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,[Production Date],103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            If txtBOMNo.arrValueMember IsNot Nothing AndAlso txtBOMNo.arrValueMember.Count > 0 Then
                qry += " AND  [BOM CODE] in (" + clsCommon.GetMulcallString(txtBOMNo.arrValueMember) + ")  "
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                qry += " AND [MAIN ITEM] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If
            qry += " ORDER BY [BOM CODE], [S.No] "
            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.GroupDescriptors.Clear()
                gv1.DataSource = dt
                For Each column As GridViewColumn In gv1.Columns
                    If column.Name = "S.No" Then

                        column.Width = 50
                    ElseIf column.Name = "Main Item Desc" Or column.Name = "Header" Or column.Name = "Main Item" Or column.Name = "UOM(Main Item's)" Or column.Name = "MAIN ITEM UOM" Or column.Name = "PRODUCED QTY." Or column.Name = "Produced Qty" Then
                        column.IsVisible = False
                    Else
                        column.Width = 200
                        column.ReadOnly = True
                    End If
                Next
                ' Me.gv1.GroupDescriptors.Expression = "[MAIN ITEM],[MAIN ITEM DESC],[MAIN ITEM UOM],[PRODUCED QTY.]"

                gv1.GroupDescriptors.Add(New GridGroupByExpression("[Header] as Item format ""{0}: {1}"" Group By [Header]"))
                gv1.GroupDescriptors.Add(New GridGroupByExpression("[BOM CODE] as BOM format ""{0}: {2}"" Group By [BOM CODE]"))
                gv1.MasterTemplate.EnableGrouping = True
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Item Consumption Report BOM-Wise")
            End If
            chk_stockingunit.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Load_ItemWise_Report()
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            'qry = " select max(TransType) as TransType,(Date) , [Item Code],max(Description) as [Description],max(LOCATION_CODE) as [Location Code],max(UOM) as [UOM],sum([Production Qty]) as [Production Qty],sum([Production Fat KG]) as [Production Fat KG],sum([Production SNF KG]) as [Production SNF KG] ,sum([Consumption Qty]) as [Consumption Qty],sum([Consumption Fat KG]) as [Consumption Fat KG],sum([Consumption SNF KG]) as [Consumption SNF KG],sum([Issue Qty]) as [Issue Qty],sum([Issue Fat KG]) as [Issue Fat KG],sum([Issue SNF KG]) as [Issue SNF KG] ,max(PROD_ENTRY_CODE) as PROD_ENTRY_CODE "
            qry = " select max(TransType) as TransType,(Date) , [Item Code],max(Description) as [Description],max(LOCATION_CODE) as [Location Code],max(UOM) as [UOM],sum([Production Qty]) as [Production Qty],sum([Production Fat KG]) as [Production Fat KG],sum([Production SNF KG]) as [Production SNF KG],sum([Production Fat KG]) + sum([Production SNF KG]) as [Production TS KG] ,sum([Consumption Qty]) as [Consumption Qty],sum([Consumption Fat KG]) as [Consumption Fat KG],sum([Consumption SNF KG]) as [Consumption SNF KG],sum([Consumption Fat KG]) +sum([Consumption SNF KG]) as [Consumption TS KG],sum([Issue Qty]) as [Issue Qty],sum([Issue Fat KG]) as [Issue Fat KG],sum([Issue SNF KG]) as [Issue SNF KG],sum([Issue Fat KG])+sum([Issue SNF KG]) as [Issue TS KG] ,max(PROD_ENTRY_CODE) as PROD_ENTRY_CODE "
            qry += " from (select ('Production') as TransType,convert(varchar(10),TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)  as [Date], (TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE) as [Item Code],(TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION) as [Description],(TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE) as [UOM],(TSPL_PP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as [Production Qty],(TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG) as [Production Fat KG],(TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG) as [Production SNF KG] ,'' as [Consumption Qty],'' as [Consumption Fat KG],'' as [Consumption SNF KG],'' as [Issue Qty],'' as [Issue Fat KG],'' as [Issue SNF KG],TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE from TSPL_PP_PRODUCTION_ENTRY left join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE  left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE left outer join TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE left outer join TSPL_PP_PE_ISSUE_ITEM_DETAIL on TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and TSPL_PP_PE_ISSUE_ITEM_DETAIL.PROD_ENTRY_CODE= TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE WHERE TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null"

            qry += "  union all"

            qry += " select ('Consumption') as TransType,convert(varchar(10),TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) as [Date],(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE) as RawItem,(TSPL_ITEM_MASTER.item_desc) as [Description],(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE) as [UOM],'' as [Production Qty],'' as [Production Fat KG],'' as [Production SNF KG],(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY) as [Consumption Qty],(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG) as [Consumption Fat KG],(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG) as [Consumption SNF KG],'' as [Issue Qty],'' as [Issue Fat KG],'' as [Issue SNF KG] ,TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,TSPL_PP_PRODUCTION_ENTRY.CONSM_LOCATION_CODE as Location_Code from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE left outer join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE left join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE where TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null "

            qry += " union all"

            qry += " select ('Issue') as TransType,convert(varchar(10),TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) as [Date],(TSPL_PP_PE_ISSUE_ITEM_DETAIL.ITEM_CODE) as RawItem,(TSPL_ITEM_MASTER.item_desc) as [Description],(TSPL_PP_PE_ISSUE_ITEM_DETAIL.UNIT_CODE) as [UOM],'' as [Production Qty],'' as [Production Fat KG],'' as [Production SNF KG],'' as [Consumption Qty],'' as [Consumption Fat KG],'' as [Consumption SNF KG],(TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_Qty) as [Issue Qty],(TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_KG) as [Issue Fat KG],(TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_KG) as [Issue SNF KG],TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,TSPL_PP_PE_ISSUE_ITEM_DETAIL.To_Location_Code as Location_Code from TSPL_PP_PE_ISSUE_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code left outer join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PE_ISSUE_ITEM_DETAIL.PROD_ENTRY_CODE )xx "

            qry += " where 1=1 and convert(date,xx.Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,xx.Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                qry += " AND xx.[Item Code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If
            qry += " group by xx.[Item Code],xx.Date,xx.PROD_ENTRY_CODE order by xx.PROD_ENTRY_CODE "

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.GroupDescriptors.Clear()
                gv1.SummaryRowsBottom.Clear()
                gv1.DataSource = dt
                gv1.Columns("TransType").IsVisible = False
                gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                FormatGrid()
                ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Item Consumption Report Item-Wise")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False         
        Next

        gv1.Columns("Date").IsVisible = True
        gv1.Columns("Date").Width = 100
        gv1.Columns("Date").HeaderText = "Date"

        gv1.Columns("Item Code").IsVisible = True
        gv1.Columns("Item Code").Width = 100
        gv1.Columns("Item Code").HeaderText = "Item Code"

        gv1.Columns("Description").IsVisible = True
        gv1.Columns("Description").Width = 100
        gv1.Columns("Description").HeaderText = "Description"

        gv1.Columns("UOM").IsVisible = True
        gv1.Columns("UOM").Width = 100
        gv1.Columns("UOM").HeaderText = "UOM"

        gv1.Columns("Location Code").IsVisible = True
        gv1.Columns("Location Code").Width = 100
        gv1.Columns("Location Code").HeaderText = "Location Code"

        gv1.Columns("Production Qty").IsVisible = True
        gv1.Columns("Production Qty").Width = 100
        gv1.Columns("Production Qty").HeaderText = "Production Qty"

        gv1.Columns("Production Fat KG").IsVisible = True
        gv1.Columns("Production Fat KG").Width = 100
        gv1.Columns("Production Fat KG").HeaderText = "Production Fat KG"

        gv1.Columns("Production SNF KG").IsVisible = True
        gv1.Columns("Production SNF KG").Width = 100
        gv1.Columns("Production SNF KG").HeaderText = "Production SNF KG"

        gv1.Columns("Consumption Qty").IsVisible = True
        gv1.Columns("Consumption Qty").Width = 100
        gv1.Columns("Consumption Qty").HeaderText = "Consumption Qty"

        gv1.Columns("Consumption Fat KG").IsVisible = True
        gv1.Columns("Consumption Fat KG").Width = 100
        gv1.Columns("Consumption Fat KG").HeaderText = "Consumption Fat KG"

        gv1.Columns("Consumption SNF KG").IsVisible = True
        gv1.Columns("Consumption SNF KG").Width = 100
        gv1.Columns("Consumption SNF KG").HeaderText = "Consumption SNF KG"

        gv1.Columns("Issue Qty").IsVisible = True
        gv1.Columns("Issue Qty").Width = 100
        gv1.Columns("Issue Qty").HeaderText = "Issue Qty"

        gv1.Columns("Issue Fat KG").IsVisible = True
        gv1.Columns("Issue Fat KG").Width = 100
        gv1.Columns("Issue Fat KG").HeaderText = "Issue Fat KG"

        gv1.Columns("Issue SNF KG").IsVisible = True
        gv1.Columns("Issue SNF KG").Width = 100
        gv1.Columns("Issue SNF KG").HeaderText = "Issue SNF KG"

        gv1.Columns("Production TS KG").IsVisible = True
        gv1.Columns("Production TS KG").Width = 100
        gv1.Columns("Consumption TS KG").IsVisible = True
        gv1.Columns("Consumption TS KG").Width = 100
        gv1.Columns("Issue TS KG").IsVisible = True
        gv1.Columns("Issue TS KG").Width = 100

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

       
        Dim item11 As New GridViewSummaryItem("Production Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Production Fat KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Production SNF KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("Consumption Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("Consumption Fat KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("Consumption SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("Issue Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("Issue Fat KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        Dim item19 As New GridViewSummaryItem("Issue SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item19)

        Dim item20 As New GridViewSummaryItem("Production TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)
        Dim item21 As New GridViewSummaryItem("Consumption TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("Issue TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)

        'gv1.GroupDescriptors.Add(New GridGroupByExpression("[Item Code] as Item format ""{0}: {1}"" Group By [Item Code]"))
        'gv1.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Item format ""{0}: {1}"" Group By Route_No"))
        'gv1.GroupDescriptors.Add(New GridGroupByExpression("VLC_Code_VLC_Uploader as Item format ""{0}: {1}"" Group By VLC_Code_VLC_Uploader"))

        'gv1.ShowGroupPanel = False
        'gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportsType.Text)
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportsType.Text)
        TemplateGridview = gv1
        If ddlReportsType.SelectedIndex = 0 Then
            Print(Exporter.Refresh)
        ElseIf ddlReportsType.SelectedIndex = 1 Then
            Load_BOMWise_Report()
        ElseIf ddlReportsType.SelectedIndex = 2 Then
            Load_ItemWise_Report()
        End If
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub RptItemConsumptionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()

            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")

            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptItemConsumptionReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub
    Private Sub txtBOMNo__My_Click(sender As Object, e As EventArgs) Handles txtBOMNo._My_Click
        Try
            Dim qry As String = " SELECT TSPL_PP_BOM_HEAD.BOM_CODE , TSPL_PP_BOM_HEAD.BOM_DATE , TSPL_PP_BOM_HEAD.Is_Post , TSPL_PP_BOM_HEAD.PROD_ITEM_CODE , TSPL_PP_BOM_HEAD.Description ,TSPL_PP_BOM_HEAD.Created_By  FROM TSPL_PP_BOM_HEAD "
            txtBOMNo.arrValueMember = clsCommon.ShowMultipleSelectForm("BOMPRD", qry, "BOM_CODE", "BOM_CODE", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ddlReportsType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReportsType.SelectedIndexChanged
        If ddlReportsType.SelectedIndex = 0 Then
            txtItemMult.Enabled = True
            txtSectionMult.Enabled = True
            txtBatchNoMult.Enabled = True
            TxtMultiLocation.Enabled = True
            txtBOMNo.Enabled = False
        ElseIf ddlReportsType.SelectedIndex = 1 Then
            txtItemMult.Enabled = True
            txtSectionMult.Enabled = False
            txtBatchNoMult.Enabled = False
            TxtMultiLocation.Enabled = False
            txtBOMNo.Enabled = True
        Else
            txtItemMult.Enabled = True
            txtSectionMult.Enabled = False
            txtBatchNoMult.Enabled = False
            TxtMultiLocation.Enabled = False
            txtBOMNo.Enabled = False
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemConsumptionReport & "'"))
                If ddlReportsType.SelectedIndex = 0 Then
                    arrHeader.Add("Name : " & "Item Consumption Report Batch-Wise")
                ElseIf ddlReportsType.SelectedIndex = 1 Then
                    arrHeader.Add("Name : " & "Item Consumption Report BOM-Wise")
                Else
                    arrHeader.Add("Name : " & "Item Consumption Report Item-Wise")
                End If

                If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
                End If
                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If
                If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
                End If
                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Item Consumption Report", gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemConsumptionReport & "'"))
                If ddlReportsType.SelectedIndex = 0 Then
                    arrHeader.Add("Name : " & "Item Consumption Report Batch-Wise")
                ElseIf ddlReportsType.SelectedIndex = 1 Then
                    arrHeader.Add("Name : " & "Item Consumption Report BOM-Wise")
                Else
                    arrHeader.Add("Name : " & "Item Consumption Report Item-Wise")
                End If

                If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
                End If
                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If
                If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
                End If
                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Item Consumption Report", gv1, arrHeader, "Item Consumption Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    
End Class
