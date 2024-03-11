Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class rptTransporterWiseReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim strRouteSel As String = ""
            Dim strZoneSel As String = ""
            Dim strCustCategoryMappInUserMaster As String = ""
            Dim strRuteSelForTransporterDeduction As String = ""
            If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
                strZoneSel = " and TSPL_ZONE_MASTER.Zone_Code in(" + clsCommon.GetMulcallString(txtmultiZone.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtmultiRoute.arrValueMember IsNot Nothing AndAlso txtmultiRoute.arrValueMember.Count > 0 Then
                strRouteSel = " and TSPL_ROUTE_MASTER.Route_No in(" + clsCommon.GetMulcallString(txtmultiRoute.arrValueMember) + ")" + Environment.NewLine
                strRuteSelForTransporterDeduction = " and TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE.Route_Code in(" + clsCommon.GetMulcallString(txtmultiRoute.arrValueMember) + ")" + Environment.NewLine
            End If

            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                strCustCategoryMappInUserMaster += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
            End If

            Dim strbaseQry As String = " Select TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, ( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Booking_Type,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName,TSPL_CUSTOMER_MASTER.OldName as [Booth] ,TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No],TSPL_ROUTE_MASTER.Route_Desc , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code],TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_ZONE_MASTER.Description as Zone_Desc,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_BOOKING_DETAIL.Booking_Qty as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],Case when TSPL_BOOKING_MATSER.Posted = 1 then 'Posted' else 'Not Posted' end [Posted] , cast ( isnull ( ( isnull (  TSPL_BOOKING_DETAIL.Booking_Qty,0) * isnull ( Stocking_Uint.Conversion_Factor,0) / nullif ( isnull (Target_Unit.Conversion_Factor,0),0)),0) as Decimal(18,2)) as  Qty_In_Ltr, TSPL_VEHICLE_MASTER.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name From TSPL_BOOKING_DETAIL 
                                         Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  
	                                     Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  
	                                     Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  
	                                     Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code  
	                                     Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code  
	                                     left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  
	                                     Left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_BOOKING_DETAIL.route_no  
	                                     Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
	                                     left Outer Join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_VEHICLE_MASTER.Transport_Id 
	                                     left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Uint  on  Stocking_Uint .item_code = TSPL_BOOKING_DETAIL.item_code and    Stocking_Uint.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code
	                                     left outer join TSPL_ITEM_UOM_DETAIL  as Target_Unit on   Target_Unit.item_code = TSPL_BOOKING_DETAIL.item_code and Target_Unit.UOM_Code = 'LTR'
                                         where 2=2   and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + txtfromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103)  
                                         and TSPL_BOOKING_MATSER.Posted = 1 and  len (isnull (TSPL_VEHICLE_MASTER.Transport_Id,'') ) > 0  and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'  " + strZoneSel + "  " + strRouteSel + "  " + strCustCategoryMappInUserMaster + " "

            Dim strCrateAccountbaseQry As String = " With CTETemp as ( Select convert(varchar,Doc_Date,103) as Doc_Date,Zone_Code,Zone_Name,CrateQtyRecd,CrateOutQty  from(
                                                    select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_Zone_Master.Description as Zone_Name , tspl_vehicle_master.Transport_Id, pp.CrateQtyRecd  as CrateQtyRecd, pp.CrateOutQty  as CrateOutQty  from ( 
                                                    select convert(date,Doc_Date,103)  as Doc_Date, (xx.Vehicle_Code) as Vehicle_Code,xx.Route_no,xx.Customer_Code, sum(xx.CrateQtyRecd) as CrateQtyRecd, sum(xx.CrateOutQty ) as CrateOutQty  from (
                                                    select Document_Date as Doc_Date,Customer_Code,Vehicle_Code,Route_Code as Route_no,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec,
                                                    Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment 
                                                    from (  (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE
                                                    left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No 
                                                    where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 )  
                                                    union all 
                                                    (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as  Doc_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_Code  as Route_No ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE
                                                    left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No 
                                                    where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  
                                                    union all 
                                                    select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Doc_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1  
                                                    union all 
                                                    select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Doc_Date,TSPL_sd_SALE_RETURN_HEAD.Customer_Code,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 )  ) as Closing 
                                                    WHERE convert(date,Document_Date ,103)>=convert(date,'" + txtfromDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) 
                                                    ) as xx where 2=2   GROUP BY Route_No, Vehicle_Code,Customer_Code ,convert(date,Doc_Date,103) 
                                                    ) as pp    left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =pp.Route_No  
                                                    left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id= TSPL_ROUTE_MASTER.vehicle_code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = pp.customer_Code 
                                                    Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where 2=2  and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'  " + strZoneSel + "  " + strRouteSel + "  " + strCustCategoryMappInUserMaster + "  ) YYY )   "


            Dim qry As String = " "
            If chkNone.Checked = True Then
                qry = "select tt.ZoneName as [Zone],convert(varchar,tt.Document_Date,103) as [Date],0 as [PM Sales],Convert(decimal(18,2),sum(tt.Booking_Qty)) as [TrSheet],0 as [AM Sales],Convert(decimal(18,2),sum(tt.Booking_Qty)) as [Total] " &
            "from ( " &
            " select TSPL_ZONE_MASTER.Description as ZoneName,TSPL_BOOKING_MATSER.Document_Date,(TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor as Booking_Qty  " &
            " from TSPL_BOOKING_MATSER " &
            " left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No " &
            " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_id=TSPL_BOOKING_DETAIL.Vehicle_Code " &
            " left join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_BOOKING_DETAIL.Unit_code " &
            " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code " &
            " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code "
                qry += " where 2=2 "
                qry += " and convert(date, TSPL_BOOKING_MATSER.Document_date,103) >=  convert(date,'" + txtfromDate.Value + "',103)  and  convert(date, TSPL_BOOKING_MATSER.Document_date,103) <= convert(date,'" + txtToDate.Value + "',103) "
                qry += " and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'  " + strCustCategoryMappInUserMaster + "   "

                If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ZONE_MASTER.Zone_Code in(" + clsCommon.GetMulcallString(txtmultiZone.arrValueMember) + ")" + Environment.NewLine
                End If

                qry += " )tt group by tt.ZoneName,convert(varchar,tt.Document_Date,103),convert(date,tt.Document_Date,103) " &
                " order by tt.ZoneName,convert(date,tt.Document_Date,103)"
            ElseIf chkRouteWise.Checked = True Then
                Dim strPivotRouteColumn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ',' +  QUOTENAME( Route_Desc) as Route_Desc FROM  (select distinct  TSPL_ROUTE_MASTER.Route_Desc from   TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No   Left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_BOOKING_DETAIL.route_no 
                                                                                         Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
                                                                                         Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
                                                                                         Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code                       
                                                                                         where 2=2   and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + txtfromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103)  
                                                                                         and TSPL_BOOKING_MATSER.Posted = 1 and  len (isnull (TSPL_VEHICLE_MASTER.Transport_Id,'') ) > 0  and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'  " + strZoneSel + "  " + strRouteSel + "  " + strCustCategoryMappInUserMaster + "  ) xxxx  order by   Route_Desc asc     FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))

                If clsCommon.myLen(strPivotRouteColumn) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                    Exit Sub
                End If
                Dim strPivotRouteColumnWithSum As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ',' + ' Sum ( isnull ( '+ QUOTENAME( Route_Desc) + ',0)) as ' + Route_Desc  as Route_Desc FROM  ( select distinct  TSPL_ROUTE_MASTER.Route_Desc from  TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No   Left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_BOOKING_DETAIL.route_no 
                                                                                                Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
                                                                                                Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
                                                                                                Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                                                                                                where 2=2   and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + txtfromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103)
                                                                                                and TSPL_BOOKING_MATSER.Posted = 1 and  len (isnull (TSPL_VEHICLE_MASTER.Transport_Id,'') ) > 0  and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'  " + strZoneSel + "  " + strRouteSel + "  " + strCustCategoryMappInUserMaster + "  ) xxxx  order by   Route_Desc asc   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                Dim strPivotRouteColumnWithSumTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+' + ' Sum ( isnull('+ QUOTENAME( Route_Desc) + ',0))  '   as Route_Desc FROM  (select distinct  TSPL_ROUTE_MASTER.Route_Desc from  TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No   
                                                                                                     Left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_BOOKING_DETAIL.route_no 
                                                                                                     Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
                                                                                                     Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
                                                                                                     Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                                                                                                     where 2=2   and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + txtfromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103)  
                                                                                                     and TSPL_BOOKING_MATSER.Posted = 1 and  len (isnull (TSPL_VEHICLE_MASTER.Transport_Id,'') ) > 0  and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'  " + strZoneSel + "  " + strRouteSel + "  " + strCustCategoryMappInUserMaster + " ) xxxx  order by   Route_Desc asc    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
                qry = " Select  Document_Date as [Date],   " + strPivotRouteColumnWithSum + "
                        , " + strPivotRouteColumnWithSumTotal + "   as [Total Liters]
                     from ( Select XFinal.Transport_Id, max(XFinal.Transporter_Name) as  Transporter_Name, XFinal.Document_Date , [Route No] ,max(Route_Desc ) as Route_Desc, sum (Qty_In_Ltr) as Qty_In_Ltr   from ( " +
                     strbaseQry + " )   XFinal group by Transport_Id,Document_Date , [Route No] )XXFinal   
	                 pivot (  sum(Qty_In_Ltr) for Route_Desc in ( " + strPivotRouteColumn + " ) ) as zpivot
                     group by  Transport_Id,Transporter_Name,Document_Date "

            ElseIf chkZoneWise.Checked = True Then
                qry = " Select   Zone_Code as [Zone Code] ,max(Zone_Desc ) as [Zone Desc], sum (Qty_In_Ltr) as [Total Liters]   from ( " + strbaseQry + " )   XFinal group by Transport_Id,  Zone_Code "

            ElseIf chkCrateAccountDateWise.Checked = True Then
                qry = strCrateAccountbaseQry + "   Select convert(varchar,Doc_Date,103) as Date,  sum ( CrateOutQty) as [No. of Crates Dispatched] ,sum (CrateQtyRecd) as [No. of Crates Received] ,  sum (isnull( CrateOutQty,0)) - sum (isnull(CrateQtyRecd,0))   as  Shortage    from (
                      Select CTETemp.Doc_Date, CTETemp.CrateQtyRecd,CTETemp.CrateOutQty from CTETemp 	  
                      ) ZZZ  group by Doc_Date "
            ElseIf chkCrateAccountZonewise.Checked = True Then
                qry = strCrateAccountbaseQry + "  Select Zone_Code as [Zone Code], max(Zone_Name) as [Zone Desc],  sum ( CrateOutQty) as [No. of Crates Dispatched] ,sum (CrateQtyRecd)  as [No. of Crates Received] , sum (isnull( CrateOutQty,0)) - sum (isnull(CrateQtyRecd,0))   as  Shortage  from (
                      Select CTETemp.Doc_Date,isnull (CTETemp.Zone_Code,'') as  Zone_Code, isnull (CTETemp.Zone_Name,'') as Zone_Name, CTETemp.CrateQtyRecd,CTETemp.CrateOutQty from CTETemp 	  
                      ) ZZZ  group by Zone_Code "
            ElseIf chkTransporterDeduction.Checked = True Then
                qry = "   select   XXXFinal.Deduction_Date as  [Deduction Date],XXXFinal.Route_Code as [Route Code],  sum (XXXFinal.SOC_NoOfCrates) as SOC_NoOfCrates ,  sum (SOC_NoOfCrates_Amount) as SOC_NoOfCrates_Amount ,sum (ELOM_QTY) as ELOM_QTY,sum(ELOM_Amount)as ELOM_Amount,sum(VC_TopLess) as VC_TopLess,sum(VC_LOGO) as VC_LOGO, Sum(VC_InnerBodyPainting) as VC_InnerBodyPainting, Sum(VC_Cleaniness) as VC_Cleaniness,Sum(VC_BottomDamage) as VC_BottomDamage,Sum(VC_Shelf) as VC_Shelf,Sum(VC_Light) as VC_Light,Sum(VC_Amount) as VC_Amount,sum(LateVehicleReport_Amount) as LateVehicleReport_Amount ,Sum(ShortageOfLoadingStaffSupervisors_Amount) as ShortageOfLoadingStaffSupervisors_Amount, Sum(Net_Amount ) as Net_Amount , max(Remarks ) as Remarks from (
                          select TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Transporter_code,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE.Route_Code, convert (varchar,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Deduction_Date,103) as Deduction_Date ,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.SOC_NoOfCrates, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.SOC_NoOfCrates_Amount,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.ELOM_QTY,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.ELOM_Amount,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_TopLess,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_LOGO, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_InnerBodyPainting, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_Cleaniness,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_BottomDamage,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_Shelf,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_Light,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.VC_Amount,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.LateVehicleReport_Amount ,TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.ShortageOfLoadingStaffSupervisors_Amount, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Net_Amount , TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Remarks from TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL left outer join TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE on TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE.Document_Code = TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Document_Code left outer join TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER on TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code = TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Document_Code left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Transporter_code
                          where 2=2  and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.IsPosted = 'Y'   and convert(date, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Deduction_Date,103) >= convert(date,'" + txtfromDate.Value + "',103) and  convert(date, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Deduction_Date,103) <= convert(date,'" + txtToDate.Value + "',103) and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Transporter_code = '" + clsCommon.myCstr(txtTransport.Value) + "'   " + strRuteSelForTransporterDeduction + "

                          ) XXXFinal group by Transporter_code,Route_Code,Deduction_Date order by Route_Code  ,convert (date,Deduction_Date,103) "

            End If
            'QueryForAllZoneQty = " select Convert(decimal(18,2),sum((TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor)) as Booking_Qty  " & _
            '" from TSPL_BOOKING_MATSER " & _
            '" left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No " & _
            '" left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_id=TSPL_BOOKING_DETAIL.Vehicle_Code " & _
            '" left join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_BOOKING_DETAIL.Unit_code " & _
            '" left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " & _
            '" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code " & _
            '" left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code " & _
            '" where 2=2 " & _
            '" and convert(date, TSPL_BOOKING_MATSER.Document_date,103) >=  convert(date,'" + txtfromDate.Value + "',103)  and  convert(date, TSPL_BOOKING_MATSER.Document_date,103) <= convert(date,'" + txtToDate.Value + "',103) " & _
            '" and TSPL_VEHICLE_MASTER.Transport_Id= '" + clsCommon.myCstr(txtTransport.Value) + "'"

            'If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
            '    QueryForAllZoneQty += " and TSPL_ZONE_MASTER.Zone_Code in(" + clsCommon.GetMulcallString(txtmultiZone.arrValueMember) + ")" + Environment.NewLine
            'End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True

                gv1.EnableFiltering = True
                'RadGroupBox3, RadGroupBox1 , txtTransport,txtmultiZone, txtmultiRoute 
                RadGroupBox3.Enabled = False
                RadGroupBox1.Enabled = False
                txtTransport.Enabled = False
                txtmultiZone.Enabled = False
                txtmultiRoute.Enabled = False
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()

            ReStoreGridLayout()
            gv1.BestFitColumns()

            If chkNone.Checked = True Then
                Dim Total As Double = 0
                For i As Integer = 0 To gv1.Rows.Count - 1
                    Total += clsCommon.myCdbl(gv1.Rows(i).Cells("Total").Value)
                Next

                txtRate.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(Price_Ltr_KG) from TSPL_VEHICLE_MASTER WHERE Transport_Id='" & txtTransport.Value & "'"))
                txtQuantity.Value = clsCommon.myCdbl(Total)
                txtAmt.Value = Math.Round(clsCommon.myCdbl(txtRate.Value * txtQuantity.Value), 2)
                'llZone.Text = lblZone.Text

                txtN_QtyofMilk.Value = clsCommon.myCdbl(Total)
                txtN_PTC.Value = Math.Round(clsCommon.myCdbl(txtRate.Value * txtQuantity.Value), 2)
                txtN_NCDFI.Value = Math.Round((txtN_PTC.Value * 0.3) / 100, 2)
                txtN_PLUS.Value = Math.Round((txtN_NCDFI.Value * 18) / 100, 2)
                txtN_NetPayable.Value = Math.Round(txtN_PTC.Value - (txtN_NCDFI.Value + txtN_PLUS.Value), 2)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
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

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next
        If chkNone.Checked = True Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("PM Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("TrSheet", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("AM Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf chkRouteWise.Checked = True OrElse chkCrateAccountDateWise.Checked = True Then

            If gv1.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = 1 To gv1.Columns.Count - 1
                    Dim aa = gv1.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

        ElseIf chkZoneWise.Checked = True OrElse chkCrateAccountZonewise.Checked = True Then
            If gv1.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = 2 To gv1.Columns.Count - 1
                    Dim aa = gv1.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
        ElseIf chkTransporterDeduction.Checked = True OrElse chkTransporterDeduction.Checked = True Then
            If gv1.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = 2 To gv1.Columns.Count - 2
                    Dim aa = gv1.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            gv1.Columns("SOC_NoOfCrates").HeaderText = "Shortage Of Crates" + Environment.NewLine + "No Of Crates"
            gv1.Columns("SOC_NoOfCrates_Amount").HeaderText = "Shortage Of Crates" + Environment.NewLine + "Amount in Rs."
            gv1.Columns("ELOM_QTY").HeaderText = "Excess Loading Of Milk" + Environment.NewLine + "Qty(Ltrs)"
            gv1.Columns("ELOM_Amount").HeaderText = "Excess Loading Of Milk" + Environment.NewLine + "Amount in Rs."
            gv1.Columns("VC_TopLess").HeaderText = "Vehicle Condition" + Environment.NewLine + "TopLess"
            gv1.Columns("VC_LOGO").HeaderText = "Vehicle Condition" + Environment.NewLine + "Logo"
            gv1.Columns("VC_InnerBodyPainting").HeaderText = "Vehicle Condition" + Environment.NewLine + "Inner Body Painting"
            gv1.Columns("VC_Cleaniness").HeaderText = "Vehicle Condition" + Environment.NewLine + "Cleaniness"
            gv1.Columns("VC_BottomDamage").HeaderText = "Vehicle Condition" + Environment.NewLine + "BottomDamage"
            gv1.Columns("VC_Shelf").HeaderText = "Vehicle Condition" + Environment.NewLine + "Shelf"
            gv1.Columns("VC_Light").HeaderText = "Vehicle Condition" + Environment.NewLine + "Light"
            gv1.Columns("VC_Amount").HeaderText = "Vehicle Condition" + Environment.NewLine + "Total Amount"
            gv1.Columns("LateVehicleReport_Amount").HeaderText = "Late Vehicle Report"
            gv1.Columns("ShortageOfLoadingStaffSupervisors_Amount").HeaderText = ""
            gv1.Columns("ShortageOfLoadingStaffSupervisors_Amount").HeaderText = "Shortage Of Loading" + Environment.NewLine + "Staff Supervisors"
            gv1.Columns("Net_Amount").HeaderText = "Net Amount"

        End If

    End Sub
    Sub Reset()
        'txtmultiZone.arrValueMember = Nothing
        'txtmultiRoute.arrValueMember = Nothing
        lblTransporter.Text = ""
        txtTransport.Value = ""
        txtRate.Value = 0
        txtAmt.Value = 0
        txtQuantity.Value = 0
        txtN_QtyofMilk.Value = 0
        txtN_PTC.Value = 0
        txtN_NCDFI.Value = 0
        txtN_PLUS.Value = 0
        txtN_NetPayable.Value = 0
        'txtfromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        RadGroupBox3.Enabled = True
        RadGroupBox1.Enabled = True
        txtTransport.Enabled = True
        txtmultiZone.Enabled = True
        txtmultiRoute.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            If clsCommon.myLen(txtTransport.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Plz Select Transporter.", Me.Text)
                txtTransport.Focus()
                Exit Sub
            End If
            'If clsCommon.myLen(txtZone.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Plz Select Zone.", Me.Text)
            '    txtZone.Focus()
            '    Exit Sub
            'End If
            If chkNone.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + chkNone.Text
            ElseIf chkRouteWise.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + chkRouteWise.Text
            ElseIf chkZoneWise.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + chkZoneWise.Text
            ElseIf chkCrateAccountDateWise.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "AccDay"
            ElseIf chkCrateAccountZonewise.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "AccZone"
            ElseIf chkTransporterDeduction.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "TrpDed"
            End If

            TemplateGridview = gv1
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            Print(Exporter.Refresh)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        txtfromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If chkNone.Checked = True Then
                    arrHeader.Add("Name : Transporter Wise Report")
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                    arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                    'If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                    arrHeader.Add("Transporter : " + txtTransport.Value)
                    'End If
                    If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
                        arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtmultiZone.arrValueMember))
                    End If
                ElseIf chkRouteWise.checked OrElse chkZoneWise.Checked = True Then

                    Dim strCity As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Upper (City_Code) as City_Code  from tspl_Company_master where Comp_code = '" + objCommonVar.CurrentCompanyCode + "' "))
                    arrHeader.Add(objCommonVar.CurrentCompanyName.ToUpper())
                    arrHeader.Add("MILK PRODUCTS FACTORY - " + strCity + "")
                    arrHeader.Add("DISPATCH SECTION")
                    If chkZoneWise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " ZONE WISE SALES FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                    If chkRouteWise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " ROUTE WISE SALES FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                    If chkCrateAccountDateWise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " DAY WISE CRATE ACCOUNT FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                    If chkCrateAccountZonewise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " ZONE WISE CRATE ACCOUNT FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                ElseIf chkTransporterDeduction.checked = True Then
                    Dim strCity As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Upper (City_Code) as City_Code  from tspl_Company_master where Comp_code = '" + objCommonVar.CurrentCompanyCode + "' "))
                    arrHeader.Add(objCommonVar.CurrentCompanyName.ToUpper() + " -MPF: " + strCity)
                    arrHeader.Add("PTC Recoveries towards deviation of agreements and tender conditions")
                    arrHeader.Add("Section : Dispatch")
                    arrHeader.Add(" Name of the transport :" + lblTransporter.Text + "              From:  " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " To: " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")

                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If chkNone.Checked = True Then
                    arrHeader.Add(("From : " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To : " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                    arrHeader.Add("Transporter : " + lblTransporter.Text)
                    If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
                        arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtmultiZone.arrValueMember))
                    End If
                ElseIf chkTransporterDeduction.Checked = True Then
                    Dim strCity As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Upper (City_Code) as City_Code  from tspl_Company_master where Comp_code = '" + objCommonVar.CurrentCompanyCode + "' "))
                    arrHeader.Add(objCommonVar.CurrentCompanyName.ToUpper() + " -MPF: " + strCity)
                    arrHeader.Add("PTC Recoveries towards deviation of agreements and tender conditions")
                    arrHeader.Add("Section : Dispatch")
                    arrHeader.Add("Name of the transport :" + lblTransporter.Text + "                            From:  " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " To: " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                Else
                    Dim strCity As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Upper (City_Code) as City_Code  from tspl_Company_master where Comp_code = '" + objCommonVar.CurrentCompanyCode + "' "))
                    arrHeader.Add(objCommonVar.CurrentCompanyName.ToUpper())
                    arrHeader.Add("MILK PRODUCTS FACTORY - " + strCity + "")
                    arrHeader.Add("DISPATCH SECTION")
                    If chkZoneWise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " ZONE WISE SALES FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                    If chkRouteWise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " ROUTE WISE SALES FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                    If chkCrateAccountDateWise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " DAY WISE CRATE ACCOUNT FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                    If chkCrateAccountZonewise.Checked = True Then
                        arrHeader.Add("" + lblTransporter.Text.ToUpper() + " ZONE WISE CRATE ACCOUNT FOR THE PERIOD " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "")
                    End If
                End If


                Dim arrFooter As List(Of String) = New List(Of String)()
                If chkNone.Checked = True Then
                    arrFooter.Add("Rate   : " & clsCommon.myCstr(txtRate.Value))
                    arrFooter.Add("Qty    : " & clsCommon.myCstr(txtQuantity.Value))
                    arrFooter.Add("Amount : " & clsCommon.myCstr(txtAmt.Value))
                    arrFooter.Add(" ")
                    arrFooter.Add("    ABSTRACT")
                    arrFooter.Add("1. Qty Of milk Transported : " + clsCommon.myCstr(txtN_QtyofMilk.Value))
                    arrFooter.Add("2. PTC Commission          : " + clsCommon.myCstr(txtN_PTC.Value))
                    arrFooter.Add("3. NCDFI 0.3%              : " + clsCommon.myCstr(txtN_NCDFI.Value))
                    arrFooter.Add("4. PLUS GST 18% ON 0.3%    : " + clsCommon.myCstr(txtN_PLUS.Value))
                    arrFooter.Add("5. Net Payable             : " + clsCommon.myCstr(txtN_NetPayable.Value))
                ElseIf chkRouteWise.Checked = True OrElse chkZoneWise.Checked = True Then
                    Dim PenaltiesAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Net_Amount) as Net_Amount from TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL left outer join TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER on TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.document_code=TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.document_code where TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.IsPosted='Y' and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.transporter_code='" + txtTransport.Value + "' and convert(date, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.deduction_date,103) >= convert(date,'" + txtfromDate.Value + "',103) and  convert(date, TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.deduction_date,103) <= convert(date,'" + txtToDate.Value + "',103)"))
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    'arrFooter.Add("Note: The PTC Penalties for the above period is Rs._______    /-")
                    arrFooter.Add("Note: The PTC Penalties for the above period is Rs. " + clsCommon.myCstr(PenaltiesAmt) + " /-")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("DAIRY MANAGER")
                    arrFooter.Add("(DISPATCH SECTION)")
                ElseIf chkCrateAccountDateWise.Checked = True OrElse chkCrateAccountZonewise.Checked = True Then
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("Remarks:")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("DAIRY MANAGER")
                    arrFooter.Add("(DISPATCH SECTION)")
                ElseIf chkTransporterDeduction.Checked = True Then
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("Sr.Asst/JM(Dispatch)                                            ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("                                                                ")
                    arrFooter.Add("Dairy Manager")
                    arrFooter.Add("(Dispatch)")

                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(objCommonVar.CurrentCompanyName, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode, arrFooter)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtTransport__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTransport._MYValidating
        Try
            Dim qry As String = " select Transport_Id as Code, Transporter_Name as Name from TSPL_TRANSPORT_MASTER"
            txtTransport.Value = clsCommon.ShowSelectForm("TransFnder1", qry, "Code", "", txtTransport.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtTransport.Value) > 0 Then
                lblTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transporter_Name from tspl_transport_Master WHERE Transport_Id='" + txtTransport.Value + "'"))
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtmultiZone__My_Click(sender As Object, e As EventArgs) Handles txtmultiZone._My_Click
        Try
            Dim qry As String = " select Zone_Code as Code, Description as Name from TSPL_Zone_MASTER "
            txtmultiZone.arrValueMember = clsCommon.ShowMultipleSelectForm("Zone@Sel", qry, "Code", "Name", txtmultiZone.arrValueMember, txtmultiZone.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtmultiRoute__My_Click(sender As Object, e As EventArgs) Handles txtmultiRoute._My_Click
        Try
            Dim qry As String = " Select Route_No as Code, Route_Desc as Name from TSPL_ROUTE_MASTER "
            txtmultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RoutMulti@Sel@Transpoter", qry, "Code", "Name", txtmultiRoute.arrValueMember, txtmultiRoute.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
