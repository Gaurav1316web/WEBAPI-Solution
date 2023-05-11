Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProductionMRP

#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public MRP_DATE As Date = Nothing
    Public MRP_REMARKS As String = Nothing
    Public MRP_DESCRIPTION As String = Nothing
    Public MRP_Location As String = Nothing
    Public Location_Desc As String = Nothing
    Public MRP_FROM As Date = Nothing
    Public MRP_TO As Date = Nothing
    Public MRP_ITEM_TYPE As String = Nothing
    Public POSTED As Boolean = Nothing
    Public Auto_Indent As Integer = Nothing
    Public Auto_PO As Integer = Nothing
    Public Department_Code As String = Nothing
    Public ArrProductionPlanCode As ArrayList
    Public ObjListMRPDetail As List(Of clsMRPProductionDetail)
    Public ObjListMRPBomDetail As List(Of clsMRPProductionBOMDetail)
    Public ObjListMRPBomItemDetail As List(Of clsMRPProductionBOMItemDetail)
    Public ObjListMRPPendingPO As List(Of clsMRPProductionPendingPO)
    Public ObjListMRPPendingSRN As List(Of clsMRPProductionPendingSRN)
    ' 

    '===========auto po=============
    Public AAuto_Vendor_Code As String = Nothing
    Public AAuto_Vendor_Name As String = Nothing
    Public AAuto_PO_Rate As Double = Nothing
    Public AAuto_Last_Rate As Double = Nothing
    Public AAuto_Avg_Rate As Double = Nothing
    Public Arr_Auto_Po As List(Of clsProductionMRP) = Nothing
