Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
Public Class ClsUDLSalesQuery
    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select distinct TAX" & intloop & " from " & TableName & " where coalesce(Tax" & intloop & ",'')<>''"
                    Else
                        qry = qry & " Union  " & "select distinct TAX" & intloop & " from " & TableName & " where coalesce(Tax" & intloop & ",'')<>''"
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function
    Public Shared Function GetTaxQuery() As String
        Dim qryTaxQuery As String = ""
        Dim lstTables As New List(Of String)
        lstTables.Add("TSPL_SD_SALE_INVOICE_DETAIL")
        lstTables.Add("TSPL_SCRAPINVOICE_DETAIL")
        lstTables.Add("TSPL_SD_SALE_RETURN_DETAIL")
        lstTables.Add("TSPL_TRANSFER_ORDER_DETAIL")
        lstTables.Add("TSPL_CSA_TRANSFER_DETAIL")
        qryTaxQuery = GetTaxQuery(lstTables)
        Return qryTaxQuery
    End Function
    Public Shared Function GetMIS_ITem_GroupColumn() As String
        Dim MIS_Item_Group As String = ""
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Nothing))
        Return MIS_Item_Group
    End Function
    Public Shared Function GetPivotForFinalOuterQry() As String
        Dim qryTaxQuery As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strPivotForFinalOuter As String = ""
        qryTaxQuery = GetTaxQuery()
        strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        Return strPivotForFinalOuterQuery
    End Function
    Public Shared Function ReturnQueryWithCSASalePatti(ByVal obj As clsSaleRegisterParameterType, ByVal formType As String) As ArrayList
        Dim From_Date As Date = obj.From_Date
        Dim To_Date As Date = obj.To_Date
        Dim Unit_Code As String = obj.Unit_Code
        Dim QryLst As New ArrayList

        Dim strCodeColumn As String = ""
        Dim strCodeColumnForVirtual As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim MIS_Item_Group As String = GetMIS_ITem_GroupColumn()
        Dim dtCategory As DataTable


   
            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeColumnForVirtual += ","
                        strCodeColumnMax += ","
                        strCodeDescColumn += ","
                        strCodeDescColumnMax += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnForVirtual += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
                Next
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
                " select * from ( " + Environment.NewLine & _
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Description' as Item_Category_CodeDesc " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine & _
                " )xx" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                " ) Pivt" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " (" + Environment.NewLine & _
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                " ) Pivt1 " + Environment.NewLine & _
                " ) xxx group by Item_Code "
                ''End of Category Table start now.
            End If



            ''Virtual Category Table start now.


            Dim strMCCMaterial As String = ""
            Dim qryTaxQuery As String = ""
            Dim strPivotForOuter As String
            'Dim lstTables As New List(Of String)
            'lstTables.Add("TSPL_SD_SALE_INVOICE_DETAIL")
            'lstTables.Add("TSPL_SCRAPINVOICE_DETAIL")
            'lstTables.Add("TSPL_SD_SALE_RETURN_DETAIL")
            'lstTables.Add("TSPL_TRANSFER_ORDER_DETAIL")
            'lstTables.Add("TSPL_CSA_TRANSFER_DETAIL")
            qryTaxQuery = GetTaxQuery()

            'strPivotForOuter = " select distinct (select Distinct ',sum(isnull(final.'+tax1+',0)) as '+TAX1 from ( " & qryTaxQuery
            strPivotForOuter = "select distinct (select Distinct ',sum(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
            strPivotForOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

            Dim strPivotForOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuter))
            '============================

            Dim strPivotForFinalOuter As String
            strPivotForFinalOuter = ""
            strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
            strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
            strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))

            Dim strPivotForFinalOuterPercent As String
            strPivotForFinalOuterPercent = " select distinct (select  Distinct ',xx.['+tax1+'%'+']' from ( " & qryTaxQuery
            strPivotForFinalOuterPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
            Dim strPivotForFinalOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuterPercent))

            Dim strPivotForTransfer_In As String
            strPivotForFinalOuter = ""
            strPivotForFinalOuter = " select distinct (select Distinct ',0 as ['+tax1+']' from ( " & qryTaxQuery
            strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
            strPivotForTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))

            Dim strPivotFortRANSFER_INPercent As String
            strPivotFortRANSFER_INPercent = " select distinct (select  Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
            strPivotFortRANSFER_INPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
            Dim strPivotFortRANSFER_INPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))
            '===========

            Dim strPivotForGroupOuter As String
            strPivotForGroupOuter = "select SUBSTRING(ax,2,len(Ax)) from ("
            'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
            strPivotForGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

            strPivotForGroupOuter += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"
            Dim strPivotFoGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))



            Dim strPivotForOuterForBulk As String
            strPivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+TAX1+']' from ( " & qryTaxQuery

            strPivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

            Dim strPivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))

            Dim strDoublePivotForOuterForBulk As String

            strDoublePivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery


            strDoublePivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

            Dim strDoublePivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))


            Dim strPivotForInner As String
            strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
            strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ( " & qryTaxQuery

            strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

            Dim strPivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

            '' taxcolumns for no tax 
            strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
            strPivotForInner += " select distinct (select Distinct ',Null as ['+tax1+']' from ( " & qryTaxQuery

            strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

            Dim strPivotForInnerQueryNoTax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))


            Dim strDoublePivotForInner As String
            strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
            strDoublePivotForInner += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery

            strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

            Dim strDoublePivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))

            '' tax rate columns for no tax 
            strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
            strDoublePivotForInner += " select distinct (select Distinct ',Null as ['+tax1+'%'+']' from ( " & qryTaxQuery

            strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

            Dim strDoublePivotForInnerQueryNoTax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))


            Dim qryQC As String = ""
            qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" & _
                    " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," & _
                    " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " & _
                    " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

            Dim qryKG As String = ""

            qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
            Dim qryStock As String = ""
            qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

            '' query for transaction  UOM conversion
            Dim qryTransStock As String = ""
            If clsCommon.myLen(Unit_Code) <= 0 Then
                qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
            Else
                qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
            End If
            '===================Added By Preeti Gupta===================
            Dim qryRateStock As String = ""
            ' =============================================================

            Dim strItemGroup As String = ""
            strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" & _
                           " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " & _
                           " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" & _
                           " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "

            Dim strSDCommonQuery As String = ""
            Dim strTaxColumns As String = ""
            Dim strSDJoinQry As String = ""
            Dim strSDTaxRate As String = ""
            Dim strSDTaxRateColumn As String = ""
            Dim strSDTaxRateBlankColumn As String = ""
            strSDTaxRateBlankColumn = " '' as _Type ,"
            strSDTaxRateColumn = "  ttr._Type ,"
            Dim strSDEndQry As String = ",TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1_Rate"
            strSDCommonQuery = " select case when TSPL_SD_SALE_INVOICE_HEAD.Against_C_Form = 1 then 'C' else '' End as Formtype,case when ISNUll(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Document_Type else  CASE WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='ALL' THEN 'SD' ELSE TSPL_SD_SALE_INVOICE_HEAD.Trans_Type END end as Trans_Type  ,TSPL_SD_SALE_INVOICE_HEAD.Status ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location, " & _
                               " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Code , " & _
                               " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Line_No , " & _
                               " TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code , TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Item_Cost , " & _
                               " TSPL_SD_SALE_INVOICE_DETAIL.Amount *(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end as AMount,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per ,TSPL_SD_SALE_INVOICE_detail.Total_Disc_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) - case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end as Disc_Amt,case when coalesce(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_INVOICE_DETAIL.sampling,0)=1 or coalesce(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,'')='Y' then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) end as [Scheme Amount] , " & _
                               " (Amount-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='AS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end as Amt_Less_Discount , " & _
                               " TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Total_Tax_AMt ,(Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Total_Amt,case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS' then ''  when ManualVehicle <> '' then '' else TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code end as Vehicle_Code , case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS' then TSPL_SD_SALE_INVOICE_HEAD.VehicleNo  when ManualVehicle <> '' then ManualVehicle else COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_SD_SALE_INVOICE_HEAD.VEHICLENO) end as Vehicle_No,(case when TSPL_SD_SALE_INVOICE_DETAIL.Line_No=1 then (TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge+coalesce(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0))*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end) as  Additional_Charge, " & _
                               " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Document Amt]," & _
                               " TSPL_Customer_Invoice_Head.Discount_Amount*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Document Discount Amt], " & _
                               " TSPL_Customer_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Total Tax], " & _
                               " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Total Add Charge], " & _
                               " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                               " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_INVOICE_HEAD.GRNo as [GR No],tspl_sd_shipment_head.gr_date as [GR Date],TSPL_SD_SALE_INVOICE_HEAD.WayBillNo as [WayBill No],TSPL_SD_SHIPMENT_HEAD.transport_id as [Transporter Code],case when len(TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual) > 0 then TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual else TSPL_SD_SHIPMENT_HEAD.Transporter_Name end as [Transporter Name],(case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS else  TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code end) as [Delivery No]  ,Shipment_Code as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no as [Booking No], TSPL_SD_SALE_INVOICE_DETAIL.MRP ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Code as [Scheme Code] ,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Code as [Cash Scheme Code] ,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount as [Cash Scheme Amount], TSPL_SD_SALE_INVOICE_DETAIL.Price_code as [Price Code],'' as Created_By,'' as Modify_By ,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM,TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor,tspl_sd_sale_invoice_detail.Sampling,tspl_sd_sale_invoice_detail.Scheme_Item ,"

            strSDEndQry = " from TSPL_SD_SALE_INVOICE_DETAIL " & _
                               " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                               " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                               " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                               " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code " & _
                               " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
                               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "

            strSDJoinQry = "  where  2=2 " & _
                               " and (case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='FS' then 'Fresh Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' then 'Product Sale' " & _
                               " when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' then 'MCC Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type <>'MT' then 'Export Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type ='MT' then 'Merchant Trade' WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='SD' then 'General Sale' WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='CSA' then 'CSA Sale' " & _
                               " else  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                               " and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" & obj.Document_Code & "' "
            End If

            strMCCMaterial = " select case when len([Form Type])>0 then [Form Type] else   _Type end  as [Form Type],[Trans Type],[Location Code],[Location Name],Loc.State as [Location State],loc.TIN_No as [Dispatch Location Tin No], (CASE WHEN [Invoice Type]='T' THEN 'Tax' when [Invoice Type]='R' then 'Retail' when [Invoice Type]='N' then 'None' else [Invoice Type] end) as [Invoice Type],[Document No],[Document_date],Vehicle_Code as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,2)) as [Additional Amount],[Customer Code],[Customer Address],Cust.Struct_Code,[Customer Name],Cust.Cust_Group_Code as [Customer Group Code],Cust_Group.Cust_Group_Desc as [Customer Group Description],Cust.Zone_Code as [Customer Zone Code],Zone.Description as [Customer Zone Description], [Parent Customer No],[Parent Customer Code], [Parent Customer Name],coalesce(Cust.State_Code,Cust.State_Code) as [Customer State Code],coalesce(Cust.State_Name,Cust_Loc.State_Name) as [Customer State Desc],coalesce(Cust.Tin_No,cust_loc.Tin_No) as [Tin No],Item_Group.Item_Group as [Item Group Code],Item_Group.Group_Description as [Item Group Description] "
            
                If clsCommon.myLen(strCategoryTable) > 0 Then
                    ''richa agarwal to avoid ambiguous error
                    '  strMCCMaterial += "," + strCodeColumn + "," + strCodeDescColumn
                    strMCCMaterial += "," + strCodeColumnForVirtual + "," + strCodeDescColumn
                End If


            ' BM00000008438 BM00000008391
            'strMCCMaterial += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %],  (coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0))  as [Discount Amount],[Scheme Amount],[Amount Less Discount]  as [Amount Less Discount]" + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + ",case when [trans type]='Fresh Sale Retturn' then [Amount Less Discount] + case when coalesce([Total Tax Amount],0)<0 then 0 else coalesce( [Scheme Amount],0) end else ([Total Amount]-[Total Tax Amount] + case when coalesce([Total Tax Amount],0)>0 then 0 else coalesce( [Scheme Amount],0) end) end as [Sale Amount],[Total Tax Amount], (cast(Additional_Charge as numeric(18,2))+[Total Amount]) as [Total Amount], " & _
            strMCCMaterial += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],"
            If clsCommon.myLen(Unit_Code) <= 0 Then
                strMCCMaterial += " cast(([Item Cost]*Stock_SU.Conversion_Factor)/(case when coalesce(rate_stock_su.Conversion_Factor,1)=0 then 1 else coalesce(rate_stock_su.Conversion_Factor,1) end) as Numeric(18,3))  as [Item Rate] "
            Else
                strMCCMaterial += "  cast(( case when isnull(Rate_Stock_SU.Conversion_Factor,0)<=0 then ([Item Cost]) else ([Item Cost] * Rate_select_SU.Conversion_Factor)/ Rate_Stock_SU.Conversion_Factor end )"
                strMCCMaterial += " as Numeric(18,3)) as [Item Rate]"
            End If
            strMCCMaterial += " , [Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)*coalesce(StockKG.Conversion_Factor,1)/(100) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)*coalesce(StockKG.Conversion_Factor,1)/(100) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %],  (coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0))  as [Discount Amount],[Scheme Amount],[Amount Less Discount]  as [Amount Less Discount]" + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + ",case when [trans type]='Fresh Sale Return' then [Amount Less Discount]  else ([Total Amount]-[Total Tax Amount] + case when coalesce([Total Tax Amount],0)>0 then 0 else coalesce( [Scheme Amount],0) end) end as [Sale Amount],[Total Tax Amount], (cast(Additional_Charge as numeric(18,2))+[Total Amount]) as [Total Amount], " & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax]+ case when (coalesce([Total Tax Amount],0)<>0 or [Scheme Amount]<=0) and [Document No]<>'SRFS-003/15-16/000006' then 0 else coalesce([AR Document Discount Amt],0)  end as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge], "
            ''richa agarwal change to show csa sales account for csa sale and csa sale return
        strMCCMaterial += " left(Item.Sales_Account, Len(Item.Sales_Account)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code as [Sales Account],"

        'strMCCMaterial += "  case when [trans type] in ('CSA Sale','CSA Sale Return') then  left(Item.Sales_Account, Len(Item.Sales_Account)-3)+  'TSPL_LOCATION_MASTER.Loc_Segment_Code else  (case when coalesce(item.GSOC_Acct,'')<>'' then  left( item.GSOC_Acct, Len( item.GSOC_Acct)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code 'else '' end)  end as [Sales Account], " & _

        strMCCMaterial += " [GR No],convert(varchar,[GR Date],103) as [GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount], [Price Code], case when Sampling=0 then  'N' else case when sampling=1 then'Y' end end as sampling, Scheme_Item as [Scheme Type],isnull(TSPL_ITEM_UOM_DETAIL.Net_Weight,0)*cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Net Weight] ,status "



            ''richa agarwal add merchant trade trans_type in below qry BM00000008390 (Applied For DCC Also)
            strMCCMaterial += " from (select max(final._Type) as _Type , max(final.FormType) as [Form Type],case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='EX' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' when Trans_Type='MT' then 'Merchant Trade' WHEN Trans_Type ='SD' then 'General Sale' else  Trans_Type end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) AS [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]- case when (Trans_Type ='FS') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] ,[Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Created_By,final.Modify_By ,final. RATE_UOM,final. Conv_Factor,final.Sampling,final.Scheme_Item"
            strMCCMaterial += " from ("
            'strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as Tax1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1Rate"
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied

            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & "  and (coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax7,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax10,'')='') )t "

            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + ") " & _

            strMCCMaterial += "   union all"
            '' query for tax1 applied
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as Tax1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1Rate"

            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax1 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and TSPL_SD_SALE_INVOICE_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as Tax2_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2+'%' as Tax2Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax2 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as Tax3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3+'%' as Tax3Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax3 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as Tax4_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX4+'%' as Tax4Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax4 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt as Tax5_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX5+'%' as Tax5Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax5 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX6 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt as Tax6_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX6+'%' as Tax6Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax6 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX7 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt as Tax7_AMt,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX7+'%' as Tax7Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax7 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX8 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt as Tax8_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX8+'%' as Tax8Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax8 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX9 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt as Tax9_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX9+'%' as Tax9Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax9 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX10 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt as Tax10_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX10+'%' as Tax10Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax10 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " )final"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount] , [Price Code] ,final.Created_By,final.Modify_By,final.RATE_UOM,final.Conv_Factor,final.Sampling,final.Scheme_Item " '', " + strPivotFoGrouprOuterQuery + "


            strMCCMaterial += " union all"
            strMCCMaterial += " select '' as _Type,'' as Formtype, case when InvoiceAgainst='Against Dispatch Trade' then 'Bulk Sale Trade'  else 'Bulk Sale' end as Trans_Type ,coalesce(TSPL_LOCATION_MASTER.Main_Location_Code,TSPL_LOCATION_MASTER.Location_Code) as Bill_To_Location,TSPL_INVOICE_Master_BULKSALE.Posted,coalesce(Main_Loc.Location_Desc,TSPL_LOCATION_MASTER.location_desc) as Location_Desc ,'Invoice' as Invoice_type ,TSPL_INVOICE_Master_BULKSALE.Document_No as Document_code ,convert(varchar,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) Document_Date,case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')='' then TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No else isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')  end as Vehicle_Code,case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')='' then TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No else isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')  end as Vehicle_No,(case when ROW_NUMBER() over (partition by TSPL_INVOICE_Master_BULKSALE.Document_No order by TSPL_INVOICE_DETAIL_BULKSALE.Item_Code )=1 then coalesce(TSPL_INVOICE_Master_BULKSALE.RoundOffAmount,0) else 0 end) as Additional_Charge, " & _
                              " TSPL_INVOICE_Master_BULKSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Parent_Customer_No,"
            strMCCMaterial += " Parent_Master.Cust_Code as Parent_Customer_Code,Parent_Master.Customer_Name as Parent_Cust_Name, "
            strMCCMaterial += " TSPL_INVOICE_DETAIL_BULKSALE.Item_Code,tspl_item_master.Item_Desc ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty ,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Item_cost,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatKG ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFKG  ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,0 as Disc_per,0 as Disc_Amt,0 as [Scheme Amount],TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amt_less_Discount,0 as Total_tax_amt " + strPivotForOuterQueryforBulk + " " + strDoublePivotForOuterQueryforBulk + ",(TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount) as Total_Amt, " & _
                              " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                              " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                              " (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.total_tax-TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                              " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge],'' as [GR No],null as [GRN Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as  [Delivery No]  ,'' as  [Shipment No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],TSPL_INVOICE_Master_BULKSALE.Created_By ,TSPL_INVOICE_Master_BULKSALE.Modified_By,NULL as RATE_UOM,0 as Conv_Factor ,0 as Sampling,'N' as Scheme_Item " & _
                              " from TSPL_INVOICE_DETAIL_BULKSALE "

            strMCCMaterial += " left outer join TSPL_INVOICE_Master_BULKSALE on TSPL_INVOICE_Master_BULKSALE.Document_No =TSPL_INVOICE_DETAIL_BULKSALE.Document_No "
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_INVOICE_Master_BULKSALE.Location_Code"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as Main_Loc on TSPL_LOCATION_MASTER.Main_Location_Code =Main_Loc.Location_Code"
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_INVOICE_Master_BULKSALE.Customer_Code"
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_INVOICE_DETAIL_BULKSALE.Item_Code"
        strMCCMaterial += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_INVOICE_Master_BULKSALE.Document_No  where 2=2 " & _
                            " and (case when InvoiceAgainst='Against Dispatch Trade' then 'Bulk Sale Trade'  else 'Bulk Sale' end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "
        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strMCCMaterial += " and TSPL_INVOICE_DETAIL_BULKSALE.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strMCCMaterial += " and TSPL_INVOICE_Master_BULKSALE.Location_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strMCCMaterial += " and TSPL_INVOICE_Master_BULKSALE.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strMCCMaterial += " and TSPL_INVOICE_Master_BULKSALE.Document_No = '" & obj.Document_Code & "' "
        End If

            strMCCMaterial += " union all"
            strMCCMaterial += " select max(final._Type) as _Type,max(final.Formtype) as [Form Type],case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='Exp' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' WHEN Trans_Type ='SD' then 'General Sale' else Trans_Type  end  as [Trans Type],final.Loc_Code  as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.shipment_No  as [Document No],final.Document_Date as [Document_date],'' as Vehicle_Code,'' as Vehicle_No,0 as Additional_Charge,final.cust_Code  as [Customer Code],MAX(final.CustAdd ) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.shipped_Qty  as [Quantity],final.Unit_code as [UOM],final.price  as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.ItemAmt ,final.DiscountPer  as [Discount Per],final.TotalDiscountAmt   as [Discount Amount],final.[Scheme Amount]   as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.TotalTaxAmt  as [Total Tax Amount],final.Doc_Amt  as [Total Amount], " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] ,[Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Created_By ,final.Modify_By,final. RATE_UOM,final.Conv_Factor ,0 as Sampling,'N' as Scheme_Item  from ("

            Dim strScarpCommonQry As String = ""
            strScarpCommonQry = " select '' as Formtype,'SS' as Trans_Type,TSPL_SCRAPINVOICE_HEAD.ispost as Status ,TSPL_SCRAPINVOICE_HEAD.Loc_Code,TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
                                " ,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd, " & _
                                " case when isnull(Is_CashSale,'N')='Y' then 'Cash Sale Invoice' when isnull(Is_Scrap,'N')='Y' then 'Scrap Sale Invoice' else 'Misc Sale Invoice' end as Invoice_Type,TSPL_SCRAPINVOICE_HEAD.invoice_No as shipment_No ,convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103 ) as Document_Date , " & _
                                " TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code ," & _
                                " TSPL_SCRAPINVOICE_DETAIL.price ,0 as InvoiceFatPer ,0 as InvoiceSNFPer ,0 as InvoiceFatKG ,0 as InvoiceSNFKG , " & _
                                " TSPL_SCRAPINVOICE_DETAIL.ItemAmt ,TSPL_SCRAPINVOICE_DETAIL.DiscountPer,0 as [Scheme Amount] ,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as Amt_less_Discount, " & _
                                " TSPL_SCRAPINVOICE_DETAIL.TotalDiscountAmt , " & _
                                " TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt ,TSPL_SCRAPINVOICE_DETAIL.TotalAmt+Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end as Doc_Amt,'' as Vehicle_Code,'' as Vehicle_No,Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end as Additional_Charge," & _
                                " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                                " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                                " TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                                " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                                " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                                " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code] ,'' as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP,  '' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code], '' as Created_By ,'' as Modify_By ,NULL as RATE_UOM,0 as Conv_Factor,"

            strSDEndQry = " from TSPL_SCRAPINVOICE_DETAIL  " & _
                          " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No " & _
                          " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.invoice_No " & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD.Cust_Code " & _
                          " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPINVOICE_HEAD.shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No "

            strSDJoinQry = " where 2 = 2 AND 'Misc Sale' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                          " and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SCRAPINVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SCRAPINVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SCRAPINVOICE_HEAD.cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDJoinQry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = '" & obj.Document_Code & "' "
            End If
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied



            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax7,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax10,'')='') )t "
            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            '' query for tax1 applied
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX1 ,TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax1 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax1<>'')"
            strMCCMaterial += " s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX2 ,TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax2 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax2<>'' )"
            strMCCMaterial += " s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = "TSPL_SCRAPINVOICE_DETAIL.TAX3 ,TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt , TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax3 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate and ttr._type<>'OH'"
            strMCCMaterial += "  select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax3<>'' )"
            strMCCMaterial += " s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX4 ,TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax4 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax4<>'' )"
            strMCCMaterial += " s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX5 ,TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax5 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax5<>'' )"
            strMCCMaterial += " s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX6 ,TSPL_SCRAPINVOICE_DETAIL.TAX6_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX6_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax6 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX6_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax6<>'' )"
            strMCCMaterial += " s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX7 ,TSPL_SCRAPINVOICE_DETAIL.TAX7_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX7_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax7 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX7_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax7<>'' )"
            strMCCMaterial += " s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + ")) t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX8 ,TSPL_SCRAPINVOICE_DETAIL.TAX8_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX8_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax8 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX8_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax8<>'' )"
            strMCCMaterial += "  s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX9 ,TSPL_SCRAPINVOICE_DETAIL.TAX9_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX9_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax9 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX9_Rate and ttr._type<>'OH'"
            strMCCMaterial += "  select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax9<>'')"
            strMCCMaterial += " s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX10 ,TSPL_SCRAPINVOICE_DETAIL.TAX10_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX10_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax10 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX10_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax10<>'')"
            strMCCMaterial += " s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " ) final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Loc_Code  left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.shipment_No  ,final.Item_Code ,final.Loc_Code  ,final.cust_Code  ,final.shipped_Qty  ,final.TotalTaxAmt  ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.price  ,final.ItemAmt  ,final.DiscountPer,final.TotalDiscountAmt  ,final.Amt_less_Discount   ,final.Amt_Less_Discount ,final.Doc_Amt,final.[Scheme Amount],QC.FAT_Per,QC.SNF_Per,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No] ,MRP ,  [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Created_By ,final.Modify_By,final.RATE_UOM,final. Conv_Factor " ' ," + strPivotFoGrouprOuterQuery + " 
            '' query for return

            Dim strSDRCommonQuery As String = ""


            strSDRCommonQuery = " select '' as Formtype,(CASE WHEN TSPL_SD_SALE_RETURN_HEAD.Trans_Type='ALL' THEN 'SDR' ELSE TSPL_SD_SALE_RETURN_HEAD.Trans_Type+'R' END) as Trans_Type,TSPL_SD_SALE_RETURN_HEAD.Status ,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location, " & _
                              " TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_RETURN_HEAD.Document_Type,TSPL_SD_SALE_RETURN_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code , " & _
                              " convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_SD_SALE_RETURN_DETAIL.Line_No , " & _
                              " -TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code ,TSPL_SD_SALE_RETURN_DETAIL.Item_Cost , " & _
                              " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as Amount ,TSPL_SD_SALE_RETURN_DETAIL.Disc_Per ,case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)=0 then -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)  + case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then 1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end as Disc_Amt,case when coalesce(FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then -1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) end  as [Scheme Amount] , " & _
                              " -(Amount- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then Total_Disc_Amt else Total_Disc_Amt end  + case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type<>'FS' then case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else 0 end)  as Amt_Less_Discount , " & _
                              " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt ,-(Amount+coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0)- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then 0 else coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end )  as Total_Amt,TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,-(case when TSPL_SD_SALE_RETURN_DETAIL.Line_No=1 then (coalesce(TSPL_SD_SALE_RETURN_HEAD.Total_Add_Charge,0)+coalesce(TSPL_SD_SALE_RETURN_HEAD.RoundOffAmount,0)) else 0 end) as Additional_Charge, " & _
                              " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                              " -TSPL_Customer_Invoice_Head.Discount_Amount-coalesce(TSPL_SD_SALE_RETURN_HEAD.headDisc_AMt,0) as [AR Document Discount Amt], " & _
                              " -TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],-TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                              " -(TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                              " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                              " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_RETURN_HEAD.GRNo as [GR No],TSPL_SD_SHIPMENT_HEAD.gr_date as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code as [Delivery No],Against_Shipment_No as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No as [Booking No],TSPL_SD_SALE_RETURN_DETAIL.MRP, TSPL_SD_SALE_RETURN_DETAIL.Scheme_Code ,TSPL_SD_SALE_RETURN_DETAIL.Cash_Scheme_Code ,TSPL_SD_SALE_RETURN_DETAIL.Cash_Scheme_Amount*(-1) as Cash_Scheme_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Price_code ,'' as Created_By ,'' as Modify_By,NULL as RATE_UOM,0 as Conv_Factor, TSPL_SD_SALE_RETURN_DETAIL.Sampling,TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item,"
            strSDEndQry = " from TSPL_SD_SALE_RETURN_DETAIL " & _
                                " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
                                " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_RETURN_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                                " left join TSPL_Customer_Invoice_Head on TSPL_SD_SALE_RETURN_HEAD.Document_Code=  case when len(isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,''))>0  then TSPL_Customer_Invoice_Head.Against_Sale_Return_No else TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return end " & _
                                " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code " & _
                                " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " & _
                                " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code " & _
                                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code "
 _
            strSDJoinQry = " WHERE 2=2 AND (case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='FS' then 'Fresh Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='CSA' then 'CSA Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' then 'Product Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' then 'MCC Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='EXP' then 'Export Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='Bulk Sale' then 'Bulk Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='SS' then 'Misc Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type in ('SD','All') then 'General Sale Return' else TSPL_SD_SALE_RETURN_HEAD.trans_Type  end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDJoinQry += " and TSPL_SD_SALE_RETURN_HEAD.Document_Code = '" & obj.Document_Code & "' "
            End If

            strMCCMaterial += " union all "

            strMCCMaterial += " select max(final._Type) as _Type, max(final.Formtype) as [Form Type],case when Trans_Type ='FSR' then 'Fresh Sale Return' when Trans_Type ='CSAR' then 'CSA Sale Return' when Trans_Type='PSR' then 'Product Sale Return' when Trans_Type='MCCR' then 'MCC Sale Return' when Trans_Type='EXPR' then 'Export Sale Return'when Trans_Type='Bulk Sale' then 'Bulk Sale Return' when Trans_Type ='SSR' then 'Misc Sale' when Trans_Type ='SDR' then 'General Sale Return' else trans_Type  end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount], " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]  - case when (Trans_Type ='FSR' or Trans_Type ='PSR') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],final.[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code ,final.Created_By ,final.Modify_By,final.RATE_UOM ,final.Conv_Factor ,final. Sampling,final.Scheme_Item from ( "

            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax3,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax5,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax7,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax9,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax10,'')='') )t "

            strMCCMaterial += "   union all"
            '' query for tax1 applied
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX1 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt as TAX1_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax1 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX2 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt as TAX2_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax2 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX3 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt as TAX3_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax3 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate and ttr._type<>'OH'"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax7 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt as TAX4_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax4 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt as TAX5_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax5 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate and ttr._type<>'OH'"

            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX6 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt as TAX6_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX6_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax6 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX6_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX7 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt as TAX7_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax7 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX8 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt as TAX8_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX8_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax8 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX8_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX9 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt as TAX9_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX9_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax9 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX9_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX10 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt as TAX10_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX10_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax10 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX10_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " )final"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code ,final.Created_By ,final.Modify_By ,final.RATE_UOM,final.Conv_Factor ,final. Sampling,final.Scheme_Item " '', " + strPivotFoGrouprOuterQuery + " 

            strMCCMaterial += " union all "

            '''' bulk sale return 
            strMCCMaterial += "  select '' as _Type,'' as Formtype,'Bulk Sale Return' as Trans_Type ,TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code as Bill_To_Location,TSPL_SALE_RETURN_MASTER_BULKSALE.Posted,TSPL_LOCATION_MASTER.Location_Desc ,'Invoice' as Invoice_type ,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as Document_code ,convert(varchar,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) Document_Date,'' as Vehicle_Code,'' as Vehicle_No,coalesce(-1 * TSPL_SALE_RETURN_MASTER_BULKSALE.roundoffamount,0) as Additional_Charge, " & _
                               " TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Parent_Customer_No," & _
                               " Parent_Master.Cust_Code as Parent_Customer_Code,Parent_Master.Customer_Name as Parent_Cust_Name ," & _
                               " TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code,tspl_item_master.Item_Desc ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as Qty ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate as Item_cost,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatKG as InvoiceFatKG ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFKG as InvoiceSNFKG  ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Amount,0 as Disc_per,0 as Disc_Amt,0 as [Scheme Amount],-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Amt_less_Discount,0 as Total_tax_amt " + strPivotForOuterQueryforBulk + " " + strDoublePivotForOuterQueryforBulk + ",-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Total_Amt, " & _
                               " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-1 * TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                               " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                               " -1 * (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.total_tax-TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                               " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge],'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as [Delivery No]  ,'' as  [Shipment No],'' as  [Booking No],0 AS MRP, '' as [Scheme Code] ,'' as  [Cash Scheme Code] , 0 as [Cash Scheme Amount], '' as [Price Code] ,TSPL_SALE_RETURN_MASTER_BULKSALE.Created_By ,TSPL_SALE_RETURN_MASTER_BULKSALE.Modified_By,NULL as RATE_UOM,0 as Conv_Factor,0 as Sampling,'N' as Scheme_Item" & _
                               " from TSPL_SALE_RETURN_DETAIL_BULKSALE "

            strMCCMaterial += " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No "
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code"
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code"
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code"
            strMCCMaterial += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_Return_No=TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  where 2=2 " & _
                                " and 'Bulk Sale Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                                " and convert(date,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            Dim strTranferCommonQuery As String = ""
            strTranferCommonQuery = " select case when TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF = 1 then 'F' else '' End as Formtype,'Transfer' as Trans_Type,TSPL_TRANSFER_ORDER_HEAD.Status ,TSPL_TRANSFER_ORDER_HEAD.From_Location as Bill_To_Location, " & _
                                    " GIT_Main_Loc.Location_Code as To_Location,TSPL_TRANSFER_ORDER_HEAD.Transfer_Type as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No , " & _
                                    " convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code , " & _
                                    " (case when TSPL_TRANSFER_ORDER_DETAIL.In_Qty>0 then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty end) as Qty, " & _
                                    " TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost , " & _
                                    " TSPL_TRANSFER_ORDER_DETAIL.Amount ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt ,0 as [Scheme Amount], " & _
                                    " (TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Amt_Less_Discount ,0 as Total_Tax_Amt ,(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Total_Amt,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No) as Vehicle_No,0 as Additional_Charge, " & _
                                    " '' as [AR Document No],0 as  [AR Document Amt],0 as [AR Document Discount Amt],0 as  [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_TRANSFER_ORDER_HEAD.GR_No as [GR No],TSPL_TRANSFER_ORDER_HEAD.gr_date as [GR Date],TSPL_TRANSFER_ORDER_HEAD.WayBill_No as [WayBill No],TSPL_TRANSFER_ORDER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) > 0 then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end  as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP, '' as [Scheme Code] ,'' as  [Cash Scheme Code] , 0 as [Cash Scheme Amount], '' as [Price Code],'' as Created_By,'' as Modified_By,NULL as RATE_UOM,0 as Conv_Factor, "

            strSDEndQry = " from TSPL_TRANSFER_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                          " left join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_Head.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                          " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSFER_ORDER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                          " left join ( select  max(Location_Code) as Location_Code,GIT_Location from TSPL_LOCATION_MASTER where GIT_Location is not null " & _
                          " group by GIT_Location ) GIT_Main_Loc on TSPL_TRANSFER_ORDER_HEAD.To_Location=GIT_Main_Loc.GIT_Location  "

            strSDJoinQry = "  where Transfer_Type <>'I' AND 'Transfer' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                          " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDJoinQry += " and GIT_Main_Loc.Location_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDJoinQry += " and TSPL_TRANSFER_ORDER_HEAD.Document_No = '" & obj.Document_Code & "' "
        End If
        ' & " coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)))/coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,1) ,2) as  Item_Cost "_
            strMCCMaterial += " union all "
        strMCCMaterial += "   select *,0 as Sampling,'N' as Scheme_Item from (Select '' as _Type, '' as Formtype,'MCC Transfer' as Trans_Type ,TSPL_MCC_Dispatch_Challan.MCC_Code as  Bill_To_Location,TSPL_MCC_Dispatch_Challan.isPosted as " _
            & " Status, sendr.location_desc as  location_desc,'MCC Transfer' as Invoice_Type,TSPL_MCC_Dispatch_Challan.Chalan_NO as PI_NO , " _
            & " convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103 ) as PI_Date, '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge , " _
            & " tspl_milk_Transfer_In.location_Code as Customer_Code,'' AS [CustAdd],  recv.Location_Desc  as Customer_Name ,'' as [Parent Vendor No],'' as [Parent Vendor Code]," _
            & " '' as [Parent Vendor Name], TSPL_MCC_Dispatch_Challan.Item_Code, TSPL_MCC_Dispatch_Challan.Item_Desc , TSPL_MCC_Dispatch_Challan.Net_Qty  as Qty " _
            & " ,TSPL_MCC_Dispatch_Challan.UOM_Code as  Unit_code ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) " _
            & " * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * " _
            & " coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)))/case when coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,1)=0 then 1 else coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,1) end ,2) as  Item_Cost " _
                & " ,  t_FAT_Recd.Param_Field_Value as [FAT Per]" _
                & " ,t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) " _
                & " as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) as [SNF KG],amount as  Amount ,0" _
                & " as Disc_Per  ,0 as Disc_Amt,0 as [Scheme Amount] ,  amount as  Amt_Less_Discount   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & ",   0 as Total_Tax_Amt " _
                & " ,amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AR Document No],TSPL_vendor_Invoice_Head.Document_Total [AR Document Amt]" _
                & " ,TSPL_vendor_Invoice_Head.Discount_Amount as [AR Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AR Amount Before Tax]," _
                & " TSPL_vendor_Invoice_Head.total_tax as [AR Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AR Total Add Charge],'' as [GRNO],null as [GRN Date],'' as Way_BillNo," _
                & " TSPL_MCC_Dispatch_Challan.tanker_No,tm.description,'' as  [delivery No],'' as [Shiping No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],'' as Created_By ,'' as Modified_By,NULL as RATE_UOM,0 as Conv_Factor from TSPL_MCC_Dispatch_Challan  left outer  join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No " _
                & " =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_location_Master  sendr ON sendr.Location_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE " _
                & " left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on " _
                & " recv.location_code=TSPL_MILK_TRANSFER_IN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No" _
                & " =TSPL_MILK_TRANSFER_IN.Receipt_Challan_No left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join " _
                & " (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On " _
                & " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') " _
                & " t_SNF_Recd On t_SNF_Recd.Chalan_NO   = TSPL_MCC_Dispatch_Challan.Chalan_NO   Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*" _
                & " From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = " _
                & " TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.Chalan_No " _
                & " = TSPL_MCC_Dispatch_Challan.Chalan_NO where 2=2 " & _
                  " AND 'MCC Transfer' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                  " and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <= convert(date,('" & To_Date & "'),103) )t "
            '' transaction unit conversion
            strMCCMaterial += " union all "
            strMCCMaterial += " select max(final._Type) as _Type, max(Formtype) as [Form Type],Trans_type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.To_Location as [Customer Code],'' As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Location_Desc) as [Customer Name] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount]," & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,  [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount],  [Price Code],final.Created_By ,final.Modified_By,final.RATE_UOM,final.Conv_Factor,0 as Sampling,'N' as Scheme_Item from ( "
            'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

            '' query for no tax applied
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax1,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax5,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax7,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax9,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax10,'')='') )t "
            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            '' quert for no tax applied
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax1 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX2 ,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax2 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX3 ,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax3 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX4 ,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax4 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX5 ,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax5 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate and ttr._type<>'OH'"

            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax6 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX7 ,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax7 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX8 ,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax8 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax9 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX10 ,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax10 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " )final"
            ''-------------------------
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location "
            strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            'strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_No ,final.Item_Code ,final.Bill_To_Location ,final.To_Location ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,[Scheme Code] ,[Cash Scheme Code] ,[Cash Scheme Amount], [Price Code] ,final.Created_By ,final.Modified_By,final. RATE_UOM,final.Conv_Factor"
            ''richa

            strMCCMaterial += Environment.NewLine + " --- QUERY FOR TRANSFER RETURN---------------------- ADDED BY RICHA AGARWAL ------------" + Environment.NewLine


            Dim strTranferReturnCommonQuery As String = ""
            strTranferReturnCommonQuery = " select case	when TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF = 1 then 'F' else '' End as Formtype,'Transfer Return' as Trans_Type,TSPL_TRANSFER_ORDER_HEAD.Status ,  TSPL_TRANSFER_ORDER_HEAD.From_Location as Bill_To_Location, " & _
                                    " GIT_Main_Loc.Location_Code as To_Location,TSPL_TRANSFER_ORDER_HEAD.Transfer_Type as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No , " & _
                                    " convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code , " & _
                                    " -(case when TSPL_TRANSFER_ORDER_DETAIL.In_Qty>0 then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty end) as Qty, " & _
                                    " TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost , " & _
                                    " -TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt ,0 as [Scheme Amount], " & _
                                    " -(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Amt_Less_Discount ,0 as Total_Tax_Amt ,-(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Total_Amt,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No) as Vehicle_No,0 as Additional_Charge, " & _
                                    " '' as [AR Document No],0 as  [AR Document Amt],0 as [AR Document Discount Amt],0 as  [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_TRANSFER_ORDER_HEAD.GR_No as [GR No],TSPL_TRANSFER_ORDER_HEAD.gr_date as [GR Date],TSPL_TRANSFER_ORDER_HEAD.WayBill_No as [WayBill No],TSPL_TRANSFER_ORDER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) > 0 then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP, '' as [Scheme Code] , '' as [Cash Scheme Code] ,0 as   [Cash Scheme Amount],  '' as [Price Code],'' as Created_By ,'' as Modified_By,NULL as RATE_UOM,0 as Conv_Factor,"

            strSDEndQry = " from TSPL_TRANSFER_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                " Left Outer Join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_RETURN.Transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_No " & _
                          " left join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_Head.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                          " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSFER_ORDER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                          " left join ( select  max(Location_Code) as Location_Code,GIT_Location from TSPL_LOCATION_MASTER where GIT_Location is not null " & _
                          " group by GIT_Location ) GIT_Main_Loc on TSPL_TRANSFER_ORDER_HEAD.To_Location=GIT_Main_Loc.GIT_Location  "

            strSDJoinQry = " where TSPL_TRANSFER_ORDER_HEAD.Transfer_Type ='O' AND 'Transfer Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                          " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) AND ISNULL(TSPL_TRANSFER_RETURN.Document_No ,'')<>'' "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDJoinQry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDJoinQry += " and  GIT_Main_Loc.Location_Code  in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDJoinQry += " and TSPL_TRANSFER_RETURN.Document_No = '" & obj.Document_Code & "' "
            End If

            strMCCMaterial += " union all "
            strMCCMaterial += " select max(final._Type) as _Type,max(Formtype) as [Form Type],Trans_type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER.Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge, final.To_Location as [Customer Code],'' As [Customer Address] ,max(TSPL_CUSTOMER_MASTER.Location_Desc) as [Customer Name] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount]," & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount],  [Price Code] ,final.Created_By ,final.Modified_By,final. RATE_UOM,final. Conv_Factor,0 as Sampling,'N' as Scheme_Item from ( "
            'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

            '' query for no tax applied
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax1,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax5,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax7,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax9,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax10,'')='') )t "

            strMCCMaterial += Environment.NewLine + " union all "
            '' quert for no tax applied
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT AS TAX1_AMT ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax1 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX2 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt AS TAX2_Amt ,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax2 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX3 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt AS TAX3_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax3 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX4 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt AS TAX4_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax4 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX5 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt AS TAX5_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax5 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate and ttr._type<>'OH'"

            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt AS TAX6_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax6 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX7 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt AS TAX7_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax7 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX8 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt AS TAX8_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax8 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt AS TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax9 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX10 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt AS TAX10_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax10 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate and ttr._type<>'OH'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t" & _
             " )final" & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location " & _
             " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code " & _
             " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location " & _
             " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code " & _
             " group by  final.Trans_Type,final .Status  ,final.Document_No ,final.Item_Code ,final.Bill_To_Location ,final.To_Location ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,  [Scheme Code] ,  [Cash Scheme Code] , [Cash Scheme Amount],  [Price Code] ,final.Created_By ,final.Modified_By,final. RATE_UOM,final.Conv_Factor" + Environment.NewLine

            strMCCMaterial += Environment.NewLine + " --- QUERY FOR TRANSFER RETURN END---------------------- ADDED BY RICHA AGARWAL ------------" + Environment.NewLine


            strMCCMaterial += ") xx"
            '===============Added by preeti Gupta ===
            strMCCMaterial += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =xx.[Location Code] "
            '=======================================
            ''richa agarwal change to show csa sales account for csa sale and csa sale return
            '' strMCCMaterial += " left outer join (select ITEM.Item_Code,ITEM.Item_Desc,ITEM.Structure_Code,SA.Sales_Account from TSPL_ITEM_MASTER Item left join TSPL_SALES_ACCOUNTS SA on Item.Sale_Class_Code=SA.Sales_Class_Code) Item on Item.Item_Code =xx.[Item Code] "
            strMCCMaterial += " left outer join (select ITEM.Item_Code,ITEM.Item_Desc,ITEM.Structure_Code,SA.Sales_Account, isnull(CA.GSOC_Acct,'') as GSOC_Acct from TSPL_ITEM_MASTER Item left join TSPL_SALES_ACCOUNTS SA on Item.Sale_Class_Code=SA.Sales_Class_Code left join TSPL_CUSTOMER_ACCOUNT_SET  CA on Item.Cust_Account =CA.Cust_Account ) Item on Item.Item_Code =xx.[Item Code] "
            ''--------------------------
            '' transaction unit conversion
            strMCCMaterial += " inner join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " "
            ''end transaction unit conversion
            strMCCMaterial += " left join (" & strItemGroup & ") as Item_Group on Item.Structure_Code=Item_Group.Structure_Code "
            'strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location "
            strMCCMaterial += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code "
            '===============================Added By Preeti Gupta===================================
            strMCCMaterial += " left join (" & qryStock & ") as Rate_Stock_SU on xx.[Item Code]=Rate_Stock_SU.Item_Code and isnull(xx.[RATE_UOM],xx.UOM) =Rate_Stock_SU.UOM_Code  "
            strMCCMaterial += " inner join (" & qryTransStock & ") as Rate_select_SU on xx.[Item Code]=Rate_select_SU.Item_Code and Rate_select_SU.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " "
            ' ============================================================================================
            strMCCMaterial += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  "
            strMCCMaterial += " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =xx.[Item Code]  and TSPL_ITEM_UOM_DETAIL.uom_code=xx.[uom]"
            strMCCMaterial += " left join (select Cust_Code,Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_CUSTOMER_MASTER.Struct_Code,TSPL_CUSTOMER_MASTER.Tin_No,TSPL_CUSTOMER_MASTER.state as State_Code,tspl_State_Master.State_Name from TSPL_CUSTOMER_MASTER left join tspl_State_Master on tspl_State_Master.state_Code=TSPL_CUSTOMER_MASTER.state) as Cust on xx.[Customer Code]=Cust.Cust_Code  left join (select location_Code as Cust_Code,Tin_No,State_Code,State_Name from TSPL_location_MASTER left join tspl_State_Master on tspl_State_Master.state_Code=TSPL_location_MASTER.state) as Cust_Loc on xx.[Customer Code]=Cust_Loc.Cust_Code  "
            strMCCMaterial += " left join (select Cust_Group_Code,Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER) as Cust_Group on Cust.Cust_Group_Code=Cust_Group.Cust_Group_Code "
            strMCCMaterial += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code " & _
                              " left join TSPL_LOCATION_MASTER as Loc on Loc.Location_Code=xx.[Location Code] "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                strMCCMaterial += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]"
            End If
        strMCCMaterial += " where 2 = 2  and  convert(date,xx.Document_Date,103) >= convert(date,('" + From_Date + "'),103) and convert(date,xx.Document_Date,103) <= convert(date,('" + To_Date + "'),103) and xx.Status=1 " ' + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and xx.[UOM]='" + txtUOM.Value + "' ", ""))
            QryLst.Add(strMCCMaterial)
            QryLst.Add(strPivotForFinalOuterQuery)
            Return QryLst
    End Function
    Public Shared Function ConversionKGtoLTRorLTRtoKG(ByVal QryCond As String)
        
        If clsCommon.myLen(QryCond) > 0 Then
            QryCond = " And " & QryCond
        End If
        Dim Qry As String = " Select (FromUOM) as FromUOM,(TOUOM) as TOUOM,max(CF) as CF From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Product_Type ='MI' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION  where Product_Type ='MI' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where Product_Type ='MI' UNION  All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where Product_Type ='MI' ) yyy  group by FromUOM, TOUOM"
        Return Qry
    End Function
End Class
