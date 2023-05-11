Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class clsMilkPurchaseInvoiceProvisionHead
    Public Shared Function SaveData(ByVal obj As clsMilkPurchaseInvoiceMCC, ByVal objList As List(Of clsMilkPurchaseInvoiceMCCDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim squery As String = "delete from TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL where MILK_DOC_Code in (select DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD where convert(date,DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and MCC_CODE='" + obj.MCC_CODE + "' and VSP_CODE='" + obj.VSP_CODE + "')"
            clsDBFuncationality.ExecuteNonQuery(squery, trans)

            squery = "delete from TSPL_MILK_PURCHASE_INVOICE_PROVISION_DETAIL where Doc_Code in (select DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD where convert(date,DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and MCC_CODE='" + obj.MCC_CODE + "' and VSP_CODE='" + obj.VSP_CODE + "')"
            clsDBFuncationality.ExecuteNonQuery(squery, trans)

            squery = "delete from TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD where DOC_CODE in (select DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD where convert(date,DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and MCC_CODE='" + obj.MCC_CODE + "' and VSP_CODE='" + obj.VSP_CODE + "')"
            clsDBFuncationality.ExecuteNonQuery(squery, trans)

            obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"), clsDocType.MilkPurInvoiceProvsion, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "Irregular_MCC_CODE", obj.Irregular_MCC_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)

            clsCommon.AddColumnsForChange(coll, "VENDOR_INVOICE_NO", IIf(clsCommon.myCstr(obj.VENDOR_INVOICE_NO) = "", obj.DOC_CODE, clsCommon.myCstr(obj.VENDOR_INVOICE_NO)))
            clsCommon.AddColumnsForChange(coll, "VENDOR_INVOICE_DATE", clsCommon.GetPrintDate(obj.VENDOR_INVOICE_DATE, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", clsCommon.myCstr(obj.ROUTE_CODE))
            clsCommon.AddColumnsForChange(coll, "Payment", clsCommon.myCstr(obj.Payment))
            clsCommon.AddColumnsForChange(coll, "Total_Amount", clsCommon.myCdbl(obj.Amount))
            clsCommon.AddColumnsForChange(coll, "Total_Basic_Amount", clsCommon.myCdbl(obj.Basic_Amount))
            clsCommon.AddColumnsForChange(coll, "Total_Commission", clsCommon.myCdbl(obj.Commission))
            clsCommon.AddColumnsForChange(coll, "Total_Amount_ACc", clsCommon.myCdbl(obj.Total_Amount_Acc))
            clsCommon.AddColumnsForChange(coll, "Total_PaymentCommission", clsCommon.myCdbl(obj.Total_PaymentCommission))
            clsCommon.AddColumnsForChange(coll, "Total_Head_Load_Amount", clsCommon.myCdbl(obj.Total_Head_Load_Amount))
            clsCommon.AddColumnsForChange(coll, "Total_Own_Asset_Amount", clsCommon.myCdbl(obj.Total_Own_Asset_Amount))
            clsCommon.AddColumnsForChange(coll, "Total_Deduction_Amount", clsCommon.myCdbl(obj.Total_Deduction_Amount))
            clsCommon.AddColumnsForChange(coll, "Description", clsCommon.myCstr(obj.Description))

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "SRN_Net_Amount", obj.SRN_Net_Amount)
            clsCommon.AddColumnsForChange(coll, "SRN_RO_Amount", obj.SRN_RO_Amount)
            '' save program_code
            clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code, True)
            If Not obj.FROM_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd-MMM-yyyy"), True)
            End If
            If Not obj.TO_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd-MMM-yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Handling_Charges_Per", obj.Handling_Charges_Per)
            clsCommon.AddColumnsForChange(coll, "Handling_Charges_Amount", obj.Handling_Charges_Amount)
            clsCommon.AddColumnsForChange(coll, "Handling_Charges_RO_Amount", obj.Handling_Charges_RO_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Head_Load_RO_Amount", obj.Total_Head_Load_RO_Amount)

            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            isSaved = isSaved AndAlso clsMilkPurchaseInvoiceProvisionDetail.SaveData(obj.DOC_CODE, objList, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetQryPayableMccMilkRegister(ByVal qry As String, ByVal txtFromDate As Date, ByVal txtToDate As Date) As String
        Dim FinalQuery As String = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[Vendor Group Code],aa.[Vendor Group Desc],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.CLR,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount, aa.[VSP Code],ExtrCol.SaleAmt,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,ExtrCol.DeductionAmt, IncetiveAmt,aa.NET_AMOUNT+Round_Off+Handling_Charges_Amount+Head_Load_Amount-isnull(SNF_Ded_Amount,0)-isnull(SaleAmt,0)+VSP_Commission_Amount-VSP_Deduction_Amount+VSP_Day_Wise_Incentive-isnull(DeductionAmt,0)+IncetiveAmt as PayableAmt  from ( "
        FinalQuery += " select xxx.* ,"
        FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
        FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
        FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
        FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
        FinalQuery += " from ("
        FinalQuery += " select xx.*"
        FinalQuery += " from ( "
        FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],MAX(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,MAX (pp.[Vendor Group Code]) as [Vendor Group Code] ,MAX ([Vendor Group Desc]) as [Vendor Group Desc],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
        FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
        FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
        FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
        FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
        FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
        FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
        FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,max([VSP Code]) as [VSP Code],sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,sum(IncetiveAmt) as IncetiveAmt  from ("
        FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine & ""
        FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Vlc Code] "
        FinalQuery += " )as xx" + Environment.NewLine
        FinalQuery += " ) as xxx ) as aa" + Environment.NewLine +
            "left join ( select vendor_code,sum(Total_Amt* case when RI=1 then 1 else 0 end) as SaleAmt,sum(Total_Amt* case when RI=2 then 1 else 0 end) as DeductionAmt from (" + Environment.NewLine +
"select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,1 as RI " + Environment.NewLine +
"from TSPL_SD_SALE_INVOICE_HEAD  " + Environment.NewLine +
"left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     " + Environment.NewLine +
"where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"select  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code , TSPL_VENDOR_INVOICE_DETAIL.Total_Amount as Total_Amount ,2 as RI" + Environment.NewLine +
"from TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  " + Environment.NewLine +
"where  Document_Type='D' and ((TSPL_VENDOR_INVOICE_HEAD.isDeduction='1'  " + Environment.NewLine +
"and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'') or  len(coalesce(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0)   " + Environment.NewLine +
"and TSPL_VENDOR_INVOICE_HEAD.POSTING_DATE  between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm:ss tt") + "'  " + Environment.NewLine +
")x group by vendor_code) as ExtrCol on ExtrCol.vendor_code= aa.[VSP Code]"
        Return FinalQuery
    End Function

    '    Public Shared Function GetQryProData(ByVal DOC_CODE As String, ByVal strVLCCode As String, ByVal strSRN_No As List(Of String), ByVal FromDate As Date, ByVal ToDate As Date) As String
    '        Dim qry As String = "insert into TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS (InvoiceNo,Doc_Date,Shift,VSPAmt,Farmer_Qty,Farmer_FAT_KG,Farmer_SNF_KG,FATLossAmt,SNFLossAmt,FarmerAmt,NetMilkAmt,NoteAmt)" + Environment.NewLine +
    '                    "  select '" + DOC_CODE + "' as InvoiceNo,doc_date,shift,VSPAmt,Farmer_Qty,Farmer_FAT_KG,Farmer_SNF_KG,FATLossAmt,SNFLossAmt,FarmerAmt,NetMilkAmt,VSPAmt-NetMilkAmt as NoteAmt from (
    'select *,case when FarmerAmt=0 then VSPAmt else round(FarmerAmt-(FATLossAmt+SNFLossAmt),0) end as NetMilkAmt	 from (
    'select mcc_Code,VLC_code, doc_date,shift,sum(AMOUNT * case when RI=1 then 1 else 0 end ) as VSPAmt
    ',sum(Qty * case when RI=1 then 0 else 1 end ) as Farmer_Qty
    ',cast( sum(FAT_KG * case when RI=1 then 0 else 1 end ) as decimal(18,2))as Farmer_FAT_KG
    ',cast(sum(SNF_KG * case when RI=1 then 0 else 1 end ) as decimal(18,2)) as Farmer_SNF_KG
    ',(round (case when   (sum( FAT_KG * case when RI=1 then 0 else 1 end )>0 and sum(FAT_KG * case when RI=1 then -1 else 1 end)>0) then ((cast(sum(FAT_KG * case when RI=1 then -1 else 1 end) as decimal(18,2)) * sum( AMOUNT * case when RI=1 then 0 else 1 end ) * 0.52)/cast(sum( FAT_KG * case when RI=1 then 0 else 1 end ) as decimal(18,2))) else 0 end 
    ',0)) as FATLossAmt,
    'round(case when   (sum( SNF_KG * case when RI=1 then 0 else 1 end )>0 and sum(SNF_KG * case when RI=1 then -1 else 1 end)>0) then ((cast(sum(SNF_KG * case when RI=1 then -1 else 1 end) as decimal(18,2)) * sum( AMOUNT * case when RI=1 then 0 else 1 end ) * 0.48)/cast(sum( SNF_KG * case when RI=1 then 0 else 1 end ) as decimal(18,2))) else 0 end,0) as SNFLossAmt 
    ',sum(AMOUNT * case when RI=1 then 0 else 1 end ) as FarmerAmt  from (" + Environment.NewLine +
    '    "select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD. VLC_CODE ,convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT,TSPL_MILK_SRN_DETAIL.Qty,CONVERT(DECIMAL(10,2), ROUND(Qty*FAT_PER/100, 2, 1)) as FAT_KG,CONVERT(DECIMAL(10,2), ROUND(Qty*SNF_PER/100, 2, 1)) as SNF_KG,TSPL_MILK_SRN_DETAIL.AMOUNT,1 as Chk,1 as RI " + Environment.NewLine +
    '    "from TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
    '    "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
    '    "where  TSPL_MILK_SRN_HEAD.VLC_CODE ='" + strVLCCode + "' and  TSPL_MILK_SRN_DETAIL.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and Farmer_Pro_Code is not NULL" + Environment.NewLine +
    '    "union all" + Environment.NewLine +
    '    "select  TSPL_VLC_DATA_UPLOADER.MCC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code,convert(date,File_Date,103) as DOC_DATE ,shift,isnull(TSPL_VLC_DATA_UPLOADER.qty,0) as Qty,((TSPL_VLC_DATA_UPLOADER.qty*TSPL_VLC_DATA_UPLOADER.fat/100 )) as FAT_KG,((TSPL_VLC_DATA_UPLOADER.qty*TSPL_VLC_DATA_UPLOADER.snf/100 ))  as SNF_KG , isnull(TSPL_VLC_DATA_UPLOADER.Amount,0) as AMOUNT,0 as Chk,2 as RI   " + Environment.NewLine +
    '    "from TSPL_VLC_DATA_UPLOADER   " + Environment.NewLine +
    '    "left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE  and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code " + Environment.NewLine +
    '    "left join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MP_CODE  and TSPL_MP_MASTER. VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_CODE  " + Environment.NewLine +
    '    "where 2 = 2  and  convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_VLC_MASTER_HEAD.VLC_Code='" + strVLCCode + "'" + Environment.NewLine +
    '    "union all   " + Environment.NewLine +
    '    "select TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code,convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Document_Date,(case when TSPL_VLC_DATA_UPLOADER_MASTER.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift," + Environment.NewLine +
    '    "TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,(((TSPL_VLC_DATA_UPLOADER_DETAIL.Qty*TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer)/100))  as FAT_KG ,(((TSPL_VLC_DATA_UPLOADER_DETAIL.Qty*TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer)/100)) as SNF_KG  ,  TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as VLC_Amount,0 as Chk,3 as RI  " + Environment.NewLine +
    '    "from TSPL_VLC_DATA_UPLOADER_DETAIL   " + Environment.NewLine +
    '    "inner join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code=TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code  " + Environment.NewLine +
    '    "left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code  " + Environment.NewLine +
    '    "where 2 = 2  and  convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code  IN ('" + strVLCCode + "') " + Environment.NewLine +
    '    ")x group by mcc_Code,VLC_code, doc_date,shift having sum(chk)>0" + Environment.NewLine +
    '    ")xx " + Environment.NewLine +
    '    ")xxx"
    '        Return qry
    '    End Function

    Public Shared Function GetQryProData(ByVal DOC_CODE As String, ByVal strVLCCode As String, ByVal strSRN_No As List(Of String), ByVal FromDate As Date, ByVal ToDate As Date, ByVal SettLocalSaleAllowedPer As Decimal, ByVal SettLocalSaleRate As Decimal, ByVal IsNewFormatApplicable As Boolean) As String
        Dim qry As String = "insert into TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS (InvoiceNo,Doc_Date,Shift,VSPAmt,Farmer_Qty,Farmer_FAT_KG,Farmer_SNF_KG,FATLossAmt,SNFLossAmt,FarmerAmt,NetMilkAmt,NoteAmt,VSP_Qty,VSP_Reject_Qty ,Farmer_Std_Qty,VSP_Std_Qty,Short_Received,Local_Sale_Qty,Local_Sale_Amt)" + Environment.NewLine +
                    "  select '" + DOC_CODE + "' as InvoiceNo,doc_date,shift,VSPAmt,Farmer_Qty,Farmer_FAT_KG,Farmer_SNF_KG,FATLossAmt,SNFLossAmt,FarmerAmt,NetMilkAmt,VSPAmt-NetMilkAmt as NoteAmt,VSP_Qty,VSP_Reject_Qty ,Farmer_Std_Qty,VSP_Std_Qty,Short_Received,case when Short_Received>(Farmer_Qty*" + clsCommon.myCstr(SettLocalSaleAllowedPer) + "/100) then cast(Short_Received-(Farmer_Qty*" + clsCommon.myCstr(SettLocalSaleAllowedPer) + "/100) as decimal(18,2)) else 0 end as Local_Sale_Qty,cast(((case when Short_Received>(Farmer_Qty * " + clsCommon.myCstr(SettLocalSaleAllowedPer) + "/100) then cast(Short_Received-(Farmer_Qty * " + clsCommon.myCstr(SettLocalSaleAllowedPer) + "/100) as decimal(18,2)) else 0 end) *" + clsCommon.myCstr(SettLocalSaleRate) + ")as decimal(18,2)) as Local_Sale_Amt from (
select *,case when FarmerAmt=0 then VSPAmt else round(FarmerAmt "
        If Not IsNewFormatApplicable Then
            qry += " - (FATLossAmt + SNFLossAmt)"
        End If
        qry += ",0) End As NetMilkAmt,(CONVERT(Decimal(10,2),round(Farmer_FAT_KG/6.5*52,2,1))+CONVERT(Decimal(10,2), round(Farmer_SNF_KG/9*48,2,1))) As Farmer_Std_Qty,(CONVERT(Decimal(10,2), round(VSP_FAT_KG/6.5*52,2,1))+CONVERT(Decimal(10,2), round(VSP_SNF_KG/9*48,2,1))) As VSP_Std_Qty,(Farmer_Qty-VSP_Qty-VSP_Reject_Qty) As Short_Received from (
select mcc_Code,VLC_code, doc_date,shift,sum(AMOUNT * case when RI=1 then 1 else 0 end ) as VSPAmt
,cast(sum(Qty * case when RI in (2,3) then 1 else 0 end )as decimal(18,2)) as Farmer_Qty
,cast( sum(FAT_KG * case when RI in (2,3) then 1 else 0 end ) as decimal(18,2))as Farmer_FAT_KG
,cast(sum(SNF_KG * case when RI in (2,3) then 1 else 0 end ) as decimal(18,2)) as Farmer_SNF_KG
,(round (case when   (sum( FAT_KG * case when RI in (2,3) then 1 else 0 end )>0 and sum(FAT_KG * case when RI=1 then -1 else (case when RI in (2,3) then 1 else 0 end) end)>0) then ((cast(sum(FAT_KG * case when RI=1 then -1 else (case when RI in (2,3) then 1 else 0 end) end) as decimal(18,2)) * sum( AMOUNT * case when RI in (2,3) then 1 else 0 end ) * 0.52)/cast(sum( FAT_KG * case when RI in (2,3) then 1 else 0 end ) as decimal(18,2))) else 0 end 
,0)) as FATLossAmt,
round(case when   (sum( SNF_KG * case when RI in (2,3) then 1 else 0 end )>0 and sum(SNF_KG * case when RI=1 then -1 else (case when RI in (2,3) then 1 else 0 end) end)>0) then ((cast(sum(SNF_KG * case when RI=1 then -1 else (case when RI in (2,3) then 1 else 0 end) end) as decimal(18,2)) * sum( AMOUNT * case when RI in (2,3)  then 1 else 0 end ) * 0.48)/cast(sum( SNF_KG * case when RI in (2,3)  then 1 else 0 end ) as decimal(18,2))) else 0 end,0) as SNFLossAmt 
,sum(AMOUNT * case when RI in (2,3)  then 1 else 0 end ) as FarmerAmt  
,cast(sum(Qty * case when RI =1 then 1 else 0 end ) as decimal(18,2)) as VSP_Qty
,cast(sum(Qty * case when RI =4 then 1 else 0 end ) as decimal(18,2)) as VSP_Reject_Qty
,cast( sum(FAT_KG * case when RI =1 then 1 else 0 end ) as decimal(18,2))as VSP_FAT_KG
,cast(sum(SNF_KG * case when RI =1 then 1 else 0 end ) as decimal(18,2)) as VSP_SNF_KG  from (" + Environment.NewLine +
    "select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD. VLC_CODE ,convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT,TSPL_MILK_SRN_DETAIL.Qty,CONVERT(DECIMAL(10,2), ROUND(Qty*FAT_PER/100, 2, 1)) as FAT_KG,CONVERT(DECIMAL(10,2), ROUND(Qty*SNF_PER/100, 2, 1)) as SNF_KG,TSPL_MILK_SRN_DETAIL.AMOUNT,1 as Chk,1 as RI " + Environment.NewLine +
    "from TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
    "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
    "where  TSPL_MILK_SRN_HEAD.VLC_CODE ='" + strVLCCode + "' and  TSPL_MILK_SRN_DETAIL.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and Farmer_Pro_Code is not NULL" + Environment.NewLine +
    "union all" + Environment.NewLine +
    "select  TSPL_VLC_DATA_UPLOADER.MCC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code,convert(date,File_Date,103) as DOC_DATE ,shift,isnull(TSPL_VLC_DATA_UPLOADER.qty,0) as Qty,((TSPL_VLC_DATA_UPLOADER.qty*TSPL_VLC_DATA_UPLOADER.fat/100 )) as FAT_KG,((TSPL_VLC_DATA_UPLOADER.qty*TSPL_VLC_DATA_UPLOADER.snf/100 ))  as SNF_KG , isnull(TSPL_VLC_DATA_UPLOADER.Amount,0) as AMOUNT,0 as Chk,2 as RI   " + Environment.NewLine +
    "from TSPL_VLC_DATA_UPLOADER   " + Environment.NewLine +
    "left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE  and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code " + Environment.NewLine +
    "left join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MP_CODE  and TSPL_MP_MASTER. VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_CODE  " + Environment.NewLine +
    "where 2 = 2  and  convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_VLC_MASTER_HEAD.VLC_Code='" + strVLCCode + "'" + Environment.NewLine +
    "union all   " + Environment.NewLine +
    "select TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code,convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Document_Date,(case when TSPL_VLC_DATA_UPLOADER_MASTER.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift," + Environment.NewLine +
    "TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,(((TSPL_VLC_DATA_UPLOADER_DETAIL.Qty*TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer)/100))  as FAT_KG ,(((TSPL_VLC_DATA_UPLOADER_DETAIL.Qty*TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer)/100)) as SNF_KG  ,  TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as VLC_Amount,0 as Chk,3 as RI  " + Environment.NewLine +
    "from TSPL_VLC_DATA_UPLOADER_DETAIL   " + Environment.NewLine +
    "inner join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code=TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code  " + Environment.NewLine +
    "left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code  " + Environment.NewLine +
    "where 2 = 2  and  convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code  IN ('" + strVLCCode + "') " + Environment.NewLine +
    "union all
select TSPL_MILK_REJECT_HEAD.MCC_CODE,TSPL_MILK_REJECT_DETAIL.VLC_CODE ,convert(date, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG,CONVERT(DECIMAL(10,2), ROUND(ACC_WEIGHT_KG*FAT/100, 2, 1)) as FAT_KG,CONVERT(DECIMAL(10,2), ROUND(ACC_WEIGHT_KG*SNF/100, 2, 1)) as SNF_KG,TSPL_MILK_REJECT_DETAIL.AMOUNT,0 as Chk,4 as RI 
from TSPL_MILK_REJECT_DETAIL 
left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE
where  TSPL_MILK_REJECT_DETAIL.VLC_CODE in ('" + strVLCCode + "') and  convert(date, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "'  " + Environment.NewLine +
    ")x group by mcc_Code,VLC_code, doc_date,shift having sum(chk)>0" + Environment.NewLine +
    ")xx " + Environment.NewLine +
    ")xxx"
        Return qry
    End Function
End Class
Public Class clsMilkPurchaseInvoiceProvisionDetail
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkPurchaseInvoiceMCCDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkPurchaseInvoiceMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SRN_CODE", obj.SRN_CODE)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.AMOUNT)
                clsCommon.AddColumnsForChange(coll, "Cans", IIf(Math.Round(clsCommon.myCdbl(obj.Cans), 0) = 0, 1, Math.Round(clsCommon.myCdbl(obj.Cans), 0)))
                clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                clsCommon.AddColumnsForChange(coll, "COMMISSION", obj.COMMISSION)
                clsCommon.AddColumnsForChange(coll, "Service_Charge", obj.Service_Charge)
                clsCommon.AddColumnsForChange(coll, "payment_commission", obj.Payment_COMMISSION)
                clsCommon.AddColumnsForChange(coll, "Net_Amount", obj.Net_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "Deduction_Amount", obj.Deduction)
                clsCommon.AddColumnsForChange(coll, "Head_Load_Amount", obj.Head_Load_Amount)
                clsCommon.AddColumnsForChange(coll, "Own_Asset_Amount", obj.Own_Asset_Amount)
                clsCommon.AddColumnsForChange(coll, "Correction_Factor", obj.Correction_Factor)
                clsCommon.AddColumnsForChange(coll, "FAT_PER", obj.FAT_PER)
                clsCommon.AddColumnsForChange(coll, "Incentive", obj.Incentive)
                clsCommon.AddColumnsForChange(coll, "IncentiveEMP", obj.IncentiveEMP)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Acc_Qty", obj.Acc_Qty)
                clsCommon.AddColumnsForChange(coll, "RATE", obj.RATE)
                clsCommon.AddColumnsForChange(coll, "SNF_PER", obj.SNF_PER)
                clsCommon.AddColumnsForChange(coll, "Service_Charge_Amount", obj.Service_Charge_Amount)
                clsCommon.AddColumnsForChange(coll, "TOTAL_AMOUNT", obj.TOTAL_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "TOTAL_AMOUNT_Acc", obj.TOTAL_AMOUNT_Acc)
                clsCommon.AddColumnsForChange(coll, "VEHICLE_NO", obj.VEHICLE_NO)
                clsCommon.AddColumnsForChange(coll, "VLC_NO", obj.VLC_NO)
                clsCommon.AddColumnsForChange(coll, "Handling_Charges_Amount", obj.Handling_Charges_Amount)
                clsCommon.AddColumnsForChange(coll, "SRN_Net_Amount", obj.SRN_Net_Amount)
                clsCommon.AddColumnsForChange(coll, "SRN_RO_Amount", obj.SRN_RO_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_PROVISION_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_PURCHASE_INVOICE_PROVISION_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
            Next
        End If

        Return True
    End Function


End Class
