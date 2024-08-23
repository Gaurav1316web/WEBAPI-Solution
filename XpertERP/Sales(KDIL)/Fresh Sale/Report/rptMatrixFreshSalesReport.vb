Imports common
Imports System.IO
Public Class RptMatrixFreshSalesReport
    Inherits FrmMainTranScreen
    Dim isSchemeItem As Boolean = False
    Dim IsReportTypeChanged As Boolean = False
    Dim strQry As String = ""
    Dim dt As DataTable
    Dim ApplyMilkPouchPrint As Boolean = False
    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub
    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        strQry = "Select TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc As Name From TSPL_ITEM_MASTER "
        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub
    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        strQry = "Select TSPL_ZONE_MASTER.Zone_Code As Code,  TSPL_ZONE_MASTER.Description As Name From TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub
    Private Sub txtLorry__My_Click(sender As Object, e As EventArgs) Handles txtLorry._My_Click
        strQry = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,   TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER"
        txtLorry.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLorry.arrValueMember, txtLorry.arrDispalyMember)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        If chkMilkPouch.Checked AndAlso rbtnAsPerBooking.Checked = True Then
            PrintAsPerBooking(Exporter.Refresh)
        Else
            If clsCommon.CompairString(ddlReportType.SelectedValue, "MGPD") = CompairStringResult.Equal Then
                MilkGatePassDetailReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "TCS") = CompairStringResult.Equal Then
                TCSReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Demand Sheet") = CompairStringResult.Equal Then
                DemandSheetReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "PGPD") = CompairStringResult.Equal Then
                ProductGatePassDetailReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "MSR") = CompairStringResult.Equal Then
                MilkSaleReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "PSR") = CompairStringResult.Equal Then
                ProductSaleReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "DMGPD") = CompairStringResult.Equal Then
                DairyMilkGatePassReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "DPGPD") = CompairStringResult.Equal Then
                DairyProductGatePassReport(Exporter.Refresh)
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "MPDR") = CompairStringResult.Equal Then
                MilkProductDemandReport(Exporter.Refresh)
                Exit Sub
            End If
            Print(Exporter.Refresh)
        End If
    End Sub
    Private Sub ProductSaleReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            strWhrClause = " and convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "''
                             and  convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'
                              and  convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = "Select Item_Code,Item_Desc,Alies_Name from tspl_item_master where 2=2 and Is_Ambient=1 and Item_Type='F'"
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinKG,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "cast(Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) as DECIMAL(18, 2)) As   [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0) )"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinKG,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = "cast(Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) as DECIMAL(18, 2)) As  [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0) )"
                    End If
                Next
            End If
            Dim MainQuery1 As String = ""
            Dim query As String = ""
            Dim sql As String = ""
            MainQuery = "select "
            query = MainQuery
            MainQuery += "[Route No],[Route Desc], "
            MainQuery1 = "Select "
            sql += itemNames + ",convert (decimal(18,2), Sum(" + itemamt + ")) As Total from
                         (Select [Route No],[Route Desc],Shift_Type,max(Conversion_Factor) as Conversion_Factor ,max(CFinLTR) as CFinLTR ,max(CFinPouch) as CFinPouch,max(CFinKG) as CFinKG" + itemName + " from 
                              (Select ItemConversionInLTR.Conversion_Factor AS 'CFinLTR',ItemConversionInPouch.Conversion_Factor As CFinPouch,
                              ItemConversionInKG.Conversion_Factor as CFinKG,CurrentUnit.Conversion_Factor, TSPL_SD_SHIPMENT_HEAD.Route_No as [Route No],tspl_route_master.Route_Desc as [Route Desc],
                              TSPL_DEMAND_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_SD_SHIPMENT_HEAD.Shift_Type, TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                              TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty as Qty From TSPL_SD_SHIPMENT_BOOKING_DETAIL
                              LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.DOCUMENT_CODE
                              left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.TR_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code
                              left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
							  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                              Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                              left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code
                              and CurrentUnit.uom_code=	TSPL_DEMAND_BOOKING_DETAIL.unit_code 
                              left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code
                              and CrateUnit.uom_code='Crate'
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL 
                              where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where
                              UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                              left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where 
                              UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                              Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SHIPMENT_HEAD.Vehicle_Code
                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SHIPMENT_HEAD.Route_No
                              Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.Location_Code
                              where 2=2 " + strWhrClause2 + "  and TSPL_ITEM_MASTER.Is_Ambient=1  and TSPL_ITEM_MASTER. Item_Type='F'  and TSPL_SD_SHIPMENT_HEAD.Shift_Type in ('AM','PM')   " + whrcls + " 
                              group by TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_ITEM_MASTER.Alies_Name,TSPL_ROUTE_MASTER.[Route_Desc],TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
                              TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty,TSPL_SD_SHIPMENT_HEAD.Shift_Type,  ItemConversionInLTR.Conversion_Factor,ItemConversionInPouch.Conversion_Factor,ItemConversionInKG.Conversion_Factor ,CurrentUnit.Conversion_Factor  )tab1  
                              pivot( sum(Qty) for Description in (" + itemName1 + ") ) as Tab2"
            query += sql + " group by [Route No],[Route Desc],Shift_Type)tmp"
            MainQuery += sql + " group by [Route No],[Route Desc],Shift_Type)tmp
                                group by [Route No],[Route Desc]"
            MainQuery1 += sql + " group by [Route No],[Route Desc],Shift_Type)tmp
                                 group by Shift_Type"
            Dim dtgv As New DataTable
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(MainQuery)
            dt = clsDBFuncationality.GetDataTable(query)
            dt1 = clsDBFuncationality.GetDataTable(MainQuery1)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dr As DataRow = dtgv.NewRow()
            dr(dtgv.Columns("Route No")) = ""
            dr(dtgv.Columns("Route Desc")) = "Total"
            For ii As Integer = 0 To dt.Columns.Count - 1
                dr(dtgv.Columns(ii + 2)) = clsCommon.myCDecimal(dt.Rows(0)(dt.Columns(ii)))
            Next
            dtgv.Rows.Add(dr)
            Dim dr1 As DataRow = dtgv.NewRow()
            dr1(dtgv.Columns("Route No")) = ""
            dr1(dtgv.Columns("Route Desc")) = "T.Morning Sale"
            For ii As Integer = 0 To dt1.Columns.Count - 1
                dr1(dtgv.Columns(ii + 2)) = clsCommon.myCDecimal(dt1.Rows(0)(dt1.Columns(ii)))
            Next
            dtgv.Rows.Add(dr1)
            Dim dr2 As DataRow = dtgv.NewRow()
            dr2(dtgv.Columns("Route No")) = ""
            dr2(dtgv.Columns("Route Desc")) = "T.Evening Sale"
            For ii As Integer = 0 To dt1.Columns.Count - 1
                dr2(dtgv.Columns(ii + 2)) = clsCommon.myCDecimal(dt1.Rows(1)(dt1.Columns(ii)))
            Next
            dtgv.Rows.Add(dr2)
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub MilkSaleReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            strWhrClause = " and convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "''
                             and  convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'
                              and  convert(date, TSPL_SD_SHIPMENT_HEAD." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = "Select Item_Code,Item_Desc,Alies_Name from tspl_item_master where 2=2 and Is_FreshItem=1"
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinLTR,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "cast(Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) as DECIMAL(18, 2)) As   [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0) )"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinLTR,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = "cast(Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) as DECIMAL(18, 2)) As  [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0) )"
                    End If
                Next
            End If
            Dim MainQuery1 As String = ""
            Dim query As String = ""
            Dim sql As String = ""
            MainQuery = "select "
            query = MainQuery
            MainQuery += "[Route No],[Route Desc], "
            MainQuery1 = "Select "
            sql += itemNames + ",convert (decimal(18,2), Sum(" + itemamt + ")) As Total from
                         (Select [Route No],[Route Desc],Shift_Type,max(Conversion_Factor) as Conversion_Factor ,max(CFinLTR) as CFinLTR ,max(CFinPouch) as CFinPouch" + itemName + " from 
                              (Select ItemConversionInLTR.Conversion_Factor AS 'CFinLTR',ItemConversionInPouch.Conversion_Factor As CFinPouch,
                              CurrentUnit.Conversion_Factor, TSPL_SD_SHIPMENT_HEAD.Route_No as [Route No],tspl_route_master.Route_Desc as [Route Desc],
                              TSPL_DEMAND_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_SD_SHIPMENT_HEAD.Shift_Type, TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                              TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty as Qty From TSPL_SD_SHIPMENT_BOOKING_DETAIL
                              LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.DOCUMENT_CODE
                              left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.TR_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code
                              left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
							  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                              Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                              left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code
                              and CurrentUnit.uom_code=	TSPL_DEMAND_BOOKING_DETAIL.unit_code 
                              left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code
                              and CrateUnit.uom_code='Crate'
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch
                              on ItemConversionInPouch.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR
                              on ItemConversionInLTR.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                              Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SHIPMENT_HEAD.Vehicle_Code
                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SHIPMENT_HEAD.Route_No
                              Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.Location_Code
                              where 2=2 " + strWhrClause2 + "  and  TSPL_ITEM_MASTER.Is_FreshItem=1  and TSPL_SD_SHIPMENT_HEAD.Shift_Type in ('AM','PM')   " + whrcls + " 
                              group by TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_ITEM_MASTER.Alies_Name,TSPL_ROUTE_MASTER.[Route_Desc],TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
                              TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty,TSPL_SD_SHIPMENT_HEAD.Shift_Type,  ItemConversionInLTR.Conversion_Factor,ItemConversionInPouch.Conversion_Factor ,CurrentUnit.Conversion_Factor  )tab1  
                              pivot( sum(Qty) for Description in (" + itemName1 + ") ) as Tab2"
            query += sql + " group by [Route No],[Route Desc],Shift_Type)tmp"
            MainQuery += sql + " group by [Route No],[Route Desc],Shift_Type)tmp
                                group by [Route No],[Route Desc]"
            MainQuery1 += sql + " group by [Route No],[Route Desc],Shift_Type)tmp
                                 group by Shift_Type"
            Dim dtgv As New DataTable
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(MainQuery)
            dt = clsDBFuncationality.GetDataTable(query)
            dt1 = clsDBFuncationality.GetDataTable(MainQuery1)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dr As DataRow = dtgv.NewRow()
            dr(dtgv.Columns("Route No")) = ""
            dr(dtgv.Columns("Route Desc")) = "Total"
            For ii As Integer = 0 To dt.Columns.Count - 1
                dr(dtgv.Columns(ii + 2)) = clsCommon.myCDecimal(dt.Rows(0)(dt.Columns(ii)))
            Next
            dtgv.Rows.Add(dr)
            Dim dr1 As DataRow = dtgv.NewRow()
            dr1(dtgv.Columns("Route No")) = ""
            dr1(dtgv.Columns("Route Desc")) = "T.Morning Sale"
            For ii As Integer = 0 To dt1.Columns.Count - 1
                dr1(dtgv.Columns(ii + 2)) = clsCommon.myCDecimal(dt1.Rows(0)(dt1.Columns(ii)))
            Next
            dtgv.Rows.Add(dr1)
            Dim dr2 As DataRow = dtgv.NewRow()
            dr2(dtgv.Columns("Route No")) = ""
            dr2(dtgv.Columns("Route Desc")) = "T.Evening Sale"
            For ii As Integer = 0 To dt1.Columns.Count - 1
                dr2(dtgv.Columns(ii + 2)) = clsCommon.myCDecimal(dt1.Rows(1)(dt1.Columns(ii)))
            Next
            dtgv.Rows.Add(dr2)
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ProductGatePassDetailReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            strWhrClause = " and convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            Dim ShiftType As String = Nothing
            If rbtnMrng.Checked Then
                ShiftType = "'Morning'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnEvng.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnBoths.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = "Select Item_Code,Item_Desc,Alies_Name from tspl_item_master where 2=2 and Is_Ambient=1 and Item_Type='F'"
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinKG,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "CAST(ROUND(Sum([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]),2)AS DECIMAL(10, 2)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinKG,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = ",cast(round(Sum([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]),2)AS DECIMAL(10, 2)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    End If
                Next
            End If
            Dim query As String = ""
            MainQuery = " select [Route No],[Route Desc] " + itemNames + ",Sum(" + itemamt + ") As Total from (Select [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch" + itemName + "
                        from 
                              (Select ItemConversionInLTR.Conversion_Factor AS 'CFinLTR',ItemConversionInPouch.Conversion_Factor As CFinPouch,
                              CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor as CFinKG, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No as [Route No],tspl_route_master.Route_Desc as [Route Desc],
                              TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                              TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as Qty From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE
                              Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No
                              Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code
                              Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code
                              left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.item_code and 	CurrentUnit.uom_code=	TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.unit_code 
						      left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.item_code  and 	CrateUnit.uom_code=	TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.unit_code 
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code 
                              left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code
                              Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No
                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No
                              LEFT OUTER JOIN TSPL_BOOKING_DETAIL ON TSPL_BOOKING_DETAIL.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No AND TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No=TSPL_BOOKING_DETAIL.Line_No
                              LEFT OUTER JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_BOOKING_DETAIL.Against_DemandBooking_No AND TSPL_BOOKING_DETAIL.Line_No= TSPL_DEMAND_BOOKING_DETAIL.Line_No
                              Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code
                              left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code
                              where 2=2 " + strWhrClause2 + " and TSPL_ITEM_MASTER.Is_Ambient=1  and TSPL_ITEM_MASTER. Item_Type='F'   " + whrcls + "   group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No,
							 				TSPL_ITEM_MASTER.Alies_Name,TSPL_ROUTE_MASTER.[Route_Desc],TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code,
                              TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,  ItemConversionInLTR.Conversion_Factor,ItemConversionInPouch.Conversion_Factor ,CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor  )tab1  
                              pivot( sum(Qty) for Description in (" + itemName1 + ") ) as Tab2  group by [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch,CFinKG)tmp
                                group by [Route No],[Route Desc]"
            query = MainQuery
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub MilkProductDemandReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date")
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            strWhrClause = " and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            Dim ShiftType As String = Nothing
            If rbtnMrng.Checked Then
                ShiftType = "'Morning'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnEvng.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnBoths.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = " select TSPL_DEMAND_BOOKING_DETAIL.Item_Code,max(Alies_Name)Alies_Name
                        from TSPL_DEMAND_BOOKING_DETAIL
                        left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                        left outer join TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No
                        WHERE 2=2 " + strWhrClause2 + " " + whrcls + "
                        group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code   order by Item_Code "
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    End If
                Next
            End If
            Dim query As String = ""
            MainQuery = " select Route_No,max(Route_Desc)Route_Desc " + itemNames + ",Sum(" + itemamt + ") As Total from (Select Route_No,max(Route_Desc)Route_Desc " + itemName + " from(
                        select Document_No,max(Document_Date)Document_Date,max(Route_No)Route_No,max(Route_Desc)Route_Desc,Item_Code,max(Alies_Name)Alies_Name,sum(PouchQty+CrateQty+Quantity) as Qty
                        from
                        (select ISNULL(case when TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Pouch' then sum(ISNULL(Qty/ItemConversionInCrate.Conversion_Factor,0))  end,0) as PouchQty,
                        ISNULL(case when TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Crate' then sum(ISNULL(Qty,0)) end,0) as CrateQty,
                        ISNULL(case when TSPL_DEMAND_BOOKING_DETAIL.Unit_code<>'Crate' and TSPL_DEMAND_BOOKING_DETAIL.Unit_code<>'Pouch' then sum(ISNULL(Qty/ItemConversionInCrate.Conversion_Factor,0)) end,0)as Quantity,
                        max(TSPL_DEMAND_BOOKING_DETAIL.Document_No)Document_No,                        
                        max(Document_Date)Document_Date,max(TSPL_DEMAND_BOOKING_MASTER.Route_No)Route_No,max(Route_Desc)Route_Desc,max(ItemConversionInLTR.Conversion_Factor) as ItemInLTR ,
                        max(ItemConversionInPouch.Conversion_Factor) as ItemInPouch,max(ItemConversionInCrate.Conversion_Factor) as ItemInCrate,
                        TSPL_DEMAND_BOOKING_DETAIL.Item_Code,max(Alies_Name)Alies_Name,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,sum(Qty)Qty from TSPL_DEMAND_BOOKING_DETAIL
                        left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                        left OUTER join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code and CurrentUnit.uom_code=TSPL_DEMAND_BOOKING_DETAIL.unit_code
                        left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code  and 	CrateUnit.uom_code=	'Crate' 
						left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
						left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
						left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Crate') as ItemConversionInCrate on ItemConversionInCrate.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                        left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No
                        where 2=2 " + strWhrClause2 + "  " + whrcls + "  group by TSPL_DEMAND_BOOKING_DETAIL.Document_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
                        TSPL_DEMAND_BOOKING_DETAIL.Unit_code )xx group by xx.Document_No,xx.Item_Code)  as tab1  
                              pivot( sum(Qty) for Alies_Name in (" + itemName1 + ") ) as Tab2  group by Route_No)tmp
                                group by Route_No"
            query = MainQuery
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display")
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            'Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)
            Dim item As GridViewSummaryItem
            For dblrows As Integer = 2 To Gv1.Columns.Count - 1
                item = New GridViewSummaryItem(Gv1.Columns(dblrows).HeaderText, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DairyProductGatePassReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "GPDate"
            strWhrClause = " and convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            Dim ShiftType As String = Nothing
            If rbtnMrng.Checked Then
                ShiftType = "'Morning'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnEvng.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnBoths.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = "Select Item_Code,Item_Desc,Alies_Name from tspl_item_master where 2=2 and Is_Ambient=1 and Item_Type='F'"
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinKG,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "CAST(ROUND(Sum([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]),2)AS DECIMAL(10, 2)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinKG,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = ",cast(round(Sum([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]),2)AS DECIMAL(10, 2)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    End If
                Next
            End If
            Dim query As String = ""
            MainQuery = " select [Route No],[Route Desc] " + itemNames + ",Sum(" + itemamt + ") As Total from (Select [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch" + itemName + "
                        from 
                              (Select ItemConversionInLTR.Conversion_Factor AS 'CFinLTR',ItemConversionInPouch.Conversion_Factor As CFinPouch,
                              CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor as CFinKG, TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No as [Route No],tspl_route_master.Route_Desc as [Route Desc],
                              TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                              TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty as Qty From TSPL_DAIRYSALE_GATEPASS_DETAIL
                              Left Outer Join TSPL_DAIRYSALE_GATEPASS_MASTER On TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode
                              Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                              left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code 
						      left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code  and 	CrateUnit.uom_code=	'Crate'
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code 
                              left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No
                              Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code
                              where 2=2 " + strWhrClause2 + " and TSPL_ITEM_MASTER.Is_Ambient=1  and TSPL_ITEM_MASTER. Item_Type='F'   
                              group by TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No,TSPL_ITEM_MASTER.Alies_Name,
                              TSPL_ROUTE_MASTER.[Route_Desc],TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code,
                              TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty,  ItemConversionInLTR.Conversion_Factor,ItemConversionInPouch.Conversion_Factor ,CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor  )tab1  
                              pivot( sum(Qty) for Description in (" + itemName1 + ") ) as Tab2  group by [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch,CFinKG)tmp
                                group by [Route No],[Route Desc]"
            query = MainQuery
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DairyMilkGatePassReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "GPDate"
            strWhrClause = " and convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            Dim ShiftType As String = Nothing
            If rbtnMrng.Checked Then
                ShiftType = "'Morning'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnEvng.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnBoths.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = "Select Item_Code,Item_Desc,Alies_Name from tspl_item_master where 2=2 and Is_FreshItem=1"
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinLTR,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinLTR,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    End If
                Next
            End If
            Dim query As String = ""
            MainQuery = " select [Route No],[Route Desc] " + itemNames + ",Sum(" + itemamt + ") As Total from (Select [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch" + itemName + "
                        from 
                              (Select ItemConversionInLTR.Conversion_Factor AS 'CFinLTR',ItemConversionInPouch.Conversion_Factor As CFinPouch,
                              CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor as CFinKG, TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No as [Route No],tspl_route_master.Route_Desc as [Route Desc],
                              TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                              TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty as Qty From TSPL_DAIRYSALE_GATEPASS_DETAIL
                              Left Outer Join TSPL_DAIRYSALE_GATEPASS_MASTER On TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode
                              Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                              left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code 
						      left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code  and 	CrateUnit.uom_code=	'Crate'
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code 
                              left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No
                              Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code
                              where 2=2 " + strWhrClause2 + " and  TSPL_ITEM_MASTER.Is_FreshItem=1  
                              group by TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No,TSPL_ITEM_MASTER.Alies_Name,
                              TSPL_ROUTE_MASTER.[Route_Desc],TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code,
                              TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty,  ItemConversionInLTR.Conversion_Factor,ItemConversionInPouch.Conversion_Factor ,CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor  )tab1  
                              pivot( sum(Qty) for Description in (" + itemName1 + ") ) as Tab2  group by [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch,CFinKG)tmp
                                group by [Route No],[Route Desc]"
            query = MainQuery
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub TCSReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_DETAIL.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_DETAIL.Route_No in (" + ss + ")  "
            End If
            If clsCommon.CompairString(rddlTCSShift.Text, "Morning") = CompairStringResult.Equal Then
                strWhrClause2 += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' "
            ElseIf clsCommon.CompairString(rddlTCSShift.Text, "Evening") = CompairStringResult.Equal Then
                strWhrClause2 += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' "
            End If
            '            MainQuery = "select Document_No as [Document No], max(Document_Date) as [Document Date], max([Time]) as [Time], max([Customer Code]) as [Customer Code], [WdName] as [Customer Name], [Group], [Route No], max( [Booking Time(AM / PM) ] ) as [Booking Time(AM / PM) ], Max(Created_By) as [Created By], max(Created_Date) as [Created Date], max(Modified_By) as [Modified By], max(Modified_Date) as [Modified Date], max(Total_Amt) as [TCS Base Amount], cast( isnull(((isnull(max(TCSAmount),0) *100)/max(Total_Amt)),0) as decimal(18,2)) as [TCS%], isnull(max(TCSAmount),0) as [TCS Amount], (max(Total_Amt) +isnull(max(TCSAmount),0)) as [Total Amount] from ( select max(zzz.item_code) as item_code, zzz.Document_No, max(Document_Date) as Document_Date, max([Time]) as [Time], zzz.[DO No], zzz.[Short Close], max(GatePass_Type) as [Booking Time(AM / PM) ], Max(Created_By) as Created_By, max(Created_Date) as Created_Date, max(Modified_By) as Modified_By, max(Modified_Date) as Modified_Date, max(DocumentAmount) as DocumentAmount,max(zzz.Total_Amt) as Total_Amt, max([Customer Code]) as [Customer Code], zzz.[VEHICLE NO], zzz.[WdName], zzz.Description, zzz.Cust_Group_Code as [Group], zzz.[Route No], sum(qty) as qty, sum(QtyLtr) as QtyLtr, max(zzz.TCSAmount) as TCSAmount from ( Select isnull( TSPL_BOOKING_MATSER.GatePass_Type, '' ) as GatePass_Type, TSPL_BOOKING_MATSER.TCSAmount, TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Total_Amt, Convert ( varchar, TSPL_BOOKING_MATSER.Document_Date, 103 ) as Document_Date, case when LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100 ), 7 ) ) = '12:00AM' then LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_BOOKING_MATSER.Created_Date, 100 ), 7 ) ) else LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100 ), 7 ) ) end as [Time], TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No], isnull( TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close, 'N' ) as [Short Close], TSPL_BOOKING_MATSER.Booking_Type, TSPL_BOOKING_MATSER.BookingThrough, Convert ( varchar, TSPL_BOOKING_MATSER.TruckSheetGenerate ) as TruckSheetGenerate, Convert ( varchar, TSPL_BOOKING_MATSER.AgainstGatePass ) as AgainstGatePass, Convert ( varchar, TSPL_BOOKING_MATSER.is_Cancelled ) as is_Cancelled, TSPL_BOOKING_MATSER.Payment_Mode, ( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH' ) + '.' + format ( TSPL_BOOKING_MATSER.Created_Date, 'mm' ) ) as Created_Date_Time, TSPL_BOOKING_MATSER.Created_By, convert ( varchar, TSPL_BOOKING_MATSER.Created_Date, 103 ) as Created_Date, TSPL_BOOKING_MATSER.Modified_By, Convert ( varchar, TSPL_BOOKING_MATSER.Modified_Date, 103 ) as Modified_Date, TSPL_BOOKING_DETAIL.DocumentAmount, TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, isnull( TSPL_CUSTOMER_MASTER.cust_category_code, '' ) as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_BOOKING_DETAIL.Unit_code as UOM, TSPL_BOOKING_DETAIL.route_no as [Route No], TSPL_ITEM_MASTER.Alies_Name As [Description], TSPL_VEHICLE_MASTER.Description [Lorry_No], TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_MASTER.Zone_Code, IsNull( TSPL_VEHICLE_MASTER.Description, '''' ) As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth, TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT) ], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT) ], TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as [Dispatch No(T) ], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T) ], TBL_SCHEME_VALUE.Scheme_Booking_Qty, ( CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty = 0 THEN 0 ELSE ( TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor )/ coalesce( TSPL_ITEM_UOM_DETAILltr.Conversion_Factor, TSPL_ITEM_UOM_DETAILKG.Conversion_Factor ) END ) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No, STUFF( ( SELECT ', ' + QUOTENAME ( TSPL_SD_SHIPMENT_DETAIL.Document_Code ) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 1 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Dispatch_No], STUFF( ( SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 1 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Invoice_No] from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable = 1 Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No, STUFF( ( SELECT ', ' + QUOTENAME ( TSPL_SD_SHIPMENT_DETAIL.Document_Code ) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 0 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Dispatch_No], STUFF( ( SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 0 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Invoice_No] from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable = 0 Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No left Outer Join ( Select TSPL_BOOKING_DETAIL.Document_No, Sum ( isnull(Booking_Qty, 0) ) as Scheme_Booking_Qty from TSPL_BOOKING_DETAIL where Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE On TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code = 'LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code = 'KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code = 'CRATE' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAILUOM.UOM_Code 
            ' where 2 = 2 and convert( date, TSPL_BOOKING_MATSER.Document_Date, 103 ) >= '" + clsCommon.GetPrintDate(fromDate.Value) + "' and convert( date, TSPL_BOOKING_MATSER.Document_Date, 103 ) <= '" + clsCommon.GetPrintDate(ToDate.Value) + "'
            '  " + strWhrClause2 + "
            ') zzz 
            ' where zzz.Scheme_Item = 'N' group by zzz.Document_No, zzz.[VEHICLE NO], zzz.WdName, zzz.Description, zzz.[Customer Category Code], zzz.Cust_Group_Code, zzz.Zone_Code, zzz.[Route No], zzz.[DO NO], zzz.[Short Close] ) zpivot
            ' group by zpivot.Document_No, zpivot.[VEHICLE NO], zpivot.[WdName], zpivot.[Group], zpivot.[Route No], zpivot.[DO NO], zpivot.[Short Close]"
            MainQuery = "select  Document_No as [Document No],max(Document_Date) as [Document Date],max([Time]) as [Time],max([Customer Code]) as [Customer Code],[WdName] as [Customer Name], 
  [Group],[Route No], max([Booking Time(AM / PM) ]) as [Booking Time(AM / PM) ], Max(Created_By) as [Created By], max(Created_Date) as [Created Date],max(Modified_By) as [Modified By],max(Modified_Date) as [Modified Date],max(Total_Amt) - isnull(max(TCSAmount),0) as [TCS Base Amount],cast(isnull(((isnull(max(TCSAmount),0) * 100)/ max(Total_Amt)),0) as decimal(18, 2)) as [TCS % ],isnull(max(TCSAmount),0) as [TCS Amount],(max(Total_Amt) ) as [Total Amount] 
from (select max(zzz.item_code) as item_code,zzz.Document_No,max(Document_Date) as Document_Date,max([Time]) as [Time],zzz.[DO No], zzz.[Short Close],max(GatePass_Type) as [Booking Time(AM / PM) ],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date,max(DocumentAmount) as DocumentAmount,	   max(zzz.Total_Amt) as Total_Amt,max([Customer Code]) as [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,     zzz.Cust_Group_Code as [Group],zzz.[Route No],sum(qty) as qty,sum(QtyLtr) as QtyLtr,max(zzz.TCSAmount) as TCSAmount
  from ( Select isnull( TSPL_BOOKING_MATSER.GatePass_Type, '' ) as GatePass_Type,TSPL_BOOKING_MATSER.TCSAmount, TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Total_Amt, Convert (varchar, TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,case when LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Document_Date,100),7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date,100),7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date,100),7)) end as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],         isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,         TSPL_BOOKING_MATSER.BookingThrough,Convert ( varchar, TSPL_BOOKING_MATSER.TruckSheetGenerate ) as TruckSheetGenerate, Convert ( varchar, TSPL_BOOKING_MATSER.AgainstGatePass ) as AgainstGatePass, Convert ( varchar, TSPL_BOOKING_MATSER.is_Cancelled ) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,(format ( TSPL_BOOKING_MATSER.Created_Date, 'HH' ) + '.' + format ( TSPL_BOOKING_MATSER.Created_Date, 'mm' ) ) as Created_Date_Time, TSPL_BOOKING_MATSER.Created_By, convert ( varchar, TSPL_BOOKING_MATSER.Created_Date, 103 ) as Created_Date, TSPL_BOOKING_MATSER.Modified_By, Convert ( varchar, TSPL_BOOKING_MATSER.Modified_Date, 103 ) as Modified_Date,         TSPL_BOOKING_DETAIL.DocumentAmount, TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq,     TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,       isnull( TSPL_CUSTOMER_MASTER.cust_category_code, '' ) as [Customer Category Code],TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName,TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No],TSPL_ITEM_MASTER.Alies_Name As [Description],       TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code,          IsNull(TSPL_VEHICLE_MASTER.Description,'''') As [VEHICLE NO],TSPL_BOOKING_DETAIL.Booking_Qty as Qty,          TSPL_BOOKING_MATSER.Document_Date As [Order Date], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],          TSPL_CUSTOMER_MASTER.OldName as Booth, TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT) ],          TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT) ],TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as [Dispatch No(T) ],TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T) ],TBL_SCHEME_VALUE.Scheme_Booking_Qty,( CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty = 0 THEN 0 ELSE ( TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor )/ coalesce( TSPL_ITEM_UOM_DETAILltr.Conversion_Factor, TSPL_ITEM_UOM_DETAILKG.Conversion_Factor ) END ) AS QtyLtr  From TSPL_BOOKING_DETAIL 
          Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No 
          Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
          Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code 
          Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
          Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code 
          left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code Left Outer Join (
            Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,STUFF( ( SELECT ', ' + QUOTENAME ( TSPL_SD_SHIPMENT_DETAIL.Document_Code )
			FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail 
                   left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code
                    left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code 
                  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 1 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Dispatch_No],
              STUFF( ( SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 1 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Invoice_No]
            from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE 
              inner Join TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code 
              inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code 
            where 
              TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable = 1 
            Group by 
              TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No
          ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No 
         Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No, STUFF( ( SELECT ', ' + QUOTENAME ( TSPL_SD_SHIPMENT_DETAIL.Document_Code ) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 0 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Dispatch_No], STUFF( ( SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code, delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable = 0 and ThisTableDetail.Posted = 1 FOR XML PATH ('') ), 1, 2, '' ) AS [Invoice_No] from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable = 0 Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No 
         left Outer Join ( Select TSPL_BOOKING_DETAIL.Document_No, Sum ( isnull(Booking_Qty, 0) ) as Scheme_Booking_Qty from TSPL_BOOKING_DETAIL where Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE On TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No 
          LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No 
          left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILLTR.Item_Code 
          and TSPL_ITEM_UOM_DETAILLTR.UOM_Code = 'LTR' 
          left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILKG.Item_Code 
          and TSPL_ITEM_UOM_DETAILKG.UOM_Code = 'KG' 
          left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILCREATE.Item_Code 
          and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code = 'CRATE' 
          left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILUOM.Item_Code 
          and TSPL_BOOKING_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAILUOM.UOM_Code
                where 2 = 2 and convert( date, TSPL_BOOKING_MATSER.Document_Date, 103 ) >= '" + clsCommon.GetPrintDate(fromDate.Value) + "' and convert( date, TSPL_BOOKING_MATSER.Document_Date, 103 ) <= '" + clsCommon.GetPrintDate(ToDate.Value) + "'
                " + strWhrClause2 + " ) zzz where zzz.Scheme_Item = 'N' group by zzz.Document_No, zzz.[VEHICLE NO], zzz.WdName, zzz.Description, zzz.[Customer Category Code], zzz.Cust_Group_Code, zzz.Zone_Code, zzz.[Route No], zzz.[DO NO], zzz.[Short Close] ) zpivot group by zpivot.Document_No, zpivot.[VEHICLE NO], zpivot.[WdName], zpivot.[Group], zpivot.[Route No], zpivot.[DO NO], zpivot.[Short Close]"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(MainQuery)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total Amount").IsPinned = True
            Gv1.Columns("Total Amount").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DemandSheetReport(ByVal IsPrint As Exporter)
        Try
            Dim strItem2WithSum As String = ""
            Dim ItemInUse As String = ""
            Dim strCreateConv As String = ""
            strCreateConv = " TSPL_BOOKING_DETAIL.Booking_Qty "
            Dim strWhrClause As String = String.Empty
            strWhrClause = " and convert(date, TSPL_BOOKING_MATSER.Document_Date ,103) = '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' "
            If clsCommon.CompairString(rddlTCSShift.Text, "Morning") = CompairStringResult.Equal Then
                strWhrClause += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' "
            ElseIf clsCommon.CompairString(rddlTCSShift.Text, "Evening") = CompairStringResult.Equal Then
                strWhrClause += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' "
            End If
            If clsCommon.myLen(txtFndRoute.Value) > 0 Then
                strWhrClause += " and TSPL_BOOKING_DETAIL.Route_No in ('" + txtFndRoute.Value + "')  "
            End If
            If clsCommon.myLen(txtfndCustomer.Value) > 0 Then
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in ('" & txtfndCustomer.Value & "')  "
            End If
            Dim ItemQry As String = "select  distinct  '[' + Alies_Name +']'  Alies_Name , +'Sum(isnull(' + '[' + Alies_Name +']' +',0))' + ' as ' + '[' + Alies_Name + ']' Alies_Sum_Name , RowNo , Sku_Seq , Item_Code  from (select tspl_item_master.Item_Code,tspl_item_master.Alies_Name ,1 as RowNo,tspl_item_master.Sku_Seq , TSPL_BOOKING_MATSER.Document_Date ,TSPL_ROUTE_MASTER.route_no , TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_BOOKING_MATSER.GatePass_Type from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code   left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Item_Code = tspl_item_master.Item_Code	  
	  Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    union
    select tspl_item_master.Item_Code ,tspl_item_master.Alies_Name,1 as RowNo,tspl_item_master.Sku_Seq, TSPL_BOOKING_MATSER.Document_Date ,TSPL_ROUTE_MASTER.route_no , TSPL_CUSTOMER_MASTER.Cust_Code , TSPL_BOOKING_MATSER.GatePass_Type from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 	  left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Item_Code = tspl_item_master.Item_Code   	  Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    union all
    select tspl_item_master.Item_Code ,tspl_item_master.Alies_Name,2 as RowNo,tspl_item_master.Sku_Seq, TSPL_BOOKING_MATSER.Document_Date ,TSPL_ROUTE_MASTER.route_no , TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_BOOKING_MATSER.GatePass_Type from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code   left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Item_Code = tspl_item_master.Item_Code	  Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No
  Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
    where  tspl_item_master.Is_Ambient=1   and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 
    )z	where (Alies_Name !='' or Alies_Name is null) and convert(date,Document_Date,103) = convert(date,'" & fromDate.Value & "' , 103) "
            If clsCommon.myLen(txtFndRoute.Value) > 0 Then
                ItemQry += " and Route_No in ('" + txtFndRoute.Value + "')  "
            End If
            If clsCommon.myLen(txtfndCustomer.Value) > 0 Then
                ItemQry += " and Cust_Code in ('" & txtfndCustomer.Value & "')  "
            End If
            If clsCommon.CompairString(rddlTCSShift.Text, "Morning") = CompairStringResult.Equal Then
                ItemQry += " and GatePass_Type='AM' "
            ElseIf clsCommon.CompairString(rddlTCSShift.Text, "Evening") = CompairStringResult.Equal Then
                ItemQry += " and GatePass_Type='PM' "
            End If
            ItemQry += " order by RowNo,Sku_Seq,Item_Code"
            Dim dtItem As DataTable = clsDBFuncationality.GetDataTable(ItemQry)
            If dtItem Is Nothing OrElse dtItem.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Dim arrItems As New List(Of String)
            Dim arrItemSum As New List(Of String)
            If dtItem IsNot Nothing OrElse dtItem.Rows.Count > 0 Then
                For Each dr As DataRow In dtItem.Rows
                    arrItems.Add(dr("Alies_Name"))
                    arrItemSum.Add(dr("Alies_Sum_Name"))
                Next
            End If
            Dim strItem2 As String = clsCommon.GetMulcallStringWithComma(arrItems)
            strItem2WithSum = clsCommon.GetMulcallStringWithComma(arrItemSum)
            ItemInUse = " TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
            ItemInUse += strWhrClause
            ItemInUse += "  order by Alies_Name "
            Dim strAliasCol As String = "( TSPL_ITEM_MASTER.Alies_Name )"
            Dim MainQuery As String = ""
            MainQuery = " select  ROW_NUMBER() Over (Order By  max(Document_Date)) AS [SNo.] , max(UOM)[Qty In] , max([Customer Code]) as Booth , " + strItem2WithSum + " , sum(DocumentAmount) as [Total Amount],isnull(max(TCSAmount),0) TCS from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Time]) as [Time]  ,zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,sum(Amount_with_Tax) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr , max(UOM)UOM , max(TCSAmount)TCSAmount from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr  ,TSPL_BOOKING_MATSER.TCSAmount From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  left outer join ( select DISTINCT  Unit_code from TSPL_DEMAND_BOOKING_DETAIL ) as TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Unit_code = TSPL_BOOKING_DETAIL.Unit_code
  Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2  " + strWhrClause + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_No,zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.[DO NO],zpivot.[Short Close] "
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(MainQuery)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.SummaryRowsBottom.Clear()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item3 As New GridViewSummaryItem("TCS", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                For i As Integer = 3 To Gv1.Columns.Count - 3
                    Dim items = Gv1.Columns(i).HeaderText()
                    Dim item2 As New GridViewSummaryItem(items, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Gv1.Columns(i).FormatString = "{0:n2}"
                Next
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                ReStoreGridLayout()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub MilkGatePassDetailReport(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim whrcls As String = Nothing
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            strWhrClause = " and convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            strWhrClause2 = " and convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            Dim ShiftType As String = Nothing
            If rbtnMrng.Checked Then
                ShiftType = "'Morning'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnEvng.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            ElseIf rbtnBoths.Checked Then
                ShiftType = "'Evening'"
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType = (" + ShiftType + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + ss + ")  "
            End If
            If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
            End If
            Dim ItemInUse As String = ""
            Dim itemqry As String = Nothing
            Dim itemName As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemName1 As String = Nothing
            Dim DedCode As String = Nothing
            Dim itemamt As String = Nothing
            Dim dtitemName As DataTable = Nothing
            itemqry = "Select Item_Code,Item_Desc,Alies_Name from tspl_item_master where 2=2 and Is_FreshItem=1"
            dtitemName = clsDBFuncationality.GetDataTable(itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If clsCommon.myLen(itemName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                        DedCode += "," + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinLTR,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames += "," + "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 += "," + "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt += "+(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    Else
                        DedCode = clsCommon.myCstr(dtitemName.Rows(i)("Item_Code"))
                        itemName = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]*Conversion_Factor/CFinLTR,0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemNames = ",Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemName1 = "[" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "]"
                        itemamt = "(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Alies_Name")) + "],0))"
                    End If
                Next
            End If
            Dim query As String = ""
            MainQuery = " select [Route No],[Route Desc] " + itemNames + ",Sum(" + itemamt + ") As Total from (Select [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch" + itemName + "
                        from 
                              (Select ItemConversionInLTR.Conversion_Factor AS 'CFinLTR',ItemConversionInPouch.Conversion_Factor As CFinPouch,
                              CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor as CFinKG, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No as [Route No],tspl_route_master.Route_Desc as [Route Desc],
                              TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                              TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as Qty From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE
                              Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No
                              Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code
                              Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code
                              left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.item_code and 	CurrentUnit.uom_code=	TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.unit_code 
						      left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.item_code  and 	CrateUnit.uom_code=	'Crate'
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code
							  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code 
                              left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code
                              Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No
                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No
                              LEFT OUTER JOIN TSPL_BOOKING_DETAIL ON TSPL_BOOKING_DETAIL.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No AND TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No=TSPL_BOOKING_DETAIL.Line_No
                              LEFT OUTER JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_BOOKING_DETAIL.Against_DemandBooking_No AND TSPL_BOOKING_DETAIL.Line_No= TSPL_DEMAND_BOOKING_DETAIL.Line_No
                              Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code
                              left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code
                              where 2=2 " + strWhrClause2 + " and  TSPL_ITEM_MASTER.Is_FreshItem=1 " + whrcls + "   group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No,
							 				TSPL_ITEM_MASTER.Alies_Name,TSPL_ROUTE_MASTER.[Route_Desc],TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code,
                              TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,  ItemConversionInLTR.Conversion_Factor,ItemConversionInPouch.Conversion_Factor ,CurrentUnit.Conversion_Factor,ItemConversionInKG.Conversion_Factor  )tab1  
                              pivot( sum(Qty) for Description in (" + itemName1 + ") ) as Tab2  group by [Route No],[Route Desc],Conversion_Factor,CFinLTR,CFinPouch,CFinKG)tmp
                                group by [Route No],[Route Desc]"
            query = MainQuery
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns("Total").IsPinned = True
            Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub PrintAsPerBooking(ByVal IsPrint As Exporter)
        Try
            'Sanjay,Add Customer Category 
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            If chkFirstAndSecondSpellAbstract.Checked = True AndAlso (chkBookingWise.Checked = True OrElse chkSaleInvoiceWise.Checked = True OrElse chkSummary.Checked = True OrElse chkFirstAndSecondSpell.Checked = True OrElse chkRouteBoothWise.Checked = True OrElse ChkDayWiseSummary.Checked = True) Then
                Throw New Exception("Select only one check box at a time First And Second Spell Card Sale Abstract or Other Check Box ")
            End If
            'If chkBookingWise.Checked = False Then
            '    Throw New Exception("Select Booking Wise")
            'End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            If chkFilterByCreatedDate.Checked Then
                strDate = "Created_Date"
            End If
            If chkBookingWise.Checked Then
                strWhrClause = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
                strWhrRoutSummaryPrint = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            End If
            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                strWhrClause += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If chkBookingWise.Checked = True Or chkRouteBoothWise.Checked = True Then
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_DETAIL.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Route_No in (" + ss + ")  "
                End If
                If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_DETAIL.Unit_code in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Unit_code in (" + ss + ")  "
                End If
                If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
                End If
            ElseIf chkSummary.Checked = True Then
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_DETAIL.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Route_No in (" + ss + ")  "
                End If
                'ElseIf chkSaleInvoiceWise.Checked = True Then
                '    If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                '        Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                '        Dim sss As String = ss.Replace("'", "''")
                '        strWhrClause += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + sss + ")  "
                '        strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + ss + ")  "
                '    End If
                '    If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                '        Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                '        Dim sss As String = ss.Replace("'", "''")
                '        strWhrClause += " and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code in (" + sss + ")  "
                '        strWhrClause2 += " and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code in (" + ss + ")  "
                '    End If
                '    If clsCommon.CompairString(ddlInvocieType.SelectedValue, "Taxable") = CompairStringResult.Equal Then
                '        strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =1  "
                '    ElseIf clsCommon.CompairString(ddlInvocieType.SelectedValue, "Non Taxable") = CompairStringResult.Equal Then
                '        strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =0  "
                '    End If
                'Else
                '    If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                '        Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                '        Dim sss As String = ss.Replace("'", "''")
                '        strWhrClause += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + sss + ")  "
                '        strWhrClause2 += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + ss + ")  "
                '    End If
                '    If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                '        Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                '        Dim sss As String = ss.Replace("'", "''")
                '        strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                '        strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
                '    End If
            End If
            If chkBookingWise.Checked = True AndAlso chkGatePass.Checked = True Then
                strWhrClause += " and TSPL_BOOKING_MATSER.AgainstGatePass=1 "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.AgainstGatePass=1 "
            End If
            If chkBookingWise.Checked = True Then
                'Dim strAliasCol As String = " z.ItemDescNew  "
                Dim ItemInUse As String = "
Select distinct finalItemQry.ItemDescNew,tspl_item_master.Sku_Seq from (
select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1
and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch')
union
select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq  from tspl_item_master 
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
where  tspl_item_master.Is_FreshItem =0 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =0 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1
 ) finalItemQry
Left Outer Join tspl_item_master On finalItemQry.Item_Code = tspl_item_master.Item_Code
Left Outer Join TSPL_BOOKING_DETAIL On finalItemQry.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code
Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  TSPL_BOOKING_DETAIL.Scheme_Item='N' 
" & strWhrClause2 & " order by tspl_item_master.Sku_Seq"
                'Dim strItmeHeadingScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME( " + strAliasCol + ")  as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strItmeHeadingScheme As String = ""
                Dim strItemHeadingSum As String = ""
                Dim TempDt As DataTable = clsDBFuncationality.GetDataTable(ItemInUse)
                If TempDt Is Nothing OrElse TempDt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                For i As Integer = 0 To TempDt.Rows.Count - 1
                    If clsCommon.CompairString(strItmeHeadingScheme, "") = CompairStringResult.Equal Then
                        strItmeHeadingScheme += "[" + TempDt.Rows(i).Item("ItemDescNew") + "]"
                        strItemHeadingSum += "isnull([" + TempDt.Rows(i).Item("ItemDescNew") + "],0)"
                    Else
                        strItmeHeadingScheme += ",[" + TempDt.Rows(i).Item("ItemDescNew") + "]"
                        strItemHeadingSum += "+isnull([" + TempDt.Rows(i).Item("ItemDescNew") + "],0)"
                    End If
                Next
                Dim whr As String = String.Empty
                If cboShift.Text = "Morning" Then
                    whr += "where 2=2 and xx.ShiftWise= 'Morning' "
                ElseIf cboShift.Text = "Evening" Then
                    whr += "where 2=2 and xx.ShiftWise= 'Evening' "
                Else
                    whr += "where 2=2 "
                End If
                Dim qry As String = "select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY Cust_code asc) as Sno,Route_no AS [Route],Cust_code AS [Customer Code],customer_name as [Customer Name],ShiftWise as [Shift],
" + strItmeHeadingScheme + ",(" + strItemHeadingSum + ") as [Grand Total]
,Created_By as [Created By],modified_By as [Modified By] from (
select * from 
(select tspl_booking_detail.Cust_code,tspl_Customer_master.customer_name,tspl_Customer_master.Route_no,tspl_booking_detail.Booking_qty,tspl_item_master.Short_Description +' - '+ tspl_booking_detail.Unit_code as ItemDescNew,tspl_booking_matser.Modified_By,tspl_booking_matser.Created_By,CASE WHEN tspl_booking_matser.gatePass_type='AM' THEN 'Morning' else 'Evening' end as ShiftWise from tspl_booking_matser
left outer join tspl_booking_detail on tspl_booking_detail.document_no=tspl_booking_matser.document_no
left outer join tspl_item_master on tspl_item_master.item_code=tspl_booking_detail.item_code
left outer join tspl_Customer_master on tspl_Customer_master.Cust_code=tspl_booking_detail.Cust_code
where 1=1 " + strWhrClause2 + " 
)final
PIVOT(
sum(final.Booking_qty) 
FOR ItemDescNew IN (" + strItmeHeadingScheme + ")) AS pivot_table )xx " + whr + " "
                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(qry)
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                    Gv1.Columns.Clear()
                    Gv1.DataSource = dtgv
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.BestFitColumns()
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 5 To Gv1.Columns.Count - 3
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                        Gv1.Columns(i).FormatString = "{0:n2}"
                    Next
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    ReStoreGridLayout()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        Try
            'Sanjay,Add Customer Category 
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            If chkFirstAndSecondSpellAbstract.Checked = True AndAlso (chkBookingWise.Checked = True OrElse chkSaleInvoiceWise.Checked = True OrElse chkSummary.Checked = True OrElse chkFirstAndSecondSpell.Checked = True OrElse chkRouteBoothWise.Checked = True OrElse ChkDayWiseSummary.Checked = True) Then
                Throw New Exception("Select only one check box at a time First And Second Spell Card Sale Abstract or Other Check Box ")
            End If
            If chkSaleInvoiceWise.Checked = True And chkBookingWise.Checked = True And chkSummary.Checked And chkFirstAndSecondSpell.Checked = True Then
                Throw New Exception("Select only one check box at a time Booking Wise/Sale Invoice Wise/Summary/First And Second Spell Card Sale Summary")
            End If
            If chkSaleInvoiceWise.Checked = True And chkBookingWise.Checked = True Then
                Throw New Exception("Select only one check box at a time Booking Wise/Sale Invoice Wise")
            End If
            If chkSaleInvoiceWise.Checked = True And chkSummary.Checked = True Then
                Throw New Exception("Select only one check box at a time Sale Invoice Wise/Summary")
            End If
            If chkBookingWise.Checked = True And chkSummary.Checked Then
                Throw New Exception("Select only one check box at a time Booking Wise/Summary")
            End If
            If chkBookingWise.Checked = True Then
                If (chkMilkPouch.Checked = True OrElse chkProduct.Checked = True) AndAlso chkRouteSummary.Checked = True Then
                    Throw New Exception("Select only one check box at a time (Milk Pouch/Product) or Route Summary Print")
                End If
            End If
            If chkMilkPouch.Checked = True And chkProduct.Checked = True Then
                Throw New Exception("Select only one check box at a time Milk Pouch/Product")
            End If
            If chkSaleInvoiceWise.Checked = False Or chkSummary.Checked = False Then
                If clsCommon.CompairString(ddlInvocieType.SelectedValue, "Taxable") = CompairStringResult.Equal Or clsCommon.CompairString(ddlInvocieType.SelectedValue, "Non Taxable") = CompairStringResult.Equal Then
                    Throw New Exception("Select Invoice Type only with Sale Invoice Wise")
                End If
            End If
            If (chkRouteBoothWise.Checked = True AndAlso chkBookingWise.Checked = True) OrElse (chkRouteBoothWise.Checked = True AndAlso chkSummary.Checked = True) OrElse (chkRouteBoothWise.Checked = True AndAlso chkSaleInvoiceWise.Checked = True) OrElse (chkRouteBoothWise.Checked = True AndAlso chkFirstAndSecondSpell.Checked = True) Then
                Throw New Exception("Select only one check box at a time Route/Booth Wise or Other Check Box ")
            End If
            If ChkDayWiseSummary.Checked Then
                If (Not chkSaleInvoiceWise.Checked AndAlso Not chkBookingWise.Checked) Then
                    Throw New Exception("Please Check one option sale invoice wise or Booking wise")
                End If
                If (chkSummary.Checked OrElse chkFirstAndSecondSpell.Checked OrElse chkRouteBoothWise.Checked) Then
                    Throw New Exception("Select check only one check box At a time Day wise summary / Summary /First and second spell or Route Booth wise ")
                End If
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            itemCode = " and 2=2 "
            Dim strDate As String = "Document_Date"
            If chkFilterByCreatedDate.Checked Then
                strDate = "Created_Date"
            End If
            If chkBookingWise.Checked Or chkFirstAndSecondSpell.Checked = True Or chkFirstAndSecondSpellAbstract.Checked = True Or chkRouteBoothWise.Checked = True Then
                strWhrClause = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
                strWhrRoutSummaryPrint = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            ElseIf chkSaleInvoiceWise.Checked Then
                strWhrClause = " and convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            ElseIf chkSummary.Checked Then
                strWhrClause = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            Else
                strWhrClause = " and convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            End If
            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                strWhrClause += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + ss + ")  "
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 AndAlso chkSummary.Checked = False Then
                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_LOCATION_MASTER.Location_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If
            If txtLorry.arrValueMember IsNot Nothing AndAlso txtLorry.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLorry.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + sss + ")  "
                strWhrClause2 += " and TSPL_VEHICLE_MASTER.Vehicle_Id in (" + ss + ")  "
            End If
            If chkBookingWise.Checked = True Or chkRouteBoothWise.Checked = True Then
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_DETAIL.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Route_No in (" + ss + ")  "
                End If
                If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_DETAIL.Unit_code in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Unit_code in (" + ss + ")  "
                End If
                If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
                End If
            ElseIf chkSummary.Checked = True Then
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_DETAIL.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Route_No in (" + ss + ")  "
                End If
                If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(txtBookingType.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_BOOKING_MATSER.Booking_Type in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_BOOKING_MATSER.Booking_Type in (" + ss + ")  "
                End If
            ElseIf chkSaleInvoiceWise.Checked = True Then
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + ss + ")  "
                End If
                If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code in (" + ss + ")  "
                End If
                If clsCommon.CompairString(ddlInvocieType.SelectedValue, "Taxable") = CompairStringResult.Equal Then
                    strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =1  "
                ElseIf clsCommon.CompairString(ddlInvocieType.SelectedValue, "Non Taxable") = CompairStringResult.Equal Then
                    strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =0  "
                End If
            Else
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + ss + ")  "
                End If
                If TxtUOM.arrValueMember IsNot Nothing AndAlso TxtUOM.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtUOM.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code in (" + ss + ")  "
                End If
            End If
            If chkBookingWise.Checked = True AndAlso chkGatePass.Checked = True Then
                strWhrClause += " and TSPL_BOOKING_MATSER.AgainstGatePass=1 "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.AgainstGatePass=1 "
            End If
            If chkSummary.Checked = True Then
                Dim qry As String = " Select Final.Document_No as [Document No], max(Final.Document_Date) as [Document Date],max([Dispatch No(NT)]) as [Dispatch No(NT)] ,max ([Invoice No(NT)]) as [Invoice No(NT)],max([Dispatch No(T)]) as [Dispatch No(T)] ,max ([Invoice No(T)]) as [Invoice No(T)],max(Case when   convert (decimal(18,2),final.Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],max(Final.Booking_Type) as [Booking Type],Case when max(Final.Cust_Group_Code) in ('Distributor','Other') Or max(AgainstGatePass) =1 then 'NA' else case when  max(TruckSheetGenerate) =1 then 'Yes' else 'No' end end as TruckSheetGenerate, Case when  max(AgainstGatePass) =1 then 'Yes' else '' end as AgainstGatePass,Case when max(is_Cancelled) =1 then 'Yes' else '' end Cancelled,max(Final.Payment_Mode) as [Payment Mode],max(Final.location_code) as [Location Code],max(Final.Location_Desc) as [Location Desc] ,max(Final.[Customer Code]) as [Customer Code],max(Final.WdName) as [Customer Name],max(Final.Booth) as [Booth Name],max(Final.[Customer Category Code]) as [Customer Category Code],max(Final.Cust_Group_Code) as [Cust Group Code],max(Final.[Cust Group Desc]) as [Cust Group Desc],max(Final.[Route No]) as [Route No],max(Final.Route_Desc) as [Route Desc] ,max(Final.Zone_Code) as [Zone Code],max( Final.Zone_Desc) as [Zone Desc],max([VEHICLE NO]) as [VEHICLE NO],max(Final.Created_By) as [Created By],max( Final.Created_Date) as [Created Date],max(Final.Modified_By) as [Modified By],max(Final.Modified_Date) as [Modified Date] ,max(Final.DocumentAmount) as [Amount] , max(Posted) as [Status] From ( " &
                " Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName,TSPL_CUSTOMER_MASTER.OldName as [Booth] ,TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No],TSPL_ROUTE_MASTER.Route_Desc , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code],TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_ZONE_MASTER.Description as Zone_Desc,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],Case when TSPL_BOOKING_MATSER.Posted = 1 then 'Posted' else 'Not Posted' end [Posted] From TSPL_BOOKING_DETAIL " &
                " Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " &
                " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code " &
                " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                " Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code " &
                " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code " &
                " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                " Left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_BOOKING_DETAIL.route_no " &
                " Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " &
                " Left Outer Join ( " &
                " Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE  inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code  inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code  where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No    " &
                "  ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                " Left Outer Join ( " &
                " Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No   " &
                "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                " where 2=2 " + strWhrClause2 + " " &
                " ) Final Group By Final.Document_No "
                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(qry)
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    Gv1.DataSource = dtgv
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.BestFitColumns()
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    ReStoreGridLayout()
                End If
            Else
                'sanjay BHA/24/08/18-000480 Show only item which is in transaction
                Dim ItemInUse As String = ""
                If chkBookingWise.Checked = True Or chkFirstAndSecondSpell.Checked = True Or chkFirstAndSecondSpellAbstract.Checked = True Or chkRouteBoothWise.Checked = True Then
                    ItemInUse = " TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                    If isSchemeItem = False Then
                        ItemInUse += " and Scheme_Item='N' "
                    End If
                    If chkFirstAndSecondSpell.Checked = True Or chkFirstAndSecondSpellAbstract.Checked = True Then
                        ItemInUse += " and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH' "
                    End If
                    If chkMilkPouch.Checked = True Then
                        ItemInUse += "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =1 "
                    ElseIf chkProduct.Checked = True Then
                        ItemInUse += "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =0 "
                    End If
                    ItemInUse += strWhrClause2
                    If chkMilkPouch.Checked = False AndAlso chkProduct.Checked = False Then
                        ItemInUse += " order by Alies_Name "
                    End If
                ElseIf chkSaleInvoiceWise.Checked = True Then
                    ItemInUse = " TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                    If isSchemeItem = False Then
                        ItemInUse += " and Scheme_Item='N' "
                    End If
                    ItemInUse += strWhrClause2
                    ItemInUse += " order by Alies_Name "
                Else
                    ItemInUse = " TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                    If isSchemeItem = False Then
                        ItemInUse += " and Scheme_Item='N' "
                    End If
                    ItemInUse += strWhrClause2
                    ItemInUse += " order by Alies_Name "
                End If
                Dim strAliasCol As String = "( TSPL_ITEM_MASTER.Alies_Name )"
                If ChkDayWiseSummary.Checked Then
                    strAliasCol = "( case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_ITEM_MASTER.Alies_Name else 'BI Products' end )"
                End If
                Dim strSchemeItem As String = Nothing
                strSchemeItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol + "+'(S)') as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                If String.IsNullOrEmpty(strSchemeItem) Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                Dim strItem As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strItem2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strItmeHeadingScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME( " + strAliasCol + ") +' as ' + QUOTENAME( " + strAliasCol + "+'(S)')  as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strSumItemOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strSumItemSchemeOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + "+'(S)') +',0))' +' as ' + QUOTENAME( " + strAliasCol + "+'(S)') as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strGrandTotal As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( " + strAliasCol + "+'(S)') +',0))'  as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strGrandTotalWithoutScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))'  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                Dim strItem2WithSum As String = ""
                If chkMilkPouch.Checked = True OrElse chkProduct.Checked = True Then 'Item dispaly order as per item master seq
                    Dim TempItemInUse As String = ItemInUse + " group by TSPL_ITEM_MASTER.Alies_Name,TSPL_ITEM_MASTER.Sku_Seq order by TSPL_ITEM_MASTER.Sku_Seq "
                    'strItem2WithSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + TempItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                    strItem2WithSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT ',' +'Sum(' + QUOTENAME( " + strAliasCol + ") +')' + ' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + TempItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                Else
                    strItem2WithSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                End If
                Dim query As String = ""
                If isSchemeItem = False Then
                    If ChkDayWiseSummary.Checked Then
                        strItem2WithSum = strItem2WithSum.Replace("Sum(isnull([BI Products],0)) as [BI Products],", "")
                        Dim strTotal As String = strGrandTotalWithoutScheme.Replace("Sum(isnull([BI Products],0))+", "")
                        If chkBookingWise.Checked Then
                            MainQuery = "select convert(varchar, Document_Date ,103) as [Document Date],Document_Date as ddFilter, * from ( select    convert(date, zpivot.Document_Date,103) as Document_Date, " + strItem2WithSum + " ,(" + strTotal + ") as [Total],Sum(isnull([BI Products],0)) as [BI Products], (" + strGrandTotalWithoutScheme + ") as [Grand Total] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,CAST(sum(QtyLtr) as decimal(18,2)) as qty,sum(QtyLtr) as QtyLtr from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_ITEM_MASTER.Alies_Name else 'BI Products' end As [Description] " &
                                        ",TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                      " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                      " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                      "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                      "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                      "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                      " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                      " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                      "  where 2=2 " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by convert(date, zpivot.Document_Date,103)  )x order by ddFilter"
                        ElseIf chkSaleInvoiceWise.Checked Then
                            MainQuery = "select convert(varchar, Document_Date ,103) as [Document Date],Document_Date as ddFilter, * from ( select    convert(date, zpivot.Document_Date,103) as Document_Date, " + strItem2WithSum + " ,(" + strTotal + ") as [Total],Sum(isnull([BI Products],0)) as [BI Products], (" + strGrandTotalWithoutScheme + ") as [Grand Total] from  (select zzz.Document_Date,zzz.Description,CAST(sum(QtyLtr) as decimal(18,2)) as qty from (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Document_No,Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No] ,case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_ITEM_MASTER.Alies_Name else 'BI Products' end As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_SD_SALE_INVOICE_DETAIL.Qty  as Qty, TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],(CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Qty=0 THEN 0 ELSE (TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr  From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' " &
                                        " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  where 2=2 " + strWhrClause2 + " )zzz where 1=1 group by   zzz.Document_Date,zzz.Description  ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date )x order by ddFilter "
                        End If
                    ElseIf chkBookingWise.Checked = True Then
                        Dim strCreateConv As String = ""
                        Dim strIsMilkPouch As String = ""
                        If chkMilkPouch.Checked = True Then
                            If rdbLtr.Checked = True Then
                                strCreateConv = " convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) "
                            Else
                                strCreateConv = " convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILCREATE.Conversion_Factor,0)) "
                            End If
                            strIsMilkPouch = "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =1 "
                        ElseIf chkProduct.Checked = True Then
                            strCreateConv = " TSPL_BOOKING_DETAIL.Booking_Qty "
                            strIsMilkPouch = "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =0 "
                        Else
                            strCreateConv = " TSPL_BOOKING_DETAIL.Booking_Qty "
                        End If
                        'MainQuery = " select  Document_No as [Document No],max(Document_Date) as [Document Date],max([Time])as [Time],[DO No],[Short Close],max ([Dispatch No(NT)]) as [Dispatch No(NT)] ,max( [Invoice No(NT)]) as [Invoice No(NT)],max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)], max(Booking_Type) as [Booking Type], max(BookingThrough) as [Booking Through], Case when [Group] in ('Distributor','Other') Or max(AgainstGatePass) =1 then 'NA' else case when  max(TruckSheetGenerate) =1 then 'Yes' else 'No' end end as TruckSheetGenerate, Case when  max(AgainstGatePass) =1 then 'Yes' else '' end as AgainstGatePass,Case when max(is_Cancelled) =1 then 'Yes' else '' end Cancelled,max(Payment_Mode) as [Payment Mode],[VEHICLE NO],max([Customer Code]) as [Customer Code],[WdName] as [Customer Name],max(Booth) as Booth,[Group],[Cust Group Desc],[Customer Category Code],[Zone],[Route No], max([Booking Time(AM/PM)]) as [Booking Time(AM/PM)],Max(Created_By) as [Created By],max(Created_Date) as [Created Date],max(Modified_By) as [Modified By],max(Modified_Date) as [Modified Date],sum(DocumentAmount) as [Amount], max(isnull (Scheme_Booking_Qty,0)) as [Scheme Qty] , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Time]) as [Time]  ,zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,sum(Amount_with_Tax) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                        '          " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                        '          " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                        '          "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                        '          "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                        '          "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                        '          " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                        '          " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                        '          "  where 2=2  " + strIsMilkPouch + "  " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_No,zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.[DO NO],zpivot.[Short Close] "

                        If rbtnDateWise.Checked = True Then
                            MainQuery = " Select  max(Document_No) As [Document No], (Document_Date) As [Document Date], max([Time]) As [Time], max([DO No])[DO No], max([Short Close])[Short Close],
                            max(Booking_Type) As [Booking Type], max(BookingThrough) As [Booking Through], Case When max([Group]) In ('Distributor', 'Other') Or max(AgainstGatePass) = 1 then 'NA' else case when max(TruckSheetGenerate) = 1 then 'Yes' else 'No' end end as TruckSheetGenerate, 
                            Case when max(AgainstGatePass) = 1 then 'Yes' else '' end as AgainstGatePass, Case when max(is_Cancelled) = 1 then 'Yes' else '' end Cancelled, 
                            max(Payment_Mode) As [Payment Mode], max([VEHICLE NO]), max([Customer Code]) As [Customer Code], max([WdName]) As [Customer Name], max(Booth) As Booth, 
                            max([Group]), max([Cust Group Desc]), max([Customer Category Code]), max([Zone]), max([Route No]), max( [Booking Time(AM / PM) ] ) As [Booking Time(AM / PM) ],
                            Max(Created_By) As [Created By], max(Created_Date) As [Created Date], max(Modified_By) As [Modified By], max(Modified_Date) As [Modified Date], 
                            sum(DocumentAmount) As [Amount], max( isnull (Scheme_Booking_Qty, 0) ) As [Scheme Qty]," + strItem2WithSum + " ,(" + strGrandTotalWithoutScheme + ") As [Grand Total] , 
                            cast(SUM(QtyLtr) As Decimal(18, 2) ) As [Total In Ltr]
                            from ( select max(zzz.[VEHICLE NO])[VEHICLE NO], max(zzz.[WdName])[WdName],  max(zzz.Cust_Group_Code) as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],
                            max([Customer Category Code]) As [Customer Category Code],max(zzz.Zone_Code) As [Zone], max(zzz.[Route No])[Route No], max(zzz.[DO No])[DO No],  
                            max(zzz.[Short Close])[Short Close], max(zzz.item_code) As item_code,  max(zzz.[Document_No])[Document_No], (Document_Date) As Document_Date, 
                            max([Time]) As [Time], max(Scheme_Booking_Qty) As Scheme_Booking_Qty, max(Booking_Type) As Booking_Type, max(BookingThrough) As [BookingThrough],
                            max(TruckSheetGenerate) As TruckSheetGenerate, max(AgainstGatePass) As AgainstGatePass, max(is_Cancelled) As is_Cancelled, max(Payment_Mode) As Payment_Mode, 
                            max(GatePass_Type) As [Booking Time(AM / PM) ], Max(Created_By) As Created_By, max(Created_Date) As Created_Date, max(Modified_By) As Modified_By,
                            max(Modified_Date) As Modified_Date, sum(Amount_with_Tax) As DocumentAmount, max(Booth) As [Booth],  max([Customer Code]) As [Customer Code], 
                            (zzz.Description)Description, sum(qty) as qty, sum(QtyLtr) as QtyLtr from "
                        Else
                            MainQuery = " select Document_No as [Document No], max(Document_Date) as [Document Date], max([Time]) as [Time], [DO No], [Short Close], max(Booking_Type) as [Booking Type], max(BookingThrough) as [Booking Through], Case when [Group] in ('Distributor', 'Other') Or max(AgainstGatePass) = 1 then 'NA' else case when max(TruckSheetGenerate) = 1 then 'Yes' else 'No' end end as TruckSheetGenerate, Case when max(AgainstGatePass) = 1 then 'Yes' else '' end as AgainstGatePass, Case when max(is_Cancelled) = 1 then 'Yes' else '' end Cancelled, max(Payment_Mode) as [Payment Mode], [VEHICLE NO], max([Customer Code]) as [Customer Code], [WdName] as [Customer Name], max(Booth) as Booth, [Group], [Cust Group Desc], [Customer Category Code], [Zone], [Route No], max( [Booking Time(AM / PM) ] ) as [Booking Time(AM / PM) ], Max(Created_By) as [Created By], max(Created_Date) as [Created Date], max(Modified_By) as [Modified By], max(Modified_Date) as [Modified Date], sum(DocumentAmount) as [Amount], max( isnull (Scheme_Booking_Qty, 0) ) as [Scheme Qty]," + strItem2WithSum + " ,(" + strGrandTotalWithoutScheme + ") as [Grand Total] , cast( SUM(QtyLtr) as decimal(18, 2) ) as [Total In Ltr] from ( select max(zzz.item_code) as item_code, zzz.Document_No, max(Document_Date) as Document_Date, max([Time]) as [Time], zzz.[DO No], zzz.[Short Close], max(Scheme_Booking_Qty) as Scheme_Booking_Qty, max(Booking_Type) as Booking_Type, max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass, max(is_Cancelled) as is_Cancelled, max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM / PM) ], Max(Created_By) as Created_By, max(Created_Date) as Created_Date, max(Modified_By) as Modified_By, max(Modified_Date) as Modified_Date, sum(Amount_with_Tax) as DocumentAmount, max(Booth) as [Booth], max([Customer Category Code]) as [Customer Category Code], max([Customer Code]) as [Customer Code], zzz.[VEHICLE NO], zzz.[WdName], zzz.Description, zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc], zzz.Zone_Code as [Zone], zzz.[Route No], sum(qty) as qty, sum(QtyLtr) as QtyLtr from "
                        End If
                        '                  If rbtnDateWise.Checked = True Then
                        '                      MainQuery = "group by zzz.Document_Date,zzz.Description	) as s pivot ( sum(Qty) for Description in ([CHAI SATHI 1 LTR],[DAHI 200],[DTM 200],[DTM 500],[DTM 6L],[FCM 500 ML],[FCM 6 LTR],[NGHEE 15 KG],[P-CHAACH 500],[PNR 200],[SKM 6L],[SM 500],[TM 1L],[TM 500],[TM 6L] ) ) as zpivot group by zpivot.Document_Date
                        '"
                        '                  End If

                        MainQuery += " ( Select isnull( TSPL_BOOKING_MATSER.GatePass_Type, '' ) as GatePass_Type, TSPL_BOOKING_MATSER.Document_No, Convert (date,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,case when LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100 ), 7 ) ) = '12:00AM' then LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_BOOKING_MATSER.Created_Date, 100 ), 7 ) ) else LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100 ), 7 ) ) end as [Time], TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No], isnull( TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close, 'N' ) as [Short Close], TSPL_BOOKING_MATSER.Booking_Type, TSPL_BOOKING_MATSER.BookingThrough, Convert ( varchar, TSPL_BOOKING_MATSER.TruckSheetGenerate ) as TruckSheetGenerate, Convert ( varchar, TSPL_BOOKING_MATSER.AgainstGatePass ) as AgainstGatePass, Convert ( varchar, TSPL_BOOKING_MATSER.is_Cancelled ) as is_Cancelled, TSPL_BOOKING_MATSER.Payment_Mode, ( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH' ) + '.' + format ( TSPL_BOOKING_MATSER.Created_Date, 'mm' ) ) as Created_Date_Time, TSPL_BOOKING_MATSER.Created_By, convert ( varchar, TSPL_BOOKING_MATSER.Created_Date, 103 ) as Created_Date, TSPL_BOOKING_MATSER.Modified_By, Convert ( varchar, TSPL_BOOKING_MATSER.Modified_Date, 103 ) as Modified_Date, TSPL_BOOKING_DETAIL.Amount_with_Tax, TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, isnull( TSPL_CUSTOMER_MASTER.cust_category_code, '' ) as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_BOOKING_DETAIL.Unit_code as UOM, TSPL_BOOKING_DETAIL.route_no as [Route No], TSPL_ITEM_MASTER.Alies_Name As [Description], TSPL_VEHICLE_MASTER.Description [Lorry_No], TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_MASTER.Zone_Code, IsNull( TSPL_VEHICLE_MASTER.Description, '''' ) As [VEHICLE NO], convert ( decimal(18, 2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor, 0 ) ) as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth, TBL_SCHEME_VALUE.Scheme_Booking_Qty, ( CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty = 0 THEN 0 ELSE ( TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor )/ coalesce( TSPL_ITEM_UOM_DETAILltr.Conversion_Factor, TSPL_ITEM_UOM_DETAILKG.Conversion_Factor ) END ) AS QtyLtr From
                                     TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                    " left Outer Join ( Select TSPL_BOOKING_DETAIL.Document_No, Sum ( isnull(Booking_Qty, 0) ) as Scheme_Booking_Qty from TSPL_BOOKING_DETAIL where Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE On TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No 
                                    left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code =" + IIf(rbtnAsPerBooking.Checked, "TSPL_BOOKING_DETAIL.Unit_Code", IIf(rdbCreate.Checked, "'CRATE'", "'LTR'")) + " 
                                    left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code = 'KG' 
                                    --left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code = 'CRATE' 
                                    left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAILUOM.UOM_Code
                        where 2 = 2 " + strIsMilkPouch + "  " + strWhrClause2 + " ) zzz where zzz.Scheme_Item = 'N' "

                        If rbtnDateWise.Checked = True Then
                            MainQuery += " group by zzz.Document_Date,zzz.Description	) as s pivot ( sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date "
                        Else
                            MainQuery += "group by zzz.Document_No, zzz.[VEHICLE NO], zzz.WdName, zzz.Description, zzz.[Customer Category Code], zzz.Cust_Group_Code, zzz.Zone_Code, zzz.[Route No], zzz.[DO NO], zzz.[Short Close] ) as s pivot ( sum(Qty) for Description in (" + strItem2 + " ) ) as zpivot group by zpivot.Document_No, zpivot.[VEHICLE NO], zpivot.[WdName], zpivot.[Group], zpivot.[Cust Group Desc], zpivot.[Customer Category Code], zpivot.[Zone], zpivot.[Route No], zpivot.[DO NO], zpivot.[Short Close]"
                        End If

                        'MainQuery += " group by zzz.Document_No, zzz.[VEHICLE NO], zzz.WdName, zzz.Description, zzz.[Customer Category Code], zzz.Cust_Group_Code, zzz.Zone_Code, zzz.[Route No], zzz.[DO NO], zzz.[Short Close] ) as s pivot ( sum(Qty) for Description in (" + strItem2 + " ) ) as zpivot group by zpivot.Document_No, zpivot.[VEHICLE NO], zpivot.[WdName], zpivot.[Group], zpivot.[Cust Group Desc], zpivot.[Customer Category Code], zpivot.[Zone], zpivot.[Route No], zpivot.[DO NO], zpivot.[Short Close]"
                        If chkMilkPouch.Checked = True OrElse chkProduct.Checked = True Then
                            'MainQuery = " select XXXFinal.[Route No],  XXXFinal.[Customer Name], " + strItem2 + " ,[Grand Total], XXXFinal.[Modified By]  from ( " + MainQuery + " ) XXXFinal "
                            If clsCommon.CompairString(cboShift.Text, "Both") = CompairStringResult.Equal Then
                                MainQuery = " select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                              ,  XXXFinal.[Customer Name] asc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], " + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total], max( XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Created By]  from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No]	 group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name] "
                            ElseIf clsCommon.CompairString(cboShift.Text, "Morning") = CompairStringResult.Equal Then
                                MainQuery = " select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                              ,  XXXFinal.[Customer Name] asc, [Document Date] desc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], [Document Date] ,max([Time]) as [Time],case when XXXFinal.[Booking Time(AM / PM) ]  = 'AM' then 'Morning' else 'Evening' end as Shift  ," + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total], max(XXXFinal.[Modified By]) as  [Modified By], max(XXXFinal.[Created By]) as  [Created By] from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No] where XXXFinal.[Booking Time(AM / PM) ] = 'AM' group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name],[Document Date] ,XXXFinal.[Booking Time(AM / PM) ] "
                            ElseIf clsCommon.CompairString(cboShift.Text, "Evening") = CompairStringResult.Equal Then
                                MainQuery = " select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                              ,  XXXFinal.[Customer Name] asc, [Document Date] desc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name],[Document Date],max([Time]) as [Time] ,case when XXXFinal.[Booking Time(AM / PM) ] = 'AM' then 'Morning' else 'Evening' end as Shift  ," + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total] , max(XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Created By] from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No] where XXXFinal.[Booking Time(AM / PM) ] = 'PM' group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name],[Document Date],XXXFinal.[Booking Time(AM / PM) ] "
                            ElseIf clsCommon.CompairString(cboShift.Text, "Shift Wise") = CompairStringResult.Equal Then
                                MainQuery = " select  XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name],[Document Date], case when XXXFinal.[Booking Time(AM / PM) ] = 'AM' then 'Morning' else 'Evening' end as Shift  ,max([Time]) as [Time] ," + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total] , max(XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Created By] from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No] where XXXFinal.[Booking Time(AM / PM) ] in ('PM', 'AM') group by  XXXFinal.[Route No],  XXXFinal.[Customer Name],[Document Date],XXXFinal.[Booking Time(AM / PM) ] "
                            End If
                        End If
                        If chkRouteSummary.Checked = True Then
                            'MainQuery = " select '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' as FromDate, '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' as ToDate,max(TSPL_ROUTE_MASTER.City_Code) as [Route_CityCode], max( TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, max(TSPL_COMPANY_MASTER.Add1) as [Comp_Add1] , max(TSPL_COMPANY_MASTER.Add2) as [Comp_Add2] , max(TSPL_COMPANY_MASTER.Add3) as Comp_Add3 , max(TSPL_COMPANY_MASTER.Phone1) as Comp_Phone , max(TSPL_COMPANY_MASTER.Phone2) as Comp_Phone2, max(TSPL_COMPANY_MASTER.Fax) as Comp_Fax , max(TSPL_COMPANY_MASTER.Email) as CompEmail , max(TSPL_COMPANY_MASTER.State) as Comp_StateCode ,max(TSPL_STATE_MASTER_Comp.STATE_NAME) as Comp_STATE_NAME, max (TSPL_COMPANY_MASTER.Pincode) as Comp_Pincode,
                            '              XXXFinal.[Route No], XXXFinal.[Customer Code], max(XXXFinal.[Customer Name]) as [Customer Name], XXXFinal.item_code, max(XXXFinal.Description) as [Item Name], sum(qty) as qty , sum(QtyNotMilkPouch) as QtyNotMilkPouch, sum(XXXFinal.MilkAmt) as  MilkAmt, sum (XXXFinal.ProductAmt) as ProductAmt ,sum(XXXFinal.MilkAmt) + sum(XXXFinal.ProductAmt) as TotalAmt , max( XXXFinal.Comp_Code) as Comp_Code   from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], 
                            '              ([Customer Code]) as  [Customer Code]
                            '              ,zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr, sum (QtyNotMilkPouch) as QtyNotMilkPouch ,sum( MilkAmt) as MilkAmt , sum (ProductAmt) as ProductAmt, max([Customer Name]) as [Customer Name], max(Comp_Code) as Comp_Code from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO],
                            '              case when TSPL_ITEM_MASTER.Is_Milk_Pouch =1 then convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) else 0 end as Qty, case when TSPL_ITEM_MASTER.Is_Milk_Pouch =0 then convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) else 0 end as QtyNotMilkPouch, case when TSPL_ITEM_MASTER.Is_Milk_Pouch =1 then TSPL_BOOKING_DETAIL.Amount_with_Tax else 0 end as MilkAmt,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name]
                            '              ,case when TSPL_ITEM_MASTER.Is_Milk_Pouch =0 then TSPL_BOOKING_DETAIL.Amount_with_Tax else 0 end as ProductAmt,
                            '              TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr,
                            '              TSPL_BOOKING_MATSER.Comp_Code  
                            '              From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No  ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No   Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No   ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where   Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code   where 2=2  
                            '              " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close]
                            '              ,zzz.[Customer Code]
                            '              )XXXFinal  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = XXXFinal.Comp_Code
                            '              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No]
                            '              left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Comp on TSPL_STATE_MASTER_Comp.STATE_CODE = TSPL_COMPANY_MASTER.State  
                            '              group by XXXFinal.[Route No] , [Customer Code],item_code "
                            MainQuery = " select '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' as FromDate, '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' as ToDate, max(XXXXXFinal.SNO) as SNO, route_no, [Cust_Code],max(Customer_Name) as Customer_Name,max(Credit_Customer) as Credit_Customer,max(ItemNamePart1) as ItemNamePart1,max(ItemNamePart2) as ItemNamePart2, sum(isnull([Item1],0)) as [Item1] ,sum(isnull([Item2],0)) as [Item2],sum(isnull([Item3],0)) as [Item3],sum(isnull([Item4],0)) as [Item4],sum(isnull([Item5],0)) as [Item5],sum(isnull([Item6],0)) as [Item6],sum(isnull([Item7],0)) as [Item7],sum(isnull([Item8],0)) as [Item8],sum(isnull([Item9],0)) as [Item9],sum(isnull([Item10],0)) as [Item10],sum(isnull([Item11],0)) as [Item11],sum(isnull([Item12],0)) as [Item12], sum (isnull(QtyNotMilkPouch,0)) as QtyNotMilkPouch ,sum(isnull(MilkAmt,0)) as MilkAmt, sum(isnull(ProductAmt,0)) as ProductAmt,
                                            sum(isnull(MilkAmt,0)) + sum(isnull(ProductAmt,0))  as TotalMilkAmt,
                                            sum(isnull(QtyLtr,0)) as QtyLtr ,
                                            max( [Route_CityCode]) as [Route_CityCode], max(Comp_Name) as Comp_Name, max([Comp_Add1]) as [Comp_Add1] , max([Comp_Add2]) as [Comp_Add2] , max(Comp_Add3) as Comp_Add3 , max(Comp_Phone) as Comp_Phone , max(Comp_Phone2) as Comp_Phone2, max(Comp_Fax) as Comp_Fax , max(CompEmail) as CompEmail , max(Comp_StateCode) as Comp_StateCode ,max(Comp_STATE_NAME) as Comp_STATE_NAME 
                                            , max(P1Item1) as P1Item1, max(P1Item2) as P1Item2, max(P1Item3) as P1Item3, max(P1Item4) as P1Item4, max(P1Item5) as P1Item5, max(P1Item6) as P1Item6, max(P1Item7) as P1Item7, max(P1Item8) as P1Item8, max(P1Item9) as P1Item9, max(P1Item10) as P1Item10, max(P1Item11) as P1Item11, max(P1Item12 ) as P1Item12
                                             , max(P2Item1) as P2Item1, max(P2Item2) as P2Item2, max(P2Item3) as P2Item3, max(P2Item4) as P2Item4, max(P2Item5) as P2Item5, max(P2Item6) as P2Item6, max(P2Item7) as P2Item7, max(P2Item8) as P2Item8, max(P2Item9) as P2Item9, max(P2Item10) as P2Item10, max(P2Item11) as P2Item11, max(P2Item12 ) as P2Item12 , max(Transporter_Name) as Transporter_Name, max(Comp_Pincode) as Comp_Pincode
                                            from (
                                            select 1 as SNO, route_no, [Cust_Code],(Customer_Name) as Customer_Name,Credit_Customer,ItemNamePart1,ItemNamePart2, (isnull([Item1],0)) as [Item1] ,(isnull([Item2],0)) as [Item2],(isnull([Item3],0)) as [Item3],(isnull([Item4],0)) as [Item4],(isnull([Item5],0)) as [Item5],(isnull([Item6],0)) as [Item6],(isnull([Item7],0)) as [Item7],(isnull([Item8],0)) as [Item8],(isnull([Item9],0)) as [Item9],(isnull([Item10],0)) as [Item10],(isnull([Item11],0)) as [Item11],(isnull([Item12],0)) as [Item12], isnull(QtyNotMilkPouch,0) as QtyNotMilkPouch ,isnull(MilkAmt,0) as MilkAmt, isnull(ProductAmt,0) as ProductAmt,  isnull(QtyLtr,0) as QtyLtr ,
                                            ( [Route_CityCode]) as [Route_CityCode], (Comp_Name) as Comp_Name, ([Comp_Add1]) as [Comp_Add1] , ([Comp_Add2]) as [Comp_Add2] , (Comp_Add3) as Comp_Add3 , (Comp_Phone) as Comp_Phone , (Comp_Phone2) as Comp_Phone2, (Comp_Fax) as Comp_Fax , (CompEmail) as CompEmail , (Comp_StateCode) as Comp_StateCode ,(Comp_STATE_NAME) as Comp_STATE_NAME,item_code,Transporter_Name,Comp_Pincode
                                            from (
                                            select max(Final.Row_Number) as ItemNo, Final.Item_Code , max(Final.Alies_Name) as Alies_Name, Final.Cust_Code, max(Final.Customer_Name) as Customer_Name, sum(Qty) as Qty ,sum(QtyNotMilkPouch) as QtyNotMilkPouch ,sum(MilkAmt) as MilkAmt, sum(ProductAmt) as ProductAmt, max([Order Date]) as [Order Date],sum(Final.QtyLtr) as QtyLtr,max(Final. [Route_CityCode]) as [Route_CityCode], max(Final.Comp_Name) as Comp_Name, max(Final.[Comp_Add1]) as [Comp_Add1] , max(Final.[Comp_Add2] ) as [Comp_Add2] , max(Final. Comp_Add3) as Comp_Add3 , max(Final.Comp_Phone) as Comp_Phone , max(Final.Comp_Phone2) as Comp_Phone2, max(Final.Comp_Fax) as Comp_Fax , max(Final.CompEmail) as CompEmail , max(Final.Comp_StateCode) as Comp_StateCode ,max(Final.Comp_STATE_NAME) as Comp_STATE_NAME,  max(Final.Comp_Pincode) as Comp_Pincode,max(Final.route_no) as route_no, max(Credit_Customer) as Credit_Customer,max(ItemNamePart1) as ItemNamePart1, max(ItemNamePart2) as ItemNamePart2, max(Transporter_Name) as Transporter_Name from (
                                            select TBL_ITEM12.Row_Number, TSPL_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Alies_Name,
                                            TSPL_BOOKING_DETAIL.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, case when TSPL_ITEM_MASTER.Is_Milk_Pouch =1 then convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) else 0 end as Qty, case when TSPL_ITEM_MASTER.Is_Milk_Pouch =0 then convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) else 0 end as QtyNotMilkPouch, case when TSPL_ITEM_MASTER.Is_Milk_Pouch =1 then TSPL_BOOKING_DETAIL.Amount_with_Tax else 0 end as MilkAmt,case when TSPL_ITEM_MASTER.Is_Milk_Pouch =0 then TSPL_BOOKING_DETAIL.Amount_with_Tax else 0 end as ProductAmt,
                                                                                      TSPL_BOOKING_MATSER.Document_Date As [Order Date],(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr, (TSPL_ROUTE_MASTER.City_Code) as [Route_CityCode], ( TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, (TSPL_COMPANY_MASTER.Add1) as [Comp_Add1] , (TSPL_COMPANY_MASTER.Add2) as [Comp_Add2] , (TSPL_COMPANY_MASTER.Add3) as Comp_Add3 , (TSPL_COMPANY_MASTER.Phone1) as Comp_Phone , (TSPL_COMPANY_MASTER.Phone2) as Comp_Phone2, (TSPL_COMPANY_MASTER.Fax) as Comp_Fax , (TSPL_COMPANY_MASTER.Email) as CompEmail , (TSPL_COMPANY_MASTER.State) as Comp_StateCode ,(TSPL_STATE_MASTER_Comp.STATE_NAME) as Comp_STATE_NAME,  (TSPL_COMPANY_MASTER.Pincode) as Comp_Pincode,
                                                                                      TSPL_BOOKING_DETAIL.route_no , isnull(TSPL_CUSTOMER_MASTER.Credit_Customer,'N') as Credit_Customer
										                                              ,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', 
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS ItemNamePart2
                                            ,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name,0, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', 
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+1) AS ItemNamePart1 , tspl_transport_master.Transporter_Name
                                            from TSPL_BOOKING_DETAIL 
                                            left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No =  TSPL_BOOKING_MATSER.Document_No 
                                            inner Join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code 
                                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code 
                                            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_BOOKING_MATSER.Comp_Code
                                                                                      left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
                                                                                      left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Comp on TSPL_STATE_MASTER_Comp.STATE_CODE = TSPL_COMPANY_MASTER.State 
										                                              left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
										                                              left outer join tspl_transport_master on tspl_transport_master.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id
                                                                                      left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  
                                                                                      left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code = TSPL_LOCATION_MASTER.Location_Code   
                                            left outer join (select 'Item'+Convert(varchar(10), ROW_NUMBER() OVER(ORDER BY TSPL_ITEM_MASTER.Sku_Seq,TSPL_ITEM_MASTER.Item_Code)) AS Row_Number, TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.Sku_Seq from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code in (select TSPL_BOOKING_DETAIL.Item_Code from TSPL_BOOKING_DETAIL  left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No =  TSPL_BOOKING_MATSER.Document_No 
                                                              inner Join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
                                                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
                                                              left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
                                                              left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code = TSPL_LOCATION_MASTER.Location_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code where 2=2 and TSPL_ITEM_MASTER.Is_Milk_Pouch = 1 " + strWhrClause2 + " )  ) TBL_ITEM12 on TBL_ITEM12.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
                                            where 2=2 and TSPL_ITEM_MASTER.Is_Milk_Pouch = 1 " + strWhrClause2 + "
                                            ) Final group by Final.[route_no] , Final.[Cust_Code],Final.item_code 
                                            ) XXXFinal 
                                            pivot ( sum(XXXFinal.Qty) for ItemNo in ([Item1],[Item2],[Item3],[Item4],[Item5],[Item6],[Item7],[Item8],[Item9],[Item10],[Item11],[Item12]) ) QtyPivot 
                                             ) XXXXXFinal 
                                             left outer join (select 1 as SNO, max(isnull(P1Item1,'')) as P1Item1, max(isnull(P1Item2,'')) as P1Item2 , max(isnull(P1Item3,'')) as P1Item3, max(isnull(P1Item4,'')) as P1Item4, max(isnull(P1Item5,'')) as P1Item5,max(isnull(P1Item6,'')) as P1Item6, max(isnull(P1Item7,'')) as P1Item7 , max(isnull(P1Item8,'')) as P1Item8, max(isnull(P1Item9,'')) as P1Item9, max(isnull(P1Item10,'')) as P1Item10 , max(isnull(P1Item11,'')) as P1Item11, max(isnull(P1Item12,'')) as P1Item12 ,
                                               max(isnull(P2Item1,'')) as P2Item1, max(isnull(P2Item2,'')) as P2Item2 , max(isnull(P2Item3,'')) as P2Item3, max(isnull(P2Item4,'')) as P2Item4, max(isnull(P2Item5,'')) as P2Item5,max(isnull(P2Item6,'')) as P2Item6, max(isnull(P2Item7,'')) as P2Item7 , max(isnull(P2Item8,'')) as P2Item8, max(isnull(P2Item9,'')) as P2Item9, max(isnull(P2Item10,'')) as P2Item10 , max(isnull(P2Item11,'')) as P2Item11, max(isnull(P2Item12,'')) as P2Item12 
                                             from (
                                             select  Case when ROW_NUMBER = 1 then ItemNamePart1 end as P1Item1,Case when ROW_NUMBER = 2 then ItemNamePart1 end as P1Item2,Case when ROW_NUMBER = 3 then ItemNamePart1 end as P1Item3, Case when ROW_NUMBER = 4 then ItemNamePart1 end as P1Item4, Case when ROW_NUMBER = 5 then ItemNamePart1 end as P1Item5, case when ROW_NUMBER = 6 then ItemNamePart1 end as P1Item6,Case when ROW_NUMBER = 7 then ItemNamePart1 end as P1Item7,Case when ROW_NUMBER = 8 then ItemNamePart1 end as P1Item8, Case when ROW_NUMBER = 9 then ItemNamePart1 end as P1Item9, Case when ROW_NUMBER = 10 then ItemNamePart1 end as P1Item10,Case when ROW_NUMBER = 11 then ItemNamePart1 end as P1Item11, Case when ROW_NUMBER = 12 then ItemNamePart1 end as P1Item12 
                                             ,Case when ROW_NUMBER = 1 then ItemNamePart2 end as P2Item1,Case when ROW_NUMBER = 2 then ItemNamePart2 end as P2Item2,Case when ROW_NUMBER = 3 then ItemNamePart2 end as P2Item3, Case when ROW_NUMBER = 4 then ItemNamePart2 end as P2Item4, Case when ROW_NUMBER = 5 then ItemNamePart2 end as P2Item5, case when ROW_NUMBER = 6 then ItemNamePart2 end as P2Item6,Case when ROW_NUMBER = 7 then ItemNamePart2 end as P2Item7,Case when ROW_NUMBER = 8 then ItemNamePart2 end as P2Item8, Case when ROW_NUMBER = 9 then ItemNamePart2 end as P2Item9, Case when ROW_NUMBER = 10 then ItemNamePart2 end as P2Item10,Case when ROW_NUMBER = 11 then ItemNamePart2 end as P2Item11, Case when ROW_NUMBER = 12 then ItemNamePart2 end as P2Item12
                                             from (select Convert(varchar(10), ROW_NUMBER() OVER(ORDER BY TSPL_ITEM_MASTER.Sku_Seq,TSPL_ITEM_MASTER.Item_Code)) AS Row_Number, TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.Sku_Seq, SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', 
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS ItemNamePart2
                                            ,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name,0, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', 
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+1) AS ItemNamePart1 from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code in (select TSPL_BOOKING_DETAIL.Item_Code from TSPL_BOOKING_DETAIL left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No =  TSPL_BOOKING_MATSER.Document_No 
                                            inner Join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
                                            left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
                                            left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
                                            left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code = TSPL_LOCATION_MASTER.Location_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code where 2=2 and TSPL_ITEM_MASTER.Is_Milk_Pouch = 1 " + strWhrClause2 + " ) )  XX ) XXX ) TBL_ItemNamePart1 on TBL_ItemNamePart1.SNO = XXXXXFinal.SNO
                                             group by [route_no], [Cust_Code] 
                                              "
                            Dim dtPrint As DataTable = clsDBFuncationality.GetDataTable(MainQuery)
                            If dtPrint IsNot Nothing And dtPrint.Rows.Count > 0 Then
                                Dim frmCRV As New frmCrystalReportViewer()
                                frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptRouteWiseSalesSummary", "Route Summary")
                                frmCRV = Nothing
                                Return
                            Else
                                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                                Return
                            End If
                        End If
                    ElseIf chkRouteBoothWise.Checked = True Then
                        MainQuery = " select   Document_Date as [Document Date],  [Customer Code] as [Booth],[Zone],[Route No],Booking_Type as [Booking Type] , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2 " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date,zpivot.[Customer Code],zpivot.[Zone],zpivot.[Route No],zpivot.Booking_Type "
                    ElseIf chkFirstAndSecondSpell.Checked = True Then
                        MainQuery = " select [Zone], Document_Date as [Document Date], max(Booking_Type) as [Booking Type],max(Payment_Mode) as [Payment Mode],max(isnull (Scheme_Booking_Qty,0)) as [Scheme Qty],  " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,sum(AdvanceAmount) as [Total Price],sum(RemittedAmount) as [Remitted Amount],sum(AdvanceAmount)-sum(RemittedAmount) as [Difference Amount],cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(zzz.Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,max(qty) as qty,max(QtyLtr) as QtyLtr,max(AdvanceAmount) as AdvanceAmount,sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS RemittedAmount  from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr, isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2 " + strWhrClause2 + "  and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH'  )zzz " & Environment.NewLine &
                                  " LEFT OUTER JOIN TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No and isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' " & Environment.NewLine &
                                  " where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date,zpivot.[Zone] order by zpivot.Document_Date"
                    ElseIf chkFirstAndSecondSpellAbstract.Checked = True Then
                        Dim strItem2L As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol + "+'#L')  as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                        Dim strItem2WithSumL As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + "+'#L')  + ',0))' + ' as ' + QUOTENAME( " + strAliasCol + "+'#L') as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                        MainQuery = " select [Zone], Document_Date as [Document Date],  " + strItem2WithSum + "," + strItem2WithSumL + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,sum(AdvanceAmount) as [Total Price],sum(RemittedAmount) as [Remitted Amount],sum(AdvanceAmount)-sum(RemittedAmount) as [Difference Amount] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(zzz.Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,(zzz.Description+'#L') AS Description#L,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,max(qty) as qty,max(QtyLtr) as QtyLtr,max(AdvanceAmount) as AdvanceAmount,sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS RemittedAmount  from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr, isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                 "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                 " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                 " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                 "  where 2=2 " + strWhrClause2 + "  and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH'  )zzz " & Environment.NewLine &
                                 " LEFT OUTER JOIN TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No and isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' " & Environment.NewLine &
                                 " where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot " &
                                " pivot (  sum(QtyLtr) for Description#L in ( " + strItem2L + " ) ) as zpivot1 " &
                                " group by zpivot1.[Zone],zpivot1.Document_Date order by zpivot1.[Zone],Convert(date,zpivot1.Document_Date,103) "
                    ElseIf chkSaleInvoiceWise.Checked = True Then
                        MainQuery = " select  Document_No as [Document No],[VEHICLE NO],[WdName],[Group],[Cust Group Desc], [Customer Category Code],[Zone],[Route No],UOM , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total], case WHEN upper(UOM)='CAN' THEN (" + strGrandTotalWithoutScheme + ") else 0 END AS [Can total], case WHEN upper(UOM)='CRATE' THEN (" + strGrandTotalWithoutScheme + ") else 0 END AS [Crate total], case WHEN upper(UOM)='BOX' THEN (" + strGrandTotalWithoutScheme + ") else 0 END AS [Box total] from (select zzz.Document_No,zzz.[VEHICLE NO],zzz.[Customer Category Code],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone] ,zzz.UOM,zzz.[Route No] ,sum(qty) as qty from  (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Document_No,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No] ,TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_SD_SALE_INVOICE_DETAIL.Qty  as Qty, TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2 " + strWhrClause2 + " )zzz where 1=1 group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.[Customer Category Code],zzz.Zone_Code,zzz.[Route No],zzz.UOM 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_No,zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.UOM "
                    Else
                        MainQuery = " select  [VEHICLE NO],[WdName],[Group],[Cust Group Desc], [Customer Category Code],[Zone],[Route No],UOM  , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total]  from (select zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc], zzz.[Customer Category Code],zzz.Zone_Code as [Zone] ,zzz.[Route No],zzz.UOM  ,sum(qty) as qty from  (Select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No as [Route No],TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code as UOM, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No,TSPL_CUSTOMER_MASTER.Cust_Group_Code,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code],TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as Qty, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2 " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N'  group by zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.[Customer Category Code],zzz.Zone_Code,zzz.[Route No] ,zzz.UOM ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.UOM "
                    End If
                    query = MainQuery
                Else
                    Dim querySchemeItem As String = ""
                    Dim querySchemeItem2 As String = ""
                    If chkBookingWise.Checked = True OrElse chkBookingWise.Checked = True Then
                        Dim TempQry As String = " SELECT STUFF((SELECT distinct ',' + QUOTENAME(TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
                        Dim ItemDesc As String = clsDBFuncationality.getSingleValue(TempQry)
                        MainQueryForScheme = "select  QtyLtr,Document_No,[VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem + " , " + strItmeHeadingScheme + " from (select zzz.Document_No,zzz.[VEHICLE NO],zzz.WdName,zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select  TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.item_code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code where 2=2  " + strWhrClause2 + " )zzz WHERE zzz.Scheme_Item='Y' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in (" + ItemDesc + ") ) as zpivot "
                        querySchemeItem = MainQueryForScheme 'MainQuery
                        MainQueryForScheme = " select  QtyLtr,Document_No as [Document No],[VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem2 + " , " + strSchemeItem + " from (select zzz.Document_No,zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code where 2=2  " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot  "
                        querySchemeItem2 = MainQueryForScheme 'MainQuery
                        query = " Select Document_No as [Document No],[VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strSumItemOnly + ", " + strSumItemSchemeOnly + " , (" + strGrandTotal + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (  " &
                              "  " + querySchemeItem + "  " &
                              " Union All " &
                              "  " + querySchemeItem2 + " " &
                              " ) xyzp  group by xyzp.Document_No,xyzp.[VEHICLE NO],[WdName],xyzp.[Group],xyzp.[Cust Group Desc],[Customer Category Code],xyzp.[Zone]  "
                    ElseIf chkSaleInvoiceWise.Checked = True Then
                        Dim TempQry As String = " SELECT STUFF((SELECT distinct ',' + QUOTENAME(TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
                        Dim ItemDesc As String = clsDBFuncationality.getSingleValue(TempQry)
                        MainQueryForScheme = "select  Document_No,[VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem + " , " + strItmeHeadingScheme + " from (select zzz.Document_No,zzz.[VEHICLE NO],zzz.WdName,zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty from  (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Document_No,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code,TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_SD_SALE_INVOICE_DETAIL.Qty  as Qty, TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2  " + strWhrClause2 + " )zzz WHERE zzz.Scheme_Item='Y' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in (" + ItemDesc + ") ) as zpivot "
                        querySchemeItem = MainQueryForScheme
                        MainQueryForScheme = " select  Document_No as [Document No],[VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem2 + " , " + strSchemeItem + " from (select zzz.Document_No,zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty from  (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Document_No,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code ,TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_SD_SALE_INVOICE_DETAIL.Qty  as Qty, TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2  " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot  "
                        querySchemeItem2 = MainQueryForScheme
                        query = " Select Document_No as [Document No],[VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strSumItemOnly + ", " + strSumItemSchemeOnly + " , (" + strGrandTotal + ") as [Grand Total] from (  " &
                             "  " + querySchemeItem + "  " &
                             " Union All " &
                             "  " + querySchemeItem2 + " " &
                             " ) xyzp  group by xyzp.Document_No,xyzp.[VEHICLE NO],[WdName],xyzp.[Group],xyzp.[Cust Group Desc],xyzp.[Customer Category Code],xyzp.[Zone]  "
                    Else
                        Dim TempQry As String = " SELECT STUFF((SELECT distinct ',' + QUOTENAME(TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
                        Dim ItemDesc As String = clsDBFuncationality.getSingleValue(TempQry)
                        MainQueryForScheme = "select  [VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem + " , " + strItmeHeadingScheme + " from (select zzz.[VEHICLE NO],zzz.WdName,zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty from  (Select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '') As [VEHICLE NO], TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.QTY as Qty, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2  " + strWhrClause2 + " )zzz  WHERE zzz.Scheme_Item='Y' group by zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in (" + ItemDesc + ") ) as zpivot  "
                        querySchemeItem = MainQueryForScheme 'MainQuery
                        MainQueryForScheme = " select  [VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem2 + " , " + strSchemeItem + " from (select zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty from  (Select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as Qty, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2  " + strWhrClause2 + " )zzz WHERE ZZZ.Scheme_Item='N' group by zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot  "
                        querySchemeItem2 = MainQueryForScheme 'MainQuery
                        query = " Select [VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strSumItemOnly + ", " + strSumItemSchemeOnly + " , (" + strGrandTotal + ") as [Grand Total] from (  " &
                                "  " + querySchemeItem + "  " &
                                " Union All " &
                                "  " + querySchemeItem2 + " " &
                                " ) xyzp  group by xyzp.[VEHICLE NO],[WdName],xyzp.[Group],xyzp.[Cust Group Desc],xyzp.[Customer Category Code],xyzp.[Zone]  "
                    End If
                End If
                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(query)
                If chkFirstAndSecondSpellAbstract.Checked = True AndAlso dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                    Dim dtData As DataTable = dtgv.Copy()
                    Dim dtResult As DataTable = dtgv.Clone()
                    Dim view As DataView = New DataView(dtData)
                    Dim dtZone As DataTable = view.ToTable(True, "Zone")
                    dtResult.Columns.Add(New DataColumn("Total In Ltr", System.Type.GetType("System.Decimal")))
                    dtData.Columns.Add(New DataColumn("Total In Ltr", System.Type.GetType("System.Decimal")))
                    'Row wise total
                    For i As Integer = 0 To dtData.Rows.Count - 1
                        Dim RowLtrTotal As Decimal = 0.0
                        For j As Integer = 0 To dtData.Columns.Count - 1
                            If dtData.Columns(j).ColumnName.ToString().Contains("#L") Then
                                RowLtrTotal = RowLtrTotal + clsCommon.myCdbl(dtData.Rows(i)(j))
                            End If
                        Next
                        dtData.Rows(i)("Total In Ltr") = clsCommon.myCdbl(RowLtrTotal)
                    Next
                    For i As Integer = 0 To dtZone.Rows.Count - 1
                        Dim dtZoneData As DataTable = Nothing
                        Dim dr As DataRow() = dtData.Select(" Zone='" + clsCommon.myCstr(dtZone.Rows(i)("Zone")) + "'")
                        If dr IsNot Nothing AndAlso dr.Length > 0 Then
                            dtZoneData = dr.CopyToDataTable()
                            dtResult.Merge(dtZoneData, True, MissingSchemaAction.Ignore)
                        End If
                        'Column wise total
                        Dim RowQtyTotal As DataRow = dtResult.NewRow
                        RowQtyTotal("Document Date") = clsCommon.myCstr(dtZone.Rows(i)("Zone")) + " Total"
                        Dim RowLtrTotal As DataRow = dtResult.NewRow
                        RowLtrTotal("Document Date") = "Total In Ltr"
                        Dim TempLtrTotal As Decimal = 0.0
                        For j As Integer = 2 To dtZoneData.Columns.Count - 2
                            Dim TempColTotal As Double = clsCommon.myCdbl(dtZoneData.Compute("SUM([" + dtZoneData.Columns(j).ColumnName.ToString() + "])", ""))
                            If dtZoneData.Columns(j).ColumnName.ToString().Contains("#L") Then
                                RowLtrTotal(dtZoneData.Columns(j).ColumnName.ToString().Replace("#L", "")) = TempColTotal
                                TempLtrTotal = TempLtrTotal + TempColTotal
                            Else
                                RowQtyTotal(dtZoneData.Columns(j).ColumnName.ToString()) = TempColTotal
                            End If
                        Next
                        RowLtrTotal("Total In Ltr") = TempLtrTotal
                        dtResult.Rows.Add(RowQtyTotal)
                        dtResult.Rows.Add(RowLtrTotal)
                        Dim newBlankRow1 As DataRow = dtResult.NewRow
                        dtResult.Rows.Add(newBlankRow1)
                    Next
                    'Grand Total
                    Dim GrandQtyTotal As DataRow = dtResult.NewRow
                    GrandQtyTotal("Document Date") = "Grand Total"
                    Dim GrandLtrTotal As DataRow = dtResult.NewRow
                    GrandLtrTotal("Document Date") = "Grand Total In Ltr"
                    Dim TempGrandLtrTotal As Decimal = 0.0
                    For j As Integer = 2 To dtData.Columns.Count - 2
                        Dim TempColTotal As Double = clsCommon.myCdbl(dtData.Compute("SUM([" + dtData.Columns(j).ColumnName.ToString() + "])", ""))
                        If dtData.Columns(j).ColumnName.ToString().Contains("#L") Then
                            GrandLtrTotal(dtData.Columns(j).ColumnName.ToString().Replace("#L", "")) = TempColTotal
                            TempGrandLtrTotal = TempGrandLtrTotal + TempColTotal
                        Else
                            GrandQtyTotal(dtData.Columns(j).ColumnName.ToString()) = TempColTotal
                        End If
                    Next
                    GrandLtrTotal("Total In Ltr") = TempGrandLtrTotal
                    dtResult.Rows.Add(GrandQtyTotal)
                    dtResult.Rows.Add(GrandLtrTotal)
                    dtgv = Nothing
                    dtgv = dtResult.Copy()
                End If
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()

                Gv1.Columns.Clear()
                Gv1.DataSource = dtgv
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.BestFitColumns()
                If clsCommon.myLen(Gv1.Columns("Document Date")) > 0 Then
                    Gv1.Columns("Document Date").FormatString = "{0 : dd/MM/yyyy}"
                End If
                If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
                Dim item As Integer = 0
                If isSchemeItem = False Then
                    If ChkDayWiseSummary.Checked = True Then
                        item = 2
                    ElseIf chkBookingWise.Checked = True AndAlso (chkMilkPouch.Checked = False AndAlso chkProduct.Checked = False) Then
                        item = 28 ' 10 New column add in 8 column
                    ElseIf chkBookingWise.Checked = True AndAlso (chkMilkPouch.Checked = True OrElse chkProduct.Checked = True) Then
                        If clsCommon.CompairString(cboShift.Text, "Both") = CompairStringResult.Equal Then
                            item = 3
                        ElseIf clsCommon.CompairString(cboShift.Text, "Shift Wise") = CompairStringResult.Equal Then
                            item = 5
                        Else
                            item = 5
                        End If
                    ElseIf chkSaleInvoiceWise.Checked = True Then
                        item = 9
                    ElseIf chkRouteBoothWise.Checked = True Then
                        item = 6
                    Else
                        item = 8
                    End If
                Else
                    If chkBookingWise.Checked = True Then
                        item = 6
                    ElseIf chkSaleInvoiceWise.Checked = True Then
                        item = 7
                    ElseIf chkRouteBoothWise.Checked = True Then
                        item = 6
                    Else
                        item = 6
                    End If
                End If
                If chkFirstAndSecondSpellAbstract.Checked = False Then
                    If Gv1.Rows.Count > 0 Then
                        Dim summaryRowItem As New GridViewSummaryRowItem()
                        For i As Integer = item To Gv1.Columns.Count - 1
                            Dim aa = Gv1.Columns(i).HeaderText()
                            Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                            If clsCommon.CompairString(aa, "Modified By") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(aa, "Created By") <> CompairStringResult.Equal Then
                                summaryRowItem.Add(item1)
                                Gv1.Columns(i).FormatString = "{0:n2}"
                            End If
                        Next
                        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    End If
                End If
                ' Hide Column when footer grand total <= 0
                If chkFirstAndSecondSpell.Checked = False AndAlso chkFirstAndSecondSpellAbstract.Checked = False Then
                    For i As Integer = item To Gv1.Columns.Count - 5
                        Dim grandTotal As Decimal = 0
                        For j As Integer = 0 To Gv1.Rows.Count - 1
                            Dim columnValue As Object = String.Empty
                            columnValue = Gv1.Rows(j).Cells(i).Value
                            If (Not IsDBNull(Gv1.Rows(j).Cells(i).Value) AndAlso columnValue IsNot Nothing) Then
                                grandTotal = grandTotal + clsCommon.myCdbl(Gv1.Rows(j).Cells(i).Value)
                            End If
                        Next
                        If (clsCommon.myCdbl(grandTotal) > 0) Then
                            Gv1.Columns(i).IsVisible = True
                        Else
                            Gv1.Columns(i).IsVisible = False
                        End If
                    Next
                End If
                If chkFirstAndSecondSpell.Checked = True OrElse chkFirstAndSecondSpellAbstract.Checked = True Then
                    Gv1.Columns("Grand Total").IsVisible = False
                End If
                If chkFirstAndSecondSpellAbstract.Checked = True Then
                    For i As Integer = 0 To Gv1.Columns.Count - 1
                        If Gv1.Columns(i).HeaderText.Contains("#L") Then
                            Gv1.Columns(i).IsVisible = False
                        End If
                    Next
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Pinned column 
                If ChkDayWiseSummary.Checked Then
                    Gv1.Columns("ddFilter").IsVisible = False
                Else
                    Gv1.Columns(0).IsPinned = True
                    Gv1.Columns(1).IsPinned = True
                    If chkRouteBoothWise.Checked = True Then
                        Gv1.Columns(2).IsPinned = True
                        Gv1.Columns(3).IsPinned = True
                        Gv1.Columns(4).IsPinned = True
                    End If
                    If chkMilkPouch.Checked = False AndAlso chkProduct.Checked = False Then
                        Gv1.Columns("Grand Total").IsPinned = True
                        Gv1.Columns("Grand Total").PinPosition = PinnedColumnPosition.Right
                    End If
                    If Gv1.Columns.Contains("Total Price") Then
                        Gv1.Columns("Total Price").IsPinned = True
                        Gv1.Columns("Total Price").PinPosition = PinnedColumnPosition.Right
                    End If
                    If Gv1.Columns.Contains("Remitted Amount") Then
                        Gv1.Columns("Remitted Amount").IsPinned = True
                        Gv1.Columns("Remitted Amount").PinPosition = PinnedColumnPosition.Right
                    End If
                    If Gv1.Columns.Contains("Difference Amount") Then
                        Gv1.Columns("Difference Amount").IsPinned = True
                        Gv1.Columns("Difference Amount").PinPosition = PinnedColumnPosition.Right
                    End If
                    If Gv1.Columns.Contains("Total In Ltr") Then
                        Gv1.Columns("Total In Ltr").IsPinned = True
                        Gv1.Columns("Total In Ltr").PinPosition = PinnedColumnPosition.Right
                    End If
                End If
                If (chkMilkPouch.Checked = True OrElse chkProduct.Checked = True) AndAlso clsCommon.CompairString(cboShift.Text, "Shift Wise") = CompairStringResult.Equal Then
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Route No] as [Route No] format ""{0}: {1}"" Group By [Route No]"))
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Customer Code] as [Customer Code] format ""{0}: {1}"" Group By [Customer Code]"))
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Customer Name] as [Customer Name] format ""{0}: {1}"" Group By [Customer Name]"))
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Document Date] as [Document Date] format ""{0}: {1}"" Group By [Document Date]"))
                    Gv1.AutoExpandGroups = True
                    Gv1.ShowGroupPanel = False
                    Gv1.ShowRowHeaderColumn = False
                    Gv1.AllowAddNewRow = False
                    Gv1.AllowDeleteRow = False
                    Gv1.EnableFiltering = True
                    Gv1.ShowFilteringRow = True
                End If
                Try
                    If chkMilkPouch.Checked = True OrElse chkProduct.Checked = True Then
                        Dim strItemFatch() As String = strItem2.Split(",")
                        For count As Integer = 0 To strItemFatch.Length - 1
                            Dim strCode As String = strItemFatch(count)
                            Dim strCode2 As String = Replace(strItemFatch(count), "[", "")
                            strCode2 = Replace(strCode2, "]", "")
                            Dim sum As Integer = clsCommon.myCdbl(dtgv.Compute("SUM(" + strCode + ")", String.Empty))
                            If Gv1.Columns.Contains(strCode2) Then
                                If sum > 0 Then
                                    Gv1.Columns(strCode2).IsVisible = True
                                Else
                                    Gv1.Columns(strCode2).IsVisible = False
                                End If
                            End If
                        Next
                    End If

                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptMatrixFreshSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        txtPTSDateFrom.Value = clsCommon.GETSERVERDATE()
        ' txtPTSDateTo.Value = clsCommon.GETSERVERDATE()
        txtCreditDateFrom.Value = clsCommon.GETSERVERDATE()
        txtCreditDateTo.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        isSchemeItem = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeItemQty, clsFixedParameterCode.AllowSchemeItemQty, Nothing)) = 1, True, False)
        If isSchemeItem = True Then
            chkFirstAndSecondSpellAbstract.Visible = False
        Else
            chkFirstAndSecondSpellAbstract.Visible = True
        End If
        LoadInvoiceType()
        ApplyMilkPouchPrint = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyMilkPouchPrint, clsFixedParameterCode.ApplyMilkPouchPrint, Nothing)) = 0, False, True)
        If ApplyMilkPouchPrint = True Then
            chkBookingWise.Checked = True
            chkMilkPouch.Checked = True
        End If
        cboShift.Text = "Both"
        ReportType()
        ReportDW()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtCustomer.arrValueMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        txtItemCode.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        txtLorry.arrValueMember = Nothing
        TxtRoute.arrValueMember = Nothing
        TxtUOM.arrValueMember = Nothing
        txtBookingType.arrValueMember = Nothing
        TxtMultiCustomerCategory.arrValueMember = Nothing
        txtfndCustomer.Visible = False
        txtFndRoute.Visible = False
        txtCustomer.Location = New System.Drawing.Point(133, 158)
        lblCustomer.Location = New System.Drawing.Point(21, 158)
        MyLabel10.Location = New System.Drawing.Point(21, 261)
        TxtRoute.Location = New System.Drawing.Point(133, 261)
        RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        txtfndCustomer.Value = ""
        txtFndRoute.Value = ""
        ReportType()
        ReportDW()
        LoadInvoiceType()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        txtPTSDateFrom.Value = clsCommon.GETSERVERDATE()
        'txtPTSDateTo.Value = clsCommon.GETSERVERDATE()
        txtCreditDateFrom.Value = clsCommon.GETSERVERDATE()
        txtCreditDateTo.Value = clsCommon.GETSERVERDATE()
        rbtnTaxable.Checked = True
        fndCustomer.Value = Nothing
        If clsCommon.myLen(cboShift.Text) > 0 Then
            cboShift.Text = "Both"
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Dim shift As String = ""
            If rbtnMrng.Checked Then
                shift = "Morning"
            Else
                shift = "Evening"
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If clsCommon.CompairString(ddlReportType.SelectedValue, "MGPD") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlReportType.SelectedValue, "PGPD") = CompairStringResult.Equal Then
                arrHeader.Add("Shift : " & shift)
            End If
            arrHeader.Add("Name: " & ddlReportType.Text)
            'arrHeader.Add("Age as of: " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy") + " cutoff Date " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMatrixFreshSalesReport & "'"))
            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Matrix Fresh Sale Report")
                clsCommon.MyExportToPDF("Matrix Fresh Sale Report", Gv1, arrHeader, "Matrix Fresh Sale Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    'added by richa  BHA/21/08/18-000469 
    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub
    Private Sub TxtUOM__My_Click(sender As Object, e As EventArgs) Handles TxtUOM._My_Click
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Name from TSPL_UNIT_MASTER where 1=1"
        TxtUOM.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtUOM.arrValueMember, TxtUOM.arrDispalyMember)
    End Sub
    Sub LoadInvoiceType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Taxable")
        dt.Rows.Add("Non Taxable")
        ddlInvocieType.DataSource = dt
        ddlInvocieType.ValueMember = "Code"
        ddlInvocieType.DisplayMember = "Code"
    End Sub
    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMatrixFreshSalesReport & "'"))
            If txtCustomerGroup.arrDispalyMember IsNot Nothing AndAlso txtCustomerGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtItemCode.arrDispalyMember IsNot Nothing AndAlso txtItemCode.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
            End If
            If txtLorry.arrDispalyMember IsNot Nothing AndAlso txtLorry.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Lorry : " + clsCommon.GetMulcallStringWithComma(txtLorry.arrDispalyMember))
            End If
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If
            If TxtRoute.arrDispalyMember IsNot Nothing AndAlso TxtRoute.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(TxtRoute.arrDispalyMember))
            End If
            If TxtUOM.arrDispalyMember IsNot Nothing AndAlso TxtUOM.arrDispalyMember.Count > 0 Then
                arrHeader.Add("UOM : " + clsCommon.GetMulcallStringWithComma(TxtUOM.arrDispalyMember))
            End If
            If TxtMultiCustomerCategory.arrDispalyMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Category: " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrDispalyMember))
            End If
            If clsCommon.myLen(ddlInvocieType.Text) > 0 Then
                arrHeader.Add("Invoice Type : " + ddlInvocieType.Text)
            End If
            clsCommon.MyExportToPDF("Matrix Fresh Sale Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : VIJ/09/12/19-000100 , VIJ/03/12/19-000088 By Prabhakar
    Private Sub txtBookingType__My_Click(sender As Object, e As EventArgs) Handles txtBookingType._My_Click
        If chkSummary.Checked = True OrElse chkBookingWise.Checked = True OrElse chkRouteBoothWise.Checked = True Then
            strQry = " Select final.Code From (Select 'CD' Code Union All Select 'CR' Code Union All Select 'SO' Code Union All Select 'Cash' Code Union All Select 'Festive Order' Code Union All Select 'Distributor' Code ) final  "
            txtBookingType.arrValueMember = clsCommon.ShowMultipleSelectForm("BookingType@MaterixReport", strQry, "Code", "Code", txtBookingType.arrValueMember, txtBookingType.arrDispalyMember)
        Else
            clsCommon.MyMessageBoxShow(Me, "This Filter Allow For Summmary/Booking Wise/RuteBoothWise", Me.Text)
        End If
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If chkSummary.Checked Then
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = MyBase.Form_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                Gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = Gv1.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Save Layout working Only for Summary type Data.", Me.Text)
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
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
    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCaMulSel1", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
    Private Sub ChkBookingWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBookingWise.ToggleStateChanged
        Try
            If chkBookingWise.Checked = True Then
                chkGatePass.Enabled = True
                chkMilkPouch.Enabled = True
                chkProduct.Enabled = True
                chkRouteSummary.Enabled = True
                pnlMilkPouch.Enabled = True
                If ApplyMilkPouchPrint = True Then
                    chkMilkPouch.Checked = True
                End If
            Else
                chkGatePass.Enabled = False
                chkGatePass.Checked = False
                chkMilkPouch.Enabled = False
                chkMilkPouch.Checked = False
                chkProduct.Enabled = False
                chkProduct.Checked = False
                pnlMilkPouch.Enabled = False
                chkRouteSummary.Enabled = False
                chkRouteSummary.Checked = False
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnRouteSummaryPrint_Click(sender As Object, e As EventArgs) Handles btnRouteSummaryPrint.Click
        PageSetupReport_ID = MyBase.Form_ID
        Print(Exporter.Print)
    End Sub
    Private Sub chkRouteSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkRouteSummary.ToggleStateChanged
        Try
            If chkRouteSummary.Checked = True Then
                btnRouteSummaryPrint.Enabled = True
                chkMilkPouch.Checked = False
                chkProduct.Checked = False
            Else
                btnRouteSummaryPrint.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chkMilkPouch_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMilkPouch.ToggleStateChanged
        Try
            If chkMilkPouch.Checked = True Then
                chkRouteSummary.Checked = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub chkProduct_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkProduct.ToggleStateChanged
        Try
            If chkProduct.Checked = True Then
                chkRouteSummary.Checked = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnPrintTrkSht_Click(sender As Object, e As EventArgs) Handles btnPrintTrkSht.Click
        Try
            clsDemandBookingSale.PrintDOSData(txtMultPTSRoute.arrValueMember, ddlPTSShift.Text, txtPTSDateFrom.Value, rbtnMilk.Checked, rbtnProduct.Checked, chkIndividualCustomer.Checked, 0, 70, False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    '    Private Sub btnPrintTrkSht_Click(sender As Object, e As EventArgs) Handles btnPrintTrkSht.Click
    '        Try
    '            Dim BaseQry As String = Nothing
    '            BaseQry = " select xfinal.*,case when (select top 1 posted from TSPL_DEMAND_BOOKING_MASTER where Route_No in( xfinal.route_no) and ShiftType= xfinal.ShiftType and convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') =1 then 'Approved' else 'Pending' end as DocStatus ,TSPL_CUSTOMER_MASTER.Credit_Customer,TSPL_CUSTOMER_MASTER.Display_Seq 
    'from (select xx.*,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull((select sum(ItemNetAmount) as netamt  from TSPL_DEMAND_BOOKING_MASTER left join  TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No where  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ddlPTSShift.Text + "'  and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "')"
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                BaseQry += "and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            BaseQry += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=xx.Cust_Code ),0 ) + isnull((xx.PrevItemNetAmount),0) ) else 0 end) else 0 end as AmountBE,
    'case when xx.SNO=1 then xx.Crate_Collect else 0 end as TotalCollectCrate,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull(tcs.TCSAmount,0)+isnull(prevtcs.pTCSAmount,0)) else 0 end) else 0 end as TotalTCSAmt
    'from ( select XXFinal.Cust_Code as Cust_Code, max(XXFinal.ShiftType) as ShiftType, XXFinal.Sku_Seq as Sku_Seq ,max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Short_Description) as Short_Description, sum(XXFinal.Qty) as Qty, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.Crate) as Crate, max(XXFinal.Pouch) as Pouch, sum(XXFinal.ItemNetAmount) as ItemNetAmount,max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc,max(XXFinal.PrevCrate) as Crate_Collect, max(XXFinal.CompanyName) as CompanyName ,max(XXFinal.TranspoterName) as TranspoterName,max(XXFinal.DriverName) as DriverName,max(XXFinal.Vehicle_No) as Vehicle_No, max(XXFinal.Item_Rate) as Item_Rate, max(XXFinal.CFForLTR) as CFForLTR, max(XXFinal.Conversion_Factor) as Conversion_Factor, sum(XXFinal.QTYLtr) as QTYLtr,max(XXFinal.PrevItemNetAmount) as PrevItemNetAmount,ROW_NUMBER() over (Partition by Cust_Code order by Cust_Code) as SNO,max(XXFinal.CreditCust) as CreditCust
    'from ( select  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.ShiftType, TSPL_ITEM_MASTER.Sku_Seq,TSPL_DEMAND_BOOKING_MASTER.Document_Date,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty,0 as PrevQty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise Else 0 End As Crate, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As PrevCrate,
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End As Pouch,
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As PrevPouch,
    'TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,0 as PrevItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,
    'TSPL_COMPANY_MASTER.Comp_Name as CompanyName,TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName,TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
    'TSPL_DEMAND_BOOKING_DETAIL.Item_Rate,ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,Convert(decimal(18, 2),(TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ ITEMDETAIL.CFForLTR) As QTYLtr,TSPL_CUSTOMER_MASTER.Credit_Customer as CreditCust
    'from TSPL_DEMAND_BOOKING_DETAIL 
    'Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
    'Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
    'left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
    'Left Join ( select Conversion_factor AS CFForLTR, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR'  ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
    'Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
    'Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
    'Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
    'Left Join TSPL_COMPANY_MASTER on 2=2 where  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ddlPTSShift.Text + "' and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value, "dd/MMM/yyyy") + "') "
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                BaseQry += "and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            If rbtnMilk.Checked Then
    '                BaseQry += " And TSPL_ITEM_MASTER.Is_FreshItem=1 "
    '            ElseIf rbtnProduct.Checked Then
    '                BaseQry += " And TSPL_ITEM_MASTER.Is_Ambient=1 "
    '            End If
    '            If Not chkIndividualCustomer.Checked Then
    '                BaseQry += " and TSPL_DEMAND_BOOKING_MASTER.IsIndividualCustomer=0 "
    '            End If
    '            BaseQry += " union all
    'select  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,'" + ddlPTSShift.Text + "'  as ShiftType,TSPL_ITEM_MASTER.Sku_Seq,'" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "' as Document_Date, 
    'TSPL_ITEM_MASTER.Short_Description,0 as Qty,TabCustWiseCrate.Qty as PrevQty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As Crate,
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TabCustWiseCrate.TotalCrates_ItemWise Else 0 End As PrevCrate, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As Pouch, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TabCustWiseCrate.qty Else 0 End As PrevPouch,
    '0 as ItemNetAmount,NetAmount as PrevItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc, 
    'TSPL_COMPANY_MASTER.Comp_Name as CompanyName,TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName,TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
    'TSPL_DEMAND_BOOKING_DETAIL.Item_Rate,ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0 As QTYLtr,TSPL_CUSTOMER_MASTER.Credit_Customer as CreditCust
    'from (select ROW_NUMBER() over (PARTITION BY xx.Cust_Code order by xx.Cust_Code, xx.ORDCol desc) as SNO, xx.Cust_Code,xx.ORDCol,sum(xx.TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(xx.TotalLtr_ItemWise) as TotalLtr,sum(xx.ItemNetAmount) as NetAmount,sum(xx.qty) as Qty
    'from(select innBD.Cust_Code, convert(varchar, InnBM.Document_Date, 102)+ case when innBD.ShiftType = 'Evening' then 'B' else 'A' end as ORDCol,innBD.TotalCrates_ItemWise, innBD.TotalLtr_ItemWise, innBD.ItemNetAmount,innBD.qty
    'from TSPL_DEMAND_BOOKING_MASTER as InnBM 
    'left outer join TSPL_DEMAND_BOOKING_DETAIL innBD on innBD.Document_No = InnBM.Document_No 
    'where 2 = 2  "
    '            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
    '                BaseQry += " and innBD.ShiftType='Evening' and ( CONVERT(date, InnBM.Document_Date, 103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value.AddDays(-1)) + "') "
    '            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
    '                BaseQry += " and innBD.ShiftType='Morning' and CONVERT(date, InnBM.Document_Date,103)='" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "'" ' or CONVERT(date, InnBM.Document_Date,103)<'" + clsCommon.GetPrintDate(txtDate.Value) + "') "
    '            End If
    '            BaseQry += " and innBD.Cust_Code is not null ) xx  
    'group by xx.Cust_Code,xx.ORDCol 
    ')  TabCustWiseCrate 
    'left join TSPL_Demand_Booking_Detail on TabCustWiseCrate.cust_Code=TSPL_Demand_Booking_Detail.cust_Code and TabCustWiseCrate.SNO=1
    'Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
    'inner join (select Against_DemandBooking_No,sum(isnull(TCSAmount,0)) as tcs_amt from TSPL_BOOKING_matser group by Against_DemandBooking_No) as TSPL_BOOKING_matser on TSPL_BOOKING_matser.Against_DemandBooking_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
    'Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code   And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
    'left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
    'Left Join (select Conversion_factor AS CFForLTR, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR') as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
    'Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
    'Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
    'Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
    'Left Join TSPL_COMPANY_MASTER on 2=2 "
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                BaseQry += " where TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            If rbtnMilk.Checked Then
    '                BaseQry += " And TSPL_ITEM_MASTER.Is_FreshItem=1 "
    '            ElseIf rbtnProduct.Checked Then
    '                BaseQry += " And TSPL_ITEM_MASTER.Is_Ambient=1 "
    '            End If
    '            BaseQry += " )XXFinal
    '  where XXFinal.Cust_Code in (select distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code from TSPL_DEMAND_BOOKING_MASTER left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No   where 2=2  and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code is not null "
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                BaseQry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            BaseQry += " )Group by XXFinal.Cust_Code,XXFinal.Sku_Seq )xx
    'left join ( select sum(XYZ.TCSAmount) as TCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No from (
    'select TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER
    'left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
    'where 2=2"
    '            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
    '                BaseQry += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
    '            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
    '                BaseQry += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
    '            End If

    '            BaseQry += " group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No) XYZ
    'group by XYZ.Cust_Code)  as tcs on xx.Cust_Code=tcs.Cust_Code left join (select sum(XYZ.pTCSAmount) as pTCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No from (
    'select (TSPL_BOOKING_MATSER.TCSAmount) as pTCSAmount ,(TSPL_BOOKING_MATSER.Against_DemandBooking_No) Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No where 2=2 "
    '            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
    '                BaseQry += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value.AddDays(-1)) + "') "
    '            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
    '                BaseQry += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
    '            End If
    '            BaseQry += " group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No
    ') XYZ
    'group by XYZ.Cust_Code
    ') as prevtcs on xx.Cust_Code=prevtcs.Cust_Code) xfinal 
    'left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=xfinal.Cust_Code "

    '            Dim qry As String = " select Short_Description from (" + BaseQry + " )xx group by Short_Description order by max(Sku_Seq)"
    '            Dim dtItem As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            If dtItem IsNot Nothing AndAlso dtItem.Rows.Count <= 0 Then
    '                Throw New Exception("No Data found to print")
    '            End If


    '            qry = "select Route_No ,max(Route_Desc) as Route_Desc,max(TranspoterName) as TranspoterName,max(DriverName) as DriverName,MAX(Vehicle_No) as Vehicle_No,convert(varchar, max(Document_Date),103) as Document_Date,FORMAT(GETDATE(), 'dd/MM/yyyy hh:mm tt') as PrintDateTime ,max(ShiftType) as ShiftType,max(DocStatus) as DocStatus,Cust_Code,case when Credit_Customer='Y' then 'Department Booth' else 'Normal Booth' end as Credit_Customer "
    '            For Each drItem As DataRow In dtItem.Rows
    '                qry += ",sum((case when Credit_Customer='Y' then QTYLtr else Crate end) * (case when Short_Description='" + clsCommon.myCstr(drItem("Short_Description")) + "' then 1 else 0 end)) as [" + clsCommon.myCstr(drItem("Short_Description")) + "] "
    '            Next
    '            qry += ",sum(case when Credit_Customer='Y' then QTYLtr else Crate end) as [TotalCrate]
    ',sum(ItemNetAmount) as ItemNetAmount
    ',sum(AmountBE) as AmountBE
    ',sum(TotalTCSAmt) as TotalTCSAmt
    ',sum(TotalCollectCrate) as TotalCollectCrate
    'from (
    '" + BaseQry + "
    ')xx Group by xx.Route_No,Cust_Code,Credit_Customer
    'order by xx.Route_No,xx.Credit_Customer,max(Display_Seq)"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

    '#Region "Add Grand Total"
    '            qry = "select  Route_No,sum( QTYLtr ) as [TOTLTR] "
    '            For Each drItem As DataRow In dtItem.Rows
    '                qry += ",sum( QTYLtr * (case when Short_Description='" + clsCommon.myCstr(drItem("Short_Description")) + "' then 1 else 0 end)) as [LTR#$#" + clsCommon.myCstr(drItem("Short_Description")) + "]  
    ',sum( Crate * (case when Short_Description='" + clsCommon.myCstr(drItem("Short_Description")) + "' then 1 else 0 end)) as [CRATE#$#" + clsCommon.myCstr(drItem("Short_Description")) + "]  
    ',sum( Pouch * (case when Short_Description='" + clsCommon.myCstr(drItem("Short_Description")) + "' then 1 else 0 end)) as [POUCH#$#" + clsCommon.myCstr(drItem("Short_Description")) + "]  "
    '            Next
    '            qry += " from ( " + BaseQry + " )  xx group by  Route_No order by Route_No "
    '            Dim dtTotal As DataTable = clsDBFuncationality.GetDataTable(qry)

    '            For Each drTotal As DataRow In dtTotal.Rows
    '                Dim drLtr As DataRow = dt.NewRow
    '                drLtr("Route_No") = drTotal("Route_No")
    '                drLtr("Cust_Code") = "Litre"
    '                drLtr("Credit_Customer") = "Grand Total"
    '                drLtr("TotalCrate") = drTotal("TOTLTR")
    '                Dim drCrate As DataRow = dt.NewRow
    '                drCrate("Route_No") = drTotal("Route_No")
    '                drCrate("Cust_Code") = "Crate"
    '                drCrate("Credit_Customer") = "Grand Total"
    '                drCrate("TotalCrate") = 0
    '                Dim drPourch As DataRow = dt.NewRow
    '                drPourch("Route_No") = drTotal("Route_No")
    '                drPourch("Cust_Code") = "Pouch"
    '                drPourch("Credit_Customer") = "Grand Total"
    '                drPourch("TotalCrate") = 0
    '                For Each drItem As DataRow In dtItem.Rows
    '                    drLtr(clsCommon.myCstr(drItem("Short_Description"))) = drTotal("LTR#$#" + clsCommon.myCstr(drItem("Short_Description")))
    '                    drCrate(clsCommon.myCstr(drItem("Short_Description"))) = drTotal("CRATE#$#" + clsCommon.myCstr(drItem("Short_Description")))
    '                    drPourch(clsCommon.myCstr(drItem("Short_Description"))) = drTotal("POUCH#$#" + clsCommon.myCstr(drItem("Short_Description")))

    '                    Dim Quotient As Integer = clsCommon.myCDecimal(drPourch(clsCommon.myCstr(drItem("Short_Description"))) / 12)
    '                    Dim Reminder As Integer = drPourch(clsCommon.myCstr(drItem("Short_Description"))) Mod 12
    '                    drCrate(clsCommon.myCstr(drItem("Short_Description"))) += Quotient
    '                    drPourch(clsCommon.myCstr(drItem("Short_Description"))) = Reminder

    '                    drCrate("TotalCrate") += drCrate(clsCommon.myCstr(drItem("Short_Description")))
    '                    drPourch("TotalCrate") += drPourch(clsCommon.myCstr(drItem("Short_Description")))
    '                Next
    '                dt.Rows.Add(drLtr)
    '                dt.Rows.Add(drCrate)
    '                dt.Rows.Add(drPourch)
    '            Next
    '            dt.AcceptChanges()
    '#End Region


    '            Dim obj As clsDosPrint = New clsDosPrint()
    '            obj.ReportName = objCommonVar.CurrentCompanyName

    '            obj.HideGroupHeader = True
    '            obj.HideLastGroupTotal = True
    '            'obj.ShowPageNo = True
    '            'obj.PageSetupCustomizeCharColumn = 140
    '            obj.PageSetupCustomizeCharRows = 70

    '            obj.objReportGroup = New clsDosPrintReportGroup
    '            obj.objReportGroup.Name = "Route_No"

    '            obj.objReportGroup.HeaderText1 = "DAILY TENTATIVE DEMAND SHEET FOR AREA NO: #$Route_No$# Date: #$Document_Date$# Shift: #$ShiftType$# Status: #$DocStatus$#"
    '            obj.objReportGroup.arrHeaderText1 = New List(Of clsDosPrintReportGroupReplaceHeader)
    '            Dim objGRH As New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "Route_No"
    '            objGRH.ConstString = "#$Route_No$#"
    '            obj.objReportGroup.arrHeaderText1.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "Document_Date"
    '            objGRH.ConstString = "#$Document_Date$#"
    '            obj.objReportGroup.arrHeaderText1.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "ShiftType"
    '            objGRH.ConstString = "#$ShiftType$#"
    '            obj.objReportGroup.arrHeaderText1.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "DocStatus"
    '            objGRH.ConstString = "#$DocStatus$#"
    '            obj.objReportGroup.arrHeaderText1.Add(objGRH)

    '            obj.objReportGroup.HeaderText2 = "#$Route_Desc$# ( ROUTE: #$Route_No$# )  #$TranspoterName$#  DRIVER: #$DriverName$#  VEHICLE NO:#$Vehicle_No$#  PRINT AT: #$PrintDateTime$#"
    '            obj.objReportGroup.arrHeaderText2 = New List(Of clsDosPrintReportGroupReplaceHeader)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "Route_Desc"
    '            objGRH.ConstString = "#$Route_Desc$#"
    '            obj.objReportGroup.arrHeaderText2.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "Route_No"
    '            objGRH.ConstString = "#$Route_No$#"
    '            obj.objReportGroup.arrHeaderText2.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "TranspoterName"
    '            objGRH.ConstString = "#$TranspoterName$#"
    '            obj.objReportGroup.arrHeaderText2.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "DriverName"
    '            objGRH.ConstString = "#$DriverName$#"
    '            obj.objReportGroup.arrHeaderText2.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "Vehicle_No"
    '            objGRH.ConstString = "#$Vehicle_No$#"
    '            obj.objReportGroup.arrHeaderText2.Add(objGRH)

    '            objGRH = New clsDosPrintReportGroupReplaceHeader
    '            objGRH.ColumnName = "PrintDateTime"
    '            objGRH.ConstString = "#$PrintDateTime$#"
    '            obj.objReportGroup.arrHeaderText2.Add(objGRH)


    '            obj.arrGroup = New List(Of clsDosPrintGroup)()
    '            obj.arrGroup.Add(clsDosPrintGroup.GetObject("Credit_Customer", "Details of", ""))

    '            obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
    '            obj.arrColumn = New List(Of clsDosPrintColumn)()
    '            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cust_Code", "Booth", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
    '            For Each drItem As DataRow In dtItem.Rows
    '                obj.arrColumn.Add(clsDosPrintColumn.SetColumn(clsCommon.myCstr(drItem("Short_Description")), clsCommon.myCstr(drItem("Short_Description")), False, DosPrintAlignment.Right, 10, True, DecimalPlaces.One))
    '            Next
    '            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TotalCrate", "Total", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Zero))
    '            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ItemNetAmount", "Shift Amt", False, DosPrintAlignment.Right, 12, True, DecimalPlaces.Two))
    '            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AmountBE", "  Total Amt", False, DosPrintAlignment.Right, 14, True, DecimalPlaces.Two))
    '            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TotalTCSAmt", "TCS", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
    '            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TotalCollectCrate", "Crate", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Zero))

    '            obj.Print(obj, dt, PageSetup.Landscap)
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub btnPrintTrkSht_Click(sender As Object, e As EventArgs) Handles btnPrintTrkSht.Click
    '        Try
    '            Dim MilkProductBoth As String = Nothing
    '            Dim Qry As String = Nothing
    '            Qry = " select xfinal.*,case when (select top 1 posted from TSPL_DEMAND_BOOKING_MASTER where Route_No in( xfinal.route_no) and ShiftType= xfinal.ShiftType and convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') =1 then 'Approved' else 'Pending' end as DocStatus
    'from (select xx.*,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull((select sum(ItemNetAmount) as netamt  from TSPL_DEMAND_BOOKING_MASTER left join  TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No where  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ddlPTSShift.Text + "'  and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "')"
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                Qry += "and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            Qry += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=xx.Cust_Code ),0 ) + isnull((xx.PrevItemNetAmount),0) ) else 0 end) else 0 end as AmountBE,
    'case when xx.SNO=1 then xx.Crate_Collect else 0 end as TotalCollectCrate,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull(tcs.TCSAmount,0)+isnull(prevtcs.pTCSAmount,0)) else 0 end) else 0 end as TotalTCSAmt
    'from ( select XXFinal.Cust_Code as Cust_Code, max(XXFinal.ShiftType) as ShiftType, XXFinal.Sku_Seq as Sku_Seq ,max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Short_Description) as Short_Description, sum(XXFinal.Qty) as Qty, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.Crate) as Crate, max(XXFinal.Pouch) as Pouch, sum(XXFinal.ItemNetAmount) as ItemNetAmount,max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc,max(XXFinal.PrevCrate) as Crate_Collect, max(XXFinal.CompanyName) as CompanyName ,max(XXFinal.TranspoterName) as TranspoterName,max(XXFinal.DriverName) as DriverName,max(XXFinal.Vehicle_No) as Vehicle_No, max(XXFinal.Item_Rate) as Item_Rate, max(XXFinal.CFForLTR) as CFForLTR, max(XXFinal.Conversion_Factor) as Conversion_Factor, sum(XXFinal.QTYLtr) as QTYLtr,max(XXFinal.PrevItemNetAmount) as PrevItemNetAmount,ROW_NUMBER() over (Partition by Cust_Code order by Cust_Code) as SNO,max(XXFinal.CreditCust) as CreditCust
    'from ( select  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.ShiftType, TSPL_ITEM_MASTER.Sku_Seq,TSPL_DEMAND_BOOKING_MASTER.Document_Date,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty,0 as PrevQty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise Else 0 End As Crate, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As PrevCrate,
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End As Pouch,
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As PrevPouch,
    'TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,0 as PrevItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,
    'TSPL_COMPANY_MASTER.Comp_Name as CompanyName,TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName,TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
    'TSPL_DEMAND_BOOKING_DETAIL.Item_Rate,ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,Convert(decimal(18, 2),(TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ ITEMDETAIL.CFForLTR) As QTYLtr,TSPL_CUSTOMER_MASTER.Credit_Customer as CreditCust
    'from TSPL_DEMAND_BOOKING_DETAIL 
    'Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
    'Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
    'left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
    'Left Join ( select Conversion_factor AS CFForLTR, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR'  ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
    'Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
    'Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
    'Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
    'Left Join TSPL_COMPANY_MASTER on 2=2 where  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ddlPTSShift.Text + "' and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value, "dd/MMM/yyyy") + "') "
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                Qry += "and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            If rbtnMilk.Checked Then
    '                MilkProductBoth = "'Product'"
    '                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
    '            ElseIf rbtnProduct.Checked Then
    '                MilkProductBoth = "'Milk'"
    '                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
    '            Else
    '                MilkProductBoth = "'Milk','Product'"
    '                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
    '            End If
    '            If Not chkIndividualCustomer.Checked Then
    '                Qry += " and TSPL_DEMAND_BOOKING_MASTER.IsIndividualCustomer=0 "
    '            End If
    '            Qry += " union all
    'select  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,'" + ddlPTSShift.Text + "'  as ShiftType,TSPL_ITEM_MASTER.Sku_Seq,'" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "' as Document_Date, 
    'TSPL_ITEM_MASTER.Short_Description,0 as Qty,TabCustWiseCrate.Qty as PrevQty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As Crate,
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TabCustWiseCrate.TotalCrates_ItemWise Else 0 End As PrevCrate, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As Pouch, 
    'Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TabCustWiseCrate.qty Else 0 End As PrevPouch,
    '0 as ItemNetAmount,NetAmount as PrevItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc, 
    'TSPL_COMPANY_MASTER.Comp_Name as CompanyName,TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName,TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
    'TSPL_DEMAND_BOOKING_DETAIL.Item_Rate,ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0 As QTYLtr,TSPL_CUSTOMER_MASTER.Credit_Customer as CreditCust
    'from (select ROW_NUMBER() over (PARTITION BY xx.Cust_Code order by xx.Cust_Code, xx.ORDCol desc) as SNO, xx.Cust_Code,xx.ORDCol,sum(xx.TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(xx.TotalLtr_ItemWise) as TotalLtr,sum(xx.ItemNetAmount) as NetAmount,sum(xx.qty) as Qty
    'from(select innBD.Cust_Code, convert(varchar, InnBM.Document_Date, 102)+ case when innBD.ShiftType = 'Evening' then 'B' else 'A' end as ORDCol,innBD.TotalCrates_ItemWise, innBD.TotalLtr_ItemWise, innBD.ItemNetAmount,innBD.qty
    'from TSPL_DEMAND_BOOKING_MASTER as InnBM 
    'left outer join TSPL_DEMAND_BOOKING_DETAIL innBD on innBD.Document_No = InnBM.Document_No 
    'where 2 = 2  "
    '            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
    '                Qry += " and innBD.ShiftType='Evening' and ( CONVERT(date, InnBM.Document_Date, 103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value.AddDays(-1)) + "') "
    '            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
    '                Qry += " and innBD.ShiftType='Morning' and CONVERT(date, InnBM.Document_Date,103)='" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "'" ' or CONVERT(date, InnBM.Document_Date,103)<'" + clsCommon.GetPrintDate(txtDate.Value) + "') "
    '            End If
    '            Qry += " and innBD.Cust_Code is not null ) xx  
    'group by xx.Cust_Code,xx.ORDCol 
    ')  TabCustWiseCrate 
    'left join TSPL_Demand_Booking_Detail on TabCustWiseCrate.cust_Code=TSPL_Demand_Booking_Detail.cust_Code and TabCustWiseCrate.SNO=1
    'Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
    'inner join (select Against_DemandBooking_No,sum(isnull(TCSAmount,0)) as tcs_amt from TSPL_BOOKING_matser group by Against_DemandBooking_No) as TSPL_BOOKING_matser on TSPL_BOOKING_matser.Against_DemandBooking_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
    'Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code   And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
    'left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
    'Left Join (select Conversion_factor AS CFForLTR, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR') as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
    'Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
    'Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
    'Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
    'Left Join TSPL_COMPANY_MASTER on 2=2 "
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                Qry += " where TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            If rbtnMilk.Checked Then
    '                MilkProductBoth = "'Product'"
    '                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
    '            ElseIf rbtnProduct.Checked Then
    '                MilkProductBoth = "'Milk'"
    '                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
    '            Else
    '                MilkProductBoth = "'Milk','Product'"
    '                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
    '            End If
    '            Qry += " )XXFinal
    '  where XXFinal.Cust_Code in (select distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code from TSPL_DEMAND_BOOKING_MASTER left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No   where 2=2  and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code is not null "
    '            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
    '                Qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
    '            End If
    '            Qry += " )Group by XXFinal.Cust_Code,XXFinal.Sku_Seq )xx
    'left join ( select sum(XYZ.TCSAmount) as TCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No from (
    'select TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER
    'left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
    'where 2=2"
    '            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
    '                Qry += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
    '            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
    '                Qry += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
    '            End If

    '            Qry += " group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No) XYZ
    'group by XYZ.Cust_Code)  as tcs on xx.Cust_Code=tcs.Cust_Code left join (select sum(XYZ.pTCSAmount) as pTCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No from (
    'select (TSPL_BOOKING_MATSER.TCSAmount) as pTCSAmount ,(TSPL_BOOKING_MATSER.Against_DemandBooking_No) Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No where 2=2 "
    '            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
    '                Qry += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value.AddDays(-1)) + "') "
    '            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
    '                Qry += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
    '            End If
    '            Qry += "         
    'group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No
    ') XYZ
    'group by XYZ.Cust_Code
    ') as prevtcs on xx.Cust_Code=prevtcs.Cust_Code) xfinal "
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)


    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingTruckSheet", "Demand Booking Truck Sheet")
    '            frmCRV = Nothing
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, Me.Text)
    '        End Try
    '    End Sub
    Private Sub btnTrkShtSummaryRW_Click(sender As Object, e As EventArgs) Handles btnTrkShtSummaryRW.Click
        Try
            Dim whrcls As String = Nothing
            If clsCommon.myLen(txtPTSDateFrom.Value) > 0 Then
                whrcls = " where Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(txtPTSDateFrom.Value, "dd/MMM/yyyy") + "' "
            End If
            If clsCommon.myLen(ddlPTSShift.Text) > 0 Then
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" + ddlPTSShift.Text + "'"
            End If
            If clsCommon.myLen(txtCustMultFnd.arrValueMember) > 0 Then
                whrcls += " And TSPL_DEMAND_BOOKING_DETAIL.Cust_Code IN (" + clsCommon.GetMulcallString(txtCustMultFnd.arrValueMember) + ")"
            End If
            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
                whrcls += " And TSPL_ROUTE_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
            End If
            Dim MilkProductBoth As String = Nothing
            If rbtnMilk.Checked Then
                whrcls += " And TSPL_ITEM_MASTER.Is_FreshItem=1"
            ElseIf rbtnProduct.Checked Then
                whrcls += " And TSPL_ITEM_MASTER.Is_Ambient=1"
            End If
            Dim Qry As String = Nothing
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "TNK") = CompairStringResult.Equal Then
                Qry = " select TSPL_DEMAND_BOOKING_MASTER.Route_No ,TSPL_ROUTE_MASTER.Route_Desc, TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Qty, TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Is_FreshItem, TSPL_ITEM_MASTER.Sku_Seq, TSPL_DEMAND_BOOKING_MASTER.Document_Date, TSPL_DEMAND_BOOKING_MASTER.ShiftType, TSPL_ROUTE_MASTER.Route_Desc as Route_Desc, TSPL_DEMAND_BOOKING_MASTER.Route_No as Route_No, Isnull(TSPL_COMPANY_MASTER.Comp_Name,'Tonk Zila Dugdh Utpadak Sahakari Sangh Ltd.') as CompanyName, TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName, case when TSPL_ITEM_MASTER.Is_FreshItem=1 then ((TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ITEMDETAILCrate.CFForCrate) else 0 end as QtyCrate, case when TSPL_ITEM_MASTER.Is_FreshItem=1 then ((TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ITEMDETAILLTR.CFForLTR) else 0 end as QtyLTR,  case   WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_code = 'CUP' then qty * ITEMDETAILcup.Conversion_factor / ITEMDETAILCrate.CFForCrate when TSPL_ITEM_MASTER.Is_FreshItem=0 then (TSPL_DEMAND_BOOKING_DETAIL.Qty) else 0 end as PQty, TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount from TSPL_DEMAND_BOOKING_MASTER left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code Left Join (select Conversion_factor AS CFForCrate,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='CRATE') as ITEMDETAILCrate on ITEMDETAILCrate.Item_code=TSPL_ITEM_UOM_DETAIL.Item_Code Left Join (select Conversion_factor AS CFForLTR,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ITEMDETAILLTR on ITEMDETAILLTR.Item_code=TSPL_ITEM_UOM_DETAIL.Item_Code  Left Join (select Conversion_factor AS Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='CUP') as ITEMDETAILcup on ITEMDETAILcup.Item_code=TSPL_ITEM_UOM_DETAIL.Item_Code Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No Left Join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code Left Join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id Left Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "' " + whrcls
            Else
                Qry = "select Max(TSPL_DEMAND_BOOKING_DETAIL.ShiftType)ShiftType,
                                Max(TSPL_ITEM_MASTER.Short_Description)Short_Description,Sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) as Qty,Max(TSPL_DEMAND_BOOKING_DETAIL.Unit_code)Unit_code,
                                Case When Max(TSPL_DEMAND_BOOKING_DETAIL.Unit_Code)='Crate' Then Sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) Else 0 End As Crate,
                                Case When Max(TSPL_DEMAND_BOOKING_DETAIL.Unit_Code)='Pouch' Then Sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) Else 0 End As Pouch,
                                sum(TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount)as ItemNetAmount,Max(TSPL_DEMAND_BOOKING_MASTER.Document_Date)Document_Date,
                                Max(TSPL_ROUTE_MASTER.Route_Desc)Route_Desc,
                                Max(TSPL_DEMAND_BOOKING_MASTER.Route_No)Route_No,
                                Max(Isnull(TSPL_COMPANY_MASTER.Comp_Name,'Jaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.')) as CompanyName,
                                Max(TSPL_TRANSPORT_MASTER.Transporter_Name) as TranspoterName,
                                Max(TSPL_VEHICLE_MASTER.DriverName)Vehicle_Code,Max(TSPL_DEMAND_BOOKING_DETAIL.Item_Rate)Item_Rate,
            					ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
            					Sum(Convert(decimal(18,2),(TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ITEMDETAIL.CFForLTR)) As QTYLtr,
            max(TSPL_ITEM_MASTER.Sku_Seq) as Sku_Seq
                                from TSPL_DEMAND_BOOKING_MASTER
                                Left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
                                Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
            					Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
            					Left Join (select Conversion_factor AS CFForLTR,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_ITEM_UOM_DETAIL.Item_Code
                                Left Join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code
                                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No
                                Left Join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id
                                Left Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'" + whrcls
                Qry += " Group By TSPL_DEMAND_BOOKING_MASTER.Route_No,ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_DEMAND_BOOKING_DETAIL.Item_Code "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "TNK") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingRouteWiseTNK", "Demand Booking")

                Else
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingRouteWise", "Demand Booking")
                End If
                frmCRV = Nothing
            Else
                Throw New Exception("Data Not Found!")
            End If

            'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingRouteWise", "Demand Booking")
            'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBooking", "Demand Booking")
            'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptDemandBooking", "Demand Booking", "rptSubDemandBooking")

        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtMultPTSRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultPTSRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        txtMultPTSRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", txtMultPTSRoute.arrValueMember, txtMultPTSRoute.arrDispalyMember)
    End Sub
    Private Sub txtCustMultFnd__My_Click(sender As Object, e As EventArgs) Handles txtCustMultFnd._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustMultFnd.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustMultFnd.arrValueMember, txtCustMultFnd.arrDispalyMember)
    End Sub
    Private Sub btnCreditPrint_Click(sender As Object, e As EventArgs) Handles btnCreditPrint.Click
        Try
            Dim Qry As String = Nothing
            Dim BaseQry As String = Nothing
            Dim Item As String = Nothing
            Dim Item_LtrQty As String = Nothing
            Dim Item_Amount As String = Nothing
            Dim dt As DataTable = Nothing
            If clsCommon.myLen(txtCreditDateFrom.Value) <= 0 AndAlso clsCommon.myLen(txtCreditDateTo.Value) <= 0 Then
                Throw New Exception("Date Can't be blank.")
            End If
            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                Throw New Exception("Customer Can't be blank.")
            End If
            BaseQry = "(Select *,(Convert(decimal(18,2),(Qty*ConversionFactor)/CF)) As QtyInLtr,
                        Convert(decimal(18,2),(valueInRs/((Qty*ConversionFactor)/CF))) As RateLtr
                        from 
                        (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code As Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date As Invoice_Date,TSPL_ITEM_UOM_DETAIL.Conversion_Factor As ConversionFactor,ItemConvertLTR.Conversion_Factor As CF,TSPL_ITEM_MASTER.HSN_Code,TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,
                        (CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then 0 else (convert(Decimal(18,2), TSPL_SD_sale_invoice_DETAIL.Qty ))end) as Qty,TSPL_SD_sale_invoice_DETAIL.Unit_code,
                        (CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then 0 else ((case when TSPL_SD_sale_invoice_DETAIL.Sampling=1 then 0 else  TSPL_SD_sale_invoice_DETAIL.Amt_Less_Discount end)) end)  as valueInRs,
                        TSPL_SD_SHIPMENT_HEAD.DO_Item_Type As TaxableNonTaxable,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,(TSPL_CUSTOMER_MASTER.Add1+','+TSPL_CUSTOMER_MASTER.Add2+','+TSPL_CUSTOMER_MASTER.Add3) As CustAddress,
                        TSPL_CUSTOMER_MASTER.City_Code As Cust_City_Code,TSPL_STATE_MASTER.STATE_CODE As Cust_STATE_CODE,TSPL_STATE_MASTER.STATE_NAME As Cust_STATE_NAME,
                        TSPL_CUSTOMER_MASTER.Pin_Code As Cust_PinCode,(case when isnull(TSPL_CUSTOMER_MASTER.Phone1,'')<>'' then TSPL_CUSTOMER_MASTER.Phone1  when  isnull(TSPL_CUSTOMER_MASTER.Phone2,'')<>'' then + ', '+ TSPL_CUSTOMER_MASTER.Phone2 end) as Cust_Phone,TSPL_CUSTOMER_MASTER.GSTNO As Cust_GstNo,TSPL_SD_SALE_INVOICE_HEAD.Comp_Code,TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE,TSPL_ROUTE_MASTER.Route_Desc,TSPL_BOOKING_MATSER.Is_Credit_Customer
                        from TSPL_SD_sale_invoice_DETAIL
                        Left Outer Join TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE
                        left outer join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
                        Left Outer Join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                        left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                        left outer join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State 
                        left outer join TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code And  TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_sale_invoice_DETAIL.Unit_code 
                        Left Outer Join TSPL_DEPARTMENT_MASTER On TSPL_DEPARTMENT_MASTER.comp_code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code
                        Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No
                        Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
                        Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Against_DemandBooking_No=TSPL_DEMAND_BOOKING_MASTER.Document_No 
                        Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code And TSPL_ITEM_MASTER.Structure_Code=TSPL_SD_sale_invoice_DETAIL.Structure_Code 
                        left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConvertLTR on ItemConvertLTR.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code  
                        ) As abc
                        where Is_Credit_Customer='N' And Invoice_Date>=Convert(Date,'" + txtCreditDateFrom.Value + "',103) And Invoice_Date<=Convert(Date,'" + txtCreditDateTo.Value + "',103) And Cust_Code='" + clsCommon.myCstr(fndCustomer.Value) + "'"
            If rbtnTaxable.Checked Then
                BaseQry += " And TaxableNonTaxable='T' "
            Else
                BaseQry += " And TaxableNonTaxable='NT' "
            End If
            BaseQry += ")"
            Qry = "select Item_Code from " + BaseQry + "xxxx group by Item_Code Order by Item_Code"
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 5 Then
                Throw New Exception("Items can't be more than 5.")
            Else
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If clsCommon.myLen(Item) > 0 Then
                            Item += " Max(Case When Item_Code='" + clsCommon.myCstr(dt.Rows(i)("Item_Code")) + "' Then Item_Desc End) As Item" + clsCommon.myCstr(i + 1) + ","
                            Item += " Sum(Case When Item_Code='" + clsCommon.myCstr(dt.Rows(i)("Item_Code")) + "' Then  QtyInLtr Else 0 End) As Item" + clsCommon.myCstr(i + 1) + "_QtyInLtr,"
                            Item += " Max(Case When Item_Code='" + clsCommon.myCstr(dt.Rows(i)("Item_Code")) + "' Then RateLtr Else 0 End) As Item" + clsCommon.myCstr(i + 1) + "_RateLtr,"
                            Item_LtrQty += "+ Item" + clsCommon.myCstr(i + 1) + "_QtyInLtr"
                            Item_Amount += "+ Item" + clsCommon.myCstr(i + 1) + "_QtyInLtr*Item" + clsCommon.myCstr(i + 1) + "_RateLtr"
                        Else
                            Item = " Max(Case When Item_Code='" + clsCommon.myCstr(dt.Rows(i)("Item_Code")) + "' Then Item_Desc End) As Item" + clsCommon.myCstr(i + 1) + ","
                            Item += " Sum(Case When Item_Code='" + clsCommon.myCstr(dt.Rows(i)("Item_Code")) + "' Then QtyInLtr Else 0 End) As Item" + clsCommon.myCstr(i + 1) + "_QtyInLtr,"
                            Item += " Max(Case When Item_Code='" + clsCommon.myCstr(dt.Rows(i)("Item_Code")) + "' Then RateLtr Else 0 End) As Item" + clsCommon.myCstr(i + 1) + "_RateLtr,"
                            Item_LtrQty = "Item" + clsCommon.myCstr(i + 1) + "_QtyInLtr"
                            Item_Amount = "Item" + clsCommon.myCstr(i + 1) + "_QtyInLtr*Item" + clsCommon.myCstr(i + 1) + "_RateLtr"
                        End If
                    Next
                    Qry = Nothing
                    dt = Nothing
                    Qry = "Select '" + clsCommon.GetPrintDate(txtCreditDateFrom.Value, "dd-MMM-yyyy") + "' As FromDate,'" + clsCommon.GetPrintDate(txtCreditDateTo.Value, "dd-MMM-yyyy") + "' As ToDate,Main.*, 
                    convert(Decimal(18,2),(" + Item_LtrQty + ")) As TotalMilk,
                    convert(Decimal(18,2),(" + Item_Amount + ")) As TotalAmount,
                    TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.add1,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.add3,TSPL_COMPANY_MASTER.Email,
                    TSPL_COMPANY_MASTER.City_Code As Comp_CityCode,TSPL_COMPANY_MASTER.State As Comp_StateCode,TSPL_COMPANY_MASTER.Pincode As Comp_PinCode,TSPL_STATE_MASTER.STATE_NAME As Comp_StateNme,
                    case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,TSPL_COMPANY_MASTER.GSTReg_No
                    from 
                    (Select Max(Invoice_No)Invoice_No,FORMAT((Invoice_Date),'dd-MMM-yyyy')Date,FORMAT(Max(Invoice_Date),'MMM-yyyy')MonthDate, 
                    Max(Cust_Code)Cust_Code,Max(Customer_Name)Customer_Name,Max(CustAddress)CustAddress,Max(Cust_City_Code)Cust_City_Code,Max(Cust_STATE_CODE)Cust_STATE_CODE,Max(Cust_State_Name)Cust_State_Name,Max(Cust_PinCode)Cust_PinCode,
                    Max(Cust_Phone)Cust_Phone,Max(Cust_GstNo)Cust_GstNo,
                    Max(TaxableNonTaxable)TaxableNonTaxable,Max(Structure_Code)Structure_Code,Max(Item_Code)Item_Code,Max(Item_Desc)Item_Desc,Max(HSN_Code)HSN_Code,
                " + Item + "   
                    Max(Comp_Code)Comp_Code,Max(DEPARTMENT_CODE)DEPARTMENT_CODE,Max(Route_Desc)Route_Desc,Max(Is_Credit_Customer)Is_Credit_Customer
                    from " + BaseQry + " As xyz    Group By FORMAT((Invoice_Date),'dd-MMM-yyyy')) As Main 
                    left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =Main.Comp_Code 
                    Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State
                    Where convert(Decimal(18,2),(" + Item_LtrQty + "))>0 "
                    Qry += " Order By Invoice_No"
                    dt = clsDBFuncationality.GetDataTable(Qry)
                End If
            End If
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptTaxNonTaxableCustomerCreditReport", "Customer Credit Report")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReportType()
        IsReportTypeChanged = False
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))
        'dt.Rows.Add("Select", "SL")
        dt.Rows.Add("Matrix Fresh Sale", "MFS")
        dt.Rows.Add("Demand Wise", "DW")
        dt.Rows.Add("Truck Sheet", "TS")
        dt.Rows.Add("Milk Gate Pass Demand Detail", "MGPD")
        dt.Rows.Add("Product Gate Pass Demand Detail", "PGPD")
        dt.Rows.Add("Dairy Milk Gate Pass Detail", "DMGPD")
        dt.Rows.Add("Dairy Product Gate Pass Detail", "DPGPD")
        dt.Rows.Add("Milk Sale Report", "MSR")
        dt.Rows.Add("Product Sale Report", "PSR")
        dt.Rows.Add("Credit Sale Report", "CSR")
        dt.Rows.Add("Milk Product Demand Report", "MPDR")
        dt.Rows.Add("TCS", "TCS")
        dt.Rows.Add("Route Booth Wise", "RBW")
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "JPR") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
            dt.Rows.Add("Demand Sheet", "Demand Sheet")
        End If
        ddlReportType.DataSource = dt
        ddlReportType.DisplayMember = "Code"
        ddlReportType.ValueMember = "Value"
        IsReportTypeChanged = True
    End Sub
    Private Sub ReportDW()
        If IsReportTypeChanged = True Then
            If clsCommon.CompairString(ddlReportType.SelectedValue, "MFS") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = True
                chkBookingWise.Checked = True
                RadGroupBox7.Visible = False
                RadGroupBox3.Visible = True
                chkFilterByCreatedDate.Visible = True
                'chkSaleInvoiceWise.Visible = True
                pnlMilkPouch.Visible = True
                chkMilkPouch.Checked = True
                chkRouteSummary.Visible = True
                chkProduct.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                txtfndBooth.Visible = False
            End If
        End If
    End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReportType.SelectedIndexChanged
        If IsReportTypeChanged = True Then
            If clsCommon.CompairString(ddlReportType.SelectedValue, "DW") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                RadGroupBox7.Visible = False
                RadGroupBox3.Visible = True
                chkFilterByCreatedDate.Visible = True
                'chkSaleInvoiceWise.Visible = True
                pnlMilkPouch.Visible = True
                chkRouteSummary.Visible = True
                chkProduct.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                txtfndBooth.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                '--VARSHHA
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "RBW") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = True
                RadGroupBox3.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chkSaleInvoiceWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkBookingWise.Visible = False
                txtCustomerGroup.Visible = False
                lblCustomerGroup.Visible = False
                txtCustomer.Visible = False
                lblCustomer.Visible = False
                MyLabel2.Visible = False
                txtItemCode.Visible = False
                MyLabel3.Visible = True
                txtLorry.Visible = False
                lblLocation.Visible = False
                txtLocation.Visible = False
                MyLabel1.Visible = False
                txtZone.Visible = False
                MyLabel10.Visible = False
                TxtRoute.Visible = False
                MyLabel4.Visible = False
                TxtUOM.Visible = False
                MyLabel5.Visible = False
                txtBookingType.Visible = False
                MyLabel6.Visible = False
                TxtMultiCustomerCategory.Visible = False
                lblSubCategory.Visible = False
                ddlInvocieType.Visible = False
                RadGroupBox7.Visible = False
                RadGroupBox5.Visible = False
                If clsCommon.CompairString(ddlReportType.SelectedValue, "RBW") = CompairStringResult.Equal Then
                    RadGroupBox2.Location = New Point(21, 138)
                Else
                    RadGroupBox2.Location = New Point(607, 80)
                End If
                'txtfndCustomer.Location = New System.Drawing.Point(133, 2)
                'lblCustomer.Location = New System.Drawing.Point(21, 158)
                'txtCustomer.Location = New System.Drawing.Point(133, 158)
                ' MyLabel10.Location = New System.Drawing.Point(21, 261)
                'TxtRoute.Location = New System.Drawing.Point(133, 261)
                'RadGroupBox2.Location = New Point(21, 138)
                btnPrintRoutBoothwise.Visible = True
                btnTrkShtSummaryRW.Visible = False
                btnPrintTrkSht.Visible = False
                txtCustMultFnd.Visible = False
                txtfndBooth.Visible = True
                MyLabel13.Visible = True
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                '--VARSHA END
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "TS") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = True
                RadGroupBox3.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chkSaleInvoiceWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkBookingWise.Visible = False
                txtCustomerGroup.Visible = False
                lblCustomerGroup.Visible = False
                txtCustomer.Visible = False
                lblCustomer.Visible = False
                MyLabel2.Visible = False
                txtItemCode.Visible = False
                MyLabel3.Visible = False
                txtLorry.Visible = False
                lblLocation.Visible = False
                txtLocation.Visible = False
                MyLabel1.Visible = False
                txtZone.Visible = False
                MyLabel10.Visible = False
                TxtRoute.Visible = False
                MyLabel4.Visible = False
                TxtUOM.Visible = False
                MyLabel5.Visible = False
                txtBookingType.Visible = False
                MyLabel6.Visible = False
                TxtMultiCustomerCategory.Visible = False
                lblSubCategory.Visible = False
                ddlInvocieType.Visible = False
                RadGroupBox7.Visible = False
                RadGroupBox5.Visible = False
                If clsCommon.CompairString(ddlReportType.SelectedValue, "TS") = CompairStringResult.Equal Then
                    RadGroupBox2.Location = New Point(21, 138)
                Else
                    RadGroupBox2.Location = New Point(607, 80)
                End If
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                'RadGroupBox2.Location = New Point(21, 138)
                txtfndBooth.Visible = False
                btnPrintTrkSht.Visible = True
                btnTrkShtSummaryRW.Visible = True
                btnPrintRoutBoothwise.Visible = False
                txtCustMultFnd.Visible = False
                MyLabel13.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "MGPD") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chkSaleInvoiceWise.Visible = False
                RadGroupBox3.Visible = True
                RadGroupBox7.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "PGPD") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                'chkSaleInvoiceWise.Visible = False
                chkFilterByCreatedDate.Visible = False
                chkProduct.Visible = False
                RadGroupBox7.Visible = True
                RadGroupBox3.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                btnPrintRoutBoothwise.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "DPGPD") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chkSaleInvoiceWise.Visible = False
                RadGroupBox3.Visible = True
                RadGroupBox7.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "DMGPD") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chkSaleInvoiceWise.Visible = False
                RadGroupBox3.Visible = True
                RadGroupBox7.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "MSR") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                RadGroupBox7.Visible = False
                chkBookingWise.Visible = False
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "PSR") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                RadGroupBox7.Visible = False
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "MFS") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                RadGroupBox7.Visible = False
                RadGroupBox3.Visible = True
                chkFilterByCreatedDate.Visible = True
                'chksaleInvoicewise.Visible = True
                pnlMilkPouch.Visible = True
                chkRouteSummary.Visible = True
                chkProduct.Visible = True
                chkBookingWise.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "CSR") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                RadGroupBox3.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chksaleInvoicewise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkBookingWise.Visible = False
                txtCustomerGroup.Visible = False
                lblCustomerGroup.Visible = False
                txtCustomer.Visible = False
                lblCustomer.Visible = False
                MyLabel2.Visible = False
                txtItemCode.Visible = False
                MyLabel3.Visible = False
                txtLorry.Visible = False
                lblLocation.Visible = False
                txtLocation.Visible = False
                MyLabel1.Visible = False
                txtZone.Visible = False
                MyLabel10.Visible = False
                TxtRoute.Visible = False
                MyLabel4.Visible = False
                TxtUOM.Visible = False
                MyLabel5.Visible = False
                txtBookingType.Visible = False
                MyLabel6.Visible = False
                TxtMultiCustomerCategory.Visible = False
                lblSubCategory.Visible = False
                ddlInvocieType.Visible = False
                RadGroupBox7.Visible = False
                RadGroupBox5.Visible = True
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "MPDR") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chksaleInvoicewise.Visible = False
                RadGroupBox3.Visible = True
                RadGroupBox7.Visible = True
                txtCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = True
                txtItemCode.Visible = True
                MyLabel3.Visible = True
                txtLorry.Visible = True
                lblLocation.Visible = True
                txtLocation.Visible = True
                MyLabel1.Visible = True
                txtZone.Visible = True
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = True
                TxtUOM.Visible = True
                MyLabel5.Visible = True
                txtBookingType.Visible = True
                MyLabel6.Visible = True
                TxtMultiCustomerCategory.Visible = True
                lblSubCategory.Visible = True
                ddlInvocieType.Visible = True
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                lblCustomer.Location = New System.Drawing.Point(21, 158)
                txtCustomer.Location = New System.Drawing.Point(133, 158)
                MyLabel10.Location = New System.Drawing.Point(21, 261)
                TxtRoute.Location = New System.Drawing.Point(133, 261)
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "TCS") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chksaleInvoicewise.Visible = False
                RadGroupBox3.Visible = True
                RadGroupBox7.Visible = False
                txtCustomerGroup.Visible = False
                lblCustomerGroup.Visible = False
                txtCustomer.Visible = True
                lblCustomer.Visible = True
                MyLabel2.Visible = False
                txtItemCode.Visible = False
                MyLabel3.Visible = False
                txtLorry.Visible = False
                lblLocation.Visible = False
                txtLocation.Visible = False
                MyLabel1.Visible = False
                txtZone.Visible = False
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = False
                TxtUOM.Visible = False
                MyLabel5.Visible = False
                txtBookingType.Visible = False
                MyLabel6.Visible = False
                TxtMultiCustomerCategory.Visible = False
                lblSubCategory.Visible = False
                ddlInvocieType.Visible = False
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = True
                rddlTCSShift.Visible = True
                lbltcsShift.Location = New System.Drawing.Point(21, 87)
                rddlTCSShift.Location = New System.Drawing.Point(116, 87)
                lblCustomer.Location = New System.Drawing.Point(21, 111)
                txtCustomer.Location = New System.Drawing.Point(116, 111)
                MyLabel10.Location = New System.Drawing.Point(21, 138)
                TxtRoute.Location = New System.Drawing.Point(116, 138)
                txtfndCustomer.Visible = False
                txtFndRoute.Visible = False
                ToDate.Visible = True
                RadLabel2.Visible = True
                RadLabel1.Text = "From"
                RadGroupBox3.Size = New System.Drawing.Size(246, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Demand Sheet") = CompairStringResult.Equal Then
                RadGroupBox2.Visible = False
                chkBookingWise.Visible = False
                pnlMilkPouch.Visible = False
                chkRouteSummary.Visible = False
                chkProduct.Visible = False
                chkFilterByCreatedDate.Visible = False
                'chksaleInvoicewise.Visible = False
                RadGroupBox3.Visible = True
                RadGroupBox7.Visible = False
                txtCustomerGroup.Visible = False
                lblCustomerGroup.Visible = False
                txtCustomer.Visible = False
                lblCustomer.Visible = True
                MyLabel2.Visible = False
                txtItemCode.Visible = False
                MyLabel3.Visible = False
                txtLorry.Visible = False
                lblLocation.Visible = False
                txtLocation.Visible = False
                MyLabel1.Visible = False
                txtZone.Visible = False
                MyLabel10.Visible = True
                TxtRoute.Visible = True
                MyLabel4.Visible = False
                TxtUOM.Visible = False
                MyLabel5.Visible = False
                txtBookingType.Visible = False
                MyLabel6.Visible = False
                TxtMultiCustomerCategory.Visible = False
                lblSubCategory.Visible = False
                ddlInvocieType.Visible = False
                RadGroupBox5.Visible = False
                lbltcsShift.Visible = False
                rddlTCSShift.Visible = False
                lbltcsShift.Location = New System.Drawing.Point(21, 87)
                rddlTCSShift.Location = New System.Drawing.Point(116, 87)
                txtfndCustomer.Visible = True
                txtFndRoute.Visible = True
                lblCustomer.Location = New System.Drawing.Point(21, 111)
                txtfndCustomer.Location = New System.Drawing.Point(116, 111)
                MyLabel10.Location = New System.Drawing.Point(21, 138)
                txtFndRoute.Location = New System.Drawing.Point(116, 138)
                TxtRoute.Visible = False
                ToDate.Visible = False
                RadLabel2.Visible = False
                RadLabel1.Text = "Date"
                lbltcsShift.Visible = True
                rddlTCSShift.Visible = True
                lbltcsShift.Location = New System.Drawing.Point(21, 87)
                rddlTCSShift.Location = New System.Drawing.Point(116, 87)
                RadGroupBox3.Size = New System.Drawing.Size(132, 42)
                btnPrintRoutBoothwise.Visible = False
                txtfndBooth.Visible = False
            End If
        End If
    End Sub
    Private Sub fndCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomer._MYValidating
        Try
            Dim qry As String = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
            fndCustomer.Value = clsCommon.ShowSelectForm("@Customer", qry, "Code", "", fndCustomer.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtFndRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFndRoute._MYValidating
        Try
            Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER "
            txtFndRoute.Value = clsCommon.ShowSelectForm("@Route", qry, "Code", "", txtFndRoute.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtfndCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtfndCustomer._MYValidating
        Try
            Dim qry As String = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
            txtfndCustomer.Value = clsCommon.ShowSelectForm("@Customer", qry, "Code", "", txtfndCustomer.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrintRoutBoothwise_Click(sender As Object, e As EventArgs) Handles btnPrintRoutBoothwise.Click
        Try
            Dim MilkProductBoth As String = Nothing
            Dim Qry As String = Nothing
            Qry = "select xx.*
     ,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull((select sum(ItemNetAmount) as netamt  from TSPL_DEMAND_BOOKING_MASTER left join  TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
	 left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
where  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ddlPTSShift.Text + "'  and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "')"
            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
                Qry += "and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
            End If
            Qry += "and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtfndBooth.Value) + "'"
            Qry += "and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=xx.Cust_Code ),0 ) + isnull((xx.PrevItemNetAmount),0) ) else 0 end) else 0 end as AmountBE,
     case when xx.SNO=1 then xx.Crate_Collect else 0 end as TotalCollectCrate,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull(tcs.TCSAmount,0)+isnull(prevtcs.pTCSAmount,0)) else 0 end) else 0 end as TotalTCSAmt
      from ( select XXFinal.Cust_Code as Cust_Code, max(XXFinal.ShiftType) as ShiftType, XXFinal.Sku_Seq as Sku_Seq ,max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Short_Description) as Short_Description, sum(XXFinal.Qty) as Qty, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.Crate) as Crate, max(XXFinal.Pouch) as Pouch, sum(XXFinal.ItemNetAmount) as ItemNetAmount,max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc,max(XXFinal.PrevCrate) as Crate_Collect, max(XXFinal.CompanyName) as CompanyName ,max(XXFinal.TranspoterName) as TranspoterName,max(XXFinal.DriverName) as DriverName,max(XXFinal.Vehicle_No) as Vehicle_No, max(XXFinal.Item_Rate) as Item_Rate, max(XXFinal.CFForLTR) as CFForLTR, max(XXFinal.Conversion_Factor) as Conversion_Factor, sum(XXFinal.QTYLtr) as QTYLtr,max(XXFinal.PrevItemNetAmount) as PrevItemNetAmount,ROW_NUMBER() over (Partition by Cust_Code order by Cust_Code) as SNO
    from
    (
    select 
      TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, 
      TSPL_DEMAND_BOOKING_DETAIL.ShiftType, 
      TSPL_ITEM_MASTER.Sku_Seq, 
      TSPL_DEMAND_BOOKING_MASTER.Document_Date, 
      TSPL_ITEM_MASTER.Short_Description, 
      TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty, 
      0 as PrevQty,
      TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
      Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise Else 0 End As Crate, 
      Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As PrevCrate, 
      Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End As Pouch, 
        Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As PrevPouch,
      TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount, 
        0 as PrevItemNetAmount,
      TSPL_DEMAND_BOOKING_MASTER.Route_No, 
      TSPL_ROUTE_MASTER.Route_Desc, 
      
        TSPL_COMPANY_MASTER.Comp_Name as CompanyName, 
      TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName, 
      TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
      TSPL_DEMAND_BOOKING_DETAIL.Item_Rate, 
      ITEMDETAIL.CFForLTR, 
      TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 
      Convert(
        decimal(18, 2), 
        (
          TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor
        )/ ITEMDETAIL.CFForLTR
      ) As QTYLtr
    from 
      TSPL_DEMAND_BOOKING_DETAIL 
      Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
	  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
      Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
      Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
      And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
      Left Join (
        select 
          Conversion_factor AS CFForLTR, 
          TSPL_ITEM_UOM_DETAIL.Item_code 
        from 
          TSPL_ITEM_UOM_DETAIL 
        where 
          UOM_code = 'LTR'
      ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
      Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
      Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
      Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
      Left Join TSPL_COMPANY_MASTER on TSPL_DEMAND_BOOKING_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code
    where 
      TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ddlPTSShift.Text + "' 
      and (
        CONVERT(
          date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 
          103
        )= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "'
      ) "
            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
                Qry += "and TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
            End If
            Qry += "and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtfndBooth.Value) + "'"
            If rbtnMilk.Checked Then
                MilkProductBoth = "'Product'"
                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
            ElseIf rbtnProduct.Checked Then
                MilkProductBoth = "'Milk'"
                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
            Else
                MilkProductBoth = "'Milk','Product'"
                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
            End If
            Qry += "  union all
      select 
      TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, 
      '" + ddlPTSShift.Text + "'  as ShiftType, 
      TSPL_ITEM_MASTER.Sku_Seq, 
      '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "' as Document_Date, 
     TSPL_ITEM_MASTER.Short_Description, 
      0 as Qty, 
      TabCustWiseCrate.Qty as PrevQty,
      TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
      Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As Crate, 
      Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TabCustWiseCrate.TotalCrates_ItemWise Else 0 End As PrevCrate, 
      Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As Pouch, 
        Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TabCustWiseCrate.qty Else 0 End As PrevPouch,
      0 as ItemNetAmount, 
        NetAmount as PrevItemNetAmount,
      TSPL_DEMAND_BOOKING_MASTER.Route_No, 
      TSPL_ROUTE_MASTER.Route_Desc, 
      Isnull(
        TSPL_COMPANY_MASTER.Comp_Name, 'Jaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.'
      ) as CompanyName, 
      TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName, 
      TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
      TSPL_DEMAND_BOOKING_DETAIL.Item_Rate, 
      ITEMDETAIL.CFForLTR, 
      TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 
      0 As QTYLtr
    from 
      (
        select 
          ROW_NUMBER() over (
            PARTITION BY xx.Cust_Code 
            order by 
              xx.Cust_Code, 
              xx.ORDCol desc
          ) as SNO, 
          xx.Cust_Code, 
          xx.ORDCol, 
          sum(xx.TotalCrates_ItemWise) as TotalCrates_ItemWise, 
          sum(xx.TotalLtr_ItemWise) as TotalLtr, 
          sum(xx.ItemNetAmount) as NetAmount, 
    	  sum(xx.qty) as Qty
        from 
          (
            select 
              innBD.Cust_Code, 
              convert(
                varchar, InnBM.Document_Date, 102
              )+ case when innBD.ShiftType = 'Evening' then 'B' else 'A' end as ORDCol, 
              innBD.TotalCrates_ItemWise, 
              innBD.TotalLtr_ItemWise, 
              innBD.ItemNetAmount,innBD.qty
            from 
              TSPL_DEMAND_BOOKING_MASTER as InnBM 
              left outer join TSPL_DEMAND_BOOKING_DETAIL innBD on innBD.Document_No = InnBM.Document_No 
            where 
              2 = 2  "
            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
                Qry += " and innBD.ShiftType='Evening' and ( CONVERT(date, InnBM.Document_Date, 103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value.AddDays(-1)) + "') "
            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
                Qry += " and innBD.ShiftType='Morning' and CONVERT(date, InnBM.Document_Date,103)='" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "'" ' or CONVERT(date, InnBM.Document_Date,103)<'" + clsCommon.GetPrintDate(txtDate.Value) + "') "
            End If
            Qry += " and innBD.Cust_Code is not null ) xx  
        group by 
          xx.Cust_Code, 
          xx.ORDCol
      )  TabCustWiseCrate 
        left join TSPL_Demand_Booking_Detail on TabCustWiseCrate.cust_Code=TSPL_Demand_Booking_Detail.cust_Code and TabCustWiseCrate.SNO=1
      Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
    inner join (select Against_DemandBooking_No,sum(isnull(TCSAmount,0)) as tcs_amt from TSPL_BOOKING_matser group by Against_DemandBooking_No) as TSPL_BOOKING_matser on TSPL_BOOKING_matser.Against_DemandBooking_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
      Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
      Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
      And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
      Left Join (
        select 
          Conversion_factor AS CFForLTR, 
          TSPL_ITEM_UOM_DETAIL.Item_code 
        from 
          TSPL_ITEM_UOM_DETAIL 
        where 
          UOM_code = 'LTR'
      ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
      Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
      Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
      Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
	  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
      Left Join TSPL_COMPANY_MASTER on TSPL_DEMAND_BOOKING_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code "
            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
                Qry += " where TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
            End If
            Qry += "and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtfndBooth.Value) + "'"
            If rbtnMilk.Checked Then
                MilkProductBoth = "'Product'"
                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
            ElseIf rbtnProduct.Checked Then
                MilkProductBoth = "'Milk'"
                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
            Else
                MilkProductBoth = "'Milk','Product'"
                Qry += " And TSPL_ITEM_MASTER.Structure_Code NOT IN (" + MilkProductBoth + ")"
            End If
            ' where TSPL_ROUTE_MASTER.Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "'
            Qry += " )XXFinal
      where XXFinal.Cust_Code in (select Cust_Code from TSPL_Customer_Master  where 2=2 "
            If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
                Qry += " and Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
            End If
            ' Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + " ')
            Qry += " )Group by XXFinal.Cust_Code,XXFinal.Sku_Seq )xx
    left join (
    select 
     sum(XYZ.TCSAmount) as TCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No
    	  from(
     select TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER
    left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
	left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code
    where TSPL_BOOKING_MATSER.GatePass_Type='" + ddlPTSShift.Text + "' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') 
    group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No) XYZ
    group by XYZ.Cust_Code
      )  as tcs on xx.Cust_Code=tcs.Cust_Code
    left join (
    select 
     sum(XYZ.pTCSAmount) as pTCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No
    	  from(select (TSPL_BOOKING_MATSER.TCSAmount) as pTCSAmount ,(TSPL_BOOKING_MATSER.Against_DemandBooking_No) Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code
    		  from TSPL_BOOKING_MATSER
    left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
    where 2=2 "
            If clsCommon.CompairString(ddlPTSShift.Text, "Morning") = CompairStringResult.Equal Then
                Qry += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value.AddDays(-1)) + "') "
            ElseIf clsCommon.CompairString(ddlPTSShift.Text, "Evening") = CompairStringResult.Equal Then
                Qry += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtPTSDateFrom.Value) + "') "
            End If
            Qry += "         
    group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No
    ) XYZ
    group by XYZ.Cust_Code
    ) as prevtcs on xx.Cust_Code=prevtcs.Cust_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandRoutBoothWise", "Demand Rout Booth Wise")
            frmCRV = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtfndBooth__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtfndBooth._MYValidating
        Try
            Dim QRY As String = "select TSPL_DEMAND_BOOKING_dETAIL.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Name] from TSPL_DEMAND_BOOKING_MASTER 
                                 left outer join TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_no
                                 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_dETAIL.Cust_Code"
            Dim strWhrcls As String = "TSPL_DEMAND_BOOKING_MASTER.Route_No IN (" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ") and 	 convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) = convert(date,'" & txtPTSDateFrom.Value & "',103) and TSPL_DEMAND_BOOKING_MASTER.ShiftType = '" & ddlPTSShift.Text & "' and  TSPL_DEMAND_BOOKING_DETAIL.qty > 0 
                                       GROUP BY TSPL_DEMAND_BOOKING_dETAIL.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name"
            txtfndBooth.Value = clsCommon.ShowSelectForm("@Customers", QRY, "Code", strWhrcls, txtfndBooth.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGPSummaryRouteWise_Click(sender As Object, e As EventArgs) Handles btnGPSummaryRouteWise.Click
        Try
            PrintGPSummaryRW()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub PrintGPSummaryRW()
        Try
            If clsCommon.myLen(ddlPTSShift.Text) > 0 Then
                Dim qry As String = "select max(TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name,( max(TSPL_COMPANY_MASTER.Add1) +max(TSPL_COMPANY_MASTER.Add2) + max(TSPL_COMPANY_MASTER.Add3)) as Company_Address,TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No,
max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Desc,max(TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType) as ShiftType,max(TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date) as Supply_Date,
max(TSPL_VEHICLE_MASTER.Vehicle_No) as Vehicle_No,TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code,
max(TSPL_ITEM_MASTER.Short_Description) as Short_Description,TSPL_ITEM_MASTER.Sku_Seq,
TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code,sum(TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty) as Qty,
case when max(TSPL_ITEM_MASTER.Is_FreshItem)=1 then (sum(TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty)*max(ItemConvinUOM.Conversion_Factor)/max(ItemConvInLtr.Conversion_Factor)) when max(TSPL_ITEM_MASTER.Is_Ambient)=1 then (sum(TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty)*max(ItemConvinUOM.Conversion_Factor)/max(ItemConvInKG.Conversion_Factor)) else (sum(TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty)*max(ItemConvinUOM.Conversion_Factor)/max(ItemConvInLtr.Conversion_Factor)) end as [Qty In LTR/KG],
max(TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate) as TotalCrate
from  TSPL_DAIRYSALE_GATEPASS_MASTER
left join TSPL_DAIRYSALE_GATEPASS_DETAIL on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode
left join TSPL_ROUTE_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
left join TSPL_VEHICLE_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id=TSPL_VEHICLE_MASTER.Vehicle_Id
left join TSPL_COMPANY_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code
left join TSPL_ITEM_UOM_DETAIL as ItemConvInLtr on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code=ItemConvInLtr.Item_Code and ItemConvInLtr.UOM_Code='LTR'
left join TSPL_ITEM_UOM_DETAIL as ItemConvInKG on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code=ItemConvInKG.Item_Code and ItemConvInKG.UOM_Code='KG'
left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code=ItemConvinUOM.Item_Code and TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code=ItemConvinUOM.UOM_Code
where 2=2 and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date)='" & clsCommon.GetPrintDate(txtPTSDateFrom.Value) & "' and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType='" & ddlPTSShift.Text & "' "
                If clsCommon.myLen(txtMultPTSRoute.arrValueMember) > 0 Then
                    qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No in(" + clsCommon.GetMulcallString(txtMultPTSRoute.arrValueMember) + ")"
                End If
                If rbtnMilk.Checked Then
                    qry += " and TSPL_ITEM_MASTER.Is_FreshItem=1 "
                ElseIf rbtnProduct.Checked Then
                    qry += " and TSPL_ITEM_MASTER.Is_Ambient=1 "
                End If
                qry += "group by TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No,TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code,TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code,TSPL_ITEM_MASTER.Sku_Seq "

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptGPRouteWiseSummary", "GatePass Route Wise")
                    frmCRV = Nothing
                Else
                    Throw New Exception("No Data Found!")
                End If

            Else
                Throw New Exception("Please Select Shift.")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



End Class
