Imports common
Imports System.IO
Public Class rptDemandReport
    Inherits FrmMainTranScreen
    Dim isSchemeItem As Boolean = False
    Dim IsReportTypeChanged As Boolean = False
    Dim strQry As String = ""
    Dim dt As DataTable
    Dim ApplyMilkPouchPrint As Boolean = False
    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        Try
            'Sanjay,Add Customer Category 
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If

            If chkBookingWise.Checked = True Then
                If (chkMilkPouch.Checked = True OrElse chkProduct.Checked = True) AndAlso chkRouteSummary.Checked = True Then
                    Throw New Exception("Select only one check box at a time (Milk Pouch/Product) or Route Summary Print")
                End If
            End If
            If chkMilkPouch.Checked = True And chkProduct.Checked = True Then
                Throw New Exception("Select only one check box at a time Milk Pouch/Product")
            End If

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
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
            If chkBookingWise.Checked Or chkFirstAndSecondSpell.Checked = True Then
                ' strWhrClause = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                'strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
                'strWhrRoutSummaryPrint = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause = "  and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
                strWhrRoutSummaryPrint = "  and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "

            ElseIf chkSaleInvoiceWise.Checked Then
                strWhrClause = " and convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_SD_SALE_INVOICE_HEAD." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            ElseIf chkSummary.Checked Then
                'strWhrClause = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                'strWhrClause2 = "  and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
                strWhrClause = " and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = "  and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "

            Else
                strWhrClause = " and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
                strWhrClause2 = " and convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_DEMAND_BOOKING_MASTER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            End If
            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                strWhrClause += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + sss + ")  "
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If
            If chkBookingWise.Checked = True Then
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in (" + ss + ")  "
                End If

            Else
                If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                    Dim ss As String = clsCommon.GetMulcallString(TxtRoute.arrValueMember)
                    Dim sss As String = ss.Replace("'", "''")
                    strWhrClause += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in (" + sss + ")  "
                    strWhrClause2 += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in (" + ss + ")  "
                End If
            End If
            If chkBookingWise.Checked = True AndAlso chkGatePass.Checked = True Then
                strWhrClause += " and TSPL_BOOKING_MATSER.AgainstGatePass=1 "
                strWhrClause2 += " and TSPL_BOOKING_MATSER.AgainstGatePass=1 "
            End If
            If chkSummary.Checked = True Then
                Dim qry As String = " Select Final.Document_No as [Document No], max(Final.Document_Date) as [Document Date],max([Dispatch No(NT)]) as [Dispatch No(NT)] ,max ([Invoice No(NT)]) as [Invoice No(NT)],max([Dispatch No(T)]) as [Dispatch No(T)] ,max ([Invoice No(T)]) as [Invoice No(T)],max(Case when   convert (decimal(18,2),final.Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],max(Final.Booking_Type) as [Booking Type],Case when max(Final.Cust_Group_Code) in ('Distributor','Other') Or max(AgainstGatePass) =1 then 'NA' else case when  max(TruckSheetGenerate) =1 then 'Yes' else 'No' end end as TruckSheetGenerate, Case when  max(AgainstGatePass) =1 then 'Yes' else '' end as AgainstGatePass,Case when max(is_Cancelled) =1 then 'Yes' else '' end Cancelled,max(Final.Payment_Mode) as [Payment Mode],max(Final.location_code) as [Location Code],max(Final.Location_Desc) as [Location Desc] ,max(Final.[Customer Code]) as [Customer Code],max(Final.WdName) as [Customer Name],max(Final.Booth) as [Booth Name],max(Final.[Customer Category Code]) as [Customer Category Code],max(Final.Cust_Group_Code) as [Cust Group Code],max(Final.[Cust Group Desc]) as [Cust Group Desc],max(Final.[Route No]) as [Route No],max(Final.Route_Desc) as [Route Desc] ,max(Final.Zone_Code) as [Zone Code],max( Final.Zone_Desc) as [Zone Desc],max([VEHICLE NO]) as [VEHICLE NO],max(Final.Created_By) as [Created By],max( Final.Created_Date) as [Created Date],max(Final.Modified_By) as [Modified By],max(Final.Modified_Date) as [Modified Date] ,max(Final.DocumentAmount) as [Amount] , max(Posted) as [Status] From ( " &
                " Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,
                TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName,TSPL_CUSTOMER_MASTER.OldName as [Booth] ,TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No],TSPL_ROUTE_MASTER.Route_Desc , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code],TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_ZONE_MASTER.Description as Zone_Desc,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],Case when TSPL_BOOKING_MATSER.Posted = 1 then 'Posted' else 'Not Posted' end [Posted] From TSPL_BOOKING_DETAIL " &
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
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    gv1.DataSource = dtgv
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

                End If
            Else
                'sanjay BHA/24/08/18-000480 Show only item which is in transaction
                Dim ItemInUse As String = ""
                If chkBookingWise.Checked = True Or chkFirstAndSecondSpell.Checked = True Then
                    'ItemInUse = " TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                    ItemInUse = " TSPL_DEMAND_BOOKING_DETAIL 
                    Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                    Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                    Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code
                    Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code
                    left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code
                    where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "

                    'If isSchemeItem = False Then
                    '    ItemInUse += " and Scheme_Item='N' "
                    'End If
                    If chkFirstAndSecondSpell.Checked = True Then
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
                    'ItemInUse = " TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                    ItemInUse = "  TSPL_DEMAND_BOOKING_DETAIL
  Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
  Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
  Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.Location_Code 
  left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code 
  where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "

                    'If isSchemeItem = False Then
                    '    ItemInUse += " and Scheme_Item='N' "
                    'End If
                    ItemInUse += strWhrClause2
                    ItemInUse += " order by Alies_Name "
                End If
                Dim strAliasCol As String = "( TSPL_ITEM_MASTER.Alies_Name )"
                'If ChkDayWiseSummary.Checked Then
                '    strAliasCol = "( case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_ITEM_MASTER.Alies_Name else 'BI Products' end )"
                'End If
                Dim strSchemeItem As String = Nothing
                'strSchemeItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol + "+'(S)') as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
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
                            MainQuery = "select convert(varchar, Document_Date ,103) as [Document Date],Document_Date as ddFilter, * from ( select    convert(date, zpivot.Document_Date,103) as Document_Date, " + strItem2WithSum + " ,(" + strTotal + ") as [Total],Sum(isnull([BI Products],0)) as [BI Products], (" + strGrandTotalWithoutScheme + ") as [Grand Total] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,CAST(sum(QtyLtr) as decimal(18,2)) as qty,sum(QtyLtr) as QtyLtr from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_ITEM_MASTER.Alies_Name else 'BI Products' end As [Description] " &
                                        ",TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                      "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                      " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  " &
                                      " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                      "  where 2=2 " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by convert(date, zpivot.Document_Date,103)  )x order by ddFilter"
                        ElseIf chkSaleInvoiceWise.Checked Then
                            MainQuery = "select convert(varchar, Document_Date ,103) as [Document Date],Document_Date as ddFilter, * from ( select    convert(date, zpivot.Document_Date,103) as Document_Date, " + strItem2WithSum + " ,(" + strTotal + ") as [Total],Sum(isnull([BI Products],0)) as [BI Products], (" + strGrandTotalWithoutScheme + ") as [Grand Total] from  (select zzz.Document_Date,zzz.Description,CAST(sum(QtyLtr) as decimal(18,2)) as qty from (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Document_No,Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No] ,case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_ITEM_MASTER.Alies_Name else 'BI Products' end As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_SD_SALE_INVOICE_DETAIL.Qty  as Qty, TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],(CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Qty=0 THEN 0 ELSE (TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr  From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' " &
                                        " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  where 2=2 " + strWhrClause2 + " )zzz where 1=1 group by   zzz.Document_Date,zzz.Description  ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date )x order by ddFilter "
                        End If
                    ElseIf chkBookingWise.Checked = True Then
                        Dim strCreateConv As String = ""
                        Dim strIsMilkPouch As String = ""
                        If chkMilkPouch.Checked = True Then
                            If rdbLtr.Checked = True Then
                            Else
                                strCreateConv = " convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) "
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
                            MainQuery = " Select  max(Document_No) As [Document No], (Document_Date) As [Document Date], max([Time]) As [Time],
                            Case When max([Group]) In ('Distributor', 'Other') Or max(AgainstGatePass) = 'Y' then 'NA' else case when max(TruckSheetGenerate) = 'Y' then 'Yes' else 'No' end end as TruckSheetGenerate, 
                            Case when max(AgainstGatePass) = 'Y' then 'Yes' else '' end as AgainstGatePass,
                            max([VEHICLE NO]) as [VEHICLE NO], max([Customer Code]) As [Customer Code], max([WdName]) As [Customer Name], max(Booth) As Booth, 
                            max([Group]) as [Group], max([Cust Group Desc]) as [Cust Group Desc], max([Customer Category Code]) as [Customer Category Code], max([Zone]) as [Zone], max([Route No]) as [Route No], max( [Booking Time(AM / PM) ] ) As [Booking Time(AM / PM) ],
                            Max(Created_By) As [Created By],Max(SourceBy)SourceBy , max(Created_Date) As [Created Date], max(Modified_By) As [Modified By], max(Modified_Date) As [Modified Date], 
                            sum(DocumentAmount) As [Amount]," + strItem2WithSum + " ,(" + strGrandTotalWithoutScheme + ") As [Grand Total] , 
                            cast(SUM(QtyLtr) As Decimal(18, 2) ) As [Total In Ltr]
                            from ( select max(zzz.[VEHICLE NO])[VEHICLE NO], max(zzz.[WdName])[WdName],  max(zzz.Cust_Group_Code) as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],
                            max([Customer Category Code]) As [Customer Category Code],max(zzz.Zone_Code) As [Zone], max(zzz.[Route No])[Route No], max(zzz.item_code) As item_code,  max(zzz.[Document_No])[Document_No], (Document_Date) As Document_Date, 
                            max([Time]) As [Time],
                            max(TruckSheetGenerate) As TruckSheetGenerate, max(AgainstGatePass) As AgainstGatePass, 
                            max(GatePass_Type) As [Booking Time(AM / PM) ], Max(Created_By) As Created_By,Max(SourceBy)SourceBy , max(Created_Date) As Created_Date, max(Modified_By) As Modified_By,
                            max(Modified_Date) As Modified_Date, sum(ItemNetAmount) As DocumentAmount, max(Booth) As [Booth],  max([Customer Code]) As [Customer Code], 
                            (zzz.Description)Description, sum(qty) as qty, sum(QtyLtr) as QtyLtr from "
                        Else
                            MainQuery = " select Document_No as [Document No], max(Document_Date) as [Document Date], max([Time]) as [Time], 
                                          Case when [Group] in ('Distributor', 'Other') Or max(AgainstGatePass) = 'Y' then 'NA' else case when max(TruckSheetGenerate) = 'Y' then 'Yes' else 'No' end end as TruckSheetGenerate, 
                                          Case when max(AgainstGatePass) = 'Y' then 'Yes' else '' end as AgainstGatePass,
                                           [VEHICLE NO], max([Customer Code]) as [Customer Code], [WdName] as [Customer Name], 
                                          max(Booth) as Booth, [Group], [Cust Group Desc], [Customer Category Code], [Zone], [Route No], max( [Booking Time(AM / PM) ] ) as [Booking Time(AM / PM) ],
                                          Max(Created_By) as [Created By],
 Max(SourceBy)SourceBy ,max(Created_Date) as [Created Date], max(Modified_By) as [Modified By], max(Modified_Date) as [Modified Date],
                                          sum(DocumentAmount) as [Amount]," + strItem2WithSum + " ,(" + strGrandTotalWithoutScheme + ") as [Grand Total] , 
                                          cast( SUM(QtyLtr) as decimal(18, 2) ) as [Total In Ltr] 
                                          from ( select max(zzz.item_code) as item_code, zzz.Document_No, max(Document_Date) as Document_Date, max([Time]) as [Time], 
                                          max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,
                                           max(GatePass_Type) as [Booking Time(AM / PM) ], Max(Created_By) as Created_By,Max(SourceBy)SourceBy ,
                                           max(Created_Date) as Created_Date, max(Modified_By) as Modified_By, max(Modified_Date) as Modified_Date, sum(ItemNetAmount) as DocumentAmount,
                                           max(Booth) as [Booth], max([Customer Category Code]) as [Customer Category Code], max([Customer Code]) as [Customer Code],
                                           zzz.[VEHICLE NO], zzz.[WdName], zzz.Description, zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],
                                           zzz.Zone_Code as [Zone], zzz.[Route No], sum(qty) as qty, sum(QtyLtr) as QtyLtr from "
                        End If
                        '                  If rbtnDateWise.Checked = True Then
                        '                      MainQuery = "group by zzz.Document_Date,zzz.Description	) as s pivot ( sum(Qty) for Description in ([CHAI SATHI 1 LTR],[DAHI 200],[DTM 200],[DTM 500],[DTM 6L],[FCM 500 ML],[FCM 6 LTR],[NGHEE 15 KG],[P-CHAACH 500],[PNR 200],[SKM 6L],[SM 500],[TM 1L],[TM 500],[TM 6L] ) ) as zpivot group by zpivot.Document_Date
                        '"
                        '                  End If

                        MainQuery += " ( Select isnull( TSPL_DEMAND_BOOKING_MASTER.ShiftType, '' ) as GatePass_Type, TSPL_DEMAND_BOOKING_MASTER.Document_No,
                                      Convert (date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Document_Date,case when LTRIM( RIGHT( CONVERT( VARCHAR(20),
                                     TSPL_DEMAND_BOOKING_MASTER.Document_Date, 100 ), 7 ) ) = '12:00AM' then LTRIM( RIGHT( CONVERT( VARCHAR(20), 
                                     TSPL_DEMAND_BOOKING_MASTER.Created_Date, 100 ), 7 ) ) else LTRIM( RIGHT( CONVERT( VARCHAR(20), TSPL_DEMAND_BOOKING_MASTER.Document_Date, 100 ), 7 ) ) end as [Time],
                                     Convert ( varchar, TSPL_DEMAND_BOOKING_DETAIL.IsTruckSheetGenerated ) as TruckSheetGenerate,Convert ( varchar, TSPL_DEMAND_BOOKING_DETAIL.IsGatePassGenerated ) as AgainstGatePass,
                                    ( format ( TSPL_DEMAND_BOOKING_MASTER.Created_Date, 'HH' ) + '.' + format ( TSPL_DEMAND_BOOKING_MASTER.Created_Date, 'mm' ) ) as Created_Date_Time,
                                    case when TSPL_DEMAND_BOOKING_DETAIL.Created_By='' then TSPL_DEMAND_BOOKING_MASTER.Created_By else TSPL_DEMAND_BOOKING_DETAIL.Created_By end as Created_By, 
case when Isnull(TSPL_DEMAND_BOOKING_DETAIL.Created_By,'')='' then 'Erp' else 'MOBLIE'  end as [SourceBy], 
convert ( varchar, TSPL_DEMAND_BOOKING_MASTER.Created_Date, 103 ) as Created_Date, TSPL_DEMAND_BOOKING_MASTER.Modified_By,
                                    Convert ( varchar, TSPL_DEMAND_BOOKING_MASTER.Modified_Date, 103 ) as Modified_Date, TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,
                                    TSPL_ITEM_MASTER.Sku_Seq, TSPL_DEMAND_BOOKING_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, 
                                    isnull( TSPL_CUSTOMER_MASTER.cust_category_code, '' ) as [Customer Category Code], TSPL_DEMAND_BOOKING_DETAIL.Cust_Code As [Customer Code],
                                    TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_DEMAND_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_DEMAND_BOOKING_DETAIL.Unit_code as UOM,
                                    TSPL_DEMAND_BOOKING_MASTER.route_no as [Route No], TSPL_ITEM_MASTER.Alies_Name As [Description], TSPL_VEHICLE_MASTER.Description [Lorry_No],
                                    TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_MASTER.Zone_Code, IsNull( TSPL_VEHICLE_MASTER.Description, '''' ) As [VEHICLE NO],
                                    convert ( decimal(18, 2), TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor, 0 ) ) as Qty,
                                    TSPL_DEMAND_BOOKING_MASTER.Document_Date As [Order Date], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], 
                                    TSPL_CUSTOMER_MASTER.OldName as Booth, ( CASE WHEN TSPL_DEMAND_BOOKING_DETAIL.Qty = 0 THEN 0 ELSE ( TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor )/ coalesce( TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,
                                    TSPL_ITEM_UOM_DETAILKG.Conversion_Factor ) END ) AS QtyLtr From

                                     TSPL_DEMAND_BOOKING_DETAIL 
                                    Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                                    Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                                    Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                                    Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code
                                    Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code 
                                    left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_DEMAND_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code =" + IIf(rbtnAsPerBooking.Checked, "TSPL_DEMAND_BOOKING_DETAIL.Unit_Code", IIf(rdbCreate.Checked, "'CRATE'", "'LTR'")) + " 
                                    left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_DEMAND_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code = 'KG' 
                                    --left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_DEMAND_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code = 'CRATE' 
                                    left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_DEMAND_BOOKING_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_DEMAND_BOOKING_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAILUOM.UOM_Code
                        where 2 = 2 " + strIsMilkPouch + "  " + strWhrClause2 + ") zzz "

                        If rbtnDateWise.Checked = True Then
                            MainQuery += " group by zzz.Document_Date,zzz.Description	) as s pivot ( sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date "
                        Else
                            MainQuery += "group by zzz.Document_No, zzz.[VEHICLE NO], zzz.WdName, zzz.Description, zzz.[Customer Category Code], zzz.Cust_Group_Code, zzz.Zone_Code, zzz.[Route No] ) as s pivot ( sum(Qty) for Description in (" + strItem2 + " ) ) as zpivot group by zpivot.Document_No, zpivot.[VEHICLE NO], zpivot.[WdName], zpivot.[Group], zpivot.[Cust Group Desc], zpivot.[Customer Category Code], zpivot.[Zone], zpivot.[Route No] "
                        End If

                        'MainQuery += " group by zzz.Document_No, zzz.[VEHICLE NO], zzz.WdName, zzz.Description, zzz.[Customer Category Code], zzz.Cust_Group_Code, zzz.Zone_Code, zzz.[Route No], zzz.[DO NO], zzz.[Short Close] ) as s pivot ( sum(Qty) for Description in (" + strItem2 + " ) ) as zpivot group by zpivot.Document_No, zpivot.[VEHICLE NO], zpivot.[WdName], zpivot.[Group], zpivot.[Cust Group Desc], zpivot.[Customer Category Code], zpivot.[Zone], zpivot.[Route No], zpivot.[DO NO], zpivot.[Short Close]"
                        If chkMilkPouch.Checked = True OrElse chkProduct.Checked = True Then
                            'MainQuery = " select XXXFinal.[Route No],  XXXFinal.[Customer Name], " + strItem2 + " ,[Grand Total], XXXFinal.[Modified By]  from ( " + MainQuery + " ) XXXFinal "
                            If clsCommon.CompairString(cboShift.Text, "Both") = CompairStringResult.Equal Then
                                MainQuery = " select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                              ,  XXXFinal.[Customer Name] asc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], " + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total], max( XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Generated by] ,     max(XXXFinal.SourceBy) as  [Source]   from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No]	 group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name] "
                            ElseIf clsCommon.CompairString(cboShift.Text, "Morning") = CompairStringResult.Equal Then
                                MainQuery = " select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                              ,  XXXFinal.[Customer Name] asc, [Document Date] desc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], [Document Date] ,max([Time]) as [Time],case when XXXFinal.[Booking Time(AM / PM) ]  = 'AM' then 'Morning' else 'Evening' end as Shift  ," + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total], max(XXXFinal.[Modified By]) as  [Modified By], max(XXXFinal.[Created By]) as  [Generated by] ,     max(XXXFinal.SourceBy) as  [Source]  from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No] where XXXFinal.[Booking Time(AM / PM) ] = 'Morning' group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name],[Document Date] ,XXXFinal.[Booking Time(AM / PM) ] "
                            ElseIf clsCommon.CompairString(cboShift.Text, "Evening") = CompairStringResult.Equal Then
                                MainQuery = " select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                              ,  XXXFinal.[Customer Name] asc, [Document Date] desc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name],[Document Date],max([Time]) as [Time] ,case when XXXFinal.[Booking Time(AM / PM) ] = 'AM' then 'Morning' else 'Evening' end as Shift  ," + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total] , max(XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Generated by] ,     max(XXXFinal.SourceBy) as  [Source]  from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No] where XXXFinal.[Booking Time(AM / PM) ] = 'Evening' group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name],[Document Date],XXXFinal.[Booking Time(AM / PM) ] "
                            ElseIf clsCommon.CompairString(cboShift.Text, "Shift Wise") = CompairStringResult.Equal Then
                                MainQuery = " select  XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name],[Document Date], case when XXXFinal.[Booking Time(AM / PM) ] = 'AM' then 'Morning' else 'Evening' end as Shift  ,max([Time]) as [Time] ," + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total] , max(XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Generated by],     max(XXXFinal.SourceBy) as  [Source]  from ( " + MainQuery + " ) XXXFinal left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No] where XXXFinal.[Booking Time(AM / PM) ] in ('Evening', 'Morning') group by  XXXFinal.[Route No],  XXXFinal.[Customer Name],[Document Date],XXXFinal.[Booking Time(AM / PM) ] "
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
                                            select TBL_ITEM12.Row_Number, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Alies_Name,
                                            TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, case when TSPL_ITEM_MASTER.Is_Milk_Pouch =1 then convert (decimal(18,2), TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) else 0 end as Qty,
                                            case when TSPL_ITEM_MASTER.Is_Milk_Pouch =0 then convert (decimal(18,2), TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) else 0 end as QtyNotMilkPouch,
                                            case when TSPL_ITEM_MASTER.Is_Milk_Pouch =1 then TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount else 0 end as MilkAmt,case when TSPL_ITEM_MASTER.Is_Milk_Pouch =0 then TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount else 0 end as ProductAmt,
                                                                                      TSPL_DEMAND_BOOKING_MASTER.Document_Date As [Order Date],
                                            (CASE WHEN TSPL_DEMAND_BOOKING_DETAIL.Qty=0 THEN 0 ELSE (TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr,
                                            (TSPL_ROUTE_MASTER.City_Code) as [Route_CityCode], ( TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, (TSPL_COMPANY_MASTER.Add1) as [Comp_Add1] , (TSPL_COMPANY_MASTER.Add2) as [Comp_Add2] , (TSPL_COMPANY_MASTER.Add3) as Comp_Add3 , (TSPL_COMPANY_MASTER.Phone1) as Comp_Phone , (TSPL_COMPANY_MASTER.Phone2) as Comp_Phone2, (TSPL_COMPANY_MASTER.Fax) as Comp_Fax , (TSPL_COMPANY_MASTER.Email) as CompEmail , (TSPL_COMPANY_MASTER.State) as Comp_StateCode ,(TSPL_STATE_MASTER_Comp.STATE_NAME) as Comp_STATE_NAME,  (TSPL_COMPANY_MASTER.Pincode) as Comp_Pincode,
                                                                                      TSPL_DEMAND_BOOKING_MASTER.route_no , isnull(TSPL_CUSTOMER_MASTER.Credit_Customer,'N') as Credit_Customer
										                                              ,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', 
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS ItemNamePart2
                                            ,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name,0, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', 
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+1) AS ItemNamePart1 , tspl_transport_master.Transporter_Name
                                            from TSPL_DEMAND_BOOKING_DETAIL 
                                            left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Document_No =  TSPL_DEMAND_BOOKING_MASTER.Document_No 
                                            inner Join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'
        left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG'
left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'
left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_DEMAND_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code 
                                            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_DEMAND_BOOKING_MASTER.Comp_Code
                                                                                      left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.route_no
                                                                                      left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Comp on TSPL_STATE_MASTER_Comp.STATE_CODE = TSPL_COMPANY_MASTER.State 
										                                              left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
										                                              left outer join tspl_transport_master on tspl_transport_master.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id
                                                                                      left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code  
                                                                                      left outer join TSPL_LOCATION_MASTER on TSPL_DEMAND_BOOKING_MASTER.Location_Code = TSPL_LOCATION_MASTER.Location_Code   
                                            left outer join (select 'Item'+Convert(varchar(10), ROW_NUMBER() OVER(ORDER BY TSPL_ITEM_MASTER.Sku_Seq,TSPL_ITEM_MASTER.Item_Code)) AS Row_Number, TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.Sku_Seq from TSPL_ITEM_MASTER where
TSPL_ITEM_MASTER.Item_Code in (select TSPL_DEMAND_BOOKING_DETAIL.Item_Code from TSPL_DEMAND_BOOKING_DETAIL
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Document_No =  TSPL_DEMAND_BOOKING_MASTER.Document_No 
                                                              inner Join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                                              left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.route_no
                                                              left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
                                                              left outer join TSPL_LOCATION_MASTER on TSPL_DEMAND_BOOKING_MASTER.Location_Code = TSPL_LOCATION_MASTER.Location_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code where 2=2 and TSPL_ITEM_MASTER.Is_Milk_Pouch = 1 " + strWhrClause2 + " )  ) TBL_ITEM12 on TBL_ITEM12.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
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
                                            REVERSE(TSPL_ITEM_MASTER.Alies_Name))+1) AS ItemNamePart1 from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code in (select TSPL_DEMAND_BOOKING_DETAIL.Item_Code from TSPL_DEMAND_BOOKING_DETAIL
                                            left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Document_No =  TSPL_DEMAND_BOOKING_MASTER.Document_No 
                                            inner Join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                            left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.route_no
                                            left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
                                            left outer join TSPL_LOCATION_MASTER on TSPL_DEMAND_BOOKING_MASTER.Location_Code = TSPL_LOCATION_MASTER.Location_Code
                                            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code where 2=2 and TSPL_ITEM_MASTER.Is_Milk_Pouch = 1 " + strWhrClause2 + " ) )  XX ) XXX ) TBL_ItemNamePart1 on TBL_ItemNamePart1.SNO = XXXXXFinal.SNO
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
                        MainQuery = " select   Document_Date as [Document Date],  [Customer Code] as [Booth],[Zone],[Route No],Booking_Type as [Booking Type] , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No    " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2 " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date,zpivot.[Customer Code],zpivot.[Zone],zpivot.[Route No],zpivot.Booking_Type "
                    ElseIf chkFirstAndSecondSpell.Checked = True Then
                        MainQuery = " select [Zone], Document_Date as [Document Date], max(Booking_Type) as [Booking Type],max(Payment_Mode) as [Payment Mode],max(isnull (Scheme_Booking_Qty,0)) as [Scheme Qty],  " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,sum(AdvanceAmount) as [Total Price],sum(RemittedAmount) as [Remitted Amount],sum(AdvanceAmount)-sum(RemittedAmount) as [Difference Amount],cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(zzz.Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,max(qty) as qty,max(QtyLtr) as QtyLtr,max(AdvanceAmount) as AdvanceAmount,sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS RemittedAmount  from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr, isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2 " + strWhrClause2 + "  and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH'  )zzz " & Environment.NewLine &
                                  " LEFT OUTER JOIN TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No and isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' " & Environment.NewLine &
                                  " where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date,zpivot.[Zone] order by zpivot.Document_Date"
                    ElseIf chkFirstAndSecondSpellAbstract.Checked = True Then
                        Dim strItem2L As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol + "+'#L')  as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                        Dim strItem2WithSumL As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + "+'#L')  + ',0))' + ' as ' + QUOTENAME( " + strAliasCol + "+'#L') as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                        MainQuery = " select [Zone], Document_Date as [Document Date],  " + strItem2WithSum + "," + strItem2WithSumL + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,sum(AdvanceAmount) as [Total Price],sum(RemittedAmount) as [Remitted Amount],sum(AdvanceAmount)-sum(RemittedAmount) as [Difference Amount] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date ,max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type, max(zzz.Payment_Mode) as Payment_Mode, max(Case when   convert (decimal(18,2),Created_Date_Time) between 8 and 20 then 'AM'  else 'PM'  end) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,(zzz.Description+'#L') AS Description#L,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,max(qty) as qty,max(QtyLtr) as QtyLtr,max(AdvanceAmount) as AdvanceAmount,sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS RemittedAmount  from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr, isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                 "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                 " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No   " &
                                 " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                 "  where 2=2 " + strWhrClause2 + "  and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH'  )zzz " & Environment.NewLine &
                                 " LEFT OUTER JOIN TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No and isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' " & Environment.NewLine &
                                 " where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot " &
                                " pivot (  sum(QtyLtr) for Description#L in ( " + strItem2L + " ) ) as zpivot1 " &
                                " group by zpivot1.[Zone],zpivot1.Document_Date order by zpivot1.[Zone],Convert(date,zpivot1.Document_Date,103) "
                    ElseIf chkSaleInvoiceWise.Checked = True Then
                        MainQuery = " select  Document_No as [Document No],[VEHICLE NO],[WdName],[Group],[Cust Group Desc], [Customer Category Code],[Zone],[Route No],UOM , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total], case WHEN upper(UOM)='CAN' THEN (" + strGrandTotalWithoutScheme + ") else 0 END AS [Can total], case WHEN upper(UOM)='CRATE' THEN (" + strGrandTotalWithoutScheme + ") else 0 END AS [Crate total], case WHEN upper(UOM)='BOX' THEN (" + strGrandTotalWithoutScheme + ") else 0 END AS [Box total] from (select zzz.Document_No,zzz.[VEHICLE NO],zzz.[Customer Category Code],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone] ,zzz.UOM,zzz.[Route No] ,sum(qty) as qty from  (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Document_No,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No] ,TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_SD_SALE_INVOICE_DETAIL.Qty  as Qty, TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2 " + strWhrClause2 + " )zzz where 1=1 group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.[Customer Category Code],zzz.Zone_Code,zzz.[Route No],zzz.UOM 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_No,zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.UOM "
                    Else
                        MainQuery = " select  [VEHICLE NO],[WdName],[Group],[Cust Group Desc], [Customer Category Code],[Zone],[Route No],
                                      UOM  , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] 
                                      from (select zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],
                                      zzz.[Customer Category Code],zzz.Zone_Code as [Zone] ,zzz.[Route No],zzz.UOM  ,sum(qty) as qty 
                                      from  (Select  TSPL_ITEM_MASTER.Sku_Seq, TSPL_DEMAND_BOOKING_MASTER.Location_Code,
                                      TSPL_LOCATION_MASTER.Location_Desc, TSPL_DEMAND_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName,
                                      TSPL_DEMAND_BOOKING_MASTER.Route_No as [Route No],TSPL_DEMAND_BOOKING_DETAIL.Unit_code as UOM, TSPL_DEMAND_BOOKING_DETAIL.Item_Code as Item_Code,
                                      TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                                      isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code],TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO],
                                      TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty, TSPL_DEMAND_BOOKING_MASTER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc]
                                      From TSPL_DEMAND_BOOKING_DETAIL 
                                     Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                                     Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                                     Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                     Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
                                     Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.Location_Code
                                     left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code
                                      where 2=2 " + strWhrClause2 + " )zzz
                                      group by zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.[Customer Category Code],zzz.Zone_Code,zzz.[Route No] ,zzz.UOM ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.UOM "
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
                gv1.DataSource = Nothing
                gv1.Rows.Clear()

                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.BestFitColumns()
                If clsCommon.myLen(gv1.Columns("Document Date")) > 0 Then
                    gv1.Columns("Document Date").FormatString = "{0 : dd/MM/yyyy}"
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
                    If gv1.Rows.Count > 0 Then
                        Dim summaryRowItem As New GridViewSummaryRowItem()
                        For i As Integer = item To gv1.Columns.Count - 1
                            Dim aa = gv1.Columns(i).HeaderText()
                            Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                            If clsCommon.CompairString(aa, "Modified By") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(aa, "Created By") <> CompairStringResult.Equal Then
                                summaryRowItem.Add(item1)
                                gv1.Columns(i).FormatString = "{0:n2}"
                            End If
                        Next
                        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    End If
                End If
                ' Hide Column when footer grand total <= 0
                If chkFirstAndSecondSpell.Checked = False AndAlso chkFirstAndSecondSpellAbstract.Checked = False Then
                    For i As Integer = item To gv1.Columns.Count - 5
                        Dim grandTotal As Decimal = 0
                        For j As Integer = 0 To gv1.Rows.Count - 1
                            Dim columnValue As Object = String.Empty
                            columnValue = gv1.Rows(j).Cells(i).Value
                            If (Not IsDBNull(gv1.Rows(j).Cells(i).Value) AndAlso columnValue IsNot Nothing) Then
                                grandTotal = grandTotal + clsCommon.myCdbl(gv1.Rows(j).Cells(i).Value)
                            End If
                        Next
                        If (clsCommon.myCdbl(grandTotal) > 0) Then
                            gv1.Columns(i).IsVisible = True
                        Else
                            gv1.Columns(i).IsVisible = False
                        End If
                    Next
                End If
                If chkFirstAndSecondSpell.Checked = True OrElse chkFirstAndSecondSpellAbstract.Checked = True Then
                    gv1.Columns("Grand Total").IsVisible = False
                End If
                If chkFirstAndSecondSpellAbstract.Checked = True Then
                    For i As Integer = 0 To gv1.Columns.Count - 1
                        If gv1.Columns(i).HeaderText.Contains("#L") Then
                            gv1.Columns(i).IsVisible = False
                        End If
                    Next
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Pinned column 
                If ChkDayWiseSummary.Checked Then
                    gv1.Columns("ddFilter").IsVisible = False
                Else
                    gv1.Columns(0).IsPinned = True
                    gv1.Columns(1).IsPinned = True
                    If chkRouteBoothWise.Checked = True Then
                        gv1.Columns(2).IsPinned = True
                        gv1.Columns(3).IsPinned = True
                        gv1.Columns(4).IsPinned = True
                    End If
                    If chkMilkPouch.Checked = False AndAlso chkProduct.Checked = False Then
                        gv1.Columns("Grand Total").IsPinned = True
                        gv1.Columns("Grand Total").PinPosition = PinnedColumnPosition.Right
                    End If
                    If gv1.Columns.Contains("Total Price") Then
                        gv1.Columns("Total Price").IsPinned = True
                        gv1.Columns("Total Price").PinPosition = PinnedColumnPosition.Right
                    End If
                    If gv1.Columns.Contains("Remitted Amount") Then
                        gv1.Columns("Remitted Amount").IsPinned = True
                        gv1.Columns("Remitted Amount").PinPosition = PinnedColumnPosition.Right
                    End If
                    If gv1.Columns.Contains("Difference Amount") Then
                        gv1.Columns("Difference Amount").IsPinned = True
                        gv1.Columns("Difference Amount").PinPosition = PinnedColumnPosition.Right
                    End If
                    If gv1.Columns.Contains("Total In Ltr") Then
                        gv1.Columns("Total In Ltr").IsPinned = True
                        gv1.Columns("Total In Ltr").PinPosition = PinnedColumnPosition.Right
                    End If
                End If
                If (chkMilkPouch.Checked = True OrElse chkProduct.Checked = True) AndAlso clsCommon.CompairString(cboShift.Text, "Shift Wise") = CompairStringResult.Equal Then
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[Route No] as [Route No] format ""{0}: {1}"" Group By [Route No]"))
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[Customer Code] as [Customer Code] format ""{0}: {1}"" Group By [Customer Code]"))
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[Customer Name] as [Customer Name] format ""{0}: {1}"" Group By [Customer Name]"))
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[Document Date] as [Document Date] format ""{0}: {1}"" Group By [Document Date]"))
                    gv1.AutoExpandGroups = True
                    gv1.ShowGroupPanel = False
                    gv1.ShowRowHeaderColumn = False
                    gv1.AllowAddNewRow = False
                    gv1.AllowDeleteRow = False
                    gv1.EnableFiltering = True
                    gv1.ShowFilteringRow = True
                End If
                Try
                    If chkMilkPouch.Checked = True OrElse chkProduct.Checked = True Then
                        Dim strItemFatch() As String = strItem2.Split(",")
                        For count As Integer = 0 To strItemFatch.Length - 1
                            Dim strCode As String = strItemFatch(count)
                            Dim strCode2 As String = Replace(strItemFatch(count), "[", "")
                            strCode2 = Replace(strCode2, "]", "")
                            Dim sum As Integer = clsCommon.myCdbl(dtgv.Compute("SUM(" + strCode + ")", String.Empty))
                            If gv1.Columns.Contains(strCode2) Then
                                If sum > 0 Then
                                    gv1.Columns(strCode2).IsVisible = True
                                Else
                                    gv1.Columns(strCode2).IsVisible = False
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

    Private Sub rptDemandReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        cboShift.Text = "Both"
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)

    End Sub

    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)

    End Sub
End Class