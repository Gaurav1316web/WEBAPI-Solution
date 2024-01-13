'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 26/22/2012-------------------------------------
'--------------------------------Last modify Time - 01:00 PM -------------------------------------
'--------------------------------Last modify date - 18/12/2012-------------------------------------
'--------------------------------Last modify date - 28/01/2013-------------------------------------
'--------------------------------Last modify date - 25/07/2013  00:15 AM-------------------------------------
'--------------------------------Last modify date - 25/07/2013  12:45 PM-------------------------------------
''Last Modify by priti 23/10/2013 02:00 pm   for bug no BM00000000857


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Public Class FrmShippingStockreport1
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As SqlDataReader
    Dim arraylistLoc As ArrayList
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable



    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()

            Next
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        PrintData()
    End Sub
    Sub PrintData()
        Try
            strQuery = LoadQuery("", "", "", 0)
            If clsCommon.myLen(strQuery) > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, clsDBFuncationality.GetDataTable(strQuery), "crptShippingStock", "Stock Reconciliation Report")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub LoadData()
        Try
            strQuery = LoadQuery("", "", "", 0)
            Dim strUnit As String = ""
            Dim strFilled As String = ""
            Dim strEmpty As String = ""
            Dim strGroup As String = ""
            If ddlUnitType.Text = "Filled" Then
                strUnit = "F"
            ElseIf ddlUnitType.Text = "Empty" Then
                strUnit = "E"
            ElseIf ddlUnitType.Text = "Both" Then
                strUnit = "B"
            End If
            If strUnit = "F" Then
                strFilled += " and (a.Unit_code='FC' or a.Unit_code='FB' )   "
            Else
                strFilled = ""
            End If
            If strUnit = "E" Then
                strEmpty += " and (a.Unit_Code ='EC' or a.Unit_Code ='EB' or a.Unit_Code ='SH')   "
            Else
                strEmpty = ""
            End If

            If rdbSku.IsChecked Then
                strGroup = "a.Item_Code,TSPL_ITEM_MASTER.Item_Desc"
            ElseIf rdbPack.IsChecked Then
                strGroup = "a.Pack,a.PackDesc"
            ElseIf rdbFlavour.IsChecked Then
                strGroup = "a.Flavour,a.FlavourDesc"
            End If

            If rdbDetail.IsChecked Then
                strQuery = "select " & strGroup & ",MRPCase,MRPBottle,Location,Location_Code,Location_Desc,Class_Code,convert(decimal(18,2),sum(OP)) as OP, " & _
                "convert(decimal(18,2),sum(AdjustQty)) as Plant,convert(decimal(18,2),sum(StockAdjustQty)) as StockAdjustQty, " & _
                "convert(decimal(18,2),sum(StockReceipt)) as [Stock Receipt],convert(decimal(18,2),sum(StockDispatch)) as [Stock Dispatch], " & _
           "convert(decimal(18,2),sum(DirectReceipt)) as [Direct Receipt],convert(decimal(18,2),sum(DirectDispatch)) as [Direct Dispatch],convert(decimal(18,2),sum(InDirectReceipt)) as [Indirect Receipt], " & _
           "convert(decimal(18,2),sum(InDirectDispatch)) as [Indirect Dispatch],convert(decimal(18,2),sum(RouteLeakage)) as [Route Leakage],convert(decimal(18,2),sum(RouteBreakage)) as [Route Breakage], " & _
           "convert(decimal(18,2),sum(TransitLeakage)) as [Transit Leakage],convert(decimal(18,2),sum(TransitBreakage)) as [Transit Breakage],convert(decimal(18,2),sum(ManufLeakage)) as [Manufactured Leakage], " & _
           "convert(decimal(18,2),sum(ManufBreakage)) as [Manufactured Breakage], " & _
           "convert(decimal(18,2),(sum(RouteLeakage) + sum(TransitLeakage) + sum(ManufLeakage))) as [T.Leakage], " & _
           "convert(decimal(18,2),(sum(RouteBreakage) + sum(TransitBreakage) + sum(ManufBreakage))) as [T.Breakage], " & _
           "convert(decimal(18,2),(sum(WHLeak) + sum(WHBreak) )) as [WHLeak], " & _
           "convert(decimal(18,2), sum(Short)) as [Short], " & _
           "convert(decimal(18,2),(sum(DirectDispatch) + sum(InDirectDispatch))) as [T.Sale], " & _
           "convert(decimal(18,2),(SUM(OP) + sum(AdjustQty)+ sum(StockAdjustQty) + sum(DirectReceipt) + sum(InDirectReceipt) + sum(StockReceipt)) -  " & _
           "(sum(DirectDispatch) + sum(InDirectDispatch) + sum(StockDispatch) + sum(WHLeak) + sum(WHBreak) ) ) as [Closing Balance]  " & _
           " from ( " & strQuery & "  ) a left outer join tspl_item_master on a.item_code=tspl_item_master.item_code where Location not in  (select description from tspl_fixed_parameter where TYPE='" & clsFixedParameterType.SalesmanPhysicalLocation & "' and code='" & clsFixedParameterCode.SalesmanPhysicalLocation & "')  " & strFilled & " " & strEmpty & " " & _
           " group by Location_Code,Location_Desc,Class_Code," & strGroup & ",MRPCase,MRPBottle,Location"

            ElseIf rdbSummary.IsChecked Then
                strQuery = "select " & strGroup & ",MRPCase,MRPBottle,Location,Location_Code,Location_Desc,Class_Code,convert(decimal(18,2),sum(OP)) as OP,convert(decimal(18,2),sum(AdjustQty)) as Plant,convert(decimal(18,2),sum(StockAdjustQty)) as StockAdjustQty, " & _
                "(convert(decimal(18,2),sum(StockReceipt)) + convert(decimal(18,2),sum(DirectReceipt)) + convert(decimal(18,2),sum(InDirectReceipt)) ) as [Receipt], " & _
           "(convert(decimal(18,2),sum(DirectDispatch)) + convert(decimal(18,2),sum(InDirectDispatch))) as [Sale], " & _
           "convert(decimal(18,2),sum(StockDispatch)) as [DepotTransfer], " & _
           "(convert(decimal(18,2),sum(RouteLeakage)) + convert(decimal(18,2),sum(RouteBreakage)) + " & _
           " convert(decimal(18,2),sum(TransitLeakage)) + convert(decimal(18,2),sum(TransitBreakage))+ " & _
           " convert(decimal(18,2),sum(ManufLeakage)) + convert(decimal(18,2),sum(ManufBreakage)) )  as [Wastage], " & _
           "convert(decimal(18,2),(sum(WHLeak) + sum(WHBreak) )) as [WHLeak], " & _
           "convert(decimal(18,2),sum(Short)) as [Short], " & _
           "convert(decimal(18,2),(SUM(OP) + sum(AdjustQty) + sum(StockAdjustQty) + sum(DirectReceipt) + sum(InDirectReceipt) + sum(StockReceipt)) -  " & _
           "(sum(DirectDispatch) + sum(InDirectDispatch) + sum(StockDispatch) + sum(WHLeak) + sum(WHBreak) )) as [Closing Balance]  " & _
           " from ( " & strQuery & "  ) a left outer join tspl_item_master on a.item_code=tspl_item_master.item_code where Location not in  (select description from tspl_fixed_parameter where TYPE='" & clsFixedParameterType.SalesmanPhysicalLocation & "' and code='" & clsFixedParameterCode.SalesmanPhysicalLocation & "')  " & strFilled & " " & strEmpty & "" & _
           "group by Location_Code,Location_Desc,Class_Code," & strGroup & ",MRPCase,MRPBottle,Location"

            ElseIf rdbLocwise.IsChecked Then
                strQuery = "select Location,Location_Desc, " & _
                "case when Location='Route Sales' then (convert(decimal(18,2),sum(OP)) )  else convert(decimal(18,2),sum(OP)) end  as OP, " & _
                "convert(decimal(18,2),sum(AdjustQty)) as Plant, " & _
                "convert(decimal(18,2),sum(SRN)) as SRN, " & _
                 "convert(decimal(18,2),sum(SR)) as [SR], " & _
                  "convert(decimal(18,2),sum(StockAdjustQty)) as [StockAdjustQty], " & _
                 "case when Location='Route Sales' then (convert(decimal(18,2),sum(RouteOut)) - convert(decimal(18,2),sum(RouteIn)) - convert(decimal(18,2),sum(SaleTransfer)) ) else  ( convert(decimal(18,2),sum(DepotIn)) -convert(decimal(18,2),sum(DepotOut)) ) end  as DepotTransfer, " & _
                 "convert(decimal(18,2),sum(Sale)) + convert(decimal(18,2),sum(SaleTransfer))  as Sale, " & _
            "convert(decimal(18,2),sum(saleIC)) as [SaleIc], " & _
           "convert(decimal(18,2),sum(PR)) as [PR], " & _
           "(convert(decimal(18,2),sum(RouteLeakage)) + convert(decimal(18,2),sum(RouteBreakage)) + " & _
           " convert(decimal(18,2),sum(TransitLeakage)) + convert(decimal(18,2),sum(TransitBreakage))+ " & _
           " convert(decimal(18,2),sum(ManufLeakage)) + convert(decimal(18,2),sum(ManufBreakage)) )  as [Wastage], " & _
           "convert(decimal(18,2),(sum(WHLeak) + sum(WHBreak) )) as [WHLeak], " & _
           "convert(decimal(18,2),sum(Short)) as [Short], " & _
           "case when Location='Route Sales' then (convert(decimal(18,2),sum(OP)) + (convert(decimal(18,2),sum(RouteOut)) - convert(decimal(18,2),sum(RouteIn)) - convert(decimal(18,2),sum(SaleTransfer)) ) )    else " & _
           "convert(decimal(18,2),(SUM(OP) + sum(AdjustQty) + sum(StockAdjustQty) + sum(SRN) + sum(SR) + sum(StockReceipt) + sum(RouteIn) + sum(DepotIn)) -  " & _
           "(sum(RouteOut) + sum(DepotOut) + sum(Sale) + sum(SaleIc) + sum(PR) + sum(WHLeak) + sum(WHBreak) )) end as [Closing Balance]  " & _
           " from ( " & strQuery & "  ) a  " & _
           "group by Location_Desc,Location"

            End If



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
    Private Function LoadQuery(ByVal strColumn As String, ByVal strLocDetail As String, ByVal strItemDetail As String, ByVal strMRP As Integer)
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
        'Dim strloc1 As String
        Dim strLocAll, strItemAll, strLoc, strUnit As String
        strUnit = ""
        strLoc = ""
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
            '  Dim objCmd As SqlCommand
            'Dim objReader As SqlDataReader
            Dim dt As New DataTable
            'Dim strItemCode, strPackSize, strLocation, strItemDesc, strLocDesc, Fdate As String
            ' Dim oldInQty, oldOutQty As Decimal


            If rdbLocwise.IsChecked Then

                Dim strOpening As String = "SELECT   TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_INVENTORY_MOVEMENT.Source_Doc_No as DocNo,TSPL_INVENTORY_MOVEMENT.Punching_Date as Docdate,0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP, " & _
            "isnull(Shipment_Type,'Sale') as Shipment_Type,TSPL_INVENTORY_MOVEMENT.UOM AS Unit_Code, TSPL_INVENTORY_MOVEMENT.Item_Code," & _
            "TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc, " & _
            "case when inout='I' then ISNULL(TSPL_INVENTORY_MOVEMENT.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) else ISNULL(TSPL_INVENTORY_MOVEMENT.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) * -1 end   AS OP, 0 AS AdjustQty, 0 AS StockReceipt, 0 AS StockDispatch, " & _
            "0 AS DirectReceipt, 0 AS DirectDispatch, 0 AS InDirectReceipt, 0 AS InDirectDispatch, 0 AS RouteLeakage, " & _
            "0 AS RouteBreakage, 0 AS TransitLeakage, 0 AS TransitBreakage, 0 AS ManufLeakage, 0 AS ManufBreakage," & _
            "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, '' AS Ftime, '' AS Totime, " & _
            "TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,InOut as InOut  FROM " & _
            "TSPL_INVENTORY_MOVEMENT left  outer  JOIN TSPL_LOCATION_MASTER ON  " & _
            "TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_LOCATION_MASTER.Location_Code left  outer  JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_INVENTORY_MOVEMENT.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left  outer  JOIN TSPL_ITEM_DETAILS ON " & _
            "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS.Item_Code left  outer  JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " & _
            "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
            "TSPL_SHIPMENT_MASTER ON TSPL_INVENTORY_MOVEMENT.Source_Doc_No = TSPL_SHIPMENT_MASTER.Shipment_No " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "WHERE TSPL_LOCATION_MASTER.Location_Type  <> 'Logical' and " & _
            "  TSPL_INVENTORY_MOVEMENT.Punching_Date < = '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' "

                Dim Un1 As String = "union all "
                '--2.   Adjustment entry (Plant) Item Type only FM


                Dim strPlant As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate, 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak, " & _
                "case when Stock_Type <> '' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1) end else 0 end    as StockAdjustQty, MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,mrp, 'Sale' as Shipment_Type,TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code," & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP, " & _
                "case when (TSPL_ADJUSTMENT_HEADER.ItemType='FT' or TSPL_ADJUSTMENT_HEADER.ItemType='FM') and Stock_Type='' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1) end else 0 end as AdjustQty, " & _
                "0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch,0 as InDirectReceipt, " & _
                "0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage, " & _
                "0 as ManufLeakage,0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime, " & _
                "TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
                "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS  as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_ADJUSTMENT_DETAIL.unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when  TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) " & _
                "where  TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and   TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "'  and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and Posted='Y' "

                '--3  Stock Transfer (Receipt) Transfer LoadIn ITem Type is Full and empty (both) and From Location is Physical

                Dim strDepotTransferIn1 As String = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate, 0 as SRN,0 as Sale,0 as PR,0 as DepotOut , " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
               "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
               "0 as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt, " & _
               "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
               "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
               "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
               "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
               "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
               "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code  " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and Transfer_Type='LI' and " & _
               "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type='Physical'  and   TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
               "  TSPL_TRANSFER_HEAD.EntryDateTime  <=  '" & strToDateTime & "' "



                '-- 4  Stock Transfer (Receipt) Transfer LoadOut

                Dim strDepotTransferIn2 As String = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate, 0 as SRN,0 as Sale,0 as PR,0 as DepotOut , " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end  as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as  Location_Code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as adjustqty,0 as StockReceipt, " & _
                "0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code, " & _
                "'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and  TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                "  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' "

                '-- 5 Stock Transfer (Dispatch) Transfer LoadOut

                Dim strDepotTransferOut1 As String = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate , 0 as SRN,0 as Sale,0 as PR, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end  as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as adjustqty,0 as StockReceipt, " & _
                "0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
                "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type <> 'logical' and  TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'  "


                '--6  Stock Transfer (Dispatch) Transfer LoadIn ITem Type is Full and empty(both) and From Location is Physical

                Dim strDepotTransferOut2 As String = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate, 0 as SRN,0 as Sale,0 as PR, " & _
               "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
               "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER_1.Location_Code as Location,TSPL_LOCATION_MASTER_1.Loc_Segment_Code as Location_Code,TSPL_LOCATION_MASTER_1.Location_Desc, " & _
               "0 as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt, " & _
               "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
               "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
               "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
               "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
               "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
               "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
               "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                       " and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
               "where Post='Y' and Transfer_Type='LI'  and " & _
               "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type ='Physical' and TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "'  AND " & _
               "  TSPL_TRANSFER_HEAD.EntryDateTime <= '" & strToDateTime & "'  "



                '--7   Direct (Receipt) SRN detail where Customer type=D/P/M /V/O

                Dim strSRN As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_SRN_HEAD.SRN_No as  DocNo,TSPL_SRN_HEAD.SRN_Date as Docdate , " & _
                "TSPL_SRN_DETAIL.SRN_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor + isnull(Free_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)  as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Item_Code, " & _
                "TSPL_SRN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 AS OP, 0 AS adjustqty, 0 AS StockReceipt, 0 AS StockDispatch,0 AS DirectReceipt, " & _
                "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
                "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM " & _
                "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                "TSPL_SRN_DETAIL ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_DETAIL.Location RIGHT OUTER JOIN " & _
                "TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                "TSPL_SRN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code  " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where Posting_Date <> '' and " & _
                "(Is_Internal=0) and  " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_SRN_HEAD.SRN_Date >=  '" & strFromDateTime & "' AND  " & _
                " TSPL_SRN_HEAD.SRN_Date <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "

                '--8  Direct (Receipt) Adjustment detail where Customer type=D/P/M/V/O  and itemtype is empty

                Dim strAdjustment As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate, 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak, " & _
                "case when Stock_Type='' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1)  end else 0 end as StockAdjustQty, " & _
                "MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code," & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as AdjustQty, " & _
                "0 as StockReceipt,0 as StockDispatch, " & _
                "0  as DirectReceipt, " & _
                "0 as DirectDispatch,0 as InDirectReceipt, " & _
                "0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage, " & _
                "0 as ManufLeakage,0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime, " & _
                "TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
                "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS  as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_ADJUSTMENT_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when  TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) " & _
                "where TSPL_ADJUSTMENT_HEADER.ItemType='E' and Reference_Document <> 'Load Out/Transfer' and Posted='Y' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_ITEM_DETAILS.Class_Name='size' and " & _
                " TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical'  "

                '--9  Direct (Receipt) SaleReturnInterCompany detail where Customer type=D/P/M/V/O  
                Dim strSaleInterCompany As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_SALE_RETURN_INTER_HEAD.Document_No as DocNo,TSPL_SALE_RETURN_INTER_HEAD.Document_Date as Docdate,0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type,TSPL_SALE_RETURN_INTER_DETAIL.Unit_code,TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, " & _
                "TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc,0 AS OP, " & _
                "0 AS adjustqty,0 AS StockReceipt, 0 AS StockDispatch,TSPL_SALE_RETURN_INTER_DETAIL.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS DirectReceipt, 0 as DirectDispatch,0 as InDirectReceipt, " & _
                "0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, 0 as TransitLeakage,0 as TransitBreakage, " & _
                "0 as ManufLeakage,0 as ManufBreakage, '27/Aug/2012 12:00 AM' AS Fdate, '29/Aug/2012 11:59 PM' AS Tdate,'' as Ftime,'' as Totime, " & _
                "TSPL_ITEM_DETAILS.Class_Code,'' as SelectLoc,'' as InOut  FROM  TSPL_SALE_RETURN_INTER_HEAD left outer JOIN  TSPL_SALE_RETURN_INTER_DETAIL ON " & _
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

                '--10  Direct (Dispatch) Sale Invoice where Customer type=D/P/M /V/O

                Dim strSale_SaleIC_SaleTransfer As String = " SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate,0 as SRN,case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' then  case when Inter_Branch='N' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end else 0 end as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn , " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as SaleTransfer, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' then  case when Inter_Branch='Y' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end else 0 end as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type,TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then 'Route Sales' else TSPL_LOCATION_MASTER.Location_Code end as Location, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then 'Route Sales' else TSPL_LOCATION_MASTER.Loc_Segment_Code end as Location_code, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then 'Route Sales Depot' else TSPL_LOCATION_MASTER.Location_Desc end AS Location_Desc, " & _
                "0 AS OP, 0 AS adjustqty,0 AS StockReceipt, 0 AS StockDispatch,0 AS DirectReceipt, " & _
                "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage, " & _
                "0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut FROM  TSPL_SALE_INVOICE_HEAD left outer JOIN TSPL_SALE_INVOICE_DETAIL ON  " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
                "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN" & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                    "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where Is_Post='Y'  and " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal >=  '" & strFromDateTime & "' AND  " & _
                " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "



                '--11  Direct (Receipt) Sale Return where Customer type=D/P/M /V/O

                Dim strSaleReturn As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_SALE_RETURN_HEAD.Sale_Return_No as DocNo,TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Docdate,0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP_Amt as MRP,'Sale' AS Shipment_Type, TSPL_SALE_RETURN_DETAIL.Unit_code, TSPL_SALE_RETURN_DETAIL.Item_Code, " & _
                "TSPL_SALE_RETURN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code AS Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc, 0 AS OP, 0 AS adjustqty, 0 AS StockReceipt, 0 AS StockDispatch, " & _
                "TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS DirectReceipt, " & _
                "0 AS DirectDispatch, 0 AS InDirectReceipt,  " & _
                "0 AS InDirectDispatch, Leak_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS RouteLeakage, Burst_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS RouteBreakage, 0 AS TransitLeakage, 0 AS TransitBreakage, 0 AS ManufLeakage, " & _
                "0 AS ManufBreakage, '31/Aug/2012 12:00 AM' AS Fdate, '05/Oct/2012 11:59 PM' AS Tdate, '' AS Ftime, '' AS Totime, TSPL_ITEM_DETAILS.Class_Code, " & _
                "'' AS SelectLoc, '' AS InOut " & _
                "FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                "TSPL_SALE_RETURN_DETAIL LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                 "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                "TSPL_SALE_RETURN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
                "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
                "TSPL_SALE_RETURN_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_RETURN_HEAD.Invoice_No LEFT OUTER JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code AND " & _
                "TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code ON  " & _
                "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = TSPL_SALE_RETURN_HEAD.Sale_Return_No ON  " & _
                "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_HEAD.Cust_Code And " & _
                "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "WHERE (TSPL_SALE_RETURN_HEAD.Is_Post = 'Y')  AND (TSPL_ITEM_DETAILS.Class_Name = 'size') and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' AND " & _
                "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date >= '" & strFromDateTime & "') AND " & _
                "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= '" & strToDateTime & "') AND " & _
                "(TSPL_LOCATION_MASTER.Location_Type <> 'logical') AND  " & _
                "(TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Sale') and TSPL_SALE_RETURN_HEAD.is_post='Y'  "



                '--12  Route Leakage (LKG) Transfer LoadIn

                Dim strRouteLeakbreak As String = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  Shortage else 0 end as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate ,0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Leak/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteLeakage, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Burst/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut  from " & _
               "TSPL_TRANSFER_HEAD left outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
               "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
               "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
               "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                                    "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
               "where Post='Y' and Transfer_Type='LI' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                "  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "



                '--13 Transit Leakage (LKG) SRN detail

                Dim strTransitLeakbreak As String = "SELECT   TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,Short_Qty as short,TSPL_SRN_HEAD.SRN_No as  DocNo,TSPL_SRN_HEAD.SRN_Date as Docdate,0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_SRN_DETAIL.Unit_code, TSPL_SRN_DETAIL.Item_Code, TSPL_SRN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                          "TSPL_LOCATION_MASTER.Location_Desc, 0 AS OP, 0 AS adjustqty, 0 AS StockReceipt, 0 AS StockDispatch, 0 AS DirectReceipt, " & _
                          "0 AS DirectDispatch, 0 AS InDirectReceipt, 0 AS InDirectDispatch, " & _
                          "0 AS RouteLeakage, 0 AS RouteBreakage, " & _
                          "Leak_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS TransitLeakage, Burst_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS TransitBreakage, " & _
                          "0 AS ManufLeakage, 0 AS ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, '' AS Ftime, '' AS Totime, " & _
                          "TSPL_ITEM_DETAILS.Class_Code, '' AS SelectLoc,'' as InOut " & _
                          "FROM  TSPL_SRN_HEAD left outer JOIN " & _
                          "TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No left outer JOIN " & _
                          "TSPL_LOCATION_MASTER ON TSPL_SRN_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code " & _
                          "left outer join TSPL_ITEM_DETAILS  on TSPL_ITEM_DETAILS.Item_Code = TSPL_SRN_DETAIL.Item_Code " & _
                           "left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1  on TSPL_ITEM_DETAILS_1.Item_Code = TSPL_SRN_DETAIL.Item_Code " & _
                          "INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                          "TSPL_SRN_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                                         "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                          "where Posting_Date <> '' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' " & _
                          "and  TSPL_SRN_HEAD.SRN_Date >=   '" & strFromDateTime & "'  AND  " & _
                          "  TSPL_SRN_HEAD.SRN_Date  <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "


                '--14  Manufacturing Leakage (LKG) Adjustment detail

                Dim strManufactringleakbreak As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate,  0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP, 'Sale' as Shipment_Type,TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code, " & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 as OP,0 as AdjustQty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage, 0 as TransitBreakage, " & _
                "case when TSPL_ADJUSTMENT_HEADER.ItemType <> 'E' then   TSPL_ADJUSTMENT_DETAIL.LeakageQty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as ManufLeakage,  " & _
                "case when TSPL_ADJUSTMENT_HEADER.ItemType <> 'E' then   TSPL_ADJUSTMENT_DETAIL.Breakage/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as ManufBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM " & _
                "TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code  = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN" & _
                 "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_ADJUSTMENT_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when  TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) " & _
                "where Posted='Y' and " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and   TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "

                '-- 15 Direct (Dispatch) Transfer LoadOut  where to location is logical

                Dim strRouteOut As String = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short, TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate , 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,'Route Sales' as Location,'Route Sales' as Location_code, " & _
                "'Route Sales Depot' as Location_Desc,0 as OP,0 as adjustqty,0 as StockReceipt, " & _
                "0 as StockDispatch,0 as DirectReceipt, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN  " & _
                "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                   "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type = 'logical' and  TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' "

                '-- 16 Direct (Receipt) Transfer LoadIn  where from location is logical

                Dim strRouteIn As String = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate , 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteIn , " & _
              "0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
              "TSPL_TRANSFER_DETAIL.Item_Desc,'Route Sales' as Location,'Route Sales' as Location_code, " & _
                "'Route Sales Depot' as Location_Desc, " & _
              "0 as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as DirectReceipt, " & _
              "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
              "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
              "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
              "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
              "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
              "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
              "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
              "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
              "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code " & _
              "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
              "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
              "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
              "where Post='Y' and Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type = 'Full' and " & _
              "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type='Logical'  and   TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
              "  TSPL_TRANSFER_HEAD.EntryDateTime  <=  '" & strToDateTime & "' "

                '-- 17 Stock (Dispatch) purchase return  where item type is F

                Dim strPR As String = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_PR_HEAD.PR_No as DocNo,TSPL_PR_HEAD.PR_Date as Docdate, 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_PR_DETAIL.Unit_code as Unit_Code,TSPL_PR_DETAIL.Item_Code, " & _
                "TSPL_PR_DETAIL.Item_Desc,TSPL_LOCATION_MASTER_1.Location_Code as Location,TSPL_LOCATION_MASTER_1.Loc_Segment_Code as Location_Code,TSPL_LOCATION_MASTER_1.Location_Desc, " & _
                "0 as OP,0 as adjustqty,0 as StockReceipt,case when  TSPL_PR_DETAIL.Unit_code <> 'SH' then TSPL_PR_DETAIL.PR_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as StockDispatch, " & _
                "0 as DirectReceipt, 0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, " & _
                "'" & strToDateTime & "' AS Tdate, '' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc, " & _
                "'' as InOut from TSPL_PR_HEAD  left outer join TSPL_PR_DETAIL on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No  left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_PR_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN TSPL_ITEM_DETAILS ON  " & _
                "TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON  " & _
                "TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
                "TSPL_PR_HEAD.Bill_To_Location = TSPL_LOCATION_MASTER_1.Location_Code   left outer JOIN TSPL_ITEM_UOM_DETAIL ON " & _
                "TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_PR_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_PR_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where TSPL_PR_HEAD.status=1  and TSPL_PR_HEAD.Item_Type = 'F' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  " & _
                "TSPL_PR_HEAD.Posting_Date  >=  '" & strFromDateTime & "' AND   TSPL_PR_HEAD.Posting_Date <=  '" & strToDateTime & "' "

                '-- 18 Warehouse leakage and breakage

                Dim strWHleakbreak As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short,TSPL_WH_BREAKAGE_HEAD.Document_No as DocNo,TSPL_WH_BREAKAGE_HEAD.Document_Date as Docdate, 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,  0 as SR,TSPL_WH_BREAKAGE_DETAIL.Leakage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as WHLeak, " & _
                "(TSPL_WH_BREAKAGE_DETAIL.Breakage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor + TSPL_WH_BREAKAGE_DETAIL.Shortage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
                "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
                "TSPL_WH_BREAKAGE_DETAIL.Unit_Code,TSPL_WH_BREAKAGE_DETAIL.Item_Code, TSPL_WH_BREAKAGE_DETAIL.Item_Description as Item_Desc, " & _
                "TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc, 0 as OP,0 as AdjustQty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt, " & _
                "0 as DirectDispatch, 0 as InDirectReceipt,0 as InDirectDispatch, 0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage, " & _
                "0 as TransitBreakage,0 as ManufLeakage, " & _
                "0  as ManufBreakage, " & _
                "'01/Jul/2013 12:00 AM' AS Fdate, '29/Jul/2013 11:59 PM' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code, " & _
                "'' as SelectLoc,'' as InOut FROM TSPL_WH_BREAKAGE_HEAD left outer JOIN  TSPL_WH_BREAKAGE_DETAIL ON  " & _
                "TSPL_WH_BREAKAGE_HEAD.Document_No = TSPL_WH_BREAKAGE_DETAIL.Document_No left outer JOIN TSPL_LOCATION_MASTER ON " & _
                "TSPL_WH_BREAKAGE_HEAD.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN  TSPL_ITEM_DETAILS ON " & _
                "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " & _
                "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code  left outer JOIN TSPL_ITEM_UOM_DETAIL ON " & _
                "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_WH_BREAKAGE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
                "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_WH_BREAKAGE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
                "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_WH_BREAKAGE_DETAIL.unit_code='FC' then 'FB' when TSPL_WH_BREAKAGE_DETAIL.unit_code='FB' then 'FC' " & _
                "when TSPL_WH_BREAKAGE_DETAIL.unit_code='EC' then 'EB' when  TSPL_WH_BREAKAGE_DETAIL.unit_code='EB' then 'EC'  end)  " & _
                "where Is_Post=1 and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_WH_BREAKAGE_HEAD.Document_Date  >=  '" & strFromDateTime & "' AND " & _
                "TSPL_WH_BREAKAGE_HEAD.Document_Date <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "

                '-- 19 Direct (Dispatch) Transfer LoadOut  where to location is logical

                Dim strRouteOutOP As String = "select TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,0 as short, TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate , 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,'Route Sales' as Location,'Route Sales' as Location_code, " & _
                "'Route Sales Depot' as Location_Desc,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as OP,0 as adjustqty,0 as StockReceipt, " & _
                "0 as StockDispatch,0 as DirectReceipt, " & _
                "0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN  " & _
                "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code " & _
                 "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                   "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type = 'logical' and " & _
                "TSPL_TRANSFER_HEAD.EntryDateTime  <  '" + strFromDateTime + "' "

                '-- 20 Direct (Receipt) Transfer LoadIn  where from location is logical

                Dim strRouteInOP As String = "select  TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate , 0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn , " & _
              "0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
              "TSPL_TRANSFER_DETAIL.Item_Desc,'Route Sales' as Location,'Route Sales' as Location_code, " & _
                "'Route Sales Depot' as Location_Desc, " & _
              " - case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt, " & _
              "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
              "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
              "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
              "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
              "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
              "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
               "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
              "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
              "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code " & _
              "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
              "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
              "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
              "where Post='Y' and Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type = 'Full' and " & _
              "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type='Logical'  and " & _
              "TSPL_TRANSFER_HEAD.EntryDateTime <   '" + strFromDateTime + "'"


                '--10  Direct (Dispatch) Sale Invoice where Customer type=D/P/M /V/O

                Dim strSale_SaleIC_SaleTransferOP As String = " SELECT TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 0 as short, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate,0 as SRN,0 as Sale,0 as PR,0 as DepotOut ,0 as DepotIn ,0 as RouteOut ,0 as RouteIn , " & _
                "0 as SaleTransfer, " & _
                "0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type,TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then 'Route Sales' else TSPL_LOCATION_MASTER.Location_Code end as Location, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then 'Route Sales' else TSPL_LOCATION_MASTER.Loc_Segment_Code end as Location_code, " & _
                "case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then 'Route Sales Depot' else TSPL_LOCATION_MASTER.Location_Desc end AS Location_Desc, " & _
                "- case when TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end  AS OP, 0 AS adjustqty,0 AS StockReceipt, 0 AS StockDispatch,0 AS DirectReceipt, " & _
                "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage, " & _
                "0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut FROM  TSPL_SALE_INVOICE_HEAD left outer JOIN TSPL_SALE_INVOICE_DETAIL ON  " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
                "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                    "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where Is_Post='Y'  and " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal <  '" & strFromDateTime & "'  and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "




                If strItemAll = "N" Then
                    strOpening += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strPlant += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDepotTransferIn1 += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDepotTransferIn2 += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDepotTransferOut1 += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDepotTransferOut2 += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSRN += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strAdjustment += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSale_SaleIC_SaleTransfer += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSaleInterCompany += " and TSPL_SALE_RETURN_INTER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSaleReturn += " and TSPL_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strManufactringleakbreak += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strTransitLeakbreak += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strRouteLeakbreak += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strPR += " and TSPL_PR_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strRouteOut += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strRouteIn += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strWHleakbreak += " and TSPL_WH_BREAKAGE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strRouteOutOP += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strRouteInOP += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSale_SaleIC_SaleTransferOP += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                End If

                If strLocAll = "N" Then
                    strOpening += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strPlant += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strAdjustment += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDepotTransferIn1 += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDepotTransferIn2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDepotTransferOut1 += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDepotTransferOut2 += " and TSPL_LOCATION_MASTER_1.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSRN += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strAdjustment += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSale_SaleIC_SaleTransfer += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSaleInterCompany += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSaleReturn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strManufactringleakbreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strTransitLeakbreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strRouteLeakbreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strPR += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strRouteOut += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strRouteIn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strWHleakbreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strRouteOutOP += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strRouteInOP += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSale_SaleIC_SaleTransferOP += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                End If


                If strUnit = "E" Then
                    strOpening += " and (TSPL_INVENTORY_MOVEMENT.UOm='EC' or TSPL_INVENTORY_MOVEMENT.UOM='EB' or TSPL_INVENTORY_MOVEMENT.UOM='SH')"
                    strPlant += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
                    strDepotTransferIn1 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strDepotTransferIn2 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strDepotTransferOut1 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strDepotTransferOut2 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strSRN += " and (TSPL_SRN_DETAIL.Unit_code='EC' or TSPL_SRN_DETAIL.Unit_code='EB' or TSPL_SRN_DETAIL.Unit_code='SH') "
                    strAdjustment += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
                    strSaleInterCompany += " and (TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='EC' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='EB' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='SH') "
                    strSale_SaleIC_SaleTransfer += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH') "
                    strSaleReturn += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='EC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='EB' or TSPL_SALE_RETURN_DETAIL.Unit_code ='SH') "
                    strRouteLeakbreak += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strTransitLeakbreak += " and (TSPL_SRN_DETAIL.Unit_code='EC' or TSPL_SRN_DETAIL.Unit_code='EB' or TSPL_SRN_DETAIL.Unit_code='SH') "
                    strManufactringleakbreak += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
                    strRouteOut += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strRouteIn += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strPR += " and (TSPL_PR_DETAIL.Unit_code ='EC' or TSPL_PR_DETAIL.Unit_code ='EB' or TSPL_PR_DETAIL.Unit_code ='SH') "
                    strWHleakbreak += " and (TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='EC' or TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='EB' or TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='SH') "
                    strRouteOutOP += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strRouteInOP += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                    strSale_SaleIC_SaleTransferOP += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH') "
                End If

                If strUnit = "F" Then
                    strOpening += " and (TSPL_INVENTORY_MOVEMENT.UOm='FC' or TSPL_INVENTORY_MOVEMENT.UOM='FB' )"
                    strPlant += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB')"
                    strDepotTransferIn1 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strDepotTransferIn2 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strDepotTransferOut1 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strDepotTransferOut2 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strSRN += " and (TSPL_SRN_DETAIL.Unit_code='FC' or TSPL_SRN_DETAIL.Unit_code='FB') "
                    strAdjustment += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB')"
                    strSaleInterCompany += " and (TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FB') "
                    strSale_SaleIC_SaleTransfer += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='FB') "
                    strSaleReturn += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='FB') "
                    strRouteLeakbreak += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strTransitLeakbreak += " and (TSPL_SRN_DETAIL.Unit_code='FC' or TSPL_SRN_DETAIL.Unit_code='FB') "
                    strManufactringleakbreak += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB')"
                    strRouteOut += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strRouteIn += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strPR += " and (TSPL_PR_DETAIL.Unit_code ='FC' or TSPL_PR_DETAIL.Unit_code ='FB') "
                    strWHleakbreak += " and (TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='FC' or TSPL_WH_BREAKAGE_DETAIL.Unit_Code ='FB') "
                    strRouteOutOP += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strRouteInOP += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
                    strSale_SaleIC_SaleTransferOP += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='FB') "
                End If




                strQuery = strOpening & Un1 & strPlant & Un1 & strDepotTransferIn1 & Un1 & strDepotTransferIn2 & Un1 & strDepotTransferOut1 & Un1 & strDepotTransferOut2 & Un1 & strSRN & Un1 & strAdjustment & Un1 & strSaleInterCompany & Un1 & strSale_SaleIC_SaleTransfer & Un1 & strSaleReturn & Un1 & strRouteLeakbreak & Un1 & strTransitLeakbreak & Un1 & strManufactringleakbreak & Un1 & strRouteOut & Un1 & strRouteIn & Un1 & strPR & Un1 & strWHleakbreak & Un1 & strRouteOutOP & Un1 & strRouteInOP & Un1 & strSale_SaleIC_SaleTransferOP

                If strColumn = "OP" And strLocDetail <> "Route Sales" Then
                    strOpening = strOpening & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strOpening & ") a "
                ElseIf strColumn = "OP" And strLocDetail = "Route Sales" Then
                    strRouteOutOP = strRouteOutOP & "  "
                    strRouteOutOP = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strRouteOutOP & ") a "

                    strRouteInOP = strRouteInOP & "  "
                    strRouteInOP = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strRouteInOP & ") a "

                    strSale_SaleIC_SaleTransferOP = strSale_SaleIC_SaleTransferOP & "  "
                    strSale_SaleIC_SaleTransferOP = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strSale_SaleIC_SaleTransferOP & ") a "

                    strQuery = strRouteOutOP & Un1 & strRouteInOP & Un1 & strSale_SaleIC_SaleTransferOP

                ElseIf strColumn = "Plant" Then
                    strPlant = strPlant & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,adjustqty as Qty from (" & strPlant & ") a"
                ElseIf strColumn = "SRN" Then
                    strSRN = strSRN & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SRN as Qty from (" & strSRN & ") a "
                ElseIf strColumn = "SR" Then
                    strSaleReturn = strSaleReturn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SR as Qty from (" & strSaleReturn & ") a "
                ElseIf strColumn = "StockAdjustQty" Then
                    strPlant = strPlant & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strPlant = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,StockAdjustQty as Qty from (" & strPlant & ") a "
                    strAdjustment = strAdjustment & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strAdjustment = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,StockAdjustQty as Qty from (" & strAdjustment & ") a "

                    strQuery = strPlant & Un1 & strAdjustment
                ElseIf strColumn = "DepotTransfer" And strLocDetail = "Route Sales" Then
                    'strRouteOut = strRouteOut & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strRouteOut = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,RouteOut as Qty from (" & strRouteOut & ") a "

                    'strRouteIn = strRouteIn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strRouteIn = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,- RouteIn as Qty from (" & strRouteIn & ") a "

                    'strSale_SaleIC_SaleTransfer = strSale_SaleIC_SaleTransfer & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strSale_SaleIC_SaleTransfer = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc, - SaleTransfer as Qty from (" & strSale_SaleIC_SaleTransfer & ") a "

                    strQuery = strRouteOut & Un1 & strRouteIn & Un1 & strSale_SaleIC_SaleTransfer

                ElseIf strColumn = "DepotTransfer" And strLocDetail <> "Route Sales" Then
                    strDepotTransferIn1 = strDepotTransferIn1 & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strDepotTransferIn1 = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,DepotIn as Qty from (" & strDepotTransferIn1 & ") a "

                    strDepotTransferIn2 = strDepotTransferIn2 & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strDepotTransferIn2 = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,DepotIn as Qty from (" & strDepotTransferIn2 & ") a "

                    strDepotTransferOut1 = strDepotTransferOut1 & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strDepotTransferOut1 = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,-DepotOut as Qty from (" & strDepotTransferOut1 & ") a "

                    strDepotTransferOut2 = strDepotTransferOut2 & "  and TSPL_LOCATION_MASTER_1.Location_Code= '" & strLocDetail & "' "
                    strDepotTransferOut2 = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,-DepotOut as Qty from (" & strDepotTransferOut2 & ") a "

                    strQuery = strDepotTransferIn1 & Un1 & strDepotTransferIn2 & Un1 & strDepotTransferOut1 & Un1 & strDepotTransferOut2

                ElseIf strColumn = "Sale" And strLocDetail = "Route Sales" Then
                    strSale_SaleIC_SaleTransfer = strSale_SaleIC_SaleTransfer & "  "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SaleTransfer as Qty from (" & strSale_SaleIC_SaleTransfer & ") a "
                ElseIf strColumn = "Sale" And strLocDetail <> "Route Sales" Then
                    strSale_SaleIC_SaleTransfer = strSale_SaleIC_SaleTransfer & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,Sale as Qty from (" & strSale_SaleIC_SaleTransfer & ") a "

                ElseIf strColumn = "SaleIc" Then
                    strSaleInterCompany = strSaleInterCompany & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,SaleIC as Qty from (" & strSaleInterCompany & ") a "

                ElseIf strColumn = "PR" Then
                    strPR = strPR & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,PR as Qty from (" & strPR & ") a "

                ElseIf strColumn = "Wastage" Then
                    strTransitLeakbreak = strTransitLeakbreak & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strTransitLeakbreak = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,TransitLeakage + TransitBreakage as Qty from (" & strTransitLeakbreak & ") a "

                    strRouteLeakbreak = strRouteLeakbreak & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strRouteLeakbreak = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,RouteLeakage + RouteBreakage as Qty from (" & strRouteLeakbreak & ") a "

                    strManufactringleakbreak = strManufactringleakbreak & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strManufactringleakbreak = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,ManufLeakage + ManufBreakage as Qty from (" & strManufactringleakbreak & ") a "

                    strQuery = strTransitLeakbreak & Un1 & strRouteLeakbreak & Un1 & strManufactringleakbreak

                ElseIf strColumn = "WHLeak" Then
                    strWHleakbreak = strWHleakbreak & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "' "
                    strQuery = "Select DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,WHLeak + WHBreak as Qty from (" & strWHleakbreak & ") a "

                ElseIf strColumn = "Closing Balance" Then


                End If

            Else






                Dim strOpening As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,case when TSPL_INVENTORY_MOVEMENT.Trans_Type='WH' then 'Warehouse Leak/Break' when TSPL_INVENTORY_MOVEMENT.Trans_Type='IC-AD' then 'Adjustment' else TSPL_INVENTORY_MOVEMENT.Trans_Type end as TransType, " & _
                "TSPL_INVENTORY_MOVEMENT.Punching_Date as Docdate, case when TSPL_INVENTORY_MOVEMENT.trans_type='Load Out' then sale_invoice_no else  Source_Doc_No end as DocNo,0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP, " & _
            "isnull(TSPL_SHIPMENT_MASTER.Shipment_Type,'Sale') as Shipment_Type,TSPL_INVENTORY_MOVEMENT.UOM AS Unit_Code, TSPL_INVENTORY_MOVEMENT.Item_Code," & _
            "TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc, " & _
            "case when inout='I' then ISNULL(TSPL_INVENTORY_MOVEMENT.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) else ISNULL(TSPL_INVENTORY_MOVEMENT.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) * -1 end   AS OP, 0 AS AdjustQty, 0 AS StockReceipt, 0 AS StockDispatch, " & _
            "0 AS DirectReceipt, 0 AS DirectDispatch, 0 AS InDirectReceipt, 0 AS InDirectDispatch, 0 AS RouteLeakage, " & _
            "0 AS RouteBreakage, 0 AS TransitLeakage, 0 AS TransitBreakage, 0 AS ManufLeakage, 0 AS ManufBreakage," & _
            "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, '' AS Ftime, '' AS Totime, " & _
            "TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,InOut as InOut  FROM " & _
            "TSPL_INVENTORY_MOVEMENT left  outer  JOIN TSPL_LOCATION_MASTER ON  " & _
            "TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_LOCATION_MASTER.Location_Code left  outer  JOIN " & _
            "TSPL_ITEM_UOM_DETAIL ON TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "TSPL_INVENTORY_MOVEMENT.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code left  outer  JOIN TSPL_ITEM_DETAILS ON " & _
            "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS.Item_Code left  outer  JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " & _
            "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
            "TSPL_SHIPMENT_MASTER ON TSPL_INVENTORY_MOVEMENT.Source_Doc_No = TSPL_SHIPMENT_MASTER.Shipment_No " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
            "left outer join tspl_sale_invoice_head on TSPL_INVENTORY_MOVEMENT.source_doc_no=tspl_sale_invoice_head.shipment_no " & _
            "left outer join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
            "WHERE TSPL_LOCATION_MASTER.Location_Type  <> 'Logical' and " & _
            "  TSPL_INVENTORY_MOVEMENT.Punching_Date < = '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' "

                Dim Un1 As String = "union all "
                '--2.   Adjustment entry (Plant) Item Type only FM


                Dim strPlant As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Adjustment' as TransType, TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate,TSPL_ADJUSTMENT_DETAIL.Adjustment_No as DocNo, 0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,case when Stock_Type <> '' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1) end else 0 end  as StockAdjustQty, MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,mrp, 'Sale' as Shipment_Type,TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code," & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP, " & _
                "case when (TSPL_ADJUSTMENT_HEADER.ItemType='FT' or TSPL_ADJUSTMENT_HEADER.ItemType='FM') and Stock_Type='' then case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor * (-1) end else 0 end as AdjustQty, " & _
                "0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch,0 as InDirectReceipt, " & _
                "0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage, " & _
                "0 as ManufLeakage,0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime, " & _
                "TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
                "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                 "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_ADJUSTMENT_DETAIL.unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when  TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) " & _
                "where  TSPL_ITEM_DETAILS.Class_Name='size'  and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and   TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "'  and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and Posted='Y' "

                '--3  Stock Transfer (Receipt) Transfer LoadIn ITem Type is Full and empty (both) and From Location is Physical

                Dim strSTReceiptLoadIn As String = "select TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo, 0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
               "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
               "0 as OP,0 as adjustqty,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as StockReceipt,0 as StockDispatch,0 as DirectReceipt, " & _
               "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
               "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
               "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
               "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
               "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
               "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code  " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and Transfer_Type='LI' and " & _
               "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type='Physical'  and   TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
               "  TSPL_TRANSFER_HEAD.EntryDateTime  <=  '" & strToDateTime & "' "

                '--7  Stock Transfer (Dispatch) Transfer LoadIn ITem Type is Full and empty(both) and From Location is Physical

                Dim strSTDispatchLoadIn As String = "select  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
               "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER_1.Location_Code as Location,TSPL_LOCATION_MASTER_1.Loc_Segment_Code as Location_Code,TSPL_LOCATION_MASTER_1.Location_Desc, " & _
               "0 as OP,0 as adjustqty,0 as StockReceipt,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as StockDispatch,0 as DirectReceipt, " & _
               "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
               "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
               "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
               "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
               "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN " & _
               "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                       " and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
               "where Post='Y' and Transfer_Type='LI'  and " & _
               "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type ='Physical' and TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "'  AND " & _
               "  TSPL_TRANSFER_HEAD.EntryDateTime <= '" & strToDateTime & "'  "

                '-- 5  Stock Transfer (Receipt) Transfer LoadOut

                Dim strSTReceiptLoadOut As String = "select TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo, 0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as  Location_Code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as adjustqty,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as StockReceipt, " & _
                "0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code, " & _
                "'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                 "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and  TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                "  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' "

                '-- 6 Stock Transfer (Dispatch) Transfer LoadOut

                Dim strSTDispatchLoadOut As String = "select TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,0 as short, 0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as adjustqty,0 as StockReceipt, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
                "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type <> 'logical' and  TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'  "






                '--10   Direct (Receipt) SRN detail where Customer type=D/P/M /V/O

                Dim strReceiptSRN As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'SRN' as TransType,TSPL_SRN_HEAD.SRN_Date as Docdate , TSPL_SRN_HEAD.SRN_No as DocNo,0 as short, 0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Item_Code, " & _
                "TSPL_SRN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 AS OP, 0 AS adjustqty, 0 AS StockReceipt, 0 AS StockDispatch,TSPL_SRN_DETAIL.SRN_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor + isnull(Free_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) AS DirectReceipt, " & _
                "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
                "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM " & _
                "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                "TSPL_SRN_DETAIL ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_DETAIL.Location RIGHT OUTER JOIN " & _
                "TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                "TSPL_SRN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code  " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where Posting_Date <> '' and " & _
                "(Is_Internal=0) and  " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_SRN_HEAD.SRN_Date >=  '" & strFromDateTime & "' AND  " & _
                " TSPL_SRN_HEAD.SRN_Date <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "

                '--11  Direct (Receipt) Adjustment detail where Customer type=D/P/M/V/O  and itemtype is empty

                Dim strReceiptAdjustment As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Adjustment' as TransType,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate,TSPL_ADJUSTMENT_DETAIL.Adjustment_No as DocNo,  0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code," & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as AdjustQty, " & _
                "0 as StockReceipt,0 as StockDispatch, " & _
                "case when Stock_Type='' then  case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end else 0 end  as DirectReceipt, " & _
                "case when Stock_Type='' then  case when (Adjustment_Type='BI' or Adjustment_Type='QI') then  0 else TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end else 0 end as DirectDispatch,0 as InDirectReceipt, " & _
                "0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage, " & _
                "0 as ManufLeakage,0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime, " & _
                "TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
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

                '--12  Direct (Receipt) SaleReturnInterCompany detail where Customer type=D/P/M/V/O  
                Dim strReceiptInterComp As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Inter Company' as TransType,TSPL_SALE_RETURN_INTER_HEAD.Document_Date as Docdate,TSPL_SALE_RETURN_INTER_HEAD.Document_No as DocNo,0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type,TSPL_SALE_RETURN_INTER_DETAIL.Unit_code,TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, " & _
                "TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc,0 AS OP, " & _
                "0 AS adjustqty,0 AS StockReceipt, 0 AS StockDispatch,TSPL_SALE_RETURN_INTER_DETAIL.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS DirectReceipt, 0 as DirectDispatch,0 as InDirectReceipt, " & _
                "0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, 0 as TransitLeakage,0 as TransitBreakage, " & _
                "0 as ManufLeakage,0 as ManufBreakage, '27/Aug/2012 12:00 AM' AS Fdate, '29/Aug/2012 11:59 PM' AS Tdate,'' as Ftime,'' as Totime, " & _
                "TSPL_ITEM_DETAILS.Class_Code,'' as SelectLoc,'' as InOut  FROM  TSPL_SALE_RETURN_INTER_HEAD left outer JOIN  TSPL_SALE_RETURN_INTER_DETAIL ON " & _
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

                '--13  Direct (Dispatch) Sale Invoice where Customer type=D/P/M /V/O

                Dim strDispatchSaleInvoice = " SELECT TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Load Out' as TransType,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer, " & _
                "case when Inter_Branch='Y' then TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type,TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 AS OP, 0 AS adjustqty,0 AS StockReceipt, 0 AS StockDispatch,0 AS DirectReceipt, " & _
                "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage, " & _
                "0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut FROM  TSPL_SALE_INVOICE_HEAD left outer JOIN TSPL_SALE_INVOICE_DETAIL ON  " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
                "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code  left outer JOIN " & _
                "TSPL_ITEM_DETAILS  as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                    "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where Is_Post='Y'  and " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal >=  '" & strFromDateTime & "' AND  " & _
                " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and " & _
                "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale'"



                '--14  Direct (Receipt) Sale Return where Customer type=D/P/M /V/O

                Dim strReceiptSaleReturn As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Sale Return' as TransType, TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Docdate,TSPL_SALE_RETURN_HEAD.Sale_Return_No as DocNo,0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP_Amt as MRP,'Sale' AS Shipment_Type, TSPL_SALE_RETURN_DETAIL.Unit_code, TSPL_SALE_RETURN_DETAIL.Item_Code, " & _
                "TSPL_SALE_RETURN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code AS Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc, 0 AS OP, 0 AS adjustqty, 0 AS StockReceipt, 0 AS StockDispatch, " & _
                "TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS DirectReceipt, " & _
                "0 AS DirectDispatch, 0 AS InDirectReceipt,  " & _
                "0 AS InDirectDispatch, Leak_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS RouteLeakage, Burst_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS RouteBreakage, 0 AS TransitLeakage, 0 AS TransitBreakage, 0 AS ManufLeakage, " & _
                "0 AS ManufBreakage, '31/Aug/2012 12:00 AM' AS Fdate, '05/Oct/2012 11:59 PM' AS Tdate, '' AS Ftime, '' AS Totime, TSPL_ITEM_DETAILS.Class_Code, " & _
                "'' AS SelectLoc, '' AS InOut " & _
                "FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                "TSPL_SALE_RETURN_DETAIL LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                "TSPL_SALE_RETURN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
                "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
                "TSPL_SALE_RETURN_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_RETURN_HEAD.Invoice_No LEFT OUTER JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code ON  "
                'AND  " & _
                '"TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code " & _
                strReceiptSaleReturn += " TSPL_SALE_RETURN_DETAIL.Sale_Return_No = TSPL_SALE_RETURN_HEAD.Sale_Return_No ON  " & _
                "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_HEAD.Cust_Code And " & _
                "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "WHERE (TSPL_SALE_RETURN_HEAD.Is_Post = 'Y')  AND (TSPL_ITEM_DETAILS.Class_Name = 'size') and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' AND " & _
                "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date >= '" & strFromDateTime & "') AND " & _
                "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= '" & strToDateTime & "') AND " & _
                "(TSPL_LOCATION_MASTER.Location_Type <> 'logical') AND  " & _
                "(TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Sale') and TSPL_SALE_RETURN_HEAD.is_post='Y'  "



                '--21  Route Leakage (LKG) Transfer LoadIn

                Dim strRoutLeakBreak As String = "select TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  Shortage else 0 end as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Leak/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteLeakage, " & _
                "case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Burst/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut  from " & _
               "TSPL_TRANSFER_HEAD left outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
               "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
               "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
               "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                                    "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
               "where Post='Y' and Transfer_Type='LI' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                "  TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "



                '--23 Transit Leakage (LKG) SRN detail

                Dim strTransitLeakBreak As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'SRN' as TransType, TSPL_SRN_HEAD.SRN_Date as Docdate , TSPL_SRN_HEAD.SRN_No as DocNo,Short_Qty as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_SRN_DETAIL.Unit_code, TSPL_SRN_DETAIL.Item_Code, TSPL_SRN_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                          "TSPL_LOCATION_MASTER.Location_Desc, 0 AS OP, 0 AS adjustqty, 0 AS StockReceipt, 0 AS StockDispatch, 0 AS DirectReceipt, " & _
                          "0 AS DirectDispatch, 0 AS InDirectReceipt, 0 AS InDirectDispatch, " & _
                          "0 AS RouteLeakage, 0 AS RouteBreakage, " & _
                          "Leak_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS TransitLeakage, " & _
                          "Burst_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS TransitBreakage, " & _
                          "0 AS ManufLeakage, 0 AS ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, '' AS Ftime, '' AS Totime, " & _
                          "TSPL_ITEM_DETAILS.Class_Code, '' AS SelectLoc,'' as InOut " & _
                          "FROM  TSPL_SRN_HEAD left outer JOIN " & _
                          "TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No left outer JOIN " & _
                          "TSPL_LOCATION_MASTER ON TSPL_SRN_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code " & _
                          "left outer join TSPL_ITEM_DETAILS  on TSPL_ITEM_DETAILS.Item_Code = TSPL_SRN_DETAIL.Item_Code " & _
                           "left outer join TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1  on TSPL_ITEM_DETAILS_1.Item_Code = TSPL_SRN_DETAIL.Item_Code " & _
                          "INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                          "TSPL_SRN_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                          "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                          "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                          "where Posting_Date <> '' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' " & _
                          "and  TSPL_SRN_HEAD.SRN_Date >=   '" & strFromDateTime & "'  AND  " & _
                          "  TSPL_SRN_HEAD.SRN_Date  <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "


                '--25  Manufacturing Leakage (LKG) Adjustment detail

                Dim strManuLeakBreak As String = "SELECT  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Adjustment' as TransType,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Docdate,0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP, 'Sale' as Shipment_Type,TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code, " & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 as OP,0 as AdjustQty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt,0 as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage, 0 as TransitBreakage, " & _
                "case when TSPL_ADJUSTMENT_HEADER.ItemType <> 'E' then   TSPL_ADJUSTMENT_DETAIL.LeakageQty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as ManufLeakage,  " & _
                "case when TSPL_ADJUSTMENT_HEADER.ItemType <> 'E' then   TSPL_ADJUSTMENT_DETAIL.Breakage/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as ManufBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut FROM " & _
                "TSPL_ADJUSTMENT_HEADER left outer JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer JOIN " & _
                "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_HEADER.Loc_Code  = TSPL_LOCATION_MASTER.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code  left outer JOIN " & _
                 "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_ADJUSTMENT_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                            "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_ADJUSTMENT_DETAIL.unit_code='FC' then 'FB' when TSPL_ADJUSTMENT_DETAIL.unit_code='FB' then 'FC'  when TSPL_ADJUSTMENT_DETAIL.unit_code='EC' then 'EB' when  TSPL_ADJUSTMENT_DETAIL.unit_code='EB' then 'EC'  end) " & _
                "where Posted='Y' and " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and   TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' "


                '-- 27 Direct (Dispatch) Transfer LoadOut  where to location is logical

                Dim strDispatchLoadOut As String = "select TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,0 as short, case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc,0 as OP,0 as adjustqty,0 as StockReceipt, " & _
                "0 as StockDispatch,0 as DirectReceipt,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as DirectDispatch, " & _
                "0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as ManufLeakage,0 as ManufBreakage,0 as TransitBreakage, " & _
                "'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
                "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
                "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN  " & _
                "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
               "TSPL_TRANSFER_HEAD.to_Location = TSPL_LOCATION_MASTER_1.Location_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
                  "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                   "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
                "where Post='Y' and " & _
                "Transfer_Type='LO' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type = 'logical' and  TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' "

                '-- 28 Direct (Receipt) Transfer LoadIn  where from location is logical

                Dim strReceiptLoadIn As String = "select  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Transfer' as TransType,TSPL_TRANSFER_HEAD.Transfer_Date as Docdate,TSPL_TRANSFER_HEAD.Transfer_No as DocNo, 0 as short,0 as RouteOut ,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as RouteIn , " & _
              "0 as SaleTransfer,0 as  saleIC,0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_TRANSFER_DETAIL.Item_Code, " & _
              "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
              "0 as OP,0 as adjustqty,0 as StockReceipt,0 as StockDispatch,case when  TSPL_TRANSFER_DETAIL.UOM <> 'SH' then TSPL_TRANSFER_DETAIL.LoadIn_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as DirectReceipt, " & _
              "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
              "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate, " & _
              "'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc,'' as InOut from TSPL_TRANSFER_HEAD left outer join " & _
              "TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
              "TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer JOIN " & _
              "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  " & _
               "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN  " & _
              "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
              "TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER_1.Location_Code " & _
              "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
              "TSPL_TRANSFER_DETAIL.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
              "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                        "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when UOM='FC' then 'FB' when UOM='FB' then 'FC'  when UOM='EC' then 'EB' when  UOM='EB' then 'EC'  end) " & _
              "where Post='Y' and Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type = 'Full' and " & _
              "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and TSPL_LOCATION_MASTER_1.Location_Type='Logical'  and   TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
              "  TSPL_TRANSFER_HEAD.EntryDateTime  <=  '" & strToDateTime & "' "

                '-- 29 Stock (Dispatch) purchase return  where item type is F

                Dim strDispatchPurchaseRet As String = "select TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Purchase Return' as TransType,TSPL_PR_HEAD.PR_Date as Docdate, TSPL_PR_HEAD.PR_No as DocNo , 0 as short, 0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP,'Sale' as Shipment_Type,TSPL_PR_DETAIL.Unit_code as Unit_Code,TSPL_PR_DETAIL.Item_Code, " & _
                "TSPL_PR_DETAIL.Item_Desc,TSPL_LOCATION_MASTER_1.Location_Code as Location,TSPL_LOCATION_MASTER_1.Loc_Segment_Code as Location_Code,TSPL_LOCATION_MASTER_1.Location_Desc, " & _
                "0 as OP,0 as adjustqty,0 as StockReceipt,case when  TSPL_PR_DETAIL.Unit_code <> 'SH' then TSPL_PR_DETAIL.PR_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as StockDispatch, " & _
                "0 as DirectReceipt, 0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch,0 as RouteLeakage,0 as RouteBreakage, " & _
                "0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage,0 as ManufBreakage,'" & strFromDateTime & "' AS Fdate, " & _
                "'" & strToDateTime & "' AS Tdate, '' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code,'" & strLoc & "' as SelectLoc, " & _
                "'' as InOut from TSPL_PR_HEAD inner join TSPL_PR_DETAIL on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No inner join " & _
                "TSPL_LOCATION_MASTER on TSPL_PR_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code  INNER JOIN TSPL_ITEM_DETAILS ON  " & _
                "TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code  left outer  JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON  " & _
                "TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer JOIN TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
                "TSPL_PR_HEAD.Bill_To_Location = TSPL_LOCATION_MASTER_1.Location_Code  INNER JOIN TSPL_ITEM_UOM_DETAIL ON " & _
                "TSPL_PR_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_PR_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_PR_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                       "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where TSPL_PR_HEAD.status=1  and TSPL_PR_HEAD.Item_Type = 'F' and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  " & _
                "TSPL_PR_HEAD.Posting_Date  >=  '" & strFromDateTime & "' AND   TSPL_PR_HEAD.Posting_Date <=  '" & strToDateTime & "' "

                '-- 30 Warehouse leakage and breakage

                Dim strWarehouseLeakBreak As String = "SELECT TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour, 'Warehouse Leak/Break' as TransType,TSPL_WH_BREAKAGE_HEAD.Document_Date as Docdate,TSPL_WH_BREAKAGE_HEAD.Document_No as DocNo, 0 as short,0 as RouteOut ,0 as RouteIn ,0 as SaleTransfer,0 as  saleIC,  0 as SR,TSPL_WH_BREAKAGE_DETAIL.Leakage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as WHLeak, " & _
                "(TSPL_WH_BREAKAGE_DETAIL.Breakage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor + TSPL_WH_BREAKAGE_DETAIL.Shortage_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as WHBreak,0 as StockAdjustQty,MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
                "MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MRPBottle,MRP,'Sale' as Shipment_Type, " & _
                "TSPL_WH_BREAKAGE_DETAIL.Unit_Code,TSPL_WH_BREAKAGE_DETAIL.Item_Code, TSPL_WH_BREAKAGE_DETAIL.Item_Description as Item_Desc, " & _
                "TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code, " & _
                "TSPL_LOCATION_MASTER.Location_Desc, 0 as OP,0 as AdjustQty,0 as StockReceipt,0 as StockDispatch,0 as DirectReceipt, " & _
                "0 as DirectDispatch, 0 as InDirectReceipt,0 as InDirectDispatch, 0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage, " & _
                "0 as TransitBreakage,0 as ManufLeakage, " & _
                "0  as ManufBreakage, " & _
                "'01/Jul/2013 12:00 AM' AS Fdate, '29/Jul/2013 11:59 PM' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code, " & _
                "'' as SelectLoc,'' as InOut FROM TSPL_WH_BREAKAGE_HEAD left outer JOIN  TSPL_WH_BREAKAGE_DETAIL ON  " & _
                "TSPL_WH_BREAKAGE_HEAD.Document_No = TSPL_WH_BREAKAGE_DETAIL.Document_No left outer JOIN TSPL_LOCATION_MASTER ON " & _
                "TSPL_WH_BREAKAGE_HEAD.Loc_Code = TSPL_LOCATION_MASTER.Location_Code left outer JOIN  TSPL_ITEM_DETAILS ON " & _
                "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN  TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " & _
                "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code  left outer JOIN TSPL_ITEM_UOM_DETAIL ON " & _
                "TSPL_WH_BREAKAGE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_WH_BREAKAGE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
                "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_WH_BREAKAGE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
                "TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when TSPL_WH_BREAKAGE_DETAIL.unit_code='FC' then 'FB' when TSPL_WH_BREAKAGE_DETAIL.unit_code='FB' then 'FC' " & _
                "when TSPL_WH_BREAKAGE_DETAIL.unit_code='EC' then 'EB' when  TSPL_WH_BREAKAGE_DETAIL.unit_code='EB' then 'EC'  end)  " & _
                "where Is_Post=1 and TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_WH_BREAKAGE_HEAD.Document_Date  >=  '" & strFromDateTime & "' AND " & _
                "TSPL_WH_BREAKAGE_HEAD.Document_Date <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical'  "

                '--13  Direct (Dispatch) Sale Invoice where sale type is transfer
                Dim strDispatchSaleTransfer = " SELECT  TSPL_ITEM_DETAILS.Class_Desc as PackDesc,TSPL_ITEM_DETAILS_1.Class_Desc as FlavourDesc,TSPL_ITEM_DETAILS.Class_Desc as Pack,TSPL_ITEM_DETAILS_1.Class_Desc as Flavour,'Load Out' as TransType,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Docdate,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo,0 as short,0 as RouteOut ,0 as RouteIn ,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as SaleTransfer,0 as  saleIC, 0 as SR,0 as WHLeak,0 as WHBreak,0 as StockAdjustQty,MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  as MRPBottle,MRP_Amt as MRP,'Sale' as Shipment_Type,TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Desc,TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location_code,TSPL_LOCATION_MASTER.Location_Desc, " & _
                "0 AS OP, 0 AS adjustqty,0 AS StockReceipt, 0 AS StockDispatch,0 AS DirectReceipt, " & _
                "0 as DirectDispatch,0 as InDirectReceipt,0 as InDirectDispatch, " & _
                "0 as RouteLeakage,0 as RouteBreakage,0 as TransitLeakage,0 as TransitBreakage,0 as ManufLeakage, " & _
                "0 as ManufBreakage, '" & strFromDateTime & "' AS Fdate, '" & strToDateTime & "' AS Tdate,'' as Ftime,'' as Totime,TSPL_ITEM_DETAILS.Class_Code " & _
                ",'" & strLoc & "' as SelectLoc,'' as InOut FROM  TSPL_SALE_INVOICE_HEAD left outer JOIN TSPL_SALE_INVOICE_DETAIL ON  " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer JOIN TSPL_SHIPMENT_MASTER ON  " & _
                "TSPL_SALE_INVOICE_HEAD.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No left outer JOIN TSPL_TRANSFER_HEAD ON  " & _
                "TSPL_SHIPMENT_MASTER.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No  left outer JOIN " & _
                "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
                "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code left outer JOIN " & _
                "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 "left outer JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                "and TSPL_ITEM_UOM_DETAIL_1.UOM_Code=(case when unit_code='FC' then 'FB' when unit_code='FB' then 'FC'  when unit_code='EC' then 'EB' when  unit_code='EB' then 'EC'  end) " & _
                "where TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  and " & _
                "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' and  TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND  " & _
                "TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' and TSPL_LOCATION_MASTER.Location_Type <> 'logical' and " & _
                "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer'"




                If strItemAll = "N" Then
                    strOpening += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strPlant += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                    strSTReceiptLoadIn += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSTReceiptLoadOut += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                    strSTDispatchLoadIn += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strSTDispatchLoadOut += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                    strReceiptSRN += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strReceiptAdjustment += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strReceiptInterComp += " and TSPL_SALE_RETURN_INTER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strReceiptSaleReturn += " and TSPL_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strReceiptLoadIn += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                    strDispatchSaleInvoice += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDispatchLoadOut += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDispatchPurchaseRet += " and TSPL_PR_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strDispatchSaleTransfer += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                    strRoutLeakBreak += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strTransitLeakBreak += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strManuLeakBreak += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strWarehouseLeakBreak += " and TSPL_WH_BREAKAGE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "

                End If

                If strLocAll = "N" Then
                    strOpening += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strPlant += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                    strSTReceiptLoadIn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSTReceiptLoadOut += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                    strSTDispatchLoadIn += " and TSPL_LOCATION_MASTER_1.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSTDispatchLoadOut += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                    strReceiptSRN += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strReceiptAdjustment += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strReceiptInterComp += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strReceiptSaleReturn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strReceiptLoadIn += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                    strDispatchSaleInvoice += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDispatchLoadOut += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDispatchPurchaseRet += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strDispatchSaleTransfer += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                    strRoutLeakBreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strTransitLeakBreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strManuLeakBreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strWarehouseLeakBreak += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                End If

                'If strUnit = "E" Then
                '    strOpening += " and (TSPL_INVENTORY_MOVEMENT.UOm='EC' or TSPL_INVENTORY_MOVEMENT.UOM='EB' or TSPL_INVENTORY_MOVEMENT.UOM='SH')"
                '    strPlant += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
                '    strSTReceiptLoadIn += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strSTDispatchLoadIn += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strSTReceiptLoadOut += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strSTDispatchLoadOut += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strReceiptSRN += " and (TSPL_SRN_DETAIL.Unit_code='EC' or TSPL_SRN_DETAIL.Unit_code='EB' or TSPL_SRN_DETAIL.Unit_code='SH') "
                '    strReceiptAdjustment += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
                '    strReceiptInterComp += " and (TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='EC' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='EB' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='SH') "
                '    strReceiptSaleReturn += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='EC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='EB' or TSPL_SALE_RETURN_DETAIL.Unit_code ='SH') "
                '    strDispatchSaleInvoice += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH') "
                '    strDispatchLoadOut += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strReceiptLoadIn += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strDispatchPurchaseRet += " and (TSPL_PR_DETAIL.Unit_code ='EC' or TSPL_PR_DETAIL.Unit_code ='EB' or TSPL_PR_DETAIL.Unit_code ='SH') "
                '    strDispatchSaleTransfer += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH') "
                '    strRoutLeakBreak += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
                '    strTransitLeakBreak += " and (TSPL_SRN_DETAIL.Unit_code='EC' or TSPL_SRN_DETAIL.Unit_code='EB' or TSPL_SRN_DETAIL.Unit_code='SH') "
                '    strManuLeakBreak += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
                '    strWarehouseLeakBreak += " and (TSPL_WH_BREAKAGE_DETAIL.Unit_code='EC' or TSPL_WH_BREAKAGE_DETAIL.Unit_code='EB' or TSPL_WH_BREAKAGE_DETAIL.Unit_code='SH')"

                'End If

                'If strUnit = "F" Then
                '    strOpening += " and (TSPL_INVENTORY_MOVEMENT.UOM='FC' or TSPL_INVENTORY_MOVEMENT.UOM='FB' )"
                '    strPlant += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB' )"
                '    strSTReceiptLoadIn += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strSTReceiptLoadOut += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strSTDispatchLoadIn += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strSTDispatchLoadOut += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strReceiptSRN += " and (TSPL_SRN_DETAIL.Unit_code='FC' or TSPL_SRN_DETAIL.Unit_code='FB' ) "
                '    strReceiptAdjustment += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB' )"
                '    strReceiptInterComp += " and (TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FB' ) "
                '    strReceiptSaleReturn += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='FB' ) "
                '    strDispatchSaleInvoice += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='FB' ) "
                '    strDispatchLoadOut += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strReceiptLoadIn += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strDispatchPurchaseRet += " and (TSPL_PR_DETAIL.Unit_code ='FC' or TSPL_PR_DETAIL.Unit_code ='FB' ) "
                '    strDispatchSaleTransfer += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='FB' ) "
                '    strRoutLeakBreak += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB' ) "
                '    strTransitLeakBreak += " and (TSPL_SRN_DETAIL.Unit_code='FC' or TSPL_SRN_DETAIL.Unit_code='FB' ) "
                '    strManuLeakBreak += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB' )"
                '    strWarehouseLeakBreak += " and (TSPL_WH_BREAKAGE_DETAIL.Unit_code ='FC' or TSPL_WH_BREAKAGE_DETAIL.Unit_code ='FB' ) "
                'End If

                strQuery = strOpening & Un1 & strPlant & Un1 & strSTReceiptLoadIn & Un1 & strSTDispatchLoadIn & Un1 & strSTReceiptLoadOut & Un1 & strSTDispatchLoadOut & Un1 & strReceiptSRN & Un1 & strReceiptSaleReturn & Un1 & strReceiptAdjustment & Un1 & strReceiptInterComp & Un1 & strDispatchSaleInvoice & Un1 & strRoutLeakBreak & Un1 & strTransitLeakBreak & Un1 & strManuLeakBreak & Un1 & strDispatchLoadOut & Un1 & strReceiptLoadIn & Un1 & strDispatchPurchaseRet & Un1 & strWarehouseLeakBreak & Un1 & strDispatchSaleTransfer

                If rdbSummary.IsChecked Then
                    If strColumn = "OP" Then
                        strOpening = strOpening & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_INVENTORY_MOVEMENT.Item_Code='" & strItemDetail & "'"
                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,OP as Qty from (" & strOpening & ") a "
                    ElseIf strColumn = "Plant" Then
                        strPlant = strPlant & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_ADJUSTMENT_DETAIL.Item_Code='" & strItemDetail & "'"
                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,AdjustQty as Qty from (" & strPlant & ") a "
                    ElseIf strColumn = "Adjustment" Then
                        strPlant = strPlant & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_ADJUSTMENT_DETAIL.Item_Code='" & strItemDetail & "'"
                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,StockAdjustQty as Qty from (" & strPlant & ") a "
                    ElseIf strColumn = "Receipt" Then
                        strReceiptAdjustment = strReceiptAdjustment & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_ADJUSTMENT_DETAIL.Item_Code='" & strItemDetail & "'"
                        strReceiptInterComp = strReceiptInterComp & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP_amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_SALE_RETURN_INTER_DETAIL.Item_Code='" & strItemDetail & "'"
                        strReceiptLoadIn = strReceiptLoadIn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_TRANSFER_DETAIL.Item_Code='" & strItemDetail & "'"
                        strReceiptSaleReturn = strReceiptSaleReturn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP_amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_SALE_RETURN_DETAIL.Item_Code='" & strItemDetail & "'"
                        strReceiptSRN = strReceiptSRN & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_SRN_DETAIL.Item_Code='" & strItemDetail & "'"
                        strSTReceiptLoadIn = strSTReceiptLoadIn & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_TRANSFER_DETAIL.Item_Code='" & strItemDetail & "'"
                        strSTReceiptLoadOut = strSTReceiptLoadOut & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_TRANSFER_DETAIL.Item_Code='" & strItemDetail & "'"

                        Dim strReceipt = strReceiptAdjustment & Un1 & strReceiptInterComp & Un1 & strReceiptLoadIn & Un1 & strReceiptSaleReturn & Un1 & strReceiptSRN & Un1 & strSTReceiptLoadIn & Un1 & strSTReceiptLoadOut

                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(DirectReceipt+InDirectReceipt+StockReceipt) as Qty from (" & strReceipt & ") a "

                    ElseIf strColumn = "Sale" Then
                        strDispatchLoadOut = strDispatchLoadOut & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_TRANSFER_DETAIL.Item_Code='" & strItemDetail & "'"
                        strDispatchPurchaseRet = strDispatchPurchaseRet & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_PR_DETAIL.Item_Code='" & strItemDetail & "'"
                        strDispatchSaleInvoice = strDispatchSaleInvoice & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP_amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_SALE_INVOICE_DETAIL.Item_Code='" & strItemDetail & "'"
                        strDispatchSaleTransfer = strDispatchSaleTransfer & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP_amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_SALE_INVOICE_DETAIL.Item_Code='" & strItemDetail & "'"

                        Dim strReceipt = strDispatchLoadOut & Un1 & strDispatchPurchaseRet & Un1 & strDispatchSaleInvoice & Un1 & strDispatchSaleTransfer

                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(DirectDispatch+InDirectDispatch) as Qty from (" & strReceipt & ") a "
                    ElseIf strColumn = "DepotTransfer" Then
                        strSTDispatchLoadOut = strSTDispatchLoadOut & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_TRANSFER_DETAIL.Item_Code='" & strItemDetail & "'"
                        strSTDispatchLoadIn = strSTDispatchLoadIn & "  and TSPL_LOCATION_MASTER_1.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_TRANSFER_DETAIL.Item_Code='" & strItemDetail & "'"


                        Dim strReceipt = strSTDispatchLoadOut & Un1 & strSTDispatchLoadIn

                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(StockDispatch) as Qty from (" & strReceipt & ") a "
                    ElseIf strColumn = "WHLeak" Then
                        strWarehouseLeakBreak = strWarehouseLeakBreak & "  and TSPL_LOCATION_MASTER.Location_Code= '" & strLocDetail & "'  and MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor=" & strMRP & " and TSPL_WH_BREAKAGE_DETAIL.Item_Code='" & strItemDetail & "'"
                        Dim strReceipt = strWarehouseLeakBreak

                        strQuery = "Select TransType as [Trans Type],DocNo,convert(date,Docdate,103) as Docdate,Item_Code,Item_Desc,(WHLeak + WHBreak) as Qty from (" & strReceipt & ") a "
                    End If
                End If
            End If
            Return strQuery

        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String
        Dim strItem, strItemDesc As String
        strItem = ""
        strItemDesc = ""
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbSku.IsChecked Then
            strItem = "Item_Code"
            strItemDesc = "Item_Desc"
        ElseIf rdbPack.IsChecked Then
            strItem = "Pack"
            strItemDesc = "PackDesc"
        ElseIf rdbFlavour.IsChecked Then
            strItem = "Flavour"
            strItemDesc = "FlavourDesc"
        End If

        If rdbDetail.IsChecked Then
            gv1.Columns("" & strItem & "").IsVisible = True
            gv1.Columns("" & strItem & "").Width = 80
            gv1.Columns("" & strItem & "").HeaderText = "Item Code"
            gv1.Columns("" & strItem & "").PinPosition = PinnedColumnPosition.Left

            gv1.Columns("" & strItemDesc & "").IsVisible = True
            gv1.Columns("" & strItemDesc & "").Width = 80
            gv1.Columns("" & strItemDesc & "").HeaderText = "Item Desc"

            gv1.Columns("Class_Code").IsVisible = True
            gv1.Columns("Class_Code").Width = 50
            gv1.Columns("Class_Code").HeaderText = "Pack Code"

            gv1.Columns("MRPcase").IsVisible = True
            gv1.Columns("MRPcase").Width = 50
            gv1.Columns("MRPcase").HeaderText = "MRPCase"

            gv1.Columns("MRPBottle").IsVisible = True
            gv1.Columns("MRPBottle").Width = 50
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"

            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 50
            'gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Loc Desc"


            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 80
            'gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 80
            gv1.Columns("OP").HeaderText = "OP"

            gv1.Columns("Plant").IsVisible = True
            gv1.Columns("Plant").Width = 80
            gv1.Columns("Plant").HeaderText = "Plant"


            gv1.Columns("StockAdjustQty").IsVisible = True
            gv1.Columns("StockAdjustQty").Width = 80
            gv1.Columns("StockAdjustQty").HeaderText = "Adjustment"

            gv1.Columns("Stock Receipt").IsVisible = True
            gv1.Columns("Stock Receipt").Width = 80
            gv1.Columns("Stock Receipt").HeaderText = "Stock Receipt"

            gv1.Columns("Stock Dispatch").IsVisible = True
            gv1.Columns("Stock Dispatch").Width = 80
            gv1.Columns("Stock Dispatch").HeaderText = "Stock Dispatch"

            gv1.Columns("Direct Receipt").IsVisible = True
            gv1.Columns("Direct Receipt").Width = 80
            gv1.Columns("Direct Receipt").HeaderText = "Direct Receipt"

            gv1.Columns("Direct Dispatch").IsVisible = True
            gv1.Columns("Direct Dispatch").Width = 80
            gv1.Columns("Direct Dispatch").HeaderText = "Direct Dispatch"

            gv1.Columns("Indirect Receipt").IsVisible = True
            gv1.Columns("Indirect Receipt").Width = 80
            gv1.Columns("Indirect Receipt").HeaderText = "Indirect Receipt"

            gv1.Columns("Indirect Dispatch").IsVisible = True
            gv1.Columns("Indirect Dispatch").Width = 80
            gv1.Columns("Indirect Dispatch").HeaderText = "Indirect Dispatch"

            gv1.Columns("Route Leakage").IsVisible = True
            gv1.Columns("Route Leakage").Width = 80
            gv1.Columns("Route Leakage").HeaderText = "Route Leakage"

            gv1.Columns("Route Breakage").IsVisible = True
            gv1.Columns("Route Breakage").Width = 80
            gv1.Columns("Route Breakage").HeaderText = "Route Breakage"

            gv1.Columns("Transit Leakage").IsVisible = True
            gv1.Columns("Transit Leakage").Width = 80
            gv1.Columns("Transit Leakage").HeaderText = "Transit Leakage"

            gv1.Columns("Transit Breakage").IsVisible = True
            gv1.Columns("Transit Breakage").Width = 80
            gv1.Columns("Transit Breakage").HeaderText = "Transit Breakage"

            gv1.Columns("Manufactured Leakage").IsVisible = True
            gv1.Columns("Manufactured Leakage").Width = 80
            gv1.Columns("Manufactured Leakage").HeaderText = "Manufactured Leakage"

            gv1.Columns("Manufactured Breakage").IsVisible = True
            gv1.Columns("Manufactured Breakage").Width = 80
            gv1.Columns("Manufactured Breakage").HeaderText = "Manufactured Breakage"

            gv1.Columns("T.Leakage").IsVisible = True
            gv1.Columns("T.Leakage").Width = 80
            gv1.Columns("T.Leakage").HeaderText = "T.Leakage"

            gv1.Columns("T.Breakage").IsVisible = True
            gv1.Columns("T.Breakage").Width = 80
            gv1.Columns("T.Breakage").HeaderText = "T.Breakage"

            gv1.Columns("WHLeak").IsVisible = True
            gv1.Columns("WHLeak").Width = 80
            gv1.Columns("WHLeak").HeaderText = "Wearhouse Leak/Breakage"

            gv1.Columns("Short").IsVisible = False
            gv1.Columns("Short").Width = 80
            gv1.Columns("Short").HeaderText = "Shortage"

            gv1.Columns("T.Sale").IsVisible = True
            gv1.Columns("T.Sale").Width = 100
            gv1.Columns("T.Sale").HeaderText = "T.Sale"

            gv1.Columns("Closing Balance").IsVisible = True
            gv1.Columns("Closing Balance").Width = 100
            gv1.Columns("Closing Balance").HeaderText = "Closing Balance"



            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("OP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Stock Receipt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Stock Dispatch", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Direct Receipt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Direct Dispatch", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Indirect Receipt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Indirect Dispatch", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("Route Leakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("Route Breakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Transit Leakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Transit Breakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item12 As New GridViewSummaryItem("Manufactured Leakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item13 As New GridViewSummaryItem("Manufactured Breakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item14 As New GridViewSummaryItem("T.Leakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            Dim item15 As New GridViewSummaryItem("T.Breakage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)
            Dim item16 As New GridViewSummaryItem("T.Sale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
            Dim item17 As New GridViewSummaryItem("Closing Balance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item17)
            Dim item18 As New GridViewSummaryItem("Plant", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item18)
            Dim item20 As New GridViewSummaryItem("WHLeak", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)
            Dim item19 As New GridViewSummaryItem("StockAdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item51 As New GridViewSummaryItem("Short", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item51)

        ElseIf rdbSummary.IsChecked Then

            gv1.Columns("" & strItem & "").IsVisible = True
            gv1.Columns("" & strItem & "").Width = 80
            gv1.Columns("" & strItem & "").HeaderText = "Item Code"

            gv1.Columns("" & strItemDesc & "").IsVisible = True
            gv1.Columns("" & strItemDesc & "").Width = 80
            gv1.Columns("" & strItemDesc & "").HeaderText = "Item Desc"

            gv1.Columns("Class_Code").IsVisible = True
            gv1.Columns("Class_Code").Width = 50
            gv1.Columns("Class_Code").HeaderText = "Pack Code"

            gv1.Columns("MRPcase").IsVisible = True
            gv1.Columns("MRPcase").Width = 50
            gv1.Columns("MRPcase").HeaderText = "MRPCase"

            gv1.Columns("MRPBottle").IsVisible = True
            gv1.Columns("MRPBottle").Width = 50
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"

            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 50
            'gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Loc Desc"


            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 80
            'gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 80
            gv1.Columns("OP").HeaderText = "OP"

            gv1.Columns("Plant").IsVisible = True
            gv1.Columns("Plant").Width = 80
            gv1.Columns("Plant").HeaderText = "Plant"

            gv1.Columns("StockAdjustQty").IsVisible = True
            gv1.Columns("StockAdjustQty").Width = 80
            gv1.Columns("StockAdjustQty").HeaderText = "Adjustment"

            gv1.Columns("Receipt").IsVisible = True
            gv1.Columns("Receipt").Width = 80
            gv1.Columns("Receipt").HeaderText = "Receipt"

            gv1.Columns("Sale").IsVisible = True
            gv1.Columns("Sale").Width = 80
            gv1.Columns("Sale").HeaderText = "Sale"

            gv1.Columns("Depottransfer").IsVisible = True
            gv1.Columns("Depottransfer").Width = 80
            gv1.Columns("Depottransfer").HeaderText = "Depot Transfer"

            gv1.Columns("Wastage").IsVisible = False
            gv1.Columns("Wastage").Width = 80
            gv1.Columns("Wastage").HeaderText = "Wastage"

            gv1.Columns("Short").IsVisible = False
            gv1.Columns("Short").Width = 80
            gv1.Columns("Short").HeaderText = "Shortage"

            gv1.Columns("WHLeak").IsVisible = True
            gv1.Columns("WHLeak").Width = 80
            gv1.Columns("WHLeak").HeaderText = "Wearhouse Leak/Breakage"


            gv1.Columns("Closing Balance").IsVisible = True
            gv1.Columns("Closing Balance").Width = 100
            gv1.Columns("Closing Balance").HeaderText = "Closing Balance"



            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("OP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Receipt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Sale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("DepotTransfer", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Wastage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item17 As New GridViewSummaryItem("Closing Balance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item17)
            Dim item18 As New GridViewSummaryItem("Plant", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item18)
            Dim item19 As New GridViewSummaryItem("StockAdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item20 As New GridViewSummaryItem("WHLeak", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)
            Dim item51 As New GridViewSummaryItem("Short", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item51)

        ElseIf rdbLocwise.IsChecked Then


            'gv1.Columns("MRPcase").IsVisible = True
            'gv1.Columns("MRPcase").Width = 50
            'gv1.Columns("MRPcase").HeaderText = "MRPCase"

            'gv1.Columns("MRPBottle").IsVisible = True
            'gv1.Columns("MRPBottle").Width = 50
            'gv1.Columns("MRPBottle").HeaderText = "MRPBottle"

            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 50
            'gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 80
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Loc Desc"


            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 80
            'gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 80
            gv1.Columns("OP").HeaderText = "OP"

            gv1.Columns("Plant").IsVisible = True
            gv1.Columns("Plant").Width = 80
            gv1.Columns("Plant").HeaderText = "Plant"

            gv1.Columns("SRN").IsVisible = True
            gv1.Columns("SRN").Width = 80
            gv1.Columns("SRN").HeaderText = "SRN"


            gv1.Columns("SR").IsVisible = True
            gv1.Columns("SR").Width = 80
            gv1.Columns("SR").HeaderText = "Sale Return"

            gv1.Columns("StockAdjustQty").IsVisible = True
            gv1.Columns("StockAdjustQty").Width = 80
            gv1.Columns("StockAdjustQty").HeaderText = "Stock Adjustment"


            gv1.Columns("Depottransfer").IsVisible = True
            gv1.Columns("Depottransfer").Width = 80
            gv1.Columns("Depottransfer").HeaderText = "Depot Transfer"


            gv1.Columns("Sale").IsVisible = True
            gv1.Columns("Sale").Width = 80
            gv1.Columns("Sale").HeaderText = "Sale"


            gv1.Columns("SaleIc").IsVisible = True
            gv1.Columns("SaleIc").Width = 80
            gv1.Columns("SaleIc").HeaderText = "SaleIC"

            gv1.Columns("PR").IsVisible = True
            gv1.Columns("PR").Width = 80
            gv1.Columns("PR").HeaderText = "Purchase Return"

            gv1.Columns("Wastage").IsVisible = False
            gv1.Columns("Wastage").Width = 80
            gv1.Columns("Wastage").HeaderText = "Wastage"

            gv1.Columns("WHLeak").IsVisible = True
            gv1.Columns("WHLeak").Width = 80
            gv1.Columns("WHLeak").HeaderText = "Wearhouse Leak/Breakage"

            gv1.Columns("Short").IsVisible = False
            gv1.Columns("Short").Width = 80
            gv1.Columns("Short").HeaderText = "Shortage"

            gv1.Columns("Closing Balance").IsVisible = True
            gv1.Columns("Closing Balance").Width = 100
            gv1.Columns("Closing Balance").HeaderText = "Closing Balance"



            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("OP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item18 As New GridViewSummaryItem("Plant", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item18)
            Dim item3 As New GridViewSummaryItem("SRN", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("SR", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item6 As New GridViewSummaryItem("DepotTransfer", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Sale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("SaleIc", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item2 As New GridViewSummaryItem("PR", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item5 As New GridViewSummaryItem("Wastage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item17 As New GridViewSummaryItem("Closing Balance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item17)
            Dim item19 As New GridViewSummaryItem("StockAdjustQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item20 As New GridViewSummaryItem("WHLeak", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)
            Dim item51 As New GridViewSummaryItem("Short", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item51)
            'If rdbLocwise.IsChecked Then
            '    gv1.GroupDescriptors.Add(New GridGroupByExpression("Location_Code as LocCode format ""{0}: {1}"" Group By LocCode"))
            '    gv1.MasterTemplate.ExpandAllGroups()
            '    gv1.ShowGroupPanel = False
            '    gv1.MasterTemplate.AutoExpandGroups = True
            'End If
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
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
        'Dim qry As String = "select distinct Loc_Segment_Code as Location,Description as [Location Description] from TSPL_LOCATION_MASTER inner join TSPL_GL_SEGMENT_CODE on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_GL_SEGMENT_CODE.Segment_code where Location_Type='Physical'"
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' and not exists (select 1 from tspl_fixed_parameter where TYPE='" & clsFixedParameterType.SalesmanPhysicalLocation & "' and code='" & clsFixedParameterCode.SalesmanPhysicalLocation & "'  and Location_Code=description)"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Type='F' "
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
        btnPrint.Visible = MyBase.isPrintFlag
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
        RadPageView1.SelectedPage = RadPageViewPage1
        rdbLocwise.Visible = False
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
        txtFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
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
            PrintData()
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


    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If Not clsCommon.CompairString(e.Column.Name, "Closing Balance") = CompairStringResult.Equal AndAlso e.ColumnIndex > 7 Then
            Dim strLocation, strColumn, strItem, strQuery As String
            Dim strMRP As Integer
            strLocation = gv1.Rows(e.RowIndex).Cells(4).Value
            strItem = gv1.Rows(e.RowIndex).Cells(0).Value
            strMRP = gv1.Rows(e.RowIndex).Cells(2).Value

            strColumn = e.Column.Name
            strQuery = LoadQuery(strColumn, strLocation, strItem, strMRP)
            strQuery = "Select [Trans Type],DocNo,DocDate,Item_Code,Item_Desc,convert(decimal(18,2),Qty) as Qty from ( " & strQuery & " ) a where Qty <> 0"
            Dim frmStock As New FrmStockDetail
            frmStock.LoadData(strQuery)
            frmStock.Show()
        End If
    End Sub


End Class
