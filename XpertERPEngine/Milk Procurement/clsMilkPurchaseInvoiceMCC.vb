Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class clsMilkPurchaseInvoiceMCC

#Region "Variables"

    Shared sQuery As String = String.Empty
    Public DOC_CODE As String
    Public MCC_CODE As String
    Public Irregular_MCC_CODE As String
    Public DOC_DATE As Date
    Public VENDOR_INVOICE_NO As String
    Public VENDOR_INVOICE_DATE As Date
    Public ROUTE_CODE As String
    Public Description As String
    Public Payment As String
    Public VSP_CODE As String
    Public Amount As Double
    Public Basic_Amount As Double
    Public Commission As Double
    Public Incentive As Double
    Public Incentive_Head As Double
    Public Total_Amount_Acc As Double
    Public Total_PaymentCommission As Double
    Public Total_Head_Load_Amount As Double
    Public Total_Head_Load_RO_Amount As Decimal
    Public RoundOffAmount As Decimal
    Public Total_Own_Asset_Amount As Double
    Public Total_Deduction_Amount As Double

    Public MP_Amount As Decimal
    Public MP_EMP As Double
    Public MP_Incentive As Double
    Public MP_IncentiveEMP As Double

    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public FROM_DATE As Date?
    Public TO_DATE As Date?
    Public Program_Code As String = clsUserMgtCode.MilkVSPIssuePayment
    '' grid columns
    Public Handling_Charges_Per As Decimal
    Public Handling_Charges_Amount As Decimal
    Public Handling_Charges_RO_Amount As Decimal
    Public SRN_Net_Amount As Decimal
    Public SRN_RO_Amount As Decimal
    Public Shared ObjList As List(Of clsMilkPurchaseInvoiceMCCDetail)
    Public objPIRemittance As clsPIRemittance = Nothing

    Public No_Of_Asset As Integer

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkPurchaseInvoiceMCC
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MILK_PURCHASE_INVOICE_HEAD", "DOC_CODE", "TSPL_MILK_PURCHASE_INVOICE_DETAIL", "DOC_CODE", trans)
            Dim qry As String
            qry = "delete from TSPL_MILK_PURCHASE_INVOICE_DETAIL where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_PURCHASE_INVOICE_HEAD where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkPurchaseInvoiceMCC

        Dim obj As New clsMilkPurchaseInvoiceMCC()
        Dim objtr As New clsMilkPurchaseInvoiceMCCDetail

        ObjList = New List(Of clsMilkPurchaseInvoiceMCCDetail)

        Dim qry As String = "SELECT   TSPL_MILK_PURCHASE_INVOICE_HEAD.RoundOffAmount,TSPL_MILK_PURCHASE_INVOICE_HEAD.SRN_Net_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.SRN_RO_Amount, TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Per,TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount, TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_RO_Amount, TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Head_Load_RO_Amount ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.Irregular_MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE," _
        & " SHIFT,TSPL_MCC_MASTER.MCC_NAME,Description,Payment,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Commission,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_PaymentCommission,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Amount_Acc,  TSPL_MILK_PURCHASE_INVOICE_HEAD.Created_By,TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE," _
        & " TSPL_MILK_SRN_Head.ROUTE_CODE,VENDOR_INVOICE_NO,VENDOR_INVOICE_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.POSTED,TSPL_MILK_PURCHASE_INVOICE_HEAD.POSTING_DATE " _
        & " ,Total_Deduction_Amount,Total_Own_Asset_Amount,Total_Head_Load_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.Incentive_Head,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_Amount, " _
        & " TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_EMP,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_Incentive,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_IncentiveEMP,TSPL_MILK_PURCHASE_INVOICE_HEAD.FROM_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.TO_DATE  FROM TSPL_MILK_PURCHASE_INVOICE_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE inner join " _
        & " TSPL_MILK_PURCHASE_INVOICE_DETAIL pid on pid.DOC_CODE= TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE Inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=SRN_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_PURCHASE_INVOICE_HEAD)"
            Case NavigatorType.Last
                qry += " AND TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_PURCHASE_INVOICE_HEAD)"
            Case NavigatorType.Next
                qry += " AND TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_PURCHASE_INVOICE_HEAD where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_PURCHASE_INVOICE_HEAD where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.DOC_CODE = dt.Rows(0)("DOC_CODE")
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.Irregular_MCC_CODE = clsCommon.myCstr(dt.Rows(0)("Irregular_MCC_CODE"))
            obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy")
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Payment = clsCommon.myCstr(dt.Rows(0)("Payment"))
            obj.Amount = clsCommon.myCstr(dt.Rows(0)("Total_Amount"))
            obj.Commission = clsCommon.myCstr(dt.Rows(0)("Total_Commission"))
            obj.Total_PaymentCommission = clsCommon.myCstr(dt.Rows(0)("Total_PaymentCommission"))
            obj.Total_Deduction_Amount = clsCommon.myCstr(dt.Rows(0)("Total_Deduction_Amount"))
            obj.Total_Head_Load_Amount = clsCommon.myCstr(dt.Rows(0)("Total_Head_Load_Amount"))
            obj.Total_Own_Asset_Amount = clsCommon.myCstr(dt.Rows(0)("Total_Own_Asset_Amount"))
            obj.Total_Amount_Acc = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Total_Amount_Acc")), 2, MidpointRounding.AwayFromZero)
            obj.ROUTE_CODE = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.VENDOR_INVOICE_DATE = clsCommon.myCDate(dt.Rows(0)("Vendor_Invoice_Date"))
            obj.VENDOR_INVOICE_NO = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.VSP_CODE = clsCommon.myCstr(dt.Rows(0)("VSP_Code"))

            obj.MP_Amount = clsCommon.myCdbl(dt.Rows(0)("MP_Amount"))
            obj.MP_EMP = clsCommon.myCdbl(dt.Rows(0)("MP_EMP"))
            obj.MP_Incentive = clsCommon.myCdbl(dt.Rows(0)("MP_Incentive"))
            obj.MP_IncentiveEMP = clsCommon.myCdbl(dt.Rows(0)("MP_IncentiveEMP"))
            If IsDBNull(dt.Rows(0)("FROM_DATE")) = False Then
                obj.FROM_DATE = dt.Rows(0)("FROM_DATE")
            End If
            If IsDBNull(dt.Rows(0)("TO_DATE")) = False Then
                obj.TO_DATE = dt.Rows(0)("TO_DATE")
            End If


            obj.SRN_Net_Amount = clsCommon.myCdbl(dt.Rows(0)("SRN_Net_Amount"))
            obj.SRN_RO_Amount = clsCommon.myCdbl(dt.Rows(0)("SRN_RO_Amount"))

            obj.Incentive_Head = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Incentive_Head")), 2, MidpointRounding.AwayFromZero)
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Handling_Charges_Per = clsCommon.myCdbl(dt.Rows(0)("Handling_Charges_Per"))
            obj.Handling_Charges_Amount = clsCommon.myCdbl(dt.Rows(0)("Handling_Charges_Amount"))
            obj.Handling_Charges_RO_Amount = clsCommon.myCdbl(dt.Rows(0)("Handling_Charges_RO_Amount"))
            obj.Total_Head_Load_RO_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Head_Load_RO_Amount"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            strCode = dt.Rows(0)("DOC_CODE")
            strCode = obj.DOC_CODE
            obj.objPIRemittance = clsPIRemittance.GetData(obj.DOC_CODE, trans)
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = "    SELECT TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount, sd.VLC_DOC_CODE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT,Cans,TSPL_MILK_PURCHASE_INVOICE_DETAIL.CLR,COMMISSION,Service_Charge,payment_commission,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Net_Amount,Deduction_Amount,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Own_Asset_Amount,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Head_Load_Amount,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Correction_Factor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER,Incentive,IncentiveEMP,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code,item_desc,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.ACC_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER,SRN_CODE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.TOTAL_AMOUNT ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.TOTAL_AMOUNT_ACC ,sd.VEHICLE_CODE as VEHICLE_NO, sd.DOC_DATE as SRN_Date,sde.UOM_Code as UOM,TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  as VLC_NO,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Service_Charge_Amount FROM TSPL_MILK_PURCHASE_INVOICE_DETAIL " + Environment.NewLine +
        " inner join TSPL_MILK_PURCHASE_INVOICE_HEAD  pih on TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE=pih.DOC_CODE  " + Environment.NewLine +
        " inner join TSPL_MILK_SRN_HEAD sd on sd.DOC_CODE=SRN_CODE " + Environment.NewLine +
        " inner join TSPL_MILK_SRN_DETAIL sde on sde.DOC_CODE= sd.DOC_CODE " + Environment.NewLine +
        " inner join  TSPL_ITEM_MASTER itm on TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code=itm.Item_Code " + Environment.NewLine +
        " WHERE 2=2  AND TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE = '" + strCode + "' ORDER BY sd.VLC_DOC_CODE"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        'trans.Commit()
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkPurchaseInvoiceMCCDetail
                objtr.DOC_CODE = strCode
                objtr.AMOUNT = clsCommon.myCstr(dr("AMOUNT"))
                objtr.Cans = clsCommon.myCstr(dr("Cans"))
                objtr.CLR = clsCommon.myCstr(dr("CLR"))
                objtr.Service_Charge = clsCommon.myCstr(dr("Service_Charge"))
                objtr.COMMISSION = clsCommon.myCdbl(dr("COMMISSION"))
                objtr.Payment_COMMISSION = clsCommon.myCdbl(dr("payment_commission"))
                objtr.Net_AMOUNT = clsCommon.myCdbl(dr("Net_Amount"))
                objtr.Service_Charge_Amount = clsCommon.myCdbl(dr("Service_Charge_Amount"))
                objtr.Deduction = clsCommon.myCdbl(dr("Deduction_Amount"))
                objtr.Own_Asset_Amount = clsCommon.myCdbl(dr("Own_Asset_Amount"))
                objtr.Head_Load_Amount = clsCommon.myCdbl(dr("Head_Load_Amount"))
                objtr.Correction_Factor = clsCommon.myCstr(dr("Correction_Factor"))
                objtr.FAT_PER = clsCommon.myCstr(dr("FAT_PER"))
                objtr.Incentive = clsCommon.myCdbl(dr("Incentive"))
                objtr.IncentiveEMP = clsCommon.myCdbl(dr("IncentiveEMP"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                'objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))
                objtr.Qty = clsCommon.myCstr(dr("Qty"))
                objtr.Acc_Qty = clsCommon.myCstr(dr("Acc_Qty"))
                'objtr.Acc_Qty_Ltr = clsCommon.myCstr(dr("Acc_Weight_Ltr"))
                objtr.RATE = clsCommon.myCstr(dr("RATE"))
                objtr.SNF_PER = clsCommon.myCdbl(dr("SNF_PER"))
                objtr.SRN_CODE = clsCommon.myCstr(dr("SRN_CODE"))
                objtr.TOTAL_AMOUNT = clsCommon.myCstr(dr("TOTAL_AMOUNT"))
                objtr.TOTAL_AMOUNT_Acc = clsCommon.myCstr(dr("TOTAL_AMOUNT_Acc"))
                objtr.VEHICLE_NO = clsCommon.myCstr(dr("VEHICLE_NO"))
                objtr.SRN_DATE = clsCommon.myCstr(dr("SRN_DATE"))
                objtr.Item_Desc = clsCommon.myCstr(dr("ITEM_DESC"))
                objtr.UOM = clsCommon.myCstr(dr("UOM"))
                objtr.VLC_NO = clsCommon.myCstr(dr("VLC_NO"))
                objtr.Handling_Charges_Amount = clsCommon.myCdbl(dr("Handling_Charges_Amount"))

                objtr.SRN_Net_Amount = clsCommon.myCdbl(dr("SRN_Net_Amount"))
                objtr.SRN_RO_Amount = clsCommon.myCdbl(dr("SRN_RO_Amount"))

                ObjList.Add(objtr)
            Next
        End If

        clsMilkPurchaseInvoiceMCC.ObjList = ObjList
        Return obj
    End Function

    Public Shared Function OpenPriceChart(ByVal isProvision As Boolean, ByVal SRN_Code As String, ByVal Incentive_rate As Double, ByVal trans As SqlTransaction, ByVal Incentive_UOM As String) As Double
        Dim TabMilkPIHead As String = "TSPL_MILK_PURCHASE_INVOICE_HEAD"
        Dim TabMilkPIDetail As String = "TSPL_MILK_PURCHASE_INVOICE_DETAIL"
        If isProvision Then
            TabMilkPIHead = "TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD"
            TabMilkPIDetail = "TSPL_MILK_PURCHASE_INVOICE_PROVISION_DETAIL"
        End If
        Dim DtSRN As DataTable
        Dim FatW As Double = 0
        Dim SNfW As Double = 0
        Dim FATRate As Double = 0
        Dim SNFRate As Double = 0
        Dim FATValue As Double = 0
        Dim SNfValue As Double = 0
        Dim FATRatio As Double = 0
        Dim SNFRatio As Double = 0
        Dim StdRate As Double = 0
        Dim fatKG As Double = 0
        Dim snfKG As Double = 0
        Dim strQry As String = String.Empty
        Dim whrcls As String = " "
        Dim CalculateIncentiveOnStdQty As Boolean = False '' get value from setting
        Dim conv_fac As Decimal = 0
        conv_fac = clsWeightConversionInfo.GetConversion_factor("LTR", "KG", trans)
        If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
            If clsCommon.myLen(objCommonVar.DefaultMilkItemCode) <= 0 Then
                Throw New Exception("Please first define the MCC Default Milk item in milk setting")
            End If
            whrcls = " and TSPL_WEIGHT_CONVERSION.Structure_Code in ('" + clsItemMaster.GetItemStructureCode(objCommonVar.DefaultMilkItemCode, trans) + "') "
        End If

        strQry = "(select SUM(NewQty ) as NewQty,SUM(SNFQTY ) as SNF_KG,SUM(FATQTY ) as FAT_KG,(SUM(FATQTY )/SUM(NewQty ))*100 as FAT_Per,(SUM(SNFQTY )/SUM(NewQty ))*100 " _
           & " as SNF_Per,SUM(Amount )/SUM(NewQty ) as Rate,SUM(Amount ) as Amount,doc_code,srn_code,FAT_Pers,Fat_ratio,SNF_Pers,SNF_Ratio,Milk_Rate,((SUM(FATQTY)/FAT_Pers) * (Fat_ratio/100.00)+(SUM(SNFQTY )/SNF_Pers) * (SNF_Ratio/100.00))*100 as STDQty from (" _
           & " select DOC_DATE,UOM_Code,FATQTY*CF as FATQTY,SNFQTY*CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF,RATE ,cf*Net_AMOUNT as Amount ," _
           & " MCC_CODE  ,VSP_CODE ,SHIFT,ROUTE_CODE ,Vendor_Name ,DOC_CODE,srn_code,Price_Code  from (select " + TabMilkPIHead + ".DOC_CODE," _
           & " srn_code,Price_Code,TSPL_MILK_RECEIPT_DETAIL.UOM_Code," + TabMilkPIDetail + ".Qty  ," + TabMilkPIDetail + ".FAT_PER " _
           & " ,(" + TabMilkPIDetail + ".FAT_PER*" + TabMilkPIDetail + ".Qty/100) as FATQTY," + TabMilkPIDetail + ".SNF_PER," _
            & " (" + TabMilkPIDetail + ".SNF_PER *" + TabMilkPIDetail + ".Qty/100) as SNFQTY  ," + TabMilkPIDetail + ".RATE " _
            & " as RATE," + TabMilkPIDetail + ".Net_AMOUNT, " + TabMilkPIHead + ".MCC_CODE , TSPL_MILK_receipt_HEAD.DOC_DATE," _
            & " " + TabMilkPIHead + ".VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT, " + TabMilkPIHead + ".ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name " _
            & " from " + TabMilkPIDetail + " Inner Join " + TabMilkPIHead + " On " + TabMilkPIHead + ".DOC_CODE =" + TabMilkPIDetail + ".DOC_CODE " _
            & " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =" + TabMilkPIDetail + ".SRN_CODE " _
            & " left outer join TSPL_MILK_SRN_detail  on TSPL_MILK_SRN_detail .DOC_CODE  =TSPL_MILK_SRN_HEAD .DOC_CODE " _
            & " left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE " _
            & " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE  " _
            & " left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and " _
               & " TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE and TSPL_MILK_SRN_HEAD.sample_No = TSPL_MILK_RECEIPT_DETAIL.sample_No Left Outer Join TSPL_VENDOR_MASTER On " _
               & " " + TabMilkPIHead + ".VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP' " _
             & " where 2=2   and TSPL_MILK_SRN_HEAD. DOC_CODE='" & SRN_Code & "' ) xx   left outer join (Select Distinct yyy.* From (  " _
             & " Select Container_UOM as FromUOM, Contained_UOM as TOUOM,Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION Where 2=2 " + whrcls _
             & " UNION All  Select Contained_UOM as FromUOM, Container_UOM as TOUOM,Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION Where 2=2  " + whrcls _
             & " UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  Where 2=2  " + whrcls _
             & " UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION Where 2=2 " + whrcls + " )" _
             & " yyy) zzz on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='KG' ) ttt inner join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio," _
             & " Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER " _
             & " on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) price_chart on price_chart.Code=ttt.Price_Code group by MCC_CODE," _
             & " VSP_CODE,DOC_CODE,srn_code,FAT_Pers,Fat_ratio,SNF_Pers,SNF_Ratio,Milk_Rate  )"
        DtSRN = clsDBFuncationality.GetDataTable(strQry, trans)
        If DtSRN.Rows.Count > 0 Then
            FatW = clsCommon.myCdbl(DtSRN.Rows(0)("FAT_ratio"))
            SNfW = clsCommon.myCdbl(DtSRN.Rows(0)("SNF_ratio"))
            fatKG = clsCommon.myCdbl(DtSRN.Rows(0)("FAT_KG"))
            snfKG = clsCommon.myCdbl(DtSRN.Rows(0)("SNF_KG"))
            If clsCommon.CompairString(Incentive_UOM, "LTR") = CompairStringResult.Equal Then
                fatKG = Math.Truncate(fatKG / conv_fac * 1000) / 1000
                snfKG = Math.Truncate(snfKG / conv_fac * 1000) / 1000
            End If
            FATRatio = clsCommon.myCdbl(DtSRN.Rows(0)("FAT_Pers"))
            SNFRatio = clsCommon.myCdbl(DtSRN.Rows(0)("SNF_Pers"))
            FATRate = Math.Round(clsCommon.myCdbl(Incentive_rate) * FatW / FATRatio, 2)
            SNFRate = Math.Round(clsCommon.myCdbl(Incentive_rate) * SNfW / SNFRatio, 2)
            FATValue = Math.Round(fatKG * FATRate, 2)
            SNfValue = Math.Round(snfKG * SNFRate, 2)
            Return FATValue + SNfValue
        End If
        Return 0
    End Function
    Public Shared Function OpenPriceChartMP(ByVal Inv_Code As String, ByVal SRN_Code As String, ByVal Incentive_rate As Double, ByVal trans As SqlTransaction, ByVal Incentive_UOM As String) As Double
        Dim DtSRN As DataTable
        Dim FatW As Double = 0
        Dim SNfW As Double = 0
        Dim FATRate As Double = 0
        Dim SNFRate As Double = 0
        Dim FATValue As Double = 0
        Dim SNfValue As Double = 0
        Dim FATRatio As Double = 0
        Dim SNFRatio As Double = 0
        Dim StdRate As Double = 0
        Dim fatKG As Double = 0
        Dim snfKG As Double = 0
        Dim strQry As String = String.Empty
        Dim whrcls As String = String.Empty
        Dim CalculateIncentiveOnStdQty As Boolean = False '' get value from setting
        Dim conv_fac As Decimal = 0
        conv_fac = clsWeightConversionInfo.GetConversion_factor("LTR", "KG", trans)
        If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
            If clsCommon.myLen(objCommonVar.DefaultMilkItemCode) <= 0 Then
                Throw New Exception("Please first define the MCC Default Milk item in milk setting")
            End If
            whrcls = " and TSPL_WEIGHT_CONVERSION.Structure_Code in ('" + clsItemMaster.GetItemStructureCode(objCommonVar.DefaultMilkItemCode, trans) + "') "
        End If

        'Dim dt As DataTable
        Dim QryMP As String = " select ttt.MCC_Code,ttt.VSP_CODE,ttt.VLC_CODE,ttt.DOC_CODE,ttt.SRN_CODE,ttt.DOC_DATE,ttt.NewQty,ttt.SNF_KG,ttt.FAT_KG,ttt.FAT_Per,ttt.SNF_Per,ttt.Rate,ttt.Amount " & _
                              " from ( select SUM(NewQty ) as NewQty,SUM(SNFQTY ) as SNF_KG,SUM(FATQTY ) as FAT_KG,(SUM(FATQTY )/SUM(NewQty ))*100 as FAT_Per," & _
                              " (SUM(SNFQTY )/SUM(NewQty ))*100  as SNF_Per,SUM(Amount )/SUM(NewQty ) as Rate,SUM(Amount ) as Amount,DOC_CODE,DOC_DATE,srn_code,MCC_CODE,VSP_CODE,VLC_CODE  " & _
                              " from ( select DOC_DATE,UOM_Code,FATQTY*CF as FATQTY,SNFQTY*CF as SNFQTY,MP_Qty*CF as NewQty, MP_Qty,FromUOM,TOUOM,CF,RATE ,cf*Net_AMOUNT as Amount ," & _
                              " MCC_CODE  ,VSP_CODE ,SHIFT,ROUTE_CODE ,Vendor_Name ,DOC_CODE,srn_code,Price_Code,VLC_CODE  from " & _
                              " (select coalesce(MPColl.Manual_Doc_No,PD_Doc_No) as SRN_Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,(select max(Price_Code) as Price_Code " & _
                              " from TSPL_MILK_PRICE_MASTER) as Price_Code,MPColl.UOM_Code,MPColl.MP_Qty  ,MPColl.FAT_PER  ,(MPColl.FAT_PER*MP_Qty/100) as FATQTY,MPColl.SNF_PER," & _
                              " (MPColl.SNF_PER *MPColl.MP_Qty/100) as SNFQTY  ,(case when MPColl.MP_Qty<=0 then 0 else  MPColl.MP_AMOUNT/MPColl.MP_Qty end) as RATE,MPColl.Net_AMOUNT," & _
                              " TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , MPColl.FILE_DATE as DOC_DATE, TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_CODE ," & _
                              " MPColl.SHIFT, TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE,TSPL_VENDOR_MASTER.Vendor_Name  " & _
                              " from TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY MPColl " & _
                              " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =MPColl.DOC_CODE " & _
                              " LEFT JOIN TSPL_VLC_MASTER_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE=TSPL_VLC_MASTER_HEAD.VSP_CODE " & _
                              " Left Outer Join TSPL_VENDOR_MASTER On  TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code " & _
                              " And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  and coalesce(MPColl.Manual_Doc_No,PD_Doc_No)='" & SRN_Code & "' ) xx   " & _
                              " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM,  Container_Qty*Contained_Qty as CF " & _
                              " from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF " & _
                              " from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1  as CF " & _
                              " from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " ) yyy) zzz " & _
                              " on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='KG' ) ttt group by  DOC_CODE,DOC_DATE,srn_code,MCC_CODE,VSP_CODE,VLC_CODE,MCC_CODE  ,VSP_CODE ,SHIFT,ROUTE_CODE ,Vendor_Name ,DOC_CODE,srn_code,Price_Code,VLC_CODE) as ttt where ttt.DOC_CODE='" & Inv_Code & "' and ttt.SRN_Code='" & SRN_Code & "' "

        DtSRN = clsDBFuncationality.GetDataTable(QryMP, trans)
        If DtSRN.Rows.Count > 0 Then
            Dim dtPrice As New DataTable
            QryMP = "select Price.Price_Code,price_chart.FAT_Pers,price_chart.SNF_Pers,price_chart.Fat_ratio,price_chart.SNF_Ratio, price_chart.Milk_Rate from (select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code as Price_Code from TSPL_FAT_SNF_UPLOADER_MASTER " & _
                              " inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & clsCommon.myCstr(DtSRN.Rows(0).Item("MCC_Code")) & "' and " & _
                              " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code='" & clsCommon.myCstr(DtSRN.Rows(0).Item("VLC_Code")) & "' and " & _
                              " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code where  posted='1' and  fat='" & Math.Round(clsCommon.myCdbl(DtSRN.Rows(0).Item("FAT_Per")), 1) & "' and SNF='" & Math.Round(clsCommon.myCdbl(DtSRN.Rows(0).Item("SNF_Per")), 1) & "' " & _
                              " and (date< '" & clsCommon.GetPrintDate(DtSRN.Rows(0).Item("DOC_Date"), "dd-MMM-yyyy") & "' or (date= '" & clsCommon.GetPrintDate(DtSRN.Rows(0).Item("DOC_Date"), "dd-MMM-yyyy") & "')) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) as Price " & _
                              " inner join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate," & _
                              " TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER " & _
                              " inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) price_chart on price_chart.Code=Price.Price_Code"
            dtPrice = clsDBFuncationality.GetDataTable(QryMP, trans)
            If dtPrice.Rows.Count <= 0 Then
                Return 0
            End If
            FatW = clsCommon.myCdbl(dtPrice.Rows(0)("FAT_ratio"))
            SNfW = clsCommon.myCdbl(dtPrice.Rows(0)("SNF_ratio"))
            fatKG = clsCommon.myCdbl(DtSRN.Rows(0)("FAT_KG"))
            snfKG = clsCommon.myCdbl(DtSRN.Rows(0)("SNF_KG"))
            If clsCommon.CompairString(Incentive_UOM, "LTR") = CompairStringResult.Equal Then
                fatKG = Math.Truncate(fatKG / conv_fac * 1000) / 1000
                snfKG = Math.Truncate(snfKG / conv_fac * 1000) / 1000
            End If
            FATRatio = clsCommon.myCdbl(dtPrice.Rows(0)("FAT_Pers"))
            SNFRatio = clsCommon.myCdbl(dtPrice.Rows(0)("SNF_Pers"))
            FATRate = Math.Round(clsCommon.myCdbl(Incentive_rate) * FatW / FATRatio, 2)
            SNFRate = Math.Round(clsCommon.myCdbl(Incentive_rate) * SNfW / SNFRatio, 2)
            FATValue = Math.Round(fatKG * FATRate, 2)
            SNfValue = Math.Round(snfKG * SNFRate, 2)
            Return FATValue + SNfValue
        End If
        Return 0
    End Function

    Public Shared Function GetIncentive(ByVal MCC_Code As String, ByVal Vspcode As String, ByVal Incentive_Code As String, ByVal trans As SqlTransaction) As DataTable
        sQuery = "select TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,TSPL_INCENTIVE_MASTER_HEAD.End_Date,SCHEME_FOR,Calc_Type,rate_type,TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE,SLAB_FROM" _
         & " ,SLAB_TO,TS_From,Ts_To,RATE,RATE_UOM,Starting_Shift,ending_shift,TSPL_INCENTIVE_MASTER_HEAD.Qty_Type ,TSPL_INCENTIVE_DETAIL.FAT_FROM,TSPL_INCENTIVE_DETAIL.FAT_TO,TSPL_INCENTIVE_DETAIL.SNF_FROM,TSPL_INCENTIVE_DETAIL.SNF_TO,TSPL_INCENTIVE_DETAIL.Type from (SELECT TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Form_Type,TSPL_VENDOR_MASTER.Apply_Mult_Incentive," _
         & " CASE WHEN TSPL_VENDOR_MASTER.Apply_Mult_Incentive=1 THEN TSPL_VSP_INCENTIVE.INCENTIVE_CODE ELSE TSPL_VENDOR_MASTER.incentive END AS Incentive " _
         & " FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VSP_INCENTIVE ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE.VENDOR_CODE where coalesce(TSPL_VSP_INCENTIVE.INCENTIVE_CODE,TSPL_VENDOR_MASTER.incentive)='" & Incentive_Code & "') AS TSPL_VENDOR_MASTER " _
         & " inner join TSPL_INCENTIVE_MASTER_HEAD on incentive=TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE " _
         & " inner join TSPL_INCENTIVE_DETAIL on  Form_Type='VSP' and TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE and " _
         & " vendor_code='" & Vspcode & "' inner join tspl_Mcc_Uom_Detail on tspl_Mcc_Uom_Detail.mcc_COde='" & MCC_Code & "'  "
        ''Balwinder on 28/04/2020
        If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            sQuery += " and tspl_Mcc_Uom_Detail.uom_Code=Rate_Uom "
        End If
        sQuery += "order by TSPL_INCENTIVE_DETAIL.incentive_code,TSPL_INCENTIVE_DETAIL.slab_From desc"
        Return clsDBFuncationality.GetDataTable(sQuery, trans)

    End Function

    Public Shared Function GetMPIncentive(ByVal MCC_Code As String, ByVal MPcode As String, ByVal Incentive_Code As String, ByVal trans As SqlTransaction) As DataTable
        sQuery = "select TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,TSPL_INCENTIVE_MASTER_HEAD.End_Date,SCHEME_FOR,Calc_Type,rate_type,TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE,SLAB_FROM" _
         & " ,SLAB_TO,TS_From,Ts_To,RATE,RATE_UOM,Starting_Shift,ending_shift,TSPL_INCENTIVE_MASTER_HEAD.Qty_Type from TSPL_MP_INCENTIVE  " + Environment.NewLine + _
