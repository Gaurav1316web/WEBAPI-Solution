'--12/06/2012---Updation by--[Pankaj kumar]-Udation In Print Qry in Case of LoadOut Sku_Seq is Added in Order By--------According to--Balwinder Sigh
'--31/12/2012-10:57AM--Updation by--[Pankaj kumar]-Added Two new DataFields(Address Of To_Location, Tin No) in Report(rptEmptyTransferLoadoutCustom)--------According to--Varun
Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ReportTransfer
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        PrintData()
    End Sub
    Sub PrintData()
        PrintData(txtTransferNo.Value)

    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub


    Private Sub txtTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTransferNo._MYValidating
        Dim qry As String = "select Transfer_No ,Transfer_Date,Transfer_Type,Item_Type,Vehicle_No,Route_No,Price_Code,From_Location as [From Location],(select location_desc from TSPL_LOCATION_MASTER where Location_Code =TSPL_TRANSFER_HEAD .From_Location  )as [From Location Desc],To_Location  as [To Location],(select location_desc from TSPL_LOCATION_MASTER where Location_Code =TSPL_TRANSFER_HEAD .To_Location  )as [To Location Desc] from TSPL_TRANSFER_HEAD"
        txtTransferNo.Value = clsCommon.ShowSelectForm("TranferReport", qry, "Transfer_No", "", txtTransferNo.Value, "Transfer_Date desc", isButtonClicked)
    End Sub

    Public Function PrintData(ByVal strNo As String) As Boolean
        Return PrintData(strNo, True)
    End Function
    Public Function PrintData(ByVal strNo As String, ByVal isPrePrinted As Boolean) As Boolean
        Return PrintData(strNo, isPrePrinted, False)
    End Function
    Public Function PrintData(ByVal strNo As String, ByVal isPrePrinted As Boolean, ByVal isPrePrintTAx As Boolean) As Boolean
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.myLen(strNo) > 0 Then
                Dim str As String = "'' as totalqty"
                Dim qry As String = "select  Uom as Unit,sum(case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then TSPL_TRANSFER_DETAIL.Item_Qty else TSPL_TRANSFER_DETAIL.LoadIn_Qty end) as Qty from TSPL_TRANSFER_DETAIL left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No where TSPL_TRANSFER_HEAD.Transfer_No = '" + strNo + "' group by TSPL_TRANSFER_DETAIL.Uom"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = "'"
                    For Each dr As DataRow In dt.Rows
                        str = str + clsCommon.myCstr(dr("Unit")) + " - " + clsCommon.myCstr(dr("Qty")) + " "
                    Next
                    str = str + "'" + " as totalqty"
                End If

                dt = New DataTable()
                qry = "select Item_Type,Transfer_Type,(case when (TSPL_LOCATION_MASTER.Excisable='T' and TSPL_TRANSFER_HEAD.Transfer_Type='LO') then 1 else 0 end) as isExcisableLoadout,(case when TSPL_LOCATION_MASTER.Location_Type='Physical' and ToLocatioin.Location_Type='Physical' then 1 else 0 end ) BothLocationPhysical from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as ToLocatioin on ToLocatioin.Location_Code=TSPL_TRANSFER_HEAD.To_Location  where Transfer_No='" + strNo + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim isBothLocationPhysical As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("BothLocationPhysical")) = 1, True, False)
                    Dim strItemType As String = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                    Dim strTrasactionType As String = clsCommon.myCstr(dt.Rows(0)("Transfer_Type"))
                    If clsCommon.CompairString(strItemType, "Empty") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strTrasactionType, "LO") = CompairStringResult.Equal Then
                            qry = "select TSPL_TRANSFER_HEAD.Created_By,TSPL_TRANSFER_HEAD.Modify_By,GPCode,TSPL_COMPANY_MASTER.Tin_No as TinNo, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, " & _
                            " TSPL_COMPANY_MASTER.Comp_Name,TSPL_TRANSFER_HEAD.Transfer_No,TSPL_TRANSFER_HEAD.Transfer_Date,TSPL_LOCATION_MASTER.Location_Desc as To_LocationName, " & _
                            "(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End " & _
                            " + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + " & _
                            " Case When TSPL_LOCATION_MASTER.Add4='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add4 end ) as To_LocAddress, " & _
                            "TSPL_LOCATION_MASTER.TIN_No  ,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc , " & _
                            "case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then TSPL_TRANSFER_DETAIL.Item_Qty else " & _
                            "TSPL_TRANSFER_DETAIL. LoadIn_Qty end as Qty,TSPL_TRANSFER_DETAIL.BasicPrice_WithTax, " & _
                            "TSPL_TRANSFER_DETAIL.Item_Price,TSPL_TRANSFER_DETAIL.Amount,TSPL_TRANSFER_HEAD.Vehicle_No, " & _
                            "case when Uom='FC' then Item_Qty end as FCS, case when Uom='FB' then Item_Qty end as FBS, " & _
                            "case when Uom='SH' then Item_Qty end as FSH, case when Uom='EC' then Item_Qty end as ECS, " & _
                            "case when Uom='EB' then isnull(Item_Qty,0) else 0 end as EBS,Leak,Burst,0 as Shortage,0 as NSBT, " & _
                            "0 as HfilledBT,0 as burstBT,0 as Expdt,0 as UnloadBKG, 0 as TRLkg,0 as TRBkg,0 as rust,0 as dirty,0 as MRP,0 as ChipBT,   " & _
                            " TSPL_LOCATION_MASTER.Location_Desc as locDesc, TSPL_LOCATION_MASTER.Pin_Code  as locPin, TSPL_LOCATION_MASTER.TIN_No as LocTinNo, TSPL_LOCATION_MASTER.CST_No as locCSTNo, TSPL_LOCATION_MASTER.Phone1 +'  '+ TSPL_LOCATION_MASTER.Phone2+'  '+TSPL_LOCATION_MASTER.Telphone as locPhone, TSPL_TRANSFER_HEAD.Item_Type" & _
                            " from TSPL_TRANSFER_DETAIL" & _
                            " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No " & _
                            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.From_Location LEFT OUTER JOIN  " & _
                            " TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code left outer join " & _
                            " TSPL_GATEPASS_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_GATEPASS_DETAIL.DocNo  " & _
                            " where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "'" & _
                            " ORDER by TSPL_TRANSFER_DETAIL.Line_No"
                            dt = clsDBFuncationality.GetDataTable(qry)
                            'InventryViewer.funreport(dt, ERP.EnumTecxpertPaperSize.PaperSize10x12, "rptEmptyTransferLoadOutCustomGun", "Empty Loadout Report")

                            If isPrePrinted Then
                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "rptEmptyTransferLoadOutCustomGun", "Empty LoadOut Report")
                            Else
                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "rptEmptyTransferLoadoutCustomVizag", "Empty Loadin Report")
                            End If


                        Else
                            qry = "select  Created_By,Modify_By,max(isnull(RGB,0)) as RGB ,SUM(ConvQty) as ConvQty,max(GPCode) as GPCode,max(Tin_No) as  Tin_No,max(CST_LST) as CST_LST,max(Ecc_No) as Ecc_No,max(Comp_Name) as Comp_Name,max(CompAddress) as CompAddress,max(GPCode) as GPCode,max(Tin_No) as  Tin_No,max(CST_LST) as CST_LST,max(Ecc_No) as Ecc_No,max(Comp_Name) as Comp_Name,max(CompAddress) as CompAddress, " & _
                            "Transfer_No,max(Transfer_Date) as Transfer_Date,max(From_LocationName)as From_LocationName, MAX(FromLocAddress) as FromLocAddress, MAX(TinNo ) as TinNo, max(Vehicle_No) as Vehicle_No,Item_Code,max(Item_Desc) as Item_Desc,sum(isnull(FCS,0)) as FCS,sum(isnull(FBS,0)) as FBS,sum(isnull(FSH,0)) as FSH,sum(isnull(ECS,0)) as ECS,sum(isnull(EBS,0)) as EBS,sum(Leak) as Leak, " & _
                            "sum(Burst) as Burst,sum(Shortage) as Shortage,0 as NSBT,0 as HfilledBT,0 as burstBT,0 as Expdt,0 as UnloadBKG, 0 as TRLkg,0 as TRBkg,0 as rust,0 as dirty,0 as MRP,0 as ChipBT ,sum(Breakage) as Breakage, " & _
                            " MAX(locDesc) as locDesc, MAX(locPin) as locPin, MAX(LocTinNo) as LocTinNo, MAX(locCSTNo) as locCSTNo, MAX(locPhone) as locPhone, MAX(Item_Type) as ItemType from(" & _
                            " select  Created_By,Modify_By, RGB, ConvQty,GPCode,Tin_No,CST_LST, Ecc_No, Comp_Name,CompAddress,Transfer_No,Transfer_Date,From_LocationName, FromLocAddress, TinNo  ,Vehicle_No,Item_Code,Item_Desc,case when Unit_code='FC' then Qty end as FCS, case when Unit_code='FB' then Qty end as FBS, case when Unit_code='SH' then Qty end as FSH, case when Unit_code='EC' then Qty end as ECS, " & _
                            "case when Unit_code='EB' then Qty end as EBS,Leak,Burst,Shortage,Breakage, locDesc, locPin, LocTinNo, locCSTNo, locPhone , Item_Type " & _
                            " from(select (select sum (isnull(Item_Qty,0)) from(select case when uom ='EC' then Item_Qty else 0 end as Item_Qty from TSPL_TRANSFER_DETAIL where transfer_no=(  select Load_Out_No  from TSPL_TRANSFER_HEAD where Transfer_No ='" + strNo + "'))as RGB) as RGB,(case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then Item_Qty/Conversion_Factor else LoadIn_Qty/Conversion_Factor end) as ConvQty,GPCode,TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, " & _
                            " TSPL_COMPANY_MASTER.Comp_Name,  (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then " & _
                            " '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then " & _
                            " '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then " & _
                            " '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress,TSPL_TRANSFER_HEAD.Transfer_No, " & _
                            " replace(CONVERT(varchar(11),TSPL_TRANSFER_HEAD.Transfer_Date,104),'.','/') as Transfer_Date,(( select FromLoc_Desc   from TSPL_TRANSFER_HEAD  where Transfer_No =(" & _
                            "  select Load_Out_No  from TSPL_TRANSFER_HEAD where Transfer_No ='" + strNo + "'))) as  From_LocationName, (TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end ) as  FromLocAddress, (( select TIN_No from TSPL_LOCATION_MASTER  where Location_Code  =(  select To_Location  from TSPL_TRANSFER_HEAD  where Transfer_No =(  select Load_Out_No  from TSPL_TRANSFER_HEAD where Transfer_No ='ELWR/12/00007')))) as  TinNo " & _
                            " , TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,(case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then Item_Qty else LoadIn_Qty end) as Qty,Uom as Unit_code,TSPL_TRANSFER_DETAIL.Leak,TSPL_TRANSFER_DETAIL.Burst,TSPL_TRANSFER_DETAIL.Shortage,TSPL_TRANSFER_DETAIL.Breakage,TSPL_TRANSFER_HEAD.Created_By,TSPL_TRANSFER_HEAD.Modify_By, " & _
                            " TSPL_LOCATION_MASTER.Location_Desc as locDesc, TSPL_LOCATION_MASTER.Pin_Code  as locPin, TSPL_LOCATION_MASTER.TIN_No as LocTinNo, TSPL_LOCATION_MASTER.CST_No as locCSTNo, TSPL_LOCATION_MASTER.Phone1 +'  '+ TSPL_LOCATION_MASTER.Phone2+'  '+TSPL_LOCATION_MASTER.Telphone as locPhone, TSPL_TRANSFER_HEAD.Item_Type" & _
                            " from TSPL_TRANSFER_DETAIL " & _
                            " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No" & _
                            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.To_Location LEFT OUTER JOIN  " & _
                            " TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code left outer join " & _
                            " TSPL_GATEPASS_DETAIL on TSPL_TRANSFER_HEAD.Load_Out_No=TSPL_GATEPASS_DETAIL.DocNo  " & _
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
                            " where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "') as Final" & _
                            " )SuperFinal group by Transfer_No,Item_Code,Created_By,Modify_By"
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If isPrePrinted Then
                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "rptEmptyTransferLoadInCustomGun", "Empty Loadin Report")
                            Else
                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "rptEmptyTransferLoadInCustomVizag", "Empty Loadin Report")
                            End If

                        End If
                    Else
                        If clsCommon.myCdbl(dt.Rows(0)("isExcisableLoadout")) = 1 Then
                            qry = "select  sum(Empty_Value * (case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then TSPL_TRANSFER_DETAIL.Item_Qty else TSPL_TRANSFER_DETAIL.LoadIn_Qty end)) as EmptyValue from TSPL_TRANSFER_DETAIL left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No= TSPL_TRANSFER_DETAIL.Transfer_No where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "'"
                            Dim strEmptyValue As String = clsCommon.myCstr(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)))

                            Dim strAddress = "TSPL_LOCATION_MASTER.Add1+(case when LEN(TSPL_LOCATION_MASTER.Add2)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add2+(case when LEN(TSPL_LOCATION_MASTER.Add3)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add3+(case when LEN(TSPL_LOCATION_MASTER.Add4)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add4+(case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.City_Code+(case when LEN(TSPL_LOCATION_MASTER.State)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.State+(case when LEN(TSPL_LOCATION_MASTER.Country)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Country"

                            qry = "select (select Transporter_name from TSPL_Transport_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_Id =TSPL_Transport_MASTER.Transport_Id where TSPL_VEHICLE_MASTER.Vehicle_Id = (select Vehicle_Code from TSPL_TRANSFER_HEAD where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "')  ) as TPTName, TSPL_TRANSFER_HEAD.Reference_Doc_No, TSPL_TRANSFER_HEAD.Mode_Of_Transport,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_TRANSFER_HEAD.Km_Reading ,TSPL_CHAPTER_HEAD.Description as ChapterName,(" + strAddress + ") as Address,TSPL_TRANSFER_HEAD.Transfer_No as Sale_Invoice_No,replace(CONVERT(varchar(11), TSPL_TRANSFER_HEAD.Transfer_Date,104),'.','/') as  Sale_Invoice_Date,(Select Location_Desc from TSPL_LOCATION_MASTER WHere GIT_Location=TSPL_TRANSFER_HEAD.To_Location) as Cust_Name, TSPL_TRANSFER_HEAD.TAX2 ,TSPL_TRANSFER_HEAD.TAX2_Rate,TSPL_TRANSFER_HEAD.TAX2_Amt," & _
                            " TSPL_TRANSFER_HEAD.TAX3 ,TSPL_TRANSFER_HEAD.TAX3_Rate,TSPL_TRANSFER_HEAD.TAX3_Amt," & _
                            " TSPL_TRANSFER_HEAD.TAX4 ,TSPL_TRANSFER_HEAD.TAX4_Rate,TSPL_TRANSFER_HEAD.TAX4_Amt," & _
                            " TSPL_TRANSFER_HEAD.TAX5 ,TSPL_TRANSFER_HEAD.TAX5_Rate,TSPL_TRANSFER_HEAD.TAX5_Amt," & _
                            " TSPL_TRANSFER_DETAIL.Item_Qty as Invoice_Qty,(TSPL_TRANSFER_DETAIL.Item_Desc +' ('+TSPL_TRANSFER_DETAIL.Uom +')') as Item_Desc,TSPL_TRANSFER_DETAIL.MRP as MRP_Amt,TSPL_TRANSFER_DETAIL. Basic_Price as Basic_Rate, TSPL_TRANSFER_DETAIL.TAX1_Rate AS DTax1Rate,TSPL_TRANSFER_DETAIL.Tax1_Assessable_Amt as Total_Assessable_Amt,(TSPL_TRANSFER_DETAIL.Item_Qty *TSPL_TRANSFER_DETAIL.Basic_Price) as Total_Basic_Amt," + strEmptyValue + " as Empty_Value,0 as ttlTPT,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName,CONVERT(varchar(100),TSPL_TRANSFER_HEAD.Date_Time_Removal,108) as RemovelTime,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_TRANSFER_DETAIL.Item_Code and  (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when TSPL_TRANSFER_DETAIL.Uom='FB' then 'FC' else case when TSPL_TRANSFER_DETAIL.Uom='FC' then 'FB' end end) )  AS Conversion," + str + ",'' as FOCItem, 0 as Inv_Discount_Amt" & _
                            ",0 as TAX1_Rate1,0 as TAX1_Rate2,0 as TAX1_Rate3,0 as TAX1_Amt1,0 as TAX1_Amt2,0 as TAX1_Amt3," & _
                            " 0 as TAX2_Rate1,0 as TAX2_Rate2,0 as TAX2_Rate3,0 as TAX2_Amt1,0 as TAX2_Amt2,0 as TAX2_Amt3," & _
                            "  0 as TAX3_Rate1,0 as TAX3_Rate2,0 as TAX3_Rate3,0 as TAX3_Amt1,0 as TAX3_Amt2,0 as TAX3_Amt3," & _
                            " 0 as TAX4_Rate1,0 as TAX4_Rate2,0 as TAX4_Rate3,0 as TAX4_Amt1,0 as TAX4_Amt2,0 as TAX4_Amt3," & _
                            " 0 as TAX5_Rate1,0 as TAX5_Rate2,0 as TAX5_Rate3,0 as TAX5_Amt1,0 as TAX5_Amt2,0 as TAX5_Amt3 , (case when Is_AgainstFormF='0' then '' else 'Against F Form' end ) as Is_AgainstFormF,'' as Tin_No  " & _
                            " , TSPL_COMPANY_MASTER.Comp_Name, Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add1 End End End as CompAddress, TSPL_COMPANY_MASTER.Tin_No as CompTinNo, TSPL_TRANSFER_DETAIL.Assessable_Amt as Item_Assessable_Rate ,TSPL_TRANSFER_DETAIL.MRP_In_Bottle,TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Route_Desc, TSPL_TRANSFER_HEAD.Created_By,TSPL_TRANSFER_HEAD.Modify_By " & _
                            " from TSPL_TRANSFER_DETAIL" & _
                            " left outer join TSPL_TRANSFER_HEAD on  TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No" & _
                            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code" & _
                            " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads" & _
                            " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_TRANSFER_HEAD.Salesmancode" & _
                            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.To_Location" & _
                            " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_TRANSFER_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code " & _
                            " where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "'"
                            If isPrePrinted Then
                                dt = clsDBFuncationality.GetDataTable(qry)
                                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "crptCustomSaleInvoiceWithExciseForGuntur", "Excisable Loadout Transfer", True)
                            Else
                                dt = clsDBFuncationality.GetDataTable(qry)
                                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, EnumTecxpertPaperSize.NA, "ExcisableTransferReport", "Excisable Loadout Transfer", True)
                            End If
                        Else
                            ' ''qry = "select bb.Item_Code,MAX(item) as item, max(transfer_number)as  transfer_number,max(date)as date,max(salesman_name)as salesman_name,max(route_number)as route_number,max(tn_number)as tn_number,max(trip_number)as trip_number ,sum(leak)as leak,sum(brust)as brust,sum(short)as short, (mrp) as mrp, max(Vehicle_No)as Vehicle_No,sum(itemqty) as itemqty,SUM(isnull(FCS,0)) as FCS,SUM(isnull(FBS,0)) as FBS,SUM(isnull(FSH,0)) as FSH,SUM(isnull(ECS,0)) as ECS,SUM(isnull(EBS,0)) as EBS,SUM(isnull(ESH,0)) as ESH,max(description) as description ,max(Reference) as Reference, MAX(Total_Transfer_Amount) as Total_Transfer_Amount ,Max(TSPL_ITEM_MASTER.Sku_Seq) as Sku_Seq,max(RemovalTime) as RemovalTime ,max(LoadoutFromLocation) as  LoadoutFromLocation ,max(Conversion_Factor) as [COnversion Factor] from(" & _
                            ' ''" select Item_Code,transfer_number,time,date,salesman_name,route_number,tn_number,trip_number," & _
                            ' ''" case when Uom='FC' then qty  end as FCS,  case when Uom='FB' then qty end as FBS,  case when Uom='SH' then qty end as FSH,  case when Uom='EC' then qty  end as ECS,  case when Uom='EB' then qty end as EBS, case when Uom='SH' then qty end as ESH, " & _
                            ' ''" item,Qty,leak,brust,short,mrp,Vehicle_No ,Qty as itemqty,description ,Reference,Total_Transfer_Amount,RemovalTime,LoadoutFromLocation,Conversion_Factor from " & _
                            ' ''" ( select TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL .Uom ,TSPL_TRANSFER_HEAD.Transfer_No AS transfer_number,TSPL_TRANSFER_HEAD.Date_Time_Removal as time ,TSPL_TRANSFER_HEAD.Transfer_Date as date,TSPL_EMPLOYEE_MASTER.Emp_Name as salesman_name ,TSPL_TRANSFER_HEAD.Route_No  as route_number,TSPL_LOCATION_MASTER.TIN_No  as tn_number , TSPL_TRANSFER_HEAD. Trip_No  as trip_number,TSPL_TRANSFER_DETAIL .Item_Desc as item, " & _
                            ' ''" ( case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty end )as Qty," & _
                            ' ''" TSPL_TRANSFER_DETAIL.Leak*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as leak  ,  " & _
                            ' ''"TSPL_TRANSFER_DETAIL.Burst*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as brust ,TSPL_TRANSFER_DETAIL.Shortage*TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as short ,TSPL_TRANSFER_DETAIL.MRP/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_ITEM_UOM_DETAIL.Conversion_Factor ,TSPL_TRANSFER_HEAD.description ,TSPL_TRANSFER_HEAD.Reference,TSPL_TRANSFER_HEAD.Total_Transfer_Amount,ltrim(right(convert(varchar(25), Date_Time_Removal, 100), 7)) as RemovalTime,(select  FromLoc_Desc from TSPL_TRANSFER_HEAD as LOTable where LOTable.Transfer_No= TSPL_TRANSFER_HEAD.Load_Out_No ) as  LoadoutFromLocation " & _
                            ' ''"from TSPL_TRANSFER_DETAIL left outer join  TSPL_TRANSFER_HEAD   on TSPL_TRANSFER_DETAIL.Transfer_No  = TSPL_TRANSFER_HEAD.Transfer_No " & _
                            ' ''"left outer join TSPL_EMPLOYEE_MASTER on TSPL_TRANSFER_HEAD.To_Location =TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                            ' ''"left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location =TSPL_LOCATION_MASTER.Location_code " & _
                            ' ''"left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when  TSPL_TRANSFER_DETAIL.Uom='FB' then 'FC' else case when TSPL_TRANSFER_DETAIL.Uom='FC' then 'FB' end end) " & _
                            ' ''" where TSPL_TRANSFER_HEAD.Transfer_No ='" + strNo + "' and  (case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty+TSPL_TRANSFER_DETAIL.Leak +TSPL_TRANSFER_DETAIL.Burst +TSPL_TRANSFER_DETAIL.Shortage  end )>0  ) xxx ) bb Left Outer Join TSPL_ITEM_MASTER on bb.Item_Code  =TSPL_ITEM_MASTER.Item_Code group by bb.Item_Code,bb.mrp Order by Sku_Seq "
                            ' ''dt = clsDBFuncationality.GetDataTable(qry)



                            '------------------For new format-----------------------


                            qry = "select (select Transporter_name from TSPL_Transport_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_Id =TSPL_Transport_MASTER.Transport_Id where TSPL_VEHICLE_MASTER.Vehicle_Id = (select Vehicle_Code from TSPL_TRANSFER_HEAD where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "')  ) as TPTName,MAX(bb.Created_By) as Created_By,max(bb.Modify_By) as Modify_By,  MAX(GPNO) as GPNo,max(total_basic_amt) as total_basic_amt,sum(basic_amt) as basic_amt,bb.Item_Code,MAX(item) as item, max(transfer_number)as  transfer_number,MAX(Load_Out_No)as Load_Out_No ,max(CONVERT(VARCHAR, date,103)) as date,max(salesman_name)as salesman_name,max(route_number)as route_number,max(route_desc)as route_desc,max(tn_number)as tn_number,max(trip_number)as trip_number ,sum(leak)as leak,sum(brust)as brust,sum(short)as short, (mrp) as mrp, max(Vehicle_No)as Vehicle_No,sum(itemqty) as itemqty,SUM(isnull(FCS,0)) as FCS,SUM(isnull(FBS,0)) as FBS,SUM(isnull(FSH,0)) as FSH,SUM(isnull(ECS,0)) as ECS,SUM(isnull(EBS,0)) as EBS,SUM(isnull(ESH,0)) as ESH,max(description) as description ,max(Reference) as Reference, MAX(Total_Transfer_Amount) as Total_Transfer_Amount ,Max(TSPL_ITEM_MASTER.Sku_Seq) as Sku_Seq,max(RemovalTime) as RemovalTime ,max(LoadoutFromLocation) as  LoadoutFromLocation ,max(Conversion_Factor) as [COnversion Factor],MAX(From_Location) as From_Location,max(To_Location) as To_Location ,MAX(Location_Desc)as Location_Desc,max(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,SUM(Basic_Price) as basic_Price,SUM(BasicPrice_WithTax) as BasicPrice_WithTax,max(Reference_Doc_No) as Reference_Doc_No,max(Emp_Name) as emp_name, (select Reference_Doc_No from TSPL_TRANSFER_HEAD where TSPL_TRANSFER_HEAD.Transfer_No = MAX( bb.Load_Out_No) ) as LORefernceNO from(" & _
                                    " select Created_By,Modify_By,Emp_Name, GPNO,total_basic_amt,basic_amt,Item_Code,transfer_number,time,date,salesman_name,route_number,Route_Desc,tn_number,trip_number," & _
                           " case when Uom='FC' then qty  end as FCS,  case when Uom='FB' then qty end as FBS,  case when Uom='SH' then qty end as FSH,  case when Uom='EC' then qty  end as ECS,  case when Uom='EB' then qty end as EBS, case when Uom='SH' then qty end as ESH, " & _
                           " item,Qty,leak,brust,short,mrp,Vehicle_No ,Qty as itemqty,description ,Reference,Total_Transfer_Amount,RemovalTime,LoadoutFromLocation,Conversion_Factor ,Basic_Price,BasicPrice_WithTax,From_Location,Location_Desc,To_Location,Add1,Add2,Add3,Reference_Doc_No,Load_Out_No from " & _
                           " ( select  TSPL_TRANSFER_HEAD.Created_By,TSPL_TRANSFER_HEAD.Modify_By,TSPL_TRANSFER_HEAD.Load_Out_No," & _
   "  (select top 1 Emp_Name  from TSPL_EMPLOYEE_MASTER where TSPL_TRANSFER_HEAD.Salesmancode =TSPL_EMPLOYEE_MASTER.EMP_CODE  ) as Emp_Name,  (select top 1 GPCode  from TSPL_GATEPASS_DETAIL where TSPL_GATEPASS_DETAIL.DocNo=TSPL_TRANSFER_HEAD.Transfer_No) as GPNO , " & _
                                        " total_basic_amt,basic_amt,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL .Uom ,TSPL_TRANSFER_HEAD.Transfer_No AS transfer_number,TSPL_TRANSFER_HEAD.Date_Time_Removal as time ,TSPL_TRANSFER_HEAD.Transfer_Date as date,TSPL_EMPLOYEE_MASTER.Emp_Name as salesman_name ,TSPL_TRANSFER_HEAD.Route_No  as route_number,TSPL_TRANSFER_HEAD.Route_Desc, LOC1.TIN_No  as tn_number , TSPL_TRANSFER_HEAD. Trip_No  as trip_number,TSPL_TRANSFER_DETAIL .Item_Desc as item, " & _
                           " ( case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty end )as Qty," & _
                           " TSPL_TRANSFER_DETAIL.Leak*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as leak  ,  " & _
                           "TSPL_TRANSFER_DETAIL.Burst*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as brust ,TSPL_TRANSFER_DETAIL.Shortage*TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as short ,TSPL_TRANSFER_DETAIL.MRP/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_ITEM_UOM_DETAIL.Conversion_Factor ,TSPL_TRANSFER_HEAD.description ,TSPL_TRANSFER_HEAD.Reference,TSPL_TRANSFER_HEAD.Total_Transfer_Amount,ltrim(right(convert(varchar(25), Date_Time_Removal, 100), 7)) as RemovalTime,(select  FromLoc_Desc from TSPL_TRANSFER_HEAD as LOTable where LOTable.Transfer_No= TSPL_TRANSFER_HEAD.Load_Out_No ) as  LoadoutFromLocation,TSPL_TRANSFER_HEAD.From_Location,TSPL_TRANSFER_HEAD.To_Location,LOC1.Location_Desc,LOC1.Add1,LOC1.Add2,LOC1.Add3,TSPL_TRANSFER_DETAIL.BasicPrice_WithTax ,TSPL_TRANSFER_DETAIL.Basic_Price ,     TSPL_TRANSFER_HEAD.Reference_Doc_No    " & _
                           "from TSPL_TRANSFER_DETAIL left outer join  TSPL_TRANSFER_HEAD   on TSPL_TRANSFER_DETAIL.Transfer_No  = TSPL_TRANSFER_HEAD.Transfer_No " & _
                           " left outer join TSPL_EMPLOYEE_MASTER on TSPL_TRANSFER_HEAD.Salesmancode  =TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                           "left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location =TSPL_LOCATION_MASTER.Location_code " & _
                           " left outer join TSPL_LOCATION_MASTER as LOC1 on TSPL_TRANSFER_HEAD.To_Location =LOC1.Location_code " & _
                           "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when  TSPL_TRANSFER_DETAIL.Uom='FB' then 'FC' else case when TSPL_TRANSFER_DETAIL.Uom='FC' then 'FB' end end) " & _
                           " where TSPL_TRANSFER_HEAD.Transfer_No ='" + strNo + "' and  (case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty+TSPL_TRANSFER_DETAIL.Leak +TSPL_TRANSFER_DETAIL.Burst +TSPL_TRANSFER_DETAIL.Shortage  end )>0  ) xxx ) bb Left Outer Join TSPL_ITEM_MASTER on bb.Item_Code  =TSPL_ITEM_MASTER.Item_Code group by bb.Item_Code,bb.mrp Order by Sku_Seq "

                            dt = clsDBFuncationality.GetDataTable(qry)

                            '-------------------------------------------------------






                            If strTrasactionType = "LI" Then
                                If isPrePrinted Then
                                    If isBothLocationPhysical Then
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "TransferLIBothLogical", "TransferReport")
                                    Else
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "transferpreprinted", "TransferReport")
                                    End If
                                Else
                                    If (objCommonVar.CurrentCompanyCode = "VIZAG") Then
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "TransferLI-VIZ", "TransferReport")
                                    Else
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "TransferLI-GUN", "TransferReport")
                                    End If

                                End If
                            Else
                                If isPrePrinted Then

                                    Dim loctype As String = clsDBFuncationality.getSingleValue("select Location_Type from tspl_location_master where  Location_Code='" + dt.Rows(0)("To_Location") + "'")
                                    Dim Transype As String = clsDBFuncationality.getSingleValue("select Trans_Type from TSPL_TRANSFER_HEAD where  transfer_No='" + strNo + "'")

                                    'Dim loc As DataTable = clsDBFuncationality.getSingleValue(loctype)
                                    If clsCommon.CompairString(Transype, "Route") = CompairStringResult.Equal OrElse clsCommon.CompairString(Transype, "Depot") = CompairStringResult.Equal Then

                                        If isPrePrintTAx Then
                                            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "TransferPreprintedLORouteTAX", "TransferReport")

                                        Else
                                            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "TransferPreprintedLORoute", "TransferReport")

                                        End If

                                    ElseIf clsCommon.CompairString(loctype, "Physical") = CompairStringResult.Equal Then
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "transferpreprintedLO", "TransferReport")
                                    Else
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "transferpreprintedLOoldfrt", "TransferReport")
                                    End If

                                Else
                                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Guntur") = CompairStringResult.Equal Then
                                        qry = "    select (select Transporter_name from TSPL_Transport_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_Id =TSPL_Transport_MASTER.Transport_Id where TSPL_VEHICLE_MASTER.Vehicle_Id = (select Vehicle_Code from TSPL_TRANSFER_HEAD where TSPL_TRANSFER_HEAD.Transfer_No='NLO/13/03000')  ) as TPTName,MAX(bb.Created_By) as Created_By,max(bb.Modify_By) as Modify_By,  MAX(GPNO) as GPNo,max(total_basic_amt) as total_basic_amt,sum(basic_amt) as basic_amt,bb.Item_Code,MAX(item) as item, max(transfer_number)as  transfer_number,MAX(date) as Date,   MAX(Load_Out_No)as Load_Out_No ,max(CONVERT(VARCHAR, date,103)) as date,max(salesman_name)as salesman_name,max(route_number)as route_number,max(route_desc)as route_desc,max(tn_number)as tn_number,max(trip_number)as trip_number ,sum(leak)as leak,sum(brust)as brust,sum(short)as short, (mrp) as mrp, max(Vehicle_No)as Vehicle_No,sum(itemqty) as itemqty,SUM(isnull(FCS,0)) as FCS,SUM(isnull(FBS,0)) as FBS,SUM(isnull(FSH,0)) as FSH,SUM(isnull(ECS,0)) as ECS,SUM(isnull(EBS,0)) as EBS,SUM(isnull(ESH,0)) as ESH,max(description) as description ,max(Reference) as Reference, MAX(Total_Transfer_Amount) as Total_Transfer_Amount ,Max(TSPL_ITEM_MASTER.Sku_Seq) as Sku_Seq,max(RemovalTime) as RemovalTime ,max(LoadoutFromLocation) as  LoadoutFromLocation ,max(Conversion_Factor) as [COnversion Factor],MAX(From_Location) as From_Location,max(To_Location) as To_Location ,MAX(Location_Desc)as Location_Desc,max(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,SUM(Basic_Price) as basic_Price,SUM(BasicPrice_WithTax) as BasicPrice_WithTax,max(Reference_Doc_No) as Reference_Doc_No,max(Emp_Name) as emp_name, (select Reference_Doc_No from TSPL_TRANSFER_HEAD where TSPL_TRANSFER_HEAD.Transfer_No = MAX( bb.Load_Out_No) ) as LORefernceNO"
                                        qry += ",MAX( TAX1) as TAX1,MAX(TAX1_Rate) as TAX1_Rate,MAX( TAX1_Assessable_Amt) as TAX1_Assessable_Amt,MAX( TAX1_Amt) as TAX1_Amt,MAX( TAX2) as TAX2,MAX( TAX2_Rate) as TAX2_Rate,MAX( TAX2_Assessable_Amt) as TAX2_Assessable_Amt,MAX( TAX2_Amt) as TAX2_Amt,MAX( TAX3) as TAX3,MAX( TAX3_Rate) as TAX3_Rate,MAX( TAX3_Assessable_Amt) as TAX3_Assessable_Amt,MAX( TAX3_Amt) as TAX3_Amt,MAX( TAX4) as TAX4,MAX( TAX4_Rate) as TAX4_Rate,MAX( TAX4_Assessable_Amt) as TAX4_Assessable_Amt,MAX(TAX4_Amt) as TAX4_Amt,MAX( TAX5) as TAX5,MAX( TAX5_Rate) as TAX5_Rate,MAX( TAX5_Assessable_Amt) as TAX5_Assessable_Amt,MAX( TAX5_Amt) as TAX5_Amt,MAX( KM_Reading) as KM_Reading"
                                        qry += ",max(LocationAddress) as LocationAddress,max(locPin) as locPin,max(LocTinNo) as LocTinNo,max(locCSTNo) as locCSTNo,max(locPhone1) as locPhone1,max(locPhone2) as locPhone2,MAX(isRGB) as isRGB "
                                        qry += " from"
                                        qry += "( "
                                        qry += "select isRGB , Created_By,Modify_By,Emp_Name, GPNO,total_basic_amt,basic_amt,Item_Code,transfer_number,time,date,salesman_name,route_number,Route_Desc,tn_number,trip_number, case when Uom='FC' then qty  end as FCS,  case when Uom='FB' then qty end as FBS,  case when Uom='SH' then qty end as FSH,  case when Uom='EC' then qty  end as ECS,  case when Uom='EB' then qty end as EBS, case when Uom='SH' then qty end as ESH,  item,Qty,leak,brust,short,mrp,Vehicle_No ,Qty as itemqty,description ,Reference,Total_Transfer_Amount,RemovalTime,LoadoutFromLocation,Conversion_Factor ,Basic_Price,BasicPrice_WithTax,From_Location,Location_Desc,To_Location,Add1,Add2,Add3,Reference_Doc_No,Load_Out_No,TAX1,TAX1_Rate,TAX1_Assessable_Amt,TAX1_Amt,TAX2,TAX2_Rate,TAX2_Assessable_Amt,TAX2_Amt,TAX3,TAX3_Rate,TAX3_Assessable_Amt,TAX3_Amt,TAX4,TAX4_Rate,TAX4_Assessable_Amt,TAX4_Amt,TAX5,TAX5_Rate,TAX5_Assessable_Amt,TAX5_Amt,KM_Reading ,LocationAddress,locPin,LocTinNo,locCSTNo,locPhone1,locPhone2"
                                        qry += "  from "
                                        qry += "( "
                                        qry += "select case when TSPL_TRANSFER_DETAIL.Empty_Value>0 then 'RGB' else 'PET' end isRGB ,  TSPL_TRANSFER_HEAD.Created_By,TSPL_TRANSFER_HEAD.Modify_By,TSPL_TRANSFER_HEAD.Load_Out_No,  (select top 1 Emp_Name  from TSPL_EMPLOYEE_MASTER where TSPL_TRANSFER_HEAD.Salesmancode =TSPL_EMPLOYEE_MASTER.EMP_CODE  ) as Emp_Name,  (select top 1 GPCode  from TSPL_GATEPASS_DETAIL where TSPL_GATEPASS_DETAIL.DocNo=TSPL_TRANSFER_HEAD.Transfer_No) as GPNO ,  total_basic_amt,basic_amt,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL .Uom ,TSPL_TRANSFER_HEAD.Transfer_No AS transfer_number,TSPL_TRANSFER_HEAD.Date_Time_Removal as time ,TSPL_TRANSFER_HEAD.Transfer_Date as date,TSPL_EMPLOYEE_MASTER.Emp_Name as salesman_name ,TSPL_TRANSFER_HEAD.Route_No  as route_number,TSPL_TRANSFER_HEAD.Route_Desc, LOC1.TIN_No  as tn_number , TSPL_TRANSFER_HEAD. Trip_No  as trip_number,TSPL_TRANSFER_DETAIL .Item_Desc as item,  ( case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty end )as Qty, TSPL_TRANSFER_DETAIL.Leak*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as leak  ,  TSPL_TRANSFER_DETAIL.Burst*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as brust ,TSPL_TRANSFER_DETAIL.Shortage*TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as short ,TSPL_TRANSFER_DETAIL.MRP/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_ITEM_UOM_DETAIL.Conversion_Factor ,TSPL_TRANSFER_HEAD.description ,TSPL_TRANSFER_HEAD.Reference,TSPL_TRANSFER_HEAD.Total_Transfer_Amount,ltrim(right(convert(varchar(25), Date_Time_Removal, 100), 7)) as RemovalTime,(select  FromLoc_Desc from TSPL_TRANSFER_HEAD as LOTable where LOTable.Transfer_No= TSPL_TRANSFER_HEAD.Load_Out_No ) as  LoadoutFromLocation,TSPL_TRANSFER_HEAD.From_Location,TSPL_TRANSFER_HEAD.To_Location,LOC1.Location_Desc,LOC1.Add1,LOC1.Add2,LOC1.Add3,TSPL_TRANSFER_DETAIL.BasicPrice_WithTax ,TSPL_TRANSFER_DETAIL.Basic_Price ,     TSPL_TRANSFER_HEAD.Reference_Doc_No  ,"
                                        qry += "(select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code= TSPL_TRANSFER_HEAD.TAX1) as TAX1,"
                                        qry += "TSPL_TRANSFER_HEAD.TAX1_Rate, TSPL_TRANSFER_HEAD.TAX1_Assessable_Amt,  TSPL_TRANSFER_HEAD.TAX1_Amt, (select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code=TSPL_TRANSFER_HEAD.TAX2) as TAX2, TSPL_TRANSFER_HEAD.TAX2_Rate,  TSPL_TRANSFER_HEAD.TAX2_Assessable_Amt, TSPL_TRANSFER_HEAD.TAX2_Amt,(select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code= TSPL_TRANSFER_HEAD.TAX3) as TAX3,  TSPL_TRANSFER_HEAD.TAX3_Rate, TSPL_TRANSFER_HEAD.TAX3_Assessable_Amt, TSPL_TRANSFER_HEAD.TAX3_Amt,  (select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code= TSPL_TRANSFER_HEAD.TAX4) as TAX4, TSPL_TRANSFER_HEAD.TAX4_Rate, TSPL_TRANSFER_HEAD.TAX4_Assessable_Amt,  TSPL_TRANSFER_HEAD.TAX4_Amt, (select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code= TSPL_TRANSFER_HEAD.TAX5) as TAX5, TSPL_TRANSFER_HEAD.TAX5_Rate,  TSPL_TRANSFER_HEAD.TAX5_Assessable_Amt, TSPL_TRANSFER_HEAD.TAX5_Amt ,TSPL_TRANSFER_HEAD.KM_Reading "
                                        qry += ",(TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end) as LocationAddress,  TSPL_LOCATION_MASTER.Pin_Code  as locPin, TSPL_LOCATION_MASTER.TIN_No as LocTinNo, TSPL_LOCATION_MASTER.CST_No as locCSTNo, TSPL_LOCATION_MASTER.Phone1 as locPhone1, TSPL_LOCATION_MASTER.Phone2 as locPhone2  "
                                        qry += "from TSPL_TRANSFER_DETAIL "
                                        qry += " left outer join  TSPL_TRANSFER_HEAD   on TSPL_TRANSFER_DETAIL.Transfer_No  = TSPL_TRANSFER_HEAD.Transfer_No "
                                        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_TRANSFER_HEAD.Salesmancode  =TSPL_EMPLOYEE_MASTER.EMP_CODE"
                                        qry += "   left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location =TSPL_LOCATION_MASTER.Location_code  left outer join TSPL_LOCATION_MASTER as LOC1 on TSPL_TRANSFER_HEAD.To_Location =LOC1.Location_code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when  TSPL_TRANSFER_DETAIL.Uom='FB' then 'FC' else case when TSPL_TRANSFER_DETAIL.Uom='FC' then 'FB' end end)  where TSPL_TRANSFER_HEAD.Transfer_No ='" + strNo + "' and  (case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty+TSPL_TRANSFER_DETAIL.Leak +TSPL_TRANSFER_DETAIL.Burst +TSPL_TRANSFER_DETAIL.Shortage  end"
                                        qry += " )>0 "
                                        qry += " ) xxx "
                                        qry += ") bb Left Outer Join TSPL_ITEM_MASTER on bb.Item_Code  =TSPL_ITEM_MASTER.Item_Code group by bb.Item_Code,bb.mrp   Order by Sku_Seq "
                                        dt = clsDBFuncationality.GetDataTable(qry)
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "rptTransferPrintG", "TransferReport")
                                    Else
                                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "TransferLO", "TransferReport")

                                    End If
                                End If

                            End If
                        End If
                    End If
                End If


                qry = "select Post from TSPL_TRANSFER_HEAD where Transfer_No='" + strNo + "'"
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "N") = CompairStringResult.Equal Then
                    Dim isSettingOn As Boolean = IIf(clsCommon.CompairString("Y", clsFixedParameter.GetData(clsFixedParameterType.PrintVerify, clsFixedParameterCode.Transfer, Nothing)) = CompairStringResult.Equal, True, False)
                    Dim isCorrect As String = "N"
                    If isSettingOn Then
                        If (common.clsCommon.MyMessageBoxShow(Me, "Have you checked and verified  Properly ? ", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                            isCorrect = "Y"
                        End If
                    Else
                        isCorrect = "Y"
                    End If
                    qry = "update TSPL_TRANSFER_HEAD set printed='" + isCorrect + "' where transfer_no='" + strNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If

            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please select TRANSFER NO.")
                Return False
            End If
            frmCRV = Nothing
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
            Return False
        End Try
    End Function


    ''Public Shared Function PrintData(ByVal strNo As String, ByVal isPrePrinted As Boolean) As Boolean
    ''    Try
    ''        If clsCommon.myLen(strNo) > 0 Then
    ''            Dim str As String = "'' as totalqty"
    ''            Dim qry As String = "select  Uom as Unit,sum(case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then TSPL_TRANSFER_DETAIL.Item_Qty else TSPL_TRANSFER_DETAIL.LoadIn_Qty end) as Qty from TSPL_TRANSFER_DETAIL left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No where TSPL_TRANSFER_HEAD.Transfer_No = '" + strNo + "' group by TSPL_TRANSFER_DETAIL.Uom"
    ''            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    ''            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    ''                str = "'"
    ''                For Each dr As DataRow In dt.Rows
    ''                    str = str + clsCommon.myCstr(dr("Unit")) + " - " + clsCommon.myCstr(dr("Qty")) + " "
    ''                Next
    ''                str = str + "'" + " as totalqty"
    ''            End If

    ''            dt = New DataTable()
    ''            qry = "select Item_Type,Transfer_Type,(case when (TSPL_LOCATION_MASTER.Excisable='T' and TSPL_TRANSFER_HEAD.Transfer_Type='LO') then 1 else 0 end) as isExcisableLoadout,(case when TSPL_LOCATION_MASTER.Location_Type='Physical' and ToLocatioin.Location_Type='Physical' then 1 else 0 end ) BothLocationPhysical from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as ToLocatioin on ToLocatioin.Location_Code=TSPL_TRANSFER_HEAD.To_Location  where Transfer_No='" + strNo + "'"
    ''            dt = clsDBFuncationality.GetDataTable(qry)
    ''            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    ''                Dim isBothLocationPhysical As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("BothLocationPhysical")) = 1, True, False)
    ''                Dim strItemType As String = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
    ''                Dim strTrasactionType As String = clsCommon.myCstr(dt.Rows(0)("Transfer_Type"))
    ''                If clsCommon.CompairString(strItemType, "Empty") = CompairStringResult.Equal Then
    ''                    If clsCommon.CompairString(strTrasactionType, "LO") = CompairStringResult.Equal Then
    ''                        qry = "select TSPL_TRANSFER_HEAD.Transfer_No,TSPL_TRANSFER_HEAD.Transfer_Date,TSPL_LOCATION_MASTER.Location_Desc as To_LocationName ,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc ,case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then TSPL_TRANSFER_DETAIL.Item_Qty else  TSPL_TRANSFER_DETAIL. LoadIn_Qty end as Qty,TSPL_TRANSFER_DETAIL.Item_Price,TSPL_TRANSFER_DETAIL.Amount,TSPL_TRANSFER_HEAD.Vehicle_No  " & _
    ''                        " from TSPL_TRANSFER_DETAIL" & _
    ''                        " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No " & _
    ''                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.To_Location " & _
    ''                        " where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "'" & _
    ''                        " ORDER by TSPL_TRANSFER_DETAIL.Line_No"
    ''                        dt = clsDBFuncationality.GetDataTable(qry)
    ''                        InventryViewer.funreport(dt, "rptEmptyTransferLoadOutCustom", "Empty Loadout Report")

    ''                    Else
    ''                        qry = "select Transfer_No,max(Transfer_Date) as Transfer_Date,max(From_LocationName)as From_LocationName,max(Vehicle_No) as Vehicle_No,Item_Code,max(Item_Desc) as Item_Desc,sum(isnull(FCS,0)) as FCS,sum(isnull(FBS,0)) as FBS,sum(isnull(FSH,0)) as FSH,sum(isnull(ECS,0)) as ECS,sum(isnull(EBS,0)) as EBS,sum(Leak) as Leak,sum(Burst) as Burst,sum(Shortage) as Shortage  from(" & _
    ''                        " select Transfer_No,Transfer_Date,From_LocationName,Vehicle_No,Item_Code,Item_Desc,case when Unit_code='FC' then Qty end as FCS, case when Unit_code='FB' then Qty end as FBS, case when Unit_code='SH' then Qty end as FSH, case when Unit_code='EC' then Qty end as ECS,case when Unit_code='EB' then Qty end as EBS,Leak,Burst,Shortage" & _
    ''                        " from(select TSPL_TRANSFER_HEAD.Transfer_No,replace(CONVERT(varchar(11),TSPL_TRANSFER_HEAD.Transfer_Date,104),'.','/') as Transfer_Date,TSPL_LOCATION_MASTER.Location_Desc as  From_LocationName,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,(case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then Item_Qty else LoadIn_Qty end) as Qty,Uom as Unit_code,TSPL_TRANSFER_DETAIL.Leak,TSPL_TRANSFER_DETAIL.Burst,TSPL_TRANSFER_DETAIL.Shortage from TSPL_TRANSFER_DETAIL " & _
    ''                        " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No" & _
    ''                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.From_Location" & _
    ''                        " where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "') as Final" & _
    ''                        " )SuperFinal group by Transfer_No,Item_Code"
    ''                        dt = clsDBFuncationality.GetDataTable(qry)
    ''                        InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptEmptyTransferLoadInCustom", "Empty Loadin Report")

    ''                    End If
    ''                Else
    ''                    If clsCommon.myCdbl(dt.Rows(0)("isExcisableLoadout")) = 1 Then
    ''                        qry = "select  sum(Empty_Value * (case when TSPL_TRANSFER_HEAD.Transfer_Type='LO' then TSPL_TRANSFER_DETAIL.Item_Qty else TSPL_TRANSFER_DETAIL.LoadIn_Qty end)) as EmptyValue from TSPL_TRANSFER_DETAIL left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No= TSPL_TRANSFER_DETAIL.Transfer_No where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "'"
    ''                        Dim strEmptyValue As String = clsCommon.myCstr(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)))

    ''                        Dim strAddress = "TSPL_LOCATION_MASTER.Add1+(case when LEN(TSPL_LOCATION_MASTER.Add2)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add2+(case when LEN(TSPL_LOCATION_MASTER.Add3)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add3+(case when LEN(TSPL_LOCATION_MASTER.Add4)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add4+(case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.City_Code+(case when LEN(TSPL_LOCATION_MASTER.State)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.State+(case when LEN(TSPL_LOCATION_MASTER.Country)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Country"

    ''                        qry = "select TSPL_TRANSFER_HEAD.Mode_Of_Transport,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_TRANSFER_HEAD.Km_Reading ,TSPL_CHAPTER_HEAD.Description as ChapterName,(" + strAddress + ") as Address,TSPL_TRANSFER_HEAD.Transfer_No as Sale_Invoice_No,replace(CONVERT(varchar(11), TSPL_TRANSFER_HEAD.Transfer_Date,104),'.','/') as  Sale_Invoice_Date,'' as Cust_Name,TSPL_TRANSFER_HEAD.TAX2 ,TSPL_TRANSFER_HEAD.TAX2_Rate,TSPL_TRANSFER_HEAD.TAX2_Amt," & _
    ''                        " TSPL_TRANSFER_HEAD.TAX3 ,TSPL_TRANSFER_HEAD.TAX3_Rate,TSPL_TRANSFER_HEAD.TAX3_Amt," & _
    ''                        " TSPL_TRANSFER_HEAD.TAX4 ,TSPL_TRANSFER_HEAD.TAX4_Rate,TSPL_TRANSFER_HEAD.TAX4_Amt," & _
    ''                        " TSPL_TRANSFER_HEAD.TAX5 ,TSPL_TRANSFER_HEAD.TAX5_Rate,TSPL_TRANSFER_HEAD.TAX5_Amt," & _
    ''                        " TSPL_TRANSFER_DETAIL.Item_Qty as Invoice_Qty,(TSPL_TRANSFER_DETAIL.Item_Desc +' ('+TSPL_TRANSFER_DETAIL.Uom +')') as Item_Desc,TSPL_TRANSFER_DETAIL.MRP as MRP_Amt,TSPL_TRANSFER_DETAIL. Basic_Price as Basic_Rate, TSPL_TRANSFER_DETAIL.TAX1_Rate AS DTax1Rate,TSPL_TRANSFER_DETAIL.Tax1_Assessable_Amt as Total_Assessable_Amt,(TSPL_TRANSFER_DETAIL.Item_Qty *TSPL_TRANSFER_DETAIL.Basic_Price) as Total_Basic_Amt," + strEmptyValue + " as Empty_Value,0 as ttlTPT,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName,CONVERT(varchar(100),TSPL_TRANSFER_HEAD.Date_Time_Removal,108) as RemovelTime,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_TRANSFER_DETAIL.Item_Code and  (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when TSPL_TRANSFER_DETAIL.Uom='FB' then 'FC' else case when TSPL_TRANSFER_DETAIL.Uom='FC' then 'FB' end end) )  AS Conversion," + str + ",'' as FOCItem, 0 as Inv_Discount_Amt" & _
    ''                        ",0 as TAX1_Rate1,0 as TAX1_Rate2,0 as TAX1_Rate3,0 as TAX1_Amt1,0 as TAX1_Amt2,0 as TAX1_Amt3," & _
    ''                        " 0 as TAX2_Rate1,0 as TAX2_Rate2,0 as TAX2_Rate3,0 as TAX2_Amt1,0 as TAX2_Amt2,0 as TAX2_Amt3," & _
    ''                        "  0 as TAX3_Rate1,0 as TAX3_Rate2,0 as TAX3_Rate3,0 as TAX3_Amt1,0 as TAX3_Amt2,0 as TAX3_Amt3," & _
    ''                        " 0 as TAX4_Rate1,0 as TAX4_Rate2,0 as TAX4_Rate3,0 as TAX4_Amt1,0 as TAX4_Amt2,0 as TAX4_Amt3," & _
    ''                        " 0 as TAX5_Rate1,0 as TAX5_Rate2,0 as TAX5_Rate3,0 as TAX5_Amt1,0 as TAX5_Amt2,0 as TAX5_Amt3" & _
    ''                        " from TSPL_TRANSFER_DETAIL" & _
    ''                        " left outer join TSPL_TRANSFER_HEAD on  TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No" & _
    ''                        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code" & _
    ''                        " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads" & _
    ''                        " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_TRANSFER_HEAD.Salesmancode" & _
    ''                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.To_Location" & _
    ''                        " where TSPL_TRANSFER_HEAD.Transfer_No='" + strNo + "'"
    ''                        dt = clsDBFuncationality.GetDataTable(qry)
    ''                        SalesReportViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x12, "crptCustomSaleInvoiceWithExcise", "Excisable Loadout Transfer", True)
    ''                    Else
    ''                        qry = "select bb.Item_Code,MAX(item) as item, max(transfer_number)as  transfer_number,max(date)as date,max(salesman_name)as salesman_name,max(route_number)as route_number,max(tn_number)as tn_number,max(trip_number)as trip_number ,sum(leak)as leak,sum(brust)as brust,sum(short)as short, max(mrp) as mrp, max(Vehicle_No)as Vehicle_No,sum(itemqty) as itemqty,SUM(isnull(FCS,0)) as FCS,SUM(isnull(FBS,0)) as FBS,SUM(isnull(FSH,0)) as FSH,SUM(isnull(ECS,0)) as ECS,SUM(isnull(EBS,0)) as EBS,SUM(isnull(ESH,0)) as ESH,max(description) as description ,max(Reference) as Reference, MAX(Total_Transfer_Amount) as Total_Transfer_Amount ,Max(TSPL_ITEM_MASTER.Sku_Seq) as Sku_Seq,max(RemovalTime) as RemovalTime ,max(LoadoutFromLocation) as  LoadoutFromLocation from(" & _
    ''                        " select Item_Code,transfer_number,time,date,salesman_name,route_number,tn_number,trip_number," & _
    ''                        " case when Uom='FC' then qty  end as FCS,  case when Uom='FB' then qty end as FBS,  case when Uom='SH' then qty end as FSH,  case when Uom='EC' then qty  end as ECS,  case when Uom='EB' then qty end as EBS, case when Uom='SH' then qty end as ESH, " & _
    ''                        " item,Qty,leak,brust,short,mrp,Vehicle_No ,Qty as itemqty,description ,Reference,Total_Transfer_Amount,RemovalTime,LoadoutFromLocation from " & _
    ''                        " ( select TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL .Uom ,TSPL_TRANSFER_HEAD.Transfer_No AS transfer_number,TSPL_TRANSFER_HEAD.Date_Time_Removal as time ,TSPL_TRANSFER_HEAD.Transfer_Date as date,TSPL_EMPLOYEE_MASTER.Emp_Name as salesman_name ,TSPL_TRANSFER_HEAD.Route_No  as route_number,TSPL_LOCATION_MASTER.TIN_No  as tn_number , TSPL_TRANSFER_HEAD. Trip_No  as trip_number,TSPL_TRANSFER_DETAIL .Item_Desc as item, " & _
    ''                        " ( case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty end )as Qty," & _
    ''                        " TSPL_TRANSFER_DETAIL.Leak*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as leak  ,  " & _
    ''                        "TSPL_TRANSFER_DETAIL.Burst*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as brust ,TSPL_TRANSFER_DETAIL.Shortage*TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as short ,TSPL_TRANSFER_DETAIL.MRP as mrp,TSPL_TRANSFER_HEAD.Vehicle_No,TSPL_ITEM_UOM_DETAIL.Conversion_Factor ,TSPL_TRANSFER_HEAD.description ,TSPL_TRANSFER_HEAD.Reference,TSPL_TRANSFER_HEAD.Total_Transfer_Amount,ltrim(right(convert(varchar(25), Date_Time_Removal, 100), 7)) as RemovalTime,(select  FromLoc_Desc from TSPL_TRANSFER_HEAD as LOTable where LOTable.Transfer_No= TSPL_TRANSFER_HEAD.Load_Out_No ) as  LoadoutFromLocation " & _
    ''                        "from TSPL_TRANSFER_DETAIL left outer join  TSPL_TRANSFER_HEAD   on TSPL_TRANSFER_DETAIL.Transfer_No  = TSPL_TRANSFER_HEAD.Transfer_No " & _
    ''                        "left outer join TSPL_EMPLOYEE_MASTER on TSPL_TRANSFER_HEAD.Salesmancode =TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
    ''                        "left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location =TSPL_LOCATION_MASTER.Location_code " & _
    ''                        "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when  TSPL_TRANSFER_DETAIL.Uom='FB' then 'FC' else case when TSPL_TRANSFER_DETAIL.Uom='FC' then 'FB' end end) " & _
    ''                        " where TSPL_TRANSFER_HEAD.Transfer_No ='" + strNo + "' and  (case when TSPL_TRANSFER_HEAD.Transfer_Type ='LO' then TSPL_TRANSFER_DETAIL.Item_Qty  else TSPL_TRANSFER_DETAIL.LoadIn_Qty end )>0  ) xxx ) bb Left Outer Join TSPL_ITEM_MASTER on bb.Item_Code  =TSPL_ITEM_MASTER.Item_Code group by bb.Item_Code Order by Sku_Seq "
    ''                        dt = clsDBFuncationality.GetDataTable(qry)
    ''                        If strTrasactionType = "LI" Then
    ''                            If isPrePrinted Then
    ''                                If isBothLocationPhysical Then
    ''                                    InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "TransferLIBothLogical", "TransferReport")
    ''                                Else
    ''                                    InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x12, "transferpreprinted", "TransferReport")
    ''                                End If
    ''                            Else
    ''                                InventryViewer.funreport(dt, EnumTecxpertPaperSize.NA, "TransferLI", "TransferReport")
    ''                            End If
    ''                        Else
    ''                            If isPrePrinted Then
    ''                                ''InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x12, "transferpreprintedLONew", "TransferReport")
    ''                                InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x12, "transferpreprintedLO", "TransferReport")
    ''                            Else
    ''                                InventryViewer.funreport(dt, EnumTecxpertPaperSize.NA, "TransferLO", "TransferReport")
    ''                            End If

    ''                        End If
    ''                    End If
    ''                End If
    ''            End If


    ''            qry = "select Post from TSPL_TRANSFER_HEAD where Transfer_No='" + strNo + "'"
    ''            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "N") = CompairStringResult.Equal Then
    ''                Dim isSettingOn As Boolean = IIf(clsCommon.CompairString("Y", clsFixedParameter.GetData(clsFixedParameterType.PrintVerify, clsFixedParameterCode.Transfer, Nothing)) = CompairStringResult.Equal, True, False)
    ''                Dim isCorrect As String = "N"
    ''                If isSettingOn Then
    ''                    If (common.clsCommon.MyMessageBoxShow("Have you checked and verified  Properly ? ", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
    ''                        isCorrect = "Y"
    ''                    End If
    ''                Else
    ''                    isCorrect = "Y"
    ''                End If
    ''                qry = "update TSPL_TRANSFER_HEAD set printed='" + isCorrect + "' where transfer_no='" + strNo + "'"
    ''                clsDBFuncationality.ExecuteNonQuery(qry)
    ''            End If

    ''        Else
    ''            common.clsCommon.MyMessageBoxShow("Please select TRANSFER NO.")
    ''            Return False
    ''        End If
    ''        Return True
    ''    Catch ex As Exception
    ''        common.clsCommon.MyMessageBoxShow(ex.Message)
    ''        Return False
    ''    End Try
    ''End Function






    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ReportTransfer)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub ReportTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Ctrl+P Print the Report")
    End Sub

    Private Sub ReportTransfer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        PrintData(txtTransferNo.Value, True)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        PrintData(txtTransferNo.Value, False)
    End Sub
End Class
