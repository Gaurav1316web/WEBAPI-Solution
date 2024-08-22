Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine

'Sanjay Ticket No-TEC/17/05/19-000497,TEC/17/05/19-000498,TEC/17/05/19-000503,TEC/17/05/19-000493,TEC/17/05/19-000494,TEC/17/05/19-000500,TEC/17/05/19-000488,ERO/06/06/19-000637,TEC/17/05/19-000490,TEC/17/05/19-000491
'TEC/17/05/19-000499
Public Class rptAuditTrailReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strModuleCode As String = Nothing


    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try

            If clsCommon.myLen(fndScreen.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Screen ....", Me.Text)
                Exit Sub
            End If
            If txtToDate.Value < txtFromDate.Value Then
                clsCommon.MyMessageBoxShow(Me, "To Date cant be less than from date", Me.Text)
                Exit Sub
            End If
            Dim strMoudleName As String = lblModule.Text
            Dim ScrScreenName As String = lblScreen.Text
            Dim ScreenType As String = Nothing
            If rdbMaster.Checked = True Then
                ScreenType = "Master"
            Else
                ScreenType = "Transation"
            End If
            Dim qry As String = Nothing
            Dim StrOrderBy As String = Nothing

            If clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleSaleDairy) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmZoneMasterDS) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Zone_Code as [Document Code], TSPL_ZONE_MASTER.Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ZONE_MASTER " &
                          "  where 2= 2  and  Convert (date, TSPL_ZONE_MASTER.Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, TSPL_ZONE_MASTER.Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmRouteMasterDS) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Route_No as [Document Code],Route_Desc as [Description],'' as [Document Date],Created_By as [Created By],Convert(varchar,Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date],'NA' as [Status] from tspl_route_master " &
                          "  where Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDistributorRouteTagging) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code as [Document Code],'' as [Description],Created_Date as [Document Date],Created_By as [Created By],Convert(varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date],Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_DISTRIBUTOR_ROUTE " &
                          "  where Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDistributorCommission) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Doc_No as [Document Code],'' as [Description],Document_Date as [Document Date],Created_By as [Created By],Convert(varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date],Case when  IsPosted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_Distributor_Commission_Head " &
                          "  where Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSchemeMasterDairyDS) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Scheme_Code as [Document Code],Scheme_Desc as [Description] ,'' as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SCHEME_MASTER_NEW " &
                          " where Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.SaleIncentiveMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], INCENTIVE_CODE as [Document Code],Description as [Description] ,Convert (varchar,INCENTIVE_DATE,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SALES_INCENTIVE_HEADER " &
                          " where Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomerDeduction) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Deduction_Code as [Document Code],Deduction_Name as [Description] ,'' as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_CUSTOMER_DEDUCTION_HEAD " &
                          "  where Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime,zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmbookingdairy) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_BOOKING_MATSER " &
                          " where From_Screen_Code<>'BOOK-DS-CU' and Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeliveryOrderDairy) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " &
                          "  where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSaleDispatchDairy) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SD_SHIPMENT_HEAD " &
                          "  where Screen_Type='DS'  and Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDemandBooking) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_DEMAND_BOOKING_MASTER " &
                          "  where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDCSDEmandBooking) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_DCS_DEMAND_BOOKING_MASTER " &
                          "  where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSaleInvoicedairy) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SD_SALE_INVOICE_HEAD " & _
                          " where Screen_Type='DS'  and Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSaleReturndairy) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SD_SALE_RETURN_HEAD " & _
                          " where Screen_Type='DS'  and Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGatePassDairy) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], 'NA' as [Status] from TSPL_GATEPASS_MASTER_DAIRYSALE " & _
                          " where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCrateReceviedDairySale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE " & _
                          " where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBookingDairyMultipleDistributor) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Main_Booking_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_BOOKING_MATSER " & _
                          " where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date,'" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPOSBookingDairyMultipleDistributor) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Main_Booking_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_POS_BOOKING_MATSER " & _
                          " where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDairyGatePass) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], GPCode as [Document Code],'' as [Description] ,convert (varchar, GPDate,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Post = 'Y' then 'Posted' else 'Pending' end as [Status] from TSPL_DAIRYSALE_GATEPASS_MASTER " & _
                          " where  Convert (date, GPDate,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, GPDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDairyBookingCustomer) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_BOOKING_MATSER " & _
                          " where From_Screen_Code='BOOK-DS-CU' and  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPerformaInvoiceDairy) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from tspl_dairy_proforma_invoice_head " & _
                          " where  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                    ''ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmItemProjection) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], projection_code as [Document Code],'' as [Description] ,convert (varchar, projection_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Post_Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_ITEM_PROJECTION_HEAD " & _
                          " where  Convert (date, projection_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, projection_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomerIncentiveEntry) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Doc_Code as [Document Code],'' as [Description] ,convert (varchar, Doc_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD " & _
                          " where  Convert (date, Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "


                End If
                '==================================== Fresh Sale ==============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleFreshSale) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmZoneMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Zone_Code as [Document Code], TSPL_ZONE_MASTER.Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ZONE_MASTER " & _
                          "  where 2= 2  and Convert (date, TSPL_ZONE_MASTER.Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, TSPL_ZONE_MASTER.Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmRouteMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Route_No as [Document Code],Route_Desc as [Description],'' as [Document Date],Created_By as [Created By],Convert(varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date],'NA' as [Status] from tspl_route_master " & _
                          "  where Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSalesLevelHierarchy) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Level_Code as [Document Code],Description as [Description] ,'' as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], 'NA' as [Status] from TSPL_Sales_Hierarchy_Levels " & _
                          " where  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBookingEntry) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_BOOKING_MATSER " & _
                          " where From_Screen_Code='' and  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeliveryNoteFreshSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
                          " where From_Screen_Code='' and  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmDispatchFreshSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SD_SHIPMENT_HEAD " & _
                          " where  Trans_Type='FS' and Screen_Type='' and  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmInvoiceFreshSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SD_SALE_INVOICE_HEAD " & _
                          "  where  Trans_Type='FS' and Screen_Type='' and  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCreateReceived) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE " & _
                          " where    Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSaleReturnFreshSale) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when  Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SD_SALE_RETURN_HEAD " & _
                          " where Trans_Type='FS' and Screen_Type='' and   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmGatePassFS) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], GPCode as [Document Code],'' as [Description] ,convert (varchar, GPDate,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], 'NA' as [Status] from TSPL_GATEPASS_MASTER_FRESHSALE " & _
                          " where   Convert (date, GPDate,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, GPDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCrateReceviedDairySale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE " & _
                          " where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmInvoiceCrateLinerDetail) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,MODIFY_BY as [Modified By],Convert (varchar,MODIFY_DATE,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_INVOICE_CRATE_LINER_HEAD " & _
                          " where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmsaleReturnGateEntryFS) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Gate_Entry_No as [Document Code],'' as [Description] ,convert (varchar, Gate_Entry_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,MODIFY_BY as [Modified By],Convert (varchar,MODIFY_DATE,103) as [Modified Date], Case when  Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SALE_RETURN_GATE_ENTRY_HEAD " & _
                          " where   Convert (date, Gate_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Gate_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSubsidyCreditNote) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], ''  as [Status] from TSPL_SUBSIDY_CRADIT_NOTE " & _
                          " where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                End If
                '==================================== Product Sale ==============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleProductSale) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSchemeMasterDairy) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Scheme_Code as [Document Code],Scheme_Desc as [Description] ,'' as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], 'NA'  as [Status] from TSPL_SCHEME_MASTER_NEW " & _
                          "  where   Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmTragetMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Target_Code as [Document Code],Target_Desc as [Description] ,Convert (varchar, DocumentDate,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], 'NA'  as [Status] from TSPL_TARGET_MASTER_HEAD " & _
                          " where   Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.DispatchChecklist) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Code as [Document Code],Description as [Description] ,'' as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], 'NA'  as [Status] from TSPL_DISPATCH_CHECKLIST_MASTER " & _
                          " where   Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBookingProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Status = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_BOOKING_MASTER_PRODUCTSALE " & _
                          " where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date,'" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmdispatchAdviceProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Posted = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_DISPATCH_ADVICE_HEAD_PRODUCTSALE " & _
                          "  where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeliveryPrderProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Status = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_SD_SALES_ORDER_HEAD " & _
                          " where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSalesOrderProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Posted = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE " & _
                          " where   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmShipmentProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Status = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_SD_SHIPMENT_HEAD " & _
                          " where TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' and TSPL_SD_SHIPMENT_HEAD.Screen_Type='' and   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSaleInvoiceProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Status = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_SD_SALE_INVOICE_HEAD  " & _
                          " where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and Screen_Type='' and   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSaleReturnProductSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code as [Document Code],Description as [Description] ,convert (varchar, Document_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Status = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_SD_SALE_RETURN_HEAD " & _
                          " where TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='' and   Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmProductDispatchGateOut) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],'' as [Description] ,convert (varchar, Gate_Out_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], 'NA'  as [Status] from TSPL_Product_Dispatch_Gate_Out " & _
                          "  where   Convert (date, Gate_Out_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Gate_Out_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGateEntryReturnPS) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], GE_CODE as [Document Code],'' as [Description] ,convert (varchar, GE_DATE,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when Posted = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_GATE_ENTRY_RETURN_PS " & _
                          "  where   Convert (date, GE_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, GE_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmsaleReturnGateEntryPS) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Gate_Entry_No as [Document Code],'' as [Description] ,convert (varchar, Gate_Entry_Date,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],Convert (varchar,Modify_Date,103) as [Modified Date], Case when Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_SALE_RETURN_GATE_ENTRY_HEAD " & _
                          "  where   Convert (date, Gate_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Gate_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmGatePassPS) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], GPCode as [Document Code],'' as [Description] ,convert (varchar, GPDate,103) as [Document Date],Created_By as [Created By],convert (varchar,Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],Convert (varchar,Modified_Date,103) as [Modified Date], Case when Post = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_GATEPASS_master_ProductSale " & _
                          " where   Convert (date, GPDate,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, GPDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                End If
                '==================================== Electrical ==============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleElectrical) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSlotMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SLOT_MASTER " & _
                          "  where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDGMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_DG_MASTER " & _
                          " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "',103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDailyElectricalEntry) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Remarks as [Description] ,Convert (varchar,Document_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                End If
                '==================================== Job Work Inward ==============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleJobWorkInWard) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJWParameterMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_JW_Parameter_MASTER    " & _
                          " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)    "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJWFormulaMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_JW_FORMULA " & _
                          "  where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date,  '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJWVendorFormula) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DocCode as [Document Code], '' as [Description] ,DocDate as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Posted_By as [Modified By],convert(varchar, Posted_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end  as [Status] from TSPL_VENDOR_FORMULA_MAPPING    " & _
                          " where 2= 2  and Convert (date,Posted_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Posted_Date,103)  < = convert ( date,  '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.JWIItemPriceMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], Description as [Description] ,convert (varchar,Price_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Posted_By as [Modified By],convert(varchar, Posted_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end  as [Status] from TSPL_JWI_PRICE_HEAD " & _
                          "  where 2= 2  and Convert (date,Price_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Price_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)    "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSRNJobWorkEstimate) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as [Description] ,convert (varchar,Document_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end  as [Status] from TSPL_JWI_ESTIMATION_HEAD " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJobWorkBillig) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], Description as [Description] ,convert (varchar,Document_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end  as [Status] from TSPL_JOBWORK_BILLING_HEAD " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                End If
                '==================================== Job Work Outward==============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleJobWorkOutWard) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJobWorkoutwordMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA'  as [Status] from TSPL_JOB_OUTWARD_PRICE_MASTER    " & _
                          " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJobWorkoutwordMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], '' as [Description] ,convert ( varchar, Price_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA'  as [Status] from TSPL_JOB_OUTWARD_PRICE_HEAD    " & _
                          " where 2= 2  and Convert (date,Price_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Price_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkJobWorkTransfer) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], '' as [Description] ,convert ( varchar, Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when  isPosted = 1 then 'Posted' else 'Pending' end  as [Status] from TSPL_MILK_JOBWORK_TRANSFER_HEAD " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkJobWorkTransferReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Remarks as [Description] ,convert ( varchar, Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],'NA'  as [Status] from TSPL_MILK_JOBWORK_TRANSFER_RETURN  " & _
                          "  where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkJobWorkTransferOther) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TRANSFER_NO as [Document Code], Remarks as [Description] ,convert ( varchar, TRANSFER_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end  as [Status] from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD " & _
                          " where 2= 2  and Convert (date,TRANSFER_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, TRANSFER_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkJobWorkTransferOtherReturn) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Remarks as [Description] ,convert ( varchar, Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],'' as [Status] from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN    " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date,'" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGateEntry_JWO) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Gate_Entry_No as [Document Code], '' as [Description] ,convert ( varchar, Date_And_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_JWO_GATE_ENTRY " & _
                          " where 2= 2  and Convert (date,Date_And_Time,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Date_And_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmWeighment_JWO) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Weighment_No as [Document Code], '' as [Description] ,convert ( varchar, Weighment_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_JWO_Weighment  " & _
                          " where 2= 2  and Convert (date,Weighment_Date,103)  > = Convert (date,  '" + txtFromDate.Value + "' ,103) and Convert (date, Weighment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmQC_JWO) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],QC_No as [Document Code], '' as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_JWO_QUALITY_CHECK " & _
                          " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmUnloading_JWO) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Unloading_No as [Document Code], '' as [Description] ,convert (varchar,Unloading_Date_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_JWO_UNLOADING " & _
                          " where 2= 2  and Convert (date,Unloading_Date_Time,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Unloading_Date_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.JWO_SRN) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as [Description] ,convert (varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_JWO_SRN_HEAD    " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.JWO_SRN_Return) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as [Description] ,convert (varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],'' as [Status] from TSPL_JWO_SRN_RETURN  " & _
                          " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmJobWorkConsumption) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Adjustment_No as [Document Code], Description as [Description] ,convert (varchar,Adjustment_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],Case when Posted = 'Y' then 'Posted' else 'Pending' end as [Status] from TSPL_ADJUSTMENT_HEADER    " & _
                          " where 2= 2  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103) "
                End If

                '==============Milk Procurement MCC======================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleMCCMilkProcurement) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],MCC_Code as [Document Code], MCC_NAME as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_mcc_master " &
                      "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkRouteMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Route_Code as [Document Code], Route_Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_mcc_route_master " &
                      "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVillageMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Village_Code as [Document Code], Village_Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_village_master " &
                      "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVSPMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Vendor_Code as [Document Code], Vendor_Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VENDOR_MASTER " &
                      "  where 2= 2 and form_type='VSP' and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVLCMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],VLC_Code as [Document Code], VLC_Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VLC_MASTER_HEAD " &
                      "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVLCUploader) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],VLC_Code as [Document Code], '' as Description,convert (varchar,TSPL_VLC_UPLOADER_MASTER.Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VLC_UPLOADER_MASTER " &
                      "  where 2= 2  and  Convert (date, TSPL_VLC_UPLOADER_MASTER.Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, TSPL_VLC_UPLOADER_MASTER.Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMPMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],MP_Code as [Document Code], MP_Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_mp_master " &
                      "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPrimaryTransporterMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Vendor_Code as [Document Code], Vendor_Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VENDOR_MASTER " &
                      "  where 2= 2 and form_type='PTM' and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPrimaryTransporterVehicalMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Vehicle_Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_Primary_Vehicle_Master " &
                      "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVLCRouteShiftMaster) = CompairStringResult.Equal Then
                    qry = " SELECT distinct '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], Description as Description,convert (varchar,Doc_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_vlc_route_shift_master " &
                      "  where 2= 2  and  Convert (date, Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkPricePlanning) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Planning_Code as [Document Code], Planning_Description as Description,convert (varchar,Planning_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PRICE_CHART_PLANNING " &
                 "  where 2= 2  and  Convert (date, Planning_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Planning_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPriceChartMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code],Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_PRICE_MASTER " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmPriceChartUploader) = CompairStringResult.Equal Then
                    qry = " SELECT distinct '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code as [Document Code], '' as Description,convert (varchar,TSPL_FAT_SNF_UPLOADER_MASTER.date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_FAT_SNF_UPLOADER_MASTER " &
                 "  where 2= 2  and  Convert (date, TSPL_FAT_SNF_UPLOADER_MASTER.date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, TSPL_FAT_SNF_UPLOADER_MASTER.date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPriceChartMaster_Bulk) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_PRICE_MASTER " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCMaterialSalePriceChart) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code as [Document Code], '' as Description,convert (varchar,TSPL_MCC_RATE_UPLOADER_MASTER.date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_MCC_RATE_UPLOADER_MASTER " &
                 "  where 2= 2  and  Convert (date, TSPL_MCC_RATE_UPLOADER_MASTER.date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, TSPL_MCC_RATE_UPLOADER_MASTER.date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkVehicleMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Vehicle_Id as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VEHICLE_MASTER " &
                 "  where 2= 2  and  Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkAdvanceMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_JOB_OUTWARD_PRICE_MASTER " &
                 "  where 2= 2  and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmdeductionGroup) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Ded_Code as [Document Code], Ded_Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_DEDUCTION_GROUP " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date,Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGroupOfDeduction) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Ded_Code as [Document Code], Ded_Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_DEDUCTION_GROUP " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeductionMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_DEDUCTION_MASTER " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.DeductionMapping) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], Description as Description,convert (varchar,Doc_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Post_Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_DEDUCTION_MAPPING_HEAD " &
                 "  where 2= 2  and  Convert (date, Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CanMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_CAN_MASTER " &
                 "  where 2= 2  and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date,Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.DockMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_DOCK_MASTER " &
                 "  where 2= 2  and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmTankerDispatchPriceMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PRICE_CODE as [Document Code], PRICE_DESC as Description,convert (varchar,PRICE_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_TANKER_DISPATCH_PRICE_MASTER " &
                 "  where 2= 2  and  Convert (date, PRICE_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, PRICE_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkRejectType) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CODE as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_MILK_REJECT_TYPE " &
                 "  where 2= 2  and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkReasonMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CODE as [Document Code], Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from Tspl_Mcc_Reason_Master " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date,Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPaymentCycleMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PC_CODE as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PAYMENT_CYCLE_MASTER " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmIncentiveMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],INCENTIVE_CODE as [Document Code], Description as Description,convert (varchar,INCENTIVE_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_INCENTIVE_MASTER_HEAD " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCSMSSettiing) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Program_Code as [Document Code], _Name as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_MCC_MAIL_SMS_Setting " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOpenMCCShiftManual) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],MCC_SHIFT_CODE as [Document Code], '' as Description,convert (varchar,MCC_SHIFT_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                     " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_OPEN_MCC_SHIFT " &
                     "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmVSPIncentiveTagging) = CompairStringResult.Equal Then
                    qry = " SELECT DISTINCT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], '' as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VSP_INCENTIVE_Detail " &
                 "  where 2= 2  and  Convert (date, Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVLCMasterTarget) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], '' as Description,convert (varchar,Document_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 'Y' then 'Posted' else 'Pending'  end as [Status] from TSPL_Vlc_Target_Detail " &
                 "  where 2= 2  and  Convert (date, Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date,Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmItemChargeCategoryMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Charge_Cat_Code as [Document Code], Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from tspl_charge_category " &
                 "  where 2= 2  and  Convert (date, Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FreightChargesMaster) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Freight_Code as [Document Code], freight_Description as Description,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_FREIGHT_CHARGES_MASTER " &
                 "  where 2= 2  and  Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "

                    '======Milk Procurement MCC Detail------
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOpenMCCShift) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],MCC_SHIFT_CODE as [Document Code], '' as Description,convert(varchar,MCC_SHIFT_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_OPEN_MCC_SHIFT " &
                      "  where 2= 2  and  Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkGateEntryIn) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Entry_Code as [Document Code], '' as Description,convert(varchar,Entry_Date,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_GATE_ENTRY_IN " &
                      "  where 2= 2  and  Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkGateEntryWeightment) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Weighment_Code as [Document Code], '' as Description,'' as [Document Date],GW_Created_By as [Created By],Convert(varchar, GW_Created_Date,103) as [Created Date] " &
                     " ,GW_Modified_By as [Modified By],convert(varchar, GW_Modified_Date,103) as [Modified Date],case when GW_Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_GATE_ENTRY_WEIGHTMENT " &
                     "  where 2= 2  and  Convert (date,GW_Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, GW_Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkReceipt) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOC_CODE as [Document Code], '' as Description,DOC_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                    " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_RECEIPT_HEAD " &
                    "  where 2= 2  and  Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkReject) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOC_CODE as [Document Code], '' as Description,DOC_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                  " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_REJECT_HEAD " &
                  "  where 2= 2  and  Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkSample) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOC_CODE as [Document Code], '' as Description,DOC_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                    " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_milk_Sample_Head " &
                    "  where 2= 2  and  Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkGateEntryOut) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Gate_Out_Code as [Document Code], '' as Description,Gate_Out_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_GATE_ENTRY_OUT " &
                          "  where 2= 2  and  Convert (date,Gate_Out_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Gate_Out_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkSRN) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOC_CODE as [Document Code], '' as Description,DOC_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_SRN_Head " &
                         "  where 2= 2  and  Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MilkTruckSheet) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOC_CODE as [Document Code], '' as Description,DOC_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_milk_truck_sheet_Head " &
                        "  where 2= 2  and  Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVlcdataUploadar) = CompairStringResult.Equal Then
                    qry = " SELECT distinct '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], '' as Description,DOC_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                    " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_vlc_data_uploader " &
                    "  where 2= 2  and  Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmVLCDataUploaderManual) = CompairStringResult.Equal Then
                    qry = " SELECT distinct '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], '' as Description,Document_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VLC_DATA_UPLOADER_MASTER " &
                        "  where 2= 2  and  Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkShiftEndMCC) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], '' as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                       " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_Shift_End_HEAD " &
                       "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCGateEntry) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Remarks as Description,Document_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                    " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_GATE_ENTRY " &
                    "  where 2= 2  and  Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCWeighment) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as Description,Document_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                   " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Status_Gross_Weight = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_WEIGHMENT " &
                   "  where 2= 2  and  Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCTankerGateOut) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],GATE_OUT_NO as [Document Code], '' as Description,GATE_OUT_DATE as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when IS_POSTED = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_TANKER_GATE_OUT " &
                "  where 2= 2  and  Convert (date,GATE_OUT_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, GATE_OUT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCDispatch) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Chalan_NO as [Document Code], '' as Description,Dispatch_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
               " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when ISPOSTED = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_mcc_dispatch_challan " &
               "  where 2= 2  and  Convert (date,Dispatch_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Dispatch_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MCCDispatchReturn) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_NO as [Document Code], Remarks as Description,Document_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                     " ,'' as [Modified By],'' as [Modified Date],'NA' as [Status] from TSPL_MCC_Dispatch_Challan_Return " &
                     "  where 2= 2  and  Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDispatchTransfer) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_NO as [Document Code], '' as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                   " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_MCC_dispatch_transfer " &
                   "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkPurchaseInvoice) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], Description as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_PURCHASE_INVOICE_HEAD " &
                "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmVSPAssetIssue) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], Remarks as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                      " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_VSPAsset_HEAD " &
                      "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCMaterial) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], Description as Description,Document_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                  " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_SD_SHIPMENT_HEAD " &
                  "  where 2= 2  and  Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCMaterialSaleReturn) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], Description as Description,Document_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
               " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_SD_SALE_RETURN_HEAD " &
               "  where 2= 2  and  Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], Remarks as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                        " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_VSPItem_HEAD " & _
                        "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProvisionEntry) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], '' as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PROVISION_ENTRY " & _
                        "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.IncentiveEntry) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], '' as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                      " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when is_Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_INCENTIVE_ENTRY_HEAD " & _
                      "  where 2= 2  and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPaymentProcess) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], PaymentDesc as Description,Doc_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PAYMENT_PROCESS_HEAD " & _
                          "  where 2= 2 and FarmType='PP' and  Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCTankerDispatchReturn) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Return_NO as [Document Code], Remarks as Description,Return_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_Tanker_Dispatch_Return_head " & _
                         "  where 2= 2 and  Convert (date,Return_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Return_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmsaleReturnGateEntryMCCSAle) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Gate_Entry_No as [Document Code], Remarks as Description,Gate_Entry_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_SALE_RETURN_GATE_ENTRY_HEAD " & _
                         "  where 2= 2 and  Convert (date,Gate_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Gate_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMccScrapGatePass) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],GPCode as [Document Code], Remarks as Description,GPDate as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                   " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_SCRAP_GATEPASS_MASTER " & _
                   "  where 2= 2 and  Convert (date,GPDate,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, GPDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MccMilkTransferPrice) = CompairStringResult.Equal Then
                    qry = " SELECT '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], '' as Description,Price_Date as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                 " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_MILK_TRANSFER_PRICE " & _
                 "  where 2= 2 and  Convert (date,Price_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Price_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                End If
                '------------------------- Farmer Payment [TEC/17/05/19-000504]--------------------------------------------------------------
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleFarmerPayment) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCMaterialFarmer) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], '' as [Description],convert (varchar,Document_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],Case when Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_MCC_Sale_Farmer_Head " & _
                          " where 2= 2  and Convert (date,Document_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "


                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMCCMaterialSaleReturnFarmer) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], '' as [Description],convert (varchar,Document_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],Case when Status = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_MCC_Sale_Return_Head_Farmer " & _
                          " where 2= 2  and Convert (date,Document_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Document_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], '' as [Description],convert (varchar,Doc_DATE,103) as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],Case when Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_MILK_PURCHASE_INVOICE_HEAD    " & _
                          "  where 2= 2  and Convert (date,Doc_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPaymentProcessFarmer) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], '' as [Description] ,convert (varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],Case when isPosted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_PAYMENT_PROCESS_HEAD " & _
                          " where 2= 2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmFarmerPaymentAdjustment) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Adjustment_No as [Document Code], Description as [Description] ,convert (varchar,Adjustment_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, Created_Date,103) as [Modified Date],Case when Is_Post = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_MP_Pay_Adj_Head " & _
                          " where 2= 2  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLockMPCollectionPC) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LOCK_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],Case when Posted = 1 then 'Posted' else 'Pending' end as [Status] from TSPL_LOCK_MP_PC " & _
                          " where 2= 2  and Convert (date,MODIFIED_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, MODIFIED_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "

                End If
                '------------------------- Farmer Payment --------------------------------------------------------------
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleTDSPayroll) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSectionAllowanceMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],'NA' as [Status] from TSPL_SECTION_ALLOWANCE_MASTER    " & _
                          " where 2= 2  and Convert (date,MODIFIED_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, MODIFIED_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSavingsMaster) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],SAVINGS_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],'NA' as [Status] from TSPL_SAVINGS_MASTER " & _
                          " where 2= 2  and Convert (date,MODIFIED_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, MODIFIED_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103)  "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmployeeSavingsMapping) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOCUMENT_CODE as [Document Code], '' as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],'NA' as [Status] from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER    " & _
                          " where 2= 2  and Convert (date,MODIFIED_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, MODIFIED_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmIncomeTaxSlab) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_HR_TDS_INCOME_TAX_SLAB " & _
                          " where 2= 2  and Convert (date,MODIFIED_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, MODIFIED_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmHouseRentDeclaration) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],'NA' as [Status] from TSPL_HOUSE_RENT_DECLARATION    " & _
                          " where 2= 2  and Convert (date,MODIFIED_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "' ,103) and Convert (date, MODIFIED_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)   "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.IncomeTaxTDSCalculation) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,Convert (varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " & _
                          " ,Created_By as [Modified By],convert(varchar, MODIFIED_DATE,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD  " & _
                          " where 2= 2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                End If
                '--------Milk Procurement Bulk-------------------------
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleProductionDairy) = CompairStringResult.Equal Then

                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmTankerTransporterMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Vendor_Code as [Document Code], Vendor_Name as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_VENDOR_MASTER  " &
                         " where form_type='TTM'  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmTankerMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Tanker_No as [Document Code], Tanker_Name as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_TANKER_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmParameterMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_parameter_master  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ParameterValueMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Parameter_CODE as [Document Code], Value as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_Parameter_value_master  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPriceChartBulkProc) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], '' as [Description] ,Convert (varchar,Price_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_Bulk_Price_MASTER  " &
                         " where 2= 2  and Convert (date,Price_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Price_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmContractTanker) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TANKER_CODE as [Document Code], TANKER_NO as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_CONTRACT_TANKER_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSupplierMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],SUPPLIER_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SUPPLIER_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDivertedContractor) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CONTRACTOR_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_DIVERTED_CONTRACTOR_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkTypeMast) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],MILK_TYPE_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_MILK_TYPE_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkGradeMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],MILK_GRADE_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_MILK_GRADE_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBulkRoutMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ROUTE_NO as [Document Code], ROUTE_NAME as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_BULK_ROUTE_MASTER  " &
                         " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.TankerCleaningItem) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,Convert (varchar,Apply_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                                             " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_TANKER_CLEANING_ITEM_HEAD  " &
                                             " where 2= 2  and Convert (date,Apply_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Apply_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                    '--------------------------Milk Procurement Bulk Transaction--------------------
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPOBulkProc) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PO_No as [Document Code], '' as [Description] ,Convert (varchar,Date_And_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PO_BULK_MASTER  " &
                         " where 2= 2  and Convert (date,Date_And_Time,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Date_And_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmIntimation) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Intimation_No as [Document Code], '' as [Description] ,Convert (varchar,Date_And_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_Intimation_Master  " &
                         " where 2= 2  and Convert (date,Date_And_Time,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Date_And_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGateEntry) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Gate_Entry_No as [Document Code], '' as [Description] ,Convert (varchar,Date_And_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_gate_entry_details  " &
                         " where 2= 2  and Convert (date,Date_And_Time,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Date_And_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmWeighment) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Weighment_No as [Document Code], '' as [Description] ,Convert (varchar,Weighment_date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_weighment_detail  " &
                         " where 2= 2  and Convert (date,Weighment_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Weighment_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmQualityCheck) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],QC_No as [Document Code], '' as [Description] ,Convert (varchar,QC_Out_Date_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_QUALITY_CHECK  " &
                         " where 2= 2  and Convert (date,QC_Out_Date_Time,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QC_Out_Date_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmUnloading) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Unloading_No as [Document Code], '' as [Description] ,Convert (varchar,Unloading_Date_Time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_UNLOADING  " &
                         " where 2= 2  and Convert (date,Unloading_Date_Time,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Unloading_Date_Time,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCleaning) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], '' as [Description] ,convert(varchar,start_date_time,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSpl_Cleaning  " &
                         " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGateOut) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], '' as [Description] ,Convert (varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'' as [Status] from TSpl_Gate_Out  " &
                         " where 2= 2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBulkMilkSRN) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],SRN_NO as [Document Code], '' as [Description] ,Convert (varchar,SRN_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_bulk_milk_srn  " &
                         " where 2= 2  and Convert (date,SRN_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, SRN_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBulkMilkPurchaseInvoice) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DOC_NO as [Document Code], '' as [Description] ,Convert (varchar,DOC_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_BULK_MILK_PURCHASE_INVOICE_head  " &
                         " where 2= 2  and Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.SecondarySettingForQC) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as [Description] ,Convert (varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_SECONDARY_SETTING_QC_HEAD  " &
                         " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkTransferIn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Receipt_Challan_No as [Document Code], '' as [Description] ,Convert (varchar,Receipt_Challan_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_milk_transfer_in  " &
                         " where 2= 2  and Convert (date,Receipt_Challan_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Receipt_Challan_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProvisionEntry) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], '' as [Description] ,Convert (varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PROVISION_ENTRY  " &
                         " where 2= 2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBulkMilkSRNReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],SRN_Return_NO as [Document Code], '' as [Description] ,Convert (varchar,SRN_Return_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_Bulk_Milk_SRN_Return  " &
                         " where 2= 2  and Convert (date,SRN_Return_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, SRN_Return_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.MccMilkTransferPrice) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], '' as [Description] ,Convert (varchar,Price_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MCC_MILK_TRANSFER_PRICE  " &
                         " where 2= 2  and Convert (date,Price_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Price_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmMilkPurchaseReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Pur_Return_No as [Document Code], '' as [Description] ,Convert (varchar,Pur_Return_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD  " &
                         " where 2= 2  and Convert (date,Pur_Return_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Pur_Return_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmjobWorkDebitNote) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as [Description] ,Convert (varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_JOBWORK_DEBIT_NOTE_HEAD  " &
                         " where 2= 2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMilkTransferInReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Receipt_Challan_Return_No as [Document Code], '' as [Description] ,Convert (varchar,Receipt_Challan_Return_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when isPosted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_MILK_TRANSFER_IN_RETURN  " &
                         " where 2= 2  and Convert (date,Receipt_Challan_Return_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Receipt_Challan_Return_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.DariyProductionUploader) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modified_By as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Status=1 then 'Posted' else 'Pending' end   as [Status] from TSPL_PRODUCTION_UPLOADER_HEAD" &
                    " where   Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                End If

                '--------Dairy Production-------------------------
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleProductionDairy) = CompairStringResult.Equal Then

                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSectionMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],section_code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                         " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SECTION_MASTER  " &
                         " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmStageMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Stage_code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_STAGE_MASTER  " &
                        " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPPLogSheetMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_QC_LOG_SHEET_MASTER  " &
                        " where 2= 2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionLogSheet) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code], Description as [Description] ,Convert (varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PP_LOG_SHEET_HEAD  " &
                        " where 2= 2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSectionStageMapping) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code], '' as [Description] ,Convert (varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SECTION_STAGE_MAPPING_HEAD  " &
                        " where 2= 2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBillOfMaterialDairy) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],BOM_CODE as [Document Code], Description as [Description] ,Convert (varchar,BOM_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Is_Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_BOM_Head  " &
                        " where 2= 2  and Convert (date,BOM_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, BOM_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProfitCenter) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CODE as [Document Code], Name as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PROFIT_CENTER_MASTER  " &
                        " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmLineMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LINE_NO as [Document Code], '' as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_LINE_MASTER  " &
                        " where 2= 2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionPlanningDairy) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Plan_Code as [Document Code], Description as [Description] ,Convert (varchar,Plan_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Is_Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_PRODUCTION_PLAN_HEAD  " &
                        " where 2= 2  and Convert (date,Plan_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Plan_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBatchOrderDairy) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Batch_Code as [Document Code], Description as [Description] ,Convert (varchar,Batch_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Is_Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_BATCH_ORDER_HEAD  " &
                        " where 2= 2  and Convert (date,Batch_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Batch_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionIssueEntry) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Issue_Code as [Document Code], Description as [Description] ,Convert (varchar,Issue_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Is_Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_ISSUE_HEAD  " &
                        " where 2= 2  and Convert (date,Issue_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Issue_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionStandardization) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Standardization_Code as [Document Code], '' as [Description] ,Convert (varchar,Standardization_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_STANDARDIZATION_HEAD  " &
                        " where 2= 2  and Convert (date,Standardization_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Standardization_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ProcessProductionStandardizationFinalQC) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],QC_Code as [Document Code], '' as [Description] ,Convert (varchar,QC_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_STD_FINALQC_HEAD  " &
                        " where 2= 2  and Convert (date,QC_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QC_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionStageProcess) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],STAGE_PROCESS_CODE as [Document Code], '' as [Description] ,Convert (varchar,STAGE_PROCESS_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_STAGE_PROCESS_HEAD  " &
                        " where 2= 2  and Convert (date,STAGE_PROCESS_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, STAGE_PROCESS_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionEntry) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PROD_ENTRY_CODE as [Document Code], Description as [Description] ,Convert (varchar,PROD_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_PRODUCTION_ENTRY  " &
                        " where 2= 2  and Convert (date,PROD_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, PROD_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionEntryFinalQC) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],QC_Code as [Document Code], Description as [Description] ,Convert (varchar,QC_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PE_FINALQC_HEAD  " &
                        " where 2= 2  and Convert (date,QC_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QC_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmWreckageBooking) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],WRECKAGE_ENTRY_CODE as [Document Code], Description as [Description] ,Convert (varchar,PROD_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_WRECKAGE_Entry  " &
                        " where 2= 2  and Convert (date,PROD_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, PROD_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAssembDis) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CODE as [Document Code], Description as [Description] ,Convert (varchar,ASSEMBLY_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PROD_ASSEMBLIES  " &
                        " where 2= 2  and Convert (date,ASSEMBLY_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ASSEMBLY_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.JobWorkDispatchProduction) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],shipment_No as [Document Code], Description as [Description] ,Convert (varchar,shipment_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when ispost = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_scrapsale_head  " &
                        " where Doc_Type='J'  and Convert (date,shipment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, shipment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProdReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PROD_RETURN_CODE as [Document Code], Description as [Description] ,Convert (varchar,RETURN_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PP_PRODUCTION_RETURN  " &
                        " where 2= 2  and Convert (date,RETURN_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, RETURN_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSiloMilkTransfer) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], Description as [Description] ,Convert (varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_SILO_MILK_TRANSFER_HEAD  " &
                        " where IsJobWork=0  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSiloMilkTransfer_JOBWORK) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code], Description as [Description] ,Convert (varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                        " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_SILO_MILK_TRANSFER_HEAD  " &
                        " where IsJobWork=1  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                End If

                '--------Material Management-------------------------
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleMaterial) = CompairStringResult.Equal Then

                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmItemMasterRMOther) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Item_Code as [Document Code], Item_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from tspl_item_master  " &
                          " where Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.itemStructure) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Structure_Code as [Document Code], Structure_Descq as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Create_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_STRUCTURE_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.itemPurchaseAccount) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Purchase_Class_Code as [Document Code], Purchase_Class_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from tspl_purchase_accounts  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.itemSaleAccount) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Sales_Class_Code as [Document Code], Sales_Class_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from tspl_sales_accounts  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.unitMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Unit_Code as [Document Code], Unit_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_UNIT_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.chapterhead) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Chapter_Head_Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from tspl_chapter_head  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.packType) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Class_Type as [Document Code], '' as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PACKTYPE_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PriceComponentMasters) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Comp_code as [Document Code], Price_Comp_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PRICE_COMPONENT_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PriceComponentMapping) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code as [Document Code], Price_Code_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PRICE_COMPONENT_MAPPING  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PricePlan) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Plan_Code as [Document Code], '' as [Description] ,Convert (varchar,Plan_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Post_Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_ITEM_PRICE_PLAN_HEADER  " &
                          " where 2=2  and Convert (date,Plan_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Plan_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PriceMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Item_Price_ID as [Document Code], Price_Code_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_ITEM_Price_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnBreakageHead1) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Breakage_Type as [Document Code], Description as [Description] ,'' as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_BREAKAGE_HEAD  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date,Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.locationMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Location_Code as [Document Code], Location_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_LOCATION_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmStandardscheme) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],StdSchCode as [Document Code], Descraption as [Description] ,Convert (varchar,FomeDate,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_STANDARD_SCHEME  " &
                          " where 2=2  and Convert (date,FomeDate,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, FomeDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmStandardRateItem) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],StdRateCode as [Document Code], Descraption as [Description] ,Convert (varchar,FomeDate,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_STANDARD_RATE_ITEM  " &
                          " where 2=2  and Convert (date,FomeDate,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, FomeDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmItemCategoryLevel) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ITEM_CATEGORY_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ITEM_CATEGORY_LEVEL  " &
                          " where Form_Type='ITEM'  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmItemCategoryStructure) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ITEM_CATEGORY_STRUCT_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ITEM_CATEGORY_STRUCTURE  " &
                          " where Form_Type='ITEM'  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.WarrantyMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Name as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_WARRANTY_MASTER  " &
                          " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSchemeMasterNew) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Scheme_Code as [Document Code], Scheme_Desc as [Description] ,Convert (varchar,Start_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SCHEME_MASTER_NEW  " &
                          " where 2=2  and Convert (date,Start_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Start_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPriceGroupMapping) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Group_Code as [Document Code], Price_Group_Desc as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_PRICE_GROUP_MAPPING_HEAD  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.SublocationMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Sub_Location_Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_SUB_LOCATION_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLocationCategoryLevel) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ITEM_CATEGORY_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ITEM_CATEGORY_LEVEL  " &
                          " where Form_Type='LOCATION'  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLocationCategoryStructure) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ITEM_CATEGORY_STRUCT_CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ITEM_CATEGORY_STRUCTURE  " &
                          " where Form_Type='LOCATION'  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCatalogMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Catalog_Code as [Document Code], Catalog_Desc as [Description] ,Convert (varchar,Catalog_Date,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_CATALOG_MASTER  " &
                          " where 2=2  and Convert (date,Catalog_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Catalog_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPartNoMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from tspl_part_no_master  " &
                          " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.InvetorySourceCode) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Name as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_INVENTORY_SOURCE_CODE  " &
                          " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmItemTypeMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ITEM_TYPE_CODE as [Document Code], ITEM_TYPE_NAME as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],'NA' as [Status] from TSPL_ITEM_TYPE_MASTER  " &
                          " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmHSNMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CODE as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_HSN_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOverheadCostMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],COST_CODE as [Document Code], Description as [Description] ,Convert (varchar,COST_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_OVERHEAD_COST  " &
                          " where 2=2 and Convert (date,COST_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, COST_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmOverheadCostGroup) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],GROUP_CODE as [Document Code], Description as [Description] ,Convert (varchar,GROUP_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_OVERHEAD_COST_GROUP_HEAD  " &
                          " where 2=2  and Convert (date,GROUP_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, GROUP_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmItemCostMapping) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],HCODE as [Document Code], Description as [Description] ,Convert (varchar,DOC_DATE,103)  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_ITEM_COST_MAPPING_HEADS  " &
                          " where 2=2 and Convert (date,DOC_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, DOC_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmWeightUomMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], Description as [Description] ,''  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when POSTED = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_WEIGHT_UOM_MASTER  " &
                          " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '------------Material Management Transaction--------
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.Indent) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Indent_No as [Document Code], Description as [Description] ,Indent_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_INDENT_HEAD  " &
                          " where 2=2  and Convert (date,Indent_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Indent_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.Transfer) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Description as [Description] ,Document_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Status = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_TRANSFER_ORDER_HEAD  " &
                          " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnEmptyTrans) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Adjustment_No as [Document Code], Description as [Description] ,Adjustment_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                           " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 'Y' then 'Posted' else 'Pending'  end as [Status] from TSPL_ADJUSTMENT_HEADER  " &
                           " where isnull(ItemType,'')='E'  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnStoreAdjustment) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Adjustment_No as [Document Code], Description as [Description] ,Adjustment_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                           " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 'Y' then 'Posted' else 'Pending'  end as [Status] from TSPL_ADJUSTMENT_HEADER  " &
                           " where isnull(AdjustType,'') <> 'Consume'  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmWarehouseBreakage) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Description as [Description] ,Document_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Is_Post = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_WH_BREAKAGE_HEAD  " &
                          " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPhysicalStock) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Physical_No as [Document Code], Description as [Description] ,Stock_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Is_Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_PHYSICAL_STOCK  " &
                          " where 2=2  and Convert (date,Stock_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Stock_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ChangeItemSerialNumber) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code], '' as [Description] ,Document_Date  as [Document Date],'' as [Created By],'' as [Created Date] " &
                          " ,'' as [Modified By],'' as [Modified Date],'NA' as [Status] from TSPL_SERIAL_ITEM  " &
                          " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.TransferReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], Remarks as [Description] ,Document_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,'' as [Modified By],'' as [Modified Date],'NA' as [Status] from TSPL_TRANSFER_RETURN  " &
                          " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmTransferGateOut) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code], '' as [Description] ,Gate_Out_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],'NA' as [Status] from TSPL_Transfer_Gate_Out  " &
                          " where 2=2  and Convert (date,Gate_Out_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Gate_Out_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGateEntryReturnTransfer) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],GE_CODE as [Document Code], Remarks as [Description] ,GE_DATE  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_GATE_ENTRY_RETURN_TRANSFER  " &
                          " where 2=2  and Convert (date,GE_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, GE_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmJobWorkInventory) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Adjustment_No as [Document Code], Description as [Description] ,Adjustment_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 'Y' then 'Posted' else 'Pending'  end as [Status] from TSPL_ADJUSTMENT_HEADER  " &
                          " where isnull(AdjustType,'') = 'Consume'  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmRawMilkConsumtion) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Adjustment_No as [Document Code], Description as [Description] ,Adjustment_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modify_By as [Modified By],convert(varchar, Modify_Date,103) as [Modified Date],case when Posted = 'Y' then 'Posted' else 'Pending'  end as [Status] from TSPL_ADJUSTMENT_HEADER  " &
                          " where isnull(AdjustType,'') = 'Consume'  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGeneralWeighment) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Weighment_No as [Document Code], Remarks as [Description] ,Weighment_Date  as [Document Date],Created_By as [Created By],Convert(varchar, Created_Date,103) as [Created Date] " &
                          " ,Modified_By as [Modified By],convert(varchar, Modified_Date,103) as [Modified Date],case when Posted = 1 then 'Posted' else 'Pending'  end as [Status] from TSPL_GENERAL_WEIGHMENT_DETAIL  " &
                          " where 2=2  and Convert (date,Weighment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Weighment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                End If
                '======================Added by preeti Gupta against ticket no[TEC/17/05/19-000489]=============================
                '------------Purchase Master--------
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModulePurchase) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CostCenter) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], cost_code as [Document Code],cost_Name AS Description" &
                ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_CostCenter_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.RequisitSubTypeMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.RequisitSubTypeMaster, "Requisite SubType Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code as [Document Code],Description AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],convert(varchar,Modified_By,103) as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_Requisit_SubType_Master" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmFormIssueReceiptEntry) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmFormIssueReceiptEntry, "Form Issue/Receive List Entry"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Doc_no as [Document Code],'' AS Description" &
                ",doc_date as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_FORM_SERIAL_NO_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCostCentreGroupStores) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmCostCentreGroupStores, "Cost Centre Group Stores"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], CostCenter_code as [Document Code],Description AS Description" &
            " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_COST_CENTRE_GROUP_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeliveryTermsMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmDeliveryTermsMaster, "Delivery Terms Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], stage_code as [Document Code],Description AS Description" &
                " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_STAGE_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.capexmaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.capexmaster, "Capex Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code as [Document Code],Description AS Description" &
                " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_CAPEX_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.capexbudget) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.capexbudget, "Sub Capex Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code as [Document Code],Description AS Description" &
                ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_CAPEX_BUDGET_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmUnitMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmUnitMaster, "Unit Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code as [Document Code],Description AS Description" &
                " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_COST_CENTER_UNIT_MASTER" &
                 " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCostCenterTypeMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmCostCenterTypeMaster, "Cost Center Type Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code as [Document Code],Description AS Description" &
                " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_COST_CENTER_TYPE_MASTER" &
                 " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '*********************************************Transaction Of Purchae*******************************
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnPurchaseRequistion, "Purchase Indent"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],REQUISITION_ID  as [Document Code] ,convert(varchar,Requisition_Date,103)  as [Document Date],TSPL_REQUISITION_HEAD.Description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date], case when Status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_REQUISITION_HEAD " &
                     " where 2=2 and  Is_Internal='N' and Convert (date,Requisition_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Requisition_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_REQUISITION_HEAD.Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.RFQ) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.RFQ, "RFQ"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], RFQ_No  as [Document Code] ,convert(varchar,RFQ_Date,103)  as [Document Date],'' as Description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when Is_Post=1 then 'Posted' else 'pendng' end  as [Status] from TSPL_RFQ_HEAD " &
                     " where 2=2  and Convert (date,RFQ_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, RFQ_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.VendorQuotation) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.VendorQuotation, "Vendor Quotation"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Code  as [Document Code] ,convert(varchar,VQDate,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end  as [Status] from TSPL_VENDOR_QUOTATION_HEAD " &
                     " where 2=2  and Convert (date,VQDate,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, VQDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnPurchaseOrder, "Purchase Order"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Purchaseorder_no  as [Document Code] ,convert(varchar,purchaseorder_date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status],close_yn as [Close] from tspl_purchase_order_head " &
                     " where 2=2  and Convert (date,purchaseorder_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, purchaseorder_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_PURCHASE_ORDER_HEAD.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnGRN) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnGRN, "Gate Received Note"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], GRN_No  as [Document Code] ,convert(varchar,GRN_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from tspl_grn_head " &
                     " where 2=2  and Convert (date,GRN_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, GRN_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and tspl_grn_head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.POWeighment) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.POWeighment, "PO Weighment"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Weighment_Code  as [Document Code] ,convert(varchar,Weighment_date,103)  as [Document Date],'' as description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modified_By as [Modified By],Modified_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_PO_WEIGHTMENT_HEAD " &
                     " where 2=2  and Convert (date,Weighment_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Weighment_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnMRN) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnMRN, "Material Received Note"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], MRN_No  as [Document Code] ,convert(varchar,MRN_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from tspl_MRN_head " &
                     " where 2=2  and Convert (date,MRN_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, MRN_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and tspl_MRN_head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnSRN, "Store Received Note"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],SRN_No  as [Document Code] ,convert(varchar,SRN_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from tspl_SRN_Head " &
                     " where 2=2  and Convert (date,SRN_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, SRN_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and tspl_SRN_Head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnPurchaseInvoice) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnPurchaseInvoice, "Purchase Invoice"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PI_No  as [Document Code] ,convert(varchar,PI_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from tspl_PI_head " &
                     " where 2=2  and Convert (date,PI_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, PI_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and tspl_PI_head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnPurchaseReturn) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnPurchaseReturn, "Purchase Return"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], PR_No  as [Document Code] ,convert(varchar,PR_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from tspl_PR_head " &
                     " where 2=2  and Convert (date,PR_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, PR_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and tspl_PR_head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    '--clsUserMgtCode.mbtnNRGP, "NRGP Request"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnNRGP) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Booking_no  as [Document Code] ,convert(varchar,Booking_Date ,103) as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modified_By as [Modified By],Modified_Date as [Modified Date],case when Posted =1 then 'Posted' else 'pendng' end as [Status] from TSPL_NRGP_REQUEST_HEAD " &
                     " where 2=2  and Convert (date,Booking_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Booking_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_NRGP_REQUEST_HEAD.location_code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnGatePass) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnGatePass, "RGP/NRGP"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], RGP_No  as [Document Code] ,convert(varchar,RGP_Date,103)  as [Document Date],remarks as description ,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from tspl_RGP_Head " &
                     " where 2=2  and Convert (date,RGP_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, RGP_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and tspl_RGP_Head.location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnIssueReturn) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnIssueReturn, "Issue/Return/Transfer"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Doc_No  as [Document Code] ,convert(varchar,Doc_Date,103)  as [Document Date],remarks as description ,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_IssueReturn_HEAD " &
                     " where 2=2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_IssueReturn_HEAD.From_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    '--clsUserMgtCode.frmMaterialQuotation, "Material Quotation"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMaterialQuotation) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Code  as [Document Code] ,convert(varchar,QDate,103)  as [Document Date],remarks as description ,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_SCRAP_QUOTATION_HEAD " &
                     " where 2=2  and Convert (date,QDate,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAP_QUOTATION_HEAD.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMaterialQuotationOrder) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmMaterialQuotationOrder, "Material Quotation Order"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Code  as [Document Code] ,convert(varchar,QODate,103)  as [Document Date],remarks as description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_SCRAP_QUOTATION_ORDER_HEAD " &
                     " where 2=2  and Convert (date,QODate,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QODate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAP_QUOTATION_ORDER_HEAD.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMaterialQuotationComparison) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmMaterialQuotationComparison, "Material Quotation Comparison"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Code  as [Document Code] ,convert(varchar,QCDate,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD " &
                     " where 2=2  and Convert (date,QCDate,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QCDate,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ScrapSale) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.ScrapSale, "Material Sales"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Shipment_no  as [Document Code] ,convert(varchar,Shipment_Date,103)  as [Document Date],'' as description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end as [Status] from TSPL_SCRAPSALE_HEAD " &
                     " where 2=2 and  Doc_Type='S' and Convert (date,Shipment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Shipment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAPSALE_HEAD.Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmItemConversion) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmItemConversion, "Item Conversion"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Doc_code  as [Document Code] ,''  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    " ,Modified_By as [Modified By],Modified_Date as [Modified Date],case when isposted=1 then 'Posted' else 'pendng' end as [Status] from TSPL_Item_Conversion_Head " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmStoreRequistion) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmStoreRequistion, "Store Requisition"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], REQUISITION_ID  as [Document Code] ,convert(varchar,Requisition_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status=1 then 'Posted' else 'pendng' end  as [Status],close_yn as [Close] from TSPL_REQUISITION_HEAD " &
                     " where 2=2 and Is_Internal='Y' and Convert (date,Requisition_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Requisition_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_REQUISITION_HEAD.Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPurchaseSchedule) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmPurchaseSchedule, "Purchase Schedule"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_Code  as [Document Code] ,convert(varchar,Document_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modified_By as [Modified By],Modified_Date as [Modified Date],case when is_post=1 then 'Posted' else 'pendng' end  as [Status] from TSPL_PO_SCH_HEAD " &
                     " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmScrapSaleGateOut) = CompairStringResult.Equal Then

                    '--clsUserMgtCode.FrmScrapSaleGateOut, "Misc. Sale Gate Out"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No  as [Document Code] ,convert(varchar,Gate_Out_Date,103)  as [Document Date],'' as description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],'' as [Status] from TSPL_SCRAPSALE_GATE_OUT" &
                     " where 2=2  and Convert (date,Gate_Out_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Gate_Out_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAPSALE_GATE_OUT.From_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.JobWorkDispatch) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.JobWorkDispatch, "Job Work Dispatch"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Shipment_no  as [Document Code] ,convert(varchar,Shipment_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status =1 then 'Posted' else 'pendng' end as [Status] from TSPL_SCRAPSALE_HEAD " &
                     " where 2=2 and Doc_Type='J' and Convert (date,Shipment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Shipment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAPSALE_HEAD.Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ScrapSaleRetrun) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.ScrapSaleRetrun, "Material Sales Return"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_no  as [Document Code] ,convert(varchar,Return_ship_Date,103)  as [Document Date],description,Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when status =1 then 'Posted' else 'pendng' end as [Status] from TSPL_SCRAPSALE_HEAD_Return " &
                     " where 2=2  and Convert (date,Return_ship_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Return_ship_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_SCRAPSALE_HEAD_Return.Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    '--clsUserMgtCode.frmsaleReturnGateEntryMISSAle, "Sale Return Gate Entry Misc Sale"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmsaleReturnGateEntryMISSAle) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Gate_Entry_No  as [Document Code] ,convert(varchar,Gate_Entry_Date,103)  as [Document Date],comment as description, Created_By as [Created By],Created_Date as [Created Date]" &
                    ",Modify_By as [Modified By],Modify_Date as [Modified Date],case when Posted=1 then 'Posted' else 'pendng' end as [Status] from TSPL_Sale_Return_Gate_Entry_Head " &
                     " where 2=2  and Convert (date,Gate_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Gate_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_Sale_Return_Gate_Entry_Head.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    '**************************************************************************************************


                End If
                '============================Added by preeti Gupta Against ticket no[TEC/17/05/19-000486][Payable Module]============
                '==============================Master===============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModulePayable) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.vendoraccountset) = CompairStringResult.Equal Then
                    'clsUserMgtCode.vendoraccountset, "Vendor Account Set"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Acct_Set_Code as [Document Code],Acct_set_desc AS Description" &
                            " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_VENDOR_ACCOUNT_SET" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.vendorgroup) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.vendorgroup, "Vendor Group"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Ven_Group_Code as [Document Code],Group_Desc AS Description" &
                         " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_VENDOR_GROUP" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.vendormaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.vendormaster, "Vendor Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], vendor_Code as [Document Code],Vendor_Name AS Description" &
                            " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_Vendor_Master" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.paymentTerms) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.paymentTerms, "Payment Terms"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], terms_code as [Document Code],terms_desc AS Description" &
                            " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_terms_master" &
                    " where 2=2  and Convert (date,Created_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Created_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.VendorBankMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.VendorBankMaster, "Vendor Bank Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Bank_Code as [Document Code],Bank_Name AS Description" &
                        " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from tspl_vendor_bank_master" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.transportMasterVendor) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.transportMasterVendor, "Transport Master"

                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Transport_ID as [Document Code],Transporter_Name AS Description" &
                        " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_TRANSPORT_MASTER" &
                    " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmHirerachyLevelMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmHirerachyLevelMaster, "Hierarchy Level Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Hirerachy_Code as [Document Code],description AS Description" &
                            " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_HIRERACHY_LEVEL_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.vendorSubgroup) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.vendorSubgroup, "Vendor Sub Group"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Ven_Sub_Group_Code  as [Document Code],SubGroup_Desc AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from tspl_Vendor_Sub_Group" &
                    " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '=======================Transaction====================
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PaymentEntryNew) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.PaymentEntryNew, "Payment Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Payment_No as [Document Code] ,convert(date,Payment_Date,103)  as [Document Date],reference as Description,Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                            " ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when Posted =1 then 'Posted' else 'pendng' end as [Status] from TSPL_PAYMENT_HEADER " &
                              " where 2=2  and Convert (date,Payment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Payment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnAPInvoiceEntry) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnAPInvoiceEntry, "AP Invoice Entry"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No as [Document Code],Description ,convert(date,Invoice_Entry_Date,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                            ",Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when isnull(Posting_Date,'')='' then 'Pending' else 'Posted' end  as [Status],Invoice_Type from tspl_vendor_invoice_head " &
                              " where 2=2  and Convert (date,Invoice_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Invoice_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PaymentAdjustmentEntry) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.PaymentAdjustmentEntry, "Payment Adjustment Entry"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], TSPL_Payment_Adjustment_Header.Adjustment_no as [Document Code],Description ,convert(date,TSPL_Payment_Adjustment_Header.adjustment_date,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when Is_Post='Y ' then 'Posted' else 'pendng' end as [Status] from TSPL_Payment_Adjustment_Header " &
                          " where 2=2  and Convert (date,adjustment_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, adjustment_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSupplierReg) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmSupplierReg, "Supplier Registration"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Registration_No  as [Document Code],comments as Description ,convert(date,Registration_Date,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when Posted =1 then 'Posted' else 'Pending' end as [Status] from TSPL_SUPPLIER_REGISTRATION " &
                          " where 2=2  and Convert (date,Registration_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Registration_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.VendorRegistration) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.VendorRegistration, "Vendor Registration"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Registration_No  as [Document Code] ,Name as Description,''   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],''  as [Status] from TSPL_VENDORREGISTRATION_MASTER " &
                          " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                End If
                '============================Added by preeti Gupta Against ticket no[TEC/17/05/19-000484][Common Services]============
                '==============================Master===============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleCommonServices) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.taxAuthority) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.taxAuthority, "Vendor Account Set"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Acct_Set_Code as [Document Code],Acct_set_desc AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_VENDOR_ACCOUNT_SET" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.taxRate) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.taxRate, "Tax Rate"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Tax_Code  as [Document Code],Tax_Rate_Desc  AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_TAX_RATES" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.taxGroup) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.taxGroup, "Tax Group"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Tax_Group_Code   as [Document Code],Tax_Group_Desc   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_TAX_GROUP_MASTER" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.paymentCodes) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.paymentCodes, "Payment Code"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Payment_code   as [Document Code],Payment_Desc   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_payment_code" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.cityMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.cityMaster, "City Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],city_Code   as [Document Code],City_Name   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_city_master" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmAbateMentMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmAbateMentMaster, "Abatment Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Abatement_Code    as [Document Code],abatement_Desc   AS Description" &
                ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_ABATEMENT_MASTER" &
                " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmAbateMentMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmAbateMentMaster, "Abatment Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Abatement_Code    as [Document Code],abatement_Desc   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_ABATEMENT_MASTER" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCompanyMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmCompanyMaster, "Company Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],comp_Code  as [Document Code],Comp_Name   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_company_master" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmAdditionalCharges) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmAdditionalCharges, "Additional Charges"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code  as [Document Code],Description   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_ADDITIONAL_CHARGES" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FormMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FormMaster, "Form Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],form_code  as [Document Code],Form_Name   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_Form_Master" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.bankMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.bankMaster, "Bank Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Bank_Code  as [Document Code],Description   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from tspl_Bank_Master" &
                    " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCurrencyMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmCurrencyMaster, "Currency Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Currency_code  as [Document Code],Currency_Name   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(date,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(date,Modified_Date,103) as [Modified Date],'' as [Status] from tspl_Currency_master" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCurrencyConversion) = CompairStringResult.Equal Then

                    '--clsUserMgtCode.frmCurrencyConversion, "Currency Conversion Rate"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code  as [Document Code],description   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from tspl_Currency_conversion_Rate" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomFieldMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.CustomFieldMaster, "Custom Field Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code  as [Document Code],Name   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_CUSTOM_FIELD_HEAD" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomFieldMapping) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.CustomFieldMapping, "Custom Field Mapping"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code  as [Document Code],Name   AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_CUSTOM_FIELD_HEAD" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmRegionMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.frmRegionMaster, "Region Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Region_code  as [Document Code],Region_Name   AS Description" &
                 " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_REGION_MASTER" &
" where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '--clsUserMgtCode.frmCountryMaster1, "Country Master"
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomFieldMapping) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Comp_Code  as [Document Code],Comp_Name   AS Description" &
                 " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],modify_by as [Modified By],convert(varchar,modify_date,103) as [Modified Date],'' as [Status] from tspl_company_master" &