"inner join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_MP_INCENTIVE.INCENTIVE_CODE  " + Environment.NewLine + _
"inner join TSPL_INCENTIVE_DETAIL on  TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE = TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE " + Environment.NewLine + _
          " inner join tspl_Mcc_Uom_Detail on tspl_Mcc_Uom_Detail.mcc_COde='" & MCC_Code & "' and " _
         & " tspl_Mcc_Uom_Detail.uom_Code=Rate_Uom where tspl_MP_incentive.mp_code='" & MPcode & "' order by TSPL_INCENTIVE_DETAIL.incentive_code,TSPL_INCENTIVE_DETAIL.slab_From desc"
        Dim DtIncentive As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
        Return DtIncentive
    End Function

    Public Shared Function GetVSPIncentiveMaster(ByVal MCC_Code As String, ByVal Vspcode As String, ByVal trans As SqlTransaction) As DataTable
        sQuery = "select TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,TSPL_INCENTIVE_MASTER_HEAD.End_Date,SCHEME_FOR,Calc_Type,rate_type,TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE " _
         & " ,Starting_Shift,ending_shift,Qty_Type from (SELECT TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Form_Type,TSPL_VENDOR_MASTER.Apply_Mult_Incentive," _
         & " CASE WHEN TSPL_VENDOR_MASTER.Apply_Mult_Incentive=1 THEN TSPL_VSP_INCENTIVE.INCENTIVE_CODE ELSE TSPL_VENDOR_MASTER.incentive END AS Incentive " _
         & " FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VSP_INCENTIVE ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE.VENDOR_CODE) AS TSPL_VENDOR_MASTER " _
         & " inner join TSPL_INCENTIVE_MASTER_HEAD on incentive=TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE " _
         & " where TSPL_VENDOR_MASTER.VENDOR_CODE='" & Vspcode & "'"

        Dim DtIncentive As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
        Return DtIncentive
    End Function


    Public Shared Sub Calculate_Incentive(ByRef dtAlldata As DataTable, ByRef dtprevdata As DataTable, ByRef rowincentive As DataRow, ByVal trans As SqlTransaction)
        Calculate_Incentive(False, dtAlldata, dtprevdata, rowincentive, trans)
    End Sub
    Public Shared Sub Calculate_Incentive(ByVal isProvision As Boolean, ByRef dtAlldata As DataTable, ByRef dtprevdata As DataTable, ByRef rowincentive As DataRow, ByVal trans As SqlTransaction)
        If dtAlldata IsNot Nothing AndAlso dtAlldata.Rows.Count > 0 Then
            For Each row As DataRow In dtAlldata.Rows
                row.Item("Incentive_value") = OpenPriceChart(isProvision, clsCommon.myCstr(row.Item("code")), clsCommon.myCdbl(rowincentive("rate")), trans, clsCommon.myCstr(rowincentive("RATE_UOM")))
            Next
        End If
        If dtprevdata IsNot Nothing AndAlso dtprevdata.Rows.Count > 0 Then
            For Each row As DataRow In dtprevdata.Rows
                row.Item("Incentive_value") = OpenPriceChart(isProvision, clsCommon.myCstr(row.Item("code")), clsCommon.myCdbl(rowincentive("rate")), trans, clsCommon.myCstr(rowincentive("RATE_UOM")))
            Next
        End If
    End Sub
    Public Shared Sub Calculate_Incentive2(ByRef dtAlldata As DataTable, ByRef dtprevdata As DataTable, ByVal Incentive_Value As Decimal, ByVal Inv_Rate As Decimal, ByVal trans As SqlTransaction)
        Calculate_Incentive2(dtAlldata, dtprevdata, Incentive_Value, Inv_Rate, trans, "")
    End Sub

    Public Shared Sub Calculate_Incentive2(ByRef dtAlldata As DataTable, ByRef dtprevdata As DataTable, ByVal Incentive_Value As Decimal, ByVal Inv_Rate As Decimal, ByVal trans As SqlTransaction, ByVal strQtyColumn As String)
        If clsCommon.myLen(strQtyColumn) <= 0 Then
            strQtyColumn = "POQty"
        End If
        If dtAlldata IsNot Nothing AndAlso dtAlldata.Rows.Count > 0 Then
            For Each row As DataRow In dtAlldata.Rows
                row.Item("Incentive_value") = Inv_Rate * clsCommon.myCdbl(row.Item(strQtyColumn))
            Next
        End If
        If dtprevdata IsNot Nothing AndAlso dtprevdata.Rows.Count > 0 Then
            For Each row As DataRow In dtprevdata.Rows
                row.Item("Incentive_value") = Inv_Rate * clsCommon.myCdbl(row.Item(strQtyColumn))
            Next
        End If
    End Sub
    Public Shared Sub Calculate_Incentive_MP(ByVal Inv_Code As String, ByRef dtAlldata As DataTable, ByRef dtprevdata As DataTable, ByRef rowincentive As DataRow, ByVal trans As SqlTransaction)
        For Each row As DataRow In dtAlldata.Rows
            row.Item("Incentive_value") = OpenPriceChartMP(Inv_Code, clsCommon.myCstr(row.Item("code")), clsCommon.myCdbl(rowincentive("rate")), trans, clsCommon.myCstr(rowincentive("RATE_UOM")))
        Next
        For Each row As DataRow In dtprevdata.Rows
            row.Item("Incentive_value") = OpenPriceChartMP(Inv_Code, clsCommon.myCstr(row.Item("code")), clsCommon.myCdbl(rowincentive("rate")), trans, clsCommon.myCstr(rowincentive("RATE_UOM")))
        Next
    End Sub

    Public Shared Function Calculate_Quality_Incentive_Amount(ByVal dtAllData As DataTable, ByVal drSRN As DataRow, ByVal Incentive_Code As String, ByVal trans As SqlTransaction) As ArrayList
        Dim Fat_Per As Decimal = 0
        Dim SNF_Per As Decimal = 0
        Dim CLR As Decimal = 0
        Dim FAT_KG As Decimal = 0
        Dim SNF_KG As Decimal = 0
        Dim Qty As Decimal = 0
        Dim BaseQty As Decimal = 0
        Dim IncentiveAmount As Decimal = 0
        Dim conv_fac As Decimal = 0
        conv_fac = clsWeightConversionInfo.GetConversion_factor("LTR", "KG", trans)
        Dim arr As New ArrayList
        arr.Add(0)
        arr.Add(0)
        '' assign values to variables
        If clsCommon.myLen(drSRN.Item("code")) Then
            Fat_Per = clsCommon.myCdbl(drSRN.Item("Fat_Per"))
            SNF_Per = clsCommon.myCdbl(drSRN.Item("SNF_Per"))
            CLR = clsCommon.myCdbl(drSRN.Item("CLR"))
            FAT_KG = clsCommon.myCdbl(drSRN.Item("FAT_KG"))
            SNF_KG = clsCommon.myCdbl(drSRN.Item("SNF_KG"))
            Qty = clsCommon.myCdbl(drSRN.Item("POQty"))
            BaseQty = Qty
        Else
            Return arr
        End If

        Dim qry As String = ""
        Dim dtIncentive As DataTable
        qry = " select INCENTIVE_CODE,INCENTIVE_TYPE,LINE_NO,SLAB_FROM,SLAB_TO,RATE,RATE_UOM,FOR_PERIOD,Parameter_Type,OPERATER_TYPE,Param_Seq,OP_Seq,Rate_Type from ( " & _
              " select Detail.INCENTIVE_CODE,Detail.INCENTIVE_TYPE,Detail.LINE_NO,Detail.SLAB_FROM,Detail.SLAB_TO,Detail.RATE,Detail.RATE_UOM," & _
              " Detail.FOR_PERIOD,Detail.TS_FROM,Detail.TS_TO,Detail.Parameter_Type,Detail.OPERATER_TYPE, " & _
              " (case when Detail.Parameter_Type='FAT' then 1 when Detail.Parameter_Type='SNF' then 2 when Detail.Parameter_Type='CLR' then 3 end) as Param_Seq, " & _
              " (case when Detail.OPERATER_TYPE='None' then 0 when Detail.OPERATER_TYPE in ('OR','XOR') then 1 when Detail.OPERATER_TYPE='AND' then 2 " & _
              " when Detail.OPERATER_TYPE='Continue' then 3 end) as OP_Seq,Head.Rate_Type " & _
              " from TSPL_INCENTIVE_MASTER_HEAD Head inner join TSPL_INCENTIVE_DETAIL Detail on Head.INCENTIVE_CODE=Detail.INCENTIVE_CODE " & _
              " where Head.INCENTIVE_CODE='" & Incentive_Code & "' ) as Final order by INCENTIVE_CODE,Param_Seq,SLAB_FROM desc"
        dtIncentive = clsDBFuncationality.GetDataTable(qry, trans)
        Dim CondFatStatus As Boolean = False
        Dim CondSNFStatus As Boolean = False
        Dim CondCLRStatus As Boolean = False
        Dim QtyConv As Decimal = 0

        If dtIncentive.Select("Parameter_Type='FAT'").Length <= 0 Then
            CondFatStatus = True
        End If
        If dtIncentive.Select("Parameter_Type='SNF'").Length <= 0 Then
            CondSNFStatus = True
        End If
        If dtIncentive.Select("Parameter_Type='CLR'").Length <= 0 Then
            CondCLRStatus = True
        End If
        '' 1.2
        For Each IncRow As DataRow In dtIncentive.Select("Parameter_Type='FAT'") '' 1.2
            If clsCommon.CompairString(clsCommon.myCstr(drSRN.Item("Unit")), clsCommon.myCstr(IncRow.Item("RATE_UOM"))) = CompairStringResult.Equal Then
                QtyConv = 1
            ElseIf clsCommon.CompairString(clsCommon.myCstr(drSRN.Item("Unit")), "KG") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                QtyConv = 1 / conv_fac
            ElseIf clsCommon.CompairString(clsCommon.myCstr(drSRN.Item("Unit")), "LTR") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("RATE_UOM")), "KG") = CompairStringResult.Equal Then
                QtyConv = conv_fac
            End If
            Qty = BaseQty * QtyConv
            If CondFatStatus = True AndAlso CondSNFStatus = True AndAlso CondCLRStatus = True Then
                Continue For
            End If
            'If clsCommon.CompairString(IncRow.Item("Parameter_Type"), "FAT") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(IncRow.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("FAT_PER")) And clsCommon.myCdbl(drSRN.Item("FAT_PER")) <= clsCommon.myCdbl(IncRow.Item("SLAB_TO")) Then ''1.2.1
                CondFatStatus = True
                If clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                    '    FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                    'End If
                    IncentiveAmount = FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                    arr(0) = clsCommon.myCdbl(IncRow.Item("RATE"))
                    arr(1) = IncentiveAmount
                ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                    IncentiveAmount = Qty * clsCommon.myCdbl(IncRow.Item("RATE")) * clsCommon.myCdbl(drSRN.Item("FAT_PER")) ''By Balwinder on 29/08/2018
                    arr(0) = clsCommon.myCdbl(IncRow.Item("RATE"))
                    arr(1) = IncentiveAmount
                End If
                If clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "None") = CompairStringResult.Equal Then '' 1.2.1.1 a
                    Return arr
                    'If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                ElseIf clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "Continue") = CompairStringResult.Equal OrElse clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "AND") = CompairStringResult.Equal OrElse clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "OR") = CompairStringResult.Equal OrElse clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "XOR") = CompairStringResult.Equal Then '' 1.2.1.1b
                    If dtIncentive.Select("Parameter_Type='FAT' and Line_No=" & dtIncentive.Compute("Max(Line_No)", "Parameter_Type='FAT'") & "").Length > 0 Then
                        '' find last range of the slab to find out that tere are further or , and conditions left to calculate incentive
                        Dim drOp As DataRow
                        Dim Optype As String
                        Dim Line_No As Integer
                        If clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "Continue") = CompairStringResult.Equal Then
                            drOp = dtIncentive.Select("Parameter_Type='FAT' and Line_No=" & dtIncentive.Compute("Max(Line_No)", "Parameter_Type='FAT'") & "")(0)
                            Optype = clsCommon.myCstr(drOp.Item("OPERATER_TYPE")) '' 1.2.1.1c
                            Line_No = clsCommon.myCdbl(drOp.Item("Line_No")) '' 1.2.1.1c
                        Else
                            drOp = IncRow
                            Optype = clsCommon.myCstr(drOp.Item("OPERATER_TYPE")) '' 1.2.1.1c
                            Line_No = clsCommon.myCdbl(drOp.Item("Line_No")) '' 1.2.1.1c
                        End If

                        If clsCommon.CompairString(Optype, "OR") = CompairStringResult.Equal Then
                            For Each IncRowSNF As DataRow In dtIncentive.Select("Line_No>" & Line_No & " and Parameter_Type<>'FAT'", "SLAB_FROM") '' 1.2
                                If clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
                                    If CondSNFStatus = True Then
                                        Continue For
                                    End If
                                    If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("SNF_PER")) And clsCommon.myCdbl(drSRN.Item("SNF_PER")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                        CondSNFStatus = True
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                                FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                            End If
                                            IncentiveAmount = IncentiveAmount + FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                            'arr(0) = clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                            arr(1) = IncentiveAmount
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                            IncentiveAmount = IncentiveAmount + Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                            'arr(0) = clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                            arr(1) = IncentiveAmount
                                        End If
                                        ''Return arr
                                        Continue For
                                    Else
                                        Continue For
                                    End If
                                ElseIf clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
                                    If CondCLRStatus = True Then
                                        Continue For
                                    End If
                                    If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("CLR")) And clsCommon.myCdbl(drSRN.Item("CLR")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                        CondCLRStatus = True
                                        '' no further calculation in clr case
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                                FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                            End If
                                            IncentiveAmount = IncentiveAmount + FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                            arr(1) = IncentiveAmount
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                            IncentiveAmount = IncentiveAmount + Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                            arr(1) = IncentiveAmount
                                        End If
                                        'Return arr
                                        Continue For
                                    Else
                                        Continue For
                                    End If
                                End If
                            Next
                        ElseIf clsCommon.CompairString(Optype, "XOR") = CompairStringResult.Equal Then
                            For Each IncRowSNF As DataRow In dtIncentive.Select("Line_No>" & Line_No & " and Parameter_Type<>'FAT'", "SLAB_FROM") '' 1.2
                                If clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
                                    If CondSNFStatus = True Then
                                        Continue For
                                    End If
                                    If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("SNF_PER")) And clsCommon.myCdbl(drSRN.Item("SNF_PER")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                        CondSNFStatus = True
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                                FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                            End If
                                            'IncentiveAmount = FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                            'IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        End If
                                        Continue For
                                        'Exit For
                                    Else
                                        Continue For
                                    End If
                                ElseIf clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
                                    If CondCLRStatus = True Then
                                        Continue For
                                    End If
                                    If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("CLR")) And clsCommon.myCdbl(drSRN.Item("CLR")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                        CondCLRStatus = True
                                        '' no further calculation in clr case
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                                FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                            End If
                                            'IncentiveAmount = FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                            'IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        End If
                                        Continue For
                                        'Exit For
                                    Else
                                        Continue For
                                    End If
                                End If
                            Next
                        ElseIf clsCommon.CompairString(Optype, "AND") = CompairStringResult.Equal Then
                            For Each IncRowSNF As DataRow In dtIncentive.Select("Line_No>" & Line_No & " and Parameter_Type<>'FAT'", "SLAB_FROM") '' 1.2
                                If clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
                                    If CondSNFStatus = True Then
                                        Continue For
                                    End If
                                    If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("SNF_PER")) And clsCommon.myCdbl(drSRN.Item("SNF_PER")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                        CondSNFStatus = True
                                        Continue For
                                        'Exit For
                                        'If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                        '    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                        '        SNF_KG = Math.Truncate(SNF_KG / conv_fac * 1000) / 1000
                                        '    End If
                                        '    IncentiveAmount = SNF_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                        'ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                        '    IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        '    Exit For
                                        'End If
                                    Else
                                        IncentiveAmount = 0
                                        arr(1) = IncentiveAmount
                                        'Return arr
                                        Continue For
                                    End If
                                ElseIf clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
                                    If CondCLRStatus = True Then
                                        Continue For
                                    End If
                                    If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("CLR")) And clsCommon.myCdbl(drSRN.Item("CLR")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                        CondCLRStatus = True
                                        Continue For
                                        '' no further calculation in clr case
                                        'IncentiveAmount = IncentiveAmount * 1
                                        'Exit For
                                        'If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                        '    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                        '        SNF_KG = Math.Truncate(SNF_KG / conv_fac * 1000) / 1000
                                        '    End If
                                        '    IncentiveAmount = SNF_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                        'ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                        '    IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        '    Exit For
                                        'End If
                                    Else
                                        IncentiveAmount = 0
                                        Continue For
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            Else
                If dtIncentive.Select("Parameter_Type='FAT' and Line_No=" & dtIncentive.Compute("Max(Line_No)", "Parameter_Type='FAT'") & "").Length > 0 Then
                    '' find last range of the slab to find out that tere are further or , and conditions left to calculate incentive
                    If CondFatStatus = True Then
                        If clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                            End If
                            IncentiveAmount = FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                            arr(0) = clsCommon.myCdbl(IncRow.Item("RATE"))
                            arr(1) = IncentiveAmount
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRow.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                            IncentiveAmount = Qty * clsCommon.myCdbl(IncRow.Item("RATE"))
                            arr(0) = clsCommon.myCdbl(IncRow.Item("RATE"))
                            arr(1) = IncentiveAmount
                        End If
                    End If

                    Dim drOp As DataRow
                    Dim Optype As String
                    Dim Line_No As Integer
                    If clsCommon.CompairString(IncRow.Item("OPERATER_TYPE"), "Continue") = CompairStringResult.Equal Then
                        drOp = dtIncentive.Select("Parameter_Type='FAT' and Line_No=" & dtIncentive.Compute("Max(Line_No)", "Parameter_Type='FAT'") & "")(0)
                        Optype = clsCommon.myCstr(drOp.Item("OPERATER_TYPE")) '' 1.2.1.1c
                        Line_No = clsCommon.myCdbl(drOp.Item("Line_No")) '' 1.2.1.1c
                    Else
                        drOp = IncRow
                        Optype = clsCommon.myCstr(drOp.Item("OPERATER_TYPE")) '' 1.2.1.1c
                        Line_No = clsCommon.myCdbl(drOp.Item("Line_No")) '' 1.2.1.1c
                    End If

                    If clsCommon.CompairString(Optype, "OR") = CompairStringResult.Equal Then
                        For Each IncRowSNF As DataRow In dtIncentive.Select("Line_No>" & Line_No & " and Parameter_Type<>'FAT'", "SLAB_FROM") '' 1.2
                            If clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
                                If CondSNFStatus = True Then
                                    Continue For
                                End If
                                If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("SNF_PER")) And clsCommon.myCdbl(drSRN.Item("SNF_PER")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                    CondSNFStatus = True
                                    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                            FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                        End If
                                        IncentiveAmount = IncentiveAmount + FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                        'arr(0) = clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        arr(1) = IncentiveAmount
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                        IncentiveAmount = IncentiveAmount + Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        'arr(0) = clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        arr(1) = IncentiveAmount
                                    End If
                                    Continue For
                                    'Exit For
                                Else
                                    Continue For
                                End If
                            ElseIf clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
                                If CondCLRStatus = True Then
                                    Continue For
                                End If
                                If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("CLR")) And clsCommon.myCdbl(drSRN.Item("CLR")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                    CondCLRStatus = True
                                    '' no further calculation in clr case
                                    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                            FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                        End If
                                        IncentiveAmount = IncentiveAmount + FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                        arr(1) = IncentiveAmount
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                        IncentiveAmount = IncentiveAmount + Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                        arr(1) = IncentiveAmount
                                    End If
                                    Continue For
                                    'Exit For
                                Else
                                    Continue For
                                End If
                            End If
                        Next
                    ElseIf clsCommon.CompairString(Optype, "XOR") = CompairStringResult.Equal Then
                        For Each IncRowSNF As DataRow In dtIncentive.Select("Line_No>" & Line_No & " and Parameter_Type<>'FAT'", "SLAB_FROM") '' 1.2
                            If clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
                                If CondSNFStatus = True Then
                                    Continue For
                                End If
                                If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("SNF_PER")) And clsCommon.myCdbl(drSRN.Item("SNF_PER")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                    CondSNFStatus = True
                                    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                            FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                        End If
                                        IncentiveAmount = FAT_KG * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                        IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                    End If
                                    Continue For
                                    'Exit For
                                Else
                                    Continue For
                                End If
                            ElseIf clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
                                If CondCLRStatus = True Then
                                    Continue For
                                End If
                                If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("CLR")) And clsCommon.myCdbl(drSRN.Item("CLR")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                    CondCLRStatus = True
                                    '' no further calculation in clr case
                                    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                            FAT_KG = Math.Truncate(FAT_KG / conv_fac * 1000) / 1000
                                        End If
                                        'IncentiveAmount = FAT_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                        'IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                    End If
                                    Continue For
                                    'Exit For
                                Else
                                    Continue For
                                End If
                            End If
                        Next
                    ElseIf clsCommon.CompairString(Optype, "AND") = CompairStringResult.Equal Then
                        For Each IncRowSNF As DataRow In dtIncentive.Select("Line_No>" & Line_No & " and Parameter_Type<>'FAT'", "SLAB_FROM") '' 1.2
                            If clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
                                If CondSNFStatus = True Then
                                    Continue For
                                End If
                                If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("SNF_PER")) And clsCommon.myCdbl(drSRN.Item("SNF_PER")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                    CondSNFStatus = True
                                    Continue For
                                    'Exit For
                                    'If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                    '    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                    '        SNF_KG = Math.Truncate(SNF_KG / conv_fac * 1000) / 1000
                                    '    End If
                                    '    IncentiveAmount = SNF_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                    '    IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                    '    Exit For
                                    'End If
                                Else
                                    IncentiveAmount = 0
                                    arr(1) = IncentiveAmount
                                    Continue For
                                    'Exit For
                                End If
                            ElseIf clsCommon.CompairString(IncRowSNF.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
                                If CondCLRStatus = True Then
                                    Continue For
                                End If
                                If clsCommon.myCdbl(IncRowSNF.Item("SLAB_FROM")) <= clsCommon.myCdbl(drSRN.Item("CLR")) And clsCommon.myCdbl(drSRN.Item("CLR")) <= clsCommon.myCdbl(IncRowSNF.Item("SLAB_TO")) Then ''1.2.1
                                    CondSNFStatus = True
                                    Continue For
                                    '' no further calculation in clr case
                                    'IncentiveAmount = IncentiveAmount * 1
                                    'Exit For
                                    'If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "F") = CompairStringResult.Equal Then
                                    '    If clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("RATE_UOM")), "LTR") = CompairStringResult.Equal Then
                                    '        SNF_KG = Math.Truncate(SNF_KG / conv_fac * 1000) / 1000
                                    '    End If
                                    '    IncentiveAmount = SNF_KG * clsCommon.myCdbl(IncRow.Item("RATE"))
                                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(IncRowSNF.Item("Rate_Type")), "Q") = CompairStringResult.Equal Then
                                    '    IncentiveAmount = Qty * clsCommon.myCdbl(IncRowSNF.Item("RATE"))
                                    '    Exit For
                                    'End If
                                Else
                                    IncentiveAmount = 0
                                    Continue For
                                    'Exit For
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            'ElseIf clsCommon.CompairString(IncRow.Item("Parameter_Type"), "SNF") = CompairStringResult.Equal Then
            'ElseIf clsCommon.CompairString(IncRow.Item("Parameter_Type"), "CLR") = CompairStringResult.Equal Then
            'End If
        Next
        Return arr
    End Function

    Public Shared Sub Calculate_Quality_Incentive(ByRef dtAlldata As DataTable, ByRef dtprevdata As DataTable, ByVal Incentive_Code As String, ByVal trans As SqlTransaction)
        If dtAlldata IsNot Nothing AndAlso dtAlldata.Rows.Count > 0 Then
            For Each row As DataRow In dtAlldata.Rows
                row.Item("Incentive_value") = Calculate_Quality_Incentive_Amount(dtAlldata, row, Incentive_Code, trans)(1)
            Next
        End If

        If dtprevdata IsNot Nothing AndAlso dtprevdata.Rows.Count > 0 Then
            For Each row As DataRow In dtprevdata.Rows
                row.Item("Incentive_value") = Calculate_Quality_Incentive_Amount(dtAlldata, row, Incentive_Code, trans)(1)
            Next
        End If

    End Sub

    Public Shared Function LoadDataQuery_For_Incentive(ByVal Inv_Code As String, ByVal VspCode As String, ByVal MCC_Code As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal is_For_Saved_Invoice As Boolean, ByVal trans As SqlTransaction, ByVal days_count As Integer) As ArrayList
        Return LoadDataQuery_For_Incentive(False, Inv_Code, VspCode, MCC_Code, Frm_date, To_date, is_For_Saved_Invoice, trans, days_count)
    End Function
    Public Shared Function LoadDataQuery_For_Incentive(ByVal isProvision As Boolean, ByVal Inv_Code As String, ByVal VspCode As String, ByVal MCC_Code As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal is_For_Saved_Invoice As Boolean, ByVal trans As SqlTransaction, ByVal days_count As Integer) As ArrayList
        Dim strTabMilkPurchaseInvoiceDetail As String = "TSPL_MILK_PURCHASE_INVOICE_DETAIL"
        If isProvision Then
            strTabMilkPurchaseInvoiceDetail = "TSPL_MILK_PURCHASE_INVOICE_PROVISION_DETAIL"
        End If
        Dim days_Count_extra As Integer = days_count
        Dim ArrReturn As New ArrayList
        Dim Qty As Double = 0
        Dim record_date As Date = Today
        Dim qry As String = ""
        Dim qry_prev As String = ""
        Dim Pc_Value As Double = clsDBFuncationality.getSingleValue("select PC_VALUE from TSPL_PAYMENT_CYCLE_MASTER inner join TSPL_VENDOR_MASTER on " _
                                            & " TSPL_VENDOR_MASTER.PC_CODE=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where vendor_code='" & VspCode & "'", trans)
        Dim DtIncentiveMaster As DataTable = GetVSPIncentiveMaster(MCC_Code, VspCode, trans)
        If DtIncentiveMaster.Rows.Count <= 0 Then
            Return ArrReturn
            Exit Function
        End If
        If DtIncentiveMaster.Rows(0).Item("Scheme_For") = "PC" Then
            For Each Incrow As DataRow In DtIncentiveMaster.Rows()
                Dim Whrcls As String = ""
                Dim Whrcls_prev As String = ""
                If is_For_Saved_Invoice Then
                    Whrcls = " and Is_Incentive_Created='N' and " + strTabMilkPurchaseInvoiceDetail + ".SRN_CODE is not null and TSPL_MILK_SRN_HEAD.Against_Reject_No is null and TSPL_MILK_SRN_DETAIL.NET_AMOUNT>0"
                    Whrcls_prev = " and TSPL_MILK_SRN_HEAD.Against_Reject_No is null and TSPL_MILK_SRN_DETAIL.NET_AMOUNT>0"
                Else
                    Whrcls = " and " + strTabMilkPurchaseInvoiceDetail + ".Doc_Code='" & clsCommon.myCstr(Inv_Code) & "'  "
                    Whrcls_prev = " and " + strTabMilkPurchaseInvoiceDetail + ".Doc_Code<>'" & clsCommon.myCstr(Inv_Code) & "' "
                End If
                qry = "select distinct CAST(0 as bit) as Sel,code,convert(date,Final.DOC_DATE,103) AS DOC_DATE,DENSE_RANK() over (order by convert(date,Final.DOC_DATE,103)) as Date_Day,MONTH(convert(date,Final.DOC_DATE,103)) as Date_Month,Year(convert(date,Final.DOC_DATE,103)) as Date_Year,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
                & " ,Unit ,Qty as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
                & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
                & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,commision_pers as Commission,payment_commision_pers as Payment_Commission,0.00 as Incentive_value,SUM((Custom_Qty *RI)) as Custom_Qty,max(Custom_UOM) as Custom_UOM,max(Dock_Collection_Milk_Type) as Dock_Collection_Milk_Type from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
                & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,TSPL_VENDOR_MASTER.Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
                & " ,TSPL_ITEM_MASTER.Item_Desc as IName,(case when TSPL_INCENTIVE_MASTER_HEAD.Qty_Type='ACTQ' then TSPL_MILK_SRN_DETAIL.Qty when TSPL_INCENTIVE_MASTER_HEAD.Qty_Type='STDQ' then ((TSPL_MILK_SRN_detail.FAT_KG/Price_Chart.FAT_Pers) * (Price_Chart.Fat_ratio/100.00)+ " _
                & " (TSPL_MILK_SRN_detail.SNF_KG/Price_Chart.SNF_Pers) * (Price_Chart.SNF_Ratio/100.00))*100 else TSPL_MILK_SRN_DETAIL.Qty end)  as Qty,0 as Unapproved,TSPL_MILK_SRN_DETAIL.UOM_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
                & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code " _
                & " ,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift " _
                & " ,TSPL_INCENTIVE_MASTER_HEAD.Qty_Type,TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG,TSPL_INVENTORY_MOVEMENT_NEW.Custom_UOM,case when  TSPL_INVENTORY_MOVEMENT_NEW.Custom_Coversion_Factor>0 then cast((Stock_Qty/TSPL_INVENTORY_MOVEMENT_NEW.Custom_Coversion_Factor) as decimal(18,3)) else 0 end as Custom_Qty,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type from TSPL_MILK_SRN_DETAIL left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.source_doc_no=TSPL_MILK_SRN_DETAIL.DOC_CODE and Trans_Type='MCC-MSRN' left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
                & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code " _
                & " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE " _
                & " left join (SELECT TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Form_Type,TSPL_VENDOR_MASTER.Apply_Mult_Incentive," _
                & " CASE WHEN TSPL_VENDOR_MASTER.Apply_Mult_Incentive=1 THEN TSPL_VSP_INCENTIVE.INCENTIVE_CODE ELSE TSPL_VENDOR_MASTER.incentive END AS Incentive " _
                & " FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VSP_INCENTIVE ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE.VENDOR_CODE WHERE coalesce(TSPL_VSP_INCENTIVE.INCENTIVE_CODE,TSPL_VENDOR_MASTER.incentive)= '" & clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")) & "') AS TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
                & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
                & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No " _
                & " Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  " _
                & " left join TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
                & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  " _
                & " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE " _
                & " left join " + strTabMilkPurchaseInvoiceDetail + " on " + strTabMilkPurchaseInvoiceDetail + ".SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code " _
                & " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE " _
                & " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code " _
                & " from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart " _
                & " on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code " _
                & " Inner join TSPL_INCENTIVE_MASTER_HEAD on INCENTIVE_CODE= TSPL_VENDOR_MASTER.Incentive and TSPL_INCENTIVE_MASTER_HEAD.start_date<=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) and TSPL_INCENTIVE_MASTER_HEAD.End_date>=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) where  TSPL_MILK_SRN_HEAD.Posted=1 " & Whrcls & " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)"

                qry_prev = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,DENSE_RANK() over (order by convert(date,Final.DOC_DATE,103)) as Date_Day,MONTH(convert(date,Final.DOC_DATE,103)) as Date_Month,Year(convert(date,Final.DOC_DATE,103)) as Date_Year,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
                & " ,Unit ,Qty as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
                & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
                & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,commision_pers as Commission,payment_commision_pers as Payment_Commission,0.00 as Incentive_value,SUM((Custom_Qty *RI)) as Custom_Qty,max(Custom_UOM) as Custom_UOM,max(Dock_Collection_Milk_Type) as Dock_Collection_Milk_Type from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
                & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,TSPL_VENDOR_MASTER.Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
                & " ,TSPL_ITEM_MASTER.Item_Desc as IName,(case when TSPL_INCENTIVE_MASTER_HEAD.Qty_Type='ACTQ' then TSPL_MILK_SRN_DETAIL.Qty when TSPL_INCENTIVE_MASTER_HEAD.Qty_Type='STDQ' then ((TSPL_MILK_SRN_detail.FAT_KG/Price_Chart.FAT_Pers) * (Price_Chart.Fat_ratio/100.00)+ " _
                & " (TSPL_MILK_SRN_detail.SNF_KG/Price_Chart.SNF_Pers) * (Price_Chart.SNF_Ratio/100.00))*100 else TSPL_MILK_SRN_DETAIL.Qty end)  as Qty,0 as Unapproved,TSPL_MILK_SRN_DETAIL.UOM_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
                & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift,TSPL_INCENTIVE_MASTER_HEAD.Rate_Type,TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG,TSPL_INVENTORY_MOVEMENT_NEW.Custom_UOM,case when  TSPL_INVENTORY_MOVEMENT_NEW.Custom_Coversion_Factor>0 then cast((Stock_Qty/TSPL_INVENTORY_MOVEMENT_NEW.Custom_Coversion_Factor) as decimal(18,3)) else 0 end as Custom_Qty,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type from TSPL_MILK_SRN_DETAIL left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.source_doc_no=TSPL_MILK_SRN_DETAIL.DOC_CODE and Trans_Type='MCC-MSRN' left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
                & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
                & " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE " _
                & " left join (SELECT TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Form_Type,TSPL_VENDOR_MASTER.Apply_Mult_Incentive," _
                & " CASE WHEN TSPL_VENDOR_MASTER.Apply_Mult_Incentive=1 THEN TSPL_VSP_INCENTIVE.INCENTIVE_CODE ELSE TSPL_VENDOR_MASTER.incentive END AS Incentive " _
                & " FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VSP_INCENTIVE ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE.VENDOR_CODE WHERE coalesce(TSPL_VSP_INCENTIVE.INCENTIVE_CODE,TSPL_VENDOR_MASTER.incentive)= '" & clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")) & "') AS TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
                & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
                & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
                & " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
                & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  " _
                & " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE " _
                & " left join " + strTabMilkPurchaseInvoiceDetail + " on " + strTabMilkPurchaseInvoiceDetail + ".SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code " _
                & " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE " _
                & " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code " _
                & " from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart " _
                & " on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code " _
                & " Inner join TSPL_INCENTIVE_MASTER_HEAD on INCENTIVE_CODE=TSPL_VENDOR_MASTER.incentive and TSPL_INCENTIVE_MASTER_HEAD.start_date<=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) and TSPL_INCENTIVE_MASTER_HEAD.End_date>=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) where  TSPL_MILK_SRN_HEAD.Posted=1 " & Whrcls_prev & " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date.AddDays(-Pc_Value), "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date.AddDays(-Pc_Value), "dd-MMM-yyyy") & "',103)"

                If clsCommon.myLen(VspCode) > 0 Then
                    qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VspCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0  
                    qry_prev += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VspCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0  
                End If
                qry &= " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers,payment_commision_pers,Qty  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "
                qry_prev &= " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers,payment_commision_pers,Qty  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "

                Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim dtprevdata As DataTable = clsDBFuncationality.GetDataTable(qry_prev, trans)

                If dtAllData.Rows.Count <= 0 Then
                    Continue For
                End If
                If clsCommon.CompairString(Incrow.Item("Starting_Shift"), "E") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("Start_date") & "' and shift='Morning'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtprevdata.Select("Doc_Date='" & Incrow.Item("Start_date") & "' and shift='Morning'")
                        dtprevdata.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtprevdata.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'")
                        dtprevdata.Rows.Remove(row1)
                    Next
                End If
                If clsCommon.CompairString(Incrow.Item("Ending_Shift"), "M") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("End_date") & "' and shift='Evening'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtprevdata.Select("Doc_Date='" & Incrow.Item("End_date") & "' and shift='Evening'")
                        dtprevdata.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtprevdata.Select("Doc_Date>'" & Incrow.Item("End_date") & "'")
                        dtprevdata.Rows.Remove(row1)
                    Next
                End If
                If dtAllData.Rows.Count <= 0 Then
                    Continue For
                End If
                Dim Dtincentive As DataTable = GetIncentive(MCC_Code, VspCode, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                '' calculate quality incentive
                If clsCommon.CompairString(Incrow.Item("INCENTIVE_TYPE"), "QLTY") = CompairStringResult.Equal Then
                    ArrReturn.Add(0)
                    Calculate_Quality_Incentive(dtAllData, dtprevdata, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                    Continue For
                End If
                Dim DaysSetting As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOfDaysForMultiInceForSameVSPForSamePayCycle, clsFixedParameterCode.NoOfDaysForMultiInceForSameVSPForSamePayCycle, trans))
                If DaysSetting = 1 Then
                    days_count = clsCommon.myCdbl(dtAllData.Compute("Max(Date_Day)", ""))
                End If
                For Each row As DataRow In Dtincentive.Rows()
                    Dim strQtyColumnSumOF As String = "SUM(POQty)"
                    Dim strQtyColumn As String = "POQty"
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_UOM")), clsCommon.myCstr(dtAllData.Rows(0)("Custom_UOM"))) = CompairStringResult.Equal Then
                        strQtyColumnSumOF = "SUM(Custom_Qty)"
                        strQtyColumn = "Custom_Qty"
                    End If
                    Dim dblFatPer As Decimal = 0
                    Dim dblSNFPer As Decimal = 0
                    Dim dtResults As DataTable = Nothing
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "SLQLTYTYPE") = CompairStringResult.Equal Then
                        Try
                            dtResults = dtAllData.Select("Dock_Collection_Milk_Type='" + clsCommon.myCstr(row.Item("Type")) + "'").CopyToDataTable()
                        Catch ex As Exception
                        End Try
                        If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dtResults.Compute("SUM(POQty)", "")) > 0 Then
                                dblFatPer = clsCommon.myCdbl(dtResults.Compute("SUM(FAT_Kg)", "")) * 100 / clsCommon.myCdbl(dtResults.Compute("SUM(POQty)", ""))
                                dblSNFPer = clsCommon.myCdbl(dtResults.Compute("SUM(SNF_Kg)", "")) * 100 / clsCommon.myCdbl(dtResults.Compute("SUM(POQty)", ""))
                            End If
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "A") = CompairStringResult.Equal Then '================Avg. Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "SLQLTYTYPE") = CompairStringResult.Equal Then '=============Qty,FAT,SNF Slab and Type ===========Payment Cycle=====================
                                If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                                    If clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) _
                                    And clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("SLAB_To")) _
                                    And dblFatPer >= clsCommon.myCdbl(row.Item("FAT_FROM")) _
                                    And dblFatPer < clsCommon.myCdbl(row.Item("FAT_TO")) _
                                    And dblSNFPer >= clsCommon.myCdbl(row.Item("SNF_FROM")) _
                                    And dblSNFPer < clsCommon.myCdbl(row.Item("SNF_TO")) Then
                                        ArrReturn.Add(row("Rate"))
                                        Calculate_Incentive(isProvision, dtResults, dtprevdata, row, trans)
                                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtResults.Compute("SUM(Incentive_Value)", ""))))
                                        SaveIncentiveDetail(isProvision, dtResults, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    End If
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "SLQLTYTYPE") = CompairStringResult.Equal Then '=============Qty,FAT,SNF Slab and Type ===========Payment Cycle=====================
                                If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                                    If clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) _
                                    And clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("SLAB_To")) _
                                    And dblFatPer >= clsCommon.myCdbl(row.Item("FAT_FROM")) _
                                    And dblFatPer < clsCommon.myCdbl(row.Item("FAT_TO")) _
                                    And dblSNFPer >= clsCommon.myCdbl(row.Item("SNF_FROM")) _
                                    And dblSNFPer < clsCommon.myCdbl(row.Item("SNF_TO")) Then
                                        ArrReturn.Add(row("Rate"))
                                        Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, ""))
                                        ArrReturn.Add(incentive_value)
                                        Calculate_Incentive2(dtResults, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                        SaveIncentiveDetail(isProvision, dtResults, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    End If
                                End If
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "F") = CompairStringResult.Equal Then '================Flat Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive(isProvision, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "SLQLTYTYPE") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                                    If clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) _
                                    And clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("SLAB_To")) _
                                    And dblFatPer >= clsCommon.myCdbl(row.Item("FAT_FROM")) _
                                    And dblFatPer < clsCommon.myCdbl(row.Item("FAT_TO")) _
                                    And dblSNFPer >= clsCommon.myCdbl(row.Item("SNF_FROM")) _
                                    And dblSNFPer < clsCommon.myCdbl(row.Item("SNF_TO")) Then
                                        ArrReturn.Add(row("Rate"))
                                        Calculate_Incentive(isProvision, dtResults, dtprevdata, row, trans)
                                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtResults.Compute("SUM(Incentive_Value)", ""))))
                                        SaveIncentiveDetail(isProvision, dtResults, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    End If
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = Math.Round(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")), 2, MidpointRounding.ToEven)
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")))
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute(strQtyColumnSumOF, ""))
                                    ArrReturn.Add(incentive_value)

                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                    SaveIncentiveDetail(isProvision, dtAllData, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "SLQLTYTYPE") = CompairStringResult.Equal Then '=============Qty,FAT,SNF Slab and Type ===========Payment Cycle=====================
                                If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                                    If clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) _
                                    And clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, "")) < clsCommon.myCdbl(row.Item("SLAB_To")) _
                                    And dblFatPer >= clsCommon.myCdbl(row.Item("FAT_FROM")) _
                                    And dblFatPer < clsCommon.myCdbl(row.Item("FAT_TO")) _
                                    And dblSNFPer >= clsCommon.myCdbl(row.Item("SNF_FROM")) _
                                    And dblSNFPer < clsCommon.myCdbl(row.Item("SNF_TO")) Then
                                        ArrReturn.Add(row("Rate"))
                                        Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtResults.Compute(strQtyColumnSumOF, ""))
                                        ArrReturn.Add(incentive_value)

                                        Calculate_Incentive2(dtResults, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, strQtyColumn)
                                        SaveIncentiveDetail(isProvision, dtResults, Inv_Code, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                                    End If
                                End If
                            End If

                        End If

                    End If
                Next
            Next
        End If
a:      Dim arrexttra As ArrayList = Calculate_Extra_Incentive(Inv_Code, VspCode, MCC_Code, Frm_date, To_date, is_For_Saved_Invoice, trans, days_Count_extra)
        If arrexttra.Count > 0 Then
            If ArrReturn.Count > 0 Then
                ArrReturn(1) = ArrReturn(1) + arrexttra(1)
            Else
                ArrReturn.Add(arrexttra(0))
                ArrReturn.Add(arrexttra(1))
            End If

        End If
        If ArrReturn.Count > 2 Then
            Dim counter As Integer = 1
            Dim Incentive_Value As Double = 0
            For Each row As String In ArrReturn
                If counter > 2 And counter Mod 2 = 0 Then
                    Incentive_Value += clsCommon.myCdbl(row)
                End If
                counter += 1
            Next
            ArrReturn(1) += Incentive_Value
        End If
        Return ArrReturn

    End Function
    Public Shared Function SaveIncentiveDetail(ByVal isProvision As Boolean, ByVal dtAllData As DataTable, ByVal Inv_Code As String, ByVal Incentive_Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim strTab As String = "TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL"
        If isProvision Then
            strTab = "TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL"
        End If
        Dim qry As String = ""
        Dim isSaved As Boolean = True
        For Each dr As DataRow In dtAllData.Rows
            qry = "insert into " + strTab + "(MILK_DOC_Code,MILK_SRN_Code,MILK_Item_Code,Incentive_Code,Incentive_Amount)"
            qry = qry & " select '" & Inv_Code & "','" & clsCommon.myCstr(dr.Item("Code")) & "','" & clsCommon.myCstr(dr.Item("ICode")) & "','" & Incentive_Code & "','" & clsCommon.myCdbl(dr.Item("Incentive_value")) & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Next
        Return isSaved
    End Function
    Public Shared Function SaveIncentiveDetailMP(ByVal dtAllData As DataTable, ByVal MCC_CODE As String, ByVal Inv_Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = True
        Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & MCC_CODE & "'", trans)
        For Each dr As DataRow In dtAllData.Rows
            qry = " update TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY set MP_Incentive=" & clsCommon.myCdbl(dr.Item("Incentive_value")) & ",MP_IncentiveEMP=(Case when VSP.Nature='E' then VSP.Actual_charges ELSE 0 end)*" & _
          " CASE WHEN " & is_Emp_On_Amount_Only & "='1' THEN 0 ELSE " & clsCommon.myCdbl(dr.Item("Incentive_value")) & "/100 END from TSPL_VENDOR_MASTER VSP WHERE VSP.Vendor_Code=TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY.VSP_CODE " & _
          " AND TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY.DOC_CODE='" & Inv_Code & "' AND TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY.VSP_CODE='" & clsCommon.myCstr(dr.Item("VENDOR")) & "' and coalesce(Manual_DOC_NO,PD_DOC_NO)='" & clsCommon.myCstr(dr.Item("Code")) & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Next
        Return isSaved
    End Function



    '=========================Extra Incentive Calculation Added on Jan,23 2014==================
    Public Shared Function SaveExtraIncentive(ByVal Dt As DataTable, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            For Each row As DataRow In Dt.Rows
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "Invoice_Code", clsCommon.myCstr(row.Item("Invoice_Code")))
                clsCommon.AddColumnsForChange(coll, "Incentive_Code", clsCommon.myCstr(row.Item("Incentive_Code")))
                clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCdbl(row.Item("Rate")))
                clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(row.Item("Incentive_Value")))
                Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_Milk_Purchase_Invoice_VSP_INCENTIVE_Detail where Invoice_Code='" & clsCommon.myCstr(row.Item("Invoice_Code")) & "' and incentive_Code='" & clsCommon.myCstr(row.Item("Incentive_Code")) & "'", trans)
                If check <= 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Purchase_Invoice_VSP_INCENTIVE_Detail", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Purchase_Invoice_VSP_INCENTIVE_Detail", OMInsertOrUpdate.Update, " TSPL_Milk_Purchase_Invoice_VSP_INCENTIVE_Detail.Invoice_code='" & clsCommon.myCstr(row.Item("Invoice_Code")) & "' and incentive_code='" & clsCommon.myCstr(row.Item("Incentive_Code")) & "'", trans)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetIncentiveExtra(ByVal MCC_Code As String, ByVal Vspcode As String, ByVal Incentive_Code As String, ByVal trans As SqlTransaction) As DataTable
        sQuery = "select TSPL_INCENTIVE_MASTER_HEAD.START_DATE,TSPL_INCENTIVE_MASTER_HEAD.End_Date,TSPL_INCENTIVE_MASTER_HEAD.Incentive_Code,SCHEME_FOR,Calc_Type,rate_type,TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE,SLAB_FROM " _
        & " ,SLAB_TO,TS_From,Ts_To,RATE,RATE_UOM,0.00 as Incentive_Value,Starting_Shift,ending_shift from TSPL_VSP_INCENTIVE_Detail inner join TSPL_INCENTIVE_MASTER_HEAD on TSPL_VSP_INCENTIVE_Detail.incentive_code" _
        & " =TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE  inner join TSPL_INCENTIVE_DETAIL on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=" _
        & " TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE and  vSp_code='" & Vspcode & "' and TSPL_INCENTIVE_MASTER_HEAD.incentive_Code='" & Incentive_Code & "' inner join tspl_Mcc_Uom_Detail on " _
        & " tspl_Mcc_Uom_Detail.mcc_COde='" & MCC_Code & "' and tspl_Mcc_Uom_Detail.mcc_COde=TSPL_VSP_INCENTIVE_Detail.mcc_code and " _
         & "  tspl_Mcc_Uom_Detail.uom_Code=Rate_Uom order by TSPL_INCENTIVE_DETAIL.incentive_code,TSPL_INCENTIVE_DETAIL.slab_From desc"
        Dim DtIncentive As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
        Return DtIncentive
    End Function

    Public Shared Function Calculate_Extra_Incentive(ByVal Inv_Code As String, ByVal VspCode As String, ByVal MCC_Code As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal is_For_Saved_Invoice As Boolean, ByVal trans As SqlTransaction, ByVal days_count As Integer) As ArrayList
        Dim Arrincentive As New ArrayList
        '' commented by Panch Raj because now no use of extra incentive (provided multiple incentive functionality in vsp master and also on vsp incentive tagging)
        'Try
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_VSP_INCENTIVE_Detail where vsp_code='" & VspCode & "' and mcc_code='" & MCC_Code & "'", trans)

        '    Arrincentive.Add(0)
        '    Arrincentive.Add(0)
        '    For Each row As DataRow In dt.Rows
        '        Dim arrexttra As ArrayList = LoadDataQuery_For_Incentive_Extra(Inv_Code, VspCode, MCC_Code, Frm_date, To_date, is_For_Saved_Invoice, trans, days_count, clsCommon.myCstr(row.Item("Incentive_Code")))
        '        If arrexttra.Count > 0 Then
        '            Arrincentive(1) += arrexttra(1)
        '        End If
        '    Next
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString)
        '    Return Nothing
        'End Try
        Return Arrincentive
    End Function

    Public Shared Function LoadDataQuery_For_Incentive_Extra(ByVal Inv_Code As String, ByVal VspCode As String, ByVal MCC_Code As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal is_For_Saved_Invoice As Boolean, ByVal trans As SqlTransaction, ByVal days_count As Integer, ByVal Incentive_Code As String) As ArrayList
        Dim ArrReturn As New ArrayList
        Dim Qty As Double = 0
        Dim record_date As Date
        Dim qry As String = ""
        Dim qry_prev As String = ""
        ' Dim dtIncentiveAmount As DataTable

        Dim Pc_Value As Double = clsDBFuncationality.getSingleValue("select PC_VALUE from TSPL_PAYMENT_CYCLE_MASTER inner join TSPL_VENDOR_MASTER on " _
                                            & " TSPL_VENDOR_MASTER.PC_CODE=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where vendor_code='" & VspCode & "'", trans)
        Dim Dtincentive As DataTable = GetIncentiveExtra(MCC_Code, VspCode, Incentive_Code, trans)
        If Dtincentive.Rows.Count <= 0 Then
            Return ArrReturn
            Exit Function
        End If
        Dim Whrcls As String = ""
        Dim Whrcls_prev As String = ""
        If is_For_Saved_Invoice Then
            Whrcls = " and Is_Incentive_Created='N' and TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is not null "
        Else
            Whrcls = " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.Doc_Code='" & clsCommon.myCstr(Inv_Code) & "' AND TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is not null"
            Whrcls_prev = " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.Doc_Code<>'" & clsCommon.myCstr(Inv_Code) & "' AND TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is not null "
        End If
        '' get Quantitive_Type
        Dim Qty_Type As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Qty_Type from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE='" & Incentive_Code & "'", trans))
        'If is_For_Saved_Invoice Then ' If Dtincentive.Rows(0).Item("Scheme_For") = "Month" Or Dtincentive.Rows(0).Item("Scheme_For") = "Year" Then
        qry = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,MONTH(convert(date,Final.DOC_DATE,103)) as Date_Month,Year(convert(date,Final.DOC_DATE,103)) as Date_Year,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
        & " ,Unit ,Qty as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
        & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
        & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,commision_pers as Commission,payment_commision_pers as Payment_Commission,0.00 as Incentive_value from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
        & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
        & " ,Item_Desc as IName,(case when '" & Qty_Type & "'='ACTQ' then TSPL_MILK_SRN_DETAIL.Qty when '" & Qty_Type & "'='STDQ' then ((TSPL_MILK_SRN_detail.FAT_KG/Price_Chart.FAT_Pers) * (Price_Chart.Fat_ratio/100)+ " _
        & " (TSPL_MILK_SRN_detail.SNF_KG/Price_Chart.SNF_Pers) * (Price_Chart.SNF_Ratio/100))*100 else TSPL_MILK_SRN_DETAIL.Qty end)  as Qty,0 as Unapproved,Unit_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
        & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
        & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
        & " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
        & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
        & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No  Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
        & " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
        & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code " _
        & " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE  " _
        & " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code " _
        & " from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart " _
        & " on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code " _
        & " where  TSPL_MILK_SRN_HEAD.Posted=1 " & Whrcls & " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)"
        'Else
        'qry = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
        '& " ,Unit ,Qty as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
        '& " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
        '& " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,commision_pers as Commission,payment_commision_pers as Payment_Commission from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
        '& " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
        '& " ,Item_Desc as IName,TSPL_MILK_SRN_DETAIL.Qty  as Qty,0 as Unapproved,Unit_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
        '& " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
        '& " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
        '& " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
        '& " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
        '& " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
        '& " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
        '& " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE Inner join TSPL_INCENTIVE_MASTER_HEAD on INCENTIVE_CODE=TSPL_VENDOR_MASTER.incentive and TSPL_INCENTIVE_MASTER_HEAD.start_date<=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) and TSPL_INCENTIVE_MASTER_HEAD.End_date>=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) where  TSPL_MILK_SRN_HEAD.Posted=1 and TSPL_MILK_PURCHASE_INVOICE_DETAIL.Doc_Code='" & clsCommon.myCstr(Inv_Code) & "'   and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)"
        'End If
        ''/(case when Unit_Code='LTR' then 1.03 else 1.00 end)
        qry_prev = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,MONTH(convert(date,Final.DOC_DATE,103)) as Date_Month,Year(convert(date,Final.DOC_DATE,103)) as Date_Year,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
        & " ,Unit ,Qty as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
        & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
        & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,commision_pers as Commission,payment_commision_pers as Payment_Commission,0.00 as Incentive_value from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
        & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
        & " ,Item_Desc as IName,(case when '" & Qty_Type & "'='ACTQ' then TSPL_MILK_SRN_DETAIL.Qty when '" & Qty_Type & "'='STDQ' then ((TSPL_MILK_SRN_detail.FAT_KG/Price_Chart.FAT_Pers) * (Price_Chart.Fat_ratio/100)+ " _
        & " (TSPL_MILK_SRN_detail.SNF_KG/Price_Chart.SNF_Pers) * (Price_Chart.SNF_Ratio/100))*100 else TSPL_MILK_SRN_DETAIL.Qty end)  as Qty,0 as Unapproved,Unit_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
        & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
        & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
        & " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
        & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
        & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
        & " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
        & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code " _
        & " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE  " _
        & " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code " _
        & " from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart " _
        & " on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code " _
        & " where  TSPL_MILK_SRN_HEAD.Posted=1 " & Whrcls_prev & " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date.AddDays(-Pc_Value), "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date.AddDays(-Pc_Value), "dd-MMM-yyyy") & "',103)"

        If clsCommon.myLen(VspCode) > 0 Then
            qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VspCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0  
            qry_prev += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VspCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0  
        End If
        qry &= " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers,payment_commision_pers,Qty  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "
        qry_prev &= " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers,payment_commision_pers,Qty  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "

        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim dtprevdata As DataTable = clsDBFuncationality.GetDataTable(qry_prev, trans)

        If dtAllData.Rows.Count <= 0 Then
            Return ArrReturn
            Exit Function
        End If
        Dtincentive.Columns.Add("Invoice_Code")
        If clsCommon.CompairString(Dtincentive.Rows(0).Item("Starting_Shift"), "E") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date<'" & Dtincentive.Rows(0).Item("Start_date") & "'").Count > 0 Then
            For Each row As DataRow In dtAllData.Select("Doc_Date='" & Dtincentive.Rows(0).Item("Start_date") & "' and shift='Morning'")
                dtAllData.Rows.Remove(row)
            Next
            For Each row As DataRow In dtprevdata.Select("Doc_Date='" & Dtincentive.Rows(0).Item("Start_date") & "' and shift='Morning'")
                dtprevdata.Rows.Remove(row)
            Next
            For Each row As DataRow In dtAllData.Select("Doc_Date<'" & Dtincentive.Rows(0).Item("Start_date") & "'")
                dtAllData.Rows.Remove(row)
            Next
            For Each row As DataRow In dtprevdata.Select("Doc_Date<'" & Dtincentive.Rows(0).Item("Start_date") & "'")
                dtprevdata.Rows.Remove(row)
            Next
        End If
        If clsCommon.CompairString(Dtincentive.Rows(0).Item("Ending_Shift"), "M") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date>'" & Dtincentive.Rows(0).Item("End_date") & "'").Count > 0 Then
            For Each row As DataRow In dtAllData.Select("Doc_Date='" & Dtincentive.Rows(0).Item("End_date") & "' and shift='Evening'")
                dtAllData.Rows.Remove(row)
            Next
            For Each row As DataRow In dtprevdata.Select("Doc_Date='" & Dtincentive.Rows(0).Item("End_date") & "' and shift='Evening'")
                dtprevdata.Rows.Remove(row)
            Next
            For Each row As DataRow In dtAllData.Select("Doc_Date>'" & Dtincentive.Rows(0).Item("End_date") & "'")
                dtAllData.Rows.Remove(row)
            Next
            For Each row As DataRow In dtprevdata.Select("Doc_Date>'" & Dtincentive.Rows(0).Item("End_date") & "'")
                dtprevdata.Rows.Remove(row)
            Next
        End If
        'For Each row As DataRow In dtAllData.Rows
        '    For Each rowincentive As DataRow In Dtincentive.Rows
        '        row.Item("Incentive_value") = OpenPriceChart(clsCommon.myCstr(row.Item("code")), clsCommon.myCdbl(rowincentive("rate")), trans)
        '    Next
        'Next
        'For Each row As DataRow In dtprevdata.Rows
        '    For Each rowincentive As DataRow In Dtincentive.Rows
        '        row.Item("Incentive_value") = OpenPriceChart(clsCommon.myCstr(row.Item("code")), clsCommon.myCdbl(rowincentive("rate")), trans)
        '    Next
        'Next
        'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
        '================================Payment Cycle==================================
        If Dtincentive.Rows(0).Item("Scheme_For") = "PC" Then
            For Each row As DataRow In Dtincentive.Rows()
                If clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "A") = CompairStringResult.Equal Then '================Avg. Calculation============Payment Cycle================
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                            If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                            If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ' ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                            If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                            If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                            If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                            If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ' ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        End If
                    End If

                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "F") = CompairStringResult.Equal Then '================Flat Calculation============Payment Cycle================
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ' ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                ArrReturn.Add(row("Rate"))
                                Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                ArrReturn.Add(row("Rate"))
                                'Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                'ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                Exit For
                            End If
                        End If

                    End If

                End If
            Next
            'For Each row As DataRow In Dtincentive.Rows()
            '    If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
            '        If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
            '            ArrReturn.Add(row("Rate"))
            '            'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
            '            ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
            '            Exit For
            '        End If
            '    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
            '        If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
            '            ArrReturn.Add(row("Rate"))
            '            'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
            '            ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
            '            Exit For
            '        End If
            '    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
            '        If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
            '            ArrReturn.Add(row("Rate"))
            '            'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
            '            ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
            '            Exit For
            '        End If
            '    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
            '        If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
            '            ArrReturn.Add(row("Rate"))
            '            ' ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
            '            ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
            '            Exit For
            '        End If
            '    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
            '        If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
            '            ArrReturn.Add(row("Rate"))
            '            'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
            '            ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
            '            Exit For
            '        End If
            '    End If
            'Next
            '============================================================================
        ElseIf Dtincentive.Rows(0).Item("Scheme_For") = "PPC" Then
            For Each row As DataRow In Dtincentive.Rows()
                If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Previous Payment Cycle================
                    If clsCommon.myCdbl(dtprevdata.Compute("SUm(POQTy)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", ""))))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Previous Payment Cycle==========================
                    If clsCommon.myCdbl(dtprevdata.Compute("SUm(POQTy)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", ""))))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Previous Payment Cycle=====================
                    If (clsCommon.myCdbl(dtprevdata.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(Fat_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                        ArrReturn.Add(row("Rate"))
                        ' ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", ""))))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Previous Payment Cycle=====================
                    If (clsCommon.myCdbl(dtprevdata.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(Fat_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", ""))))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Previous Payment Cycle=====================
                    If clsCommon.myCdbl(dtprevdata.Compute("SUm(POQTy)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(Fat_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(Fat_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTY)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", ""))))
                        Exit For
                    End If
                End If
            Next
            '============================================================================
        ElseIf Dtincentive.Rows(0).Item("Scheme_For") = "DPC" Then
            For Each row As DataRow In Dtincentive.Rows()
                If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Difference of Current and Previous Payment Cycle================
                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "")) - clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", "")))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Difference of Current and Previous Payment Cycle==========================
                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "")) - clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", "")))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Difference of Current and Previous Payment Cycle=====================
                    If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "")) - clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", "")))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Difference of Current and Previous Payment Cycle=====================
                    If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "")) - clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", "")))
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Difference of Current and Previous Payment Cycle=====================
                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                        ArrReturn.Add(row("Rate"))
                        'ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQTy)", "")))
                        ArrReturn.Add(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "")) - clsCommon.myCdbl(dtprevdata.Compute("SUM(Incentive_Value)", "")))
                        Exit For
                    End If
                End If
            Next
            '============================================================================
            '======================================Day========================================
        ElseIf Dtincentive.Rows(0).Item("Scheme_For") = "Day" Then
            days_count = 1
            For Each row As DataRow In Dtincentive.Rows()
                If clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "F") = CompairStringResult.Equal Then '================Flat Calculation============DAY================
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Calculation============DAY================
                        Qty = 0
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '==============================Quantity Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '==============================Slab Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '==============================Total Solid========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        End If

                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============DAY================
                        Qty = 0
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '==============================Quantity Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '==============================Slab Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '==============================Total Solid========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        End If

                    End If

                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "A") = CompairStringResult.Equal Then '================Flat Calculation============Quantity Based================
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Calculation============DAY================
                        Qty = 0
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '==============================Quantity Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '==============================Slab Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '==============================Total Solid========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                            End If
                        End If

                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============DAY================
                        Qty = 0
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '==============================Quantity Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '==============================Slab Based========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '==============================Total Solid========Day===================================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Day=====================
                            For Each Rowd As DataRow In dtAllData.Rows
                                If record_date <> clsCommon.myCDate(Rowd.Item("Doc_date")) Then
                                    If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                        If Not ArrReturn.Contains(row("Rate")) Then
                                            ArrReturn.Add(row("Rate"))
                                        End If
                                        Calculate_Incentive(dtAllData, dtprevdata, row, trans)
                                        Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                        record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                                    End If
                                End If
                            Next
                            If clsCommon.myCdbl(Qty) > 0 Then
                                ArrReturn.Add(clsCommon.myCdbl(Qty))
                            End If
                        End If

                    End If


                End If

            Next
            '==========================================================================================
            '======================================Month========================================
        ElseIf Dtincentive.Rows(0).Item("Scheme_For") = "Month" Then
            For Each row As DataRow In Dtincentive.Rows()
                Qty = 0
                If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '==============================Quantity Based========Month===================================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Month(record_date) <> Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '==============================Slab Based========Month===================================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Month(record_date) <> Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '==============================Total Solid========Month===================================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Month(record_date) <> Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Month=====================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Month(record_date) <> Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Month=====================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Month(record_date) <> Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "Date_Month='" & Month(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                End If

            Next
            '==========================================================================================
            '======================================Year========================================
        ElseIf Dtincentive.Rows(0).Item("Scheme_For") = "Year" Then
            For Each row As DataRow In Dtincentive.Rows()
                Qty = 0
                If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '==============================Quantity Based========Year===================================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Year(record_date) <> Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '==============================Slab Based========Year===================================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Year(record_date) <> Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '==============================Total Solid========Year===================================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Year(record_date) <> Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Year=====================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Year(record_date) <> Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Year=====================
                    For Each Rowd As DataRow In dtAllData.Rows
                        If Year(record_date) <> Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) Then
                            If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) < clsCommon.myCdbl(row.Item("Slab_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", "date_year='" & Year(clsCommon.myCDate(Rowd.Item("Doc_date"))) & "'"))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                If Not ArrReturn.Contains(row("Rate")) Then
                                    ArrReturn.Add(row("Rate"))
                                End If
                                Qty += clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "Doc_Date='" & Rowd.Item("Doc_date") & "'"))
                                record_date = clsCommon.myCDate(Rowd.Item("Doc_date"))
                            End If
                        End If
                    Next
                    If clsCommon.myCdbl(Qty) > 0 Then
                        ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(Qty))
                    End If
                End If

            Next
            '==========================================================================================
        End If
