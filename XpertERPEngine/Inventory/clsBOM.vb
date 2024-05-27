'===================BM00000003069,Updated By Rohit========================
'--------------BM00000003167----------------------------

Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsBOM

#Region "Variables"
    Public prodcategorycode As String = Nothing
    Public prodcatdesc As String = Nothing
    Public revisionno As String = Nothing
    Public BOM_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public BOM_DATE As Date = Nothing
    Public REVISION_NO As String = Nothing
    Public START_DATE As Date = Nothing
    Public END_DATE As Date = Nothing
    Public STATUS As String = Nothing
    Public IS_DEFAULT As Boolean = Nothing
    Public ATTACHED_DOC As Byte()
    Public ATTACHED_DOC_PATH As String = Nothing
    Public PROD_ITEM_CODE As String = Nothing
    Public PROD_ITEM_NAME As String = Nothing
    Public PROD_QUANTITY As String = Nothing
    Public PROD_ITEM_UNIT_CODE As String = Nothing
    Public MIN_BATCH_SIZE As Decimal = Nothing
    Public APPROVED_BY As String = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Is_OSP As Integer = Nothing

    Public PROD_ITEM_TYPE As String = Nothing
    Public PROD_ITEM_CATEGORY_CODE As String = Nothing
    Public PROD_ITEM_CATEGORY_NAME As String = Nothing

    Public JobWork_Loc As String = Nothing
    '' grid columns
    Public Line_No As Integer
    Public CONSM_ITEM_TYPE As String
    Public CONSM_ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public CONSM_QUANTITY As Decimal
    Public CONSM_ITEM_UNIT_CODE As String
    Public FAT As String = Nothing
    Public SNF As String = Nothing
    Public REMARKS As String = Nothing
    Public Rejection As Decimal = Nothing
    Public alticode As String = Nothing
    Public altunitcode As String = Nothing
    Public altiname As String = Nothing
    Public altitype As String = Nothing
    Public CONSM_ITEM_PRODUCT_TYPE As String

    Public Shared ObjList As List(Of clsBOM)
    Public ObjListBOM As List(Of clsBOMItemDetail)
    Public ObjListBOMOP As List(Of clsBOMStage)
    Public Section_Code As String
    Public Section_Name As String
    Public stagecode As String = Nothing
    Public stagename As String = Nothing
    Public stage_seq As Integer = Nothing
    'Public Arr As New List(Of clsEmpSalaryPayHeadDetails)
    Public IsPost As String = Nothing
    Public ObjCostGroupDetail As List(Of clsBomCostMappingDetails)
    Public OverHeadCostGroupCode As String = Nothing
    Public OverHeadCost As Double = 0

    Public Byproduct_Item_Code As String = Nothing
    Public Byproduct_Item_UOM As String = Nothing
    Public Byproduct_Item_Qty As Decimal
