Imports System.Data.SqlClient
Public Class clsItemMaster
#Region "Variables"
    Public FG_for_CF As Integer = 0
    Public BomBuildQty As Double = 0
    Public NIR_QC As Boolean = False
    Public AllowSRNWithoutShortReject As Integer = 0
    Public Is_Scheme_Item As Boolean = False
    Public Distributor_Commission As Decimal = Nothing
    Public CNF_Commission As Decimal = Nothing
    Public Correction_Factor As Decimal = Nothing
    Public Is_Batch_Item As Boolean = False
    Public RAL As Boolean = False
    Public Is_Rate_Change_OnDairyDispatch As Integer = 0
    Public Is_QC_SNF_Based As Integer = 0
    Public Is_AllowQC_ON_Production As Integer = 0
    Public FG_for_CF_RPT As Integer = 0
    Public Cust_Account As String = Nothing
    Public Cust_Account_Name As String = Nothing
    Public Part_No As String = Nothing
    Public Drawing_No As String = Nothing
    Public std_pur_rate As Decimal = Nothing
    Public Item_Code As String = ""
    Public Item_Desc As String = ""
    Public Item_Short_Desc As String = ""
    Public Unit_Code As String = ""
    Public uom_code As String = ""
    Public Structure_Code As String = ""
    Public Structure_Desc As String = ""
    Public Purchase_Class_Code As String = ""
    Public Sale_Class_Code As String = ""
    Public Cheapter_Heads As String = ""
    Public item_category As String = ""
    Public Sub_item_category As String = ""
    Public TypeOfItm As String = ""
    Public DcsSeqNo As Double = 0
    Public Item_Type As String = ""
    Public Morning As Boolean = False
    Public IsTaxable As Boolean = False
    Public Cost As Double = 0
    Public Tolerance As Double = 0
    Public Production_Tolerance As Double = 0
    Public Rate As Double = 0
    Public Active As Boolean = False
    Public AlternativeItem As String = ""
    Public ItemSpecification As String = ""
    Public Modify_Date As String = ""
    Public Item_Category_Struct_Code As String = ""
    Public SubItemType As String = ""
    Public Asset_Life As String = ""
    Public Warranty_period = ""
    Public Is_Serial_Item As Boolean = False
    Public Is_Pick_Auto_SrNo As Boolean = False
    Public Serial_Counter As String = ""
    Public Warranty_Code As String = ""
    Public Warranty_Name As String = ""
    Public Weight_UOM As String = ""
    Public Rack_No As String = ""
    Public Weight_Value As Double = 0
    Public ITFCode As String
    Public Is_MRP As Boolean = False
    Public Is_FreshItem As Boolean = False
    Public Is_Ambient As Boolean = False
    Public Tax_Exempted As Integer = 0
    Public Sku_Seq As Int64 = 0
    Public Is_DisplayDemand As Boolean = False
    Public Is_ExcludeAPP As Boolean = False
    Public shelflife As String = Nothing
    Public Min_shelf_life As String = Nothing
    Public RemainingQtyToPurchase As Double = 0
    Public ArrUomDetails As List(Of clsItemUOMDetails) = Nothing
    Public ArrItemMasterCategory As List(Of clsItemMasterCategory) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Product_Type As String = ""
    Public CSA_Type As String = ""
    Public paramcode As String = Nothing
    Public paramdesc As String = Nothing
    Public Lrange As Decimal = Nothing
    Public Urange As Decimal = Nothing
    Public status As String = Nothing
    Public value1 As String = Nothing
    Public value2 As String = Nothing
    Public Actual_range As Decimal = Nothing
    Public Posted As Decimal = Nothing
    Public actual_value As String = Nothing
    Public StandardRate As Decimal = 0
    Public actual_status As String = Nothing
    Public Arr_Param As List(Of clsItemMaster) = Nothing
    '=======Rohit=====================
    Public Is_Purchaseable_item As String = Nothing
    Public Is_Allow_QC As String = Nothing
    '==========================================
    '' Anubhooti 11-Sep-2014
    Public Is_CrateType As Boolean = False
    Public GL_Account As String = Nothing
    '=========Rohit Nov 18,2014 --add Used as (Mcc Sale,Mcc Issue) field============
    Public Item_used_as As String = String.Empty
    Public CreateSepAssetForEachQty As String = "1"
    Public Warranty_Applied_From As String = String.Empty
    Public Is_Auto_Weighment As Boolean = False
    Public HSNCode As String = Nothing
    Public Skip_GST As Boolean = False
    Public Is_Power_And_Fuel As Boolean = False
    Public STD_FatPer As Decimal = 0
    Public STD_SNFPer As Decimal = 0
    Public Chilled_Freezen As Boolean = False
    Public Alies_Name As String = ""
    Public Alies_Name2 As String = ""
    Public Alies_Name3 As String = ""
    Public Crate As Boolean = False
    Public Can As Boolean = False
    Public Is_CAN_Type As Boolean = False
    Public Is_Scrap_Item As Boolean = False
    Public Scrap_Item_Code As String = Nothing
    Public Is_Leakage_Not_Applicable As Boolean = False
    Public Is_Milk_Pouch As Boolean = False
    Public Is_Advance_Required As Boolean = False
    Public Is_Insurance As Integer = 0
    Public InsuranceNo As String = ""
    Public InsuranceFromDate As Date?
    Public InsuranceToDate As Date?
    Public Marketing_Seq As Int64 = 0
    Public Arr_Purchase_QC_Parameter As List(Of clsItemPurchaseQCParameter) = Nothing
    Public ArrSchedule As List(Of clsItemSchedule) = Nothing
    Public ApplyRoundingInStdProd As Boolean = False
    Public Visual_QC As Boolean = False
    Public Security_Deduction As Decimal
    Public Item_Desc_Hindi As String = Nothing
    Public Item_Short_Desc_Hindi As String = Nothing
    Public Alies_Name_Hindi As String = Nothing
    Public BuyBackType As Integer = 0
    Public BuyBackValue As Decimal = 0
