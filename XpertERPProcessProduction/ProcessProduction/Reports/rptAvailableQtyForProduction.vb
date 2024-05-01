Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptAvailableQtyForProduction
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)

        Dim Query As String = ""
        Dim dt_booking As DataTable
        Dim counting As Integer = 0
        If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
            clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
            Exit Sub
        End If

        Query = "select tspl_booking_detail.Item_Code from tspl_booking_detail left join tspl_booking_matser on tspl_booking_matser.Document_No=tspl_booking_detail.Document_No"

        Query += " and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            Query += " and  tspl_booking_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If


        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            Query += " and tspl_booking_detail.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If

        Query += " group by tspl_booking_detail.Item_Code"
        dt_booking = common.clsDBFuncationality.GetDataTable(Query)

        'Ticket no- BHA/22/11/18-000696 Use last posted BOM
        'For ii As Integer = 0 To dt_booking.Rows.Count - 1
        '    counting = 0
        '    Query = "select count(*) as cc from TSPL_PP_BOM_HEAD where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + clsCommon.myCstr(dt_booking.Rows(ii).Item(0)) + "'"
        '    counting = clsCommon.myCdbl(common.clsDBFuncationality.getSingleValue(Query))
        '    If counting > 1 Then
        '        clsCommon.MyMessageBoxShow("More than One BOM define for Item" + clsCommon.myCstr(dt_booking.Rows(ii).Item(0)), Me.Text)
        '        Exit Sub
        '    End If
        'Next


        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

        If txtItemMult.arrDispalyMember IsNot Nothing AndAlso txtItemMult.arrDispalyMember.Count > 0 Then
            arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
        End If
        Dim qry As String = Nothing

        qry = "SELECT ROW_NUMBER() OVER (ORDER BY final.ITEM_CODE) as [Sno]" & _
 ",final.Item_Code as [Item Code],final.Item_Desc as [Item Desc],final.PROD_ITEM_UNIT_CODE as [UOM] " & _
 ",SUM(final.[Booking Qty]) as [Booking Qty] " & _
 ",SUM(final.[Dispatch Qty]) as [Dispatch Qty]  " & _
 ",SUM(final.[Booking Vs Dispath]) as [Booking Vs Dispath] " & _
 ",sum(isnull(round(STK.Inventry_Qty/BOM1.Conversion_Factor,2),0)) as [Available Qty] " & _
  ",SUM(final.[Booking Vs Dispath])-sum(isnull(round(STK.Inventry_Qty/BOM1.Conversion_Factor,2),0)) as [Qty To Be Produced] " & _
   "     from " & _
  "(SELECT  BOOK.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,BOM.PROD_ITEM_UNIT_CODE  " & _
 ",sum(isnull(round(BOOK.Booking_Qty/BOM.Conversion_Factor,2),0)) as [Booking Qty]  " & _
 ",sum(isnull(round(SHIP.Dispatch_Qty/BOM.Conversion_Factor,2),0)) as [Dispatch Qty]  " & _
",sum(isnull(round(BOOK.Booking_Qty/BOM.Conversion_Factor,2),0)-isnull(round(SHIP.Dispatch_Qty/BOM.Conversion_Factor,2),0)) as [Booking Vs Dispath] " & _
",BOOK.Loc_Code " & _
"FROM ( (select  " & _
"TSPL_BOOKING_MATSER.Document_No as BOOK_NO, " & _
"tspl_booking_detail.Item_Code,  tspl_booking_detail.Loc_Code ,sum(tspl_booking_detail.Booking_Qty*isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) Booking_Qty  from tspl_booking_detail left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=tspl_booking_detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=tspl_booking_detail.Unit_Code  where 1 = 1  "

        qry += " and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and  tspl_booking_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If


        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            qry += " and tspl_booking_detail.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If

        qry += "Group by tspl_booking_detail.Item_Code,tspl_booking_detail.Loc_Code ,TSPL_BOOKING_MATSER.Document_No " & _
 ")BOOK  " & _