#End Region

    ''while do change in BOM tables then do changes in History tables also.
    Public Shared Function GetBOMFinderWithValidityCheck(ByVal whrcls As String, ByVal strCurrCode As String, ByVal Doc_Date As Date, ByVal isButtonClicked As Boolean) As String
        Dim values As String = ""
        Try
            Dim qry As String = "select TSPL_PP_BOM_HEAD.bom_code as Code,TSPL_PP_BOM_HEAD.bom_date as [BOM Date],TSPL_PP_BOM_HEAD.Description, " & _
                " TSPL_PP_BOM_HEAD.valid_from_date as [Valid From],TSPL_PP_BOM_HEAD.valid_upto_date as [Valid Upto],TSPL_PP_BOM_HEAD.Status, " & _
                " (case when is_osp=1 then (Select top 1 vendor_name from tspl_vendor_master where vendor_code=TSPL_PP_BOM_HEAD.vendor_code) else '' end) as [OSP], " & _
                " tspl_pp_bom_head.ITEM_CATEGORY_CODE as [Production Category],TSPL_PP_BOM_HEAD.prod_item_code as [Main Item Code], " & _
                " tspl_item_master.item_desc as [Item Description],TSPL_PP_BOM_HEAD.prod_item_unit_code as [Unit],TSPL_PP_BOM_HEAD.prod_quantity as [Quantity]," & _
                " TSPL_PP_BOM_HEAD.section_code as [Section Code],tspl_section_master.description as [Section],TSPL_PP_BOM_HEAD.revision_no as [Revision No]," & _
                " (case when TSPL_PP_BOM_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status] from TSPL_PP_BOM_HEAD " & _
                " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_BOM_HEAD.prod_item_code " & _
                " left outer join tspl_section_master on tspl_section_master.section_code=TSPL_PP_BOM_HEAD.section_code "
            If clsCommon.myLen(whrcls) > 0 Then
                whrcls = whrcls & " and '" & clsCommon.GetPrintDate(Doc_Date, "dd-MMM-yyyy") & "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date)"
            Else
                whrcls = " and '" & clsCommon.GetPrintDate(Doc_Date, "dd-MMM-yyyy") & "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date)" '" tspl_pp_bom_head.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            End If
            values = clsCommon.ShowSelectForm("BOMFND", qry, "Code", whrcls, strCurrCode, "Code", isButtonClicked)


        Catch ex As Exception
        End Try
        Return values
    End Function
    Public Shared Function GetBOMFinder(ByVal whrcls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim values As String = ""
        Try
            Dim qry As String = "select TSPL_PP_BOM_HEAD.bom_code as Code,TSPL_PP_BOM_HEAD.bom_date as [BOM Date],TSPL_PP_BOM_HEAD.Description,TSPL_PP_BOM_HEAD.valid_from_date as [Valid From],TSPL_PP_BOM_HEAD.valid_upto_date as [Valid Upto],TSPL_PP_BOM_HEAD.Status,(case when is_osp=1 then (Select top 1 vendor_name from tspl_vendor_master where vendor_code=TSPL_PP_BOM_HEAD.vendor_code) else '' end) as [OSP],tspl_pp_bom_head.ITEM_CATEGORY_CODE as [Production Category],TSPL_PP_BOM_HEAD.prod_item_code as [Main Item Code],tspl_item_master.item_desc as [Item Description],TSPL_PP_BOM_HEAD.prod_item_unit_code as [Unit],TSPL_PP_BOM_HEAD.prod_quantity as [Quantity],TSPL_PP_BOM_HEAD.section_code as [Section Code],tspl_section_master.description as [Section],TSPL_PP_BOM_HEAD.revision_no as [Revision No],(case when TSPL_PP_BOM_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status] from TSPL_PP_BOM_HEAD left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_BOM_HEAD.prod_item_code left outer join tspl_section_master on tspl_section_master.section_code=TSPL_PP_BOM_HEAD.section_code"
            If clsCommon.myLen(whrcls) > 0 Then
                whrcls = whrcls
            Else
                whrcls = "" '" tspl_pp_bom_head.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            End If
            values = clsCommon.ShowSelectForm("BOMFND", qry, "Code", whrcls, strCurrCode, "Code", isButtonClicked, "bom_date")


        Catch ex As Exception
        End Try
        Return values
    End Function

    Public Shared Function GetFatSNFKG_AfterConversion(ByVal Item_Code As String, ByVal Unit_Code As String, ByVal Qty As Decimal, ByVal Fat_Snf_Pers As Decimal, ByVal trans As SqlTransaction) As Decimal

        Dim Kg_Value As Decimal = 0
        If clsCommon.myLen(Item_Code) <= 0 Then
            Return Kg_Value
        End If
        If clsCommon.myLen(Unit_Code) <= 0 Then
            Return Kg_Value
        End If
        Dim qry As String = "select Product_Type,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & Item_Code & "'"
        Dim dt As DataTable
        Dim Wt_uom As String
        Dim Wt_Value As Decimal
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Dim Product_Type As String = clsCommon.myCstr(dt.Rows(0).Item("Product_Type"))
            If Not (clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(Product_Type, "MP") = CompairStringResult.Equal) Then
                Return Kg_Value
            End If
            Wt_uom = clsCommon.myCstr(dt.Rows(0).Item("Weight_UOM"))
            If clsCommon.myLen(Wt_uom) <= 0 Then
                Throw New Exception("Please update Weight UOM in Item Master for Item-" & Item_Code & "")
            End If
            Wt_Value = clsCommon.myCdbl(dt.Rows(0).Item("Weight_Value"))
            If Wt_Value <= 0 Then
                Throw New Exception("Please update Weight Value in Item Master for Item-" & Item_Code & "")
            End If
        Else
            Return Kg_Value
        End If

        Dim Cnvsrn_Factr As Decimal = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans))
        Dim Weight_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
        If clsCommon.myLen(Weight_KG_Unit) <= 0 Then
            Throw New Exception("Please update ProductionFATSNF_KG_Unit in fixed parameter.")
        End If

        Dim KG_Cnvrsn_Value As Decimal = Nothing
        If clsCommon.CompairString(Wt_uom, Weight_KG_Unit) = CompairStringResult.Equal Then
            KG_Cnvrsn_Value = 1
        Else
            '' work done by Panch Raj-Description:Weight UOM conversion fundamental failed because we can define single conversion for a particular Product Type but Product Type -Milk have different convsrsion than Ghee items hence fetching conversion from item uom conversion if KG uom is defined fro that item
            KG_Cnvrsn_Value = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Weight_KG_Unit, trans))
            If KG_Cnvrsn_Value <> 0 Then
                KG_Cnvrsn_Value = 1 / KG_Cnvrsn_Value
                Wt_Value = 1
            Else
                qry = "select top 1 CF from (Select (case when (Container_UOM='" & Wt_uom & "' and Contained_UOM='" & Weight_KG_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_KG_Unit & "' and Contained_UOM='" & Wt_uom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(Item_Code, trans) + "'))aa where isnull(cast(CF as float),0)<>0 order by Product_Type desc"
                KG_Cnvrsn_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            End If
        End If

        If KG_Cnvrsn_Value > 0 Then

            Kg_Value = ((Wt_Value * Qty * Cnvsrn_Factr) * KG_Cnvrsn_Value)
            Kg_Value = System.Math.Round((Kg_Value * Fat_Snf_Pers) / 100, 3)
        Else
            Kg_Value = 0
        End If

        Return Kg_Value
    End Function

    Public Shared Function GetFatSNFPercentage_AfterConversion(ByVal Item_Code As String, ByVal Unit_Code As String, ByVal Qty As Decimal, ByVal Fat_Snf_KG As Decimal, ByVal trans As SqlTransaction) As Decimal
        Return GetFatSNFPercentage_AfterConversion(Item_Code, Unit_Code, Qty, Fat_Snf_KG, trans, False)
    End Function
    Public Shared Function GetFatSNFPercentage_AfterConversion(ByVal Item_Code As String, ByVal Unit_Code As String, ByVal Qty As Decimal, ByVal Fat_Snf_KG As Decimal, ByVal trans As SqlTransaction, ByVal For10decimalPlaces As Boolean) As Decimal
        Dim Percent_Value As Decimal = 0
        If clsCommon.myLen(Item_Code) <= 0 Then
            Return Percent_Value
        End If
        If clsCommon.myLen(Unit_Code) <= 0 Then
            Return Percent_Value
        End If
        Dim qry As String = "select Product_Type,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & Item_Code & "'"
        Dim dt As DataTable
        Dim Wt_uom As String
        Dim Wt_Value As Decimal
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Dim Product_Type As String = clsCommon.myCstr(dt.Rows(0).Item("Product_Type"))
            If Not (clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(Product_Type, "MP") = CompairStringResult.Equal) Then
                Return Percent_Value
            End If
            Wt_uom = clsCommon.myCstr(dt.Rows(0).Item("Weight_UOM"))
            If clsCommon.myLen(Wt_uom) <= 0 Then
                Throw New Exception("Please update Weight UOM in Item Master for Item-" & Item_Code & "")
            End If
            Wt_Value = clsCommon.myCdbl(dt.Rows(0).Item("Weight_Value"))
            If Wt_Value <= 0 Then
                Throw New Exception("Please update Weight Value in Item Master for Item-" & Item_Code & "")
            End If
        Else
            Return Percent_Value
        End If

        Dim Cnvsrn_Factr As Decimal = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans))
        Dim Weight_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
        If clsCommon.myLen(Weight_KG_Unit) <= 0 Then
            Throw New Exception("Please update ProductionFATSNF_KG_Unit in fixed parameter.")
        End If

        Dim KG_Cnvrsn_Value As Decimal = Nothing
        If clsCommon.CompairString(Wt_uom, Weight_KG_Unit) = CompairStringResult.Equal Then
            KG_Cnvrsn_Value = 1
        Else
            KG_Cnvrsn_Value = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Weight_KG_Unit, trans))
            If KG_Cnvrsn_Value <> 0 Then
                KG_Cnvrsn_Value = 1 / KG_Cnvrsn_Value
                Wt_Value = 1
            Else
                qry = "select top 1 CF from (Select (case when (Container_UOM='" & Wt_uom & "' and Contained_UOM='" & Weight_KG_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_KG_Unit & "' and Contained_UOM='" & Wt_uom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(Item_Code, trans) + "'))aa where isnull(cast(CF as float),0)<>0 order by Product_Type desc"
                KG_Cnvrsn_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            End If
        End If
        If KG_Cnvrsn_Value > 0 Then
            Percent_Value = ((Wt_Value * Qty * Cnvsrn_Factr) * KG_Cnvrsn_Value)
            If For10decimalPlaces Then
                Percent_Value = System.Math.Round(clsCommon.myCDivide((Fat_Snf_KG * 100), Percent_Value), 10)
            Else
                Percent_Value = System.Math.Round(clsCommon.myCDivide((Fat_Snf_KG * 100), Percent_Value), 2)
            End If
        Else
            Percent_Value = 0
        End If
        Return Percent_Value
    End Function

    Public Shared Function GetItemCostOfMilk_AfterConversion(ByVal Item_Code As String, ByVal Unit_Code As String, ByVal Qty As Decimal, ByVal Amount As Decimal, ByVal trans As SqlTransaction) As Decimal

        Dim Kg_Value As Decimal = 0
        If clsCommon.myLen(Item_Code) <= 0 Then
            Return Kg_Value
        End If
        If clsCommon.myLen(Unit_Code) <= 0 Then
            Return Kg_Value
        End If
        Dim qry As String = "select Product_Type,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & Item_Code & "'"
        Dim dt As DataTable
        Dim Wt_uom As String
        Dim Wt_Value As Decimal
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Dim Product_Type As String = clsCommon.myCstr(dt.Rows(0).Item("Product_Type"))
            If Not (clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(Product_Type, "MP") = CompairStringResult.Equal) Then
                Return Kg_Value
            End If
            Wt_uom = clsCommon.myCstr(dt.Rows(0).Item("Weight_UOM"))
            If clsCommon.myLen(Wt_uom) <= 0 Then
                Throw New Exception("Please update Weight UOM in Item Master for Item-" & Item_Code & "")
            End If
            Wt_Value = clsCommon.myCdbl(dt.Rows(0).Item("Weight_Value"))
            If Wt_Value <= 0 Then
                Throw New Exception("Please update Weight Value in Item Master for Item-" & Item_Code & "")
            End If
        Else
            Return Kg_Value
        End If

        Dim Cnvsrn_Factr As Decimal = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans))
        Dim Weight_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
        If clsCommon.myLen(Weight_KG_Unit) <= 0 Then
            Throw New Exception("Please update ProductionFATSNF_KG_Unit in fixed parameter.")
        End If

        Dim KG_Cnvrsn_Value As Decimal = Nothing
        If clsCommon.CompairString(Wt_uom, Weight_KG_Unit) = CompairStringResult.Equal Then
            KG_Cnvrsn_Value = 1
        Else
            '' work done by Panch Raj-Description:Weight UOM conversion fundamental failed because we can define single conversion for a particular Product Type but Product Type -Milk have different convsrsion than Ghee items hence fetching conversion from item uom conversion if KG uom is defined fro that item
            KG_Cnvrsn_Value = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Weight_KG_Unit, trans))
            If KG_Cnvrsn_Value <> 0 Then
                KG_Cnvrsn_Value = 1 / KG_Cnvrsn_Value
                Wt_Value = 1
            Else
                qry = "select top 1 CF from (Select (case when (Container_UOM='" & Wt_uom & "' and Contained_UOM='" & Weight_KG_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_KG_Unit & "' and Contained_UOM='" & Wt_uom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(Item_Code, trans) + "'))aa where isnull(cast(CF as float),0)<>0 order by Product_Type desc"
                KG_Cnvrsn_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            End If
        End If

        If KG_Cnvrsn_Value > 0 Then

            Kg_Value = ((Wt_Value * Qty * Cnvsrn_Factr) * KG_Cnvrsn_Value)
            ' Kg_Value = System.Math.Round((Kg_Value * Fat_Snf_Pers) / 100, 2)
            If Kg_Value > 0 Then
                Kg_Value = System.Math.Round((Amount / Kg_Value), 2)
            Else
                Kg_Value = 0
            End If

        Else
            Kg_Value = 0
        End If

        Return Kg_Value
    End Function
    Public Shared Function CheckBOMExist(ByVal BOM_Code As String, ByVal Item_Code As String, ByVal Unit_Code As String) As Integer
        Dim str As Integer = 0
        Dim qry As String = "select count(*) from tspl_pp_bom_head where bom_code<>'" + BOM_Code + "' and prod_item_code='" + Item_Code + "' "
        If clsCommon.myLen(Unit_Code) > 0 Then
            qry += " and prod_item_unit_code='" + Unit_Code + "' "
        End If
        str = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return str
    End Function

    Public Shared Function GetBOMCodeOnDoubleClick(ByVal BOM_Code As String, ByVal Item_Code As String, ByVal Unit_Code As String) As String
        Dim str As String = ""
        Dim qry As String = "select distinct (select ','''+bom_code+'''' from tspl_pp_bom_head where bom_code<>'" + BOM_Code + "' and prod_item_code='" + Item_Code + "' "
        If clsCommon.myLen(Unit_Code) > 0 Then
            qry += " and prod_item_unit_code='" + Unit_Code + "' "
        End If
        qry += " for xml path('')) as bom_code "
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsBOM
        Try
            Dim obj As New clsBOM()
            ObjList = New List(Of clsBOM)
            Dim qry As String = "select BOM_OVERHEADCOST.HCODE as OverHeadCostGroupCode, TSPL_PP_BOM_HEAD.*,tspl_vendor_master.vendor_name " & _
                " from TSPL_PP_BOM_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PP_BOM_HEAD.vendor_code " & _
                " LEFT OUTER JOIN (SELECT Document_Code,HCODE  FROM TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS WHERE TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code ='" + strCode + "' " & _
                " GROUP BY Document_Code,HCODE) AS BOM_OVERHEADCOST ON BOM_OVERHEADCOST.Document_Code=TSPL_PP_BOM_HEAD.bom_code where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_PP_BOM_HEAD.bom_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_PP_BOM_HEAD.bom_code in (select min(bom_code) from TSPL_PP_BOM_HEAD )"
                Case NavigatorType.Last
                    qry += " and TSPL_PP_BOM_HEAD.bom_code in (select max(bom_code) from TSPL_PP_BOM_HEAD )"
                Case NavigatorType.Next
                    qry += " and TSPL_PP_BOM_HEAD.bom_code in (select min(bom_code) from TSPL_PP_BOM_HEAD where bom_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_PP_BOM_HEAD.bom_code in (select max(bom_code) from TSPL_PP_BOM_HEAD where bom_code<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.OverHeadCostGroupCode = clsCommon.myCstr(dt.Rows(0)("OverHeadCostGroupCode"))
                obj.BOM_CODE = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))
                obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("Description"))
                Try
                    obj.BOM_DATE = clsCommon.myCDate(dt.Rows(0)("BOM_DATE"))
                Catch exx As Exception
                    obj.BOM_DATE = clsCommon.GETSERVERDATE(trans)
                End Try

                obj.STATUS = clsCommon.myCstr(dt.Rows(0)("status"))
                obj.PROD_ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_ITEM_CODE"))
                Try
                    obj.START_DATE = clsCommon.myCDate(dt.Rows(0)("valid_from_date"))
                Catch exx As Exception
                    obj.START_DATE = clsCommon.GETSERVERDATE(trans)
                End Try
                Try
                    obj.END_DATE = clsCommon.myCDate(dt.Rows(0)("valid_upto_date"))
                Catch exx As Exception
                    obj.END_DATE = clsCommon.GETSERVERDATE(trans)
                End Try

                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("vendor_name"))
                obj.Is_OSP = CInt(clsCommon.myCdbl(dt.Rows(0)("is_osp")))
                obj.JobWork_Loc = clsCommon.myCstr(dt.Rows(0)("JobWork_Loc"))

                obj.prodcategorycode = clsCommon.myCstr(dt.Rows(0)("ITEM_CATEGORY_CODE"))
                obj.prodcatdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + obj.prodcategorycode + "'", trans))
                obj.PROD_ITEM_NAME = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + obj.PROD_ITEM_CODE + "'", trans))
                obj.PROD_ITEM_TYPE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + obj.PROD_ITEM_CODE + "'", trans))
                obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_ITEM_UNIT_CODE"))
                obj.PROD_QUANTITY = clsCommon.myCdbl(dt.Rows(0)("PROD_QUANTITY"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
                obj.Section_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + obj.Section_Code + "'", trans))
                obj.IsPost = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
                obj.revisionno = clsCommon.myCstr(dt.Rows(0)("revision_no"))

                obj.Byproduct_Item_Code = clsCommon.myCstr(dt.Rows(0)("Byproduct_Item_Code"))
                obj.Byproduct_Item_UOM = clsCommon.myCstr(dt.Rows(0)("Byproduct_Item_UOM"))
                obj.Byproduct_Item_Qty = clsCommon.myCdbl(dt.Rows(0)("Byproduct_Item_Qty"))


                obj.ObjListBOM = New List(Of clsBOMItemDetail)
                qry = "select * from TSPL_PP_BOM_ITEM_DETAIL where bom_code='" + obj.BOM_CODE + "' order by line_no"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsBOMItemDetail()

                        objtr.Line_No = CInt(dr("LINE_NO"))
                        objtr.CONSM_ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                        objtr.ITEM_DESCRIPTION = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.CONSM_ITEM_CODE + "'", trans))
                        objtr.CONSM_ITEM_TYPE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.CONSM_ITEM_CODE + "'", trans))
                        objtr.CONSM_ITEM_PRODUCT_TYPE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + objtr.CONSM_ITEM_CODE + "'", trans))
                        objtr.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                        objtr.CONSM_QUANTITY = clsCommon.myCdbl(dr("QUANTITY"))
                        objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                        objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                        If dr("fat_kg") Is DBNull.Value Then
                            objtr.fat_kg = 0
                        Else
                            objtr.fat_kg = clsCommon.myCdbl(dr("fat_kg"))
                        End If
                        If dr("snf_kg") Is DBNull.Value Then
                            objtr.snf_kg = 0
                        Else
                            objtr.snf_kg = clsCommon.myCdbl(dr("snf_kg"))
                        End If

                        objtr.Rejection = clsCommon.myCdbl(dr("Rejection_Pers"))
                        objtr.alticode = clsCommon.myCstr(dr("Alt_Item_Code"))
                        objtr.altiname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.alticode + "'", trans))
                        objtr.altitype = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.alticode + "'", trans))
                        objtr.altunitcode = clsCommon.myCstr(dr("Alt_Unit_code"))
                        objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))
                        objtr.Deactive = clsCommon.myCstr(dr("Deactive"))
                        objtr.Consm_Base = clsCommon.myCstr(dr("Consm_Base"))

                        If dr("Effective_Date") Is DBNull.Value Then
                            objtr.Effectivedate = Nothing
                        Else
                            objtr.Effectivedate = clsCommon.myCDate(dr("Effective_Date"))
                        End If
                        ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
                        objtr.ProcessLossPer = clsCommon.myCdbl(dr("ProcessLossPer"))
                        objtr.ProcessLossQty = clsCommon.myCdbl(dr("ProcessLossQty"))
                        ''-----------
                        obj.ObjListBOM.Add(objtr)

                    Next
                End If

                obj.ObjListBOMOP = New List(Of clsBOMStage)
                qry = "select * from TSPL_PP_BOM_STAGE_DETAIL where bom_code='" + obj.BOM_CODE + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsBOMStage()
                        objtr.stagecode = clsCommon.myCstr(dr("Stage_Code"))
                        objtr.stagename = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_stage_master where stage_code='" + objtr.stagecode + "'", trans))
                        If dr("Sequence") Is DBNull.Value Then
                            objtr.stage_seq = 1
                        Else
                            objtr.stage_seq = CInt(dr("Sequence"))
                        End If
                        objtr.AR_Item_Code = clsCommon.myCstr(dr("AR_Item_Code"))
                        objtr.Bi_Prod = clsCommon.myCstr(dr("Bi_Prod"))
                        obj.ObjListBOMOP.Add(objtr)


                    Next
                End If
                '===================BOM OVERHEAD COST=======
                obj.ObjCostGroupDetail = New List(Of clsBomCostMappingDetails)
                qry = "select TSPL_ITEM_COST_MAPPING_DETAILS_ALL.DDCODE,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.HCODE,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.Item_Code," & _
                    " TSPL_ITEM_COST_MAPPING_DETAILS_ALL.UOM,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.SNO,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.COST_CODE," & _
                    " TSPL_ITEM_COST_MAPPING_DETAILS_ALL.COST,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.OverHead_Cost as Overhead_Cost, TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.BomRatePerHour ,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS. BomHours , TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.BomNO " & _
                    "  from  TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS inner join TSPL_PP_BOM_HEAD on TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code=TSPL_PP_BOM_HEAD.BOM_CODE  " & _
                      " INNER JOIN TSPL_ITEM_COST_MAPPING_DETAILS_ALL  ON TSPL_ITEM_COST_MAPPING_DETAILS_ALL.HCODE=TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.HCODE " & _
                     " AND TSPL_ITEM_COST_MAPPING_DETAILS_ALL.HCODE =TSPL_PP_BOM_HEAD.OverHeadCostGroup_Code and  TSPL_ITEM_COST_MAPPING_DETAILS_ALL.COST_CODE=TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.COST_CODE " & _
                     " and  TSPL_ITEM_COST_MAPPING_DETAILS_ALL.Item_Code= TSPL_PP_BOM_HEAD.PROD_ITEM_CODE  " & _
                     " and TSPL_ITEM_COST_MAPPING_DETAILS_ALL.UOM=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE  " & _
                " where TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code ='" + obj.BOM_CODE + "'"
                'qry = "select * from TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS where TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code='" + obj.BOM_CODE + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    obj.ObjCostGroupDetail = New List(Of clsBomCostMappingDetails)
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsBomCostMappingDetails
                        objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                        objtr.DCODE = clsCommon.myCstr(dr("DDCODE"))
                        objtr.HCODE = clsCommon.myCstr(dr("HCODE"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.UOM = clsCommon.myCstr(dr("UOM"))
                        objtr.COST_CODE = clsCommon.myCstr(dr("COST_CODE"))
                        objtr.COST = clsCommon.myCdbl(dr("COST"))
                        objtr.Overheadcost = clsCommon.myCdbl(dr("Overhead_Cost"))
                        If String.IsNullOrEmpty(clsCommon.myCstr(dr("BomRatePerHour"))) = False Then
                            objtr.BomRatePerHour = clsCommon.myCdbl(dr("BomRatePerHour"))
                        Else
                            objtr.BomRatePerHour = 0
                        End If
                        If String.IsNullOrEmpty(clsCommon.myCstr(dr("BomHours"))) = False Then
                            objtr.BomHours = clsCommon.myCdbl(dr("BomHours"))
                        Else
                            objtr.BomHours = 0
                        End If
                        If String.IsNullOrEmpty(clsCommon.myCstr(dr("BomNO"))) = False Then
                            objtr.BomNO = clsCommon.myCdbl(dr("BomNO"))
                        Else
                            objtr.BomNO = 0
                        End If
                        obj.ObjCostGroupDetail.Add(objtr)
                    Next
                End If
                '=============================
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetOverHeadCostGroupDetail(ByVal strDocNo As String, ByVal strMainItem As String, ByVal strMainUOM As String) As clsBOM
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As New clsBOM
            ' Dim qry As String = " select DDCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST,Cost as Overhead_Cost from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE= '" + strDocNo + "' AND Item_Code ='" + strMainItem + "' AND UOM='" + strMainUOM + "' order by SNO asc "
            Dim qry As String = " select TSPL_ITEM_COST_MAPPING_DETAILS_ALL.DDCODE,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.HCODE,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.Item_Code,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.UOM,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.SNO,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.COST_CODE,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.COST,TSPL_ITEM_COST_MAPPING_DETAILS_ALL.Cost as Overhead_Cost from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where TSPL_ITEM_COST_MAPPING_DETAILS_ALL.HCODE= '" + strDocNo + "' AND TSPL_ITEM_COST_MAPPING_DETAILS_ALL.Item_Code ='" + strMainItem + "' AND TSPL_ITEM_COST_MAPPING_DETAILS_ALL.UOM='" + strMainUOM + "' order by TSPL_ITEM_COST_MAPPING_DETAILS_ALL.SNO asc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ObjCostGroupDetail = New List(Of clsBomCostMappingDetails)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsBomCostMappingDetails
                    objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                    objtr.DCODE = clsCommon.myCstr(dr("DDCODE"))
                    objtr.HCODE = clsCommon.myCstr(dr("HCODE"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.UOM = clsCommon.myCstr(dr("UOM"))
                    objtr.COST_CODE = clsCommon.myCstr(dr("COST_CODE"))
                    objtr.COST = clsCommon.myCdbl(dr("COST"))
                    objtr.Overheadcost = clsCommon.myCdbl(dr("Overhead_Cost"))
                    obj.ObjCostGroupDetail.Add(objtr)
                Next
            End If
            trans.Commit()
            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal StrSection As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, StrSection, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal StrSection As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_BOM_HEAD", "BOM_CODE", "TSPL_PP_BOM_STAGE_DETAIL", "BOM_CODE", "TSPL_PP_BOM_ITEM_DETAIL", "BOM_CODE", trans)
            qry = "delete from TSPL_PP_BOM_ITEM_DETAIL where BOM_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_BOM_STAGE_DETAIL_PP
            qry = "DELETE FROM TSPL_PP_BOM_STAGE_DETAIL WHERE bom_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_BOM_HEAD where BOM_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveHistoryData(ByVal strcode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim obj As clsBOM = clsBOM.GetData(strcode, NavigatorType.Current, trans)

            If obj Is Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) <= 0 Then
                Throw New Exception("Document not amended.")
            End If

            Dim History_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(History_No) from TSPL_PP_BOM_HEAD_HISTORY", trans))

            If clsCommon.myLen(History_No) <= 0 Then
                History_No = "Hist0000000000000000000000000000001"
            Else
                History_No = clsCommon.incval(History_No)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
            clsCommon.AddColumnsForChange(coll, "Description", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "BOM_DATE", clsCommon.GetPrintDate(obj.BOM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "valid_from_date", clsCommon.GetPrintDate(obj.START_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "valid_upto_date", clsCommon.GetPrintDate(obj.END_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "STATUS", clsCommon.myCstr(obj.STATUS))
            clsCommon.AddColumnsForChange(coll, "PROD_ITEM_CODE", clsCommon.myCstr(obj.PROD_ITEM_CODE))
            clsCommon.AddColumnsForChange(coll, "PROD_ITEM_UNIT_CODE", clsCommon.myCstr(obj.PROD_ITEM_UNIT_CODE))
            clsCommon.AddColumnsForChange(coll, "PROD_QUANTITY", clsCommon.myCdbl(obj.PROD_QUANTITY))
            clsCommon.AddColumnsForChange(coll, "Section_code", obj.Section_Code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy hh:mm tt")))
            clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", obj.prodcategorycode)
            clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_Values", "D")
            clsCommon.AddColumnsForChange(coll, "Is_Post", obj.IsPost)
            clsCommon.AddColumnsForChange(coll, "revision_no", obj.revisionno)
            clsCommon.AddColumnsForChange(coll, "History_No", History_No)
            clsCommon.AddColumnsForChange(coll, "Is_OSP", obj.Is_OSP)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)

            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_HEAD_HISTORY", OMInsertOrUpdate.Insert, "", trans)

            isSaved = isSaved AndAlso clsBOMItemDetail.SaveHistoryBOMDetail(obj.BOM_CODE, History_No, obj.ObjListBOM, 0, trans)
            isSaved = isSaved AndAlso clsBOMStage.SaveHistoryBOMStage(obj.BOM_CODE, History_No, obj.Section_Code, obj.ObjListBOMOP, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsBOM, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True

            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsBOM, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            If isNewEntry Then 'clsCommon.myLen(obj.BOM_CODE) <= 0
                If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                    obj.BOM_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.BOM_DATE), clsDocType.BOM, clsDocTransactionType.BOMOSPTYPE, "")
                Else
                    obj.BOM_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.BOM_DATE), clsDocType.BOM, clsDocTransactionType.SNQuotationOther, "")
                End If
            End If
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.BOM_CODE), "TSPL_PP_BOM_HEAD", "BOM_CODE", "TSPL_PP_BOM_STAGE_DETAIL", "BOM_CODE", "TSPL_PP_BOM_ITEM_DETAIL", "BOM_CODE", trans)
            End If

            'Dim qry As String = "select count(*) FROM TSPL_PP_BOM_HEAD WHERE BOM_CODE='" + obj.BOM_CODE + "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
            clsCommon.AddColumnsForChange(coll, "Description", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "BOM_DATE", clsCommon.GetPrintDate(obj.BOM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "valid_from_date", clsCommon.GetPrintDate(obj.START_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "valid_upto_date", clsCommon.GetPrintDate(obj.END_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "STATUS", clsCommon.myCstr(obj.STATUS))
            clsCommon.AddColumnsForChange(coll, "PROD_ITEM_CODE", clsCommon.myCstr(obj.PROD_ITEM_CODE))
            clsCommon.AddColumnsForChange(coll, "PROD_ITEM_UNIT_CODE", clsCommon.myCstr(obj.PROD_ITEM_UNIT_CODE))
            clsCommon.AddColumnsForChange(coll, "PROD_QUANTITY", clsCommon.myCdbl(obj.PROD_QUANTITY))
            clsCommon.AddColumnsForChange(coll, "Section_code", obj.Section_Code, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy hh:mm tt")))
            clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", obj.prodcategorycode)
            clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_Values", "D")
            clsCommon.AddColumnsForChange(coll, "Is_Post", obj.IsPost)
            clsCommon.AddColumnsForChange(coll, "revision_no", obj.revisionno)
            clsCommon.AddColumnsForChange(coll, "Is_OSP", obj.Is_OSP)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "JobWork_Loc", obj.JobWork_Loc)
            clsCommon.AddColumnsForChange(coll, "OverHead_Cost", clsCommon.myCdbl(obj.OverHeadCost))
            clsCommon.AddColumnsForChange(coll, "OverHeadCostGroup_Code", clsCommon.myCstr(obj.OverHeadCostGroupCode))

            clsCommon.AddColumnsForChange(coll, "Byproduct_Item_Code", obj.Byproduct_Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Byproduct_Item_UOM", obj.Byproduct_Item_UOM)
            clsCommon.AddColumnsForChange(coll, "Byproduct_Item_Qty", obj.Byproduct_Item_Qty)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_HEAD", OMInsertOrUpdate.Update, " TSPL_PP_BOM_HEAD.BOM_CODE='" + obj.BOM_CODE + "'", trans)
            End If

            isSaved = isSaved AndAlso clsBOMItemDetail.SaveBOMDetail(obj.BOM_CODE, obj.ObjListBOM, 0, trans)
            isSaved = isSaved AndAlso clsBOMStage.SaveBOMStage(obj.BOM_CODE, obj.Section_Code, obj.ObjListBOMOP, trans)
            isSaved = isSaved AndAlso clsBomCostMappingDetails.SaveBOMOverHeadCost(obj.BOM_CODE, obj.ObjCostGroupDetail, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(strDocNo, isCheckForPosted, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsBOM = clsBOM.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.BOM_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If


            Dim qry As String = "Update TSPL_PP_BOM_HEAD set Is_Post='1',status='Approved',Modified_By='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where BOM_CODE ='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_PP_BOM_HEAD", "BOM_CODE", trans)
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function FinderForBOM(ByVal strCode As String, ByVal isButtonClicked As Boolean, Optional ByVal Item_Code As String = "") As clsBOM
        Dim obj As clsBOM = Nothing
        Dim qry As String = "select BOM_CODE as Code,DESCRIPTION as Name,BOM_DATE,REVISION_NO,PROD_ITEM_CODE AS ITEM_CODE,PROD_QUANTITY AS BUILD_QTY,START_DATE,END_DATE,STATUS,IS_DEFAULT  from TSPL_BOM_HEAD_PP"
        Dim WhrCls As String = "STATUS='APPROVED'"
        If clsCommon.myLen(Item_Code) > 0 Then
            WhrCls = WhrCls & " and PROD_ITEM_CODE='" & Item_Code & "'"
        End If
        strCode = clsCommon.ShowSelectForm("BOMFINDER", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select T1.BOM_CODE as Code,T1.DESCRIPTION as Name,T1.REVISION_NO,T1.PROD_ITEM_CODE AS ITEM_CODE,T2.ITEM_DESC,PROD_ITEM_UNIT_CODE AS UNIT_CODE,PROD_QUANTITY AS BUILD_QTY FROM TSPL_BOM_HEAD_PP T1 LEFT JOIN TSPL_ITEM_MASTER T2 ON  T1.PROD_ITEM_CODE=T2.ITEM_CODE  WHERE T1.BOM_CODE='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsBOM()
                obj.BOM_CODE = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.REVISION_NO = clsCommon.myCstr(dt.Rows(0)("REVISION_NO"))
                obj.PROD_ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("ITEM_CODE"))
                obj.ITEM_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("ITEM_DESC"))
                obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("UNIT_CODE"))
                obj.PROD_QUANTITY = clsCommon.myCdbl(dt.Rows(0)("BUILD_QTY"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForItem(ByVal strCode As String, ByVal WhrCls As String, ByVal isButtonClicked As Boolean) As clsBOM
        Dim obj As clsBOM = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name,ITEM_TYPE AS [Item Type],Product_Type from  TSPL_ITEM_MASTER"


        strCode = clsItemMaster.getFinder(WhrCls, strCode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,item_type,Item_Desc,Unit_Code,Product_type from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsBOM()
                obj.PROD_ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.ITEM_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.PROD_ITEM_TYPE = clsCommon.myCstr(dt.Rows(0)("item_type"))

                Dim producttype As String = clsCommon.myCstr(dt.Rows(0)("Product_type"))
                obj.CONSM_ITEM_PRODUCT_TYPE = clsCommon.myCstr(clsItemMaster.ProductType(producttype))

            End If
        End If
        Return obj
    End Function

    Public Shared Function GetSNF_PERS(ByVal Icode As String) As Double
        Dim SNFPers As Double = 0
        SNFPers = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + Icode + "' and TSPL_PARAMETER_MASTER.Type='SNF'"))

        Return Math.Round(SNFPers, 2)
    End Function
    Public Shared Function GetFAT_PERS(ByVal Icode As String) As Double
        Dim SNFPers As Double = 0
        SNFPers = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + Icode + "' and TSPL_PARAMETER_MASTER.Type='FAT'"))

        Return Math.Round(SNFPers, 2)
    End Function

    Public Shared Function GetItemTypeCode(ByVal ItemType As String) As String
        Dim str As String = ""

        'If clsCommon.CompairString(ItemType, "Raw Material") = CompairStringResult.Equal Then
        '    str = "R"
        'ElseIf clsCommon.CompairString(ItemType, "Finished Good") = CompairStringResult.Equal Then
        '    str = "F"
        'ElseIf clsCommon.CompairString(ItemType, "Semi Finished Good") = CompairStringResult.Equal Then
        '    str = "S"
        'ElseIf clsCommon.CompairString(ItemType, "Asset") = CompairStringResult.Equal Then
        '    str = "A"
        'ElseIf clsCommon.CompairString(ItemType, "Other") = CompairStringResult.Equal Then
        '    str = "O"
        'End If
        ''richa BHA/13/07/18-000156 pick item type from item master
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ITEM_TYPE_CODE FROM TSPL_ITEM_TYPE_MASTER where ITEM_TYPE_NAME='" & clsCommon.myCstr(ItemType) & "'"))
        Return str
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso ReverseAndUnpost(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "Update TSPL_PP_BOM_HEAD set Is_Post='0',status='Open',Modified_By='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where BOM_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_BOM_HEAD", "BOM_CODE", trans)
            SaveHistoryData(strCode, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetBOMDesc(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "select Description from TSPL_PP_BOM_HEAD where BOM_Code='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function CheckCode(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "select BOM_Code from TSPL_PP_BOM_HEAD where BOM_Code='" + strCode + "' "
        Return If(clsCommon.myLen(clsDBFuncationality.getSingleValue(qry, trans)) > 0, True, False)
    End Function
    Public Shared Function GetBOMQty(ByVal strCode As String, ByVal Item_Code As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "select QUANTITY from TSPL_PP_BOM_ITEM_DETAIL where BOM_Code='" + strCode + "' and Item_Code='" & Item_Code & "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetBOMBuildUOM(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "select PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD where BOM_Code='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class

Public Class clsBOMItemDetail
#Region "Variables"
    Public prodcategorycode As String = Nothing
    Public prodcatdesc As String = Nothing
    Public revisionno As String = Nothing
    Public BOM_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public BOM_DATE As Date = Nothing
    Public REVISION_NO As String = Nothing
    Public START_DATE As Date = Nothing
    Public END_DATE As Date = Nothing
    Public STATUS As String = Nothing
    Public IS_DEFAULT As Boolean = Nothing
    Public ATTACHED_DOC As Byte()
    Public ATTACHED_DOC_PATH As String = Nothing
    Public PROD_ITEM_CODE As String = Nothing
    Public PROD_ITEM_NAME As String = Nothing
    Public PROD_QUANTITY As String = Nothing
    Public PROD_ITEM_UNIT_CODE As String = Nothing
    Public MIN_BATCH_SIZE As Decimal = Nothing
    Public APPROVED_BY As String = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public ProcessLossPer As Double = 0
    Public ProcessLossQty As Double = 0
    Public PROD_ITEM_TYPE As String = Nothing
    Public PROD_ITEM_CATEGORY_CODE As String = Nothing
    Public PROD_ITEM_CATEGORY_NAME As String = Nothing


    '' grid columns
    Public Line_No As Integer
    Public CONSM_ITEM_TYPE As String
    Public CONSM_ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public CONSM_QUANTITY As Decimal
    Public CONSM_ITEM_UNIT_CODE As String
    Public FAT As String = Nothing
    Public SNF As String = Nothing
    Public REMARKS As String = Nothing
    Public Rejection As Decimal = Nothing
    Public alticode As String = Nothing
    Public altunitcode As String = Nothing
    Public altiname As String = Nothing
    Public altitype As String = Nothing
    Public CONSM_ITEM_PRODUCT_TYPE As String
    Public Deactive As String = Nothing
    Public Effectivedate As Date = Nothing
    Public Consm_Base As String = Nothing
    Public ObjListBOM As List(Of clsBOMItemDetail)

    Public Section_Code As String
    Public Section_Name As String
    Public IsPost As String = Nothing
#End Region

    Public Shared Function SaveBOMDetail(ByVal Bomcode As String, ByVal arr As List(Of clsBOMItemDetail), ByVal Check As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PP_BOM_ITEM_DETAIL where bom_code='" + Bomcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            For Each objtr As clsBOMItemDetail In arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", Bomcode)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", objtr.Line_No)
                clsCommon.AddColumnsForChange(coll, "ITEM_CODE", objtr.CONSM_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "UNIT_CODE", objtr.CONSM_ITEM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", objtr.CONSM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "FAT", objtr.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objtr.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.fat_kg)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.snf_kg)
                clsCommon.AddColumnsForChange(coll, "Rejection_Pers", objtr.Rejection)
                clsCommon.AddColumnsForChange(coll, "Alt_Item_Code", objtr.alticode)
                clsCommon.AddColumnsForChange(coll, "Alt_Unit_code", objtr.altunitcode)
                clsCommon.AddColumnsForChange(coll, "REMARKS", objtr.REMARKS)
                clsCommon.AddColumnsForChange(coll, "Deactive", objtr.Deactive)
                ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
                clsCommon.AddColumnsForChange(coll, "ProcessLossPer", objtr.ProcessLossPer)
                clsCommon.AddColumnsForChange(coll, "ProcessLossQty", objtr.ProcessLossQty)
                ''----------------
                clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(objtr.Effectivedate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Consm_Base", objtr.Consm_Base, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveHistoryBOMDetail(ByVal Bomcode As String, ByVal History_No As String, ByVal arr As List(Of clsBOMItemDetail), ByVal Check As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            For Each objtr As clsBOMItemDetail In arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", Bomcode)
                clsCommon.AddColumnsForChange(coll, "History_No", History_No)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", objtr.Line_No)
                clsCommon.AddColumnsForChange(coll, "ITEM_CODE", objtr.CONSM_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "UNIT_CODE", objtr.CONSM_ITEM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", objtr.CONSM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "FAT", objtr.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objtr.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.fat_kg)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.snf_kg)
                clsCommon.AddColumnsForChange(coll, "Rejection_Pers", objtr.Rejection)
                clsCommon.AddColumnsForChange(coll, "Alt_Item_Code", objtr.alticode)
                clsCommon.AddColumnsForChange(coll, "Alt_Unit_code", objtr.altunitcode)
                clsCommon.AddColumnsForChange(coll, "REMARKS", objtr.REMARKS)
                clsCommon.AddColumnsForChange(coll, "Deactive", objtr.Deactive)
                ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
                clsCommon.AddColumnsForChange(coll, "ProcessLossPer", objtr.ProcessLossPer)
                clsCommon.AddColumnsForChange(coll, "ProcessLossQty", objtr.ProcessLossQty)
                ''----------------
                clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(objtr.Effectivedate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Consm_Base", objtr.Consm_Base, True)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_ITEM_DETAIL_HISTORY", OMInsertOrUpdate.Insert, "", trans)
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsBOMStage
#Region "Variables"
    Public ObjListBOMOP As List(Of clsBOMStage)
    Public stagecode As String = Nothing
    Public stagename As String = Nothing
    Public stage_seq As Integer = Nothing
    Public AR_Item_Code As String = Nothing
    Public Bi_Prod As String = Nothing
#End Region

    Public Shared Function SaveBOMStage(ByVal Bomcode As String, ByVal sectioncode As String, ByVal arr As List(Of clsBOMStage), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PP_BOM_STAGE_DETAIL where bom_code='" + Bomcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            For Each objtr As clsBOMStage In arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", Bomcode)
                clsCommon.AddColumnsForChange(coll, "Section_Code", sectioncode)
                clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.stagecode)
                clsCommon.AddColumnsForChange(coll, "AR_Item_Code", objtr.AR_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bi_Prod", objtr.Bi_Prod, True)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_STAGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveHistoryBOMStage(ByVal Bomcode As String, ByVal History_No As String, ByVal sectioncode As String, ByVal arr As List(Of clsBOMStage), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            For Each objtr As clsBOMStage In arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", Bomcode)
                clsCommon.AddColumnsForChange(coll, "Section_Code", sectioncode)
                clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.stagecode)
                clsCommon.AddColumnsForChange(coll, "Sequence", objtr.stage_seq)
                clsCommon.AddColumnsForChange(coll, "History_No", History_No)
                clsCommon.AddColumnsForChange(coll, "AR_Item_Code", objtr.AR_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bi_Prod", objtr.Bi_Prod, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_STAGE_DETAIL_HISTORY", OMInsertOrUpdate.Insert, "", trans)
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsBomCostMappingDetails
#Region "Variables"
    Public Document_Code As String
    Public DCODE As String
    Public HCODE As String
    Public Item_Code As String
    Public UOM As String
    Public SNO As Integer
    Public COST_CODE As String
    Public COST_DESC As String
    'Public standardRatePerHour As Double
    'Public standardHours As Double
    'Public standardNO As Double
    Public COST As Double ' standard Cost
    Public BomRatePerHour As Double
    Public BomHours As Double
    Public BomNO As Double
    Public Overheadcost As Double ' Bom Cost

#End Region
    Public Shared Function SaveBOMOverHeadCost(ByVal Bomcode As String, ByVal arr As List(Of clsBomCostMappingDetails), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS where Document_Code='" + Bomcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            For Each objtr As clsBomCostMappingDetails In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", Bomcode)
                ' clsCommon.AddColumnsForChange(coll, "Section_Code", sectioncode)
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "HCODE", objtr.HCODE, True)
                clsCommon.AddColumnsForChange(coll, "COST_CODE", objtr.COST_CODE)
                clsCommon.AddColumnsForChange(coll, "BomRatePerHour", objtr.BomRatePerHour, True)
                clsCommon.AddColumnsForChange(coll, "BomHours", objtr.BomHours, True)
                clsCommon.AddColumnsForChange(coll, "BomNO", objtr.BomNO, True)
                clsCommon.AddColumnsForChange(coll, "OverHead_Cost", objtr.Overheadcost, True)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
