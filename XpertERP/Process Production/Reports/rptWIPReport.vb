Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Preeti Gupta against ticket no[BM00000008459,BM00000008475]-==============
'Ticket No-No  ERO/09/07/19-000677 ,Sanjay Add TSKG,TS%
Public Class RptWIPReport
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptWTPReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    'Sub Print(ByVal IsPrint As Exporter)
    Sub loadReport()
        ' Ticket No : BHA/16/07/18-000171 By Prabhakar - For  Manual Batch No column Add 
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
        'If txtBatchNoMult.arrDispalyMember IsNot Nothing AndAlso txtBatchNoMult.arrDispalyMember.Count > 0 Then
        '    arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
        'End If
        If txtItemMult.arrDispalyMember IsNot Nothing AndAlso txtItemMult.arrDispalyMember.Count > 0 Then
            arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
        End If
        Dim qry As String = Nothing

        Dim subQry As String = GetBaseQuery()

        '=========================================================================================================================================
        ' = ADDED BY KUNAL : TICKET BM00000009509  ' Add UOM column in Daily Production Report '
        '=========================================================================================================================================
        Dim Baseqry As String = ""

        If rbtnDetail.IsChecked Then
            Baseqry = " select  coalesce(xx.section,'')+coalesce(xx.location_code,'')+convert(varchar,xx.Standardization_Date,103)+coalesce(xx.[Item Code],'') as XgroupCode,xx.[Type] , xx.Section,xx.Location_Code as [Location Code],TSPL_LOCATION_MASTER .Location_Desc as [Location Name],xx.[Doc Code] as [Doc No],convert(varchar,xx.Standardization_Date,103) as [Standardization Date] ,xx.[Item Code] ,TSPL_ITEM_MASTER .Item_Desc as [Item Name],"
            If chk_stockingunit.Checked Then
                Baseqry += " stockunitconv.UOM_Code as UOM ,"
            Else
                Baseqry += " xx.Unit_Code as UOM ,"
            End If

            Baseqry += " case when TSPL_ITEM_MASTER .Item_Type='R'then 'Raw Material' " & _
             " when TSPL_ITEM_MASTER .Item_Type='F'then 'Finished Good' " & _
             " when TSPL_ITEM_MASTER .Item_Type='S'then 'Semi Finished Good'  " & _
             "  when TSPL_ITEM_MASTER .Item_Type='A'then 'Asset'  " & _
             "  when TSPL_ITEM_MASTER .Item_Type='T'then 'Trading Good'   " & _
             "  when TSPL_ITEM_MASTER .Item_Type='O'then 'Other' " & _
             " end  as [Item Type]," & _
             " case when tspl_item_master.Product_Type=''then 'Other' when "
            Baseqry += " tspl_item_master.Product_Type='MI'then 'Milk' "
            Baseqry += " when tspl_item_master.Product_Type='MB'then 'Melted Butter'  "
            Baseqry += "  when tspl_item_master.Product_Type='CH'then 'Cheese'  "
            Baseqry += " when tspl_item_master.Product_Type='CU'then 'Curd'  "
            Baseqry += "  when tspl_item_master.Product_Type='CA'then 'Cream'   "
            Baseqry += "  when tspl_item_master.Product_Type='DG'then 'Desi-Ghee'  "
            Baseqry += " when tspl_item_master.Product_Type='BU'then 'Butter' "
            Baseqry += " when tspl_item_master.Product_Type='BM'then 'Butter Milk'  "
            Baseqry += " when tspl_item_master.Product_Type='PS'then 'Paper Seal'  "
            Baseqry += " when tspl_item_master.Product_Type='MS'then 'Manual Seal'  "
            Baseqry += " when tspl_item_master.Product_Type='MP'then 'Milk Product' "
            Baseqry += " end    as [Item Group Type]"
            If chk_stockingunit.Checked Then
                Baseqry += ",xx.batch_code as [Batch Code],xx.ManualBatchNo as [Manual Batch No],xx.sub_batch_code as [Child Batch Code] " & _
                ",((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) as [Op_Qty], " & _
                " ((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) as [Op_Fat_Kg], " & _
                " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) as [Op_SNF_Kg] "

                Baseqry += " ,((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) + " & _
                " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) as [Op TS Kg] "

                Baseqry += ",((isnull(unitconv.Conversion_Factor,1)*xx.[In Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Quantity],((isnull(unitconv.Conversion_Factor,1)*xx.[In FAT kg])/isnull(stockunitconv.Conversion_Factor,1)) as [Fat_Kg],((isnull(unitconv.Conversion_Factor,1)*xx.[In SNF Kg])/isnull(stockunitconv.Conversion_Factor,1)) as [SNF_Kg] " & _
                    ",((isnull(unitconv.Conversion_Factor,1)*xx.[In FAT kg])/isnull(stockunitconv.Conversion_Factor,1)) + ((isnull(unitconv.Conversion_Factor,1)*xx.[In SNF Kg])/isnull(stockunitconv.Conversion_Factor,1)) as [TS Kg] " & _
                    ",((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) - ((isnull(unitconv.Conversion_Factor,1)*xx.[In Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Qty], " & _
                    " ((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) - ((isnull(unitconv.Conversion_Factor,1)*xx.[In FAT kg])/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Fat Kg], " & _
                    " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) - ((isnull(unitconv.Conversion_Factor,1)*xx.[In SNF Kg])/isnull(stockunitconv.Conversion_Factor,1)) as [Bal SNF Kg] " & _
                    " ,((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) - ((isnull(unitconv.Conversion_Factor,1)*xx.[In FAT kg])/isnull(stockunitconv.Conversion_Factor,1)) + " & _
                    " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end) - ((isnull(unitconv.Conversion_Factor,1)*xx.[In SNF Kg])/isnull(stockunitconv.Conversion_Factor,1)) as [Bal TS Kg] " & _
                  " from ("
            Else
                Baseqry += ",xx.batch_code as [Batch Code],xx.ManualBatchNo as [Manual Batch No],xx.sub_batch_code as [Child Batch Code] " & _
                ",((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) as [Op_Qty], " & _
                " ((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) as [Op_Fat_Kg], " & _
                " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) as [Op_SNF_Kg] "

                Baseqry += " ,((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) + " & _
                " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) as [Op TS Kg] "

                Baseqry += ",xx.[In Qty] as [Quantity],xx.[In FAT kg] as [Fat_Kg],xx.[In SNF Kg] as [SNF_Kg],xx.[In FAT kg]+xx.[In SNF Kg] as [TS Kg] " & _
                    ",((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) - xx.[In Qty] as [Bal Qty], " & _
                    " ((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) - xx.[In FAT kg] as [Bal Fat Kg], " & _
                    " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) - xx.[In SNF kg] as [Bal SNF Kg] " & _
                    " ,((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) - xx.[In FAT kg] + " & _
                    " ((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end) - xx.[In SNF kg] as [Bal TS Kg] " & _
                    " from ("
            End If
            

            Baseqry += " " + subQry + ")as xx"
            Baseqry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =xx.[item code] left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =xx .Location_Code "

            ''=====================for stock opening==========================================================
            Baseqry += " left outer join TSPL_INV_MOVE_DL on TSPL_INV_MOVE_DL.item_code=xx.[Item Code] and TSPL_INV_MOVE_DL.location_code=coalesce(xx.section,xx.Location_Code) and convert(date, xx.Standardization_Date,103)=convert(date,TSPL_INV_MOVE_DL.trans_date,103) "
            If chk_stockingunit.Checked Then
                Baseqry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=xx.[Item Code]" & _
                             " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=xx.[Item Code] and unitconv.UOM_Code=xx.unit_code " & _
                             " left outer join TSPL_ITEM_UOM_DETAIL as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom"
            Else
                Baseqry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=xx.[Item Code] and tspl_item_uom_detail.uom_code=xx.unit_code " & _
                            " left outer join tspl_item_uom_detail as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom  "
            End If
            
            ''=============================================================================================
            Baseqry += " where 2=2 and xx.[In Qty]<>0 and  convert(date, xx.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date, xx.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                Baseqry += " and  xx.[item code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                Baseqry += " and xx.Section in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                Baseqry += " and xx.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
            End If
            Baseqry += " order by xx.Standardization_Date,[Doc No]"
        ElseIf rbtnSummary.IsChecked Then
            If clsCommon.CompairString(cboReportType.SelectedValue, "") = CompairStringResult.Equal Then
                cboReportType.Focus()
                cboReportType.Select()
                clsCommon.MyMessageBoxShow("Select Summary Type First.")
                Exit Sub
            End If

            If clsCommon.CompairString(cboReportType.SelectedValue, "P") = CompairStringResult.Equal Then ''Period wise
                Baseqry = " select  convert(varchar,xx.Standardization_Date,103)+coalesce(xx.[Item Code],'') as XgroupCode,xx.[Type] ,convert(varchar,xx.Standardization_Date,103) as [Standardization Date],xx.[Item Code] ,max(TSPL_ITEM_MASTER .Item_Desc) as [Item Name],"
                If chk_stockingunit.Checked Then
                    Baseqry += " max(stockunitconv.UOM_Code) as UOM ,"
                Else
                    Baseqry += " max(xx.Unit_Code) as UOM ,"
                End If
                Baseqry += " max(case when TSPL_ITEM_MASTER .Item_Type='R'then 'Raw Material' "
                Baseqry += " when TSPL_ITEM_MASTER .Item_Type='F'then 'Finished Good' "
                Baseqry += " when TSPL_ITEM_MASTER .Item_Type='S'then 'Semi Finished Good'  "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='A'then 'Asset'  "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='T'then 'Trading Good'   "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='O'then 'Other' "
                Baseqry += " end) as [Item Type],"
                Baseqry += " max(case when tspl_item_master.Product_Type=''then 'Other' when "
                Baseqry += " tspl_item_master.Product_Type='MI'then 'Milk' "
                Baseqry += " when tspl_item_master.Product_Type='MB'then 'Melted Butter'  "
                Baseqry += "  when tspl_item_master.Product_Type='CH'then 'Cheese'  "
                Baseqry += " when tspl_item_master.Product_Type='CU'then 'Curd'  "
                Baseqry += "  when tspl_item_master.Product_Type='CA'then 'Cream'   "
                Baseqry += "  when tspl_item_master.Product_Type='DG'then 'Desi-Ghee'  "
                Baseqry += " when tspl_item_master.Product_Type='BU'then 'Butter' "
                Baseqry += " when tspl_item_master.Product_Type='BM'then 'Butter Milk'  "
                Baseqry += " when tspl_item_master.Product_Type='PS'then 'Paper Seal'  "
                Baseqry += " when tspl_item_master.Product_Type='MS'then 'Manual Seal'  "
                Baseqry += " when tspl_item_master.Product_Type='MP'then 'Milk Product' "
                Baseqry += " end) as [Item Group Type]"
                'Baseqry += ",xx.batch_code as [Batch Code],xx.sub_batch_code as [Child Batch Code] "
                If chk_stockingunit.Checked Then
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_Qty], " & _
               " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_Fat_Kg], " & _
               " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_SNF_Kg] "

                    Baseqry += ", avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) + " & _
                    " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op TS Kg] "

                    Baseqry += ",sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In Qty],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Quantity],sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Fat_Kg],sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [SNF_Kg] "
                    Baseqry += ",sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) + sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [TS Kg] "

                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In Qty],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Qty], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Fat Kg], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal SNF Kg] " & _
                        " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) + " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal TS Kg] " & _
                        " from ( "
                Else
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_Qty], " & _
               " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_Fat_Kg], " & _
               " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_SNF_Kg] "
                    Baseqry += " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) + " & _
                     " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op TS Kg] "
                    Baseqry += ",sum(isnull(xx.[In Qty],0)) as [Quantity],sum(isnull(xx.[In FAT kg],0)) as [Fat_Kg],sum(isnull(xx.[In SNF Kg],0)) as [SNF_Kg],sum(isnull(xx.[In FAT kg],0)) +sum(isnull(xx.[In SNF Kg],0)) as [TS Kg] "

                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In Qty],0)) as [Bal Qty], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In FAT kg],0)) as [Bal Fat Kg], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In SNF kg],0)) as [Bal SNF Kg] " & _
                        " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In FAT kg],0)) + " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In SNF kg],0)) as [Bal TS Kg] " & _
                        " from ( "
                End If
               

                Baseqry += " " + subQry + ")as xx"
                Baseqry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =xx.[item code] left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =xx .Location_Code "

                ''=====================for stock opening==========================================================
                Baseqry += " left outer join TSPL_INV_MOVE_DL on TSPL_INV_MOVE_DL.item_code=xx.[Item Code] and TSPL_INV_MOVE_DL.location_code=coalesce(xx.section,xx.Location_Code) and convert(date, xx.Standardization_Date,103)=convert(date,TSPL_INV_MOVE_DL.trans_date,103) "
                If chk_stockingunit.Checked Then
                    Baseqry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=xx.[Item Code]" & _
                                 " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=xx.[Item Code] and unitconv.UOM_Code=xx.unit_code " & _
                                 " left outer join TSPL_ITEM_UOM_DETAIL as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom"
                Else
                    Baseqry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=xx.[Item Code] and tspl_item_uom_detail.uom_code=xx.unit_code " & _
                                " left outer join tspl_item_uom_detail as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom  "
                End If
                ''=============================================================================================

                Baseqry += " where 2=2 and xx.[In Qty]<>0 and convert(date, xx.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date, xx.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    Baseqry += " and  xx.[item code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
                End If
                If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                    Baseqry += " and xx.Section in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
                End If
                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    Baseqry += " and xx.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
                End If
                Baseqry += " group by xx.[Type] ,xx.[Item Code],xx.Standardization_Date order by xx.Standardization_Date,[Item Code]"
            ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "I") = CompairStringResult.Equal Then ''Item wise
                Baseqry = " select  coalesce(xx.[Item Code],'') as XgroupCode,xx.[Type],xx.[Item Code] ,max(TSPL_ITEM_MASTER .Item_Desc) as [Item Name] ,"
                If chk_stockingunit.Checked Then
                    Baseqry += " max(stockunitconv.UOM_Code) as UOM ,"
                Else
                    Baseqry += " max(xx.Unit_Code) as UOM ,"
                End If
                Baseqry += " max(case when TSPL_ITEM_MASTER .Item_Type='R'then 'Raw Material' "
                Baseqry += " when TSPL_ITEM_MASTER .Item_Type='F'then 'Finished Good' "
                Baseqry += " when TSPL_ITEM_MASTER .Item_Type='S'then 'Semi Finished Good'  "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='A'then 'Asset'  "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='T'then 'Trading Good'   "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='O'then 'Other' "
                Baseqry += " end)  as [Item Type],"
                Baseqry += " max(case when tspl_item_master.Product_Type=''then 'Other' when "
                Baseqry += " tspl_item_master.Product_Type='MI'then 'Milk' "
                Baseqry += " when tspl_item_master.Product_Type='MB'then 'Melted Butter'  "
                Baseqry += "  when tspl_item_master.Product_Type='CH'then 'Cheese'  "
                Baseqry += " when tspl_item_master.Product_Type='CU'then 'Curd'  "
                Baseqry += "  when tspl_item_master.Product_Type='CA'then 'Cream'   "
                Baseqry += "  when tspl_item_master.Product_Type='DG'then 'Desi-Ghee'  "
                Baseqry += " when tspl_item_master.Product_Type='BU'then 'Butter' "
                Baseqry += " when tspl_item_master.Product_Type='BM'then 'Butter Milk'  "
                Baseqry += " when tspl_item_master.Product_Type='PS'then 'Paper Seal'  "
                Baseqry += " when tspl_item_master.Product_Type='MS'then 'Manual Seal'  "
                Baseqry += " when tspl_item_master.Product_Type='MP'then 'Milk Product' "
                Baseqry += " end) as [Item Group Type]"
                If chk_stockingunit.Checked Then
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_Qty], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_Fat_Kg], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_SNF_Kg] "

                    Baseqry += " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) + " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op TS Kg] "

                    Baseqry += ",sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In Qty],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Quantity],sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Fat_Kg],sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [SNF_Kg] "
                    Baseqry += ",sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) +sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [TS Kg] "
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In Qty],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Qty], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Fat Kg], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal SNF Kg] " & _
                        ",avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) + " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal TS Kg] " & _
                   " from ( "
                Else
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_Qty], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_Fat_Kg], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_SNF_Kg] "

                    Baseqry += " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) + " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op TS Kg] "

                    Baseqry += ",sum(isnull(xx.[In Qty],0)) as [Quantity],sum(isnull(xx.[In FAT kg],0)) as [Fat_Kg],sum(isnull(xx.[In SNF Kg],0)) as [SNF_Kg],sum(isnull(xx.[In FAT kg],0)) + sum(isnull(xx.[In SNF Kg],0)) as [TS Kg] "

                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In Qty],0)) as [Bal Qty], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In FAT kg],0)) as [Bal Fat Kg], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In SNF kg],0)) as [Bal SNF Kg] " & _
                        " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In FAT kg],0)) + " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In SNF kg],0)) as [Bal TS Kg] " & _
                        " from ( "
                End If
                

                Baseqry += " " + subQry + ")as xx"
                Baseqry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =xx.[item code] "

                ''=====================for stock opening==========================================================
                Baseqry += " left outer join TSPL_INV_MOVE_DL on TSPL_INV_MOVE_DL.item_code=xx.[Item Code] and TSPL_INV_MOVE_DL.location_code=coalesce(xx.section,xx.Location_Code) and convert(date, xx.Standardization_Date,103)=convert(date,TSPL_INV_MOVE_DL.trans_date,103) "
                If chk_stockingunit.Checked Then
                    Baseqry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=xx.[Item Code]" & _
                                                " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=xx.[Item Code] and unitconv.UOM_Code=xx.unit_code " & _
                                                " left outer join TSPL_ITEM_UOM_DETAIL as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom"
                Else
                    Baseqry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=xx.[Item Code] and tspl_item_uom_detail.uom_code=xx.unit_code " & _
                                " left outer join tspl_item_uom_detail as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom  "
                End If
            ''=============================================================================================

            Baseqry += " where 2=2 and xx.[In Qty]<>0 and convert(date, xx.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date, xx.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                Baseqry += " and  xx.[item code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                Baseqry += " and xx.Section in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                Baseqry += " and xx.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
            End If
            Baseqry += " group by xx.[Type],xx.[Item Code] order by xx.[Item Code]"
            ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "S") = CompairStringResult.Equal Then ''Section wise
                Baseqry = " select  coalesce(xx.section,'')+coalesce(xx.location_code,'')+coalesce(xx.[Item Code],'') as XgroupCode,xx.[Type] , xx.Section,xx.Location_Code as [Location Code],max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,xx.[Item Code] ,max(TSPL_ITEM_MASTER .Item_Desc) as [Item Name] ,"
                If chk_stockingunit.Checked Then
                    Baseqry += " max(stockunitconv.UOM_Code) as UOM ,"
                Else
                    Baseqry += " max(xx.Unit_Code) as UOM ,"
                End If
                Baseqry += " max(case when TSPL_ITEM_MASTER .Item_Type='R'then 'Raw Material' "
                Baseqry += " when TSPL_ITEM_MASTER .Item_Type='F'then 'Finished Good' "
                Baseqry += " when TSPL_ITEM_MASTER .Item_Type='S'then 'Semi Finished Good'  "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='A'then 'Asset'  "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='T'then 'Trading Good'   "
                Baseqry += "  when TSPL_ITEM_MASTER .Item_Type='O'then 'Other' "
                Baseqry += " end)  as [Item Type],"
                Baseqry += " max(case when tspl_item_master.Product_Type=''then 'Other' when "
                Baseqry += " tspl_item_master.Product_Type='MI'then 'Milk' "
                Baseqry += " when tspl_item_master.Product_Type='MB'then 'Melted Butter'  "
                Baseqry += "  when tspl_item_master.Product_Type='CH'then 'Cheese'  "
                Baseqry += " when tspl_item_master.Product_Type='CU'then 'Curd'  "
                Baseqry += "  when tspl_item_master.Product_Type='CA'then 'Cream'   "
                Baseqry += "  when tspl_item_master.Product_Type='DG'then 'Desi-Ghee'  "
                Baseqry += " when tspl_item_master.Product_Type='BU'then 'Butter' "
                Baseqry += " when tspl_item_master.Product_Type='BM'then 'Butter Milk'  "
                Baseqry += " when tspl_item_master.Product_Type='PS'then 'Paper Seal'  "
                Baseqry += " when tspl_item_master.Product_Type='MS'then 'Manual Seal'  "
                Baseqry += " when tspl_item_master.Product_Type='MP'then 'Milk Product' "
                Baseqry += " end) as [Item Group Type]"
                'Baseqry += ",xx.batch_code as [Batch Code],xx.sub_batch_code as [Child Batch Code] "
                If chk_stockingunit.Checked Then
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_Qty], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_Fat_Kg], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op_SNF_Kg] "

                    Baseqry += " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) + " & _
                     " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) as [Op TS Kg] "

                    Baseqry += ",sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In Qty],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Quantity],sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Fat_Kg],sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [SNF_Kg] "
                    Baseqry += ",sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) +sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF Kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [TS Kg]"
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In Qty],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Qty], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal Fat Kg], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal SNF Kg] " & _
                        " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In FAT kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) + " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(stockunitconv.conversion_factor,0)=0 then 1 else isnull(stockunitconv.conversion_factor,0) end)) - sum((isnull(unitconv.Conversion_Factor,1)*(isnull(xx.[In SNF kg],0)))/isnull(stockunitconv.Conversion_Factor,1)) as [Bal TS Kg] " & _
                        " from ( "
                Else
                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_Qty], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_Fat_Kg], " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op_SNF_Kg] "

                    Baseqry += " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) + " & _
                " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) as [Op TS Kg] "

                    Baseqry += ",sum(isnull(xx.[In Qty],0)) as [Quantity],sum(isnull(xx.[In FAT kg],0)) as [Fat_Kg],sum(isnull(xx.[In SNF Kg],0)) as [SNF_Kg],sum(isnull(xx.[In FAT kg],0)) + sum(isnull(xx.[In SNF Kg],0)) as [TS Kg] "

                    Baseqry += ",avg(((isnull(TSPL_INV_MOVE_DL.cl_qty,0) - isnull(TSPL_INV_MOVE_DL.trans_qty,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In Qty],0)) as [Bal Qty], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In FAT kg],0)) as [Bal Fat Kg], " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In SNF kg],0)) as [Bal SNF Kg] " & _
                        " ,avg(((isnull(TSPL_INV_MOVE_DL.cl_fat_kg,0)-isnull(TSPL_INV_MOVE_DL.fat_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In FAT kg],0)) + " & _
                        " avg(((isnull(TSPL_INV_MOVE_DL.cl_snf_kg,0)-isnull(TSPL_INV_MOVE_DL.snf_kg,0)) * isnull(UOMDET.conversion_factor,0)) / (case when isnull(tspl_item_uom_detail.conversion_factor,0)=0 then 1 else isnull(tspl_item_uom_detail.conversion_factor,0) end)) - sum(isnull(xx.[In SNF kg],0)) as [Bal TS Kg] " & _
                        " from ( "
                End If
                

                Baseqry += " " + subQry + ")as xx"
                Baseqry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =xx.[item code] left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =xx .Location_Code "

                ''=====================for stock opening==========================================================
                Baseqry += " left outer join TSPL_INV_MOVE_DL on TSPL_INV_MOVE_DL.item_code=xx.[Item Code] and TSPL_INV_MOVE_DL.location_code=coalesce(xx.section,xx.Location_Code) and convert(date, xx.Standardization_Date,103)=convert(date,TSPL_INV_MOVE_DL.trans_date,103) "
                
                If chk_stockingunit.Checked Then
                    Baseqry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=xx.[Item Code]" & _
                                 " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=xx.[Item Code] and unitconv.UOM_Code=xx.unit_code " & _
                                 " left outer join TSPL_ITEM_UOM_DETAIL as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom"
                Else
                    Baseqry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=xx.[Item Code] and tspl_item_uom_detail.uom_code=xx.unit_code " & _
                                " left outer join tspl_item_uom_detail as UOMDET on UOMDET.item_code=xx.[Item Code] and UOMDET.uom_code=TSPL_INV_MOVE_DL.stock_uom  "
                End If
                ''=============================================================================================

                Baseqry += " where 2=2 and xx.[In Qty]<>0 and convert(date, xx.Standardization_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date, xx.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    Baseqry += " and  xx.[item code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
                End If
                If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                    Baseqry += " and xx.Section in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
                End If
                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    Baseqry += " and xx.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
                End If
                Baseqry += " group by xx.[Type] , xx.Section,xx.Location_Code,xx.[Item Code] order by xx.section,xx.[Item Code]"
            End If
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Baseqry)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        SetGridFormationOFGV1()
        ' gv1.Columns("Standardization_Date").IsVisible = False
        If gv1.Columns.Contains("Standardization Date") Then
            gv1.Columns("Standardization Date").HeaderText = "Date"
        End If

        gv1.BestFitColumns()
        chk_stockingunit.Enabled = False
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()
    End Sub

    Private Function GetBaseQuery() As String
        Dim Baseqry As String = "select 'Production' as Type , final.Standardization_Date ,final.Section_Code as [Section],final.batch_code,final.ManualBatchNo,final.sub_batch_code ,final.Std_Code as [Doc Code],final.std_Item_Code as [Item Code],final.Prd_Qty as [In Qty],final.Prd_Fat_Kg as [In FAT kg],final.Prd_SNF_Kg as [In SNF Kg],Location_Code , Unit_Code from " & _
            " (select   TSPL_PP_BATCH_ORDER_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo,stuff(TSPL_PP_BATCH_ORDER_HEAD.sub_batch_code,1,1,'') as sub_batch_code,BOProduction.Standardization_Date ,TSPL_PP_BATCH_ORDER_HEAD.Section_Code  ,BOProduction.Std_Code,BOProduction.std_Item_Code  ,BOProduction.Prd_Qty ,BOProduction.Prd_SNF_Per ,BOProduction.Prd_FAT_Per ,BOProduction.Prd_Fat_Kg ,BOProduction.Prd_SNF_Kg,TSPL_PP_BATCH_ORDER_HEAD.Location_Code ,Unit_Code as  Unit_Code " & _
            "  from TSPL_PP_BATCH_ORDER_HEAD " & _
        " left join (select TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code as Std_Code,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date ,TSPL_PP_STANDARDIZATION_HEAD.Main_Batch_Code as Std_Batch  ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code as std_Item_Code, TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code as Unit_Code , TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Quantity   as Batch_Qty," & _
        " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty    as Prd_Qty,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_per    as Prd_FAT_Per,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_per    as Prd_SNF_Per,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG    as Prd_Fat_Kg,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG    as Prd_SNF_Kg  " & _
        " from TSPL_PP_STANDARDIZATION_HEAD left join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code =TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "

        ''===============================below query show add/remove items at standardization==========================
        Baseqry += "  union all " & _
        " select TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code as Std_Code,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date ,TSPL_PP_STANDARDIZATION_HEAD.Main_Batch_Code as Std_Batch  ,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code as std_Item_Code, TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Unit_Code as Unit_Code , 0 as Batch_Qty " & _
            " ,case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.add_remove_type='Add' then -1 * isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.add_remove_qty,0) else isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.add_remove_qty,0) end as Prd_Qty,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_FAT_per as Prd_FAT_Per " & _
            " ,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_snf_per as Prd_SNF_Per,case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.add_remove_type='Add' then -1 * isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_FAT_KG,0) else isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_fat_kg,0) end as Prd_Fat_Kg " & _
            " ,case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.add_remove_type='Add' then -1 * isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_SNF_KG,0) else isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ar_snf_kg,0) end as Prd_SNF_Kg " & _
            " from TSPL_PP_STANDARDIZATION_HEAD left join TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code =TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        ''===============================end query show add/remove items==========================

        Baseqry += " union all" & _
        " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE as prd_Code,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as prd_Item_code , TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE as prd_unit_code  " & _
        " , TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY  as Batch_Qty," & _
        " TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY as Prd_Qty,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per as prd_FAT_Per,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per as Prd_SNF_Per,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG as Prd_FAT_KG,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as Prd_SNF_Kg" & _
        " from  TSPL_PP_PRODUCTION_ENTRY  left join " & _
        " TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE "

        ''===============================below query show wreakage items at production entry==========================
        Baseqry += "  union all" & _
        " select TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE as prd_Code,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch ,TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE as prd_Item_code , TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code as prd_unit_code, 0 as Batch_Qty," & _
        " case when coalesce(TSPL_PP_PE_WRECKAGE_FLASHING.wreckage_qty,0)>0 then isnull(TSPL_PP_PE_WRECKAGE_FLASHING.wreckage_qty,0) else -1 * isnull(TSPL_PP_PE_WRECKAGE_FLASHING.back_qty,0) end as Prd_Qty,isnull(TSPL_PP_PE_WRECKAGE_FLASHING.avail_FAT_Per,0) as prd_FAT_Per, " & _
            " isnull(TSPL_PP_PE_WRECKAGE_FLASHING.avail_SNF_Per,0) as Prd_SNF_Per,case when coalesce(TSPL_PP_PE_WRECKAGE_FLASHING.wreckage_qty,0)>0 then isnull(TSPL_PP_PE_WRECKAGE_FLASHING.avail_FAT_KG,0) else -1 * isnull(TSPL_PP_PE_WRECKAGE_FLASHING.avail_FAT_KG,0) end as Prd_FAT_KG, " & _
            " case when coalesce(TSPL_PP_PE_WRECKAGE_FLASHING.wreckage_qty,0)>0 then isnull(TSPL_PP_PE_WRECKAGE_FLASHING.avail_SNF_KG,0) else -1 * isnull(TSPL_PP_PE_WRECKAGE_FLASHING.avail_SNF_KG,0) end as Prd_SNF_Kg" & _
        " from  TSPL_PP_PRODUCTION_ENTRY  left join " & _
        " TSPL_PP_PE_WRECKAGE_FLASHING on TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE "
        ''===============================end query show wreakage items==========================

        ''===============================below query show scrab items at production entry==========================
        Baseqry += " union all " & _
         " select TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE as prd_Code,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,TSPL_PP_PRODUCTION_ENTRY.Batch_Code as Prd_Batch ,TSPL_PP_PE_SCRAP_DETAIL.ITEM_CODE as prd_Item_code , TSPL_PP_PE_SCRAP_DETAIL.Unit_Code as prd_unit_code, 0 as Batch_Qty, " & _
         " TSPL_PP_PE_SCRAP_DETAIL.SCRAP_QTY as Prd_Qty,isnull(TSPL_PP_PE_SCRAP_DETAIL.avail_FAT_Per,0) as prd_FAT_Per, isnull(TSPL_PP_PE_SCRAP_DETAIL.avail_SNF_Per,0) as Prd_SNF_Per,TSPL_PP_PE_SCRAP_DETAIL.Avail_FAT_KG as Prd_FAT_KG, " & _
         " TSPL_PP_PE_SCRAP_DETAIL.Avail_SNF_KG as Prd_SNF_Kg from  TSPL_PP_PRODUCTION_ENTRY  left join TSPL_PP_PE_SCRAP_DETAIL on TSPL_PP_PE_SCRAP_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PE_SCRAP_DETAIL.PROD_ENTRY_CODE "
        ''===============================end query show wreakage items==========================

        ''=================================below query is of stage process====================
        Baseqry += "union all " & _
         " select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE as prd_Code,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE ,TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code as Prd_Batch ,TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.ITEM_CODE as prd_Item_code , TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code as prd_unit_code, TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Quantity as Batch_Qty, " & _
         " TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty as Prd_Qty,case when isnull(TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty,0)>0 then (isnull(TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG,0)/isnull(TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.produced_qty,0)) * 100 else 0 end as prd_FAT_Per, " & _
         " case when isnull(TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty,0)>0 then (isnull(TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG,0)/isnull(TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.produced_qty,0)) * 100 else 0 end as Prd_SNF_Per,TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG as Prd_FAT_KG, " & _
         " TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG as Prd_SNF_Kg from  TSPL_PP_STAGE_PROCESS_HEAD  left join TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE =TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.STAGE_PROCESS_CODE "

        ''=================================below query is of stage process -> issue item detail====================
        Baseqry += " union all " & _
         " select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE as prd_Code,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE ,TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code as Prd_Batch ,TSPL_PP_SP_ISSUE_ITEM_DETAIL.ITEM_CODE as prd_Item_code , TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code as prd_unit_code, 0 as Batch_Qty," & _
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_Qty as Prd_Qty,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_Per as prd_FAT_Per,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Prd_SNF_Per,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Prd_FAT_KG, " & _
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Prd_SNF_Kg from  TSPL_PP_STAGE_PROCESS_HEAD  left join TSPL_PP_SP_ISSUE_ITEM_DETAIL on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE =TSPL_PP_SP_ISSUE_ITEM_DETAIL.STAGE_PROCESS_CODE "

        ''=================================below query is of stage process -> add/remove item detail====================
        Baseqry += " union all " & _
         " select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE as prd_Code,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE ,TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code as Prd_Batch ,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ITEM_CODE as prd_Item_code , TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Unit_Code as prd_unit_code, 0 as Batch_Qty," & _
         " case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then -1 * TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY else TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY end as Prd_Qty,0 as prd_FAT_Per,0 as Prd_SNF_Per,0 as Prd_FAT_KG,0 as Prd_SNF_Kg " & _
         " from  TSPL_PP_STAGE_PROCESS_HEAD  left join TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE =TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE "
        ''===============================end query==========================

        Baseqry += " ) as BOProduction on BOProduction.Std_Batch =TSPL_PP_BATCH_ORDER_HEAD.Batch_Code where coalesce(BOProduction.std_Item_Code,'')<>'') as final "

        '--bada wala union----------
        Baseqry += "  union all" & _
        " select  'consumption' as Type,final.Standardization_Date ,final.CONSM_SECTION_CODE as [Section],final.main_batch_code as batch_code,final.ManualBatchNo,final.child_batch_code as sub_batch_code,final.Stand_code as [Doc Code],final.CONSM_ITEM_CODE as [Item Code],-1 * isnull(final.CONSM_QTY,0) as [In Qty],-1 * isnull(final.Consm_FAT_Kg,0) as [In FAT Kg],-1 * isnull(final.Consm_SNf_Kg,0) as [In SNF Kg],Loaction_Code as Location_code ,  final.Consm_Unit_COde  from (select " & _
        " TSPL_PP_STANDARDIZATION_HEAD.main_batch_code,TSPL_PP_STANDARDIZATION_HEAD.ManualBatchNo,TSPL_PP_STANDARDIZATION_HEAD.child_batch_code,TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code ,TSPL_PP_STANDARDIZATION_HEAD.CONSM_SECTION_CODE," & _
        " std.*,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Item_Code as Issue_Item ,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_Qty  as Issue_Qty,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Issue_Fat_Per,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Issue_SNF_per,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Issue_Fat_Kg,TSPL_PP_STD_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Issue_SNF_Kg from (select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code as Stand_code,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE as Consm_Unit_COde,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per as consm_Fat_Per,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per as Consm_SNF_Per ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG as Consm_FAT_Kg ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG as Consm_SNf_Kg  from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where Standardization_Code  is not null) std" & _
        " left  join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL  on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code =std.Stand_code" & _
        " inner  join TSPL_PP_STANDARDIZATION_HEAD  on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code =std.Stand_code" & _
        " left join TSPL_PP_STD_ISSUE_ITEM_DETAIL  on TSPL_PP_STD_ISSUE_ITEM_DETAIL.Standardization_Code =std.Stand_code and TSPL_PP_STD_ISSUE_ITEM_DETAIL.Item_Code =std.CONSM_ITEM_CODE " & _
        "  union all" & _
        " select case when (SUBSTRING(TSPL_PP_PRODUCTION_ENTRY.batch_code,1,2)='C-' or SUBSTRING('c-bo',1,2)='SC') then NULL else TSPL_PP_PRODUCTION_ENTRY.batch_code end as main_batch_code,TSPL_PP_PRODUCTION_ENTRY.ManualBatchNo,case when (SUBSTRING(TSPL_PP_PRODUCTION_ENTRY.batch_code,1,2)='C-' or SUBSTRING('c-bo',1,2)='SC') then TSPL_PP_PRODUCTION_ENTRY.batch_code else NULL end as child_batch_code,TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE ,TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE as CONSM_SECTION_CODE , std.*,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code as Issue_Item  ,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_Qty  as Issue_Qty,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_Per  as Issue_Fat_Per,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Issue_SNF_per,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Issue_Fat_Kg,TSPL_PP_PE_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Issue_SNF_Kg from (select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE as Stand_code,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE as Consm_Unit_COde,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per as consm_Fat_Per,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per as Consm_SNF_Per ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG as Consm_FAT_Kg ,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG as Consm_SNf_Kg  from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE   is not null" & _
        " ) std" & _
        " inner  join TSPL_PP_PRODUCTION_ENTRY   on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE  =std.Stand_code" & _
        " left join TSPL_PP_PE_ISSUE_ITEM_DETAIL  on TSPL_PP_PE_ISSUE_ITEM_DETAIL.PROD_ENTRY_CODE  =std.Stand_code and TSPL_PP_PE_ISSUE_ITEM_DETAIL.Item_Code =std.CONSM_ITEM_CODE) as Final"

        Return Baseqry
    End Function

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
        Next

        gv1.Columns("Op_Qty").HeaderText = "Op Qty"
        gv1.Columns("Op_Fat_Kg").HeaderText = "Op Fat Kg"
        gv1.Columns("Op_SNF_Kg").HeaderText = "Op SNF Kg"
        gv1.Columns("Quantity").HeaderText = "Quantity"
        gv1.Columns("Fat_Kg").HeaderText = "Fat Kg"
        gv1.Columns("SNF_Kg").HeaderText = "SNF Kg"

        gv1.BestFitColumns()

        gv1.GroupDescriptors.Add(New GridGroupByExpression("XgroupCode as Group format ""{0}: {2}"" Group By XgroupCode"))

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Op_Qty", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Op_Fat_Kg", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Op_SNF_Kg", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item3)
        Dim item11 As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item21 As New GridViewSummaryItem("Fat_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item31 As New GridViewSummaryItem("SNF_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item31)

        Dim item41 As New GridViewSummaryItem("Op TS Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item41)
        Dim item51 As New GridViewSummaryItem("TS Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item51)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.AutoExpandGroups = True
    End Sub
    Sub Reset()
        chk_stockingunit.Checked = False
        chk_stockingunit.Enabled = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        cboReportType.SelectedValue = Nothing
        rbtnSummary.IsChecked = False
        rbtnDetail.IsChecked = True
        cboReportType.Enabled = rbtnSummary.IsChecked
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub LoadReportType()
        cboReportType.DataSource = Nothing
        Dim qry As String = "select 'P' as Code,'Date Wise' as Name union all select 'I' as Code,'Item Wise' as Name union all select 'S' as Code,'Section Wise' as Name"
        cboReportType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"
    End Sub

    Private Sub RptWIPReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        LoadReportType()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")

        Reset()
    End Sub

    Private Sub RptWIPReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
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

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportId()
        TemplateGridview = gv1
        loadReport()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            If rbtnSummary.IsChecked Then
                arrHeader.Add("Summary Report")
            Else
                arrHeader.Add("Detail Report")
            End If

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            ' Dim arr As List(Of String)
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

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("WIP Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("WIP Report", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
   


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub rbtnSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnSummary.ToggleStateChanged, rbtnDetail.ToggleStateChanged
        cboReportType.Enabled = rbtnSummary.IsChecked

        If rbtnSummary.IsChecked Then
            cboReportType.SelectedValue = "S"
        Else
            cboReportType.SelectedValue = Nothing
        End If
    End Sub

    Private Function GetReportId()
        Dim ReportId As String = ""
        If rbtnDetail.IsChecked = True Then
            ReportId = MyBase.Form_ID
        ElseIf rbtnSummary.IsChecked = True Then
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboReportType.Text)
        End If
        Return ReportId
    End Function

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            print(EnumExportTo.Excel)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            If rbtnSummary.IsChecked Then
                arrHeader.Add("Name : Summary of " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptWTPReport & "'"))
                arrHeader.Add("Summary Type :" + cboReportType.Text)
            Else
                arrHeader.Add("Name : Detail of " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptWTPReport & "'"))
            End If

            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("WIP Report", gv1, arrHeader, "WIP Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
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
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