a:      If ArrReturn.Count > 2 Then
            Dim counter As Integer = 1
            Dim Incentive_Value As Double = 0
            For Each row As String In ArrReturn
                If counter > 2 And counter Mod 2 = 0 Then
                    Incentive_Value += clsCommon.myCdbl(row)
                End If
                counter += 1
            Next
            ArrReturn(1) += Incentive_Value
        End If
        If ArrReturn.Count <= 0 Then
            Return ArrReturn
        End If
        For Each row As DataRow In Dtincentive.Rows
            row.Item("Invoice_Code") = clsCommon.myCstr(Inv_Code)
            row.Item("Incentive_Value") = clsCommon.myCdbl(ArrReturn(1))
        Next
        SaveExtraIncentive(Dtincentive, trans)
        Return ArrReturn

    End Function
    '======================================================================================================
    Public Function Gettable(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = " SELECT DOC_CODE as [DOC CODE],VLC_DOC_CODE as [VLC DOC CODE],SAMPLE_NO as [SAMPLE NO],VLC_CODE as [VLC CODE],TSPL_ITEM_MASTER.Item_Code + '(' + Item_Desc + ')' as [Item],ROUTE_CODE as [ROUTE CODE],VSP_CODE as [VSP CODE],VEHICLE_CODE as [VEHICLE CODE], "
        qry += " NO_OF_CANS as [NO OF CANS],MILK_WEIGHT as [MILK WEIGHT],TYPE,MILK_TYPE as [MILK TYPE],FAT,SNF,SAMPLE_NO_VALUES as [SAMPLE NO VALUES],MCC_CODE as [MCC CODE],DOC_DATE as [DOC DATE],SHIFT,COMM_PORT as [COM PORT],MACHINE_NO as [MACHINE NO],Case when IS_MANUAL='N' then 'Auto' else 'Manual' end as [Entry Type] FROM TSPL_MILK_PURCHASE_INVOICE_DETAIL inner join tspl_item_master on tspl_item_master.item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_Code  WHERE 2=2"
        qry += " AND TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkPurchaseInvoiceMCC, ByVal objList As List(Of clsMilkPurchaseInvoiceMCCDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, trans)

            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            trans.Rollback()
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkPurchaseInvoiceMCC, ByVal objList As List(Of clsMilkPurchaseInvoiceMCCDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            If GetPost(obj.DOC_DATE, obj.MCC_CODE, trans) Then
                Throw New Exception("This Code:" + obj.DOC_CODE + " Is Posted.")
            End If
            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"), clsDocType.MilkPurInvoice, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
            Else
                isNewEntry = False
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.DOC_CODE, "TSPL_MILK_PURCHASE_INVOICE_HEAD", "DOC_CODE", "TSPL_MILK_PURCHASE_INVOICE_DETAIL", "DOC_CODE", trans)
                Dim squery As String = "delete from tspl_milk_purchase_invoice_detail where Doc_Code='" & obj.DOC_CODE & "'"
                clsDBFuncationality.ExecuteNonQuery(squery, trans)
                'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
            End If


            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "Irregular_MCC_CODE", obj.Irregular_MCC_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
            clsCommon.AddColumnsForChange(coll, "No_Of_Asset", obj.No_Of_Asset)
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

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_PURCHASE_INVOICE_HEAD where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMilkPurchaseInvoiceMCCDetail.SaveData(obj.DOC_CODE, objList, trans)
            isSaved = isSaved AndAlso clsPIRemittance.SaveData(obj.objPIRemittance, obj.DOC_CODE, obj.DOC_DATE, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function LoadDataQuery_For_Incentive_MP(ByVal Inv_Code As String, ByVal VspCode As String, ByVal MCC_Code As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal is_For_Saved_Invoice As Boolean, ByVal trans As SqlTransaction, ByVal days_count As Integer) As ArrayList
        Dim days_Count_extra As Integer = days_count
        Dim ArrReturn As New ArrayList
        Dim Qty As Double = 0
        Dim record_date As Date = Today
        Dim qry As String = ""
        Dim qry_prev As String = ""
        ' Dim days_count As Integer = To_date.Day - Frm_date.Day
        Dim Pc_Value As Double = clsDBFuncationality.getSingleValue("select PC_VALUE from TSPL_PAYMENT_CYCLE_MASTER inner join TSPL_VENDOR_MASTER on " _
                                            & " TSPL_VENDOR_MASTER.PC_CODE=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where vendor_code='" & VspCode & "'", trans)
        'Dim Dtincentive As DataTable = GetIncentive(MCC_Code, VspCode, trans)
        Dim DtIncentiveMaster As DataTable = GetVSPIncentiveMaster(MCC_Code, VspCode, trans)
        'If Dtincentive.Rows.Count <= 0 Then
        '    'GoTo a
        '    Return ArrReturn
        '    Exit Function
        'End If
        If DtIncentiveMaster.Rows.Count <= 0 Then
            'GoTo a
            sQuery = " Update tspl_Milk_Purchase_Invoice_Head set MP_Amount=Inv_MP.MP_AMOUNT,MP_EMP=Inv_MP.MP_EMP,MP_Incentive=Inv_MP.MP_Incentive ,MP_IncentiveEMP=Inv_MP.MP_IncentiveEMP " & _
                    " from (select DOC_CODE,VSP_CODE,sum(MP_AMOUNT) as MP_AMOUNT,sum(MP_EMP) as MP_EMP,sum(MP_Incentive)as MP_Incentive,sum(MP_IncentiveEMP) as MP_IncentiveEMP " & _
                    " from TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY group by DOC_CODE,VSP_CODE)  Inv_MP where tspl_Milk_Purchase_Invoice_Head.doc_code=Inv_MP.DOC_CODE " & _
                    " and tspl_Milk_Purchase_Invoice_Head.VSP_CODE=Inv_MP.VSP_CODE  and tspl_Milk_Purchase_Invoice_Head.doc_code='" & clsCommon.myCstr(Inv_Code) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            Return ArrReturn
            Exit Function
        End If

        If DtIncentiveMaster.Rows(0).Item("Scheme_For") = "PC" Then
            For Each Incrow As DataRow In DtIncentiveMaster.Rows()
                Dim Whrcls As String = ""
                Dim Whrcls_prev As String = ""
                Whrcls = " and Final.Doc_Code='" & clsCommon.myCstr(Inv_Code) & "' "
                qry = " SELECT distinct CAST(0 as bit) as Sel,COALESCE(Final.MANUAL_DOC_NO,PD_DOC_NO) AS CODE," &
                      " convert(date,Final.FILE_DATE,103) AS DOC_DATE,DENSE_RANK() over (order by convert(date,Final.FILE_DATE,103)) as Date_Day," &
                      " MONTH(convert(date,Final.FILE_DATE,103)) as Date_Month,Year(convert(date,Final.FILE_DATE,103)) as Date_Year, " &
                      " NULL AS ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,FINAL.VSP_CODE AS VENDOR,VSP.Vendor_Name,'' as IName, " &
                      " Uom_Code AS  Unit , MP_Qty as POQty, MP_Qty as GRNQty,0 as UnapprovedQty,MP_Qty as PedningQty ,0 as Rate " &
                      " ,0 as Assessable,MP_AMOUNT as Amount,FAT_PER,SNF_PER, " &
                      " 0 AS CLR,FAT_KG,SNF_KG,0 AS cans,'' AS VEHICLE_CODE,'' AS Vehicle_Name,0 AS Correction_factor,Final.shift,commision_pers as Commission," &
                      " payment_commision_pers as Payment_Commission,0.00 as Incentive_value FROM TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY  Final " &
                      " LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON Final.VLC_Code=VLC.VLC_Code " &
                      " LEFT JOIN TSPL_VENDOR_MASTER VSP ON Final.VSP_CODE=VSP.Vendor_Code " &
                      " Inner join TSPL_INCENTIVE_MASTER_HEAD on INCENTIVE_CODE= VSP.Incentive and TSPL_INCENTIVE_MASTER_HEAD.start_date<=convert(date,Final.FILE_DATE,103) " &
                      " and TSPL_INCENTIVE_MASTER_HEAD.End_date>=convert(date,Final.FILE_DATE,103) WHERE 2=2 " & Whrcls


                If clsCommon.myLen(VspCode) > 0 Then
                    qry += " and FINAL.VSP_Code='" + VspCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0  
                End If
                Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim dtprevdata As DataTable = New DataTable
                If dtAllData.Rows.Count <= 0 Then
                    Continue For
                End If
                If clsCommon.CompairString(Incrow.Item("Starting_Shift"), "E") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("Start_date") & "' and shift='Morning'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                End If
                If clsCommon.CompairString(Incrow.Item("Ending_Shift"), "M") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("End_date") & "' and shift='Evening'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                End If
                Dim Dtincentive As DataTable = GetIncentive(MCC_Code, VspCode, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                Dim DaysSetting As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOfDaysForMultiInceForSameVSPForSamePayCycle, clsFixedParameterCode.NoOfDaysForMultiInceForSameVSPForSamePayCycle, trans))
                If DaysSetting = 1 Then
                    days_count = clsCommon.myCdbl(dtAllData.Compute("Max(Date_Day)", ""))
                End If
                For Each row As DataRow In Dtincentive.Rows()
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "A") = CompairStringResult.Equal Then '================Avg. Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    'Calculate_Incentive_MP(dtAllData, dtprevdata, row, trans)
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            End If
                        End If

                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "F") = CompairStringResult.Equal Then '================Flat Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Calculate_Incentive_MP(Inv_Code, dtAllData, dtprevdata, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    Calculate_Incentive2(dtAllData, dtprevdata, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    SaveIncentiveDetailMP(dtAllData, MCC_Code, Inv_Code, trans)
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        End If
a:      Dim arrexttra As ArrayList = Calculate_Extra_Incentive(Inv_Code, VspCode, MCC_Code, Frm_date, To_date, is_For_Saved_Invoice, trans, days_Count_extra)
        If arrexttra.Count > 0 Then
            If ArrReturn.Count > 0 Then
                ArrReturn(1) = ArrReturn(1) + arrexttra(1)
            Else
                ArrReturn.Add(arrexttra(0))
                ArrReturn.Add(arrexttra(1))
            End If

        End If
        If ArrReturn.Count >= 2 Then
            Dim counter As Integer = 1
            Dim Incentive_Value As Double = 0
            For Each row As String In ArrReturn
                If counter > 1 And counter Mod 2 = 0 Then
                    Incentive_Value += clsCommon.myCdbl(row)
                End If
                counter += 1
            Next
        End If
        sQuery = " Update tspl_Milk_Purchase_Invoice_Head set MP_Amount=Inv_MP.MP_AMOUNT,MP_EMP=Inv_MP.MP_EMP,MP_Incentive=Inv_MP.MP_Incentive ,MP_IncentiveEMP=Inv_MP.MP_IncentiveEMP " & _
                    " from (select DOC_CODE,VSP_CODE,sum(MP_AMOUNT) as MP_AMOUNT,sum(MP_EMP) as MP_EMP,sum(MP_Incentive)as MP_Incentive,sum(MP_IncentiveEMP) as MP_IncentiveEMP " & _
                    " from TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY group by DOC_CODE,VSP_CODE)  Inv_MP where tspl_Milk_Purchase_Invoice_Head.doc_code=Inv_MP.DOC_CODE " & _
                    " and tspl_Milk_Purchase_Invoice_Head.VSP_CODE=Inv_MP.VSP_CODE  and tspl_Milk_Purchase_Invoice_Head.doc_code='" & clsCommon.myCstr(Inv_Code) & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        Return ArrReturn
    End Function

    Public Shared Function SaveMPData(ByVal Doc_Code As String, ByVal From_Date As Date, ByVal To_Date As Date, ByVal MCC_CODE As String, ByVal VSP_CODE As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim arrVSP As New ArrayList
        arrVSP.Add(VSP_CODE)

        qry = "select (case when TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT.VLC_Code is not null or TSPL_VENDOR_MASTER.VSP_Farmer_Billing=1 then 1 else 0 end) as MP_Billing  " & _
            " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT.VLC_Code  " & _
            " left join TSPL_VENDOR_MASTER on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where TSPL_VLC_MASTER_HEAD.VSP_Code='" & VSP_CODE & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso clsCommon.myCdbl(dt.Rows(0).Item("MP_Billing")) > 0 Then
            '' validate mp collection fat and snf per
            Dim qryCheck As String = " select max(Doc_No) from (select Doc_No,round((CASE WHEN VLC_Qty<=0 THEN 0 ELSE FAT_KG*100/VLC_Qty END),3) as Fat_Per,round((CASE WHEN VLC_Qty<=0 THEN 0 ELSE SNF_KG*100/VLC_Qty END),3) AS SNF_Per " & _
                " from (" & GetQuery(From_Date, To_Date, MCC_CODE, arrVSP) & ") MPData)  Data where (Fat_Per>100 or SNF_Per>100) "
            Dim doc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryCheck, trans))
            If clsCommon.myLen(doc) > 0 Then
                Throw New Exception("Fat KG or SNF KG for doc No-" & doc & " are calculated wrongly. Please update and regenerate Bill.")
            End If
            Dim conv_fac As Decimal = clsWeightConversionInfo.GetConversion_factor("LTR", "KG", trans)
            qry = "delete from TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY where DOC_CODE='" & Doc_Code & "' and VSP_CODE='" & VSP_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = " insert into TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY( DOC_CODE,MANUAL_DOC_NO,PD_DOC_NO,FILE_DATE,SHIFT,MP_Qty,Uom_Code,FAT_PER,SNF_PER,FAT_KG,SNF_KG,MCC_CODE,VLC_Code,VSP_CODE,MP_AMOUNT,MP_EMP," & _
                  " MP_Incentive,MP_IncentiveEMP,Net_AMOUNT) " & _
                  " select '" & Doc_Code & "' as DOC_CODE,(case when Type='Manual' then Doc_No else null end) as MANUAL_DOC_NO,(case when Type='PDU' then Doc_No else null end) as PD_DOC_NO,FILE_DATE,SHIFT, " & _
                  " VLC_Qty as MP_Qty,Uom_Code,round((CASE WHEN VLC_Qty<=0 THEN 0 ELSE FAT_KG*100/VLC_Qty END),3) AS VLC_Fat,round((CASE WHEN VLC_Qty<=0 THEN 0 ELSE SNF_KG*100/VLC_Qty END),3) AS VLC_SNF,FAT_KG,SNF_KG,MCC_Code,VLC_CODE,VSP_CODE,Amount,0,0,0,Amount from (" & GetQuery(From_Date, To_Date, MCC_CODE, arrVSP) & ") MPData"

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' UPDATE EMP AMOUNT        
            qry = " update TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY set MP_EMP=(Case when VSP.Nature='E' then VSP.Actual_charges ELSE 0 end)*" & _
                  " (CASE WHEN VSP.Service_Charge_Type='%(Percentage)' THEN MP_AMOUNT/100 " & _
                  " WHEN VSP.Service_Charge_Type='Rate/Kg' THEN (CASE WHEN Uom_Code='KG' THEN  MP_Qty ELSE MP_Qty*" & conv_fac & " END) " & _
                  " WHEN VSP.Service_Charge_Type='Rate/Ltr' THEN (CASE WHEN Uom_Code='LTR' THEN  MP_Qty ELSE MP_Qty/" & conv_fac & " END) END) from TSPL_VENDOR_MASTER VSP WHERE VSP.Vendor_Code=TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY.VSP_CODE " & _
                  " AND TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY.DOC_CODE='" & Doc_Code & "' AND TSPL_MILK_PURCHASE_INVOICE_MP_COLLEC_SUMMARY.VSP_CODE='" & VSP_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        End If

        Return True
    End Function
    Public Shared Function GetQuery(ByVal From_Date As Date, ByVal To_Date As Date, ByVal MCC_CODE As String, ByVal arrVSP As ArrayList) As String
        Dim qry As String = GetBaseQueryWithMP(From_Date, To_Date, MCC_CODE, arrVSP)
        qry = " select Type,Doc_No,Doc_Date,File_Date,MCC_Code,VLC_CODE,VLC_Code_VLC_Uploader,[Shift],UOM_CODE,MCC_CODE_VLC_UPLOADER,VSP_CODE," & _
              " SUM(VLC_Qty) AS VLC_Qty,SUM(FAT_KG) AS FAT_KG,SUM(SNF_KG) AS SNF_KG,SUM(VLC_Water) AS VLC_Water,SUM(Amount) AS Amount from (" & qry & ") as Final GROUP BY Type,Doc_No,Doc_Date,File_Date,MCC_Code,VLC_CODE,VLC_Code_VLC_Uploader,[Shift],UOM_CODE,MCC_CODE_VLC_UPLOADER,VSP_CODE "
        Return qry
    End Function
    Public Shared Function GetBaseQueryWithMP(ByVal From_Date As Date?, ByVal To_Date As Date?, ByVal MCC_CODE As String, ByVal arrVSP As ArrayList) As String
        Dim whrcls As String = ""
        If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
            If clsCommon.myLen(objCommonVar.DefaultMilkItemCode) <= 0 Then
                Throw New Exception("Please first define the MCC Default Milk item in milk setting")
            End If
            whrcls = " and TSPL_WEIGHT_CONVERSION.Structure_Code in (select Structure_Code from TSPL_ITEM_MASTER where Item_Code='" + objCommonVar.DefaultMilkItemCode + "') "
        End If

        Dim qry As String
        Dim qryPDU As String = ""
        Dim qryManual As String = ""
        qryPDU = " select 'PDU' as Type,VLCData.Doc_No,VLCData.Doc_Date as Doc_Date,VLCData.File_Date as File_Date ,VLCData.MCC_Code,VLC.VLC_CODE,VLC.VLC_Code_VLC_Uploader,VLCData.Route_No,coalesce(MP.MP_CODE,MP1.MP_CODE) AS MP_CODE,coalesce(MP.MP_Name,MP1.MP_Name) as MP_Name ,VLCData.shift as [Shift]," & _
              " VLCData.Milk_Type,Round(VLCData.qty,3) as VLC_Qty,VLCData.Uom_Code,round(VLCData.fat,3) as VLC_Fat,round(VLCData.snf,3) as VLC_SNF,round(VLCData.fat_KG,3) as fat_KG,round(VLCData.snf_KG,3) as snf_KG,VLCData.water as VLC_Water,VLCData.Rate,Round(VLCData.Amount,2) as Amount,mcc.Mcc_Code_VLC_Uploader as Mcc_Code_VLC_Uploader,coalesce(MP.MP_Code_VLC_Uploader,MP1.MP_Code_VLC_Uploader) AS MP_Code_Uploader,VLC.VSP_CODE from TSPL_VLC_DATA_UPLOADER VLCData " & _
              " left join TSPL_VLC_MASTER_HEAD VLC on VLCData.VLC_CODE=VLC.VLC_Code_VLC_Uploader and VLC.MCC=VLCData.MCC_Code " & _
              " left join TSPL_MP_MASTER MP ON VLCData.MP_CODE =MP.MP_Code " & _
              " left join TSPL_MP_MASTER MP1 ON VLCData.MP_CODE =MP1.MP_Code_VLC_Uploader and MP1.VLC_Code=VLC.VLC_Code " & _
              " left join TSPL_MCC_MASTER MCC on VLCData.MCC_Code=mcc.MCC_Code where 2=2  "
        If Not From_Date Is Nothing Then
            qryPDU = qryPDU & " and cast(VLCData.File_Date as date) >= convert(date,'" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "',103)"
        End If

        If Not To_Date Is Nothing Then
            qryPDU = qryPDU & " and cast(VLCData.File_Date as date) <= convert(date,'" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "',103)"
        End If
        'qryPDU = qryPDU & " and cast(VLCData.File_Date as date) between convert(date,'" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "',103)"


        If clsCommon.myLen(MCC_CODE) > 0 Then
            qryPDU = qryPDU & " and VLCData.MCC_Code='" & MCC_CODE & "'"
        End If
        If Not arrVSP Is Nothing AndAlso arrVSP.Count > 0 Then
            qryPDU = qryPDU & " and VLC.VSP_CODE in (" & clsCommon.GetMulcallString(arrVSP) & ")"
        End If

        qryManual = " select MFinal.Type,MFinal.Document_Code,MFinal.Doc_Date as Document_Date,MFinal.Document_Date,MFinal.MCC,MFinal.VLC_Code,MFinal.VLC_Code_VLC_Uploader,MFinal.Route_Code,MFinal.Farmer_Code," & _
                    " MFinal.MP_Name,MFinal.Shift,MFinal.Milk_Type,round(MFinal.VLC_Qty,3) as VLC_Qty,MFinal.Unit_Code,round(MFinal.VLC_Fat,3) as VLC_Fat,round(MFinal.VLC_SNF,3) as VLC_SNF,round(MFinal.VLC_FAT_KG*Conv.CF,3) as fat_KG,round(MFinal.VLC_SNF_KG*Conv.CF,3) as snf_KG,MFinal.VLC_Water,MFinal.Rate,round(MFinal.Amount,2) as Amount,MFinal.Mcc_Code_VLC_Uploader,MFinal.MP_Code_Uploader,MFinal.VSP_CODE from " & _
                    " ( select 'Manual' as Type,VLCM.Document_Code,VLCM.Document_Date as Doc_Date,VLCM.Document_Date as Document_Date,VLC.MCC,VLCM.VLC_Code,VLC.VLC_Code_VLC_Uploader,VLCM.Route_Code,VLCD.Farmer_Code,MP.MP_Name ,VLCM.Shift, " & _
                    " '' as Milk_Type,VLCD.Qty as VLC_Qty,VLCD.Unit_Code,VLCD.FatPer as VLC_Fat,VLCD.SNFPer as VLC_SNF,(VLCD.Qty*VLCD.FatPer/100) as VLC_FAT_KG, " & _
                    " (VLCD.Qty*VLCD.SNFPer/100) as VLC_SNF_KG,null as VLC_Water,VLCD.Rate,VLCD.Amount,'' as Mcc_Code_VLC_Uploader,MP.MP_Code_VLC_Uploader AS MP_Code_Uploader,VLC.VSP_CODE from TSPL_VLC_DATA_UPLOADER_MASTER VLCM " & _
                    " inner join TSPL_VLC_DATA_UPLOADER_DETAIL VLCD on VLCM.Document_Code=VLCD.Document_Code " & _
                    " left join TSPL_VLC_MASTER_HEAD VLC on VLCM.VLC_CODE=VLC.VLC_Code " & _
                    " left join TSPL_MP_MASTER MP ON VLCD.Farmer_Code=MP.MP_Code where 2=2 "

        If Not From_Date Is Nothing Then
            qryManual = qryManual & " and cast(VLCM.Document_Date as date) >= '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' "
        End If
        If Not To_Date Is Nothing Then
            qryManual = qryManual & " and cast(VLCM.Document_Date as date) <= '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "' "
        End If
        'qryManual = qryManual & " and cast(VLCM.Document_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "'"

        If clsCommon.myLen(MCC_CODE) > 0 Then
            qryManual = qryManual & " and VLC.MCC='" & MCC_CODE & "'"
        End If
        If Not arrVSP Is Nothing AndAlso arrVSP.Count > 0 Then
            qryManual = qryManual & " and VLC.VSP_CODE in (" & clsCommon.GetMulcallString(arrVSP) & ")"
        End If

        qryManual = qryManual & ") as MFinal "
        qryManual = qryManual & " left join (Select distinct yyy.FromUOM,yyy.TOUOM,max(CF) as CF From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF " & _
                    " from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + "  UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " " & _
                    " UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " " & _
                    " UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " )as yyy group by yyy.FromUOM,yyy.TOUOM) as Conv on MFinal.Unit_Code=Conv.FromUOM and Conv.TOUOM='KG'"
        qry = qryPDU & " Union All " & qryManual & ""

        Return qry
    End Function

    Public Shared Function TSBasedAmountForLossByVSP(ByVal strMCCCode As String, ByVal strVSPCode As String, ByVal From_Date As Date, ByVal To_Date As Date, ByVal tran As SqlTransaction) As Double
        Dim whrcls As String = ""
        If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
            If clsCommon.myLen(objCommonVar.DefaultMilkItemCode) <= 0 Then
                Throw New Exception("Please first define the MCC Default Milk item in milk setting")
            End If
            whrcls = " and TSPL_WEIGHT_CONVERSION.Structure_Code in ('" + clsItemMaster.GetItemStructureCode(objCommonVar.DefaultMilkItemCode, tran) + "') "
        End If

        Dim qry As String
        qry = "select sum(FATAmt+SNFAmt) as LOSSAmt  from (" + Environment.NewLine + _
        "select *,case when FatPer_MP-FatPer_VSP>0 then round(FatPer_MP-FatPer_VSP,2) else 0 end as DiffFAT,case when SNFPer_MP-SNFPer_VSP>0 then round(SNFPer_MP-SNFPer_VSP,2) else 0 end as DiffSNF,round( case when FatPer_MP-FatPer_VSP>0 then ((FatPer_MP-FatPer_VSP)*TSRate_MP/100)*QtyKG_MP else 0 end ,2)as FATAmt" + Environment.NewLine + _
        ",round(case when SNFPer_MP-SNFPer_VSP>0 then ((SNFPer_MP-SNFPer_VSP)*TSRate_MP/100)*QtyKG_MP else 0 end,2) as SNFAmt   from (select Document_Date,Shift,VSP_Code,QtyKG_VSP,FatKG_VSP,SNFKG_VSP,round(case when QtyKG_VSP=0 then 0 else Amount_VSP/QtyKG_VSP end,2) as Rate_VSP,Amount_VSP ,round(case when QtyKG_VSP=0 then 0 else FatKG_VSP*100/QtyKG_VSP end,2) as FatPer_VSP,round(case when QtyKG_VSP=0 then 0 else SNFKG_VSP*100/QtyKG_VSP end,2) as SNFPer_VSP,round(case when QtyKG_VSP=0 then 0 else  TSAmount_VSP/QtyKG_VSP end,2) as TSRate_VSP, TSAmount_VSP" + Environment.NewLine + _
        ",QtyKG_MP,FatKG_MP,SNFKG_MP,round( case when QtyKG_MP=0 then 0 else Amount_MP/QtyKG_MP end,2) as Rate_MP,Amount_MP,round(case when QtyKG_MP=0 then 0 else FatKG_MP*100/QtyKG_MP end,2) as FatPer_MP,round(case when QtyKG_MP=0 then 0 else SNFKG_MP*100/QtyKG_MP end,2) as SNFPer_MP,round(case when QtyKG_MP=0 then 0 else  TSAmount_MP/QtyKG_MP end,2) as TSRate_MP,TSAmount_MP" + Environment.NewLine + _
        "from (" + Environment.NewLine + _
        "select Document_Date,Shift,VSP_Code,sum(QtyKG * case when type='VSP' then 1 else 0 end) as QtyKG_VSP,sum(fat_KG * case when type='VSP' then 1 else 0 end) as FatKG_VSP,sum(snf_KG * case when type='VSP' then 1 else 0 end) as SNFKG_VSP,sum(Amount * case when type='VSP' then 1 else 0 end) as Amount_VSP,sum(TSAmount * case when type='VSP' then 1 else 0 end) as TSAmount_VSP,sum(QtyKG * case when type='VSP' then 0 else 1 end) as QtyKG_MP,sum(fat_KG * case when type='VSP' then 0 else 1 end) as FatKG_MP,sum(snf_KG * case when type='VSP' then 0 else 1 end) as SNFKG_MP,sum(Amount * case when type='VSP' then 0 else 1 end) as Amount_MP,sum(TSAmount * case when type='VSP' then 0 else 1 end) as TSAmount_MP" + Environment.NewLine + _
        "from (" + Environment.NewLine + _
        "select xxx.Type,xxx.MCC,xxx.Route_Code,xxx.VLC_Code,xxx.VSP_Code,xxx.VLC_Code_VLC_Uploader,xxx.Document_Code,convert(date, xxx.Document_Date,103) as Document_Date,xxx.Shift,xxx.MPCode,xxx.MPName,xxx.Qty,xxx.Unit_Code,round(xxx.Qty*Conv.CF,3) as QtyKG,xxx.FatPer,round((xxx.Qty*Conv.CF*xxx.FatPer)/100,3) as fat_KG,xxx.SNFPer,round((xxx.Qty*Conv.CF*xxx.SNFPer)/100,3) as snf_KG,xxx.Rate,round(xxx.Amount,2) as Amount,xxx.TSRate,round(TSRate*xxx.Qty*Conv.CF,2) as TSAmount" + Environment.NewLine + _
        "from (" + Environment.NewLine + _
        "select *,round(case when FatPer+SNFPer=0 then 0 else (Rate*100/(FatPer+SNFPer)) end,2) as TSRate from (" + Environment.NewLine + _
        "select 'Manual' as Type,TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,substring(TSPL_VLC_DATA_UPLOADER_MASTER.Shift,1,1) as Shift,TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code as MPCode,TSPL_MP_MASTER.MP_Name as MPName,TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code,TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer,TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer ,TSPL_VLC_DATA_UPLOADER_DETAIL.Rate,TSPL_VLC_DATA_UPLOADER_DETAIL.Amount" + Environment.NewLine + _
        "from TSPL_VLC_DATA_UPLOADER_MASTER " + Environment.NewLine + _
        "inner join  TSPL_VLC_DATA_UPLOADER_DETAIL on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code " + Environment.NewLine + _
        "left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_CODE=TSPL_VLC_MASTER_HEAD.VLC_Code " + Environment.NewLine + _
        "left join TSPL_MP_MASTER ON TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code=TSPL_MP_MASTER.MP_Code " + Environment.NewLine + _
        "where 2=2 and TSPL_VLC_MASTER_HEAD.MCC='" + strMCCCode + "' and TSPL_VLC_MASTER_HEAD.VSP_CODE ='" + strVSPCode + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(From_Date), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(To_Date), "dd/MMM/yyyy hh:mm tt") + "'   " + Environment.NewLine + _
        "union all" + Environment.NewLine + _
        "select 'PDU' as Type,TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_VLC_DATA_UPLOADER.Route_No,TSPL_VLC_MASTER_HEAD.VLC_CODE,TSPL_VLC_MASTER_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader" + Environment.NewLine + _
        ",TSPL_VLC_DATA_UPLOADER.Doc_No as Document_Code,TSPL_VLC_DATA_UPLOADER.File_Date as Document_Date,substring(TSPL_VLC_DATA_UPLOADER.shift,1,1) as shift ,coalesce(TSPL_MP_MASTER.MP_CODE,MP1.MP_CODE) AS MPCode,coalesce(TSPL_MP_MASTER.MP_Name,MP1.MP_Name) as MPName  ," + Environment.NewLine + _
        "TSPL_VLC_DATA_UPLOADER.Qty,TSPL_VLC_DATA_UPLOADER.Uom_Code as Unit_Code,TSPL_VLC_DATA_UPLOADER.fat as FATPer,TSPL_VLC_DATA_UPLOADER.snf as SNFPer,TSPL_VLC_DATA_UPLOADER.Rate ,TSPL_VLC_DATA_UPLOADER.Amount  " + Environment.NewLine + _
        "from TSPL_VLC_DATA_UPLOADER " + Environment.NewLine + _
        "left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_DATA_UPLOADER.VLC_CODE=TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code " + Environment.NewLine + _
        "left join TSPL_MP_MASTER ON TSPL_VLC_DATA_UPLOADER.MP_CODE =TSPL_MP_MASTER.MP_Code " + Environment.NewLine + _
        "left join TSPL_MP_MASTER MP1 ON TSPL_VLC_DATA_UPLOADER.MP_CODE =MP1.MP_Code_VLC_Uploader and MP1.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code " + Environment.NewLine + _
        "left join TSPL_MCC_MASTER on TSPL_VLC_DATA_UPLOADER.MCC_Code=TSPL_MCC_MASTER.MCC_Code " + Environment.NewLine + _
        "where 2=2 and  TSPL_VLC_DATA_UPLOADER.File_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(From_Date), "dd/MMM/yyyy hh:mm tt") + "' and  TSPL_VLC_DATA_UPLOADER.File_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(To_Date), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_VLC_DATA_UPLOADER.MCC_Code='" + strMCCCode + "' and TSPL_VLC_MASTER_HEAD.VSP_CODE ='" + strVSPCode + "'" + Environment.NewLine + _
        "union all" + Environment.NewLine + _
        " select 'VSP' as Type,TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD.ROUTE_CODE,TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_MILK_SRN_HEAD.VSP_CODE, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.DOC_CODE as Document_Code,TSPL_MILK_SRN_HEAD.DOC_DATE as Document_Date, substring(TSPL_MILK_SRN_HEAD.SHIFT,1,1) as Shift,'' as MPCode,'' as MPName,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.UOM_Code as Unit_Code,TSPL_MILK_SRN_DETAIL.FAT_PER as FATPer,TSPL_MILK_SRN_DETAIL.SNF_PER as SNFPer,TSPL_MILK_SRN_DETAIL.RATE,TSPL_MILK_SRN_DETAIL.AMOUNT " + Environment.NewLine + _
        "from TSPL_MILK_SRN_DETAIL" + Environment.NewLine + _
        "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
        "left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE" + Environment.NewLine + _
        "where  TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCCode + "' and TSPL_MILK_SRN_HEAD.VSP_CODE='" + strVSPCode + "' and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(From_Date), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(To_Date), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
        ")x" + Environment.NewLine + _
        ") as xxx  " + Environment.NewLine + _
        "left join (Select distinct yyy.FromUOM,yyy.TOUOM,max(CF) as CF From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + "  " + Environment.NewLine + _
        "UNION All " + Environment.NewLine + _
        "Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " " + Environment.NewLine + _
        "UNION All   " + Environment.NewLine + _
        "Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " " + Environment.NewLine + _
        "UNION All " + Environment.NewLine + _
        "Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION where 2=2 " + whrcls + " )as yyy group by yyy.FromUOM,yyy.TOUOM) as Conv on xxx.Unit_Code=Conv.FromUOM and Conv.TOUOM='KG'" + Environment.NewLine + _
        ")xxxx group by VSP_Code,Document_Date,Shift" + Environment.NewLine + _
        ")xxxxx" + Environment.NewLine + _
        ")xxxxxx where (FatPer_MP>FatPer_VSP or SNFPer_MP>SNFPer_VSP)" + Environment.NewLine + _
        ")xxxxxxx"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran))
    End Function

    Public Shared Function GetMPQuery() As String
        Dim qry As String
        Dim qryPDU As String = ""
        Dim qryManual As String = ""
        qryPDU = " select 'PDU' as Type,VLCData.Doc_No,VLCData.Doc_Date as Doc_Date,VLCData.File_Date as File_Date ,VLCData.MCC_Code,VLC.VLC_CODE,VLC.VLC_Code_VLC_Uploader,VLCData.Route_No,coalesce(MP.MP_CODE,MP1.MP_CODE) AS MP_CODE,coalesce(MP.MP_Name,MP1.MP_Name) as MP_Name ,VLCData.shift as [Shift]," & _
              " VLCData.Milk_Type,VLCData.qty as VLC_Qty,VLCData.Uom_Code,VLCData.fat as VLC_Fat,VLCData.snf as VLC_SNF,VLCData.fat_KG,VLCData.snf_KG,VLCData.water as VLC_Water,VLCData.Rate,VLCData.Amount,mcc.Mcc_Code_VLC_Uploader as Mcc_Code_VLC_Uploader,coalesce(MP.MP_Code_VLC_Uploader,MP1.MP_Code_VLC_Uploader) AS MP_Code_Uploader,VLC.VSP_CODE from TSPL_VLC_DATA_UPLOADER VLCData " & _
              " left join TSPL_VLC_MASTER_HEAD VLC on VLCData.VLC_CODE=VLC.VLC_Code_VLC_Uploader and VLC.MCC=VLCData.MCC_Code " & _
              " left join TSPL_MP_MASTER MP ON VLCData.MP_CODE =MP.MP_Code " & _
              " left join TSPL_MP_MASTER MP1 ON VLCData.MP_CODE =MP1.MP_Code_VLC_Uploader " & _
              " left join TSPL_MCC_MASTER MCC on VLCData.MCC_Code=mcc.MCC_Code where 2=2  "


        qryManual = " select MFinal.Type,MFinal.Document_Code,MFinal.Doc_Date as Document_Date,MFinal.Document_Date,MFinal.MCC,MFinal.VLC_Code,MFinal.VLC_Code_VLC_Uploader,MFinal.Route_Code,MFinal.Farmer_Code," & _
                    " MFinal.MP_Name,MFinal.Shift,MFinal.Milk_Type,MFinal.VLC_Qty,MFinal.Unit_Code,MFinal.VLC_Fat,MFinal.VLC_SNF,MFinal.VLC_FAT_KG,MFinal.VLC_SNF_KG,MFinal.VLC_Water,MFinal.Rate,MFinal.Amount,MFinal.Mcc_Code_VLC_Uploader,MFinal.MP_Code_Uploader,MFinal.VSP_CODE from " & _
                    " ( select 'Manual' as Type,VLCM.Document_Code,VLCM.Document_Date as Doc_Date,VLCM.Document_Date as Document_Date,VLC.MCC,VLCM.VLC_Code,VLC.VLC_Code_VLC_Uploader,VLCM.Route_Code,VLCD.Farmer_Code,MP.MP_Name ,VLCM.Shift, " & _
                    " '' as Milk_Type,VLCD.Qty as VLC_Qty,VLCD.Unit_Code,VLCD.FatPer as VLC_Fat,VLCD.SNFPer as VLC_SNF,(VLCD.Qty*VLCD.FatPer/100) as VLC_FAT_KG, " & _
                    " (VLCD.Qty*VLCD.SNFPer/100) as VLC_SNF_KG,null as VLC_Water,VLCD.Rate,VLCD.Amount,'' as Mcc_Code_VLC_Uploader,MP.MP_Code_VLC_Uploader AS MP_Code_Uploader,VLC.VSP_CODE from TSPL_VLC_DATA_UPLOADER_MASTER VLCM " & _
                    " inner join TSPL_VLC_DATA_UPLOADER_DETAIL VLCD on VLCM.Document_Code=VLCD.Document_Code " & _
                    " left join TSPL_VLC_MASTER_HEAD VLC on VLCM.VLC_CODE=VLC.VLC_Code " & _
                    " left join TSPL_MP_MASTER MP ON VLCD.Farmer_Code=MP.MP_Code where 2=2 "


        qryManual = qryManual & ") as MFinal "

        qry = " select Type,Doc_No,Doc_Date,File_Date,MCC_Code,VLC_CODE,VLC_Code_VLC_Uploader,[Shift],UOM_CODE,MCC_CODE_VLC_UPLOADER,VSP_CODE," & _
              " SUM(VLC_Qty) AS VLC_Qty,SUM(FAT_KG) AS FAT_KG,SUM(SNF_KG) AS SNF_KG,SUM(VLC_Water) AS VLC_Water,SUM(Amount) AS Amount from (" & qryPDU & "Union All " & qryManual & ") as Final GROUP BY Type,Doc_No,Doc_Date,File_Date,MCC_Code,VLC_CODE,VLC_Code_VLC_Uploader,[Shift],UOM_CODE,MCC_CODE_VLC_UPLOADER,VSP_CODE "

        Return qry
    End Function
    Public Shared Function GetVSPWithoutMPData(ByVal From_Date As Date, ByVal To_Date As Date, ByVal MCC_CODE As String, ByVal arrVSP As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = GetQuery(From_Date, To_Date, MCC_CODE, arrVSP)
        qry = "Select VSP_Code,Count(*) as Total_Rec from (" & qry & ") MPData group by VSP_Code having Count(*)<=0"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim arr As New ArrayList
        For Each dr As DataRow In dt.Rows
            arr.Add(clsCommon.myCstr(dr.Item("VSP_Code")))
        Next
        Return arr
    End Function

    Public Shared Function GetIrregular_Location(ByVal srn_Code As String, ByVal trans As SqlTransaction)
        Dim qry As String = "select TSPL_MILK_SRN_HEAD  .Mcc_Code" + Environment.NewLine +
        " from TSPL_MILK_SRN_HEAD " + Environment.NewLine +
        " where tspl_Milk_srn_Head.doc_Code='" + srn_Code + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

        ''Changed by balwinder to handle rejection case.
        'Dim strmcc As String = clsDBFuncationality.getSingleValue("select TSPL_MILK_SAMPLE_HEAD.Mcc_Code from TSPL_MILK_SAMPLE_HEAD inner join " _
        ' & " TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE where tspl_Milk_srn_Head.doc_Code='" & srn_Code & "'", trans)
        'Return strmcc
    End Function

    Public Shared Function PostCommissionData(ByVal FormId As String, ByVal strDocNo As String, ByVal obj As clsMilkPurchaseInvoiceMCC, ByVal trans As SqlTransaction) As Boolean

        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select vendor_code,vendor_name from TSPL_VENDOR_MASTER where Vendor_Code=(select Joint_Name from tspl_vendor_Master where vendor_code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PI_HEAD", "DOC_Code", obj.DOC_CODE, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim conv_fac As Decimal = 0
        conv_fac = clsWeightConversionInfo.GetConversion_factor("LTR", "KG", trans)
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
        For Each objTr As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                'Dim qry1 As String = "update TSPL_SRN_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.Qty) + " where SRN_No='" + objTr.SRN_CODE + "' and Item_Code='" + objTr.Item_Code + "'  "
                'clsDBFuncationality.ExecuteNonQuery(qry1, trans)

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.RATE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT", OMInsertOrUpdate.Update, "Item_Code='" + objTr.Item_Code + "' and Source_Doc_No='" + objTr.SRN_CODE + "' and Trans_Type='SRN'", trans)

            End If
        Next



        Dim objVendorInvHead As New clsVedorInvoiceHead()
        'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code")
        objVendorInvHead.Vendor_Name = vendor_name 'DtJoint.Rows(0)("Vendor_name")
        objVendorInvHead.Vendor_Invoice_No = obj.VENDOR_INVOICE_NO
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans) 'obj.MCC_CODE
        objVendorInvHead.Description = "AP Invoice of Commission Against Milk Purchase Invoice-" & obj.DOC_CODE & " For VSP : " & vendor_name
        'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans)) 'DtJoint.Rows(0)("Vendor_Code")
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.VSP_CODE) 'DtJoint.Rows(0)("Vendor_name")
        End If

        objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
        ''objVendorInvHead.PO_Number = obj.p

        '' ''added by priti
        ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
        ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
        '' '' priti ends here
        'objVendorInvHead.Order_No = txtOrderNo.Text
        ' objVendorInvHead.Total_Tax = 0

        objVendorInvHead.On_Hold = False
        'Dim srndate As String = ""
        'Dim srncode As String = ""
        'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
        '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
        '        Dim query As String = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + objTr.SRN_CODE + "' "
        '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"))
        '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
        '    End If
        'Next



        'objVendorInvHead.Description = "Commision of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code") + "/" + DtJoint.Rows(0)("Vendor_name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
        'objVendorInvHead.Tax_Calculation_Type = Nothing
        'objVendorInvHead.Tax_Group = Nothing
        'If (clsCommon.myLen(obj.TAX1) > 0) Then
        '    objVendorInvHead.TAX1 = obj.TAX1
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
        '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
        '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
        '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
        '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX2) > 0) Then
        '    objVendorInvHead.TAX2 = obj.TAX2
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
        '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
        '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
        '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
        '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX3) > 0) Then
        '    objVendorInvHead.TAX3 = obj.TAX3
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
        '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
        '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
        '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
        '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX4) > 0) Then
        '    objVendorInvHead.TAX4 = obj.TAX4
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
        '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
        '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
        '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
        '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX5) > 0) Then
        '    objVendorInvHead.TAX5 = obj.TAX5
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
        '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
        '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

        '    End If
        '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
        '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
        '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX6) > 0) Then
        '    objVendorInvHead.TAX6 = obj.TAX6
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
        '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
        '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
        '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
        '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX7) > 0) Then
        '    objVendorInvHead.TAX7 = obj.TAX7
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
        '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
        '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

        '    End If
        '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
        '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
        '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX8) > 0) Then
        '    objVendorInvHead.TAX8 = obj.TAX8
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
        '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
        '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
        '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
        '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX9) > 0) Then
        '    objVendorInvHead.TAX9 = obj.TAX9
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
        '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
        '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
        '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
        '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX10) > 0) Then
        '    objVendorInvHead.TAX10 = obj.TAX10
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
        '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
        '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
        '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
        '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
        'End If

        'objVendorInvHead.Terms_Code = obj.Terms_Code
        'objVendorInvHead.Terms_Description = obj.TermsName
        objVendorInvHead.Due_Date = obj.DOC_DATE
        objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
        objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
        objVendorInvHead.Amount_Less_Discount = obj.Commission
        objVendorInvHead.Document_Total = obj.Commission
        objVendorInvHead.Balance_Amt = obj.Commission
        'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
        ' objVendorInvHead.RefDocNo = obj.DOC_CODE
        objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
        objVendorInvHead.RefDocType = "MI-CO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Commission_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'If clsCommon.myLen(obj.Irregular_MCC_CODE) > 0 Then
            '    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            '    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Irregular_MCC_CODE, trans)
            '    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
            '        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
            '        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Irregular_MCC_CODE, trans)
            '    End If
            'Else
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
            End If
            'End If

        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

        'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

        'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

        'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

        'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

        'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

        'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

        'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

        'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

        'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

        'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        Dim ii As Integer = 0
        Dim isFirstTime As Boolean = True
        ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
        'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
        objVendorInvHead.Total_Landed_Amt = 0
        For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            Dim strICode As String = objPIDetail.Item_Code
            'If clsCommon.CompairString(objPIDetail.Row_Type, frmGRN.RowTypeMisc) = CompairStringResult.Equal Then
            '    strICode = strFirstItemCode
            'End If

            ''Fill VendorInvoice details Data
            'qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
            'End If
            ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Commission_ACCount"))
            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


            'If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
            '    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
            '    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.MCC_CODE, trans)
            'End If

            'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
            '    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
            '    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PI_Qty
            'End If

            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If

            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            If clsCommon.CompairString(clsCommon.myCstr(objPIDetail.Service_Charge), "Rate/Kg") = CompairStringResult.Equal Then
                objVendorInvDetail.Amount = (objPIDetail.Acc_Qty * objPIDetail.COMMISSION)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objPIDetail.Service_Charge), "Rate/Ltr") = CompairStringResult.Equal Then
                objVendorInvDetail.Amount = ((objPIDetail.Acc_Qty / conv_fac) * objPIDetail.COMMISSION)
            Else
                objVendorInvDetail.Amount = Math.Round(objPIDetail.AMOUNT * objPIDetail.COMMISSION / 100, 2)
            End If

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0

            objVendorInvDetail.Amount_less_Discount = objVendorInvDetail.Amount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = objVendorInvDetail.Amount
            objVendorInvDetail.Landed_Amount = objVendorInvDetail.Amount
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
            ''End of Fill Vendor Invoice Detail Data
        Next

        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If

        If obj.objPIRemittance IsNot Nothing Then
            objVendorInvHead.RemittanceObject = New clsRemittance()
            objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
            objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
            objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
            objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
            objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
            objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
            objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
            objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
            objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
            objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
            objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
            objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
            objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
            objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
            objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
            objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
            objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
            objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
            objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
            objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
            objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
            objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
            objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
            objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
            objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
            objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
            objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
            objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
            objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
            objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
            objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
            objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

            objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
            objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
            objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
            objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
            objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
            objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
            objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
            objVendorInvHead.Balance_Amt = obj.Amount - obj.objPIRemittance.Actual_Total_TDS
            objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code



        End If

        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            ' Throw New Exception("No GL Account Found For AP Invoice")
            Throw New Exception("No Commission GL Account Found For AP Invoice for vendor - " & vendor_name & "")
        End If
        ''multicurrency
        'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
        'objVendorInvHead.ConvRate = 1applicable
        objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") 'obj.DOC_DATE
        ''end multicurrency
        Dim issaved As Boolean = True
        issaved = issaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        issaved = issaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.DOC_DATE)
        Return issaved
    End Function

    Public Shared Function PostIncentiveData(ByVal FormId As String, ByVal strDocNo As String, ByVal vspcode As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal trans As SqlTransaction) As Boolean
        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select vendor_code,vendor_name from TSPL_VENDOR_MASTER where Vendor_Code=(select Joint_Name from tspl_vendor_Master where vendor_code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
        Dim ArrAmount As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(strDocNo, obj.VSP_CODE, obj.MCC_CODE, Frm_date, To_date, False, trans, 0)
        If ArrAmount.Count <= 0 Then
            Return False
        ElseIf ArrAmount(0) <= 0 Then
            Return False
        End If
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PI_HEAD", "DOC_Code", obj.DOC_CODE, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
        For Each objTr As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                'Dim qry1 As String = "update TSPL_SRN_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.Qty) + " where SRN_No='" + objTr.SRN_CODE + "' and Item_Code='" + objTr.Item_Code + "'  "
                'clsDBFuncationality.ExecuteNonQuery(qry1, trans)

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.RATE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT", OMInsertOrUpdate.Update, "Item_Code='" + objTr.Item_Code + "' and Source_Doc_No='" + objTr.SRN_CODE + "' and Trans_Type='SRN'", trans)

            End If
        Next



        Dim objVendorInvHead As New clsVedorInvoiceHead()
        'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code")
        objVendorInvHead.Vendor_Name = vendor_name 'DtJoint.Rows(0)("Vendor_name")
        objVendorInvHead.Vendor_Invoice_No = obj.VENDOR_INVOICE_NO
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans) 'obj.MCC_CODE
        'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans)) 'DtJoint.Rows(0)("Vendor_Code")
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.VSP_CODE) 'DtJoint.Rows(0)("Vendor_name")
        End If

        objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type

        objVendorInvHead.On_Hold = False
        'Dim srndate As String = ""
        'Dim srncode As String = ""
        'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
        '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
        '        Dim query As String = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + objTr.SRN_CODE + "' "
        '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"))
        '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
        '    End If
        'Next



        'objVendorInvHead.Description = "Incentive of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code") + "/" + DtJoint.Rows(0)("Vendor_name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
        objVendorInvHead.Due_Date = obj.DOC_DATE
        objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
        objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
        objVendorInvHead.Amount_Less_Discount = ArrAmount(1) 'obj.Incentive
        objVendorInvHead.Document_Total = ArrAmount(1) 'obj.Incentive
        objVendorInvHead.Balance_Amt = ArrAmount(1) 'obj.Incentive
        'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
        objVendorInvHead.RefDocNo = obj.DOC_CODE
        objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
        objVendorInvHead.RefDocType = "MI-IN"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Incentive_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
            End If
        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If



        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        Dim ii As Integer = 0
        Dim isFirstTime As Boolean = True
        ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
        'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
        objVendorInvHead.Total_Landed_Amt = 0
        For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            Dim strICode As String = objPIDetail.Item_Code
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCount"))
            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            objVendorInvDetail.Amount = objPIDetail.Incentive '(objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = objPIDetail.Incentive '(objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = objPIDetail.Incentive '(objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
            objVendorInvDetail.Landed_Amount = objPIDetail.Incentive '(objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
            'sQuery = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code='" & objPIDetail.SRN_CODE & "'"
            'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            ''End of Fill Vendor Invoice Detail Data
        Next

        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If

        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            'Throw New Exception("No GL Account Found For AP Invoice")
            Throw New Exception("No Incentive GL Account Found For AP Invoice for vendor - " & vendor_name & "")
        End If
        ''multicurrency
        'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
        'objVendorInvHead.ConvRate = 1applicable
        objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") 'obj.DOC_DATE
        ''end multicurrency
        Dim issaved As Boolean = True
        issaved = issaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        issaved = issaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.DOC_DATE)
        Return issaved
    End Function

    Public Shared Function PostIncentiveData_Month_and_Year_Wise(ByVal FormId As String, ByVal strDocNo As List(Of String), ByVal vspcode As String, ByVal Frm_date As Date, ByVal To_date As Date, ByVal trans As SqlTransaction) As Boolean
        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select vendor_code,vendor_name from TSPL_VENDOR_MASTER where Vendor_Code=(select Joint_Name from tspl_vendor_Master where vendor_code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo(0), NavigatorType.Current, trans)
        Dim ArrAmount As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(strDocNo(0), obj.VSP_CODE, obj.MCC_CODE, Frm_date, To_date, True, trans, 0)
        If ArrAmount.Count <= 0 Then
            Return False
        ElseIf ArrAmount(0) <= 0 Then
            Return False
        End If
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PI_HEAD", "DOC_Code", obj.DOC_CODE, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
        For Each objTr As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                'Dim qry1 As String = "update TSPL_SRN_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.Qty) + " where SRN_No='" + objTr.SRN_CODE + "' and Item_Code='" + objTr.Item_Code + "'  "
                'clsDBFuncationality.ExecuteNonQuery(qry1, trans)

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.RATE)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.RATE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT", OMInsertOrUpdate.Update, "Item_Code='" + objTr.Item_Code + "' and Source_Doc_No='" + objTr.SRN_CODE + "' and Trans_Type='SRN'", trans)

            End If
        Next



        Dim objVendorInvHead As New clsVedorInvoiceHead()
        'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code")
        objVendorInvHead.Vendor_Name = vendor_name 'DtJoint.Rows(0)("Vendor_name")
        objVendorInvHead.Vendor_Invoice_No = obj.VENDOR_INVOICE_NO
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans) 'obj.MCC_CODE
        'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans)) 'DtJoint.Rows(0)("Vendor_Code")
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.VSP_CODE) 'DtJoint.Rows(0)("Vendor_name")
        End If

        objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type

        objVendorInvHead.On_Hold = False
        'Dim srndate As String = ""
        'Dim srncode As String = ""
        'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
        '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
        '        Dim query As String = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + objTr.SRN_CODE + "' "
        '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"))
        '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
        '    End If
        'Next
        'Dim Doc_Code As String = ""
        'For Each Str As String In strDocNo
        '    Doc_Code &= IIf(Doc_Code <> "", "," & Str, Str)
        '    'sQuery = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code=(select Distinct srn_Code from tspl_milk_Purchase_Invoice_Detail where doc_Code='" & Str & "')"
        '    'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        'Next

        'objVendorInvHead.Description = "Incentive of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code") + "/" + DtJoint.Rows(0)("Vendor_name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
        objVendorInvHead.Due_Date = obj.DOC_DATE
        objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
        objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
        objVendorInvHead.Amount_Less_Discount = ArrAmount(1) 'obj.Incentive
        objVendorInvHead.Document_Total = ArrAmount(1) 'obj.Incentive
        objVendorInvHead.Balance_Amt = ArrAmount(1) 'obj.Incentive
        'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
        'objVendorInvHead.RefDocNo = obj.DOC_CODE
        'objVendorInvHead.Against_MillkPurchaseInvoice_No = Doc_Code
        objVendorInvHead.RefDocType = "MI-IN"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Incentive_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
            End If
        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If



        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        Dim ii As Integer = 0
        Dim isFirstTime As Boolean = True
        ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
        'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
        objVendorInvHead.Total_Landed_Amt = 0
        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If



        ''multicurrency
        'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
        'objVendorInvHead.ConvRate = 1a7pplicable
        objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") 'obj.DOC_DATE
        ''end multicurrency
        Dim Doc_Code As String = ""
        For Each Str As String In strDocNo
            Doc_Code &= IIf(Doc_Code <> "", "," & Str, Str)
            obj = clsMilkPurchaseInvoiceMCC.GetData(Str, NavigatorType.Current, trans)
            For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
                Dim strICode As String = objPIDetail.Item_Code
                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCount"))
                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                objVendorInvDetail.Amount = (objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = (objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = (objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
                objVendorInvDetail.Landed_Amount = (objPIDetail.Qty * clsCommon.myCdbl(ArrAmount(0)))
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                ''End of Fill Vendor Invoice Detail Data
            Next

        Next
        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            'Throw New Exception("No GL Account Found For AP Invoice")
            Throw New Exception("No Incentive GL Account Found For AP Invoice for vendor - " & vendor_name & "")
        End If
        objVendorInvHead.Description = "Milk Incentive Invoice against Milk Purchase Invoice - " & Doc_Code
        Dim issaved As Boolean = True
        issaved = issaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        issaved = issaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.DOC_DATE)
        issaved = issaved AndAlso clsMilkPurchaseInvoiceMCCDetail.SaveDataIncentive(objVendorInvHead.Document_No, strDocNo, trans)
        Return issaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return PostData(FormId, strDocNo, trans, "", "")
    End Function


    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, ByVal strAPInvNo As String, ByVal strAPInvJENo As String) As Boolean
        Try
            Dim isRecreateAPInvoice As Boolean = False
            If clsCommon.myLen(strAPInvNo) > 0 Then
                If clsCommon.myLen(strAPInvJENo) > 0 Then
                    isRecreateAPInvoice = True
                Else
                    Throw New Exception("Please provice Journal Entry voucher no for AP Invoice Entry No " + strAPInvNo)
                End If
            End If
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Invoice No not found to Post")
            End If
            Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If Not isRecreateAPInvoice Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Invoice", obj.MCC_CODE, obj.DOC_DATE, trans)
                Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PI_HEAD", "DOC_Code", obj.DOC_CODE, trans)
                If isResult = False Then
                    Return False
                End If
                For Each objTr As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
                    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.RATE)
                        clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.RATE)
                        clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.RATE)
                        clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.RATE)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT", OMInsertOrUpdate.Update, "Item_Code='" + objTr.Item_Code + "' and Source_Doc_No='" + objTr.SRN_CODE + "' and Trans_Type='SRN'", trans)
                    End If
                Next
            End If

            Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
            Dim qry As String = ""

            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = obj.VSP_CODE
            objVendorInvHead.Vendor_Name = vendor_name
            objVendorInvHead.Vendor_Invoice_No = obj.VENDOR_INVOICE_NO
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = obj.DOC_DATE
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans) 'obj.MCC_CODE
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + vendor_name)
            End If
            objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
            objVendorInvHead.On_Hold = False
            objVendorInvHead.Due_Date = obj.DOC_DATE
            objVendorInvHead.Discount_Base = 0
            objVendorInvHead.Discount_Amount = 0
            Dim dtbAmt As Decimal = Math.Round(obj.Total_Amount_Acc, 2, MidpointRounding.AwayFromZero)
            objVendorInvHead.RoundOffAmount = 0
            Dim settAutoRounoffSeprateAccount As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoRoundOffSeprateAccountOnVendorTransaction, clsFixedParameterCode.AutoRoundOffSeprateAccountOnVendorTransaction, trans)) = 1)
            If settAutoRounoffSeprateAccount Then
                objVendorInvHead.RoundOffAmount = Math.Round(dtbAmt, 0, MidpointRounding.AwayFromZero) - dtbAmt
                dtbAmt = Math.Round(dtbAmt, 0, MidpointRounding.AwayFromZero)

                '-------------Update Milk Purchase Invoice Amount and Round off amount by balwinder on 27/12/2018 against ticket BHA/20/12/18-000760
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "RoundOffAmount", objVendorInvHead.RoundOffAmount)
                clsCommon.AddColumnsForChange(coll, "Total_Amount_Acc", dtbAmt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_HEAD", OMInsertOrUpdate.Update, "DOC_CODE='" + obj.DOC_CODE + "'", trans)
                '-------------End of Update Milk Purchase Invoice Amount and Round off amount

                objVendorInvHead.RoundOffAmount += obj.Handling_Charges_RO_Amount
            End If
            objVendorInvHead.Amount_Less_Discount = dtbAmt
            objVendorInvHead.Document_Total = dtbAmt
            objVendorInvHead.Balance_Amt = dtbAmt
            objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocType = "MI-PI"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Incentive_ACCOUNT,Handling_Charges from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            Dim strIncentiveAccount As String = ""
            Dim strHandlingAccount As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
                End If
                strIncentiveAccount = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
                strHandlingAccount = clsCommon.myCstr(dt.Rows(0)("Handling_Charges"))
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            If Not settAutoRounoffSeprateAccount Then
                If obj.Handling_Charges_RO_Amount > 0 Then
                    qry = "select Code,Description from tspl_Additional_Charges where Is_RoundOff=1"
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                        Throw New Exception("Please create a addition charge of roundoff type")
                    End If
                    objVendorInvHead.Add_Charge_Code1 = clsCommon.myCstr(dtTemp.Rows(0)("Code"))
                    objVendorInvHead.Add_Charge_Name1 = clsCommon.myCstr(dtTemp.Rows(0)("Description"))
                    objVendorInvHead.Add_Charge_Amt1 = -1 * obj.Handling_Charges_RO_Amount
                    dtTemp = Nothing

                    objVendorInvHead.Total_Add_Charge += objVendorInvHead.Add_Charge_Amt1
                End If
            End If

            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            objVendorInvHead.Total_Landed_Amt = 0
            Dim dblTotIncentiveAmt As Decimal = 0
            For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
                Dim strICode As String = objPIDetail.Item_Code
                ''Fill VendorInvoice details Data
                qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                End If
                Dim strPaybleCleanigCtrlACName As String
                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                If clsCommon.myLen(obj.Irregular_MCC_CODE) > 0 Then
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Irregular_MCC_CODE, trans)
                    strPaybleCleanigCtrlACName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
                Else
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
                    strPaybleCleanigCtrlACName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
                End If
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.MCC_CODE, trans)
                End If
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                objVendorInvDetail.Comments = "Y"
                objVendorInvDetail.Amount = objPIDetail.TOTAL_AMOUNT
                objVendorInvDetail.Discount_Per = Math.Round(clsCommon.myCdbl(objPIDetail.Deduction) * 100 / IIf(clsCommon.myCdbl(objPIDetail.TOTAL_AMOUNT) = 0, 1, clsCommon.myCdbl(objPIDetail.TOTAL_AMOUNT)), 2)
                objVendorInvDetail.Discount = objPIDetail.Deduction
                Dim dblIncentiveAmt As Decimal = Math.Round(clsCommon.myCdbl(objPIDetail.Incentive), 2, MidpointRounding.AwayFromZero)
                dblTotIncentiveAmt += dblIncentiveAmt
                Dim dblAmt As Double = Math.Round(objPIDetail.Net_AMOUNT, 2, MidpointRounding.AwayFromZero) - dblIncentiveAmt - objPIDetail.Handling_Charges_Amount
                objVendorInvDetail.Amount_less_Discount = dblAmt
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = dblAmt
                objVendorInvDetail.Landed_Amount = dblAmt
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                ''End of Fill Vendor Invoice Detail Data
            Next
            If dblTotIncentiveAmt > 0 Then
                If clsCommon.myLen(strIncentiveAccount) <= 0 Then
                    Throw New Exception("Please set Incentive ACCOUNT of vendor account set " + objVendorInvHead.Account_Set)
                End If
                strIncentiveAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strIncentiveAccount, obj.MCC_CODE, trans)
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strIncentiveAccount
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strIncentiveAccount, trans)
                objVendorInvDetail.Amount = obj.Incentive_Head
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = obj.Incentive_Head
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = dblTotIncentiveAmt
                objVendorInvDetail.Landed_Amount = dblTotIncentiveAmt
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
            End If

            If obj.Handling_Charges_Amount > 0 Then
                If clsCommon.myLen(strHandlingAccount) <= 0 Then
                    Throw New Exception("Please set Handling Charges ACCOUNT of vendor account set " + objVendorInvHead.Account_Set)
                End If
                strHandlingAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strHandlingAccount, obj.MCC_CODE, trans)
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strHandlingAccount
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strHandlingAccount, trans)
                objVendorInvDetail.Amount = obj.Handling_Charges_Amount
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = obj.Handling_Charges_Amount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = obj.Handling_Charges_Amount
                objVendorInvDetail.Landed_Amount = obj.Handling_Charges_Amount
                objVendorInvHead.Total_Landed_Amt += obj.Handling_Charges_Amount
                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
            End If

            objVendorInvHead.Empty_Amount = 0
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") 'obj.DOC_DATE

            If obj.objPIRemittance IsNot Nothing Then
                objVendorInvHead.RemittanceObject = New clsRemittance()
                objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
                objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
                objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
                objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
                objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
                objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
                objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
                objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
                objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
                objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
                objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
                objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
                objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
                objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
                objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
                objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS
                objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
                objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
                objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
                objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
                objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
                objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
                objVendorInvHead.Balance_Amt = objVendorInvHead.Balance_Amt - obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
            End If

            If isRecreateAPInvoice Then
                objVendorInvHead.Document_No = strAPInvNo
                objVendorInvHead.SaveData(objVendorInvHead, True, trans, strAPInvJENo)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, strAPInvJENo)
            Else
                If LCase(obj.Commission) > 0 Then
                    PostCommissionData(FormId, strDocNo, obj, trans)
                End If
                clsMilkPurchaseInvoiceMCC.SaveDeductionDebitNoteEntry(obj.DOC_CODE, trans)
                clsMilkPurchaseInvoiceMCC.SaveHeadLoadCreditNoteEntry(obj.DOC_CODE, trans)
                clsMilkPurchaseInvoiceMCC.SaveOwnAssetCreditNoteEntry(obj.DOC_CODE, trans)
                clsMilkPurchaseInvoiceMCC.createDebitNoteMP(obj.DOC_CODE, trans)
                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
            End If
            qry = " Update TSPL_MILK_PURCHASE_INVOICE_HEAD set Posted=1, Posting_Date='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',Modified_By='" + objCommonVar.CurrentUserCode + "' "
            qry += " Where DOC_Code='" + strDocNo + "'"
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MILK_PURCHASE_INVOICE_HEAD", "DOC_CODE", trans)
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetPost(ByVal DocDate As Date, ByVal DOC_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_PURCHASE_INVOICE_HEAD WHERE DOC_CODE='" & DOC_Code & "' AND DOC_DATE='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function SaveDeductionDebitNoteEntry(ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Total_Deduction_Amount <= 0 Then
            Return True
        End If
        If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        'Dim RejLoc As Integer = clsDBFuncationality.getSingleValue("select count(Rejected_Location) from TSPL_LOCATION_MASTER where Location_Code='" & obj.from_location & "'", trans)
        'If RejLoc <= 0 Then
        '    Throw New Exception("Rejected Location Not filled of [" + obj.From_Location + "]")
        'End If
        'Dim Rej_loc As String = clsDBFuncationality.getSingleValue("select Rejected_Location from TSPL_LOCATION_MASTER where Location_Code='" & obj.from_location & "'", trans)
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
        'objVendorInvHead.Document_No = obj.DOC_CODE 'clsDBFuncationality.getSingleValue("select document_no from tspl_vendor_invoice_head where against_Poinvoice_no='" & obj.PI_No & "' and document_type='D'", trans) 'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.VSP_CODE
        objVendorInvHead.Vendor_Name = vendor_name
        objVendorInvHead.Vendor_Invoice_No = obj.DOC_CODE '
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans)

        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans))
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + vendor_name)
        End If
        objVendorInvHead.Document_Type = "D" ''Purchase Return will make Debit Note of PIInvoice
        ''objVendorInvHead.PO_Number = obj.p

        '' ''added by priti
        objVendorInvHead.RefDocType = "Milk_DE"
        objVendorInvHead.RefDocNo = obj.DOC_CODE

        objVendorInvHead.Terms_Code = Nothing 'obj.Terms_Code
        objVendorInvHead.Terms_Description = Nothing 'obj.TermsName
        objVendorInvHead.Due_Date = obj.DOC_DATE
        objVendorInvHead.Discount_Base = Nothing ' obj.Discount_Base
        objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
        objVendorInvHead.Amount_Less_Discount = obj.Total_Deduction_Amount
        objVendorInvHead.Document_Total = obj.Total_Deduction_Amount
        objVendorInvHead.Balance_Amt = obj.Total_Deduction_Amount

        objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Deduction_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
            End If
        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

        'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

        'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

        'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

        'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

        'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

        'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

        'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

        'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

        'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

        'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

        objVendorInvHead.Empty_Amount = 0

        Dim ii As Integer = 0
        For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            ''Fill VendorInvoice details Data
            ''qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"
            'qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"

            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    Throw New Exception("Please set Purchase Account set for item " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ")")
            'End If
            ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If
            ''Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("deduction_Account"))

            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))



            'If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
            '    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
            '    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.MCC_CODE, trans)
            'End If

            ''If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
            ''    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
            ''    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PR_Qty
            ''End If

            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If

            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            objVendorInvDetail.Amount = objPIDetail.Deduction
            objVendorInvDetail.Discount_Per = 0 'objPIDetail.Disc_Per
            objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
            objVendorInvDetail.Amount_less_Discount = objPIDetail.Deduction 'objPIDetail.Amt_Less_Discount
            objVendorInvDetail.Total_Amount = objPIDetail.Deduction
            objVendorInvDetail.Landed_Amount = objPIDetail.Deduction 'objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
            ''End of Fill Vendor Invoice Detail Data
        Next



        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If

        'If obj.objPIRemittance IsNot Nothing Then
        '    objVendorInvHead.RemittanceObject = New clsRemittance()
        '    objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
        '    objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
        '    objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
        '    objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
        '    objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
        '    objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
        '    objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
        '    objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
        '    objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
        '    objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
        '    objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
        '    objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
        '    objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
        '    objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
        '    objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
        '    objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
        '    objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
        '    objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
        '    objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
        '    objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
        '    objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
        '    objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
        '    objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
        '    objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
        '    objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
        '    objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
        '    objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
        '    objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
        '    objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
        '    objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
        '    objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

        '    objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
        '    objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
        '    objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
        '    objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
        '    objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
        '    objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
        '    objVendorInvHead.Balance_Amt = obj.PI_Total_Amt - obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
        'End If
        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            ' Throw New Exception("No GL Account Found For AP Invoice")
            Throw New Exception("No Deduction GL Account Found For AP Invoice for vendor - " & vendor_name & "")
        End If
        isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

        Return isSaved
    End Function
    Shared Function createDebitNoteMP(ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim objtr As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
        Dim dblAmt As Double = 0
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPMPDiffrenceOnTSBasis, clsFixedParameterCode.VSPMPDiffrenceOnTSBasis, trans)) = 1 Then
            dblAmt = Math.Round(clsMilkPurchaseInvoiceMCC.TSBasedAmountForLossByVSP(objtr.MCC_CODE, objtr.VSP_CODE, objtr.FROM_DATE, objtr.TO_DATE, trans), 2, MidpointRounding.ToEven)
        ElseIf objtr.MP_Amount > 0 AndAlso (objtr.Total_Amount_Acc) > (objtr.MP_Amount + objtr.MP_EMP + objtr.MP_Incentive + objtr.MP_IncentiveEMP) Then
            dblAmt = objtr.Total_Amount_Acc - objtr.MP_Amount - objtr.MP_EMP - objtr.MP_Incentive - objtr.MP_IncentiveEMP
        End If
        If dblAmt > 0 Then
            Dim strLocSeg As String = clsLocation.GetSegmentCode(objtr.MCC_CODE, trans)

            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code,Description from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default VSP deduction code")
            End If

            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
            objVendorInvDetail.DeductionDesc = clsCommon.myCstr(dtDed.Rows(0)("Description"))
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objtr.DOC_DATE, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objtr.VSP_CODE
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objtr.VSP_CODE, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = strLocSeg
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against VSP Vs MP Milk Invoice No : " & objtr.DOC_CODE & " .VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p

            '' ''added by priti
            objVendorInvHead.RefDocType = "VSP-MP"
            objVendorInvHead.RefDocNo = objtr.DOC_CODE
            'objVendorInvHead.Ref_SNo = objtr.SAMPLE_NO
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strLocSeg, True, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strLocSeg, True, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



            ''Set AP Invvoice Detail Table

            Dim strInvCtrlAC As String = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set GL Account Code for Deduction Code:" & objVendorInvDetail.DeductionCode)
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strLocSeg, True, trans)




            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmt

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmt
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmt
            objVendorInvDetail.Landed_Amount = dblAmt
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmt
            objVendorInvHead.Discount_Base += dblAmt
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmt
            objVendorInvHead.Document_Total += dblAmt
            objVendorInvHead.Balance_Amt += dblAmt
            ''End of Set AP Invvoice Header Table

            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(objVendorInvHead.Invoice_Entry_Date, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, "", False)
        End If
        Return True
    End Function

    Public Shared Function SaveHeadLoadCreditNoteEntry(ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Total_Head_Load_Amount <= 0 Then
            Return True
        End If
        If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.VSP_CODE
        objVendorInvHead.Vendor_Name = vendor_name
        objVendorInvHead.Vendor_Invoice_No = obj.DOC_CODE
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans)
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans))
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + vendor_name)
        End If
        objVendorInvHead.Document_Type = "C"
        objVendorInvHead.RefDocType = "Milk_HE"
        objVendorInvHead.RefDocNo = obj.DOC_CODE

        objVendorInvHead.Terms_Code = Nothing
        objVendorInvHead.Terms_Description = Nothing
        objVendorInvHead.Due_Date = obj.DOC_DATE
        objVendorInvHead.Discount_Base = obj.Total_Head_Load_Amount + obj.Total_Head_Load_RO_Amount
        objVendorInvHead.Discount_Amount = 0
        objVendorInvHead.Amount_Less_Discount = objVendorInvHead.Discount_Base
        objVendorInvHead.Document_Total = obj.Total_Head_Load_Amount
        objVendorInvHead.Balance_Amt = obj.Total_Head_Load_Amount

        objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Head_Load_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
            End If
        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        If obj.Total_Head_Load_RO_Amount > 0 Then
            qry = "select Code,Description from tspl_Additional_Charges where Is_RoundOff=1"
            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                Throw New Exception("Please create a addition charge of roundoff type")
            End If
            objVendorInvHead.Add_Charge_Code1 = clsCommon.myCstr(dtTemp.Rows(0)("Code"))
            objVendorInvHead.Add_Charge_Name1 = clsCommon.myCstr(dtTemp.Rows(0)("Description"))
            objVendorInvHead.Add_Charge_Amt1 = -1 * obj.Total_Head_Load_RO_Amount
            dtTemp = Nothing

            objVendorInvHead.Total_Add_Charge += objVendorInvHead.Add_Charge_Amt1
        End If

        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

        objVendorInvHead.Empty_Amount = 0

        Dim ii As Integer = 0
        For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Head_Load_Account"))
            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            objVendorInvDetail.Amount = objPIDetail.Head_Load_Amount
            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = objPIDetail.Head_Load_Amount
            objVendorInvDetail.Total_Amount = objPIDetail.Head_Load_Amount
            objVendorInvDetail.Landed_Amount = objPIDetail.Head_Load_Amount
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
        Next
        objVendorInvHead.Empty_Amount = 0
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If
        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            Throw New Exception("No Head Load GL Account Found For AP Invoice for vendor - " & vendor_name & "")
        End If
        isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        Return isSaved
    End Function

    Public Shared Function SaveOwnAssetCreditNoteEntry(ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Total_Own_Asset_Amount <= 0 Then
            Return True
        End If
        If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        'Dim RejLoc As Integer = clsDBFuncationality.getSingleValue("select count(Rejected_Location) from TSPL_LOCATION_MASTER where Location_Code='" & obj.from_location & "'", trans)
        'If RejLoc <= 0 Then
        '    Throw New Exception("Rejected Location Not filled of [" + obj.From_Location + "]")
        'End If
        'Dim Rej_loc As String = clsDBFuncationality.getSingleValue("select Rejected_Location from TSPL_LOCATION_MASTER where Location_Code='" & obj.from_location & "'", trans)
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
        'objVendorInvHead.Document_No = obj.DOC_CODE 'clsDBFuncationality.getSingleValue("select document_no from tspl_vendor_invoice_head where against_Poinvoice_no='" & obj.PI_No & "' and document_type='D'", trans) 'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.VSP_CODE
        objVendorInvHead.Vendor_Name = vendor_name
        objVendorInvHead.Vendor_Invoice_No = obj.DOC_CODE '
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans)
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.VSP_CODE + "'", trans))
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + vendor_name)
        End If
        objVendorInvHead.Document_Type = "C" ''Purchase Return will make Debit Note of PIInvoice
        ''objVendorInvHead.PO_Number = obj.p

        '' ''added by priti
        objVendorInvHead.RefDocType = "Milk_OW"
        objVendorInvHead.RefDocNo = obj.DOC_CODE

        objVendorInvHead.Terms_Code = Nothing 'obj.Terms_Code
        objVendorInvHead.Terms_Description = Nothing 'obj.TermsName
        objVendorInvHead.Due_Date = obj.DOC_DATE
        objVendorInvHead.Discount_Base = Nothing ' obj.Discount_Base
        objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
        objVendorInvHead.Amount_Less_Discount = obj.Total_Own_Asset_Amount
        objVendorInvHead.Document_Total = obj.Total_Own_Asset_Amount
        objVendorInvHead.Balance_Amt = obj.Total_Own_Asset_Amount

        objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Own_Asset_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
            End If
        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

        'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

        'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

        'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

        'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

        'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

        'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

        'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

        'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

        'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

        'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

        objVendorInvHead.Empty_Amount = 0

        Dim ii As Integer = 0
        For Each objPIDetail As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
            ''Fill VendorInvoice details Data
            ''qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"
            'qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"

            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    Throw New Exception("Please set Purchase Account set for item " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ")")
            'End If
            ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If
            ''Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Own_Asset_Account"))

            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))



            'If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
            '    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
            '    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.MCC_CODE, trans)
            'End If

            ''If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
            ''    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
            ''    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PR_Qty
            ''End If

            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If

            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            objVendorInvDetail.Amount = objPIDetail.Own_Asset_Amount
            objVendorInvDetail.Discount_Per = 0 'objPIDetail.Disc_Per
            objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
            objVendorInvDetail.Amount_less_Discount = objPIDetail.Own_Asset_Amount 'objPIDetail.Amt_Less_Discount
            objVendorInvDetail.Total_Amount = objPIDetail.Own_Asset_Amount
            objVendorInvDetail.Landed_Amount = objPIDetail.Own_Asset_Amount 'objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
            ''End of Fill Vendor Invoice Detail Data
        Next



        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If

        'If obj.objPIRemittance IsNot Nothing Then
        '    objVendorInvHead.RemittanceObject = New clsRemittance()
        '    objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
        '    objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
        '    objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
        '    objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
        '    objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
        '    objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
        '    objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
        '    objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
        '    objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
        '    objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
        '    objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
        '    objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
        '    objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
        '    objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
        '    objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
        '    objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
        '    objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
        '    objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
        '    objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
        '    objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
        '    objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
        '    objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
        '    objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
        '    objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
        '    objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
        '    objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
        '    objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
        '    objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
        '    objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
        '    objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
        '    objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

        '    objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
        '    objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
        '    objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
        '    objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
        '    objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
        '    objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
        '    objVendorInvHead.Balance_Amt = obj.PI_Total_Amt - obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
        'End If
        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            ' Throw New Exception("No GL Account Found For AP Invoice")
            Throw New Exception("No Own Asset GL Account Found For AP Invoice for vendor - " & vendor_name & "")
        End If
        isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

        Return isSaved
    End Function
    ''To be Uncomment
    'Public Sub SelectMilkSRNItemsForVspPayment(ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction)
    '    Dim obj_SRN As New clsMilkSRNMCC
    '    Dim frm As New frmMILKPendingSRN()
    '    frm.VendorCode = Vsp_Name
    '    'frm.strCurrCode = FndSRNNO.Value
    '    frm.Frm_date = frm_date
    '    frm.To_date = End_date
    '    Dim StrDoc As New List(Of String)
    '    If Is_With_Bill Then
    '        If Not frm.LoaDHeadDataQuery(trans) Then
    '            Exit Sub
    '        End If
    '    Else
    '        If Not frm.LoaDHeadDataQueryVsp(trans) Then
    '            Exit Sub
    '        End If
    '    End If
    '    'frm.ShowDialog()
    '    'For Each Get_srn_no As String In strSRN_No
    '    For Each row As GridViewRowInfo In frm.gvHead.Rows()
    '        If strSRN_No.Contains(clsCommon.myCstr(row.Cells(frmMILKPendingSRN.colHCode).Value)) Then
    '            frm.gvHead.CurrentRow = row
    '            row.Cells(frmMILKPendingSRN.colHSelect).Value = True
    '            'frm.LoadDetailData(True, clsCommon.myCstr(row.Cells(frm.colHCode).Value))
    '        End If
    '    Next
    '    'Next
    '    frm.btnOKPressed()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '            obj_SRN = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
    '            If obj_SRN IsNot Nothing AndAlso clsCommon.myLen(obj_SRN.DOC_CODE) > 0 Then
    '                '            txtCode.Value = obj.DOC_CODE
    '                '  If dtpDocDate.MinDate < obj.DOC_DATE Then
    '                '      dtpDocDate.MinDate = obj.DOC_DATE
    '                '  End If
    '                '  FndMccCode.Value = obj.MCC_CODE
    '                '  'If clsCommon.myLen(obj.MCC_CODE) > 0 Then
    '                '  '    Payment_Cycle_value = clsDBFuncationality.getSingleValue("SELECT payment_cycle from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
    '                '  'End If
    '                '  DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(FndMccCode.Value) & "'", trans)
    '                '  lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")

    '                '  'dtpDocDate.Value = obj.DOC_DATE

    '                '  FndSRNNO.Value = ""
    '                '  fndVSPCode.Value = obj.VSP_CODE

    '                '  txtPayment.Text = clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                '  fndRouteCOde.Text = obj.ROUTE_CODE


    '                '  lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'", trans)
    '                '  ' If LCase(txtPayment.Text) = "different" Then
    '                '  '   lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select joint_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.text & "'")
    '                '  'Else
    '                '  lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                '  'End If

    '                '  'If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
    '                '  LoadBlankGridVSpPay()
    '                '  ' Dim sQuery As String = "select * from TSPL_MILK_Shift_End_DETAIL where  MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                '  '& "and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"
    '                '  Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                '& "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"

    '                '  Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                '  For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                '      gv1.Rows.AddNew()

    '                '      FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.DOC_CODE, FndSRNNO.Value & "," & obj1.DOC_CODE)
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
    '                '      ' gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj1.MILK_Qty
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colAcc_Qty).Value = obj1.ACC_Qty
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge_Type


    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = 0 '0
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveEMP).Value = 0
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_CODE
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc

    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.DOC_CODE
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj.DOC_DATE
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.AMOUNT
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj.VEHICLE_CODE
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VlC_Code
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colHead_Load_Amount).Value = obj1.Head_Load_Amount
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colOwn_Asset_Amount).Value = obj1.Own_Asset_Amount

    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission
    '                '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
    '                '      If DtShiftEnd.Rows.Count > 0 Then
    '                '          Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "' and srn_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value) & "'")
    '                '          'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
    '                '          If dr.Length > 0 Then
    '                '              gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                '          End If
    '                '      End If
    '                '      If Not Is_With_Bill Then
    '                '          If Not StrDoc.Contains(obj1.Invoice_Code) Then
    '                '              StrDoc.Add(obj1.Invoice_Code)
    '                '          End If
    '                '      End If
    '                '      ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = 0
    '                '  Next
    '                'Else
    '                '    gv1.Rows.AddNew()
    '                'End If



    '                'If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
    '                '    For ii As Integer = 0 To gv1.RowCount - 1
    '                '        UpdateCurrentRow(ii)
    '                '    Next
    '                'End If
    '                Dim objHead As clsMilkPurchaseInvoiceMCC
    '                Dim TotHeadLoad As Double = 0
    '                Dim TotOwnAsset As Double = 0
    '                Dim TotDeduction_Amount As Double = 0
    '                '' asign screen vaules in objHead
    '                objHead = New clsMilkPurchaseInvoiceMCC
    '                objHead.DOC_CODE = ""
    '                objHead.DOC_DATE = clsCommon.myCDate(End_date) 'obj_SRN.DOC_DATE)
    '                objHead.Description = ""
    '                objHead.ROUTE_CODE = clsCommon.myCstr(obj_SRN.ROUTE_CODE)
    '                objHead.VSP_CODE = clsCommon.myCstr(obj_SRN.VSP_CODE)
    '                objHead.Payment = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj_SRN.VSP_CODE & "'", trans))
    '                objHead.Irregular_MCC_CODE = clsCommon.myCstr(obj_SRN.MCC_CODE)



    '                Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)

    '                Dim obj As clsMilkPurchaseInvoiceMCCDetail = Nothing
    '                Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj_SRN.MCC_CODE) & "' " _
    '             & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj_SRN.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj_SRN.SHIFT) = "M", "Morning", "Evening") & "'"

    '                Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)

    '                '========================Total==================
    '                Dim totAmount As Double = 0
    '                Dim totCommssion As Double = 0
    '                Dim totPaymentCommssion As Double = 0
    '                Dim totAmountwithPaymentCommssion As Double = 0
    '                Dim totAmountIncentive As Double = 0
    '                Dim totAmountIncentiveEMP As Double = 0
    '                Dim totBasicAmount As Double = 0

    '                '==============================================
    '                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                    obj = New clsMilkPurchaseInvoiceMCCDetail
    '                    obj.DOC_CODE = ""
    '                    obj.AMOUNT = clsCommon.myCdbl(obj1.AMOUNT)
    '                    obj.Cans = clsCommon.myCdbl(obj1.Cans)
    '                    obj.CLR = clsCommon.myCdbl(obj1.CLR)
    '                    obj.COMMISSION = clsCommon.myCdbl(obj1.Commission)
    '                    obj.Payment_COMMISSION = clsCommon.myCdbl(obj1.Payment_Commission)
    '                    If DtShiftEnd.Rows.Count > 0 Then
    '                        Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(obj1.VlC_Code) & "' and srn_code='" & clsCommon.myCstr(obj1.DOC_CODE) & "'")
    '                        'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
    '                        If dr.Length > 0 Then
    '                            obj.Deduction = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                        End If
    '                    End If
    '                    'obj.Deduction = clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                    obj.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
    '                    obj.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
    '                    obj.Correction_Factor = clsCommon.myCdbl(obj1.Correction_Factor)
    '                    obj.FAT_PER = clsCommon.myCdbl(obj1.FAT)
    '                    'obj.Incentive = clsCommon.myCdbl(grow.Cells(colIncentive).Value)
    '                    'obj.IncentiveEMP = clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)
    '                    obj.Item_Code = clsCommon.myCstr(obj1.Item_CODE)
    '                    obj.MCC_CODE = obj1.MCC_CODE
    '                    obj.Qty = clsCommon.myCdbl(obj1.MILK_Qty)
    '                    obj.Acc_Qty = clsCommon.myCdbl(obj1.ACC_Qty)
    '                    obj.Service_Charge = clsCommon.myCstr(obj1.Service_Charge_Type)
    '                    obj.RATE = clsCommon.myCdbl(obj1.RATE)
    '                    obj.SNF_PER = clsCommon.myCdbl(obj1.SNF)
    '                    obj.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
    '                    'obj.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
    '                    '=====================================
    '                    Dim Commission_AMount As Double = 0
    '                    Dim Payment_Commission_AMount As Double = 0
    '                    If clsCommon.myCstr(obj1.Service_Charge_Type) = "%(Percentage)" Then
    '                        'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100
    '                        'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100
    '                        'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

    '                        Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Commission) / 100, 2)
    '                        Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                        ' obj1.i = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                    ElseIf clsCommon.myCstr(obj1.Service_Charge_Type) = "Rate/Kg" Then
    '                        'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colCOMMISSION).Value
    '                        'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colPaymentCOMMISSION).Value
    '                        'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

    '                        Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.ACC_Qty) * clsCommon.myCdbl(obj1.Commission), 2)
    '                        Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.ACC_Qty) * clsCommon.myCdbl(obj1.Payment_Commission), 2)
    '                    ElseIf clsCommon.myCstr(obj1.Service_Charge_Type) = "Rate/Ltr" And clsCommon.myCstr(obj1.UOM) = "LTR" Then
    '                        'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colCOMMISSION).Value
    '                        'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colPaymentCOMMISSION).Value
    '                        'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

    '                        Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.MILK_Qty) * clsCommon.myCdbl(obj1.Commission), 2)
    '                        Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.MILK_Qty) * clsCommon.myCdbl(obj1.Payment_Commission), 2)
    '                    End If
    '                    'grow.Cells(colDocument_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                    'grow.Cells(colTOTAL_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                    'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
    '                    'grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))

    '                    'obj.do = clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                    obj.TOTAL_AMOUNT = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount), 2) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                    'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
    '                    obj.Net_AMOUNT = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount), 2) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))

    '                    '===============================================================
    '                    obj.SRN_CODE = clsCommon.myCstr(obj1.DOC_CODE)
    '                    obj.TOTAL_AMOUNT = clsCommon.myCdbl(obj.TOTAL_AMOUNT)
    '                    obj.VEHICLE_NO = clsCommon.myCstr(obj_SRN.VEHICLE_CODE)
    '                    obj.VLC_NO = clsCommon.myCstr(obj1.VlC_Code)
    '                    obj.Net_AMOUNT = clsCommon.myCdbl(obj.Net_AMOUNT)
    '                    obj1.NET_AMOUNT = clsCommon.myCdbl(obj.Net_AMOUNT)
    '                    objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
    '                    obj.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
    '                    If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
    '                        objHead.Irregular_MCC_CODE = ""
    '                    End If

    '                    objList.Add(obj)
    '                    TotHeadLoad += obj.Head_Load_Amount
    '                    TotOwnAsset += obj.Own_Asset_Amount
    '                    TotDeduction_Amount += obj.Deduction

    '                    totAmount += obj.AMOUNT
    '                    totBasicAmount += obj.AMOUNT
    '                    totCommssion += Commission_AMount
    '                    totPaymentCommssion += Payment_Commission_AMount
    '                    totAmountwithPaymentCommssion += obj.Net_AMOUNT
    '                Next
    '                objHead.Head_Load_Amount = TotHeadLoad
    '                objHead.Own_Asset_Amount = TotOwnAsset
    '                objHead.Deduction_Amount = TotDeduction_Amount


    '                objHead.VENDOR_INVOICE_NO = "" 'clsCommon.myCstr(TxtVendorInvoiceNo.Text)
    '                objHead.VENDOR_INVOICE_DATE = obj_SRN.DOC_DATE 'clsCommon.myCDate(VENDOR_INVOICE_DATE.Value)
    '                objHead.Amount = clsCommon.myCdbl(totAmount)
    '                objHead.Basic_Amount = Math.Round(clsCommon.myCdbl(totAmount) - clsCommon.myCdbl(totCommssion), 2)
    '                objHead.Commission = clsCommon.myCdbl(totCommssion)
    '                objHead.ACC_Amount = clsCommon.myCdbl(totAmountwithPaymentCommssion)
    '                objHead.PaymentCommission = clsCommon.myCdbl(totPaymentCommssion)

    '                'End If
    '                'Next
    '                ' ''For Custom Fields
    '                ''Dim obj As New clsMilkPurchaseInvoiceMCC()
    '                'obj = New clsMilkPurchaseInvoiceMCC
    '                'obj.Form_ID = MyBase.Form_ID
    '                'obj.arrCustomFields = New List(Of clsCustomFieldValues)
    '                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
    '                '    UcCustomFields1.GetData(obj.arrCustomFields)
    '                'End If
    '                'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
    '                '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
    '                'End If
    '                ' ''End of For Custom Fields

    '                If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList, trans) Then
    '                    'trans.Commit()
    '                    'Dim transs As SqlTransaction
    '                    'UcAttachment1.SaveData(objHead.DOC_CODE)
    '                    Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
    '                    Dim totincentiveEMP As Double = 0
    '                    Dim totincentive As Double = 0
    '                    totAmount = 0
    '                    totBasicAmount = 0
    '                    totAmountwithPaymentCommssion = 0
    '                    Dim is_processed As Integer = 0
    '                    Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & obj.MCC_CODE & "'", trans)
    '                    If incentive.Count > 0 Then
    '                        If incentive(1) > 0 Then
    '                            For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                                If is_processed = 0 Then
    '                                    totincentiveEMP = Math.Round(clsCommon.myCdbl(incentive(1)) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                                    totAmount += obj.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                    totBasicAmount += obj.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                    obj.Net_AMOUNT += +IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                    totAmountwithPaymentCommssion += obj.Net_AMOUNT '+ totincentiveEMP '+ incentive(1)
    '                                    sQuery = "Update tspl_Milk_Purchase_Invoice_Detail set Total_Amount='" & clsCommon.myCdbl(obj.AMOUNT) & "',Total_Amount_Acc='" & clsCommon.myCdbl(obj.Net_AMOUNT) & "',Net_Amount='" & clsCommon.myCdbl(obj.Net_AMOUNT) & "',incentive='" & incentive(1) & "' , incentiveEMP='" & totincentiveEMP & "' where srn_code='" & obj.SRN_CODE & "'"
    '                                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                                    is_processed = 1
    '                                End If
    '                                'Exit For
    '                            Next
    '                            is_processed = 0
    '                            totAmount = objHead.Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                            totAmountwithPaymentCommssion = objHead.ACC_Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)

    '                            'totincentiveEMP += clsCommon.myCdbl(gv1.Rows(0).Cells(colIncentiveEMP).Value)
    '                            sQuery = "select * from tspl_Milk_Purchase_Invoice_Head where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                            'sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount=convert(Total_Amount as float)+" & clsCommon.myCdbl(totAmount) & ",Total_Amount_Acc=convert(Total_Amount_Acc  as float)+" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & " ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                            sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount='" & clsCommon.myCdbl(totAmount) & "',Total_Amount_Acc='" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                        End If
    '                    End If
    '                    clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", objHead.DOC_CODE, trans)
    '                End If
    '                ' Return True
    '            End If


    '        End If
    '    End If

    'End Sub