#End Region
    ''Richa 20201616
    '==================================
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetDCSItemFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_UOM_DETAIL.UOM_Code from TSPL_ITEM_MASTER
inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  
inner join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code"
        Dim whr As String = "TSPL_ITEM_MASTER.FG_for_CF=1 and TSPL_UNIT_MASTER.Box_Type='Y'"
        strCode = clsCommon.ShowSelectForm("DCSISFnd", qry, "Item_Code", whr, strCode, "Item_Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = New clsItemMaster
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where " + whr + " and TSPL_ITEM_MASTER.Item_Code='" + strCode + "'")
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.uom_code = clsCommon.myCstr(dt.Rows(0)("UOM_Code"))
        End If
        Return obj
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Else
            whrcls = whrcls ' + " and comp_code='" + objCommonVar.CurrentCompanyCode + "'" ' & "  and Active='1' "because in master all items should show whether it is active or inactive but in transaction only active items come
        End If
        Dim pivotheader As String = ""
        pivotheader = GetNLevelPivotHeader()
        Dim qry As String = " select Item_Code as [Code] ,Item_Desc as [Item Description],Sku_Seq as [Seq. No.],ISNULL(Short_Description,'') AS Short_Description,ISNULL(CSA_TYPE,'') AS [Item Group Type],case when Is_Tax_Exempted=0 then 'None' when Is_Tax_Exempted=1 then 'Tax' when Is_Tax_Exempted=2 then 'Excise' end as TaxType ,Drawing_no as [Drawing No],Part_No as [Part No] ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price] ,Father_Code as [Father Code] ,Father_QTy as [Father Quantity] ,Cheapter_Heads as [Cheapter Heads] ,Mother_Code as [Mother Code] ,Mother_Qty as [Mother Quantity] ,Opening_Balance as [Opening Balance] ,Two_Count_Status as [Two Count Status] ,Three_Count_Status as [Three Count Status] ,Server_Type as [Server Type] ,Mfg_Date as [Manufacturing Date] ,Batch_No as [Batch No] ,Best_Befor_UseDate as [Best Befor Use Date] ,Item_Type as [Item Type],Item_Used_as as [Used as] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Flavour_Seq as [Flavour Sequence] ,Pack_Seq as [Pack Sequence] ,Sku_Seq as [SKU Sequence] ,show as [Show] ,item_category as [Item Category] ,tech_shelf_life as [Tech Shelf Life] ,Cost as [Cost] ,Sub_item_category as [Item Sub Category] ,TypeOfItm as [Type Of Item] ,NoMRP as [No MRP] ,Morning as [Morning] ,IsTaxable as Taxable ,PROD_ITEM_CATEGORY_CODE as [Prod Item Category Code] ,Rate as [Rate] ,Active as [Active] ,AlternativeItem as [Alternative Item] ,ItemSpecification as [Item Specification] ,Item_Category_Struct_Code as [Item Category Structure Code] ,SubItemType as [item Sub Type] ,Is_Serial_Item as [Is Serial Item] ,Serial_Counter as [Serial Counter] ,WARRANTY_CODE as [Warranty Code] ,Is_Pick_Auto_SrNo as [Is Pick Auto Srno] ,Weight_UOM as [Weight UOM] ,Weight_Value as [Weight Value] ,Asset_Life as [Asset Life] ,Warranty_Period as [Warranty Period] ,Is_MRP as [Is MRP] ,ITF_CODE as [ITF Code],Is_FreshItem as [Fresh Item],Is_Ambient as [Ambient],Product_type as [Product Type],Is_CrateType As [Crate Type],Is_CAN_Type as [CAN Type],ISNULL(GL_Account,'') As [GL Account],Is_Batch_Item as [Batch Item],Is_Leakage_Not_Applicable as [Leakage Not Applicable] From tspl_item_master      "
        If clsCommon.myLen(pivotheader) > 0 Then
            If clsCommon.myLen(whrcls) > 0 Then
                whrcls = " where  tspl_item_master.Active=1 and  " + whrcls
            Else
                ' done by priti KDI/08/06/18-000353
                whrcls = "  where  tspl_item_master.Active=1 "
            End If
            qry = "select * from (select aa.*,a.DESCRIPTION,a.cat_value from (select tspl_item_master.Item_Code as [Code] ,Item_Desc as [Item Description],ISNULL(tspl_item_master.Short_Description ,'') As  Short_Description,ISNULL(CSA_TYPE,'') AS [Item Group Type],case when Is_Tax_Exempted=0 then 'None' when Is_Tax_Exempted=1 then 'Tax' when Is_Tax_Exempted=2 then 'Excise' end as TaxType ,Drawing_no as [Drawing No],Part_No as [Part No]  ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price] ,Father_Code as [Father Code] ,Father_QTy as [Father Quantity] ,Cheapter_Heads as [Cheapter Heads] ,Mother_Code as [Mother Code] ,Mother_Qty as [Mother Quantity] ,Opening_Balance as [Opening Balance] ,Two_Count_Status as [Two Count Status] ,Three_Count_Status as [Three Count Status] ,Server_Type as [Server Type] ,Mfg_Date as [Manufacturing Date] ,Batch_No as [Batch No] ,Best_Befor_UseDate as [Best Befor Use Date] ,Item_Type as [Item_Type],Item_Used_as as [Used as] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Flavour_Seq as [Flavour Sequence] ,Pack_Seq as [Pack Sequence] ,Sku_Seq as [SKU Sequence] ,show as [Show] ,item_category as [Item Category] ,tech_shelf_life as [Tech Shelf Life] ,Cost as [Cost],Sub_item_category as [Item Sub Category] ,TypeOfItm as [Type Of Item] ,NoMRP as [No MRP] ,Morning as [Morning] ,IsTaxable as Taxable  ,PROD_ITEM_CATEGORY_CODE as [Prod Item Category Code] ,Rate as [Rate] ,Active as [Active] ,AlternativeItem as [Alternative Item] ,ItemSpecification as [Item Specification] ,Item_Category_Struct_Code as [Item Category Structure Code] ,SubItemType as [item Sub Type] ,Is_Serial_Item as [Is Serial Item] ,Serial_Counter as [Serial Counter] ,WARRANTY_CODE as [Warranty Code] ,Is_Pick_Auto_SrNo as [Is Pick Auto Srno] ,Weight_UOM as [Weight UOM] ,Weight_Value as [Weight Value] ,Asset_Life as [Asset Life] ,Warranty_Period as [Warranty Period] ,Is_MRP as [Is MRP] ,ITF_CODE as [ITF Code],Is_FreshItem as [Fresh Item],Is_Ambient as [Ambient],Product_type as [Product Type],Is_CrateType As [Crate Type],Is_CAN_Type as [CAN Type],ISNULL(GL_Account,'') As [GL Account],Is_Leakage_Not_Applicable as [Leakage Not Applicable] From tspl_item_master " + whrcls + ") aa left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=aa.Code) as s pivot(max(cat_value) for description in (" + pivotheader + "))t"
            whrcls = ""
        End If
        str = clsCommon.ShowSelectForm("ITMMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getFinderMultiple(ByVal whrcls As String, ByVal arrcurcode As ArrayList) As ArrayList
        Dim str As String = " select Item_Code as [Code] ,Item_Desc as [Item Description],Sku_Seq as [Seq. No.],ISNULL(Short_Description,'') AS Short_Description,ISNULL(CSA_TYPE,'') AS [Item Group Type],case when Is_Tax_Exempted=0 then 'None' when Is_Tax_Exempted=1 then 'Tax' when Is_Tax_Exempted=2 then 'Excise' end as TaxType ,Drawing_no as [Drawing No],Part_No as [Part No] ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price] ,Father_Code as [Father Code] ,Father_QTy as [Father Quantity] ,Cheapter_Heads as [Cheapter Heads] ,Mother_Code as [Mother Code] ,Mother_Qty as [Mother Quantity] ,Opening_Balance as [Opening Balance] ,Two_Count_Status as [Two Count Status] ,Three_Count_Status as [Three Count Status] ,Server_Type as [Server Type] ,Mfg_Date as [Manufacturing Date] ,Batch_No as [Batch No] ,Best_Befor_UseDate as [Best Befor Use Date] ,Item_Type as [Item Type],Item_Used_as as [Used as] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Flavour_Seq as [Flavour Sequence] ,Pack_Seq as [Pack Sequence] ,Sku_Seq as [SKU Sequence] ,show as [Show] ,item_category as [Item Category] ,tech_shelf_life as [Tech Shelf Life] ,Cost as [Cost] ,Sub_item_category as [Item Sub Category] ,TypeOfItm as [Type Of Item] ,NoMRP as [No MRP] ,Morning as [Morning] ,IsTaxable as Taxable ,PROD_ITEM_CATEGORY_CODE as [Prod Item Category Code] ,Rate as [Rate] ,Active as [Active] ,AlternativeItem as [Alternative Item] ,ItemSpecification as [Item Specification] ,Item_Category_Struct_Code as [Item Category Structure Code] ,SubItemType as [item Sub Type] ,Is_Serial_Item as [Is Serial Item] ,Serial_Counter as [Serial Counter] ,WARRANTY_CODE as [Warranty Code] ,Is_Pick_Auto_SrNo as [Is Pick Auto Srno] ,Weight_UOM as [Weight UOM] ,Weight_Value as [Weight Value] ,Asset_Life as [Asset Life] ,Warranty_Period as [Warranty Period] ,Is_MRP as [Is MRP] ,ITF_CODE as [ITF Code],Is_FreshItem as [Fresh Item],Is_Ambient as [Ambient],Product_type as [Product Type],Is_CrateType As [Crate Type],Is_CAN_Type as [CAN Type],ISNULL(GL_Account,'') As [GL Account],Is_Batch_Item as [Batch Item],Is_Leakage_Not_Applicable as [Leakage Not Applicable] From tspl_item_master      "
        str += " Where 2=2 "
        If clsCommon.myLen(whrcls) > 0 Then
            str += " and " + whrcls
        End If
        Return clsCommon.ShowMultipleSelectForm("M@ITMTFND", str, "Code", "", arrcurcode, Nothing)
    End Function
    Public Shared Function getFinderForActiveAndIncative(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Else
            whrcls = whrcls ' + " and comp_code='" + objCommonVar.CurrentCompanyCode + "'" ' & "  and Active='1' "because in master all items should show whether it is active or inactive but in transaction only active items come
        End If
        Dim pivotheader As String = ""
        pivotheader = GetNLevelPivotHeader()
        Dim qry As String = " select Item_Code as [Code] ,Item_Desc as [Item Description],Sku_Seq as [Seq. No.],ISNULL(Short_Description,'') AS Short_Description,isnull(tspl_item_master.Alies_Name2,'') as [Alies Name2],ISNULL(CSA_TYPE,'') AS [Item Group Type],case when Is_Tax_Exempted=0 then 'None' when Is_Tax_Exempted=1 then 'Tax' when Is_Tax_Exempted=2 then 'Excise' end as TaxType ,Drawing_no as [Drawing No],Part_No as [Part No] ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price] ,Father_Code as [Father Code] ,Father_QTy as [Father Quantity] ,Cheapter_Heads as [Cheapter Heads] ,Mother_Code as [Mother Code] ,Mother_Qty as [Mother Quantity] ,Opening_Balance as [Opening Balance] ,Two_Count_Status as [Two Count Status] ,Three_Count_Status as [Three Count Status] ,Server_Type as [Server Type] ,Mfg_Date as [Manufacturing Date] ,Batch_No as [Batch No] ,Best_Befor_UseDate as [Best Befor Use Date] ,Item_Type as [Item Type],Item_Used_as as [Used as] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Flavour_Seq as [Flavour Sequence] ,Pack_Seq as [Pack Sequence] ,Sku_Seq as [SKU Sequence] ,show as [Show] ,item_category as [Item Category] ,tech_shelf_life as [Tech Shelf Life] ,Cost as [Cost] ,Sub_item_category as [Item Sub Category] ,TypeOfItm as [Type Of Item] ,NoMRP as [No MRP] ,Morning as [Morning] ,IsTaxable as Taxable ,PROD_ITEM_CATEGORY_CODE as [Prod Item Category Code] ,Rate as [Rate] ,Active as [Active] ,AlternativeItem as [Alternative Item] ,ItemSpecification as [Item Specification] ,Item_Category_Struct_Code as [Item Category Structure Code] ,SubItemType as [item Sub Type] ,Is_Serial_Item as [Is Serial Item] ,Serial_Counter as [Serial Counter] ,WARRANTY_CODE as [Warranty Code] ,Is_Pick_Auto_SrNo as [Is Pick Auto Srno] ,Weight_UOM as [Weight UOM] ,Weight_Value as [Weight Value] ,Asset_Life as [Asset Life] ,Warranty_Period as [Warranty Period] ,Is_MRP as [Is MRP] ,ITF_CODE as [ITF Code],Is_FreshItem as [Fresh Item],Is_Ambient as [Ambient],Product_type as [Product Type],Is_CrateType As [Crate Type],Is_CAN_Type as [CAN Type],ISNULL(GL_Account,'') As [GL Account],Is_Batch_Item as [Batch Item],Is_Leakage_Not_Applicable as [Leakage Not Applicable],isnull(tspl_item_master.Alies_Name,'') as [Alies Name], isnull(tspl_item_master.Alies_Name3,'') as [Alies Name3] From tspl_item_master      "
        If clsCommon.myLen(pivotheader) > 0 Then
            If clsCommon.myLen(whrcls) > 0 Then
                whrcls = " where  2=2 and  " + whrcls
            Else
                ' done by priti KDI/08/06/18-000353
                whrcls = "  where  2=2 "
            End If
            qry = "select * from (select aa.*,a.DESCRIPTION,a.cat_value from (select tspl_item_master.Item_Code as [Code] ,Item_Desc as [Item Description],ISNULL(tspl_item_master.Short_Description ,'') As  Short_Description, isnull(tspl_item_master.Alies_Name2,'') as [Alies Name2],ISNULL(CSA_TYPE,'') AS [Item Group Type],case when Is_Tax_Exempted=0 then 'None' when Is_Tax_Exempted=1 then 'Tax' when Is_Tax_Exempted=2 then 'Excise' end as TaxType ,Drawing_no as [Drawing No],Part_No as [Part No]  ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price] ,Father_Code as [Father Code] ,Father_QTy as [Father Quantity] ,Cheapter_Heads as [Cheapter Heads] ,Mother_Code as [Mother Code] ,Mother_Qty as [Mother Quantity] ,Opening_Balance as [Opening Balance] ,Two_Count_Status as [Two Count Status] ,Three_Count_Status as [Three Count Status] ,Server_Type as [Server Type] ,Mfg_Date as [Manufacturing Date] ,Batch_No as [Batch No] ,Best_Befor_UseDate as [Best Befor Use Date] ,Item_Type as [Item_Type],Item_Used_as as [Used as] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Flavour_Seq as [Flavour Sequence] ,Pack_Seq as [Pack Sequence] ,Sku_Seq as [SKU Sequence] ,show as [Show] ,item_category as [Item Category] ,tech_shelf_life as [Tech Shelf Life] ,Cost as [Cost],Sub_item_category as [Item Sub Category] ,TypeOfItm as [Type Of Item] ,NoMRP as [No MRP] ,Morning as [Morning] ,IsTaxable as Taxable  ,PROD_ITEM_CATEGORY_CODE as [Prod Item Category Code] ,Rate as [Rate] ,Active as [Active] ,AlternativeItem as [Alternative Item] ,ItemSpecification as [Item Specification] ,Item_Category_Struct_Code as [Item Category Structure Code] ,SubItemType as [item Sub Type] ,Is_Serial_Item as [Is Serial Item] ,Serial_Counter as [Serial Counter] ,WARRANTY_CODE as [Warranty Code] ,Is_Pick_Auto_SrNo as [Is Pick Auto Srno] ,Weight_UOM as [Weight UOM] ,Weight_Value as [Weight Value] ,Asset_Life as [Asset Life] ,Warranty_Period as [Warranty Period] ,Is_MRP as [Is MRP] ,ITF_CODE as [ITF Code],Is_FreshItem as [Fresh Item],Is_Ambient as [Ambient],Product_type as [Product Type],Is_CrateType As [Crate Type],Is_CAN_Type as [CAN Type],ISNULL(GL_Account,'') As [GL Account],Is_Leakage_Not_Applicable as [Leakage Not Applicable],isnull(tspl_item_master.Alies_Name,'') as [Alies Name], isnull(tspl_item_master.Alies_Name3,'') as [Alies Name3] From tspl_item_master " + whrcls + ") aa left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=aa.Code) as s pivot(max(cat_value) for description in (" + pivotheader + "))t"
            whrcls = ""
        End If
        str = clsCommon.ShowSelectForm("ITMMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetFinderForDrawingNo(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select distinct drawing_no as [Drawing_No] from tspl_item_master "
        str = clsCommon.myCstr(clsCommon.ShowSelectForm("ITEMDRWFND", qry, "Drawing_No", whrCls, strCurrCode, "Drawing_No", isButtonClicked))
        Return str
    End Function
    Public Shared Function GetItemDrawingNo(ByVal Item_Code As String, ByVal trans As SqlTransaction) As String
        Dim str As String = ""
        Dim qry As String = "select drawing_no as [Drawing_No] from tspl_item_master where Item_Code='" & Item_Code & "' "
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return str
    End Function
    Public Shared Function IsBatchItem(ByVal strICode As String) As Boolean
        Return IsBatchItem(strICode, Nothing)
    End Function
    Public Shared Function IsBatchItem(ByVal strICode As String, ByVal tran As SqlTransaction) As Boolean
        Dim qry As String = "select Is_Batch_Item from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran)) = 1, True, False)
    End Function
    Public Shared Function GetProductionTolerance(ByVal strICode As String, ByVal tran As SqlTransaction) As Decimal
        Dim qry As String = "select Production_Tolerance from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran))
    End Function
    Public Shared Function GetSelfLife(ByVal strICode As String, ByVal tran As SqlTransaction) As Integer
        Dim qry As String = "select tech_shelf_life from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran))
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Item No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = "Update TSPL_ITEM_MASTER set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Item_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetNLevelPivotHeader() As String
        '----------------------------picks description of n-level category---------------------------------
        Dim qry As String = "select distinct (Select distinct ',['+DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('')) as x"
        Dim pivotheader As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.myLen(pivotheader) > 0 AndAlso pivotheader.Substring(0, 1) = "," Then
            pivotheader = pivotheader.Substring(1, pivotheader.Length - 1)
        End If
        Return pivotheader
        '--------------------------------------------------------------------------------------------------
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function FinderForItem(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean, Optional ByVal Product_type As String = "", Optional ByVal Whrcls As String = Nothing) As clsItemMaster
        Return FinderForItem(strCode, strItemType, False, isButtonClicked, "", "", Product_type, Whrcls)
    End Function
    Public Shared Function FinderForItem(ByVal strCode As String, ByVal strItemType As String, ByVal isForFinishedOrOtherFinished As Boolean, ByVal isButtonClicked As Boolean, ByVal strVendorCode As String, ByVal strCustomerCode As String, Optional ByVal Product_type As String = "", Optional ByVal Whrclass As String = Nothing) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        '------------07/08/2014--------pivot category---------------------------
        Dim pivotheader As String = ""
        pivotheader = clsItemMaster.GetNLevelPivotHeader()
        '---------------------------------------------------------------------
        '' Anubhooti 21-Aug-2014 (Only fetch Item_Image )
        Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as Code,Item_Desc as Name,TSPL_ITEM_MASTER.Short_Description as 'Short Description',Sku_Seq as [Seq. No.],TSPL_ITEM_MASTER.Structure_Code as [Structure Code] ,TSPL_ITEM_MASTER.Structure_Desc as [Structure Desc],TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],ITF_CODE as [ITF CODE],Item_Image, TSPL_ITEM_MASTER.Cost "
        If clsCommon.myLen(pivotheader) <= 0 Then
            qry += ",category_Level.* "
        End If
        If clsCommon.myLen(strVendorCode) > 0 Then
            qry += ",(select top 1 vendor_item_no from  TSPL_VENDOR_ITEM_DETAIL where TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code and TSPL_VENDOR_ITEM_DETAIL.vendor_code='" + strVendorCode + "' order by Start_Date desc  ) as [Vendor Item]"
        ElseIf clsCommon.myLen(strCustomerCode) > 0 Then
            qry += ",(select top 1 Customer_item_no from  TSPL_CUSTOMER_ITEM_DETAIL where TSPL_CUSTOMER_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code and TSPL_CUSTOMER_ITEM_DETAIL.Customer_Code='" + strCustomerCode + "' order by Start_Date desc  ) as [Customer Item]"
        End If
        qry += " from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "
        If clsCommon.myLen(pivotheader) <= 0 Then
            '===============Added By Rohit on June 09,2014 to show item Make,Model,Size,type for each Item===================
            qry += " Left Join (select Item_Code,MAX(Make) as _Make,MAX(Model) as _Model,MAX(Size) as _Size,MAX(Type) as _Type from(" _
                & " select Item_code,case when CATEGORY_LEVEL=2 then Item_Cagetory_Values  end AS  'Make',case when CATEGORY_LEVEL=3 then Item_Cagetory_Values end AS 'MODEL',case when CATEGORY_LEVEL=4 then Item_Cagetory_Values end AS  'Size' ,case when CATEGORY_LEVEL=1 then Item_Cagetory_Values end AS 'Type' " _
                & " from TSPL_ITEM_MASTER_CATEGORY cat inner join TSPL_ITEM_CATEGORY_LEVEL lev on cat.Item_Category_Code=lev.ITEM_CATEGORY_CODE" _
                & " ) tt group by item_Code) category_Level on category_Level.Item_code=TSPL_ITEM_MASTER.Item_Code"
            '================================================================================================================
        End If
        Dim WhrCls As String = " TSPL_ITEM_MASTER.Active=1 "
        If clsCommon.myLen(Whrclass) > 0 Then
            WhrCls += " and " + Whrclass
        End If
        'Ravi
        If isForFinishedOrOtherFinished Then
            Dim strNewQry As String = "select item_type_code from tspl_item_type_master where item_type_code='" + strItemType + "'"
            Dim strNewVar As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strNewQry))
            'If clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "f") = CompairStringResult.Equal Then
            '    WhrCls += " and item_type='f'"
            'ElseIf clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "a") = CompairStringResult.Equal Then
            '    WhrCls += " and item_type='a'"
            'ElseIf clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "r") = CompairStringResult.Equal Then
            '    WhrCls += " and item_type='r'"
            'ElseIf clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "s") = CompairStringResult.Equal Then
            '    WhrCls += " and item_type='s'"
            'ElseIf clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "o") = CompairStringResult.Equal Then
            '    WhrCls += " and item_type = 'o'"
            'ElseIf clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "t") = CompairStringResult.Equal Then
            '    WhrCls += " and item_type = 't'"
            'Else
            '    WhrCls += "item_type<>'f' and tspl_item_master.active=1"
            'End If
            If clsCommon.myLen(strNewVar) > 0 Then
                WhrCls += " and item_type='" + strItemType + "' "
            Else
                WhrCls += " and item_type<>'f' and tspl_item_master.active=1 "
            End If
        Else
            If clsCommon.myLen(strItemType) > 0 Then
                WhrCls += " and item_type ='" + strItemType + "'"
            End If
        End If
        If Product_type <> "" Then
            WhrCls &= " and " & Product_type
        End If
        '-----------------------------------------------------
        '' Anubhooti 21-Aug-2014 On Settings
        Dim Is_Purchaseable As Integer = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='Is_Purchaseable_Item'")
        If clsCommon.myCdbl(Is_Purchaseable) = 1 Then
            WhrCls &= " AND TSPL_ITEM_MASTER.Is_Purchaseable='1' "
        End If
        If clsCommon.myLen(pivotheader) > 0 Then
            Dim query As String = qry + " where " + WhrCls
            qry = ""
            qry = "select * from (select a.DESCRIPTION,a.cat_value,b.* from (" + query + ")b"
            qry += " left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=b.code) as s pivot(max(cat_value) for description in (" + pivotheader + "))t"
            WhrCls = ""
        End If
        '-----------------------------------------------------------------------
        strCode = clsCommon.ShowSelectForm("ItemFinder" + IIf(clsCommon.myLen(strVendorCode) > 0, "V", IIf(clsCommon.myLen(strCustomerCode) > 0, "C", "")), qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select HSN_Code, Item_Code,TSPL_ITEM_MASTER.Weight_UOM as Weight_UOM,TSPL_ITEM_MASTER.Weight_Value as Weight_Value,Item_Desc,Unit_Code,Is_Serial_Item,Is_Pick_Auto_SrNo,Is_MRP,TSPL_ITEM_MASTER.RACK_NO As Rack_No, TSPL_ITEM_MASTER.Cost, TSPL_ITEM_MASTER.Is_Tax_Exempted,Product_Type,TSPL_ITEM_MASTER.Is_Batch_Item,TSPL_ITEM_MASTER.Can,TSPL_ITEM_MASTER.Crate,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.is_Insurance from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 isnull(UOM_Code,'') from TSPL_ITEM_UOM_DETAIL where Item_Code ='" & clsCommon.myCstr(dt.Rows(0)("Item_Code")) & "' and Default_UOM =1", Nothing))
                obj.Is_Serial_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Serial_Item")) = 1, True, False)
                obj.Is_Pick_Auto_SrNo = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Pick_Auto_SrNo")) = 1, True, False)
                obj.Is_Batch_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Batch_Item")) = 1, True, False)
                obj.Is_MRP = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1, True, False)
                obj.Weight_UOM = clsCommon.myCstr(dt.Rows(0)("Weight_UOM"))
                obj.Weight_Value = clsCommon.myCdbl(dt.Rows(0)("Weight_Value"))
                ''richa Ticket No. BM00000003197 on 24/07/2014
                obj.Rack_No = clsCommon.myCstr(dt.Rows(0)("Rack_No"))
                obj.Cost = clsCommon.myCdbl(dt.Rows(0)("Cost"))
                obj.Tax_Exempted = clsCommon.myCdbl(dt.Rows(0)("Is_Tax_Exempted"))
                obj.Product_Type = clsCommon.myCstr(dt.Rows(0)("Product_Type"))
                obj.HSNCode = clsCommon.myCstr(dt.Rows(0)("HSN_Code"))
                obj.Can = IIf(clsCommon.myCdbl(dt.Rows(0)("Can")) = 1, True, False)
                obj.Crate = IIf(clsCommon.myCdbl(dt.Rows(0)("Crate")) = 1, True, False)
                '------------------------------------------
                obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                obj.Is_Insurance = clsCommon.myCdbl(dt.Rows(0)("Is_Insurance"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForItemPriceList(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean, Optional ByVal Product_type As String = "", Optional ByVal Whrcls As String = Nothing) As clsItemMaster
        Return FinderForItemPriceList(strCode, strItemType, False, isButtonClicked, "", "", Product_type, Whrcls)
    End Function
    Public Shared Function FinderForItemPriceList(ByVal strCode As String, ByVal strItemType As String, ByVal isForFinishedOrOtherFinished As Boolean, ByVal isButtonClicked As Boolean, ByVal strVendorCode As String, ByVal strCustomerCode As String, Optional ByVal Product_type As String = "", Optional ByVal Whrclass As String = Nothing) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        ''------------07/08/2014--------pivot category---------------------------
        'Dim pivotheader As String = ""
        'pivotheader = clsItemMaster.GetNLevelPivotHeader()
        ''---------------------------------------------------------------------
        '' Anubhooti 21-Aug-2014 (Only fetch Item_Image )
        Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as Code,Item_Desc as Name ,Sku_Seq as [Seq. No.],TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],ITF_CODE as [ITF CODE],Item_Image, TSPL_ITEM_MASTER.Cost "
        'If clsCommon.myLen(pivotheader) <= 0 Then
        '    qry += ",category_Level.* "
        'End If
        If clsCommon.myLen(strVendorCode) > 0 Then
            qry += ",(select top 1 vendor_item_no from  TSPL_VENDOR_ITEM_DETAIL where TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code and TSPL_VENDOR_ITEM_DETAIL.vendor_code='" + strVendorCode + "' order by Start_Date desc  ) as [Vendor Item]"
        ElseIf clsCommon.myLen(strCustomerCode) > 0 Then
            qry += ",(select top 1 Customer_item_no from  TSPL_CUSTOMER_ITEM_DETAIL where TSPL_CUSTOMER_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code and TSPL_CUSTOMER_ITEM_DETAIL.Customer_Code='" + strCustomerCode + "' order by Start_Date desc  ) as [Customer Item]"
        End If
        qry += " from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "
        'If clsCommon.myLen(pivotheader) <= 0 Then
        '    '===============Added By Rohit on June 09,2014 to show item Make,Model,Size,type for each Item===================
        '    qry += " Left Join (select Item_Code,MAX(Make) as _Make,MAX(Model) as _Model,MAX(Size) as _Size,MAX(Type) as _Type from(" _
        '        & " select Item_code,case when CATEGORY_LEVEL=2 then Item_Cagetory_Values  end AS  'Make',case when CATEGORY_LEVEL=3 then Item_Cagetory_Values end AS 'MODEL',case when CATEGORY_LEVEL=4 then Item_Cagetory_Values end AS  'Size' ,case when CATEGORY_LEVEL=1 then Item_Cagetory_Values end AS 'Type' " _
        '        & " from TSPL_ITEM_MASTER_CATEGORY cat inner join TSPL_ITEM_CATEGORY_LEVEL lev on cat.Item_Category_Code=lev.ITEM_CATEGORY_CODE" _
        '        & " ) tt group by item_Code) category_Level on category_Level.Item_code=TSPL_ITEM_MASTER.Item_Code"
        '    '================================================================================================================
        'End If
        Dim WhrCls As String = Nothing
        Dim query As String = qry
        qry = ""
        qry = "select * from ( select a.DESCRIPTION,a.cat_value,a.Item_Cagetory_Values,b.* from (" + query + ")b"
        qry += " left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=b.code ) as final"
        '-----------------------------------------------------------------------
        strCode = clsCommon.ShowSelectForm("ItemFinder" + IIf(clsCommon.myLen(strVendorCode) > 0, "V", IIf(clsCommon.myLen(strCustomerCode) > 0, "C", "")), qry, "Code", Whrclass, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,TSPL_ITEM_MASTER.Weight_UOM as Weight_UOM,TSPL_ITEM_MASTER.Weight_Value as Weight_Value,Item_Desc,Unit_Code,Is_Serial_Item,Is_Pick_Auto_SrNo,Is_MRP,TSPL_ITEM_MASTER.RACK_NO As Rack_No, TSPL_ITEM_MASTER.Cost, TSPL_ITEM_MASTER.Is_Tax_Exempted,Product_Type,TSPL_ITEM_MASTER.Is_Batch_Item from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.Is_Serial_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Serial_Item")) = 1, True, False)
                obj.Is_Pick_Auto_SrNo = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Pick_Auto_SrNo")) = 1, True, False)
                obj.Is_Batch_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Batch_Item")) = 1, True, False)
                obj.Is_MRP = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1, True, False)
                obj.Weight_UOM = clsCommon.myCstr(dt.Rows(0)("Weight_UOM"))
                obj.Weight_Value = clsCommon.myCdbl(dt.Rows(0)("Weight_Value"))
                ''richa Ticket No. BM00000003197 on 24/07/2014
                obj.Rack_No = clsCommon.myCdbl(dt.Rows(0)("Rack_No"))
                obj.Cost = clsCommon.myCdbl(dt.Rows(0)("Cost"))
                obj.Tax_Exempted = clsCommon.myCdbl(dt.Rows(0)("Is_Tax_Exempted"))
                obj.Product_Type = clsCommon.myCstr(dt.Rows(0)("Product_Type"))
                '------------------------------------------
            End If
        End If
        Return obj
    End Function
    Public Shared Function GetKGConvQty(ByVal strItem As String, ByVal strConvertedUnit As String, ByVal dblQty As Double) As Double
        Dim strCurrentUnit As String = "Kg"
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strCurrentUnit & "'"))
        Dim dblConvQty As Double = 0
        If clsCommon.myLen(strConvertedUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strConvertedUnit & "'"))
            If dblCurrentConvF > 0 Then
                dblConvQty = Math.Round(Math.Round((dblOrgConvF / dblCurrentConvF), 2) * dblQty, 6)
            End If
        End If
        Return dblConvQty
    End Function
    Public Shared Function FinderForThirdPartyItem(ByVal strCode As String, ByVal strItemType As String, ByVal strLocation As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Return FinderForThirdPartyItem(strCode, strItemType, strLocation, False, isButtonClicked, "", "")
    End Function
    Public Shared Function FinderForThirdPartyItem(ByVal strCode As String, ByVal strItemType As String, ByVal strLocation As String, ByVal isForFinishedOrOtherFinished As Boolean, ByVal isButtonClicked As Boolean, ByVal strVendorCode As String, ByVal strCustomerCode As String) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "select distinct TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name ,Sku_Seq as [Seq. No.] ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],ITF_CODE as [ITF CODE] "
        qry += " from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "
        qry += " left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code "
        Dim WhrCls As String = ""
        If isForFinishedOrOtherFinished Then
            If clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                WhrCls = "Item_Type='F'  and TSPL_ITEM_MASTER.Active=1"
            ElseIf clsCommon.myLen(strItemType) > 0 AndAlso clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                WhrCls = "Item_Type='A' and  TSPL_ITEM_MASTER.Active=1"
            Else
                WhrCls = "Item_Type<>'F' and TSPL_ITEM_MASTER.Active=1"
            End If
        Else
            If clsCommon.myLen(strItemType) > 0 Then
                WhrCls = "Item_Type ='" + strItemType + "' and TSPL_ITEM_MASTER.Active=1"
            Else
                WhrCls = " TSPL_ITEM_MASTER.Active=1"
            End If
        End If
        WhrCls += " and Location_Code='" & strLocation & "'"
        strCode = clsCommon.ShowSelectForm("ItemFinder" + IIf(clsCommon.myLen(strVendorCode) > 0, "V", IIf(clsCommon.myLen(strCustomerCode) > 0, "C", "")), qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,TSPL_ITEM_MASTER.Weight_UOM as Weight_UOM,TSPL_ITEM_MASTER.Weight_Value as Weight_Value,Item_Desc,Unit_Code,Is_Serial_Item,Is_Pick_Auto_SrNo,Is_MRP,TSPL_ITEM_MASTER.Is_Batch_Item from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.Is_Serial_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Serial_Item")) = 1, True, False)
                obj.Is_Pick_Auto_SrNo = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Pick_Auto_SrNo")) = 1, True, False)
                obj.Is_MRP = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1, True, False)
                obj.Weight_UOM = clsCommon.myCstr(dt.Rows(0)("Weight_UOM"))
                obj.Weight_Value = clsCommon.myCdbl(dt.Rows(0)("Weight_Value"))
                obj.Is_Batch_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Batch_Item")) = 1, True, False)
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForuom(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select distinct uom_code as code from TSPL_ITEM_UOM_DETAIL "
        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_code='" + strItemType + "'"
        End If
        Return clsCommon.ShowSelectForm("UOMFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
    End Function
    Public Shared Function FinderForItemWithPrice(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER"
        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_Type='" + strItemType + "'"
        End If
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForItemEmpty(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "SELECT TSPL_ITEM_MASTER.Item_Code AS Code, TSPL_ITEM_MASTER.Item_Desc AS Name, Sku_Seq as [Seq. No.],TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] "
        qry += " FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_ITEM_MASTER.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code"
        Dim WhrCls As String = "(UOM_Code = 'EC' or UOM_Code = 'EB' or UOM_Code = 'SH') and (Item_Type='F') and TSPL_ITEM_MASTER.Active=1 "
        strCode = clsCommon.ShowSelectForm("ItemFinderEmpty", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForFinishedGoods(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "SELECT TSPL_ITEM_MASTER.Item_Code AS Code, TSPL_ITEM_MASTER.Item_Desc AS Name,Sku_Seq as [Seq. No.], TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] "
        qry += " FROM TSPL_ITEM_MASTER  "
        Dim WhrCls As String = "(Item_Type='F') and Unit_Code <> 'SH' and TSPL_ITEM_MASTER.Active=1 "
        strCode = clsCommon.ShowSelectForm("ItemFinderFinishedGoods", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForRMOther(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_UOM_DETAIL.Net_Weight as [Net Weight] FROM TSPL_ITEM_UOM_DETAIL left join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code"
        Dim WhrCls As String = " TSPL_ITEM_UOM_DETAIL.Net_Weight > 0"
        strCode = clsCommon.ShowSelectForm("ItemFinderStore11", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                'obj.Is_Serial_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Serial_Item")) = 1, True, False)
                'obj.Is_Pick_Auto_SrNo = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Pick_Auto_SrNo")) = 1, True, False)
            End If
        End If
        Return obj
    End Function
    Public Shared Function isItemOfSameType(ByVal strItemType As String, ByVal strItemTypeName As String, ByVal ArrICode As List(Of String)) As Boolean
        Dim qry As String = "select Item_Code,Item_Desc,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(ArrICode) + ") "
        'Ravi
        'If clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
        '    qry += " and Item_Type not in ('F')"
        'ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
        '    qry += " and Item_Type not in ('A')"
        'ElseIf clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
        '    qry += " and Item_Type not in ('R')"
        'ElseIf clsCommon.CompairString(strItemType, "S") = CompairStringResult.Equal Then
        '    qry += " and Item_Type not in ('S')"
        'ElseIf clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
        '    qry += " and Item_Type not in ('O')"
        'ElseIf clsCommon.CompairString(strItemType, "T") = CompairStringResult.Equal Then
        '    qry += " and Item_Type not in ('T')"
        'End If
        qry += " and Item_Type not in ('" + strItemType + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim msg As String = "Error : Follwing Item's Type is not of " + strItemTypeName
            For Each dr As DataRow In dt.Rows
                msg = msg + Environment.NewLine + "Item Code : " + clsCommon.myCstr(dr("Item_Code")) + " Description : " + clsCommon.myCstr(dr("Item_Desc"))
            Next
            Throw New Exception(msg)
        End If
        Return True
    End Function
    Public Shared Function isItemOfSameExempted(ByVal ArrICode As List(Of String)) As Integer
        Dim qry As String = "select 1 from TSPL_ITEM_MASTER where Is_Tax_Exempted=1 and Item_Code in (" + clsCommon.GetMulcallString(ArrICode) + ")"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows.Count
        End If
        Return 0
    End Function
    Public Shared Function isItemTaxableOrNonTaxable(ByVal ArrICode As List(Of String), ByVal isTaxable As Boolean) As Boolean
        If ArrICode Is Nothing OrElse ArrICode.Count <= 0 Then
            Throw New Exception("Please provide the item list")
        End If
        Dim qry As String = "select *  from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(ArrICode) + ") and IsTaxable <> '" + IIf(isTaxable, "1", "0") + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("All item should be " + IIf(isTaxable, "Taxable", "Non Taxable"))
        End If
        Return True
    End Function
    Public Shared Function isItemOfSameExcisable(ByVal ArrICode As List(Of String)) As Integer
        Dim qry As String = "select 1 from TSPL_ITEM_MASTER where Is_Tax_Exempted=2 and Item_Code in (" + clsCommon.GetMulcallString(ArrICode) + ")"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows.Count
        End If
        Return 0
    End Function
    Public Shared Function GetItemType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Finished Goods"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Semi Finished Good"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Raw Material"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Asset"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Trading Good"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Other"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Public Shared Function GetItemTypeWithNON_Inventory() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Finished Goods"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Semi Finished Good"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Raw Material"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Asset"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Trading Good"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Non-Inventory"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Other"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Public Shared Function IsItemUsedWithUOM(ByVal strItemCode As String, ByVal strUOM As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Select SUM(Used) From (" &
                " Select COUNT(*) as Used from TSPL_ADJUSTMENT_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_Code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_TRANSFER_DETAIL WHERE Item_Code='" + strItemCode + "' AND UOM='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + strItemCode + "' AND UOM='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used From TSPL_SCRAPINVOICE_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SALES_Quotation_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SALES_ORDER_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SHIPMENT_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SALE_INVOICE_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_PURCHASE_ORDER_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SRN_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_RGP_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_IssueReturn_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
             " UNION" &
                " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT where Item_Code = '" + strItemCode + "' AND UOM = '" + strUOM + "' " &
             " UNION" &
                " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code = '" + strItemCode + "' AND UOM = '" + strUOM + "' "
            Dim checkStockingUom As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from tspl_item_uom_detail where Item_Code = '" + strItemCode + "' and Stocking_Unit ='Y' and UOM_Code ='" + strUOM + "' "))
            If checkStockingUom = True Then
                qry = qry + " UNION" &
                    " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT where Item_Code = '" + strItemCode + "' AND Stock_UOM ='" + strUOM + "'  " &
                 " UNION" &
                    " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code = '" + strItemCode + "' AND Stock_UOM = '" + strUOM + "' "
            End If
            qry = qry + " ) XXX"
            Return IIf(clsDBFuncationality.getSingleValue(qry, trans) > 0, True, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    ' Ticket No : BHA/23/08/18-000478 By Prabhakar
    Public Shared Function IsItemUsedWithUOMForStockingCheck(ByVal strItemCode As String, ByVal strUOM As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Select SUM(Used) From (" &
                " Select COUNT(*) as Used from TSPL_ADJUSTMENT_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_Code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_TRANSFER_DETAIL WHERE Item_Code='" + strItemCode + "' AND UOM='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + strItemCode + "' AND UOM='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used From TSPL_SCRAPINVOICE_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SALES_Quotation_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SALES_ORDER_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SHIPMENT_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SD_SALE_INVOICE_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_PURCHASE_ORDER_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_SRN_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_RGP_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
            " UNION" &
                " Select COUNT(*) as Used from TSPL_IssueReturn_DETAIL WHERE Item_Code='" + strItemCode + "' AND Unit_code='" + strUOM + "'" &
             " UNION" &
                " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT where Item_Code = '" + strItemCode + "' AND UOM = '" + strUOM + "' " &
             " UNION" &
                " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code = '" + strItemCode + "' AND UOM = '" + strUOM + "' "
            qry = qry + " UNION" &
                    " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT where Item_Code = '" + strItemCode + "' AND Stock_UOM in (select UOM_Code from tspl_item_uom_detail where Item_Code = '" + strItemCode + "' and Stocking_Unit ='Y')  " &
                 " UNION" &
                    " select COUNT(*) as Used from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code = '" + strItemCode + "' AND Stock_UOM in (select UOM_Code from tspl_item_uom_detail where Item_Code = '" + strItemCode + "' and Stocking_Unit ='Y') "
            qry = qry + " ) XXX"
            Return IIf(clsDBFuncationality.getSingleValue(qry, trans) > 0, True, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetFatherCode(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Father_Code from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' and Father_Code not in ('NIL')"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function CheckItemCode(ByVal strICode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select count(Item_Code) from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetItemName(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemAliasName(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select isnull(ALIES_NAME,'') as ALIES_NAME from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemSecurityDeduction(ByVal strICode As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = "select isnull(Security_Deduction,0) as ALIES_NAME from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemShortDescription(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select isnull(Short_Description,'') as Short_Description from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemStructureCode(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Structure_Code from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemHSNCode(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select HSN_Code from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function IsTaxableItem(ByVal strICode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0, True, False)
    End Function
    Public Shared Function GetItemCSAType(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select CSA_Type from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemWeightUnit(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Weight_UOM from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemWeightValue(ByVal strICode As String, ByVal trans As SqlTransaction) As Double
        Dim qry As String = "select Weight_Value from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemCost(ByVal ItemCode As String, ByVal UOM As String, ByVal trans As SqlTransaction) As Double
        Dim ItemCost As Double = 0
        ItemCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Item_Cost from TSPL_ITEM_UOM_DETAIL where Item_Code='" & ItemCode & "' and UOM_Code='" & UOM & "' ", trans))
        Return ItemCost
    End Function
    Public Shared Function getTotalItemWeight(ByVal itemCode As String, ByVal UOM As String, ByVal dblQty As Double, ByVal trans As SqlTransaction) As Double
        Dim stkUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from TSPL_ITEM_UOM_DETAIL where Item_Code='" & itemCode & "' and Stocking_Unit='Y'", trans))
        Dim ConvFact As Double = 0
        Dim weightUnit As String = ""
        Dim Itemweight As Double = 0
        Dim TotalItemweight As Double = 0
        Dim weightUnitValue As Double = 0
        ConvFact = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Conversion_Factor   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & itemCode & "' and UOM_Code='" & UOM & "' ", trans))
        Itemweight = ConvFact * clsCommon.myCdbl(clsItemMaster.GetItemWeightValue(itemCode, Nothing))
        TotalItemweight = dblQty * Itemweight
        Return TotalItemweight
    End Function
    Public Shared Function GetWeitht(ByVal strICode As String, ByVal strUOM As String, ByVal trans As SqlTransaction) As Double
        Dim qry As String = "select Weight from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" + strUOM + "'"
        Return (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
    End Function
    Public Shared Function GetConvertionFactor(ByVal strICode As String, ByVal strUOM As String, ByVal trans As SqlTransaction) As Double
        Dim qry As String = "select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" + strUOM + "'"
        Return (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
    End Function
    Public Shared Function GetQtyInLtrFromKgByCLR(ByVal dclQtyInKG As String, ByVal dclCLR As String) As Decimal
        Dim retValue As Decimal = 0
        retValue = dclQtyInKG / ((1000 + dclCLR) / 1000)
        Return retValue
    End Function
    Public Shared Function GetStockUnit(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and Stocking_Unit='Y'"
        Return (clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)))
    End Function
    Public Shared Function GetCustomConversionUOM(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and Custom_Conversion=1"
        Return (clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)))
    End Function
    Public Shared Function GetItemDefaultUnit(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select top 1 UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and Default_UOM=1"
        Return (clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)))
    End Function
    Public Shared Function GetSaleAccGLAC(ByVal strICode As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_SALES_ACCOUNTS.Sales_Account,TSPL_SALES_ACCOUNTS.Sales_Class_Desc  "
        qry += " from TSPL_SALES_ACCOUNTS "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code "
        qry += " where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function
    Public Shared Function GetReturnableConGLAC(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select isnull(TSPL_SALES_ACCOUNTS.Returnable_Container,'') as Returnable_Container  " &
         " from TSPL_SALES_ACCOUNTS " &
         " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code " &
         " where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
        Return clsDBFuncationality.getSingleValue(qry, trans)
    End Function
    Public Shared Function GetSchemeAccGLAC(ByVal strICode As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_SALES_ACCOUNTS.Schemes,TSPL_SALES_ACCOUNTS.Sales_Class_Desc  "
        qry += " from TSPL_SALES_ACCOUNTS "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code "
        qry += " where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function
    Public Shared Function GetSaleReturnAccGLAC(ByVal strICode As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_SALES_ACCOUNTS.Sales_Return_Account,TSPL_SALES_ACCOUNTS.Sales_Class_Desc  "
        qry += " from TSPL_SALES_ACCOUNTS "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code "
        qry += " where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function
    Public Shared Function GetDamageAccGLAC(ByVal strICode As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_SALES_ACCOUNTS.Damaged_Goods,TSPL_SALES_ACCOUNTS.Sales_Class_Desc  "
        qry += " from TSPL_SALES_ACCOUNTS "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code "
        qry += " where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function
    Public Shared Function IsRequiredItemVisualQC(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select Visual_QC from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemType(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select Item_Type from TSPL_ITEM_MASTER where  Item_Code='" + strICode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemProductType(ByVal strICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select Product_Type from TSPL_ITEM_MASTER where  Item_Code='" + strICode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function IsItemRounding(ByVal strICode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select ApplyRoundingInStdProd from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetItemFatSNF(ByVal strICode As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim obj As New MIlkComponentType
        Dim qry As String = " select QCPM.Item_Code,PM.Type,QCPM.Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER QCPM " &
                            " inner join (SELECT * FROM  TSPL_PARAMETER_MASTER WHERE Type IN ('FAT','SNF')) PM on QCPM.Code=PM.Code " &
                            " WHERE QCPM.Item_Code='" + strICode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count = 0 Then
            Return obj
        Else
            '' get fat %
            Dim dr() As DataRow = dt.Select("Type='FAT'")
            If dr.Length > 0 Then
                obj.FAT_Per = clsCommon.myCdbl(dr(0).Item("Actual_Range"))
            Else
                obj.FAT_Per = 0
            End If
            '' get SNF %
            dr = dt.Select("Type='SNF'")
            If dr.Length > 0 Then
                obj.SNF_Per = clsCommon.myCdbl(dr(0).Item("Actual_Range"))
            Else
                obj.SNF_Per = 0
            End If
        End If
        Return obj
    End Function
    Public Shared Function LoadItemProductType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Others"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "MI"
        dr("Name") = "Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "MB"
        dr("Name") = "Melted Butter"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "CH"
        dr("Name") = "Cheese"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "CU"
        dr("Name") = "Curd"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "CA"
        dr("Name") = "Cream"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "DG"
        dr("Name") = "Desi-Ghee"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "BU"
        dr("Name") = "Butter"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "BM"
        dr("Name") = "Butter Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "PS"
        dr("Name") = "Paper Seal"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "MS"
        dr("Name") = "Manual Seal"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "MP"
        dr("Name") = "Milk Product"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Public Shared Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(Product_type, "MI") = CompairStringResult.Equal Then
            values = "Milk"
        ElseIf clsCommon.CompairString(Product_type, "CH") = CompairStringResult.Equal Then
            values = "Cheese"
        ElseIf clsCommon.CompairString(Product_type, "MB") = CompairStringResult.Equal Then
            values = "Melted Butter"
        ElseIf clsCommon.CompairString(Product_type, "CU") = CompairStringResult.Equal Then
            values = "Curd"
        ElseIf clsCommon.CompairString(Product_type, "CA") = CompairStringResult.Equal Then
            values = "Cream"
        ElseIf clsCommon.CompairString(Product_type, "BU") = CompairStringResult.Equal Then
            values = "Butter"
        ElseIf clsCommon.CompairString(Product_type, "BM") = CompairStringResult.Equal Then
            values = "Butter Milk"
        ElseIf clsCommon.CompairString(Product_type, "DG") = CompairStringResult.Equal Then
            values = "Desi-Ghee"
        ElseIf clsCommon.CompairString(Product_type, "PS") = CompairStringResult.Equal Then
            values = "Paper Seal"
        ElseIf clsCommon.CompairString(Product_type, "MS") = CompairStringResult.Equal Then
            values = "Manual Seal"
        ElseIf clsCommon.CompairString(Product_type, "MP") = CompairStringResult.Equal Then
            values = "Milk Product"
        ElseIf clsCommon.CompairString(Product_type, "") = CompairStringResult.Equal Then
            values = "Others"
        End If
        Return values
    End Function
    Public Shared Function ItemType(ByVal itype As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(itype, "R") = CompairStringResult.Equal Then
            values = "Raw Material"
        ElseIf clsCommon.CompairString(itype, "F") = CompairStringResult.Equal Then
            values = "Finished Good"
        ElseIf clsCommon.CompairString(itype, "S") = CompairStringResult.Equal Then
            values = "Semi Finished Good"
        ElseIf clsCommon.CompairString(itype, "A") = CompairStringResult.Equal Then
            values = "Asset"
        ElseIf clsCommon.CompairString(itype, "H") = CompairStringResult.Equal Then
            values = "Fresh"
        ElseIf clsCommon.CompairString(itype, "O") = CompairStringResult.Equal Then
            values = "Other"
        End If
        Return values
    End Function
    Public Shared Function IsItemTypeEmpty(ByVal strICode As String, ByVal strUOM As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If clsCommon.CompairString(strUOM, "EB") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "EC") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "SH") = CompairStringResult.Equal Then
            qry = "Select Empty from TSPL_UNIT_MASTER where Unit_Code='" + strUOM + "'"
        Else
            qry = "Select Two_Count_Status from TSPL_ITEM_MASTER where  Item_Code='" + strICode + "'"
        End If
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "Y") = CompairStringResult.Equal Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function LoadWarrantyDate() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "--Select--"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Sale Invoice Date"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Manufacturing Date"
        dt.Rows.Add(dr)
        Return dt
    End Function
    'Public Shared Function IsItemTypeEmpty(ByVal strICode As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim qry As String = " select Two_Count_Status from TSPL_ITEM_MASTER where  Item_Code='" + strICode + "'"
    '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "Y") = CompairStringResult.Equal Then
    '        Return True
    '    End If
    '    Return False
    'End Function
    'Private Function TotalApplyQtyInBottle() As Decimal
    '    Dim totalMRP As Decimal = 0
    '    Dim currentindex As Integer
    '    Dim pendingqty As Decimal
    '    Dim balance As Decimal
    '    Dim totalamt As Decimal
    '    Dim pendingvalue As Decimal = 0
    '    Dim otherconversion As Decimal = 0
    '    Dim total As Decimal = 0
    '    Dim totalloadinqty12 As Decimal = 0
    '    Dim total1 As Decimal = 0
    '    Dim ConvFact As Decimal = 0
    '    If cmbitemtype.Text = "Full" Then
    '        For Each gr As GridViewRowInfo In gv1.Rows
    '            If Not clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colShortage).Value) = 0 Then
    '                Dim strconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gr.Cells(colItemCode).Value + "' and UOM_Code = '" + gr.Cells(colUOM).Value + "'"))
    '                If strconversion = 1 Then
    '                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = 1 Then
    '                        Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colLeak).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colShortage).Value) * convfactor
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    Else
    '                        Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code = '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colLeak).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colShortage).Value) * convfactor
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    End If
    '                Else
    '                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = 1 Then
    '                        Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value / strconversion = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    Else
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next
    '    Else
    '        For Each gr As GridViewRowInfo In gv1.Rows
    '            If Not clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colShortage).Value) = 0 Then
    '                Dim strconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gr.Cells(colItemCode).Value + "' and UOM_Code = '" + gr.Cells(colUOM).Value + "'"))
    '                If strconversion = 1 Then
    '                    Dim currentconversion1 As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and UOM_Code = '" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
    '                    If currentconversion1 = 1 Then
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * currentconversion1 = gr.Cells(colMRP).Value Then
    '                            ConvFact = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gr.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gr.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
    '                            If gr.Cells(colUOM).Value <> "SH" Then
    '                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * ConvFact + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * ConvFact + clsCommon.myCdbl(gr.Cells(colLeak).Value) * ConvFact + clsCommon.myCdbl(gr.Cells(colShortage).Value) * ConvFact
    '                            Else
    '                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * 1 + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * 1 + clsCommon.myCdbl(gr.Cells(colLeak).Value) * 1 + clsCommon.myCdbl(gr.Cells(colShortage).Value) * 1
    '                            End If
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    Else
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * currentconversion1 + 100 = gr.Cells(colMRP).Value Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    End If
    '                Else
    '                    Dim currentconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and UOM_Code = '" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
    '                    If currentconversion = 1 Then
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value * strconversion + 100 Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
    '                            totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
    '                        End If
    '                    Else
    '                        If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value Then
    '                            total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End If
    '    Return total1
    'End Function
    Public Shared Function GetItems(ByVal ItemType As String)
        Dim ArrItem As New List(Of clsItemMaster)
        Try
            Dim Qry As String = "Select Item_Code, Item_Desc, Convert(Decimal(18,2),Cost) as itemCost  from TSPL_ITEM_MASTER Where Item_Type='" + ItemType + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Dim objTr As New clsItemMaster()
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Cost = clsCommon.myCdbl(dr("itemCost"))
                    ArrItem.Add(objTr)
                Next
            End If
            Return ArrItem
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ArrItem
    End Function
    Public Shared Function IsItemHaveEmptyValue(ByVal strICode As String) As Boolean
        Dim qry As String = "select top 1 (Empty_Value_Bottle+Empty_Value_Shell) as EmptyValue from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' order by Start_Date desc"
        Dim EmptyValue As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If EmptyValue > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function IsSerializeItem(ByVal strICode As String) As Boolean
        Dim qry As String = "select Is_Serial_Item from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1, True, False)
    End Function
    Public Shared Function GetItemTypeFromMaster(ByVal strICode As String) As String
        Return GetItemTypeFromMaster(strICode, Nothing)
    End Function
    Public Shared Function GetItemTypeFromMaster(ByVal strICode As String, ByVal tran As SqlTransaction) As String
        Dim qry As String = "select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))
    End Function
    Public Shared Function IsMRPItem(ByVal strICode As String, ByVal tran As SqlTransaction) As Boolean
        Dim qry As String = "select Is_MRP from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran)) = 1, True, False)
    End Function
    Public Shared Function IsMRPItem(ByVal strICode As String) As Boolean
        Return IsMRPItem(strICode, Nothing)
    End Function
    Public Shared Function IsPickAutoSerializeItem(ByVal strICode As String) As Boolean
        Dim qry As String = "select Is_Pick_Auto_SrNo from TSPL_ITEM_MASTER where Item_Code='" + strICode + "'"
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1, True, False)
    End Function
    '======================created by preeti gupta Against Ticket No[BHA/18/05/18-000027]
    Public Shared Function chkCanorCarte(ByVal ItemCode As String, ByVal CRATE As Integer, ByVal CAN As Integer, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(ItemCode) <= 0 Then
            Return True
        End If
        Dim CountCrate As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code not in ('" + ItemCode + "') and Crate=1", trans)
        Dim CountCAN As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code not in ('" + ItemCode + "') and CAN=1", trans)
        If CRATE = 1 Then
            If Not CountCrate = 0 Then
                Throw New Exception("CRATE is already created for other item")
                Return False
            End If
        End If
        If CAN = 1 Then
            If Not CountCAN = 0 Then
                Throw New Exception("CAN is already created for other item")
                Return False
            End If
        End If
        Return True
    End Function
    Public Function SaveDataRMOther(ByVal obj As clsItemMaster, ByVal ArrDatabase As List(Of String), ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Item_Code, "TSPL_ITEM_MASTER", "Item_Code", "TSPL_ITEM_UOM_DETAIL", "Item_Code", "TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER", "Item_Code", trans)
            End If
            Dim qry As String = "delete from TSPL_ITEM_UOM_DETAIL where Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, ArrDatabase, trans)
            qry = "delete from TSPL_ITEM_MASTER_CATEGORY where Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, ArrDatabase, trans)
            qry = "delete from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER where Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, ArrDatabase, trans)
            qry = "delete from TSPL_ITEM_SCHEDULE_PENALTY where Against_Schedule_PK_Id in (select PK_ID from TSPL_ITEM_SCHEDULE where Item_Code='" + obj.Item_Code + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ITEM_SCHEDULE where Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "Short_Description", obj.Item_Short_Desc)
            '=============Added by preeti gupta Against Ticket no[ERO/10/05/18-000302]
            clsCommon.AddColumnsForChange(coll, "Alies_Name", obj.Alies_Name)
            clsCommon.AddColumnsForChange(coll, "Alies_Name2", obj.Alies_Name2)
            clsCommon.AddColumnsForChange(coll, "Alies_Name3", obj.Alies_Name3)
            '=========================================================================
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Part_No", obj.Part_No)
            clsCommon.AddColumnsForChange(coll, "Drawing_No", obj.Drawing_No)
            clsCommon.AddColumnsForChange(coll, "Purchase_Price", obj.std_pur_rate)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
            clsCommon.AddColumnsForChange(coll, "Structure_Desc", obj.Structure_Desc)
            clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", obj.Purchase_Class_Code)
            clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", obj.Sale_Class_Code)
            clsCommon.AddColumnsForChange(coll, "item_category", obj.item_category, True)
            clsCommon.AddColumnsForChange(coll, "Sub_item_category", obj.Sub_item_category, True)
            clsCommon.AddColumnsForChange(coll, "TypeOfItm", obj.TypeOfItm, True)
            clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", obj.Cheapter_Heads)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type) ''R OR O
            '' newly added column SubItemType-
            clsCommon.AddColumnsForChange(coll, "SubItemType", obj.SubItemType) ''Direct or Packaging
            '' end newly added column SubItemType
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Morning", IIf(obj.Morning, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Chilled_Freezen", IIf(obj.Chilled_Freezen, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IsTaxable", IIf(obj.IsTaxable, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Cost", obj.Cost)
            clsCommon.AddColumnsForChange(coll, "tolerence", obj.Tolerance)
            clsCommon.AddColumnsForChange(coll, "Production_Tolerance", obj.Production_Tolerance)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Asset_Life", obj.Asset_Life)
            clsCommon.AddColumnsForChange(coll, "Warranty_Period", obj.Warranty_period)
            clsCommon.AddColumnsForChange(coll, "Active", IIf(obj.Active, 1, 0))
            clsCommon.AddColumnsForChange(coll, "AlternativeItem", obj.AlternativeItem)
            clsCommon.AddColumnsForChange(coll, "Sku_Seq", clsCommon.myCdbl(obj.Sku_Seq))
            clsCommon.AddColumnsForChange(coll, "DcsSeqNo", clsCommon.myCdbl(obj.DcsSeqNo))
            clsCommon.AddColumnsForChange(coll, "Is_DisplayDemand", clsCommon.myCdbl(obj.Is_DisplayDemand))
            clsCommon.AddColumnsForChange(coll, "Is_ExcludeAPP", clsCommon.myCdbl(obj.Is_ExcludeAPP))
            clsCommon.AddColumnsForChange(coll, "ItemSpecification", obj.ItemSpecification)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Item_Category_Struct_Code", obj.Item_Category_Struct_Code, True)
            clsCommon.AddColumnsForChange(coll, "Is_Serial_Item", IIf(obj.Is_Serial_Item, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Pick_Auto_SrNo", IIf(obj.Is_Pick_Auto_SrNo, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Serial_Counter", obj.Serial_Counter)
            clsCommon.AddColumnsForChange(coll, "Warranty_Code", obj.Warranty_Code, True)
            clsCommon.AddColumnsForChange(coll, "Weight_UOM", obj.Weight_UOM)
            clsCommon.AddColumnsForChange(coll, "Weight_Value", obj.Weight_Value)
            clsCommon.AddColumnsForChange(coll, "ITF_Code", obj.ITFCode)
            clsCommon.AddColumnsForChange(coll, "Is_MRP", IIf(obj.Is_MRP, 1, 0))
            '============ROhit=============================
            clsCommon.AddColumnsForChange(coll, "Is_Purchaseable", IIf(obj.Is_Purchaseable_item, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_AllowQC_ON_Purchase", IIf(obj.Is_Allow_QC, 1, 0))
            '================================================
            clsCommon.AddColumnsForChange(coll, "Is_FreshItem", IIf(obj.Is_FreshItem, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Batch_Item", IIf(obj.Is_Batch_Item, 1, 0))
            clsCommon.AddColumnsForChange(coll, "RAL", IIf(obj.RAL, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Rate_Change_OnDairyDispatch", obj.Is_Rate_Change_OnDairyDispatch)
            clsCommon.AddColumnsForChange(coll, "Is_QC_SNF_Based", obj.Is_QC_SNF_Based)
            clsCommon.AddColumnsForChange(coll, "Is_AllowQC_ON_Production", obj.Is_AllowQC_ON_Production)
            clsCommon.AddColumnsForChange(coll, "FG_for_CF_RPT", obj.FG_for_CF_RPT)
            clsCommon.AddColumnsForChange(coll, "AllowSRNWithoutShortReject", obj.AllowSRNWithoutShortReject)
            clsCommon.AddColumnsForChange(coll, "Is_Ambient", IIf(obj.Is_Ambient, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Tax_Exempted", obj.Tax_Exempted)
            '' Anubhooti 11-Sep-2014 BM00000003891
            clsCommon.AddColumnsForChange(coll, "Is_CrateType", IIf(obj.Is_CrateType, 1, 0))
            clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account, True)
            ''
            'If Not String.IsNullOrEmpty(obj.Rack_No) Then
            clsCommon.AddColumnsForChange(coll, "Rack_No", obj.Rack_No)
            'End If
            clsCommon.AddColumnsForChange(coll, "Product_Type", obj.Product_Type)
            clsCommon.AddColumnsForChange(coll, "CSA_Type", obj.CSA_Type)
            clsCommon.AddColumnsForChange(coll, "Item_used_as", obj.Item_used_as) ''S OR I
            clsCommon.AddColumnsForChange(coll, "CreateSepAssetForEachQty", obj.CreateSepAssetForEachQty)
            ' BM00000007860 BM00000007910
            clsCommon.AddColumnsForChange(coll, "Warranty_Applied_From", obj.Warranty_Applied_From, True)
            clsCommon.AddColumnsForChange(coll, "Is_Scheme_Item", obj.Is_Scheme_Item)
            clsCommon.AddColumnsForChange(coll, "Distributor_Commission", obj.Distributor_Commission)
            clsCommon.AddColumnsForChange(coll, "CNF_Commission", obj.CNF_Commission)
            clsCommon.AddColumnsForChange(coll, "Correction_Factor", obj.Correction_Factor)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account, True)
            clsCommon.AddColumnsForChange(coll, "Is_Auto_Weighment", IIf(obj.Is_Auto_Weighment, 1, 0))
            clsCommon.AddColumnsForChange(coll, "tech_shelf_life", obj.shelflife)
            clsCommon.AddColumnsForChange(coll, "Min_shelf_life", obj.Min_shelf_life)
            clsCommon.AddColumnsForChange(coll, "Skip_GST", IIf(obj.Skip_GST, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Power_And_Fuel", IIf(obj.Is_Power_And_Fuel, 1, 0))
            clsCommon.AddColumnsForChange(coll, "HSN_Code", obj.HSNCode)
            clsCommon.AddColumnsForChange(coll, "STD_FatPer", obj.STD_FatPer)
            clsCommon.AddColumnsForChange(coll, "STD_SNFPer", obj.STD_SNFPer)
            clsCommon.AddColumnsForChange(coll, "Crate", IIf(obj.Crate, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Can", IIf(obj.Can, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_CAN_Type", IIf(obj.Is_CAN_Type, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Scrap_Item", IIf(obj.Is_Scrap_Item, 1, 0))
            If obj.Is_Scrap_Item = True Then
                clsCommon.AddColumnsForChange(coll, "Scrap_Item_Code", obj.Scrap_Item_Code, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Scrap_Item_Code", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Is_Milk_Pouch", IIf(obj.Is_Milk_Pouch, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Advance_Required", IIf(obj.Is_Advance_Required, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Leakage_Not_Applicable", IIf(obj.Is_Leakage_Not_Applicable, 1, 0))
            clsCommon.AddColumnsForChange(coll, "FG_for_CF", obj.FG_for_CF)
            clsCommon.AddColumnsForChange(coll, "BomBuildQty", obj.BomBuildQty)
            clsCommon.AddColumnsForChange(coll, "NIR_QC", IIf(obj.NIR_QC, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Insurance", obj.Is_Insurance)
            If obj.Is_Insurance = 1 Then
                clsCommon.AddColumnsForChange(coll, "InsuranceNo", obj.InsuranceNo, True)
                clsCommon.AddColumnsForChange(coll, "InsuranceFromDate", clsCommon.GetPrintDate(obj.InsuranceFromDate, "dd/MMM/yyyy"), True)
                clsCommon.AddColumnsForChange(coll, "InsuranceToDate", clsCommon.GetPrintDate(obj.InsuranceToDate, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "InsuranceNo", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "InsuranceFromDate", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "InsuranceToDate", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Marketing_Seq", clsCommon.myCdbl(obj.Marketing_Seq))
            clsCommon.AddColumnsForChange(coll, "Visual_QC", IIf(obj.Visual_QC, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Security_Deduction", obj.Security_Deduction)
            clsCommon.AddColumnsForChange(coll, "ApplyRoundingInStdProd", IIf(obj.ApplyRoundingInStdProd, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Item_Desc_Hindi", obj.Item_Desc_Hindi, True, True)
            clsCommon.AddColumnsForChange(coll, "Short_Description_Hindi", obj.Item_Short_Desc_Hindi, True, True)
            clsCommon.AddColumnsForChange(coll, "Alies_Name_Hindi", obj.Alies_Name_Hindi, True, True)
            clsCommon.AddColumnsForChange(coll, "BuyBackType", obj.BuyBackType, True, True)
            clsCommon.AddColumnsForChange(coll, "BuyBackValue", obj.BuyBackValue, True, True)
            If isNewEntry Then
                ' If clsCommon.myLen(obj.Item_Code) <= 0 Then 
                ' Ticket No : ERO/11/07/19-000679 By Prabhakar
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, trans)) = 1 Then
                    'obj.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, obj.Item_Type, trans))
                    qry = "SELECT PREFIX FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + clsCommon.myCstr(obj.Item_Type) + "'"
                    obj.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    qry = "update TSPL_ITEM_TYPE_MASTER set PREFIX='" + clsCommon.incval(obj.Item_Code) + "' where ITEM_TYPE_CODE='" + clsCommon.myCstr(obj.Item_Type) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                'End If
                If clsCommon.myLen(obj.Item_Code) <= 0 Then
                    Throw New Exception("Item Code not found to save")
                End If
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDatabase, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDatabase, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "Item_Code='" + obj.Item_Code + "'", trans)
            End If
            clsItemUOMDetails.SaveData(obj.Item_Code, obj.ArrUomDetails, ArrDatabase, trans)
            clsItemMasterCategory.SaveData(obj.Item_Code, obj.ArrItemMasterCategory, ArrDatabase, trans)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.Item_Code, obj.arrCustomFields, trans)
            clsItemMaster.SaveData_Parameter(obj.Item_Code, obj.Arr_Param, trans)
            chkCanorCarte(obj.Item_Code, IIf(obj.Crate, 1, 0), IIf(obj.Can, 1, 0), trans)
            clsItemPurchaseQCParameter.SaveData(obj.Item_Code, obj.Arr_Purchase_QC_Parameter, trans)
            clsItemSchedule.SaveData(obj.Item_Code, obj.ArrSchedule, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData_Parameter(ByVal itemcode As String, ByVal arr As List(Of clsItemMaster), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsItemMaster()
            Dim isSaved As Boolean = True
            If arr.Count <= 0 Then
                'Return True
            End If
            Dim coll As New Hashtable()
            Dim qry As String = "delete from TSPL_ITEM_QC_PARAMETER_MASTER where item_code='" + itemcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each objtr As clsItemMaster In arr
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Item_Code", itemcode)
                clsCommon.AddColumnsForChange(coll, "Code", objtr.paramcode)
                clsCommon.AddColumnsForChange(coll, "Lower_range", objtr.Lrange)
                clsCommon.AddColumnsForChange(coll, "Upper_range", objtr.Urange)
                clsCommon.AddColumnsForChange(coll, "Status", objtr.status)
                clsCommon.AddColumnsForChange(coll, "Value1", objtr.value1)
                clsCommon.AddColumnsForChange(coll, "Value2", objtr.value2)
                clsCommon.AddColumnsForChange(coll, "Actual_Range", objtr.Actual_range)
                clsCommon.AddColumnsForChange(coll, "Actual_Status", objtr.actual_status)
                clsCommon.AddColumnsForChange(coll, "Actual_Value", objtr.actual_value)
                clsCommon.AddColumnsForChange(coll, "StandardRate", objtr.StandardRate)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_QC_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                qry = " update TSPL_ITEM_MASTER set STD_FatPer=QC.Fat_Per,STD_SNFPer=QC.SNF_Per from (select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from ( select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range else 0 end) as Fat_Per, " &
                      " (case when QCP.Type='SNF' then Item_QCP.Actual_Range else 0  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP  left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code where Item_QCP.Item_Code='" + itemcode + "') as QC  group by Item_Code) QC " &
                      " where TSPL_ITEM_MASTER.Item_Code=QC.Item_Code and TSPL_ITEM_MASTER.Item_Code='" + itemcode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetDataRMOther(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Try
            Dim qry As String = "select tspl_item_master.*,TSPL_WARRANTY_MASTER.Name as Warranty_Name,CreateSepAssetForEachQty,Tspl_Customer_account_Set.cust_acct_desc from tspl_item_master "
            qry += " left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code "
            qry += " left outer join Tspl_Customer_account_Set on Tspl_Customer_account_Set.cust_account=tspl_item_master.cust_account "
            qry += " where 2=2 "
            'Ticket No-ALF/05/03/19-000093 show only active item
            Select Case NavType
                Case NavigatorType.Current
                    qry += " and tspl_item_master.item_code in ('" + strCode + "')"
                Case NavigatorType.Next
                    qry += " and tspl_item_master.item_code in (select min(item_code) from tspl_item_master where tspl_item_master.Active=1 and item_code  >'" + strCode + "')"
                Case NavigatorType.First
                    qry += " and tspl_item_master.item_code in (select MIN(item_code) from tspl_item_master where tspl_item_master.Active=1 ) "
                Case NavigatorType.Last
                    qry += " and tspl_item_master.item_code in (select Max(item_code) from tspl_item_master where tspl_item_master.Active=1 )"
                Case NavigatorType.Previous
                    qry += " and tspl_item_master.item_code in (select Max(item_code) from tspl_item_master where tspl_item_master.Active=1 and item_code  <'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster
                obj.shelflife = clsCommon.myCstr(dt.Rows(0)("tech_shelf_life"))
                obj.Min_shelf_life = clsCommon.myCstr(dt.Rows(0)("Min_shelf_life"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Item_Desc_Hindi = clsCommon.myCstr(dt.Rows(0)("Item_Desc_Hindi"))
                obj.Part_No = clsCommon.myCstr(dt.Rows(0)("Part_No"))
                obj.Drawing_No = clsCommon.myCstr(dt.Rows(0)("Drawing_No"))
                obj.Item_Short_Desc = clsCommon.myCstr(dt.Rows(0)("Short_Description"))
                obj.Item_Short_Desc_Hindi = clsCommon.myCstr(dt.Rows(0)("Short_Description_Hindi"))
                obj.Alies_Name = clsCommon.myCstr(dt.Rows(0)("Alies_Name"))
                obj.Alies_Name_Hindi = clsCommon.myCstr(dt.Rows(0)("Alies_Name_Hindi"))
                obj.Alies_Name2 = clsCommon.myCstr(dt.Rows(0)("Alies_Name2"))
                obj.Alies_Name3 = clsCommon.myCstr(dt.Rows(0)("Alies_Name3"))
                obj.std_pur_rate = clsCommon.myCdbl(dt.Rows(0)("Purchase_Price"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.Cost = clsCommon.myCdbl(dt.Rows(0)("Cost"))
                obj.Tolerance = clsCommon.myCdbl(dt.Rows(0)("tolerence"))
                obj.Production_Tolerance = clsCommon.myCdbl(dt.Rows(0)("Production_Tolerance"))
                obj.Structure_Code = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
                obj.Structure_Desc = clsCommon.myCstr(dt.Rows(0)("Structure_Desc"))
                obj.Purchase_Class_Code = clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code"))
                obj.Sale_Class_Code = clsCommon.myCstr(dt.Rows(0)("Sale_Class_Code"))
                obj.item_category = clsCommon.myCstr(dt.Rows(0)("item_category"))
                obj.Sub_item_category = clsCommon.myCstr(dt.Rows(0)("Sub_item_category"))
                obj.TypeOfItm = clsCommon.myCstr(dt.Rows(0)("TypeOfItm"))
                obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                obj.Cheapter_Heads = clsCommon.myCstr(dt.Rows(0)("Cheapter_Heads"))
                obj.Item_Category_Struct_Code = clsCommon.myCstr(dt.Rows(0)("Item_Category_Struct_Code"))
                obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                obj.Asset_Life = clsCommon.myCdbl(dt.Rows(0)("Asset_Life"))
                obj.Warranty_period = clsCommon.myCdbl(dt.Rows(0)("Warranty_Period"))
                obj.Active = IIf(clsCommon.myCdbl(dt.Rows(0)("Active")) = 1, True, False)
                obj.AlternativeItem = clsCommon.myCstr(dt.Rows(0)("AlternativeItem"))
                obj.ItemSpecification = clsCommon.myCstr(dt.Rows(0)("ItemSpecification"))
                obj.Modify_Date = clsCommon.myCstr(dt.Rows(0)("Modify_Date"))
                obj.Morning = IIf(clsCommon.myCdbl(dt.Rows(0)("Morning")) = 1, True, False)
                obj.Chilled_Freezen = IIf(clsCommon.myCdbl(dt.Rows(0)("Chilled_Freezen")) = 1, True, False)
                obj.IsTaxable = IIf(clsCommon.myCdbl(dt.Rows(0)("IsTaxable")) = 1, True, False)
                '' newly added column SubItemType
                obj.SubItemType = clsCommon.myCstr(dt.Rows(0)("SubItemType"))
                '' End newly added column SubItemType
                obj.Is_Serial_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Serial_Item")) = 1, True, False)
                obj.Is_Pick_Auto_SrNo = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Pick_Auto_SrNo")) = 1, True, False)
                obj.Serial_Counter = clsCommon.myCstr(dt.Rows(0)("Serial_Counter"))
                obj.Warranty_Code = clsCommon.myCstr(dt.Rows(0)("Warranty_Code"))
                obj.Warranty_Name = clsCommon.myCstr(dt.Rows(0)("Warranty_Name"))
                obj.Weight_UOM = clsCommon.myCstr(dt.Rows(0)("Weight_UOM"))
                obj.Weight_Value = clsCommon.myCdbl(dt.Rows(0)("Weight_Value"))
                obj.Sku_Seq = clsCommon.myCdbl(dt.Rows(0)("Sku_Seq"))
                obj.DcsSeqNo = clsCommon.myCdbl(dt.Rows(0)("DcsSeqNo"))
                obj.Is_DisplayDemand = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_DisplayDemand")) = 1, True, False)
                obj.Is_ExcludeAPP = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_ExcludeAPP")) = 1, True, False)
                obj.BuyBackType = clsCommon.myCdbl(dt.Rows(0)("BuyBackType"))
                obj.BuyBackValue = clsCommon.myCdbl(dt.Rows(0)("BuyBackValue"))
                obj.ITFCode = clsCommon.myCstr(dt.Rows(0)("ITF_CODE"))
                obj.Is_MRP = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1, True, False)
                obj.AllowSRNWithoutShortReject = clsCommon.myCdbl(dt.Rows(0)("AllowSRNWithoutShortReject"))
                obj.Is_Rate_Change_OnDairyDispatch = clsCommon.myCdbl(dt.Rows(0)("Is_Rate_Change_OnDairyDispatch"))
                obj.Is_QC_SNF_Based = clsCommon.myCdbl(dt.Rows(0)("Is_QC_SNF_Based"))
                obj.Is_FreshItem = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_FreshItem")) = 1, True, False)
                obj.Is_Batch_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Batch_Item")) = 1, True, False)
                obj.RAL = IIf(clsCommon.myCdbl(dt.Rows(0)("RAL")) = 1, True, False)
                obj.Is_Ambient = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Ambient")) = 1, True, False)
                obj.Tax_Exempted = clsCommon.myCdbl(dt.Rows(0)("Is_Tax_Exempted"))
                obj.Rack_No = clsCommon.myCstr(dt.Rows(0)("Rack_No"))
                obj.Product_Type = clsCommon.myCstr(dt.Rows(0)("Product_Type"))
                obj.CSA_Type = clsCommon.myCstr(dt.Rows(0)("CSA_Type"))
                obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
                obj.HSNCode = clsCommon.myCstr(dt.Rows(0)("HSN_Code"))
                obj.FG_for_CF = clsCommon.myCdbl(dt.Rows(0)("FG_for_CF"))
                obj.Is_AllowQC_ON_Production = clsCommon.myCdbl(dt.Rows(0)("Is_AllowQC_ON_Production"))
                obj.FG_for_CF_RPT = clsCommon.myCdbl(dt.Rows(0)("FG_for_CF_RPT"))
                obj.BomBuildQty = clsCommon.myCdbl(dt.Rows(0)("BomBuildQty"))
                obj.NIR_QC = (clsCommon.myCdbl(dt.Rows(0)("NIR_QC")) = 1)
                obj.Cust_Account = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
                obj.Cust_Account_Name = clsCommon.myCstr(dt.Rows(0)("cust_acct_desc"))
                obj.Is_Scheme_Item = clsCommon.myCBool(dt.Rows(0)("Is_Scheme_Item"))
                obj.Distributor_Commission = clsCommon.myCdbl(dt.Rows(0)("Distributor_Commission"))
                obj.CNF_Commission = clsCommon.myCdbl(dt.Rows(0)("CNF_Commission"))
                obj.Correction_Factor = clsCommon.myCdbl(dt.Rows(0)("Correction_Factor"))
                '================Rohit============================
                obj.Is_Purchaseable_item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Purchaseable")) = 1, True, False)
                obj.Is_Allow_QC = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_AllowQC_ON_Purchase")) = 1, True, False)
                '==================================================
                '' Anubhooti 11-Sep-2014 BM00000003847
                obj.Is_CrateType = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_CrateType")) = 1, True, False)
                obj.GL_Account = clsCommon.myCstr(dt.Rows(0)("GL_Account"))
                ''
                '=========ROhit================
                obj.Item_used_as = clsCommon.myCstr(dt.Rows(0)("Item_used_as"))
                obj.CreateSepAssetForEachQty = clsCommon.myCdbl(dt.Rows(0)("CreateSepAssetForEachQty"))
                '============================
                ' BM00000007860 BM00000007910
                obj.Warranty_Applied_From = clsCommon.myCstr(dt.Rows(0)("Warranty_Applied_From"))
                obj.Is_Auto_Weighment = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Auto_Weighment")) = 1, True, False)
                obj.Skip_GST = IIf(clsCommon.myCdbl(dt.Rows(0)("Skip_GST")) = 1, True, False)
                obj.Is_Power_And_Fuel = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Power_And_Fuel")) = 1, True, False)
                obj.STD_FatPer = clsCommon.myCdbl(dt.Rows(0)("STD_FatPer"))
                obj.STD_SNFPer = clsCommon.myCdbl(dt.Rows(0)("STD_SNFPer"))
                obj.Crate = IIf(clsCommon.myCdbl(dt.Rows(0)("Crate")) = 1, True, False)
                obj.Can = IIf(clsCommon.myCdbl(dt.Rows(0)("Can")) = 1, True, False)
                obj.Is_CAN_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_CAN_Type")) = 1, True, False)
                obj.Is_Scrap_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Scrap_Item")) = 1, True, False)
                obj.Is_Milk_Pouch = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Milk_Pouch")) = 1, True, False)
                obj.Is_Advance_Required = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Advance_Required")) = 1, True, False)
                obj.Scrap_Item_Code = clsCommon.myCstr(dt.Rows(0)("Scrap_Item_Code"))
                obj.Is_Leakage_Not_Applicable = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Leakage_Not_Applicable")) = 1, True, False)
                obj.Is_Insurance = clsCommon.myCdbl(dt.Rows(0)("Is_Insurance"))
                If clsCommon.myCdbl(dt.Rows(0)("Is_Insurance")) > 0 Then
                    obj.InsuranceNo = clsCommon.myCstr(dt.Rows(0)("InsuranceNo"))
                    obj.InsuranceFromDate = clsCommon.myCDate(dt.Rows(0)("InsuranceFromDate"), "dd/MMM/yyyy")
                    obj.InsuranceToDate = clsCommon.myCDate(dt.Rows(0)("InsuranceToDate"), "dd/MMM/yyyy")
                End If
                obj.Marketing_Seq = clsCommon.myCdbl(dt.Rows(0)("Marketing_Seq"))
                obj.Visual_QC = (clsCommon.myCDecimal(dt.Rows(0)("Visual_QC")) = 1)
                obj.Security_Deduction = clsCommon.myCDecimal(dt.Rows(0)("Security_Deduction"))
                obj.ApplyRoundingInStdProd = (clsCommon.myCDecimal(dt.Rows(0)("ApplyRoundingInStdProd")) = 1)
                ''richa agarwal TEC/19/12/18-000383 27 Dec,2018
                qry = " select Item_Code,UOM_Code,UOM_Description,Conversion_Factor,Stocking_Unit,Default_UOM,Print_UOM,Gross_Weight,Net_Weight,Job_Work_Rate,pieces,Item_Cost,Custom_Conversion from TSPL_ITEM_UOM_DETAIL where Item_Code='" + obj.Item_Code + "' order by Stocking_Unit desc"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.ArrUomDetails = New List(Of clsItemUOMDetails)()
                    For Each dr As DataRow In dt.Rows
                        Dim objtr As New clsItemUOMDetails()
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.UOM_Code = clsCommon.myCstr(dr("UOM_Code"))
                        objtr.UOM_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + objtr.UOM_Code + "'"))
                        objtr.Conversion_Factor = clsCommon.myCdbl(dr("Conversion_Factor"))
                        objtr.Stocking_Unit = clsCommon.myCstr(dr("Stocking_Unit"))
                        ''added by richa agarwal against ticket no BM00000004327
                        objtr.Default_UOM = clsCommon.myCdbl(dr("Default_UOM"))
                        objtr.Print_UOM = clsCommon.myCdbl(dr("Print_UOM"))
                        ''===========================
                        objtr.Pieces = clsCommon.myCdbl(dr("pieces"))
                        objtr.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                        objtr.Net_Weight = clsCommon.myCdbl(dr("Net_Weight"))
                        objtr.Job_Work_Rate = clsCommon.myCdbl(dr("Job_Work_Rate"))
                        objtr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                        objtr.Custom_Conversion = (clsCommon.myCdbl(dr("Custom_Conversion")) = 1)
                        obj.ArrUomDetails.Add(objtr)
                    Next
                End If
                qry = " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no as CATEGORY_LEVEL,TSPL_ITEM_MASTER_CATEGORY.*,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as TSPL_ITEM_CATEGORY_LEVEL_Desription,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as TSPL_ITEM_CATEGORY_LEVEL_VALUES_Descirption,"
                qry += " TSPL_ITEM_CATEGORY_LEVEL_VALUES.Bin_No as Item_Cagetory_Values_BIN_NO from TSPL_ITEM_MASTER_CATEGORY  left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and isnull(TSPL_ITEM_CATEGORY_LEVEL.form_type,'item')='Item' "
                qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values "
                qry += " and TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE and isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'item')='item' and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_LEVEL.form_type "
                qry += " left outer join TSPL_ITEM_CATEGORY_STRUCT_DETAIL on TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_CATEGORY_LEVEL.item_category_code and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type and TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type=TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type "
                qry += " where Item_Code='" + obj.Item_Code + "' and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE='" + obj.Item_Category_Struct_Code + "' order by TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.ArrItemMasterCategory = New List(Of clsItemMasterCategory)()
                    For Each dr As DataRow In dt.Rows
                        Dim objtr As New clsItemMasterCategory()
                        objtr.Item_code = clsCommon.myCstr(dr("Item_code"))
                        objtr.SNO = clsCommon.myCdbl(dr("CATEGORY_LEVEL"))
                        objtr.Item_Category_Code = clsCommon.myCstr(dr("Item_Category_Code"))
                        objtr.Item_Category_Code_Desc = clsCommon.myCstr(dr("TSPL_ITEM_CATEGORY_LEVEL_Desription"))
                        objtr.Item_Cagetory_Values = clsCommon.myCstr(dr("Item_Cagetory_Values"))
                        objtr.Item_Cagetory_Values_Desc = clsCommon.myCstr(dr("TSPL_ITEM_CATEGORY_LEVEL_VALUES_Descirption"))
                        objtr.Item_Cagetory_Values_BIN_NO = clsCommon.myCstr(dr("Item_Cagetory_Values_BIN_NO"))
                        objtr.Master_Value = clsCommon.myCstr(dr("Master_Value"))
                        objtr.SKU_Value = clsCommon.myCstr(dr("SKU_Value"))
                        obj.ArrItemMasterCategory.Add(objtr)
                    Next
                End If
                qry = "select TSPL_ITEM_QC_PARAMETER_MASTER.code,TSPL_ITEM_QC_PARAMETER_MASTER.lower_range,TSPL_ITEM_QC_PARAMETER_MASTER.upper_range,tspl_parameter_master.description,TSPL_ITEM_QC_PARAMETER_MASTER.status,TSPL_ITEM_QC_PARAMETER_MASTER.value1,TSPL_ITEM_QC_PARAMETER_MASTER.value2,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status,TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + obj.Item_Code + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    obj.Arr_Param = New List(Of clsItemMaster)
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsItemMaster
                        objtr.paramcode = clsCommon.myCstr(dr("code"))
                        objtr.paramdesc = clsCommon.myCstr(dr("description"))
                        objtr.Lrange = clsCommon.myCdbl(dr("lower_range"))
                        objtr.Urange = clsCommon.myCdbl(dr("upper_range"))
                        objtr.status = clsCommon.myCstr(dr("status"))
                        objtr.value1 = clsCommon.myCstr(dr("value1"))
                        objtr.value2 = clsCommon.myCstr(dr("value2"))
                        objtr.Actual_range = clsCommon.myCdbl(dr("Actual_Range"))
                        objtr.actual_status = clsCommon.myCstr(dr("Actual_Status"))
                        objtr.actual_value = clsCommon.myCstr(dr("Actual_Value"))
                        objtr.StandardRate = clsCommon.myCdbl(dr("StandardRate"))
                        obj.Arr_Param.Add(objtr)
                    Next
                End If
                obj.Arr_Purchase_QC_Parameter = clsItemPurchaseQCParameter.GetData(obj.Item_Code, Nothing)
                obj.ArrSchedule = clsItemSchedule.GetData(obj.Item_Code, Nothing)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function GetStoreAdjustmentItemType(ByVal strICode As String)
        Dim qry As String = "select Case when  Item_Type='O' or Item_Type='0'  then 'OT' else  case when Item_Type='R' then 'RM' else '' end end as StoreAdjustmentItemType    from tspl_item_master where Item_Code='" + strICode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetStoreAdjustmentItemTypeWithTrans(ByVal strICode As String, ByVal trans As SqlTransaction)
        Dim qry As String = "select Case when  Item_Type='O' or Item_Type='0'  then 'OT' else  case when Item_Type='R' then 'RM' else '' end end as StoreAdjustmentItemType    from tspl_item_master where Item_Code='" + strICode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetMRPInBottle(ByVal strICode As String, ByVal strUOm As String, ByVal MRP As Double) As Double
        Dim dblReturnMRP As Double = MRP
        Dim qry As String = "select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code= case when '" + strUOm + "'='FB' then 'FC' else 'FB' end"
        Dim dblConvFac As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return IIf(dblConvFac = 0, MRP, Math.Round(MRP / dblConvFac, 2, MidpointRounding.AwayFromZero))
    End Function
    Public Shared Function GetItemUOMCount(ByVal strICode As String) As Integer
        Dim qry As String = "select count(distinct UOM_Code) as Tot from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strICode & "'"
        Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return Count
    End Function
    Public Shared Function GetItemSerialCounter(ByVal strItemCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Serial_Counter from TSPL_ITEM_MASTER where Item_Code='" + strItemCode + "'"
        Dim strSerialCounter As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        If clsCommon.myLen(strSerialCounter) > 0 Then
            qry = "Update TSPL_ITEM_MASTER set Serial_Counter='" + clsCommon.incval(strSerialCounter) + "' where Item_Code='" + strItemCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Else
            Throw New Exception("Plase set Serial counter for item " + strItemCode)
        End If
        Return strSerialCounter
    End Function
    Public Shared Function GetMasterOrSKUCategory(ByVal ItemCode As String, ByVal MasterCategory As Boolean, ByVal SKUCategory As Boolean) As Decimal
        Dim str As Decimal = 0
        Try
            If MasterCategory AndAlso Not SKUCategory Then
                str = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select a.description from (select TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Master_Value='1' and TSPL_ITEM_MASTER_CATEGORY.Item_code='" + ItemCode + "')a"))
            ElseIf SKUCategory AndAlso Not MasterCategory Then
                str = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select a.description from (select TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.SKU_Value='1' and TSPL_ITEM_MASTER_CATEGORY.Item_code='" + ItemCode + "')a"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function GetStructureType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "FCM"
        dr("Name") = "Full Cream Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "STM"
        dr("Name") = "Standardized Tonned Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "TM"
        dr("Name") = "Tonned Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "DTM"
        dr("Name") = "Double Tonned Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Chhachh"
        dr("Name") = "Chhachh"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Curd"
        dr("Name") = "Curd"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Flavoured"
        dr("Name") = "Flavoured Milk"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "DG"
        dr("Name") = "Desi Ghee"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "CG"
        dr("Name") = "Cow Ghee"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "DC"
        dr("Name") = "Dairy Cream"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Butter"
        dr("Name") = "Butter"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "DW"
        dr("Name") = "Dairy Whitner"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Lassi"
        dr("Name") = "Lassi"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "SMP"
        dr("Name") = "SMP"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "WMP"
        dr("Name") = "WMP"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Paneer"
        dr("Name") = "Paneer"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Other"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Enum InventoryType
        inventory
        NonInventory
        All
    End Enum
    Public Shared Function getItemTypeQuery(Optional ByVal WhrCls As String = "") As DataTable
        Dim dt As DataTable = New DataTable()
        'Dim Whr = ""
        'If InvType = InventoryType.inventory Then
        '    Whr += " AND IS_NON_INVENTORY=0 "
        'ElseIf InvType = InventoryType.NonInventory Then
        '    Whr += " AND IS_NON_INVENTORY=1 "
        'End If
        'If clsCommon.myLen(WhrCls) > 0 Then
        '    Whr += WhrCls
        'End If
        Dim qry As String = " SELECT '' AS Code,'Select' as Name union SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER WHERE 1=1  " + WhrCls
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
End Class
