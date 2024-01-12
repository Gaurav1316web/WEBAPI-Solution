'''' created by priti on 03/10/2013 for bug no BM00000000600
Imports common
Public Class FrmStockDispatchReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim arraylistLoc As ArrayList
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable

   
  
    Sub LoadData()
        Try
            strQuery = LoadQuery("", "", 0)
            dt = clsDBFuncationality.GetDataTable(strQuery)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv1.DataSource = dt
            SetGridFormationOFGV1()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function LoadQuery(ByVal strColumn As String, ByVal strLocDetail As String, ByVal strColumnIndex As Integer)
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Item", Me.Text)
            Return False
            Exit Function
        End If

        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location", Me.Text)
            Return False
            Exit Function
        End If
        Dim strFromDateTime As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy hh:mm tt")
        Dim strToDateTime As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy hh:mm tt")
        'Dim i As Integer
        '  Dim strloc1 As String
        Dim strLocAll, strItemAll, strLoc, strUnit As String
        strLoc = ""
        strUnit = ""
        If chkLocationAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkItemAll.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If
        If ddlUnitType.Text = "Filled" Then
            strUnit = "F"
        ElseIf ddlUnitType.Text = "Empty" Then
            strUnit = "E"
        ElseIf ddlUnitType.Text = "Both" Then
            strUnit = "B"
        End If
        'If strLocAll = "N" Then
        '    arraylistLoc = cbgLocation.CheckedValue
        '    For i = 0 To arraylistLoc.Count - 1
        '        strloc1 += "," + arraylistLoc(i).ToString
        '        strLoc = strloc1.TrimStart(",")

        '    Next

        'End If

        Try

            ''''' Code for Opening balance
            'Dim objReader As SqlDataReader
            Dim dt As New DataTable
            Dim strItemCode As String
            ' Dim DecQty, oldInQty, oldOutQty, decOP, decConvF As Decimal
            strItemCode = ""

            ''''''''    code for Opening 

            Dim strOpening As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,case when TSPL_INVENTORY_MOVEMENT.Trans_Type='WH' then 'Warehouse Leak/Break' when TSPL_INVENTORY_MOVEMENT.Trans_Type='IC-AD' then 'Adjustment' else TSPL_INVENTORY_MOVEMENT.Trans_Type end as TransType, " & _
            "0 as WHBreak,TSPL_INVENTORY_MOVEMENT.Punching_Date as Docdate,Trans_Id,0 as Stock,0 as SI,0 as Dispatch, " & _
            "case when TSPL_INVENTORY_MOVEMENT.trans_type='Load Out' then sale_invoice_no else  Source_Doc_No end as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP, isnull(TSPL_SHIPMENT_MASTER.Shipment_Type,'Sale') as Shipment_Type,  " & _
            "TSPL_INVENTORY_MOVEMENT.UOM AS Unit_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,  " & _
            "case when TSPL_INVENTORY_MOVEMENT.Location_Code='SPL' then  'Salesman Depot' else TSPL_LOCATION_MASTER.Location_Code end as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,  " & _
            "case when TSPL_INVENTORY_MOVEMENT.Location_Code='SPL' then  'Salesman Depot' else TSPL_LOCATION_MASTER.Location_Desc end as Location_Desc, case when inout='I' then ISNULL(TSPL_INVENTORY_MOVEMENT.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) else  " & _
            "ISNULL(TSPL_INVENTORY_MOVEMENT.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) * -1 end   AS OP, 0 AS AdjustQty,  " & _
            "0 AS RouteSale, 0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale FROM  " & _
            "TSPL_INVENTORY_MOVEMENT left  outer  JOIN TSPL_LOCATION_MASTER ON   " & _
            "TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_LOCATION_MASTER.Location_Code left  outer  JOIN   " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "TSPL_INVENTORY_MOVEMENT.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left  outer  JOIN TSPL_ITEM_DETAILS ON  " & _
            "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS.Item_Code left  outer  JOIN TSPL_ITEM_DETAILS  as TSPL_ITEM_DETAILS_1 ON  " & _
            "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code  LEFT OUTER JOIN TSPL_SHIPMENT_MASTER ON  " & _
            "TSPL_INVENTORY_MOVEMENT.Source_Doc_No = TSPL_SHIPMENT_MASTER.Shipment_No left outer join  " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and  " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end)  " & _
            "left outer join tspl_sale_invoice_head on TSPL_INVENTORY_MOVEMENT.source_doc_no=tspl_sale_invoice_head.shipment_no " & _
            "WHERE TSPL_LOCATION_MASTER.Location_Type  <> 'Logical' and   TSPL_INVENTORY_MOVEMENT.Punching_Date < = '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") + "' and  " & _
            "TSPL_ITEM_DETAILS.Class_Name='size'    and GIT_Type='N'  AND TSPL_INVENTORY_MOVEMENT.Location_Code <> 'SPL' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"

            'If strItemAll = "N" Then
            '    strOpening += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If

            'If strLocAll = "N" Then
            '    strOpening += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strOpening += " and (TSPL_INVENTORY_MOVEMENT.UOm='FC' or TSPL_INVENTORY_MOVEMENT.UOM='FB' )"
            'End If
            'If strUnit = "E" Then
            '    strOpening += " and (TSPL_INVENTORY_MOVEMENT.UOm='EC' or TSPL_INVENTORY_MOVEMENT.UOM='EB' or TSPL_INVENTORY_MOVEMENT.UOM='SH')"
            'End If
            'StrSql1 += "union all "

            '''''' code for plant



            Dim strPlant_Stock = "SELECT    TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Adjustment' as TransType,0 as WHBreak,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate,Adjustment_Line_No,case when (TSPL_ADJUSTMENT_HEADER.ItemType='FT' or TSPL_ADJUSTMENT_HEADER.ItemType='FM') and " & _
            "Stock_Type <> '' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then " & _
            "TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else " & _
            "TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1)  end else 0 end as Stock, " & _
            "0 as SI,0 as Dispatch,TSPL_ADJUSTMENT_DETAIL.Adjustment_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,mrp, 'Sale' as Shipment_Type, " & _
            "TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc, " & _
            "TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
            "TSPL_LOCATION_MASTER.Location_Desc,0 as OP, case when (TSPL_ADJUSTMENT_HEADER.ItemType='FT' or TSPL_ADJUSTMENT_HEADER.ItemType='FM') and " & _
            "Stock_Type='' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then " & _
            "TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else " & _
            "TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1) end else 0 end as AdjustQty, " & _
            "0 AS RouteSale, 0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  FROM  " & _
            "TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON  " & _
            "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
            "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN TSPL_ITEM_UOM_DETAIL ON " & _
            "TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_ADJUSTMENT_DETAIL.unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when " & _
            "TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when " & _
            "TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) where  TSPL_ITEM_DETAILS.Class_Name='size' and " & _
            "TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND  TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "'   and " & _
            "TSPL_LOCATION_MASTER.Location_Type <> 'logical' and Posted='Y' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"

            Dim strEmptyAdjustment As String = "SELECT    TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Adjustment' as TransType,0 as WHBreak,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate,Adjustment_Line_No, " & _
            "case when Stock_Type='' then  case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end else 0 end  as Stock, " & _
            "0 as SI,0 as Dispatch,TSPL_ADJUSTMENT_DETAIL.Adjustment_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,mrp, 'Sale' as Shipment_Type, " & _
            "TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc, " & _
            "TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
            "TSPL_LOCATION_MASTER.Location_Desc,0 as OP, 0 as AdjustQty, " & _
            "0 AS RouteSale, case when Stock_Type='' then  case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  0 else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end else 0 end  AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale FROM TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
              "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
              "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
              "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code  left outer JOIN " & _
              "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
              "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
              "TSPL_ADJUSTMENT_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
              "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when  TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) " & _
              "where TSPL_ADJUSTMENT_HEADER.ItemType='E' and Reference_Document <> 'Load Out/Transfer' and Posted='Y'  and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and " & _
              " TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
              " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical'  "

            'If strItemAll = "N" Then
            '    strPlant_Stock += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strPlant_Stock += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strPlant_Stock += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strPlant_Stock += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH') "
            'End If

            'StrSql1 += "union all "

            ''''' code for route Transfer for Opening of Location salesman depot
            Dim strSalesmanOpeningRouteTransfer = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Transfer' as TransType,0 as WHBreak,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,Line_No,0 as Stock, " & _
            "0 as SI,0 as Dispatch, " & _
            "TSPL_TRANSFER_HEAD.Transfer_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
            "TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
            "TSPL_TRANSFER_DETAIL.Item_Desc,'Salesman Depot' as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, 'Salesman Depot' as Location_Desc, " & _
            " case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as OP,0 as adjustqty, " & _
            "0 as RouteSale, " & _
            "0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  from TSPL_TRANSFER_HEAD left outer join " & _
            "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer " & _
            "JOIN TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
             "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "where Post='Y' and Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_TRANSFER_HEAD.From_Location <> 'SPL' and " & _
            "TSPL_TRANSFER_HEAD.EntryDateTime <=   '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") + "' and " & _
            "Route_No <> '' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"
            'If strItemAll = "N" Then
            '    strSalesmanOpeningRouteTransfer += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strSalesmanOpeningRouteTransfer += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strSalesmanOpeningRouteTransfer += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strSalesmanOpeningRouteTransfer += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH')  "
            'End If

            'StrSql1 += "union all "

            ''''' code for route Transfer for Stock in Location salesman depot
            Dim strSalesmanStock = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType, 0 as WHBreak,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,Line_No,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as Stock, " & _
            "0 as SI,0 as Dispatch, " & _
            "TSPL_TRANSFER_HEAD.Transfer_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
            "TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
            "TSPL_TRANSFER_DETAIL.Item_Desc,'Salesman Depot' as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, 'Salesman Depot' as Location_Desc, " & _
            "0 as OP,0 as adjustqty, " & _
            "0 as RouteSale, " & _
            "0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  from TSPL_TRANSFER_HEAD left outer join " & _
            "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer " & _
            "JOIN TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "where Post='Y' and Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_TRANSFER_HEAD.From_Location <> 'SPL' and " & _
            "TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'    and " & _
            " Route_No <> '' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"
            'If strItemAll = "N" Then
            '    strSalesmanStock += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strSalesmanStock += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strSalesmanStock += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strSalesmanStock += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH')  "
            'End If

            'StrSql1 += "union all "

            ''''' code for route Transfer
            Dim strRouteTransfer = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Transfer' as TransType,0 as WHBreak, TSPL_TRANSFER_HEAD.Transfer_Date as Docdate, Line_No,0 as Stock,0 as SI,0 as Dispatch, " & _
            "TSPL_TRANSFER_HEAD.Transfer_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
            "TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
            "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as OP,0 as adjustqty, " & _
            "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteSale, " & _
            "0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  from TSPL_TRANSFER_HEAD left outer join " & _
            "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer " & _
            "JOIN TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "where Post='Y' and Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_TRANSFER_HEAD.From_Location <> 'SPL' and " & _
            "TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'    and " & _
            " Route_No <> '' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"
            'If strItemAll = "N" Then
            '    strRouteTransfer += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strRouteTransfer += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strRouteTransfer += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strRouteTransfer += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH')  "
            'End If

            'StrSql1 += "union all "


            '''''''' LoadOut for depot sale for transit location
            Dim strDispatch_TransitDepotSale = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType, 0 as WHBreak,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,Line_No,0 as Stock,0 as SI,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as Dispatch, " & _
            "TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_LOCATION_MASTER_1.Location_Code as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
            "TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as adjustqty,0 as RouteSale, " & _
            "0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, " & _
            "0 AS PurReturn, case when (LoadIntable.Load_Out_No is null or LoadIntable.Load_Out_No ='')  or  (LoadIntable.EntryDateTime > =  '" & strToDateTime & "') or ( LoadIntable.Post='N' ) then convert(decimal(18,2),case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) else 0 end   as DepotSale " & _
            "from TSPL_TRANSFER_HEAD left outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
            " TSPL_TRANSFER_HEAD as LoadIntable on TSPL_TRANSFER_HEAD.Transfer_No=LoadIntable.Load_Out_No left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN TSPL_LOCATION_MASTER AS " & _
            "TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "where TSPL_TRANSFER_HEAD.Post='Y'  and TSPL_TRANSFER_HEAD.Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_LOCATION_MASTER_1.Location_Type <> 'logical' and " & _
            "TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'    and " & _
            " TSPL_TRANSFER_HEAD.Route_No = '' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'  "
            If strItemAll = "N" Then
                strDispatch_TransitDepotSale += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            End If
            'If strLocAll = "N" Then
            '    strDispatch_TransitDepotSale += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strDispatch_TransitDepotSale += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strDispatch_TransitDepotSale += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH')  "
            'End If

            'StrSql1 += "union all "


            '''''''' Load In for depot sale for final location
            Dim strDepotSaleActual = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,0 as WHBreak,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate, Line_No,0 as Stock,0 as SI,0 as Dispatch,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_LOCATION_MASTER_1.Location_Code as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
            "TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc,0 as OP, " & _
            "0  as adjustqty,0 as RouteSale, " & _
            "0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, " & _
            "0 AS PurReturn,convert(decimal(18,2),case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then (TSPL_TRANSFER_DETAIL.LoadIn_Qty + Leak+Burst+Shortage)/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as DepotSale " & _
            "from TSPL_TRANSFER_HEAD left outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No  " & _
            "left outer join  TSPL_TRANSFER_HEAD as LoadOuttable on TSPL_TRANSFER_HEAD.Load_Out_No=LoadOuttable.Transfer_No  left outer join " & _
            "TSPL_LOCATION_MASTER on LoadOuttable.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN TSPL_LOCATION_MASTER AS " & _
            "TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "where TSPL_TRANSFER_HEAD.Post='Y' and TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and " & _
            "LoadOuttable.EntryDateTime  >=  '" & strFromDateTime & "' AND  LoadOuttable.EntryDateTime <=  '" & strToDateTime & "'    and " & _
            " LoadOuttable.Route_No = '' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' " & _
            " and TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' "
            If strItemAll = "N" Then
                strDepotSaleActual += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            End If
            'If strLocAll = "N" Then
            '    strDepotSaleActual += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strDepotSaleActual += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strDepotSaleActual += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH')  "
            'End If

            Dim strItemCodestring, strMainItemCode, strmainItemCodeString, strPivot, strsum, strExist As String
            strItemCodestring = ""
            strsum = ""
            strmainItemCodeString = ""
            strPivot = "select distinct Toloc  from (" & strDepotSaleActual & " Union All " & strDispatch_TransitDepotSale & " ) xx where Toloc <> ''"
            dt = clsDBFuncationality.GetDataTable(strPivot)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","

                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    strsum = strsum & " isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)" & "+"

                    'strsum = strsum & " isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                Next
            End If

            If strItemCode <> "" Then
                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
                strExist = True
            Else
                strExist = False
            End If

            'StrSql1 += "union all "

            '''''''' Load In for Stock for selected date in Stock Column

            Dim strStock = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,0 as WHBreak, TSPL_TRANSFER_HEAD.Transfer_Date as Docdate, Line_No,convert(decimal(18,2),case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then (TSPL_TRANSFER_DETAIL.LoadIn_Qty )/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Stock,0 as SI,0 as Dispatch,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_LOCATION_MASTER_1.Location_Code as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
            "TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER_1.Location_Code as Location, " & _
            "TSPL_LOCATION_MASTER_1.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER_1.Location_Desc,0 as OP, " & _
            "0  as adjustqty,0 as RouteSale, " & _
            "0 AS Saleinvoice, 0 AS Salereturn, 0 AS SRN, " & _
            "0 AS PurReturn,0 as DepotSale " & _
            "from TSPL_TRANSFER_HEAD left outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No  " & _
            "left outer join  TSPL_TRANSFER_HEAD as LoadOuttable on TSPL_TRANSFER_HEAD.Load_Out_No=LoadOuttable.Transfer_No  left outer join " & _
            "TSPL_LOCATION_MASTER on LoadOuttable.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN TSPL_LOCATION_MASTER AS " & _
            "TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "where TSPL_TRANSFER_HEAD.Post='Y' and TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and " & _
            "TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'    and " & _
            " TSPL_TRANSFER_HEAD.to_Location <> 'spl' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"
            'If strItemAll = "N" Then
            '    strStock += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strStock += " and TSPL_LOCATION_MASTER_1.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strStock += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
            'End If
            'If strUnit = "E" Then
            '    strStock += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH')  "
            'End If


            'StrSql1 += "union all "


            ''''''  for direct sale
            Dim strSale_SaleInterCompany_SalesmanSale = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Load Out' as TransType,0 as WHBreak, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id, 0 as Stock,  case when Inter_Branch='Y' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end  as SI, " & _
            "0 as Dispatch, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,'' as Toloc,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type, " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
            "case when Shipment_Type='Sale' then TSPL_LOCATION_MASTER.Location_Code else 'Salesman Depot' end as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
            "case when Shipment_Type='Sale' then TSPL_LOCATION_MASTER.Location_Desc  else 'Salesman Depot' end as Location_Desc, 0 AS OP, 0 AS adjustqty, 0 AS RouteSale, " & _
            "case when Inter_Branch='N' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor  else 0 end  AS Saleinvoice,  " & _
            "0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  FROM  TSPL_SALE_INVOICE_HEAD left outer JOIN " & _
            "TSPL_SALE_INVOICE_DETAIL ON  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer JOIN  " & _
            "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC' " & _
            "when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) where Is_Post='Y'  and TSPL_ITEM_DETAILS.Class_Name='size' and " & _
            "TSPL_SALE_INVOICE_HEAD.Date_Time_Removal >=  '" & strFromDateTime & "' AND   " & _
            "TSPL_SALE_INVOICE_HEAD.Date_Time_Removal <=  '" & strToDateTime & "'  and " & _
            "TSPL_LOCATION_MASTER.Location_Type <> 'logical' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' "


            'If strLocAll = "N" Then
            '    strSale_SaleInterCompany_SalesmanSale += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strItemAll = "N" Then
            '    strSale_SaleInterCompany_SalesmanSale += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strSale_SaleInterCompany_SalesmanSale += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='FB')  "
            'End If
            'If strUnit = "E" Then
            '    strSale_SaleInterCompany_SalesmanSale += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH')  "
            'End If

            'StrSql1 += "union all "


            ''''''  for direct sale Opening of transfer type for salesman depot
            Dim strSalesmanOpeningSaleTransfer = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,  'Load Out' as TransType, 0 as WHBreak,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id,0 as Stock,0  as SI, " & _
            "0 as Dispatch, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,'' as Toloc,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type, " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
            "case when Shipment_Type='Sale' then TSPL_LOCATION_MASTER.Location_Code else 'Salesman Depot' end as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
            "case when Shipment_Type='Sale' then TSPL_LOCATION_MASTER.Location_Desc  else 'Salesman Depot' end as Location_Desc, " & _
            "-TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS OP, 0 AS adjustqty, 0 AS RouteSale, " & _
            "0 AS Saleinvoice,  " & _
            "0 AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  FROM  TSPL_SALE_INVOICE_HEAD left outer JOIN " & _
            "TSPL_SALE_INVOICE_DETAIL ON  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer JOIN  " & _
            "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC' " & _
            "when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) where Is_Post='Y'  and TSPL_ITEM_DETAILS.Class_Name='size' and " & _
            "TSPL_SALE_INVOICE_HEAD.Date_Time_Removal < =   '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") + "' AND   " & _
            "TSPL_LOCATION_MASTER.Location_Type <> 'logical'  and " & _
            " Shipment_Type='transfer' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'"
            'If strLocAll = "N" Then
            '    strSalesmanOpeningSaleTransfer += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strItemAll = "N" Then
            '    strSalesmanOpeningSaleTransfer += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strSalesmanOpeningSaleTransfer += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='FB')  "
            'End If
            'If strUnit = "E" Then
            '    strSalesmanOpeningSaleTransfer += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH')  "
            'End If
            'StrSql1 += "union all "


            '''''' for sale return
            Dim strSaleReturn = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Sale Return' as TransType,0 as WHBreak, TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Docdate,TSPL_SALE_RETURN_DETAIL.Sale_Return_Id,0 as Stock,0 as SI,0 as Dispatch, TSPL_SALE_RETURN_HEAD.Sale_Return_No as DocNo,'' as Toloc,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP_Amt as MRP,'Sale' AS Shipment_Type, " & _
            "TSPL_SALE_RETURN_DETAIL.Unit_code, TSPL_SALE_RETURN_DETAIL.Item_Code, TSPL_SALE_RETURN_DETAIL.Item_Desc, " & _
            "TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code AS Location_code, " & _
            "TSPL_LOCATION_MASTER.Location_Desc, 0 AS OP, 0 AS adjustqty,  0 AS RouteSale,0 AS Saleinvoice, " & _
            "TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS Salereturn, " & _
            "0 AS SRN, 0 AS PurReturn,0 as DepotSale  FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN  " & _
            "TSPL_SALE_RETURN_DETAIL LEFT OUTER JOIN TSPL_ITEM_DETAILS ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_SALE_RETURN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
            "TSPL_SALE_RETURN_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_RETURN_HEAD.Invoice_No LEFT OUTER JOIN " & _
            "TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code ON " & _
            "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_HEAD.Cust_Code And " & _
            "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC' " & _
            "when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) WHERE (TSPL_SALE_RETURN_HEAD.Is_Post = 'Y')  AND " & _
            "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_SALE_RETURN_HEAD.Sale_Return_Date >= '" & strFromDateTime & "') AND  " & _
            "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= '" & strToDateTime & "' ) AND (TSPL_LOCATION_MASTER.Location_Type <> 'logical') AND  " & _
            "(TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Sale') and TSPL_SALE_RETURN_HEAD.is_post='Y' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' "

            Dim strSaleInterCompany As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Sale Return InterCompany' as TransType,0 as WHBreak, TSPL_SALE_RETURN_INTER_HEAD.Document_Date as Docdate,TSPL_SALE_RETURN_INTER_DETAIL.Line_No, 0 as Stock, " & _
            "0 as SI, " & _
            "0 as Dispatch, TSPL_SALE_RETURN_INTER_HEAD.Document_No as DocNo,'' as Toloc,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type, " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code,TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, " & _
            "TSPL_LOCATION_MASTER.Location_Code  as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
            "TSPL_LOCATION_MASTER.Location_Desc  as Location_Desc, 0 AS OP, 0 AS adjustqty, 0 AS RouteSale, " & _
            "0  AS Saleinvoice,  " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS Salereturn, 0 AS SRN, 0 AS PurReturn,0 as DepotSale  FROM  TSPL_SALE_RETURN_INTER_HEAD left outer JOIN  TSPL_SALE_RETURN_INTER_DETAIL ON " & _
               "TSPL_SALE_RETURN_INTER_HEAD.Document_No = TSPL_SALE_RETURN_INTER_DETAIL.Document_No left outer JOIN TSPL_LOCATION_MASTER on " & _
               "TSPL_SALE_RETURN_INTER_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_CUSTOMER_MASTER ON " & _
               "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN TSPL_ITEM_DETAILS ON " & _
               "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code " & _
               "left outer JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " & _
               "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
               "TSPL_SALE_RETURN_INTER_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
               "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                       "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
               "where Is_Post=1 and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and " & _
               "TSPL_SALE_RETURN_INTER_HEAD.Document_Date >=  '" & strFromDateTime & "' AND " & _
               "TSPL_SALE_RETURN_INTER_HEAD.Document_Date <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "


            'If strLocAll = "N" Then
            '    strSaleReturn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strItemAll = "N" Then
            '    strSaleReturn += " and TSPL_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strSaleReturn += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='FB' )  "
            'End If
            'If strUnit = "E" Then
            '    strSaleReturn += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='EC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='EB' or TSPL_SALE_RETURN_DETAIL.Unit_code ='SH')  "
            'End If
            'StrSql1 += "union all "



            '''''''''''''' for SRN
            Dim strSRN = "SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'SRN' as TransType,0 as WHBreak, TSPL_SRN_HEAD.SRN_Date as Docdate , Line_No,0 as Stock,0 as SI,0 as Dispatch, TSPL_SRN_HEAD.SRN_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
            "TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Item_Code, TSPL_SRN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, " & _
            "TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc, 0 AS OP, 0 AS adjustqty, " & _
            "0 AS RouteSale,0 AS Saleinvoice, 0 AS Salereturn, " & _
            "TSPL_SRN_DETAIL.SRN_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor + isnull(Free_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) AS SRN, " & _
            "0 AS PurReturn,0 as DepotSale FROM TSPL_LOCATION_MASTER RIGHT OUTER JOIN TSPL_SRN_DETAIL ON  " & _
            "TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_DETAIL.Location RIGHT OUTER JOIN " & _
            "TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No LEFT OUTER JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "TSPL_SRN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code  and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC' " & _
            "when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) where Posting_Date <> '' and " & _
            "(Is_Internal=0) and  TSPL_ITEM_DETAILS.Class_Name='size' and  TSPL_SRN_HEAD.SRN_Date >=  '" & strFromDateTime & "' AND " & _
            "TSPL_SRN_HEAD.SRN_Date <=  '" & strToDateTime & "'  and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' "
            'If strItemAll = "N" Then
            '    strSRN += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strSRN += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strSRN += " and (TSPL_SRN_DETAIL.Unit_code='FC' or TSPL_SRN_DETAIL.Unit_code='FB' )  "
            'End If
            'If strUnit = "E" Then
            '    strSRN += " and (TSPL_SRN_DETAIL.Unit_code='EC' or TSPL_SRN_DETAIL.Unit_code='EB' or TSPL_SRN_DETAIL.Unit_code='SH')  "
            'End If

            'StrSql1 += "union all "


            ''''''''''''''''for Purchase Return
            Dim strPurchaseReturn = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Purchase Return' as TransType,0 as WHBreak,TSPL_PR_HEAD.PR_Date as Docdate,  Line_No,0 as Stock,0 as SI,0 as Dispatch,TSPL_PR_HEAD.PR_No as DocNo ,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
            "TSPL_PR_DETAIL.Unit_code as Unit_Code,TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, " & _
            "TSPL_LOCATION_MASTER_1.Location_Code as Location,TSPL_LOCATION_MASTER_1.Loc_Segment_Code as Location_Code, " & _
            "TSPL_LOCATION_MASTER_1.Location_Desc, 0 as OP,0 as adjustqty, 0 AS RouteSale,0 AS Saleinvoice,  " & _
            "0 AS Salereturn, 0 AS SRN, case when  TSPL_PR_DETAIL.Unit_code <> 'SH' then " & _
            "TSPL_PR_DETAIL.PR_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS PurReturn, " & _
            "0 as DepotSale from TSPL_PR_HEAD inner join TSPL_PR_DETAIL on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No inner join " & _
            "TSPL_LOCATION_MASTER on TSPL_PR_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code  INNER JOIN  " & _
            "TSPL_ITEM_DETAILS ON  TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON  TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
            "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_PR_HEAD.Bill_To_Location = TSPL_LOCATION_MASTER_1.Location_Code  INNER JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_PR_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_PR_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when " & _
            "unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
            "where TSPL_PR_HEAD.status=1  and TSPL_PR_HEAD.Item_Type = 'F' and TSPL_ITEM_DETAILS.Class_Name='size' and " & _
            "TSPL_PR_HEAD.Posting_Date  >=  '" & strFromDateTime & "' AND   TSPL_PR_HEAD.Posting_Date <=  '" & strToDateTime & "' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour'  "

            'If strItemAll = "N" Then
            '    strPurchaseReturn += " and TSPL_PR_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strPurchaseReturn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strPurchaseReturn += " and (TSPL_PR_DETAIL.Unit_code ='FC' or TSPL_PR_DETAIL.Unit_code ='FB' )  "
            'End If
            'If strUnit = "E" Then
            '    strPurchaseReturn += " and (TSPL_PR_DETAIL.Unit_code ='EC' or TSPL_PR_DETAIL.Unit_code ='EB' or TSPL_PR_DETAIL.Unit_code ='SH')  "
            'End If



            Dim strWHleakbreak As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Warehouse Leak/Break' as TransType,(TSPL_WH_BREAKAGE_DETAIL.Breakage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor + TSPL_WH_BREAKAGE_DETAIL.Shortage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as WHBreak, " & _
            "TSPL_WH_BREAKAGE_HEAD.Document_Date as Docdate,  Line_No,0 as Stock,0 as SI,0 as Dispatch,TSPL_WH_BREAKAGE_HEAD.Document_No as DocNo,'' as Toloc,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
            "TSPL_WH_BREAKAGE_DETAIL.Unit_code as Unit_Code,TSPL_WH_BREAKAGE_DETAIL.Item_Code, TSPL_WH_BREAKAGE_DETAIL.Item_Description as Item_Desc, " & _
            "TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_Code, " & _
            "TSPL_LOCATION_MASTER.Location_Desc, 0 as OP,0 as adjustqty, 0 AS RouteSale,0 AS Saleinvoice,  " & _
            "0 AS Salereturn, 0 AS SRN, 0 AS PurReturn, " & _
            "0 as DepotSale FROM TSPL_WH_BREAKAGE_HEAD left outer JOIN  TSPL_WH_BREAKAGE_DETAIL ON  " & _
               "TSPL_WH_BREAKAGE_HEAD.Document_No = TSPL_WH_BREAKAGE_DETAIL.Document_No left outer JOIN TSPL_LOCATION_MASTER ON " & _
               "TSPL_WH_BREAKAGE_HEAD.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN  TSPL_ITEM_DETAILS ON " & _
               "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " & _
               "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code  left outer JOIN TSPL_ITEM_UOM_DETAIL ON " & _
               "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
               "TSPL_WH_BREAKAGE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
               "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_WH_BREAKAGE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
               "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_WH_BREAKAGE_DETAIL.unit_code='FC' then 'FB' when TSPL_WH_BREAKAGE_DETAIL.unit_code='FB' then 'FC' " & _
               "when TSPL_WH_BREAKAGE_DETAIL.unit_code='EC' then 'EB' when  TSPL_WH_BREAKAGE_DETAIL.unit_code='EB' then 'EC'  end)  " & _
               "where Is_Post=1 and TSPL_ITEM_DETAILS.Class_Name='size' and  TSPL_WH_BREAKAGE_HEAD.Document_Date  >=  '" & strFromDateTime & "' AND " & _
               "TSPL_WH_BREAKAGE_HEAD.Document_Date <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' "

            'If strItemAll = "N" Then
            '    strWHleakbreak += " and TSPL_WH_BREAKAGE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            'End If
            'If strLocAll = "N" Then
            '    strWHleakbreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If strUnit = "F" Then
            '    strWHleakbreak += " and (TSPL_WH_BREAKAGE_DETAIL.Unit_code='FC' or TSPL_WH_BREAKAGE_DETAIL.Unit_code='FB' )   "
            'End If
            'If strUnit = "E" Then
            '    strWHleakbreak += " and (TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='EC' or TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='EB' or TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='SH')   "
            'End If

            Dim strItem As String = ""
            Dim strLocstring As String = ""
            Dim strFilled As String = ""
            Dim strEmpty As String = ""
            Dim strGroup As String = ""

            If strItemAll = "N" Then
                strItem += " and Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            Else
                strItem = ""
            End If
            If strLocAll = "N" Then
                strLocstring += " and Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLocstring = ""
            End If
            If strUnit = "F" Then
                strFilled += " and (Unit_code='FC' or Unit_code='FB' )   "
            Else
                strFilled = ""
            End If
            If strUnit = "E" Then
                strEmpty += " and (Unit_Code ='EC' or Unit_Code ='EB' or Unit_Code ='SH')   "
            Else
                strEmpty = ""
            End If

            Dim strUnion As String = " Union all "

            Dim StrSql1 As String = strOpening & strUnion & strPlant_Stock & strUnion & strSalesmanOpeningRouteTransfer & strUnion & strSalesmanStock & strUnion & strRouteTransfer & strUnion & strDispatch_TransitDepotSale & strUnion & strDepotSaleActual & strUnion & strStock & strUnion & strSale_SaleInterCompany_SalesmanSale & strUnion & strSalesmanOpeningSaleTransfer & strUnion & strSaleReturn & strUnion & strSRN & strUnion & strPurchaseReturn & strUnion & strWHleakbreak & strUnion & strSaleInterCompany & strUnion & strEmptyAdjustment

            Dim strClosing, strPivot2 As String
            If strExist = True Then
                strClosing = ",convert(decimal(18,2),((sum(OP) + sum(AdjustQty) + sum(Stock) + sum(Salereturn) + sum(SRN)) - (sum(Saleinvoice) + sum(SI)  + sum(PurReturn) +   (sum(RouteSale) +  sum(Dispatch))  + sum(WHBreak)  ))) as closing"
                strPivot2 = "pivot  (sum(depotsale) for Toloc in (" & strItemCodestring & ") ) as pvt1"
            Else
                strClosing = "convert(decimal(18,2),((sum(OP) + sum(AdjustQty) + sum(Stock) + sum(Salereturn) + sum(SRN)) - (sum(Saleinvoice) + sum(SI)  + sum(PurReturn) + (sum(RouteSale) +  sum(Dispatch)) + sum(WHBreak)  ))) as closing"
                strPivot2 = ""
            End If
            'If strExist = True Then
            '    strClosing = ",convert(decimal(18,2),(((OP) + (AdjustQty) + (OP) + (Salereturn) + (SRN)) - ((Saleinvoice) + (RouteSale)  + (PurReturn) - " & strsum & " ))) as closing"
            'Else
            '    strClosing = "convert(decimal(18,2),(((OP) + (AdjustQty) + (OP) + (Salereturn) + (SRN)) - ((Saleinvoice) + (RouteSale)  + (PurReturn) - " & strsum & " ))) as closing"
            'End If

            'strQuery = "select Location,Location_Desc, OP,AdjustQty, " & _
            '    "Saleinvoice,Salereturn,SRN," & _
            '    "PurReturn, " & _
            '    "" & strmainItemCodeString & " " & strClosing & " from( " & StrSql1 & " ) aa pivot  " & _
            '    "(sum(depotsale) for Toloc in (" & strItemCodestring & ") ) as pvt1 "
            If strLoc = "N" Then

            End If

            If rdbSku.IsChecked Then
                strGroup = "Item_Code"
            ElseIf rdbPack.IsChecked Then
                strGroup = "Pack"
            ElseIf rdbFlavour.IsChecked Then
                strGroup = "Flavour"
            End If
            If rdbSummary.IsChecked Then
                strQuery = "select Location,Location_Desc, " & _
                "convert(decimal(18,2),SUM(OP)) as OP, " & _
                "convert(decimal(18,2),sum(AdjustQty)) as AdjustQty, " & _
                "convert(decimal(18,2),sum(Stock)) as Stock, " & _
                "convert(decimal(18,2),sum(SRN)) as SRN," & _
                "convert(decimal(18,2),sum(Salereturn)) as Salereturn, " & _
                "convert(decimal(18,2),sum(Saleinvoice)) as Saleinvoice, " & _
                "convert(decimal(18,2),sum(SI)) as SI, " & _
                 "convert(decimal(18,2),sum(PurReturn)) as PurReturn, " & _
                 "(convert(decimal(18,2),sum(RouteSale)) +  convert(decimal(18,2),sum(Dispatch))) as TotalDispacth,  " & _
                "convert(decimal(18,2),sum(RouteSale)) as RouteSale,  " & _
                "convert(decimal(18,2),sum(WHBreak)) as WHBreak,  " & _
                "" & strmainItemCodeString & " " & strClosing & " from( " & StrSql1 & " ) aa  " & strPivot2 & "  " & _
                " where 2=2  " & strFilled & " " & strEmpty & " " & strLocstring & " " & strItem & "  group by Location,Location_Desc "
            Else
                strQuery = "select " & strGroup & ", MRPBottle,MRPCase,Location,Location_Desc, " & _
                "convert(decimal(18,2),SUM(OP)) as OP, " & _
                "convert(decimal(18,2),sum(AdjustQty)) as AdjustQty, " & _
                "convert(decimal(18,2),sum(Stock)) as Stock, " & _
                "convert(decimal(18,2),sum(SRN)) as SRN, " & _
                "convert(decimal(18,2),sum(Salereturn)) as Salereturn, " & _
                "convert(decimal(18,2),sum(Saleinvoice)) as Saleinvoice, " & _
                 "convert(decimal(18,2),sum(SI)) as SI, " & _
                 "convert(decimal(18,2),sum(PurReturn)) as PurReturn, " & _
                 "(convert(decimal(18,2),sum(RouteSale)) +  convert(decimal(18,2),sum(Dispatch))) as TotalDispacth,  " & _
                "convert(decimal(18,2),sum(RouteSale)) as RouteSale,  " & _
                "convert(decimal(18,2),sum(WHBreak)) as WHBreak,  " & _
               "" & strmainItemCodeString & " " & strClosing & " from( " & StrSql1 & " ) aa " & strPivot2 & "  " & _
                " where 2=2  " & strFilled & " " & strEmpty & " " & strLocstring & " " & strItem & "  group by Location,Location_Desc ," & strGroup & ",MRPBottle,MRPCase"
            End If


            If strColumn = "OP" And strLocDetail <> "Salesman Depot" Then
                strOpening = strOpening & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strOpening & ") a where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "OP" And strLocDetail = "Salesman Depot" Then
                strOpening = strSalesmanOpeningRouteTransfer & strUnion & strSalesmanOpeningSaleTransfer
                strOpening = strOpening
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strOpening & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "AdjustQty" And strLocDetail <> "Salesman Depot" Then
                strPlant_Stock = strPlant_Stock & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,AdjustQty as Qty from (" & strPlant_Stock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "Stock" And strLocDetail <> "Salesman Depot" Then

                strStock = strStock & "  and TSPL_LOCATION_MASTER_1.Location_Code= '" & strLocDetail & "' "
                strPlant_Stock = strPlant_Stock & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strEmptyAdjustment = strEmptyAdjustment & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strStock = strStock & strUnion & strPlant_Stock & strUnion & strEmptyAdjustment
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,Stock as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "Stock" And strLocDetail = "Salesman Depot" Then
                strStock = strSalesmanStock
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,Stock as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "SRN" Then
                strSRN = strSRN & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SRN as Qty from (" & strSRN & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "Salereturn" Then
                strSaleReturn = strSaleReturn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strSaleInterCompany = strSaleInterCompany & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strStock = strSaleReturn & strUnion & strSaleInterCompany
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,Salereturn as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "Saleinvoice" And strLocDetail <> "Salesman Depot" Then
                strEmptyAdjustment = strEmptyAdjustment & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strSale_SaleInterCompany_SalesmanSale = strSale_SaleInterCompany_SalesmanSale & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and Shipment_Type='Sale' and Inter_Branch='N' "
                strStock = strEmptyAdjustment & strUnion & strSale_SaleInterCompany_SalesmanSale
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,Saleinvoice as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "Saleinvoice" And strLocDetail = "Salesman Depot" Then
                strSale_SaleInterCompany_SalesmanSale = strSale_SaleInterCompany_SalesmanSale & "  and Shipment_Type='Transfer' and Inter_Branch='N' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,Saleinvoice as Qty from (" & strSale_SaleInterCompany_SalesmanSale & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "SI" And strLocDetail <> "Salesman Depot" Then
                strSale_SaleInterCompany_SalesmanSale = strSale_SaleInterCompany_SalesmanSale & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and Shipment_Type='Sale' and Inter_Branch='Y' "
                strSaleInterCompany = strSaleInterCompany & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strStock = strSale_SaleInterCompany_SalesmanSale
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SI as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "SI" And strLocDetail = "Salesman Depot" Then
                strSale_SaleInterCompany_SalesmanSale = strSale_SaleInterCompany_SalesmanSale & "  and Shipment_Type='Transfer' and Inter_Branch='Y' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SI as Qty from (" & strSale_SaleInterCompany_SalesmanSale & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "PurReturn" Then
                strPurchaseReturn = strPurchaseReturn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,PurReturn as Qty from (" & strPurchaseReturn & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "TotalDispacth" Then
                strDispatch_TransitDepotSale = strDispatch_TransitDepotSale & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strRouteTransfer = strRouteTransfer & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strStock = strDispatch_TransitDepotSale & strUnion & strRouteTransfer
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(RouteSale + Dispatch) as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "RouteSale" Then
                strRouteTransfer = strRouteTransfer & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(RouteSale ) as Qty from (" & strRouteTransfer & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn = "WHBreak" Then
                strWHleakbreak = strWHleakbreak & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,WHBreak  as Qty from (" & strWHleakbreak & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            ElseIf strColumn <> "closing" And strColumnIndex > 12 Then
                strDispatch_TransitDepotSale = strDispatch_TransitDepotSale & "  and TSPL_LOCATION_MASTER_1.Location_Code= '" & strColumn & "' and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  "
                strDepotSaleActual = strDepotSaleActual & "  and TSPL_LOCATION_MASTER_1.Location_Code= '" & strColumn & "' and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  "
                strStock = strDispatch_TransitDepotSale & strUnion & strDepotSaleActual
                strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(DepotSale ) as Qty from (" & strStock & ") a  where 2=2  " & strFilled & " " & strEmpty & ""
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return strQuery
    End Function
    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If Not (clsCommon.CompairString(e.Column.Name, "closing") = CompairStringResult.Equal OrElse clsCommon.CompairString(e.Column.Name, "Location") = CompairStringResult.Equal OrElse clsCommon.CompairString(e.Column.Name, "Location_Desc") = CompairStringResult.Equal) Then
                Dim strLocation, strColumn, strQuery As String
                Dim strCoumnIndex As Integer
                strLocation = gv1.Rows(e.RowIndex).Cells(0).Value
                strCoumnIndex = e.ColumnIndex
                strColumn = e.Column.Name
                strQuery = LoadQuery(strColumn, strLocation, strCoumnIndex)
                strQuery = "Select  [Trans Type],DocNo,DocDate,Item_Code,Item_Desc,convert(decimal(18,2),Qty) as Qty from ( " & strQuery & " ) a where Qty <> 0"
                Dim frmStock As New FrmStockDetail
                frmStock.LoadData(strQuery)
                frmStock.Show()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        Dim strItemCode, strGroup As String
        strGroup = ""
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If rdbDetail.IsChecked Then
            If rdbSku.IsChecked Then
                strGroup = "Item_Code"
            ElseIf rdbPack.IsChecked Then
                strGroup = "Pack"
            ElseIf rdbFlavour.IsChecked Then
                strGroup = "Flavour"
            End If

            gv1.Columns("" & strGroup & "").IsVisible = True
            gv1.Columns("" & strGroup & "").Width = 80
            gv1.Columns("" & strGroup & "").HeaderText = strGroup

            gv1.Columns("MRPcase").IsVisible = True
            gv1.Columns("MRPcase").Width = 50
            gv1.Columns("MRPcase").HeaderText = "MRPCase"

            gv1.Columns("MRPBottle").IsVisible = True
            gv1.Columns("MRPBottle").Width = 50
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"


            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Loc Desc"

            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 80
            gv1.Columns("OP").HeaderText = "OP"

            gv1.Columns("AdjustQty").IsVisible = True
            gv1.Columns("AdjustQty").Width = 80
            gv1.Columns("AdjustQty").HeaderText = "Plant"

            gv1.Columns("RouteSale").IsVisible = True
            gv1.Columns("RouteSale").Width = 80
            gv1.Columns("RouteSale").HeaderText = "Route Transfer"

            gv1.Columns("Saleinvoice").IsVisible = True
            gv1.Columns("Saleinvoice").Width = 80
            gv1.Columns("Saleinvoice").HeaderText = "Sale"

            gv1.Columns("Salereturn").IsVisible = True
            gv1.Columns("Salereturn").Width = 80
            gv1.Columns("Salereturn").HeaderText = "Sale Return"

            gv1.Columns("SRN").IsVisible = True
            gv1.Columns("SRN").Width = 80
            gv1.Columns("SRN").HeaderText = "Purchase"

            gv1.Columns("PurReturn").IsVisible = True
            gv1.Columns("PurReturn").Width = 80
            gv1.Columns("PurReturn").HeaderText = "Purchase Return"

            gv1.Columns("TotalDispacth").IsVisible = True
            gv1.Columns("TotalDispacth").Width = 80
            gv1.Columns("TotalDispacth").HeaderText = "Total Dispatch"

            gv1.Columns("closing").IsVisible = True
            gv1.Columns("closing").Width = 80
            gv1.Columns("closing").HeaderText = "Closing"

            gv1.Columns("SI").IsVisible = True
            gv1.Columns("SI").Width = 80
            gv1.Columns("SI").HeaderText = "Sale InterCompany"

            gv1.Columns("Stock").IsVisible = True
            gv1.Columns("Stock").Width = 80
            gv1.Columns("Stock").HeaderText = "Stock"

            gv1.Columns("WHBreak").IsVisible = True
            gv1.Columns("WHBreak").Width = 80
            gv1.Columns("WHBreak").HeaderText = "Warehouse Leak/Breakage"


            For ii As Integer = 15 To gv1.Columns.Count - 1
                strItemCode = gv1.Columns(ii).FieldName
                gv1.Columns("" & strItemCode & "").IsVisible = True
                gv1.Columns("" & strItemCode & "").Width = 80
                gv1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next



            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("OP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("AdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Saleinvoice", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Salereturn", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SRN", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("PurReturn", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item8 As New GridViewSummaryItem("TotalDispacth", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("SI", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item11 As New GridViewSummaryItem("WHBreak", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item19 As New GridViewSummaryItem("StockAdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item20 As New GridViewSummaryItem("closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)

            For ii As Integer = 15 To gv1.Columns.Count - 2
                intCount = intCount + 1
                strItemCode = gv1.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next
        Else

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Loc Desc"

            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 80
            gv1.Columns("OP").HeaderText = "OP"

            gv1.Columns("AdjustQty").IsVisible = True
            gv1.Columns("AdjustQty").Width = 80
            gv1.Columns("AdjustQty").HeaderText = "Plant"

            gv1.Columns("RouteSale").IsVisible = True
            gv1.Columns("RouteSale").Width = 80
            gv1.Columns("RouteSale").HeaderText = "Route Transfer"

            gv1.Columns("Saleinvoice").IsVisible = True
            gv1.Columns("Saleinvoice").Width = 80
            gv1.Columns("Saleinvoice").HeaderText = "Sale Invoice"

            gv1.Columns("Salereturn").IsVisible = True
            gv1.Columns("Salereturn").Width = 80
            gv1.Columns("Salereturn").HeaderText = "Sale Return"

            gv1.Columns("SRN").IsVisible = True
            gv1.Columns("SRN").Width = 80
            gv1.Columns("SRN").HeaderText = "Purchase"

            gv1.Columns("PurReturn").IsVisible = True
            gv1.Columns("PurReturn").Width = 80
            gv1.Columns("PurReturn").HeaderText = "Purchase Return"

            gv1.Columns("TotalDispacth").IsVisible = True
            gv1.Columns("TotalDispacth").Width = 80
            gv1.Columns("TotalDispacth").HeaderText = "Total Dispatch"

            gv1.Columns("SI").IsVisible = True
            gv1.Columns("SI").Width = 80
            gv1.Columns("SI").HeaderText = "Sale InterCompany"

            gv1.Columns("Stock").IsVisible = True
            gv1.Columns("Stock").Width = 80
            gv1.Columns("Stock").HeaderText = "Stock"

            gv1.Columns("closing").IsVisible = True
            gv1.Columns("closing").Width = 80
            gv1.Columns("closing").HeaderText = "Closing"

            gv1.Columns("WHBreak").IsVisible = True
            gv1.Columns("WHBreak").Width = 80
            gv1.Columns("WHBreak").HeaderText = "Warehouse Leak/Breakage"

            For ii As Integer = 13 To gv1.Columns.Count - 1
                strItemCode = gv1.Columns(ii).FieldName
                gv1.Columns("" & strItemCode & "").IsVisible = True
                gv1.Columns("" & strItemCode & "").Width = 80
                gv1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next


            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("OP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("AdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Saleinvoice", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Salereturn", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SRN", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("PurReturn", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("RouteSale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("TotalDispacth", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("SI", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Stock", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("WHBreak", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item19 As New GridViewSummaryItem("StockAdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item20 As New GridViewSummaryItem("closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)

            For ii As Integer = 13 To gv1.Columns.Count - 2
                intCount = intCount + 1
                strItemCode = gv1.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next
        End If

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.EnableFiltering = True
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub ExportToExcel()
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If

            If chkItemSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Item : " + strtemp)
            End If

            clsCommon.MyExportToExcel("Stock Reco ", gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        Dim strReportTitle As String = ""



        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Stock Reco Report : " + strReportTitle)
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy"))
            Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            If chkLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                Dim style6 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)
                Dim style7 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "")

            End If

        End If
    End Sub
    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' and not exists (select 1 from tspl_fixed_parameter where TYPE='" & clsFixedParameterType.SalesmanPhysicalLocation & "' and code='" & clsFixedParameterCode.SalesmanPhysicalLocation & "'  and Location_Code=description) union all select 'Salesman Depot','Salesman Depot' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Type='F'"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Scheme Applicable"
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.StockRecoReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmShippingStockreport1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        txtFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        txtToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())
        ddlUnitType.Text = "Filled"
        rdbSummary.IsChecked = True
        rdbSku.IsChecked = True
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged, chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        txtToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE)
    End Sub
    Sub Reset()
        ddlUnitType.Text = "Filled"
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        rdbSummary.IsChecked = True
        rdbSku.IsChecked = True
    End Sub

    Private Sub FrmShippingStockreport1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.P Then
            'PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadData()
    End Sub


    Private Sub txtToDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtToDate.Leave
        'txtToDate.Value = clsCommon.GetDateWithEndTime(txtToDate.Value)
    End Sub

    Private Sub txtToDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtToDate.LostFocus
        Dim strDate As String
        strDate = (txtToDate.Value.ToShortTimeString)
        If strDate = "12:00 AM" Then
            txtToDate.Value = clsCommon.GetDateWithEndTime(txtToDate.Value)
        End If
    End Sub


End Class