" where 2=2  and Convert (date,modify_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, modify_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.frmStateMaster1, "State Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmStateMaster1) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],State_code  as [Document Code],State_Name   AS Description" &
                  " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from tspl_state_master" &
 " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.DistrictMaster, "District Master"
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomFieldMapping) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code  as [Document Code],Name   AS Description" &
                  " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from tspl_district_master" &
 " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.bankBranchMaster, "Bank  Branch Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.bankBranchMaster) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],BRANCH_CODE   as [Document Code],BRANCH_NAME    AS Description" &
                  " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from tspl_bank_branch_master" &
 " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.FrmFormSerialNoMaster, "Form Nos. Entry"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmFormSerialNoMaster) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No    as [Document Code],''    AS Description" &
                 " ,convert(varchar,Doc_Date,103)  as [Document Date],Created_By as [Created By],convert(date,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(date,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_FORM_SERIAL_NO_MASTER" &
" where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "

                    '--clsUserMgtCode.FrmMCCDiscountMaster, "Discount Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmMCCDiscountMaster) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code    as [Document Code],description   AS Description" &
                 " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_Discount_Master" &
" where 2=2  and Convert (date,Created_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Created_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "



                    '--clsUserMgtCode.AreaMaster, "Area Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.AreaMaster) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code    as [Document Code],Name    AS Description" &
                 " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_AREA_MASTER" &