"left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=BOOK.Item_Code  " & _
 "Left Join ( SELECT * FROM (select ROW_NUMBER() OVER(PARTITION BY TSPL_PP_BOM_HEAD.PROD_ITEM_CODE ORDER BY TSPL_PP_BOM_HEAD.BOM_DATE DESC) as ss,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE  " & _
 "from TSPL_PP_BOM_HEAD left join TSPL_ITEM_UOM_DETAIL on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE=TSPL_ITEM_UOM_DETAIL.UOM_Code where isnull(TSPL_PP_BOM_HEAD.Is_Post,0)=1 "

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_BOM_HEAD.PROD_ITEM_CODE in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If

        qry += ")tt WHERE tt.ss=1)BOM on TSPL_ITEM_MASTER.Item_Code=BOM.PROD_ITEM_CODE  " & _
  "left outer join ( select sum(SHIP.SHIP_QTY) Dispatch_Qty,SHIP.Item_Code,SHIP.Location,DO.Booking_No " & _
   "     FROM " & _
  "((select TSPL_SD_SHIPMENT_DETAIL.Delivery_Code as DO_NO,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Location, " & _
 "sum(TSPL_SD_SHIPMENT_DETAIL.Qty*isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) as SHIP_QTY " & _
  "      from TSPL_SD_SHIPMENT_DETAIL " & _
 "left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_Code " & _
  "      where TSPL_SD_SHIPMENT_DETAIL.Delivery_Code Is Not null " & _
 "group by TSPL_SD_SHIPMENT_DETAIL.Delivery_Code ,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Location)SHIP " & _
  "      INNER Join " & _
 "(select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No AS DO_NO,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No " & _
 ",TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code " & _
  "      from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " & _
 "LEFT OUTER JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
 "GROUP BY TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No " & _
 ",TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No)DO " & _
 "ON DO.DO_NO=SHIP.DO_NO AND DO.Item_Code=SHIP.Item_Code AND DO.Location_Code=SHIP.Location)GROUP BY SHIP.Item_Code,SHIP.Location,DO.Booking_No " & _
  ") SHIP on SHIP.Item_Code=TSPL_ITEM_MASTER.Item_Code  and SHIP.Location=BOOK.Loc_Code and ship.Booking_No=book.BOOK_NO) group by BOOK.Item_Code,BOOK.Loc_Code,TSPL_ITEM_MASTER.Item_Desc,BOM.PROD_ITEM_UNIT_CODE)final " & _
 "left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=final.Item_Code " & _
    "left outer join  ( select sum(INV_QTY.Stock_Qty) as Inventry_Qty,INV_QTY.Item_Code,INV_QTY.Stock_UOM AS Inventry_UOM,INV_QTY.Location_Code FROM     (select sum(case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end *TSPL_INVENTORY_MOVEMENT.Stock_Qty) as Stock_Qty,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Stock_UOM ,TSPL_INVENTORY_MOVEMENT.Location_Code from TSPL_INVENTORY_MOVEMENT  where 1=1	"

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and  TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If

        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If

        qry += " and TSPL_INVENTORY_MOVEMENT.PUNCHING_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"


        qry += "group by TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Stock_UOM, TSPL_INVENTORY_MOVEMENT.Location_Code UNION ALL select sum(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 1 else -1 end *TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty) as Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM ,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code from TSPL_INVENTORY_MOVEMENT_NEW  where 1=1  "

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If

        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If

        qry += " and TSPL_INVENTORY_MOVEMENT_NEW.PUNCHING_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"

        qry += " group by TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM, TSPL_INVENTORY_MOVEMENT_NEW.Location_Code )INV_QTY GROUP BY INV_QTY.Item_Code,INV_QTY.Stock_UOM,INV_QTY.Location_Code " & _
            ")STK  on STK.Item_Code=final.Item_Code " & _
        "and STK.Location_Code=final.Loc_Code " & _
          "Left Join  ( select * from ( select ROW_NUMBER() OVER(PARTITION BY TSPL_PP_BOM_HEAD.PROD_ITEM_CODE ORDER BY TSPL_PP_BOM_HEAD.BOM_DATE DESC) as ss1,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE  " & _
         "from TSPL_PP_BOM_HEAD left join TSPL_ITEM_UOM_DETAIL on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE=TSPL_ITEM_UOM_DETAIL.UOM_Code where isnull(TSPL_PP_BOM_HEAD.Is_Post,0)=1 "

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_BOM_HEAD.PROD_ITEM_CODE in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If

        qry += " )tt1 where tt1.ss1=1)BOM1 on TSPL_ITEM_MASTER.Item_Code=BOM1.PROD_ITEM_CODE  " & _
       "GROUP BY final.Item_Code ,final.Item_Desc ,final.PROD_ITEM_UNIT_CODE "



        'SANJAY COMMENT
        '        qry = "SELECT ROW_NUMBER() OVER (ORDER BY BOOK.ITEM_CODE) as [Sno] ,BOOK.Item_Code as [Item Code] " & _
        '",TSPL_ITEM_MASTER.Item_Desc as [Item Desc] " & _
        '",BOM.PROD_ITEM_UNIT_CODE as [UOM] " & _
        '",isnull(round(BOOK.Booking_Qty/BOM.Conversion_Factor,2),0) as [Booking Qty] " & _
        '",isnull(round(STK.Inventry_Qty/BOM.Conversion_Factor,2),0) as [Available Qty] " & _
        '",case when isnull(round(BOOK.Booking_Qty/BOM.Conversion_Factor,2),0)-isnull(round(STK.Inventry_Qty/BOM.Conversion_Factor,2),0) <= 0 then 0 " & _
        '"else isnull(round(BOOK.Booking_Qty/BOM.Conversion_Factor,2),0)-isnull(round(STK.Inventry_Qty/BOM.Conversion_Factor,2),0) " & _
        '"end as [Qty To Be Produced] " & _
        ' "       FROM " & _
        '"( " & _
        '"(select tspl_booking_detail.Item_Code, " & _
        '       " tspl_booking_detail.Loc_Code " & _
        '",sum(tspl_booking_detail.Booking_Qty*isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) Booking_Qty " & _
        '       " from tspl_booking_detail " & _
        '    "left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " & _
        '   "left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=tspl_booking_detail.Item_Code " & _
        '   "and TSPL_ITEM_UOM_DETAIL.UOM_Code=tspl_booking_detail.Unit_Code " & _
        '       " where 1 = 1 "

        '        qry += " and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        '        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
        '            qry += " and  tspl_booking_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        '        End If


        '        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
        '            qry += " and tspl_booking_detail.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        '        End If


        '        qry += " Group by tspl_booking_detail.Item_Code,tspl_booking_detail.Loc_Code  " & _
        '")BOOK " & _
        '"left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=BOOK.Item_Code " & _
        '               " Left Join " & _
        '        " (select TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD left join TSPL_ITEM_UOM_DETAIL " & _
        '       "on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=TSPL_ITEM_UOM_DETAIL.Item_Code " & _
        '       "and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE=TSPL_ITEM_UOM_DETAIL.UOM_Code "


        '        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
        '            qry += " where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        '        End If

        '        qry += " )BOM on TSPL_ITEM_MASTER.Item_Code=BOM.PROD_ITEM_CODE " & _
        '       " left outer join  " & _
        '    "( " & _
        '    "select sum(INV_QTY.Stock_Qty) as Inventry_Qty,INV_QTY.Item_Code,INV_QTY.Stock_UOM AS Inventry_UOM,INV_QTY.Location_Code " & _
        '    "FROM " & _
        '    "    (select sum(case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end " & _
        '    "*TSPL_INVENTORY_MOVEMENT.Stock_Qty) as Stock_Qty,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Stock_UOM " & _
        '    ",TSPL_INVENTORY_MOVEMENT.Location_Code " & _
        '    "from TSPL_INVENTORY_MOVEMENT  " & _
        '    "where 1=1  "

        '        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
        '            qry += " and  TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        '        End If

        '        qry += " and TSPL_INVENTORY_MOVEMENT.PUNCHING_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"

        '        qry += "	group by TSPL_INVENTORY_MOVEMENT.Item_Code " & _
        '            ",TSPL_INVENTORY_MOVEMENT.Stock_UOM, TSPL_INVENTORY_MOVEMENT.Location_Code " & _
        '    "UNION ALL " & _
        '    "select sum(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 1 else -1 end " & _
        '    "*TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty) as Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM " & _
        '    ",TSPL_INVENTORY_MOVEMENT_NEW.Location_Code " & _
        '    "from TSPL_INVENTORY_MOVEMENT_NEW  " & _
        '    "where 1=1  "

        '        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
        '            qry += " and  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        '        End If

        '        qry += " and TSPL_INVENTORY_MOVEMENT_NEW.PUNCHING_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
        '        qry += "group by TSPL_INVENTORY_MOVEMENT_NEW.Item_Code " & _
        '    ",TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM, TSPL_INVENTORY_MOVEMENT_NEW.Location_Code " & _
        '    ")INV_QTY " & _
        '    "GROUP BY INV_QTY.Item_Code,INV_QTY.Stock_UOM,INV_QTY.Location_Code " & _
        '    ")STK  " & _
        '    "on STK.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        '    "and STK.Inventry_UOM=TSPL_ITEM_MASTER.Unit_Code " & _
        '    "and STK.Location_Code=BOOK.Loc_Code " & _
        '    ") "
        'SANJAY COMMENT


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

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
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        SetGridFormationOFGV1()
        gv1.BestFitColumns()
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()
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

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("Booking Qty").FormatString = "{0:n2}"
        gv1.Columns("Dispatch Qty").FormatString = "{0:n2}"
        gv1.Columns("Booking Vs Dispath").FormatString = "{0:n2}"
        gv1.Columns("Available Qty").FormatString = "{0:n2}"
        gv1.Columns("Qty To Be Produced").FormatString = "{0:n2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Booking Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Dispatch Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Booking Vs Dispath", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Available Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Qty To Be Produced", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub


    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub



    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptAvailableQtyForProduction_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptAvailableQtyForProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
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

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAvailableQtyForProduction & "'"))

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
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
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAvailableQtyForProduction & "'"))

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Available Quantity For Production Report", gv1, arrHeader, "Available Quantity For Production Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