#End Region

    Public Shared Function GetItemConvQty(ByVal strItem As String, ByVal strSourceUnit As String, ByVal dblQty As Double, ByVal strTargerUnit As String) As Double
        Dim dblTargetConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strTargerUnit & "'"))
        Dim dblConvQty As Double = 0
        If clsCommon.myLen(strSourceUnit) > 0 Then
            Dim dblSourceConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strSourceUnit & "'"))
            If dblTargetConvF > 0 Then
                dblConvQty = Math.Round(Math.Round((dblSourceConvF / dblTargetConvF), 2) * dblQty, 6)
            End If
        End If
        Return dblConvQty
    End Function

    Public Shared Function GetStockQty(ByVal ItemCode As String, ByVal Uom_Code As String, ByVal Loc_Code As String) As Double
        Dim DblValue As Double = 0
        'Dim qry As String = clsItemLocationDetails.getBaseQryForItemBalanceDuringTransaction(ItemCode, Uom_Code, Loc_Code, clsCommon.myCDate(clsCommon.GETSERVERDATE()), "", False, 0, Nothing)
        'qry = "select SUM(Qty *RI )as ActualBalanceQty from (" + qry + ")FinalQry group by ICode"
        Dim qry As String = " select convert (decimal(18,2), sum (Stock_Qty)) as Stock_Qty  from ( " & _
                            " select Item_Type,Item_Type_Name, Item_Category_Struct_Code  as Item_Category_Struct_Code,Item_Code,Item_Desc,itf_code,Stock_Qty,Stock_UOM from ( " & _
                            "  select  Item_Type,max(Item_Type_Name) as Item_Type_Name,max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code, SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM from (select * from ( select gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  , InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/nullif (FATSNFConvertedUnit.Conversion_Factor,0) end))  ,0) as QtyKG, '" + Uom_Code + "' as Stock_UOM,(InventroyMovement.Stock_Qty / nullif (TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) as Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/nullif (FATSNFConvertedUnit.Conversion_Factor,0) end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/nullif (FATSNFConvertedUnit.Conversion_Factor,0) end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code  " & _
                            "  ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select 0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType  from TSPL_INVENTORY_MOVEMENT union all  " & _
                            "  select ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType from TSPL_INVENTORY_MOVEMENT_NEW " & _
                            " ) InventroyMovement  " & _
                            "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code " & _
                            "  left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code " & _
                            "  left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " & _
                            "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code  " & _
                            "  left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='" + Uom_Code + "' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + Uom_Code + "' " & _
                            " left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  and Item_Code in ('" + ItemCode + "')  " & _
                            "  and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + Loc_Code + "') ))xxx Group by Item_Type,Item_Code )xxxx " & _
                            "  ) Final " & _
                            "  "
        DblValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return DblValue
    End Function

    Public Shared Function GetPendingPOkQty(ByVal ItemCode As String, ByVal Uom_Code As String, ByVal Loc_Code As String) As Double
        Dim DblValue As Double = 0
        'Dim qry As String = " select Sum (Fianl.Final_Qty_In_Target_UOM) as Final_Qty_In_Target_UOM from ( " & _
        '                    " select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No , TSPL_PURCHASE_ORDER_Detail.Item_Code  , TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location, TSPL_PURCHASE_ORDER_Detail.Unit_Code,TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty, Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty ) ) ) as Final_Qty_In_Target_UOM   from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_Detail on TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
        '                    " left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code and Source_UOM.Uom_Code = TSPL_PURCHASE_ORDER_Detail.Unit_Code " & _
        '                    " left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code and Target_Uom.Uom_Code = '" + Uom_Code + "' " & _
        '                    " where  TSPL_PURCHASE_ORDER_HEAD.Status =0  and TSPL_PURCHASE_ORDER_Detail.Item_Code = '" + ItemCode + "' and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + Loc_Code + "' " & _
        '                    " ) Fianl "
        Dim qry As String = " select Sum ( case when SRN_QTY < = Final_Qty_In_Target_UOM then   Fianl.Final_Qty_In_Target_UOM -  SRN_QTY else 0 end) as Pending_PO_Qty from  " & _
                               " ( " & _
                               " select  " & _
                               " TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_Vendor_Master.Vendor_Name,Convert (varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date , TSPL_PURCHASE_ORDER_Detail.Item_Code,TSPL_ITEM_MASTER.Item_Desc  , TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location, " & _
                               " TSPL_PURCHASE_ORDER_Detail.Unit_Code,TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty,   Convert (decimal (18,2),isnull (TBL_SRN.SRN_QTY,0) ) as SRN_QTY , " & _
                               " Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty ) ) )    as Final_Qty_In_Target_UOM    " & _
                               " from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_Detail on TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
                               " left outer join  " & _
                               " (  " & _
                               " select XXXX.PO_ID,XXXX.PO_Qty,XXXX.SRN_QTY, XXXX.Item_Code,XXXX.Unit_code from ( " & _
                               " select TSPL_SRN_DETAIL.PO_ID , Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2),  TSPL_SRN_DETAIL.PO_Qty  ) ) ) as PO_Qty " & _
                               " , Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_SRN_DETAIL.SRN_QTY  ) ) ) as SRN_QTY , TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL. Unit_Code from TSPL_SRN_DETAIL  " & _
                               " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO =TSPL_SRN_DETAIL.SRN_No  " & _
                               " left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_SRN_DETAIL.Item_Code and Source_UOM.Uom_Code = TSPL_SRN_DETAIL.Unit_Code " & _
                               " left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_SRN_DETAIL.Item_Code and Target_Uom.Uom_Code = '" + Uom_Code + "' " & _
                               " where TSPL_SRN_DETAIL.SRN_QTY > 0 and TSPL_SRN_DETAIL.Item_Code in ( '" + ItemCode + "' )  and TSPL_SRN_HEAD.BILL_TO_LOCATION  = '" + Loc_Code + "' ) XXXX    " & _
                               " )  as TBL_SRN on TBL_SRN.PO_ID = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
                               " left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code and Source_UOM.Uom_Code = TSPL_PURCHASE_ORDER_Detail.Unit_Code " & _
                               " left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code and Target_Uom.Uom_Code = '" + Uom_Code + "'  " & _
                               " left outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code " & _
                               " left outer Join TSPL_Vendor_Master on TSPL_Vendor_Master.Vendor_Code = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code " & _
                               " where  " & _
                               " TSPL_PURCHASE_ORDER_HEAD.Status =1   " & _
                               " and TSPL_PURCHASE_ORDER_HEAD.Close_yn = 'N'   " & _
                               " and TSPL_PURCHASE_ORDER_Detail.Item_Code in ('" + ItemCode + "')  " & _
                               " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + Loc_Code + "' " & _
                               "  ) Fianl "
        DblValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return DblValue
    End Function

    Public Shared Function GetPendingSRNkQty(ByVal ItemCode As String, ByVal Uom_Code As String, ByVal Loc_Code As String) As Double
        Dim DblValue As Double = 0
        'Dim qry As String = " select Sum (Fianl.Final_Qty_In_Target_UOM) as Final_Qty_In_Target_UOM from ( " & _
        '                    " select TSPL_SRN_HEAD.SRN_No , TSPL_SRN_DETAIL.Item_Code  , TSPL_SRN_HEAD.Bill_To_Location, TSPL_SRN_DETAIL.Unit_Code,TSPL_SRN_DETAIL.SRN_Qty, " & _
        '                    " Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_SRN_DETAIL.SRN_Qty ) ) )  as Final_Qty_In_Target_UOM   from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No  " & _
        '                    " left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_SRN_DETAIL.Item_Code and Source_UOM.Uom_Code = TSPL_SRN_DETAIL.Unit_Code " & _
        '                    " left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_SRN_DETAIL.Item_Code and Target_Uom.Uom_Code = '" + Uom_Code + "' " & _
        '                    " where  TSPL_SRN_HEAD.Status =0  and TSPL_SRN_DETAIL.Item_Code = '" + ItemCode + "' and TSPL_SRN_HEAD.Bill_To_Location = '" + Loc_Code + "' " & _
        '                    " ) Fianl "
        Dim qry As String = " select sum (Final.Pending_SRN_QTY) as Pending_SRN_QTY from " & _
                            " ( " & _
                            " select TSPL_SRN_DETAIL.PO_ID ,  TSPL_SRN_DETAIL.PO_Qty ,Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_SRN_DETAIL.SRN_QTY  ) ) )  as Pending_SRN_QTY  , TSPL_SRN_DETAIL.Item_Code, TSPL_SRN_DETAIL.Unit_Code " & _
                            " from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO =TSPL_SRN_DETAIL.SRN_No left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_SRN_DETAIL.Item_Code and Source_UOM.Uom_Code = TSPL_SRN_DETAIL.Unit_Code left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_SRN_DETAIL.Item_Code and Target_Uom.Uom_Code = '" + Uom_Code + "'  " & _
                            " where TSPL_SRN_HEAD.Status = 0 and SRN_QTY > 0   and TSPL_SRN_DETAIL.Item_Code = '" + ItemCode + "'  " & _
                            " ) Final "
        DblValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return DblValue
    End Function

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "SELECT MRP_CODE AS Code,MRP_DESCRIPTION AS Description,MRP_FROM AS [From],MRP_TO as [To]," & _
                            " MRP_REMARKS as [Remarks],MRP_LOCATION as [Location] FROM TSPL_PP_MRP_HEAD "

        str = clsCommon.ShowSelectForm("TSPL_PP_MRP_HEAD", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return clsCommon.myCstr(str)
    End Function


    Public Shared Function GetItemLevelQty(ByVal Type As String, ByVal Item_Code As String) As Double
        Dim qty As Double = Nothing
        Dim qry As String = ""
        If clsCommon.CompairString(Type, "Min") = CompairStringResult.Equal Then
            qry = "select min_level from TSPL_ITEM_REORDER_LEVEL_NEW where item_code='" + Item_Code + "'"
        ElseIf clsCommon.CompairString(Type, "Max") = CompairStringResult.Equal Then
            qry = "select max_level from TSPL_ITEM_REORDER_LEVEL_NEW where item_code='" + Item_Code + "'"
        ElseIf clsCommon.CompairString(Type, "ROL") = CompairStringResult.Equal Then
            qry = "select reorder_level from TSPL_ITEM_REORDER_LEVEL_NEW where item_code='" + Item_Code + "'"
        End If
        qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return qty
    End Function

    Public Shared Function GetPendingPOQty(ByVal Item_Code As String) As Double
        Dim qty As Double = Nothing
        Dim qry As String = "select SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty from ( " + Environment.NewLine & _
                        " select TSPL_PURCHASE_ORDER_HEAD.SaleInvoiceNo,TSPL_PURCHASE_ORDER_DETAIL.Bin_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,TSPL_PURCHASE_ORDER_DETAIL.AbatementRate from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_HEAD.close_yn='N' " & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_GRN_DETAIL.GRN_Qty as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=0 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
                " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_PO_SCH_DETAIL.PO_code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code where TSPL_PO_SCH_HEAD.is_post=1 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_PO_SCH_DETAIL.PO_Code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_PO_SCH_DETAIL.schedule_qty as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code  where TSPL_PO_SCH_HEAD.is_post=0 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=1 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=1 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
    " union all  " + Environment.NewLine & _
    " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_DETAIL.rgp_qty as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=0 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine
        qry += " )Final where final.icode='" + Item_Code + "'"
        qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return qty
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionMRP
        Try
            Return GetData(strCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select	MRP_DATE,MRP_Location from TSPL_PP_MRP_HEAD where MRP_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMRPForProduction, clsCommon.myCstr(dt.Rows(0)("MRP_Location")), clsCommon.myCDate(dt.Rows(0)("MRP_DATE")), trans)

            End If

            Dim qry As String

            qry = "DELETE FROM TSPL_PP_MRP_BOM_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_MRP_PRODUCTION_PLAN WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_PP_MRP_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "DELETE FROM TSPL_PP_MRP_BOM_Item_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_PP_MRP_Pending_PO WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "DELETE FROM TSPL_PP_MRP_Pending_SRN WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_PP_MRP_HEAD where MRP_CODE ='" + strCode + "' "
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionMRP
        Try
            Dim obj As New clsProductionMRP()
            'obj.ObjListMRPDetail = New List(Of clsMRPProductionDetail)

            Dim qry As String = "SELECT TSPL_PP_MRP_HEAD.*,tspl_location_master.location_desc FROM TSPL_PP_MRP_HEAD " & _
            "  left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PP_MRP_HEAD.mrp_location " & _
            " where 2=2  "

            Select Case NavType
                Case NavigatorType.First
                    qry += " AND MRP_CODE = (select MIN(MRP_CODE) from TSPL_PP_MRP_HEAD )"
                Case NavigatorType.Last
                    qry += " AND MRP_CODE = (select Max(MRP_CODE) from TSPL_PP_MRP_HEAD )"
                Case NavigatorType.Next
                    qry += " AND MRP_CODE = (select Min(MRP_CODE) from TSPL_PP_MRP_HEAD where   MRP_CODE>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " AND MRP_CODE = (select Max(MRP_CODE) from TSPL_PP_MRP_HEAD where  MRP_CODE<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " AND MRP_CODE = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj.MRP_CODE = clsCommon.myCstr(dt.Rows(0)("MRP_CODE"))
                obj.MRP_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("MRP_DESCRIPTION"))
                obj.MRP_REMARKS = clsCommon.myCstr(dt.Rows(0)("MRP_REMARKS"))
                obj.MRP_DATE = clsCommon.myCDate(dt.Rows(0)("MRP_DATE"))
                obj.MRP_FROM = clsCommon.myCDate(dt.Rows(0)("MRP_FROM"))
                obj.MRP_TO = clsCommon.myCDate(dt.Rows(0)("MRP_To"))
                obj.MRP_Location = clsCommon.myCstr(dt.Rows(0)("MRP_Location"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
                'obj.PROD_PLAN_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_CODE"))
                'obj.PROD_PLAN_DESC = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_DESC"))
                'obj.Include_Stock = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Stock")))
                'obj.Include_Pending_QC = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Pending_QC")))
                'obj.Include_Pending_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Pending_PO")))
                'obj.Include_Item_Level = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Item_Level")))
                obj.Auto_Indent = CInt(clsCommon.myCdbl(dt.Rows(0)("Auto_Indent")))
                obj.Auto_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Auto_PO")))
                obj.Department_Code = clsCommon.myCstr(dt.Rows(0)("Department_Code"))
                obj.MRP_ITEM_TYPE = clsCommon.myCstr(dt.Rows(0)("ITEM_TYPE"))
                'obj.Consider_Open_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Consider_Open_PO")))
                'obj.Auto_Schedule_Open_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Auto_Schedule_Open_PO")))
                strCode = clsCommon.myCstr(dt.Rows(0)("MRP_CODE"))
            End If
            obj.ObjListMRPDetail = clsMRPProductionDetail.GetMRPDetail(strCode, trans)
            obj.ArrProductionPlanCode = clsMRPPlanningCode.GetData(obj.MRP_CODE, trans)
            obj.ObjListMRPBomDetail = clsMRPProductionBOMDetail.GetMRPBomDetail(strCode, trans)
            obj.ObjListMRPBomItemDetail = clsMRPProductionBOMItemDetail.GetMRPBomDetail(strCode, trans)
            obj.ObjListMRPPendingPO = clsMRPProductionPendingPO.GetMRPPendingPODetail(strCode, trans)
            obj.ObjListMRPPendingSRN = clsMRPProductionPendingSRN.GetMRPPendingSRNDetail(strCode, trans)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function SaveData(ByVal obj As clsProductionMRP, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans, strCode)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsProductionMRP, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.MRP_CODE = clsERPFuncationality.GetNextCode(trans, obj.MRP_DATE, clsDocType.MRP, "", obj.MRP_Location)
                strCode = obj.MRP_CODE
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMRPForProduction, obj.MRP_Location, obj.MRP_DATE, trans)
            Dim qry As String
            qry = " DELETE FROM TSPL_MRP_PRODUCTION_PLAN WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " DELETE FROM TSPL_PP_MRP_BOM_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '

            qry = " DELETE FROM TSPL_PP_MRP_BOM_Item_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_PP_MRP_Pending_PO WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "DELETE FROM TSPL_PP_MRP_Pending_SRN WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "DELETE FROM TSPL_PP_MRP_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.MRP_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "MRP_CODE", obj.MRP_CODE)
            clsCommon.AddColumnsForChange(coll, "MRP_DESCRIPTION", obj.MRP_DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "MRP_DATE", clsCommon.GetPrintDate(obj.MRP_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_FROM", clsCommon.GetPrintDate(obj.MRP_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_TO", clsCommon.GetPrintDate(obj.MRP_TO, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_REMARKS", obj.MRP_REMARKS)
            clsCommon.AddColumnsForChange(coll, "MRP_Location", clsCommon.myCstr(obj.MRP_Location), True)
            clsCommon.AddColumnsForChange(coll, "ITEM_TYPE", clsCommon.myCstr(obj.MRP_ITEM_TYPE), True)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            'clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", clsCommon.myCstr(obj.PROD_PLAN_CODE), True)
            'clsCommon.AddColumnsForChange(coll, "Include_Stock", obj.Include_Stock)
            'clsCommon.AddColumnsForChange(coll, "Include_Pending_QC", obj.Include_Pending_QC)
            'clsCommon.AddColumnsForChange(coll, "Include_Pending_PO", obj.Include_Pending_PO)
            'clsCommon.AddColumnsForChange(coll, "Include_Item_Level", obj.Include_Item_Level)
            clsCommon.AddColumnsForChange(coll, "Auto_PO", obj.Auto_PO)
            clsCommon.AddColumnsForChange(coll, "Auto_Indent", obj.Auto_Indent)
            clsCommon.AddColumnsForChange(coll, "Department_Code", obj.Department_Code, True)
            'clsCommon.AddColumnsForChange(coll, "Auto_Schedule_Open_PO", obj.Auto_Schedule_Open_PO)
            'clsCommon.AddColumnsForChange(coll, "Trans_Id", "A_Mobile")

            'clsCommon.AddColumnsForChange(coll, "MRP_DAYS", "0")
            'clsCommon.AddColumnsForChange(coll, "MRP_QTY", "0")


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_HEAD", OMInsertOrUpdate.Update, "  TSPL_PP_MRP_HEAD.MRP_CODE='" + obj.MRP_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMRPPlanningCode.SaveData(obj.MRP_CODE, obj.MRP_DATE, obj.ArrProductionPlanCode, trans)
            isSaved = isSaved AndAlso clsMRPProductionDetail.SaveData(obj.MRP_CODE, obj.ObjListMRPDetail, trans)
            isSaved = isSaved AndAlso clsMRPProductionBOMDetail.SaveData(obj.MRP_CODE, obj.ObjListMRPBomDetail, trans)
            isSaved = isSaved AndAlso clsMRPProductionBOMItemDetail.SaveData(obj.MRP_CODE, obj.ObjListMRPBomItemDetail, trans)
            isSaved = isSaved AndAlso clsMRPProductionPendingPO.SaveData(obj.MRP_CODE, obj.ObjListMRPPendingPO, trans)
            isSaved = isSaved AndAlso clsMRPProductionPendingSRN.SaveData(obj.MRP_CODE, obj.ObjListMRPPendingSRN, trans)
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocno As String, ByVal isAutoIndent As Boolean, ByVal isAutoPO As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso PostData(strDocno, isAutoIndent, isAutoPO, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isAutoIndent As Boolean, ByVal isAutoPO As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsProductionMRP = clsProductionMRP.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.MRP_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMRPForProduction, obj.MRP_Location, obj.MRP_DATE, trans)



            If isAutoIndent Then
                issaved = issaved AndAlso SavePurchaseIndent(obj, trans)
            End If

            Dim qry As String = ""
            'qry = "update TSPL_PP_MRP_DETAIL set Actual_Requird_Qty=dtl.ActualQty from ( " & _
            '     " select MRP_CODE,Item_Code,Unit_Code,sum(case when ActualQty>0 then ActualQty else Qty end) as ActualQty  from TSPL_PP_MRP_PO_DETAIL  where mrp_code='" + strDocNo + "' " & _
            '     " group by MRP_CODE,Item_Code,Unit_Code) dtl where TSPL_PP_MRP_DETAIL.MRP_CODE=dtl.MRP_CODE and TSPL_PP_MRP_DETAIL.Item_Code=dtl.Item_Code " & _
            '     " and TSPL_PP_MRP_DETAIL.RM_UNIT_CODE=dtl.Unit_Code"

            'issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_PP_MRP_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where MRP_CODE ='" + strDocNo + "' "
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            obj = Nothing

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function SavePurchaseIndent(ByVal obj As clsProductionMRP, ByVal trans As SqlTransaction) As Boolean
        Dim objReq As New clsRequistionHead()
        Try
            Dim issaved As Boolean = True

            'Dim qry As String = "select ROW_NUMBER() over (partition by tspl_item_master.item_type order by TSPL_PP_MRP_DETAIL.Item_Code) as sno, TSPL_ITEM_MASTER.Item_Type,TSPL_PP_MRP_DETAIL.* from TSPL_PP_MRP_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_MRP_DETAIL.Item_Code "
            'qry += " where TSPL_PP_MRP_DETAIL.mrp_code='" + obj.MRP_CODE + "'"
            Dim qry As String = " select distinct TSPL_ITEM_MASTER.Item_Type from TSPL_PP_MRP_BOM_Item_DETAIL " & _
                                " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PP_MRP_BOM_Item_DETAIL.Item_Code " & _
                                " where TSPL_PP_MRP_BOM_Item_DETAIL.MRP_CODE = '" + obj.MRP_CODE + "' and TSPL_PP_MRP_BOM_Item_DETAIL.Net_Require_Qty > 0 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    qry = " select TSPL_PP_MRP_HEAD.MRP_CODE , TSPL_PP_MRP_HEAD.MRP_Location, TSPL_PP_MRP_BOM_Item_DETAIL.Item_Code , TSPL_PP_MRP_BOM_Item_DETAIL.UNIT_CODE, TSPL_PP_MRP_BOM_Item_DETAIL.Net_Require_Qty, TSPL_ITEM_MASTER.Item_Type from TSPL_PP_MRP_BOM_Item_DETAIL " & _
                          " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PP_MRP_BOM_Item_DETAIL.Item_Code " & _
                          " Left Outer Join TSPL_PP_MRP_HEAD on TSPL_PP_MRP_HEAD.MRP_CODE = TSPL_PP_MRP_BOM_Item_DETAIL.MRP_CODE " & _
                          " where TSPL_PP_MRP_BOM_Item_DETAIL.MRP_CODE = '" + obj.MRP_CODE + "' and TSPL_PP_MRP_BOM_Item_DETAIL.Net_Require_Qty > 0 and TSPL_ITEM_MASTER.Item_Type = '" + clsCommon.myCstr(dr("Item_Type")) + "' "
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                        objReq = New clsRequistionHead()
                        objReq.ArrTr = New List(Of clsRequistionDetail)
                        objReq.Requisition_Date = clsCommon.GETSERVERDATE(trans)
                        objReq.Cust_OrderNo = Nothing
                        objReq.Expire_Date = clsCommon.GETSERVERDATE(trans).AddYears(1)
                        objReq.Requisition_Date = clsCommon.GETSERVERDATE(trans)
                        objReq.Ref_No = Nothing
                        objReq.Description = obj.MRP_DESCRIPTION
                        objReq.Remarks = obj.MRP_REMARKS
                        objReq.Location = obj.MRP_Location
                        objReq.RQ_Detail_Total_Amt = 0
                        objReq.Mode_Of_Transport = "By Road"
                        objReq.Comments = Nothing
                        objReq.Is_Internal = "N"
                        objReq.Requisition_Type = "L"
                        objReq.Dept = obj.Department_Code  ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code as Code from TSPL_GL_SEGMENT_CODE where Seg_No='3'", trans))
                        objReq.Dept_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_SEGMENT_CODE where Seg_No='3' and Segment_code='" + obj.Department_Code + "'", trans))
                        objReq.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                        objReq.Request_By = objCommonVar.CurrentUserCode
                        objReq.close_yn = "N"
                        Dim line_no As Integer = 0
                        Dim item_code As String = ""
                        objReq.ArrTr = New List(Of clsRequistionDetail)
                        For Each dr2 As DataRow In dt2.Rows
                            Dim objReqDetail As New clsRequistionDetail()
                            objReqDetail.Line_No = line_no + 1
                            objReqDetail.Item_Code = clsCommon.myCstr(dr2("item_code"))
                            objReqDetail.Item_Desc = clsItemMaster.GetItemName(clsCommon.myCstr(dr2("item_code")), trans)
                            objReqDetail.Vendor_Code = Nothing
                            objReqDetail.Requisition_Qty = clsCommon.myCdbl(dr2("Net_Require_Qty"))
                            objReqDetail.Balance_Qty = Nothing
                            objReqDetail.Location = obj.MRP_Location
                            '=================================
                            Dim qryy As String = ""
                            qryy = "select TOP 1 ISNULL(TSPL_SRN_DETAIL.Landed_Cost_Rate,0) from TSPL_SRN_HEAD LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No where Item_Code='" + clsCommon.myCstr(objReqDetail.Item_Code) + "' ORDER BY TSPL_SRN_HEAD.SRN_Date DESC"
                            Dim itemcost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryy, trans))
                            '================================
                            objReqDetail.Item_Cost = itemcost
                            objReqDetail.Status = "N"
                            objReqDetail.Order_No = Nothing
                            objReqDetail.Vendor_ItemNo = Nothing
                            objReqDetail.Item_Net_Amt = clsCommon.myCdbl(objReqDetail.Requisition_Qty * itemcost)
                            objReqDetail.Unit_Code = clsCommon.myCstr(dr2("UNIT_CODE"))
                            objReqDetail.Remarks = "" 'clsCommon.myCstr(dr("Remarks"))
                            objReqDetail.Specification = Nothing
                            If clsCommon.myLen(item_code) <= 0 Then
                                item_code = clsCommon.myCstr(dr2("item_code"))
                            Else
                                item_code = item_code + "','" + clsCommon.myCstr(dr2("item_code"))
                            End If
                            objReq.ArrTr.Add(objReqDetail)
                        Next
                        If objReq IsNot Nothing Then
                            If objReq.SaveData(objReq, True, trans) Then
                                qry = "Update TSPL_PP_MRP_BOM_Item_DETAIL set REQUISITION_ID='" + objReq.Requisition_Id + "' where MRP_CODE ='" + obj.MRP_CODE + "' and item_code in ('" + item_code + "')"
                                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "update TSPL_REQUISITION_HEAD set MRP_CODE = '" + obj.MRP_CODE + "' where Requisition_Id = '" + objReq.Requisition_Id + "' "
                                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        End If
                    End If
                Next
            End If


            '========================================================================================
            ' Dim line_no As Integer = 0
            'Dim item_code As String = ""
            'objReq = New clsRequistionHead()
            'objReq.ArrTr = New List(Of clsRequistionDetail)

            'If obj IsNot Nothing Then

            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        For Each dr As DataRow In dt.Rows
            '            If clsCommon.myCdbl(dr("sno")) = 1 Then 'doing so that itemtype-wise pr made.
            '                If objReq.ArrTr.Count > 0 Then
            '                    If objReq.SaveData(objReq, True, trans) Then
            '                        qry = "Update TSPL_PP_MRP_DETAIL set REQUISITION_ID='" + objReq.Requisition_Id + "' where MRP_CODE ='" + obj.MRP_CODE + "' and item_code in ('" + item_code + "')"
            '                        issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '                    End If
            '                End If

            '                item_code = ""
            '                objReq = New clsRequistionHead()
            '                objReq.ArrTr = New List(Of clsRequistionDetail)
            '            End If

            '            'objReq.Requisition_Date = clsCommon.GETSERVERDATE(trans)
            '            'objReq.Cust_OrderNo = Nothing
            '            'objReq.Expire_Date = clsCommon.GETSERVERDATE(trans).AddYears(1)
            '            'objReq.Requisition_Date = clsCommon.GETSERVERDATE(trans)
            '            'objReq.Ref_No = Nothing
            '            'objReq.Description = obj.MRP_DESCRIPTION
            '            'objReq.Remarks = obj.MRP_REMARKS
            '            'objReq.Location = obj.MRP_Location
            '            'objReq.RQ_Detail_Total_Amt = 0
            '            'objReq.Mode_Of_Transport = "By Road"
            '            'objReq.Comments = Nothing
            '            'objReq.Is_Internal = "N"
            '            'objReq.Requisition_Type = "L"
            '            'objReq.Dept = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code as Code from TSPL_GL_SEGMENT_CODE where Seg_No='3'", trans))
            '            'objReq.Dept_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_SEGMENT_CODE where Seg_No='3' and Segment_code='" + objReq.Dept + "'", trans))
            '            'objReq.Item_Type = clsCommon.myCstr(dr("item_type"))
            '            'objReq.Request_By = objCommonVar.CurrentUserCode
            '            'objReq.close_yn = "N"

            '            Dim objReqDetail As New clsRequistionDetail()

            '            objReqDetail.Line_No = line_no + 1
            '            objReqDetail.Item_Code = clsCommon.myCstr(dr("item_code"))
            '            objReqDetail.Item_Desc = clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), trans)
            '            objReqDetail.Vendor_Code = Nothing
            '            objReqDetail.Requisition_Qty = clsCommon.myCdbl(dr("Net_Requird_Qty"))
            '            objReqDetail.Balance_Qty = Nothing
            '            objReqDetail.Location = obj.MRP_Location
            '            objReqDetail.Item_Cost = 0
            '            objReqDetail.Status = "N"
            '            objReqDetail.Order_No = Nothing
            '            objReqDetail.Vendor_ItemNo = Nothing
            '            objReqDetail.Item_Net_Amt = 0
            '            objReqDetail.Unit_Code = clsCommon.myCstr(dr("RM_UNIT_CODE"))
            '            objReqDetail.Remarks = clsCommon.myCstr(dr("Remarks"))
            '            objReqDetail.Specification = Nothing

            '            item_code = item_code + "','" + clsCommon.myCstr(dr("item_code"))

            '            objReq.ArrTr.Add(objReqDetail)
            '        Next
            '    End If


            'End If 'if for obj cond. check

            'If objReq IsNot Nothing Then
            '    If objReq.SaveData(objReq, True, trans) Then
            '        qry = "Update TSPL_PP_MRP_DETAIL set REQUISITION_ID='" + objReq.Requisition_Id + "' where MRP_CODE ='" + obj.MRP_CODE + "' and item_code in ('" + item_code + "')"
            '        issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    End If
            'End If

            'item_code = Nothing
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objReq = Nothing
        End Try

    End Function

    Public Shared Function GetAutoPODetail(ByVal Item_Code As String) As clsProductionMRP
        Dim obj As New clsProductionMRP()
        obj.Arr_Auto_Po = New List(Of clsProductionMRP)
        Dim qty As Double = Nothing
        Dim qry As String = "select final.vendor,final.rate,SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty from ( " + Environment.NewLine & _
                        " select TSPL_PURCHASE_ORDER_HEAD.SaleInvoiceNo,TSPL_PURCHASE_ORDER_DETAIL.Bin_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,TSPL_PURCHASE_ORDER_DETAIL.AbatementRate from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_HEAD.close_yn='N' " & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_GRN_DETAIL.GRN_Qty as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=0 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
                " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_PO_SCH_DETAIL.PO_code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code where TSPL_PO_SCH_HEAD.is_post=1 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_PO_SCH_DETAIL.PO_Code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_PO_SCH_DETAIL.schedule_qty as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code  where TSPL_PO_SCH_HEAD.is_post=0 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=1 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=1 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
    " union all  " + Environment.NewLine & _
    " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_DETAIL.rgp_qty as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=0 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine
        qry += " )Final where final.icode='" + Item_Code + "' and isnull(final.vendor,'')<>'' group by final.vendor,final.rate"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsProductionMRP()

                objtr.AAuto_Vendor_Code = clsCommon.myCstr(dr("vendor"))
                objtr.AAuto_Vendor_Name = clsVendorMaster.GetName(objtr.AAuto_Vendor_Code, Nothing)
                objtr.AAuto_PO_Rate = clsCommon.myCdbl(dr("rate"))
                objtr.AAuto_Last_Rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 item_cost from TSPL_PURCHASE_ORDER_DETAIL left outer join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=tspl_purchase_order_detail.purchaseorder_no where item_code='" + Item_Code + "' and tspl_purchase_order_head.status='1' and tspl_purchase_order_head.vendor_code='" + clsCommon.myCstr(dr("vendor")) + "' order by tspl_purchase_order_head.purchaseorder_date desc"))
                objtr.AAuto_Avg_Rate = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(item_cost,0))/count(item_code) from TSPL_PURCHASE_ORDER_DETAIL left outer join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=tspl_purchase_order_detail.purchaseorder_no where item_code='" + Item_Code + "' and tspl_purchase_order_head.status='1' and tspl_purchase_order_head.vendor_code='" + clsCommon.myCstr(dr("vendor")) + "'")), 2)

                obj.Arr_Auto_Po.Add(objtr)
            Next
        End If

        Return obj
    End Function
    Public Shared Function GetOpenPO(ByVal Item_Code As String, Optional trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Head.PurchaseOrder_No from TSPL_PURCHASE_ORDER_HEAD Head inner join TSPL_PURCHASE_ORDER_DETAIL dtl on Head.PurchaseOrder_No=dtl.PurchaseOrder_No" & _
                            " where dtl.Item_Code='" & Item_Code & "' and Head.Is_Open_PO=1 and Head.Status=1 "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCode As String = ""
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = 0 Then
                strCode = clsCommon.myCstr(dr.Item("PurchaseOrder_No"))
            Else
                strCode = strCode & ";" & clsCommon.myCstr(dr.Item("PurchaseOrder_No"))
            End If
        Next
        Return strCode
    End Function

    Public Shared Sub ReverseAndUnpost(ByVal strCode As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = ""
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select	MRP_DATE,MRP_Location from TSPL_PP_MRP_HEAD where MRP_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMRPForProduction, clsCommon.myCstr(dt.Rows(0)("MRP_Location")), clsCommon.myCDate(dt.Rows(0)("MRP_DATE")), trans)

            End If
            Qry = " delete from TSPL_REQUISITION_DETAIL where Requisition_Id in (select Requisition_Id from TSPL_REQUISITION_HEAD where MRP_CODE = '" + strCode + "') "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = " delete from TSPL_REQUISITION_HEAD  where MRP_CODE = '" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_PP_MRP_BOM_Item_DETAIL set REQUISITION_ID='' where MRP_CODE ='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_PP_MRP_HEAD set POSTED=0  where MRP_CODE ='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsMRPProductionDetail
#Region "Variables"

    Public MRP_CODE As String = Nothing
    Public Line_No As String = Nothing
    Public PLAN_CODE As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public UNIT_CODE As String = Nothing
    Public Qty As Double = Nothing
    Public BOM As String = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPProductionDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMRPProductionDetail In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "PLAN_CODE", obj.PLAN_CODE)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PP_MRP_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
                Next

            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMRPDetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPProductionDetail)
        Dim dt As New DataTable
        Try

            Dim stock_qty As Decimal = Nothing
            Dim qry As String = ""
            qry = "SELECT TSPL_PP_MRP_DETAIL.*,tspl_item_master.item_desc as itmdesc FROM TSPL_PP_MRP_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_MRP_DETAIL.item_code where TSPL_PP_MRP_DETAIL.MRP_CODE='" & MRP_CODE & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            Dim objtr As clsMRPProductionDetail
            Dim ObjList As New List(Of clsMRPProductionDetail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMRPProductionDetail()

                    objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.PLAN_CODE = clsCommon.myCstr(dr("PLAN_CODE"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("itmdesc"))
                    objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                    objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objtr.BOM = clsCommon.myCstr(dr("BOM_CODE"))
                    '=============get stock_qty from po detail table is posted otherwise from detail table,so that no chnge in calc. reflect on screen.
                    'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_PP_MRP_PO_DETAIL where mrp_code='" + objtr.MRP_CODE + "' and item_code='" + objtr.Item_Code + "'", trans)) Then
                    '    stock_qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 stock_qty from TSPL_PP_MRP_PO_DETAIL where mrp_code='" + objtr.MRP_CODE + "' and item_code='" + objtr.Item_Code + "'", trans))
                    '    objtr.Stock_Qty = stock_qty
                    'Else
                    '    objtr.Stock_Qty = clsCommon.myCdbl(dr("stock_qty"))
                    'End If
                    'objtr.Actual_Requird_Qty = clsCommon.myCdbl(dr("Actual_Requird_Qty"))
                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try

    End Function

End Class

Public Class clsMRPProductionPODetail
#Region "variables"
    Public MRP_CODE As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public PurchaseOrder_Date As DateTime = Nothing
    Public Bill_To_Location As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Net_Req_Qty As Double = Nothing
    Public MRP_PO_Rate As Double = Nothing
    Public PO_Last_Rate As Double = Nothing
    Public PO_Avg_Rate As Double = Nothing
    Public Item_Cost As Double = Nothing
    Public Qty As Double = Nothing
    Public Remarks As String = Nothing
    Public Stock_Qty As Decimal = Nothing

    Public OpenPONO As String = Nothing
    Public OpenPODate As String = Nothing
    Public MOQ As Double = 0
    Public SPQ As Double = 0
    Public SOB As Double = 0
    Public SOBQty As Double = 0
    Public ActualQty As Double = 0
    Public ScheduleType As String = Nothing

    Public Arr As List(Of clsMRPProductionPODetail) = Nothing
#End Region

    Public Shared Function GetBOMOtherItems(ByVal ItemCode As String) As String
        Dim str As String = ""
        Dim qry As String = " select TOP 1 BOM_CODE from TSPL_PP_BOM_HEAD where PROD_ITEM_CODE = '" + ItemCode + "' and TSPL_PP_BOM_HEAD.Is_Post = 1 and TSPL_PP_BOM_HEAD.STATUS = 'Approved'  and  Convert (datetime, GETDATE(),103) > = CONVERT (datetime , TSPL_PP_BOM_HEAD.Valid_FROM_DATE , 103)   and  Convert (datetime ,GETDATE(),103) < = CONVERT (datetime , TSPL_PP_BOM_HEAD.Valid_UPTO_DATE , 103)  order by CONVERT (datetime , Created_Date , 103) , BOM_CODE desc "
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRPProductionPODetail, ByVal MRP_CODE As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, MRP_CODE, trans)

            trans.Commit()

            trans = clsDBFuncationality.GetTransactin()

            isSaved = isSaved AndAlso CreateAutoPO(MRP_CODE, trans)
            'isSaved = isSaved AndAlso clsProductionMRP.PostData(MRP_CODE, False, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()

            Dim qry As String = "delete from TSPL_PP_MRP_PO_DETAIL where mrp_code='" + MRP_CODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRPProductionPODetail, ByVal MRP_CODE As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PP_MRP_PO_DETAIL where mrp_code='" + MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsMRPProductionPODetail In obj.Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", objtr.MRP_CODE)
                    'clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", objtr.PurchaseOrder_No)
                    'clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Date", objtr.PurchaseOrder_Date)
                    clsCommon.AddColumnsForChange(coll, "Bill_To_Location", objtr.Bill_To_Location)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", objtr.Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Name", objtr.Vendor_Name)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Net_Req_Qty", objtr.Net_Req_Qty)
                    clsCommon.AddColumnsForChange(coll, "MRP_PO_Rate", objtr.MRP_PO_Rate)
                    clsCommon.AddColumnsForChange(coll, "PO_Last_Rate", objtr.PO_Last_Rate)
                    clsCommon.AddColumnsForChange(coll, "PO_Avg_Rate", objtr.PO_Avg_Rate)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "stock_qty", objtr.Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)

                    clsCommon.AddColumnsForChange(coll, "OpenPONO", objtr.OpenPONO, True)
                    clsCommon.AddColumnsForChange(coll, "OpenPODate", objtr.OpenPODate, True)
                    clsCommon.AddColumnsForChange(coll, "MOQ", objtr.MOQ)
                    clsCommon.AddColumnsForChange(coll, "SPQ", objtr.SPQ)
                    clsCommon.AddColumnsForChange(coll, "SOB", objtr.SOB)
                    clsCommon.AddColumnsForChange(coll, "SOBQty", objtr.SOBQty)
                    clsCommon.AddColumnsForChange(coll, "ActualQty", objtr.ActualQty)
                    clsCommon.AddColumnsForChange(coll, "ScheduleType", objtr.ScheduleType, True)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_PO_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CreateAutoPO(ByVal MRP_CODE As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsPurchaseOrderHead()
        Dim objOpenPO As New clsPurchaseOrderHead()
        Try
            Dim isSaved As Boolean = True
            Dim objMRP As clsProductionMRP = clsProductionMRP.GetData(MRP_CODE, NavigatorType.Current, trans)
            Dim qry As String = "select final.* from (select row_number() over (partition by OpenPONO,tspl_item_master.item_type,TSPL_PP_MRP_PO_DETAIL.vendor_code,TSPL_PP_MRP_PO_DETAIL.ScheduleType order by TSPL_PP_MRP_PO_DETAIL.OpenPONO,tspl_item_master.item_type,TSPL_PP_MRP_PO_DETAIL.vendor_code,TSPL_PP_MRP_PO_DETAIL.ScheduleType) as sno,tspl_item_master.item_type,TSPL_PP_MRP_PO_DETAIL.* from TSPL_PP_MRP_PO_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_MRP_PO_DETAIL.Item_Code where mrp_code='" + MRP_CODE + "')final order by final.OpenPONO,final.item_type,final.vendor_code,final.ScheduleType"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim Comparision_Sno As Decimal = 0
            Dim item_code As String = ""
            Dim line_no As Integer = 0
            Dim vendr_code As String = Nothing

            obj = New clsPurchaseOrderHead()
            obj.Arr = New List(Of clsPurchaseOrderDetail)

            objOpenPO = New clsPurchaseOrderHead()
            objOpenPO.Arr = New List(Of clsPurchaseOrderDetail)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows

                    If clsCommon.myCdbl(dr("sno")) = 1 Then 'beacuse ther is multi line data of same vendor and merge it in one array for single po
                        Comparision_Sno = clsCommon.myCdbl(dr("sno"))
                        line_no = 0
                        If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                            isSaved = isSaved AndAlso obj.SaveData(obj, True, False, trans)
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_PP_MRP_PO_DETAIL set purchaseorder_no='" + obj.PurchaseOrder_No + "',purchaseorder_date='" + clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") + "' where mrp_code='" + MRP_CODE + "' and vendor_code='" + vendr_code + "' and item_code in ('" + item_code + "')", trans)
                        End If
                        If objOpenPO IsNot Nothing AndAlso objOpenPO.Arr.Count > 0 Then
                            clsPurchaseOrderHead.SaveScheduleData(objOpenPO, objOpenPO.Schedule_Type, objMRP.MRP_FROM, trans)
                        End If

                        item_code = ""
                        obj = New clsPurchaseOrderHead()
                        obj.Arr = New List(Of clsPurchaseOrderDetail)

                        objOpenPO = New clsPurchaseOrderHead()
                        objOpenPO.Arr = New List(Of clsPurchaseOrderDetail)
                    End If
                    obj.PurchaseOrder_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE(trans))
                    obj.Delivery_date = obj.PurchaseOrder_Date
                    obj.Delivery_Duration = Nothing
                    obj.PurchaseOrder_Type = "L" 'domestic
                    obj.Against_RGP = "0"
                    obj.Against_Vendor_Quotation = Nothing
                    obj.Is_Open_PO = "0"
                    obj.Vendor_Code = clsCommon.myCstr(dr("vendor_code"))
                    vendr_code = clsCommon.myCstr(dr("vendor_code"))
                    obj.Vendor_Name = clsCommon.myCstr(dr("vendor_name"))
                    obj.On_Hold = False
                    obj.Ref_No = Nothing
                    obj.Remarks = clsCommon.myCstr(dr("remarks"))
                    obj.Description = "Auto PO from MRP(STD) having code: " + MRP_CODE + ""
                    obj.Bill_To_Location = clsCommon.myCstr(dr("bill_to_location"))
                    obj.Ship_To_Location = Nothing
                    obj.Tax_Group = GetDefaultTaxGroup(clsCommon.myCstr(dr("bill_to_location")), clsCommon.myCstr(dr("vendor_code")), "P", trans)
                    obj.Mode_Of_Transport = "By Road"
                    obj.Item_Type = clsItemMaster.GetItemType(clsCommon.myCstr(dr("item_code")), trans)
                    'obj.Auto_PO = "1"
                    Dim objtr As New clsPurchaseOrderDetail()
                    objtr.Line_No = line_no + 1
                    objtr.Row_Type = "Item"
                    objtr.Item_Code = clsCommon.myCstr(dr("item_code"))
                    objtr.Item_Desc = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                    objtr.PurchaseOrder_Qty = clsCommon.myCdbl(dr("qty"))
                    objtr.Unit_code = clsCommon.myCstr(dr("unit_code"))
                    objtr.Location = clsCommon.myCstr(dr("bill_to_location"))
                    objtr.Item_Cost = clsCommon.myCdbl(dr("item_cost"))
                    objtr.Amount = clsCommon.myCdbl(objtr.Item_Cost) * clsCommon.myCdbl(objtr.PurchaseOrder_Qty)
                    objtr.Disc_Per = 0
                    objtr.Disc_Amt = 0
                    objtr.Amt_Less_Discount = objtr.Amount
                    objtr.Item_Net_Amt = objtr.Amount
                    objtr.Remarks = clsCommon.myCstr(dr("remarks"))
                    objtr.Last_Same_Vendor_Rate = clsCommon.myCdbl(dr("PO_Last_Rate"))
                    item_code = item_code + "','" + clsCommon.myCstr(dr("item_code"))

                    '' Open PO columns

                    If clsCommon.myLen(clsCommon.myCstr(dr.Item("OpenPONO"))) <= 0 Then
                        obj.Arr.Add(objtr)
                    Else
                        Dim SchType As String = "W"
                        If objOpenPO Is Nothing OrElse objOpenPO.Arr.Count <= 0 Then
                            objOpenPO = obj.Clone()
                        End If

                        objOpenPO.PurchaseOrder_No = clsCommon.myCstr(dr.Item("OpenPONO"))
                        objtr.PurchaseOrder_Qty = clsCommon.myCdbl(dr.Item("ActualQty"))
                        If clsCommon.CompairString(clsCommon.myCstr(dr.Item("ScheduleType")), "Monthly") = CompairStringResult.Equal Then
                            SchType = "M"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("ScheduleType")), "Weekly") = CompairStringResult.Equal Then
                            SchType = "W"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("ScheduleType")), "Daily") = CompairStringResult.Equal Then
                            SchType = "D"
                        End If
                        objOpenPO.Schedule_Type = SchType
                        objOpenPO.Arr.Add(objtr)

                        'clsPurchaseOrderHead.SaveScheduleData(objOpenPO, SchType, objMRP.MRP_FROM, trans)
                    End If

                Next
                If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    isSaved = isSaved AndAlso obj.SaveData(obj, True, False, trans)
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_PP_MRP_PO_DETAIL set purchaseorder_no='" + obj.PurchaseOrder_No + "',purchaseorder_date='" + clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") + "' where mrp_code='" + MRP_CODE + "' and vendor_code='" + vendr_code + "' and item_code in ('" + item_code + "')", trans)
                End If
                If objOpenPO IsNot Nothing AndAlso objOpenPO.Arr.Count > 0 Then
                    clsPurchaseOrderHead.SaveScheduleData(objOpenPO, objOpenPO.Schedule_Type, objMRP.MRP_FROM, trans)
                End If
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function

    Public Shared Function GetDefaultTaxGroup(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group=1 and  Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "

        If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
            qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
        ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
            qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
        Else
            Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
        End If
        qry += " )x) "
        qry += " group by Tax_Group_Code"

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
End Class
Public Class clsMRPProductionOpenPOItemDetail
#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public OpenPONo As String = Nothing
    Public OpenPODate As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public OpenPoRate As Double = 0
    Public Unit_Code As String = Nothing
    Public Unit_Rate As Double = 0
    Public Vendor_Unit_Rate As Double = 0
    Public TOTAL_STD_DAY As Decimal = 0
    Public MOQ As Double = 0
    Public SPQ As Double = 0
    Public SOB As Double = 0
#End Region
    Public Shared Function GetOpenPODetail(ByVal PO_No As String, ByVal ItemCode As String, Optional ByVal trans As SqlTransaction = Nothing) As clsMRPProductionOpenPOItemDetail
        Dim obj As New clsMRPProductionOpenPOItemDetail
        Dim qry As String = " select PO.PurchaseOrder_No,convert(varchar,PO.PurchaseOrder_Date,103) as PurchaseOrder_Date,PO.Vendor_Code,PO.Vendor_Name,PO.Item_Code,PO.Item_Desc,PO.Unit_code,PO.Item_Cost, " & _
                            " VID.vendor_code as Vendor_Code_Map,VID.Min_Order_Qty,VID.SP_QTY,VID.SOB,VID.item_rate as Vendor_Unit_Rate,VID.TOTAL_STD_DAY from (" & _
                            " select Head.PurchaseOrder_No,Head.PurchaseOrder_Date,Head.Vendor_Code,Head.Vendor_Name,dtl.Item_Code,dtl.Item_Desc,dtl.Unit_code,dtl.Item_Cost " & _
                            " from TSPL_PURCHASE_ORDER_HEAD Head inner join TSPL_PURCHASE_ORDER_DETAIL dtl on Head.PurchaseOrder_No=dtl.PurchaseOrder_No " & _
                            " where dtl.Item_Code='" & ItemCode & "' and Head.PurchaseOrder_No='" & PO_No & "' ) as PO " & _
                            " left join TSPL_VENDOR_ITEM_DETAIL VID on PO.Vendor_Code=VID.vendor_code and po.Item_Code=VID.item_no "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0).Item("Vendor_Code_Map"))) <= 0 Then
                Throw New Exception("Item : " & ItemCode & " is not mapped with any vendor. ")
            End If

            obj.OpenPONo = clsCommon.myCstr(dt.Rows(0).Item("PurchaseOrder_No"))
            obj.OpenPODate = clsCommon.myCstr(dt.Rows(0).Item("PurchaseOrder_Date"))
            obj.OpenPoRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Cost"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0).Item("Unit_code"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0).Item("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0).Item("Vendor_Name"))

            obj.Item_Code = clsCommon.myCstr(dt.Rows(0).Item("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0).Item("Item_Desc"))
            obj.Unit_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Cost"))
            obj.Vendor_Unit_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Vendor_Unit_Rate"))
            obj.TOTAL_STD_DAY = clsCommon.myCdbl(dt.Rows(0).Item("TOTAL_STD_DAY"))
            obj.MOQ = clsCommon.myCdbl(dt.Rows(0).Item("Min_Order_Qty"))
            obj.SPQ = clsCommon.myCdbl(dt.Rows(0).Item("SP_QTY"))
            obj.SOB = clsCommon.myCdbl(dt.Rows(0).Item("SOB"))
        End If
        Return obj
    End Function

End Class

Public Class clsMRPPlanningCode
#Region "variables"
    Public TR_CODE As String = Nothing
    Public MRP_CODE As String = Nothing
    Public PLAN_CODE As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal dtDocDate As DateTime, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each strItemStrctCode As String In Arr
                    Dim coll As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", strTRCode)
                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "PLAN_CODE", strItemStrctCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_PRODUCTION_PLAN", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "select Plan_Code from TSPL_PP_MRP_DETAIL  where MRP_Code = '" + strCode + "' and Plan_Code <> '' "  ' "Select TSPL_MRP_PRODUCTION_PLAN.* from TSPL_MRP_PRODUCTION_PLAN Where MRP_CODE='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("PLAN_CODE")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMRPProductionBOMDetail
#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public Line_No As String = Nothing
    Public Main_ITEM_CODE As String = Nothing
    Public Main_Item_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public UNIT_CODE As String = Nothing
    Public Total_Qty_Required As Double = Nothing
    'Public Stock_Qty As Double = Nothing
    'Public Pending_PO_Qty As Double = Nothing
    'Public Pending_SRN_Qty As Decimal = Nothing
    'Public Net_Require_Qty As Decimal = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPProductionBOMDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMRPProductionBOMDetail In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Main_ITEM_CODE", obj.Main_ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "Total_Qty_Required", obj.Total_Qty_Required)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_BOM_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PP_MRP_BOM_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
                Next

            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMRPBomDetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPProductionBOMDetail)
        Dim dt As New DataTable
        Try

            Dim stock_qty As Decimal = Nothing
            Dim qry As String = ""
            qry = "SELECT TSPL_PP_MRP_BOM_DETAIL.*,tspl_item_master.item_desc as itmdesc, TSPL_ITEM_MASTER_For_Main_ITEM.item_desc as MainItemDesc FROM TSPL_PP_MRP_BOM_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_MRP_BOM_DETAIL.item_code left outer join TSPL_ITEM_MASTER as TSPL_ITEM_MASTER_For_Main_ITEM  on TSPL_ITEM_MASTER_For_Main_ITEM.Item_Code = TSPL_PP_MRP_BOM_DETAIL.Main_Item_Code where TSPL_PP_MRP_BOM_DETAIL.MRP_CODE='" & MRP_CODE & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            Dim objtr As clsMRPProductionBOMDetail
            Dim ObjList As New List(Of clsMRPProductionBOMDetail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMRPProductionBOMDetail()

                    objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.Main_ITEM_CODE = clsCommon.myCstr(dr("Main_ITEM_CODE"))
                    objtr.Main_Item_Desc = clsCommon.myCstr(dr("MainItemDesc"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("itmdesc"))
                    objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                    objtr.Total_Qty_Required = clsCommon.myCdbl(dr("Total_Qty_Required"))
                    'objtr.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                    'objtr.Pending_PO_Qty = clsCommon.myCdbl(dr("Pending_PO_Qty"))
                    'objtr.Pending_SRN_Qty = clsCommon.myCdbl(dr("Pending_SRN_Qty"))
                    'objtr.Net_Require_Qty = clsCommon.myCdbl(dr("Net_Require_Qty"))
                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try

    End Function

End Class

'========================================= Requeried Item Qty================================================================


Public Class clsMRPProductionBOMItemDetail
#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public Line_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public UNIT_CODE As String = Nothing
    Public Total_Qty_Required As Double = Nothing
    Public Stock_Qty As Double = Nothing
    Public Pending_PO_Qty As Double = Nothing
    Public Pending_SRN_Qty As Decimal = Nothing
    Public Net_Require_Qty As Decimal = Nothing
    Public Requisition_Id As String = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPProductionBOMItemDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMRPProductionBOMItemDetail In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "Total_Qty_Required", obj.Total_Qty_Required)
                    clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Pending_PO_Qty", obj.Pending_PO_Qty)
                    clsCommon.AddColumnsForChange(coll, "Pending_SRN_Qty", obj.Pending_SRN_Qty)
                    clsCommon.AddColumnsForChange(coll, "Net_Require_Qty", obj.Net_Require_Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_BOM_ITEM_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PP_MRP_BOM_ITEM_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
                Next

            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMRPBomDetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPProductionBOMItemDetail)
        Dim dt As New DataTable
        Try
            Dim stock_qty As Decimal = Nothing
            Dim qry As String = ""
            qry = "SELECT TSPL_PP_MRP_BOM_ITEM_DETAIL.*,tspl_item_master.item_desc as itmdesc FROM TSPL_PP_MRP_BOM_ITEM_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_MRP_BOM_ITEM_DETAIL.item_code where TSPL_PP_MRP_BOM_ITEM_DETAIL.MRP_CODE='" & MRP_CODE & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim objtr As clsMRPProductionBOMItemDetail
            Dim ObjList As New List(Of clsMRPProductionBOMItemDetail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMRPProductionBOMItemDetail()
                    objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("itmdesc"))
                    objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                    objtr.Total_Qty_Required = clsCommon.myCdbl(dr("Total_Qty_Required"))
                    objtr.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                    objtr.Pending_PO_Qty = clsCommon.myCdbl(dr("Pending_PO_Qty"))
                    objtr.Pending_SRN_Qty = clsCommon.myCdbl(dr("Pending_SRN_Qty"))
                    objtr.Net_Require_Qty = clsCommon.myCdbl(dr("Net_Require_Qty"))
                    objtr.Requisition_Id = clsCommon.myCstr(dr("Requisition_Id"))
                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try

    End Function

End Class

'==================================================Pending PO ==========================================================================
Public Class clsMRPProductionPendingPO
#Region "Variables"
    ' PurchaseOrder_No , PurchaseOrder_Date,Vendor_Code,Bill_To_Location, Item_Code , Unit_Code, PurchaseOrder_Qty
    Public MRP_CODE As String = Nothing
    Public Line_No As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public PurchaseOrder_Date As Date = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public Location_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public UNIT_CODE As String = Nothing
    Public PurchaseOrder_Qty As Double = Nothing
    Public GRN_QTY As Double = Nothing
    Public MRN_QTY As Double = Nothing
    Public SRN_Qty As Double = Nothing
    Public Pending_PO_Qty As Double = Nothing
    ' GRN_QTY, MRN_QTY,SRN_Qty,Pending_PO_Qty
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPProductionPendingPO), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMRPProductionPendingPO In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No)
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Date", clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Qty", obj.PurchaseOrder_Qty)
                    clsCommon.AddColumnsForChange(coll, "GRN_QTY", obj.GRN_QTY)
                    clsCommon.AddColumnsForChange(coll, "MRN_QTY", obj.MRN_QTY)
                    clsCommon.AddColumnsForChange(coll, "SRN_Qty", obj.SRN_Qty)
                    clsCommon.AddColumnsForChange(coll, "Pending_PO_Qty", obj.Pending_PO_Qty)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_Pending_PO", OMInsertOrUpdate.Insert, "TSPL_PP_MRP_Pending_PO.MRP_CODE='" + strDocNo + "' ", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMRPPendingPODetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPProductionPendingPO)
        Dim dt As New DataTable
        Try

            Dim stock_qty As Decimal = Nothing
            Dim qry As String = ""
            qry = "SELECT TSPL_PP_MRP_Pending_PO.*,tspl_item_master.item_desc as itmdesc, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VENDOR_MASTER.Vendor_Name FROM TSPL_PP_MRP_Pending_PO left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_MRP_Pending_PO.item_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_PP_MRP_Pending_PO.Bill_To_Location left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PP_MRP_Pending_PO.Vendor_Code   where TSPL_PP_MRP_Pending_PO.MRP_CODE='" & MRP_CODE & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim objtr As clsMRPProductionPendingPO
            Dim ObjList As New List(Of clsMRPProductionPendingPO)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMRPProductionPendingPO()

                    objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.PurchaseOrder_No = clsCommon.myCstr(dr("PurchaseOrder_No"))
                    objtr.PurchaseOrder_Date = clsCommon.myCDate(dr("PurchaseOrder_Date"))
                    objtr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objtr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objtr.Bill_To_Location = clsCommon.myCstr(dr("Bill_To_Location"))
                    objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("itmdesc"))
                    objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                    objtr.PurchaseOrder_Qty = clsCommon.myCdbl(dr("PurchaseOrder_Qty"))
                    objtr.GRN_QTY = clsCommon.myCdbl(dr("GRN_QTY"))
                    objtr.MRN_QTY = clsCommon.myCdbl(dr("MRN_QTY"))
                    objtr.SRN_Qty = clsCommon.myCdbl(dr("SRN_Qty"))
                    objtr.Pending_PO_Qty = clsCommon.myCdbl(dr("Pending_PO_Qty"))
                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Function