" where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.FrmSAC, "SAC Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSAC) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code    as [Document Code],Description    AS Description" &
                 " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_SAC_MASTER" &
" where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.FrmItemWiseTax, "Item Wise Tax Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmItemWiseTax) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],HCode    as [Document Code],Description    AS Description" &
                  ",convert(varchar,doc_date,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when status =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_ITEM_WISE_TAX" &
 " where 2=2  and Convert (date,Created_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Created_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.FrmRackBinMaster, "Rack/Bin Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmRackBinMaster) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],code    as [Document Code],Description    AS Description" &
                 " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],''  as [Status] from tspl_Rack_master" &
" where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--clsUserMgtCode.frmDeptHeadCustomerMapping, "Dept Head Customer Mapping"
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeptHeadCustomerMapping) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],User_Code    as [Document Code],''    AS Description" &
                 " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],''  as [Status] from TSPL_CUSTOMER_GROUP_MAPPING" &
 " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and  Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "


                    '--clsUserMgtCode.paymentTerms, "Payment Terms"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.paymentTerms) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],terms_code    as [Document Code],Terms_Desc   AS Description" &
                 " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],''  as [Status] from tspl_terms_master" &
 " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '====================================Transaction Of Common Services================
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.reverseTransaction) = CompairStringResult.Equal Then
                    '--===========clsUserMgtCode.reverseTransaction, "Bank Reverse Entry"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Reverse_code  as [Document Code] ,Reason as Description,convert(varchar,Reversal_Date,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when post='P' then 'Posted' else 'Pending' end  as [Status] from TSPL_BANK_REVERSE " &
                        " where 2=2  and Convert (date,Reversal_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Reversal_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.bankTransfer) = CompairStringResult.Equal Then
                    ' --===========clsUserMgtCode.bankTransfer, "Bank Transfer"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Transfer_No   as [Document Code] ,Reference  as Description,convert(varchar,Transfer_Date ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when post='P' then 'Posted' else 'Pending' end  as [Status] from TSPL_BANK_TRANSFER " &
                        " where 2=2  and Convert (date,Transfer_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Transfer_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBankReco) = CompairStringResult.Equal Then
                    '--===========clsUserMgtCode.FrmBankReco, "Bank Reconciliation"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], reconciliation_id   as [Document Code] ,Description  as Description,convert(varchar,Reconciliation_date ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when post='P' then 'Posted' else 'Pending' end  as [Status] from tspl_BankReco_Head " &
                           " where 2=2  and Convert (date,Reconciliation_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Reconciliation_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCFormEntry) = CompairStringResult.Equal Then
                    '--===========clsUserMgtCode.FrmCFormEntry, "CForm Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], document_No   as [Document Code] ,Description  as Description,convert(varchar,Document_Date ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when posted='P' then 'Posted' else 'Pending' end  as [Status] from TSPL_CForm_HEADER " &
                           " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBankGuaranteeMaster1) = CompairStringResult.Equal Then
                    '--==========FrmBankGuaranteeMaster1, "Bank Guarantee Master"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], DocNo   as [Document Code] ,Description  as Description,convert(varchar,Date ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when status='Y' then 'Posted' else 'Pending' end  as [Status] from tspl_bank_guarantee_master " &
                           " where 2=2  and Convert (date,Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.BankOpeningReco) = CompairStringResult.Equal Then
                    '--==========BankOpeningReco, "Bank Opening Reco"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], code   as [Document Code] ,Description  as Description,convert(varchar,Reco_Date ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when status=1 then 'Posted' else 'Pending' end  as [Status] from tspl_bank_opening_reco " &
                           " where 2=2  and Convert (date,Reco_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Reco_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.RevaluationEntry) = CompairStringResult.Equal Then
                    '--=========RevaluationEntry, "Revaluation Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_No    as [Document Code] ,Description  as Description,convert(varchar,Document_Date  ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when status=1 then 'Posted' else 'Pending' end  as [Status] from TSPL_REVALUATION_HEAD " &
                           " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.LoanEntry) = CompairStringResult.Equal Then
                    '--=========LoanEntry, "Loan Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Loan_Code    as [Document Code] ,Remarks  as Description,convert(varchar,Loan_Date  ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when status=1 then 'Posted' else 'Pending' end  as [Status] from TSPL_LOAN_ENTRY " &
                           " where 2=2  and Convert (date,Loan_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Loan_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.LoanEntry) = CompairStringResult.Equal Then
                    '--========clsUserMgtCode.LoanInstallment, "Loan Installment Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], installment_code    as [Document Code] ,Remarks  as Description,convert(varchar,installment_Date  ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when status=1 then 'Posted' else 'Pending' end  as [Status] from TSPL_LOAN_INSTALLMENT_ENTRY  " &
                           " where 2=2  and Convert (date,installment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Loan_Date,103)  < = convert ( installment_Date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                End If

                '============================Added by preeti Gupta Against ticket no[TEC/17/05/19-000485][Receivable]============
                '==============================Master===============================================================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleReceivable) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomerType) = CompairStringResult.Equal Then

                    '--clsUserMgtCode.CustomerType, "Customer Type"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Cust_Type_Code  as [Document Code],Cust_Type_Desc  AS Description" &
                   " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_customer_type_master" &
                    " where 2=2  and Convert (varchar,ModifY_Date,103)  > = Convert (varchar, '" + txtFromDate.Value + "'  ,103) and Convert (varchar, ModifY_Date,103)  < = convert ( varchar, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomerAccountSet) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.CustomerAccountSet, "Customer Account Set"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Cust_Account   as [Document Code],Cust_Acct_Desc   AS Description" &
                     ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_CUSTOMER_ACCOUNT_SET" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomerGroup) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.CustomerGroup, "Customer Group"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],cust_group_code   as [Document Code],cust_group_desc   AS Description" &
                     ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_customer_group_master" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomeCategory) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.CustomeCategory, "Customer Category"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CUST_CATEGORY_CODE    as [Document Code],CUST_CATEGORY_DESC    AS Description" &
                     " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_CUSTOMER_CATEGORY_MASTER" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CustomerMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.CustomerMaster, "Customer Master"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],cust_code    as [Document Code],customer_Name    AS Description" &
                     " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from tspl_customer_master" &
                    " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.SecondaryCustomerMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.SecondaryCustomerMaster, "Secondary Customer Master"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],cust_code    as [Document Code],customer_Name    AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_SECONDARY_CUSTOMER_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmPOSGRoupMaster) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.FrmPOSGRoupMaster, "POS Group Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],group_code    as [Document Code],Description    AS Description" &
                    " ,convert(varchar,doc_Date,103) as [Document Date],Create_By as [Created By],convert(varchar,Create_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_POS_GROUP_MASTER" &
                    " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                    '--==================================Receivable Transaction============
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ReceiptEntry) = CompairStringResult.Equal Then
                    '--=================clsUserMgtCode.ReceiptEntry, "Receipt Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Receipt_No    as [Document Code],QuickEntryNo as [Quick Entry] ,Entry_Desc   as Description,convert(varchar,Receipt_Date   ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when Posted ='Y' then 'Posted' else 'Pending' end  as [Status] from TSPL_RECEIPT_HEADER " &
                      " where 2=2  and Convert (date,Receipt_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Receipt_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ReceiptAdjustmentEntry) = CompairStringResult.Equal Then
                    '--=================clsUserMgtCode.ReceiptAdjustmentEntry, "Receipt Adjustment Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], adjustment_no    as [Document Code] ,Description    as Description,convert(varchar,Adjustment_Date    ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when Is_Post  ='Y' then 'Posted' else 'Pending' end  as [Status] from TSPL_Receipt_Adjustment_Header " &
                      " where 2=2  and Convert (date,Adjustment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Adjustment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnARInvoiceEntry) = CompairStringResult.Equal Then
                    '--=================clsUserMgtCode.mbtnARInvoiceEntry, "AR Invoice Entry
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Document_no    as [Document Code] ,Description    as Description,convert(varchar,Document_date    ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when status  =1 then 'Posted' else 'Pending' end  as [Status] from tspl_customer_invoice_head " &
                      " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmReceiptInvoiceMapping) = CompairStringResult.Equal Then
                    '--=================clsUserMgtCode.FrmReceiptInvoiceMapping, "Receipt Invoice Mapping"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], Doc_Code    as [Document Code] ,Description   as Description,convert(varchar,Document_Date    ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when isPosted  =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_Receipt_InvoiceMapping_Head " &
                      " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCustomersOutstanding) = CompairStringResult.Equal Then
                    '--=================clsUserMgtCode.FrmCustomersOutstanding, "Customer Outstanding"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], customer_outsanding_no    as [Document Code] ,''   as Description,convert(varchar,Document_Date    ,103)   as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],case when posted  =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_CUSTOMER_OUTSTANDING_HEADER " &
                      " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "


                End If
                '============================Added by preeti Gupta GL Master[TEC/17/05/19-000487]===============
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleGL) = CompairStringResult.Equal Then

                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.accountStructure) = CompairStringResult.Equal Then

                    '--clsUserMgtCode.accountStructure, "Account Structure Master"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],STR_CODE  as [Document Code],STR_DESCRIPTION AS Description" &
                     ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,ModifY_Date,103) as [Modified Date],'' as [Status] from TSPL_GL_STRUCTURE" &
                     " where 2=2  and Convert (date,ModifY_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ModifY_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.AccountMainGroup) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.AccountMainGroup, "Account Main Group"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Account_Main_Group_Code   as [Document Code],Account_Main_Group_Desc  AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_ACCOUNT_MAIN_GROUPS" &
                 " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.accountGroup) = CompairStringResult.Equal Then
                    ''--clsUserMgtCode.accountGroup, "Account Group"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Account_Group_Code   as [Document Code],Account_Group_Desc   AS Description" &
                     ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_ACCOUNT_GROUPS" &
                     " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.AccountSubGroup) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.AccountSubGroup, "Account Sub Group"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Account_Sub_Group_Code    as [Document Code],Account_Sub_Group_Desc    AS Description" &
                    ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_ACCOUNT_SUB_GROUPS" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.AccountGLMainAccount) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.AccountGLMainAccount, "GL Main Account"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Main_GL_Account    as [Document Code],Main_GL_Account_Desc     AS Description" &
                        ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_ACCOUNT_MAIN_GL_ACCOUNT" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.glAccount) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.glAccount, "General Ledger Account"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Account_Code as [Document Code],Description      AS Description" &
                     ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_GL_ACCOUNTS" &
                     " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.sourceCode) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.sourceCode, "SOURCE CODE"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],sourcecode     as [Document Code],sourcedescription      AS Description" &
                            ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_GL_SOURCECODE" &
                         " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '--clsUserMgtCode.glsecurity, "GL Security"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.glsecurity) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],user_code    as [Document Code],GL_Segment      AS GL_Segment" &
                     " ,'' as [Document Date],'' as  Description,Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],ModifY_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],'' as [Status] from TSPL_GL_SEGMENT_PERMISSION" &
                     " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '--clsUserMgtCode.FiscalYear, "Financial Year Master"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FiscalYear) = CompairStringResult.Equal Then
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],fiscal_code    as [Document Code],Fiscal_Name      AS Description" &
                         ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_Fiscal_Year_Master" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '--clsUserMgtCode.CostCentreFinancial, "Cost Centre Master Financial"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CostCentreFinancial) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type], cost_center_fin_code     as [Document Code],cost_center_fin_Name      AS Description" &
                         ",'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By as [Modified By],convert(varchar,Modified_Date,103) as [Modified Date],'' as [Status] from TSPL_COST_CENTRE_FINANCIAL" &
                        " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '====================Transaction of GL===================================
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.journalEntry) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.journalEntry, "Journal Entry"
                    qry = "select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Voucher_No as [Document Code],voucher_desc   as Description ,convert(varchar,voucher_date,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                          " ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when Authorized  ='A' then 'Posted' else 'Pending' end as [Status],Auto_Reverse from tspl_journal_master " &
                      " where 2=2  and Convert (date,voucher_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, voucher_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnVCGLEntry) = CompairStringResult.Equal Then
                    '--clsUserMgtCode.mbtnVCGLEntry, "Vendor/Customer/GL Entry"
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code],Remarks   as Description ,convert(varchar,Document_Date ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                          " ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date],case when status  =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_VCGL_Head " &
                      " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                    '============================Added by preeti Gupta GL Master[TEC/17/05/19-000487]===============


                End If
                '============================Added by preeti Gupta Bulk Sale Master[TEC/17/05/19-000495]===============
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleBulkSale) = CompairStringResult.Equal Then

                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBulkSalePriceChart) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Price_Code   as [Document Code],'' AS Description " &
                   "  ,convert(varchar,TSPL_BulkSalePrice_MASTER.Price_Date,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when posted   =1 then 'Posted' else 'Pending' end as [Status] from TSPL_BulkSalePrice_MASTER " &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + " ',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.TankerMasterSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_TANKER_MASTER_SALE.Tanker_Code    as [Document Code],'' AS Description " &
                    " ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from TSPL_TANKER_MASTER_SALE " &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '=======================Transaction========================================
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmSalesOrderBS) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_SALES_ORDER_MASTER_BulkSale.Document_No as [Document Code],''   as Description ,convert(varchar,Document_Date ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                          " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SALES_ORDER_MASTER_BulkSale " &
                     " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmGateEntrySale) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_GATEENTRY_SALE.Document_No as [Document Code],''   as Description ,convert(varchar,Document_Date ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                          " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_GATEENTRY_SALE" &
                    " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmWeighmentEntry) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No  as [Document Code],''   as Description ,convert(varchar,Weighment_Date  ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                           ",Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_WEIGHMENT_DETAIL_BULKSALE" &
                    " where 2=2  and Convert (date,Weighment_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Weighment_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmQualityCheckBulkSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_QUALITY_CHECK_BULKSALE.QC_No   as [Document Code],''   as Description ,convert(varchar,QC_Date   ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    "  ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_QUALITY_CHECK_BULKSALE" &
                 " where 2=2  and Convert (date,QC_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, QC_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmLoadingTanker) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No    as [Document Code],''   as Description ,convert(varchar,LoadingTanker_Date    ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_LOADING_TANKER_DETAIL_BULKSALE" &
                  " where 2=2  and Convert (date,LoadingTanker_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, LoadingTanker_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmDispatchBulkSale) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_Dispatch_BulkSale.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                      ",Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_Dispatch_BulkSale" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmInvoiceBulkSale) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_INVOICE_MASTER_BULKSAlE.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                      ",Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_INVOICE_MASTER_BULKSAlE" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmDispatchBulkSaleTrade) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_Dispatch_BulkSale_Trade.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_Dispatch_BulkSale_Trade" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCreateAutoInvoiceBS) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_INVOICE_MASTER_BULKSAlE.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    "  ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_INVOICE_MASTER_BULKSAlE" &
                "  where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBulkDispatchReturnSale) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_Dispatch_BulkSale_History.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_Dispatch_BulkSale_History" &
                "  where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmBulkSaleReturn) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SALE_RETURN_MASTER_BULKSALE" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCanSaleUploader) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_CAN_SALE_HEAD.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    "  ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_CAN_SALE_HEAD" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCanSale) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_CAN_SALE_HEAD.Document_No     as [Document Code],''   as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    "  ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_CAN_SALE_HEAD" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmCanReceived) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No     as [Document Code],Comments    as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     " ,Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE" &
                 " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Trans_Type='Can'"
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                    'ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmDispatchBulkSaleTradeReturn) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_Dispatch_BulkSale_Trade_Return.Document_No     as [Document Code],''    as Description ,convert(varchar,Document_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_Dispatch_BulkSale_Trade_Return" &
                        " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "


                End If
                '=======================Added by Preeti HR======================
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleHR) = CompairStringResult.Equal Then

                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCountryMaster) = CompairStringResult.Equal Then

                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_COUNTRY_MASTER.COUNTRY_CODE   as [Document Code],TSPL_COUNTRY_MASTER.COUNTRY_NAME  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_COUNTRY_MASTER " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmStateMaster) = CompairStringResult.Equal Then
                    qry = "select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_STATE_MASTER.STATE_CODE    as [Document Code],TSPL_STATE_MASTER.STATE_NAME   AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_STATE_MASTER " &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBranchMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_BRANCH_MASTER.BRANCH_CODE     as [Document Code],TSPL_BRANCH_MASTER.BRANCH_CODE    AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_BRANCH_MASTER " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOTMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_OT_MASTER.OT_CODE      as [Document Code],TSPL_OT_MASTER.OT_NAME    AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_OT_MASTER " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAttendanceMaster) = CompairStringResult.Equal Then
                    qry = "select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_ATTENDANCE_MASTER.ATTENDANCE_CODE       as [Document Code],TSPL_ATTENDANCE_MASTER.ATTENDANCE_NAME     AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_ATTENDANCE_MASTER " &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "


                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBonusMaster) = CompairStringResult.Equal Then
                    qry = "select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_BONUS_MASTER.BONUS_CODE        as [Document Code],TSPL_BONUS_MASTER.BONUS_NAME      AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_BONUS_MASTER " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPayPeriodMaster) = CompairStringResult.Equal Then
                    qry = "select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE         as [Document Code],TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME       AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_PAYPERIOD_MASTER " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPFRulesMaster) = CompairStringResult.Equal Then
                    qry = "select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_PF_RULE_MASTER.PFRULE_CODE          as [Document Code],''       AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_PF_RULE_MASTER" &
                  " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmESIRulesMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_ESI_RULE_MASTER.ESIRULE_CODE           as [Document Code],''       AS Description" &
                   " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_ESI_RULE_MASTER " &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLeaveMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_LEAVE_MASTER.LEAVE_CODE            as [Document Code],TSPL_LEAVE_MASTER.LEAVE_NAME      AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_LEAVE_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmShiftMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_SHIFT_MASTER.SHIFT_CODE             as [Document Code],TSPL_SHIFT_MASTER.SHIFT_NAME       AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_SHIFT_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDepartmentMaster) = CompairStringResult.Equal Then
                    qry = "    select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE              as [Document Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME        AS Description" &
                   " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_DEPARTMENT_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDevisionMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_DEVISION_MASTER.DEVISION_CODE               as [Document Code],TSPL_DEVISION_MASTER.DEVISION_NAME         AS Description" &
                   " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_DEVISION_MASTER " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSubDepartmentMaster) = CompairStringResult.Equal Then
                    qry = "   select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type]," &
                            " TSPL_SUB_DEPARTMENT_MASTER.SUB_DEPARTMENT_CODE as [Document Code],TSPL_SUB_DEPARTMENT_MASTER.DESCRIPTION            AS Description,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_SUB_DEPARTMENT_MASTER" &
                             " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCastCategoryMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_CAST_CATEGORY_MASTER.CAST_CATEGORY_CODE                as [Document Code],TSPL_CAST_CATEGORY_MASTER.CAST_CATEGORY_NAME          AS Description,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By]," &
                        " convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_CAST_CATEGORY_MASTER" &
                        " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDocumentMaster) = CompairStringResult.Equal Then
                    qry = "   select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_DOCUMENT_MASTER.DOCUMENT_CODE  as [Document Code],TSPL_DOCUMENT_MASTER.DOCUMENT_NAME   AS Description," &
                    " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_DOCUMENT_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCourseMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_COURSE_MASTER.COURSE_CODE   as [Document Code],TSPL_COURSE_MASTER.COURSE_CODE    AS Description," &
                  "  '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_COURSE_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGradeMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_GRADE_MASTER.GRADE_CODE    as [Document Code],TSPL_GRADE_MASTER.GRADE_NAME     AS Description," &
                        "  '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_GRADE_MASTER" &
                       " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLanguageMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_LANGUAGE_MASTER.LANGUAGE_CODE     as [Document Code],TSPL_LANGUAGE_MASTER.LANGUAGE_NAME     AS Description," &
                         " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_LANGUAGE_MASTER" &
                        " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOccupationMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_OCCUPATION_MASTER.OCCUPATION_CODE      as [Document Code],TSPL_OCCUPATION_MASTER.OCCUPATION_NAME      AS Description," &
                  "  '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_OCCUPATION_MASTER" &
                  " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmReligionMaster) = CompairStringResult.Equal Then
                    qry = "   select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_RELIGION_MASTER.RELIGION_CODE       as [Document Code],TSPL_RELIGION_MASTER.RELIGION_NAME       AS Description," &
                   " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_RELIGION_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSkillMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_SKILL_MASTER.SKILL_CODE        as [Document Code],TSPL_SKILL_MASTER.SKILL_NAME        AS Description," &
                    " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_SKILL_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOTSlab) = CompairStringResult.Equal Then
                    qry = "   select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_OT_SLAB.OT_CODE         as [Document Code],TSPL_OT_SLAB.SLAB_DESCRIPTION         AS Description," &
                    " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_OT_SLAB" &
                      "   where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmConveyanceRateMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_CONVEYANCE_RATE_MASTER.CONV_RATE_CODE          as [Document Code],TSPL_CONVEYANCE_RATE_MASTER.DESCRIPTION          AS Description," &
                    " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_CONVEYANCE_RATE_MASTER" &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmODMaster) = CompairStringResult.Equal Then
                    qry = "  select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_OD_MASTER.OD_CODE           as [Document Code],TSPL_OD_MASTER.DESCRIPTION          AS Description," &
                    "  '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_OD_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPayrollSetting) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_PAYROLL_SETTING.PAY_SETTING_CODE            as [Document Code],'' AS Description," &
                   " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_PAYROLL_SETTING " &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPayrollDesignationMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_DESIGNATION_MASTER.Designation_id             as [Document Code],Designation_Desc  AS Description," &
                   " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By   as [Modified By],convert(varchar,Modify_Date  ,103) as [Modified Date],'' as [Status] from  TSPL_DESIGNATION_MASTER" &
                   " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPaymentMode) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_PAYMENT_MODE.Code             as [Document Code],TSPL_PAYMENT_MODE.Name   AS Description," &
                   " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],'' as [Status] from  TSPL_PAYMENT_MODE " &
                  " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPTSlab) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_PT_RULE_MASTER.PT_CODE as [Document Code],TSPL_PT_RULE_MASTER.PT_NAME    AS Description," &
                   " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],'' as [Status] from  TSPL_PT_RULE_MASTER" &
                    " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    '===================Added by Preeti gupta[04/10/2019]================
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmployee_Master) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_EMPLOYEE_MASTER.EMP_CODE      as [Document Code],TSPL_EMPLOYEE_MASTER.Emp_Name    as Description ,convert(varchar,Joining_date      ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                      "  ,Modify_By   as [Modified By],convert(varchar,Modify_Date  ,103) as [Modified Date],''  as [Status] from TSPL_EMPLOYEE_MASTER" &
                   " where 2=2  and Convert (date,Joining_date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Joining_date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmployeeStatus) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_EMPLOYEE_STATUS.EMP_STATUS_CODE  as [Document Code],''   as Description ,convert(varchar,APPLICABLE_FROM       ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                      "  ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],''  as [Status] from TSPL_EMPLOYEE_STATUS" &
                      " where 2=2  and Convert (date,APPLICABLE_FROM,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, APPLICABLE_FROM,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPayHeadDefinitions) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],PAY_HEAD_CODE   as [Document Code],PAY_HEAD_NAME   as Description ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                      "  ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],''  as [Status] from TSPL_PAYHEAD_MASTER" &
                  " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSalaryStructure) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],SALARY_STRUCTURE_CODE    as [Document Code],SALARY_STRUCTURE_NAME    as Description ," &
                    " '' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],''  as [Status] from TSPL_SALARY_STRUCTURE" &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmpSalary) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],EMP_SAL_CODE    as [Document Code],''   as Description ,convert(varchar,APPLICABLE_FROM       ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                      "  ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],''  as [Status] from TSPL_EMPLOYEE_SALARY" &
                      " where 2=2  and Convert (date,APPLICABLE_FROM,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, APPLICABLE_FROM,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmPTSlab) = CompairStringResult.Equal Then


                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLeaveOpeningBalance) = CompairStringResult.Equal Then

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLeaveOpeningBalance) = CompairStringResult.Equal Then



                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGeneralHolidays) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],GHOLIDAY_CODE     as [Document Code],DESCRIPTION    as Description ,convert(varchar,HOLIDAY_DATE        ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],''  as [Status] from TSPL_GENERAL_HOLIDAYS" &
                        " where 2=2  and Convert (date,HOLIDAY_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, HOLIDAY_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmWeeklyHolidays) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],WKHOLIDAY_CODE      as [Document Code],WKHOLIDAY_NAME     as Description ,convert(varchar,APPLICABLE_FROM         ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],''  as [Status] from TSPL_WEEKLY_HOLIDAYS" &
                         " where 2=2  and Convert (date,APPLICABLE_FROM,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, APPLICABLE_FROM,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLeaveAllotment) = CompairStringResult.Equal Then

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLeaveApplication) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LVAPPLICATION_CODE       as [Document Code],LEAVE_REASON      as Description ,convert(varchar,APPLICATION_DATE          ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                  " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pnding' end  as [Status] from TSPL_LEAVE_APPLICATION" &
                  "  where 2=2  and Convert (date,APPLICATION_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, APPLICATION_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLeaveAdjustment) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LVADJUSTMENT_CODE        as [Document Code],LEAVE_REASON      as Description ,convert(varchar,ADJUSTMENT_DATE,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                 "  ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_LEAVE_ADJUSTMENT" &
                " where 2=2  and Convert (date,ADJUSTMENT_DATE,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ADJUSTMENT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmployeeIncrement) = CompairStringResult.Equal Then

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAllowanceDetails) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],ALLOWANCE_CODE         as [Document Code],ALLOWANCE_REMARKS       as Description ,convert(varchar,ALLOWANCE_DATE ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_ALLOWANCE" &
                " where 2=2  and Convert (date,ALLOWANCE_DATE ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ALLOWANCE_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDeductionDetails) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DEDUCTION_CODE         as [Document Code],DEDUCTION_REMARKS       as Description ,convert(varchar,DEDUCTION_DATE ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
               " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_DEDUCTION" &
                " where 2=2  and Convert (date,DEDUCTION_DATE ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, DEDUCTION_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmReimbursementDetails) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],REIMBURSEMENT_CODE          as [Document Code],REIMBURSEMENT_REMARK        as Description ,convert(varchar,REIMBURSEMENT_DATE,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_EMP_REIMBURSEMENT" &
                    " where 2=2  and Convert (date,REIMBURSEMENT_DATE ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, REIMBURSEMENT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmApplyLoan) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LOAN_CODE           as [Document Code],LOAN_DESCRIPTION         as Description ,convert(varchar,LOAN_DATE ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    "  ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_LOAN_APPLICATION" &
                    " where 2=2  and Convert (date,LOAN_DATE ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, LOAN_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmGenerateBonus) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],EMP_BONUS_CODE            as [Document Code],DESCRIPTION          as Description ,convert(varchar,Posting_Date  ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
               " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_EMPLOYEE_BONUS" &
                " where 2=2  and Convert (date,Modified_Date  ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLoanGeneration) = CompairStringResult.Equal Then
                    qry = "    select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LOAN_GENERATION_CODE             as [Document Code], GENERATE_REMARKS   as Description ,convert(varchar,GENERATION_DATE   ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
              " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_LOAN_GENERATION" &
            " where 2=2  and Convert (date,GENERATION_DATE  ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, GENERATION_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLoanAdjustment) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],LOANADJUSTMENT_CODE              as [Document Code], ADJUSTMENT_REASON   as Description ,convert(varchar,ADJUSTMENT_DATE    ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_LOAN_ADJUSTMENT" &
                " where 2=2  and Convert (date,ADJUSTMENT_DATE  ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ADJUSTMENT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDailyAttendance) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],DLA_CODE               as [Document Code], DESCRIPTION    as Description ,convert(varchar,Att_Date     ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
              " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_DAILY_ATTENDANCE" &
                " where 2=2  and Convert (date,Att_Date  ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Att_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmHourlyAttendance) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_HOURLY_ATTENDANCE.DLA_CODE               as [Document Code], DESCRIPTION    as Description ,convert(varchar,TSPL_HOURLY_ATTENDANCE_DETAIL.ATTENDANCE_DATE ,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
               " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date],case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_HOURLY_ATTENDANCE" &
               " left join TSPL_HOURLY_ATTENDANCE_DETAIL on TSPL_HOURLY_ATTENDANCE_DETAIL.DLA_CODE=TSPL_HOURLY_ATTENDANCE.DLA_CODE " &
              " where 2=2  and Convert (date,ATTENDANCE_DATE  ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ATTENDANCE_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMonthlyAttendance) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_MONTHLY_ATTENDANCE.MTA_CODE  as [Document Code], DESCRIPTION  as Description ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                         " case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_MONTHLY_ATTENDANCE" &
                        " where 2=2  and Convert (date,Modified_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmOTSheet) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_OT_SHEET.OT_SHEET_CODE  as [Document Code], ''    as Description ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                            " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                            " case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_OT_SHEET" &
                            " where 2=2  and Convert (date,Modified_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAdjustmentVoucher) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE   as [Document Code], ADJUSTMENT_REMARK     as Description ,convert(varchar,ADJUSTMENT_DATE,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                          " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                          " case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_ADJUSTMENT_VOUCHER" &
                          " where 2=2  and Convert (date,ADJUSTMENT_DATE   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ADJUSTMENT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmSalaryGeneration) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE    as [Document Code], GENERATE_REMARKS      as Description ,convert(varchar,GENERATE_DATE ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                        " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                        " case when POSTED =1 then 'Posted' else 'Pending' end  as [Status] from TSPL_GENERATE_SALARY" &
                        " where 2=2  and Convert (date,GENERATE_DATE   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, GENERATE_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmployeeGratuity) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_GRATUITY.EMP_CODE      as [Document Code], ''      as Description ,convert(varchar,DOJ  ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                        " ''  as [Status] from TSPL_GRATUITY" &
                        " where 2=2  and Convert (date,DOJ   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, DOJ,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmLTAClaim) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_LTA_Claim_Head.LTA_CODE       as [Document Code], ''      as Description ,convert(varchar,Claim_From_Date   ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                    " ''  as [Status] from TSPL_LTA_Claim_Head" &
                    " where 2=2  and Convert (date,Claim_From_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Claim_From_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "

                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmMediclaimEntry) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_MEDICLAIM_HEAD.DOCUMENT_CODE        as [Document Code], DESCRIPTION       as Description ,convert(varchar,DATE    ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                            ",Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                             " case when Status  ='N' then 'Pending' else 'Posted' end as [Status] from TSPL_MEDICLAIM_HEAD" &
                             " where 2=2  and Convert (date,DATE   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmFullAndFinalSettlement) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_FF_SETTLEMENT_HEAD.EMP_CODE         as [Document Code], DESCRIPTION       as Description ,convert(varchar,Document_Date     ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                            " case when POSTED =1 then 'Posted' else 'Pending'  end as [Status] from TSPL_FF_SETTLEMENT_HEAD" &
                            " where 2=2  and Convert (date,Document_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEmployeeShiftChange) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE          as [Document Code], DESCRIPTION       as Description ,convert(varchar,SHIFT_APP_DATE      ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                            " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                            " case when POSTED =1 then 'Posted' else 'Pending'  end as [Status] from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD" &
                            " where 2=2  and Convert (date,SHIFT_APP_DATE   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, SHIFT_APP_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmODSheet) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_OD_SHEET.OD_SHEET_CODE           as [Document Code], ''       as Description ,convert(varchar,FROM_Date       ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                    " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                    " '' as [Status] from TSPL_OD_SHEET" &
                    "  where 2=2  and Convert (date,FROM_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, FROM_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmConveyanceClaim) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_CONVEYANCE_CLAIM.CLAIM_CODE            as [Document Code], ''       as Description ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                          " '' as [Status] from TSPL_CONVEYANCE_CLAIM" &
                            " where 2=2  and Convert (date,Modified_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmEmployeeTransfer) = CompairStringResult.Equal Then
                    qry = "   select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_EMPLOYEE_TRANSFER.Document_Code as [Document Code], Description  as Description ,convert(varchar,Document_Date,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                        " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                        " '' as [Status] from TSPL_EMPLOYEE_TRANSFER" &
                        " where 2=2  and Convert (date,Document_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmEmpIncrement) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE  as [Document Code], ''  as Description ,convert(varchar,INCREMENT_DATE ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                        ",Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                        " case when POSTED =1 then 'Posted' else 'Pending' end as [Status] from TSPL_EMPLOYEE_INCREMENT_HEAD" &
                        " where 2=2  and Convert (date,INCREMENT_DATE   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, INCREMENT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmAllotmentOfLeaves) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE   as [Document Code], ALLOTMENT_REMARKS   as Description ,convert(varchar,ALLOTMENT_DATE  ,103)  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     " ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                  "  '' as [Status] from TSPL_LEAVE_ALLOTMENT" &
                    " where 2=2  and Convert (date,ALLOTMENT_DATE   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, ALLOTMENT_DATE,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.EmployeeBandMaster) = CompairStringResult.Equal Then
                    qry = "  select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_EMPLOYEE_BAND_MASTER.code   as [Document Code], DESCRIPTION    as Description ,''  as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                  "  ,Modified_By    as [Modified By],convert(varchar,Modified_Date   ,103) as [Modified Date]," &
                    "'' as [Status] from TSPL_EMPLOYEE_BAND_MASTER" &
                   " where 2=2  and Convert (date,Modified_Date   ,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                End If
                ''''''Sanjay, Tax deduction at source'''''''''''''''''''
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleTDS) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.NatureOfDeduction) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_TDS_DEDUCTION_HEAD.Deduction_Code   as [Document Code],TSPL_TDS_DEDUCTION_HEAD.Description  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],'' as [Status] from  TSPL_TDS_DEDUCTION_HEAD " &
                     " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PartyDetails) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_TDS_VENDOR_DETAILS.Vendor_Code   as [Document Code],TSPL_VENDOR_MASTER.Vendor_Name AS Description" &
                    " ,'' as [Document Date],TSPL_TDS_VENDOR_DETAILS.Created_By as [Created By],convert(varchar,TSPL_TDS_VENDOR_DETAILS.Created_Date,103) as [Created Date],TSPL_TDS_VENDOR_DETAILS.Modify_By  as [Modified By],convert(varchar,TSPL_TDS_VENDOR_DETAILS.Modify_Date ,103) as [Modified Date],'' as [Status] from  TSPL_TDS_VENDOR_DETAILS " &
                    "  left join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TDS_VENDOR_DETAILS.Vendor_Code " &
                     " where 2=2  and Convert (date,TSPL_TDS_VENDOR_DETAILS.Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, TSPL_TDS_VENDOR_DETAILS.Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.TDSSection) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_TDS_SECTION_MASTER.TDS_Group   as [Document Code],TSPL_TDS_SECTION_MASTER.Description  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],'' as [Status] from  TSPL_TDS_SECTION_MASTER " &
                     " where 2=2  and Convert (date,Modify_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modify_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnAPInvoiceEntryTDS) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],TSPL_VENDOR_INVOICE_HEAD.Document_No as [Document Code],'' as Description ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],(case when len(Posting_Date) is null then 'Pending' else 'Posted' end)   as [Status] from TSPL_VENDOR_INVOICE_HEAD" &
                        " where TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1  and Convert (date,Invoice_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Invoice_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.TDSPAYMENT) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code],'' as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],case when Posted   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_TDS_PAYMENT_HEADER" &
                        " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                End If
                ''''''Sanjay, Fixed Assets'''''''''''''''''''
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleFixedAsset) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.DepAccSets) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],AcSet_Code as [Document Code],AcSet_Desc  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_Dep_AccountSet " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.Categories) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Category_Code   as [Document Code],Description  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_ASSET_CATEGORY " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.CostFACenter) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],CostCenter_Code  as [Document Code],CostCenter_Name  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_FA_COST_CENTER_MASTER " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAssetGroups) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Group_Code   as [Document Code],Description  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_ASSET_GROUP " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmDepreciationMethod) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code   as [Document Code],Description  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_DEPRECIATION_METHOD " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.DepPeriod) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],period_Code   as [Document Code],period_Desc  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_DEPRECIATION_PERIODS " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.Template) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Template_Code as [Document Code],template_Name  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_FA_TEMPLATE_MASTER " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAssetBookMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Book_Code as [Document Code],Book_Name  AS Description" &
                    " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_FA_BOOK_MASTER " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    'Fixed Assets Transaction
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FAAcquisitionEntry) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Acquisition_Code as [Document Code],Description as Description ,convert(varchar,Acquisition_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_ACQUISITION_HEAD" &
                        " where TSPL_ACQUISITION_HEAD.Acquisition_Type<>'Merge' and Convert (date,Acquisition_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Acquisition_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FAAssetDepreciation) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],'' as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Created_By  as [Modified By],convert(varchar,Created_Date ,103) as [Modified Date],'' as [Status] from TSPL_ASSET_DEPRECIATION" &
                        " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FADisposalEntry) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_No as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_ASSET_SCRAP_HEAD" &
                        " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAsset_Issue_Return) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Asset_Id as [Document Code],'' as Description ,convert(varchar,Trans_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from TSPL_ASSET_ISSUE_RETURN" &
                        " where 2=2  and Convert (date,Trans_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Trans_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmAssetStoreRequistion) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Requisition_Id as [Document Code],Description as Description ,convert(varchar,Requisition_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_REQUISITION_HEAD" &
                        " where  Is_Internal='Y' and Requisition_Type<>''  and Convert (date,Requisition_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Requisition_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmIssueItemsToAsset) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code],'' as Description ,convert(varchar,Doc_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_IssueItemToAssembledAsset_Head" &
                        " where 2=2  and Convert (date,Doc_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Doc_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FAAssetWork) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_ASSET_WORK_HEAD" &
                        " where 2=2  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FAMergeAcquisitionEntry) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Acquisition_Code as [Document Code],Description as Description ,convert(varchar,Acquisition_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                         ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_ACQUISITION_HEAD" &
                        " where TSPL_ACQUISITION_HEAD.Acquisition_Type='Merge' and Convert (date,Acquisition_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Acquisition_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                End If
                ''''''Sanjay, Export Sale'''''''''''''''''''
            ElseIf clsCommon.CompairString(strModuleCode, clsUserMgtCode.ModuleExportSale) = CompairStringResult.Equal Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEnquiryMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Code as [Document Code],Name AS Description" &
                    " ,convert(varchar,Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  tspl_enquiry_master " &
                     " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmNotifiedPartyMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code],Description  AS Description" &
                  " ,'' as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_NOTIFY_PARTY_HEAD " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmCHAChargeMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_No as [Document Code],Description  AS Description" &
                  " ,convert(varchar,Doc_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_CHA_CHARGE_MASTER " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmExIncentiveMaster) = CompairStringResult.Equal Then
                    qry = " select  '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Doc_Code as [Document Code],Description  AS Description" &
                  " ,convert(varchar,Doc_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date],Modified_By  as [Modified By],convert(varchar,Modified_Date ,103) as [Modified Date],'' as [Status] from  TSPL_Ex_Incentive_Head " &
                   " where 2=2  and Convert (date,Modified_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Modified_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Created Date],103)  "
                    ''Transaction
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEXSalesQuotation) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status=1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SD_SALES_Quotation_HEAD" &
                    " where salesorder_type='EX' and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEXSalesOrder) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SD_SALES_ORDER_HEAD" &
                    " where TSPL_SD_SALES_ORDER_HEAD.trans_type='EXP' and TSPL_SD_SALES_ORDER_HEAD.salesorder_type='EX'  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEXPorformaInvoice) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_EX_PI_HEAD" &
                    " where TSPL_EX_PI_HEAD.document_type='EX' and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEXCommercialInvoice) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_EX_COMMERCIAL_INVOICE_HEAD" &
                    " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='EX' and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEXSalesInvoice) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SD_SALE_INVOICE_HEAD" &
                    " where TSPL_SD_SALE_INVOICE_HEAD.trans_type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.document_type='EX' and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmEXSalesReturn) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Document_Code as [Document Code],Description as Description ,convert(varchar,Document_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Status   =1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SD_SALE_RETURN_HEAD" &
                    " where TSPL_SD_SALE_RETURN_HEAD.document_type='EX' and TSPL_SD_SALE_RETURN_HEAD.trans_type='EXP'  and Convert (date,Document_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Document_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmsaleReturnGateEntryExportSAle) = CompairStringResult.Equal Then
                    qry = " select '" + strMoudleName + "' as [Module Name],'" + ScrScreenName + "' as [Screen Name] , '" + ScreenType + "' as [Screen Type],Gate_Entry_No as [Document Code],'' as Description ,convert(varchar,Gate_Entry_Date,103) as [Document Date],Created_By as [Created By],convert(varchar,Created_Date,103) as [Created Date]" &
                     ",Modify_By  as [Modified By],convert(varchar,Modify_Date ,103) as [Modified Date],case when Posted=1 then 'Posted' else 'Pending' end   as [Status] from TSPL_SALE_RETURN_GATE_ENTRY_HEAD" &
                    " where 2=2  and Convert (date,Gate_Entry_Date,103)  > = Convert (date, '" + txtFromDate.Value + "'  ,103) and Convert (date, Gate_Entry_Date,103)  < = convert ( date, '" + txtToDate.Value + "',103) "
                    StrOrderBy = " order by Convert (datetime, zz.[Document Date],103)  "
                End If
            End If
            If clsCommon.myLen(qry) > 0 Then
                qry = "select zz.[Module Name],zz.[Screen Name],zz.[Screen Type],zz.[Document Code],zz.Description,zz.[Document Date],zz.[Created By],cre_user.User_Name as [Created By Name],zz.[Created Date] " &
                    ",zz.[Modified By],modi_user.User_Name as [Modified By Name],zz.[Modified Date],zz.[Status] from (" + qry + " )zz" &
                    " left join tspl_user_master as cre_user on cre_user.user_code=zz.[Created By] " &
                   " left join tspl_user_master as modi_user on modi_user.user_code=zz.[Modified By] " + StrOrderBy


                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.Columns.Clear()
                    gv1.Rows.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.ShowGroupPanel = True
                    gv1.EnableFiltering = True
                    RadPageView1.SelectedPage = RadPageViewPage2

                    fndScreen.Enabled = False
                    RadGroupBox2.Enabled = False
                    Panel1.Enabled = False

                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                End If

                gv1.DataSource = dt
                SetGridFormationOFGV1()
                gv1.BestFitColumns()

                ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No Data Related to this screen.")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
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

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        fndScreen.Value = ""
        lblScreen.Text = ""
        fndScreen.Enabled = True
        RadGroupBox2.Enabled = True
        Panel1.Enabled = True
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportGridID()
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        GetReportID()
        Print(Exporter.Refresh)
        GetReportID()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rdbMaster.Checked Then
            VarID += "_M"
        ElseIf rdbTransation.Checked Then
            VarID += "_T"
        End If
        gv1.VarID = VarID

    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbMaster.Checked = True Then
            VarID += "_SU"
        Else
            rdbTransation.Checked = True
            VarID += "_DE"
        End If
        gv1.VarID = VarID
    End Sub
    Sub GetReportID()
        Dim VarID As String = ""
        If rdbMaster.Checked Then
            VarID += "_M"
        ElseIf rdbTransation.Checked Then
            VarID += "_T"
        End If
        gv1.VarID = VarID
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
        'Wrong formatted date update in UDL
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Dim qry As String = "update TSPL_TANKER_MASTER set Created_Date='06/12/2016' where Tanker_No='UP13N9585'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If
        'Wrong formatted date update in UDL
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        strModuleCode = MyBase.Module_Code
        fndModule.Value = MyBase.Module_Code
        lblModule.Text = clsDBFuncationality.getSingleValue(" select Program_Name  from TSPL_PROGRAM_MASTER where Program_Code = '" + strModuleCode + "' ")
        fndModule.Enabled = False
        txtFromDate.Checked = True
        txtToDate.Checked = True
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
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
                arrHeader.Add("Name : Audit Trail Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If


                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
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
                arrHeader.Add("Name : Audit Trail Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If

                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Audit Trail Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtEmployee__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = "select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Code] ,TSPL_EMPLOYEE_MASTER.Emp_Name as [Name] from TSPL_EMPLOYEE_MASTER "
    '    txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@Employee", qry, "Code", "Name", txtEmployee.arrValueMember, txtEmployee.arrDispalyMember)
    'End Sub

    Private Sub fndScreen__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndScreen._MYValidating
        Dim qry As String = Nothing
        Dim wher As String = " 2= 2"
        If rdbMaster.Checked = True Then
            qry = "  select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name , TBL_MODULE.Program_Name as [Module Name] from TSPL_PROGRAM_MASTER " &
                  "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code " &
                  "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code  "
            wher = " TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') and TBL_MODULE.Program_Code in ('" + strModuleCode + "') and TBL_SMODULE.Program_Name in ('Setup') and  TSPL_PROGRAM_MASTER.Program_Code in ('" + clsUserMgtCode.FrmZoneMasterDS + "','" + clsUserMgtCode.frmRouteMasterDS + "','" + clsUserMgtCode.frmDistributorRouteTagging + "','" + clsUserMgtCode.frmDistributorCommission + "','" + clsUserMgtCode.FrmSchemeMasterDairyDS + "','" + clsUserMgtCode.SaleIncentiveMaster + "','" + clsUserMgtCode.CustomerDeduction + "','" + clsUserMgtCode.frmbookingdairy + "','" + clsUserMgtCode.frmDeliveryOrderDairy + "','" + clsUserMgtCode.frmSaleDispatchDairy + "','" + clsUserMgtCode.frmSaleInvoicedairy + "','" + clsUserMgtCode.frmSaleReturndairy + "','" + clsUserMgtCode.frmGatePassDairy + "','" + clsUserMgtCode.frmCrateReceviedDairySale + "','" + clsUserMgtCode.frmBookingDairyMultipleDistributor + "','" + clsUserMgtCode.frmPOSBookingDairyMultipleDistributor + "','" + clsUserMgtCode.frmDairyGatePass + "','" + clsUserMgtCode.frmDairyBookingCustomer + "','" + clsUserMgtCode.frmPerformaInvoiceDairy + "','" + clsUserMgtCode.CustomerIncentiveEntry + "','" + clsUserMgtCode.FrmZoneMaster + "','" + clsUserMgtCode.frmRouteMaster + "','" + clsUserMgtCode.frmSalesLevelHierarchy + "','" + clsUserMgtCode.FrmBookingEntry + "','" + clsUserMgtCode.frmDeliveryNoteFreshSale + "','" + clsUserMgtCode.FrmDispatchFreshSale + "','" + clsUserMgtCode.frmInvoiceFreshSale + "','" + clsUserMgtCode.frmCreateReceived + "','" + clsUserMgtCode.frmSaleReturnFreshSale + "','" + clsUserMgtCode.FrmGatePassFS + "','" + clsUserMgtCode.frmCrateReceviedDairySale + "','" + clsUserMgtCode.FrmInvoiceCrateLinerDetail + "','" + clsUserMgtCode.frmsaleReturnGateEntryFS + "','" + clsUserMgtCode.frmSubsidyCreditNote + "','" + clsUserMgtCode.FrmSchemeMasterDairy + "','" + clsUserMgtCode.FrmTragetMaster + "','" + clsUserMgtCode.DispatchChecklist + "','" + clsUserMgtCode.frmBookingProductSale + "','" + clsUserMgtCode.frmdispatchAdviceProductSale + "','" + clsUserMgtCode.frmDeliveryPrderProductSale + "','" + clsUserMgtCode.frmSalesOrderProductSale + "','" + clsUserMgtCode.frmShipmentProductSale + "','" + clsUserMgtCode.frmSaleInvoiceProductSale + "','" + clsUserMgtCode.frmSaleReturnProductSale + "','" + clsUserMgtCode.FrmProductDispatchGateOut + "','" + clsUserMgtCode.frmGateEntryReturnPS + "','" + clsUserMgtCode.frmsaleReturnGateEntryPS + "','" + clsUserMgtCode.FrmGatePassPS + "','" + clsUserMgtCode.ModuleElectrical + "','" + clsUserMgtCode.frmDGMaster + "','" + clsUserMgtCode.frmDailyElectricalEntry + "','" + clsUserMgtCode.frmJWParameterMaster + "','" + clsUserMgtCode.frmJWFormulaMaster + "','" + clsUserMgtCode.frmJWVendorFormula + "','" + clsUserMgtCode.JWIItemPriceMaster + "','" + clsUserMgtCode.FrmSRNJobWorkEstimate + "','" + clsUserMgtCode.frmJobWorkBillig + "','" + clsUserMgtCode.frmJobWorkoutwordMaster + "','" + clsUserMgtCode.frmJobWorkoutwordMaster + "','" + clsUserMgtCode.frmMilkJobWorkTransfer + "','" + clsUserMgtCode.frmMilkJobWorkTransferReturn + "','" + clsUserMgtCode.frmMilkJobWorkTransferOther + "','" + clsUserMgtCode.frmMilkJobWorkTransferOtherReturn + "','" + clsUserMgtCode.frmGateEntry_JWO + "','" + clsUserMgtCode.frmWeighment_JWO + "','" + clsUserMgtCode.frmQC_JWO + "','" + clsUserMgtCode.frmUnloading_JWO + "','" + clsUserMgtCode.JWO_SRN + "','" + clsUserMgtCode.JWO_SRN_Return + "','" + clsUserMgtCode.frmJobWorkConsumption + "','" + clsUserMgtCode.frmMCCMaster + "','" + clsUserMgtCode.frmMilkRouteMaster + "','" + clsUserMgtCode.frmVillageMaster + "','" + clsUserMgtCode.frmVSPMaster + "','" + clsUserMgtCode.frmVLCMaster + "','" + clsUserMgtCode.frmVLCUploader + "','" + clsUserMgtCode.frmMPMaster + "','" + clsUserMgtCode.frmPrimaryTransporterMaster + "','" + clsUserMgtCode.frmPrimaryTransporterVehicalMaster + "','" + clsUserMgtCode.frmVLCRouteShiftMaster + "','" + clsUserMgtCode.MilkPricePlanning + "','" + clsUserMgtCode.frmPriceChartMaster + "','" + clsUserMgtCode.FrmPriceChartUploader + "','" + clsUserMgtCode.frmPriceChartMaster_Bulk + "','" + clsUserMgtCode.frmMCCMaterialSalePriceChart + "','" + clsUserMgtCode.frmMilkVehicleMaster + "','" + clsUserMgtCode.frmMilkAdvanceMaster + "','" + clsUserMgtCode.frmdeductionGroup + "','" + clsUserMgtCode.frmGroupOfDeduction + "','" + clsUserMgtCode.frmDeductionMaster + "','" + clsUserMgtCode.DeductionMapping + "','" + clsUserMgtCode.CanMaster + "','" + clsUserMgtCode.DockMaster + "','" + clsUserMgtCode.FrmTankerDispatchPriceMaster + "','" + clsUserMgtCode.MilkRejectType + "','" + clsUserMgtCode.frmMilkReasonMaster + "','" + clsUserMgtCode.frmPaymentCycleMaster + "','" + clsUserMgtCode.frmIncentiveMaster + "','" + clsUserMgtCode.frmMCCSMSSettiing + "','" + clsUserMgtCode.frmOpenMCCShiftManual + "','" + clsUserMgtCode.FrmVSPIncentiveTagging + "','" + clsUserMgtCode.frmVLCMasterTarget + "','" + clsUserMgtCode.frmItemChargeCategoryMaster + "','" + clsUserMgtCode.FreightChargesMaster + "','" + clsUserMgtCode.frmMCCMaster + "','" + clsUserMgtCode.frmMCCMaterialSaleReturnFarmer + "','" + clsUserMgtCode.MPBillGeneration + "','" + clsUserMgtCode.frmPaymentProcessFarmer + "','" + clsUserMgtCode.frmFarmerPaymentAdjustment + "','" + clsUserMgtCode.frmLockMPCollectionPC + "','" + clsUserMgtCode.frmSectionAllowanceMaster + "','" + clsUserMgtCode.frmSavingsMaster + "','" + clsUserMgtCode.frmEmployeeSavingsMapping + "','" + clsUserMgtCode.frmIncomeTaxSlab + "','" + clsUserMgtCode.frmHouseRentDeclaration + "','" + clsUserMgtCode.IncomeTaxTDSCalculation + "','" + clsUserMgtCode.frmOpenMCCShift + "','" + clsUserMgtCode.MilkGateEntryIn + "','" + clsUserMgtCode.MilkGateEntryWeightment + "','" + clsUserMgtCode.frmMilkReceipt + "','" + clsUserMgtCode.MilkReject + "','" + clsUserMgtCode.frmMilkSample + "','" + clsUserMgtCode.MilkGateEntryOut + "','" + clsUserMgtCode.frmMilkSRN + "','" + clsUserMgtCode.frmVlcdataUploadar + "','" + clsUserMgtCode.FrmVLCDataUploaderManual + "','" + clsUserMgtCode.frmMilkShiftEndMCC + "','" + clsUserMgtCode.frmMCCDispatch + "','" + clsUserMgtCode.frmMilkPurchaseInvoice + "','" + clsUserMgtCode.frmVSPAssetIssue + "','" + clsUserMgtCode.frmMCCMaterial + "','" + clsUserMgtCode.frmMCCMaterialSaleReturn + "','" + clsUserMgtCode.frmProvisionEntry + "','" + clsUserMgtCode.IncentiveEntry + "','" + clsUserMgtCode.frmPaymentProcess + "','" + clsUserMgtCode.frmMccScrapGatePass + "','" + clsUserMgtCode.MccMilkTransferPrice + "" &
                        "','" + clsUserMgtCode.frmTankerTransporterMaster + "','" + clsUserMgtCode.frmTankerMaster + "','" + clsUserMgtCode.frmParameterMaster + "','" + clsUserMgtCode.ParameterValueMaster + "','" + clsUserMgtCode.frmPriceChartBulkProc + "','" + clsUserMgtCode.FrmContractTanker + "','" + clsUserMgtCode.FrmSupplierMaster + "','" + clsUserMgtCode.frmDivertedContractor + "','" + clsUserMgtCode.frmMilkTypeMast + "','" + clsUserMgtCode.frmMilkGradeMaster + "','" + clsUserMgtCode.FrmBulkRoutMaster + "','" + clsUserMgtCode.TankerCleaningItem + "" &
                        "','" + clsUserMgtCode.frmPOBulkProc + "','" + clsUserMgtCode.frmIntimation + "','" + clsUserMgtCode.frmGateEntry + "','" + clsUserMgtCode.frmWeighment + "','" + clsUserMgtCode.frmQualityCheck + "','" + clsUserMgtCode.frmUnloading + "','" + clsUserMgtCode.frmCleaning + "','" + clsUserMgtCode.frmGateOut + "','" + clsUserMgtCode.frmBulkMilkSRN + "','" + clsUserMgtCode.frmBulkMilkPurchaseInvoice + "','" + clsUserMgtCode.frmMilkTransferIn + "','" + clsUserMgtCode.frmProvisionEntry + "','" + clsUserMgtCode.frmBulkMilkSRNReturn + "','" + clsUserMgtCode.MccMilkTransferPrice + "','" + clsUserMgtCode.FrmMilkPurchaseReturn + "','" + clsUserMgtCode.frmMilkTransferInReturn + "" &
                        "','" + clsUserMgtCode.FrmSectionMaster + "','" + clsUserMgtCode.FrmStageMaster + "','" + clsUserMgtCode.frmPPLogSheetMaster + "','" + clsUserMgtCode.frmProcessProductionLogSheet + "','" + clsUserMgtCode.FrmSectionStageMapping + "','" + clsUserMgtCode.frmBillOfMaterialDairy + "','" + clsUserMgtCode.frmProfitCenter + "','" + clsUserMgtCode.FrmLineMaster + "" &
                        "','" + clsUserMgtCode.frmProductionPlanningDairy + "','" + clsUserMgtCode.frmBatchOrderDairy + "','" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmProductionEntryFinalQC + "','" + clsUserMgtCode.frmWreckageBooking + "','" + clsUserMgtCode.frmAssembDis + "','" + clsUserMgtCode.JobWorkDispatchProduction + "','" + clsUserMgtCode.frmProcessProdReturn + "','" + clsUserMgtCode.frmSiloMilkTransfer + "','" &
                        "','" + clsUserMgtCode.FrmItemMasterRMOther + "','" + clsUserMgtCode.itemStructure + "','" + clsUserMgtCode.itemPurchaseAccount + "','" + clsUserMgtCode.itemSaleAccount + "','" + clsUserMgtCode.unitMaster + "','" + clsUserMgtCode.chapterhead + "','" + clsUserMgtCode.packType + "','" + clsUserMgtCode.PriceComponentMasters + "','" + clsUserMgtCode.PriceComponentMapping + "','" + clsUserMgtCode.PricePlan + "','" + clsUserMgtCode.PriceMaster + "','" + clsUserMgtCode.mbtnBreakageHead1 + "','" + clsUserMgtCode.locationMaster + "','" + clsUserMgtCode.frmStandardscheme + "','" + clsUserMgtCode.frmStandardRateItem + "','" + clsUserMgtCode.frmItemCategoryLevel + "" &
                        "','" + clsUserMgtCode.frmItemCategoryStructure + "','" + clsUserMgtCode.WarrantyMaster + "','" + clsUserMgtCode.frmSchemeMasterNew + "','" + clsUserMgtCode.frmPriceGroupMapping + "','" + clsUserMgtCode.frmLocationCategoryLevel + "','" + clsUserMgtCode.frmLocationCategoryStructure + "','" + clsUserMgtCode.FrmCatalogMaster + "','" + clsUserMgtCode.frmPartNoMaster + "','" + clsUserMgtCode.InvetorySourceCode + "','" + clsUserMgtCode.FrmItemTypeMaster + "','" + clsUserMgtCode.frmHSNMaster + "','" + clsUserMgtCode.frmOverheadCostMaster + "','" + clsUserMgtCode.FrmOverheadCostGroup + "','" + clsUserMgtCode.FrmItemCostMapping + "','" + clsUserMgtCode.frmWeightUomMaster + "" &
                        "','" + clsUserMgtCode.Indent + "','" + clsUserMgtCode.Transfer + "','" + clsUserMgtCode.mbtnEmptyTrans + "','" + clsUserMgtCode.mbtnStoreAdjustment + "','" + clsUserMgtCode.frmWarehouseBreakage + "','" + clsUserMgtCode.frmPhysicalStock + "','" + clsUserMgtCode.ChangeItemSerialNumber + "','" + clsUserMgtCode.TransferReturn + "','" + clsUserMgtCode.FrmTransferGateOut + "','" + clsUserMgtCode.frmGateEntryReturnTransfer + "','" + clsUserMgtCode.FrmJobWorkInventory + "','" + clsUserMgtCode.frmRawMilkConsumtion + "','" + clsUserMgtCode.frmGeneralWeighment + "" &
                         "','" + clsUserMgtCode.CostCenter + "','" + clsUserMgtCode.RequisitSubTypeMaster + "','" + clsUserMgtCode.FrmFormIssueReceiptEntry + "','" + clsUserMgtCode.FrmCostCentreGroupStores + "','" + clsUserMgtCode.frmDeliveryTermsMaster + "','" + clsUserMgtCode.capexmaster + "','" + clsUserMgtCode.capexbudget + "','" + clsUserMgtCode.frmUnitMaster + "','" + clsUserMgtCode.frmCostCenterTypeMaster + "" &
                          "','" + clsUserMgtCode.vendorSubgroup + "','" + clsUserMgtCode.FrmHirerachyLevelMaster + "','" + clsUserMgtCode.transportMasterVendor + "','" + clsUserMgtCode.VendorBankMaster + "','" + clsUserMgtCode.paymentTerms + "','" + clsUserMgtCode.vendormaster + "','" + clsUserMgtCode.vendorgroup + "','" + clsUserMgtCode.vendoraccountset + "" &
                           "','" + clsUserMgtCode.taxAuthority + "','" + clsUserMgtCode.taxRate + "','" + clsUserMgtCode.taxGroup + "','" + clsUserMgtCode.paymentCodes + "','" + clsUserMgtCode.cityMaster + "','" + clsUserMgtCode.FrmAbateMentMaster + "','" + clsUserMgtCode.FrmCompanyMaster + "','" + clsUserMgtCode.FrmAdditionalCharges + "','" + clsUserMgtCode.FormMaster + "','" + clsUserMgtCode.bankMaster + "','" + clsUserMgtCode.frmRegionMaster + "','" + clsUserMgtCode.frmCountryMaster1 + "','" + clsUserMgtCode.frmStateMaster1 + "','" + clsUserMgtCode.DistrictMaster + "','" + clsUserMgtCode.bankBranchMaster + "','" + clsUserMgtCode.FrmFormSerialNoMaster + "','" + clsUserMgtCode.FrmMCCDiscountMaster + "','" + clsUserMgtCode.AreaMaster + "','" + clsUserMgtCode.FrmCompanyMaster + "','" + clsUserMgtCode.FrmSAC + "','" + clsUserMgtCode.FrmItemWiseTax + "','" + clsUserMgtCode.FrmRackBinMaster + "','" + clsUserMgtCode.paymentTerms + "" &
                           "','" + clsUserMgtCode.CustomerType + "','" + clsUserMgtCode.CustomerAccountSet + "','" + clsUserMgtCode.CustomerGroup + "','" + clsUserMgtCode.CustomeCategory + "','" + clsUserMgtCode.CustomerMaster + "','" + clsUserMgtCode.SecondaryCustomerMaster + "','" + clsUserMgtCode.FrmPOSGRoupMaster + "" &
                           "','" + clsUserMgtCode.accountStructure + "','" + clsUserMgtCode.AccountMainGroup + "','" + clsUserMgtCode.accountGroup + "','" + clsUserMgtCode.AccountSubGroup + "','" + clsUserMgtCode.AccountGLMainAccount + "','" + clsUserMgtCode.glAccount + "','" + clsUserMgtCode.sourceCode + "','" + clsUserMgtCode.glsecurity + "','" + clsUserMgtCode.FiscalYear + "','" + clsUserMgtCode.CostCentreFinancial + "" &
                            "','" + clsUserMgtCode.FrmBulkSalePriceChart + "','" + clsUserMgtCode.TankerMasterSale + "" &
                              "','" + clsUserMgtCode.frmCountryMaster + "','" + clsUserMgtCode.frmStateMaster + "','" + clsUserMgtCode.frmBranchMaster + "','" + clsUserMgtCode.frmOTMaster + "','" + clsUserMgtCode.frmAttendanceMaster + "','" + clsUserMgtCode.frmBonusMaster + "','" + clsUserMgtCode.frmPayPeriodMaster + "','" + clsUserMgtCode.frmPFRulesMaster + "','" + clsUserMgtCode.frmESIRulesMaster + "','" + clsUserMgtCode.frmLeaveMaster + "','" + clsUserMgtCode.frmShiftMaster + "','" + clsUserMgtCode.frmDepartmentMaster + "','" + clsUserMgtCode.frmDevisionMaster + "','" + clsUserMgtCode.frmSubDepartmentMaster + "','" + clsUserMgtCode.frmCastCategoryMaster + "','" + clsUserMgtCode.frmDocumentMaster + "','" + clsUserMgtCode.frmCourseMaster + "','" + clsUserMgtCode.frmGradeMaster + "','" + clsUserMgtCode.frmLanguageMaster + "','" + clsUserMgtCode.frmOccupationMaster + "','" + clsUserMgtCode.frmReligionMaster + "','" + clsUserMgtCode.frmSkillMaster + "','" + clsUserMgtCode.frmOTSlab + "','" + clsUserMgtCode.frmConveyanceRateMaster + "','" + clsUserMgtCode.frmODMaster + "','" + clsUserMgtCode.frmPayrollSetting + "','" + clsUserMgtCode.frmSubDepartmentMaster + "','" + clsUserMgtCode.frmPayrollDesignationMaster + "','" + clsUserMgtCode.frmPaymentMode + "','" + clsUserMgtCode.frmPTSlab + "" &
                        "','" + clsUserMgtCode.NatureOfDeduction + "','" + clsUserMgtCode.PartyDetails + "','" + clsUserMgtCode.TDSSection + "" &
                        "','" + clsUserMgtCode.DepAccSets + "','" + clsUserMgtCode.Categories + "','" + clsUserMgtCode.CostFACenter + "','" + clsUserMgtCode.frmAssetGroups + "','" + clsUserMgtCode.frmDepreciationMethod + "','" + clsUserMgtCode.DepPeriod + "','" + clsUserMgtCode.Template + "','" + clsUserMgtCode.frmAssetBookMaster + "" &
                        "','" + clsUserMgtCode.frmEnquiryMaster + "','" + clsUserMgtCode.frmNotifiedPartyMaster + "','" + clsUserMgtCode.frmCHAChargeMaster + "','" + clsUserMgtCode.frmExIncentiveMaster + "" &
                        "')  "
        ElseIf rdbTransation.Checked Then
            qry = "  select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name , TBL_MODULE.Program_Name as [Module Name] from TSPL_PROGRAM_MASTER " & _
                  "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code " & _
                  "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code "
            wher = " TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') and TBL_MODULE.Program_Code in ('" + strModuleCode + "') and TBL_SMODULE.Program_Name in ('Transaction','MCC Transaction','Bulk Transaction')  and  TSPL_PROGRAM_MASTER.Program_Code in   ('" + clsUserMgtCode.FrmZoneMasterDS + "','" + clsUserMgtCode.frmRouteMasterDS + "','" + clsUserMgtCode.FrmSchemeMasterDairyDS + "','" + clsUserMgtCode.SaleIncentiveMaster + "','" + clsUserMgtCode.CustomerDeduction + "','" + clsUserMgtCode.frmbookingdairy + "','" + clsUserMgtCode.frmDeliveryOrderDairy + "','" + clsUserMgtCode.frmSaleDispatchDairy + "','" + clsUserMgtCode.frmDemandBooking + "','" + clsUserMgtCode.frmDCSDEmandBooking + "','" + clsUserMgtCode.frmSaleInvoicedairy + "','" + clsUserMgtCode.frmSaleReturndairy + "','" + clsUserMgtCode.frmGatePassDairy + "','" + clsUserMgtCode.frmCrateReceviedDairySale + "','" + clsUserMgtCode.frmBookingDairyMultipleDistributor + "','" + clsUserMgtCode.frmPOSBookingDairyMultipleDistributor + "','" + clsUserMgtCode.frmDairyGatePass + "','" + clsUserMgtCode.frmDairyBookingCustomer + "','" + clsUserMgtCode.frmPerformaInvoiceDairy + "','" + clsUserMgtCode.CustomerIncentiveEntry + "','" + clsUserMgtCode.FrmZoneMaster + "','" + clsUserMgtCode.frmRouteMaster + "','" + clsUserMgtCode.frmSalesLevelHierarchy + "','" + clsUserMgtCode.FrmBookingEntry + "','" + clsUserMgtCode.frmDeliveryNoteFreshSale + "','" + clsUserMgtCode.FrmDispatchFreshSale + "','" + clsUserMgtCode.frmInvoiceFreshSale + "','" + clsUserMgtCode.frmCreateReceived + "','" + clsUserMgtCode.frmSaleReturnFreshSale + "','" + clsUserMgtCode.FrmGatePassFS + "','" + clsUserMgtCode.frmCrateReceviedDairySale + "','" + clsUserMgtCode.FrmInvoiceCrateLinerDetail + "','" + clsUserMgtCode.frmsaleReturnGateEntryFS + "','" + clsUserMgtCode.frmSubsidyCreditNote + "','" + clsUserMgtCode.FrmSchemeMasterDairy + "','" + clsUserMgtCode.FrmTragetMaster + "','" + clsUserMgtCode.DispatchChecklist + "','" + clsUserMgtCode.frmBookingProductSale + "','" + clsUserMgtCode.frmdispatchAdviceProductSale + "','" + clsUserMgtCode.frmDeliveryPrderProductSale + "','" + clsUserMgtCode.frmSalesOrderProductSale + "','" + clsUserMgtCode.frmShipmentProductSale + "','" + clsUserMgtCode.frmSaleInvoiceProductSale + "','" + clsUserMgtCode.frmSaleReturnProductSale + "','" + clsUserMgtCode.FrmProductDispatchGateOut + "','" + clsUserMgtCode.frmGateEntryReturnPS + "','" + clsUserMgtCode.frmsaleReturnGateEntryPS + "','" + clsUserMgtCode.FrmGatePassPS + "','" + clsUserMgtCode.ModuleElectrical + "','" + clsUserMgtCode.frmDGMaster + "','" + clsUserMgtCode.frmDailyElectricalEntry + "','" + clsUserMgtCode.frmJWParameterMaster + "','" + clsUserMgtCode.frmJWFormulaMaster + "','" + clsUserMgtCode.frmJWVendorFormula + "','" + clsUserMgtCode.JWIItemPriceMaster + "','" + clsUserMgtCode.FrmSRNJobWorkEstimate + "','" + clsUserMgtCode.frmJobWorkBillig + "','" + clsUserMgtCode.frmJobWorkoutwordMaster + "','" + clsUserMgtCode.frmJobWorkoutwordMaster + "','" + clsUserMgtCode.frmMilkJobWorkTransfer + "','" + clsUserMgtCode.frmMilkJobWorkTransferReturn + "','" + clsUserMgtCode.frmMilkJobWorkTransferOther + "','" + clsUserMgtCode.frmMilkJobWorkTransferOtherReturn + "','" + clsUserMgtCode.frmGateEntry_JWO + "','" + clsUserMgtCode.frmWeighment_JWO + "','" + clsUserMgtCode.frmQC_JWO + "','" + clsUserMgtCode.frmUnloading_JWO + "','" + clsUserMgtCode.JWO_SRN + "','" + clsUserMgtCode.JWO_SRN_Return + "','" + clsUserMgtCode.frmJobWorkConsumption + "','" + clsUserMgtCode.frmMCCMaster + "','" + clsUserMgtCode.frmMilkRouteMaster + "','" + clsUserMgtCode.frmVillageMaster + "','" + clsUserMgtCode.frmVSPMaster + "','" + clsUserMgtCode.frmVLCMaster + "','" + clsUserMgtCode.frmVLCUploader + "','" + clsUserMgtCode.frmMPMaster + "','" + clsUserMgtCode.frmPrimaryTransporterMaster + "','" + clsUserMgtCode.frmPrimaryTransporterVehicalMaster + "','" + clsUserMgtCode.frmVLCRouteShiftMaster + "','" + clsUserMgtCode.MilkPricePlanning + "','" + clsUserMgtCode.frmPriceChartMaster + "','" + clsUserMgtCode.FrmPriceChartUploader + "','" + clsUserMgtCode.frmPriceChartMaster_Bulk + "','" + clsUserMgtCode.frmMCCMaterialSalePriceChart + "','" + clsUserMgtCode.frmMilkVehicleMaster + "','" + clsUserMgtCode.frmMilkAdvanceMaster + "','" + clsUserMgtCode.frmdeductionGroup + "','" + clsUserMgtCode.frmGroupOfDeduction + "','" + clsUserMgtCode.frmDeductionMaster + "','" + clsUserMgtCode.DeductionMapping + "','" + clsUserMgtCode.CanMaster + "','" + clsUserMgtCode.DockMaster + "','" + clsUserMgtCode.FrmTankerDispatchPriceMaster + "','" + clsUserMgtCode.MilkRejectType + "','" + clsUserMgtCode.frmMilkReasonMaster + "','" + clsUserMgtCode.frmPaymentCycleMaster + "','" + clsUserMgtCode.frmIncentiveMaster + "','" + clsUserMgtCode.frmMCCSMSSettiing + "','" + clsUserMgtCode.frmOpenMCCShiftManual + "','" + clsUserMgtCode.FrmVSPIncentiveTagging + "','" + clsUserMgtCode.frmVLCMasterTarget + "','" + clsUserMgtCode.frmItemChargeCategoryMaster + "','" + clsUserMgtCode.FreightChargesMaster + "' ,'" + clsUserMgtCode.frmMCCMaster + "','" + clsUserMgtCode.frmMCCMaterialSaleReturnFarmer + "','" + clsUserMgtCode.MPBillGeneration + "','" + clsUserMgtCode.frmPaymentProcessFarmer + "','" + clsUserMgtCode.frmFarmerPaymentAdjustment + "','" + clsUserMgtCode.frmLockMPCollectionPC + "','" + clsUserMgtCode.frmSectionAllowanceMaster + "','" + clsUserMgtCode.frmSavingsMaster + "','" + clsUserMgtCode.frmEmployeeSavingsMapping + "','" + clsUserMgtCode.frmIncomeTaxSlab + "','" + clsUserMgtCode.frmHouseRentDeclaration + "','" + clsUserMgtCode.IncomeTaxTDSCalculation + "','" + clsUserMgtCode.frmOpenMCCShift + "','" + clsUserMgtCode.MilkGateEntryIn + "','" + clsUserMgtCode.MilkGateEntryWeightment + "','" + clsUserMgtCode.frmMilkReceipt + "','" + clsUserMgtCode.MilkReject + "','" + clsUserMgtCode.frmMilkSample + "','" + clsUserMgtCode.MilkGateEntryOut + "','" + clsUserMgtCode.frmMilkSRN + "','" + clsUserMgtCode.frmVlcdataUploadar + "','" + clsUserMgtCode.FrmVLCDataUploaderManual + "','" + clsUserMgtCode.frmMilkShiftEndMCC + "','" + clsUserMgtCode.frmMCCDispatch + "','" + clsUserMgtCode.frmMilkPurchaseInvoice + "','" + clsUserMgtCode.frmVSPAssetIssue + "','" + clsUserMgtCode.frmMCCMaterial + "','" + clsUserMgtCode.frmMCCMaterialSaleReturn + "','" + clsUserMgtCode.frmProvisionEntry + "','" + clsUserMgtCode.IncentiveEntry + "','" + clsUserMgtCode.frmPaymentProcess + "','" + clsUserMgtCode.frmMccScrapGatePass + "','" + clsUserMgtCode.MccMilkTransferPrice + "" &
                   "','" + clsUserMgtCode.frmTankerTransporterMaster + "','" + clsUserMgtCode.frmTankerMaster + "','" + clsUserMgtCode.frmParameterMaster + "','" + clsUserMgtCode.ParameterValueMaster + "','" + clsUserMgtCode.frmPriceChartBulkProc + "','" + clsUserMgtCode.FrmContractTanker + "','" + clsUserMgtCode.FrmSupplierMaster + "','" + clsUserMgtCode.frmDivertedContractor + "','" + clsUserMgtCode.frmMilkTypeMast + "','" + clsUserMgtCode.frmMilkGradeMaster + "','" + clsUserMgtCode.FrmBulkRoutMaster + "','" + clsUserMgtCode.TankerCleaningItem + "" &
                   "','" + clsUserMgtCode.frmPOBulkProc + "','" + clsUserMgtCode.frmIntimation + "','" + clsUserMgtCode.frmGateEntry + "','" + clsUserMgtCode.frmWeighment + "','" + clsUserMgtCode.frmQualityCheck + "','" + clsUserMgtCode.frmUnloading + "','" + clsUserMgtCode.frmCleaning + "','" + clsUserMgtCode.frmGateOut + "','" + clsUserMgtCode.frmBulkMilkSRN + "','" + clsUserMgtCode.frmBulkMilkPurchaseInvoice + "','" + clsUserMgtCode.frmMilkTransferIn + "','" + clsUserMgtCode.frmProvisionEntry + "','" + clsUserMgtCode.frmBulkMilkSRNReturn + "','" + clsUserMgtCode.MccMilkTransferPrice + "','" + clsUserMgtCode.FrmMilkPurchaseReturn + "','" + clsUserMgtCode.frmMilkTransferInReturn + "" &
                   "','" + clsUserMgtCode.FrmSectionMaster + "','" + clsUserMgtCode.FrmStageMaster + "','" + clsUserMgtCode.frmPPLogSheetMaster + "','" + clsUserMgtCode.frmProcessProductionLogSheet + "','" + clsUserMgtCode.FrmSectionStageMapping + "','" + clsUserMgtCode.frmBillOfMaterialDairy + "','" + clsUserMgtCode.frmProfitCenter + "','" + clsUserMgtCode.FrmLineMaster + "" &
                   "','" + clsUserMgtCode.frmProductionPlanningDairy + "','" + clsUserMgtCode.frmBatchOrderDairy + "','" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmProductionEntryFinalQC + "','" + clsUserMgtCode.frmWreckageBooking + "','" + clsUserMgtCode.frmAssembDis + "','" + clsUserMgtCode.JobWorkDispatchProduction + "','" + clsUserMgtCode.frmProcessProdReturn + "','" + clsUserMgtCode.frmSiloMilkTransfer + "','" &
                   "','" + clsUserMgtCode.FrmItemMasterRMOther + "','" + clsUserMgtCode.itemStructure + "','" + clsUserMgtCode.itemPurchaseAccount + "','" + clsUserMgtCode.itemSaleAccount + "','" + clsUserMgtCode.unitMaster + "','" + clsUserMgtCode.chapterhead + "','" + clsUserMgtCode.packType + "','" + clsUserMgtCode.PriceComponentMasters + "','" + clsUserMgtCode.PriceComponentMapping + "','" + clsUserMgtCode.PricePlan + "','" + clsUserMgtCode.PriceMaster + "','" + clsUserMgtCode.mbtnBreakageHead1 + "','" + clsUserMgtCode.locationMaster + "','" + clsUserMgtCode.frmStandardscheme + "','" + clsUserMgtCode.frmStandardRateItem + "','" + clsUserMgtCode.frmItemCategoryLevel + "" &
                   "','" + clsUserMgtCode.frmItemCategoryStructure + "','" + clsUserMgtCode.WarrantyMaster + "','" + clsUserMgtCode.frmSchemeMasterNew + "','" + clsUserMgtCode.frmPriceGroupMapping + "','" + clsUserMgtCode.frmLocationCategoryLevel + "','" + clsUserMgtCode.frmLocationCategoryStructure + "','" + clsUserMgtCode.FrmCatalogMaster + "','" + clsUserMgtCode.frmPartNoMaster + "','" + clsUserMgtCode.InvetorySourceCode + "','" + clsUserMgtCode.FrmItemTypeMaster + "','" + clsUserMgtCode.frmHSNMaster + "','" + clsUserMgtCode.frmOverheadCostMaster + "','" + clsUserMgtCode.FrmOverheadCostGroup + "','" + clsUserMgtCode.FrmItemCostMapping + "','" + clsUserMgtCode.frmWeightUomMaster + "" &
                   "','" + clsUserMgtCode.Indent + "','" + clsUserMgtCode.Transfer + "','" + clsUserMgtCode.mbtnEmptyTrans + "','" + clsUserMgtCode.mbtnStoreAdjustment + "','" + clsUserMgtCode.frmWarehouseBreakage + "','" + clsUserMgtCode.frmPhysicalStock + "','" + clsUserMgtCode.ChangeItemSerialNumber + "','" + clsUserMgtCode.TransferReturn + "','" + clsUserMgtCode.FrmTransferGateOut + "','" + clsUserMgtCode.frmGateEntryReturnTransfer + "','" + clsUserMgtCode.FrmJobWorkInventory + "','" + clsUserMgtCode.frmRawMilkConsumtion + "','" + clsUserMgtCode.frmGeneralWeighment + "" &
                    "','" + clsUserMgtCode.mbtnPurchaseRequistion + "','" + clsUserMgtCode.RFQ + "','" + clsUserMgtCode.VendorQuotation + "','" + clsUserMgtCode.mbtnPurchaseOrder + "','" + clsUserMgtCode.mbtnGRN + "','" + clsUserMgtCode.POWeighment + "','" + clsUserMgtCode.mbtnMRN + "','" + clsUserMgtCode.mbtnSRN + "','" + clsUserMgtCode.mbtnPurchaseInvoice + "','" + clsUserMgtCode.mbtnNRGP + "','" + clsUserMgtCode.mbtnPurchaseReturn + "','" + clsUserMgtCode.mbtnGatePass + "','" + clsUserMgtCode.mbtnIssueReturn + "','" + clsUserMgtCode.frmMaterialQuotation + "','" + clsUserMgtCode.frmMaterialQuotationOrder + "','" + clsUserMgtCode.frmMaterialQuotationComparison + "','" + clsUserMgtCode.ScrapSale + "','" + clsUserMgtCode.frmStoreRequistion + "','" + clsUserMgtCode.frmPurchaseSchedule + "','" + clsUserMgtCode.FrmScrapSaleGateOut + "','" + clsUserMgtCode.ScrapSaleRetrun + "','" + clsUserMgtCode.frmsaleReturnGateEntryMISSAle + "" &
                     "','" + clsUserMgtCode.PaymentEntryNew + "','" + clsUserMgtCode.mbtnAPInvoiceEntry + "','" + clsUserMgtCode.PaymentAdjustmentEntry + "','" + clsUserMgtCode.FrmSupplierReg + "','" + clsUserMgtCode.VendorRegistration + "" &
                      "','" + clsUserMgtCode.reverseTransaction + "','" + clsUserMgtCode.bankTransfer + "','" + clsUserMgtCode.FrmBankReco + "','" + clsUserMgtCode.FrmCFormEntry + "','" + clsUserMgtCode.BankOpeningReco + "" &
                       "','" + clsUserMgtCode.ReceiptEntry + "','" + clsUserMgtCode.ReceiptAdjustmentEntry + "','" + clsUserMgtCode.mbtnARInvoiceEntry + "','" + clsUserMgtCode.FrmCustomersOutstanding + "" &
                "','" + clsUserMgtCode.journalEntry + "','" + clsUserMgtCode.mbtnVCGLEntry + "" &
                "','" + clsUserMgtCode.FrmSalesOrderBS + "','" + clsUserMgtCode.FrmGateEntrySale + "','" + clsUserMgtCode.FrmWeighmentEntry + "','" + clsUserMgtCode.FrmLoadingTanker + "','" + clsUserMgtCode.FrmQualityCheckBulkSale + "','" + clsUserMgtCode.FrmLoadingTanker + "','" + clsUserMgtCode.FrmDispatchBulkSale + "','" + clsUserMgtCode.FrmInvoiceBulkSale + "','" + clsUserMgtCode.FrmCreateAutoInvoiceBS + "','" + clsUserMgtCode.FrmBulkDispatchReturnSale + "','" + clsUserMgtCode.FrmBulkSaleReturn + "','" + clsUserMgtCode.FrmCanSale + "','" + clsUserMgtCode.FrmCanReceived + "','" + clsUserMgtCode.frmMCCMaterialFarmer + "','" &
                 "','" + clsUserMgtCode.EmployeeBandMaster + "','" + clsUserMgtCode.FrmAllotmentOfLeaves + "','" + clsUserMgtCode.FrmEmpIncrement + "','" + clsUserMgtCode.FrmEmployeeTransfer + "','" + clsUserMgtCode.frmConveyanceClaim + "','" + clsUserMgtCode.frmODSheet + "','" + clsUserMgtCode.frmEmployeeShiftChange + "','" + clsUserMgtCode.frmFullAndFinalSettlement + "','" + clsUserMgtCode.frmMediclaimEntry + "','" + clsUserMgtCode.frmLTAClaim + "','" + clsUserMgtCode.frmEmployeeGratuity + "','" + clsUserMgtCode.frmSalaryGeneration + "','" + clsUserMgtCode.frmAdjustmentVoucher + "','" + clsUserMgtCode.frmOTSheet + "','" + clsUserMgtCode.frmMonthlyAttendance + "','" + clsUserMgtCode.frmHourlyAttendance + "','" + clsUserMgtCode.frmDailyAttendance + "','" + clsUserMgtCode.frmLoanAdjustment + "','" + clsUserMgtCode.frmLoanGeneration + "','" + clsUserMgtCode.frmGenerateBonus + "','" + clsUserMgtCode.frmApplyLoan + "','" + clsUserMgtCode.frmReimbursementDetails + "','" + clsUserMgtCode.frmDeductionDetails + "','" + clsUserMgtCode.frmAllowanceDetails + "','" + clsUserMgtCode.frmLeaveAdjustment + "','" + clsUserMgtCode.frmLeaveApplication + "','" + clsUserMgtCode.frmWeeklyHolidays + "','" + clsUserMgtCode.frmGeneralHolidays + "','" + clsUserMgtCode.frmEmpSalary + "','" + clsUserMgtCode.frmSalaryStructure + "','" + clsUserMgtCode.frmPayHeadDefinitions + "','" + clsUserMgtCode.frmEmployee_Master + "','" + clsUserMgtCode.frmHourlyAttendance + "" &
            "','" + clsUserMgtCode.mbtnAPInvoiceEntryTDS + "','" + clsUserMgtCode.TDSPAYMENT + "" &
            "','" + clsUserMgtCode.FAAcquisitionEntry + "','" + clsUserMgtCode.FAAssetDepreciation + "','" + clsUserMgtCode.FADisposalEntry + "','" + clsUserMgtCode.frmAsset_Issue_Return + "','" + clsUserMgtCode.frmAssetStoreRequistion + "','" + clsUserMgtCode.frmIssueItemsToAsset + "','" + clsUserMgtCode.FAAssetWork + "','" + clsUserMgtCode.FAMergeAcquisitionEntry + "" &
            "','" + clsUserMgtCode.frmEXSalesQuotation + "','" + clsUserMgtCode.frmEXSalesOrder + "','" + clsUserMgtCode.frmEXPorformaInvoice + "','" + clsUserMgtCode.frmEXCommercialInvoice + "','" + clsUserMgtCode.frmEXSalesInvoice + "','" + clsUserMgtCode.frmEXSalesReturn + "','" + clsUserMgtCode.frmsaleReturnGateEntryExportSAle + "" &
            "','" + clsUserMgtCode.DariyProductionUploader + "')  "
        End If

        'strQuery = "select distinct Bill_To_Location as Code,Location_Desc as Description from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code"
        fndScreen.Value = clsCommon.ShowSelectForm("LocationSegGP", qry, "Code", wher, fndScreen.Value, "TSPL_PROGRAM_MASTER.sno", isButtonClicked)
        lblScreen.Text = clsDBFuncationality.getSingleValue("select  case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name  from TSPL_PROGRAM_MASTER where Program_Code='" & fndScreen.Value & "'")
        If clsCommon.myLen(fndScreen.Value) > 0 Then
            RadGroupBox2.Enabled = False
        Else
            RadGroupBox2.Enabled = True
        End If
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column Is gv1.Columns("Document Code") Then
                'If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmZoneMasterDS) = CompairStringResult.Equal Then
                DrillDown()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub DrillDown()
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                'If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRN") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(fndScreen.Value, clsCommon.myCstr(gv1.CurrentRow.Cells("Document Code").Value))
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