End Class


Public Class clsMilkPurchaseInvoiceMCCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public SRN_CODE As String
    Public SRN_DATE As Date
    Public Item_Code As String
    Public Item_Desc As String
    Public UOM As String
    Public Qty As Double
    Public Acc_Qty As Double
    'Public Acc_Qty_Ltr As Double
    Public FAT_PER As Double
    Public SNF_PER As Double
    Public MCC_CODE As String
    Public VEHICLE_NO As String
    Public VLC_NO As String
    Public Cans As String
    Public Correction_Factor As String
    Public CLR As Double
    Public RATE As Double
    Public AMOUNT As Double
    Public Net_AMOUNT As Double
    Public Incentive As Double
    Public IncentiveEMP As Double
    Public COMMISSION As Double
    Public Service_Charge As String
    Public Payment_COMMISSION As Double
    Public Deduction As Double
    Public Head_Load_Amount As Double
    Public Own_Asset_Amount As Double
    Public TOTAL_AMOUNT As Double
    Public TOTAL_AMOUNT_Acc As Double
    Public Service_Charge_Amount As Double
    Public Handling_Charges_Amount As Decimal
    Public SRN_Net_Amount As Decimal
    Public SRN_RO_Amount As Decimal
#End Region


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

                ' If obj.VLC_DOC_CODE = "" Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                'Else
                'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE='" + strDocNo + "' and TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_DOC_CODE='" & vlcDoc(0) & "'", trans)
                'End If

            Next

        End If

        Return True
    End Function

    Public Shared Function SaveDataIncentive(ByVal strDocNo As String, ByVal Arr As List(Of String), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "AP_Invoice_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Milk_Purchase_Invoice_CODE", obj)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Purchase_Invoice_Incentive_Detail", OMInsertOrUpdate.Insert, "TSPL_Milk_Purchase_Invoice_Incentive_Detail.AP_Invoice_Code='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function

End Class