End Class

' ================================ SRN Pending Qty ===================================================

Public Class clsMRPProductionPendingSRN
#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public Line_No As String = Nothing
    Public SRN_No As String = Nothing
    Public SRN_DATE As Date = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public Location_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public UNIT_CODE As String = Nothing
    Public SRN_Qty As Double = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPProductionPendingSRN), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMRPProductionPendingSRN In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
                    clsCommon.AddColumnsForChange(coll, "SRN_DATE", clsCommon.GetPrintDate(obj.SRN_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "SRN_Qty", obj.SRN_Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_MRP_Pending_SRN", OMInsertOrUpdate.Insert, "TSPL_PP_MRP_Pending_SRN.MRP_CODE='" + strDocNo + "' ", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMRPPendingSRNDetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPProductionPendingSRN)
        Dim dt As New DataTable
        Try

            Dim stock_qty As Decimal = Nothing
            Dim qry As String = ""
            qry = "SELECT TSPL_PP_MRP_Pending_SRN.*,tspl_item_master.item_desc as itmdesc, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VENDOR_MASTER.Vendor_Name FROM TSPL_PP_MRP_Pending_SRN left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_MRP_Pending_SRN.item_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_PP_MRP_Pending_SRN.Bill_To_Location left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PP_MRP_Pending_SRN.Vendor_Code   where TSPL_PP_MRP_Pending_SRN.MRP_CODE='" & MRP_CODE & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim objtr As clsMRPProductionPendingSRN
            Dim ObjList As New List(Of clsMRPProductionPendingSRN)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMRPProductionPendingSRN()

                    objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.SRN_No = clsCommon.myCstr(dr("SRN_No"))
                    objtr.SRN_DATE = clsCommon.myCDate(dr("SRN_DATE"))
                    objtr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objtr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objtr.Bill_To_Location = clsCommon.myCstr(dr("Bill_To_Location"))
                    objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("itmdesc"))
                    objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                    objtr.SRN_Qty = clsCommon.myCdbl(dr("SRN_Qty"))
                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Function

End Class